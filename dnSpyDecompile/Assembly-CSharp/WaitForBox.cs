using System;
using Blizzard.T5.Jobs;

// Token: 0x0200068E RID: 1678
public class WaitForBox : IJobDependency, IAsyncJobResult
{
	// Token: 0x06005E13 RID: 24083 RVA: 0x001E9D76 File Offset: 0x001E7F76
	public bool IsReady()
	{
		return Box.Get() != null;
	}
}
