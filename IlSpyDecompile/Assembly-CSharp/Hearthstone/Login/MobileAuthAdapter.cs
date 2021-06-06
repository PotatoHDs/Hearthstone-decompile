using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	internal class MobileAuthAdapter : IMobileAuth
	{
		public bool IsAuthenticated => MobileAuth.IsAuthenticated;

		public void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.AuthenticateSoftAccount(softAccountId, authenticationDelegate);
		}

		public Region[] BuiltInRegions()
		{
			return MobileAuth.BuiltInRegions();
		}

		public void Configure(Configuration configuration)
		{
			MobileAuth.Configure(configuration);
		}

		public void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener)
		{
			MobileAuth.GenerateGuestSoftAccountId(region, listener);
		}

		public Account? GetAuthenticatedAccount()
		{
			return MobileAuth.GetAuthenticatedAccount();
		}

		public List<Account> GetSoftAccounts()
		{
			List<Account> list;
			try
			{
				list = MobileAuth.GetSoftAccounts();
			}
			catch (Exception ex)
			{
				Log.Login.PrintDebug("Unexepcted exceception from mobile auth {0}:\n{1}", ex.Message, ex.StackTrace);
				list = new List<Account>();
			}
			if (list == null)
			{
				Log.Login.PrintDebug("Unexpected null soft account list");
				list = new List<Account>();
			}
			return list;
		}

		public List<Account> GetAllAccounts()
		{
			List<Account> list;
			try
			{
				list = MobileAuth.GetAllAccounts();
			}
			catch (Exception ex)
			{
				Log.Login.PrintDebug("Unexepcted exceception from mobile auth:{0}\n{1}", ex.Message, ex.StackTrace);
				list = new List<Account>();
			}
			if (list == null)
			{
				Log.Login.PrintDebug("Unexpected null full account list");
				list = new List<Account>();
			}
			return list;
		}

		public void AuthenticateAccount(Account account, string regionId, IAuthenticationDelegate callback)
		{
			MobileAuth.AuthenticateAccount(account, regionId, callback);
		}

		public void ImportAccount(string authenticationToken, string regionCode, IImportAccountCallback importCallback)
		{
			MobileAuth.ImportAccount(Blizzard.MobileAuth.ImportAccount.CreateWithHardAccountAuthenticationToken(authenticationToken, regionCode), importCallback);
		}

		public void ImportGuestAccount(string guestId, string regionId, IImportAccountCallback importCallback)
		{
			MobileAuth.ImportAccount(Blizzard.MobileAuth.ImportAccount.CreateWithSoftAccountBnetGuestID(guestId, regionId), importCallback);
		}

		public void Logout()
		{
			MobileAuth.Logout();
		}

		public void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentChallenge(url, authenticationDelegate);
		}

		public void PresentLogin(IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentLogin(authenticationDelegate);
		}

		public void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate)
		{
			MobileAuth.PresentHealUpSoftAccount(account, authenticationDelegate);
		}

		public void RemoveAccountById(string accountId, IOnAccountRemovedListener removedListener)
		{
			MobileAuth.RemoveAccountById(accountId, removedListener);
		}
	}
}
