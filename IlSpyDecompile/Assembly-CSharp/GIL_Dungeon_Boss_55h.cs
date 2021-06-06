using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_55h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomWispLines = new List<string> { "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_02.prefab:99cd15bc7f01799419e95264a6001b42", "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_03.prefab:6dafb4b22c71af64093d6ee4bf7eb356", "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_04.prefab:a9602988cfe2c26498142ae555290ce9" };

	private List<string> m_RandomAssimilationLines = new List<string> { "VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_01.prefab:a6058e565a8c41840a594ee925f4b2f1", "VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_02.prefab:7191f790b24aa1144a51d2b31db77777", "VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_03.prefab:165abae668774ed4c811ddd64640aad8" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_55h_Female_NightElf_Intro_01.prefab:e3728559d0b0985429d242b1fe2b9b8b", "VO_GILA_BOSS_55h_Female_NightElf_EmoteResponse_01.prefab:116d3641525316e48946409c8fc0c513", "VO_GILA_BOSS_55h_Female_NightElf_Death_01.prefab:43f663f0d8f79e74bb961d940249124b", "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_01.prefab:1fb7a796c7919ea4eb8de297546ee318", "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_02.prefab:e9fc95ab8de424f4886016550f337f3c", "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_03.prefab:a0f70912013f6384198a5572b898d4c2", "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_02.prefab:99cd15bc7f01799419e95264a6001b42", "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_03.prefab:6dafb4b22c71af64093d6ee4bf7eb356", "VO_GILA_BOSS_55h_Female_NightElf_EventKillsWisp_04.prefab:a9602988cfe2c26498142ae555290ce9", "VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_01.prefab:a6058e565a8c41840a594ee925f4b2f1",
			"VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_02.prefab:7191f790b24aa1144a51d2b31db77777", "VO_GILA_BOSS_55h_Female_NightElf_EventAssimilation_03.prefab:165abae668774ed4c811ddd64640aad8", "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCompass_01.prefab:4976a34329a9cbc48926976b879ca402", "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCartographer_01.prefab:68105bfdb75d15444a189b7713d2eb6a"
		})
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_55h_Female_NightElf_Intro_01.prefab:e3728559d0b0985429d242b1fe2b9b8b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_55h_Female_NightElf_EmoteResponse_01.prefab:116d3641525316e48946409c8fc0c513", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_55h_Female_NightElf_Death_01.prefab:43f663f0d8f79e74bb961d940249124b";
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToPlayedCardWithTiming(entity);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "GILA_BOSS_55t2")
		{
			string text = PopRandomLineWithChance(m_RandomAssimilationLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_01.prefab:1fb7a796c7919ea4eb8de297546ee318", "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_02.prefab:e9fc95ab8de424f4886016550f337f3c", "VO_GILA_BOSS_55h_Female_NightElf_HeroPower_03.prefab:a0f70912013f6384198a5572b898d4c2" };
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "GILA_853b"))
		{
			if (cardId == "GILA_802")
			{
				yield return PlayBossLine(actor, "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCartographer_01.prefab:68105bfdb75d15444a189b7713d2eb6a");
			}
		}
		else
		{
			yield return PlayBossLine(actor, "VO_GILA_BOSS_55h_Female_NightElf_EventPlayCompass_01.prefab:4976a34329a9cbc48926976b879ca402");
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item) && missionEvent == 101)
		{
			string text = PopRandomLineWithChance(m_RandomWispLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}
}
