using OpenHardwareMonitor.Hardware;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PCMonitor
{
	public partial class EditMonitor : Form
	{
		List<Config> _configs;
		Computer _computer;
		internal EditMonitor(List<Config> configs, Computer computer)
		{
			_configs = configs;
			_computer = computer;
			InitializeComponent();
			
		}

	}
}
