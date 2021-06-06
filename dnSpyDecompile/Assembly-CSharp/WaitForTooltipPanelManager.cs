using System;
using Blizzard.T5.Jobs;

// Token: 0x020006E4 RID: 1764
public class WaitForTooltipPanelManager : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600624A RID: 25162 RVA: 0x00200F35 File Offset: 0x001FF135
	public bool IsReady()
	{
		return TooltipPanelManager.Get() != null;
	}
}
