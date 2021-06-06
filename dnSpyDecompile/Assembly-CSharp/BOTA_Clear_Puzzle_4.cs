using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000411 RID: 1041
public class BOTA_Clear_Puzzle_4 : BOTA_MissionEntity
{
	// Token: 0x06003942 RID: 14658 RVA: 0x001207E8 File Offset: 0x0011E9E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		foreach (string soundPath in new List<string>
		{
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Complete_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_04,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_05,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_06,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_07,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Intro_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_04,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_05,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Return_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003943 RID: 14659 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected override float ChanceToPlayRandomVOLine()
	{
		return 1f;
	}

	// Token: 0x06003944 RID: 14660 RVA: 0x001209E0 File Offset: 0x0011EBE0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		BOTA_MissionEntity.s_introLine = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Intro_01;
		BOTA_MissionEntity.s_returnLine = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Return_01;
		this.s_victoryLine_1 = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01;
		this.s_victoryLine_2 = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03;
		this.s_victoryLine_3 = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02;
		this.s_victoryLine_4 = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04;
		this.s_victoryLine_5 = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05;
		this.s_victoryLine_6 = null;
		this.s_victoryLine_7 = null;
		this.s_victoryLine_8 = null;
		this.s_victoryLine_9 = null;
		this.s_emoteLines = new List<string>
		{
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03
		};
		this.s_idleLines = new List<string>
		{
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_04,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_05,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_06,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Idle_07
		};
		this.s_restartLines = new List<string>
		{
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_01,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_02,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_03,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_04,
			BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Restart_05
		};
	}

	// Token: 0x06003945 RID: 14661 RVA: 0x00120B8E File Offset: 0x0011ED8E
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

	// Token: 0x06003946 RID: 14662 RVA: 0x00120BA4 File Offset: 0x0011EDA4
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(2f);
			yield return base.PlayClosingLine("DrBoom_Banner_Quote.prefab:ff8653a27a00c464ea5552e3c6b6dc5c", BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003947 RID: 14663 RVA: 0x00120BBA File Offset: 0x0011EDBA
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
		if (!(cardId == "ICC_041"))
		{
			if (cardId == "ICC_099")
			{
				yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(actor, BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04001E53 RID: 7763
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Complete_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Complete_01.prefab:6fe3e92b704b5f34b8bffc0563221018");

	// Token: 0x04001E54 RID: 7764
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_01.prefab:f37406e97ea7dd345884bfd7334bfc21");

	// Token: 0x04001E55 RID: 7765
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_02.prefab:14c7245670f0d9f43a9d645dfbc95481");

	// Token: 0x04001E56 RID: 7766
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_EmoteResponse_03.prefab:5c1ec8319513deb449247f3e5dbaffba");

	// Token: 0x04001E57 RID: 7767
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_01.prefab:58927d2670bc42b45833daa3356f0993");

	// Token: 0x04001E58 RID: 7768
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_02.prefab:0e4c00a6c5b68cc409453d23683e2015");

	// Token: 0x04001E59 RID: 7769
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_03.prefab:8dcf4f07ddd22b047938805a45b45bbb");

	// Token: 0x04001E5A RID: 7770
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_04.prefab:86a553e0090fe2248b4e82541e5d9072");

	// Token: 0x04001E5B RID: 7771
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_05.prefab:efca1c511606c744da4b1a60e9becf81");

	// Token: 0x04001E5C RID: 7772
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_06 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_06.prefab:457770625d8a9c745a7f5e77726baa11");

	// Token: 0x04001E5D RID: 7773
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Idle_07 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Idle_07.prefab:f9367d13bcef4c9409534e2413ef1b89");

	// Token: 0x04001E5E RID: 7774
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Intro_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Intro_01.prefab:d51864f7c13cdf943ab16a629683040c");

	// Token: 0x04001E5F RID: 7775
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_PlayDefile_01.prefab:e6b564af96d315b45a0d958d973e6208");

	// Token: 0x04001E60 RID: 7776
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_PlayTicking_01.prefab:3dfecb4521329dd48bea94f9662a519b");

	// Token: 0x04001E61 RID: 7777
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_01.prefab:c26d5c52553d3a7489a8733a0e181aee");

	// Token: 0x04001E62 RID: 7778
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_02.prefab:9ec6e11b51bfd324f9c6f15d469c0ff9");

	// Token: 0x04001E63 RID: 7779
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_03.prefab:a971edf9f35942145b4aa6d1efb1fa02");

	// Token: 0x04001E64 RID: 7780
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_04.prefab:58f0d140708c8694283cf659051c54bc");

	// Token: 0x04001E65 RID: 7781
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Restart_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Restart_05.prefab:1cda323a761a5314fa54d678f3d29a31");

	// Token: 0x04001E66 RID: 7782
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Return_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Return_01.prefab:5338107286875594dbfd7d324903e628");

	// Token: 0x04001E67 RID: 7783
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_01.prefab:b46498e17da2eca409226c20562a68c1");

	// Token: 0x04001E68 RID: 7784
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_02.prefab:e3d2560e7085ad449bbb53fab0a39fac");

	// Token: 0x04001E69 RID: 7785
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_03.prefab:9623b96f7131e4b4da7edbc55ecb99e4");

	// Token: 0x04001E6A RID: 7786
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_04.prefab:a2772aa2ac0e3fe499d5d9c06ce7fdbd");

	// Token: 0x04001E6B RID: 7787
	private static readonly AssetReference VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05 = new AssetReference("VO_BOTA_BOSS_09h_Female_Banshee_Puzzle_05.prefab:06a7b84a92ab69644968abe855836eee");

	// Token: 0x04001E6C RID: 7788
	private static readonly AssetReference VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01 = new AssetReference("VO_BOTA_BOSS_20h_Male_Goblin_UI_Wing3_Complete_01.prefab:35eef2e00ddf24d4698bf4b2f52a458a");

	// Token: 0x04001E6D RID: 7789
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001E6E RID: 7790
	private string COMPLETE_LINE = BOTA_Clear_Puzzle_4.VO_BOTA_BOSS_09h_Female_Banshee_Complete_01;
}
