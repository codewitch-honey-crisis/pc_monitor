using OpenHardwareMonitor.Hardware;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMonitor
{
	public partial class EditMonitor : Form
	{
		List<Config> _configs;
		Config _config;
		Computer _computer;
		internal EditMonitor(List<Config> configs, Config config, Computer computer)
		{
			_configs = configs;
			_config = config;
			_computer = computer;
			InitializeComponent();
			RefreshPortList();
			RefreshEntryList();
		}
		void RefreshPortList()
		{
			PortComboBox.Items.Clear();
			var names = SerialPort.GetPortNames();
			for(var i = 0;i<names.Length;++i)
			{
				var found = false;
				for(var j = 0;j<_configs.Count;++j)
				{
					var cfg = _configs[j];
					if(cfg!=_config)
					{
						if (cfg.PortName == names[i])
						{
							found = true;
							break;
						}
					}
				}
				if (!found)
				{
					PortComboBox.Items.Add(names[i]);
				}
			}
			if (!string.IsNullOrEmpty(_config.PortName))
			{
				PortComboBox.Text = _config.PortName;
			}
		}
		void RefreshEntryList()
		{
			EntryList.Items.Clear();
			foreach(var entry in _config.Entries) {
				EntryList.Items.Add(entry);
			}
		}
		private void RefreshPortsButton_Click(object sender, EventArgs e)
		{
			RefreshPortList();
		}
		int _ParseComPort(string port)
		{
			if (string.IsNullOrWhiteSpace(port)) return -1;
			if (!port.ToLowerInvariant().StartsWith("com"))
			{
				return -1;
			}
			var num = port.Substring(3);
			int i;
			if (!int.TryParse(num, NumberStyles.Integer, CultureInfo.InvariantCulture.NumberFormat, out i))
			{
				return -1;
			}
			if (i < 1 || i > 255)
			{
				return -1;
			}
			return i;
		}
		private void PortComboBox_Validating(object sender, CancelEventArgs e)
		{
			e.Cancel = _ParseComPort(PortComboBox.Text)<1;
		}

		private void PortComboBox_Leave(object sender, EventArgs e)
		{
			try
			{
				if (_config.Port != null && _config.Port.IsOpen)
				{
					_config.Port.Close();
				}
			}
			catch { }
			_config.PortName = PortComboBox.Text;
		}

		private void NewButton_Click(object sender, EventArgs e)
		{
		}
			
	}
}
