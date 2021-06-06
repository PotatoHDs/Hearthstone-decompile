using System;

namespace Blizzard.BlizzardErrorMobile
{
	[Serializable]
	public class ExceptionStruct
	{
		public string m_hash;

		public string m_message;

		public string m_stackTrace;

		public string m_reportUUID;

		public string m_zipName;

		public ExceptionStruct(string hash, string message, string stackTrace, string reportUUID, string zipName)
		{
			m_hash = hash;
			m_message = message;
			m_stackTrace = stackTrace;
			m_reportUUID = reportUUID;
			m_zipName = zipName;
		}
	}
}
