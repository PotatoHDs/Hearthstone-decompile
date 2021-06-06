using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hearthstone.Core
{
	public class CompositeAssetCatalog
	{
		private class AssetInfo
		{
			public string Guid;

			public string Bundle;

			public VariantInfo VariantInfo;
		}

		private class VariantInfo
		{
			public AssetInfo Asset;

			public VariantInfo BaseAssetVariantInfo;

			public Dictionary<AssetVariantTags.Platform, VariantInfo> PlatformVariants;

			public Dictionary<AssetVariantTags.Locale, VariantInfo> LocaleVariants;

			public Dictionary<AssetVariantTags.Quality, VariantInfo> QualityVariants;
		}

		private readonly List<AssetInfo> m_assets = new List<AssetInfo>();

		private readonly HashSet<string> m_assetBundleNames = new HashSet<string>();

		private readonly Dictionary<string, AssetInfo> m_guidToAsset = new Dictionary<string, AssetInfo>();

		private readonly List<AssetVariantTags.Locale> m_loadedLocale = new List<AssetVariantTags.Locale>();

		public IEnumerable<string> GetAllAssetBundleNames()
		{
			return m_assetBundleNames;
		}

		public bool TryGetAssetLocationFromGuid(string guid, out string assetBundleName)
		{
			if (guid != null && m_guidToAsset.TryGetValue(guid, out var value))
			{
				assetBundleName = value.Bundle;
				return true;
			}
			assetBundleName = null;
			return false;
		}

		public bool TryResolveAsset(string guid, out string assetGuid, out string assetBundleName, AssetVariantTags.Locale locale, AssetVariantTags.Quality quality, AssetVariantTags.Platform platform)
		{
			if (!TryFindBaseAsset(guid, out var baseGuid, out var baseBundle))
			{
				assetBundleName = null;
				assetGuid = null;
				return false;
			}
			if (TryGetQualityVariantLocationFromBaseGuid(baseGuid, quality, out var variantBundle, out var variantGuid))
			{
				baseGuid = variantGuid;
				baseBundle = variantBundle;
			}
			if (TryGetPlatformVariantLocationFromBaseGuid(baseGuid, platform, out variantBundle, out variantGuid))
			{
				baseGuid = variantGuid;
				baseBundle = variantBundle;
			}
			if (TryGetLocaleVariantLocationFromBaseGuid(baseGuid, locale, out variantBundle, out variantGuid))
			{
				baseGuid = variantGuid;
				baseBundle = variantBundle;
			}
			assetBundleName = baseBundle;
			assetGuid = baseGuid;
			return true;
		}

		public bool TryFindBaseAsset(string searchedGuid, out string baseGuid, out string baseBundle)
		{
			baseGuid = null;
			baseBundle = null;
			string key = searchedGuid;
			for (int i = 0; i < 5; i++)
			{
				if (m_guidToAsset.TryGetValue(key, out var value))
				{
					if (value.VariantInfo != null && value.VariantInfo.BaseAssetVariantInfo != null)
					{
						key = value.VariantInfo.BaseAssetVariantInfo.Asset.Guid;
						continue;
					}
					baseGuid = value.Guid;
					baseBundle = value.Bundle;
					return true;
				}
				return false;
			}
			Error.AddDevFatal("Too many iterations looking for base asset of guid {0}. Probable base asset cycle", searchedGuid);
			return false;
		}

		public bool TryGetLocaleVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Locale locale, out string variantBundle, out string variantGuid)
		{
			return TryGetVariantLocationFromBaseGuid(baseAssetGuid, locale, GetLocaleVariants, out variantBundle, out variantGuid);
		}

		public bool TryGetPlatformVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Platform platform, out string variantBundle, out string variantGuid)
		{
			return TryGetVariantLocationFromBaseGuid(baseAssetGuid, platform, GetPlatformVariants, out variantBundle, out variantGuid);
		}

		public bool TryGetQualityVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Quality quality, out string variantBundle, out string variantGuid)
		{
			return TryGetVariantLocationFromBaseGuid(baseAssetGuid, quality, GetQualityVariants, out variantBundle, out variantGuid);
		}

		private static Dictionary<AssetVariantTags.Locale, VariantInfo> GetLocaleVariants(VariantInfo variantInfo)
		{
			return variantInfo?.LocaleVariants;
		}

		private static Dictionary<AssetVariantTags.Platform, VariantInfo> GetPlatformVariants(VariantInfo variantInfo)
		{
			return variantInfo?.PlatformVariants;
		}

		private static Dictionary<AssetVariantTags.Quality, VariantInfo> GetQualityVariants(VariantInfo variantInfo)
		{
			return variantInfo?.QualityVariants;
		}

		private bool TryGetVariantLocationFromBaseGuid<T>(string baseAssetGuid, T variantKey, Func<VariantInfo, Dictionary<T, VariantInfo>> variantsGetter, out string variantBundle, out string variantGuid)
		{
			if (m_guidToAsset.TryGetValue(baseAssetGuid, out var value))
			{
				Dictionary<T, VariantInfo> dictionary = variantsGetter(value.VariantInfo);
				if (dictionary != null && dictionary.TryGetValue(variantKey, out var value2))
				{
					variantGuid = value2.Asset.Guid;
					variantBundle = value2.Asset.Bundle;
					return true;
				}
			}
			variantGuid = null;
			variantBundle = null;
			return false;
		}

		public void LoadBaseCatalog(AssetBundle baseAssetBundle)
		{
			string text = BaseCatalogAssetPath();
			ScriptableAssetCatalog scriptableAssetCatalog = baseAssetBundle.LoadAsset<ScriptableAssetCatalog>(text);
			if (scriptableAssetCatalog != null)
			{
				Log.Asset.PrintDebug("Loaded base catalog {0}", text);
				AddAssetsFromCatalog(scriptableAssetCatalog.m_assets, scriptableAssetCatalog.m_bundleNames);
			}
			else
			{
				Error.AddDevFatal("Failed to load base catalog '{0}' in bundle '{1}'", text, baseAssetBundle.name);
			}
		}

		public void LoadQualityCatalogs(AssetBundle baseAssetBundle)
		{
			foreach (AssetVariantTags.Quality value in Enum.GetValues(typeof(AssetVariantTags.Quality)))
			{
				string text = QualityCatalogAssetPath(value);
				ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = baseAssetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text);
				if (scriptableAssetVariantCatalog != null)
				{
					Log.Asset.PrintDebug("Loaded quality catalog {0}", text);
					AddAssetsFromCatalog(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
					AddVariantsFromCatalog(scriptableAssetVariantCatalog.m_assets, value, LinkQualityVariant);
				}
			}
		}

		public void LoadPlatformCatalogs(AssetBundle baseAssetBundle)
		{
			foreach (AssetVariantTags.Platform value in Enum.GetValues(typeof(AssetVariantTags.Platform)))
			{
				string text = PlatformCatalogAssetPath(value);
				ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = baseAssetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text);
				if (scriptableAssetVariantCatalog != null)
				{
					Log.Asset.PrintDebug("Loaded platform catalog {0}", text);
					AddAssetsFromCatalog(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
					AddVariantsFromCatalog(scriptableAssetVariantCatalog.m_assets, value, LinkPlatformVariant);
				}
			}
		}

		public void LoadLocaleCatalogs()
		{
			AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(Localization.GetLocale());
			if (m_loadedLocale.Contains(localeVariantTagForLocale))
			{
				Log.Asset.PrintInfo("Skip to load asset catalog which is already loaded: {0}", localeVariantTagForLocale);
				return;
			}
			string text = LocaleCatalogBundleName(localeVariantTagForLocale);
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(text);
			if (File.Exists(assetBundlePath))
			{
				AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
				if (assetBundle != null)
				{
					string text2 = LocaleCatalogAssetPath(localeVariantTagForLocale);
					ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = assetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text2);
					if (scriptableAssetVariantCatalog != null)
					{
						Log.Asset.PrintDebug("Loaded locale catalog {0}", text2);
						AddAssetsFromCatalog(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
						AddVariantsFromCatalog(scriptableAssetVariantCatalog.m_assets, localeVariantTagForLocale, LinkLocaleVariant);
					}
					else
					{
						Error.AddDevFatal("Failed to load locale catalog '{0}' in asset bundle '{1}'", text2, assetBundlePath);
					}
					assetBundle.Unload(unloadAllLoadedObjects: false);
					m_loadedLocale.Add(localeVariantTagForLocale);
				}
				else
				{
					Error.AddDevFatal("Failed to load catalog bundle at {0}", assetBundlePath);
				}
			}
			else
			{
				Log.Asset.PrintWarning("Locale catalog bundle {0} not found", text);
			}
		}

		private void AddAssetsFromCatalog<T>(List<T> assets, List<string> bundleNames) where T : BaseAssetCatalogItem
		{
			for (int i = 0; i < assets.Count; i++)
			{
				T val = assets[i];
				if (val.bundleId >= 0 && val.bundleId < bundleNames.Count)
				{
					TryAddOrUpdateAsset(val.guid, bundleNames[val.bundleId], out var _);
					continue;
				}
				Error.AddDevFatal("Bundle id {0} out of bounds for {1}", val.bundleId, val.guid);
			}
		}

		private void AddVariantsFromCatalog<T>(List<VariantAssetCatalogItem> variants, T variantKey, Action<VariantInfo, VariantInfo, T> linkAction)
		{
			for (int i = 0; i < variants.Count; i++)
			{
				VariantAssetCatalogItem variantAssetCatalogItem = variants[i];
				TryAddVariant(variantAssetCatalogItem.baseGuid, variantAssetCatalogItem.guid, variantKey, linkAction);
			}
		}

		private bool TryAddVariant<T>(string baseGuid, string variantGuid, T variantKey, Action<VariantInfo, VariantInfo, T> linkAction)
		{
			if (!TryAddOrUpdateAsset(baseGuid, null, out var updatedAsset) || !TryAddOrUpdateAsset(variantGuid, null, out var updatedAsset2))
			{
				return false;
			}
			VariantInfo orCreateVariantInfo = GetOrCreateVariantInfo(updatedAsset);
			VariantInfo orCreateVariantInfo2 = GetOrCreateVariantInfo(updatedAsset2);
			orCreateVariantInfo2.BaseAssetVariantInfo = orCreateVariantInfo;
			linkAction(orCreateVariantInfo, orCreateVariantInfo2, variantKey);
			return true;
		}

		private void LinkLocaleVariant(VariantInfo baseInfo, VariantInfo variant, AssetVariantTags.Locale locale)
		{
			try
			{
				if (baseInfo.LocaleVariants == null)
				{
					baseInfo.LocaleVariants = new Dictionary<AssetVariantTags.Locale, VariantInfo>();
				}
				baseInfo.LocaleVariants.Add(locale, variant);
			}
			catch (Exception ex)
			{
				Log.Asset.PrintError("Failed to run LinkLocaleVariant: " + ex);
			}
		}

		private void LinkQualityVariant(VariantInfo baseInfo, VariantInfo variant, AssetVariantTags.Quality quality)
		{
			if (baseInfo.QualityVariants == null)
			{
				baseInfo.QualityVariants = new Dictionary<AssetVariantTags.Quality, VariantInfo>();
			}
			baseInfo.QualityVariants.Add(quality, variant);
		}

		private void LinkPlatformVariant(VariantInfo baseInfo, VariantInfo variant, AssetVariantTags.Platform platform)
		{
			if (baseInfo.PlatformVariants == null)
			{
				baseInfo.PlatformVariants = new Dictionary<AssetVariantTags.Platform, VariantInfo>();
			}
			baseInfo.PlatformVariants.Add(platform, variant);
		}

		private VariantInfo GetOrCreateVariantInfo(AssetInfo asset)
		{
			if (asset.VariantInfo == null)
			{
				(asset.VariantInfo = new VariantInfo()).Asset = asset;
			}
			return asset.VariantInfo;
		}

		private bool TryAddOrUpdateAsset(string guid, string bundleName, out AssetInfo updatedAsset)
		{
			if (string.IsNullOrEmpty(guid))
			{
				Error.AddDevFatal("AddOrUpdateAsset: guid is required");
				updatedAsset = null;
				return false;
			}
			if (!m_guidToAsset.TryGetValue(guid, out updatedAsset))
			{
				updatedAsset = new AssetInfo
				{
					Guid = guid
				};
				m_guidToAsset[guid] = updatedAsset;
				m_assets.Add(updatedAsset);
			}
			if (!string.IsNullOrEmpty(bundleName))
			{
				updatedAsset.Bundle = bundleName;
				m_assetBundleNames.Add(bundleName);
			}
			return true;
		}

		private static string LocaleCatalogBundleName(AssetVariantTags.Locale locale)
		{
			return $"asset_manifest_{locale.ToString().ToLower()}.unity3d";
		}

		private static string BaseCatalogAssetPath()
		{
			return "Assets/AssetManifest/base_assets_catalog.asset";
		}

		private static string QualityCatalogAssetPath(AssetVariantTags.Quality quality)
		{
			return $"Assets/AssetManifest/asset_catalog_quality_{quality.ToString().ToLower()}.asset";
		}

		private static string PlatformCatalogAssetPath(AssetVariantTags.Platform platform)
		{
			return $"Assets/AssetManifest/asset_catalog_platform_{platform.ToString().ToLower()}.asset";
		}

		private static string LocaleCatalogAssetPath(AssetVariantTags.Locale locale)
		{
			return $"Assets/AssetManifest/asset_catalog_locale_{locale.ToString().ToLower()}.asset";
		}
	}
}
