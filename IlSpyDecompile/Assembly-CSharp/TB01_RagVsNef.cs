using System.Collections;
using UnityEngine;

public class TB01_RagVsNef : MissionEntity
{
	private Card m_ragnarosCard;

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent != 1)
		{
			yield break;
		}
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			Entity hero = value.GetHero();
			Card card = hero.GetCard();
			if (hero.GetCardId() == "TBA01_1")
			{
				m_ragnarosCard = card;
			}
		}
		GameState.Get().SetBusy(busy: true);
		CardSoundSpell cardSoundSpell = m_ragnarosCard.PlayEmote(EmoteType.THREATEN);
		if (cardSoundSpell.m_CardSoundData.m_AudioSource == null || cardSoundSpell.m_CardSoundData.m_AudioSource.clip == null)
		{
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		float length = cardSoundSpell.m_CardSoundData.m_AudioSource.clip.length;
		yield return new WaitForSeconds((float)((double)length * 0.8));
		GameState.Get().SetBusy(busy: false);
	}
}
