namespace Hearthstone.Core
{
	public interface IAssetManifest
	{
		string[] GetTagGroups();

		string[] GetTagsInTagGroup(string tagGroup);

		string GetTagGroupForTag(string tag);

		void ReadLocaleCatalogs();

		string[] GetAllAssetBundleNames(Locale locale = Locale.UNKNOWN);

		bool TryGetDirectBundleFromGuid(string guid, out string assetBundleName);

		string[] GetTagsFromAssetBundle(string assetBundleName);

		string[] GetAllTags(string tagGroup, bool excludeOverridenTag);

		string ConvertToOverrideTag(string tag, string tagGroup);

		bool TryResolveAsset(string guid, out string resolvedGuid, out string resolvedBundle, AssetVariantTags.Locale locale = AssetVariantTags.Locale.enUS, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, AssetVariantTags.Platform platform = AssetVariantTags.Platform.Any);
	}
}
