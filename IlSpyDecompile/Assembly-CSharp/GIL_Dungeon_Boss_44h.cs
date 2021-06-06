using System.Collections;
using System.Collections.Generic;

public class GIL_Dungeon_Boss_44h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_BossHeal = new List<string> { "VO_GILA_BOSS_44h_Male_Troll_EventHeals_01.prefab:4ae1c7b1886eb7146afd565bd657a8f1", "VO_GILA_BOSS_44h_Male_Troll_EventHeals_02.prefab:426edecc3738b0141aaa43c0917cc5de", "VO_GILA_BOSS_44h_Male_Troll_EventHeals_04.prefab:aefa2dfc7947fc74595541542d4ad8c4" };

	private List<string> m_PlayerHeal = new List<string> { "VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_03.prefab:a1e7fa7c42a47d642a1cce3d848dcd89", "VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_04.prefab:de44a20eabddf8a47b8b7155505e7009" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_44h_Male_Troll_Intro_01.prefab:9dd32ba7833dcfd44840ca37fba0b27b", "VO_GILA_BOSS_44h_Male_Troll_EmoteResponse_01.prefab:058769cea8ad45d4da1351f69ff6a0dd", "VO_GILA_BOSS_44h_Male_Troll_Death_01.prefab:a2acdebd26eb1374dab8e62b6677b3de", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_01.prefab:7b359f4c22676d147b26675bf420498b", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_02.prefab:2b67d15a14195374b8eba98db631eb66", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_03.prefab:ef80eeff5436caa4aa68f3c5f7958b2a", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_04.prefab:7f38acc3c246298458f4cb7a22908c98", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_05.prefab:92aa0d012ac47c844844b285d627cdd3", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_06.prefab:a78930d455cb72a46b71d1698f2a183b", "VO_GILA_BOSS_44h_Male_Troll_EventHeals_01.prefab:4ae1c7b1886eb7146afd565bd657a8f1",
			"VO_GILA_BOSS_44h_Male_Troll_EventHeals_02.prefab:426edecc3738b0141aaa43c0917cc5de", "VO_GILA_BOSS_44h_Male_Troll_EventHeals_04.prefab:aefa2dfc7947fc74595541542d4ad8c4", "VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_03.prefab:a1e7fa7c42a47d642a1cce3d848dcd89", "VO_GILA_BOSS_44h_Male_Troll_EventPlayerHeals_04.prefab:de44a20eabddf8a47b8b7155505e7009", "VO_GILA_BOSS_44h_Male_Troll_EventPlaysBandages_01.prefab:a37d44a72287bbb48abe47ac1092dc61"
		})
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
		return new List<string> { "VO_GILA_BOSS_44h_Male_Troll_HeroPower_01.prefab:7b359f4c22676d147b26675bf420498b", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_02.prefab:2b67d15a14195374b8eba98db631eb66", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_03.prefab:ef80eeff5436caa4aa68f3c5f7958b2a", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_04.prefab:7f38acc3c246298458f4cb7a22908c98", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_05.prefab:92aa0d012ac47c844844b285d627cdd3", "VO_GILA_BOSS_44h_Male_Troll_HeroPower_06.prefab:a78930d455cb72a46b71d1698f2a183b" };
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_44h_Male_Troll_Death_01.prefab:a2acdebd26eb1374dab8e62b6677b3de";
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_44h_Male_Troll_Intro_01.prefab:9dd32ba7833dcfd44840ca37fba0b27b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_44h_Male_Troll_EmoteResponse_01.prefab:058769cea8ad45d4da1351f69ff6a0dd", Notification.SpeechBubbleDirection.TopRight, actor));
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
		switch (missionEvent)
		{
		case 101:
		{
			string text = PopRandomLineWithChance(m_BossHeal);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
		case 102:
		{
			string text = PopRandomLineWithChance(m_PlayerHeal);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
			break;
		}
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "GILA_506t")
			{
				yield return PlayEasterEggLine(actor, "VO_GILA_BOSS_44h_Male_Troll_EventPlaysBandages_01.prefab:a37d44a72287bbb48abe47ac1092dc61");
			}
		}
	}
}
