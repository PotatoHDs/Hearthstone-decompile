namespace bgs
{
	public interface ClientInterface
	{
		string GetVersion();

		string GetUserAgent();

		int GetApplicationVersion();

		string GetBasePersistentDataPath();

		string GetTemporaryCachePath();

		bool GetDisableConnectionMetering();

		constants.MobileEnv GetMobileEnvironment();

		string GetAuroraVersionName();

		string GetLocaleName();

		string GetPlatformName();

		constants.RuntimeEnvironment GetRuntimeEnvironment();

		IUrlDownloader GetUrlDownloader();

		int GetDataVersion();
	}
}
