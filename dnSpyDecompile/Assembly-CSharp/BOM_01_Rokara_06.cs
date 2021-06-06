using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000563 RID: 1379
public class BOM_01_Rokara_06 : BoM_01_Rokara_Dungeon
{
	// Token: 0x06004C61 RID: 19553 RVA: 0x00194154 File Offset: 0x00192354
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_01_Rokara_06.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01,
			BOM_01_Rokara_06.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02,
			BOM_01_Rokara_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01,
			BOM_01_Rokara_06.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01,
			BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01,
			BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02,
			BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01,
			BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02,
			BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01,
			BOM_01_Rokara_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02,
			BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004C62 RID: 19554 RVA: 0x00194328 File Offset: 0x00192528
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines2;
	}

	// Token: 0x06004C63 RID: 19555 RVA: 0x00194330 File Offset: 0x00192530
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004C64 RID: 19556 RVA: 0x00194338 File Offset: 0x00192538
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_deathLine = BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01;
		this.m_standardEmoteResponseLine = BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01;
	}

	// Token: 0x06004C65 RID: 19557 RVA: 0x0019436B File Offset: 0x0019256B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 506)
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03);
				GameState.Get().SetBusy(false);
				goto IL_25A;
			}
			if (missionEvent == 506)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01);
				goto IL_25A;
			}
		}
		else
		{
			if (missionEvent == 514)
			{
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02);
				goto IL_25A;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_25A;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_25A:
		yield break;
	}

	// Token: 0x06004C66 RID: 19558 RVA: 0x00194381 File Offset: 0x00192581
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
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004C67 RID: 19559 RVA: 0x00194397 File Offset: 0x00192597
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return base.RespondToPlayedCardWithTiming(entity);
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004C68 RID: 19560 RVA: 0x001943AD File Offset: 0x001925AD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyHeroPowerActor = GameState.Get().GetFriendlySidePlayer().GetHeroPower().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02);
			}
		}
		else if (turn != 11)
		{
			if (turn == 15)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01);
				yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_01_Rokara_06.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02);
			}
		}
		else
		{
			if (this.HeroPowerIsBrukan)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_06.VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01);
			}
			if (this.HeroPowerIsGuff)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_06.VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01);
			}
			if (this.HeroPowerIsTamsin)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_06.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01);
			}
			if (this.HeroPowerIsDawngrasp)
			{
				yield return base.MissionPlayVO(friendlyHeroPowerActor, BOM_01_Rokara_06.VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01);
			}
		}
		yield break;
	}

	// Token: 0x04004159 RID: 16729
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeC_Brukan_01.prefab:5860080393630a34688bc48cfba0c13d");

	// Token: 0x0400415A RID: 16730
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Rokara_Mission6ExchangeD_02.prefab:e5bc900cb4c5dc141abd3084625d3f66");

	// Token: 0x0400415B RID: 16731
	private static readonly AssetReference VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01 = new AssetReference("VO_Story_Hero_Dawngrasp_X_BloodElf_Story_Rokara_Mission6ExchangeC_Dawngrasp_01.prefab:adf54e9ead2d06f49a3f9df165503fb0");

	// Token: 0x0400415C RID: 16732
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Rokara_Mission6ExchangeC_Guff_01.prefab:ee7d75a239a37e342bd79c82df28b763");

	// Token: 0x0400415D RID: 16733
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeA_01.prefab:48fa5f24a9e7632478cef26e990809ef");

	// Token: 0x0400415E RID: 16734
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeB_02.prefab:00591dc1e45462a4eb391b8d371e1398");

	// Token: 0x0400415F RID: 16735
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6ExchangeD_01.prefab:3ec027ef1603da14d8c4fb94b77ecf8b");

	// Token: 0x04004160 RID: 16736
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02.prefab:3e9aae236b90bb9448f4384f032a0d6c");

	// Token: 0x04004161 RID: 16737
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01.prefab:582e7238727bfeb47b52ce011cb05ecf");

	// Token: 0x04004162 RID: 16738
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Rokara_Mission6ExchangeC_Tamsin_01.prefab:25fe952211d929b449860e5738be041f");

	// Token: 0x04004163 RID: 16739
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Death_01.prefab:a3028e433d633df448cb292b5e43cb9e");

	// Token: 0x04004164 RID: 16740
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6EmoteResponse_01.prefab:e3fcafe501c01e343908f2461715e11c");

	// Token: 0x04004165 RID: 16741
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeA_02.prefab:319f093f72b58da46b540a4ff12d0e78");

	// Token: 0x04004166 RID: 16742
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6ExchangeB_01.prefab:562143d64f9a09445be0a189449a2824");

	// Token: 0x04004167 RID: 16743
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01.prefab:cc7946ba21796484b9eff7a499adf736");

	// Token: 0x04004168 RID: 16744
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02.prefab:1457bfc23d454404786458c7f1a615f3");

	// Token: 0x04004169 RID: 16745
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01.prefab:a1c23fd415b999340a5456c9cec61e75");

	// Token: 0x0400416A RID: 16746
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02.prefab:02061417d6e6b5244ab2ea911e965694");

	// Token: 0x0400416B RID: 16747
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03.prefab:435c9bcbc35299147a186bd6f4137fb7");

	// Token: 0x0400416C RID: 16748
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01.prefab:f59b701a91724504bb6eb712b81f9873");

	// Token: 0x0400416D RID: 16749
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Loss_01.prefab:7ff1903f9c6cd824e98be7330725e3df");

	// Token: 0x0400416E RID: 16750
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02.prefab:097e13f3eda817441b22ff5e68449ad4");

	// Token: 0x0400416F RID: 16751
	private static readonly AssetReference VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03 = new AssetReference("VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03.prefab:5ec3738994efaa447bc62b0d313a9587");

	// Token: 0x04004170 RID: 16752
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Intro_02,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Intro_01
	};

	// Token: 0x04004171 RID: 16753
	private List<string> m_missionEventTriggerInGame_VictoryPostExplosionLines = new List<string>
	{
		BOM_01_Rokara_06.VO_Story_Hero_Rokara_Female_Orc_Story_Rokara_Mission6Victory_01,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_02,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Victory_03
	};

	// Token: 0x04004172 RID: 16754
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_01,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6HeroPower_02
	};

	// Token: 0x04004173 RID: 16755
	private List<string> m_BossIdleLines2 = new List<string>
	{
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_01,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_02,
		BOM_01_Rokara_06.VO_Story_Hero_Twinbraid_Male_Dwarf_Story_Rokara_Mission6Idle_03
	};

	// Token: 0x04004174 RID: 16756
	private HashSet<string> m_playedLines = new HashSet<string>();
}
