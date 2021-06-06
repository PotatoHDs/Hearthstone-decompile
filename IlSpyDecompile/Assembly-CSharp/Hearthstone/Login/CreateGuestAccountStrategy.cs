using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	public class CreateGuestAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate, IGuestSoftAccountIdListener
	{
		private TokenPromise Promise { get; set; }

		private IMobileAuth MobileAuthInterface { get; set; }

		private ILogger Logger { get; set; }

		private ITelemetryClient TelemetryClient { get; set; }

		public CreateGuestAccountStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			Logger = logger;
			TelemetryClient = telemetryClient;
		}

		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			if (PlatformSettings.LocaleVariant == LocaleVariant.China)
			{
				return false;
			}
			if (parameters.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture))
			{
				return AreTutorialsComplete();
			}
			return false;
		}

		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			Promise = promise;
			MobileAuthInterface = parameters.MobileAuth;
			Region currentlyConnectedRegion = MASDKRegionHelper.GetCurrentlyConnectedRegion();
			Logger?.Log(LogLevel.Information, "Attempting to create guest account for region id {0}", currentlyConnectedRegion.regionId);
			parameters.MobileAuth.GenerateGuestSoftAccountId(currentlyConnectedRegion, this);
		}

		public void Authenticated(Account authenticatedAccount)
		{
			Logger?.Log(LogLevel.Information, "Succesfully authenticated new guest account {0}", authenticatedAccount.displayName);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS);
			CreateSkipHelper.RequestShowCreateSkip();
			Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		public void AuthenticationCancelled()
		{
			Logger?.Log(LogLevel.Information, "New soft account authentication canceled");
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED);
			Promise.SetResult(TokenPromise.ResultType.Canceled);
		}

		public void AuthenticationError(BlzMobileAuthError error)
		{
			Logger?.Log(LogLevel.Error, "Error authenticating new soft account: {0} {1} {2}", error.errorCode, error.errorContext, error.errorMessage);
			SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		public void OnGuestSoftAccountIdError(BlzMobileAuthError error)
		{
			Logger?.Log(LogLevel.Error, "Error creating soft account: {0} {1} {2}", error.errorCode, error.errorContext, error.errorMessage);
			TelemetryClient?.SendMASDKGuestCreationFailure(error.errorCode);
			Promise.SetResult(TokenPromise.ResultType.Failure);
		}

		public void OnGuestSoftAccountIdRetrieved(GuestSoftAccountId guestSoftAccountId)
		{
			Logger?.Log(LogLevel.Debug, "Succesufully created soft account {0}", guestSoftAccountId.bnetGuestId);
			AdTrackingManager.Get().TrackHeadlessAccountCreated();
			MobileAuthInterface?.AuthenticateSoftAccount(guestSoftAccountId, this);
		}

		private bool AreTutorialsComplete()
		{
			return GameUtils.AreAllTutorialsComplete(Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS));
		}

		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			TelemetryClient?.SendMASDKAuthResult(result, errorCode, "CreateGuestAccountStrategy");
		}
	}
}
