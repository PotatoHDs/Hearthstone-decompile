using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200040F RID: 1039
public class BOTA_Clear_Puzzle_2 : BOTA_MissionEntity
{
	// Token: 0x06003935 RID: 14645 RVA: 0x0011FCB8 File Offset: 0x0011DEB8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Complete_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_05,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_06,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_07,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_09,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Intro_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_04,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_05,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Return_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003936 RID: 14646 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003937 RID: 14647 RVA: 0x0011FEA0 File Offset: 0x0011E0A0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Return_01;
		this.s_victoryLine_1 = null;
		this.s_victoryLine_2 = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01;
		this.s_victoryLine_3 = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01;
		this.s_victoryLine_4 = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_05,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_06,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_07,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_01,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_04,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Restart_05,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03,
			BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04
		};
	}

	// Token: 0x06003938 RID: 14648 RVA: 0x00120075 File Offset: 0x0011E275
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 99)
		{
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(enemyActor, this.COMPLETE_LINE, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04001E1C RID: 7708
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Complete_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Complete_02.prefab:88c4e83709b3c364b8eed5486ae6068c");

	// Token: 0x04001E1D RID: 7709
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_01.prefab:5f4e86e4317240747b6eaba7a2927278");

	// Token: 0x04001E1E RID: 7710
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_03.prefab:cc3732dd8a47bca449d5ff5f7e079109");

	// Token: 0x04001E1F RID: 7711
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_EmoteResponse_04.prefab:31440c4385f676545854642e92211e21");

	// Token: 0x04001E20 RID: 7712
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_01.prefab:cec08e69f54e48446b845afa4451119a");

	// Token: 0x04001E21 RID: 7713
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_02.prefab:6e32151936e1bfe498ef82276129f68b");

	// Token: 0x04001E22 RID: 7714
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_03.prefab:f582137ccf02b224ca752a4599187fcb");

	// Token: 0x04001E23 RID: 7715
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_05 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_05.prefab:c107688b86154744fb1b414758d35daa");

	// Token: 0x04001E24 RID: 7716
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_06 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_06.prefab:60677e8cfc94a5d4dbdbf730e3eaf8eb");

	// Token: 0x04001E25 RID: 7717
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_07 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_07.prefab:a358726abee05e447b9e9c7f194d57d3");

	// Token: 0x04001E26 RID: 7718
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Idle_09 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Idle_09.prefab:22df2a4665cdde640a1d439a8af04b42");

	// Token: 0x04001E27 RID: 7719
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Intro_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Intro_01.prefab:75dc11c0503b1a545a4c91453eee0823");

	// Token: 0x04001E28 RID: 7720
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_01.prefab:a09a7b5c3e5a1ee4db67833b5c90d075");

	// Token: 0x04001E29 RID: 7721
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_02.prefab:bf1745d887cf03349851e5c721aabb42");

	// Token: 0x04001E2A RID: 7722
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_03.prefab:5520fe25b536dc448b8a15100103caa8");

	// Token: 0x04001E2B RID: 7723
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_04.prefab:c32a06b894185eb4896016d7b9a6849e");

	// Token: 0x04001E2C RID: 7724
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Restart_05 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Restart_05.prefab:ff847044bbc98bb4480c89d862a221ce");

	// Token: 0x04001E2D RID: 7725
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Return_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Return_01.prefab:33d44670b3af8d94fba2764415232c5f");

	// Token: 0x04001E2E RID: 7726
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_01.prefab:023846e52033fd545b5340a711e29827");

	// Token: 0x04001E2F RID: 7727
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_02.prefab:e3a57f2f25cfccd408ce2616a06faad4");

	// Token: 0x04001E30 RID: 7728
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Puzzle_03.prefab:570ad1e08cb0f48448048bea319738a2");

	// Token: 0x04001E31 RID: 7729
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_02.prefab:6904eed4811e7b84ab97185aa6c18b7b");

	// Token: 0x04001E32 RID: 7730
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_03.prefab:3fb540bbaca7af5459842f1d1c69a0f4");

	// Token: 0x04001E33 RID: 7731
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_04.prefab:e101d2e1ea2a61d47b4b26e38b6456ce");

	// Token: 0x04001E34 RID: 7732
	private static readonly AssetReference VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01 = new AssetReference("VO_BOTA_BOSS_07h_Male_Ooze_Stuck_01.prefab:f78e26e97373ef94ea6ea15b5dde0dad");

	// Token: 0x04001E35 RID: 7733
	private string COMPLETE_LINE = BOTA_Clear_Puzzle_2.VO_BOTA_BOSS_07h_Male_Ooze_Complete_02;
}
