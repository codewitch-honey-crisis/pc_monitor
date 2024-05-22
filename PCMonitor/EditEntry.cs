using OpenHardwareMonitor.Hardware;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PCMonitor
{
    public partial class EditEntry : Form
	{
		string _orgUnit=null;
		Config _config;
		ConfigEntry _oldEntry;
		ConfigEntry _entry;
		Computer _computer;
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Gradient.Refresh();
			ValidateForm();
		}
		void ValidateForm()
		{
			var valid = true;
			if(string.IsNullOrWhiteSpace(FormatText.Text))
			{
				valid = false;
			}
			if(ValueTree.SelectedNode==null)
			{
				valid = false;
			} else
			{
				var tag = ValueTree.SelectedNode.Tag;
				if(tag==null)
				{
					valid = false;
				} else
				{
					var sens = tag as ISensor;
					if(sens==null)
					{
						valid = false;
					}
				}
			}
			double t;
			if(!double.TryParse(MaxText.Text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out t))
			{
				valid = false;
			}
			OkButton.Enabled = valid;
		}
		internal EditEntry(Config config,ConfigEntry entry ,Computer computer)
		{
			_entry = entry.Clone();
			_oldEntry = entry;
			_config = config;
			_computer = computer;
			InitializeComponent();
			ValueTree.ImageList = IconContainer.Icons;
			UpdateTree();
			Gradient.IsHsv = false;
			Gradient.StartColor = _entry.ColorStart;
			Gradient.EndColor = _entry.ColorEnd;
			HueStartLabel.BackColor = _entry.ColorStart;
			StartColorDialog.Color = _entry.ColorStart;
			HueEndLabel.BackColor = _entry.ColorEnd;
			EndColorDialog.Color = _entry.ColorEnd;
			IconBox.Image = _entry.Icon;
			if(_entry.ColorHsv)
			{
				HsvCheckBox.Checked = true;
			}
			FormatText.Text = _entry.Format;
			MaxText.Text = _entry.ValueMax.ToString();
			PathLabel.Text = _entry.Path;
			var node = FindNode(_entry.Path);
			if(node != null )
			{
				node.EnsureVisible();
				ValueTree.SelectedNode = node;
			}
		}
		TreeNode FindNode(string identifier)
		{
			try
			{
				return FindNode(identifier, ValueTree.Nodes[0]);
			}
			catch { }
			return null;
		}
		static TreeNode FindNode(string identifier, TreeNode parent)
		{
			if (parent == null)
			{
				return null;
			}
			string id = null;
			var sens = parent.Tag as ISensor;
			if(sens!=null)
			{
				id = sens.Identifier?.ToString();
			} else
			{
				var hw = parent.Tag as IHardware;
				if (hw != null)
				{
					id = hw.Identifier?.ToString();
				}
			}
			
			if (identifier==id)
			{
				return parent;
			}
		
			foreach(TreeNode node in parent.Nodes)
			{
				var result = FindNode(identifier, node);
				if(result!=null)
				{
					return result;
				}
			}
			return null;
		}
		void UpdateSensorNode(TreeNode parent)
		{
			var sensor = parent.Tag as ISensor;
			parent.Nodes.Clear();
			foreach(var value in sensor.Values)
			{
				var newNode = parent.Nodes.Add(value.Value.ToString());
				newNode.Tag = value;
				newNode.SelectedImageKey = sensor.SensorType.ToString().ToLowerInvariant();
				newNode.EnsureVisible();
			}
		}
		void UpdateHardwareNode(TreeNode parent)
		{
			var hardware = parent.Tag as IHardware;
			var toRemove = new List<TreeNode>();
			foreach (TreeNode node in parent.Nodes)
			{
				if(node.Tag is IHardware)
				{
					toRemove.Add(node);
				}
			}
			foreach(TreeNode node in toRemove)
			{
				parent.Nodes.Remove(node);
			}
			toRemove = null;
			foreach (var sensor in hardware.Sensors)
			{
				//if (sensor.Name.ToLowerInvariant().Contains("memory total")) System.Diagnostics.Debugger.Break();
				var newNode = parent.Nodes.Add(sensor.Name);
				newNode.Tag = sensor;
				newNode.ImageKey = sensor.SensorType.ToString().ToLowerInvariant();
				newNode.SelectedImageKey = newNode.ImageKey;
				newNode.EnsureVisible();
				UpdateSensorNode(newNode);

			}
			foreach (var subhardware in hardware.SubHardware)
			{
				var newNode = parent.Nodes.Add(subhardware.Name);
				newNode.Tag = subhardware;
				newNode.ImageKey = subhardware.HardwareType.ToString().ToLowerInvariant();
				newNode.SelectedImageKey = newNode.ImageKey;
				newNode.EnsureVisible();
				UpdateHardwareNode(newNode);
			}
			
		}
		void UpdateTree()
		{
			ValueTree.Nodes.Clear();
			var compNode = ValueTree.Nodes.Add("Computer");
			compNode.Tag = _computer;
			compNode.ImageKey = "computer";
			compNode.SelectedImageKey = "computer";
			foreach (var hardware in _computer.Hardware)
			{
				var newNode = compNode.Nodes.Add(hardware.Name);
				newNode.Tag = hardware;
				newNode.ImageKey = hardware.HardwareType.ToString().ToLowerInvariant();
				newNode.SelectedImageKey = newNode.ImageKey;
				newNode.EnsureVisible();
				UpdateHardwareNode(newNode);
			}
			compNode.EnsureVisible();
		}
	
		private void EditLabel_Load(object sender, EventArgs e)
		{

		}


		private void ValueTree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var tag = e.Node.Tag;
			var sens = tag as ISensor;
			if (sens != null)
			{
				PathLabel.Text = sens.Identifier.ToString();
				_entry.Path = PathLabel.Text;
				ValueLabel.Text = sens.Value.ToString();
			}
			else
			{
				ValueLabel.Text = "(no value)";
				var hw = tag as IHardware;
				if(hw != null)
				{
					PathLabel.Text += hw.Identifier.ToString();
					_entry.Path = PathLabel.Text;
				}
			}

			ValidateForm();
		}

		private void HueStartLabel_Click(object sender, EventArgs e)
		{
			if(DialogResult.OK==StartColorDialog.ShowDialog())
			{
				HueStartLabel.BackColor = StartColorDialog.Color;
				Gradient.StartColor = StartColorDialog.Color;
				_entry.ColorStart = StartColorDialog.Color;
			}
		}

		private void HueEndLabel_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == EndColorDialog.ShowDialog())
			{
				HueEndLabel.BackColor = EndColorDialog.Color;
				Gradient.EndColor = EndColorDialog.Color;
				_entry.ColorEnd = EndColorDialog.Color;
			}
		}

		private void HsvCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			Gradient.IsHsv = HsvCheckBox.Checked;
			_entry.ColorHsv = HsvCheckBox.Checked;
		}

		private void ChangeIconButton_Click(object sender, EventArgs e)
		{
			if(OpenIconFileDialog.ShowDialog() == DialogResult.OK)
			{
				using(var bmp = Bitmap.FromFile(OpenIconFileDialog.FileName)) {
					var bmp2 = new Bitmap(bmp, new Size(16, 16));
					_entry.Icon = bmp2;
					IconBox.Image = bmp2;
					
				}
			}
		}

		private void FormatText_Leave(object sender, EventArgs e)
		{
			_entry.Format = FormatText.Text;
			ValidateForm();
		}

		private void MaxText_Leave(object sender, EventArgs e)
		{

			double t;
			if (double.TryParse(MaxText.Text.Trim(), System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out t))
			{
				_entry.ValueMax = (float)t;
			}
			ValidateForm();
		}

		private void _CancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var i = _config.Entries.IndexOf(_oldEntry);
			if(0>i)
			{
				_config.Entries.Add(_entry);
			} else
			{
				_config.Entries[i] = _entry;
			}
			var sw = new StringWriter();
			Config.WriteTo(new Config[] { _config }, sw);
			MessageBox.Show(sw.ToString());
			Close();
		}

	
		private void UnitText_Enter(object sender, EventArgs e)
		{
			_orgUnit = UnitText.Text;
		}

		private void UnitText_Leave(object sender, EventArgs e)
		{
			if (_orgUnit != UnitText.Text)
			{
				try
				{
					FormatText.Text = string.Format("% {0}.0f{1}", Math.Max(MaxText.Text.Trim().Length, 1), UnitText.Text.Replace("%", "%%"));
					_entry.Format = FormatText.Text;
				}
				catch
				{

				}
			}
			ValidateForm();
		}
	}
}
