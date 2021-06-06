using System;
using Blizzard.T5.Jobs;

// Token: 0x020006E1 RID: 1761
public class WaitForLogoAnimation : IJobDependency, IAsyncJobResult
{
	// Token: 0x06006244 RID: 25156 RVA: 0x00200F0E File Offset: 0x001FF10E
	public bool IsReady()
	{
		return LogoAnimation.Get() != null;
	}
}
