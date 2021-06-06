using System;
using System.Collections.Generic;
using Hearthstone;
using UnityEngine;

// Token: 0x020009D9 RID: 2521
public class PlatformSettings
{
	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x060088DC RID: 35036 RVA: 0x002C0E33 File Offset: 0x002BF033
	public static OSCategory OS
	{
		get
		{
			return PlatformSettings.s_os;
		}
	}

	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x060088DD RID: 35037 RVA: 0x000052EC File Offset: 0x000034EC
	public static OSCategory RuntimeOS
	{
		get
		{
			return OSCategory.PC;
		}
	}

	// Token: 0x1700079F RID: 1951
	// (get) Token: 0x060088DE RID: 35038 RVA: 0x000052EC File Offset: 0x000034EC
	public static LocaleVariant LocaleVariant
	{
		get
		{
			return LocaleVariant.Global;
		}
	}

	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x060088DF RID: 35039 RVA: 0x002C0E3A File Offset: 0x002BF03A
	public static MemoryCategory Memory
	{
		get
		{
			return PlatformSettings.s_memory;
		}
	}

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x060088E0 RID: 35040 RVA: 0x002C0E41 File Offset: 0x002BF041
	public static ScreenCategory Screen
	{
		get
		{
			return PlatformSettings.s_screen;
		}
	}

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x060088E1 RID: 35041 RVA: 0x002C0E48 File Offset: 0x002BF048
	public static InputCategory Input
	{
		get
		{
			return PlatformSettings.s_input;
		}
	}

	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x060088E2 RID: 35042 RVA: 0x002C0E4F File Offset: 0x002BF04F
	public static ScreenDensityCategory ScreenDensity
	{
		get
		{
			return PlatformSettings.s_screenDensity;
		}
	}

	// Token: 0x170007A4 RID: 1956
	// (get) Token: 0x060088E3 RID: 35043 RVA: 0x002C0E56 File Offset: 0x002BF056
	public static bool IsEmulating
	{
		get
		{
			return PlatformSettings.s_isEmulating;
		}
	}

	// Token: 0x170007A5 RID: 1957
	// (get) Token: 0x060088E4 RID: 35044 RVA: 0x002C0E5D File Offset: 0x002BF05D
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

	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x060088E5 RID: 35045 RVA: 0x002C0E76 File Offset: 0x002BF076
	public static string DeviceModel
	{
		get
		{
			return PlatformSettings.s_deviceModel ?? PlatformSettings.DeviceName;
		}
	}

	// Token: 0x170007A7 RID: 1959
	// (get) Token: 0x060088E6 RID: 35046 RVA: 0x002C0E88 File Offset: 0x002BF088
	public static bool ShouldFallbackToLowRes
	{
		get
		{
			if (Application.isEditor || PlatformSettings.IsEmulating)
			{
				return false;
			}
			if (PlatformSettings.Screen == ScreenCategory.Phone)
			{
				return true;
			}
			if (PlatformSettings.IsMobileRuntimeOS && GraphicsManager.Get() != null && GraphicsManager.Get().RenderQualityLevel == GraphicsQuality.Low)
			{
				return true;
			}
			if (PlatformSettings.RuntimeOS == OSCategory.iOS)
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

	// Token: 0x060088E7 RID: 35047 RVA: 0x002C0EF4 File Offset: 0x002BF0F4
	public static int GetBestScreenMatch(List<ScreenCategory> categories)
	{
		ScreenCategory screen = PlatformSettings.Screen;
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

	// Token: 0x060088E8 RID: 35048 RVA: 0x002C0F3B File Offset: 0x002BF13B
	public static bool IsMobile()
	{
		return PlatformSettings.OS == OSCategory.iOS || PlatformSettings.OS == OSCategory.Android;
	}

	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x060088E9 RID: 35049 RVA: 0x002C0F50 File Offset: 0x002BF150
	public static bool IsMobileRuntimeOS
	{
		get
		{
			OSCategory runtimeOS = PlatformSettings.RuntimeOS;
			return runtimeOS == OSCategory.iOS || runtimeOS == OSCategory.Android;
		}
	}

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x060088EA RID: 35050 RVA: 0x002C0F6D File Offset: 0x002BF16D
	public static bool IsTablet
	{
		get
		{
			return PlatformSettings.IsMobile() && (PlatformSettings.Screen == ScreenCategory.MiniTablet || PlatformSettings.Screen == ScreenCategory.Tablet);
		}
	}

	// Token: 0x060088EB RID: 35051 RVA: 0x002C0F8C File Offset: 0x002BF18C
	public static void RecomputeDeviceSettings()
	{
		if (PlatformSettings.EmulateMobileDevice())
		{
			return;
		}
		PlatformSettings.s_os = OSCategory.PC;
		PlatformSettings.s_input = InputCategory.Mouse;
		PlatformSettings.s_screen = ScreenCategory.PC;
		PlatformSettings.s_screenDensity = ScreenDensityCategory.High;
		PlatformSettings.s_os = OSCategory.PC;
		int systemMemorySize = SystemInfo.systemMemorySize;
		if (systemMemorySize < 500)
		{
			Debug.LogWarning("Low Memory Warning: Device has only " + systemMemorySize + "MBs of system memory");
			PlatformSettings.s_memory = MemoryCategory.Low;
			return;
		}
		if (systemMemorySize < 1000)
		{
			PlatformSettings.s_memory = MemoryCategory.Low;
			return;
		}
		if (systemMemorySize < 1500)
		{
			PlatformSettings.s_memory = MemoryCategory.Medium;
			return;
		}
		PlatformSettings.s_memory = MemoryCategory.High;
	}

	// Token: 0x060088EC RID: 35052 RVA: 0x002C1014 File Offset: 0x002BF214
	private static bool EmulateMobileDevice()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		ConfigFile configFile = new ConfigFile();
		if (!configFile.FullLoad(Vars.GetClientConfigPath()))
		{
			Debug.LogWarningFormat("Failed to read DeviceEmulation from {0}", new object[]
			{
				"client.config"
			});
			return false;
		}
		DevicePreset devicePreset = new DevicePreset();
		devicePreset.ReadFromConfig(configFile);
		if (devicePreset.name == "No Emulation")
		{
			return false;
		}
		if (!configFile.Get("Emulation.emulateOnDevice", false))
		{
			return false;
		}
		PlatformSettings.s_isEmulating = true;
		PlatformSettings.s_os = devicePreset.os;
		PlatformSettings.s_input = devicePreset.input;
		PlatformSettings.s_screen = devicePreset.screen;
		PlatformSettings.s_screenDensity = devicePreset.screenDensity;
		Log.DeviceEmulation.Print("Emulating an " + devicePreset.name, Array.Empty<object>());
		return true;
	}

	// Token: 0x060088ED RID: 35053 RVA: 0x002C10DA File Offset: 0x002BF2DA
	private static void SetAndroidSettings()
	{
		PlatformSettings.s_os = OSCategory.Android;
		PlatformSettings.s_input = InputCategory.Touch;
		if (SystemInfo.systemMemorySize < 1536)
		{
			PlatformSettings.s_isDeviceInMinSpec = false;
		}
	}

	// Token: 0x060088EE RID: 35054 RVA: 0x002C10FA File Offset: 0x002BF2FA
	public static void Refresh()
	{
		PlatformSettings.RecomputeDeviceSettings();
	}

	// Token: 0x0400730B RID: 29451
	private const int MIN_RAM_REQUIREMENT = 1536;

	// Token: 0x0400730C RID: 29452
	public static bool s_isDeviceSupported = true;

	// Token: 0x0400730D RID: 29453
	public static bool s_isDeviceInMinSpec = true;

	// Token: 0x0400730E RID: 29454
	public static OSCategory s_os = OSCategory.PC;

	// Token: 0x0400730F RID: 29455
	public static MemoryCategory s_memory = MemoryCategory.High;

	// Token: 0x04007310 RID: 29456
	public static ScreenCategory s_screen = ScreenCategory.PC;

	// Token: 0x04007311 RID: 29457
	public static InputCategory s_input = InputCategory.Mouse;

	// Token: 0x04007312 RID: 29458
	public static ScreenDensityCategory s_screenDensity = ScreenDensityCategory.High;

	// Token: 0x04007313 RID: 29459
	private static string s_deviceModel = null;

	// Token: 0x04007314 RID: 29460
	private static bool s_isEmulating = false;
}
