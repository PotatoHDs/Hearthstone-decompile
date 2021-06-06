using System;
using System.Collections.Generic;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;

namespace Hearthstone.Login
{
	// Token: 0x02001142 RID: 4418
	public class MobileLoginTokenFetcher : IPlatformLoginTokenFetcher
	{
		// Token: 0x17000D71 RID: 3441
		// (get) Token: 0x0600C199 RID: 49561 RVA: 0x003ACA0E File Offset: 0x003AAC0E
		// (set) Token: 0x0600C19A RID: 49562 RVA: 0x003ACA16 File Offset: 0x003AAC16
		private ILogger Logger { get; set; }

		// Token: 0x0600C19B RID: 49563 RVA: 0x003ACA1F File Offset: 0x003AAC1F
		public MobileLoginTokenFetcher(ILogger logger, WebAuthDisplay webAuthDisplay)
		{
			this.Logger = logger;
			this.m_webAuth = webAuthDisplay;
		}

		// Token: 0x0600C19C RID: 49564 RVA: 0x003ACA3C File Offset: 0x003AAC3C
		public TokenPromise FetchToken(string challengeUrl)
		{
			this.m_promise = new TokenPromise();
			this.m_webAuthUrl = challengeUrl;
			Processor.QueueJob("GetLoginTokenFromWeb", this.Job_GetTokenFromWeb(), null);
			return this.m_promise;
		}

		// Token: 0x0600C19D RID: 49565 RVA: 0x003ACA68 File Offset: 0x003AAC68
		public void ClearCachedAuthentication()
		{
			WebAuth.ClearLoginData();
		}

		// Token: 0x0600C19E RID: 49566 RVA: 0x003ACA6F File Offset: 0x003AAC6F
		private IEnumerator<IAsyncJobResult> Job_GetTokenFromWeb()
		{
			this.m_webToken = WebAuth.GetStoredToken();
			if (string.IsNullOrEmpty(this.m_webToken))
			{
				ILogger logger = this.Logger;
				if (logger != null)
				{
					logger.Log(LogLevel.Debug, "Stored web token not usable, getting new one", Array.Empty<object>());
				}
				if (this.AreTutorialsComplete() && !this.m_hasSeenAccountLanding)
				{
					ILogger logger2 = this.Logger;
					if (logger2 != null)
					{
						logger2.Log(LogLevel.Debug, "All tutorials complete, skipping select account and assuming we are creating a new account", Array.Empty<object>());
					}
					this.m_webAuthUrl = ExternalUrlService.Get().GetAccountLandingLink();
					this.m_webAuth.SetPassTempAccounts(false);
					this.m_hasSeenAccountLanding = true;
				}
				else
				{
					MobileLoginTokenFetcher.<>c__DisplayClass13_0 CS$<>8__locals1 = new MobileLoginTokenFetcher.<>c__DisplayClass13_0();
					CS$<>8__locals1.<>4__this = this;
					ILogger logger3 = this.Logger;
					if (logger3 != null)
					{
						logger3.Log(LogLevel.Debug, "Showing switch account menu", Array.Empty<object>());
					}
					this.m_webAuth.SetPassTempAccounts(true);
					CS$<>8__locals1.isShowingMenu = true;
					this.ShowAccountSwitchMenu(delegate(object selectedAccount)
					{
						TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount = selectedAccount as TemporaryAccountManager.TemporaryAccountData.TemporaryAccount;
						if (temporaryAccount == null)
						{
							CS$<>8__locals1.isShowingMenu = false;
							return;
						}
						CS$<>8__locals1.<>4__this.m_promise.SetResult(TokenPromise.ResultType.Canceled, null);
						TemporaryAccountManager.Get().LoginTemporaryAccount(temporaryAccount);
					});
					while (CS$<>8__locals1.isShowingMenu)
					{
						yield return null;
					}
					ILogger logger4 = this.Logger;
					if (logger4 != null)
					{
						logger4.Log(LogLevel.Debug, "Returned from switch account", Array.Empty<object>());
					}
					CS$<>8__locals1 = null;
				}
				this.m_webAuth.Create(this.m_webAuthUrl);
				this.m_webAuth.Load();
				ILogger logger5 = this.Logger;
				if (logger5 != null)
				{
					logger5.Log(LogLevel.Debug, "WebAuth loaded", Array.Empty<object>());
				}
				while (!this.m_webAuth.Update())
				{
					yield return null;
				}
				this.m_webToken = this.m_webAuth.GetWebToken();
				ILogger logger6 = this.Logger;
				if (logger6 != null)
				{
					logger6.Log(LogLevel.Debug, "Got token {0} from WebAuth", new object[]
					{
						this.m_webToken
					});
				}
			}
			else
			{
				ILogger logger7 = this.Logger;
				if (logger7 != null)
				{
					logger7.Log(LogLevel.Debug, "Using stored token {0}", new object[]
					{
						this.m_webToken
					});
				}
			}
			TokenPromise.ResultType status = (!string.IsNullOrEmpty(this.m_webToken)) ? TokenPromise.ResultType.Success : TokenPromise.ResultType.Failure;
			this.m_promise.SetResult(status, this.m_webToken);
			yield break;
		}

		// Token: 0x0600C19F RID: 49567 RVA: 0x003AACD3 File Offset: 0x003A8ED3
		protected virtual bool AreTutorialsComplete()
		{
			return GameUtils.AreAllTutorialsComplete(Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS));
		}

		// Token: 0x0600C1A0 RID: 49568 RVA: 0x003ACA7E File Offset: 0x003AAC7E
		protected virtual void ShowAccountSwitchMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed cb)
		{
			TemporaryAccountManager.Get().StartShowSwitchAccountMenu(cb, true);
		}

		// Token: 0x04009C34 RID: 39988
		protected string m_webAuthUrl;

		// Token: 0x04009C35 RID: 39989
		protected string m_webToken;

		// Token: 0x04009C36 RID: 39990
		protected WebAuthDisplay m_webAuth;

		// Token: 0x04009C37 RID: 39991
		protected bool m_passSelectedTemporaryAccountId = true;

		// Token: 0x04009C38 RID: 39992
		private TokenPromise m_promise;

		// Token: 0x04009C3A RID: 39994
		private bool m_hasSeenAccountLanding;
	}
}
