using System;
using Blizzard.T5.Services;

namespace Hearthstone.Login
{
	// Token: 0x02001132 RID: 4402
	public interface ILoginService : IService, IHasUpdate
	{
		// Token: 0x0600C0DD RID: 49373
		void StartLogin();

		// Token: 0x0600C0DE RID: 49374
		bool IsLoggedIn();

		// Token: 0x0600C0DF RID: 49375
		bool IsAttemptingLogin();

		// Token: 0x0600C0E0 RID: 49376
		void ClearAuthentication();

		// Token: 0x0600C0E1 RID: 49377
		void HealupCurrentTemporaryAccount(Action<bool> finishedCallback = null);

		// Token: 0x0600C0E2 RID: 49378
		bool SupportsAccountHealup();

		// Token: 0x0600C0E3 RID: 49379
		string GetAccountIdForAuthenticatedAccount();

		// Token: 0x0600C0E4 RID: 49380
		string GetRegionForAuthenticatedAccount();

		// Token: 0x0600C0E5 RID: 49381
		void ClearAllSavedAccounts();

		// Token: 0x0600C0E6 RID: 49382
		bool RequireRegionSwitchOnSwitchAccount();
	}
}
