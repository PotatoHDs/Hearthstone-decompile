using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200057A RID: 1402
public class BOM_03_Guff_Fight_08 : BOM_03_Guff_Dungeon
{
	// Token: 0x06004DF1 RID: 19953 RVA: 0x0019C5E8 File Offset: 0x0019A7E8
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_03_Guff_Fight_08.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03,
			BOM_03_Guff_Fight_08.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02,
			BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004DF2 RID: 19954 RVA: 0x0019C7FC File Offset: 0x0019A9FC
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_DRGEVILBoss;
	}

	// Token: 0x06004DF3 RID: 19955 RVA: 0x0019C80F File Offset: 0x0019AA0F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 504:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01);
			goto IL_312;
		case 505:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02);
			GameState.Get().SetBusy(false);
			goto IL_312;
		case 506:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01);
			GameState.Get().SetBusy(false);
			goto IL_312;
		case 510:
			yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossUsesHeroPower);
			goto IL_312;
		case 514:
			yield return base.MissionPlayVO(actor, BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02);
			goto IL_312;
		case 515:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01);
			goto IL_312;
		case 516:
			yield return base.MissionPlaySound(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01);
			goto IL_312;
		case 517:
			yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossIdle);
			goto IL_312;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_312:
		yield break;
	}

	// Token: 0x06004DF4 RID: 19956 RVA: 0x0019C825 File Offset: 0x0019AA25
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		yield break;
	}

	// Token: 0x06004DF5 RID: 19957 RVA: 0x0019C83B File Offset: 0x0019AA3B
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

	// Token: 0x06004DF6 RID: 19958 RVA: 0x0019C851 File Offset: 0x0019AA51
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 3:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01);
			break;
		case 5:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01);
			break;
		case 7:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01);
			break;
		case 9:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01);
			break;
		case 11:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02);
			break;
		case 13:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01);
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02);
			yield return base.MissionPlayVO(this.Brukan_BrassRing, BOM_03_Guff_Fight_08.VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03);
			break;
		case 15:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01);
			yield return base.MissionPlayVO(this.Tamsin_BrassRing, BOM_03_Guff_Fight_08.VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02);
			yield return base.MissionPlayVO(this.Rokara_B_BrassRing, BOM_03_Guff_Fight_08.VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03);
			break;
		case 17:
			yield return base.MissionPlayVO(enemyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_03_Guff_Fight_08.VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02);
			break;
		}
		yield break;
	}

	// Token: 0x040044AB RID: 17579
	private static readonly AssetReference VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03 = new AssetReference("VO_Story_Hero_Brukan_Male_Troll_Story_Guff_Mission8ExchangeF_03.prefab:d23ad907c0ff8344ca4f7b2c7967b517");

	// Token: 0x040044AC RID: 17580
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeE_02.prefab:cc6be32822b88f6458de143c32692733");

	// Token: 0x040044AD RID: 17581
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeH_02.prefab:9d2629aab0e6775489e961c88be764ca");

	// Token: 0x040044AE RID: 17582
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8ExchangeJ_01.prefab:c86b86508ba0b4040b493f27d4526401");

	// Token: 0x040044AF RID: 17583
	private static readonly AssetReference VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Guff_Male_Tauren_Story_Guff_Mission8Intro_01.prefab:5c644ecc59b529243a072fb708b823c9");

	// Token: 0x040044B0 RID: 17584
	private static readonly AssetReference VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01 = new AssetReference("VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_01.prefab:4270937e624144343ae9747ac8e89cc8");

	// Token: 0x040044B1 RID: 17585
	private static readonly AssetReference VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02 = new AssetReference("VO_Story_Hero_Naralex_Male_NightElf_Story_Guff_Mission8ExchangeJ_02.prefab:039594ba909aed34fa2d6dd2754a3f54");

	// Token: 0x040044B2 RID: 17586
	private static readonly AssetReference VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03 = new AssetReference("VO_Story_Hero_Rokara_Female_Orc_Story_Guff_Mission8ExchangeG_03.prefab:843df793716cc214792ef126af736036");

	// Token: 0x040044B3 RID: 17587
	private static readonly AssetReference VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02 = new AssetReference("VO_Story_Hero_Tamsin_Female_Forsaken_Story_Guff_Mission8ExchangeG_02.prefab:db2c3cd16f8453f469a3fa80c3a80a8c");

	// Token: 0x040044B4 RID: 17588
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Death_01.prefab:ffc010f610157184d8d8f9e3e836dd47");

	// Token: 0x040044B5 RID: 17589
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8EmoteResponse_01.prefab:3b84b68342feca34391861e115b20181");

	// Token: 0x040044B6 RID: 17590
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeA_01.prefab:2acc05c129f5a3a45a9213721e1fb0c3");

	// Token: 0x040044B7 RID: 17591
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeB_01.prefab:f7d84d4df1af6624889fad807e1bfada");

	// Token: 0x040044B8 RID: 17592
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeC_01.prefab:ec121a0a63a21df47b1b695a452363fb");

	// Token: 0x040044B9 RID: 17593
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeD_01.prefab:078753cb865fe1344b7149fb2cf7c74b");

	// Token: 0x040044BA RID: 17594
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeE_01.prefab:2320f3d381841374aa96b509894c6e51");

	// Token: 0x040044BB RID: 17595
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_01.prefab:38fc8de18f761b24488b5e252a9dea74");

	// Token: 0x040044BC RID: 17596
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeF_02.prefab:be1a7bc9d2eb9d1448dcdb918c4ef396");

	// Token: 0x040044BD RID: 17597
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeG_01.prefab:097385fff42b7904896e566086cd9762");

	// Token: 0x040044BE RID: 17598
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8ExchangeH_01.prefab:e642e5142c1e8aa4c83cdefff4d50a82");

	// Token: 0x040044BF RID: 17599
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01.prefab:6fc7331fb5c19774a9b32d99b5de0a48");

	// Token: 0x040044C0 RID: 17600
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02.prefab:9707c60ef7f33734aace07ac0f166090");

	// Token: 0x040044C1 RID: 17601
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01.prefab:4e0133870d1d7f046b5f9f26e60cd98b");

	// Token: 0x040044C2 RID: 17602
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02.prefab:5adab9a7a9e8b5241a49efba7582b8f8");

	// Token: 0x040044C3 RID: 17603
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03.prefab:0b9f3f26bb712f2428541d652458e7f8");

	// Token: 0x040044C4 RID: 17604
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Intro_02.prefab:2bd4d404600a9544184cdc4267aeb07f");

	// Token: 0x040044C5 RID: 17605
	private static readonly AssetReference VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01 = new AssetReference("VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Loss_01.prefab:4728b15dafb05fa40bd73273fa647d00");

	// Token: 0x040044C6 RID: 17606
	private List<string> m_InGame_BossIdle = new List<string>
	{
		BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_01,
		BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_02,
		BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8Idle_03
	};

	// Token: 0x040044C7 RID: 17607
	private List<string> m_InGame_BossUsesHeroPower = new List<string>
	{
		BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_01,
		BOM_03_Guff_Fight_08.VO_Story_Hero_VoidNaralex_Male_NightElf_Story_Guff_Mission8HeroPower_02
	};

	// Token: 0x040044C8 RID: 17608
	private HashSet<string> m_playedLines = new HashSet<string>();
}
