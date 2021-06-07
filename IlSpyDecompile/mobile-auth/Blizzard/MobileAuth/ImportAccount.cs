using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public struct ImportAccount
	{
		public string authenticationToken;

		public string bnetGuestID;

		public string regionID;

		private ImportAccount(string authenticationToken, string bnetGuestID, string regionID)
		{
			this.authenticationToken = authenticationToken;
			this.bnetGuestID = bnetGuestID;
			this.regionID = regionID;
		}

		public static ImportAccount CreateWithHardAccountAuthenticationToken(string authenticationToken, string regionID)
		{
			return new ImportAccount(authenticationToken, null, regionID);
		}

		public static ImportAccount CreateWithSoftAccountBnetGuestID(string bnetGuestID, string regionID)
		{
			return new ImportAccount(null, bnetGuestID, regionID);
		}
	}
}
