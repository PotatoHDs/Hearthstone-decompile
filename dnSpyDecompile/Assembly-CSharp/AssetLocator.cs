using System;
using Blizzard.T5.AssetManager;
using Hearthstone.Core;

// Token: 0x02000851 RID: 2129
public class AssetLocator : IAssetLocator
{
	// Token: 0x06007363 RID: 29539 RVA: 0x00251942 File Offset: 0x0024FB42
	public AssetLocator(IAssetManifest assetManifest)
	{
		this.m_assetManifest = assetManifest;
	}

	// Token: 0x06007364 RID: 29540 RVA: 0x00251951 File Offset: 0x0024FB51
	public bool TryLocateAsset(string assetAddress, out string bundleName, out string assetPath)
	{
		assetPath = assetAddress;
		return this.m_assetManifest.TryGetDirectBundleFromGuid(assetAddress, out bundleName);
	}

	// Token: 0x06007365 RID: 29541 RVA: 0x00251963 File Offset: 0x0024FB63
	public string GetBundlePath(string bundleName)
	{
		return AssetBundleInfo.GetAssetBundlePath(bundleName);
	}

	// Token: 0x04005BC5 RID: 23493
	private readonly IAssetManifest m_assetManifest;
}
