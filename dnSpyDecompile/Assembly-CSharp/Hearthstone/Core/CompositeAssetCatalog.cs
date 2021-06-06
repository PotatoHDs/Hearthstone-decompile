using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x0200107E RID: 4222
	public class CompositeAssetCatalog
	{
		// Token: 0x0600B685 RID: 46725 RVA: 0x0037F95C File Offset: 0x0037DB5C
		public IEnumerable<string> GetAllAssetBundleNames()
		{
			return this.m_assetBundleNames;
		}

		// Token: 0x0600B686 RID: 46726 RVA: 0x0037F964 File Offset: 0x0037DB64
		public bool TryGetAssetLocationFromGuid(string guid, out string assetBundleName)
		{
			CompositeAssetCatalog.AssetInfo assetInfo;
			if (guid != null && this.m_guidToAsset.TryGetValue(guid, out assetInfo))
			{
				assetBundleName = assetInfo.Bundle;
				return true;
			}
			assetBundleName = null;
			return false;
		}

		// Token: 0x0600B687 RID: 46727 RVA: 0x0037F994 File Offset: 0x0037DB94
		public bool TryResolveAsset(string guid, out string assetGuid, out string assetBundleName, AssetVariantTags.Locale locale, AssetVariantTags.Quality quality, AssetVariantTags.Platform platform)
		{
			string text;
			string text2;
			if (!this.TryFindBaseAsset(guid, out text, out text2))
			{
				assetBundleName = null;
				assetGuid = null;
				return false;
			}
			string text3;
			string text4;
			if (this.TryGetQualityVariantLocationFromBaseGuid(text, quality, out text3, out text4))
			{
				text = text4;
				text2 = text3;
			}
			if (this.TryGetPlatformVariantLocationFromBaseGuid(text, platform, out text3, out text4))
			{
				text = text4;
				text2 = text3;
			}
			if (this.TryGetLocaleVariantLocationFromBaseGuid(text, locale, out text3, out text4))
			{
				text = text4;
				text2 = text3;
			}
			assetBundleName = text2;
			assetGuid = text;
			return true;
		}

		// Token: 0x0600B688 RID: 46728 RVA: 0x0037F9F8 File Offset: 0x0037DBF8
		public bool TryFindBaseAsset(string searchedGuid, out string baseGuid, out string baseBundle)
		{
			baseGuid = null;
			baseBundle = null;
			string key = searchedGuid;
			for (int i = 0; i < 5; i++)
			{
				CompositeAssetCatalog.AssetInfo assetInfo;
				if (!this.m_guidToAsset.TryGetValue(key, out assetInfo))
				{
					return false;
				}
				if (assetInfo.VariantInfo == null || assetInfo.VariantInfo.BaseAssetVariantInfo == null)
				{
					baseGuid = assetInfo.Guid;
					baseBundle = assetInfo.Bundle;
					return true;
				}
				key = assetInfo.VariantInfo.BaseAssetVariantInfo.Asset.Guid;
			}
			Error.AddDevFatal("Too many iterations looking for base asset of guid {0}. Probable base asset cycle", new object[]
			{
				searchedGuid
			});
			return false;
		}

		// Token: 0x0600B689 RID: 46729 RVA: 0x0037FA85 File Offset: 0x0037DC85
		public bool TryGetLocaleVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Locale locale, out string variantBundle, out string variantGuid)
		{
			return this.TryGetVariantLocationFromBaseGuid<AssetVariantTags.Locale>(baseAssetGuid, locale, new Func<CompositeAssetCatalog.VariantInfo, Dictionary<AssetVariantTags.Locale, CompositeAssetCatalog.VariantInfo>>(CompositeAssetCatalog.GetLocaleVariants), out variantBundle, out variantGuid);
		}

		// Token: 0x0600B68A RID: 46730 RVA: 0x0037FA9E File Offset: 0x0037DC9E
		public bool TryGetPlatformVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Platform platform, out string variantBundle, out string variantGuid)
		{
			return this.TryGetVariantLocationFromBaseGuid<AssetVariantTags.Platform>(baseAssetGuid, platform, new Func<CompositeAssetCatalog.VariantInfo, Dictionary<AssetVariantTags.Platform, CompositeAssetCatalog.VariantInfo>>(CompositeAssetCatalog.GetPlatformVariants), out variantBundle, out variantGuid);
		}

		// Token: 0x0600B68B RID: 46731 RVA: 0x0037FAB7 File Offset: 0x0037DCB7
		public bool TryGetQualityVariantLocationFromBaseGuid(string baseAssetGuid, AssetVariantTags.Quality quality, out string variantBundle, out string variantGuid)
		{
			return this.TryGetVariantLocationFromBaseGuid<AssetVariantTags.Quality>(baseAssetGuid, quality, new Func<CompositeAssetCatalog.VariantInfo, Dictionary<AssetVariantTags.Quality, CompositeAssetCatalog.VariantInfo>>(CompositeAssetCatalog.GetQualityVariants), out variantBundle, out variantGuid);
		}

		// Token: 0x0600B68C RID: 46732 RVA: 0x0037FAD0 File Offset: 0x0037DCD0
		private static Dictionary<AssetVariantTags.Locale, CompositeAssetCatalog.VariantInfo> GetLocaleVariants(CompositeAssetCatalog.VariantInfo variantInfo)
		{
			if (variantInfo == null)
			{
				return null;
			}
			return variantInfo.LocaleVariants;
		}

		// Token: 0x0600B68D RID: 46733 RVA: 0x0037FADD File Offset: 0x0037DCDD
		private static Dictionary<AssetVariantTags.Platform, CompositeAssetCatalog.VariantInfo> GetPlatformVariants(CompositeAssetCatalog.VariantInfo variantInfo)
		{
			if (variantInfo == null)
			{
				return null;
			}
			return variantInfo.PlatformVariants;
		}

		// Token: 0x0600B68E RID: 46734 RVA: 0x0037FAEA File Offset: 0x0037DCEA
		private static Dictionary<AssetVariantTags.Quality, CompositeAssetCatalog.VariantInfo> GetQualityVariants(CompositeAssetCatalog.VariantInfo variantInfo)
		{
			if (variantInfo == null)
			{
				return null;
			}
			return variantInfo.QualityVariants;
		}

		// Token: 0x0600B68F RID: 46735 RVA: 0x0037FAF8 File Offset: 0x0037DCF8
		private bool TryGetVariantLocationFromBaseGuid<T>(string baseAssetGuid, T variantKey, Func<CompositeAssetCatalog.VariantInfo, Dictionary<T, CompositeAssetCatalog.VariantInfo>> variantsGetter, out string variantBundle, out string variantGuid)
		{
			CompositeAssetCatalog.AssetInfo assetInfo;
			if (this.m_guidToAsset.TryGetValue(baseAssetGuid, out assetInfo))
			{
				Dictionary<T, CompositeAssetCatalog.VariantInfo> dictionary = variantsGetter(assetInfo.VariantInfo);
				CompositeAssetCatalog.VariantInfo variantInfo;
				if (dictionary != null && dictionary.TryGetValue(variantKey, out variantInfo))
				{
					variantGuid = variantInfo.Asset.Guid;
					variantBundle = variantInfo.Asset.Bundle;
					return true;
				}
			}
			variantGuid = null;
			variantBundle = null;
			return false;
		}

		// Token: 0x0600B690 RID: 46736 RVA: 0x0037FB58 File Offset: 0x0037DD58
		public void LoadBaseCatalog(AssetBundle baseAssetBundle)
		{
			string text = CompositeAssetCatalog.BaseCatalogAssetPath();
			ScriptableAssetCatalog scriptableAssetCatalog = baseAssetBundle.LoadAsset<ScriptableAssetCatalog>(text);
			if (scriptableAssetCatalog != null)
			{
				Log.Asset.PrintDebug("Loaded base catalog {0}", new object[]
				{
					text
				});
				this.AddAssetsFromCatalog<BaseAssetCatalogItem>(scriptableAssetCatalog.m_assets, scriptableAssetCatalog.m_bundleNames);
				return;
			}
			Error.AddDevFatal("Failed to load base catalog '{0}' in bundle '{1}'", new object[]
			{
				text,
				baseAssetBundle.name
			});
		}

		// Token: 0x0600B691 RID: 46737 RVA: 0x0037FBC8 File Offset: 0x0037DDC8
		public void LoadQualityCatalogs(AssetBundle baseAssetBundle)
		{
			foreach (object obj in Enum.GetValues(typeof(AssetVariantTags.Quality)))
			{
				AssetVariantTags.Quality quality = (AssetVariantTags.Quality)obj;
				string text = CompositeAssetCatalog.QualityCatalogAssetPath(quality);
				ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = baseAssetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text);
				if (scriptableAssetVariantCatalog != null)
				{
					Log.Asset.PrintDebug("Loaded quality catalog {0}", new object[]
					{
						text
					});
					this.AddAssetsFromCatalog<VariantAssetCatalogItem>(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
					this.AddVariantsFromCatalog<AssetVariantTags.Quality>(scriptableAssetVariantCatalog.m_assets, quality, new Action<CompositeAssetCatalog.VariantInfo, CompositeAssetCatalog.VariantInfo, AssetVariantTags.Quality>(this.LinkQualityVariant));
				}
			}
		}

		// Token: 0x0600B692 RID: 46738 RVA: 0x0037FC84 File Offset: 0x0037DE84
		public void LoadPlatformCatalogs(AssetBundle baseAssetBundle)
		{
			foreach (object obj in Enum.GetValues(typeof(AssetVariantTags.Platform)))
			{
				AssetVariantTags.Platform platform = (AssetVariantTags.Platform)obj;
				string text = CompositeAssetCatalog.PlatformCatalogAssetPath(platform);
				ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = baseAssetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text);
				if (scriptableAssetVariantCatalog != null)
				{
					Log.Asset.PrintDebug("Loaded platform catalog {0}", new object[]
					{
						text
					});
					this.AddAssetsFromCatalog<VariantAssetCatalogItem>(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
					this.AddVariantsFromCatalog<AssetVariantTags.Platform>(scriptableAssetVariantCatalog.m_assets, platform, new Action<CompositeAssetCatalog.VariantInfo, CompositeAssetCatalog.VariantInfo, AssetVariantTags.Platform>(this.LinkPlatformVariant));
				}
			}
		}

		// Token: 0x0600B693 RID: 46739 RVA: 0x0037FD40 File Offset: 0x0037DF40
		public void LoadLocaleCatalogs()
		{
			AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(Localization.GetLocale());
			if (this.m_loadedLocale.Contains(localeVariantTagForLocale))
			{
				Log.Asset.PrintInfo("Skip to load asset catalog which is already loaded: {0}", new object[]
				{
					localeVariantTagForLocale
				});
				return;
			}
			string text = CompositeAssetCatalog.LocaleCatalogBundleName(localeVariantTagForLocale);
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(text);
			if (!File.Exists(assetBundlePath))
			{
				Log.Asset.PrintWarning("Locale catalog bundle {0} not found", new object[]
				{
					text
				});
				return;
			}
			AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
			if (assetBundle != null)
			{
				string text2 = CompositeAssetCatalog.LocaleCatalogAssetPath(localeVariantTagForLocale);
				ScriptableAssetVariantCatalog scriptableAssetVariantCatalog = assetBundle.LoadAsset<ScriptableAssetVariantCatalog>(text2);
				if (scriptableAssetVariantCatalog != null)
				{
					Log.Asset.PrintDebug("Loaded locale catalog {0}", new object[]
					{
						text2
					});
					this.AddAssetsFromCatalog<VariantAssetCatalogItem>(scriptableAssetVariantCatalog.m_assets, scriptableAssetVariantCatalog.m_bundleNames);
					this.AddVariantsFromCatalog<AssetVariantTags.Locale>(scriptableAssetVariantCatalog.m_assets, localeVariantTagForLocale, new Action<CompositeAssetCatalog.VariantInfo, CompositeAssetCatalog.VariantInfo, AssetVariantTags.Locale>(this.LinkLocaleVariant));
				}
				else
				{
					Error.AddDevFatal("Failed to load locale catalog '{0}' in asset bundle '{1}'", new object[]
					{
						text2,
						assetBundlePath
					});
				}
				assetBundle.Unload(false);
				this.m_loadedLocale.Add(localeVariantTagForLocale);
				return;
			}
			Error.AddDevFatal("Failed to load catalog bundle at {0}", new object[]
			{
				assetBundlePath
			});
		}

		// Token: 0x0600B694 RID: 46740 RVA: 0x0037FE74 File Offset: 0x0037E074
		private void AddAssetsFromCatalog<T>(List<T> assets, List<string> bundleNames) where T : BaseAssetCatalogItem
		{
			for (int i = 0; i < assets.Count; i++)
			{
				T t = assets[i];
				if (t.bundleId >= 0 && t.bundleId < bundleNames.Count)
				{
					CompositeAssetCatalog.AssetInfo assetInfo;
					this.TryAddOrUpdateAsset(t.guid, bundleNames[t.bundleId], out assetInfo);
				}
				else
				{
					Error.AddDevFatal("Bundle id {0} out of bounds for {1}", new object[]
					{
						t.bundleId,
						t.guid
					});
				}
			}
		}

		// Token: 0x0600B695 RID: 46741 RVA: 0x0037FF1C File Offset: 0x0037E11C
		private void AddVariantsFromCatalog<T>(List<VariantAssetCatalogItem> variants, T variantKey, Action<CompositeAssetCatalog.VariantInfo, CompositeAssetCatalog.VariantInfo, T> linkAction)
		{
			for (int i = 0; i < variants.Count; i++)
			{
				VariantAssetCatalogItem variantAssetCatalogItem = variants[i];
				this.TryAddVariant<T>(variantAssetCatalogItem.baseGuid, variantAssetCatalogItem.guid, variantKey, linkAction);
			}
		}

		// Token: 0x0600B696 RID: 46742 RVA: 0x0037FF58 File Offset: 0x0037E158
		private bool TryAddVariant<T>(string baseGuid, string variantGuid, T variantKey, Action<CompositeAssetCatalog.VariantInfo, CompositeAssetCatalog.VariantInfo, T> linkAction)
		{
			CompositeAssetCatalog.AssetInfo asset;
			CompositeAssetCatalog.AssetInfo asset2;
			if (!this.TryAddOrUpdateAsset(baseGuid, null, out asset) || !this.TryAddOrUpdateAsset(variantGuid, null, out asset2))
			{
				return false;
			}
			CompositeAssetCatalog.VariantInfo orCreateVariantInfo = this.GetOrCreateVariantInfo(asset);
			CompositeAssetCatalog.VariantInfo orCreateVariantInfo2 = this.GetOrCreateVariantInfo(asset2);
			orCreateVariantInfo2.BaseAssetVariantInfo = orCreateVariantInfo;
			linkAction(orCreateVariantInfo, orCreateVariantInfo2, variantKey);
			return true;
		}

		// Token: 0x0600B697 RID: 46743 RVA: 0x0037FFA4 File Offset: 0x0037E1A4
		private void LinkLocaleVariant(CompositeAssetCatalog.VariantInfo baseInfo, CompositeAssetCatalog.VariantInfo variant, AssetVariantTags.Locale locale)
		{
			try
			{
				if (baseInfo.LocaleVariants == null)
				{
					baseInfo.LocaleVariants = new Dictionary<AssetVariantTags.Locale, CompositeAssetCatalog.VariantInfo>();
				}
				baseInfo.LocaleVariants.Add(locale, variant);
			}
			catch (Exception arg)
			{
				Log.Asset.PrintError("Failed to run LinkLocaleVariant: " + arg, Array.Empty<object>());
			}
		}

		// Token: 0x0600B698 RID: 46744 RVA: 0x00380000 File Offset: 0x0037E200
		private void LinkQualityVariant(CompositeAssetCatalog.VariantInfo baseInfo, CompositeAssetCatalog.VariantInfo variant, AssetVariantTags.Quality quality)
		{
			if (baseInfo.QualityVariants == null)
			{
				baseInfo.QualityVariants = new Dictionary<AssetVariantTags.Quality, CompositeAssetCatalog.VariantInfo>();
			}
			baseInfo.QualityVariants.Add(quality, variant);
		}

		// Token: 0x0600B699 RID: 46745 RVA: 0x00380022 File Offset: 0x0037E222
		private void LinkPlatformVariant(CompositeAssetCatalog.VariantInfo baseInfo, CompositeAssetCatalog.VariantInfo variant, AssetVariantTags.Platform platform)
		{
			if (baseInfo.PlatformVariants == null)
			{
				baseInfo.PlatformVariants = new Dictionary<AssetVariantTags.Platform, CompositeAssetCatalog.VariantInfo>();
			}
			baseInfo.PlatformVariants.Add(platform, variant);
		}

		// Token: 0x0600B69A RID: 46746 RVA: 0x00380044 File Offset: 0x0037E244
		private CompositeAssetCatalog.VariantInfo GetOrCreateVariantInfo(CompositeAssetCatalog.AssetInfo asset)
		{
			if (asset.VariantInfo == null)
			{
				CompositeAssetCatalog.VariantInfo variantInfo = new CompositeAssetCatalog.VariantInfo();
				asset.VariantInfo = variantInfo;
				variantInfo.Asset = asset;
			}
			return asset.VariantInfo;
		}

		// Token: 0x0600B69B RID: 46747 RVA: 0x00380074 File Offset: 0x0037E274
		private bool TryAddOrUpdateAsset(string guid, string bundleName, out CompositeAssetCatalog.AssetInfo updatedAsset)
		{
			if (string.IsNullOrEmpty(guid))
			{
				Error.AddDevFatal("AddOrUpdateAsset: guid is required", Array.Empty<object>());
				updatedAsset = null;
				return false;
			}
			if (!this.m_guidToAsset.TryGetValue(guid, out updatedAsset))
			{
				updatedAsset = new CompositeAssetCatalog.AssetInfo
				{
					Guid = guid
				};
				this.m_guidToAsset[guid] = updatedAsset;
				this.m_assets.Add(updatedAsset);
			}
			if (!string.IsNullOrEmpty(bundleName))
			{
				updatedAsset.Bundle = bundleName;
				this.m_assetBundleNames.Add(bundleName);
			}
			return true;
		}

		// Token: 0x0600B69C RID: 46748 RVA: 0x003800F3 File Offset: 0x0037E2F3
		private static string LocaleCatalogBundleName(AssetVariantTags.Locale locale)
		{
			return string.Format("asset_manifest_{0}.unity3d", locale.ToString().ToLower());
		}

		// Token: 0x0600B69D RID: 46749 RVA: 0x00380111 File Offset: 0x0037E311
		private static string BaseCatalogAssetPath()
		{
			return "Assets/AssetManifest/base_assets_catalog.asset";
		}

		// Token: 0x0600B69E RID: 46750 RVA: 0x00380118 File Offset: 0x0037E318
		private static string QualityCatalogAssetPath(AssetVariantTags.Quality quality)
		{
			return string.Format("Assets/AssetManifest/asset_catalog_quality_{0}.asset", quality.ToString().ToLower());
		}

		// Token: 0x0600B69F RID: 46751 RVA: 0x00380136 File Offset: 0x0037E336
		private static string PlatformCatalogAssetPath(AssetVariantTags.Platform platform)
		{
			return string.Format("Assets/AssetManifest/asset_catalog_platform_{0}.asset", platform.ToString().ToLower());
		}

		// Token: 0x0600B6A0 RID: 46752 RVA: 0x00380154 File Offset: 0x0037E354
		private static string LocaleCatalogAssetPath(AssetVariantTags.Locale locale)
		{
			return string.Format("Assets/AssetManifest/asset_catalog_locale_{0}.asset", locale.ToString().ToLower());
		}

		// Token: 0x040097B7 RID: 38839
		private readonly List<CompositeAssetCatalog.AssetInfo> m_assets = new List<CompositeAssetCatalog.AssetInfo>();

		// Token: 0x040097B8 RID: 38840
		private readonly HashSet<string> m_assetBundleNames = new HashSet<string>();

		// Token: 0x040097B9 RID: 38841
		private readonly Dictionary<string, CompositeAssetCatalog.AssetInfo> m_guidToAsset = new Dictionary<string, CompositeAssetCatalog.AssetInfo>();

		// Token: 0x040097BA RID: 38842
		private readonly List<AssetVariantTags.Locale> m_loadedLocale = new List<AssetVariantTags.Locale>();

		// Token: 0x02002887 RID: 10375
		private class AssetInfo
		{
			// Token: 0x0400F9DF RID: 63967
			public string Guid;

			// Token: 0x0400F9E0 RID: 63968
			public string Bundle;

			// Token: 0x0400F9E1 RID: 63969
			public CompositeAssetCatalog.VariantInfo VariantInfo;
		}

		// Token: 0x02002888 RID: 10376
		private class VariantInfo
		{
			// Token: 0x0400F9E2 RID: 63970
			public CompositeAssetCatalog.AssetInfo Asset;

			// Token: 0x0400F9E3 RID: 63971
			public CompositeAssetCatalog.VariantInfo BaseAssetVariantInfo;

			// Token: 0x0400F9E4 RID: 63972
			public Dictionary<AssetVariantTags.Platform, CompositeAssetCatalog.VariantInfo> PlatformVariants;

			// Token: 0x0400F9E5 RID: 63973
			public Dictionary<AssetVariantTags.Locale, CompositeAssetCatalog.VariantInfo> LocaleVariants;

			// Token: 0x0400F9E6 RID: 63974
			public Dictionary<AssetVariantTags.Quality, CompositeAssetCatalog.VariantInfo> QualityVariants;
		}
	}
}
