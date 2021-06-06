using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200050F RID: 1295
public class BoH_Anduin_02 : BoH_Anduin_Dungeon
{
	// Token: 0x060045E0 RID: 17888 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060045E1 RID: 17889 RVA: 0x001798B8 File Offset: 0x00177AB8
	public BoH_Anduin_02()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_02.s_booleanOptions);
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x0017995C File Offset: 0x00177B5C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01,
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01,
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01,
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03,
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02,
			BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01,
			BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060045E4 RID: 17892 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x00179B10 File Offset: 0x00177D10
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02);
		yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060045E6 RID: 17894 RVA: 0x00179B1F File Offset: 0x00177D1F
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060045E7 RID: 17895 RVA: 0x00179B27 File Offset: 0x00177D27
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060045E8 RID: 17896 RVA: 0x00179B2F File Offset: 0x00177D2F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		this.m_standardEmoteResponseLine = BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01;
	}

	// Token: 0x060045E9 RID: 17897 RVA: 0x00179B52 File Offset: 0x00177D52
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
		if (missionEvent != 504)
		{
			if (missionEvent != 507)
			{
				if (missionEvent != 515)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					yield return base.MissionPlayVO(enemyActor, this.m_standardEmoteResponseLine);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060045EA RID: 17898 RVA: 0x00179B68 File Offset: 0x00177D68
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

	// Token: 0x060045EB RID: 17899 RVA: 0x00179B7E File Offset: 0x00177D7E
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

	// Token: 0x060045EC RID: 17900 RVA: 0x00179B94 File Offset: 0x00177D94
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 7)
			{
				if (turn == 11)
				{
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01);
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_02.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x040038DD RID: 14557
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_02.InitBooleanOptions();

	// Token: 0x040038DE RID: 14558
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeA_01.prefab:0551229c227e1b449a624ac9a65700c2");

	// Token: 0x040038DF RID: 14559
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeB_01.prefab:a818ca367aee3414183616d8efb6b61a");

	// Token: 0x040038E0 RID: 14560
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_01.prefab:f25ea827c4bc4ce291766cad764e0f94");

	// Token: 0x040038E1 RID: 14561
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2ExchangeC_03.prefab:e7b3f58332e299146ab64219eee6bdc8");

	// Token: 0x040038E2 RID: 14562
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Intro_02.prefab:4d74e9fa7670ad9478148b2a35691ebf");

	// Token: 0x040038E3 RID: 14563
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission2Victory_02.prefab:fd97382211c9ab445bab5d26b98397af");

	// Token: 0x040038E4 RID: 14564
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2EmoteResponse_01.prefab:ff0e37310038d68438b8335d8512f633");

	// Token: 0x040038E5 RID: 14565
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeA_02.prefab:81da409b6daf7214eb601d93ee5354ea");

	// Token: 0x040038E6 RID: 14566
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeB_02.prefab:0d3e7d6e1b634924bbc27715396abbc8");

	// Token: 0x040038E7 RID: 14567
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2ExchangeC_02.prefab:50cb5e6205747fb4d8787c54ea4e3ead");

	// Token: 0x040038E8 RID: 14568
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01.prefab:4e000bc14de100d4a9763f131ee9ce90");

	// Token: 0x040038E9 RID: 14569
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02.prefab:54fc90dae5aaa25459fb98d4faff7610");

	// Token: 0x040038EA RID: 14570
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03.prefab:f9c3c2fa810126042adefe7c5bd7fa84");

	// Token: 0x040038EB RID: 14571
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01.prefab:da2b7baf7f818e343b54d284732c7166");

	// Token: 0x040038EC RID: 14572
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02.prefab:7878d4e85944e1648bcc90f7c95e6ef7");

	// Token: 0x040038ED RID: 14573
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03.prefab:92ac8ac41c8b938469440eb14220ade4");

	// Token: 0x040038EE RID: 14574
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_01.prefab:36cd16043cea5054dbd0995bc4b4a68c");

	// Token: 0x040038EF RID: 14575
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Intro_03.prefab:8da41afa8342dd045baf4b38b097b27f");

	// Token: 0x040038F0 RID: 14576
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Loss_01.prefab:afd0be6876e8e894a81ea9e051f2afe0");

	// Token: 0x040038F1 RID: 14577
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_01.prefab:182b42cb9887c9940997e67d8214d98b");

	// Token: 0x040038F2 RID: 14578
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Victory_02.prefab:4d023795b20ab504c8c5d27f1c6f681a");

	// Token: 0x040038F3 RID: 14579
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_01,
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_02,
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2HeroPower_03
	};

	// Token: 0x040038F4 RID: 14580
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_01,
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_02,
		BoH_Anduin_02.VO_Story_Hero_Jaina_Female_Human_Story_Anduin_Mission2Idle_03
	};

	// Token: 0x040038F5 RID: 14581
	private HashSet<string> m_playedLines = new HashSet<string>();
}
