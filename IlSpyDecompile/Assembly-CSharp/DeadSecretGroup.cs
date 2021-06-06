using System.Collections.Generic;

public class DeadSecretGroup
{
	private Card m_mainCard;

	private List<Card> m_cards = new List<Card>();

	public Card GetMainCard()
	{
		return m_mainCard;
	}

	public void SetMainCard(Card card)
	{
		m_mainCard = card;
	}

	public List<Card> GetCards()
	{
		return m_cards;
	}

	public void AddCard(Card card)
	{
		m_cards.Add(card);
	}
}
