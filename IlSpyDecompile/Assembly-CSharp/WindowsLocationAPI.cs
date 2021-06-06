using System.Runtime.InteropServices;
using Microsoft.Win32;

public static class WindowsLocationAPI
{
	private const int MIN_4_5_NET_FRAMEWORK_VERSION_NUMBER = 378389;

	private static bool m_canUseLocationDll;

	private static bool m_isInitialized;

	public static void StartGeoSearch()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			ApiStartGeoSearch();
		}
	}

	public static double GetLatitude()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetLatitude();
		}
		return 0.0;
	}

	public static double GetLongitude()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetLongitude();
		}
		return 0.0;
	}

	public static double GetVerticalAccuracy()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetVerticalAccuracy();
		}
		return 0.0;
	}

	public static double GetHorizontalAccuracy()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetHorizontalAccuracy();
		}
		return 0.0;
	}

	public static bool GetEnabled()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetEnabled();
		}
		return false;
	}

	public static bool GetReady()
	{
		CheckInit();
		if (m_canUseLocationDll)
		{
			return ApiGetReady();
		}
		return false;
	}

	[DllImport("LocationAPI", EntryPoint = "StartGeoSearch")]
	private static extern void ApiStartGeoSearch();

	[DllImport("LocationAPI", EntryPoint = "GetLatitude")]
	private static extern double ApiGetLatitude();

	[DllImport("LocationAPI", EntryPoint = "GetLongitude")]
	private static extern double ApiGetLongitude();

	[DllImport("LocationAPI", EntryPoint = "GetVerticalAccuracy")]
	private static extern double ApiGetVerticalAccuracy();

	[DllImport("LocationAPI", EntryPoint = "GetHorizontalAccuracy")]
	private static extern double ApiGetHorizontalAccuracy();

	[DllImport("LocationAPI", EntryPoint = "GetEnabled")]
	private static extern bool ApiGetEnabled();

	[DllImport("LocationAPI", EntryPoint = "GetReady")]
	private static extern bool ApiGetReady();

	private static void Init()
	{
		m_canUseLocationDll = IsNetFramework4OrHigher();
		m_isInitialized = true;
	}

	private static void CheckInit()
	{
		if (!m_isInitialized)
		{
			Init();
		}
	}

	private static bool IsNetFramework4OrHigher()
	{
		RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full");
		if (registryKey != null)
		{
			return (int)(registryKey.GetValue("Release") ?? ((object)0)) >= 378389;
		}
		registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, "").OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\");
		if (registryKey != null)
		{
			string[] subKeyNames = registryKey.GetSubKeyNames();
			foreach (string name in subKeyNames)
			{
				if (GetVersionNumberFromKey(registryKey.OpenSubKey(name)) >= 4)
				{
					return true;
				}
			}
		}
		return false;
	}

	private static int GetVersionNumberFromKey(RegistryKey versionKey)
	{
		if (versionKey == null)
		{
			return -1;
		}
		int num = ParseVersionNumber(versionKey);
		string[] subKeyNames = versionKey.GetSubKeyNames();
		foreach (string name in subKeyNames)
		{
			RegistryKey registryKey = versionKey.OpenSubKey(name);
			if (registryKey != null)
			{
				int num2 = ParseVersionNumber(registryKey);
				if (num2 > num)
				{
					num = num2;
				}
			}
		}
		return num;
	}

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
}
