using System;
using bgs;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001139 RID: 4409
	public class LegacyTokenStrategy : IAsyncMobileLoginStrategy, IImportAccountCallback, IAuthenticationDelegate
	{
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600C116 RID: 49430 RVA: 0x003AB289 File Offset: 0x003A9489
		// (set) Token: 0x0600C117 RID: 49431 RVA: 0x003AB291 File Offset: 0x003A9491
		private ILegacyTokenStorage TokenStorage { get; set; }

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x0600C118 RID: 49432 RVA: 0x003AB29A File Offset: 0x003A949A
		// (set) Token: 0x0600C119 RID: 49433 RVA: 0x003AB2A2 File Offset: 0x003A94A2
		private TokenPromise Promise { get; set; }

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600C11A RID: 49434 RVA: 0x003AB2AB File Offset: 0x003A94AB
		// (set) Token: 0x0600C11B RID: 49435 RVA: 0x003AB2B3 File Offset: 0x003A94B3
		private ILogger Logger { get; set; }

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600C11C RID: 49436 RVA: 0x003AB2BC File Offset: 0x003A94BC
		// (set) Token: 0x0600C11D RID: 49437 RVA: 0x003AB2C4 File Offset: 0x003A94C4
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600C11E RID: 49438 RVA: 0x003AB2CD File Offset: 0x003A94CD
		// (set) Token: 0x0600C11F RID: 49439 RVA: 0x003AB2D5 File Offset: 0x003A94D5
		private IMobileAuth MobileAuth { get; set; }

		// Token: 0x0600C120 RID: 49440 RVA: 0x003AB2DE File Offset: 0x003A94DE
		public LegacyTokenStrategy(ILegacyTokenStorage tokenStorage, ILogger logger, ITelemetryClient telemetryClient)
		{
			this.TokenStorage = tokenStorage;
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C121 RID: 49441 RVA: 0x003AB2FB File Offset: 0x003A94FB
		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			ILegacyTokenStorage tokenStorage = this.TokenStorage;
			return !string.IsNullOrEmpty((tokenStorage != null) ? tokenStorage.GetStoredToken() : null);
		}

		// Token: 0x0600C122 RID: 49442 RVA: 0x003AB318 File Offset: 0x003A9518
		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Executing Legacy Token Strategy", Array.Empty<object>());
			}
			if (promise == null)
			{
				ILogger logger2 = this.Logger;
				if (logger2 == null)
				{
					return;
				}
				logger2.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, no promise was supplied", Array.Empty<object>());
				return;
			}
			else
			{
				if (parameters.MobileAuth == null)
				{
					ILogger logger3 = this.Logger;
					if (logger3 != null)
					{
						logger3.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, mobile auth was null", Array.Empty<object>());
					}
					promise.SetResult(TokenPromise.ResultType.Failure, null);
					return;
				}
				string storedToken = this.TokenStorage.GetStoredToken();
				if (string.IsNullOrEmpty(storedToken))
				{
					ILogger logger4 = this.Logger;
					if (logger4 != null)
					{
						logger4.Log(Blizzard.T5.Core.LogLevel.Error, "Could not execute legacy token strategy, did not meet requirements", Array.Empty<object>());
					}
					promise.SetResult(TokenPromise.ResultType.Failure, null);
					return;
				}
				this.MobileAuth = parameters.MobileAuth;
				this.Promise = promise;
				string regionIdForConnectedFront = this.GetRegionIdForConnectedFront();
				ILogger logger5 = this.Logger;
				if (logger5 != null)
				{
					logger5.Log(Blizzard.T5.Core.LogLevel.Information, "Attempting to import legacy account for region {0}", new object[]
					{
						regionIdForConnectedFront
					});
				}
				this.MobileAuth.ImportAccount(storedToken, regionIdForConnectedFront, this);
				return;
			}
		}

		// Token: 0x0600C123 RID: 49443 RVA: 0x003AB411 File Offset: 0x003A9611
		private string GetRegionIdForConnectedFront()
		{
			if (HearthstoneApplication.IsInternal())
			{
				return this.GetRegionIdForInternal();
			}
			return this.GetRegionIdForCurrentRegion();
		}

		// Token: 0x0600C124 RID: 49444 RVA: 0x003AB428 File Offset: 0x003A9628
		private string GetRegionIdForCurrentRegion()
		{
			constants.BnetRegion currentRegion = BattleNet.GetCurrentRegion();
			string text = MASDKRegionHelper.GetRegionIdForBGSRegion(currentRegion);
			if (string.IsNullOrEmpty(text))
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(Blizzard.T5.Core.LogLevel.Error, "Could not find region id for bgs region {0}. Defaulting to {1}", new object[]
					{
						currentRegion.ToString(),
						"US"
					});
				}
				text = "US";
			}
			return text;
		}

		// Token: 0x0600C125 RID: 49445 RVA: 0x003AB488 File Offset: 0x003A9688
		private string GetRegionIdForInternal()
		{
			string environment = BattleNet.GetEnvironment();
			string regionIdForInternalEnvironment = MASDKRegionHelper.GetRegionIdForInternalEnvironment(environment);
			if (string.IsNullOrEmpty(regionIdForInternalEnvironment))
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(Blizzard.T5.Core.LogLevel.Error, "Could not find dev region id for environment {0}. Defaulting to {1}", new object[]
					{
						environment,
						"US(DEV)"
					});
				}
				return "US(DEV)";
			}
			return regionIdForInternalEnvironment;
		}

		// Token: 0x0600C126 RID: 49446 RVA: 0x003AB4DC File Offset: 0x003A96DC
		public void OnImportAccountSuccess(Account account)
		{
			if (string.IsNullOrEmpty(account.authenticationToken))
			{
				this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
				return;
			}
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Succesfully imported account {0}", new object[]
				{
					account.displayName
				});
			}
			this.SendImportResultTelemetry(MASDKImportResult.ImportResult.SUCCESS, 0);
			this.MobileAuth.AuthenticateAccount(account, this.GetRegionIdForConnectedFront(), this);
		}

		// Token: 0x0600C127 RID: 49447 RVA: 0x003AB548 File Offset: 0x003A9748
		public void OnImportAccountError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Error, "Failed to import account [{0}] {1} : {2}", new object[]
				{
					error.errorCode,
					error.errorMessage,
					error.errorContext
				});
			}
			this.TokenStorage.ClearStoredToken();
			this.SendImportResultTelemetry(MASDKImportResult.ImportResult.FAILURE, error.errorCode);
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C128 RID: 49448 RVA: 0x003AB5B7 File Offset: 0x003A97B7
		private void SendImportResultTelemetry(MASDKImportResult.ImportResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKImportResult(result, MASDKImportResult.ImportType.AUTH_TOKEN, errorCode);
		}

		// Token: 0x0600C129 RID: 49449 RVA: 0x003AB5CC File Offset: 0x003A97CC
		public void Authenticated(Account authenticatedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Information, "Successfully authenticated imported account {0}", new object[]
				{
					authenticatedAccount.displayName
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
			this.TokenStorage.ClearStoredToken();
			this.Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		// Token: 0x0600C12A RID: 49450 RVA: 0x003AB624 File Offset: 0x003A9824
		public void AuthenticationCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Warning, "Authentication for imported account canceled unexpectedly.", Array.Empty<object>());
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Canceled, null);
		}

		// Token: 0x0600C12B RID: 49451 RVA: 0x003AB658 File Offset: 0x003A9858
		public void AuthenticationError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(Blizzard.T5.Core.LogLevel.Warning, "Authentication for imported account failed due to error [{0}] {1} : {2}", new object[]
				{
					error.errorCode,
					error.errorMessage,
					error.errorContext
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C12C RID: 49452 RVA: 0x003AB6BC File Offset: 0x003A98BC
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "LegacyTokenStrategy");
		}
	}
}
