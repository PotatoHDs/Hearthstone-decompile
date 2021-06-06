using System;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001211 RID: 4625
	[Serializable]
	public class ExceptionStruct
	{
		// Token: 0x0600CF97 RID: 53143 RVA: 0x003DC9D8 File Offset: 0x003DABD8
		public ExceptionStruct(string hash, string message, string stackTrace, string reportUUID, string zipName)
		{
			this.m_hash = hash;
			this.m_message = message;
			this.m_stackTrace = stackTrace;
			this.m_reportUUID = reportUUID;
			this.m_zipName = zipName;
		}

		// Token: 0x0400A20A RID: 41482
		public string m_hash;

		// Token: 0x0400A20B RID: 41483
		public string m_message;

		// Token: 0x0400A20C RID: 41484
		public string m_stackTrace;

		// Token: 0x0400A20D RID: 41485
		public string m_reportUUID;

		// Token: 0x0400A20E RID: 41486
		public string m_zipName;
	}
}
