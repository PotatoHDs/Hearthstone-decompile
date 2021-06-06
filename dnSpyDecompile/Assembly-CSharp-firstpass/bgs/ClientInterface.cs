using System;

namespace bgs
{
	// Token: 0x02000268 RID: 616
	public interface ClientInterface
	{
		// Token: 0x06002577 RID: 9591
		string GetVersion();

		// Token: 0x06002578 RID: 9592
		string GetUserAgent();

		// Token: 0x06002579 RID: 9593
		int GetApplicationVersion();

		// Token: 0x0600257A RID: 9594
		string GetBasePersistentDataPath();

		// Token: 0x0600257B RID: 9595
		string GetTemporaryCachePath();

		// Token: 0x0600257C RID: 9596
		bool GetDisableConnectionMetering();

		// Token: 0x0600257D RID: 9597
		constants.MobileEnv GetMobileEnvironment();

		// Token: 0x0600257E RID: 9598
		string GetAuroraVersionName();

		// Token: 0x0600257F RID: 9599
		string GetLocaleName();

		// Token: 0x06002580 RID: 9600
		string GetPlatformName();

		// Token: 0x06002581 RID: 9601
		constants.RuntimeEnvironment GetRuntimeEnvironment();

		// Token: 0x06002582 RID: 9602
		IUrlDownloader GetUrlDownloader();

		// Token: 0x06002583 RID: 9603
		int GetDataVersion();
	}
}
