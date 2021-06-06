using System;
using Blizzard.T5.Jobs;

// Token: 0x020006E0 RID: 1760
public class WaitForSplashScreen : IJobDependency, IAsyncJobResult
{
	// Token: 0x06006242 RID: 25154 RVA: 0x00200F01 File Offset: 0x001FF101
	public bool IsReady()
	{
		return SplashScreen.Get() != null;
	}
}
