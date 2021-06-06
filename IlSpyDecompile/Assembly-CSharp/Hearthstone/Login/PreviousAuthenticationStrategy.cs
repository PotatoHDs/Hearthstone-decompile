using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class PreviousAuthenticationStrategy : IAsyncMobileLoginStrategy
	{
		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		public PreviousAuthenticationStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public bool MeetsRequirements(LoginStrategyParameters arguments)
		{
			if (arguments.MobileAuth.IsAuthenticated && arguments.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture))
			{
				Account? authenticatedAccount = arguments.MobileAuth.GetAuthenticatedAccount();
				if (authenticatedAccount.HasValue)
				{
					return authenticatedAccount.HasValue;
				}
				return false;
			}
			return false;
		}

		public void StartExecution(LoginStrategyParameters arguments, TokenPromise promise)
		{
			Logger?.Log(LogLevel.Debug, "Starting Execution of Previous Auth strategy");
			if (!arguments.MobileAuth.IsAuthenticated)
			{
				Logger?.Log(LogLevel.Error, "Attempted to use previous authentication when not authenticated");
				SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE);
				promise.SetResult(TokenPromise.ResultType.Failure);
				return;
			}
			Account? authenticatedAccount = arguments.MobileAuth.GetAuthenticatedAccount();
			if (!authenticatedAccount.HasValue || !authenticatedAccount.HasValue)
			{
				Logger?.Log(LogLevel.Error, "Unexpected null account, was authenticated but no account information available");
				SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE);
				promise.SetResult(TokenPromise.ResultType.Failure);
			}
			else
			{
				Logger?.Log(LogLevel.Information, "Previous authentication found, using its token");
				SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
				promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.Value.authenticationToken);
			}
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "PreviousAuthentication");
		}
	}
}
