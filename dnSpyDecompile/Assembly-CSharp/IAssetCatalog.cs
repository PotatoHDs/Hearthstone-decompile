using System;

// Token: 0x020008E1 RID: 2273
public interface IAssetCatalog
{
	// Token: 0x06007E00 RID: 32256
	string[] GetAllAssetBundleNames();

	// Token: 0x06007E01 RID: 32257
	string[] GetAllAssetPaths();

	// Token: 0x06007E02 RID: 32258
	bool TryGetAssetGuidFromPath(string path, out string guid);

	// Token: 0x06007E03 RID: 32259
	bool TryGetAssetLocation(string guid, out string assetPath, out string assetBundleName);

	// Token: 0x06007E04 RID: 32260
	bool TryUpdateAssetBundleName(string guid, string newBundleName);
}
