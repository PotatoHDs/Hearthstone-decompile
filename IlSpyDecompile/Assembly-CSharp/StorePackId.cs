using PegasusUtil;

public struct StorePackId
{
	public StorePackType Type;

	public int Id;

	public static bool operator ==(StorePackId a, StorePackId b)
	{
		if (a.Type == b.Type)
		{
			return a.Id == b.Id;
		}
		return false;
	}

	public static bool operator !=(StorePackId a, StorePackId b)
	{
		return !(a == b);
	}

	public override bool Equals(object obj)
	{
		if (((StorePackId)obj).Type == Type)
		{
			return ((StorePackId)obj).Id == Id;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return Type.GetHashCode() ^ Id;
	}

	public static ProductType GetProductTypeFromStorePackType(StorePackId storePackId)
	{
		switch (storePackId.Type)
		{
		case StorePackType.BOOSTER:
			if (!GameUtils.IsHiddenLicenseBundleBooster(storePackId))
			{
				return ProductType.PRODUCT_TYPE_BOOSTER;
			}
			return ProductType.PRODUCT_TYPE_HIDDEN_LICENSE;
		case StorePackType.MODULAR_BUNDLE:
			return ProductType.PRODUCT_TYPE_HIDDEN_LICENSE;
		default:
			return ProductType.PRODUCT_TYPE_UNKNOWN;
		}
	}
}
