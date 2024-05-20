using OpenHardwareMonitor.Hardware;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PCMonitor
{
    public partial class EditLabel : Form
	{
		int _label;
		Computer _computer;
		public EditLabel(int label, Computer computer)
		{
			_label = label;
			_computer = computer;
			InitializeComponent();
			ValueTree.ImageList = IconContainer.Icons;
			UpdateHueStartLabel();
			UpdateHueEndLabel();
			UpdateTree();
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
			var tag = parent.Tag;
			PropertyInfo prop = null;
			try
			{
				prop = tag.GetType().GetProperty("Identifier");
			}
			catch {  }
			string id = null;
			if(prop!=null)
			{
				try
				{
					id = prop.GetValue(tag) as string;
				}
				catch {  }
				if(id!=null)
				{
					if(identifier==id)
					{
						return parent;
					}
				}
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
		

		void UpdateHueEndLabel()
		{
			HueEndLabel.BackColor = HsvColor.Hsv((HueEndBar.Value / 100.0)*60,1,1);
		}
		void UpdateHueStartLabel()
		{
			HueStartLabel.BackColor = HsvColor.Hsv((HueStartBar.Value / 100.0)*60, 1, 1);
		}
		private void HueEndBar_Scroll(object sender, EventArgs e)
		{
			UpdateHueEndLabel();
		}

		private void HueStartBar_Scroll(object sender, EventArgs e)
		{
			UpdateHueStartLabel();
		}

		private void EditLabel_Load(object sender, EventArgs e)
		{

		}

		private void ValueTimer_Tick(object sender, EventArgs e)
		{
			try
			{
				var node = ValueTree.SelectedNode;
				if (node != null)
				{
					var sens = node.Tag as ISensor;
					if (sens != null)
					{
						Invoke(new Action(() => {
							ValueLabel.Text = sens.Value.ToString();
						}));
					} else
					{
						Invoke(new Action(() => {
							ValueLabel.Text = "";
						}));
					}
				} else
				{
					Invoke(new Action(() =>
					{
						ValueLabel.Text = "";
					}));

				}
			}
			catch { }
		}
	}
}
