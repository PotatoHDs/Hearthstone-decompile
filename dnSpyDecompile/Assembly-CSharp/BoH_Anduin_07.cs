using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000514 RID: 1300
public class BoH_Anduin_07 : BoH_Anduin_Dungeon
{
	// Token: 0x0600463A RID: 17978 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600463B RID: 17979 RVA: 0x0017B01C File Offset: 0x0017921C
	public BoH_Anduin_07()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_07.s_booleanOptions);
	}

	// Token: 0x0600463C RID: 17980 RVA: 0x0017B0FC File Offset: 0x001792FC
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01,
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01,
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02,
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01,
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01,
			BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01,
			BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600463D RID: 17981 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600463E RID: 17982 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600463F RID: 17983 RVA: 0x0017B2C0 File Offset: 0x001794C0
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004640 RID: 17984 RVA: 0x0017B2CF File Offset: 0x001794CF
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004641 RID: 17985 RVA: 0x0017B2D7 File Offset: 0x001794D7
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x06004642 RID: 17986 RVA: 0x0017B2DF File Offset: 0x001794DF
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BT;
		this.m_standardEmoteResponseLine = BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01;
	}

	// Token: 0x06004643 RID: 17987 RVA: 0x0017B302 File Offset: 0x00179502
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 103)
		{
			if (missionEvent == 101)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01);
				GameState.Get().SetBusy(false);
				goto IL_239;
			}
			if (missionEvent == 103)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03);
				GameState.Get().SetBusy(false);
				goto IL_239;
			}
		}
		else
		{
			if (missionEvent == 112)
			{
				yield return base.MissionPlayVOOnce(actor, this.m_missionEventTrigger502Lines);
				goto IL_239;
			}
			if (missionEvent == 507)
			{
				GameState.Get().SetBusy(true);
				yield return base.MissionPlayVO(actor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01);
				GameState.Get().SetBusy(false);
				goto IL_239;
			}
			if (missionEvent == 515)
			{
				yield return base.MissionPlayVO(actor, this.m_standardEmoteResponseLine);
				goto IL_239;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_239:
		yield break;
	}

	// Token: 0x06004644 RID: 17988 RVA: 0x0017B318 File Offset: 0x00179518
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

	// Token: 0x06004645 RID: 17989 RVA: 0x0017B32E File Offset: 0x0017952E
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

	// Token: 0x06004646 RID: 17990 RVA: 0x0017B344 File Offset: 0x00179544
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
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02);
				}
			}
			else
			{
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_07.VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x0400395F RID: 14687
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_07.InitBooleanOptions();

	// Token: 0x04003960 RID: 14688
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01.prefab:d845d3bfb2f6fe04a9bb3741f0b5b713");

	// Token: 0x04003961 RID: 14689
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01.prefab:2164189090f6aa94fb75474f122ad6ed");

	// Token: 0x04003962 RID: 14690
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02.prefab:b78ac66284a921a4999d7532fd3bf8f8");

	// Token: 0x04003963 RID: 14691
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01.prefab:345fe423e3c1cc748b05493f17fef94d");

	// Token: 0x04003964 RID: 14692
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01.prefab:296ae0a731f235a46b149eb9b1924e9e");

	// Token: 0x04003965 RID: 14693
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03.prefab:a34a3051225d65549a3ceced41813c2b");

	// Token: 0x04003966 RID: 14694
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01.prefab:6283d04613c321442a92ae360aa6fcdc");

	// Token: 0x04003967 RID: 14695
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01.prefab:ee51597d2de69e342bf4b28f7564a22d");

	// Token: 0x04003968 RID: 14696
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02.prefab:854c94bdc042c6e4b9939f34f9172bb0");

	// Token: 0x04003969 RID: 14697
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01.prefab:8a127d6e4493e7e4cb950dac500e94cf");

	// Token: 0x0400396A RID: 14698
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01.prefab:b64cf8e781652a44aaf370799ed10a85");

	// Token: 0x0400396B RID: 14699
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02.prefab:f1bc0f0d15f00fe4998bafd5945917cc");

	// Token: 0x0400396C RID: 14700
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03.prefab:f8290f36cfb34e5458fd016b8b050a7f");

	// Token: 0x0400396D RID: 14701
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01.prefab:0641e7c422bb3e140a3d4d471c760e49");

	// Token: 0x0400396E RID: 14702
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02.prefab:fe4962465803a8246b989c0bd3a9290a");

	// Token: 0x0400396F RID: 14703
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03.prefab:9c9223d9342edf34587fe9b899fbf297");

	// Token: 0x04003970 RID: 14704
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01.prefab:a0fc887b9b603a74bb2787143873a5b3");

	// Token: 0x04003971 RID: 14705
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02.prefab:028c060e4f253554c81ff691832b723b");

	// Token: 0x04003972 RID: 14706
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03.prefab:041d9c3886db4794aa7428b5c47e7eb7");

	// Token: 0x04003973 RID: 14707
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01.prefab:8b58e873ffa31e44a9ac79ccd47e8060");

	// Token: 0x04003974 RID: 14708
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01.prefab:b56aa1f374b497a4aa31dd654030e220");

	// Token: 0x04003975 RID: 14709
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01.prefab:0b235d11449ec4c44bfce28ec1506a66");

	// Token: 0x04003976 RID: 14710
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03
	};

	// Token: 0x04003977 RID: 14711
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03
	};

	// Token: 0x04003978 RID: 14712
	private List<string> m_missionEventTrigger502Lines = new List<string>
	{
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02,
		BoH_Anduin_07.VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03
	};

	// Token: 0x04003979 RID: 14713
	private HashSet<string> m_playedLines = new HashSet<string>();
}
