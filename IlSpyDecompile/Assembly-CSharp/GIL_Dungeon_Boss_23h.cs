using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_23h : GIL_Dungeon
{
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_23h_Male_Ettin_Intro_01.prefab:bfc78b140b25432439cb2605adbe4519", "VO_GILA_BOSS_23h_Male_Ettin_EmoteResponse_02.prefab:1eb9f9e18bad1594ea54fc761490bb26", "VO_GILA_BOSS_23h_Male_Ettin_Death_01.prefab:e098c858c01788c408eb0392c5ec7c5f", "VO_GILA_BOSS_23h_Male_Ettin_DefeatPlayer_01.prefab:bbce6df09242e294db15179d295d62b9", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_01.prefab:600b7c59f5b051f42861664193f77af8", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_02.prefab:a5cf79f3c6d28cb4d9f596d16695c5c0", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_03.prefab:50bda9a43a5680a4f8ff9c2a623f71d2", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_04.prefab:904f86555906835439af2abf7b104185", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_05.prefab:2637e154ff6d57242ab2776b1c5b5752" })
		{
			PreloadSound(item);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_23h_Male_Ettin_Intro_01.prefab:bfc78b140b25432439cb2605adbe4519", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_23h_Male_Ettin_EmoteResponse_02.prefab:1eb9f9e18bad1594ea54fc761490bb26", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_23h_Male_Ettin_Death_01.prefab:e098c858c01788c408eb0392c5ec7c5f";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_01.prefab:600b7c59f5b051f42861664193f77af8", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_02.prefab:a5cf79f3c6d28cb4d9f596d16695c5c0", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_03.prefab:50bda9a43a5680a4f8ff9c2a623f71d2", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_04.prefab:904f86555906835439af2abf7b104185", "VO_GILA_BOSS_23h_Male_Ettin_HeroPower_05.prefab:2637e154ff6d57242ab2776b1c5b5752" };
	}
}
