using System;

// Token: 0x020009C1 RID: 2497
public class LogInfo
{
	// Token: 0x0400725E RID: 29278
	public string m_name;

	// Token: 0x0400725F RID: 29279
	public bool m_consolePrinting;

	// Token: 0x04007260 RID: 29280
	public bool m_screenPrinting;

	// Token: 0x04007261 RID: 29281
	public bool m_filePrinting;

	// Token: 0x04007262 RID: 29282
	public Log.LogLevel m_minLevel = Log.LogLevel.Debug;

	// Token: 0x04007263 RID: 29283
	public Log.LogLevel m_defaultLevel = Log.LogLevel.Debug;

	// Token: 0x04007264 RID: 29284
	public bool m_alwaysPrintErrors = true;

	// Token: 0x04007265 RID: 29285
	public bool m_verbose;
}
