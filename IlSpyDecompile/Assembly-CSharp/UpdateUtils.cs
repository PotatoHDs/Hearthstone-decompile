using System.Collections.Generic;
using System.IO;
using bgs;
using UnityEngine;

public class UpdateUtils
{
	private static readonly Map<AndroidStore, string> s_androidStoreUrls = new Map<AndroidStore, string>
	{
		{
			AndroidStore.AMAZON,
			"http://www.amazon.com/gp/mas/dl/android?p=com.blizzard.wtcg.hearthstone"
		},
		{
			AndroidStore.GOOGLE,
			"https://play.google.com/store/apps/details?id=com.blizzard.wtcg.hearthstone"
		},
		{
			AndroidStore.ONE_STORE,
			"https://onesto.re/OA00752154"
		}
	};

	public static bool AreUpdatesEnabledForCurrentPlatform
	{
		get
		{
			if (PlatformSettings.IsMobileRuntimeOS)
			{
				return !DemoMgr.Get().IsDemo();
			}
			return false;
		}
	}

	public static bool addSkipBackupAttributeToItemAtPath(string path)
	{
		return true;
	}

	public static void ShowWirelessSettings()
	{
	}

	public static void ResizeListIfNeeded(List<string> list, int minSize)
	{
		if (list.Capacity < minSize)
		{
			list.Capacity = minSize;
		}
	}

	public static string GetLocaleFromAssetBundle(string assetBundleName)
	{
		string[] array = assetBundleName.Split('-')[0].Split('_');
		if (array.Length != 0)
		{
			string text = array[array.Length - 1];
			text = text.Substring(0, 2) + text.Substring(2, 2).ToUpper();
			if (Localization.IsValidLocaleName(text))
			{
				return text;
			}
		}
		return string.Empty;
	}

	public static void OpenAppStore()
	{
		AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
		PlatformDependentValue<string> platformDependentValue = new PlatformDependentValue<string>(PlatformCategory.OS)
		{
			iOS = "https://itunes.apple.com/app/hearthstone-heroes-warcraft/id625257520?ls=1&mt=8",
			Android = GetAndroidStoreUrl(androidStore)
		};
		PlatformDependentValue<string> platformDependentValue2 = new PlatformDependentValue<string>(PlatformCategory.OS)
		{
			iOS = "https://itunes.apple.com/cn/app/lu-shi-chuan-shuo-mo-shou/id841140063?ls=1&mt=8",
			Android = ((androidStore == AndroidStore.HUAWEI) ? "https://a.vmall.com/order/app?appId=C101669777&pkgName=com.blizzard.wtcg.hearthstone.huawei" : "https://www.battlenet.com.cn/account/download/hearthstone/android?style=hearthstone")
		};
		if (MobileDeviceLocale.GetCurrentRegionId() == constants.BnetRegion.REGION_CN)
		{
			platformDependentValue = platformDependentValue2;
		}
		Application.OpenURL(platformDependentValue);
	}

	private static string GetAndroidStoreUrl(AndroidStore store)
	{
		if (s_androidStoreUrls.TryGetValue(store, out var value))
		{
			return value;
		}
		return string.Empty;
	}

	public static void CleanupOldAssetBundles(string dataPath)
	{
		Log.Downloader.PrintInfo("Try to delete the old asset bundles");
		DirectoryInfo directoryInfo = new DirectoryInfo(dataPath);
		if (!directoryInfo.Exists)
		{
			return;
		}
		FileInfo[] files = directoryInfo.GetFiles("*.unity3d", SearchOption.AllDirectories);
		foreach (FileInfo fileInfo in files)
		{
			if ((!fileInfo.Name.Contains("_") && !fileInfo.Name.Equals("dbf.unity3d")) || fileInfo.Name.StartsWith("rad_"))
			{
				fileInfo.Delete();
				Log.Downloader.PrintInfo("Deleted {0}", fileInfo.Name);
			}
		}
	}
}
