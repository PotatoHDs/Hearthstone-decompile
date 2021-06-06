using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000408 RID: 1032
public class GIL_Dungeon_Boss_65h : GIL_Dungeon
{
	// Token: 0x06003900 RID: 14592 RVA: 0x0011EC5C File Offset: 0x0011CE5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"GILA_BOSS_65h_Gobbles_Intro.prefab:2874743bc69515b4b8dc76500e19146b",
			"GILA_BOSS_65h_Gobbles_EmoteResponse.prefab:9650db10cb0078644b486f3f78dfd5e2",
			"GILA_BOSS_65h_Gobbles_Death.prefab:70d3c25a198efa5418b249f3d38e3ba4"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003901 RID: 14593 RVA: 0x0011ECD8 File Offset: 0x0011CED8
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x06003902 RID: 14594 RVA: 0x00112BA2 File Offset: 0x00110DA2
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06003903 RID: 14595 RVA: 0x0011ECEE File Offset: 0x0011CEEE
	protected override string GetBossDeathLine()
	{
		return "GILA_BOSS_65h_Gobbles_Death.prefab:70d3c25a198efa5418b249f3d38e3ba4";
	}

	// Token: 0x06003904 RID: 14596 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003905 RID: 14597 RVA: 0x0011ECF8 File Offset: 0x0011CEF8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("GILA_BOSS_65h_Gobbles_Intro.prefab:2874743bc69515b4b8dc76500e19146b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("GILA_BOSS_65h_Gobbles_EmoteResponse.prefab:9650db10cb0078644b486f3f78dfd5e2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
