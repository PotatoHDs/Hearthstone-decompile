using Blizzard.T5.Jobs;

namespace Hearthstone.Login
{
	internal class FetchLoginToken : IJobDependency, IAsyncJobResult
	{
		public enum Status
		{
			InProgress,
			Success,
			Cancelled,
			Failed
		}

		protected TokenPromise m_promise;

		protected int m_fetchAttempts;

		private const int MAX_FETCH_ATTEMPTS = 5;

		public string Token { get; protected set; } = string.Empty;


		public string ChallengeUrl { get; protected set; }

		public Status CurrentStatus { get; set; }

		protected IPlatformLoginTokenFetcher TokenFetcher { get; set; }

		public FetchLoginToken(string challengeUrl, IPlatformLoginTokenFetcher tokenFetcher)
		{
			ChallengeUrl = challengeUrl;
			TokenFetcher = tokenFetcher;
			CurrentStatus = Status.InProgress;
		}

		public bool IsReady()
		{
			UpdateStatus();
			if (CurrentStatus != 0)
			{
				return true;
			}
			return false;
		}

		protected void UpdateStatus()
		{
			if (CurrentStatus != 0)
			{
				return;
			}
			if (m_promise == null)
			{
				m_fetchAttempts++;
				if (m_fetchAttempts > 5)
				{
					CurrentStatus = Status.Failed;
					return;
				}
				m_promise = TokenFetcher.FetchToken(ChallengeUrl);
			}
			if (m_promise.IsReady())
			{
				HearthstoneApplication.SendStartupTimeTelemetry("LoginService.UpdateStatus." + m_promise.Result);
				switch (m_promise.Result)
				{
				case TokenPromise.ResultType.Canceled:
					CurrentStatus = Status.Cancelled;
					break;
				case TokenPromise.ResultType.Unknown:
				case TokenPromise.ResultType.Failure:
					m_promise = null;
					break;
				case TokenPromise.ResultType.Success:
					CurrentStatus = Status.Success;
					Token = m_promise.Token;
					break;
				}
			}
		}
	}
}
