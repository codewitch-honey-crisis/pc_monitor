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
	public partial class GradientBox : UserControl
	{
		bool _is16Bit = false;
		bool _isHsv = false;
		Color _startColor = Color.Black;
		Color _endColor = Color.White;
		public GradientBox()
		{
			InitializeComponent();
		}
		public bool Is16Bit
		{
			get { return _is16Bit; }
			set { _is16Bit = value; Refresh(); }
		}
		public bool IsHsv { 
			get { return _isHsv; }
			set
			{
				_isHsv = value;
				Refresh();
			}
		}
		public Color StartColor {
			get
			{
				return _startColor;
			}
			set
			{
				_startColor = value;
				Refresh();
			}
		}
		public Color EndColor { 
			get
			{
				return _endColor;
			}
			set
			{
				_endColor = value;
				Refresh();
			}
		}
		static Color _To16Bit(Color color)
		{
			return Color.FromArgb(color.A, (byte)((color.R >> 3) << 3), (byte)((color.G >> 2) << 2), (byte)((color.B >> 3) << 3));
		}
		static Color _BlendRgb(Color color1,Color color2, double ratio)
		{
			if (ratio < 0) { ratio = 0; }
			if (ratio > 1) { ratio = 1; }
			byte r = (byte)(color1.R * (1.0-ratio) + color2.R * ratio);
			byte g = (byte)(color1.G * (1.0-ratio) + color2.G * ratio);
			byte b = (byte)(color1.B * (1.0 - ratio) + color2.B * ratio);
			byte a = (byte)(color1.A * (1.0 - ratio) + color2.A * ratio);
			return Color.FromArgb(a, r, g, b);
		}
		static Color _BlendHsv(HsvColor color1, HsvColor color2, double ratio)
		{
			if (ratio < 0) { ratio = 0; }
			if (ratio > 1) { ratio = 1; }
			uint h = (uint)(color1.H * (1.0 - ratio) + color2.H * ratio);
			byte s = (byte)(color1.S * (1.0 - ratio) + color2.S * ratio);
			byte v = (byte)(color1.V * (1.0 - ratio) + color2.V * ratio);
			return new HsvColor(h, s, v).ToColor();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			if(IsHsv)
			{
				HsvColor start = new HsvColor(StartColor);
				HsvColor end = new HsvColor(EndColor);
				for(int i = 0;i<Width;++i)
				{
					Color blended = _BlendHsv(start,end,((double)i)/((double)Width-1.0d));
					if(_is16Bit)
					{
						blended = _To16Bit(blended);
					}
					using(Pen p = new Pen(blended))
					{
						e.Graphics.DrawLine(p, new Point(i, 0), new Point(i, Height - 1));
					}
				}
			} else
			{
				for (int i = 0; i < Width; ++i)
				{
					Color blended = _BlendRgb(StartColor, EndColor, ((double)i) / ((double)Width - 1.0d));
					if (_is16Bit)
					{
						blended = _To16Bit(blended);
					}
					using (Pen p = new Pen(blended))
					{
						e.Graphics.DrawLine(p, new Point(i, 0), new Point(i, Height - 1));
					}
				}
			}
		}
	}
}
