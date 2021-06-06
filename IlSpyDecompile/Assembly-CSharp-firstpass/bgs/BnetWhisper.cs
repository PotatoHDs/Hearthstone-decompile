namespace bgs
{
	public class BnetWhisper
	{
		private BnetGameAccountId m_speakerId;

		private BnetGameAccountId m_receiverId;

		private string m_message;

		private ulong m_timestampMicrosec;

		private BnetErrorInfo m_errorInfo;

		public BnetGameAccountId GetSpeakerId()
		{
			return m_speakerId;
		}

		public void SetSpeakerId(BnetGameAccountId id)
		{
			m_speakerId = id;
		}

		public BnetGameAccountId GetReceiverId()
		{
			return m_receiverId;
		}

		public void SetReceiverId(BnetGameAccountId id)
		{
			m_receiverId = id;
		}

		public string GetMessage()
		{
			return m_message;
		}

		public void SetMessage(string message)
		{
			m_message = message;
		}

		public ulong GetTimestampMicrosec()
		{
			return m_timestampMicrosec;
		}

		public void SetTimestampMicrosec(ulong microsec)
		{
			m_timestampMicrosec = microsec;
		}

		public void SetTimestampMilliseconds(double milliseconds)
		{
			m_timestampMicrosec = (ulong)(milliseconds * 1000.0);
		}

		public BnetErrorInfo GetErrorInfo()
		{
			return m_errorInfo;
		}

		public void SetErrorInfo(BnetErrorInfo errorInfo)
		{
			m_errorInfo = errorInfo;
		}
	}
}
