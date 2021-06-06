using System;

namespace Hearthstone.Core
{
	// Token: 0x02001080 RID: 4224
	public interface IAssetManifest
	{
		// Token: 0x0600B6A4 RID: 46756
		string[] GetTagGroups();

		// Token: 0x0600B6A5 RID: 46757
		string[] GetTagsInTagGroup(string tagGroup);

		// Token: 0x0600B6A6 RID: 46758
		string GetTagGroupForTag(string tag);

		// Token: 0x0600B6A7 RID: 46759
		void ReadLocaleCatalogs();

		// Token: 0x0600B6A8 RID: 46760
		string[] GetAllAssetBundleNames(Locale locale = Locale.UNKNOWN);

		// Token: 0x0600B6A9 RID: 46761
		bool TryGetDirectBundleFromGuid(string guid, out string assetBundleName);

		// Token: 0x0600B6AA RID: 46762
		string[] GetTagsFromAssetBundle(string assetBundleName);

		// Token: 0x0600B6AB RID: 46763
		string[] GetAllTags(string tagGroup, bool excludeOverridenTag);

		// Token: 0x0600B6AC RID: 46764
		string ConvertToOverrideTag(string tag, string tagGroup);

		// Token: 0x0600B6AD RID: 46765
		bool TryResolveAsset(string guid, out string resolvedGuid, out string resolvedBundle, AssetVariantTags.Locale locale = AssetVariantTags.Locale.enUS, AssetVariantTags.Quality quality = AssetVariantTags.Quality.Normal, AssetVariantTags.Platform platform = AssetVariantTags.Platform.Any);
	}
}
