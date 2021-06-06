using System;

// Token: 0x02000850 RID: 2128
public static class AssetLoaderPrefs
{
	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x06007361 RID: 29537 RVA: 0x000052EC File Offset: 0x000034EC
	public static AssetLoaderPrefs.ASSET_LOADING_METHOD AssetLoadingMethod
	{
		get
		{
			return AssetLoaderPrefs.ASSET_LOADING_METHOD.ASSET_BUNDLES;
		}
	}

	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x06007362 RID: 29538 RVA: 0x000052EC File Offset: 0x000034EC
	public static AssetLoaderPrefs.ASSET_RESOLUTION_METHOD AssetResolutionMethod
	{
		get
		{
			return AssetLoaderPrefs.ASSET_RESOLUTION_METHOD.GENERATED_MANIFEST;
		}
	}

	// Token: 0x02002453 RID: 9299
	public enum ASSET_LOADING_METHOD
	{
		// Token: 0x0400E9D3 RID: 59859
		EDITOR_FILES,
		// Token: 0x0400E9D4 RID: 59860
		ASSET_BUNDLES
	}

	// Token: 0x02002454 RID: 9300
	public enum ASSET_RESOLUTION_METHOD
	{
		// Token: 0x0400E9D6 RID: 59862
		DYNAMIC,
		// Token: 0x0400E9D7 RID: 59863
		GENERATED_MANIFEST,
		// Token: 0x0400E9D8 RID: 59864
		NONE
	}
}
