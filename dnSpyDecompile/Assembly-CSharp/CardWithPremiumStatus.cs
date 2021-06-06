using System;
using System.Collections.Generic;

// Token: 0x02000103 RID: 259
public class CardWithPremiumStatus
{
	// Token: 0x06000F2D RID: 3885 RVA: 0x00055729 File Offset: 0x00053929
	public CardWithPremiumStatus(long id, TAG_PREMIUM tag)
	{
		this.cardId = id;
		this.premium = tag;
	}

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0005573F File Offset: 0x0005393F
	public long cardId { get; }

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00055747 File Offset: 0x00053947
	// (set) Token: 0x06000F30 RID: 3888 RVA: 0x0005574F File Offset: 0x0005394F
	public TAG_PREMIUM premium { get; set; }

	// Token: 0x06000F31 RID: 3889 RVA: 0x00055758 File Offset: 0x00053958
	public static List<CardWithPremiumStatus> ConvertList(List<long> cards)
	{
		List<CardWithPremiumStatus> list = new List<CardWithPremiumStatus>();
		for (int i = 0; i < cards.Count; i++)
		{
			CardWithPremiumStatus item = new CardWithPremiumStatus(cards[i], TAG_PREMIUM.NORMAL);
			list.Add(item);
		}
		return list;
	}
}
