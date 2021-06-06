using System;
using PegasusUtil;

// Token: 0x0200072A RID: 1834
public struct StorePackId
{
	// Token: 0x060066F6 RID: 26358 RVA: 0x00218F73 File Offset: 0x00217173
	public static bool operator ==(StorePackId a, StorePackId b)
	{
		return a.Type == b.Type && a.Id == b.Id;
	}

	// Token: 0x060066F7 RID: 26359 RVA: 0x00218F93 File Offset: 0x00217193
	public static bool operator !=(StorePackId a, StorePackId b)
	{
		return !(a == b);
	}

	// Token: 0x060066F8 RID: 26360 RVA: 0x00218F9F File Offset: 0x0021719F
	public override bool Equals(object obj)
	{
		return ((StorePackId)obj).Type == this.Type && ((StorePackId)obj).Id == this.Id;
	}

	// Token: 0x060066F9 RID: 26361 RVA: 0x00218FC9 File Offset: 0x002171C9
	public override int GetHashCode()
	{
		return this.Type.GetHashCode() ^ this.Id;
	}

	// Token: 0x060066FA RID: 26362 RVA: 0x00218FE4 File Offset: 0x002171E4
	public static ProductType GetProductTypeFromStorePackType(StorePackId storePackId)
	{
		StorePackType type = storePackId.Type;
		if (type != StorePackType.BOOSTER)
		{
			if (type != StorePackType.MODULAR_BUNDLE)
			{
				return ProductType.PRODUCT_TYPE_UNKNOWN;
			}
			return ProductType.PRODUCT_TYPE_HIDDEN_LICENSE;
		}
		else
		{
			if (!GameUtils.IsHiddenLicenseBundleBooster(storePackId))
			{
				return ProductType.PRODUCT_TYPE_BOOSTER;
			}
			return ProductType.PRODUCT_TYPE_HIDDEN_LICENSE;
		}
	}

	// Token: 0x040054C7 RID: 21703
	public StorePackType Type;

	// Token: 0x040054C8 RID: 21704
	public int Id;
}
