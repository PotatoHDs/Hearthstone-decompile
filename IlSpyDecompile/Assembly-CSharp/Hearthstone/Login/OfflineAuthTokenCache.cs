using System;
using bgs;
using Blizzard.T5.Core;
using Hearthstone.Core;

namespace Hearthstone.Login
{
	public class OfflineAuthTokenCache
	{
		protected DateTime? m_lastRequestedLoginTokenTime;

		private ILogger m_logger;

		public string m_webToken;

		private const int LOGIN_TOKEN_LIFETIME_SECONDS = 12600;

		private const float GENERATE_LOGIN_TOKEN_TIMEOUT_SECONDS = 5f;

		public bool FailedToGenerateLoginWebToken { get; private set; }

		protected TimeSpan? LoginTokenAge { get; set; }

		public OfflineAuthTokenCache(ILogger logger)
		{
			m_logger = logger;
		}

		public string GetStoredToken()
		{
			if (UseMobileLogin())
			{
				m_webToken = WebAuth.GetStoredToken();
			}
			return m_webToken;
		}

		public void RefreshTokenCacheIfNeeded()
		{
			if (!UseMobileLogin())
			{
				if (m_lastRequestedLoginTokenTime.HasValue)
				{
					DateTime now = DateTime.Now;
					DateTime? lastRequestedLoginTokenTime = m_lastRequestedLoginTokenTime;
					LoginTokenAge = now - lastRequestedLoginTokenTime;
				}
				if (Network.IsLoggedIn() && (!m_lastRequestedLoginTokenTime.HasValue || LoginTokenHasExpired()))
				{
					m_logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginManager: Generating new SSO login token.");
					m_lastRequestedLoginTokenTime = DateTime.Now;
					BattleNet.GenerateWtcgWebCredentials(OnGenerateWebCredentialsResponse);
					ScheduleCallback(5f, realTime: true, OnGenerateWebCredentialsTimedOut);
				}
			}
		}

		public bool LoginUsingStoredWebToken()
		{
			string storedToken = GetStoredToken();
			if (string.IsNullOrEmpty(storedToken))
			{
				m_logger?.Log(Blizzard.T5.Core.LogLevel.Warning, "LoginUsingStoredWebToken - No web token available.");
				return false;
			}
			if (BattleNet.CheckWebAuth(out var _))
			{
				m_logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginUsingStoredWebToken - Providing Web Token: {0}", m_webToken);
				BattleNet.ProvideWebAuthToken(storedToken);
				m_webToken = null;
			}
			else
			{
				m_logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "LoginUsingStoredWebToken - CheckWebAuth had no Tassadar challenge");
			}
			if (!UseMobileLogin())
			{
				m_lastRequestedLoginTokenTime = null;
			}
			return true;
		}

		protected virtual void ScheduleCallback(float secondsToWait, bool realTime, Processor.ScheduledCallback cb, object userData = null)
		{
			Processor.ScheduleCallback(secondsToWait, realTime, cb, userData);
		}

		private void OnGenerateWebCredentialsTimedOut(object userData)
		{
			if (LoginTokenHasExpired())
			{
				FailedToGenerateLoginWebToken = true;
				m_logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "OnGenerateWebCredentialsTimedOut() - Timed out before receiving SSO token.");
			}
		}

		private bool LoginTokenHasExpired()
		{
			if (LoginTokenAge.HasValue)
			{
				return LoginTokenAge > TimeSpan.FromSeconds(12600.0);
			}
			return false;
		}

		private void OnGenerateWebCredentialsResponse(bool hasToken, string token)
		{
			if (UseMobileLogin())
			{
				return;
			}
			if (!hasToken)
			{
				m_logger?.Log(Blizzard.T5.Core.LogLevel.Error, "Unable to generate login token.");
				FailedToGenerateLoginWebToken = true;
				return;
			}
			TimeSpan timeSpan = default(TimeSpan);
			if (m_lastRequestedLoginTokenTime.HasValue)
			{
				timeSpan = DateTime.Now - m_lastRequestedLoginTokenTime.Value;
				LoginTokenAge = TimeSpan.Zero;
			}
			m_logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "OnGenerateWebCredentialsResponse() - OldToken={0} | NewToken={1} | ResponseTime={2}", m_webToken, token, timeSpan.TotalSeconds);
			m_webToken = token;
		}

		protected virtual bool UseMobileLogin()
		{
			return false;
		}
	}
}
