using System;
using Blizzard.T5.Services;

namespace Hearthstone.Login
{
	public interface ILoginService : IService, IHasUpdate
	{
		void StartLogin();

		bool IsLoggedIn();

		bool IsAttemptingLogin();

		void ClearAuthentication();

		void HealupCurrentTemporaryAccount(Action<bool> finishedCallback = null);

		bool SupportsAccountHealup();

		string GetAccountIdForAuthenticatedAccount();

		string GetRegionForAuthenticatedAccount();

		void ClearAllSavedAccounts();

		bool RequireRegionSwitchOnSwitchAccount();
	}
}
