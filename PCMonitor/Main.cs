using System;
using System.Windows.Forms;
using System.IO.Ports;
using OpenHardwareMonitor.Hardware;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace PCMonitor
{
	public partial class Main : Form
	{
		const int Baud = 115200;
		// traverses OHWM data
		public class UpdateVisitor : IVisitor
		{
			public void VisitComputer(IComputer computer)
			{
				computer.Traverse(this);
			}
			public void VisitHardware(IHardware hardware)
			{
				hardware.Update();
				foreach (IHardware subHardware in hardware.SubHardware) 
					subHardware.Accept(this);
			}
			public void VisitSensor(ISensor sensor) { }
			public void VisitParameter(IParameter parameter) { }
		}
		ISensor FindSensor(string identifier)
		{
			try
			{
				return FindSensor(identifier, _computer);
			}
			catch { }
			return null;
		}
		static ISensor FindSensor(string identifier, object current)
		{
			if (current == null || string.IsNullOrEmpty(identifier))
			{
				return null;
			}
			if (current is IComputer)
			{
				var comp = (IComputer)current;
				foreach (IHardware hardware in comp.Hardware)
				{
					var result = FindSensor(identifier, hardware);
					if (result != null)
					{
						return result;
					}
				}
			}
			else if (current is IHardware)
			{
				var hardware = (IHardware)current;
				foreach (ISensor result in hardware.Sensors)
				{
					if (result.Identifier.ToString() == identifier)
					{
						return result;
					}
				}
				foreach (IHardware subhardware in hardware.SubHardware)
				{
					var result = FindSensor(identifier, subhardware);
					if (result != null)
					{
						return result;
					}
				}
			}
			return null;
		}
		float GetSensorValue(ConfigEntry entry)
		{
			var sensor = FindSensor(entry.Path);
			if (sensor == null)
			{
				return float.NaN;
			}
			if (!sensor.Value.HasValue)
			{
				return float.NaN;
			}
			return sensor.Value.GetValueOrDefault();
		}
		
		Config[] _configs;
		
		// local members for system info
		float cpuUsage;
		string cpuUsageId;
		float gpuUsage;
		string gpuUsageId;
		float cpuTemp;
		string cpuTempId;
		float cpuTjMax;
		float gpuTemp;
		string gpuTempId;
		bool isNvidia;
		private readonly Computer _computer = new Computer
		{
			CPUEnabled = true,
			GPUEnabled = true
		};
		// Populates the PortBox control with COM ports
		void RefreshConfig()
		{
			StartedCheckBox.Checked = false;
			if(File.Exists("pcmonitor.json"))
			{
				using(var reader = new StreamReader("pcmonitor.json"))
				{
					_configs = Config.ReadFrom(reader);
				}
				foreach(var cfg in _configs)
				{
					cfg.Port = new SerialPort(cfg.PortName, Baud);
				}
			}
		}
		void _ClosePort(Config cfg)
		{
			try
			{
				if (cfg.Port.IsOpen)
				{
					cfg.Port.Close();
					Log.AppendText("Closed " + cfg.PortName + Environment.NewLine);
				}
			}
			catch { }
		}
		public static void StructToBytes(object value, byte[] data, int startIndex)
		{
			var size = Marshal.SizeOf(value.GetType());
			var ptr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(value, ptr, false);
			var ba = new byte[size];
			Marshal.Copy(ptr,data, startIndex, size);
			Marshal.FreeHGlobal(ptr);
		}
		private void UpdateTimer_Tick(object sender, EventArgs e)
		{
			// use OpenHardwareMonitorLib to collect the system info
			var updateVisitor = new UpdateVisitor();
			_computer.Accept(updateVisitor);
			// go through all the ports
			int i = 0;
			if (_configs != null) {
				foreach (var cfg in _configs)
				{
					SerialPort port = cfg.Port;
					if (port != null)
					{
						if (!port.IsOpen)
						{
							port.Open();
							Log.AppendText("Opened " + cfg.PortName + " (lazy open)"+Environment.NewLine);
							port.DataReceived += Port_DataReceived;
						}
						ScreenPacket data = default;
						int psize = Marshal.SizeOf(data);
						var ba = new byte[2 + (psize * cfg.Entries.Count)];
						ba[0] = 1;
						ba[1] = (byte)cfg.Entries.Count;
						int si = 2;
						for (int j = 0; j < cfg.Entries.Count; ++j)
						{
							// put it in the struct for sending
							ConfigEntry cfge = cfg.Entries[j];
							data.Format = cfge.Format;
							data.ValueMax = cfge.ValueMax;
							data.Icon = cfge.IconData;
							data.Colors = new ushort[2];
							data.Colors[0] = cfge.ColorStartRgb565;
							data.Colors[1] = cfge.ColorEndRgb565;
							data.HsvColor = cfge.ColorHsv;
							data.Value = GetSensorValue(cfge);
							StructToBytes(data, ba, si);
							si += psize;
						}
						port.Write(ba, 0, ba.Length);
						port.BaseStream.Flush();
					}
					++i;
				}
			}
			
		}
		void CollectSystemInfo()
		{
			// use OpenHardwareMonitorLib to collect the system info
			var updateVisitor = new UpdateVisitor();
			_computer.Accept(updateVisitor);
			isNvidia = false;
			for (int i = 0; i < _computer.Hardware.Length; i++)
			{
				if (_computer.Hardware[i].HardwareType == HardwareType.CPU)
				{
					for (int j = 0; j < _computer.Hardware[i].Sensors.Length; j++)
					{
						var sensor = _computer.Hardware[i].Sensors[j];
						if (sensor.SensorType == SensorType.Temperature && 
							sensor.Name.Contains("CPU Package"))
						{
							for (int k = 0; k < sensor.Parameters.Length; ++k)
							{
								var p = sensor.Parameters[i];
								if (p.Name.ToLowerInvariant().Contains("tjmax"))
								{
									cpuTjMax = (float)p.Value;
								}
							}
							cpuTemp = sensor.Value.GetValueOrDefault();
							cpuTempId = sensor.Identifier.ToString();
						}
						else if (sensor.SensorType == SensorType.Load && 
							sensor.Name.Contains("CPU Total"))
						{
							// store
							cpuUsage = sensor.Value.GetValueOrDefault();
							cpuUsageId = sensor.Identifier.ToString();
						}
					}
				}
				if (_computer.Hardware[i].HardwareType == HardwareType.GpuAti || 
					_computer.Hardware[i].HardwareType == HardwareType.GpuNvidia)
				{
					isNvidia = _computer.Hardware[i].HardwareType == HardwareType.GpuNvidia;
					for (int j = 0; j < _computer.Hardware[i].Sensors.Length; j++)
					{
						var sensor = _computer.Hardware[i].Sensors[j];
						if (sensor.SensorType == SensorType.Temperature && 
							sensor.Name.Contains("GPU Core"))
						{
							// store
							gpuTemp = sensor.Value.GetValueOrDefault();
							gpuTempId = sensor.Identifier.ToString();
						}
						else if (sensor.SensorType == SensorType.Load && 
							sensor.Name.Contains("GPU Core"))
						{
							// store
							gpuUsage = sensor.Value.GetValueOrDefault();
							gpuUsageId = sensor.Identifier.ToString();
						}
					}
				}
			}
		}
		protected override void OnClosed(EventArgs e)
		{
			base.OnClosed(e);
			if (_configs != null)
			{
				foreach (var cfg in _configs)
				{
					try
					{
						if (cfg.Port.IsOpen)
						{
							cfg.Port.Close();
						}
					}
					catch { }
				}
			}
			_computer.Close();
		}
		
		private void Main_Resize(object sender, EventArgs e)
		{
			// hide the window to the tray on minimize
			if (WindowState == FormWindowState.Minimized)
			{
				Hide();
				Notify.Visible = true;
			}
		}

		private void Notify_Click(object sender, EventArgs e)
		{
			// show the window on tray click
			Show();
			WindowState = FormWindowState.Normal;
			Activate();
		}


		public Main()
		{
			InitializeComponent();
			_computer.Open();
			// we need paths
			CollectSystemInfo();
			Notify.Icon = System.Drawing.SystemIcons.Information;
			Show();
			RefreshConfig();
		}

		private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				string str = null;
				var port = ((SerialPort)sender);
				var ba = new byte[port.BytesToRead];
				if(ba.Length>0)
				{
					port.Read(ba,0, ba.Length);
					str = Encoding.ASCII.GetString(ba,0,ba.Length);
				}
				Invoke(new Action(() =>
				{
					if (str != null)
					{
						Log.AppendText(str);
					}
				}));
			}
			catch { }
			
		}

		private void StartedCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateTimer.Enabled = StartedCheckBox.Checked;
			if(!StartedCheckBox.Checked)
			{
				if (_configs != null)
				{
					foreach (var cfg in _configs)
					{
						_ClosePort(cfg);
					}
				}
			}
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			RefreshConfig();
		}
	}
}
