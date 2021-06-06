using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000781 RID: 1921
public class CardSpecificVoSpell : CardSoundSpell
{
	// Token: 0x06006C2E RID: 27694 RVA: 0x002304A8 File Offset: 0x0022E6A8
	public override AudioSource DetermineBestAudioSource()
	{
		CardSpecificVoData bestVoiceData = this.GetBestVoiceData();
		if (bestVoiceData == null)
		{
			return base.DetermineBestAudioSource();
		}
		return bestVoiceData.m_AudioSource;
	}

	// Token: 0x06006C2F RID: 27695 RVA: 0x002304CC File Offset: 0x0022E6CC
	public CardSpecificVoData GetBestVoiceData()
	{
		foreach (CardSpecificVoData cardSpecificVoData in this.m_CardSpecificVoDataList)
		{
			if (this.SearchForCard(cardSpecificVoData))
			{
				return cardSpecificVoData;
			}
		}
		return null;
	}

	// Token: 0x06006C30 RID: 27696 RVA: 0x00230528 File Offset: 0x0022E728
	private bool SearchForCard(CardSpecificVoData cardVOData)
	{
		if (string.IsNullOrEmpty(cardVOData.m_CardId))
		{
			return false;
		}
		foreach (SpellZoneTag zoneTag in cardVOData.m_ZonesToSearch)
		{
			List<Zone> zones = SpellUtils.FindZonesFromTag(this, zoneTag, cardVOData.m_SideToSearch);
			if (this.IsCardInZones(cardVOData.m_CardId, zones))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06006C31 RID: 27697 RVA: 0x002305A8 File Offset: 0x0022E7A8
	private bool IsCardInZones(string cardId, List<Zone> zones)
	{
		if (zones == null)
		{
			return false;
		}
		foreach (Zone zone in zones)
		{
			using (List<Card>.Enumerator enumerator2 = zone.GetCards().GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.GetEntity().GetCardId() == cardId)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x04005798 RID: 22424
	public List<CardSpecificVoData> m_CardSpecificVoDataList = new List<CardSpecificVoData>();
}
