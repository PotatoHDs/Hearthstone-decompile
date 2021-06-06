using UnityEngine;

public static class AssetLoaderDebug
{
	public const string Cheat_PrintHandles = "printassethandles";

	public const string Cheat_PrintBundles = "printassetbundles";

	public const string Cheat_OrphanAsset = "orphanasset";

	public const string Cheat_OrphanPrefab = "orphanprefab";

	public static Rect LayoutUI(Rect space)
	{
		return space;
	}

	public static string HandleCheat(string func, string[] args, string rawArgs)
	{
		return string.Empty;
	}
}
