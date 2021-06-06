using System;
using System.Collections.Generic;
using System.IO;
using bgs;
using UnityEngine;

// Token: 0x020009FE RID: 2558
public class UpdateUtils
{
	// Token: 0x06008AD1 RID: 35537 RVA: 0x000052EC File Offset: 0x000034EC
	public static bool addSkipBackupAttributeToItemAtPath(string path)
	{
		return true;
	}

	// Token: 0x06008AD2 RID: 35538 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void ShowWirelessSettings()
	{
	}

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x06008AD3 RID: 35539 RVA: 0x002C7048 File Offset: 0x002C5248
	public static bool AreUpdatesEnabledForCurrentPlatform
	{
		get
		{
			return PlatformSettings.IsMobileRuntimeOS && !DemoMgr.Get().IsDemo();
		}
	}

	// Token: 0x06008AD4 RID: 35540 RVA: 0x002C7060 File Offset: 0x002C5260
	public static void ResizeListIfNeeded(List<string> list, int minSize)
	{
		if (list.Capacity < minSize)
		{
			list.Capacity = minSize;
		}
	}

	// Token: 0x06008AD5 RID: 35541 RVA: 0x002C7074 File Offset: 0x002C5274
	public static string GetLocaleFromAssetBundle(string assetBundleName)
	{
		string[] array = assetBundleName.Split(new char[]
		{
			'-'
		})[0].Split(new char[]
		{
			'_'
		});
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

	// Token: 0x06008AD6 RID: 35542 RVA: 0x002C70DC File Offset: 0x002C52DC
	public static void OpenAppStore()
	{
		AndroidStore androidStore = AndroidDeviceSettings.Get().GetAndroidStore();
		PlatformDependentValue<string> val = new PlatformDependentValue<string>(PlatformCategory.OS)
		{
			iOS = "https://itunes.apple.com/app/hearthstone-heroes-warcraft/id625257520?ls=1&mt=8",
			Android = UpdateUtils.GetAndroidStoreUrl(androidStore)
		};
		PlatformDependentValue<string> platformDependentValue = new PlatformDependentValue<string>(PlatformCategory.OS)
		{
			iOS = "https://itunes.apple.com/cn/app/lu-shi-chuan-shuo-mo-shou/id841140063?ls=1&mt=8",
			Android = ((androidStore == AndroidStore.HUAWEI) ? "https://a.vmall.com/order/app?appId=C101669777&pkgName=com.blizzard.wtcg.hearthstone.huawei" : "https://www.battlenet.com.cn/account/download/hearthstone/android?style=hearthstone")
		};
		if (MobileDeviceLocale.GetCurrentRegionId() == constants.BnetRegion.REGION_CN)
		{
			val = platformDependentValue;
		}
		Application.OpenURL(val);
	}

	// Token: 0x06008AD7 RID: 35543 RVA: 0x002C7150 File Offset: 0x002C5350
	private static string GetAndroidStoreUrl(AndroidStore store)
	{
		string result;
		if (UpdateUtils.s_androidStoreUrls.TryGetValue(store, out result))
		{
			return result;
		}
		return string.Empty;
	}

	// Token: 0x06008AD8 RID: 35544 RVA: 0x002C7174 File Offset: 0x002C5374
	public static void CleanupOldAssetBundles(string dataPath)
	{
		global::Log.Downloader.PrintInfo("Try to delete the old asset bundles", Array.Empty<object>());
		DirectoryInfo directoryInfo = new DirectoryInfo(dataPath);
		if (!directoryInfo.Exists)
		{
			return;
		}
		foreach (FileInfo fileInfo in directoryInfo.GetFiles("*.unity3d", SearchOption.AllDirectories))
		{
			if ((!fileInfo.Name.Contains("_") && !fileInfo.Name.Equals("dbf.unity3d")) || fileInfo.Name.StartsWith("rad_"))
			{
				fileInfo.Delete();
				global::Log.Downloader.PrintInfo("Deleted {0}", new object[]
				{
					fileInfo.Name
				});
			}
		}
	}

	// Token: 0x04007389 RID: 29577
	private static readonly global::Map<AndroidStore, string> s_androidStoreUrls = new global::Map<AndroidStore, string>
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
}
