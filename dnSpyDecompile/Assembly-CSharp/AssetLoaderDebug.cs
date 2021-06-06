using System;
using UnityEngine;

// Token: 0x0200084F RID: 2127
public static class AssetLoaderDebug
{
	// Token: 0x0600735F RID: 29535 RVA: 0x00005576 File Offset: 0x00003776
	public static Rect LayoutUI(Rect space)
	{
		return space;
	}

	// Token: 0x06007360 RID: 29536 RVA: 0x0019DE03 File Offset: 0x0019C003
	public static string HandleCheat(string func, string[] args, string rawArgs)
	{
		return string.Empty;
	}

	// Token: 0x04005BC1 RID: 23489
	public const string Cheat_PrintHandles = "printassethandles";

	// Token: 0x04005BC2 RID: 23490
	public const string Cheat_PrintBundles = "printassetbundles";

	// Token: 0x04005BC3 RID: 23491
	public const string Cheat_OrphanAsset = "orphanasset";

	// Token: 0x04005BC4 RID: 23492
	public const string Cheat_OrphanPrefab = "orphanprefab";
}
