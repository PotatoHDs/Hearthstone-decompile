using System;
using System.Collections.Generic;

// Token: 0x02000344 RID: 836
public class DyingSecretGroup
{
	// Token: 0x06003091 RID: 12433 RVA: 0x000FA0D2 File Offset: 0x000F82D2
	public Card GetMainCard()
	{
		return this.m_mainCard;
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x000FA0DA File Offset: 0x000F82DA
	public List<Actor> GetActors()
	{
		return this.m_actors;
	}

	// Token: 0x06003093 RID: 12435 RVA: 0x000FA0E2 File Offset: 0x000F82E2
	public List<Card> GetCards()
	{
		return this.m_cards;
	}

	// Token: 0x06003094 RID: 12436 RVA: 0x000FA0EC File Offset: 0x000F82EC
	public void AddCard(Card card)
	{
		if (this.m_mainCard == null)
		{
			Zone zone = card.GetZone();
			this.m_mainCard = zone.GetCards().Find((Card currCard) => currCard.IsShown());
		}
		this.m_cards.Add(card);
		this.m_actors.Add(card.GetActor());
	}

	// Token: 0x04001AFA RID: 6906
	private Card m_mainCard;

	// Token: 0x04001AFB RID: 6907
	private List<Card> m_cards = new List<Card>();

	// Token: 0x04001AFC RID: 6908
	private List<Actor> m_actors = new List<Actor>();
}
