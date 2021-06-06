using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

// Token: 0x020007B0 RID: 1968
public static class WindowsLocationAPI
{
	// Token: 0x06006D24 RID: 27940 RVA: 0x002339A7 File Offset: 0x00231BA7
	public static void StartGeoSearch()
	{
		WindowsLocationAPI.CheckInit();
		if (WindowsLocationAPI.m_canUseLocationDll)
		{
			WindowsLocationAPI.ApiStartGeoSearch();
		}
	}

	// Token: 0x06006D25 RID: 27941 RVA: 0x002339BA File Offset: 0x00231BBA
	public static double GetLatitude()
	{
		WindowsLocationAPI.CheckInit();
		if (WindowsLocationAPI.m_canUseLocationDll)
		{
			return WindowsLocationAPI.ApiGetLatitude();
		}
		return 0.0;
	}

	// Token: 0x06006D26 RID: 27942 RVA: 0x002339D7 File Offset: 0x00231BD7
	public static double GetLongitude()
	{
		WindowsLocationAPI.CheckInit();
		if (WindowsLocationAPI.m_canUseLocationDll)
		{
			return WindowsLocationAPI.ApiGetLongitude();
		}
		return 0.0;
	}

	// Token: 0x06006D27 RID: 27943 RVA: 0x002339F4 File Offset: 0x00231BF4
	public static double GetVerticalAccuracy()
	{
		WindowsLocationAPI.CheckInit();
		if (WindowsLocationAPI.m_canUseLocationDll)
		{
			return WindowsLocationAPI.ApiGetVerticalAccuracy();
		}
		return 0.0;
	}

	// Token: 0x06006D28 RID: 27944 RVA: 0x00233A11 File Offset: 0x00231C11
	public static double GetHorizontalAccuracy()
	{
		WindowsLocationAPI.CheckInit();
		if (WindowsLocationAPI.m_canUseLocationDll)
		{
			return WindowsLocationAPI.ApiGetHorizontalAccuracy();
		}
		return 0.0;
	}

	// Token: 0x06006D29 RID: 27945 RVA: 0x00233A2E File Offset: 0x00231C2E
	public static bool GetEnabled()
	{
		WindowsLocationAPI.CheckInit();
		return WindowsLocationAPI.m_canUseLocationDll && WindowsLocationAPI.ApiGetEnabled();
	}

	// Token: 0x06006D2A RID: 27946 RVA: 0x00233A43 File Offset: 0x00231C43
	public static bool GetReady()
	{
		WindowsLocationAPI.CheckInit();
		return WindowsLocationAPI.m_canUseLocationDll && WindowsLocationAPI.ApiGetReady();
	}

	// Token: 0x06006D2B RID: 27947
	[DllImport("LocationAPI", EntryPoint = "StartGeoSearch")]
	private static extern void ApiStartGeoSearch();

	// Token: 0x06006D2C RID: 27948
	[DllImport("LocationAPI", EntryPoint = "GetLatitude")]
	private static extern double ApiGetLatitude();

	// Token: 0x06006D2D RID: 27949
	[DllImport("LocationAPI", EntryPoint = "GetLongitude")]
	private static extern double ApiGetLongitude();

	// Token: 0x06006D2E RID: 27950
	[DllImport("LocationAPI", EntryPoint = "GetVerticalAccuracy")]
	private static extern double ApiGetVerticalAccuracy();

	// Token: 0x06006D2F RID: 27951
	[DllImport("LocationAPI", EntryPoint = "GetHorizontalAccuracy")]
	private static extern double ApiGetHorizontalAccuracy();

	// Token: 0x06006D30 RID: 27952
	[DllImport("LocationAPI", EntryPoint = "GetEnabled")]
	private static extern bool ApiGetEnabled();

	// Token: 0x06006D31 RID: 27953
	[DllImport("LocationAPI", EntryPoint = "GetReady")]
	private static extern bool ApiGetReady();

	// Token: 0x06006D32 RID: 27954 RVA: 0x00233A58 File Offset: 0x00231C58
	private static void Init()
	{
		WindowsLocationAPI.m_canUseLocationDll = WindowsLocationAPI.IsNetFramework4OrHigher();
		WindowsLocationAPI.m_isInitialized = true;
	}

	// Token: 0x06006D33 RID: 27955 RVA: 0x00233A6A File Offset: 0x00231C6A
	private static void CheckInit()
	{
		if (!WindowsLocationAPI.m_isInitialized)
		{
			WindowsLocationAPI.Init();
		}
	}

	// Token: 0x06006D34 RID: 27956 RVA: 0x00233A78 File Offset: 0x00231C78
	private static bool IsNetFramework4OrHigher()
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");
		if (registryKey != null)
		{
			return (int)(registryKey.GetValue("Release") ?? 0) >= 378389;
		}
		registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\");
		if (registryKey != null)
		{
			foreach (string name in registryKey.GetSubKeyNames())
			{
				if (WindowsLocationAPI.GetVersionNumberFromKey(registryKey.OpenSubKey(name)) >= 4)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06006D35 RID: 27957 RVA: 0x00233B08 File Offset: 0x00231D08
	private static int GetVersionNumberFromKey(RegistryKey versionKey)
	{
		if (versionKey == null)
		{
			return -1;
		}
		int num = WindowsLocationAPI.ParseVersionNumber(versionKey);
		foreach (string name in versionKey.GetSubKeyNames())
		{
			RegistryKey registryKey = versionKey.OpenSubKey(name);
			if (registryKey != null)
			{
				int num2 = WindowsLocationAPI.ParseVersionNumber(registryKey);
				if (num2 > num)
				{
					num = num2;
				}
			}
		}
		return num;
	}

	// Token: 0x06006D36 RID: 27958 RVA: 0x00233B5C File Offset: 0x00231D5C
	private static int ParseVersionNumber(RegistryKey versionKey)
	{
		int result = -1;
		string text = (string)versionKey.GetValue("Version", "");
		if (text != "")
		{
			int.TryParse(text.Substring(0, text.IndexOf('.')), out result);
			return result;
		}
		return result;
	}

	// Token: 0x040057E1 RID: 22497
	private const int MIN_4_5_NET_FRAMEWORK_VERSION_NUMBER = 378389;

	// Token: 0x040057E2 RID: 22498
	private static bool m_canUseLocationDll;

	// Token: 0x040057E3 RID: 22499
	private static bool m_isInitialized;
}
