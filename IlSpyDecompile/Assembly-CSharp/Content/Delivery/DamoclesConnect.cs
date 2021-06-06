namespace Content.Delivery
{
	internal class DamoclesConnect : ContentConnect
	{
		private const string NYDUS_DAMOCLES_HOST = "nydus.battle.net";

		private const string DAMOCLES_URL = "us.api.blizzard.com/cms";

		private const string DAMOCLES_URL_CN = "gateway.battlenet.com.cn/cms";

		private const string ACCESS_TOKEN = "UScQ7jQYlkDUjF6C2tde8BcQsLAwoxRxq6";

		public void InitializeURL(string bNetLocaleName, string mediaCategory, bool regionCN, string saveLocation, int overrideAge)
		{
			string url = string.Format("https://{0}/ad/list?locale={1}&community=hearthstone&mediaCategory={2}&access_token={3}", regionCN ? "gateway.battlenet.com.cn/cms" : "us.api.blizzard.com/cms", bNetLocaleName, mediaCategory, "UScQ7jQYlkDUjF6C2tde8BcQsLAwoxRxq6");
			ContentConnectSettings contentConnectSettings = default(ContentConnectSettings);
			contentConnectSettings.m_contentType = mediaCategory;
			contentConnectSettings.m_url = url;
			contentConnectSettings.m_saveLocation = saveLocation;
			contentConnectSettings.m_overrideAge = overrideAge;
			ContentConnectSettings setting = contentConnectSettings;
			Init(setting);
		}

		public void InitializeNydusURL(string locale, bool regionCN, string saveLocation)
		{
			string arg = (regionCN ? "CN" : "US");
			string url = string.Format("https://{0}/WTCG/{1}/client/ads?targetRegion={2}", "nydus.battle.net", locale, arg);
			ContentConnectSettings contentConnectSettings = default(ContentConnectSettings);
			contentConnectSettings.m_url = url;
			contentConnectSettings.m_saveLocation = saveLocation;
			ContentConnectSettings setting = contentConnectSettings;
			Init(setting);
		}
	}
}
