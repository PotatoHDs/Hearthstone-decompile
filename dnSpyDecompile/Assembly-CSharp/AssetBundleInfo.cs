using System;
using System.Collections.Generic;

// Token: 0x0200084B RID: 2123
public class AssetBundleInfo
{
	// Token: 0x0600731A RID: 29466 RVA: 0x002509E2 File Offset: 0x0024EBE2
	public static string BundlePathPlatformModifier()
	{
		return "Win/";
	}

	// Token: 0x0600731B RID: 29467 RVA: 0x002509E9 File Offset: 0x0024EBE9
	public static string GetAssetBundlesDir()
	{
		return FileUtils.GetAssetPath(string.Format("Data/{0}", AssetBundleInfo.BundlePathPlatformModifier()), true);
	}

	// Token: 0x0600731C RID: 29468 RVA: 0x00250A00 File Offset: 0x0024EC00
	public static string GetAssetBundlePath(string bundleName)
	{
		string text = string.Empty;
		if (bundleName != null && !AssetBundleInfo.s_bundleNameToPath.TryGetValue(bundleName, out text))
		{
			text = FileUtils.CreateLocalFilePath(string.Format("Data/{0}{1}", AssetBundleInfo.BundlePathPlatformModifier(), bundleName), true);
			AssetBundleInfo.s_bundleNameToPath[bundleName] = text;
		}
		return text;
	}

	// Token: 0x04005BB7 RID: 23479
	private static Dictionary<string, string> s_bundleNameToPath = new Dictionary<string, string>();
}
