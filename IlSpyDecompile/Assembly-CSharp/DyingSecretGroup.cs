using System.Collections.Generic;

public class DyingSecretGroup
{
	private Card m_mainCard;

	private List<Card> m_cards = new List<Card>();

	private List<Actor> m_actors = new List<Actor>();

	public Card GetMainCard()
	{
		return m_mainCard;
	}

	public List<Actor> GetActors()
	{
		return m_actors;
	}

	public List<Card> GetCards()
	{
		return m_cards;
	}

	public void AddCard(Card card)
	{
		if (m_mainCard == null)
		{
			Zone zone = card.GetZone();
			m_mainCard = zone.GetCards().Find((Card currCard) => currCard.IsShown());
		}
		m_cards.Add(card);
		m_actors.Add(card.GetActor());
	}
}
