using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon_Boss_29h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_SmallMinionLines = new List<string> { "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_01.prefab:5af3535278e2eb2429448aa9e8d03f2c", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_02.prefab:76e60f5f24349214aa5584a05665d6a2", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_03.prefab:457cc49a55afe104c96a18083353ff1e" };

	private List<string> m_TunesLines = new List<string> { "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_01.prefab:9fb7303928f352b44bda765d3f497c5b", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_02.prefab:ee95531b139acdf4b9db63538a5a2c08", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_03.prefab:04d18c6d8f3882a4b9e48a7a40a585ae" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_29h_Female_Satyr_Intro_01.prefab:7ec7479acf5acb74b9e4302ff92736e0", "VO_GILA_BOSS_29h_Female_Satyr_EmoteResponse_01.prefab:05b465e15cf38234aa54811463501d0d", "VO_GILA_BOSS_29h_Female_Satyr_Death_01.prefab:a601eaae85a47ee47a88c04719e621c1", "VO_GILA_BOSS_29h_Female_Satyr_DefeatPlayer_01.prefab:f019545d544dcf94dbec559817062b6e", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_01.prefab:aaf9dbf346ce0e64ab4762569c8becff", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_02.prefab:183ce152b1903cf47934d9689370aab9", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_03.prefab:833d3be257d014d4da2645b4940f01f4", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_04.prefab:c9b2c969d0100a844a457a08ab032b29", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_05.prefab:6ce18bfe1350eab4398c36e1d72742da", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_01.prefab:5af3535278e2eb2429448aa9e8d03f2c",
			"VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_02.prefab:76e60f5f24349214aa5584a05665d6a2", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysSmallMinion_03.prefab:457cc49a55afe104c96a18083353ff1e", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_01.prefab:9fb7303928f352b44bda765d3f497c5b", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_02.prefab:ee95531b139acdf4b9db63538a5a2c08", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysEnchantingTune_03.prefab:04d18c6d8f3882a4b9e48a7a40a585ae", "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysOldMilitiaHorn_01.prefab:5bdaf92c95c002143884314d0a70588d"
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_29h_Female_Satyr_Intro_01.prefab:7ec7479acf5acb74b9e4302ff92736e0", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_29h_Female_Satyr_EmoteResponse_01.prefab:05b465e15cf38234aa54811463501d0d", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_29h_Female_Satyr_Death_01.prefab:a601eaae85a47ee47a88c04719e621c1";
	}

	protected override bool GetShouldSupressDeathTextBubble()
	{
		return true;
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "GILA_BOSS_29t")
		{
			string text = PopRandomLineWithChance(m_TunesLines);
			if (text != null)
			{
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_01.prefab:aaf9dbf346ce0e64ab4762569c8becff", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_02.prefab:183ce152b1903cf47934d9689370aab9", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_03.prefab:833d3be257d014d4da2645b4940f01f4", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_04.prefab:c9b2c969d0100a844a457a08ab032b29", "VO_GILA_BOSS_29h_Female_Satyr_HeroPower_05.prefab:6ce18bfe1350eab4398c36e1d72742da" };
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardID = entity.GetCardId();
			while (m_enemySpeaking)
			{
				yield return null;
			}
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardID == "GILA_852a")
			{
				yield return PlayBossLine(enemyActor, "VO_GILA_BOSS_29h_Female_Satyr_EventPlaysOldMilitiaHorn_01.prefab:5bdaf92c95c002143884314d0a70588d");
			}
			if (entity.GetATK() <= 1 && entity.IsMinion() && m_SmallMinionLines.Count != 0)
			{
				string text = m_SmallMinionLines[Random.Range(0, m_SmallMinionLines.Count)];
				m_SmallMinionLines.Remove(text);
				yield return PlayLineOnlyOnce(enemyActor, text);
			}
		}
	}
}
