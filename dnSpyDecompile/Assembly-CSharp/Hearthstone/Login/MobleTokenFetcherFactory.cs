using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001144 RID: 4420
	internal static class MobleTokenFetcherFactory
	{
		// Token: 0x0600C1B1 RID: 49585 RVA: 0x003ACD8E File Offset: 0x003AAF8E
		public static IPlatformLoginTokenFetcher ConstructMobileTokenFetcher(ILogger logger, IMobileAuth mobileAuth, bool enableMASDK)
		{
			if (!enableMASDK)
			{
				return MobleTokenFetcherFactory.ConstructLegacyMobileTokenFetcher(logger);
			}
			return MobleTokenFetcherFactory.ConstructMASDKTokenFetcher(logger, mobileAuth);
		}

		// Token: 0x0600C1B2 RID: 49586 RVA: 0x003ACDA1 File Offset: 0x003AAFA1
		private static IPlatformLoginTokenFetcher ConstructLegacyMobileTokenFetcher(ILogger logger)
		{
			return new MobileLoginTokenFetcher(logger, new WebAuthDisplay());
		}

		// Token: 0x0600C1B3 RID: 49587 RVA: 0x003ACDB0 File Offset: 0x003AAFB0
		private static IPlatformLoginTokenFetcher ConstructMASDKTokenFetcher(ILogger logger, IMobileAuth mobileAuth)
		{
			ILoginStrategyCollection loginStrategies = MobleTokenFetcherFactory.ConstructHearthstoneLoginStrategies(logger);
			return new MASDKTokenFetcher(logger, mobileAuth, loginStrategies);
		}

		// Token: 0x0600C1B4 RID: 49588 RVA: 0x003ACDCC File Offset: 0x003AAFCC
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
