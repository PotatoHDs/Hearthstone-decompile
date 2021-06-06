using System;
using System.Runtime.InteropServices;

// Token: 0x020009B6 RID: 2486
public class DLLUtils
{
	// Token: 0x0600871C RID: 34588
	[DllImport("kernel32.dll")]
	public static extern IntPtr LoadLibrary(string filename);

	// Token: 0x0600871D RID: 34589
	[DllImport("kernel32.dll")]
	public static extern IntPtr GetProcAddress(IntPtr module, string funcName);

	// Token: 0x0600871E RID: 34590
	[DllImport("kernel32.dll")]
	public static extern IntPtr GetProcAddress(IntPtr module, IntPtr ordinalResource);

	// Token: 0x0600871F RID: 34591
	[DllImport("kernel32.dll")]
	public static extern bool FreeLibrary(IntPtr module);
}
