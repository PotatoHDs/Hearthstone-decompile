using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056E RID: 1390
public class BOM_02_Xyrella_Fight_06 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004D20 RID: 19744 RVA: 0x001982E0 File Offset: 0x001964E0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01,
			BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D21 RID: 19745 RVA: 0x001984C4 File Offset: 0x001966C4
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004D22 RID: 19746 RVA: 0x001984CC File Offset: 0x001966CC
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004D23 RID: 19747 RVA: 0x00198078 File Offset: 0x00196278
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BOT;
		base.OnCreateGame();
	}

	// Token: 0x06004D24 RID: 19748 RVA: 0x001984D4 File Offset: 0x001966D4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01);
					break;
				case 517:
					yield return base.MissionPlayVO(enemyActor, this.m_InGameBossIdleLines);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004D25 RID: 19749 RVA: 0x001984EA File Offset: 0x001966EA
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

	// Token: 0x06004D26 RID: 19750 RVA: 0x00198500 File Offset: 0x00196700
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

	// Token: 0x06004D27 RID: 19751 RVA: 0x00198516 File Offset: 0x00196716
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 11)
		{
			if (turn != 3)
			{
				switch (turn)
				{
				case 7:
					yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01);
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02);
					break;
				case 9:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01);
					break;
				case 11:
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01);
					yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02);
					break;
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02);
			}
		}
		else if (turn != 15)
		{
			if (turn == 17)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_06.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01);
				yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_06.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02);
		}
		yield break;
	}

	// Token: 0x040042F4 RID: 17140
	private static readonly AssetReference VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission6Victory_01.prefab:a1fc16514848d5641a7e7dce007d9ed2");

	// Token: 0x040042F5 RID: 17141
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Death_01.prefab:09e1677e153cf50418020816f5f5cfc7");

	// Token: 0x040042F6 RID: 17142
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6EmoteResponse_01.prefab:0704f6ff4b9ad8f46a8ea058878a2b7e");

	// Token: 0x040042F7 RID: 17143
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01.prefab:d69aaff20b2436c40a4086ed12cc0f2e");

	// Token: 0x040042F8 RID: 17144
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02.prefab:036bb61ac57cd89408c1a24a88f0ad8a");

	// Token: 0x040042F9 RID: 17145
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03.prefab:319b2ad12d1b2844ea5d4ba03b9620e8");

	// Token: 0x040042FA RID: 17146
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01.prefab:23d79e2afc9274842a22b7d3f130bf41");

	// Token: 0x040042FB RID: 17147
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02.prefab:501e0696e2698cd4d9fcfbc9509c497c");

	// Token: 0x040042FC RID: 17148
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03.prefab:52860b615404ef447b5d08335fd4b2f0");

	// Token: 0x040042FD RID: 17149
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Intro_02.prefab:1066e7d924814b241ba79d00ba6d2e37");

	// Token: 0x040042FE RID: 17150
	private static readonly AssetReference VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01 = new AssetReference("VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Loss_01.prefab:e17ecbcf0163a3744a14ca0c13bea323");

	// Token: 0x040042FF RID: 17151
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeA_02.prefab:7164607a1c8c8044e8728b327c3f31ca");

	// Token: 0x04004300 RID: 17152
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeC_01.prefab:6a5ca7f24b8d19a419e38ac0b4664626");

	// Token: 0x04004301 RID: 17153
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6ExchangeF_01.prefab:feb5cc73fee7572409f8fb6e5d9b1169");

	// Token: 0x04004302 RID: 17154
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Intro_01.prefab:178f2f835dc377b4294e5d0ab2d4fe2b");

	// Token: 0x04004303 RID: 17155
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission6Victory_02.prefab:f37054c6319be9f47832ec4dfc3c2346");

	// Token: 0x04004304 RID: 17156
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeA_01.prefab:c05d6e353d567404e845547b844a8370");

	// Token: 0x04004305 RID: 17157
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeB_01.prefab:476d4a12ee01cb14fb6469b9a79cab19");

	// Token: 0x04004306 RID: 17158
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeD_02.prefab:1514d4d8ba7386b4ea220beaf26ab763");

	// Token: 0x04004307 RID: 17159
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeE_02.prefab:374918a1a9d485848947d64661187b4c");

	// Token: 0x04004308 RID: 17160
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission6ExchangeF_02.prefab:ca21672a5fd2e4246bc74411a79cf98c");

	// Token: 0x04004309 RID: 17161
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeB_02.prefab:69d970118b7c76b409a6ea45dcead83a");

	// Token: 0x0400430A RID: 17162
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeD_01.prefab:841221a8530bd7040a66a0186453d7c1");

	// Token: 0x0400430B RID: 17163
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission6ExchangeE_01.prefab:3206ae84589b22b42a2a681e0628e301");

	// Token: 0x0400430C RID: 17164
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_01,
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_02,
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6HeroPower_03
	};

	// Token: 0x0400430D RID: 17165
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_01,
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_02,
		BOM_02_Xyrella_Fight_06.VO_Story_Hero_Sniggles_Male_Goblin_Story_Xyrella_Mission6Idle_03
	};

	// Token: 0x0400430E RID: 17166
	private HashSet<string> m_playedLines = new HashSet<string>();
}
