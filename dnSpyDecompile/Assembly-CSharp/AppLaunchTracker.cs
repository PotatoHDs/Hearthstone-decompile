using System;
using Hearthstone.Attribution;

// Token: 0x0200074C RID: 1868
public static class AppLaunchTracker
{
	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x06006999 RID: 27033 RVA: 0x00226AB2 File Offset: 0x00224CB2
	// (set) Token: 0x0600699A RID: 27034 RVA: 0x00226AC1 File Offset: 0x00224CC1
	public static int LaunchCount
	{
		get
		{
			return Options.Get().GetInt(Option.LAUNCH_COUNT, 0);
		}
		private set
		{
			Options.Get().SetInt(Option.LAUNCH_COUNT, value);
		}
	}

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x0600699B RID: 27035 RVA: 0x00226AD0 File Offset: 0x00224CD0
	// (set) Token: 0x0600699C RID: 27036 RVA: 0x00226ADF File Offset: 0x00224CDF
	public static bool IsInstallReported
	{
		get
		{
			return Options.Get().GetBool(Option.IS_INSTALL_REPORTED, false);
		}
		set
		{
			Options.Get().SetBool(Option.IS_INSTALL_REPORTED, value);
		}
	}

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x0600699D RID: 27037 RVA: 0x00226AEE File Offset: 0x00224CEE
	public static ulong FirstInstallTimeMilliseconds
	{
		get
		{
			return Options.Get().GetULong(Option.FIRST_INSTALL_TIME, 0UL);
		}
	}

	// Token: 0x0600699E RID: 27038 RVA: 0x00226AFE File Offset: 0x00224CFE
	private static void SetInstallTimeIfNotSet()
	{
		if (!Options.Get().HasOption(Option.FIRST_INSTALL_TIME))
		{
			Options.Get().SetULong(Option.FIRST_INSTALL_TIME, TimeUtils.DateTimeToUnixTimeStampMilliseconds(DateTime.UtcNow));
		}
	}

	// Token: 0x0600699F RID: 27039 RVA: 0x00226B24 File Offset: 0x00224D24
	public static void TrackAppLaunch()
	{
		AppLaunchTracker.SetInstallTimeIfNotSet();
		AppLaunchTracker.LaunchCount++;
		if (!AppLaunchTracker.IsInstallReported)
		{
			if (AppLaunchTracker.FirstInstallTimeMilliseconds > 1572984000000UL)
			{
				BlizzardAttributionManager.Get().SendEvent_Install();
			}
			else
			{
				AppLaunchTracker.IsInstallReported = true;
			}
		}
		BlizzardAttributionManager.Get().SendEvent_Launch();
	}

	// Token: 0x04005663 RID: 22115
	private const ulong MILLISECONDS_15_6_RELEASE = 1572984000000UL;
}
