using Blizzard.T5.Jobs;
using Hearthstone.Http;

namespace Hearthstone.Core.Streaming
{
	internal class VersionPipelineJsonFetcher : IUnreliableJobDependency, IJobDependency, IAsyncJobResult
	{
		private enum InternalState
		{
			WAITING,
			SUCCESS,
			FAILURE
		}

		private InternalState m_state;

		private IHttpRequest m_httpRequest;

		private const string VERSION_SERVICE_PIPELINE_LIST_URL = "http://hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net:8080/api/v1/pipelines/list";

		private const int HTTP_REQUEST_TIMEOUT_SECONDS = 5;

		public string PipelineJson => m_httpRequest.ResponseAsString;

		private bool IsDone => m_httpRequest.IsDone;

		private bool Succesful => m_state == InternalState.SUCCESS;

		private bool Failed => m_state == InternalState.FAILURE;

		public VersionPipelineJsonFetcher()
		{
			m_httpRequest = HttpRequest.Get("http://hs.version-service-proxy.bnet-game-publishing-dev.cloud.blizzard.net:8080/api/v1/pipelines/list");
			m_httpRequest.TimeoutSeconds = 5;
			m_httpRequest.SendRequest();
		}

		public bool HasFailed()
		{
			if (!IsDone)
			{
				return false;
			}
			if (NeedsStateUpdate())
			{
				UpdateState();
			}
			return Failed;
		}

		public bool IsReady()
		{
			if (!IsDone)
			{
				return false;
			}
			if (NeedsStateUpdate())
			{
				UpdateState();
			}
			return Succesful;
		}

		private void UpdateState()
		{
			if (!IsDone)
			{
				m_state = InternalState.WAITING;
			}
			else if (m_httpRequest.DidTimeout)
			{
				Log.Downloader.PrintError("Could not reach version proxy service, request timed out");
				m_state = InternalState.FAILURE;
			}
			else if (m_httpRequest.IsHttpError || m_httpRequest.IsNetworkError)
			{
				Log.Downloader.PrintInfo("Error fetching pipeline information {0}: {1}", m_httpRequest.ResponseStatusCode, m_httpRequest.ErrorString);
				m_state = InternalState.FAILURE;
			}
			else
			{
				m_state = InternalState.SUCCESS;
			}
		}

		private bool NeedsStateUpdate()
		{
			return m_state == InternalState.WAITING;
		}
	}
}
