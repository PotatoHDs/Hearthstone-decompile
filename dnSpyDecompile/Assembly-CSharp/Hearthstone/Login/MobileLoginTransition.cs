using System;
using System.Runtime.CompilerServices;
using Blizzard.MobileAuth;

namespace Hearthstone.Login
{
	// Token: 0x02001143 RID: 4419
	internal class MobileLoginTransition
	{
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x0600C1A1 RID: 49569 RVA: 0x003ACA8C File Offset: 0x003AAC8C
		// (set) Token: 0x0600C1A2 RID: 49570 RVA: 0x003ACA94 File Offset: 0x003AAC94
		private IMobileAuth MobileAuth { get; set; }

		// Token: 0x17000D73 RID: 3443
		// (get) Token: 0x0600C1A3 RID: 49571 RVA: 0x003ACA9D File Offset: 0x003AAC9D
		// (set) Token: 0x0600C1A4 RID: 49572 RVA: 0x003ACAA5 File Offset: 0x003AACA5
		private MobileLoginTransition.LoginType CurrentLogin { get; set; }

		// Token: 0x0600C1A5 RID: 49573 RVA: 0x003ACAAE File Offset: 0x003AACAE
		public MobileLoginTransition(bool isMASDKEnabled, IMobileAuth mobileAuth)
		{
			this.MobileAuth = mobileAuth;
			this.CurrentLogin = (isMASDKEnabled ? MobileLoginTransition.LoginType.MASDK : MobileLoginTransition.LoginType.LEGACY);
			Log.Login.PrintDebug("Transition created with Current Login {0}", new object[]
			{
				this.CurrentLogin
			});
		}

		// Token: 0x0600C1A6 RID: 49574 RVA: 0x003ACAF0 File Offset: 0x003AACF0
		public void OnTokenFetchStarted()
		{
			MobileLoginTransition.LoginType lastLoginType = this.GetLastLoginType();
			Log.Login.PrintDebug("Transition Fetch started. Current: {0} LastLogin: {1}", new object[]
			{
				this.CurrentLogin,
				lastLoginType
			});
			if (this.CurrentLogin == MobileLoginTransition.LoginType.MASDK && lastLoginType == MobileLoginTransition.LoginType.LEGACY)
			{
				this.TransitionFromLegacyToMASDK();
			}
			else if (this.CurrentLogin == MobileLoginTransition.LoginType.LEGACY && lastLoginType == MobileLoginTransition.LoginType.MASDK)
			{
				this.TransitionFromMASDKToLegacy();
			}
			this.SetLastLoginType(this.CurrentLogin);
		}

		// Token: 0x0600C1A7 RID: 49575 RVA: 0x003ACB64 File Offset: 0x003AAD64
		public void OnLoginTokenFetched()
		{
			if (this.CurrentLogin == MobileLoginTransition.LoginType.MASDK)
			{
				this.SaveMASDKAccountInfo();
			}
		}

		// Token: 0x0600C1A8 RID: 49576 RVA: 0x003ACB75 File Offset: 0x003AAD75
		public void OnClearAuthentication()
		{
			this.ClearSavedAccountInfo();
		}

		// Token: 0x0600C1A9 RID: 49577 RVA: 0x003ACB7D File Offset: 0x003AAD7D
		private MobileLoginTransition.LoginType GetLastLoginType()
		{
			return Options.Get().GetEnum<MobileLoginTransition.LoginType>(Option.LAST_LOGIN_TYPE, MobileLoginTransition.LoginType.UNKNOWN);
		}

		// Token: 0x0600C1AA RID: 49578 RVA: 0x003ACB8C File Offset: 0x003AAD8C
		private void SetLastLoginType(MobileLoginTransition.LoginType loginType)
		{
			Options.Get().SetEnum<MobileLoginTransition.LoginType>(Option.LAST_LOGIN_TYPE, loginType);
		}

		// Token: 0x0600C1AB RID: 49579 RVA: 0x003ACB9C File Offset: 0x003AAD9C
		private void SaveMASDKAccountInfo()
		{
			Account? authenticatedAccount = this.MobileAuth.GetAuthenticatedAccount();
			Log.Login.PrintDebug(string.Format("Saving account for transition.\n                Token {0} |\n                guestId {1}", !string.IsNullOrEmpty((authenticatedAccount != null) ? authenticatedAccount.GetValueOrDefault().authenticationToken : null), !string.IsNullOrEmpty((authenticatedAccount != null) ? authenticatedAccount.GetValueOrDefault().bnetGuestId : null)), Array.Empty<object>());
			this.SaveTransitionAccountInformation((authenticatedAccount != null) ? authenticatedAccount.GetValueOrDefault().authenticationToken : null, (authenticatedAccount != null) ? authenticatedAccount.GetValueOrDefault().bnetGuestId : null);
		}

		// Token: 0x0600C1AC RID: 49580 RVA: 0x003ACC4E File Offset: 0x003AAE4E
		private void TransitionFromLegacyToMASDK()
		{
			Log.Login.PrintInfo("Transitioning from Legacy to MASDK", Array.Empty<object>());
			ILoginService loginService = HearthstoneServices.Get<ILoginService>();
			if (loginService == null)
			{
				return;
			}
			loginService.ClearAllSavedAccounts();
		}

		// Token: 0x0600C1AD RID: 49581 RVA: 0x003ACC74 File Offset: 0x003AAE74
		private void TransitionFromMASDKToLegacy()
		{
			Log.Login.PrintInfo("Transitioning from MASDK to Legacy", Array.Empty<object>());
			ValueTuple<string, string> savedAccountInfo = this.GetSavedAccountInfo();
			string item = savedAccountInfo.Item1;
			string item2 = savedAccountInfo.Item2;
			Log.Login.PrintDebug(string.Format("Got saved information:\n                Token {0} | GuestId {1}", !string.IsNullOrEmpty(item), !string.IsNullOrEmpty(item2)), Array.Empty<object>());
			if (!string.IsNullOrEmpty(item))
			{
				WebAuth.SetStoredToken(item);
			}
			if (!string.IsNullOrEmpty(item2))
			{
				TemporaryAccountManager.Get().CreatedTemporaryAccount(item2);
			}
		}

		// Token: 0x0600C1AE RID: 49582 RVA: 0x003ACD00 File Offset: 0x003AAF00
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

		// Token: 0x0600C1AF RID: 49583 RVA: 0x003ACD38 File Offset: 0x003AAF38
		[return: TupleElementNames(new string[]
		{
			"token",
			"guestAccountId"
		})]
		private ValueTuple<string, string> GetSavedAccountInfo()
		{
			Options options = Options.Get();
			string @string = options.GetString(Option.TRANSITION_AUTH_TOKEN, null);
			string string2 = options.GetString(Option.TRANSITION_GUEST_ID, null);
			return new ValueTuple<string, string>(@string, string2);
		}

		// Token: 0x0600C1B0 RID: 49584 RVA: 0x003ACD64 File Offset: 0x003AAF64
		private void ClearSavedAccountInfo()
		{
			Log.Login.PrintInfo("Clearing saved transition info", Array.Empty<object>());
			Options options = Options.Get();
			options.DeleteOption(Option.TRANSITION_AUTH_TOKEN);
			options.DeleteOption(Option.TRANSITION_GUEST_ID);
		}

		// Token: 0x02002917 RID: 10519
		private enum LoginType
		{
			// Token: 0x0400FBC4 RID: 64452
			UNKNOWN,
			// Token: 0x0400FBC5 RID: 64453
			LEGACY,
			// Token: 0x0400FBC6 RID: 64454
			MASDK
		}
	}
}
