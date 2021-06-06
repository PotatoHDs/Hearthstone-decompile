using System;
using System.Runtime.InteropServices;

public class DLLUtils
{
	[DllImport("kernel32.dll")]
	public static extern IntPtr LoadLibrary(string filename);

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetProcAddress(IntPtr module, string funcName);

	[DllImport("kernel32.dll")]
	public static extern IntPtr GetProcAddress(IntPtr module, IntPtr ordinalResource);

	[DllImport("kernel32.dll")]
	public static extern bool FreeLibrary(IntPtr module);
}
