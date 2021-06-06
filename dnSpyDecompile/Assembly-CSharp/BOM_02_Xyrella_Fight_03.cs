using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056B RID: 1387
public class BOM_02_Xyrella_Fight_03 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004CFB RID: 19707 RVA: 0x00197814 File Offset: 0x00195A14
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02,
			BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01,
			BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02,
			BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004CFC RID: 19708 RVA: 0x001970B8 File Offset: 0x001952B8
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		base.OnCreateGame();
	}

	// Token: 0x06004CFD RID: 19709 RVA: 0x00197978 File Offset: 0x00195B78
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
					yield return base.MissionPlayVO(actor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01);
					break;
				default:
					yield return base.HandleMissionEventWithTiming(missionEvent);
					break;
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(this.Tavish_BrassRing, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004CFE RID: 19710 RVA: 0x0019798E File Offset: 0x00195B8E
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

	// Token: 0x06004CFF RID: 19711 RVA: 0x001979A4 File Offset: 0x00195BA4
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

	// Token: 0x06004D00 RID: 19712 RVA: 0x001979BA File Offset: 0x00195BBA
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 3)
		{
			switch (turn)
			{
			case 7:
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01);
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02);
				break;
			case 9:
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02);
				break;
			case 11:
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01);
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02);
				break;
			case 13:
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01);
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01);
				break;
			}
		}
		else
		{
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_03.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x040042B5 RID: 17077
	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Death_01.prefab:553284cfa6f1d08438489f4821b73959");

	// Token: 0x040042B6 RID: 17078
	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3EmoteResponse_01.prefab:3dacab2bc8d18b44eacaf5a2c54a8b32");

	// Token: 0x040042B7 RID: 17079
	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02.prefab:aa34d03773f3840439f7957deda6aa38");

	// Token: 0x040042B8 RID: 17080
	private static readonly AssetReference VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01 = new AssetReference("VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Loss_01.prefab:0da5c22213931e94d889caf9cb6807aa");

	// Token: 0x040042B9 RID: 17081
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission3Victory_01.prefab:691298fed9f3b1445980963ba41f0e4c");

	// Token: 0x040042BA RID: 17082
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeA_02.prefab:4ea018362cea40a4e9935a5f8ac76839");

	// Token: 0x040042BB RID: 17083
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeB_01.prefab:2059c62d543c40c48b65f5cb947c3fb0");

	// Token: 0x040042BC RID: 17084
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeC_02.prefab:8a83e11d3b77e8f4495118d8d37ab7af");

	// Token: 0x040042BD RID: 17085
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeD_01.prefab:d70b4ae4d2abbae44b5ae589137bcd6c");

	// Token: 0x040042BE RID: 17086
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3ExchangeE_01.prefab:76000e866a1703a42994f4d532f49209");

	// Token: 0x040042BF RID: 17087
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01.prefab:ea38f3e5de7cf7840b102e948eb25c85");

	// Token: 0x040042C0 RID: 17088
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeA_01.prefab:225ced5594e5a934cacbacd4f0afe010");

	// Token: 0x040042C1 RID: 17089
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeB_02.prefab:c5ad790cfc4e7744786a63f25ebd6336");

	// Token: 0x040042C2 RID: 17090
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeC_01.prefab:45681a4188d755b458f980b08041955f");

	// Token: 0x040042C3 RID: 17091
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeD_02.prefab:f49514e58ec3d2542af0e7b6bf8abe9c");

	// Token: 0x040042C4 RID: 17092
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission3ExchangeF_01.prefab:361f89aed28b8cb49aa078a174a1424b");

	// Token: 0x040042C5 RID: 17093
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_02_Xyrella_Fight_03.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission3Intro_01,
		BOM_02_Xyrella_Fight_03.VO_Story_Hero_SludgeBeast_Male_Elemental_Story_Xyrella_Mission3Intro_02
	};

	// Token: 0x040042C6 RID: 17094
	private HashSet<string> m_playedLines = new HashSet<string>();
}
