using System;
using System.Collections.Generic;

namespace Hearthstone.Login
{
	// Token: 0x02001130 RID: 4400
	public interface ILegacyGuestAccountStorage
	{
		// Token: 0x0600C0D8 RID: 49368
		IEnumerable<GuestAccountInfo> GetStoredGuestAccounts();

		// Token: 0x0600C0D9 RID: 49369
		void ClearGuestAccounts();

		// Token: 0x0600C0DA RID: 49370
		string GetSelectedGuestAccountId();
	}
}
