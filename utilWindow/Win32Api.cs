using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace utilWindow
{
	public class Win32Api : Component
	{
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool IsWindow(IntPtr hWnd);

		const int SW_RESTORE = 9;

		public void SetMainWindow(int handle)
		{
			IntPtr ptr = new IntPtr(handle);
			if (!IsWindow(ptr))
			{
				return;
			}
			ShowWindow(ptr, SW_RESTORE);
			SetForegroundWindow(ptr);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}
	}
}
