using System;
using Blizzard.T5.Jobs;
using Hearthstone.Http;

namespace Hearthstone.Core.Streaming
{
	// Token: 0x02001096 RID: 4246
	internal class VersionPipelineJsonFetcher : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600B81B RID: 47131 RVA: 0x003868BA File Offset: 0x00384ABA
		public string PipelineJson
		{
			get
			{
				return this.m_httpRequest.ResponseAsString;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600B81C RID: 47132 RVA: 0x003868C7 File Offset: 0x00384AC7
		private bool IsDone
		{
			get
			{
				return this.m_httpRequest.IsDone;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x0600B81D RID: 47133 RVA: 0x003868D4 File Offset: 0x00384AD4
		private bool Succesful
		{
			get
			{
				return this.m_state == VersionPipelineJsonFetcher.InternalState.SUCCESS;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600B81E RID: 47134 RVA: 0x003868DF File Offset: 0x00384ADF
		private bool Failed
		{
			get
			{
				return this.m_state == VersionPipelineJsonFetcher.InternalState.FAILURE;
			}
		}

		// Token: 0x0600B81F RID: 47135 RVA: 0x003868EA File Offset: 0x00384AEA
		public VersionPipelineJsonFetcher()
		{
			this.m_httpRequest = HttpRequest.Get("http://hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net:8080/api/v1/pipelines/list");
			this.m_httpRequest.TimeoutSeconds = 5;
			this.m_httpRequest.SendRequest();
		}

		// Token: 0x0600B820 RID: 47136 RVA: 0x0038691A File Offset: 0x00384B1A
		public bool HasFailed()
		{
			if (!this.IsDone)
			{
				return false;
			}
			if (this.NeedsStateUpdate())
			{
				this.UpdateState();
			}
			return this.Failed;
		}

		// Token: 0x0600B821 RID: 47137 RVA: 0x0038693A File Offset: 0x00384B3A
		public bool IsReady()
		{
			if (!this.IsDone)
			{
				return false;
			}
			if (this.NeedsStateUpdate())
			{
				this.UpdateState();
			}
			return this.Succesful;
		}

		// Token: 0x0600B822 RID: 47138 RVA: 0x0038695C File Offset: 0x00384B5C
		private void UpdateState()
		{
			if (!this.IsDone)
			{
				this.m_state = VersionPipelineJsonFetcher.InternalState.WAITING;
				return;
			}
			if (this.m_httpRequest.DidTimeout)
			{
				Log.Downloader.PrintError("Could not reach version proxy service, request timed out", Array.Empty<object>());
				this.m_state = VersionPipelineJsonFetcher.InternalState.FAILURE;
				return;
			}
			if (this.m_httpRequest.IsHttpError || this.m_httpRequest.IsNetworkError)
			{
				Log.Downloader.PrintInfo("Error fetching pipeline information {0}: {1}", new object[]
				{
					this.m_httpRequest.ResponseStatusCode,
					this.m_httpRequest.ErrorString
				});
				this.m_state = VersionPipelineJsonFetcher.InternalState.FAILURE;
				return;
			}
			this.m_state = VersionPipelineJsonFetcher.InternalState.SUCCESS;
		}

		// Token: 0x0600B823 RID: 47139 RVA: 0x00386A04 File Offset: 0x00384C04
		private bool NeedsStateUpdate()
		{
			return this.m_state == VersionPipelineJsonFetcher.InternalState.WAITING;
		}

		// Token: 0x0400985A RID: 39002
		private VersionPipelineJsonFetcher.InternalState m_state;

		// Token: 0x0400985B RID: 39003
		private IHttpRequest m_httpRequest;

		// Token: 0x0400985C RID: 39004
		private const string VERSION_SERVICE_PIPELINE_LIST_URL = "http://hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net:8080/api/v1/pipelines/list";

		// Token: 0x0400985D RID: 39005
		private const int HTTP_REQUEST_TIMEOUT_SECONDS = 5;

		// Token: 0x020028A9 RID: 10409
		private enum InternalState
		{
			// Token: 0x0400FA8F RID: 64143
			WAITING,
			// Token: 0x0400FA90 RID: 64144
			SUCCESS,
			// Token: 0x0400FA91 RID: 64145
			FAILURE
		}
	}
}
