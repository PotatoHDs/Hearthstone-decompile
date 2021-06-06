using System;
using System.Collections.Generic;

namespace Blizzard.BlizzardErrorMobile
{
	// Token: 0x02001212 RID: 4626
	[Serializable]
	public class RecordedExceptions
	{
		// Token: 0x0400A20F RID: 41487
		public List<ExceptionStruct> m_records = new List<ExceptionStruct>();

		// Token: 0x0400A210 RID: 41488
		public List<ExceptionStruct> m_backupRecords = new List<ExceptionStruct>();

		// Token: 0x0400A211 RID: 41489
		public long m_lastReadTimeLog;
	}
}
