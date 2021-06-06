using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000410 RID: 1040
public class BOTA_Clear_Puzzle_3 : BOTA_MissionEntity
{
	// Token: 0x0600393B RID: 14651 RVA: 0x00120228 File Offset: 0x0011E428
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Complete_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_04,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_05,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_06,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_07,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_08,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_09,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Intro_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_04,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_05,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Return_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600393C RID: 14652 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x0600393D RID: 14653 RVA: 0x00120430 File Offset: 0x0011E630
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Return_01;
		this.s_victoryLine_1 = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01;
		this.s_victoryLine_2 = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01;
		this.s_victoryLine_3 = null;
		this.s_victoryLine_4 = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02;
		this.s_victoryLine_5 = null;
		this.s_victoryLine_6 = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_04,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_05,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_06,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_07,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_08,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Idle_09
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_01,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_02,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_03,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_04,
			BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Restart_05
		};
	}

	// Token: 0x0600393E RID: 14654 RVA: 0x001205F5 File Offset: 0x0011E7F5
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

	// Token: 0x0600393F RID: 14655 RVA: 0x0012060B File Offset: 0x0011E80B
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
		if (!(cardId == "OG_100"))
		{
			if (!(cardId == "GIL_665"))
			{
				if (cardId == "GVG_077")
				{
					yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001E36 RID: 7734
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Complete_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Complete_01.prefab:493eb06ad9050674fbb54c671609d583");

	// Token: 0x04001E37 RID: 7735
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_01.prefab:f6e2731b7ce04aa4fbf7dc03f2c29ea8");

	// Token: 0x04001E38 RID: 7736
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_02.prefab:1f451226373cccc4ca41fddcc3088dea");

	// Token: 0x04001E39 RID: 7737
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_EmoteResponse_03.prefab:ba0dc202f1ee51541b98eb87f076f666");

	// Token: 0x04001E3A RID: 7738
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_01.prefab:7ce8ca09c94329449b045f6f90b55fb9");

	// Token: 0x04001E3B RID: 7739
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_02.prefab:45ddd6d3e34746f428d85e1295049a32");

	// Token: 0x04001E3C RID: 7740
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_03.prefab:86978668814132b41acf8e6fa46ee789");

	// Token: 0x04001E3D RID: 7741
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_04 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_04.prefab:531cd137ac9264449a69819a0e6b6d35");

	// Token: 0x04001E3E RID: 7742
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_05 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_05.prefab:c192bd7436ca00945b783b8c3836f0c8");

	// Token: 0x04001E3F RID: 7743
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_06 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_06.prefab:7ccca8ce2befcdd4a9e1cfe85b878002");

	// Token: 0x04001E40 RID: 7744
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_07 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_07.prefab:2f553260a37daaf4ea4420ae372b649e");

	// Token: 0x04001E41 RID: 7745
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_08 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_08.prefab:41c3b8e03c3183241b97dacb73109372");

	// Token: 0x04001E42 RID: 7746
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Idle_09 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Idle_09.prefab:5d035c54f9974f749b050b23de557950");

	// Token: 0x04001E43 RID: 7747
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Intro_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Intro_01.prefab:74bbe5cb61af0a945bb818deb8016df9");

	// Token: 0x04001E44 RID: 7748
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayAnima_01.prefab:dc2000f07a54d7549931ee00d2bf23c6");

	// Token: 0x04001E45 RID: 7749
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayHorror_01.prefab:f920687cb211c63448f3a435d72c18b2");

	// Token: 0x04001E46 RID: 7750
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PlayWeakness_01.prefab:f4f9eb9a779e9c847bb5aca86403ace2");

	// Token: 0x04001E47 RID: 7751
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_01.prefab:6381f727ef3b3b547abd855ca5a36d4d");

	// Token: 0x04001E48 RID: 7752
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_02.prefab:64dccfa006bc9e94c8bd8597d2c78cc2");

	// Token: 0x04001E49 RID: 7753
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_03.prefab:c5aa8d563b1ef894fa1b3121444e3777");

	// Token: 0x04001E4A RID: 7754
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_04 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_04.prefab:a8599707c926c634ca6b4c91a60bfafe");

	// Token: 0x04001E4B RID: 7755
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Restart_05 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Restart_05.prefab:8b9a56ca3b748b643808a34bdf32ca38");

	// Token: 0x04001E4C RID: 7756
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Return_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Return_01.prefab:97da9cd0542dd8d48b3a06679db4a26a");

	// Token: 0x04001E4D RID: 7757
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_01.prefab:a007b141d68e0174b9d1178bdd209360");

	// Token: 0x04001E4E RID: 7758
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_02.prefab:73c0b40b8b708704aa2a103f92776399");

	// Token: 0x04001E4F RID: 7759
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_Puzzle_03.prefab:03e79595fd70fb6489e061cdb6e79c0c");

	// Token: 0x04001E50 RID: 7760
	private static readonly AssetReference VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01 = new AssetReference("VO_BOTA_BOSS_08h_Female_Eredar_PuzzleALT_01.prefab:458cf6cf8a59735429eb645451d58796");

	// Token: 0x04001E51 RID: 7761
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001E52 RID: 7762
	private string COMPLETE_LINE = BOTA_Clear_Puzzle_3.VO_BOTA_BOSS_08h_Female_Eredar_Complete_01;
}
