namespace Hearthstone.Login
{
	internal static class LoginEnvironmentUtil
	{
		public static string OverrideEnvironmentIfNeeded(string url)
		{
			if (!IsEnvironmentOverriden())
			{
				return url;
			}
			return GetQAEnvironmentForUrl(url);
		}

		public static string GetQAEnvironmentForUrl(string url)
		{
			return url.Replace("-live-", "-qa-");
		}

		public static bool IsEnvironmentOverriden()
		{
			if (HearthstoneApplication.IsPublic())
			{
				return false;
			}
			ConfigFile configFile = new ConfigFile();
			if (!configFile.FullLoad(Vars.GetClientConfigPath()))
			{
				Log.Login.PrintWarning("Failed to read config from {0}", "client.config");
				return false;
			}
			return configFile.Get("Aurora.UseQALogin", defaultVal: true);
		}
	}
}
