using System;
using Blizzard.T5.Jobs;

// Token: 0x020006E2 RID: 1762
public class WaitForOverlayUI : IJobDependency, IAsyncJobResult
{
	// Token: 0x06006246 RID: 25158 RVA: 0x00200F1B File Offset: 0x001FF11B
	public bool IsReady()
	{
		return OverlayUI.Get() != null;
	}
}
