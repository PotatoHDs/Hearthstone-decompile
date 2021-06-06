using System.Collections.Generic;

public class AssetBundleInfo
{
	private static Dictionary<string, string> s_bundleNameToPath = new Dictionary<string, string>();

	public static string BundlePathPlatformModifier()
	{
		return "Win/";
	}

	public static string GetAssetBundlesDir()
	{
		return FileUtils.GetAssetPath($"Data/{BundlePathPlatformModifier()}");
	}

	public static string GetAssetBundlePath(string bundleName)
	{
		string value = string.Empty;
		if (bundleName != null && !s_bundleNameToPath.TryGetValue(bundleName, out value))
		{
			value = FileUtils.CreateLocalFilePath($"Data/{BundlePathPlatformModifier()}{bundleName}");
			s_bundleNameToPath[bundleName] = value;
		}
		return value;
	}
}
