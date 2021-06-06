using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005A7 RID: 1447
public class TB_Blizzcon_2016 : MissionEntity
{
	// Token: 0x06005076 RID: 20598 RVA: 0x001A6CCC File Offset: 0x001A4ECC
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.ERROR)
		{
			this.currentMatchup = this.GetBrawlHeroes();
		}
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			if (this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.lotusHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATLOTUS;
			}
			else if (this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.kabalHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATKABAL;
			}
			else if (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.kabalHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATKABAL;
			}
			else if (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.goonsHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATGOONS;
			}
			else if (this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.lotusHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATLOTUS;
			}
			else if (this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.goonsHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATGOONS;
			}
			break;
		case TAG_PLAYSTATE.LOST:
			if (this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.lotusHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATGOONS;
			}
			else if (this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.kabalHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATGOONS;
			}
			else if (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.kabalHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATLOTUS;
			}
			else if (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.goonsHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATLOTUS;
			}
			else if (this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.lotusHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATKABAL;
			}
			else if (this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.goonsHero)
			{
				this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATKABAL;
			}
			break;
		case TAG_PLAYSTATE.TIED:
			this.matchResult = TB_Blizzcon_2016.VICTOR.ERROR;
			break;
		}
		base.NotifyOfGameOver(gameResult);
	}

	// Token: 0x06005077 RID: 20599 RVA: 0x001A6EF0 File Offset: 0x001A50F0
	private TB_Blizzcon_2016.MATCHUP GetBrawlHeroes()
	{
		this.firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero().GetClass();
		this.secondPlayerHero = GameState.Get().GetOpposingSidePlayer().GetHero().GetClass();
		if ((this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.lotusHero) || (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.goonsHero))
		{
			return TB_Blizzcon_2016.MATCHUP.GOONSVLOTUS;
		}
		if ((this.firstPlayerHero == this.goonsHero && this.secondPlayerHero == this.kabalHero) || (this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.goonsHero))
		{
			return TB_Blizzcon_2016.MATCHUP.KABALVGOONS;
		}
		if ((this.firstPlayerHero == this.kabalHero && this.secondPlayerHero == this.lotusHero) || (this.firstPlayerHero == this.lotusHero && this.secondPlayerHero == this.kabalHero))
		{
			return TB_Blizzcon_2016.MATCHUP.KABALVLOTUS;
		}
		Debug.LogError("Matchup is not as predicted. Should be only one of each hero as defined in TB_Blizzcon_2016.cs");
		return TB_Blizzcon_2016.MATCHUP.ERROR;
	}

	// Token: 0x06005078 RID: 20600 RVA: 0x001A6FEC File Offset: 0x001A51EC
	public override AudioSource GetAnnouncerLine(Card heroCard, Card.AnnouncerLineType type)
	{
		int num = UnityEngine.Random.Range(0, 3);
		if (heroCard.GetEntity().GetClass() == this.lotusHero)
		{
			switch (num)
			{
			case 0:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_01.prefab:6bc8a6bd85078984db14131a67029b04");
			case 1:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_02.prefab:c5c53500c23c1f744bdca5c5a3cbdc04");
			case 2:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_03.prefab:e98c84c7bd6010147a3c13956a6e0ece");
			}
		}
		else if (heroCard.GetEntity().GetClass() == this.goonsHero)
		{
			switch (num)
			{
			case 0:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_01.prefab:24bae69ce396c7947b1018b1976de679");
			case 1:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_02.prefab:ab33516e41188c248ae90f6cb17067fa");
			case 2:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_03.prefab:91ddf3c0c5e8d9a4ea1089fa346e5518");
			}
		}
		else if (heroCard.GetEntity().GetClass() == this.kabalHero)
		{
			switch (num)
			{
			case 0:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_01.prefab:ea1633a8b77d5fb4888b9214519c12ec");
			case 1:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_02.prefab:c4ea297c7eca6874c874b35aa7f2a018");
			case 2:
				return base.GetPreloadedSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_03.prefab:e43f5a1cc60008940a77161a3c635594");
			}
		}
		return base.GetAnnouncerLine(heroCard, type);
	}

	// Token: 0x06005079 RID: 20601 RVA: 0x001A70F0 File Offset: 0x001A52F0
	public override void PreloadAssets()
	{
		this.hasUsedLine = new bool[1000];
		this.SetupPriorityLines();
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_1st_Turn_Start_01.prefab:8be17b0dae8da254eb58a9666a008b63");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_2nd_Turn_Start_01.prefab:e5a44c5a2dfa9434a964bcc7da4b0d75");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Fill1_01.prefab:4be280fcdd3c5594aaf9125ab43be6b6");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Win_01.prefab:190589e48de533a42b90b5e170ce326d");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Winning_01.prefab:4ee560db3543f9f43a6c881f28d6e854");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Win_01.prefab:c94334b89fcf4ea4e81d0ec930e1d051");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Winning_01.prefab:7f8845179eedd5c4bac313cddef8bbe9");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_1st_Turn_Start_01.prefab:d686738dd65850e44a48e7c443c25263");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill1_01.prefab:c9cf61ce3a401064e97ccf4fafa3cff5");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill2_01.prefab:2e806195d67655141a710924c5016991");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_01.prefab:8e0079b4e73c2554d802a0ac39ba45d7");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_02.prefab:7cbbc6dbc527f994ea54a996ae75d525");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Winning_01.prefab:6c63549c3cb661d46aa47ea970217c61");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Wins_01.prefab:422826db6426bda4783fc3a36d113ee8");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Wins_01.prefab:5d9ad80e62f78404daab599afb8bdc60");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_01.prefab:6e0fe3ddd39cd424caf7369da719cd7c");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_02.prefab:4add1f34caa4c6841ae5687bc9f9a5a7");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_04.prefab:ac2104b0a0ece5240a320b4ba0968768");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_05.prefab:5bd45b4a07027974da3ad4a4b083a4b2");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem3_01.prefab:162f176395aab6b4aaf471fd549d5b40");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem2_01.prefab:92610ca6cb9ab2c4fbc8d64a77b7fa5e");
		base.PreloadSound("VO_BOSS_KAZAKUS_Male_Troll_BC_1_Arms_Dealing1_01.prefab:b93994bc7cd20b64caf25f092eb3e765");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_1st_Turn_Start_01.prefab:854d26b8a7627264ba091c0b632258f2");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_2nd_Turn_Start_01.prefab:81a12a2b061dfb149a8f2162dd9dd251");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Fill1_01.prefab:40a180e56c205c24fb70dd84c728f801");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Win_01.prefab:f59689a83886afb4aab053e6aa91ff3c");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Winning_02.prefab:1eb57a70b5a6f194c9b772c4c925da39");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Jade_Win_01.prefab:535dd787d222bc54e8c0372766751823");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Lotus_Winning_01.prefab:3512d5e4b0171f2499a5ac93d747af49");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_01.prefab:19367564a9bc4a14da1e9d2d2d06123d");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_02.prefab:de509aa2f0a49a243b9d23d70fb13168");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_2nd_Turn_Start_01.prefab:2ccf4699aa4d28e45b8c95ff79bf52f9");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Fill1_01.prefab:595b4d84ba3e55848a54388b450d40e1");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Fill2_01.prefab:f2c96720bb2daab4b87fcb8f1c663ae9");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Winning_01.prefab:e921d0ea7f239b74d8bbda8a77a7d6f6");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Wins_01.prefab:796da1c6fde86d046ba242430f5a7be4");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Winning_01.prefab:8309a83a5893c4145bd3d4fec40af94a");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Wins_01.prefab:ecdd497a01ea5f04fb369ec6e57efd76");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_02.prefab:da9bc8ef3fcddeb468c96ccf75ffc8e1");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_03.prefab:24ffb25cc6e6fe340afd95ddfb992823");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_04.prefab:32983d85d0e8f3e4fb4836444f320c60");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_3_Jade_Golem3_01.prefab:a21d7215fef07cf4a870b6ec136c68f6");
		base.PreloadSound("VO_BOSS_AYA_Female_Pandaren_BC_2_Arms_Dealing1_01.prefab:9388a49095971ee499e925c1385093b9");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:023f51537ec053c48b06d3003134c0a9");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_2nd_Turn_Start_02.prefab:fc8234cf7c6471a45b3f21e4c162ef2c");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Fill1_01.prefab:a6d42874b7ada1246883d6e150bcfcd4");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Fill2_01.prefab:3a38dd655d3d83542a6303ebbf5bd553");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Win_01.prefab:88e0a1afa65e143488d60a26451a54fe");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Winning_01.prefab:8e5e4d9dc062c0248bf4a31346c17817");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Win_02.prefab:ebc7a428c00acbf48802392c8af54462");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Winning_02.prefab:df4ff489a629a2642a6842494a6c2692");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_01.prefab:3bd5274c23af23c42ab9a2dc3de1bfc2");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_04.prefab:afdb7eef29038ce41802afe04f44371f");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Fill1_01.prefab:6bd403211b76afd448ad6eeb4fb0e9c8");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Win_01.prefab:dad027263619ad24ebab9d8ffec68436");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Winning_01.prefab:26c3e3ecb0645bf43a882631bc1d69a2");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Win_01.prefab:147a1660b03fa0c4dba706ba274c420e");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Winning_01.prefab:ab969d07a5b38ad45b6adf5d0ebd862e");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_2_Jade_Golem2_01.prefab:65b6678983cd69a49b341ec8ace0a72c");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_01.prefab:bdb1e3559c2b97c4bb9cdb78ebf19139");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_02.prefab:bf052509477065a48a760a5233513894");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_03.prefab:e0c61e1383fa32644ba6651b2ed842e1");
		base.PreloadSound("VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_04.prefab:aa45ac942e6197142a76054d72014a59");
		base.PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:cb463e997b8fafe48941c06e749d30bd");
		base.PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_1_Fill2_01.prefab:cfb6da4eb23272f40be8d9b2220ddb11");
		base.PreloadSound("VO_BOSS_CHO_Male_Ogre_BC_2_Fill1_01.prefab:e61f0b71bb9160547a872e8aa866486c");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_01.prefab:24bae69ce396c7947b1018b1976de679");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_02.prefab:ab33516e41188c248ae90f6cb17067fa");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_GrimyGoons_Intro_03.prefab:91ddf3c0c5e8d9a4ea1089fa346e5518");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_01.prefab:6bc8a6bd85078984db14131a67029b04");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_02.prefab:c5c53500c23c1f744bdca5c5a3cbdc04");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_JadeLotus_Intro_03.prefab:e98c84c7bd6010147a3c13956a6e0ece");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_01.prefab:ea1633a8b77d5fb4888b9214519c12ec");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_02.prefab:c4ea297c7eca6874c874b35aa7f2a018");
		base.PreloadSound("VO_INKEEPER_Male_Dwarf_TheKabal_Intro_03.prefab:e43f5a1cc60008940a77161a3c635594");
	}

	// Token: 0x0600507A RID: 20602 RVA: 0x001A7470 File Offset: 0x001A5670
	private void SetupPriorityLines()
	{
		this.priorityLines = new List<int>();
		this.priorityLines.Add(0);
		this.priorityLines.Add(102);
		this.priorityLines.Add(103);
		this.priorityLines.Add(202);
		this.priorityLines.Add(203);
		this.priorityLines.Add(302);
		this.priorityLines.Add(303);
	}

	// Token: 0x0600507B RID: 20603 RVA: 0x001A74F0 File Offset: 0x001A56F0
	private Vector3 GetPositionForBoss(TB_Blizzcon_2016.BOSS boss)
	{
		this.firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero().GetClass();
		switch (boss)
		{
		case TB_Blizzcon_2016.BOSS.HAN:
		case TB_Blizzcon_2016.BOSS.CHO:
			if (this.firstPlayerHero == this.goonsHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		case TB_Blizzcon_2016.BOSS.AYA:
			if (this.firstPlayerHero == this.lotusHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		case TB_Blizzcon_2016.BOSS.KAZAKUS:
			if (this.firstPlayerHero == this.kabalHero)
			{
				return NotificationManager.LEFT_OF_FRIENDLY_HERO;
			}
			return NotificationManager.RIGHT_OF_ENEMY_HERO;
		default:
			return NotificationManager.DEFAULT_CHARACTER_POS;
		}
	}

	// Token: 0x0600507C RID: 20604 RVA: 0x001A7584 File Offset: 0x001A5784
	private Notification.SpeechBubbleDirection GetBubbleDirectionForBoss(TB_Blizzcon_2016.BOSS boss)
	{
		this.firstPlayerHero = GameState.Get().GetFriendlySidePlayer().GetHero().GetClass();
		switch (boss)
		{
		case TB_Blizzcon_2016.BOSS.HAN:
			if (this.firstPlayerHero == this.goonsHero)
			{
				return Notification.SpeechBubbleDirection.BottomRight;
			}
			return Notification.SpeechBubbleDirection.TopRight;
		case TB_Blizzcon_2016.BOSS.CHO:
			if (this.firstPlayerHero == this.goonsHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		case TB_Blizzcon_2016.BOSS.AYA:
			if (this.firstPlayerHero == this.lotusHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		case TB_Blizzcon_2016.BOSS.KAZAKUS:
			if (this.firstPlayerHero == this.kabalHero)
			{
				return Notification.SpeechBubbleDirection.BottomLeft;
			}
			return Notification.SpeechBubbleDirection.TopLeft;
		default:
			return Notification.SpeechBubbleDirection.BottomLeft;
		}
	}

	// Token: 0x0600507D RID: 20605 RVA: 0x001A760C File Offset: 0x001A580C
	private IEnumerator PlayBossLine(TB_Blizzcon_2016.BOSS boss, string line, bool persistCharacter = false)
	{
		Notification.SpeechBubbleDirection bubbleDirectionForBoss = this.GetBubbleDirectionForBoss(boss);
		Vector3 positionForBoss = this.GetPositionForBoss(boss);
		switch (boss)
		{
		case TB_Blizzcon_2016.BOSS.HAN:
		case TB_Blizzcon_2016.BOSS.CHO:
			yield return base.PlayMissionFlavorLine("HanCho_Temp_BigQuote.prefab:7a7804f8f47064946bdbcfd3b78d0dac", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		case TB_Blizzcon_2016.BOSS.AYA:
			yield return base.PlayMissionFlavorLine("Aya_Temp_BigQuote.prefab:faa6811234b5e2e40b2447c7878616fe", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		case TB_Blizzcon_2016.BOSS.KAZAKUS:
			yield return base.PlayMissionFlavorLine("Kazakus_Temp_BigQuote.prefab:9d330c5f45374254181cb923722b973d", line, positionForBoss, bubbleDirectionForBoss, 2.5f, persistCharacter);
			break;
		}
		this.emoteThisTurn = true;
		this.currentTurnsWOEmote = 0;
		yield break;
	}

	// Token: 0x0600507E RID: 20606 RVA: 0x001A7630 File Offset: 0x001A5830
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.ERROR)
		{
			this.currentMatchup = this.GetBrawlHeroes();
		}
		if ((this.emoteThisTurn && !this.priorityLines.Contains(missionEvent)) || missionEvent == 1)
		{
			yield break;
		}
		if (missionEvent <= 170)
		{
			if (missionEvent <= 105)
			{
				if (missionEvent != 0)
				{
					switch (missionEvent)
					{
					case 100:
						if (!this.hasUsedLine[100])
						{
							GameState.Get().SetBusy(true);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_1st_Turn_Start_01.prefab:8be17b0dae8da254eb58a9666a008b63", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:023f51537ec053c48b06d3003134c0a9", true);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_1_1st_Turn_Start_01.prefab:cb463e997b8fafe48941c06e749d30bd", false);
							GameState.Get().SetBusy(false);
							this.hasUsedLine[100] = true;
						}
						break;
					case 101:
						if (!this.hasUsedLine[101])
						{
							GameState.Get().SetBusy(true);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_2nd_Turn_Start_01.prefab:e5a44c5a2dfa9434a964bcc7da4b0d75", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_2nd_Turn_Start_02.prefab:fc8234cf7c6471a45b3f21e4c162ef2c", false);
							GameState.Get().SetBusy(false);
							this.hasUsedLine[101] = true;
						}
						break;
					case 102:
						if (!this.hasUsedLine[102])
						{
							this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATKABAL;
						}
						break;
					case 103:
						if (!this.hasUsedLine[103])
						{
							this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATGOONS;
						}
						break;
					case 104:
						if (!this.hasUsedLine[104])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Winning_01.prefab:8e5e4d9dc062c0248bf4a31346c17817", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Winning_01.prefab:4ee560db3543f9f43a6c881f28d6e854", false);
							this.hasUsedLine[104] = true;
						}
						break;
					case 105:
						if (!this.hasUsedLine[105])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Winning_01.prefab:7f8845179eedd5c4bac313cddef8bbe9", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Winning_02.prefab:df4ff489a629a2642a6842494a6c2692", false);
							this.hasUsedLine[105] = true;
						}
						break;
					}
				}
				else
				{
					this.currentTurnsWOEmote++;
					Debug.Log(this.currentTurnsWOEmote);
					this.emoteThisTurn = false;
					if (this.currentTurnsWOEmote >= this.emoteTurnsLimit)
					{
						Gameplay.Get().StartCoroutine(this.PlayFillLine());
					}
				}
			}
			else if (missionEvent != 150)
			{
				if (missionEvent != 160)
				{
					if (missionEvent == 170)
					{
						int num = UnityEngine.Random.Range(0, 2);
						GameState.Get().SetBusy(true);
						if (num == 0 && !this.hasUsedLine[170])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa", false);
							this.hasUsedLine[170] = true;
						}
						GameState.Get().SetBusy(false);
					}
				}
				else
				{
					int num2 = UnityEngine.Random.Range(0, 2);
					GameState.Get().SetBusy(true);
					if (num2 == 1 && !this.hasUsedLine[161])
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8", false);
						this.hasUsedLine[161] = true;
					}
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				int num3 = UnityEngine.Random.Range(0, 4);
				if (!this.hasPlayedMatchupTriggerKabal)
				{
					num3 = UnityEngine.Random.Range(0, 7);
				}
				Debug.Log("Potion Trigger. Random value = " + num3);
				GameState.Get().SetBusy(true);
				switch (num3)
				{
				case 0:
					if (!this.hasUsedLine[150])
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_01.prefab:6e0fe3ddd39cd424caf7369da719cd7c", false);
						this.hasUsedLine[150] = true;
					}
					break;
				case 1:
					if (!this.hasUsedLine[151])
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_02.prefab:4add1f34caa4c6841ae5687bc9f9a5a7", false);
						this.hasUsedLine[151] = true;
					}
					break;
				case 2:
					if (!this.hasUsedLine[152])
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_04.prefab:ac2104b0a0ece5240a320b4ba0968768", false);
						this.hasUsedLine[152] = true;
					}
					break;
				case 3:
					if (!this.hasUsedLine[153])
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_0_Potion_05.prefab:5bd45b4a07027974da3ad4a4b083a4b2", false);
						this.hasUsedLine[153] = true;
					}
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.KABALVGOONS)
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Potion2_01.prefab:f2473e5ceda5f5b42b0bcdd80f352ea8", false);
						this.hasPlayedMatchupTriggerKabal = true;
					}
					else if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.KABALVLOTUS)
					{
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Potion1_01.prefab:1a2ad0f6911ab214db7de81137341307", false);
						yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Potion1_02.prefab:e9693fec0b2bb894d8de8c7a2d63b9aa", false);
						this.hasPlayedMatchupTriggerKabal = true;
					}
					break;
				}
				GameState.Get().SetBusy(false);
			}
		}
		else if (missionEvent <= 250)
		{
			switch (missionEvent)
			{
			case 200:
				if (!this.hasUsedLine[200])
				{
					GameState.Get().SetBusy(true);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_01.prefab:19367564a9bc4a14da1e9d2d2d06123d", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_1st_Turn_Start_01.prefab:d686738dd65850e44a48e7c443c25263", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_1st_Turn_Start_02.prefab:de509aa2f0a49a243b9d23d70fb13168", false);
					GameState.Get().SetBusy(false);
					this.hasUsedLine[200] = true;
				}
				break;
			case 201:
				if (!this.hasUsedLine[201])
				{
					GameState.Get().SetBusy(true);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem2_01.prefab:92610ca6cb9ab2c4fbc8d64a77b7fa5e", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_2nd_Turn_Start_01.prefab:2ccf4699aa4d28e45b8c95ff79bf52f9", false);
					GameState.Get().SetBusy(false);
					this.hasUsedLine[201] = true;
				}
				break;
			case 202:
				if (!this.hasUsedLine[202])
				{
					this.matchResult = TB_Blizzcon_2016.VICTOR.KABALBEATLOTUS;
				}
				break;
			case 203:
				if (!this.hasUsedLine[203])
				{
					this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATKABAL;
				}
				break;
			case 204:
				if (!this.hasUsedLine[204])
				{
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_01.prefab:8e0079b4e73c2554d802a0ac39ba45d7", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Winning_01.prefab:e921d0ea7f239b74d8bbda8a77a7d6f6", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Winning_02.prefab:7cbbc6dbc527f994ea54a996ae75d525", false);
					this.hasUsedLine[204] = true;
				}
				break;
			case 205:
				if (!this.hasUsedLine[205])
				{
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Winning_01.prefab:8309a83a5893c4145bd3d4fec40af94a", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Winning_01.prefab:6c63549c3cb661d46aa47ea970217c61", false);
					this.hasUsedLine[205] = true;
				}
				break;
			default:
				if (missionEvent == 250)
				{
					int num4 = UnityEngine.Random.Range(0, 3);
					if (!this.hasPlayedMatchupTriggerLotus)
					{
						num4 = UnityEngine.Random.Range(0, 6);
					}
					Debug.Log("Jade Golem Trigger. Random value = " + num4);
					GameState.Get().SetBusy(true);
					switch (num4)
					{
					case 0:
						if (!this.hasUsedLine[250])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_02.prefab:da9bc8ef3fcddeb468c96ccf75ffc8e1", false);
							this.hasUsedLine[250] = true;
						}
						break;
					case 1:
						if (!this.hasUsedLine[251])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_03.prefab:24ffb25cc6e6fe340afd95ddfb992823", false);
							this.hasUsedLine[251] = true;
						}
						break;
					case 2:
						if (!this.hasUsedLine[252])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_0_Jade_Golem_04.prefab:32983d85d0e8f3e4fb4836444f320c60", false);
							this.hasUsedLine[252] = true;
						}
						break;
					case 3:
					case 4:
					case 5:
						if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.GOONSVLOTUS)
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Jade_Golem2_01.prefab:65b6678983cd69a49b341ec8ace0a72c", false);
							this.hasPlayedMatchupTriggerLotus = true;
						}
						else if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.KABALVLOTUS)
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Jade_Golem3_01.prefab:162f176395aab6b4aaf471fd549d5b40", false);
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Jade_Golem3_01.prefab:a21d7215fef07cf4a870b6ec136c68f6", false);
							this.hasPlayedMatchupTriggerLotus = true;
						}
						break;
					}
					GameState.Get().SetBusy(false);
				}
				break;
			}
		}
		else
		{
			switch (missionEvent)
			{
			case 300:
				if (!this.hasUsedLine[300])
				{
					GameState.Get().SetBusy(true);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_01.prefab:3bd5274c23af23c42ab9a2dc3de1bfc2", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_1st_Turn_Start_01.prefab:854d26b8a7627264ba091c0b632258f2", false);
					GameState.Get().SetBusy(false);
					this.hasUsedLine[300] = true;
				}
				break;
			case 301:
				if (!this.hasUsedLine[301])
				{
					GameState.Get().SetBusy(true);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_2nd_Turn_Start_01.prefab:81a12a2b061dfb149a8f2162dd9dd251", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_1st_Turn_Start_04.prefab:afdb7eef29038ce41802afe04f44371f", false);
					GameState.Get().SetBusy(false);
					this.hasUsedLine[301] = true;
				}
				break;
			case 302:
				if (!this.hasUsedLine[302])
				{
					this.matchResult = TB_Blizzcon_2016.VICTOR.LOTUSBEATGOONS;
				}
				break;
			case 303:
				if (!this.hasUsedLine[303])
				{
					this.matchResult = TB_Blizzcon_2016.VICTOR.GOONSBEATLOTUS;
				}
				break;
			case 304:
				if (!this.hasUsedLine[304])
				{
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Lotus_Winning_01.prefab:3512d5e4b0171f2499a5ac93d747af49", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Winning_01.prefab:ab969d07a5b38ad45b6adf5d0ebd862e", false);
					this.hasUsedLine[304] = true;
				}
				break;
			case 305:
				if (!this.hasUsedLine[305])
				{
					GameState.Get().SetBusy(true);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Winning_01.prefab:26c3e3ecb0645bf43a882631bc1d69a2", false);
					yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Winning_02.prefab:1eb57a70b5a6f194c9b772c4c925da39", false);
					GameState.Get().SetBusy(false);
					this.hasUsedLine[305] = true;
				}
				break;
			default:
				if (missionEvent != 350)
				{
					if (missionEvent == 999)
					{
						this.SetNamePlate();
					}
				}
				else
				{
					int num5 = UnityEngine.Random.Range(0, 4);
					if (!this.hasPlayedMatchupTriggerGoons)
					{
						num5 = UnityEngine.Random.Range(0, 8);
					}
					Debug.Log("Arms Dealing Trigger. Random value = " + num5);
					GameState.Get().SetBusy(true);
					switch (num5)
					{
					case 0:
						if (!this.hasUsedLine[350])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_01.prefab:bdb1e3559c2b97c4bb9cdb78ebf19139", false);
							this.hasUsedLine[350] = true;
						}
						break;
					case 1:
						if (!this.hasUsedLine[351])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_02.prefab:bf052509477065a48a760a5233513894", false);
							this.hasUsedLine[351] = true;
						}
						break;
					case 2:
						if (!this.hasUsedLine[352])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_03.prefab:e0c61e1383fa32644ba6651b2ed842e1", false);
							this.hasUsedLine[352] = true;
						}
						break;
					case 3:
						if (!this.hasUsedLine[353])
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_0_Arms_Dealing_04.prefab:aa45ac942e6197142a76054d72014a59", false);
							this.hasUsedLine[353] = true;
						}
						break;
					case 4:
					case 5:
					case 6:
					case 7:
						if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.GOONSVLOTUS)
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Arms_Dealing1_01.prefab:9388a49095971ee499e925c1385093b9", false);
							this.hasPlayedMatchupTriggerGoons = true;
						}
						else if (this.currentMatchup == TB_Blizzcon_2016.MATCHUP.KABALVGOONS)
						{
							yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Arms_Dealing1_01.prefab:b93994bc7cd20b64caf25f092eb3e765", false);
							this.hasPlayedMatchupTriggerGoons = true;
						}
						break;
					}
					GameState.Get().SetBusy(false);
				}
				break;
			}
		}
		yield break;
	}

	// Token: 0x0600507F RID: 20607 RVA: 0x001A7648 File Offset: 0x001A5848
	private void SetNamePlate()
	{
		TAG_CLASS @class = GameState.Get().GetFriendlySidePlayer().GetHero().GetClass();
		TAG_CLASS class2 = GameState.Get().GetOpposingSidePlayer().GetHero().GetClass();
		if (@class == TAG_CLASS.PALADIN)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(this.grimyGoonsName);
		}
		else if (@class == TAG_CLASS.PRIEST)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(this.kabalName);
		}
		else if (@class == TAG_CLASS.DRUID)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.FRIENDLY).SetName(this.jadeLotusName);
		}
		else
		{
			Debug.Log("Incorrect class found in SetNamePlate()");
		}
		if (class2 == TAG_CLASS.PALADIN)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(this.grimyGoonsName);
			return;
		}
		if (class2 == TAG_CLASS.PRIEST)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(this.kabalName);
			return;
		}
		if (class2 == TAG_CLASS.DRUID)
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName(this.jadeLotusName);
			return;
		}
		Debug.Log("Incorrect class found in SetNamePlate()");
	}

	// Token: 0x06005080 RID: 20608 RVA: 0x001A7738 File Offset: 0x001A5938
	private IEnumerator PlayFillLine()
	{
		if (!this.hasUsedLine[101] && !this.hasUsedLine[201] && !this.hasUsedLine[301])
		{
			yield break;
		}
		bool flag = (double)UnityEngine.Random.value < 0.5;
		switch (this.currentMatchup)
		{
		case TB_Blizzcon_2016.MATCHUP.KABALVLOTUS:
			if (!this.hasUsedLine[80] && flag)
			{
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill2_01.prefab:2e806195d67655141a710924c5016991", false);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Fill2_01.prefab:f2c96720bb2daab4b87fcb8f1c663ae9", false);
				GameState.Get().SetBusy(false);
				this.hasUsedLine[80] = true;
			}
			else if (!this.hasUsedLine[81])
			{
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Fill1_01.prefab:595b4d84ba3e55848a54388b450d40e1", false);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Fill1_01.prefab:c9cf61ce3a401064e97ccf4fafa3cff5", false);
				GameState.Get().SetBusy(false);
				this.hasUsedLine[81] = true;
			}
			break;
		case TB_Blizzcon_2016.MATCHUP.KABALVGOONS:
			if (!this.hasUsedLine[70] && flag)
			{
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Fill1_01.prefab:4be280fcdd3c5594aaf9125ab43be6b6", false);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Fill1_01.prefab:a6d42874b7ada1246883d6e150bcfcd4", false);
				GameState.Get().SetBusy(false);
				this.hasUsedLine[70] = true;
			}
			else if (!this.hasUsedLine[71])
			{
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Fill2_01.prefab:3a38dd655d3d83542a6303ebbf5bd553", true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_1_Fill2_01.prefab:cfb6da4eb23272f40be8d9b2220ddb11", false);
				GameState.Get().SetBusy(false);
				this.hasUsedLine[71] = true;
			}
			break;
		case TB_Blizzcon_2016.MATCHUP.GOONSVLOTUS:
			if (!this.hasUsedLine[90])
			{
				GameState.Get().SetBusy(true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Fill1_01.prefab:40a180e56c205c24fb70dd84c728f801", false);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Fill1_01.prefab:6bd403211b76afd448ad6eeb4fb0e9c8", true);
				yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.CHO, "VO_BOSS_CHO_Male_Ogre_BC_2_Fill1_01.prefab:e61f0b71bb9160547a872e8aa866486c", false);
				GameState.Get().SetBusy(false);
				this.hasUsedLine[90] = true;
			}
			break;
		}
		yield break;
	}

	// Token: 0x06005081 RID: 20609 RVA: 0x001A7747 File Offset: 0x001A5947
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield return new WaitForSeconds(2f);
		switch (this.matchResult)
		{
		case TB_Blizzcon_2016.VICTOR.GOONSBEATKABAL:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Goons_Win_01.prefab:190589e48de533a42b90b5e170ce326d", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Goons_Win_01.prefab:88e0a1afa65e143488d60a26451a54fe", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[102] = true;
			break;
		case TB_Blizzcon_2016.VICTOR.GOONSBEATLOTUS:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Goons_Win_01.prefab:f59689a83886afb4aab053e6aa91ff3c", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Goons_Win_01.prefab:dad027263619ad24ebab9d8ffec68436", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[303] = true;
			break;
		case TB_Blizzcon_2016.VICTOR.LOTUSBEATKABAL:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Lotus_Wins_01.prefab:ecdd497a01ea5f04fb369ec6e57efd76", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Lotus_Wins_01.prefab:422826db6426bda4783fc3a36d113ee8", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[203] = true;
			break;
		case TB_Blizzcon_2016.VICTOR.LOTUSBEATGOONS:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_2_Jade_Win_01.prefab:535dd787d222bc54e8c0372766751823", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_2_Lotus_Win_01.prefab:147a1660b03fa0c4dba706ba274c420e", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[302] = true;
			break;
		case TB_Blizzcon_2016.VICTOR.KABALBEATGOONS:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.HAN, "VO_BOSS_HAN_Male_Ogre_BC_1_Kabal_Win_02.prefab:ebc7a428c00acbf48802392c8af54462", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_1_Kabal_Win_01.prefab:c94334b89fcf4ea4e81d0ec930e1d051", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[103] = true;
			break;
		case TB_Blizzcon_2016.VICTOR.KABALBEATLOTUS:
			GameState.Get().SetBusy(true);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.KAZAKUS, "VO_BOSS_KAZAKUS_Male_Troll_BC_3_Kabal_Wins_01.prefab:5d9ad80e62f78404daab599afb8bdc60", false);
			yield return this.PlayBossLine(TB_Blizzcon_2016.BOSS.AYA, "VO_BOSS_AYA_Female_Pandaren_BC_3_Kabal_Wins_01.prefab:796da1c6fde86d046ba242430f5a7be4", false);
			GameState.Get().SetBusy(false);
			this.hasUsedLine[202] = true;
			break;
		}
		yield break;
	}

	// Token: 0x040046B2 RID: 18098
	private Card m_bossCard;

	// Token: 0x040046B3 RID: 18099
	private Card m_mediva;

	// Token: 0x040046B4 RID: 18100
	private bool[] hasUsedLine;

	// Token: 0x040046B5 RID: 18101
	private int currentTurnsWOEmote;

	// Token: 0x040046B6 RID: 18102
	private int emoteTurnsLimit = 7;

	// Token: 0x040046B7 RID: 18103
	private bool emoteThisTurn;

	// Token: 0x040046B8 RID: 18104
	private List<int> priorityLines;

	// Token: 0x040046B9 RID: 18105
	private bool hasPlayedMatchupTriggerGoons;

	// Token: 0x040046BA RID: 18106
	private bool hasPlayedMatchupTriggerLotus;

	// Token: 0x040046BB RID: 18107
	private bool hasPlayedMatchupTriggerKabal;

	// Token: 0x040046BC RID: 18108
	private TB_Blizzcon_2016.MATCHUP currentMatchup = TB_Blizzcon_2016.MATCHUP.ERROR;

	// Token: 0x040046BD RID: 18109
	private TB_Blizzcon_2016.VICTOR matchResult;

	// Token: 0x040046BE RID: 18110
	private TAG_CLASS lotusHero = TAG_CLASS.DRUID;

	// Token: 0x040046BF RID: 18111
	private TAG_CLASS kabalHero = TAG_CLASS.PRIEST;

	// Token: 0x040046C0 RID: 18112
	private TAG_CLASS goonsHero = TAG_CLASS.PALADIN;

	// Token: 0x040046C1 RID: 18113
	private string grimyGoonsName = GameStrings.Get("GLOBAL_KEYWORD_GRIMY_GOONS");

	// Token: 0x040046C2 RID: 18114
	private string jadeLotusName = GameStrings.Get("GLOBAL_KEYWORD_JADE_LOTUS");

	// Token: 0x040046C3 RID: 18115
	private string kabalName = GameStrings.Get("GLOBAL_KEYWORD_KABAL");

	// Token: 0x040046C4 RID: 18116
	private TAG_CLASS firstPlayerHero;

	// Token: 0x040046C5 RID: 18117
	private TAG_CLASS secondPlayerHero;

	// Token: 0x02001F94 RID: 8084
	private enum MATCHUP
	{
		// Token: 0x0400D9B0 RID: 55728
		KABALVLOTUS,
		// Token: 0x0400D9B1 RID: 55729
		KABALVGOONS,
		// Token: 0x0400D9B2 RID: 55730
		GOONSVLOTUS,
		// Token: 0x0400D9B3 RID: 55731
		ERROR
	}

	// Token: 0x02001F95 RID: 8085
	private enum BOSS
	{
		// Token: 0x0400D9B5 RID: 55733
		HAN,
		// Token: 0x0400D9B6 RID: 55734
		CHO,
		// Token: 0x0400D9B7 RID: 55735
		AYA,
		// Token: 0x0400D9B8 RID: 55736
		KAZAKUS
	}

	// Token: 0x02001F96 RID: 8086
	private enum VICTOR
	{
		// Token: 0x0400D9BA RID: 55738
		GOONSBEATKABAL,
		// Token: 0x0400D9BB RID: 55739
		GOONSBEATLOTUS,
		// Token: 0x0400D9BC RID: 55740
		LOTUSBEATKABAL,
		// Token: 0x0400D9BD RID: 55741
		LOTUSBEATGOONS,
		// Token: 0x0400D9BE RID: 55742
		KABALBEATGOONS,
		// Token: 0x0400D9BF RID: 55743
		KABALBEATLOTUS,
		// Token: 0x0400D9C0 RID: 55744
		ERROR
	}
}
