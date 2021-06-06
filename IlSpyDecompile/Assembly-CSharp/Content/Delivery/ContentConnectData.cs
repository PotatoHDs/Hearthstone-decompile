using System;

namespace Content.Delivery
{
	[Serializable]
	public class ContentConnectData
	{
		public const int CONTENT_FILE_VERSION = 1;

		public int m_dataVersion = 1;

		public int m_age;

		public ulong m_lastDownloadTime;

		public string m_sha1OfServiceUrl;

		public string m_response;
	}
}
