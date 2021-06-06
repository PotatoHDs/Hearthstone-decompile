using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200040E RID: 1038
public class BOTA_Clear_Puzzle_1 : BOTA_MissionEntity
{
	// Token: 0x0600392D RID: 14637 RVA: 0x0011F768 File Offset: 0x0011D968
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_04,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_05,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_06,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_07,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Intro_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_04,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_05,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Return_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Complete_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600392E RID: 14638 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600392F RID: 14639 RVA: 0x0011F950 File Offset: 0x0011DB50
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_04,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_05,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_06,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Idle_07
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_01,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_02,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_03,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_04,
			BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_Restart_05
		};
	}

	// Token: 0x06003930 RID: 14640 RVA: 0x0011FAE3 File Offset: 0x0011DCE3
	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return base.PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 20)
		{
			yield return base.PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 50)
		{
			yield return base.PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 80)
		{
			yield return base.PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06, "VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06", 3f, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x06003931 RID: 14641 RVA: 0x0011FAF2 File Offset: 0x0011DCF2
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			yield return base.PlayBigCharacterQuoteAndWait("FlobbidinousFloop_BrassRing_Quote.prefab:bdd400552a67a6e4381cf9a680ebf952", BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_07h_Male_Ooze_Complete_01, "VO_BOTA_BOSS_07h_Male_Ooze_Complete_01", 3f, 1f, true, false);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003932 RID: 14642 RVA: 0x0011FB08 File Offset: 0x0011DD08
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "EX1_581")
		{
			yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_1.VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001E02 RID: 7682
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_01.prefab:46e7a21728fd64b4f953031d9d977732");

	// Token: 0x04001E03 RID: 7683
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_02.prefab:18d3481a6092cf34e96f88f1142386d8");

	// Token: 0x04001E04 RID: 7684
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_EmoteResponse_03.prefab:1eb726cce726f65419e12571297677ca");

	// Token: 0x04001E05 RID: 7685
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_01.prefab:77150d342881aa54cbb7e79165328780");

	// Token: 0x04001E06 RID: 7686
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_02.prefab:363554ecdf3b2e04ea0937af8a42656a");

	// Token: 0x04001E07 RID: 7687
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_03.prefab:b0d825a2597656144bde988096af48df");

	// Token: 0x04001E08 RID: 7688
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_04 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_04.prefab:dc44f71232290ea4280808bb5ae3df73");

	// Token: 0x04001E09 RID: 7689
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_05 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_05.prefab:b3c08919f76a50a4ca716d80926a6544");

	// Token: 0x04001E0A RID: 7690
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_06 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_06.prefab:d6485b62c515eae4dadf981a008a2506");

	// Token: 0x04001E0B RID: 7691
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Idle_07 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Idle_07.prefab:1911b8a59ce445146a8501e953ae1f64");

	// Token: 0x04001E0C RID: 7692
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Intro_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Intro_01.prefab:f0c4fac0aaafee0408f55786e085f697");

	// Token: 0x04001E0D RID: 7693
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_PlaySap_01.prefab:1417f9f83bc647c4da3eaf28b2e06692");

	// Token: 0x04001E0E RID: 7694
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_01.prefab:95ce41b863246c643b59d5aebcd80ab9");

	// Token: 0x04001E0F RID: 7695
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Puzzle_02.prefab:62e85efda01b7054e9543333724fa918");

	// Token: 0x04001E10 RID: 7696
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_01.prefab:ba7e83c3d76e9044ea9d4eb81c37d573");

	// Token: 0x04001E11 RID: 7697
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_02 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_02.prefab:d705a514b9eca334888fcc369162b96b");

	// Token: 0x04001E12 RID: 7698
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_03 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_03.prefab:6b9328e67fed6a8479a976184892d4e8");

	// Token: 0x04001E13 RID: 7699
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_04 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_04.prefab:e5ef3671705772249aafc88da4ec2a32");

	// Token: 0x04001E14 RID: 7700
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Restart_05 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Restart_05.prefab:709f39f48ab3d534b9885258806d60be");

	// Token: 0x04001E15 RID: 7701
	private static readonly AssetReference VO_BOTA_BOSS_06h_Male_Goblin_Return_01 = new AssetReference("VO_BOTA_BOSS_06h_Male_Goblin_Return_01.prefab:9c21d2a1c04d1b140a8b2c7a419f7bf5");

	// Token: 0x04001E16 RID: 7702
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Complete_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Complete_01.prefab:bc0994ae7d6cd784da984371e2e9b300");

	// Token: 0x04001E17 RID: 7703
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_02.prefab:ccb8daed1960e8c4cb86fa30101bb2a9");

	// Token: 0x04001E18 RID: 7704
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_03.prefab:dff0d5adb8926114ea74c77cbef8485d");

	// Token: 0x04001E19 RID: 7705
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_06.prefab:77fe443edf907c14ea0a30a2468ca1fb");

	// Token: 0x04001E1A RID: 7706
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Tutorial_07.prefab:95c304fd114d39743b04007e803a6b9b");

	// Token: 0x04001E1B RID: 7707
	private HashSet<string> m_playedLines = new HashSet<string>();
}
