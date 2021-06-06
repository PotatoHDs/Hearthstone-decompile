using bgs;
using Blizzard.T5.Core;

namespace Hearthstone.Login
{
	public class DesktopLoginTokenFetcher : IPlatformLoginTokenFetcher
	{
		private string m_lastWebToken;

		private bool m_disallowRepeatTokens;

		private ILogger Logger { get; set; }

		private bool HasPreviousToken => !string.IsNullOrEmpty(m_lastWebToken);

		public DesktopLoginTokenFetcher(ILogger logger)
		{
			Logger = logger;
		}

		public TokenPromise FetchToken(string challengeUrl)
		{
			string tokenIfAvailable = GetTokenIfAvailable();
			TokenPromise tokenPromise = new TokenPromise();
			TokenPromise.ResultType status = ((!string.IsNullOrEmpty(tokenIfAvailable)) ? TokenPromise.ResultType.Success : TokenPromise.ResultType.Failure);
			tokenPromise.SetResult(status, tokenIfAvailable);
			return tokenPromise;
		}

		private string GetTokenIfAvailable()
		{
			string text = GetTokenFromTokenFetcher();
			if (string.IsNullOrEmpty(text))
			{
				Logger?.Log(Blizzard.T5.Core.LogLevel.Debug, "Using stored launch option WEB_TOKEN");
				text = BattleNet.GetLaunchOption("WEB_TOKEN", encrypted: true);
			}
			if (m_lastWebToken == null || !m_lastWebToken.Equals(text))
			{
				m_lastWebToken = text;
				m_disallowRepeatTokens = false;
			}
			else if (m_disallowRepeatTokens)
			{
				text = null;
				Logger.Log(Blizzard.T5.Core.LogLevel.Information, "A repeated token was retrieved when disallowed. Setting to null token for failure");
			}
			return text;
		}

		public void ClearCachedAuthentication()
		{
			m_disallowRepeatTokens = true;
		}

		private string GetTokenFromTokenFetcher()
		{
			string text = null;
			if (IsTokenFetcherAllowed())
			{
				if (HearthstoneApplication.IsInternal() && HasPreviousToken)
				{
					Logger.Log(Blizzard.T5.Core.LogLevel.Information, "Fetching new webToken since last one failed...");
					text = HearthstoneServices.Get<ITokenFetcherService>().GenerateWebAuthToken(forceGenerate: true);
				}
				if (string.IsNullOrEmpty(text))
				{
					text = HearthstoneServices.Get<ITokenFetcherService>().GetCurrentAuthToken();
				}
			}
			return text;
		}

		protected virtual bool IsTokenFetcherAllowed()
		{
			return false;
		}
	}
}
