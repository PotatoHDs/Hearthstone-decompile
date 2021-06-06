using System;
using Hearthstone.Attribution;

public static class AppLaunchTracker
{
	private const ulong MILLISECONDS_15_6_RELEASE = 1572984000000uL;

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

	public static bool IsInstallReported
	{
		get
		{
			return Options.Get().GetBool(Option.IS_INSTALL_REPORTED, defaultVal: false);
		}
		set
		{
			Options.Get().SetBool(Option.IS_INSTALL_REPORTED, value);
		}
	}

	public static ulong FirstInstallTimeMilliseconds => Options.Get().GetULong(Option.FIRST_INSTALL_TIME, 0uL);

	private static void SetInstallTimeIfNotSet()
	{
		if (!Options.Get().HasOption(Option.FIRST_INSTALL_TIME))
		{
			Options.Get().SetULong(Option.FIRST_INSTALL_TIME, TimeUtils.DateTimeToUnixTimeStampMilliseconds(DateTime.UtcNow));
		}
	}

	public static void TrackAppLaunch()
	{
		SetInstallTimeIfNotSet();
		LaunchCount++;
		if (!IsInstallReported)
		{
			if (FirstInstallTimeMilliseconds > 1572984000000L)
			{
				BlizzardAttributionManager.Get().SendEvent_Install();
			}
			else
			{
				IsInstallReported = true;
			}
		}
		BlizzardAttributionManager.Get().SendEvent_Launch();
	}
}
