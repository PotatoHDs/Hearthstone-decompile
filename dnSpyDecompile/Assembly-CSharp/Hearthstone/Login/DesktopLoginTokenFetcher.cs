using System;
using bgs;
using Blizzard.T5.Core;

namespace Hearthstone.Login
{
	// Token: 0x0200112C RID: 4396
	public class DesktopLoginTokenFetcher : IPlatformLoginTokenFetcher
	{
		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600C0C5 RID: 49349 RVA: 0x003AADE1 File Offset: 0x003A8FE1
		// (set) Token: 0x0600C0C6 RID: 49350 RVA: 0x003AADE9 File Offset: 0x003A8FE9
		private ILogger Logger { get; set; }

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600C0C7 RID: 49351 RVA: 0x003AADF2 File Offset: 0x003A8FF2
		private bool HasPreviousToken
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_lastWebToken);
			}
		}

		// Token: 0x0600C0C8 RID: 49352 RVA: 0x003AAE02 File Offset: 0x003A9002
		public DesktopLoginTokenFetcher(ILogger logger)
		{
			this.Logger = logger;
		}

		// Token: 0x0600C0C9 RID: 49353 RVA: 0x003AAE14 File Offset: 0x003A9014
		public TokenPromise FetchToken(string challengeUrl)
		{
			string tokenIfAvailable = this.GetTokenIfAvailable();
			TokenPromise tokenPromise = new TokenPromise();
			TokenPromise.ResultType status = string.IsNullOrEmpty(tokenIfAvailable) ? TokenPromise.ResultType.Failure : TokenPromise.ResultType.Success;
			tokenPromise.SetResult(status, tokenIfAvailable);
			return tokenPromise;
		}

		// Token: 0x0600C0CA RID: 49354 RVA: 0x003AAE44 File Offset: 0x003A9044
		private string GetTokenIfAvailable()
		{
			string text = this.GetTokenFromTokenFetcher();
			if (string.IsNullOrEmpty(text))
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(Blizzard.T5.Core.LogLevel.Debug, "Using stored launch option WEB_TOKEN", Array.Empty<object>());
				}
				text = BattleNet.GetLaunchOption("WEB_TOKEN", true);
			}
			if (this.m_lastWebToken == null || !this.m_lastWebToken.Equals(text))
			{
				this.m_lastWebToken = text;
				this.m_disallowRepeatTokens = false;
			}
			else if (this.m_disallowRepeatTokens)
			{
				text = null;
				this.Logger.Log(Blizzard.T5.Core.LogLevel.Information, "A repeated token was retrieved when disallowed. Setting to null token for failure", Array.Empty<object>());
			}
			return text;
		}

		// Token: 0x0600C0CB RID: 49355 RVA: 0x003AAECF File Offset: 0x003A90CF
		public void ClearCachedAuthentication()
		{
			this.m_disallowRepeatTokens = true;
		}

		// Token: 0x0600C0CC RID: 49356 RVA: 0x003AAED8 File Offset: 0x003A90D8
		private string GetTokenFromTokenFetcher()
		{
			string text = null;
			if (this.IsTokenFetcherAllowed())
			{
				if (HearthstoneApplication.IsInternal() && this.HasPreviousToken)
				{
					this.Logger.Log(Blizzard.T5.Core.LogLevel.Information, "Fetching new webToken since last one failed...", Array.Empty<object>());
					text = HearthstoneServices.Get<ITokenFetcherService>().GenerateWebAuthToken(true, true);
				}
				if (string.IsNullOrEmpty(text))
				{
					text = HearthstoneServices.Get<ITokenFetcherService>().GetCurrentAuthToken();
				}
			}
			return text;
		}

		// Token: 0x0600C0CD RID: 49357 RVA: 0x0001FA65 File Offset: 0x0001DC65
		protected virtual bool IsTokenFetcherAllowed()
		{
			return false;
		}

		// Token: 0x04009BF6 RID: 39926
		private string m_lastWebToken;

		// Token: 0x04009BF7 RID: 39927
		private bool m_disallowRepeatTokens;
	}
}
