using System.Collections;
using UnityEngine;

public class TB_KelthuzadRafaam : MissionEntity
{
	private Card m_kelthuzadCard;

	private Actor m_kelthuzadActor;

	private bool once = true;

	public override void PreloadAssets()
	{
		PreloadSound("KT_Minions_Servants.prefab:128dc3329f23b2b439500351e9a1ec72");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		foreach (Player value in GameState.Get().GetPlayerMap().Values)
		{
			Entity hero = value.GetHero();
			Card card = hero.GetCard();
			if (hero.GetCardId() == "TB_KTRAF_H_1")
			{
				m_kelthuzadActor = card.GetActor();
			}
		}
		if (missionEvent == 3)
		{
			Debug.Log("mission event 3");
			if (once)
			{
				once = false;
				GameState.Get().SetBusy(busy: true);
				Notification.SpeechBubbleDirection direction = (m_kelthuzadActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString("KT_Minions_Servants.prefab:128dc3329f23b2b439500351e9a1ec72", "VO_EMOTE_HERO_TBKTRAF_KT_MINIONS_SERVANTS", direction, m_kelthuzadActor));
				GameState.Get().SetBusy(busy: false);
			}
		}
		yield break;
	}
}
