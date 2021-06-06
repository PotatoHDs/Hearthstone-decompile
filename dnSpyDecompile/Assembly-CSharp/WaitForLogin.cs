using System;
using Blizzard.T5.Jobs;

// Token: 0x0200036E RID: 878
public class WaitForLogin : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600339F RID: 13215 RVA: 0x0010920D File Offset: 0x0010740D
	public bool IsReady()
	{
		return Network.IsLoggedIn();
	}
}
