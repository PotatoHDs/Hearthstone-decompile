using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200050E RID: 1294
public class BoH_Anduin_01 : BoH_Anduin_Dungeon
{
	// Token: 0x060045CF RID: 17871 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060045D0 RID: 17872 RVA: 0x00179494 File Offset: 0x00177694
	public BoH_Anduin_01()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Anduin_01.s_booleanOptions);
	}

	// Token: 0x060045D1 RID: 17873 RVA: 0x00179538 File Offset: 0x00177738
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02,
			BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02,
			BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01,
			BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03,
			BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01,
			BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060045D2 RID: 17874 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060045D3 RID: 17875 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060045D4 RID: 17876 RVA: 0x001796CC File Offset: 0x001778CC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(actor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060045D5 RID: 17877 RVA: 0x001796DB File Offset: 0x001778DB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x060045D6 RID: 17878 RVA: 0x001796E3 File Offset: 0x001778E3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x060045D7 RID: 17879 RVA: 0x001796EB File Offset: 0x001778EB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_Default;
		this.m_standardEmoteResponseLine = BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01;
	}

	// Token: 0x060045D8 RID: 17880 RVA: 0x0017970E File Offset: 0x0017790E
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
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
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
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01);
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060045D9 RID: 17881 RVA: 0x00179724 File Offset: 0x00177924
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

	// Token: 0x060045DA RID: 17882 RVA: 0x0017973A File Offset: 0x0017793A
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

	// Token: 0x060045DB RID: 17883 RVA: 0x00179750 File Offset: 0x00177950
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
				if (turn == 11)
				{
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01);
					yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02);
					yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03);
				}
			}
			else
			{
				yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01);
				yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02);
			}
		}
		else
		{
			yield return base.MissionPlayVO(enemyActor, BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01);
			yield return base.MissionPlayVO(friendlyActor, BoH_Anduin_01.VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02);
		}
		yield break;
	}

	// Token: 0x040038C6 RID: 14534
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Anduin_01.InitBooleanOptions();

	// Token: 0x040038C7 RID: 14535
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeA_02.prefab:aa7cbf7409e919847b1ffae3db4df734");

	// Token: 0x040038C8 RID: 14536
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeB_02.prefab:77fd621472b276747b8debf68792404f");

	// Token: 0x040038C9 RID: 14537
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_01.prefab:24e9a1cd458be0540816b38f6d117770");

	// Token: 0x040038CA RID: 14538
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1ExchangeC_03.prefab:260037a21752be048b754d15a57615e8");

	// Token: 0x040038CB RID: 14539
	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission1Intro_02.prefab:46b083e10f1d6a842b06ab8bc1f2b7c3");

	// Token: 0x040038CC RID: 14540
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1EmoteResponse_01.prefab:f0c14cdcc45ccb842967914e2ecbe99e");

	// Token: 0x040038CD RID: 14541
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeA_01.prefab:5882b1257c7f6ff4a8d2de154c0e42b8");

	// Token: 0x040038CE RID: 14542
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeB_01.prefab:dcdfd8a448d20cc41aeecfbacfb0293d");

	// Token: 0x040038CF RID: 14543
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1ExchangeC_02.prefab:dd88a9e2bcfd57a498ab78a7bac4cc6e");

	// Token: 0x040038D0 RID: 14544
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01.prefab:9652ba1809af8014286b16d7a4526217");

	// Token: 0x040038D1 RID: 14545
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02.prefab:ca4b63ff57c78f84d9c00a2153640e00");

	// Token: 0x040038D2 RID: 14546
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03.prefab:5afe7607307634d40ad7640f43bae35b");

	// Token: 0x040038D3 RID: 14547
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01.prefab:225f2d73c513c7543bae3aa0847e458a");

	// Token: 0x040038D4 RID: 14548
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02.prefab:91f154fec9df19e4c9f205435f347df6");

	// Token: 0x040038D5 RID: 14549
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03.prefab:8009fa680afd8d64bb62c017601aaa1e");

	// Token: 0x040038D6 RID: 14550
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Intro_01.prefab:134d77bcce6b763429f503ec7911c0c1");

	// Token: 0x040038D7 RID: 14551
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Loss_01.prefab:aeb35ee10bcafdf43a751cb0694f4a29");

	// Token: 0x040038D8 RID: 14552
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_01.prefab:a3c75474456c2eb46a0d5af2ba7dde7f");

	// Token: 0x040038D9 RID: 14553
	private static readonly AssetReference VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Victory_02.prefab:55f37cb0cf500bd4f92a0d798373d5a4");

	// Token: 0x040038DA RID: 14554
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_01,
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_02,
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1HeroPower_03
	};

	// Token: 0x040038DB RID: 14555
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_01,
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_02,
		BoH_Anduin_01.VO_Story_Hero_Varian_Male_Human_Story_Anduin_Mission1Idle_03
	};

	// Token: 0x040038DC RID: 14556
	private HashSet<string> m_playedLines = new HashSet<string>();
}
