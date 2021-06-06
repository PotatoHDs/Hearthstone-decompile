using System;
using System.Collections.Generic;

// Token: 0x02000345 RID: 837
public class DeadSecretGroup
{
	// Token: 0x06003096 RID: 12438 RVA: 0x000FA179 File Offset: 0x000F8379
	public Card GetMainCard()
	{
		return this.m_mainCard;
	}

	// Token: 0x06003097 RID: 12439 RVA: 0x000FA181 File Offset: 0x000F8381
	public void SetMainCard(Card card)
	{
		this.m_mainCard = card;
	}

	// Token: 0x06003098 RID: 12440 RVA: 0x000FA18A File Offset: 0x000F838A
	public List<Card> GetCards()
	{
		return this.m_cards;
	}

	// Token: 0x06003099 RID: 12441 RVA: 0x000FA192 File Offset: 0x000F8392
	public void AddCard(Card card)
	{
		this.m_cards.Add(card);
	}

	// Token: 0x04001AFD RID: 6909
	private Card m_mainCard;

	// Token: 0x04001AFE RID: 6910
	private List<Card> m_cards = new List<Card>();
}
