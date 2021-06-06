using System;
using Blizzard.T5.Jobs;

// Token: 0x0200036F RID: 879
public class WaitForFullLoginFlowComplete : IJobDependency, IAsyncJobResult
{
	// Token: 0x060033A1 RID: 13217 RVA: 0x00109214 File Offset: 0x00107414
	public bool IsReady()
	{
		LoginManager loginManager;
		return HearthstoneServices.TryGet<LoginManager>(out loginManager) && loginManager.IsFullLoginFlowComplete;
	}
}
