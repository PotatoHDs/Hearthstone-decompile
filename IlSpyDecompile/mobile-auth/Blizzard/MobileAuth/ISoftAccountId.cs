using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public abstract class ISoftAccountId
	{
		public string regionCode;

		public string accountId;

		public string bnetGuestId;

		public ISoftAccountId(string regionCode, string accountId, string bnetGuestId)
		{
			this.regionCode = regionCode;
			this.accountId = accountId;
			this.bnetGuestId = bnetGuestId;
		}
	}
}
