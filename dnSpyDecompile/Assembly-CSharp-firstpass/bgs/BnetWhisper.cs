using System;

namespace bgs
{
	// Token: 0x0200024F RID: 591
	public class BnetWhisper
	{
		// Token: 0x06002494 RID: 9364 RVA: 0x0008183F File Offset: 0x0007FA3F
		public BnetGameAccountId GetSpeakerId()
		{
			return this.m_speakerId;
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00081847 File Offset: 0x0007FA47
		public void SetSpeakerId(BnetGameAccountId id)
		{
			this.m_speakerId = id;
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00081850 File Offset: 0x0007FA50
		public BnetGameAccountId GetReceiverId()
		{
			return this.m_receiverId;
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00081858 File Offset: 0x0007FA58
		public void SetReceiverId(BnetGameAccountId id)
		{
			this.m_receiverId = id;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00081861 File Offset: 0x0007FA61
		public string GetMessage()
		{
			return this.m_message;
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00081869 File Offset: 0x0007FA69
		public void SetMessage(string message)
		{
			this.m_message = message;
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00081872 File Offset: 0x0007FA72
		public ulong GetTimestampMicrosec()
		{
			return this.m_timestampMicrosec;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0008187A File Offset: 0x0007FA7A
		public void SetTimestampMicrosec(ulong microsec)
		{
			this.m_timestampMicrosec = microsec;
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00081883 File Offset: 0x0007FA83
		public void SetTimestampMilliseconds(double milliseconds)
		{
			this.m_timestampMicrosec = (ulong)(milliseconds * 1000.0);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00081897 File Offset: 0x0007FA97
		public BnetErrorInfo GetErrorInfo()
		{
			return this.m_errorInfo;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0008189F File Offset: 0x0007FA9F
		public void SetErrorInfo(BnetErrorInfo errorInfo)
		{
			this.m_errorInfo = errorInfo;
		}

		// Token: 0x04000F42 RID: 3906
		private BnetGameAccountId m_speakerId;

		// Token: 0x04000F43 RID: 3907
		private BnetGameAccountId m_receiverId;

		// Token: 0x04000F44 RID: 3908
		private string m_message;

		// Token: 0x04000F45 RID: 3909
		private ulong m_timestampMicrosec;

		// Token: 0x04000F46 RID: 3910
		private BnetErrorInfo m_errorInfo;
	}
}
