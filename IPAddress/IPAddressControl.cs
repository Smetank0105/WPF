using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace IPAddress
{
	internal class IPAddressControl : HwndHost
	{
		private IntPtr hControl;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr CreateWindowEx
			(int dwExStyle, string lpszClassName, string lpszWindowName, 
			int style, int x, int y, int width, int height, 
			IntPtr hParent, IntPtr hMenu, IntPtr hInst, IntPtr pvParam);
		[DllImport("user32.dll")]
		private static extern bool DestroyWindow(IntPtr hwnd);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);
		[DllImport("kernel32.dll")]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
		[DllImport("kernel32.dll")]
		private static extern int GetLastError();

		[DllImport("comctl32.dll")]
		private static extern bool InitCommonControls();

		private const string WC_IPADDRESS = "SysIPAddress32";
		private const int WS_CHILD = 0x40000000;
		private const int WS_VISIBLE = 0x10000000;
		private const int IPM_GETADDRESS = 0x0467;

		protected override HandleRef BuildWindowCore(HandleRef hParent)
		{
			InitCommonControls();
			IntPtr hInstance = GetModuleHandle(null);
			hControl = CreateWindowEx
				(
					0,
					WC_IPADDRESS,
					"",
					WS_CHILD|WS_VISIBLE,
					0, 0,
					120, 20,
					hParent.Handle,
					IntPtr.Zero,
					hInstance,
					IntPtr.Zero
				);
			if( hControl == IntPtr.Zero )
				throw new InvalidOperationException("Create IPAddressControl failed.");
			return new HandleRef(this, hControl);
		}
		protected override void DestroyWindowCore(HandleRef hwnd)
		{
			DestroyWindow(hwnd.Handle);
		}
		protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
		}
		public string GetIPAddress()
		{
			if(hControl == IntPtr.Zero)
				return string.Empty;
			IntPtr result = Marshal.AllocHGlobal(4);
			
			SendMessage(hControl, IPM_GETADDRESS, IntPtr.Zero, result);
			int dw_ID = GetLastError();
			//uint address = (uint)Marshal.ReadInt32(result);
			uint address = (uint)result.ToInt32();

			byte a = (byte)((address >> 24) & 0xFF);
			byte b = (byte)((address >> 16) & 0xFF);
			byte c = (byte)((address >> 8) & 0xFF);
			byte d = (byte)(address & 0xFF);

			return $"{a}.{b}.{c}.{d}";
		}
		public void SetIPAddress(string ip)
		{

		}
	}
}
