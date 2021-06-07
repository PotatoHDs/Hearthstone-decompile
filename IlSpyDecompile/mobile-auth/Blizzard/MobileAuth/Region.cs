using System;
using System.Collections.Generic;
using Blizzard.MobileAuth.MiniJson;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct Region
	{
		public string regionId;

		public string name;

		public string tassadarUrl;

		public string softAccountCreationUrl;

		public Region(string jsonString)
		{
			Dictionary<string, object> dictionary = Json.Deserialize(jsonString) as Dictionary<string, object>;
			regionId = dictionary["regionId"].ToString();
			name = dictionary["name"].ToString();
			tassadarUrl = dictionary["tassadarUrl"].ToString();
			softAccountCreationUrl = dictionary["softAccountCreationUrl"].ToString();
		}

		public Region(string regionId, string name, string tassadarUrl, string softAccountCreationUrl)
		{
			this.regionId = regionId;
			this.name = name;
			this.tassadarUrl = tassadarUrl;
			this.softAccountCreationUrl = softAccountCreationUrl;
		}
	}
}
