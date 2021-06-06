using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000471 RID: 1137
public class DALA_Dungeon_Boss_68h : DALA_Dungeon
{
	// Token: 0x06003D9A RID: 15770 RVA: 0x00143958 File Offset: 0x00141B58
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Death_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_02,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_03,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Intro_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01,
			DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06003D9B RID: 15771 RVA: 0x00143ACC File Offset: 0x00141CCC
	public override List<string> GetIdleLines()
	{
		return DALA_Dungeon_Boss_68h.m_IdleLines;
	}

	// Token: 0x06003D9C RID: 15772 RVA: 0x00143AD3 File Offset: 0x00141CD3
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Intro_01;
		this.m_deathLine = DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Death_01;
		this.m_standardEmoteResponseLine = DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01;
	}

	// Token: 0x06003D9D RID: 15773 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06003D9E RID: 15774 RVA: 0x00143B0B File Offset: 0x00141D0B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01, 2.5f);
			break;
		case 102:
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01, 2.5f);
			break;
		case 103:
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, DALA_Dungeon_Boss_68h.m_HeroPowerTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
		yield break;
	}

	// Token: 0x06003D9F RID: 15775 RVA: 0x00143B21 File Offset: 0x00141D21
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		if (cardId == "GVG_003" || cardId == "KAR_073" || cardId == "KAR_075" || cardId == "KAR_076" || cardId == "KAR_077" || cardId == "KAR_091")
		{
			yield return base.PlayLineOnlyOnce(enemyActor, DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06003DA0 RID: 15776 RVA: 0x00143B37 File Offset: 0x00141D37
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
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "GVG_003" || cardId == "KAR_073" || cardId == "KAR_075" || cardId == "KAR_076" || cardId == "KAR_077" || cardId == "KAR_091")
		{
			yield return base.PlayLineOnlyOnce(actor, DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x040028B8 RID: 10424
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_BossPortal_01.prefab:a873bb5e7365cdc4c915b4b650a01dbc");

	// Token: 0x040028B9 RID: 10425
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Death_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Death_01.prefab:76c49765d2c812643bcb00d7a92d6112");

	// Token: 0x040028BA RID: 10426
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_DefeatPlayer_01.prefab:d39518f20db49a745a80013ad9b4139b");

	// Token: 0x040028BB RID: 10427
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_EmoteResponse_01.prefab:0ec964c2034842a479fa06e9e81ce122");

	// Token: 0x040028BC RID: 10428
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02.prefab:9d9a010736697b44db997de8e1f6c930");

	// Token: 0x040028BD RID: 10429
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03.prefab:6be21abc0ed74f443a19711cd2a3a1b4");

	// Token: 0x040028BE RID: 10430
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04.prefab:586f7f44c7675b342b58b9a9d7055bd7");

	// Token: 0x040028BF RID: 10431
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05.prefab:598761b5a02cc504da2f14a8deae30ff");

	// Token: 0x040028C0 RID: 10432
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07.prefab:6aae75afaf0b63f48b206406a3f6eda1");

	// Token: 0x040028C1 RID: 10433
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08.prefab:43908666d428c294aaf5469ac22883e8");

	// Token: 0x040028C2 RID: 10434
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_01.prefab:c6f0ed7e5efa54b4ea0faeaed6907ce2");

	// Token: 0x040028C3 RID: 10435
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_02.prefab:7ed2e1ee86dff144386d2a50733e8cef");

	// Token: 0x040028C4 RID: 10436
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Idle_03.prefab:149ce7a0eaaeb4c4391212f44f091749");

	// Token: 0x040028C5 RID: 10437
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Intro_01.prefab:02a6b55bd379de848aa4e2271fab4fe3");

	// Token: 0x040028C6 RID: 10438
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Player10CostMinion_01.prefab:ac364d1574d7fb34ba153514c9ed047d");

	// Token: 0x040028C7 RID: 10439
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_Player1CostMinion_01.prefab:501bbfe0d99f0f544b74712260f9d650");

	// Token: 0x040028C8 RID: 10440
	private static readonly AssetReference VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01 = new AssetReference("VO_DALA_BOSS_68h_Female_BloodElf_PlayerPortal_01.prefab:73d103d2c3238064eaa756bc8e48f62e");

	// Token: 0x040028C9 RID: 10441
	private static List<string> m_IdleLines = new List<string>
	{
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_01,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_02,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_Idle_03
	};

	// Token: 0x040028CA RID: 10442
	private static List<string> m_HeroPowerTrigger = new List<string>
	{
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_02,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_03,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_04,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_05,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_07,
		DALA_Dungeon_Boss_68h.VO_DALA_BOSS_68h_Female_BloodElf_HeroPowerTrigger_08
	};

	// Token: 0x040028CB RID: 10443
	private HashSet<string> m_playedLines = new HashSet<string>();
}
