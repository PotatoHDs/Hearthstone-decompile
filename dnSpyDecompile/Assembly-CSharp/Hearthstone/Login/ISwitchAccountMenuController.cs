using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	// Token: 0x02001137 RID: 4407
	public interface ISwitchAccountMenuController
	{
		// Token: 0x0600C101 RID: 49409
		void ShowSwitchAccount(IEnumerable<Account> accounts, Action<Account?> callback);
	}
}
