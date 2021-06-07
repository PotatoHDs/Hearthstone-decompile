using System;

namespace Blizzard.MobileAuth
{
	[Serializable]
	public class GuestSoftAccountId : ISoftAccountId
	{
		public GuestSoftAccountId(string regionCode, string accountId, string bnetGuestId)
			: base(regionCode, accountId, bnetGuestId)
		{
		}
	}
}
