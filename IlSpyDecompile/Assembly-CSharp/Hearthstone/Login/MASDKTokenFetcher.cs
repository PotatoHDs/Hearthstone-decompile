using System.Collections.Generic;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using UnityEngine;

namespace Hearthstone.Login
{
	public class MASDKTokenFetcher : IPlatformLoginTokenFetcher
	{
		private const string APP_ID = "wtcg";

		private const string AUTH_URL_SCHEME = "blizzard-hearthstone";

		private TokenPromise m_promise;

		private Blizzard.T5.Core.ILogger Logger { get; set; }

		private IMobileAuth MobileAuth { get; set; }

		private ILoginStrategyCollection LoginStrategies { get; set; }

		private bool SecureModeEnabled { get; set; } = true;


		private bool DebugLoggingEnabled { get; set; }

		public MASDKTokenFetcher(Blizzard.T5.Core.ILogger logger, IMobileAuth mobileAuth, ILoginStrategyCollection loginStrategies)
		{
			Logger = logger;
			MobileAuth = mobileAuth;
			LoginStrategies = loginStrategies;
			ConfigureMobileAuth();
		}

		public TokenPromise FetchToken(string challengeUrl)
		{
			Logger?.Log(LogLevel.Debug, "Fetching token from MASDK");
			m_promise = new TokenPromise();
			Logger?.Log(LogLevel.Debug, "Attempting login strategies");
			LoginStrategyParameters loginStrategyParameters = default(LoginStrategyParameters);
			loginStrategyParameters.MobileAuth = MobileAuth;
			loginStrategyParameters.ChallengeUrl = challengeUrl;
			LoginStrategyParameters parameters = loginStrategyParameters;
			if (!LoginStrategies.AttemptExecuteLoginStrategy(parameters, m_promise))
			{
				Logger?.Log(LogLevel.Error, "No applicable login strategy found. Cannot fetch token!");
				m_promise.SetResult(TokenPromise.ResultType.Failure);
			}
			else
			{
				Logger?.Log(LogLevel.Debug, "Successfully executed login strategy");
			}
			return m_promise;
		}

		public void ClearCachedAuthentication()
		{
			MobileAuth?.Logout();
		}

		private void ConfigureMobileAuth()
		{
			LoadConfigValues();
			Configuration configuration = default(Configuration);
			configuration.appID = "wtcg";
			configuration.regions = ConstructRegionArray();
			configuration.authenticationURLScheme = "blizzard-hearthstone";
			configuration.isChinaBuild = PlatformSettings.LocaleVariant == LocaleVariant.China;
			configuration.keychainAppName = Application.identifier;
			configuration.sharedKeychainGroup = MobileCallbackManager.GetSharedKeychainIdentifier();
			configuration.loggingEnabled = DebugLoggingEnabled;
			configuration.androidIsSecure = SecureModeEnabled;
			configuration.primaryColor = new Color(0.0784f, 0.5568f, 1f);
			configuration.secondaryColor = new Color(0.0823f, 0.0901f, 0.1176f);
			configuration.textColor = Color.white;
			Configuration configuration2 = configuration;
			MASDKRegionHelper.SetConfiguredRegions(configuration2.regions);
			MobileAuth.Configure(configuration2);
		}

		private void LoadConfigValues()
		{
			ConfigFile configFile = new ConfigFile();
			if (!configFile.FullLoad(Vars.GetClientConfigPath()))
			{
				Log.Login.PrintWarning("Failed to read config from {0}", "client.config");
				DebugLoggingEnabled = HearthstoneApplication.IsInternal();
				SecureModeEnabled = HearthstoneApplication.IsPublic();
			}
			else
			{
				DebugLoggingEnabled = configFile.Get("MASDK.LoggingEnabled", HearthstoneApplication.IsInternal());
				SecureModeEnabled = configFile.Get("MASDK.SecureModeEnabled", HearthstoneApplication.IsPublic());
				Logger?.Log(LogLevel.Debug, "Loaded MASDK config values. Logging {0}, SecureMode {1}", DebugLoggingEnabled, SecureModeEnabled);
			}
		}

		private Region[] ConstructRegionArray()
		{
			List<Region> list = new List<Region>(MobileAuth.BuiltInRegions());
			if (HearthstoneApplication.IsInternal())
			{
				AddDevRegionsToList(list);
			}
			return list.ToArray();
		}

		private static void AddDevRegionsToList(List<Region> regions)
		{
			regions.Add(new Region("US(DEV)", "ST1 (US DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-us.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-us.web.blizzard.net/account/creation/")));
			regions.Add(new Region("EU(DEV)", "ST2 (EU DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-eu.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-eu.web.blizzard.net/account/creation/")));
			regions.Add(new Region("KR(DEV)", "ST3 (KR DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-kr.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-kr.web.blizzard.net/account/creation/")));
			regions.Add(new Region("CN(DEV)", "ST5 (CN DEV)", LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://login-live-cn.web.blizzard.net/login/"), LoginEnvironmentUtil.OverrideEnvironmentIfNeeded("https://account-creation-live-cn.web.blizzard.net/account/creation/")));
		}
	}
}
