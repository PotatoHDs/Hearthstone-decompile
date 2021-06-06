using System;

namespace Content.Delivery
{
	// Token: 0x02000FA4 RID: 4004
	internal class DamoclesConnect : ContentConnect
	{
		// Token: 0x0600AE2A RID: 44586 RVA: 0x003637B8 File Offset: 0x003619B8
		public void InitializeURL(string bNetLocaleName, string mediaCategory, bool regionCN, string saveLocation, int overrideAge)
		{
			string url = string.Format("https://{0}/ad/list?locale={1}&community=hearthstone&mediaCategory={2}&access_token={3}", new object[]
			{
				regionCN ? "gateway.battlenet.com.cn/cms" : "us.api.blizzard.com/cms",
				bNetLocaleName,
				mediaCategory,
				"UScQ7jQYlkDUjF6C2tde8BcQsLAwoxRxq6"
			});
			ContentConnectSettings setting = new ContentConnectSettings
			{
				m_contentType = mediaCategory,
				m_url = url,
				m_saveLocation = saveLocation,
				m_overrideAge = overrideAge
			};
			base.Init(setting);
		}

		// Token: 0x0600AE2B RID: 44587 RVA: 0x0036382C File Offset: 0x00361A2C
		public void InitializeNydusURL(string locale, bool regionCN, string saveLocation)
		{
			string arg = regionCN ? "CN" : "US";
			string url = string.Format("https://{0}/WTCG/{1}/client/ads?targetRegion={2}", "nydus.battle.net", locale, arg);
			ContentConnectSettings setting = new ContentConnectSettings
			{
				m_url = url,
				m_saveLocation = saveLocation
			};
			base.Init(setting);
		}

		// Token: 0x040094EA RID: 38122
		private const string NYDUS_DAMOCLES_HOST = "nydus.battle.net";

		// Token: 0x040094EB RID: 38123
		private const string DAMOCLES_URL = "us.api.blizzard.com/cms";

		// Token: 0x040094EC RID: 38124
		private const string DAMOCLES_URL_CN = "gateway.battlenet.com.cn/cms";

		// Token: 0x040094ED RID: 38125
		private const string ACCESS_TOKEN = "UScQ7jQYlkDUjF6C2tde8BcQsLAwoxRxq6";
	}
}
