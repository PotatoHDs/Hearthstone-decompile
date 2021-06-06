using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000405 RID: 1029
public class GIL_Dungeon_Boss_62h : GIL_Dungeon
{
	// Token: 0x060038E7 RID: 14567 RVA: 0x0011E750 File Offset: 0x0011C950
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_62h_GustaveTheGutRipper_Intro.prefab:79e90a27856ed7f41965b5180001fddf",
			"VO_GILA_BOSS_62h_GustaveTheGutRipper_Emote.prefab:75823bea3585ef44ab6cae97961d0634",
			"VO_GILA_BOSS_62h_GustaveTheGutRipper_Death.prefab:3440e77119ee33a4d9b018aef09f9411"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060038E8 RID: 14568 RVA: 0x0011E7CC File Offset: 0x0011C9CC
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060038E9 RID: 14569 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060038EA RID: 14570 RVA: 0x0011E7E2 File Offset: 0x0011C9E2
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_62h_GustaveTheGutRipper_Death.prefab:3440e77119ee33a4d9b018aef09f9411";
	}

	// Token: 0x060038EB RID: 14571 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060038EC RID: 14572 RVA: 0x0011E7EC File Offset: 0x0011C9EC
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_62h_GustaveTheGutRipper_Intro.prefab:79e90a27856ed7f41965b5180001fddf", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_62h_GustaveTheGutRipper_Emote.prefab:75823bea3585ef44ab6cae97961d0634", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
