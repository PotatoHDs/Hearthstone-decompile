using System.IO;
using Hearthstone.Core;
using UnityEngine;

public class AssetBundlesAliasedAssetResolver
{
	private ScriptableAssetMap m_cardsMap;

	public void Initialize()
	{
		LoadFromBundle();
	}

	public void Shutdown()
	{
		m_cardsMap = null;
	}

	public AssetReference GetCardDefAssetRefFromCardId(string cardId)
	{
		InitIfNeeded();
		return Resolve(cardId, m_cardsMap);
	}

	private static AssetReference Resolve(string alias, ScriptableAssetMap assetMap)
	{
		if (assetMap == null || assetMap.map == null)
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot resolve {0}. Missing map", alias);
			return null;
		}
		if (assetMap.map.TryGetValue(alias, out var value))
		{
			return AssetReference.CreateFromAssetString(value);
		}
		Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot resolve {0} among {1} entries", alias, assetMap.map.Count);
		return null;
	}

	private void InitIfNeeded()
	{
		if (m_cardsMap == null)
		{
			LoadFromBundle();
		}
	}

	public void LoadFromBundle()
	{
		string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(ScriptableAssetManifest.MainManifestBundleName);
		if (!File.Exists(assetBundlePath))
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Cannot find asset bundle for ScriptableAssetMaps '{0}', editor {1}, playing {2}", assetBundlePath, Application.isEditor, Application.isPlaying);
			return;
		}
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
		if (assetBundle == null)
		{
			Log.Asset.PrintError("[AssetBundlesAliasedAssetResolver] Failed to open manifest bundle at {0}", assetBundlePath);
			return;
		}
		m_cardsMap = assetBundle.LoadAsset<ScriptableAssetMap>("Assets/AssetManifest/AssetMaps/cards_map.asset");
		if (m_cardsMap == null)
		{
			Error.AddDevFatal("Failed to load cards map at {0} from {1}", "Assets/AssetManifest/AssetMaps/cards_map.asset", assetBundlePath);
		}
		assetBundle.Unload(unloadAllLoadedObjects: false);
	}
}
