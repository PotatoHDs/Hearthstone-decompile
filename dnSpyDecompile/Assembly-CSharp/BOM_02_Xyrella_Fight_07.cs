using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200056F RID: 1391
public class BOM_02_Xyrella_Fight_07 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004D2D RID: 19757 RVA: 0x00198738 File Offset: 0x00196938
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01,
			BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D2E RID: 19758 RVA: 0x0019897C File Offset: 0x00196B7C
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004D2F RID: 19759 RVA: 0x00198984 File Offset: 0x00196B84
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004D30 RID: 19760 RVA: 0x0019898C File Offset: 0x00196B8C
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BT_FinalBoss;
		base.OnCreateGame();
	}

	// Token: 0x06004D31 RID: 19761 RVA: 0x0019899F File Offset: 0x00196B9F
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
		case 504:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02);
			GameState.Get().SetBusy(false);
			break;
		case 505:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03);
			GameState.Get().SetBusy(false);
			break;
		case 506:
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_07.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01);
			GameState.Get().SetBusy(false);
			break;
		default:
			switch (missionEvent)
			{
			case 514:
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02);
				break;
			case 515:
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01);
				break;
			case 516:
				yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01);
				break;
			case 517:
				yield return base.MissionPlayVO(enemyActor, this.m_InGameBossIdleLines);
				break;
			default:
				yield return base.HandleMissionEventWithTiming(missionEvent);
				break;
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004D32 RID: 19762 RVA: 0x001989B5 File Offset: 0x00196BB5
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

	// Token: 0x06004D33 RID: 19763 RVA: 0x001989CB File Offset: 0x00196BCB
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

	// Token: 0x06004D34 RID: 19764 RVA: 0x001989E1 File Offset: 0x00196BE1
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
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02);
			break;
		case 5:
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02);
			break;
		case 7:
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01);
			break;
		case 9:
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02);
			break;
		case 11:
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01);
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02);
			break;
		case 13:
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01);
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02);
			yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03);
			break;
		case 15:
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01);
			break;
		case 17:
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_07.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01);
			break;
		}
		yield break;
	}

	// Token: 0x0400430F RID: 17167
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Death_01.prefab:2785bc841d610b549a4f4e868b226db4");

	// Token: 0x04004310 RID: 17168
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7EmoteResponse_01.prefab:43b35f0e54999a145a9c60206232352c");

	// Token: 0x04004311 RID: 17169
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01.prefab:911c91eea8a04dc4ab213687cb726ccb");

	// Token: 0x04004312 RID: 17170
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02.prefab:bb82e530821b8564b89fa29b79b6506a");

	// Token: 0x04004313 RID: 17171
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03.prefab:f94e4f9bd1a731c40bda668e95d0ee42");

	// Token: 0x04004314 RID: 17172
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01.prefab:0b5d0918b550f1d49b0eb996ab71dac0");

	// Token: 0x04004315 RID: 17173
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02.prefab:a6606ccf7bf663e4d96219c8e617a137");

	// Token: 0x04004316 RID: 17174
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03.prefab:7ab93e64ad861d644b9079cb26a50b69");

	// Token: 0x04004317 RID: 17175
	private static readonly AssetReference VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02 = new AssetReference("VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02.prefab:60b14b55b9e83854983a7f101855a31a");

	// Token: 0x04004318 RID: 17176
	private static readonly AssetReference VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02 = new AssetReference("VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02.prefab:daf1699f5539a454281ebe83a0b0d04c");

	// Token: 0x04004319 RID: 17177
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission7Loss_01.prefab:c23d06e87d09c3840bc8306ecd635d88");

	// Token: 0x0400431A RID: 17178
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeA_01.prefab:a9bbcdca6d682d84a8139ecb2d8cc0fc");

	// Token: 0x0400431B RID: 17179
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeB_01.prefab:db1bb406fde184a4c8a1d7ede25c14d2");

	// Token: 0x0400431C RID: 17180
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeD_01.prefab:39cea72b98ba90e4388bb999aa46d036");

	// Token: 0x0400431D RID: 17181
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeF_03.prefab:2674a36f28f955b4e93d23b50be9a067");

	// Token: 0x0400431E RID: 17182
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_01.prefab:238288c4b67cbda42be01705322d4c45");

	// Token: 0x0400431F RID: 17183
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7ExchangeJ_02.prefab:8b4c332fa00107244962e8d42eab7eb5");

	// Token: 0x04004320 RID: 17184
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01.prefab:3866a8b1dc2edb14ba269737feae1c4a");

	// Token: 0x04004321 RID: 17185
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03.prefab:b279a43ac5e607f44a26965aecfa3c20");

	// Token: 0x04004322 RID: 17186
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeA_02.prefab:46e359c07fdd9fc48b5de45448f10e75");

	// Token: 0x04004323 RID: 17187
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeB_02.prefab:be4cf4584f7b01e4a81681d610e78e9a");

	// Token: 0x04004324 RID: 17188
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeC_01.prefab:d35cde4756df6be48a6c4597fd4fcbb2");

	// Token: 0x04004325 RID: 17189
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeD_02.prefab:50c37180ca083be468b83d9e03c83faa");

	// Token: 0x04004326 RID: 17190
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_01.prefab:a711bd9a47c1e6e46aee2ceb52f9353f");

	// Token: 0x04004327 RID: 17191
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeE_02.prefab:a1d144f136489b441a0dcefc220b332b");

	// Token: 0x04004328 RID: 17192
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeF_01.prefab:d97e49208ecbac149bc88b96a4815739");

	// Token: 0x04004329 RID: 17193
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission7ExchangeH_01.prefab:1addf7e6cc01df444be97fb0ebbebfdd");

	// Token: 0x0400432A RID: 17194
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeF_02.prefab:bb948b43461e13b48807951069c3dfc2");

	// Token: 0x0400432B RID: 17195
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7ExchangeG_01.prefab:62e7ec9745226ed409bdad62fce73b34");

	// Token: 0x0400432C RID: 17196
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01.prefab:e6bba64e2009eba4eba57204a98d83ac");

	// Token: 0x0400432D RID: 17197
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_01,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_02,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7HeroPower_03
	};

	// Token: 0x0400432E RID: 17198
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_01,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_02,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Idle_03
	};

	// Token: 0x0400432F RID: 17199
	private List<string> m_IntroductionLines = new List<string>
	{
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_0CT0Bot_Male_Mech_Story_Xyrella_Mission7Intro_02,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Intro_01
	};

	// Token: 0x04004330 RID: 17200
	private List<string> m_missionEventTriggerInGame_VictoryPreExplosionLines = new List<string>
	{
		BOM_02_Xyrella_Fight_07.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission7Victory_01,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_Scabbs_Male_Gnome_Story_Xyrella_Mission7Victory_02,
		BOM_02_Xyrella_Fight_07.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission7Victory_03
	};

	// Token: 0x04004331 RID: 17201
	private HashSet<string> m_playedLines = new HashSet<string>();
}
