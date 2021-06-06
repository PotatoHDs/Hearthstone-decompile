using System;
using Blizzard.T5.Jobs;

// Token: 0x02000111 RID: 273
public class WaitForPlayerCollection : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001149 RID: 4425 RVA: 0x000453CE File Offset: 0x000435CE
	public bool IsReady()
	{
		return CollectionManager.Get() != null && CollectionManager.Get().IsFullyLoaded();
	}
}
