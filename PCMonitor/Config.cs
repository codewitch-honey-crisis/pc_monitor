using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.IO.Ports;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Configuration;

namespace PCMonitor
{
	internal class ConfigEntry : ICloneable
	{
		Image _icon;
		byte[] _iconData = null;
		public string Path { get; set; }
		public string Format { get; set; }
		public float ValueMax { get; set; }
		public Color ColorStart { get; set; }
		public Color ColorEnd { get; set; }
		public bool ColorHsv { get; set; }
		public Image Icon {
			get {
				return _icon;
			}
			set {
				if(value != _icon)
				{
					_icon = value;
					_iconData = null;
				}
			}
		}
		public byte[] IconData { 
			get {
				if(_iconData == null)
				{
					_iconData = _GetIconData();
				}
				return _iconData;
			} 
			set
			{
                if (value==null)
                {
					_icon = null;
					return;
                }
				if(value.Length!=512)
				{
					throw new InvalidDataException("The byte array does not contain a valid icon");
				}
                var bmp = new Bitmap(16,16,System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				int idx = 0;
				for(var y = 0;y<16;++y)
				{
					for(var x = 0;x<16;++x)
					{
						var rev = new byte[2];
						rev[0] = value[idx + 1];
						rev[1] = value[idx];
						var col = _Rgb565ToColor(BitConverter.ToUInt16(rev, 0));
						bmp.SetPixel(x,y, col);
						idx += 2;
					}
				}
				_icon = bmp;
			}
		}
		object ICloneable.Clone() { return Clone(); }
		public ConfigEntry Clone()
		{
			var result = new ConfigEntry();
			result.Path = Path;
			result.Format = Format;
			result.ValueMax = ValueMax;
			result.ColorStart = ColorStart;
			result.ColorEnd = ColorEnd;
			result.ColorHsv = ColorHsv;
			if (_icon != null)
			{
				result._icon = (Image)_icon.Clone();
			}
			if (_iconData != null)
			{
				result._iconData = new byte[_iconData.Length];
				Array.Copy(_iconData, result._iconData, _iconData.Length);
			}
			return result;
		}
		private byte[] _GetIconData()
		{
			if (Icon == null) return new byte[16*16*2];
			var bmp = new Bitmap(Icon);
			var ba = new byte[16 * 16 * 2];
			int bi = 0;
			for (int y = 0; y < 16; ++y)
			{
				for (int x = 0; x < 16; ++x)
				{
					Color col = bmp.GetPixel(x, y);
					ushort rgb565 = _ColorToRgb565(col);
					ba[bi++] = unchecked((byte)(rgb565 / 256));
					ba[bi++] = unchecked((byte)(rgb565 & 0xFF));
				}
			}
			return ba;
		}
		private ushort _ColorToRgb565(Color col)
		{
			ushort rgb565 = 0;
			rgb565 |= unchecked((ushort)(((byte)(col.B >> 3)) << 0));
			rgb565 |= unchecked((ushort)(((byte)(col.G >> 2)) << 5));
			rgb565 |= unchecked((ushort)(((byte)(col.R >> 3)) << 11));
			return rgb565;
		}
		private Color _Rgb565ToColor(ushort rgb565)
		{
			byte b = (byte)(((rgb565 >> 0) & 15) << 3);
			byte g = (byte)(((rgb565 >> 6) & 31) << 2);
			byte r = (byte)(((rgb565 >> 11) & 15) << 3);
			return Color.FromArgb(r,g ,b );
			
		}
		public ushort ColorStartRgb565 {
			get {
				return _ColorToRgb565(ColorStart);
			}
		}
		public ushort ColorEndRgb565 {
			get {
				return _ColorToRgb565(ColorEnd);
			}
		}
		public override string ToString()
		{
			if(string.IsNullOrEmpty(Path))
			{
				return base.ToString();
			}
			return Path.ToString();
		}
	}
	internal class Config
	{
		SerialPort _port;
		string _portName;
		public string PortName
		{
			get { return _portName; }
			set { _portName = value; 
				if( _port != null ) {
					try
					{
						if (_port.IsOpen)
						{
							_port.Close();
						}
					}
					catch
					{

					}
					_port = null;
				}
			}
		}
		public SerialPort Port
		{
			get
			{
				if( _port == null )
				{
					_port = new SerialPort(_portName);
				}
				return _port;
			}
			set
			{
				if (_port != null)
				{
					try
					{
						if (_port.IsOpen)
						{
							_port.Close();
						}
					}
					catch
					{

					}
					_port = null;
				}
				_port = value;
				if(_port != null )
				{
					_portName = _port.PortName;
				} else
				{
					_portName = null;
				}
			}
		}
		static int _ParseColorChannel(string channel)
		{
			if(string.IsNullOrWhiteSpace(channel))
			{
				return -1;
			}
			channel = channel.Trim();
			int result;
			if (channel.StartsWith("0x"))
			{
				if(int.TryParse(channel,NumberStyles.HexNumber,null ,out result))
				{
					return result;
				}
			} else
			{
				if (int.TryParse(channel, NumberStyles.Integer, null, out result))
				{
					return result;
				}
			}
			return -1;
		}
		static byte[] _ParseBlob(string blob)
		{
			if(!string.IsNullOrEmpty(blob) && blob.StartsWith("base64(") && blob.EndsWith(")"))
			{
				return Convert.FromBase64String(blob.Substring(7, blob.Length - 8));
			}
			return null;
		}
		static Color _ParseColor(string color)
		{
			if(color.StartsWith("hsv(") && color.EndsWith(")"))
			{
				var chans = color.Substring(4, color.Length - 5).Split(',');
				if (chans.Length == 3)
				{
					double h = _ParseColorChannel(chans[0].Trim());
					double s = _ParseColorChannel(chans[1].Trim());
					double v = _ParseColorChannel(chans[2].Trim());
					if (h < 0 || s < 0 || v < 0)
					{
						return Color.Transparent;
					}
					if (h > 360 || s > 100 || v > 100)
					{
						return Color.Transparent;
					}
					return new HsvColor((uint)h, (byte)s , (byte)v ).ToColor();
				}
			}
            else if (color.StartsWith("rgb(") && color.EndsWith(")"))
            {
				var chans = color.Substring(4, color.Length - 5).Split(',');
				if(chans.Length==3)
				{
					var r = _ParseColorChannel(chans[0].Trim());
					var g = _ParseColorChannel(chans[1].Trim());
					var b = _ParseColorChannel(chans[2].Trim());
					if(r < 0 || g < 0 || b < 0)
					{
						return Color.Transparent;
					}
					if (r > 255 || g > 255 || b > 255)
					{
						return Color.Transparent;
					}
					return Color.FromArgb(r, g, b);
				}
			}
            else if (char.IsLetter(color,0))
			{
				try
				{
					return Color.FromName(color);
				}
				catch { }
			}
			return Color.Transparent;
		}
		private Lazy<IList<ConfigEntry>> _entries = new Lazy<IList<ConfigEntry>>(() => { return new List<ConfigEntry>(); });
		public IList<ConfigEntry> Entries { 
			get {
				return _entries.Value;
			}
		}
		public static Config[] ReadFrom(TextReader reader)
		{
			var result = new List<Config>();
			dynamic root = JsonObject.ReadFrom(reader);
			foreach(var monitor_obj in root.monitors)
			{
				dynamic monitor = monitor_obj;
				var cfg = new Config();
				cfg.PortName = monitor.port as string;
				foreach (var entry_obj in monitor.entries)
				{
					dynamic entry = entry_obj;
					var cfge = new ConfigEntry();
					cfge.Path = entry.value as string;
					if (entry.value_max is double)
					{
						cfge.ValueMax = (float)entry.value_max;
					}
					cfge.Format = entry.format as string;
					cfge.IconData = _ParseBlob(entry.icon);
					cfge.ColorStart = _ParseColor(entry.color_start as string);
					cfge.ColorEnd = _ParseColor(entry.color_end as string);
					if (entry.color_hsv is bool)
					{
						cfge.ColorHsv = (bool)entry.color_hsv;
					}
					cfg.Entries.Add(cfge);
				}
				result.Add(cfg);
			}
			return result.ToArray();
		}
		public static void WriteTo(IEnumerable<Config> configs, TextWriter writer)
		{
			if(configs == null) { throw new ArgumentNullException(nameof(configs)); }
			dynamic doc = new JsonObject();
			doc.monitors = new JsonArray();
			foreach (var config in configs)
			{
				dynamic monitor = new JsonObject();
				monitor.port = config.PortName;
				monitor.entries = new JsonArray();
				foreach(var entry in config.Entries)
				{
					dynamic monitor_entry = new JsonObject();
					monitor_entry.icon = string.Concat("base64(", Convert.ToBase64String(entry.IconData), ")");
					monitor_entry.value = entry.Path;
					monitor_entry.value_max = entry.ValueMax;
					monitor_entry.format = entry.Format;
					monitor_entry.color_hsv = entry.ColorHsv;
					monitor_entry.color_start = string.Format("rgb({0},{1},{2})",entry.ColorStart.R,entry.ColorStart.G,entry.ColorStart.B);
					monitor_entry.color_end = string.Format("rgb({0},{1},{2})", entry.ColorEnd.R, entry.ColorEnd.G, entry.ColorEnd.B);
					monitor.entries.Add(monitor_entry);
				}
				doc.monitors.Add(monitor);
			}
			((JsonObject)doc).WriteTo(writer); writer.Flush();
		}
		public override string ToString()
		{
			if(_portName==null && _port==null)
			{
				return base.ToString();
			}
			return PortName;
		}
	}
}
