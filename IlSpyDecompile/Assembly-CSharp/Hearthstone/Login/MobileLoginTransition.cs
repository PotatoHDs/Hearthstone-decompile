using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	internal class MobileLoginTransition
	{
		private enum LoginType
		{
			UNKNOWN,
			LEGACY,
			MASDK
		}

		private IMobileAuth MobileAuth { get; set; }

		private LoginType CurrentLogin { get; set; }

		public MobileLoginTransition(bool isMASDKEnabled, IMobileAuth mobileAuth)
		{
			MobileAuth = mobileAuth;
			CurrentLogin = ((!isMASDKEnabled) ? LoginType.LEGACY : LoginType.MASDK);
			Log.Login.PrintDebug("Transition created with Current Login {0}", CurrentLogin);
		}

		public void OnTokenFetchStarted()
		{
			LoginType lastLoginType = GetLastLoginType();
			Log.Login.PrintDebug("Transition Fetch started. Current: {0} LastLogin: {1}", CurrentLogin, lastLoginType);
			if (CurrentLogin == LoginType.MASDK && lastLoginType == LoginType.LEGACY)
			{
				TransitionFromLegacyToMASDK();
			}
			else if (CurrentLogin == LoginType.LEGACY && lastLoginType == LoginType.MASDK)
			{
				TransitionFromMASDKToLegacy();
			}
			SetLastLoginType(CurrentLogin);
		}

		public void OnLoginTokenFetched()
		{
			if (CurrentLogin == LoginType.MASDK)
			{
				SaveMASDKAccountInfo();
			}
		}

		public void OnClearAuthentication()
		{
			ClearSavedAccountInfo();
		}

		private LoginType GetLastLoginType()
		{
			return Options.Get().GetEnum(Option.LAST_LOGIN_TYPE, LoginType.UNKNOWN);
		}

		private void SetLastLoginType(LoginType loginType)
		{
			Options.Get().SetEnum(Option.LAST_LOGIN_TYPE, loginType);
		}

		private void SaveMASDKAccountInfo()
		{
			Account? authenticatedAccount = MobileAuth.GetAuthenticatedAccount();
			Log.Login.PrintDebug($"Saving account for transition.\n                Token {!string.IsNullOrEmpty(authenticatedAccount?.authenticationToken)} |\n                guestId {!string.IsNullOrEmpty(authenticatedAccount?.bnetGuestId)}");
			SaveTransitionAccountInformation(authenticatedAccount?.authenticationToken, authenticatedAccount?.bnetGuestId);
		}

		private void TransitionFromLegacyToMASDK()
		{
			Log.Login.PrintInfo("Transitioning from Legacy to MASDK");
			HearthstoneServices.Get<ILoginService>()?.ClearAllSavedAccounts();
		}

		private void TransitionFromMASDKToLegacy()
		{
			Log.Login.PrintInfo("Transitioning from MASDK to Legacy");
			var (text, text2) = GetSavedAccountInfo();
			Log.Login.PrintDebug($"Got saved information:\n                Token {!string.IsNullOrEmpty(text)} | GuestId {!string.IsNullOrEmpty(text2)}");
			if (!string.IsNullOrEmpty(text))
			{
				WebAuth.SetStoredToken(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				TemporaryAccountManager.Get().CreatedTemporaryAccount(text2);
			}
		}

		private void SaveTransitionAccountInformation(string authToken, string guestAccountId)
		{
			Options options = Options.Get();
			if (!string.IsNullOrEmpty(authToken))
			{
				options.SetString(Option.TRANSITION_AUTH_TOKEN, authToken);
			}
			if (!string.IsNullOrEmpty(guestAccountId))
			{
				options.SetString(Option.TRANSITION_GUEST_ID, guestAccountId);
			}
		}

		private (string token, string guestAccountId) GetSavedAccountInfo()
		{
			Options options = Options.Get();
			string @string = options.GetString(Option.TRANSITION_AUTH_TOKEN, null);
			string string2 = options.GetString(Option.TRANSITION_GUEST_ID, null);
			return (@string, string2);
		}

		private void ClearSavedAccountInfo()
		{
			Log.Login.PrintInfo("Clearing saved transition info");
			Options options = Options.Get();
			options.DeleteOption(Option.TRANSITION_AUTH_TOKEN);
			options.DeleteOption(Option.TRANSITION_GUEST_ID);
		}
	}
}
