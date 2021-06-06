using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001146 RID: 4422
	public class PresentChallengeStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		// Token: 0x17000D76 RID: 3446
		// (get) Token: 0x0600C1C2 RID: 49602 RVA: 0x003AD15E File Offset: 0x003AB35E
		// (set) Token: 0x0600C1C3 RID: 49603 RVA: 0x003AD166 File Offset: 0x003AB366
		private ILogger Logger { get; set; }

		// Token: 0x17000D77 RID: 3447
		// (get) Token: 0x0600C1C4 RID: 49604 RVA: 0x003AD16F File Offset: 0x003AB36F
		// (set) Token: 0x0600C1C5 RID: 49605 RVA: 0x003AD177 File Offset: 0x003AB377
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C1C6 RID: 49606 RVA: 0x003AD180 File Offset: 0x003AB380
		public PresentChallengeStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C1C7 RID: 49607 RVA: 0x003AD196 File Offset: 0x003AB396
		public bool MeetsRequirements(LoginStrategyParameters arguments)
		{
			return !string.IsNullOrEmpty(arguments.ChallengeUrl);
		}

		// Token: 0x0600C1C8 RID: 49608 RVA: 0x003AD1A8 File Offset: 0x003AB3A8
		public void StartExecution(LoginStrategyParameters arguments, TokenPromise promise)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Executing Present Challenge login strategy", Array.Empty<object>());
			}
			if (string.IsNullOrEmpty(arguments.ChallengeUrl))
			{
				ILogger logger2 = this.Logger;
				if (logger2 != null)
				{
					logger2.Log(LogLevel.Error, "Unexpected execution, attempted to present challenge with missing challenge url", Array.Empty<object>());
				}
				promise.SetResult(TokenPromise.ResultType.Failure, null);
				return;
			}
			this.m_promise = promise;
			arguments.MobileAuth.PresentChallenge(arguments.ChallengeUrl, this);
		}

		// Token: 0x0600C1C9 RID: 49609 RVA: 0x003AD220 File Offset: 0x003AB420
		public void Authenticated(Account authenticatedAccount)
		{
			if (this.m_promise == null)
			{
				ILogger logger = this.Logger;
				if (logger == null)
				{
					return;
				}
				logger.Log(LogLevel.Error, "Unexpected authentication callback in Present Challenge. No promise was set", Array.Empty<object>());
				return;
			}
			else
			{
				if (string.IsNullOrEmpty(authenticatedAccount.authenticationToken))
				{
					ILogger logger2 = this.Logger;
					if (logger2 != null)
					{
						logger2.Log(LogLevel.Error, "Unexpected missing token from present challenge success", Array.Empty<object>());
					}
					this.m_promise.SetResult(TokenPromise.ResultType.Failure, null);
					return;
				}
				ILogger logger3 = this.Logger;
				if (logger3 != null)
				{
					logger3.Log(LogLevel.Information, "Present challenge succesfully authenticated account {0} {1}", new object[]
					{
						authenticatedAccount.battleTag,
						authenticatedAccount.displayName
					});
				}
				this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.SUCCESS, 0);
				this.m_promise.SetResult(TokenPromise.ResultType.Success, authenticatedAccount.authenticationToken);
				this.m_promise = null;
				return;
			}
		}

		// Token: 0x0600C1CA RID: 49610 RVA: 0x003AD2D8 File Offset: 0x003AB4D8
		public void AuthenticationCancelled()
		{
			if (this.m_promise != null)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Information, "Present challenge was canceled", Array.Empty<object>());
				}
				this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.CANCELED, 0);
				this.m_promise.SetResult(TokenPromise.ResultType.Canceled, null);
				this.m_promise = null;
				return;
			}
			ILogger logger2 = this.Logger;
			if (logger2 == null)
			{
				return;
			}
			logger2.Log(LogLevel.Error, "Unexpected authentication canceled callback in Present Challenge. No promise was set", Array.Empty<object>());
		}

		// Token: 0x0600C1CB RID: 49611 RVA: 0x003AD344 File Offset: 0x003AB544
		public void AuthenticationError(BlzMobileAuthError error)
		{
			if (this.m_promise != null)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Error, "Present Challenge authentication error. {0} {1} {2}", new object[]
					{
						error.errorCode,
						error.errorContext,
						error.errorMessage
					});
				}
				this.SendAuthResultTelemetry(MASDKAuthResult.AuthResult.FAILURE, error.errorCode);
				this.m_promise.SetResult(TokenPromise.ResultType.Failure, null);
				this.m_promise = null;
				return;
			}
			ILogger logger2 = this.Logger;
			if (logger2 == null)
			{
				return;
			}
			logger2.Log(LogLevel.Error, "Unexpected authentication error callback in Present Challenge. No promise was set", Array.Empty<object>());
		}

		// Token: 0x0600C1CC RID: 49612 RVA: 0x003AD3D3 File Offset: 0x003AB5D3
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "PresentChallengeStrategy");
		}

		// Token: 0x04009C46 RID: 40006
		private TokenPromise m_promise;
	}
}
