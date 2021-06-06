using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003EC RID: 1004
public class GIL_Dungeon_Boss_35h : GIL_Dungeon
{
	// Token: 0x060037FA RID: 14330 RVA: 0x0011B08C File Offset: 0x0011928C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_35h_Male_Beast_Intro.prefab:7d5d462108277c54fa34c7419d6b7f34",
			"VO_GILA_BOSS_35h_Male_Beast_EmoteResponse.prefab:720896f4a73b2dd499faa041441413f6",
			"VO_GILA_BOSS_35h_Male_Beast_Death.prefab:040c07f77adc84e44b9526ec8f40c5b7"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037FB RID: 14331 RVA: 0x0011B108 File Offset: 0x00119308
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037FC RID: 14332 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060037FD RID: 14333 RVA: 0x0011B11E File Offset: 0x0011931E
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_35h_Male_Beast_Death.prefab:040c07f77adc84e44b9526ec8f40c5b7";
	}

	// Token: 0x060037FE RID: 14334 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037FF RID: 14335 RVA: 0x0011B128 File Offset: 0x00119328
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_35h_Male_Beast_Intro.prefab:7d5d462108277c54fa34c7419d6b7f34", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_35h_Male_Beast_EmoteResponse.prefab:720896f4a73b2dd499faa041441413f6", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
