using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	// Token: 0x02001134 RID: 4404
	public interface IMobileAuth
	{
		// Token: 0x0600C0E8 RID: 49384
		void Configure(Configuration configuration);

		// Token: 0x0600C0E9 RID: 49385
		void PresentLogin(IAuthenticationDelegate authenticationDelegate);

		// Token: 0x0600C0EA RID: 49386
		void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate);

		// Token: 0x0600C0EB RID: 49387
		Region[] BuiltInRegions();

		// Token: 0x0600C0EC RID: 49388
		void Logout();

		// Token: 0x17000D55 RID: 3413
		// (get) Token: 0x0600C0ED RID: 49389
		bool IsAuthenticated { get; }

		// Token: 0x0600C0EE RID: 49390
		Account? GetAuthenticatedAccount();

		// Token: 0x0600C0EF RID: 49391
		void ImportAccount(string authenticationToken, string regionCode, IImportAccountCallback authenticationDelegate);

		// Token: 0x0600C0F0 RID: 49392
		void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate);

		// Token: 0x0600C0F1 RID: 49393
		void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener);

		// Token: 0x0600C0F2 RID: 49394
		List<Account> GetSoftAccounts();

		// Token: 0x0600C0F3 RID: 49395
		void ImportGuestAccount(string guestId, string regionId, IImportAccountCallback importCallback);

		// Token: 0x0600C0F4 RID: 49396
		void AuthenticateAccount(Account account, string regionId, IAuthenticationDelegate callback);

		// Token: 0x0600C0F5 RID: 49397
		List<Account> GetAllAccounts();

		// Token: 0x0600C0F6 RID: 49398
		void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate);

		// Token: 0x0600C0F7 RID: 49399
		void RemoveAccountById(string accountId, IOnAccountRemovedListener removedListener);
	}
}
