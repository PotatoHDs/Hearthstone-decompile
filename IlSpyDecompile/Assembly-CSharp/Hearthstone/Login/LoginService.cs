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
	public class LoginService : ILoginService, IService, IHasUpdate
	{
		private class AccountRemoveLogger : IOnAccountRemovedListener
		{
			private string AccountId { get; set; }

			private ILogger Logger { get; set; }

			public AccountRemoveLogger(string accountId, ILogger logger)
			{
				AccountId = accountId;
				Logger = logger;
			}

			public void OnAccountRemovedResult(bool result)
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Removal of acccount {0} has {1}", AccountId, result ? "Succeeded" : "Failed");
			}
		}

		private bool m_processWebAuth;

		private IPlatformLoginTokenFetcher m_tokenFetcher;

		private MobileLoginTransition m_mobileLoginTransition;

		private int m_loginChallengesProcessed;

		private string m_lastAuthKeyAttempted;

		private bool m_lastAuthKeyRepeated;

		private ILogger Logger { get; set; }

		private IMobileAuth MobileAuth { get; set; }

		private MASDKAccountHealup AccountHealup { get; set; }

		public LoginService(ILogger logger)
		{
			Logger = logger;
			MobileAuth = new MobileAuthAdapter();
			AccountHealup = new MASDKAccountHealup(logger, TelemetryManager.Client());
		}

		public Type[] GetDependencies()
		{
			return new Type[2]
			{
				typeof(Network),
				typeof(GameDownloadManager)
			};
		}

		public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				yield return new WaitForGameDownloadManagerState();
			}
			m_tokenFetcher = CreatePlatformTokenFetcher(Logger, serviceLocator);
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				m_mobileLoginTransition = new MobileLoginTransition(IsMASDKEnabled(), MobileAuth);
			}
		}

		public bool IsLoggedIn()
		{
			return Network.IsLoggedIn();
		}

		public bool IsAttemptingLogin()
		{
			return m_processWebAuth;
		}

		public void Shutdown()
		{
			m_tokenFetcher = null;
		}

		public void ClearAuthentication()
		{
			m_tokenFetcher?.ClearCachedAuthentication();
			m_mobileLoginTransition?.OnClearAuthentication();
		}

		public void ClearAllSavedAccounts()
		{
			if (!HearthstoneApplication.IsInternal() || !IsMASDKEnabled())
			{
				return;
			}
			List<Account> allAccounts = Blizzard.MobileAuth.MobileAuth.GetAllAccounts();
			if (allAccounts == null)
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "There were no accounts to remove");
				return;
			}
			foreach (Account item in allAccounts)
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting remove account {0} {1}", item.accountId, item.battleTag);
				MobileAuth.RemoveAccountById(item.accountId, new AccountRemoveLogger(item.accountId, Logger));
			}
		}

		public void StartLogin()
		{
			if (!IsLoggedIn() && !IsAttemptingLogin())
			{
				m_mobileLoginTransition?.OnTokenFetchStarted();
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Beginning processing login challenges");
				ResetToStartingLoginState();
			}
		}

		public bool RequireRegionSwitchOnSwitchAccount()
		{
			return IsMASDKEnabled();
		}

		private void ResetToStartingLoginState()
		{
			m_processWebAuth = true;
			m_lastAuthKeyAttempted = null;
			m_lastAuthKeyRepeated = false;
			m_loginChallengesProcessed = 0;
			HearthstoneApplication.Get().Resetting += ClearStartLoginStates;
		}

		public bool SupportsAccountHealup()
		{
			return IsMASDKEnabled();
		}

		public void HealupCurrentTemporaryAccount(Action<bool> finishedCallback = null)
		{
			AccountHealup.StartHealup(MobileAuth, finishedCallback);
		}

		public string GetAccountIdForAuthenticatedAccount()
		{
			if (!IsMASDKEnabled() || !MobileAuth.IsAuthenticated)
			{
				return string.Empty;
			}
			Account? authenticatedAccount = MobileAuth.GetAuthenticatedAccount();
			if (!authenticatedAccount.HasValue)
			{
				return string.Empty;
			}
			return authenticatedAccount.Value.accountId;
		}

		public string GetRegionForAuthenticatedAccount()
		{
			if (!IsMASDKEnabled() || !MobileAuth.IsAuthenticated)
			{
				return string.Empty;
			}
			Account? authenticatedAccount = MobileAuth.GetAuthenticatedAccount();
			if (!authenticatedAccount.HasValue)
			{
				return string.Empty;
			}
			return authenticatedAccount.Value.regionId;
		}

		public void Update()
		{
			if (!m_processWebAuth)
			{
				return;
			}
			if (IsLoggedIn())
			{
				ClearStartLoginStates();
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "We are now logged in, stopping processing challenges");
			}
			else
			{
				if (!BattleNet.HasExternalChallenge())
				{
					return;
				}
				if (!BattleNet.CheckWebAuth(out var url))
				{
					Logger?.Log(Blizzard.T5.Core.LogLevel.Warning, "Went to process a challenge but there was no url");
					return;
				}
				url = LoginEnvironmentUtil.OverrideEnvironmentIfNeeded(url);
				if (m_loginChallengesProcessed > 0 && m_lastAuthKeyRepeated)
				{
					Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Multiple attempts likely failed with same auth token, clearing authentication data");
					m_lastAuthKeyRepeated = false;
					ClearAuthentication();
				}
				Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Processing challenge, previous login challenge attempts {0}", m_loginChallengesProcessed);
				ProcessChallenge(url);
				if (IsLoginChallenge(url))
				{
					m_loginChallengesProcessed++;
				}
			}
		}

		private void ClearStartLoginStates()
		{
			m_processWebAuth = false;
			m_lastAuthKeyAttempted = null;
			m_lastAuthKeyRepeated = false;
			m_loginChallengesProcessed = 0;
			HearthstoneApplication.Get().Resetting -= ClearStartLoginStates;
		}

		private static bool IsLoginChallenge(string challengeUrl)
		{
			return challengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture);
		}

		private void OnProvideTokenFailure()
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Failed to get token to respond to login challenge");
			TelemetryManager.Client().SendWebLoginError();
			Network.Get().ShowConnectionFailureError("GLOBAL_ERROR_NETWORK_LOGIN_FAILURE");
		}

		private void OnProvideTokenCancelled()
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "User canceled login challenge");
			HearthstoneApplication.Get().Reset();
		}

		private void ProcessChallenge(string challengeUrl)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "Starting challenge process for url {0}", challengeUrl);
			HearthstoneApplication.SendStartupTimeTelemetry("LoginService.ProcessChallenge");
			Processor.QueueJobIfNotExist("Job_ProcessChallenge", Job_ProcessChallenge(challengeUrl));
		}

		private IEnumerator<IAsyncJobResult> Job_ProcessChallenge(string challengeUrl)
		{
			FetchLoginToken tokenFetchDepenency = new FetchLoginToken(challengeUrl, m_tokenFetcher);
			yield return tokenFetchDepenency;
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			switch (tokenFetchDepenency.CurrentStatus)
			{
			case FetchLoginToken.Status.Success:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.SUCCESS);
				OnProvideTokenSuccess(tokenFetchDepenency.Token);
				break;
			case FetchLoginToken.Status.Cancelled:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.CANCELED);
				OnProvideTokenCancelled();
				break;
			case FetchLoginToken.Status.Failed:
				telemetryClient.SendLoginTokenFetchResult(LoginTokenFetchResult.TokenFetchResult.FAILURE);
				OnProvideTokenFailure();
				break;
			}
		}

		private void OnProvideTokenSuccess(string token)
		{
			BattleNet.ProvideWebAuthToken(token);
			m_lastAuthKeyRepeated = !string.IsNullOrEmpty(m_lastAuthKeyAttempted) && m_lastAuthKeyAttempted == token;
			m_lastAuthKeyAttempted = token;
			m_mobileLoginTransition?.OnLoginTokenFetched();
		}

		private IPlatformLoginTokenFetcher CreatePlatformTokenFetcher(ILogger logger, ServiceLocator serviceLocator)
		{
			return new DesktopLoginTokenFetcher(logger);
		}

		private bool IsMASDKEnabled()
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				return Vars.Key("Mobile.UseMASDK").GetBool(def: true);
			}
			return false;
		}
	}
}
