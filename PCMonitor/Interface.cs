using System;
using System.Runtime.InteropServices;
namespace PCMonitor
{
	[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi,Pack =4)]
	struct ScreenPacket
	{
		// the command id
		// constexpr static const int command = 1;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
		public String Format;
		public float ValueMax;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16 * 16 * 2)]
		public byte[] Icon;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public ushort[] Colors;
		[MarshalAs(UnmanagedType.U1)]
		public bool HsvColor;
		public float Value;
	}
}
