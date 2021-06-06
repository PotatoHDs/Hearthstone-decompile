using System;
using System.Collections.Generic;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001149 RID: 4425
	public class SelectAccountStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		// Token: 0x17000D7C RID: 3452
		// (get) Token: 0x0600C1E0 RID: 49632 RVA: 0x003AD7B8 File Offset: 0x003AB9B8
		// (set) Token: 0x0600C1E1 RID: 49633 RVA: 0x003AD7C0 File Offset: 0x003AB9C0
		private ILogger Logger { get; set; }

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600C1E2 RID: 49634 RVA: 0x003AD7C9 File Offset: 0x003AB9C9
		// (set) Token: 0x0600C1E3 RID: 49635 RVA: 0x003AD7D1 File Offset: 0x003AB9D1
		private TokenPromise Promise { get; set; }

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600C1E4 RID: 49636 RVA: 0x003AD7DA File Offset: 0x003AB9DA
		// (set) Token: 0x0600C1E5 RID: 49637 RVA: 0x003AD7E2 File Offset: 0x003AB9E2
		private ISwitchAccountMenuController SwitchAccountMenu { get; set; }

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x0600C1E6 RID: 49638 RVA: 0x003AD7EB File Offset: 0x003AB9EB
		// (set) Token: 0x0600C1E7 RID: 49639 RVA: 0x003AD7F3 File Offset: 0x003AB9F3
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C1E8 RID: 49640 RVA: 0x003AD7FC File Offset: 0x003AB9FC
		public SelectAccountStrategy(ILogger logger, ISwitchAccountMenuController switchAccount, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.SwitchAccountMenu = switchAccount;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C1E9 RID: 49641 RVA: 0x003AD81C File Offset: 0x003ABA1C
		public void Authenticated(Account authenticatedAccount)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Succesfully authenticated account {0}", new object[]
				{
					authenticatedAccount.displayName
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
		}

		// Token: 0x0600C1EA RID: 49642 RVA: 0x003AD869 File Offset: 0x003ABA69
		public void AuthenticationCancelled()
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Information, "Account authentication canceled!", Array.Empty<object>());
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED, 0);
			this.Promise.SetResult(TokenPromise.ResultType.Canceled, null);
		}

		// Token: 0x0600C1EB RID: 49643 RVA: 0x003AD89C File Offset: 0x003ABA9C
		public void AuthenticationError(BlzMobileAuthError error)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Error, "Account authentication error [{0}] {1}\nMessage:{2}", new object[]
				{
					error.errorCode,
					error.errorContext,
					error.errorMessage
				});
			}
			this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
			this.Promise.SetResult(TokenPromise.ResultType.Failure, null);
		}

		// Token: 0x0600C1EC RID: 49644 RVA: 0x003AD900 File Offset: 0x003ABB00
		public bool MeetsRequirements(LoginStrategyParameters parameters)
		{
			if (!parameters.ChallengeUrl.Contains("externalChallenge=login", StringComparison.InvariantCulture))
			{
				return false;
			}
			List<Account> softAccounts = parameters.MobileAuth.GetSoftAccounts();
			return softAccounts != null && softAccounts.Count > 0;
		}

		// Token: 0x0600C1ED RID: 49645 RVA: 0x003AD940 File Offset: 0x003ABB40
		public void StartExecution(LoginStrategyParameters parameters, TokenPromise promise)
		{
			this.Promise = promise;
			List<Account> softAccounts = parameters.MobileAuth.GetSoftAccounts();
			this.SwitchAccountMenu.ShowSwitchAccount(softAccounts, delegate(Account? selectedAccount)
			{
				if (selectedAccount == null || selectedAccount == null)
				{
					ILogger logger = this.Logger;
					if (logger != null)
					{
						logger.Log(LogLevel.Information, "No account selected from menu, presenting login", Array.Empty<object>());
					}
					parameters.MobileAuth.PresentChallenge(parameters.ChallengeUrl, this);
					return;
				}
				Account value = selectedAccount.Value;
				Region currentlyConnectedRegion = MASDKRegionHelper.GetCurrentlyConnectedRegion();
				ILogger logger2 = this.Logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Information, "Selected account {0}, requesting authentication for region {1}", new object[]
					{
						value.battleTag,
						currentlyConnectedRegion.name
					});
				}
				parameters.MobileAuth.AuthenticateAccount(value, currentlyConnectedRegion.regionId, this);
			});
		}

		// Token: 0x0600C1EE RID: 49646 RVA: 0x003AD991 File Offset: 0x003ABB91
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "SelectAccountStrategy");
		}
	}
}
