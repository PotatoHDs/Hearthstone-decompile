using System;

// Token: 0x0200090D RID: 2317
[Serializable]
public class ScriptableAssetVariantCatalog : ScriptableAssetCatalog<VariantAssetCatalogItem>
{
	// Token: 0x060080E2 RID: 32994 RVA: 0x0029DD3A File Offset: 0x0029BF3A
	public bool TryAddVariant(string variantGuid, string variantBundle, string baseGuid)
	{
		if (base.TryAddAsset(variantGuid, variantBundle))
		{
			this.m_assets[this.m_assets.Count - 1].baseGuid = baseGuid;
			return true;
		}
		return false;
	}
}
