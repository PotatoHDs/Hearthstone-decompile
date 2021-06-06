using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class SelectAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		private ILogger Logger { get; set; }

		private TokenPromise Promise { get; set; }

		private ISwitchAccountMenuController SwitchAccountMenu { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		public SelectAccountStrategy(ILogger logger, ISwitchAccountMenuController switchAccount, ITelemetryClient telemetryClient)
		{
			Logger = logger;
			SwitchAccountMenu = switchAccount;
			TelemetryClient = telemetryClient;
		}

		public void Authenticated(Account authenticatedAccount)
		{
			Logger?.Log(LogLevel.Information, "Succesfully authenticated account {0}", authenticatedAccount.displayName);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
			Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		public void AuthenticationCancelled()
		{
			Logger?.Log(LogLevel.Information, "Account authentication canceled!");
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED);
			Promise.SetResult(TokenPromise.ResultType.Canceled);
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			Logger?.Log(LogLevel.Error, "Account authentication error [{0}] {1}\nMessage:{2}", error.errorCode, error.errorContext, error.errorMessage);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			if (!parameters.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture))
			{
				return false;
			}
			List<Account> softAccounts = parameters.MobileAuth.GetSoftAccounts();
			if (softAccounts != null)
			{
				return softAccounts.Count > 0;
			}
			return false;
		}

		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			Promise = promise;
			List<Account> softAccounts = parameters.MobileAuth.GetSoftAccounts();
			SwitchAccountMenu.ShowSwitchAccount(softAccounts, delegate(Account? selectedAccount)
			{
				if (!selectedAccount.HasValue || !selectedAccount.HasValue)
				{
					Logger?.Log(LogLevel.Information, "No account selected from menu, presenting login");
					parameters.MobileAuth.PresentChallenge(parameters.ChallengeUrl, this);
				}
				else
				{
					Account value = selectedAccount.Value;
					Region currentlyConnectedRegion = MASDKRegionHelper.GetCurrentlyConnectedRegion();
					Logger?.Log(LogLevel.Information, "Selected account {0}, requesting authentication for region {1}", value.battleTag, currentlyConnectedRegion.name);
					parameters.MobileAuth.AuthenticateAccount(value, currentlyConnectedRegion.regionId, this);
				}
			});
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "SelectAccountStrategy");
		}
	}
}
