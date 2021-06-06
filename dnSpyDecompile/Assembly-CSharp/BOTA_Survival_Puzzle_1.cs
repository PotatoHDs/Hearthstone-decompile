using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200041E RID: 1054
public class BOTA_Survival_Puzzle_1 : BOTA_MissionEntity
{
	// Token: 0x060039BE RID: 14782 RVA: 0x001251E4 File Offset: 0x001233E4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_02,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_03,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_04,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_05,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_06,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Intro_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_03,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_04,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_05,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_06,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_07,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Return_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Complete_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060039BF RID: 14783 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x060039C0 RID: 14784 RVA: 0x001253AC File Offset: 0x001235AC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Return_01;
		this.s_victoryLine_1 = BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_02,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_03,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_04,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_05,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Idle_06
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_01,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_03,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_04,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_05,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_06,
			BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_15h_Female_NightElf_Restart_07
		};
	}

	// Token: 0x060039C1 RID: 14785 RVA: 0x0012552F File Offset: 0x0012372F
	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.MISSION_EVENT);
		if (tag == 10)
		{
			yield return base.PlayBigCharacterQuoteAndWait(BOTA_Survival_Puzzle_1.BoommasterFlark_BrassRing_Quote, BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01", 3f, 1f, true, false);
		}
		else if (tag == 20)
		{
			yield return base.PlayBigCharacterQuoteAndWait(BOTA_Survival_Puzzle_1.BoommasterFlark_BrassRing_Quote, BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06", 3f, 1f, true, false);
		}
		else if (tag == 30)
		{
			yield return base.PlayBigCharacterQuoteAndWait(BOTA_Survival_Puzzle_1.BoommasterFlark_BrassRing_Quote, BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04", 3f, 1f, true, false);
		}
		else if (tag == 40)
		{
			yield return base.PlayBigCharacterQuoteAndWait(BOTA_Survival_Puzzle_1.BoommasterFlark_BrassRing_Quote, BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02, "VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02", 3f, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x060039C2 RID: 14786 RVA: 0x0012553E File Offset: 0x0012373E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 99)
		{
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(BOTA_Survival_Puzzle_1.BoommasterFlark_BrassRing_Quote, this.COMPLETE_LINE, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001FAB RID: 8107
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_01.prefab:7f4d4b4cb16f0dd43bec8e4673077968");

	// Token: 0x04001FAC RID: 8108
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_EmoteResponse_02.prefab:fb4402a2149690f438e8d267435936c3");

	// Token: 0x04001FAD RID: 8109
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_01.prefab:2c99a9fc4d8c9cc4e9c2b27516c5b167");

	// Token: 0x04001FAE RID: 8110
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_02 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_02.prefab:c28de8a3027e9824bb179bc414ed9b05");

	// Token: 0x04001FAF RID: 8111
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_03.prefab:4ad2b4258cfefaa428d3e865af153f7e");

	// Token: 0x04001FB0 RID: 8112
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_04 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_04.prefab:6453ec53e48b27a4e8cd791cb646629b");

	// Token: 0x04001FB1 RID: 8113
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_05 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_05.prefab:826405a6bc685dd4d963c3dfe0e882e8");

	// Token: 0x04001FB2 RID: 8114
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Idle_06 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Idle_06.prefab:994b99acbcdcbe94bb93b18f62a55b71");

	// Token: 0x04001FB3 RID: 8115
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Intro_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Intro_01.prefab:ef656224c4a3663418d29b6c2620dbfc");

	// Token: 0x04001FB4 RID: 8116
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_01.prefab:17e367c67c151f441bd1529fd2a4bb4d");

	// Token: 0x04001FB5 RID: 8117
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_03.prefab:f08d46fcaf7f22945a230eebdcfa4880");

	// Token: 0x04001FB6 RID: 8118
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_04 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_04.prefab:e3f8c577ca147e749bae6495d116d569");

	// Token: 0x04001FB7 RID: 8119
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_05 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_05.prefab:bf4e763d2070ac64a8e8f7eb0f128e35");

	// Token: 0x04001FB8 RID: 8120
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_06 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_06.prefab:62dffacf090ed5048b19090bcd672467");

	// Token: 0x04001FB9 RID: 8121
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Restart_07 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Restart_07.prefab:bb1bb6639fdbf6c40929bfde3f68bdf4");

	// Token: 0x04001FBA RID: 8122
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Return_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_NightElf_Return_01.prefab:ec4f86fff154fed45b017f89fc9365ff");

	// Token: 0x04001FBB RID: 8123
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_15h_Female_Night Elf_Puzzle_01.prefab:f6e3819d9b589114692b94607c3c87c3");

	// Token: 0x04001FBC RID: 8124
	private static readonly AssetReference VO_BOTA_BOSS_15h_Female_NightElf_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_15h_Female_Night Elf_Puzzle_03.prefab:e49106bf92dcb2e4ca2a835968c782dd");

	// Token: 0x04001FBD RID: 8125
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_01.prefab:7a500b4d8aca2364db7e7307b829b42a");

	// Token: 0x04001FBE RID: 8126
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_02.prefab:3fa57b8ac4ddbfb418ee8ddcd570d74d");

	// Token: 0x04001FBF RID: 8127
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_04.prefab:5b13df8e68fd5204f84513246dc2900f");

	// Token: 0x04001FC0 RID: 8128
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Tutorial_06.prefab:18881d3092348ed4b9b1e380705f91d4");

	// Token: 0x04001FC1 RID: 8129
	private static readonly AssetReference VO_BOTA_BOSS_16h_Male_Goblin_Complete_01 = new AssetReference("VO_BOTA_BOSS_16h_Male_Goblin_Complete_01.prefab:d0d0a88812f1fce45bc4594c5f465873");

	// Token: 0x04001FC2 RID: 8130
	private static readonly AssetReference BoommasterFlark_BrassRing_Quote = new AssetReference("BoommasterFlark_BrassRing_Quote.prefab:ac5bae8e6d4465c45b533aafa40053b2");

	// Token: 0x04001FC3 RID: 8131
	private string COMPLETE_LINE = BOTA_Survival_Puzzle_1.VO_BOTA_BOSS_16h_Male_Goblin_Complete_01;
}
