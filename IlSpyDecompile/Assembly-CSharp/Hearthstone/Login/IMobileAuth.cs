using System.Collections.Generic;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	public interface IMobileAuth
	{
		bool IsAuthenticated { get; }

		void Configure(Configuration configuration);

		void PresentLogin(IAuthenticationDelegate authenticationDelegate);

		void PresentChallenge(string url, IAuthenticationDelegate authenticationDelegate);

		Region[] BuiltInRegions();

		void Logout();

		Account? GetAuthenticatedAccount();

		void ImportAccount(string authenticationToken, string regionCode, IImportAccountCallback authenticationDelegate);

		void AuthenticateSoftAccount(GuestSoftAccountId softAccountId, IAuthenticationDelegate authenticationDelegate);

		void GenerateGuestSoftAccountId(Region region, IGuestSoftAccountIdListener listener);

		List<Account> GetSoftAccounts();

		void ImportGuestAccount(string guestId, string regionId, IImportAccountCallback importCallback);

		void AuthenticateAccount(Account account, string regionId, IAuthenticationDelegate callback);

		List<Account> GetAllAccounts();

		void PresentHealUpSoftAccount(Account account, IAuthenticationDelegate authenticationDelegate);

		void RemoveAccountById(string accountId, IOnAccountRemovedListener removedListener);
	}
}
