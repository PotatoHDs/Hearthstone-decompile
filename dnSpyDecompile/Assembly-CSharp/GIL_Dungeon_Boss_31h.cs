using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020003E8 RID: 1000
public class GIL_Dungeon_Boss_31h : GIL_Dungeon
{
	// Token: 0x060037D8 RID: 14296 RVA: 0x0011A934 File Offset: 0x00118B34
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			"VO_GILA_BOSS_31h_Male_Treant_Intro_01.prefab:fb24cfa4d71468640bb99d805d9b00b2",
			"VO_GILA_BOSS_31h_Male_Treant_Emote Response_01:6a0f7ce3a070b034694f0b2b774baec5",
			"VO_GILA_BOSS_31h_Male_Treant_Death_01.prefab:4704b92b67a05464f9f21ef411b296b0",
			"VO_GILA_BOSS_31h_Male_Treant_DefeatPlayer_01.prefab:359623b7d391a7240806665fb461f781",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_01.prefab:a286c847ac976484faa032bb9a7a6837",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_02.prefab:b58a5931aa994ba4e8253e1d1fd6102e",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_03.prefab:4d6ebb24cd61c22448afb6ef221379e5",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_05.prefab:3418219065900ee4c90203a03a3a598d"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060037D9 RID: 14297 RVA: 0x0011A9E4 File Offset: 0x00118BE4
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield break;
	}

	// Token: 0x060037DA RID: 14298 RVA: 0x0011A9FA File Offset: 0x00118BFA
	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>
		{
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_01.prefab:a286c847ac976484faa032bb9a7a6837",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_02.prefab:b58a5931aa994ba4e8253e1d1fd6102e",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_03.prefab:4d6ebb24cd61c22448afb6ef221379e5",
			"VO_GILA_BOSS_31h_Male_Treant_HeroPower_05.prefab:3418219065900ee4c90203a03a3a598d"
		};
	}

	// Token: 0x060037DB RID: 14299 RVA: 0x0011AA2D File Offset: 0x00118C2D
	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_31h_Male_Treant_Death_01.prefab:4704b92b67a05464f9f21ef411b296b0";
	}

	// Token: 0x060037DC RID: 14300 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060037DD RID: 14301 RVA: 0x0011AA34 File Offset: 0x00118C34
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_31h_Male_Treant_Intro_01.prefab:fb24cfa4d71468640bb99d805d9b00b2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_GILA_BOSS_31h_Male_Treant_Emote Response_01:6a0f7ce3a070b034694f0b2b774baec5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}
}
