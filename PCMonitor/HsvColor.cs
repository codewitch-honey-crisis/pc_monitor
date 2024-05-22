using System;
using System.Drawing;


namespace PCMonitor
{
	internal struct HsvColor
	{
		uint _h;
		byte _s;
		byte _v;
		public HsvColor(uint h, byte s,byte v)
		{
			_h = h % 361;
			_s = (byte)(s % 101);
			_v = (byte)(v % 101);
		}
		public HsvColor(Color color)
		{
			ColorToHsv(color,out _h,out _s, out _v);
		}
		public uint H
		{
			get
			{
				return _h;
			}
			set
			{
				_h = value % 361;
			}
		}
		public byte S
		{
			get
			{
				return _s;
			}
			set
			{
				_s = (byte)(value % 101);
			}
		}
		public byte V
		{
			get
			{
				return _v;
			}
			set
			{
				_v = (byte)(value % 101);
			}
		}
		public Color ToColor()
		{
			return HsvToColor(_h, _s, _v);
		}
		public static void ColorToHsv(Color color, out uint hue, out byte saturation, out byte value)
		{
			// R, G, B values are divided by 255 
			// to change the range from 0..255 to 0..1 
			double r = color.R / 255.0;
			double g = color.G / 255.0;
			double b = color.B / 255.0;

			// h, s, v = hue, saturation, value 
			double cmax = Math.Max(r, Math.Max(g, b)); // maximum of r, g, b 
			double cmin = Math.Min(r, Math.Min(g, b)); // minimum of r, g, b 
			double diff = cmax - cmin; // diff of cmax and cmin. 
			double h = -1, s = -1;

			// if cmax and cmax are equal then h = 0 
			if (cmax == cmin)
				h = 0;

			// if cmax equal r then compute h 
			else if (cmax == r)
				h = (60 * ((g - b) / diff) + 360) % 360;

			// if cmax equal g then compute h 
			else if (cmax == g)
				h = (60 * ((b - r) / diff) + 120) % 360;

			// if cmax equal b then compute h 
			else if (cmax == b)
				h = (60 * ((r - g) / diff) + 240) % 360;

			// if cmax equal zero 
			if (cmax == 0)
				s = 0;
			else
				s = (diff / cmax) * 100;

			// compute v 
			double v = cmax * 100;
			hue = (uint)h;
			saturation = (byte)s;
			value = (byte)v;
		}
		static Color HsvToColor(uint hue /* 0 - 360*/, byte saturation /* 0-100 */, double value /* 0-1 */, byte alpha = 255)
		{
			var chH = hue/360.0;
			var chS = saturation/100.0;
			var chV = value / 100.0;

			int i = (int)Math.Floor(chH * 6);
			double f = chH * 6 - i;
			double p = chV * (1 - chS);
			double q = chV * (1 - f * chS);
			double t = chV * (1 - (1 - f) * chS);
			double r = 0, g = 0, b = 0;
			switch (i % 6)
			{
				case 0: r = chV; g = t; b = p; break;
				case 1: r = q; g = chV; b = p; break;
				case 2: r = p; g = chV; b = t; break;
				case 3: r = p; g = q; b = chV; break;
				case 4: r = t; g = p; b = chV; break;
				case 5: r = chV; g = p; b = q; break;
			}
			return Color.FromArgb(alpha,(byte)(r*255),(byte)(g *255),(byte)(b*255));
		}
	}
}
