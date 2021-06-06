using System.Collections.Generic;

public class CardWithPremiumStatus
{
	public long cardId { get; }

	public TAG_PREMIUM premium { get; set; }

	public CardWithPremiumStatus(long id, TAG_PREMIUM tag)
	{
		cardId = id;
		premium = tag;
	}

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
