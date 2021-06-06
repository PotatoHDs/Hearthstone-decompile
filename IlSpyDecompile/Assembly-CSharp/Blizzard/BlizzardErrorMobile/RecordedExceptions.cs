using System;
using System.Collections.Generic;

namespace Blizzard.BlizzardErrorMobile
{
	[Serializable]
	public class RecordedExceptions
	{
		public List<ExceptionStruct> m_records = new List<ExceptionStruct>();

		public List<ExceptionStruct> m_backupRecords = new List<ExceptionStruct>();

		public long m_lastReadTimeLog;
	}
}
