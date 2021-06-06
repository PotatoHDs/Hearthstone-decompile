using System;
using System.Collections.Generic;
using bgs;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone.Core;
using Hearthstone.Streaming;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x0200113C RID: 4412
	public class LoginService : ILoginService, IService, IHasUpdate
	{
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x0600C13B RID: 49467 RVA: 0x003AB898 File Offset: 0x003A9A98
		// (set) Token: 0x0600C13C RID: 49468 RVA: 0x003AB8A0 File Offset: 0x003A9AA0
		private ILogger Logger { get; set; }

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x0600C13D RID: 49469 RVA: 0x003AB8A9 File Offset: 0x003A9AA9
		// (set) Token: 0x0600C13E RID: 49470 RVA: 0x003AB8B1 File Offset: 0x003A9AB1
		private IMobileAuth MobileAuth { get; set; }

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600C13F RID: 49471 RVA: 0x003AB8BA File Offset: 0x003A9ABA
		// (set) Token: 0x0600C140 RID: 49472 RVA: 0x003AB8C2 File Offset: 0x003A9AC2
		private MASDKAccountHealup AccountHealup { get; set; }

		// Token: 0x0600C141 RID: 49473 RVA: 0x003AB8CB File Offset: 0x003A9ACB
		public LoginService(ILogger logger)
		{
			this.Logger = logger;
			this.MobileAuth = new MobileAuthAdapter();
			this.AccountHealup = new MASDKAccountHealup(logger, TelemetryManager.Client());
		}

		// Token: 0x0600C142 RID: 49474 RVA: 0x003AB8F6 File Offset: 0x003A9AF6
		public Type[] GetDependencies()
		{
			return new Type[]
			{
				typeof(Network),
				typeof(GameDownloadManager)
			};
		}

		// Token: 0x0600C143 RID: 49475 RVA: 0x003AB918 File Offset: 0x003A9B18
		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				yield return new WaitForGameDownloadManagerState();
			}
			this.m_tokenFetcher = this.CreatePlatformTokenFetcher(this.Logger, serviceLocator);
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				this.m_mobileLoginTransition = new MobileLoginTransition(this.IsMASDKEnabled(), this.MobileAuth);
			}
			yield break;
		}

		// Token: 0x0600C144 RID: 49476 RVA: 0x0010920D File Offset: 0x0010740D
		public bool IsLoggedIn()
		{
			return Network.IsLoggedIn();
		}

		// Token: 0x0600C145 RID: 49477 RVA: 0x003AB92E File Offset: 0x003A9B2E
		public bool IsAttemptingLogin()
		{
			return this.m_processWebAuth;
		}

		// Token: 0x0600C146 RID: 49478 RVA: 0x003AB936 File Offset: 0x003A9B36
		public void Shutdown()
		{
			this.m_tokenFetcher = null;
		}

		// Token: 0x0600C147 RID: 49479 RVA: 0x003AB93F File Offset: 0x003A9B3F
		public void ClearAuthentication()
		{
			IPlatformLoginTokenFetcher tokenFetcher = this.m_tokenFetcher;
			if (tokenFetcher != null)
			{
				tokenFetcher.ClearCachedAuthentication();
			}
			MobileLoginTransition mobileLoginTransition = this.m_mobileLoginTransition;
			if (mobileLoginTransition == null)
			{
				return;
			}
			mobileLoginTransition.OnClearAuthentication();
		}

		// Token: 0x0600C148 RID: 49480 RVA: 0x003AB964 File Offset: 0x003A9B64
		public void ClearAllSavedAccounts()
		{
			if (!HearthstoneApplication.IsInternal() || !this.IsMASDKEnabled())
			{
				return;
			}
			List<Account> allAccounts = Blizzard.MobileAuth.MobileAuth.GetAllAccounts();
			if (allAccounts != null)
			{
				foreach (Account account in allAccounts)
				{
					ILogger logger = this.Logger;
					if (logger != null)
					{
						logger.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting remove account {0} {1}", new object[]
						{
							account.accountId,
							account.battleTag
						});
					}
					this.MobileAuth.RemoveAccountById(account.accountId, new LoginService.AccountRemoveLogger(account.accountId, this.Logger));
				}
				return;
			}
			ILogger logger2 = this.Logger;
			if (logger2 == null)
			{
				return;
			}
			logger2.Log(Blizzard.T5.Core.LogLevel.Information, "There were no accounts to remove", Array.Empty<object>());
		}

		// Token: 0x0600C149 RID: 49481 RVA: 0x003ABA34 File Offset: 0x003A9C34
		public void StartLogin()
		{
			if (this.IsLoggedIn() || this.IsAttemptingLogin())
			{
				return;
			}
			MobileLoginTransition mobileLoginTransition = this.m_mobileLoginTransition;
			if (mobileLoginTransition != null)
			{
				mobileLoginTransition.OnTokenFetchStarted();
			}
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Beginning processing login challenges", Array.Empty<object>());
			}
			this.ResetToStartingLoginState();
		}

		// Token: 0x0600C14A RID: 49482 RVA: 0x003ABA85 File Offset: 0x003A9C85
		public bool RequireRegionSwitchOnSwitchAccount()
		{
			return this.IsMASDKEnabled();
		}

		// Token: 0x0600C14B RID: 49483 RVA: 0x003ABA8D File Offset: 0x003A9C8D
		private void ResetToStartingLoginState()
		{
			this.m_processWebAuth = true;
			this.m_lastAuthKeyAttempted = null;
			this.m_lastAuthKeyRepeated = false;
			this.m_loginChallengesProcessed = 0;
			HearthstoneApplication.Get().Resetting += this.ClearStartLoginStates;
		}

		// Token: 0x0600C14C RID: 49484 RVA: 0x003ABA85 File Offset: 0x003A9C85
		public bool SupportsAccountHealup()
		{
			return this.IsMASDKEnabled();
		}

		// Token: 0x0600C14D RID: 49485 RVA: 0x003ABAC1 File Offset: 0x003A9CC1
		public void HealupCurrentTemporaryAccount(Action<bool> finishedCallback = null)
		{
			this.AccountHealup.StartHealup(this.MobileAuth, finishedCallback);
		}

		// Token: 0x0600C14E RID: 49486 RVA: 0x003ABAD8 File Offset: 0x003A9CD8
		public string GetAccountIdForAuthenticatedAccount()
		{
			if (!this.IsMASDKEnabled() || !this.MobileAuth.IsAuthenticated)
			{
				return string.Empty;
			}
			Account? authenticatedAccount = this.MobileAuth.GetAuthenticatedAccount();
			if (authenticatedAccount == null)
			{
				return string.Empty;
			}
			return authenticatedAccount.Value.accountId;
		}

		// Token: 0x0600C14F RID: 49487 RVA: 0x003ABB28 File Offset: 0x003A9D28
		public string GetRegionForAuthenticatedAccount()
		{
			if (!this.IsMASDKEnabled() || !this.MobileAuth.IsAuthenticated)
			{
				return string.Empty;
			}
			Account? authenticatedAccount = this.MobileAuth.GetAuthenticatedAccount();
			if (authenticatedAccount == null)
			{
				return string.Empty;
			}
			return authenticatedAccount.Value.regionId;
		}

		// Token: 0x0600C150 RID: 49488 RVA: 0x003ABB78 File Offset: 0x003A9D78
		public void Update()
		{
			if (!this.m_processWebAuth)
			{
				return;
			}
			if (!this.IsLoggedIn())
			{
				if (BattleNet.HasExternalChallenge())
				{
					string text;
					if (!BattleNet.CheckWebAuth(out text))
					{
						ILogger logger = this.Logger;
						if (logger == null)
						{
							return;
						}
						logger.Log(Blizzard.T5.Core.LogLevel.Warning, "Went to process a challenge but there was no url", Array.Empty<object>());
						return;
					}
					else
					{
						text = LoginEnvironmentUtil.OverrideEnvironmentIfNeeded(text);
						if (this.m_loginChallengesProcessed > 0 && this.m_lastAuthKeyRepeated)
						{
							ILogger logger2 = this.Logger;
							if (logger2 != null)
							{
								logger2.Log(Blizzard.T5.Core.LogLevel.Information, "Multiple attempts likely failed with same auth token, clearing authentication data", Array.Empty<object>());
							}
							this.m_lastAuthKeyRepeated = false;
							this.ClearAuthentication();
						}
						ILogger logger3 = this.Logger;
						if (logger3 != null)
						{
							logger3.Log(Blizzard.T5.Core.LogLevel.Information, "Processing challenge, previous login challenge attempts {0}", new object[]
							{
								this.m_loginChallengesProcessed
							});
						}
						this.ProcessChallenge(text);
						if (LoginService.IsLoginChallenge(text))
						{
							this.m_loginChallengesProcessed++;
						}
					}
				}
				return;
			}
			this.ClearStartLoginStates();
			ILogger logger4 = this.Logger;
			if (logger4 == null)
			{
				return;
			}
			logger4.Log(Blizzard.T5.Core.LogLevel.Information, "We are now logged in, stopping processing challenges", Array.Empty<object>());
		}

		// Token: 0x0600C151 RID: 49489 RVA: 0x003ABC70 File Offset: 0x003A9E70
		private void ClearStartLoginStates()
		{
			this.m_processWebAuth = false;
			this.m_lastAuthKeyAttempted = null;
			this.m_lastAuthKeyRepeated = false;
			this.m_loginChallengesProcessed = 0;
			HearthstoneApplication.Get().Resetting -= this.ClearStartLoginStates;
		}

		// Token: 0x0600C152 RID: 49490 RVA: 0x003ABCA4 File Offset: 0x003A9EA4
		private static bool IsLoginChallenge(string challengeUrl)
		{
			return challengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture);
		}

		// Token: 0x0600C153 RID: 49491 RVA: 0x003ABCB2 File Offset: 0x003A9EB2
		private void OnProvideTokenFailure()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Error, "Failed to get token to respond to login challenge", Array.Empty<object>());
			}
			TelemetryManager.Client().SendWebLoginError();
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
		}

		// Token: 0x0600C154 RID: 49492 RVA: 0x003ABCE9 File Offset: 0x003A9EE9
		private void OnProvideTokenCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "User canceled login challenge", Array.Empty<object>());
			}
			HearthstoneApplication.Get().Reset();
		}

		// Token: 0x0600C155 RID: 49493 RVA: 0x003ABD14 File Offset: 0x003A9F14
		private void ProcessChallenge(string challengeUrl)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Debug, "Starting challenge process for url {0}", new object[]
				{
					challengeUrl
				});
			}
			HearthstoneApplication.SendStartupTimeTelemetry("LoginService.ProcessChallenge");
			Processor.QueueJobIfNotExist("Job_ProcessChallenge", this.Job_ProcessChallenge(challengeUrl), Array.Empty<IJobDependency>());
		}

		// Token: 0x0600C156 RID: 49494 RVA: 0x003ABD63 File Offset: 0x003A9F63
		private IEnumerator<IAsyncJobResult> Job_ProcessChallenge(string challengeUrl)
		{
			FetchLoginToken tokenFetchDepenency = new FetchLoginToken(challengeUrl, this.m_tokenFetcher);
			yield return tokenFetchDepenency;
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			switch (tokenFetchDepenency.CurrentStatus)
			{
			case FetchLoginToken.Status.Success:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.SUCCESS);
				this.OnProvideTokenSuccess(tokenFetchDepenency.Token);
				break;
			case FetchLoginToken.Status.Cancelled:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.CANCELED);
				this.OnProvideTokenCancelled();
				break;
			case FetchLoginToken.Status.Failed:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.FAILURE);
				this.OnProvideTokenFailure();
				break;
			}
			yield break;
		}

		// Token: 0x0600C157 RID: 49495 RVA: 0x003ABD7C File Offset: 0x003A9F7C
		private void OnProvideTokenSuccess(string token)
		{
			BattleNet.ProvideWebAuthToken(token, null);
			this.m_lastAuthKeyRepeated = (!string.IsNullOrEmpty(this.m_lastAuthKeyAttempted) && this.m_lastAuthKeyAttempted == token);
			this.m_lastAuthKeyAttempted = token;
			MobileLoginTransition mobileLoginTransition = this.m_mobileLoginTransition;
			if (mobileLoginTransition == null)
			{
				return;
			}
			mobileLoginTransition.OnLoginTokenFetched();
		}

		// Token: 0x0600C158 RID: 49496 RVA: 0x003ABDC9 File Offset: 0x003A9FC9
		private IPlatformLoginTokenFetcher CreatePlatformTokenFetcher(ILogger logger, ServiceLocator serviceLocator)
		{
			return new DesktopLoginTokenFetcher(logger);
		}

		// Token: 0x0600C159 RID: 49497 RVA: 0x003ABDD1 File Offset: 0x003A9FD1
		private bool IsMASDKEnabled()
		{
			return PlatformSettings.IsMobileRuntimeOS && Vars.Key("Mobile.UseMASDK").GetBool(true);
		}

		// Token: 0x04009C10 RID: 39952
		private bool m_processWebAuth;

		// Token: 0x04009C11 RID: 39953
		private IPlatformLoginTokenFetcher m_tokenFetcher;

		// Token: 0x04009C12 RID: 39954
		private MobileLoginTransition m_mobileLoginTransition;

		// Token: 0x04009C13 RID: 39955
		private int m_loginChallengesProcessed;

		// Token: 0x04009C14 RID: 39956
		private string m_lastAuthKeyAttempted;

		// Token: 0x04009C15 RID: 39957
		private bool m_lastAuthKeyRepeated;

		// Token: 0x02002912 RID: 10514
		private class AccountRemoveLogger : IOnAccountRemovedListener
		{
			// Token: 0x17002D5E RID: 11614
			// (get) Token: 0x06013DD9 RID: 81369 RVA: 0x0053E546 File Offset: 0x0053C746
			// (set) Token: 0x06013DDA RID: 81370 RVA: 0x0053E54E File Offset: 0x0053C74E
			private string AccountId { get; set; }

			// Token: 0x17002D5F RID: 11615
			// (get) Token: 0x06013DDB RID: 81371 RVA: 0x0053E557 File Offset: 0x0053C757
			// (set) Token: 0x06013DDC RID: 81372 RVA: 0x0053E55F File Offset: 0x0053C75F
			private ILogger Logger { get; set; }

			// Token: 0x06013DDD RID: 81373 RVA: 0x0053E568 File Offset: 0x0053C768
			public AccountRemoveLogger(string accountId, ILogger logger)
			{
				this.AccountId = accountId;
				this.Logger = logger;
			}

			// Token: 0x06013DDE RID: 81374 RVA: 0x0053E57E File Offset: 0x0053C77E
			public void OnAccountRemovedResult(bool result)
			{
				ILogger logger = this.Logger;
				if (logger == null)
				{
					return;
				}
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Removal of acccount {0} has {1}", new object[]
				{
					this.AccountId,
					result ? "Succeeded" : "Failed"
				});
			}
		}
	}
}
