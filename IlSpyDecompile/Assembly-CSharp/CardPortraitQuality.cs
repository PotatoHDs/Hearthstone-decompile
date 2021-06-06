public class CardPortraitQuality
{
	public const int NOT_LOADED = 0;

	public const int LOW = 1;

	public const int MEDIUM = 2;

	public const int HIGH = 3;

	public int TextureQuality;

	public bool LoadPremium;

	public CardPortraitQuality(int quality, bool loadPremium)
	{
		TextureQuality = quality;
		LoadPremium = loadPremium;
	}

	public CardPortraitQuality(int quality, TAG_PREMIUM premiumType)
	{
		TextureQuality = quality;
		LoadPremium = premiumType == TAG_PREMIUM.GOLDEN;
	}

	public static CardPortraitQuality GetUnloaded()
	{
		return new CardPortraitQuality(0, loadPremium: false);
	}

	public static CardPortraitQuality GetDefault()
	{
		return new CardPortraitQuality(3, loadPremium: true);
	}

	public static CardPortraitQuality GetFromDef(CardDef def)
	{
		if (!(def == null))
		{
			return def.GetPortraitQuality();
		}
		return GetDefault();
	}

	public static bool operator >(CardPortraitQuality left, CardPortraitQuality right)
	{
		return !(left <= right);
	}

	public static bool operator <(CardPortraitQuality left, CardPortraitQuality right)
	{
		return !(left >= right);
	}

	public static bool operator >=(CardPortraitQuality left, CardPortraitQuality right)
	{
		if (left == null)
		{
			return false;
		}
		if (right == null)
		{
			return true;
		}
		if (left.TextureQuality >= right.TextureQuality)
		{
			if (!left.LoadPremium)
			{
				return !right.LoadPremium;
			}
			return true;
		}
		return false;
	}

	public static bool operator <=(CardPortraitQuality left, CardPortraitQuality right)
	{
		if (left == null)
		{
			return true;
		}
		if (right == null)
		{
			return false;
		}
		if (left.TextureQuality <= right.TextureQuality)
		{
			if (left.LoadPremium)
			{
				return right.LoadPremium;
			}
			return true;
		}
		return false;
	}

	public override string ToString()
	{
		return "(" + TextureQuality + ", " + LoadPremium.ToString() + ")";
	}
}
