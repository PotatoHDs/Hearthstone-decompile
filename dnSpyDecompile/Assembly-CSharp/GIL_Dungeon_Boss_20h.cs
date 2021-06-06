using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003DE RID: 990
public class GIL_Dungeon_Boss_20h : GIL_Dungeon
{
	// Token: 0x0600377F RID: 14207 RVA: 0x001193F0 File Offset: 0x001175F0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_20h_MangyWolf_Intro.prefab:a2fc1112ddf13f341aa993e782e2a20b",
			"VO_GILA_BOSS_20h_MangyWolf_Emote.prefab:a4a8067fb5363474b939750a725cd8d4",
			"VO_GILA_BOSS_20h_MangyWolf_Death.prefab:24b687c08c113c041b65a387fe95c1cb"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003780 RID: 14208 RVA: 0x0011946C File Offset: 0x0011766C
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003781 RID: 14209 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003782 RID: 14210 RVA: 0x00119482 File Offset: 0x00117682
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_20h_MangyWolf_Death.prefab:24b687c08c113c041b65a387fe95c1cb";
	}

	// Token: 0x06003783 RID: 14211 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003784 RID: 14212 RVA: 0x0011948C File Offset: 0x0011768C
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_20h_MangyWolf_Intro.prefab:a2fc1112ddf13f341aa993e782e2a20b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_20h_MangyWolf_Emote.prefab:a4a8067fb5363474b939750a725cd8d4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
