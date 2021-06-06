using System;
using System.IO;
using Hearthstone.Core;
using UnityEngine;

// Token: 0x0200084D RID: 2125
public class AssetBundlesAliasedAssetResolver
{
	// Token: 0x06007325 RID: 29477 RVA: 0x00250AED File Offset: 0x0024ECED
	public void Initialize()
	{
		this.LoadFromBundle();
	}

	// Token: 0x06007326 RID: 29478 RVA: 0x00250AF5 File Offset: 0x0024ECF5
	public void Shutdown()
	{
		this.m_cardsMap = null;
	}

	// Token: 0x06007327 RID: 29479 RVA: 0x00250AFE File Offset: 0x0024ECFE
	public AssetReference GetCardDefAssetRefFromCardId(string cardId)
	{
		this.InitIfNeeded();
		return AssetBundlesAliasedAssetResolver.Resolve(cardId, this.m_cardsMap);
	}

	// Token: 0x06007328 RID: 29480 RVA: 0x00250B14 File Offset: 0x0024ED14
	private static AssetReference Resolve(string alias, ScriptableAssetMap assetMap)
	{
		if (assetMap == null || assetMap.map == null)
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot resolve {0}. Missing map", new object[]
			{
				alias
			});
			return null;
		}
		string assetString;
		if (assetMap.map.TryGetValue(alias, out assetString))
		{
			return AssetReference.CreateFromAssetString(assetString);
		}
		Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot resolve {0} among {1} entries", new object[]
		{
			alias,
			assetMap.map.Count
		});
		return null;
	}

	// Token: 0x06007329 RID: 29481 RVA: 0x00250B91 File Offset: 0x0024ED91
	private void InitIfNeeded()
	{
		if (this.m_cardsMap == null)
		{
			this.LoadFromBundle();
		}
	}

	// Token: 0x0600732A RID: 29482 RVA: 0x00250BA8 File Offset: 0x0024EDA8
	public void LoadFromBundle()
	{
		string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(ScriptableAssetManifest.MainManifestBundleName);
		if (!File.Exists(assetBundlePath))
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot find asset bundle for ScriptableAssetMaps '{0}', editor {1}, playing {2}", new object[]
			{
				assetBundlePath,
				Application.isEditor,
				Application.isPlaying
			});
			return;
		}
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
		if (assetBundle == null)
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Failed to open manifest bundle at {0}", new object[]
			{
				assetBundlePath
			});
			return;
		}
		this.m_cardsMap = assetBundle.LoadAsset<ScriptableAssetMap>("Assets/AssetManifest/AssetMaps/cards_map.asset");
		if (this.m_cardsMap == null)
		{
			Error.AddDevFatal("Failed to load cards map at {0} from {1}", new object[]
			{
				"Assets/AssetManifest/AssetMaps/cards_map.asset",
				assetBundlePath
			});
		}
		assetBundle.Unload(false);
	}

	// Token: 0x04005BB9 RID: 23481
	private ScriptableAssetMap m_cardsMap;
}
