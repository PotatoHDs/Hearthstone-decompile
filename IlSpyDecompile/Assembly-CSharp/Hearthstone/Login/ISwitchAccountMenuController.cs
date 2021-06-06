using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	public interface ISwitchAccountMenuController
	{
		void ShowSwitchAccount(IEnumerable<Account> accounts, Action<Account?> callback);
	}
}
