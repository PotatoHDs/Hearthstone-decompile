using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004E9 RID: 1257
public class BTA_Dungeon_Boss_15h : BTA_Dungeon
{
	// Token: 0x06004361 RID: 17249 RVA: 0x0016D310 File Offset: 0x0016B510
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01,
			BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01,
			BTA_Dungeon_Boss_15h.RussellTheBard_Short_Strum_Underlay,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song_Underlay_part1,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song_Underlay_part2,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song3A_01_Underlay,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song3B_01_Underlay,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song3C_01_Underlay,
			BTA_Dungeon_Boss_15h.RussellTheBard_Song3D_01_Underlay
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004362 RID: 17250 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004363 RID: 17251 RVA: 0x0016D554 File Offset: 0x0016B754
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor playerActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineWithUnderlay(playerActor, BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01, BTA_Dungeon_Boss_15h.RussellTheBard_Song3C_01_Underlay, 2.5f);
				yield return base.PlayLineWithUnderlay(playerActor, BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01, BTA_Dungeon_Boss_15h.RussellTheBard_Song3D_01_Underlay, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineWithUnderlay(enemyActor, BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01, BTA_Dungeon_Boss_15h.RussellTheBard_Song3A_01_Underlay, 2.5f);
			yield return base.PlayLineWithUnderlay(enemyActor, BTA_Dungeon_Boss_15h.VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01, BTA_Dungeon_Boss_15h.RussellTheBard_Song3B_01_Underlay, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004364 RID: 17252 RVA: 0x0016D56A File Offset: 0x0016B76A
	public override void OnCreateGame()
	{
		base.OnCreateGame();
	}

	// Token: 0x0400353B RID: 13627
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Death_01.prefab:e5c8b619095374542bac028ed3654007");

	// Token: 0x0400353C RID: 13628
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_DefeatPlayer_02.prefab:8ae8f6b7e7ae0a2479e671cc7129bd7a");

	// Token: 0x0400353D RID: 13629
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_EmoteResponse_01.prefab:777b3055ee92eae42b1b27826e242da7");

	// Token: 0x0400353E RID: 13630
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_EventBoombox_01.prefab:d031062f6c4754b42a8a0975a1b221a1");

	// Token: 0x0400353F RID: 13631
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Intro_01.prefab:c513358ed8344364dbf85ee7bb4f8eca");

	// Token: 0x04003540 RID: 13632
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_KillingBlowReady_01.prefab:df8c2af5e5f7f4d4584ff6c6a87dfea6");

	// Token: 0x04003541 RID: 13633
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1A_01.prefab:9c6e2abda71d0ec49b5074792dfbf771");

	// Token: 0x04003542 RID: 13634
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1B_01.prefab:b8319bccf79c8b74d92c485cb6dd786c");

	// Token: 0x04003543 RID: 13635
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1C_01.prefab:5f1305268addceb4c9a673711114f60d");

	// Token: 0x04003544 RID: 13636
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song1D_01.prefab:b541bc13aa2323348bfcd150bbd16fab");

	// Token: 0x04003545 RID: 13637
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2A_01.prefab:6d84e27d81f910c40bbd4e5ba577a581");

	// Token: 0x04003546 RID: 13638
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2B_01.prefab:8c675c5b762b25846be298354893ceca");

	// Token: 0x04003547 RID: 13639
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2C_01.prefab:633c3a18d8e1b3849a627c6058f5d4e3");

	// Token: 0x04003548 RID: 13640
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song2D_01.prefab:df121bc515d9fe940853a5fc6de0aa80");

	// Token: 0x04003549 RID: 13641
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3A_01.prefab:7bd1910702d44ed4f8914cab4375f0d5");

	// Token: 0x0400354A RID: 13642
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3B_01.prefab:db88baed4964457498d39a08d7e852a9");

	// Token: 0x0400354B RID: 13643
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3C_01.prefab:b67a33acc2bab62468194ccd6700dffb");

	// Token: 0x0400354C RID: 13644
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Song3D_01.prefab:f0d2f6830d3ef74419a6fddfdb383faf");

	// Token: 0x0400354D RID: 13645
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_01.prefab:56af47117277b9e429bf3c7e59633d18");

	// Token: 0x0400354E RID: 13646
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_02.prefab:c824cad0fcdc132409358f46526f2610");

	// Token: 0x0400354F RID: 13647
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_Steal_03.prefab:b81beae0c979cdf4c8fcc651c5dd9dd3");

	// Token: 0x04003550 RID: 13648
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_StealDoomsayer_01.prefab:44f418a3c657052439d81529f24d64ed");

	// Token: 0x04003551 RID: 13649
	private static readonly AssetReference VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01 = new AssetReference("VO_LOOTA_BOSS_51h_Male_Dwarf_StealMinstrel_01.prefab:8cfb6f7ad88bd1443b6047f37bd70954");

	// Token: 0x04003552 RID: 13650
	private static readonly AssetReference RussellTheBard_Short_Strum_Underlay = new AssetReference("RussellTheBard_Short_Strum_Underlay.prefab:09e52281d7049da458f12cb79ee395da");

	// Token: 0x04003553 RID: 13651
	private static readonly AssetReference RussellTheBard_Song_Underlay_part1 = new AssetReference("RussellTheBard_Song_Underlay_part1.prefab:ea1958f63cb2e40469adc2c5803c061e");

	// Token: 0x04003554 RID: 13652
	private static readonly AssetReference RussellTheBard_Song_Underlay_part2 = new AssetReference("RussellTheBard_Song_Underlay_part2.prefab:f3dd880fa456f6e4aa48b7af55ddd2c6");

	// Token: 0x04003555 RID: 13653
	private static readonly AssetReference RussellTheBard_Song3A_01_Underlay = new AssetReference("RussellTheBard_Song3A_01_Underlay.prefab:ce68b6200ed0a7543acb8776ee1a78d7");

	// Token: 0x04003556 RID: 13654
	private static readonly AssetReference RussellTheBard_Song3B_01_Underlay = new AssetReference("RussellTheBard_Song3B_01_Underlay.prefab:eb9516af5cca1cf4c95f3455ef3a7fc1");

	// Token: 0x04003557 RID: 13655
	private static readonly AssetReference RussellTheBard_Song3C_01_Underlay = new AssetReference("RussellTheBard_Song3C_01_Underlay.prefab:a69e44df16d21b548becbed9fe9cfb03");

	// Token: 0x04003558 RID: 13656
	private static readonly AssetReference RussellTheBard_Song3D_01_Underlay = new AssetReference("RussellTheBard_Song3D_01_Underlay.prefab:08d078248c7b20647aa0cbf0b2bf79e4");
}
