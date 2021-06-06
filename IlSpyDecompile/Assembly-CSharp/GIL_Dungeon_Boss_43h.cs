using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_43h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomLines = new List<string> { "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_01.prefab:d273a733217d81e4d95597c70755a893", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_02.prefab:3b08e00d2aea68747a83fd9a0c2d2ecc", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_03.prefab:624016a4f79e249499dff4ae16d1b28b", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_04.prefab:617ed484b0a362e4ab35ba1de9bf310e", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_05.prefab:7a885df5d3001b144aaeba324620d575" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string> { "VO_GILA_BOSS_43h_Male_HumanGhost_Intro_01.prefab:cc1f4c2e247ff014986c89a0edf75101", "VO_GILA_BOSS_43h_Male_HumanGhost_EmoteResponse_01.prefab:33914b71d09661e4eb62aa201dc8c4a1", "VO_GILA_BOSS_43h_Male_HumanGhost_Death_01.prefab:7efb42380fd966048857eb57a899eea1", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_01.prefab:d273a733217d81e4d95597c70755a893", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_02.prefab:3b08e00d2aea68747a83fd9a0c2d2ecc", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_03.prefab:624016a4f79e249499dff4ae16d1b28b", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_04.prefab:617ed484b0a362e4ab35ba1de9bf310e", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlaysDeathrattle_05.prefab:7a885df5d3001b144aaeba324620d575", "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlayStrokeOfMidnight_01.prefab:b92bec975abfc624d9436bbf8702e6c4" })
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_43h_Male_HumanGhost_Intro_01.prefab:cc1f4c2e247ff014986c89a0edf75101", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_43h_Male_HumanGhost_EmoteResponse_01.prefab:33914b71d09661e4eb62aa201dc8c4a1", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_43h_Male_HumanGhost_Death_01.prefab:7efb42380fd966048857eb57a899eea1";
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
		return new List<string>();
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "GILA_904")
		{
			yield return PlayBossLine(enemyActor, "VO_GILA_BOSS_43h_Male_HumanGhost_EventPlayStrokeOfMidnight_01.prefab:b92bec975abfc624d9436bbf8702e6c4");
		}
		if (entity.HasTag(GAME_TAG.DEATHRATTLE))
		{
			string text = PopRandomLineWithChance(m_RandomLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
		}
	}
}
