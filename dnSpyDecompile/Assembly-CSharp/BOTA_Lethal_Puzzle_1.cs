using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000413 RID: 1043
public class BOTA_Lethal_Puzzle_1 : BOTA_MissionEntity
{
	// Token: 0x06003952 RID: 14674 RVA: 0x00121278 File Offset: 0x0011F478
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Complete_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_08,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_09,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_10,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_11,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Intro_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_05,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Return_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003953 RID: 14675 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003954 RID: 14676 RVA: 0x00121500 File Offset: 0x0011F700
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Return_01;
		this.s_victoryLine_1 = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_08,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_09,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_10,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Idle_11
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_05,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Restart_06
		};
		this.s_lethalCompleteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06,
			BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08
		};
	}

	// Token: 0x06003955 RID: 14677 RVA: 0x00121747 File Offset: 0x0011F947
	protected override IEnumerator RespondToPuzzleStartWithTiming()
	{
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		int currentMissionEvent = gameEntity.GetTag(GAME_TAG.MISSION_EVENT);
		if (currentMissionEvent == 10)
		{
			yield return base.PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 20)
		{
			yield return base.PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 40)
		{
			yield return base.PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05", 3f, 1f, true, false);
		}
		if (currentMissionEvent == 70)
		{
			yield return base.PlayBigCharacterQuoteAndWait("MyraRotspring_BrassRing_Quote.prefab:83be35e35b5e22b499ef296f55fcbd73", BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06, "VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06", 3f, 1f, true, false);
		}
		yield break;
	}

	// Token: 0x06003956 RID: 14678 RVA: 0x00121756 File Offset: 0x0011F956
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 99)
		{
			if (missionEvent == 777)
			{
				string lethalCompleteLine = base.GetLethalCompleteLine();
				if (lethalCompleteLine != null)
				{
					yield return base.PlayBossLine(enemyActor, lethalCompleteLine, 2.5f);
				}
			}
		}
		else
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

	// Token: 0x04001E85 RID: 7813
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Complete_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Complete_01.prefab:474bd745b06e121468cb0904279eea8e");

	// Token: 0x04001E86 RID: 7814
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_01.prefab:459b904bdbddd75479f408e70a3626a3");

	// Token: 0x04001E87 RID: 7815
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_02.prefab:68f821585c7371d438d95096f491e6c0");

	// Token: 0x04001E88 RID: 7816
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_03.prefab:761927665fddf1d4883751eca807a6e1");

	// Token: 0x04001E89 RID: 7817
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_EmoteResponse_04.prefab:7dc1c80b7cc7f3e40b54a13f16660678");

	// Token: 0x04001E8A RID: 7818
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_01.prefab:5bd2cf2e78907794b9ee5e38dd59d247");

	// Token: 0x04001E8B RID: 7819
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_02.prefab:4f2af8591cfe0b642a62e098f9ed6565");

	// Token: 0x04001E8C RID: 7820
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_03.prefab:689ea61945173364492ae403663c5da4");

	// Token: 0x04001E8D RID: 7821
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_04.prefab:bcef4e246ca620b41825a55380183c9d");

	// Token: 0x04001E8E RID: 7822
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_06.prefab:122a2ff9ba78a4143aad04121c4b0318");

	// Token: 0x04001E8F RID: 7823
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_08 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_08.prefab:b45ee248f2155b94680f1527cb61c491");

	// Token: 0x04001E90 RID: 7824
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_09 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_09.prefab:9e18cbad79fe5104288d45d46f5b83d2");

	// Token: 0x04001E91 RID: 7825
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_10 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_10.prefab:4ce51faf6294994419d2c33266f31a15");

	// Token: 0x04001E92 RID: 7826
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Idle_11 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Idle_11.prefab:6cf6094dee3147846b5a782ef8252f36");

	// Token: 0x04001E93 RID: 7827
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Intro_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Intro_01.prefab:1d748a37e9e4bed46bbbc968e209b610");

	// Token: 0x04001E94 RID: 7828
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_01.prefab:f099dc5102fc7da4b90ffcb2eabcdfe9");

	// Token: 0x04001E95 RID: 7829
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_02.prefab:40e0c9996cb204f49bdfac9f2d4338eb");

	// Token: 0x04001E96 RID: 7830
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Puzzle_03.prefab:ada363fc1ee66a44e98a29f20260cfd1");

	// Token: 0x04001E97 RID: 7831
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_01.prefab:82b6d09f67114b54f9cd653aa1b8009b");

	// Token: 0x04001E98 RID: 7832
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_02.prefab:231b6a350abb8394680706c25d34dbb1");

	// Token: 0x04001E99 RID: 7833
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_03.prefab:8f1f1384352765c489c3eb4a80b5cbed");

	// Token: 0x04001E9A RID: 7834
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_04.prefab:ce2d6caca1a98c14f85343938c8f5b60");

	// Token: 0x04001E9B RID: 7835
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_05 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_05.prefab:6fbf5c367961f4e4885612fe830a8ab1");

	// Token: 0x04001E9C RID: 7836
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Restart_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Restart_06.prefab:93f4a9d7181a5cc4b90e8b8c515fd229");

	// Token: 0x04001E9D RID: 7837
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Return_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Return_01.prefab:40b61fb1456bfdb43b462d9f899083b0");

	// Token: 0x04001E9E RID: 7838
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_01.prefab:de797322cb0fad94c9d7a59f9f32b560");

	// Token: 0x04001E9F RID: 7839
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_04.prefab:fc6faa26a2a28754981c1610ac7bf7a9");

	// Token: 0x04001EA0 RID: 7840
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_05.prefab:099452dc1c8d5f84bbb99898a309f564");

	// Token: 0x04001EA1 RID: 7841
	private static readonly AssetReference VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06 = new AssetReference("VO_BOTA_BOSS_14h_Female_Undead_Tutorial_06.prefab:17b9cde16ee448a49949b520bef1ee1f");

	// Token: 0x04001EA2 RID: 7842
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_01.prefab:658d5aed801b1d542959fac0ad624408");

	// Token: 0x04001EA3 RID: 7843
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_02.prefab:20a26f386c8134a4ca0e25617ed1ec02");

	// Token: 0x04001EA4 RID: 7844
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_03.prefab:2873d69f7652e084592591c5223cd00d");

	// Token: 0x04001EA5 RID: 7845
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_04.prefab:0e92dd70de6be834a90a1e24368b6510");

	// Token: 0x04001EA6 RID: 7846
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_06.prefab:6efaf12d23a1dc744a095a35c63296b5");

	// Token: 0x04001EA7 RID: 7847
	private static readonly AssetReference VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08 = new AssetReference("VO_BOTA_BOSS_10h_Male_NightElf_Lethal_08.prefab:8f67008fa28f1634bb19856744b8698c");

	// Token: 0x04001EA8 RID: 7848
	private string COMPLETE_LINE = BOTA_Lethal_Puzzle_1.VO_BOTA_BOSS_10h_Male_NightElf_Complete_01;
}
