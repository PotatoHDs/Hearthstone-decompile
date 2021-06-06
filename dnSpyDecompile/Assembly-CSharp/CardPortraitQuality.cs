using System;

// Token: 0x02000894 RID: 2196
public class CardPortraitQuality
{
	// Token: 0x0600788B RID: 30859 RVA: 0x002759D2 File Offset: 0x00273BD2
	public CardPortraitQuality(int quality, bool loadPremium)
	{
		this.TextureQuality = quality;
		this.LoadPremium = loadPremium;
	}

	// Token: 0x0600788C RID: 30860 RVA: 0x002759E8 File Offset: 0x00273BE8
	public CardPortraitQuality(int quality, TAG_PREMIUM premiumType)
	{
		this.TextureQuality = quality;
		this.LoadPremium = (premiumType == TAG_PREMIUM.GOLDEN);
	}

	// Token: 0x0600788D RID: 30861 RVA: 0x00275A01 File Offset: 0x00273C01
	public static CardPortraitQuality GetUnloaded()
	{
		return new CardPortraitQuality(0, false);
	}

	// Token: 0x0600788E RID: 30862 RVA: 0x00275A0A File Offset: 0x00273C0A
	public static CardPortraitQuality GetDefault()
	{
		return new CardPortraitQuality(3, true);
	}

	// Token: 0x0600788F RID: 30863 RVA: 0x00275A13 File Offset: 0x00273C13
	public static CardPortraitQuality GetFromDef(CardDef def)
	{
		if (!(def == null))
		{
			return def.GetPortraitQuality();
		}
		return CardPortraitQuality.GetDefault();
	}

	// Token: 0x06007890 RID: 30864 RVA: 0x00275A2A File Offset: 0x00273C2A
	public static bool operator >(CardPortraitQuality left, CardPortraitQuality right)
	{
		return !(left <= right);
	}

	// Token: 0x06007891 RID: 30865 RVA: 0x00275A36 File Offset: 0x00273C36
	public static bool operator <(CardPortraitQuality left, CardPortraitQuality right)
	{
		return !(left >= right);
	}

	// Token: 0x06007892 RID: 30866 RVA: 0x00275A42 File Offset: 0x00273C42
	public static bool operator >=(CardPortraitQuality left, CardPortraitQuality right)
	{
		return left != null && (right == null || (left.TextureQuality >= right.TextureQuality && (left.LoadPremium || !right.LoadPremium)));
	}

	// Token: 0x06007893 RID: 30867 RVA: 0x00275A71 File Offset: 0x00273C71
	public static bool operator <=(CardPortraitQuality left, CardPortraitQuality right)
	{
		return left == null || (right != null && left.TextureQuality <= right.TextureQuality && (!left.LoadPremium || right.LoadPremium));
	}

	// Token: 0x06007894 RID: 30868 RVA: 0x00275AA0 File Offset: 0x00273CA0
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"(",
			this.TextureQuality,
			", ",
			this.LoadPremium.ToString(),
			")"
		});
	}

	// Token: 0x04005E0F RID: 24079
	public const int NOT_LOADED = 0;

	// Token: 0x04005E10 RID: 24080
	public const int LOW = 1;

	// Token: 0x04005E11 RID: 24081
	public const int MEDIUM = 2;

	// Token: 0x04005E12 RID: 24082
	public const int HIGH = 3;

	// Token: 0x04005E13 RID: 24083
	public int TextureQuality;

	// Token: 0x04005E14 RID: 24084
	public bool LoadPremium;
}
