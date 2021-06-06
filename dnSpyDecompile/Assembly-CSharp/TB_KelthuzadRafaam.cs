using System;
using System.Collections;
using UnityEngine;

// Token: 0x020005B3 RID: 1459
public class TB_KelthuzadRafaam : MissionEntity
{
	// Token: 0x060050CD RID: 20685 RVA: 0x001A8B93 File Offset: 0x001A6D93
	public override void PreloadAssets()
	{
		base.PreloadSound("KT_Minions_Servants.prefab:128dc3329f23b2b439500351e9a1ec72");
	}

	// Token: 0x060050CE RID: 20686 RVA: 0x001A8BA0 File Offset: 0x001A6DA0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		foreach (Player player in GameState.Get().GetPlayerMap().Values)
		{
			Entity hero = player.GetHero();
			Card card = hero.GetCard();
			if (hero.GetCardId() == "TB_KTRAF_H_1")
			{
				this.m_kelthuzadActor = card.GetActor();
			}
		}
		if (missionEvent == 3)
		{
			Debug.Log("mission event 3");
			if (this.once)
			{
				this.once = false;
				GameState.Get().SetBusy(true);
				Notification.SpeechBubbleDirection direction = this.m_kelthuzadActor.GetEntity().IsControlledByFriendlySidePlayer() ? Notification.SpeechBubbleDirection.BottomLeft : Notification.SpeechBubbleDirection.TopRight;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechWithCustomGameString("KT_Minions_Servants.prefab:128dc3329f23b2b439500351e9a1ec72", "VO_EMOTE_HERO_TBKTRAF_KT_MINIONS_SERVANTS", direction, this.m_kelthuzadActor, 1f, true, false));
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x0400476E RID: 18286
	private Card m_kelthuzadCard;

	// Token: 0x0400476F RID: 18287
	private Actor m_kelthuzadActor;

	// Token: 0x04004770 RID: 18288
	private bool once = true;
}
