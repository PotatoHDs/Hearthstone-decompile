using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using UnityEngine;

namespace Hearthstone.Login
{
	// Token: 0x02001140 RID: 4416
	public class MASDKTokenFetcher : IPlatformLoginTokenFetcher
	{
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x0600C177 RID: 49527 RVA: 0x003AC4C7 File Offset: 0x003AA6C7
		// (set) Token: 0x0600C178 RID: 49528 RVA: 0x003AC4CF File Offset: 0x003AA6CF
		private Blizzard.T5.Core.ILogger Logger { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x0600C179 RID: 49529 RVA: 0x003AC4D8 File Offset: 0x003AA6D8
		// (set) Token: 0x0600C17A RID: 49530 RVA: 0x003AC4E0 File Offset: 0x003AA6E0
		private IMobileAuth MobileAuth { get; set; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x0600C17B RID: 49531 RVA: 0x003AC4E9 File Offset: 0x003AA6E9
		// (set) Token: 0x0600C17C RID: 49532 RVA: 0x003AC4F1 File Offset: 0x003AA6F1
		private ILoginStrategyCollection LoginStrategies { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x0600C17D RID: 49533 RVA: 0x003AC4FA File Offset: 0x003AA6FA
		// (set) Token: 0x0600C17E RID: 49534 RVA: 0x003AC502 File Offset: 0x003AA702
		private bool SecureModeEnabled { get; set; } = true;

		// Token: 0x17000D6F RID: 3439
		// (get) Token: 0x0600C17F RID: 49535 RVA: 0x003AC50B File Offset: 0x003AA70B
		// (set) Token: 0x0600C180 RID: 49536 RVA: 0x003AC513 File Offset: 0x003AA713
		private bool DebugLoggingEnabled { get; set; }

		// Token: 0x0600C181 RID: 49537 RVA: 0x003AC51C File Offset: 0x003AA71C
		public MASDKTokenFetcher(Blizzard.T5.Core.ILogger logger, IMobileAuth mobileAuth, ILoginStrategyCollection loginStrategies)
		{
			this.Logger = logger;
			this.MobileAuth = mobileAuth;
			this.LoginStrategies = loginStrategies;
			this.ConfigureMobileAuth();
		}

		// Token: 0x0600C182 RID: 49538 RVA: 0x003AC548 File Offset: 0x003AA748
		public TokenPromise FetchToken(string challengeUrl)
		{
			Blizzard.T5.Core.ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Fetching token from MASDK", Array.Empty<object>());
			}
			this.m_promise = new TokenPromise();
			Blizzard.T5.Core.ILogger logger2 = this.Logger;
			if (logger2 != null)
			{
				logger2.Log(LogLevel.Debug, "Attempting login strategies", Array.Empty<object>());
			}
			LoginStrategyParameters parameters = new LoginStrategyParameters
			{
				MobileAuth = this.MobileAuth,
				ChallengeUrl = challengeUrl
			};
			if (!this.LoginStrategies.AttemptExecuteLoginStrategy(parameters, this.m_promise))
			{
				Blizzard.T5.Core.ILogger logger3 = this.Logger;
				if (logger3 != null)
				{
					logger3.Log(LogLevel.Error, "No applicable login strategy found. Cannot fetch token!", Array.Empty<object>());
				}
				this.m_promise.SetResult(TokenPromise.ResultType.Failure, null);
			}
			else
			{
				Blizzard.T5.Core.ILogger logger4 = this.Logger;
				if (logger4 != null)
				{
					logger4.Log(LogLevel.Debug, "Successfully executed login strategy", Array.Empty<object>());
				}
			}
			return this.m_promise;
		}

		// Token: 0x0600C183 RID: 49539 RVA: 0x003AC618 File Offset: 0x003AA818
		public void ClearCachedAuthentication()
		{
			IMobileAuth mobileAuth = this.MobileAuth;
			if (mobileAuth == null)
			{
				return;
			}
			mobileAuth.Logout();
		}

		// Token: 0x0600C184 RID: 49540 RVA: 0x003AC62C File Offset: 0x003AA82C
		private void ConfigureMobileAuth()
		{
			this.LoadConfigValues();
			Configuration configuration = new Configuration
			{
				appID = "wtcg",
				regions = this.ConstructRegionArray(),
				authenticationURLScheme = "blizzard-hearthstone",
				isChinaBuild = (PlatformSettings.LocaleVariant == LocaleVariant.China),
				keychainAppName = Application.identifier,
				sharedKeychainGroup = MobileCallbackManager.GetSharedKeychainIdentifier(),
				loggingEnabled = this.DebugLoggingEnabled,
				androidIsSecure = this.SecureModeEnabled,
				primaryColor = new Color(0.0784f, 0.5568f, 1f),
				secondaryColor = new Color(0.0823f, 0.0901f, 0.1176f),
				textColor = Color.white
			};
			MASDKRegionHelper.SetConfiguredRegions(configuration.regions);
			this.MobileAuth.Configure(configuration);
		}

		// Token: 0x0600C185 RID: 49541 RVA: 0x003AC708 File Offset: 0x003AA908
		private void LoadConfigValues()
		{
			ConfigFile configFile = new ConfigFile();
			if (!configFile.FullLoad(Vars.GetClientConfigPath()))
			{
				Log.Login.PrintWarning("Failed to read config from {0}", new object[]
				{
					"client.config"
				});
				this.DebugLoggingEnabled = HearthstoneApplication.IsInternal();
				this.SecureModeEnabled = HearthstoneApplication.IsPublic();
				return;
			}
			this.DebugLoggingEnabled = configFile.Get("MASDK.LoggingEnabled", HearthstoneApplication.IsInternal());
			this.SecureModeEnabled = configFile.Get("MASDK.SecureModeEnabled", HearthstoneApplication.IsPublic());
			Blizzard.T5.Core.ILogger logger = this.Logger;
			if (logger == null)
			{
				return;
			}
			logger.Log(LogLevel.Debug, "Loaded MASDK config values. Logging {0}, SecureMode {1}", new object[]
			{
				this.DebugLoggingEnabled,
				this.SecureModeEnabled
			});
		}

		// Token: 0x0600C186 RID: 49542 RVA: 0x003AC7C0 File Offset: 0x003AA9C0
		private Region[] ConstructRegionArray()
		{
			List<Region> list = new List<Region>(this.MobileAuth.BuiltInRegions());
			if (HearthstoneApplication.IsInternal())
			{
				MASDKTokenFetcher.AddDevRegionsToList(list);
			}
			return list.ToArray();
		}

		// Token: 0x0600C187 RID: 49543 RVA: 0x003AC7F4 File Offset: 0x003AA9F4
		private static void AddDevRegionsToList(List<Region> regions)
		{
			regions.Add(new Region("US(DEV)", "ST1 (US DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-us.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-us.web.blizzard.net/account/creation/")));
			regions.Add(new Region("EU(DEV)", "ST2 (EU DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-eu.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-eu.web.blizzard.net/account/creation/")));
			regions.Add(new Region("KR(DEV)", "ST3 (KR DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-kr.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-kr.web.blizzard.net/account/creation/")));
			regions.Add(new Region("CN(DEV)", "ST5 (CN DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-cn.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-cn.web.blizzard.net/account/creation/")));
		}

		// Token: 0x04009C2C RID: 39980
		private const string APP_ID = "wtcg";

		// Token: 0x04009C2D RID: 39981
		private const string AUTH_URL_SCHEME = "blizzard-hearthstone";

		// Token: 0x04009C2E RID: 39982
		private TokenPromise m_promise;
	}
}
