using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001148 RID: 4424
	public class PreviousAuthenticationStrategy : IAsyncMobileLoginStrategy
	{
		// Token: 0x17000D7A RID: 3450
		// (get) Token: 0x0600C1D8 RID: 49624 RVA: 0x003AD630 File Offset: 0x003AB830
		// (set) Token: 0x0600C1D9 RID: 49625 RVA: 0x003AD638 File Offset: 0x003AB838
		private ILogger Logger { get; set; }

		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600C1DA RID: 49626 RVA: 0x003AD641 File Offset: 0x003AB841
		// (set) Token: 0x0600C1DB RID: 49627 RVA: 0x003AD649 File Offset: 0x003AB849
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C1DC RID: 49628 RVA: 0x003AD652 File Offset: 0x003AB852
		public PreviousAuthenticationStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C1DD RID: 49629 RVA: 0x003AD668 File Offset: 0x003AB868
		public bool MeetsRequirements(LoginStrategyParameters arguments)
		{
			if (arguments.MobileAuth.IsAuthenticated && arguments.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture))
			{
				Account? authenticatedAccount = arguments.MobileAuth.GetAuthenticatedAccount();
				return authenticatedAccount != null && authenticatedAccount != null;
			}
			return false;
		}

		// Token: 0x0600C1DE RID: 49630 RVA: 0x003AD6B8 File Offset: 0x003AB8B8
		public void StartExecution(LoginStrategyParameters arguments, TokenPromise promise)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Starting Execution of Previous Auth strategy", Array.Empty<object>());
			}
			if (!arguments.MobileAuth.IsAuthenticated)
			{
				ILogger logger2 = this.Logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Error, "Attempted to use previous authentication when not authenticated", Array.Empty<object>());
				}
				this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, 0);
				promise.SetResult(TokenPromise.ResultType.Failure, null);
				return;
			}
			Account? authenticatedAccount = arguments.MobileAuth.GetAuthenticatedAccount();
			if (authenticatedAccount == null || authenticatedAccount == null)
			{
				ILogger logger3 = this.Logger;
				if (logger3 != null)
				{
					logger3.Log(LogLevel.Error, "Unexpected null account, was authenticated but no account information available", Array.Empty<object>());
				}
				this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, 0);
				promise.SetResult(TokenPromise.ResultType.Failure, null);
				return;
			}
			ILogger logger4 = this.Logger;
			if (logger4 != null)
			{
				logger4.Log(LogLevel.Information, "Previous authentication found, using its token", Array.Empty<object>());
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
			promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.Value.authenticationToken);
		}

		// Token: 0x0600C1DF RID: 49631 RVA: 0x003AD79F File Offset: 0x003AB99F
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "PreviousAuthentication");
		}
	}
}
