using System;
using Blizzard.T5.Jobs;

// Token: 0x020006E3 RID: 1763
public class WaitForBaseUI : IJobDependency, IAsyncJobResult
{
	// Token: 0x06006248 RID: 25160 RVA: 0x00200F28 File Offset: 0x001FF128
	public bool IsReady()
	{
		return BaseUI.Get() != null;
	}
}
