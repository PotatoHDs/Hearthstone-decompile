using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005C6 RID: 1478
public class TB01_RagVsNef : MissionEntity
{
	// Token: 0x06005158 RID: 20824 RVA: 0x001AC193 File Offset: 0x001AA393
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 1)
		{
			foreach (Player player in GameState.Get().GetPlayerMap().Values)
			{
				Entity hero = player.GetHero();
				Card card = hero.GetCard();
				if (hero.GetCardId() == "TBA01_1")
				{
					this.m_ragnarosCard = card;
				}
			}
			GameState.Get().SetBusy(true);
			CardSoundSpell cardSoundSpell = this.m_ragnarosCard.PlayEmote(EmoteType.THREATEN);
			if (cardSoundSpell.m_CardSoundData.m_AudioSource == null || cardSoundSpell.m_CardSoundData.m_AudioSource.clip == null)
			{
				GameState.Get().SetBusy(false);
			}
			else
			{
				float length = cardSoundSpell.m_CardSoundData.m_AudioSource.clip.length;
				yield return new WaitForSeconds((float)((double)length * 0.8));
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x040048BC RID: 18620
	private Card m_ragnarosCard;
}
