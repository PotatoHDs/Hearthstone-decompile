using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TB_Blizzcon_2016 : MissionEntity
{
	private enum MATCHUP
	{
		KABALVLOTUS,
		KABALVGOONS,
		GOONSVLOTUS,
		ERROR
	}

	private enum BOSS
	{
		HAN,
		CHO,
		AYA,
		KAZAKUS
	}

	private enum VICTOR
	{
		GOONSBEATKABAL,
		GOONSBEATLOTUS,
		LOTUSBEATKABAL,
		LOTUSBEATGOONS,
		KABALBEATGOONS,
		KABALBEATLOTUS,
		ERROR
	}

	private Card m_bossCard;

	private Card m_mediva;

	private bool[] hasUsedLine;

	private int currentTurnsWOEmote;

	private int emoteTurnsLimit = 7;

	private bool emoteThisTurn;

	private List<int> priorityLines;

	private bool hasPlayedMatchupTriggerGoons;

	private bool hasPlayedMatchupTriggerLotus;

	private bool hasPlayedMatchupTriggerKabal;

	private MATCHUP currentMatchup = MATCHUP.ERROR;

	private VICTOR matchResult;

	private TAG_CLASS lotusHero = TAG_CLASS.DRUID;

	private TAG_CLASS kabalHero = TAG_CLASS.PRIEST;

	private TAG_CLASS goonsHero = TAG_CLASS.PALADIN;

	private string grimyGoonsName = GameStrings.Get("GLOBAL_KEYWORD_GRIMY_GOONS");

	private string jadeLotusName = GameStrings.Get("GLOBAL_KEYWORD_JADE_LOTUS");

	private string kabalName = GameStrings.Get("GLOBAL_KEYWORD_KABAL");

	private TAG_CLASS firstPlayerHero;

	private TAG_CLASS secondPlayerHero;

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (currentMatchup == MATCHUP.ERROR)
		{
			currentMatchup = GetBrawlHeroes();
		}
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			if (firstPlayerHero == goonsHero && secondPlayerHero == lotusHero)
			{
				matchResult = VICTOR.GOONSBEATLOTUS;
			}
			else if (firstPlayerHero == goonsHero && secondPlayerHero == kabalHero)
			{
				matchResult = VICTOR.GOONSBEATKABAL;
			}
			else if (firstPlayerHero == lotusHero && secondPlayerHero == kabalHero)
			{
				matchResult = VICTOR.LOTUSBEATKABAL;
			}
			else if (firstPlayerHero == lotusHero && secondPlayerHero == goonsHero)
			{
				matchResult = VICTOR.LOTUSBEATGOONS;
			}
			else if (firstPlayerHero == kabalHero && secondPlayerHero == lotusHero)
			{
				matchResult = VICTOR.KABALBEATLOTUS;
			}
			else if (firstPlayerHero == kabalHero && secondPlayerHero == goonsHero)
			{
				matchResult = VICTOR.KABALBEATGOONS;
			}
			break;
		case TAG_PLAYSTATE.LOST:
			if (firstPlayerHero == goonsHero && secondPlayerHero == lotusHero)
			{
				matchResult = VICTOR.LOTUSBEATGOONS;
			}
			else if (firstPlayerHero == goonsHero && secondPlayerHero == kabalHero)
			{
				matchResult = VICTOR.KABALBEATGOONS;
			}
			else if (firstPlayerHero == lotusHero && secondPlayerHero == kabalHero)
			{
				matchResult = VICTOR.KABALBEATLOTUS;
			}
			else if (firstPlayerHero == lotusHero && secondPlayerHero == goonsHero)
			{
				matchResult = VICTOR.GOONSBEATLOTUS;
			}
			else if (firstPlayerHero == kabalHero && secondPlayerHero == lotusHero)
			{
				matchResult = VICTOR.LOTUSBEATKABAL;
			}
			else if (firstPlayerHero == kabalHero && secondPlayerHero == goonsHero)
			{
				matchResult = VICTOR.GOONSBEATKABAL;
			}
			break;
		case TAG_PLAYSTATE.TIED:
			matchResult = VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	private MATCHUP GetBrawlHeroes()
	{
		firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetClass();
		secondPlayerHero = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetClass();
		if ((firstPlayerHero == goonsHero && secondPlayerHero == lotusHero) || (firstPlayerHero == lotusHero && secondPlayerHero == goonsHero))
		{
			return MATCHUP.GOONSVLOTUS;
		}
		if ((firstPlayerHero == goonsHero && secondPlayerHero == kabalHero) || (firstPlayerHero == kabalHero && secondPlayerHero == goonsHero))
		{
			return MATCHUP.KABALVGOONS;
		}
		if ((firstPlayerHero == kabalHero && secondPlayerHero == lotusHero) || (firstPlayerHero == lotusHero && secondPlayerHero == kabalHero))
		{
			return MATCHUP.KABALVLOTUS;
		}
		Debug.LogError("Matchup is not as predicted. Should be only one of each hero as defined in TB_Blizzcon_2016.cs");
		return MATCHUP.ERROR;
	}

	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		int num = Random.Range(0, 3);
		if (heroCard.GetEntity().GetClass() == lotusHero)
		{
			switch (num)
			{
			case 0:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_01.prefab:6bc8a6bd85078984db14131a67029b04");
			case 1:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_02.prefab:c5c53500c23c1f744bdca5c5a3cbdc04");
			case 2:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_03.prefab:e98c84c7bd6010147a3c13956a6e0ece");
			}
		}
		else if (heroCard.GetEntity().GetClass() == goonsHero)
		{
			switch (num)
			{
			case 0:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_01.prefab:24bae69ce396c7947b1018b1976de679");
			case 1:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_02.prefab:ab33516e41188c248ae90f6cb17067fa");
			case 2:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_03.prefab:91ddf3c0c5e8d9a4ea1089fa346e5518");
			}
		}
		else if (heroCard.GetEntity().GetClass() == kabalHero)
		{
			switch (num)
			{
			case 0:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_01.prefab:ea1633a8b77d5fb4888b9214519c12ec");
			case 1:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_02.prefab:c4ea297c7eca6874c874b35aa7f2a018");
			case 2:
				return GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_03.prefab:e43f5a1cc60008940a77161a3c635594");
			}
		}
		return base.GetAnnouncerLine(heroCard, type);
	}

	public override void PreloadAssets()
	{
		hasUsedLine = new bool[1000];
		SetupPriorityLines();
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_1st_Turn_Start_01.prefab:8be17b0dae8da254eb58a9666a008b63");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_2nd_Turn_Start_01.prefab:e5a44c5a2dfa9434a964bcc7da4b0d75");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Fill1_01.prefab:4be280fcdd3c5594aaf9125ab43be6b6");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Win_01.prefab:190589e48de533a42b90b5e170ce326d");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Winning_01.prefab:4ee560db3543f9f43a6c881f28d6e854");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Win_01.prefab:c94334b89fcf4ea4e81d0ec930e1d051");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Winning_01.prefab:7f8845179eedd5c4bac313cddef8bbe9");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_1st_Turn_Start_01.prefab:d686738dd65850e44a48e7c443c25263");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill1_01.prefab:c9cf61ce3a401064e97ccf4fafa3cff5");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill2_01.prefab:2e806195d67655141a710924c5016991");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_01.prefab:8e0079b4e73c2554d802a0ac39ba45d7");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_02.prefab:7cbbc6dbc527f994ea54a996ae75d525");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Winning_01.prefab:6c63549c3cb661d46aa47ea970217c61");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Wins_01.prefab:422826db6426bda4783fc3a36d113ee8");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Wins_01.prefab:5d9ad80e62f78404daab599afb8bdc60");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_01.prefab:6e0fe3ddd39cd424caf7369da719cd7c");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_02.prefab:4add1f34caa4c6841ae5687bc9f9a5a7");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_04.prefab:ac2104b0a0ece5240a320b4ba0968768");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_05.prefab:5bd45b4a07027974da3ad4a4b083a4b2");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem3_01.prefab:162f176395aab6b4aaf471fd549d5b40");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem2_01.prefab:92610ca6cb9ab2c4fbc8d64a77b7fa5e");
		PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Arms_Dealing1_01.prefab:b93994bc7cd20b64caf25f092eb3e765");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_1st_Turn_Start_01.prefab:854d26b8a7627264ba091c0b632258f2");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_2nd_Turn_Start_01.prefab:81a12a2b061dfb149a8f2162dd9dd251");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Fill1_01.prefab:40a180e56c205c24fb70dd84c728f801");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Win_01.prefab:f59689a83886afb4aab053e6aa91ff3c");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Winning_02.prefab:1eb57a70b5a6f194c9b772c4c925da39");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Jade_Win_01.prefab:535dd787d222bc54e8c0372766751823");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Lotus_Winning_01.prefab:3512d5e4b0171f2499a5ac93d747af49");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_01.prefab:19367564a9bc4a14da1e9d2d2d06123d");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_02.prefab:de509aa2f0a49a243b9d23d70fb13168");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_2nd_Turn_Start_01.prefab:2ccf4699aa4d28e45b8c95ff79bf52f9");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Fill1_01.prefab:595b4d84ba3e55848a54388b450d40e1");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Fill2_01.prefab:f2c96720bb2daab4b87fcb8f1c663ae9");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Winning_01.prefab:e921d0ea7f239b74d8bbda8a77a7d6f6");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Wins_01.prefab:796da1c6fde86d046ba242430f5a7be4");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Winning_01.prefab:8309a83a5893c4145bd3d4fec40af94a");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Wins_01.prefab:ecdd497a01ea5f04fb369ec6e57efd76");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_02.prefab:da9bc8ef3fcddeb468c96ccf75ffc8e1");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_03.prefab:24ffb25cc6e6fe340afd95ddfb992823");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_04.prefab:32983d85d0e8f3e4fb4836444f320c60");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Jade_Golem3_01.prefab:a21d7215fef07cf4a870b6ec136c68f6");
		PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Arms_Dealing1_01.prefab:9388a49095971ee499e925c1385093b9");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:023f51537ec053c48b06d3003134c0a9");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_2nd_Turn_Start_02.prefab:fc8234cf7c6471a45b3f21e4c162ef2c");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Fill1_01.prefab:a6d42874b7ada1246883d6e150bcfcd4");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Fill2_01.prefab:3a38dd655d3d83542a6303ebbf5bd553");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Win_01.prefab:88e0a1afa65e143488d60a26451a54fe");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Winning_01.prefab:8e5e4d9dc062c0248bf4a31346c17817");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Win_02.prefab:ebc7a428c00acbf48802392c8af54462");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Winning_02.prefab:df4ff489a629a2642a6842494a6c2692");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_01.prefab:3bd5274c23af23c42ab9a2dc3de1bfc2");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_04.prefab:afdb7eef29038ce41802afe04f44371f");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Fill1_01.prefab:6bd403211b76afd448ad6eeb4fb0e9c8");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Win_01.prefab:dad027263619ad24ebab9d8ffec68436");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Winning_01.prefab:26c3e3ecb0645bf43a882631bc1d69a2");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Win_01.prefab:147a1660b03fa0c4dba706ba274c420e");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Winning_01.prefab:ab969d07a5b38ad45b6adf5d0ebd862e");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Jade_Golem2_01.prefab:65b6678983cd69a49b341ec8ace0a72c");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_01.prefab:bdb1e3559c2b97c4bb9cdb78ebf19139");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_02.prefab:bf052509477065a48a760a5233513894");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_03.prefab:e0c61e1383fa32644ba6651b2ed842e1");
		PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_04.prefab:aa45ac942e6197142a76054d72014a59");
		PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:cb463e997b8fafe48941c06e749d30bd");
		PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_1_Fill2_01.prefab:cfb6da4eb23272f40be8d9b2220ddb11");
		PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_2_Fill1_01.prefab:e61f0b71bb9160547a872e8aa866486c");
		PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_01.prefab:24bae69ce396c7947b1018b1976de679");
		PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_02.prefab:ab33516e41188c248ae90f6cb17067fa");
		PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_03.prefab:91ddf3c0c5e8d9a4ea1089fa346e5518");
		PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_01.prefab:6bc8a6bd85078984db14131a67029b04");
		PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_02.prefab:c5c53500c23c1f744bdca5c5a3cbdc04");
		PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_03.prefab:e98c84c7bd6010147a3c13956a6e0ece");
		PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_01.prefab:ea1633a8b77d5fb4888b9214519c12ec");
		PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_02.prefab:c4ea297c7eca6874c874b35aa7f2a018");
		PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_03.prefab:e43f5a1cc60008940a77161a3c635594");
	}

	private void SetupPriorityLines()
	{
		priorityLines = new List<int>();
		priorityLines.Add(0);
		priorityLines.Add(102);
		priorityLines.Add(103);
		priorityLines.Add(202);
		priorityLines.Add(203);
		priorityLines.Add(302);
		priorityLines.Add(303);
	}

	private Vector3 GetPositionForBoss(BOSS boss)
	{
		firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetClass();
		switch (boss)
		{
		case BOSS.HAN:
		case BOSS.CHO:
			if (firstPlayerHero == goonsHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		case BOSS.AYA:
			if (firstPlayerHero == lotusHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		case BOSS.KAZAKUS:
			if (firstPlayerHero == kabalHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		default:
			return NotificationManager.DEFAULT_CHARACTER_POS;
		}
	}

	private Notification.SpeechBubbleDirection GetBubbleDirectionForBoss(BOSS boss)
	{
		firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetClass();
		switch (boss)
		{
		case BOSS.HAN:
			if (firstPlayerHero == goonsHero)
			{
				return Notification.SpeechBubbleDirection.BottomRight;
			}
			return Notification.SpeechBubbleDirection.TopRight;
		case BOSS.CHO:
			if (firstPlayerHero == goonsHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		case BOSS.AYA:
			if (firstPlayerHero == lotusHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		case BOSS.KAZAKUS:
			if (firstPlayerHero == kabalHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		default:
			return Notification.SpeechBubbleDirection.BottomLeft;
		}
	}

	private IEnumerator PlayBossLine(BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection bubbleDirectionForBoss = GetBubbleDirectionForBoss(boss);
		Vector3 positionForBoss = GetPositionForBoss(boss);
		switch (boss)
		{
		case BOSS.HAN:
		case BOSS.CHO:
			yield return PlayMissionFlavorLine("HanCho_Temp_BigQuote.prefab:7a7804f8f47064946bdbcfd3b78d0dac", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		case BOSS.AYA:
			yield return PlayMissionFlavorLine("Aya_Temp_BigQuote.prefab:faa6811234b5e2e40b2447c7878616fe", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		case BOSS.KAZAKUS:
			yield return PlayMissionFlavorLine("Kazakus_Temp_BigQuote.prefab:9d330c5f45374254181cb923722b973d", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		}
		emoteThisTurn = true;
		currentTurnsWOEmote = 0;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (currentMatchup == MATCHUP.ERROR)
		{
			currentMatchup = GetBrawlHeroes();
		}
		if ((emoteThisTurn && !priorityLines.Contains(missionEvent)) || missionEvent == 1)
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 0:
			currentTurnsWOEmote++;
			Debug.Log(currentTurnsWOEmote);
			emoteThisTurn = false;
			if (currentTurnsWOEmote >= emoteTurnsLimit)
			{
				Gameplay.Get().StartCoroutine(PlayFillLine());
			}
			break;
		case 100:
			if (!hasUsedLine[100])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_1st_Turn_Start_01.prefab:8be17b0dae8da254eb58a9666a008b63");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:023f51537ec053c48b06d3003134c0a9", persistCharacter: true);
				yield return PlayBossLine(BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:cb463e997b8fafe48941c06e749d30bd");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[100] = true;
			}
			break;
		case 101:
			if (!hasUsedLine[101])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_2nd_Turn_Start_01.prefab:e5a44c5a2dfa9434a964bcc7da4b0d75");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_2nd_Turn_Start_02.prefab:fc8234cf7c6471a45b3f21e4c162ef2c");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[101] = true;
			}
			break;
		case 102:
			if (!hasUsedLine[102])
			{
				matchResult = VICTOR.GOONSBEATKABAL;
			}
			break;
		case 103:
			if (!hasUsedLine[103])
			{
				matchResult = VICTOR.KABALBEATGOONS;
			}
			break;
		case 104:
			if (!hasUsedLine[104])
			{
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Winning_01.prefab:8e5e4d9dc062c0248bf4a31346c17817");
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Winning_01.prefab:4ee560db3543f9f43a6c881f28d6e854");
				hasUsedLine[104] = true;
			}
			break;
		case 105:
			if (!hasUsedLine[105])
			{
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Winning_01.prefab:7f8845179eedd5c4bac313cddef8bbe9");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Winning_02.prefab:df4ff489a629a2642a6842494a6c2692");
				hasUsedLine[105] = true;
			}
			break;
		case 150:
		{
			int num5 = Random.Range(0, 4);
			if (!hasPlayedMatchupTriggerKabal)
			{
				num5 = Random.Range(0, 7);
			}
			Debug.Log("Potion Trigger. Random value = " + num5);
			GameState.Get().SetBusy(busy: true);
			switch (num5)
			{
			case 0:
				if (!hasUsedLine[150])
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_01.prefab:6e0fe3ddd39cd424caf7369da719cd7c");
					hasUsedLine[150] = true;
				}
				break;
			case 1:
				if (!hasUsedLine[151])
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_02.prefab:4add1f34caa4c6841ae5687bc9f9a5a7");
					hasUsedLine[151] = true;
				}
				break;
			case 2:
				if (!hasUsedLine[152])
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_04.prefab:ac2104b0a0ece5240a320b4ba0968768");
					hasUsedLine[152] = true;
				}
				break;
			case 3:
				if (!hasUsedLine[153])
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_05.prefab:5bd45b4a07027974da3ad4a4b083a4b2");
					hasUsedLine[153] = true;
				}
				break;
			case 4:
			case 5:
			case 6:
			case 7:
				if (currentMatchup == MATCHUP.KABALVGOONS)
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8");
					hasPlayedMatchupTriggerKabal = true;
				}
				else if (currentMatchup == MATCHUP.KABALVLOTUS)
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307");
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa");
					hasPlayedMatchupTriggerKabal = true;
				}
				break;
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 160:
		{
			int num3 = Random.Range(0, 2);
			GameState.Get().SetBusy(busy: true);
			if (num3 == 1 && !hasUsedLine[161])
			{
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8");
				hasUsedLine[161] = true;
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 170:
		{
			int num4 = Random.Range(0, 2);
			GameState.Get().SetBusy(busy: true);
			if (num4 == 0 && !hasUsedLine[170])
			{
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa");
				hasUsedLine[170] = true;
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 200:
			if (!hasUsedLine[200])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_01.prefab:19367564a9bc4a14da1e9d2d2d06123d");
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_1st_Turn_Start_01.prefab:d686738dd65850e44a48e7c443c25263");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_02.prefab:de509aa2f0a49a243b9d23d70fb13168");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[200] = true;
			}
			break;
		case 201:
			if (!hasUsedLine[201])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem2_01.prefab:92610ca6cb9ab2c4fbc8d64a77b7fa5e");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_2nd_Turn_Start_01.prefab:2ccf4699aa4d28e45b8c95ff79bf52f9");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[201] = true;
			}
			break;
		case 202:
			if (!hasUsedLine[202])
			{
				matchResult = VICTOR.KABALBEATLOTUS;
			}
			break;
		case 203:
			if (!hasUsedLine[203])
			{
				matchResult = VICTOR.LOTUSBEATKABAL;
			}
			break;
		case 204:
			if (!hasUsedLine[204])
			{
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_01.prefab:8e0079b4e73c2554d802a0ac39ba45d7");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Winning_01.prefab:e921d0ea7f239b74d8bbda8a77a7d6f6");
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_02.prefab:7cbbc6dbc527f994ea54a996ae75d525");
				hasUsedLine[204] = true;
			}
			break;
		case 205:
			if (!hasUsedLine[205])
			{
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Winning_01.prefab:8309a83a5893c4145bd3d4fec40af94a");
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Winning_01.prefab:6c63549c3cb661d46aa47ea970217c61");
				hasUsedLine[205] = true;
			}
			break;
		case 250:
		{
			int num2 = Random.Range(0, 3);
			if (!hasPlayedMatchupTriggerLotus)
			{
				num2 = Random.Range(0, 6);
			}
			Debug.Log("Jade Golem Trigger. Random value = " + num2);
			GameState.Get().SetBusy(busy: true);
			switch (num2)
			{
			case 0:
				if (!hasUsedLine[250])
				{
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_02.prefab:da9bc8ef3fcddeb468c96ccf75ffc8e1");
					hasUsedLine[250] = true;
				}
				break;
			case 1:
				if (!hasUsedLine[251])
				{
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_03.prefab:24ffb25cc6e6fe340afd95ddfb992823");
					hasUsedLine[251] = true;
				}
				break;
			case 2:
				if (!hasUsedLine[252])
				{
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_04.prefab:32983d85d0e8f3e4fb4836444f320c60");
					hasUsedLine[252] = true;
				}
				break;
			case 3:
			case 4:
			case 5:
				if (currentMatchup == MATCHUP.GOONSVLOTUS)
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Jade_Golem2_01.prefab:65b6678983cd69a49b341ec8ace0a72c");
					hasPlayedMatchupTriggerLotus = true;
				}
				else if (currentMatchup == MATCHUP.KABALVLOTUS)
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem3_01.prefab:162f176395aab6b4aaf471fd549d5b40");
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Jade_Golem3_01.prefab:a21d7215fef07cf4a870b6ec136c68f6");
					hasPlayedMatchupTriggerLotus = true;
				}
				break;
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 300:
			if (!hasUsedLine[300])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_01.prefab:3bd5274c23af23c42ab9a2dc3de1bfc2");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_1st_Turn_Start_01.prefab:854d26b8a7627264ba091c0b632258f2");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[300] = true;
			}
			break;
		case 301:
			if (!hasUsedLine[301])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_2nd_Turn_Start_01.prefab:81a12a2b061dfb149a8f2162dd9dd251");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_04.prefab:afdb7eef29038ce41802afe04f44371f");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[301] = true;
			}
			break;
		case 302:
			if (!hasUsedLine[302])
			{
				matchResult = VICTOR.LOTUSBEATGOONS;
			}
			break;
		case 303:
			if (!hasUsedLine[303])
			{
				matchResult = VICTOR.GOONSBEATLOTUS;
			}
			break;
		case 304:
			if (!hasUsedLine[304])
			{
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Lotus_Winning_01.prefab:3512d5e4b0171f2499a5ac93d747af49");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Winning_01.prefab:ab969d07a5b38ad45b6adf5d0ebd862e");
				hasUsedLine[304] = true;
			}
			break;
		case 305:
			if (!hasUsedLine[305])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Winning_01.prefab:26c3e3ecb0645bf43a882631bc1d69a2");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Winning_02.prefab:1eb57a70b5a6f194c9b772c4c925da39");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[305] = true;
			}
			break;
		case 350:
		{
			int num = Random.Range(0, 4);
			if (!hasPlayedMatchupTriggerGoons)
			{
				num = Random.Range(0, 8);
			}
			Debug.Log("Arms Dealing Trigger. Random value = " + num);
			GameState.Get().SetBusy(busy: true);
			switch (num)
			{
			case 0:
				if (!hasUsedLine[350])
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_01.prefab:bdb1e3559c2b97c4bb9cdb78ebf19139");
					hasUsedLine[350] = true;
				}
				break;
			case 1:
				if (!hasUsedLine[351])
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_02.prefab:bf052509477065a48a760a5233513894");
					hasUsedLine[351] = true;
				}
				break;
			case 2:
				if (!hasUsedLine[352])
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_03.prefab:e0c61e1383fa32644ba6651b2ed842e1");
					hasUsedLine[352] = true;
				}
				break;
			case 3:
				if (!hasUsedLine[353])
				{
					yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_04.prefab:aa45ac942e6197142a76054d72014a59");
					hasUsedLine[353] = true;
				}
				break;
			case 4:
			case 5:
			case 6:
			case 7:
				if (currentMatchup == MATCHUP.GOONSVLOTUS)
				{
					yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Arms_Dealing1_01.prefab:9388a49095971ee499e925c1385093b9");
					hasPlayedMatchupTriggerGoons = true;
				}
				else if (currentMatchup == MATCHUP.KABALVGOONS)
				{
					yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Arms_Dealing1_01.prefab:b93994bc7cd20b64caf25f092eb3e765");
					hasPlayedMatchupTriggerGoons = true;
				}
				break;
			}
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 999:
			SetNamePlate();
			break;
		}
	}

	private void SetNamePlate()
	{
		TAG_CLASS @class = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetClass();
		TAG_CLASS class2 = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetClass();
		switch (@class)
		{
		case TAG_CLASS.PALADIN:
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(grimyGoonsName);
			break;
		case TAG_CLASS.PRIEST:
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(kabalName);
			break;
		case TAG_CLASS.DRUID:
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(jadeLotusName);
			break;
		default:
			Debug.Log("Incorrect class found in SetNamePlate()");
			break;
		}
		switch (class2)
		{
		case TAG_CLASS.PALADIN:
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(grimyGoonsName);
			break;
		case TAG_CLASS.PRIEST:
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(kabalName);
			break;
		case TAG_CLASS.DRUID:
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(jadeLotusName);
			break;
		default:
			Debug.Log("Incorrect class found in SetNamePlate()");
			break;
		}
	}

	private IEnumerator PlayFillLine()
	{
		if (!hasUsedLine[101] && !hasUsedLine[201] && !hasUsedLine[301])
		{
			yield break;
		}
		bool flag = (((double)Random.value < 0.5) ? true : false);
		switch (currentMatchup)
		{
		case MATCHUP.GOONSVLOTUS:
			if (!hasUsedLine[90])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Fill1_01.prefab:40a180e56c205c24fb70dd84c728f801");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Fill1_01.prefab:6bd403211b76afd448ad6eeb4fb0e9c8", persistCharacter: true);
				yield return PlayBossLine(BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_2_Fill1_01.prefab:e61f0b71bb9160547a872e8aa866486c");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[90] = true;
			}
			break;
		case MATCHUP.KABALVLOTUS:
			if (!hasUsedLine[80] && flag)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill2_01.prefab:2e806195d67655141a710924c5016991");
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Fill2_01.prefab:f2c96720bb2daab4b87fcb8f1c663ae9");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[80] = true;
			}
			else if (!hasUsedLine[81])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Fill1_01.prefab:595b4d84ba3e55848a54388b450d40e1");
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill1_01.prefab:c9cf61ce3a401064e97ccf4fafa3cff5");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[81] = true;
			}
			break;
		case MATCHUP.KABALVGOONS:
			if (!hasUsedLine[70] && flag)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Fill1_01.prefab:4be280fcdd3c5594aaf9125ab43be6b6");
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Fill1_01.prefab:a6d42874b7ada1246883d6e150bcfcd4");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[70] = true;
			}
			else if (!hasUsedLine[71])
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Fill2_01.prefab:3a38dd655d3d83542a6303ebbf5bd553", persistCharacter: true);
				yield return PlayBossLine(BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_1_Fill2_01.prefab:cfb6da4eb23272f40be8d9b2220ddb11");
				GameState.Get().SetBusy(busy: false);
				hasUsedLine[71] = true;
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		switch (matchResult)
		{
		case VICTOR.GOONSBEATKABAL:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Win_01.prefab:190589e48de533a42b90b5e170ce326d");
			yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Win_01.prefab:88e0a1afa65e143488d60a26451a54fe");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[102] = true;
			break;
		case VICTOR.GOONSBEATLOTUS:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Win_01.prefab:f59689a83886afb4aab053e6aa91ff3c");
			yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Win_01.prefab:dad027263619ad24ebab9d8ffec68436");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[303] = true;
			break;
		case VICTOR.KABALBEATGOONS:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Win_02.prefab:ebc7a428c00acbf48802392c8af54462");
			yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Win_01.prefab:c94334b89fcf4ea4e81d0ec930e1d051");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[103] = true;
			break;
		case VICTOR.KABALBEATLOTUS:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Wins_01.prefab:5d9ad80e62f78404daab599afb8bdc60");
			yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Wins_01.prefab:796da1c6fde86d046ba242430f5a7be4");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[202] = true;
			break;
		case VICTOR.LOTUSBEATGOONS:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Jade_Win_01.prefab:535dd787d222bc54e8c0372766751823");
			yield return PlayBossLine(BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Win_01.prefab:147a1660b03fa0c4dba706ba274c420e");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[302] = true;
			break;
		case VICTOR.LOTUSBEATKABAL:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Wins_01.prefab:ecdd497a01ea5f04fb369ec6e57efd76");
			yield return PlayBossLine(BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Wins_01.prefab:422826db6426bda4783fc3a36d113ee8");
			GameState.Get().SetBusy(busy: false);
			hasUsedLine[203] = true;
			break;
		}
	}
}
