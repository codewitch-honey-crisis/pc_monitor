using System;
using System.Drawing;


namespace PCMonitor
{
	internal static class HsvColor
	{
		public static Color Hsv(double hue /* 0 - 60*/, double saturation /* 0-1 */, double value /* 0-1 */, byte alpha = 255)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60.0)) % 6;
			double f = hue / 60.0 - Math.Floor(hue / 60.0);

			value *= 255.0;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			if (hi == 0)
				return Color.FromArgb(alpha, v, t, p);
			else if (hi == 1)
				return Color.FromArgb(alpha, q, v, p);
			else if (hi == 2)
				return Color.FromArgb(alpha, p, v, t);
			else if (hi == 3)
				return Color.FromArgb(alpha, p, q, v);
			else if (hi == 4)
				return Color.FromArgb(alpha, t, p, v);
			else
				return Color.FromArgb(alpha, v, p, q);
		}
	}
}
