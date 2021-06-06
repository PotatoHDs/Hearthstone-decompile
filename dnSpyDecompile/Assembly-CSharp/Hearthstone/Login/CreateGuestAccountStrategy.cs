using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x0200112A RID: 4394
	public class CreateGuestAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate, IGuestSoftAccountIdListener
	{
		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x0600C0AE RID: 49326 RVA: 0x003AAA48 File Offset: 0x003A8C48
		// (set) Token: 0x0600C0AF RID: 49327 RVA: 0x003AAA50 File Offset: 0x003A8C50
		private TokenPromise Promise { get; set; }

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x0600C0B0 RID: 49328 RVA: 0x003AAA59 File Offset: 0x003A8C59
		// (set) Token: 0x0600C0B1 RID: 49329 RVA: 0x003AAA61 File Offset: 0x003A8C61
		private IMobileAuth MobileAuthInterface { get; set; }

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x0600C0B2 RID: 49330 RVA: 0x003AAA6A File Offset: 0x003A8C6A
		// (set) Token: 0x0600C0B3 RID: 49331 RVA: 0x003AAA72 File Offset: 0x003A8C72
		private ILogger Logger { get; set; }

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x0600C0B4 RID: 49332 RVA: 0x003AAA7B File Offset: 0x003A8C7B
		// (set) Token: 0x0600C0B5 RID: 49333 RVA: 0x003AAA83 File Offset: 0x003A8C83
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C0B6 RID: 49334 RVA: 0x003AAA8C File Offset: 0x003A8C8C
		public CreateGuestAccountStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C0B7 RID: 49335 RVA: 0x003AAAA2 File Offset: 0x003A8CA2
		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			return PlatformSettings.LocaleVariant != LocaleVariant.China && parameters.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture) && this.AreTutorialsComplete();
		}

		// Token: 0x0600C0B8 RID: 49336 RVA: 0x003AAACC File Offset: 0x003A8CCC
		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			this.Promise = promise;
			this.MobileAuthInterface = parameters.MobileAuth;
			Region currentlyConnectedRegion = MASDKRegionHelper.GetCurrentlyConnectedRegion();
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Attempting to create guest account for region id {0}", new object[]
				{
					currentlyConnectedRegion.regionId
				});
			}
			parameters.MobileAuth.GenerateGuestSoftAccountId(currentlyConnectedRegion, this);
		}

		// Token: 0x0600C0B9 RID: 49337 RVA: 0x003AAB28 File Offset: 0x003A8D28
		public void Authenticated(Account authenticatedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Succesfully authenticated new guest account {0}", new object[]
				{
					authenticatedAccount.displayName
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
			CreateSkipHelper.RequestShowCreateSkip();
			this.Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		// Token: 0x0600C0BA RID: 49338 RVA: 0x003AAB7A File Offset: 0x003A8D7A
		public void AuthenticationCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "New soft account authentication canceled", Array.Empty<object>());
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Canceled, null);
		}

		// Token: 0x0600C0BB RID: 49339 RVA: 0x003AABB0 File Offset: 0x003A8DB0
		public void AuthenticationError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Error, "Error authenticating new soft account: {0} {1} {2}", new object[]
				{
					error.errorCode,
					error.errorContext,
					error.errorMessage
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C0BC RID: 49340 RVA: 0x003AAC14 File Offset: 0x003A8E14
		public void OnGuestSoftAccountIdError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Error, "Error creating soft account: {0} {1} {2}", new object[]
				{
					error.errorCode,
					error.errorContext,
					error.errorMessage
				});
			}
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient != null)
			{
				telemetryClient.SendMASDKGuestCreationFailure(error.errorCode);
			}
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C0BD RID: 49341 RVA: 0x003AAC84 File Offset: 0x003A8E84
		public void OnGuestSoftAccountIdRetrieved(GuestSoftAccountId guestSoftAccountId)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Succesufully created soft account {0}", new object[]
				{
					guestSoftAccountId.bnetGuestId
				});
			}
			AdTrackingManager.Get().TrackHeadlessAccountCreated();
			IMobileAuth mobileAuthInterface = this.MobileAuthInterface;
			if (mobileAuthInterface == null)
			{
				return;
			}
			mobileAuthInterface.AuthenticateSoftAccount(guestSoftAccountId, this);
		}

		// Token: 0x0600C0BE RID: 49342 RVA: 0x003AACD3 File Offset: 0x003A8ED3
		private bool AreTutorialsComplete()
		{
			return GameUtils.AreAllTutorialsComplete(Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS));
		}

		// Token: 0x0600C0BF RID: 49343 RVA: 0x003AACE6 File Offset: 0x003A8EE6
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "CreateGuestAccountStrategy");
		}
	}
}
