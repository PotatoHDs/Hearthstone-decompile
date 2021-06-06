using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020007B3 RID: 1971
public class DbfShared
{
	// Token: 0x06006D3A RID: 27962 RVA: 0x00233CF5 File Offset: 0x00231EF5
	public static AssetBundle GetAssetBundle()
	{
		return DbfShared.s_assetBundle;
	}

	// Token: 0x06006D3B RID: 27963 RVA: 0x00233CFC File Offset: 0x00231EFC
	public static void LoadSharedAssetBundle()
	{
		string sharedDBFAssetBundlePath = DbfShared.GetSharedDBFAssetBundlePath();
		DbfShared.s_assetBundle = AssetBundle.LoadFromFile(sharedDBFAssetBundlePath);
		if (DbfShared.s_assetBundle == null)
		{
			Debug.LogErrorFormat("Failed to load DBF asset bundle from: \"{0}\"", new object[]
			{
				sharedDBFAssetBundlePath
			});
		}
	}

	// Token: 0x06006D3C RID: 27964 RVA: 0x00233D3B File Offset: 0x00231F3B
	public static IEnumerator<IAsyncJobResult> Job_LoadSharedDBFAssetBundle()
	{
		DbfShared.GetSharedDBFAssetBundlePath();
		LoadAssetBundleFromFile loadDBFSharedAssetBundle = new LoadAssetBundleFromFile(DbfShared.GetSharedDBFAssetBundlePath(), true);
		yield return loadDBFSharedAssetBundle;
		DbfShared.s_assetBundle = loadDBFSharedAssetBundle.LoadedAssetBundle;
		yield break;
	}

	// Token: 0x06006D3D RID: 27965 RVA: 0x00233D43 File Offset: 0x00231F43
	private static string GetSharedDBFAssetBundlePath()
	{
		if (AssetLoaderPrefs.AssetLoadingMethod == AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES)
		{
			return FileUtils.CreateLocalFilePath(string.Format("Data/{0}dbf.unity3d", AssetBundleInfo.BundlePathPlatformModifier()), true);
		}
		return "Assets/Game/DBF-Asset/dbf.unity3d";
	}

	// Token: 0x06006D3E RID: 27966 RVA: 0x00233D68 File Offset: 0x00231F68
	public static void Reset()
	{
		if (DbfShared.s_assetBundle != null)
		{
			DbfShared.s_assetBundle.Unload(true);
		}
		DbfShared.s_assetBundle = null;
	}

	// Token: 0x040057E7 RID: 22503
	private static AssetBundle s_assetBundle;
}
