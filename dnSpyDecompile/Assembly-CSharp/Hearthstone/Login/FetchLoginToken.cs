using System;
using Blizzard.T5.Jobs;

namespace Hearthstone.Login
{
	// Token: 0x0200113B RID: 4411
	internal class FetchLoginToken : IJobDependency, IAsyncJobResult
	{
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600C130 RID: 49456 RVA: 0x003AB74C File Offset: 0x003A994C
		// (set) Token: 0x0600C131 RID: 49457 RVA: 0x003AB754 File Offset: 0x003A9954
		public string Token { get; protected set; } = string.Empty;

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600C132 RID: 49458 RVA: 0x003AB75D File Offset: 0x003A995D
		// (set) Token: 0x0600C133 RID: 49459 RVA: 0x003AB765 File Offset: 0x003A9965
		public string ChallengeUrl { get; protected set; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600C134 RID: 49460 RVA: 0x003AB76E File Offset: 0x003A996E
		// (set) Token: 0x0600C135 RID: 49461 RVA: 0x003AB776 File Offset: 0x003A9976
		public FetchLoginToken.Status CurrentStatus { get; set; }

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x0600C136 RID: 49462 RVA: 0x003AB77F File Offset: 0x003A997F
		// (set) Token: 0x0600C137 RID: 49463 RVA: 0x003AB787 File Offset: 0x003A9987
		protected IPlatformLoginTokenFetcher TokenFetcher { get; set; }

		// Token: 0x0600C138 RID: 49464 RVA: 0x003AB790 File Offset: 0x003A9990
		public FetchLoginToken(string challengeUrl, IPlatformLoginTokenFetcher tokenFetcher)
		{
			this.ChallengeUrl = challengeUrl;
			this.TokenFetcher = tokenFetcher;
			this.CurrentStatus = FetchLoginToken.Status.InProgress;
		}

		// Token: 0x0600C139 RID: 49465 RVA: 0x003AB7B8 File Offset: 0x003A99B8
		public bool IsReady()
		{
			this.UpdateStatus();
			return this.CurrentStatus != FetchLoginToken.Status.InProgress;
		}

		// Token: 0x0600C13A RID: 49466 RVA: 0x003AB7CC File Offset: 0x003A99CC
		protected void UpdateStatus()
		{
			if (this.CurrentStatus != FetchLoginToken.Status.InProgress)
			{
				return;
			}
			if (this.m_promise == null)
			{
				this.m_fetchAttempts++;
				if (this.m_fetchAttempts > 5)
				{
					this.CurrentStatus = FetchLoginToken.Status.Failed;
					return;
				}
				this.m_promise = this.TokenFetcher.FetchToken(this.ChallengeUrl);
			}
			if (!this.m_promise.IsReady())
			{
				return;
			}
			HearthstoneApplication.SendStartupTimeTelemetry("LoginService.UpdateStatus." + this.m_promise.Result);
			switch (this.m_promise.Result)
			{
			case TokenPromise.ResultType.Unknown:
			case TokenPromise.ResultType.Failure:
				this.m_promise = null;
				return;
			case TokenPromise.ResultType.Success:
				this.CurrentStatus = FetchLoginToken.Status.Success;
				this.Token = this.m_promise.Token;
				return;
			case TokenPromise.ResultType.Canceled:
				this.CurrentStatus = FetchLoginToken.Status.Cancelled;
				return;
			default:
				return;
			}
		}

		// Token: 0x04009C0D RID: 39949
		protected TokenPromise m_promise;

		// Token: 0x04009C0E RID: 39950
		protected int m_fetchAttempts;

		// Token: 0x04009C0F RID: 39951
		private const int MAX_FETCH_ATTEMPTS = 5;

		// Token: 0x02002911 RID: 10513
		public enum Status
		{
			// Token: 0x0400FBAE RID: 64430
			InProgress,
			// Token: 0x0400FBAF RID: 64431
			Success,
			// Token: 0x0400FBB0 RID: 64432
			Cancelled,
			// Token: 0x0400FBB1 RID: 64433
			Failed
		}
	}
}
