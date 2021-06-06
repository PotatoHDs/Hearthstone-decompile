using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000569 RID: 1385
public class BOM_02_Xyrella_Fight_01 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004CE2 RID: 19682 RVA: 0x00196EEC File Offset: 0x001950EC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1EmoteResponse_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeA_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeC_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_03,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_03,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Intro_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Loss_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Victory_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_03,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeC_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeD_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Intro_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Victory_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeA_01,
			BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeB_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeD_02,
			BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Death_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004CE3 RID: 19683 RVA: 0x001970B0 File Offset: 0x001952B0
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGame_BossIdleLines;
	}

	// Token: 0x06004CE4 RID: 19684 RVA: 0x001970B8 File Offset: 0x001952B8
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		base.OnCreateGame();
	}

	// Token: 0x06004CE5 RID: 19685 RVA: 0x001970CB File Offset: 0x001952CB
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 510:
					yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossUsesHeroPower);
					goto IL_2E9;
				case 514:
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Intro_02);
					goto IL_2E9;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1EmoteResponse_01);
					goto IL_2E9;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Death_01);
					goto IL_2E9;
				case 517:
					yield return base.MissionPlayVO(enemyActor, this.m_InGame_BossIdleLines);
					goto IL_2E9;
				}
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Victory_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Victory_02);
			GameState.Get().SetBusy(false);
		}
		IL_2E9:
		yield break;
	}

	// Token: 0x06004CE6 RID: 19686 RVA: 0x001970E1 File Offset: 0x001952E1
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

	// Token: 0x06004CE7 RID: 19687 RVA: 0x001970F7 File Offset: 0x001952F7
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

	// Token: 0x06004CE8 RID: 19688 RVA: 0x0019710D File Offset: 0x0019530D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 5)
		{
			if (turn != 3)
			{
				if (turn == 5)
				{
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_01);
					yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeB_02);
					yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeA_02);
			}
		}
		else if (turn != 9)
		{
			if (turn == 11)
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeD_01);
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_01.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeD_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeC_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_01.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeC_02);
		}
		yield break;
	}

	// Token: 0x0400427F RID: 17023
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1EmoteResponse_01.prefab:1fe368044aa8b4e4cac006302f30302b");

	// Token: 0x04004280 RID: 17024
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeA_02.prefab:48772f7bb231606469500a146063c6fb");

	// Token: 0x04004281 RID: 17025
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1ExchangeC_01.prefab:dc542ec51bdbc97498754dec4125394b");

	// Token: 0x04004282 RID: 17026
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_01.prefab:cef6e200853b4374f97ca0abaf54694d");

	// Token: 0x04004283 RID: 17027
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_02.prefab:f7bab445b60dfac46905a4ee134efc3b");

	// Token: 0x04004284 RID: 17028
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_03.prefab:520f3c3ac227ecb4086fe5fb668277c8");

	// Token: 0x04004285 RID: 17029
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_01.prefab:1d5d92623344da240a1dc70c08a85fdf");

	// Token: 0x04004286 RID: 17030
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_02.prefab:a12f277c44a46c3418262551a8c9695e");

	// Token: 0x04004287 RID: 17031
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_03.prefab:842f91c36e776014ea7e4e605c588542");

	// Token: 0x04004288 RID: 17032
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Intro_02.prefab:d9a54719e142d7545bf9a2358298c272");

	// Token: 0x04004289 RID: 17033
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Loss_01.prefab:43da8e9cd3932ec4597363d50040f58c");

	// Token: 0x0400428A RID: 17034
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Victory_01.prefab:1f24bd498b2325e4e8e8c1026371cc9a");

	// Token: 0x0400428B RID: 17035
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_01.prefab:ae51ed81d3f055049a524ef7beead920");

	// Token: 0x0400428C RID: 17036
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeB_03.prefab:663c3d4894946e94cab64ae4a93d4475");

	// Token: 0x0400428D RID: 17037
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeC_02.prefab:6d129dc44301efa4c8835bab75cf66dc");

	// Token: 0x0400428E RID: 17038
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1ExchangeD_01.prefab:7fc7ecedc87115b4db95ca22f4519d39");

	// Token: 0x0400428F RID: 17039
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Intro_01.prefab:db2d10b0c6deaae49897441b8340acb5");

	// Token: 0x04004290 RID: 17040
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission1Victory_02.prefab:9a204822c5ab84c45bd792cd9742a61a");

	// Token: 0x04004291 RID: 17041
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeA_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeA_01.prefab:40e8594fdb81b604eb525c33c032712d");

	// Token: 0x04004292 RID: 17042
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeB_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeB_02.prefab:d4739b1eded50e64a8e50e2eb4fca707");

	// Token: 0x04004293 RID: 17043
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeD_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission1ExchangeD_02.prefab:02e9fc250f1188249a9cbab31f0a21a1");

	// Token: 0x04004294 RID: 17044
	private static readonly AssetReference VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Death_01 = new AssetReference("VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Death_01.prefab:22f0d0660ef24254e9bf74a49d455632");

	// Token: 0x04004295 RID: 17045
	private List<string> m_InGame_BossUsesHeroPower = new List<string>
	{
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_01,
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_02,
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1HeroPower_03
	};

	// Token: 0x04004296 RID: 17046
	private List<string> m_InGame_BossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_01,
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_02,
		BOM_02_Xyrella_Fight_01.VO_Story_Hero_Kargal_Male_Orc_Story_Xyrella_Mission1Idle_03
	};

	// Token: 0x04004297 RID: 17047
	private HashSet<string> m_playedLines = new HashSet<string>();
}
