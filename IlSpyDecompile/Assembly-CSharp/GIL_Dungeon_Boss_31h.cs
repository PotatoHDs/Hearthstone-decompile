using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_31h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_31h_Male_Treant_Intro_01.prefab:fb24cfa4d71468640bb99d805d9b00b2", "VO_GILA_BOSS_31h_Male_Treant_Emote Response_01:6a0f7ce3a070b034694f0b2b774baec5", "VO_GILA_BOSS_31h_Male_Treant_Death_01.prefab:4704b92b67a05464f9f21ef411b296b0", "VO_GILA_BOSS_31h_Male_Treant_DefeatPlayer_01.prefab:359623b7d391a7240806665fb461f781", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_01.prefab:a286c847ac976484faa032bb9a7a6837", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_02.prefab:b58a5931aa994ba4e8253e1d1fd6102e", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_03.prefab:4d6ebb24cd61c22448afb6ef221379e5", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_05.prefab:3418219065900ee4c90203a03a3a598d" })
		{
			PreloadSound(item);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_31h_Male_Treant_HeroPower_01.prefab:a286c847ac976484faa032bb9a7a6837", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_02.prefab:b58a5931aa994ba4e8253e1d1fd6102e", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_03.prefab:4d6ebb24cd61c22448afb6ef221379e5", "VO_GILA_BOSS_31h_Male_Treant_HeroPower_05.prefab:3418219065900ee4c90203a03a3a598d" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_31h_Male_Treant_Death_01.prefab:4704b92b67a05464f9f21ef411b296b0";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_31h_Male_Treant_Intro_01.prefab:fb24cfa4d71468640bb99d805d9b00b2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_31h_Male_Treant_Emote Response_01:6a0f7ce3a070b034694f0b2b774baec5", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
