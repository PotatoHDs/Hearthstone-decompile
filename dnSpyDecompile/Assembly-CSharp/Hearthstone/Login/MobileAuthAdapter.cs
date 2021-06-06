using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	// Token: 0x02001141 RID: 4417
	internal class MobileAuthAdapter : IMobileAuth
	{
		// Token: 0x17000D70 RID: 3440
		// (get) Token: 0x0600C188 RID: 49544 RVA: 0x003AC8A5 File Offset: 0x003AAAA5
		public bool IsAuthenticated
		{
			get
			{
				return MobileAuth.IsAuthenticated;
			}
		}

		// Token: 0x0600C189 RID: 49545 RVA: 0x003AC8AC File Offset: 0x003AAAAC
		public void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.AuthenticateSoftAccount(softAccountId, authenticationDelegate);
		}

		// Token: 0x0600C18A RID: 49546 RVA: 0x003AC8B5 File Offset: 0x003AAAB5
		public Region[] BuiltInRegions()
		{
			return MobileAuth.BuiltInRegions();
		}

		// Token: 0x0600C18B RID: 49547 RVA: 0x003AC8BC File Offset: 0x003AAABC
		public void Configure(Configuration configuration)
		{
			MobileAuth.Configure(configuration);
		}

		// Token: 0x0600C18C RID: 49548 RVA: 0x003AC8C4 File Offset: 0x003AAAC4
		public void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener)
		{
			MobileAuth.GenerateGuestSoftAccountId(region, listener);
		}

		// Token: 0x0600C18D RID: 49549 RVA: 0x003AC8CD File Offset: 0x003AAACD
		public Account? GetAuthenticatedAccount()
		{
			return MobileAuth.GetAuthenticatedAccount();
		}

		// Token: 0x0600C18E RID: 49550 RVA: 0x003AC8D4 File Offset: 0x003AAAD4
		public List<Account> GetSoftAccounts()
		{
			List<Account> list;
			try
			{
				list = MobileAuth.GetSoftAccounts();
			}
			catch (Exception ex)
			{
				Log.Login.PrintDebug("Unexepcted exceception from mobile auth {0}:\n{1}", new object[]
				{
					ex.Message,
					ex.StackTrace
				});
				list = new List<Account>();
			}
			if (list == null)
			{
				Log.Login.PrintDebug("Unexpected null soft account list", Array.Empty<object>());
				list = new List<Account>();
			}
			return list;
		}

		// Token: 0x0600C18F RID: 49551 RVA: 0x003AC948 File Offset: 0x003AAB48
		public List<Account> GetAllAccounts()
		{
			List<Account> list;
			try
			{
				list = MobileAuth.GetAllAccounts();
			}
			catch (Exception ex)
			{
				Log.Login.PrintDebug("Unexepcted exceception from mobile auth:{0}\n{1}", new object[]
				{
					ex.Message,
					ex.StackTrace
				});
				list = new List<Account>();
			}
			if (list == null)
			{
				Log.Login.PrintDebug("Unexpected null full account list", Array.Empty<object>());
				list = new List<Account>();
			}
			return list;
		}

		// Token: 0x0600C190 RID: 49552 RVA: 0x003AC9BC File Offset: 0x003AABBC
		public void AuthenticateAccount(Account account, string regionId, IAuthenticationDelegate callback)
		{
			MobileAuth.AuthenticateAccount(account, regionId, callback);
		}

		// Token: 0x0600C191 RID: 49553 RVA: 0x003AC9C6 File Offset: 0x003AABC6
		public void ImportAccount(string authenticationToken, string regionCode, IImportAccountCallback importCallback)
		{
			MobileAuth.ImportAccount(Blizzard.MobileAuth.ImportAccount.CreateWithHardAccountAuthenticationToken(authenticationToken, regionCode), importCallback);
		}

		// Token: 0x0600C192 RID: 49554 RVA: 0x003AC9D5 File Offset: 0x003AABD5
		public void ImportGuestAccount(string guestId, string regionId, IImportAccountCallback importCallback)
		{
			MobileAuth.ImportAccount(Blizzard.MobileAuth.ImportAccount.CreateWithSoftAccountBnetGuestID(guestId, regionId), importCallback);
		}

		// Token: 0x0600C193 RID: 49555 RVA: 0x003AC9E4 File Offset: 0x003AABE4
		public void Logout()
		{
			MobileAuth.Logout();
		}

		// Token: 0x0600C194 RID: 49556 RVA: 0x003AC9EB File Offset: 0x003AABEB
		public void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentChallenge(url, authenticationDelegate);
		}

		// Token: 0x0600C195 RID: 49557 RVA: 0x003AC9F4 File Offset: 0x003AABF4
		public void PresentLogin(IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentLogin(authenticationDelegate);
		}

		// Token: 0x0600C196 RID: 49558 RVA: 0x003AC9FC File Offset: 0x003AABFC
		public void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentHealUpSoftAccount(account, authenticationDelegate);
		}

		// Token: 0x0600C197 RID: 49559 RVA: 0x003ACA05 File Offset: 0x003AAC05
		public void RemoveAccountById(string accountId, IOnAccountRemovedListener removedListener)
		{
			MobileAuth.RemoveAccountById(accountId, removedListener);
		}
	}
}
