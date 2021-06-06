using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000512 RID: 1298
public class BoH_Anduin_05 : BoH_Anduin_Dungeon
{
	// Token: 0x06004613 RID: 17939 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004614 RID: 17940 RVA: 0x0017A5BC File Offset: 0x001787BC
	public BoH_Anduin_05()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_05.s_booleanOptions);
	}

	// Token: 0x06004615 RID: 17941 RVA: 0x0017A660 File Offset: 0x00178860
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeA_02,
			BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeB_01,
			BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeC_02,
			BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Intro_02,
			BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Victory_02,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5_Victory_02,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5EmoteResponse_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeA_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeB_03,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_03,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_02,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_03,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_02,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_03,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Intro_01,
			BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Loss_01,
			BoH_Anduin_05.VO_Story_Minion_HordeAdventurer_Female_Orc_Story_Anduin_Mission5_Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004616 RID: 17942 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004617 RID: 17943 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004618 RID: 17944 RVA: 0x0017A804 File Offset: 0x00178A04
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004619 RID: 17945 RVA: 0x0017A813 File Offset: 0x00178A13
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x0600461A RID: 17946 RVA: 0x0017A81B File Offset: 0x00178A1B
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x0600461B RID: 17947 RVA: 0x0017A823 File Offset: 0x00178A23
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BT;
		this.m_standardEmoteResponseLine = BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5EmoteResponse_01;
	}

	// Token: 0x0600461C RID: 17948 RVA: 0x0017A846 File Offset: 0x00178A46
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
		if (missionEvent != 101)
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
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(base.GetEnemyActorByCardId("Story_05_HordeAdventurer"), BoH_Anduin_05.VO_Story_Minion_HordeAdventurer_Female_Orc_Story_Anduin_Mission5_Victory_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5_Victory_02);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600461D RID: 17949 RVA: 0x0017A85C File Offset: 0x00178A5C
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

	// Token: 0x0600461E RID: 17950 RVA: 0x0017A872 File Offset: 0x00178A72
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

	// Token: 0x0600461F RID: 17951 RVA: 0x0017A888 File Offset: 0x00178A88
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
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_01);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeC_02);
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeB_01);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeB_03);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_05.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x0400392A RID: 14634
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_05.InitBooleanOptions();

	// Token: 0x0400392B RID: 14635
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeA_02.prefab:bc61c1e319bfb5d4b94720110a1189a7");

	// Token: 0x0400392C RID: 14636
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeB_01.prefab:8b2fdc4eac40aaa4d9cfe2cc67cbd72b");

	// Token: 0x0400392D RID: 14637
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5ExchangeC_02.prefab:5376724e895eb404c8f1ad6083d68a38");

	// Token: 0x0400392E RID: 14638
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Intro_02.prefab:2c701b7a27367c646850c2cdeed77346");

	// Token: 0x0400392F RID: 14639
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission5Victory_02.prefab:dfd3c765dc71a4d479c24a5dda824df7");

	// Token: 0x04003930 RID: 14640
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5_Victory_02 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5_Victory_02.prefab:0ceb5ef3b9d04334da52899fb3a71d67");

	// Token: 0x04003931 RID: 14641
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5EmoteResponse_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5EmoteResponse_01.prefab:b316228640c03d149a9772f08640ce2d");

	// Token: 0x04003932 RID: 14642
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeA_01.prefab:ae486fe9671c0134dbdb96bc845907d3");

	// Token: 0x04003933 RID: 14643
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeB_03 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeB_03.prefab:f892e3d0f01a0b648b282ededb205e71");

	// Token: 0x04003934 RID: 14644
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_01.prefab:418169ac580637049a4a4a2566eb52e7");

	// Token: 0x04003935 RID: 14645
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_03 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5ExchangeC_03.prefab:f876d8b651f658b47a1a90668f1d2db3");

	// Token: 0x04003936 RID: 14646
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_01.prefab:815967509a6616343848ab05b550690a");

	// Token: 0x04003937 RID: 14647
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_02 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_02.prefab:644895d0e04984947b918db903becc9e");

	// Token: 0x04003938 RID: 14648
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_03 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_03.prefab:9fe4ff8f29377ce42bf5cde8c36eeb35");

	// Token: 0x04003939 RID: 14649
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_01.prefab:13e69112490f47148918f0a6926b348b");

	// Token: 0x0400393A RID: 14650
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_02 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_02.prefab:07be5a64ffe5e7e41957030a96e9b798");

	// Token: 0x0400393B RID: 14651
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_03 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_03.prefab:e7bfd48dd09747a4ca4fbfaa1ebec1b3");

	// Token: 0x0400393C RID: 14652
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Intro_01.prefab:68ae44cfd8533a14c9ace692fcd3e560");

	// Token: 0x0400393D RID: 14653
	private static readonly AssetReference VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Loss_01 = new AssetReference("VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Loss_01.prefab:3cd28516b1aff7e4c89694f97ccf3658");

	// Token: 0x0400393E RID: 14654
	private static readonly AssetReference VO_Story_Minion_HordeAdventurer_Female_Orc_Story_Anduin_Mission5_Victory_01 = new AssetReference("VO_Story_Minion_HordeAdventurer_Female_Orc_Story_Anduin_Mission5_Victory_01.prefab:c84554ab589247040b070341fa4fe95d");

	// Token: 0x0400393F RID: 14655
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_01,
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_02,
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5HeroPower_03
	};

	// Token: 0x04003940 RID: 14656
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_01,
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_02,
		BoH_Anduin_05.VO_Story_Hero_Nazgrim_Male_Orc_Story_Anduin_Mission5Idle_03
	};

	// Token: 0x04003941 RID: 14657
	private HashSet<string> m_playedLines = new HashSet<string>();
}
