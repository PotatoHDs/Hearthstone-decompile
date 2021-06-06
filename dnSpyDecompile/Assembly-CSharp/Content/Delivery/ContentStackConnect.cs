using System;
using System.Collections.Generic;
using UnityEngine;

namespace Content.Delivery
{
	// Token: 0x02000FA3 RID: 4003
	internal class ContentStackConnect : ContentConnect
	{
		// Token: 0x0600AE28 RID: 44584 RVA: 0x00363704 File Offset: 0x00361904
		public void InitializeURL(string contentType, string environment, string bNetLocaleName, bool regionCN, string saveLocation, int overrideAge)
		{
			string url = string.Format("https://{0}/v3/content_types/{1}/entries/?environment={2}&locale={3}", new object[]
			{
				regionCN ? "contentstack-cdn.cnc.blzstatic.cn" : "cdn.blz-contentstack.com",
				contentType,
				environment,
				bNetLocaleName.ToLower()
			});
			ContentConnectSettings setting = new ContentConnectSettings
			{
				m_contentType = contentType,
				m_url = url,
				m_apiKey = "blt29e62035cda0de64",
				m_saveLocation = saveLocation,
				m_overrideAge = overrideAge
			};
			if (!this.m_deliveryTokens.TryGetValue(environment, out setting.m_accessToken))
			{
				Debug.LogErrorFormat("Wrong environment name is used: {0}", new object[]
				{
					environment
				});
			}
			base.Init(setting);
		}

		// Token: 0x040094E4 RID: 38116
		public const string ENV_PRODUCTION = "production";

		// Token: 0x040094E5 RID: 38117
		public const string ENV_DEV = "dev";

		// Token: 0x040094E6 RID: 38118
		private const string CONTENTSTACK_HOST = "cdn.blz-contentstack.com";

		// Token: 0x040094E7 RID: 38119
		private const string CONTENTSTACK_HOST_CN = "contentstack-cdn.cnc.blzstatic.cn";

		// Token: 0x040094E8 RID: 38120
		private const string API_KEY = "blt29e62035cda0de64";

		// Token: 0x040094E9 RID: 38121
		private Dictionary<string, string> m_deliveryTokens = new Dictionary<string, string>
		{
			{
				"production",
				"csf73275fdecfc6d3c7f670709"
			},
			{
				"dev",
				"cs4a7882c8951e57ce47123bfa"
			}
		};
	}
}
