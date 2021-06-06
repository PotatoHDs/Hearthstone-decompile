using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon_Boss_48h : GIL_Dungeon
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_RandomIntroLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_Intro_01.prefab:cdbe5f3032b08604b8ba8be886d2e053", "VO_GILA_BOSS_48h_Female_Gnome_Intro_02.prefab:34204adabe066f04bb5f23c4caec2c0e", "VO_GILA_BOSS_48h_Female_Gnome_Intro_03.prefab:fa483aded9758de47b80947b3e91fc71", "VO_GILA_BOSS_48h_Female_Gnome_Intro_04.prefab:6c4b6ff176b2af6478aefcd8b7dba93c", "VO_GILA_BOSS_48h_Female_Gnome_Intro_05.prefab:f5d744502ca4b9d4b8d24193ac48d833" };

	private List<string> m_RandomPlayerHeroPowerLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_01.prefab:1e1d09fefa30b8b4da4bab9dae0bcb3b", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_02.prefab:7c6a08801ab2f5f4e9a7cbfc49b2ea38", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_03.prefab:e6106fa6866b4b348a47db1dc18711c6", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_04.prefab:b51ed40984e166b4aa39b2d91aff6862" };

	private List<string> m_RandomBossAltTimeLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_01.prefab:fbbc32ec0ab55df409e975d1ba7cce55", "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_02.prefab:1696c08fe90041c43a637e22096f143a", "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_03.prefab:28ac6a5e172862048bae4c12cd89f582" };

	private List<string> m_RandomPlayerAltTimeLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerAltTime_01.prefab:51ef876f95a55ea43a9d2387b884eec2", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerAltTime_02.prefab:39d42e44e0b1acd449cca096bc98b081" };

	private List<string> m_RandomBossTimeWarpLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_EventTimeWarp_01.prefab:fd660745420ca7f48b501e5c120a65fd", "VO_GILA_BOSS_48h_Female_Gnome_EventTimeWarp_02.prefab:813e9352f0e657945aa521a261863c93" };

	private List<string> m_RandomPlayerTimeWarpLines = new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarpToki_01.prefab:a524136f9ebce1744adbfc7b0958ab57", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarpToki_02.prefab:6f2d69fe3da5d6a4ca248554cc6c98df", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarp_01.prefab:483fbbd189ec7de46adbbf0b2328abd6", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarp_02.prefab:ccf2d22d428cfbf489cb4a316cdc56b9" };

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_48h_Female_Gnome_Intro_01.prefab:cdbe5f3032b08604b8ba8be886d2e053", "VO_GILA_BOSS_48h_Female_Gnome_Intro_02.prefab:34204adabe066f04bb5f23c4caec2c0e", "VO_GILA_BOSS_48h_Female_Gnome_Intro_03.prefab:fa483aded9758de47b80947b3e91fc71", "VO_GILA_BOSS_48h_Female_Gnome_Intro_04.prefab:6c4b6ff176b2af6478aefcd8b7dba93c", "VO_GILA_BOSS_48h_Female_Gnome_Intro_05.prefab:f5d744502ca4b9d4b8d24193ac48d833", "VO_GILA_BOSS_48h_Female_Gnome_EmoteResponse_01.prefab:7a5cb51ef214a804fae3c933aae63b57", "VO_GILA_BOSS_48h_Female_Gnome_Death_01.prefab:081f92f8c5206444c8058b5c38a0beb2", "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_01.prefab:24a5c14cf0facb4469c7b66c58d5ebec", "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_02.prefab:128b3e847fdec794691485008c6e798f", "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_03.prefab:8d8202e1499fa1d4380f3eb60a7b4e45",
			"VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_01.prefab:1e1d09fefa30b8b4da4bab9dae0bcb3b", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_02.prefab:7c6a08801ab2f5f4e9a7cbfc49b2ea38", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_03.prefab:e6106fa6866b4b348a47db1dc18711c6", "VO_GILA_BOSS_48h_Female_Gnome_PlayerHeroPower_04.prefab:b51ed40984e166b4aa39b2d91aff6862", "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_01.prefab:fbbc32ec0ab55df409e975d1ba7cce55", "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_02.prefab:1696c08fe90041c43a637e22096f143a", "VO_GILA_BOSS_48h_Female_Gnome_EventAlt_03.prefab:28ac6a5e172862048bae4c12cd89f582", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerAltTime_01.prefab:51ef876f95a55ea43a9d2387b884eec2", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerAltTime_02.prefab:39d42e44e0b1acd449cca096bc98b081", "VO_GILA_BOSS_48h_Female_Gnome_EventTimeWarp_01.prefab:fd660745420ca7f48b501e5c120a65fd",
			"VO_GILA_BOSS_48h_Female_Gnome_EventTimeWarp_02.prefab:813e9352f0e657945aa521a261863c93", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarpToki_01.prefab:a524136f9ebce1744adbfc7b0958ab57", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarpToki_02.prefab:6f2d69fe3da5d6a4ca248554cc6c98df", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarp_01.prefab:483fbbd189ec7de46adbbf0b2328abd6", "VO_GILA_BOSS_48h_Female_Gnome_EventPlayerTimeWarp_02.prefab:ccf2d22d428cfbf489cb4a316cdc56b9", "VO_GILA_BOSS_48h_Female_Gnome_EventTurn2_01.prefab:01cb517eae8bb3348998cad51c11b852", "VO_GILA_BOSS_48h_Female_Gnome_EventTurn4_01.prefab:5e7d08c465247eb429a85b2c801289e0", "VO_GILA_BOSS_48h_Female_Gnome_EventTurn6_01.prefab:a9d92540402e85342b3e744c002ec395", "VO_GILA_900h_Female_Gnome_EVENT_NEMESIS_INTRO_B_01.prefab:e2dcd279f96046943b8f9e84ddf05e1e", "VO_GILA_900h_Female_Gnome_EVENT_NEMESIS_TURN2_02.prefab:bc3fdc2e58660c2488283fe53df5a36a",
			"VO_GILA_900h_Female_Gnome_EVENT_NEMESIS_TURN6_01.prefab:dff37aa4d569154418338aab31cdaf95"
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
			string soundPath = m_RandomIntroLines[Random.Range(0, m_RandomIntroLines.Count)];
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(soundPath, Notification.SpeechBubbleDirection.TopRight, actor));
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_48h_Female_Gnome_EmoteResponse_01.prefab:7a5cb51ef214a804fae3c933aae63b57", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_48h_Female_Gnome_Death_01.prefab:081f92f8c5206444c8058b5c38a0beb2";
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "UNG_028t" && m_RandomBossTimeWarpLines.Count != 0)
			{
				string text = m_RandomBossTimeWarpLines[Random.Range(0, m_RandomBossTimeWarpLines.Count)];
				m_RandomBossTimeWarpLines.Remove(text);
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string> { "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_01.prefab:24a5c14cf0facb4469c7b66c58d5ebec", "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_02.prefab:128b3e847fdec794691485008c6e798f", "VO_GILA_BOSS_48h_Female_Gnome_HeroPower_03.prefab:8d8202e1499fa1d4380f3eb60a7b4e45" };
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "UNG_028t" && m_RandomPlayerTimeWarpLines.Count != 0)
			{
				string text = m_RandomPlayerTimeWarpLines[Random.Range(0, m_RandomPlayerTimeWarpLines.Count)];
				m_RandomPlayerTimeWarpLines.Remove(text);
				yield return PlayLineOnlyOnce(actor, text);
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, "VO_GILA_900h_Female_Gnome_EVENT_NEMESIS_TURN2_02.prefab:bc3fdc2e58660c2488283fe53df5a36a");
			yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_48h_Female_Gnome_EventTurn2_01.prefab:01cb517eae8bb3348998cad51c11b852");
			GameState.Get().SetBusy(busy: false);
			break;
		case 4:
			yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_48h_Female_Gnome_EventTurn4_01.prefab:5e7d08c465247eb429a85b2c801289e0");
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, "VO_GILA_900h_Female_Gnome_EVENT_NEMESIS_TURN6_01.prefab:dff37aa4d569154418338aab31cdaf95");
			yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_48h_Female_Gnome_EventTurn6_01.prefab:a9d92540402e85342b3e744c002ec395");
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	protected override IEnumerator RespondToResetGameFinishedWithTiming(Entity entity)
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (entity.GetControllerSide() == Player.Side.FRIENDLY)
		{
			string text = cardID;
			if (!(text == "GILA_900p"))
			{
				if (text == "GILA_BOSS_48t" && m_RandomPlayerAltTimeLines.Count != 0)
				{
					string text2 = m_RandomPlayerAltTimeLines[Random.Range(0, m_RandomPlayerAltTimeLines.Count)];
					m_RandomPlayerAltTimeLines.Remove(text2);
					yield return PlayLineOnlyOnce(enemyActor, text2);
				}
			}
			else
			{
				string text3 = PopRandomLineWithChance(m_RandomPlayerHeroPowerLines);
				if (text3 != null)
				{
					yield return PlayLineOnlyOnce(enemyActor, text3);
				}
			}
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING && cardID == "GILA_BOSS_48t")
		{
			string text4 = PopRandomLineWithChance(m_RandomBossAltTimeLines);
			if (text4 != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, text4);
			}
		}
	}
}
