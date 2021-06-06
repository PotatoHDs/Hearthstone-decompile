using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_50h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomLines = new List<string> { "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_01.prefab:934a9489c5a8b9f42a270add91ab4f82", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_02.prefab:3660ee1b841d85b43b4600560351529b", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_03.prefab:0b9ba141e22bf714ab0ab3263f55e796" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_50h_Male_Spirit_Intro_01.prefab:55a97cc019cc32f4c90c7d7c8d520a0f", "VO_GILA_BOSS_50h_Male_Spirit_EmoteResponse_01.prefab:729f8a04024b7ce4faaf8d09d3dd7ae1", "VO_GILA_BOSS_50h_Male_Spirit_Death_01.prefab:f9aa74547facbdb458a4428ed822bd0b", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_01.prefab:934a9489c5a8b9f42a270add91ab4f82", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_02.prefab:3660ee1b841d85b43b4600560351529b", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_03.prefab:0b9ba141e22bf714ab0ab3263f55e796", "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_04.prefab:ee8e8af8894c43c478c0a5c2841b8a0d" })
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
		return new List<string>();
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_50h_Male_Spirit_Death_01.prefab:f9aa74547facbdb458a4428ed822bd0b";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_50h_Male_Spirit_Intro_01.prefab:55a97cc019cc32f4c90c7d7c8d520a0f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_50h_Male_Spirit_EmoteResponse_01.prefab:729f8a04024b7ce4faaf8d09d3dd7ae1", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardID = entity.GetCardId();
		yield return WaitForEntitySoundToFinish(entity);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardID == "GILA_500p2t")
		{
			string text = PopRandomLineWithChance(m_RandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
		}
		if (entity.HasTag(GAME_TAG.ECHO))
		{
			yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_50h_Male_Spirit_EventPlaysEcho_04.prefab:ee8e8af8894c43c478c0a5c2841b8a0d");
		}
	}
}
