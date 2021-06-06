using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003EB RID: 1003
public class GIL_Dungeon_Boss_34h : GIL_Dungeon
{
	// Token: 0x060037F2 RID: 14322 RVA: 0x0011AEF8 File Offset: 0x001190F8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_34h_Male_Demon_Intro_01.prefab:127d0d9c805d73b4798b6a933fd79e8d",
			"VO_GILA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:36cd563aa82e55f46a02e7a260706d03",
			"VO_GILA_BOSS_34h_Male_Demon_Death_01.prefab:a25d8865da590d64084a27bc4bde34e7",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_01.prefab:a37c2733cf24d564c87bb936bd319288",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_02.prefab:21ff8766f68381a43be28d779d6c968a",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_03.prefab:c0db8c5648389c14faa56642050455e4",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_04.prefab:ffdef008db8e7d14abe96b4c710c7798",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_05.prefab:f0e80111bde0e2344929324cc1708723"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037F3 RID: 14323 RVA: 0x0011AFA8 File Offset: 0x001191A8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_34h_Male_Demon_Intro_01.prefab:127d0d9c805d73b4798b6a933fd79e8d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_34h_Male_Demon_EmoteResponse_01.prefab:36cd563aa82e55f46a02e7a260706d03", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060037F4 RID: 14324 RVA: 0x0011B02F File Offset: 0x0011922F
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_34h_Male_Demon_Death_01.prefab:a25d8865da590d64084a27bc4bde34e7";
	}

	// Token: 0x060037F5 RID: 14325 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037F6 RID: 14326 RVA: 0x0011B036 File Offset: 0x00119236
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037F7 RID: 14327 RVA: 0x0011B04C File Offset: 0x0011924C
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_01.prefab:a37c2733cf24d564c87bb936bd319288",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_02.prefab:21ff8766f68381a43be28d779d6c968a",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_03.prefab:c0db8c5648389c14faa56642050455e4",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_04.prefab:ffdef008db8e7d14abe96b4c710c7798",
			"VO_GILA_BOSS_34h_Male_Demon_HeroPower_05.prefab:f0e80111bde0e2344929324cc1708723"
		};
	}
}
