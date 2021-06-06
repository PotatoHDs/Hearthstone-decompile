using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056A RID: 1386
public class BOM_02_Xyrella_Fight_02 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004CEE RID: 19694 RVA: 0x0019732C File Offset: 0x0019552C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01,
			BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03,
			BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02,
			BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004CEF RID: 19695 RVA: 0x00197530 File Offset: 0x00195730
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004CF0 RID: 19696 RVA: 0x00197538 File Offset: 0x00195738
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004CF1 RID: 19697 RVA: 0x00197540 File Offset: 0x00195740
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		this.m_deathLine = BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01;
		this.m_standardEmoteResponseLine = BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01;
	}

	// Token: 0x06004CF2 RID: 19698 RVA: 0x00197573 File Offset: 0x00195773
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (missionEvent)
		{
		case 505:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(this.Tavish_BrassRing, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02);
			GameState.Get().SetBusy(false);
			goto IL_2E7;
		case 506:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01);
			GameState.Get().SetBusy(false);
			goto IL_2E7;
		case 510:
			yield return base.MissionPlayVO(enemyActor, this.m_BossUsesHeroPowerLines);
			goto IL_2E7;
		case 514:
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02);
			goto IL_2E7;
		case 515:
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01);
			goto IL_2E7;
		case 516:
			yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01);
			goto IL_2E7;
		case 517:
			yield return base.MissionPlayVO(enemyActor, this.m_InGameBossIdleLines);
			goto IL_2E7;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_2E7:
		yield break;
	}

	// Token: 0x06004CF3 RID: 19699 RVA: 0x00197589 File Offset: 0x00195789
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

	// Token: 0x06004CF4 RID: 19700 RVA: 0x0019759F File Offset: 0x0019579F
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

	// Token: 0x06004CF5 RID: 19701 RVA: 0x001975B5 File Offset: 0x001957B5
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			if (turn != 5)
			{
				switch (turn)
				{
				case 9:
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01);
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02);
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03);
					break;
				case 11:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01);
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02);
					break;
				case 13:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01);
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02);
					break;
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_02.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x04004298 RID: 17048
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Death_01.prefab:29971ca128573ea488eb4479810e57d9");

	// Token: 0x04004299 RID: 17049
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2EmoteResponse_01.prefab:a3b2b5e278c44794f9833088ecc84cda");

	// Token: 0x0400429A RID: 17050
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2ExchangeA_02.prefab:2aa8528d86e08f5428b445cfca2ce3d8");

	// Token: 0x0400429B RID: 17051
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01.prefab:d441233485d1c704a93bfb861a2323ec");

	// Token: 0x0400429C RID: 17052
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02.prefab:88bcffef7c36ed04196478b1dda606f1");

	// Token: 0x0400429D RID: 17053
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03.prefab:3b6f517ee3e5f00469259ab1f87a2c8e");

	// Token: 0x0400429E RID: 17054
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04.prefab:9c1afe58d526d8940ad5d59660fbb504");

	// Token: 0x0400429F RID: 17055
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05.prefab:abe095c594ea39e4a82e48844729f695");

	// Token: 0x040042A0 RID: 17056
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01.prefab:0e80df0d73bfc7341af096720ff8ef1d");

	// Token: 0x040042A1 RID: 17057
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02.prefab:55a078209836dbc4ebca1be4dbc25b84");

	// Token: 0x040042A2 RID: 17058
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03.prefab:2d48e5dd94b6fdd4485c8602738390a0");

	// Token: 0x040042A3 RID: 17059
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Intro_02.prefab:f598cb03ca6d3284e979c6a3dbe739d4");

	// Token: 0x040042A4 RID: 17060
	private static readonly AssetReference VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Loss_01.prefab:9374a8c18443f72489d46b0a0ab2ad42");

	// Token: 0x040042A5 RID: 17061
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission2Victory_01.prefab:ba447ffa89d528b4dbb32dc61d3baf64");

	// Token: 0x040042A6 RID: 17062
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeA_01.prefab:91655efc0fe21a54e80ba0b9a0f187f6");

	// Token: 0x040042A7 RID: 17063
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeB_02.prefab:0154141375140f242bb00e8958b9b0b6");

	// Token: 0x040042A8 RID: 17064
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeC_02.prefab:699b8a5e3aab5e644a738e48a50e5920");

	// Token: 0x040042A9 RID: 17065
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeD_01.prefab:673ba2478b2b49f429d612852699c386");

	// Token: 0x040042AA RID: 17066
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2ExchangeE_01.prefab:8bd5ee971907ec144bbfaddc471aca87");

	// Token: 0x040042AB RID: 17067
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Intro_01.prefab:92969a5db4073d648991314bdd8a7369");

	// Token: 0x040042AC RID: 17068
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission2Victory_02.prefab:1300ac01e1a15224bbf4b87eb8094297");

	// Token: 0x040042AD RID: 17069
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeB_01.prefab:5ac8d5a0431830b4782343fcaf4dc014");

	// Token: 0x040042AE RID: 17070
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_01.prefab:6b403cd5654bf91438ed8804d5e57336");

	// Token: 0x040042AF RID: 17071
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeC_03.prefab:7ea62a0591cb9ea46a61a5649492d1d9");

	// Token: 0x040042B0 RID: 17072
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeD_02.prefab:ecec765c0ed35d24ab8d12e0fa876af3");

	// Token: 0x040042B1 RID: 17073
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission2ExchangeE_02.prefab:1906283882dbe67408943bc163c2423e");

	// Token: 0x040042B2 RID: 17074
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_01,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_02,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_03,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_04,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2HeroPower_05
	};

	// Token: 0x040042B3 RID: 17075
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_01,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_02,
		BOM_02_Xyrella_Fight_02.VO_Story_Hero_Serena_Female_Harpy_Story_Xyrella_Mission2Idle_03
	};

	// Token: 0x040042B4 RID: 17076
	private HashSet<string> m_playedLines = new HashSet<string>();
}
