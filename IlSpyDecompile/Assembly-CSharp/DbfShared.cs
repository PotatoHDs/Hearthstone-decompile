using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class DbfShared
{
	private static AssetBundle s_assetBundle;

	public static AssetBundle GetAssetBundle()
	{
		return s_assetBundle;
	}

	public static void LoadSharedAssetBundle()
	{
		string sharedDBFAssetBundlePath = GetSharedDBFAssetBundlePath();
		s_assetBundle = AssetBundle.LoadFromFile(sharedDBFAssetBundlePath);
		if (s_assetBundle == null)
		{
			Debug.LogErrorFormat("Failed to load DBF asset bundle from: \"{0}\"", sharedDBFAssetBundlePath);
		}
	}

	public static IEnumerator<IAsyncJobResult> Job_LoadSharedDBFAssetBundle()
	{
		GetSharedDBFAssetBundlePath();
		LoadAssetBundleFromFile loadDBFSharedAssetBundle = new LoadAssetBundleFromFile(GetSharedDBFAssetBundlePath(), failOnError: true);
		yield return loadDBFSharedAssetBundle;
		s_assetBundle = loadDBFSharedAssetBundle.LoadedAssetBundle;
	}

	private static string GetSharedDBFAssetBundlePath()
	{
		if (AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			return FileUtils.CreateLocalFilePath($"Data/{AssetBundleInfo.BundlePathPlatformModifier()}dbf.unity3d");
		}
		return "Assets/Game/DBF-Asset/dbf.unity3d";
	}

	public static void Reset()
	{
		if (s_assetBundle != null)
		{
			s_assetBundle.Unload(unloadAllLoadedObjects: true);
		}
		s_assetBundle = null;
	}
}
