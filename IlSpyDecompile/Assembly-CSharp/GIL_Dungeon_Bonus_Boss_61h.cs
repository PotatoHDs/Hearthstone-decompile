using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIL_Dungeon_Bonus_Boss_61h : GIL_Dungeon
{
	public const string TESS_HERO = "GILA_500h4";

	public const string DARIUS_HERO = "GILA_600h2";

	public const string TOKI_HERO = "GILA_900h2";

	public const string SHAW_HERO = "GILA_400h";

	private HashSet<string> m_playedLines = new HashSet<string>();

	private bool hasPlayedTessTurnLine1;

	private bool hasPlayedTessTurnLine2;

	private bool hasPlayedDariusTurnLine1;

	private bool hasPlayedDariusTurnLine2;

	private bool hasPlayedTokiTurnLine1;

	private bool hasPlayedTokiTurnLine2;

	private bool hasPlayedShawTurnLine1;

	private bool hasPlayedShawTurnLine2;

	private string randomLine;

	private List<string> m_TessPowerLines = new List<string> { "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_01.prefab:d57755e79970d8149b3aae9dcbc8bae6", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_02.prefab:8374c14abcd95964b96a470015d8d02d", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_03.prefab:e523d8d15ed98844980081271374cfdf", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_04.prefab:70af279046a19c045b4fb6e0024d7710" };

	private List<string> m_TokiPowerLines = new List<string> { "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_01.prefab:7a469c7edc89af04aa1371af82f7b7b2", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_03.prefab:40a46a8079442f249accc382187e0616", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_04.prefab:52732a46c2c27644dac3d9116e3ca47d", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_05.prefab:2725c1590918fc1459f3542f7f1911d1" };

	private List<string> m_DariusCannonLines = new List<string> { "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_01.prefab:0812eb3430eaef440939e80ef0d51cc6", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_02.prefab:bf261abe347499f41bbeaf026e794dd8", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_03.prefab:d2d067c9aee5d6c4fa366f9355afe827", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_04.prefab:6b2a2f4c2c790cb4cbcea8913caf5eca" };

	private List<string> m_ShawHoundLines = new List<string> { "VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_01.prefab:6900c0532641a1d4983aefa20416f62e", "VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_02.prefab:19fe27faa78b9d845b74339047a765ad" };

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_GILA_BOSS_61h_Female_Orc_IntroShaw_01.prefab:535335d8e2b34334a83374b62d03f750", "VO_GILA_BOSS_61h_Female_Orc_IntroTess_01.prefab:ffbef8020373f24478241e3ea517a6f2", "VO_GILA_BOSS_61h_Female_Orc_IntroCrowley_01.prefab:5f8fb394ab0494a48aff74ac1234ed6c", "VO_GILA_BOSS_61h_Female_Orc_IntroToki_01.prefab:bcb8a646f92003e4c9797323ddc44033", "VO_GILA_BOSS_61h_Female_Orc_EmoteResponse_01.prefab:bc6efd6494a855b419b46bf9643d4da6", "VO_GILA_BOSS_61h_Female_Orc_EmoteResponseTess_01.prefab:4c726930acd2ee44eb6eb33fce12f292", "VO_GILA_BOSS_61h_Female_Orc_EmoteResponseCrowley_01.prefab:4e95c68f2b8446049b05b91b866d803c", "VO_GILA_BOSS_61h_Female_Orc_EmoteResponseShaw_01.prefab:5b8a2ee25bd548849b1c6b20a7bd26e7", "VO_GILA_BOSS_61h_Female_Orc_EmoteResponseToki_01.prefab:7a7b36e367568ea408ccec1ff319360d", "VO_GILA_BOSS_61h_Female_Orc_Death_01.prefab:899a9fc58b40f764b93b4fd6312ce163",
			"VO_GILA_BOSS_61h_Female_Orc_DefeatPlayer_01.prefab:a9bd704b56a75d2409a12a4a74a2e314", "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn2_01.prefab:e8c971c27f987ad428bac32802db5a4d", "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn4_01.prefab:fd16b5c7e7fd68440a009ad805567ab4", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_01.prefab:d57755e79970d8149b3aae9dcbc8bae6", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_02.prefab:8374c14abcd95964b96a470015d8d02d", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_03.prefab:e523d8d15ed98844980081271374cfdf", "VO_GILA_BOSS_61h_Female_Orc_EventTessScavenges_04.prefab:70af279046a19c045b4fb6e0024d7710", "VO_GILA_BOSS_61h_Female_Orc_EventTessHagathaLow_01.prefab:e7c478d87d33ecc4b98f553226ae965b", "VO_GILA_BOSS_61h_Female_Orc_EventTessLow_01.prefab:590fb10488f3f3740869709826ab04e7", "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn2_01.prefab:1ab82acdfc1cb5a4bbb1b3e77bc33862",
			"VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn4_01.prefab:61033769f4ce13c439008b3c6b0681b6", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_01.prefab:0812eb3430eaef440939e80ef0d51cc6", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_02.prefab:bf261abe347499f41bbeaf026e794dd8", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_03.prefab:d2d067c9aee5d6c4fa366f9355afe827", "VO_GILA_BOSS_61h_Female_Orc_EventCannonKill_04.prefab:6b2a2f4c2c790cb4cbcea8913caf5eca", "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyHagathaLow_01.prefab:98c3f1146ba002e46b47978c1e2bc2a5", "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyLow_01.prefab:27ec9f0458cd3084d90caad86240eb46", "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_01.prefab:9995bfad8f2e73a48b3f06cc12845f59", "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_02.prefab:5f967425032b51a47958fc3cfdb76440", "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn4_01.prefab:aa0aceaee3a6e9a4ab1b647cc1c83f43",
			"VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_01.prefab:6900c0532641a1d4983aefa20416f62e", "VO_GILA_BOSS_61h_Female_Orc_EventShawHoundKill_02.prefab:19fe27faa78b9d845b74339047a765ad", "VO_GILA_BOSS_61h_Female_Orc_EventShawLow_01.prefab:6d08f00e0ff38144b815cb3c56143544", "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn2_01.prefab:c52baf63bce5dcc458b19b5489613787", "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn4_01.prefab:6e8ada4d386e47f429de1e95412509cf", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_01.prefab:7a469c7edc89af04aa1371af82f7b7b2", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_03.prefab:40a46a8079442f249accc382187e0616", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_04.prefab:52732a46c2c27644dac3d9116e3ca47d", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHeroPower_05.prefab:2725c1590918fc1459f3542f7f1911d1", "VO_GILA_BOSS_61h_Female_Orc_EventTokiHagathaLow_01.prefab:02966241e760b9548973e9c7800b1de4",
			"VO_GILA_BOSS_61h_Female_Orc_EventTokiLow_01.prefab:29636424871b53141a77efe8c0ea54a6", "VO_GILA_BOSS_61h_Female_Orc_EventSummonCrowskin_01.prefab:5b0211815822d664bab58ae89af6caf5", "VO_GILA_BOSS_61h_Female_Orc_EventSummonGodfrey_01.prefab:e7c1c99447f410146a6591b07cfe10ea", "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_SUB_IN_01.prefab:839ebea0d5b8ea14e938406369e5b872", "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN2_01.prefab:8841f3d2df72df64f8bf56c93dc6af71", "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN4_01.prefab:2ec7c605cae6f134bb91bb92abfb7c54", "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_WOUNDED_01.prefab:d4997ebee335fcc4eab92dd382e250aa", "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_SUB_IN_01.prefab:78cbcc19eca877a4f9af73ad8a2afd0b", "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN2_01.prefab:783d2a2edfcbce34cb2bc63bdcd3e826", "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN4_01.prefab:a0951c1397e730e4798a67757267aa8c",
			"VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_WOUNDED_01.prefab:942de80c17f6f934dae5ea383b24e1fa", "VO_GILA_400h_Male_Human_EVENT_HAGATHA_INTRO_01.prefab:aeeecbcb284c22847810924c4efab9dd", "VO_GILA_400h_Male_Human_EVENT_HAGATHA_TURN4_01.prefab:b36ade3a44d65fb4599d2cef127b0c2b", "VO_GILA_400h_Male_Human_EVENT_HAGATHA_LOWHEALTH_01.prefab:dee647cca1aeebb4792ef570f19ab1ff", "VO_GILA_400h_Male_Human_EVENT_HAGATHA_ALMOSTDEAD_02.prefab:0d614b21e4be49d41aee63cb5702a9d2", "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_SUB_IN_01.prefab:f8a2268225028954896396eaee3ed896", "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_TURN2_01.prefab:99821f787624bd448abb69b058de25a7", "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_WOUNDED_01.prefab:b9f2daf4ec0707348aa0206702a71e30", "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_ALMOST_DEAD_01.prefab:2e4efefea5deeb049ae03a4e868a7ef0"
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_IntroShaw_01.prefab:535335d8e2b34334a83374b62d03f750", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			switch (GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCardId())
			{
			case "GILA_500h4":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseTess_01.prefab:4c726930acd2ee44eb6eb33fce12f292", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "GILA_600h2":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseCrowley_01.prefab:4e95c68f2b8446049b05b91b866d803c", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "GILA_900h2":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseToki_01.prefab:7a7b36e367568ea408ccec1ff319360d", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "GILA_400h":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponseShaw_01.prefab:5b8a2ee25bd548849b1c6b20a7bd26e7", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			default:
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_GILA_BOSS_61h_Female_Orc_EmoteResponse_01.prefab:bc6efd6494a855b419b46bf9643d4da6", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
		}
	}

	protected override string GetBossDeathLine()
	{
		return "VO_GILA_BOSS_61h_Female_Orc_Death_01.prefab:899a9fc58b40f764b93b4fd6312ce163";
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "GIL_618"))
		{
			if (cardId == "GIL_825")
			{
				yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_EventSummonGodfrey_01.prefab:e7c1c99447f410146a6591b07cfe10ea");
			}
		}
		else
		{
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_EventSummonCrowskin_01.prefab:5b0211815822d664bab58ae89af6caf5");
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (cardId)
		{
		case "GILA_500h4":
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroTess_01.prefab:ffbef8020373f24478241e3ea517a6f2");
			break;
		case "GILA_600h2":
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroCrowley_01.prefab:5f8fb394ab0494a48aff74ac1234ed6c");
			break;
		case "GILA_900h2":
			yield return PlayLineOnlyOnce(actor, "VO_GILA_BOSS_61h_Female_Orc_IntroToki_01.prefab:bcb8a646f92003e4c9797323ddc44033");
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			randomLine = PopRandomLineWithChance(m_TessPowerLines);
			if (randomLine != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
			}
			break;
		case 102:
			randomLine = PopRandomLineWithChance(m_DariusCannonLines);
			if (randomLine != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
			}
			break;
		case 103:
			randomLine = PopRandomLineWithChance(m_ShawHoundLines);
			if (randomLine != null)
			{
				yield return PlayLineOnlyOnce(enemyActor, randomLine);
			}
			break;
		case 105:
			switch (playerActor.GetEntity().GetCardId())
			{
			case "GILA_500h4":
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessLow_01.prefab:590fb10488f3f3740869709826ab04e7");
				break;
			case "GILA_600h2":
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyLow_01.prefab:27ec9f0458cd3084d90caad86240eb46");
				break;
			case "GILA_900h2":
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiLow_01.prefab:29636424871b53141a77efe8c0ea54a6");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_ALMOST_DEAD_01.prefab:2e4efefea5deeb049ae03a4e868a7ef0");
				break;
			case "GILA_400h":
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawLow_01.prefab:6d08f00e0ff38144b815cb3c56143544");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_ALMOSTDEAD_02.prefab:0d614b21e4be49d41aee63cb5702a9d2");
				break;
			}
			break;
		case 106:
			switch (playerActor.GetEntity().GetCardId())
			{
			case "GILA_500h4":
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_WOUNDED_01.prefab:d4997ebee335fcc4eab92dd382e250aa");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessHagathaLow_01.prefab:e7c478d87d33ecc4b98f553226ae965b");
				break;
			case "GILA_600h2":
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyHagathaLow_01.prefab:98c3f1146ba002e46b47978c1e2bc2a5");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_WOUNDED_01.prefab:942de80c17f6f934dae5ea383b24e1fa");
				break;
			case "GILA_900h2":
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_WOUNDED_01.prefab:b9f2daf4ec0707348aa0206702a71e30");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiHagathaLow_01.prefab:02966241e760b9548973e9c7800b1de4");
				break;
			case "GILA_400h":
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_LOWHEALTH_01.prefab:dee647cca1aeebb4792ef570f19ab1ff");
				break;
			}
			break;
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
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		if (turn <= 4 || turn % 2 != 0)
		{
			yield break;
		}
		switch (playerActor.GetEntity().GetCardId())
		{
		case "GILA_500h4":
			if (!hasPlayedTessTurnLine1 && !hasPlayedTessTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn2_01.prefab:e8c971c27f987ad428bac32802db5a4d");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN2_01.prefab:8841f3d2df72df64f8bf56c93dc6af71");
				hasPlayedTessTurnLine1 = true;
				GameState.Get().SetBusy(busy: false);
			}
			else if (hasPlayedTessTurnLine1 && !hasPlayedTessTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_500h3_Female_Human_EVENT_HAGATHA_TURN4_01.prefab:2ec7c605cae6f134bb91bb92abfb7c54");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTessTurn4_01.prefab:fd16b5c7e7fd68440a009ad805567ab4");
				hasPlayedTessTurnLine2 = true;
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "GILA_600h2":
			if (!hasPlayedDariusTurnLine1 && !hasPlayedDariusTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN2_01.prefab:783d2a2edfcbce34cb2bc63bdcd3e826");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn2_01.prefab:1ab82acdfc1cb5a4bbb1b3e77bc33862");
				hasPlayedDariusTurnLine1 = true;
				GameState.Get().SetBusy(busy: false);
			}
			else if (hasPlayedDariusTurnLine1 && !hasPlayedDariusTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventCrowleyTurn4_01.prefab:61033769f4ce13c439008b3c6b0681b6");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_600h_Male_Worgen_EVENT_HAGATHA_TURN4_01.prefab:a0951c1397e730e4798a67757267aa8c");
				hasPlayedDariusTurnLine2 = true;
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "GILA_900h2":
			if (!hasPlayedTokiTurnLine1 && !hasPlayedTokiTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_900h_Female_Gnome_EVENT_HAGATHA_TURN2_01.prefab:99821f787624bd448abb69b058de25a7");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn2_01.prefab:c52baf63bce5dcc458b19b5489613787");
				hasPlayedTokiTurnLine1 = true;
				GameState.Get().SetBusy(busy: false);
			}
			else if (hasPlayedTokiTurnLine1 && !hasPlayedTokiTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventTokiTurn4_01.prefab:6e8ada4d386e47f429de1e95412509cf");
				hasPlayedTokiTurnLine2 = true;
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "GILA_400h":
			if (!hasPlayedShawTurnLine1 && !hasPlayedShawTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_01.prefab:9995bfad8f2e73a48b3f06cc12845f59");
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn2_02.prefab:5f967425032b51a47958fc3cfdb76440");
				hasPlayedShawTurnLine1 = true;
				GameState.Get().SetBusy(busy: false);
			}
			else if (hasPlayedShawTurnLine1 && !hasPlayedShawTurnLine2)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_GILA_BOSS_61h_Female_Orc_EventShawTurn4_01.prefab:aa0aceaee3a6e9a4ab1b647cc1c83f43");
				yield return PlayLineOnlyOnce(playerActor, "VO_GILA_400h_Male_Human_EVENT_HAGATHA_TURN4_01.prefab:b36ade3a44d65fb4599d2cef127b0c2b");
				hasPlayedShawTurnLine2 = true;
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_GILA_BOSS_61h_Female_Orc_DefeatPlayer_01.prefab:a9bd704b56a75d2409a12a4a74a2e314";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Hagatha_Banner_Quote.prefab:678033af721880948a86bc69b02ef1ac", soundPath));
			}
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
		if (m_playedLines.Contains(entity.GetCardId()) || GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor()
			.GetEntity()
			.GetCardId() != "GILA_900h2")
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (cardId == "GILA_900p")
		{
			randomLine = PopRandomLineWithChance(m_TokiPowerLines);
			if (randomLine != null)
			{
				yield return PlayLineOnlyOnce(actor, randomLine);
			}
		}
	}
}
