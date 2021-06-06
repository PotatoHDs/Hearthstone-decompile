using System;
using UnityEngine;

[Serializable]
public class DeckInfoManaBar
{
	public GameObject m_barFill;

	public int m_manaCostID;

	public int m_numCards;

	public UberText m_costText;

	public UberText m_numCardsText;
}
