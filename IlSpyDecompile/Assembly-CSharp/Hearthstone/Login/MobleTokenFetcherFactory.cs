using System.Collections.Generic;
using Blizzard.T5.Core;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	internal static class MobleTokenFetcherFactory
	{
		public static IPlatformLoginTokenFetcher ConstructMobileTokenFetcher(ILogger logger, IMobileAuth mobileAuth, bool enableMASDK)
		{
			if (!enableMASDK)
			{
				return ConstructLegacyMobileTokenFetcher(logger);
			}
			return ConstructMASDKTokenFetcher(logger, mobileAuth);
		}

		private static IPlatformLoginTokenFetcher ConstructLegacyMobileTokenFetcher(ILogger logger)
		{
			return new MobileLoginTokenFetcher(logger, new WebAuthDisplay());
		}

		private static IPlatformLoginTokenFetcher ConstructMASDKTokenFetcher(ILogger logger, IMobileAuth mobileAuth)
		{
			ILoginStrategyCollection loginStrategies = ConstructHearthstoneLoginStrategies(logger);
			return new MASDKTokenFetcher(logger, mobileAuth, loginStrategies);
		}

		private static ILoginStrategyCollection ConstructHearthstoneLoginStrategies(ILogger logger)
		{
			SwitchAccountMenuController switchAccountMenuController = new SwitchAccountMenuController();
			ITelemetryClient telemetryClient = TelemetryManager.Client();
			return new LoginStrategyCollection(new List<IAsyncMobileLoginStrategy>
			{
				new PreviousAuthenticationStrategy(logger, telemetryClient),
				new SelectAccountStrategy(logger, switchAccountMenuController, telemetryClient),
				new LegacyCloudGuestAccountStrategy(new TemporaryAccountAccountWrapper(), switchAccountMenuController, logger, telemetryClient),
				new LegacyTokenStrategy(new WebAuthTokenWrapper(), logger, telemetryClient),
				new CreateGuestAccountStrategy(logger, telemetryClient),
				new PresentChallengeStrategy(logger, telemetryClient),
				new PresentLoginStrategy(logger, telemetryClient)
			});
		}
	}
}
