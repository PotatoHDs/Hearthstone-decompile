using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class PresentChallengeStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		private TokenPromise m_promise;

		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		public PresentChallengeStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public bool MeetsRequirements(LoginStrategyParameters arguments)
		{
			return !string.IsNullOrEmpty(arguments.ChallengeUrl);
		}

		public void StartExecution(LoginStrategyParameters arguments, TokenPromise promise)
		{
			Logger?.Log(LogLevel.Debug, "Executing Present Challenge login strategy");
			if (string.IsNullOrEmpty(arguments.ChallengeUrl))
			{
				Logger?.Log(LogLevel.Error, "Unexpected execution, attempted to present challenge with missing challenge url");
				promise.SetResult(TokenPromise.ResultType.Failure);
			}
			else
			{
				m_promise = promise;
				arguments.MobileAuth.PresentChallenge(arguments.ChallengeUrl, this);
			}
		}

		public void Authenticated(Account authenticatedAccount)
		{
			if (m_promise == null)
			{
				Logger?.Log(LogLevel.Error, "Unexpected authentication callback in Present Challenge. No promise was set");
				return;
			}
			if (string.IsNullOrEmpty(authenticatedAccount.authenticationToken))
			{
				Logger?.Log(LogLevel.Error, "Unexpected missing token from present challenge success");
				m_promise.SetResult(TokenPromise.ResultType.Failure);
				return;
			}
			Logger?.Log(LogLevel.Information, "Present challenge succesfully authenticated account {0} {1}", authenticatedAccount.battleTag, authenticatedAccount.displayName);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
			m_promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
			m_promise = null;
		}

		public void AuthenticationCancelled()
		{
			if (m_promise == null)
			{
				Logger?.Log(LogLevel.Error, "Unexpected authentication canceled callback in Present Challenge. No promise was set");
				return;
			}
			Logger?.Log(LogLevel.Information, "Present challenge was canceled");
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED);
			m_promise.SetResult(TokenPromise.ResultType.Canceled);
			m_promise = null;
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			if (m_promise == null)
			{
				Logger?.Log(LogLevel.Error, "Unexpected authentication error callback in Present Challenge. No promise was set");
				return;
			}
			Logger?.Log(LogLevel.Error, "Present Challenge authentication error. {0} {1} {2}", error.errorCode, error.errorContext, error.errorMessage);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			m_promise.SetResult(TokenPromise.ResultType.Failure);
			m_promise = null;
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "PresentChallengeStrategy");
		}
	}
}
