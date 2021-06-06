using System.Collections.Generic;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone.Core;

namespace Hearthstone.Login
{
	public class MobileLoginTokenFetcher : IPlatformLoginTokenFetcher
	{
		protected string m_webAuthUrl;

		protected string m_webToken;

		protected WebAuthDisplay m_webAuth;

		protected bool m_passSelectedTemporaryAccountId = true;

		private TokenPromise m_promise;

		private bool m_hasSeenAccountLanding;

		private ILogger Logger { get; set; }

		public MobileLoginTokenFetcher(ILogger logger, WebAuthDisplay webAuthDisplay)
		{
			Logger = logger;
			m_webAuth = webAuthDisplay;
		}

		public TokenPromise FetchToken(string challengeUrl)
		{
			m_promise = new TokenPromise();
			m_webAuthUrl = challengeUrl;
			Processor.QueueJob("GetLoginTokenFromWeb", Job_GetTokenFromWeb(), (IJobDependency[])null);
			return m_promise;
		}

		public void ClearCachedAuthentication()
		{
			WebAuth.ClearLoginData();
		}

		private IEnumerator<IAsyncJobResult> Job_GetTokenFromWeb()
		{
			m_webToken = WebAuth.GetStoredToken();
			if (string.IsNullOrEmpty(m_webToken))
			{
				Logger?.Log(LogLevel.Debug, "Stored web token not usable, getting new one");
				if (AreTutorialsComplete() && !m_hasSeenAccountLanding)
				{
					Logger?.Log(LogLevel.Debug, "All tutorials complete, skipping select account and assuming we are creating a new account");
					m_webAuthUrl = ExternalUrlService.Get().GetAccountLandingLink();
					m_webAuth.SetPassTempAccounts(passTempAccounts: false);
					m_hasSeenAccountLanding = true;
				}
				else
				{
					Logger?.Log(LogLevel.Debug, "Showing switch account menu");
					m_webAuth.SetPassTempAccounts(passTempAccounts: true);
					bool isShowingMenu = true;
					ShowAccountSwitchMenu(delegate(object selectedAccount)
					{
						TemporaryAccountManager.TemporaryAccountData.TemporaryAccount temporaryAccount = selectedAccount as TemporaryAccountManager.TemporaryAccountData.TemporaryAccount;
						if (temporaryAccount == null)
						{
							isShowingMenu = false;
						}
						else
						{
							m_promise.SetResult(TokenPromise.ResultType.Canceled);
							TemporaryAccountManager.Get().LoginTemporaryAccount(temporaryAccount);
						}
					});
					while (isShowingMenu)
					{
						yield return null;
					}
					Logger?.Log(LogLevel.Debug, "Returned from switch account");
				}
				m_webAuth.Create(m_webAuthUrl);
				m_webAuth.Load();
				Logger?.Log(LogLevel.Debug, "WebAuth loaded");
				while (!m_webAuth.Update())
				{
					yield return null;
				}
				m_webToken = m_webAuth.GetWebToken();
				Logger?.Log(LogLevel.Debug, "Got token {0} from WebAuth", m_webToken);
			}
			else
			{
				Logger?.Log(LogLevel.Debug, "Using stored token {0}", m_webToken);
			}
			TokenPromise.ResultType status = ((!string.IsNullOrEmpty(m_webToken)) ? TokenPromise.ResultType.Success : TokenPromise.ResultType.Failure);
			m_promise.SetResult(status, m_webToken);
		}

		protected virtual bool AreTutorialsComplete()
		{
			return GameUtils.AreAllTutorialsComplete(Options.Get().GetEnum<TutorialProgress>(Option.LOCAL_TUTORIAL_PROGRESS));
		}

		protected virtual void ShowAccountSwitchMenu(SwitchAccountMenu.OnSwitchAccountLogInPressed cb)
		{
			TemporaryAccountManager.Get().StartShowSwitchAccountMenu(cb, disableInputBlocker: true);
		}
	}
}
