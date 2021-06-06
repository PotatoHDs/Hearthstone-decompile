using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone.Streaming;
using UnityEngine;

namespace Hearthstone.Core
{
	// Token: 0x02001088 RID: 4232
	public class ScriptableAssetManifest : IAssetManifest
	{
		// Token: 0x0600B6C9 RID: 46793 RVA: 0x0038038C File Offset: 0x0037E58C
		public static ScriptableAssetManifest Load()
		{
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(ScriptableAssetManifest.MainManifestBundleName);
			if (GameDownloadManagerProvider.Get() == null || !GameDownloadManagerProvider.Get().IsReadyToReadAssetManifest)
			{
				Log.Asset.PrintDebug("[ScriptableAssetManifest] Not ready to read AssetManifest '{0}', editor {1}, playing {2}", new object[]
				{
					ScriptableAssetManifest.MainManifestBundleName,
					Application.isEditor,
					Application.isPlaying
				});
				return null;
			}
			if (!File.Exists(assetBundlePath))
			{
				Log.Asset.PrintError("[ScriptableAssetManifest] Cannot find asset bundle for AssetManifest '{0}', editor {1}, playing {2}", new object[]
				{
					ScriptableAssetManifest.MainManifestBundleName,
					Application.isEditor,
					Application.isPlaying
				});
				return null;
			}
			AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
			if (assetBundle == null)
			{
				Log.Asset.PrintError("[ScriptableAssetManifest] Failed to open manifest bundle at {0}", new object[]
				{
					assetBundlePath
				});
				return null;
			}
			Log.Asset.PrintDebug("[ScriptableAssetManifest] Loaded AssetManifest bundle '{0}'", new object[]
			{
				assetBundle
			});
			ScriptableAssetManifest scriptableAssetManifest = ScriptableAssetManifest.CreateEmpty();
			scriptableAssetManifest.LoadTagsMetadata(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadBaseCatalog(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadQualityCatalogs(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadPlatformCatalogs(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadLocaleCatalogs();
			assetBundle.Unload(false);
			return scriptableAssetManifest;
		}

		// Token: 0x0600B6CA RID: 46794 RVA: 0x003804B8 File Offset: 0x0037E6B8
		private void LoadTagsMetadata(AssetBundle baseAssetBundle)
		{
			ScriptableAssetTagsMetadata scriptableAssetTagsMetadata = baseAssetBundle.LoadAsset<ScriptableAssetTagsMetadata>(ScriptableAssetManifest.TAGS_METADATA_ASSET_PATH);
			if (scriptableAssetTagsMetadata != null)
			{
				Log.Asset.PrintDebug("[ScriptableAssetManifest] Loaded Tags metadata", Array.Empty<object>());
				this.m_tagsMetadata = scriptableAssetTagsMetadata;
				return;
			}
			Error.AddDevFatal("[ScriptableAssetManifest] Failed to load tags metadata '{0}' in asset bundle '{1}'", new object[]
			{
				ScriptableAssetManifest.TAGS_METADATA_ASSET_PATH,
				baseAssetBundle.name
			});
		}

		// Token: 0x0600B6CB RID: 46795 RVA: 0x00380518 File Offset: 0x0037E718
		public string[] GetAllAssetBundleNames(Locale locale)
		{
			IEnumerable<string> allAssetBundleNames = this.m_assetsCatalog.GetAllAssetBundleNames();
			if (locale == Locale.UNKNOWN)
			{
				return allAssetBundleNames.ToArray<string>();
			}
			AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(locale);
			List<string> list = new List<string>();
			foreach (string text in allAssetBundleNames)
			{
				string[] array = text.Split(new char[]
				{
					'-'
				})[0].Split(new char[]
				{
					'_'
				});
				string text2 = array[array.Length - 1];
				text2 = text2.Substring(0, 2) + text2.Substring(2, 2).ToUpper();
				if (!Localization.IsValidLocaleName(text2) || text2.Equals(localeVariantTagForLocale.ToString()))
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600B6CC RID: 46796 RVA: 0x003805F8 File Offset: 0x0037E7F8
		public bool TryGetDirectBundleFromGuid(string guid, out string assetBundleName)
		{
			return this.m_assetsCatalog.TryGetAssetLocationFromGuid(guid, out assetBundleName);
		}

		// Token: 0x0600B6CD RID: 46797 RVA: 0x00380607 File Offset: 0x0037E807
		public string[] GetTagsFromAssetBundle(string assetBundleName)
		{
			return this.m_tagsMetadata.GetTagsFromAssetBundle(assetBundleName);
		}

		// Token: 0x0600B6CE RID: 46798 RVA: 0x00380615 File Offset: 0x0037E815
		public string[] GetAllTags(string tagGroup, bool excludeOverridenTag)
		{
			return this.m_tagsMetadata.GetAllTags(tagGroup, excludeOverridenTag);
		}

		// Token: 0x0600B6CF RID: 46799 RVA: 0x00380624 File Offset: 0x0037E824
		public bool TryResolveAsset(string guid, out string resolvedGuid, out string resolvedBundle, AssetVariantTags.Locale locale = AssetVariantTags.Locale.enUS, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, AssetVariantTags.Platform platform = AssetVariantTags.Platform.Any)
		{
			return this.m_assetsCatalog.TryResolveAsset(guid, out resolvedGuid, out resolvedBundle, locale, quality, platform);
		}

		// Token: 0x0600B6D0 RID: 46800 RVA: 0x0038063A File Offset: 0x0037E83A
		public string[] GetTagGroups()
		{
			return this.m_tagsMetadata.GetTagGroups();
		}

		// Token: 0x0600B6D1 RID: 46801 RVA: 0x00380647 File Offset: 0x0037E847
		public string[] GetTagsInTagGroup(string tagGroup)
		{
			return this.m_tagsMetadata.GetTagsInTagGroup(tagGroup);
		}

		// Token: 0x0600B6D2 RID: 46802 RVA: 0x00380655 File Offset: 0x0037E855
		public string[] GetTagsInTagGroup(int tagGroupId)
		{
			return this.m_tagsMetadata.GetTagsInTagGroup(tagGroupId);
		}

		// Token: 0x0600B6D3 RID: 46803 RVA: 0x00380663 File Offset: 0x0037E863
		public string ConvertToOverrideTag(string tag, string tagGroup)
		{
			return this.m_tagsMetadata.ConvertToOverrideTag(tag, tagGroup);
		}

		// Token: 0x0600B6D4 RID: 46804 RVA: 0x00380672 File Offset: 0x0037E872
		public string ConvertToOverrideTag(string tag, int tagGroupId)
		{
			return this.m_tagsMetadata.ConvertToOverrideTag(tag, tagGroupId);
		}

		// Token: 0x0600B6D5 RID: 46805 RVA: 0x00380681 File Offset: 0x0037E881
		public string GetTagGroupForTag(string tag)
		{
			return this.m_tagsMetadata.GetTagGroupForTag(tag);
		}

		// Token: 0x0600B6D6 RID: 46806 RVA: 0x0038068F File Offset: 0x0037E88F
		public void ReadLocaleCatalogs()
		{
			this.m_assetsCatalog.LoadLocaleCatalogs();
		}

		// Token: 0x0600B6D7 RID: 46807 RVA: 0x0038069C File Offset: 0x0037E89C
		public static ScriptableAssetManifest CreateEmpty()
		{
			return new ScriptableAssetManifest
			{
				m_assetsCatalog = new CompositeAssetCatalog(),
				m_tagsMetadata = ScriptableObject.CreateInstance<ScriptableAssetTagsMetadata>()
			};
		}

		// Token: 0x040097CB RID: 38859
		private static readonly string TAGS_METADATA_ASSET_PATH = "Assets/AssetManifest/tags_metadata.asset";

		// Token: 0x040097CC RID: 38860
		public static readonly string MainManifestBundleName = "asset_manifest.unity3d";

		// Token: 0x040097CD RID: 38861
		public static readonly string BundleDepsAssetPath = "Assets/AssetManifest/bundle_deps.asset";

		// Token: 0x040097CE RID: 38862
		private CompositeAssetCatalog m_assetsCatalog;

		// Token: 0x040097CF RID: 38863
		private ScriptableAssetTagsMetadata m_tagsMetadata;
	}
}
