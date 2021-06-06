using System.Collections.Generic;
using UnityEngine;

public class CardSpecificVoSpell : CardSoundSpell
{
	public List<CardSpecificVoData> m_CardSpecificVoDataList = new List<CardSpecificVoData>();

	public override AudioSource DetermineBestAudioSource()
	{
		CardSpecificVoData bestVoiceData = GetBestVoiceData();
		if (bestVoiceData == null)
		{
			return base.DetermineBestAudioSource();
		}
		return bestVoiceData.m_AudioSource;
	}

	public CardSpecificVoData GetBestVoiceData()
	{
		foreach (CardSpecificVoData cardSpecificVoData in m_CardSpecificVoDataList)
		{
			if (SearchForCard(cardSpecificVoData))
			{
				return cardSpecificVoData;
			}
		}
		return null;
	}

	private bool SearchForCard(CardSpecificVoData cardVOData)
	{
		if (string.IsNullOrEmpty(cardVOData.m_CardId))
		{
			return false;
		}
		foreach (SpellZoneTag item in cardVOData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, item, cardVOData.m_SideToSearch);
			if (IsCardInZones(cardVOData.m_CardId, zones))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsCardInZones(string cardId, List<Zone> zones)
	{
		if (zones == null)
		{
			return false;
		}
		foreach (Zone zone in zones)
		{
			foreach (Card card in zone.GetCards())
			{
				if (card.GetEntity().GetCardId() == cardId)
				{
					return true;
				}
			}
		}
		return false;
	}
}
