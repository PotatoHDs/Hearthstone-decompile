using System;
using Blizzard.MobileAuth;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using HearthstoneTelemetry;

namespace Hearthstone.Login
{
	// Token: 0x02001147 RID: 4423
	public class PresentLoginStrategy : IAsyncMobileLoginStrategy, IAuthenticationDelegate
	{
		// Token: 0x17000D78 RID: 3448
		// (get) Token: 0x0600C1CD RID: 49613 RVA: 0x003AD3EC File Offset: 0x003AB5EC
		// (set) Token: 0x0600C1CE RID: 49614 RVA: 0x003AD3F4 File Offset: 0x003AB5F4
		private ILogger Logger { get; set; }

		// Token: 0x17000D79 RID: 3449
		// (get) Token: 0x0600C1CF RID: 49615 RVA: 0x003AD3FD File Offset: 0x003AB5FD
		// (set) Token: 0x0600C1D0 RID: 49616 RVA: 0x003AD405 File Offset: 0x003AB605
		private ITelemetryClient TelemetryClient { get; set; }

		// Token: 0x0600C1D1 RID: 49617 RVA: 0x003AD40E File Offset: 0x003AB60E
		public PresentLoginStrategy(ILogger logger, ITelemetryClient telemetryClient)
		{
			this.Logger = logger;
			this.TelemetryClient = telemetryClient;
		}

		// Token: 0x0600C1D2 RID: 49618 RVA: 0x003AD424 File Offset: 0x003AB624
		public bool MeetsRequirements(LoginStrategyParameters arguments)
		{
			return arguments.MobileAuth != null;
		}

		// Token: 0x0600C1D3 RID: 49619 RVA: 0x003AD430 File Offset: 0x003AB630
		public void StartExecution(LoginStrategyParameters arguments, TokenPromise promise)
		{
			ILogger logger = this.Logger;
			if (logger != null)
			{
				logger.Log(LogLevel.Debug, "Executing Present Login Strategy", Array.Empty<object>());
			}
			this.m_promise = promise;
			arguments.MobileAuth.PresentLogin(this);
		}

		// Token: 0x0600C1D4 RID: 49620 RVA: 0x003AD464 File Offset: 0x003AB664
		public void Authenticated(Account authenticatedAccount)
		{
			if (this.m_promise == null)
			{
				ILogger logger = this.Logger;
				if (logger == null)
				{
					return;
				}
				logger.Log(LogLevel.Error, "Unexpected authentication callback in Present Login. No promise was set", Array.Empty<object>());
				return;
			}
			else
			{
				if (string.IsNullOrEmpty(authenticatedAccount.authenticationToken))
				{
					ILogger logger2 = this.Logger;
					if (logger2 != null)
					{
						logger2.Log(LogLevel.Error, "Unexpected missing token from present login success", Array.Empty<object>());
					}
					this.m_promise.SetResult(TokenPromise.ResultType.Failure, null);
					return;
				}
				ILogger logger3 = this.Logger;
				if (logger3 != null)
				{
					logger3.Log(LogLevel.Information, "Present login succesfully authenticated account {0} {1}", new object[]
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

		// Token: 0x0600C1D5 RID: 49621 RVA: 0x003AD51C File Offset: 0x003AB71C
		public void AuthenticationCancelled()
		{
			if (this.m_promise != null)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Information, "Present login was canceled", Array.Empty<object>());
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
			logger2.Log(LogLevel.Error, "Unexpected authentication canceled callback in Present Login. No promise was set", Array.Empty<object>());
		}

		// Token: 0x0600C1D6 RID: 49622 RVA: 0x003AD588 File Offset: 0x003AB788
		public void AuthenticationError(BlzMobileAuthError error)
		{
			if (this.m_promise != null)
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Error, "Present Login authentication error. {0} {1} {2}", new object[]
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
			logger2.Log(LogLevel.Error, "Unexpected authentication error callback in Present Login. No promise was set", Array.Empty<object>());
		}

		// Token: 0x0600C1D7 RID: 49623 RVA: 0x003AD617 File Offset: 0x003AB817
		private void SendAuthResultTelemetry(MASDKAuthResult.AuthResult result, int errorCode = 0)
		{
			ITelemetryClient telemetryClient = this.TelemetryClient;
			if (telemetryClient == null)
			{
				return;
			}
			telemetryClient.SendMASDKAuthResult(result, errorCode, "PresentLoginStrategy");
		}

		// Token: 0x04009C48 RID: 40008
		private TokenPromise m_promise;
	}
}
