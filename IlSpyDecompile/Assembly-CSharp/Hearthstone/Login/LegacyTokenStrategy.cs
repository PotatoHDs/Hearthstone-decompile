using bgs;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class LegacyTokenStrategy : IAsyncMobileLoginStrategy, IImportAccountCallback, IAuthenticationDelegate
	{
		private ILegacyTokenStorage TokenStorage { get; set; }

		private TokenPromise Promise { get; set; }

		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		private IMobileAuth MobileAuth { get; set; }

		public LegacyTokenStrategy(ILegacyTokenStorage tokenStorage, ILogger logger, ITelemetryClient telemetryClient)
		{
			TokenStorage = tokenStorage;
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			return !string.IsNullOrEmpty(TokenStorage?.GetStoredToken());
		}

		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Executing Legacy Token Strategy");
			if (promise == null)
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, no promise was supplied");
				return;
			}
			if (parameters.MobileAuth == null)
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, mobile auth was null");
				promise.SetResult(TokenPromise.ResultType.Failure);
				return;
			}
			string storedToken = TokenStorage.GetStoredToken();
			if (string.IsNullOrEmpty(storedToken))
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, did not meet requirements");
				promise.SetResult(TokenPromise.ResultType.Failure);
				return;
			}
			MobileAuth = parameters.MobileAuth;
			Promise = promise;
			string regionIdForConnectedFront = GetRegionIdForConnectedFront();
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting to import legacy account for region {0}", regionIdForConnectedFront);
			MobileAuth.ImportAccount(storedToken, regionIdForConnectedFront, this);
		}

		private string GetRegionIdForConnectedFront()
		{
			if (HearthstoneApplication.IsInternal())
			{
				return GetRegionIdForInternal();
			}
			return GetRegionIdForCurrentRegion();
		}

		private string GetRegionIdForCurrentRegion()
		{
			constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
			string text = MASDKRegionHelper.GetRegionIdForBGSRegion(currentRegion);
			if (string.IsNullOrEmpty(text))
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Could not find region id for bgs region {0}. Defaulting to {1}", currentRegion.ToString(), "US");
				text = "US";
			}
			return text;
		}

		private string GetRegionIdForInternal()
		{
			string environment = BattleNet.GetEnvironment();
			string regionIdForInternalEnvironment = MASDKRegionHelper.GetRegionIdForInternalEnvironment(environment);
			if (string.IsNullOrEmpty(regionIdForInternalEnvironment))
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Could not find dev region id for environment {0}. Defaulting to {1}", environment, "US(DEV)");
				return "US(DEV)";
			}
			return regionIdForInternalEnvironment;
		}

		public void OnImportAccountSuccess(Account account)
		{
			if (string.IsNullOrEmpty(account.authenticationToken))
			{
				Promise.SetResult(TokenPromise.ResultType.Failure);
				return;
			}
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Succesfully imported account {0}", account.displayName);
			SendImportResultTelemetry(MASDKImportResult.ImportResult.SUCCESS);
			MobileAuth.AuthenticateAccount(account, GetRegionIdForConnectedFront(), this);
		}

		public void OnImportAccountError(BlzMobileAuthError error)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Failed to import account [{0}] {1} : {2}", error.errorCode, error.errorMessage, error.errorContext);
			TokenStorage.ClearStoredToken();
			SendImportResultTelemetry(MASDKImportResult.ImportResult.FAILURE, error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		private void SendImportResultTelemetry(MASDKImportResult.ImportResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKImportResult(result, MASDKImportResult.ImportType.AUTH_TOKEN, errorCode);
		}

		public void Authenticated(Account authenticatedAccount)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Information, "Successfully authenticated imported account {0}", authenticatedAccount.displayName);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
			TokenStorage.ClearStoredToken();
			Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		public void AuthenticationCancelled()
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Warning, "Authentication for imported account canceled unexpectedly.");
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED);
			Promise.SetResult(TokenPromise.ResultType.Canceled);
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			Logger?.Log(Blizzard.T5.Core.LogLevel.Warning, "Authentication for imported account failed due to error [{0}] {1} : {2}", error.errorCode, error.errorMessage, error.errorContext);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "LegacyTokenStrategy");
		}
	}
}
