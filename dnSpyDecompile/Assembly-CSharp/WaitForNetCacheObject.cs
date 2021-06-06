using System;
using Blizzard.T5.Jobs;

// Token: 0x02000607 RID: 1543
public class WaitForNetCacheObject<T> : IJobDependency, IAsyncJobResult
{
	// Token: 0x06005685 RID: 22149 RVA: 0x001C5E10 File Offset: 0x001C4010
	public bool IsReady()
	{
		NetCache netCache;
		return HearthstoneServices.TryGet<NetCache>(out netCache) && netCache.GetNetObject<T>() != null;
	}
}
