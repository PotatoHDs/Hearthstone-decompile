using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056C RID: 1388
public class BOM_02_Xyrella_Fight_04 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004D06 RID: 19718 RVA: 0x00197B10 File Offset: 0x00195D10
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission4Victory_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Death_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4EmoteResponse_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_02,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_02,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_03,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Intro_02,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Loss_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeA_02,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeB_02,
			BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4Intro_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeA_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeB_01,
			BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeC_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D07 RID: 19719 RVA: 0x00197C74 File Offset: 0x00195E74
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004D08 RID: 19720 RVA: 0x00197C7C File Offset: 0x00195E7C
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004D09 RID: 19721 RVA: 0x00197C84 File Offset: 0x00195E84
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		base.OnCreateGame();
		this.m_deathLine = BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Death_01;
		this.m_standardEmoteResponseLine = BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4EmoteResponse_01;
	}

	// Token: 0x06004D0A RID: 19722 RVA: 0x00197CB7 File Offset: 0x00195EB7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 505)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(actor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Death_01);
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
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_04.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission4Victory_01);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004D0B RID: 19723 RVA: 0x00197CCD File Offset: 0x00195ECD
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

	// Token: 0x06004D0C RID: 19724 RVA: 0x00197CE3 File Offset: 0x00195EE3
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

	// Token: 0x06004D0D RID: 19725 RVA: 0x00197CF9 File Offset: 0x00195EF9
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 3:
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeA_02);
			break;
		case 5:
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeB_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_04.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeB_02);
			break;
		case 7:
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_04.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeC_01);
			break;
		}
		yield break;
	}

	// Token: 0x040042C7 RID: 17095
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission4Victory_01.prefab:3d73e4fcb44ec6a4bb96ced7a1bbe43f");

	// Token: 0x040042C8 RID: 17096
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Death_01 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Death_01.prefab:3758c9bf126787241abe2afbc9e94c92");

	// Token: 0x040042C9 RID: 17097
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4EmoteResponse_01.prefab:f2eb26fd2a8a88d44b05fbfe81cb725f");

	// Token: 0x040042CA RID: 17098
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_01.prefab:9d528ab2cd4b2f44b8915cbc9641ad1b");

	// Token: 0x040042CB RID: 17099
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_02.prefab:748679636a6ca9240a2fb81e3fbf71b7");

	// Token: 0x040042CC RID: 17100
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_01.prefab:ce2c98813ac6fe64a9d647d01a758e57");

	// Token: 0x040042CD RID: 17101
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_02.prefab:1311216202b233a489934d4cc3ceeb60");

	// Token: 0x040042CE RID: 17102
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_03.prefab:31c2763b1b883e3489f7d7225e7683ef");

	// Token: 0x040042CF RID: 17103
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Intro_02.prefab:65650ced288819c42808354f51768fe4");

	// Token: 0x040042D0 RID: 17104
	private static readonly AssetReference VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Loss_01.prefab:0d47f72d52cf92e42a2573445fefb057");

	// Token: 0x040042D1 RID: 17105
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeA_02.prefab:4655f1336d8b9a4419ab9365bb4d7160");

	// Token: 0x040042D2 RID: 17106
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4ExchangeB_02.prefab:5c8c1f43ccb393e4986cc4a9c799d552");

	// Token: 0x040042D3 RID: 17107
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission4Intro_01.prefab:dac9e8c0f05bf1246859e07edd07c096");

	// Token: 0x040042D4 RID: 17108
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeA_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeA_01.prefab:0a45872bb9881fc4c933f41acdc7e5f3");

	// Token: 0x040042D5 RID: 17109
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeB_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeB_01.prefab:b7dc4730e3c5b4c4595fc16122de4c5f");

	// Token: 0x040042D6 RID: 17110
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission4ExchangeC_01.prefab:c2e6381189fd45c41b51d301072aa4d9");

	// Token: 0x040042D7 RID: 17111
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_01,
		BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4HeroPower_02
	};

	// Token: 0x040042D8 RID: 17112
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_01,
		BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_02,
		BOM_02_Xyrella_Fight_04.VO_Story_Hero_Whirleygig_Male_Goblin_Story_Xyrella_Mission4Idle_03
	};

	// Token: 0x040042D9 RID: 17113
	private HashSet<string> m_playedLines = new HashSet<string>();
}
