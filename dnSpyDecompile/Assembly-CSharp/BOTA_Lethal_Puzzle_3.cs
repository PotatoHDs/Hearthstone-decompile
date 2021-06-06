using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000415 RID: 1045
public class BOTA_Lethal_Puzzle_3 : BOTA_MissionEntity
{
	// Token: 0x0600395F RID: 14687 RVA: 0x00121F54 File Offset: 0x00120154
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Complete_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_06,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_07,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_08,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_09,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Intro_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_PlayHealingRain_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_06,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_07,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_08,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Return_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_06,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003960 RID: 14688 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003961 RID: 14689 RVA: 0x001221AC File Offset: 0x001203AC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Return_01;
		this.s_victoryLine_1 = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_01;
		this.s_victoryLine_2 = null;
		this.s_victoryLine_3 = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_03;
		this.s_victoryLine_4 = null;
		this.s_victoryLine_5 = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_05;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_06,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_07,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_08,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_03,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_06,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_07,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_08
		};
		this.s_lethalCompleteLines = new List<string>
		{
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_01,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_02,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_04,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_05,
			BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_06
		};
	}

	// Token: 0x06003962 RID: 14690 RVA: 0x001223F3 File Offset: 0x001205F3
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "LOOT_373")
		{
			yield return base.PlayLineOnlyOnce(actor, BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_PlayHealingRain_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003963 RID: 14691 RVA: 0x00122409 File Offset: 0x00120609
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
					GameState.Get().SetBusy(true);
					yield return base.PlayBossLine(enemyActor, lethalCompleteLine, 2.5f);
					GameState.Get().SetBusy(false);
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

	// Token: 0x04001EC4 RID: 7876
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Complete_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Complete_01.prefab:0aff580f66ac0144780e1a871839da04");

	// Token: 0x04001EC5 RID: 7877
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_01.prefab:8163056ed1098494cad9197f206f39d0");

	// Token: 0x04001EC6 RID: 7878
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_02.prefab:c601ed97699ed8e4c90965138e7be987");

	// Token: 0x04001EC7 RID: 7879
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_EmoteResponse_03.prefab:7052e872c483ae346a46242c9941de0e");

	// Token: 0x04001EC8 RID: 7880
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_01.prefab:e64d4dd9f38f6d247997d7d3b47db921");

	// Token: 0x04001EC9 RID: 7881
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_02 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_02.prefab:523f64d4a351c214591212e161c9145f");

	// Token: 0x04001ECA RID: 7882
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_03 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_03.prefab:be25ff2d6503eac45ad5322e3bbd0580");

	// Token: 0x04001ECB RID: 7883
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_04 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_04.prefab:7ee14a67e9d58834b86444f6a6a22f3b");

	// Token: 0x04001ECC RID: 7884
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_05 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_05.prefab:f4cc404356bd47447ac5ef1fbe9ac3a3");

	// Token: 0x04001ECD RID: 7885
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_06 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_06.prefab:7d8aab996b7fee542b59d9efd6313a35");

	// Token: 0x04001ECE RID: 7886
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_07 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_07.prefab:dd0b1127174e7ae48aefca150aadf295");

	// Token: 0x04001ECF RID: 7887
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_08 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_08.prefab:350b83cb3ae39004fa2ef5c9ce746076");

	// Token: 0x04001ED0 RID: 7888
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_09 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Idle_09.prefab:d129ec81ba61ba648ad71e1a3472ca8e");

	// Token: 0x04001ED1 RID: 7889
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Intro_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Intro_01.prefab:96417cba41696094b9f6716dd829d1e8");

	// Token: 0x04001ED2 RID: 7890
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_PlayHealingRain_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_PlayHealingRain_01.prefab:1144fc3d94f024a47bd38f27fb6e78cf");

	// Token: 0x04001ED3 RID: 7891
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_01.prefab:3af0c14e01064994ebd4a8eb50dfbc99");

	// Token: 0x04001ED4 RID: 7892
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_02 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_02.prefab:a4ddc5db00322444e96e4c30f5628d3e");

	// Token: 0x04001ED5 RID: 7893
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_03 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_03.prefab:12e5874fe8a27504a9ea5ce42373c829");

	// Token: 0x04001ED6 RID: 7894
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_04 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_04.prefab:e6f5f99a3e1ba17458a998ad7c96c1ee");

	// Token: 0x04001ED7 RID: 7895
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_05 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_05.prefab:361c3e92056593a4e977301e8dfc09e7");

	// Token: 0x04001ED8 RID: 7896
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_06 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_06.prefab:fd567f7613795ea468c842d6f8cf8230");

	// Token: 0x04001ED9 RID: 7897
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_07 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_07.prefab:7db895f68a4d27b4284b4f8db1d91b8a");

	// Token: 0x04001EDA RID: 7898
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_08 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Restart_08.prefab:b20f72a888f95e04cb03b078ab02d984");

	// Token: 0x04001EDB RID: 7899
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Return_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Return_01.prefab:86bf69c954dfa0a4fa58c20ccf2cc2d7");

	// Token: 0x04001EDC RID: 7900
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_01.prefab:d73e725d417797445a8c0f05f718659c");

	// Token: 0x04001EDD RID: 7901
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_02 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_02.prefab:9394636443fbcc945a79bc9f7276da92");

	// Token: 0x04001EDE RID: 7902
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_04 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_04.prefab:6e87dd8a4dcb0d6448b67a849c864ed3");

	// Token: 0x04001EDF RID: 7903
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_05 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_05.prefab:7d3c8c47c0fba244191cb5fbbada1221");

	// Token: 0x04001EE0 RID: 7904
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_06 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Lethal_06.prefab:e826e9d62df330d4b93803f5665d70a5");

	// Token: 0x04001EE1 RID: 7905
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_03.prefab:4b2fe859e8f1e54439bb89fdaa109905");

	// Token: 0x04001EE2 RID: 7906
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_05 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_05.prefab:dd28ae7ffef7f114ba721e488b41dd6c");

	// Token: 0x04001EE3 RID: 7907
	private static readonly AssetReference VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_12h_Female_WaterSpirit_Puzzle_01.prefab:e4a203d8b6acd1f488c1bc62d9ab7f99");

	// Token: 0x04001EE4 RID: 7908
	private string COMPLETE_LINE = BOTA_Lethal_Puzzle_3.VO_BOTA_BOSS_12h_Female_WaterSpirit_Complete_01;
}
