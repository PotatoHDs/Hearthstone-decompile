using Blizzard.T5.Jobs;
using Hearthstone.Streaming;

public class WaitForGameDownloadManagerState : IJobDependency, IAsyncJobResult
{
	public bool IsReady()
	{
		if (!HearthstoneServices.IsAvailable<GameDownloadManager>())
		{
			return false;
		}
		return HearthstoneServices.Get<GameDownloadManager>().IsReadyToPlay;
	}
}
