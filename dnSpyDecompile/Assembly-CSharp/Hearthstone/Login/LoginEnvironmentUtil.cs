using System;

namespace Hearthstone.Login
{
	// Token: 0x0200113A RID: 4410
	internal static class LoginEnvironmentUtil
	{
		// Token: 0x0600C12D RID: 49453 RVA: 0x003AB6D5 File Offset: 0x003A98D5
		public static string OverrideEnvironmentIfNeeded(string url)
		{
			if (!LoginEnvironmentUtil.IsEnvironmentOverriden())
			{
				return url;
			}
			return LoginEnvironmentUtil.GetQAEnvironmentForUrl(url);
		}

		// Token: 0x0600C12E RID: 49454 RVA: 0x003AB6E6 File Offset: 0x003A98E6
		public static string GetQAEnvironmentForUrl(string url)
		{
			return url.Replace("-live-", "-qa-");
		}

		// Token: 0x0600C12F RID: 49455 RVA: 0x003AB6F8 File Offset: 0x003A98F8
		public static bool IsEnvironmentOverriden()
		{
			if (HearthstoneApplication.IsPublic())
			{
				return false;
			}
			ConfigFile configFile = new ConfigFile();
			if (!configFile.FullLoad(Vars.GetClientConfigPath()))
			{
				Log.Login.PrintWarning("Failed to read config from {0}", new object[]
				{
					"client.config"
				});
				return false;
			}
			return configFile.Get("Aurora.UseQALogin", true);
		}
	}
}
