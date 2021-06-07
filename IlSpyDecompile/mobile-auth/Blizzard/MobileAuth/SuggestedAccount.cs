using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct SuggestedAccount
	{
		public string accountId;

		public string accountName;

		public string regionId;

		public string battleTag;

		internal SuggestedAccount(string accountId, string accountName, string regionId, string battleTag)
		{
			this.accountId = accountId;
			this.accountName = accountName;
			this.regionId = regionId;
			this.battleTag = battleTag;
		}
	}
}
