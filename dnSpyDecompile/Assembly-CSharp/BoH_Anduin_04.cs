using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000511 RID: 1297
public class BoH_Anduin_04 : BoH_Anduin_Dungeon
{
	// Token: 0x06004602 RID: 17922 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004603 RID: 17923 RVA: 0x0017A174 File Offset: 0x00178374
	public BoH_Anduin_04()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_04.s_booleanOptions);
	}

	// Token: 0x06004604 RID: 17924 RVA: 0x0017A218 File Offset: 0x00178418
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeA_02,
			BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeB_02,
			BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeC_02,
			BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Intro_02,
			BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Victory_02,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4EmoteResponse_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeA_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_03,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_03,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_02,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_03,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_02,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_03,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Intro_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Loss_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_01,
			BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004605 RID: 17925 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004606 RID: 17926 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004607 RID: 17927 RVA: 0x0017A3CC File Offset: 0x001785CC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004608 RID: 17928 RVA: 0x0017A3DB File Offset: 0x001785DB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x06004609 RID: 17929 RVA: 0x0017A3E3 File Offset: 0x001785E3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x0600460A RID: 17930 RVA: 0x0017A3EB File Offset: 0x001785EB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BT;
		this.m_standardEmoteResponseLine = BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4EmoteResponse_01;
	}

	// Token: 0x0600460B RID: 17931 RVA: 0x0017A40E File Offset: 0x0017860E
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
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_02);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600460C RID: 17932 RVA: 0x0017A424 File Offset: 0x00178624
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

	// Token: 0x0600460D RID: 17933 RVA: 0x0017A43A File Offset: 0x0017863A
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

	// Token: 0x0600460E RID: 17934 RVA: 0x0017A450 File Offset: 0x00178650
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
			if (turn != 3)
			{
				if (turn == 7)
				{
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_01);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeC_02);
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeB_02);
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_03);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_04.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x04003911 RID: 14609
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_04.InitBooleanOptions();

	// Token: 0x04003912 RID: 14610
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeA_02.prefab:ae5f2899f8b49264f8941693121789d1");

	// Token: 0x04003913 RID: 14611
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeB_02.prefab:57035c975aac3634fbabee05bd2bfad5");

	// Token: 0x04003914 RID: 14612
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4ExchangeC_02.prefab:f82248615609ea14f838ddd7f7fe4135");

	// Token: 0x04003915 RID: 14613
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Intro_02.prefab:0c2129a03c27fe94ca40820ec0f388b0");

	// Token: 0x04003916 RID: 14614
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission4Victory_02.prefab:ad84d064db74aa74386d7242bf7501e0");

	// Token: 0x04003917 RID: 14615
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4EmoteResponse_01.prefab:d6c339d5b9c66e845b4a8f2eb101a1c9");

	// Token: 0x04003918 RID: 14616
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeA_01.prefab:d6920c5f05cf06740892df2d691015f0");

	// Token: 0x04003919 RID: 14617
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_01.prefab:f9ae865db21f3084ea1eee5183e2dc2f");

	// Token: 0x0400391A RID: 14618
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_03 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeB_03.prefab:8f2527609dc832149be5b84e4cfae775");

	// Token: 0x0400391B RID: 14619
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_01.prefab:5f1540d482260404a86189779bec6989");

	// Token: 0x0400391C RID: 14620
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_03 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4ExchangeC_03.prefab:10090a19ddb50c54ca7637c9ef440348");

	// Token: 0x0400391D RID: 14621
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_01.prefab:e4967dd3bb6552f48a2e33178227e572");

	// Token: 0x0400391E RID: 14622
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_02.prefab:2a4ee7df84637f0459370e5298116fdb");

	// Token: 0x0400391F RID: 14623
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_03.prefab:1531b88cbf9f6534fa780c210ce6e826");

	// Token: 0x04003920 RID: 14624
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_01.prefab:48b2044506f6a0a4c94dac5f518aba89");

	// Token: 0x04003921 RID: 14625
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_02.prefab:fd223d6d9f224ba49938d316971902c5");

	// Token: 0x04003922 RID: 14626
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_03.prefab:2778000859c493b40b1fa70a2b026e30");

	// Token: 0x04003923 RID: 14627
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Intro_01.prefab:c123e6db3506ec647859edcbdf018be8");

	// Token: 0x04003924 RID: 14628
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Loss_01.prefab:1f7b699d54d522c41a9d441330821b56");

	// Token: 0x04003925 RID: 14629
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_01.prefab:d2b24436384676f499cd5fa5d7a287f6");

	// Token: 0x04003926 RID: 14630
	private static readonly AssetReference VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_02 = new AssetReference("VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Victory_02.prefab:bc33fbd89561db64dac6c2273f1455b5");

	// Token: 0x04003927 RID: 14631
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_01,
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_02,
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4HeroPower_03
	};

	// Token: 0x04003928 RID: 14632
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_01,
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_02,
		BoH_Anduin_04.VO_Story_Hero_Velen_Male_Draenei_Story_Anduin_Mission4Idle_03
	};

	// Token: 0x04003929 RID: 14633
	private HashSet<string> m_playedLines = new HashSet<string>();
}
