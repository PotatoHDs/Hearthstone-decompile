using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hearthstone.Streaming;
using UnityEngine;

namespace Hearthstone.Core
{
	public class ScriptableAssetManifest : IAssetManifest
	{
		private static readonly string TAGS_METADATA_ASSET_PATH = "Assets/AssetManifest/tags_metadata.asset";

		public static readonly string MainManifestBundleName = "asset_manifest.unity3d";

		public static readonly string BundleDepsAssetPath = "Assets/AssetManifest/bundle_deps.asset";

		private CompositeAssetCatalog m_assetsCatalog;

		private ScriptableAssetTagsMetadata m_tagsMetadata;

		public static ScriptableAssetManifest Load()
		{
			string assetBundlePath = AssetBundleInfo.GetAssetBundlePath(MainManifestBundleName);
			if (GameDownloadManagerProvider.Get() == null || !GameDownloadManagerProvider.Get().IsReadyToReadAssetManifest)
			{
				Log.Asset.PrintDebug("[ScriptableAssetManifest] Not ready to read AssetManifest '{0}', editor {1}, playing {2}", MainManifestBundleName, Application.isEditor, Application.isPlaying);
				return null;
			}
			if (!File.Exists(assetBundlePath))
			{
				Log.Asset.PrintError("[ScriptableAssetManifest] Cannot find asset bundle for AssetManifest '{0}', editor {1}, playing {2}", MainManifestBundleName, Application.isEditor, Application.isPlaying);
				return null;
			}
			AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);
			if (assetBundle == null)
			{
				Log.Asset.PrintError("[ScriptableAssetManifest] Failed to open manifest bundle at {0}", assetBundlePath);
				return null;
			}
			Log.Asset.PrintDebug("[ScriptableAssetManifest] Loaded AssetManifest bundle '{0}'", assetBundle);
			ScriptableAssetManifest scriptableAssetManifest = CreateEmpty();
			scriptableAssetManifest.LoadTagsMetadata(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadBaseCatalog(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadQualityCatalogs(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadPlatformCatalogs(assetBundle);
			scriptableAssetManifest.m_assetsCatalog.LoadLocaleCatalogs();
			assetBundle.Unload(unloadAllLoadedObjects: false);
			return scriptableAssetManifest;
		}

		private void LoadTagsMetadata(AssetBundle baseAssetBundle)
		{
			ScriptableAssetTagsMetadata scriptableAssetTagsMetadata = baseAssetBundle.LoadAsset<ScriptableAssetTagsMetadata>(TAGS_METADATA_ASSET_PATH);
			if (scriptableAssetTagsMetadata != null)
			{
				Log.Asset.PrintDebug("[ScriptableAssetManifest] Loaded Tags metadata");
				m_tagsMetadata = scriptableAssetTagsMetadata;
			}
			else
			{
				Error.AddDevFatal("[ScriptableAssetManifest] Failed to load tags metadata '{0}' in asset bundle '{1}'", TAGS_METADATA_ASSET_PATH, baseAssetBundle.name);
			}
		}

		public string[] GetAllAssetBundleNames(Locale locale)
		{
			IEnumerable<string> allAssetBundleNames = m_assetsCatalog.GetAllAssetBundleNames();
			if (locale == Locale.UNKNOWN)
			{
				return allAssetBundleNames.ToArray();
			}
			AssetVariantTags.Locale localeVariantTagForLocale = AssetVariantTags.GetLocaleVariantTagForLocale(locale);
			List<string> list = new List<string>();
			foreach (string item in allAssetBundleNames)
			{
				string[] array = item.Split('-')[0].Split('_');
				string text = array[array.Length - 1];
				text = text.Substring(0, 2) + text.Substring(2, 2).ToUpper();
				if (!Localization.IsValidLocaleName(text) || text.Equals(localeVariantTagForLocale.ToString()))
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}

		public bool TryGetDirectBundleFromGuid(string guid, out string assetBundleName)
		{
			return m_assetsCatalog.TryGetAssetLocationFromGuid(guid, out assetBundleName);
		}

		public string[] GetTagsFromAssetBundle(string assetBundleName)
		{
			return m_tagsMetadata.GetTagsFromAssetBundle(assetBundleName);
		}

		public string[] GetAllTags(string tagGroup, bool excludeOverridenTag)
		{
			return m_tagsMetadata.GetAllTags(tagGroup, excludeOverridenTag);
		}

		public bool TryResolveAsset(string guid, out string resolvedGuid, out string resolvedBundle, AssetVariantTags.Locale locale = AssetVariantTags.Locale.enUS, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, AssetVariantTags.Platform platform = AssetVariantTags.Platform.Any)
		{
			return m_assetsCatalog.TryResolveAsset(guid, out resolvedGuid, out resolvedBundle, locale, quality, platform);
		}

		public string[] GetTagGroups()
		{
			return m_tagsMetadata.GetTagGroups();
		}

		public string[] GetTagsInTagGroup(string tagGroup)
		{
			return m_tagsMetadata.GetTagsInTagGroup(tagGroup);
		}

		public string[] GetTagsInTagGroup(int tagGroupId)
		{
			return m_tagsMetadata.GetTagsInTagGroup(tagGroupId);
		}

		public string ConvertToOverrideTag(string tag, string tagGroup)
		{
			return m_tagsMetadata.ConvertToOverrideTag(tag, tagGroup);
		}

		public string ConvertToOverrideTag(string tag, int tagGroupId)
		{
			return m_tagsMetadata.ConvertToOverrideTag(tag, tagGroupId);
		}

		public string GetTagGroupForTag(string tag)
		{
			return m_tagsMetadata.GetTagGroupForTag(tag);
		}

		public void ReadLocaleCatalogs()
		{
			m_assetsCatalog.LoadLocaleCatalogs();
		}

		public static ScriptableAssetManifest CreateEmpty()
		{
			return new ScriptableAssetManifest
			{
				m_assetsCatalog = new CompositeAssetCatalog(),
				m_tagsMetadata = ScriptableObject.CreateInstance<ScriptableAssetTagsMetadata>()
			};
		}
	}
}
