using System;
using Blizzard.T5.Jobs;

// Token: 0x02000843 RID: 2115
public class WaitForAchieves : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600711A RID: 28954 RVA: 0x00247BC4 File Offset: 0x00245DC4
	public bool IsReady()
	{
		AchieveManager achieveManager;
		return HearthstoneServices.TryGet<AchieveManager>(out achieveManager) && achieveManager.IsReady();
	}
}
