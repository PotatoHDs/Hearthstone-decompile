using System;

namespace Content.Delivery
{
	// Token: 0x02000FA1 RID: 4001
	[Serializable]
	public class ContentConnectData
	{
		// Token: 0x040094DA RID: 38106
		public const int CONTENT_FILE_VERSION = 1;

		// Token: 0x040094DB RID: 38107
		public int m_dataVersion = 1;

		// Token: 0x040094DC RID: 38108
		public int m_age;

		// Token: 0x040094DD RID: 38109
		public ulong m_lastDownloadTime;

		// Token: 0x040094DE RID: 38110
		public string m_sha1OfServiceUrl;

		// Token: 0x040094DF RID: 38111
		public string m_response;
	}
}
