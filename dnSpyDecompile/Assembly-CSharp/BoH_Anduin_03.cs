using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000510 RID: 1296
public class BoH_Anduin_03 : BoH_Anduin_Dungeon
{
	// Token: 0x060045F1 RID: 17905 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x060045F2 RID: 17906 RVA: 0x00179D00 File Offset: 0x00177F00
	public BoH_Anduin_03()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_03.s_booleanOptions);
	}

	// Token: 0x060045F3 RID: 17907 RVA: 0x00179DA4 File Offset: 0x00177FA4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01,
			BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01,
			BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01,
			BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02,
			BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01,
			BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01,
			BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01,
			BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01,
			BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02,
			BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01,
			BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01,
			BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060045F4 RID: 17908 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060045F5 RID: 17909 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060045F6 RID: 17910 RVA: 0x00179F68 File Offset: 0x00178168
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060045F7 RID: 17911 RVA: 0x00179F77 File Offset: 0x00178177
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060045F8 RID: 17912 RVA: 0x00179F7F File Offset: 0x0017817F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060045F9 RID: 17913 RVA: 0x00179F87 File Offset: 0x00178187
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		this.m_standardEmoteResponseLine = BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01;
	}

	// Token: 0x060045FA RID: 17914 RVA: 0x00179FAA File Offset: 0x001781AA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 110)
		{
			switch (missionEvent)
			{
			case 102:
				yield return base.MissionPlayVOOnce(base.GetFriendlyActorByCardId("Story_05_HighPriestRohan"), BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01);
				goto IL_391;
			case 103:
				yield return base.MissionPlayVOOnce(base.GetFriendlyActorByCardId("Story_05_HighPriestRohan"), BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01);
				goto IL_391;
			case 104:
				break;
			case 105:
				yield return base.MissionPlayVOOnce(base.GetFriendlyActorByCardId("Story_05_HighPriestRohan"), BoH_Anduin_03.VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01);
				goto IL_391;
			default:
				if (missionEvent == 110)
				{
					GameState.Get().SetBusy(true);
					yield return base.MissionPlayVO(BoH_Anduin_03.VarianBrassRing, BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01);
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01);
					GameState.Get().SetBusy(false);
					goto IL_391;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 504)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02);
				yield return base.MissionPlayVO(BoH_Anduin_03.VarianBrassRing, BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02);
				GameState.Get().SetBusy(false);
				goto IL_391;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_391;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				goto IL_391;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_391:
		yield break;
	}

	// Token: 0x060045FB RID: 17915 RVA: 0x00179FC0 File Offset: 0x001781C0
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

	// Token: 0x060045FC RID: 17916 RVA: 0x00179FD6 File Offset: 0x001781D6
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

	// Token: 0x060045FD RID: 17917 RVA: 0x00179FEC File Offset: 0x001781EC
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
		case 1:
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01);
			break;
		case 3:
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01);
			break;
		case 5:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(BoH_Anduin_03.VarianBrassRing, BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_03.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01);
			GameState.Get().SetBusy(false);
			break;
		case 7:
			yield return base.MissionPlayVO(BoH_Anduin_03.VarianBrassRing, BoH_Anduin_03.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02);
			break;
		}
		yield break;
	}

	// Token: 0x040038F6 RID: 14582
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_03.InitBooleanOptions();

	// Token: 0x040038F7 RID: 14583
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeA_01.prefab:4908545736995b744a407e7ee4134c89");

	// Token: 0x040038F8 RID: 14584
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeB_01.prefab:476b43efdd7a0d84699c1eef68f5f051");

	// Token: 0x040038F9 RID: 14585
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3ExchangeC_01.prefab:46d8c2706e2cf8f42820fc5330980349");

	// Token: 0x040038FA RID: 14586
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Intro_02.prefab:d7d978e724f7e1844a0d0a792c91d038");

	// Token: 0x040038FB RID: 14587
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission3Victory_02.prefab:ab167d1a6079cd240abc998b7b238709");

	// Token: 0x040038FC RID: 14588
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3EmoteResponse_01.prefab:0d9139c8c4f2cfd4aad6707858662219");

	// Token: 0x040038FD RID: 14589
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeC_02.prefab:410a029f015a0624a95f499d19a61c78");

	// Token: 0x040038FE RID: 14590
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3ExchangeD_02.prefab:0a20e6d4332de6840b1353997da1ebf6");

	// Token: 0x040038FF RID: 14591
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01.prefab:e77dd6d44479acb41916ca00daea4d79");

	// Token: 0x04003900 RID: 14592
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02.prefab:42146e3371c173f4cb3c53a82571cb94");

	// Token: 0x04003901 RID: 14593
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03.prefab:66d7c92c1b2eff445aa3c086b3473225");

	// Token: 0x04003902 RID: 14594
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01.prefab:4d12cb3fda0eb284c9474679ba155c0d");

	// Token: 0x04003903 RID: 14595
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02.prefab:7b6736da40b60ff488e71a35dcb7e690");

	// Token: 0x04003904 RID: 14596
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03.prefab:a903d101ed0956c499d8d89192c2eb9f");

	// Token: 0x04003905 RID: 14597
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Intro_01.prefab:24a1a5ea1d4d4ce428d037986c6bb8fd");

	// Token: 0x04003906 RID: 14598
	private static readonly AssetReference VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Loss_01.prefab:8d4c62aafa6bcbb41acd800b88488085");

	// Token: 0x04003907 RID: 14599
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeC_01.prefab:0849f6c433b00c847bf63d61d48e6f71");

	// Token: 0x04003908 RID: 14600
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3ExchangeD_01.prefab:f3b7e17724564814baaed6f0c23ad079");

	// Token: 0x04003909 RID: 14601
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission3Victory_02.prefab:975d367e47bef8c4b96b50ad3d6eab65");

	// Token: 0x0400390A RID: 14602
	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerAnduin_01.prefab:5907285304104d1449743d81bc017fed");

	// Token: 0x0400390B RID: 14603
	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerHeal_01.prefab:ce942982cab8acc49a96981778b71e56");

	// Token: 0x0400390C RID: 14604
	private static readonly AssetReference VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01 = new AssetReference("VO_Story_Minion_Rohan_Male_Dwarf_TriggerMoira_01.prefab:f0b4780cc9dc72848b2053f254892a7f");

	// Token: 0x0400390D RID: 14605
	public static readonly AssetReference VarianBrassRing = new AssetReference("Varian_BrassRing_Quote.prefab:b192b80fcc22d1145bfa81b476cecc09");

	// Token: 0x0400390E RID: 14606
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_01,
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_02,
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3HeroPower_03
	};

	// Token: 0x0400390F RID: 14607
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_01,
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_02,
		BoH_Anduin_03.VO_Story_Hero_Moira_Female_Dwarf_Story_Anduin_Mission3Idle_03
	};

	// Token: 0x04003910 RID: 14608
	private HashSet<string> m_playedLines = new HashSet<string>();
}
