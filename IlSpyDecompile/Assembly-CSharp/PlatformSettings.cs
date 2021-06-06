using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

public class PlatformSettings
{
	private const int MIN_RAM_REQUIREMENT = 1536;

	public static bool s_isDeviceSupported = true;

	public static bool s_isDeviceInMinSpec = true;

	public static OSCategory s_os = OSCategory.PC;

	public static MemoryCategory s_memory = MemoryCategory.High;

	public static ScreenCategory s_screen = ScreenCategory.PC;

	public static InputCategory s_input = InputCategory.Mouse;

	public static ScreenDensityCategory s_screenDensity = ScreenDensityCategory.High;

	private static string s_deviceModel = null;

	private static bool s_isEmulating = false;

	public static OSCategory OS => s_os;

	public static OSCategory RuntimeOS => OSCategory.PC;

	public static LocaleVariant LocaleVariant => LocaleVariant.Global;

	public static MemoryCategory Memory => s_memory;

	public static ScreenCategory Screen => s_screen;

	public static InputCategory Input => s_input;

	public static ScreenDensityCategory ScreenDensity => s_screenDensity;

	public static bool IsEmulating => s_isEmulating;

	public static string DeviceName
	{
		get
		{
			if (string.IsNullOrEmpty(SystemInfo.deviceModel))
			{
				return "unknown";
			}
			return SystemInfo.deviceModel;
		}
	}

	public static string DeviceModel => s_deviceModel ?? DeviceName;

	public static bool ShouldFallbackToLowRes
	{
		get
		{
			if (Application.isEditor || IsEmulating)
			{
				return false;
			}
			if (Screen == ScreenCategory.Phone)
			{
				return true;
			}
			if (IsMobileRuntimeOS && GraphicsManager.Get() != null && GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
			{
				return true;
			}
			if (RuntimeOS == OSCategory.iOS)
			{
				NetCache netCache = NetCache.Get();
				if (netCache != null)
				{
					NetCache.NetCacheFeatures netObject = netCache.GetNetObject<NetCache.NetCacheFeatures>();
					if (netObject != null && netObject.ForceIosLowRes)
					{
						return true;
					}
				}
			}
			return false;
		}
	}

	public static bool IsMobileRuntimeOS
	{
		get
		{
			OSCategory runtimeOS = RuntimeOS;
			if (runtimeOS != OSCategory.iOS)
			{
				return runtimeOS == OSCategory.Android;
			}
			return true;
		}
	}

	public static bool IsTablet
	{
		get
		{
			if (IsMobile())
			{
				if (Screen != ScreenCategory.MiniTablet)
				{
					return Screen == ScreenCategory.Tablet;
				}
				return true;
			}
			return false;
		}
	}

	public static int GetBestScreenMatch(List<ScreenCategory> categories)
	{
		ScreenCategory screen = Screen;
		int result = 0;
		int num = int.MaxValue;
		for (int i = 0; i < categories.Count; i++)
		{
			int num2 = categories[i] - screen;
			if (num2 >= 0 && num2 < num)
			{
				result = i;
				num = num2;
			}
		}
		return result;
	}

	public static bool IsMobile()
	{
		if (OS != OSCategory.iOS)
		{
			return OS == OSCategory.Android;
		}
		return true;
	}

	public static void RecomputeDeviceSettings()
	{
		if (!EmulateMobileDevice())
		{
			s_os = OSCategory.PC;
			s_input = InputCategory.Mouse;
			s_screen = ScreenCategory.PC;
			s_screenDensity = ScreenDensityCategory.High;
			s_os = OSCategory.PC;
			int systemMemorySize = SystemInfo.systemMemorySize;
			if (systemMemorySize < 500)
			{
				Debug.LogWarning("Low Memory Warning: Device has only " + systemMemorySize + "MBs of system memory");
				s_memory = MemoryCategory.Low;
			}
			else if (systemMemorySize < 1000)
			{
				s_memory = MemoryCategory.Low;
			}
			else if (systemMemorySize < 1500)
			{
				s_memory = MemoryCategory.Medium;
			}
			else
			{
				s_memory = MemoryCategory.High;
			}
		}
	}

	private static bool EmulateMobileDevice()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		ConfigFile configFile = new ConfigFile();
		if (!configFile.FullLoad(Vars.GetClientConfigPath()))
		{
			Debug.LogWarningFormat("Failed to read DeviceEmulation from {0}", "client.config");
			return false;
		}
		DevicePreset devicePreset = new DevicePreset();
		devicePreset.ReadFromConfig(configFile);
		if (devicePreset.name == "No Emulation")
		{
			return false;
		}
		if (!configFile.Get("Emulation.emulateOnDevice", defaultVal: false))
		{
			return false;
		}
		s_isEmulating = true;
		s_os = devicePreset.os;
		s_input = devicePreset.input;
		s_screen = devicePreset.screen;
		s_screenDensity = devicePreset.screenDensity;
		Log.DeviceEmulation.Print("Emulating an " + devicePreset.name);
		return true;
	}

	private static void SetAndroidSettings()
	{
		s_os = OSCategory.Android;
		s_input = InputCategory.Touch;
		if (SystemInfo.systemMemorySize < 1536)
		{
			s_isDeviceInMinSpec = false;
		}
	}

	public static void Refresh()
	{
		RecomputeDeviceSettings();
	}
}
