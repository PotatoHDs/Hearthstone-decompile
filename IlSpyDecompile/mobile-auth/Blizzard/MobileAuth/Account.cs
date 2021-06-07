using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct Account
	{
		public string displayName;

		public string accountName;

		public string accountId;

		public string authenticationToken;

		public string regionId;

		public string version;

		public string battleTag;

		public string bnetGuestId;

		public Account(string displayName, string accountName, string accountId, string authenticationToken, string regionId, string version, string battleTag, string bnetGuestId)
		{
			this.displayName = displayName;
			this.accountName = accountName;
			this.accountId = accountId;
			this.authenticationToken = authenticationToken;
			this.regionId = regionId;
			this.version = version;
			this.battleTag = battleTag;
			this.bnetGuestId = bnetGuestId;
		}

		public override string ToString()
		{
			return "Display Name: " + displayName + "\nAccount Name: " + accountName + "\nAccount ID: " + accountId + "\nBattleTag: " + battleTag + "\nRegion: " + regionId + "\nVersion: " + version + "\nToken: " + authenticationToken + "\nGuest ID: " + bnetGuestId;
		}
	}
}
