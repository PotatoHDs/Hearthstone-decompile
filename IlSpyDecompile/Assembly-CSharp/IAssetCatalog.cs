public interface IAssetCatalog
{
	string[] GetAllAssetBundleNames();

	string[] GetAllAssetPaths();

	bool TryGetAssetGuidFromPath(string path, out string guid);

	bool TryGetAssetLocation(string guid, out string assetPath, out string assetBundleName);

	bool TryUpdateAssetBundleName(string guid, string newBundleName);
}
