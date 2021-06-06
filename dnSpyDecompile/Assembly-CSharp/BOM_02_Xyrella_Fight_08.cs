using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000570 RID: 1392
public class BOM_02_Xyrella_Fight_08 : BOM_02_Xyrella_Dungeon
{
	// Token: 0x06004D3A RID: 19770 RVA: 0x00198CC4 File Offset: 0x00196EC4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01,
			BOM_02_Xyrella_Fight_08.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02,
			BOM_02_Xyrella_Fight_08.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004D3B RID: 19771 RVA: 0x00198E78 File Offset: 0x00197078
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		base.OnCreateGame();
	}

	// Token: 0x06004D3C RID: 19772 RVA: 0x00198E8B File Offset: 0x0019708B
	public override List<string> GetBossIdleLines()
	{
		return this.m_InGameBossIdleLines;
	}

	// Token: 0x06004D3D RID: 19773 RVA: 0x00198E93 File Offset: 0x00197093
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004D3E RID: 19774 RVA: 0x00198E9B File Offset: 0x0019709B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 504)
		{
			if (missionEvent != 506)
			{
				switch (missionEvent)
				{
				case 514:
					yield return base.MissionPlayVO(actor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01);
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02);
					break;
				case 515:
					yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01);
					break;
				case 516:
					yield return base.MissionPlaySound(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01);
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
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01);
			yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_08.VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004D3F RID: 19775 RVA: 0x00198EB1 File Offset: 0x001970B1
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

	// Token: 0x06004D40 RID: 19776 RVA: 0x00198EC7 File Offset: 0x001970C7
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

	// Token: 0x06004D41 RID: 19777 RVA: 0x00198EDD File Offset: 0x001970DD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_08.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01);
				yield return base.MissionPlayVO(enemyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02);
			}
		}
		else if (turn != 11)
		{
			if (turn == 13)
			{
				yield return base.MissionPlayVO("BOM_02_Tavish_01t", BOM_02_Xyrella_Fight_08.VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01);
				yield return base.MissionPlayVO(friendlyActor, BOM_02_Xyrella_Fight_08.VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO("BOM_02_Scabbs_06t", BOM_02_Xyrella_Fight_08.VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01);
		}
		yield break;
	}

	// Token: 0x04004332 RID: 17202
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Death_01.prefab:43f518fbf34efbf4d99270cc9f7a77a6");

	// Token: 0x04004333 RID: 17203
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8EmoteResponse_01.prefab:5cad294295d046a41a4a8cfb445a7bcb");

	// Token: 0x04004334 RID: 17204
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeA_02.prefab:c5ce2ccd7c5ba554b939f6b1c5f8610f");

	// Token: 0x04004335 RID: 17205
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8ExchangeE_01.prefab:bf5d4f843d8a0e64c8627fe9e7555c9e");

	// Token: 0x04004336 RID: 17206
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01.prefab:2d6e853d5d63ef64280f75c901fb1fe7");

	// Token: 0x04004337 RID: 17207
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02.prefab:3cbbfc6c744f52c4796be2104ec2fe9a");

	// Token: 0x04004338 RID: 17208
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03.prefab:4468a374935555842959c268a43d2d4f");

	// Token: 0x04004339 RID: 17209
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01.prefab:36503adb7ae026a49bf1b5885574f38b");

	// Token: 0x0400433A RID: 17210
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02.prefab:ef0047eb852e9b8429fc423f52ad4edd");

	// Token: 0x0400433B RID: 17211
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03.prefab:8722cd897f2df3f46af04dd61bc89e22");

	// Token: 0x0400433C RID: 17212
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Intro_02.prefab:8b202003cfcfa8749818ebacc19f2388");

	// Token: 0x0400433D RID: 17213
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Loss_01.prefab:a495b5af6a8f32c4584c95d532c4b588");

	// Token: 0x0400433E RID: 17214
	private static readonly AssetReference VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01 = new AssetReference("VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Victory_01.prefab:e625a6730201f8949ae09027bdb8f113");

	// Token: 0x0400433F RID: 17215
	private static readonly AssetReference VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02 = new AssetReference("VO_Story_Hero_Tavish_Male_Dwarf_Story_Xyrella_Mission8Victory_02.prefab:0aa0bdf3d3333e64582f82595869b875");

	// Token: 0x04004340 RID: 17216
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeA_01.prefab:612bbdaf98455d54facba8bbcc638e75");

	// Token: 0x04004341 RID: 17217
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8ExchangeD_02.prefab:ceddb5d77c9092844a2cc8f84710232e");

	// Token: 0x04004342 RID: 17218
	private static readonly AssetReference VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Xyrella_Female_Draenei_Story_Xyrella_Mission8Intro_01.prefab:9d4c0dfb98a02b24083ea6757075bde9");

	// Token: 0x04004343 RID: 17219
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeB_01.prefab:833104168fe176c46aee47b8c8d60b0f");

	// Token: 0x04004344 RID: 17220
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeC_01.prefab:371fec3577a5664408d9524aabc6a318");

	// Token: 0x04004345 RID: 17221
	private static readonly AssetReference VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02 = new AssetReference("VO_Story_Minion_Scabbs_Male_Gnome_Story_Xyrella_Mission8ExchangeE_02.prefab:a218db79c0239b841b308b1cbaadd907");

	// Token: 0x04004346 RID: 17222
	private static readonly AssetReference VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01 = new AssetReference("VO_Story_Minion_Tavish_Male_Dwarf_Story_Xyrella_Mission8ExchangeD_01.prefab:89df0d79eb80033459462cbc887a579e");

	// Token: 0x04004347 RID: 17223
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_01,
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_02,
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8HeroPower_03
	};

	// Token: 0x04004348 RID: 17224
	private List<string> m_InGameBossIdleLines = new List<string>
	{
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_01,
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_02,
		BOM_02_Xyrella_Fight_08.VO_Story_Hero_Garona_Female_Orc_Story_Xyrella_Mission8Idle_03
	};

	// Token: 0x04004349 RID: 17225
	private HashSet<string> m_playedLines = new HashSet<string>();
}
