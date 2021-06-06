using System;
using Blizzard.T5.Jobs;
using Hearthstone.Streaming;

// Token: 0x02000912 RID: 2322
public class WaitForGameDownloadManagerState : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600819C RID: 33180 RVA: 0x002A2EBD File Offset: 0x002A10BD
	public bool IsReady()
	{
		return HearthstoneServices.IsAvailable<GameDownloadManager>() && HearthstoneServices.Get<GameDownloadManager>().IsReadyToPlay;
	}
}
