using System;
using bgs;
using Blizzard.T5.Core;
using Hearthstone.Core;

namespace Hearthstone.Login
{
	// Token: 0x02001145 RID: 4421
	public class OfflineAuthTokenCache
	{
		// Token: 0x17000D74 RID: 3444
		// (get) Token: 0x0600C1B5 RID: 49589 RVA: 0x003ACE56 File Offset: 0x003AB056
		// (set) Token: 0x0600C1B6 RID: 49590 RVA: 0x003ACE5E File Offset: 0x003AB05E
		public bool FailedToGenerateLoginWebToken { get; private set; }

		// Token: 0x17000D75 RID: 3445
		// (get) Token: 0x0600C1B7 RID: 49591 RVA: 0x003ACE67 File Offset: 0x003AB067
		// (set) Token: 0x0600C1B8 RID: 49592 RVA: 0x003ACE6F File Offset: 0x003AB06F
		protected TimeSpan? LoginTokenAge { get; set; }

		// Token: 0x0600C1B9 RID: 49593 RVA: 0x003ACE78 File Offset: 0x003AB078
		public OfflineAuthTokenCache(ILogger logger)
		{
			this.m_logger = logger;
		}

		// Token: 0x0600C1BA RID: 49594 RVA: 0x003ACE87 File Offset: 0x003AB087
		public string GetStoredToken()
		{
			if (this.UseMobileLogin())
			{
				this.m_webToken = WebAuth.GetStoredToken();
			}
			return this.m_webToken;
		}

		// Token: 0x0600C1BB RID: 49595 RVA: 0x003ACEA4 File Offset: 0x003AB0A4
		public void RefreshTokenCacheIfNeeded()
		{
			if (!this.UseMobileLogin())
			{
				if (this.m_lastRequestedLoginTokenTime != null)
				{
					this.LoginTokenAge = DateTime.Now - this.m_lastRequestedLoginTokenTime;
				}
				if (!Network.IsLoggedIn())
				{
					return;
				}
				if (this.m_lastRequestedLoginTokenTime == null || this.LoginTokenHasExpired())
				{
					ILogger logger = this.m_logger;
					if (logger != null)
					{
						logger.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginManager: Generating new SSO login token.", Array.Empty<object>());
					}
					this.m_lastRequestedLoginTokenTime = new DateTime?(DateTime.Now);
					BattleNet.GenerateWtcgWebCredentials(new Action<bool, string>(this.OnGenerateWebCredentialsResponse));
					this.ScheduleCallback(5f, true, new Processor.ScheduledCallback(this.OnGenerateWebCredentialsTimedOut), null);
				}
			}
		}

		// Token: 0x0600C1BC RID: 49596 RVA: 0x003ACF78 File Offset: 0x003AB178
		public bool LoginUsingStoredWebToken()
		{
			string storedToken = this.GetStoredToken();
			if (string.IsNullOrEmpty(storedToken))
			{
				ILogger logger = this.m_logger;
				if (logger != null)
				{
					logger.Log(Blizzard.T5.Core.LogLevel.Warning, "LoginUsingStoredWebToken - No web token available.", Array.Empty<object>());
				}
				return false;
			}
			string text;
			if (BattleNet.CheckWebAuth(out text))
			{
				ILogger logger2 = this.m_logger;
				if (logger2 != null)
				{
					logger2.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginUsingStoredWebToken - Providing Web Token: {0}", new object[]
					{
						this.m_webToken
					});
				}
				BattleNet.ProvideWebAuthToken(storedToken, null);
				this.m_webToken = null;
			}
			else
			{
				ILogger logger3 = this.m_logger;
				if (logger3 != null)
				{
					logger3.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginUsingStoredWebToken - CheckWebAuth had no Tassadar challenge", Array.Empty<object>());
				}
			}
			if (!this.UseMobileLogin())
			{
				this.m_lastRequestedLoginTokenTime = null;
			}
			return true;
		}

		// Token: 0x0600C1BD RID: 49597 RVA: 0x003AD022 File Offset: 0x003AB222
		protected virtual void ScheduleCallback(float secondsToWait, bool realTime, Processor.ScheduledCallback cb, object userData = null)
		{
			Processor.ScheduleCallback(secondsToWait, realTime, cb, userData);
		}

		// Token: 0x0600C1BE RID: 49598 RVA: 0x003AD02F File Offset: 0x003AB22F
		private void OnGenerateWebCredentialsTimedOut(object userData)
		{
			if (!this.LoginTokenHasExpired())
			{
				return;
			}
			this.FailedToGenerateLoginWebToken = true;
			ILogger logger = this.m_logger;
			if (logger == null)
			{
				return;
			}
			logger.Log(Blizzard.T5.Core.LogLevel.Debug, "OnGenerateWebCredentialsTimedOut() - Timed out before receiving SSO token.", Array.Empty<object>());
		}

		// Token: 0x0600C1BF RID: 49599 RVA: 0x003AD05C File Offset: 0x003AB25C
		private bool LoginTokenHasExpired()
		{
			return this.LoginTokenAge != null && this.LoginTokenAge > TimeSpan.FromSeconds(12600.0);
		}

		// Token: 0x0600C1C0 RID: 49600 RVA: 0x003AD0AC File Offset: 0x003AB2AC
		private void OnGenerateWebCredentialsResponse(bool hasToken, string token)
		{
			if (!this.UseMobileLogin())
			{
				if (!hasToken)
				{
					ILogger logger = this.m_logger;
					if (logger != null)
					{
						logger.Log(Blizzard.T5.Core.LogLevel.Error, "Unable to generate login token.", Array.Empty<object>());
					}
					this.FailedToGenerateLoginWebToken = true;
					return;
				}
				TimeSpan timeSpan;
				if (this.m_lastRequestedLoginTokenTime != null)
				{
					timeSpan = DateTime.Now - this.m_lastRequestedLoginTokenTime.Value;
					this.LoginTokenAge = new TimeSpan?(TimeSpan.Zero);
				}
				ILogger logger2 = this.m_logger;
				if (logger2 != null)
				{
					logger2.Log(Blizzard.T5.Core.LogLevel.Debug, "OnGenerateWebCredentialsResponse() - OldToken={0} | NewToken={1} | ResponseTime={2}", new object[]
					{
						this.m_webToken,
						token,
						timeSpan.TotalSeconds
					});
				}
				this.m_webToken = token;
			}
		}

		// Token: 0x0600C1C1 RID: 49601 RVA: 0x0001FA65 File Offset: 0x0001DC65
		protected virtual bool UseMobileLogin()
		{
			return false;
		}

		// Token: 0x04009C3F RID: 39999
		protected DateTime? m_lastRequestedLoginTokenTime;

		// Token: 0x04009C40 RID: 40000
		private ILogger m_logger;

		// Token: 0x04009C41 RID: 40001
		public string m_webToken;

		// Token: 0x04009C42 RID: 40002
		private const int LOGIN_TOKEN_LIFETIME_SECONDS = 12600;

		// Token: 0x04009C43 RID: 40003
		private const float GENERATE_LOGIN_TOKEN_TIMEOUT_SECONDS = 5f;
	}
}
