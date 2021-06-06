using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000537 RID: 1335
public class BoH_Rexxar_02 : BoH_Rexxar_Dungeon
{
	// Token: 0x060048FB RID: 18683 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060048FC RID: 18684 RVA: 0x00186164 File Offset: 0x00184364
	public BoH_Rexxar_02()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_02.s_booleanOptions);
	}

	// Token: 0x060048FD RID: 18685 RVA: 0x00186208 File Offset: 0x00184408
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01,
			BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02,
			BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060048FE RID: 18686 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060048FF RID: 18687 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004900 RID: 18688 RVA: 0x001863BC File Offset: 0x001845BC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004901 RID: 18689 RVA: 0x001863CB File Offset: 0x001845CB
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2IdleLines;
	}

	// Token: 0x06004902 RID: 18690 RVA: 0x001863D3 File Offset: 0x001845D3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPowerLines;
	}

	// Token: 0x06004903 RID: 18691 RVA: 0x001863DB File Offset: 0x001845DB
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01;
	}

	// Token: 0x06004904 RID: 18692 RVA: 0x001863F4 File Offset: 0x001845F4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004905 RID: 18693 RVA: 0x0018647D File Offset: 0x0018467D
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004906 RID: 18694 RVA: 0x00186493 File Offset: 0x00184693
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

	// Token: 0x06004907 RID: 18695 RVA: 0x001864A9 File Offset: 0x001846A9
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

	// Token: 0x06004908 RID: 18696 RVA: 0x001864BF File Offset: 0x001846BF
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		switch (turn)
		{
		case 1:
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01, 2.5f);
			break;
		case 2:
		case 4:
			break;
		case 3:
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01, 2.5f);
			break;
		case 5:
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_02.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02, 2.5f);
			break;
		default:
			if (turn == 11)
			{
				yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01, 2.5f);
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004909 RID: 18697 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003CEC RID: 15596
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_02.InitBooleanOptions();

	// Token: 0x04003CED RID: 15597
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2EmoteResponse_01.prefab:716fc9188ba94b643a1a6b4ce9b4894a");

	// Token: 0x04003CEE RID: 15598
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeA_01.prefab:3bdc950307ffcc84c864080e4e5d4c7d");

	// Token: 0x04003CEF RID: 15599
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeB_01.prefab:54ba83dfe63168546a893fb4cee9902b");

	// Token: 0x04003CF0 RID: 15600
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_01.prefab:accafbc5ca01b2446a0b1e7e9f9a4b02");

	// Token: 0x04003CF1 RID: 15601
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeC_02.prefab:5c6862cf543d5c9418dc47f770cb5c3d");

	// Token: 0x04003CF2 RID: 15602
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2ExchangeD_01.prefab:a46c829fefa1ac84284b92951e8b78f0");

	// Token: 0x04003CF3 RID: 15603
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01.prefab:c53048cb7e2fd2e44b9b5f11af6c37b5");

	// Token: 0x04003CF4 RID: 15604
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02.prefab:109b460cf164c7e42a4534731c477fa6");

	// Token: 0x04003CF5 RID: 15605
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03.prefab:8b61743bfeb947344ae76e89b2658cd1");

	// Token: 0x04003CF6 RID: 15606
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01.prefab:dcb85e6893637f54c97a3459609e4994");

	// Token: 0x04003CF7 RID: 15607
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02.prefab:6f1f8f107bc26fa4fb1813d29bd796b9");

	// Token: 0x04003CF8 RID: 15608
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03.prefab:bd8d6fb605583c84b874e7006942e659");

	// Token: 0x04003CF9 RID: 15609
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Intro_01.prefab:19ac28e44145e7242ade5487dda28d35");

	// Token: 0x04003CFA RID: 15610
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Loss_01.prefab:62109ca64bf150c4cbcc3475fe9ee6ca");

	// Token: 0x04003CFB RID: 15611
	private static readonly AssetReference VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Victory_01.prefab:cc536b1ce6887ff42bc913cb74dbc219");

	// Token: 0x04003CFC RID: 15612
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeA_01.prefab:d8a241fa7b4b5ed47883cb20546fba5b");

	// Token: 0x04003CFD RID: 15613
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeB_01.prefab:f73a4d56ce8a3b348affb739780c7714");

	// Token: 0x04003CFE RID: 15614
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2ExchangeC_01.prefab:c1287116512150e46a142539b219ee4c");

	// Token: 0x04003CFF RID: 15615
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_01.prefab:7c920ff42bbefa24ca09101f5d66d154");

	// Token: 0x04003D00 RID: 15616
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Intro_02.prefab:1373a967381b1564d8f7a0ab0af2df6d");

	// Token: 0x04003D01 RID: 15617
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission2Victory_01.prefab:6076503bd77e06f4683ceff0c8a0003e");

	// Token: 0x04003D02 RID: 15618
	private List<string> m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPowerLines = new List<string>
	{
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_01,
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_02,
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2HeroPower_03
	};

	// Token: 0x04003D03 RID: 15619
	private List<string> m_VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2IdleLines = new List<string>
	{
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_01,
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_02,
		BoH_Rexxar_02.VO_Story_Hero_Blackhand_Male_Orc_Story_Rexxar_Mission2Idle_03
	};

	// Token: 0x04003D04 RID: 15620
	private HashSet<string> m_playedLines = new HashSet<string>();
}
