using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000536 RID: 1334
public class BoH_Rexxar_01 : BoH_Rexxar_Dungeon
{
	// Token: 0x060048E8 RID: 18664 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060048E9 RID: 18665 RVA: 0x00185CA0 File Offset: 0x00183EA0
	public BoH_Rexxar_01()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_01.s_booleanOptions);
	}

	// Token: 0x060048EA RID: 18666 RVA: 0x00185D44 File Offset: 0x00183F44
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01,
			BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01,
			BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01,
			BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01,
			BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01,
			BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01,
			BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060048EB RID: 18667 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060048EC RID: 18668 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060048ED RID: 18669 RVA: 0x00185EE8 File Offset: 0x001840E8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060048EE RID: 18670 RVA: 0x00185EF7 File Offset: 0x001840F7
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1IdleLines;
	}

	// Token: 0x060048EF RID: 18671 RVA: 0x00185EFF File Offset: 0x001840FF
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPowerLines;
	}

	// Token: 0x060048F0 RID: 18672 RVA: 0x00185F07 File Offset: 0x00184107
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01;
	}

	// Token: 0x060048F1 RID: 18673 RVA: 0x00185F20 File Offset: 0x00184120
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

	// Token: 0x060048F2 RID: 18674 RVA: 0x00185FA9 File Offset: 0x001841A9
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor, BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060048F3 RID: 18675 RVA: 0x00185FBF File Offset: 0x001841BF
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

	// Token: 0x060048F4 RID: 18676 RVA: 0x00185FD5 File Offset: 0x001841D5
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

	// Token: 0x060048F5 RID: 18677 RVA: 0x00185FEB File Offset: 0x001841EB
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
				switch (turn)
				{
				case 7:
					yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01, 2.5f);
					break;
				case 9:
					yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01, 2.5f);
					yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01, 2.5f);
					break;
				case 11:
					yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01, 2.5f);
					break;
				}
			}
			else
			{
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_01.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060048F6 RID: 18678 RVA: 0x00176C37 File Offset: 0x00174E37
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}

	// Token: 0x04003CD4 RID: 15572
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_01.InitBooleanOptions();

	// Token: 0x04003CD5 RID: 15573
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1EmoteResponse_01.prefab:ef43dd1312c008e4a9b3dbb321c0e367");

	// Token: 0x04003CD6 RID: 15574
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeA_01.prefab:0dbcc192b3531174698b3277d91250d1");

	// Token: 0x04003CD7 RID: 15575
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01.prefab:562df924ed754654d8be78383ad2dbc1");

	// Token: 0x04003CD8 RID: 15576
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01.prefab:e291bd797129e9d48881956190f6d126");

	// Token: 0x04003CD9 RID: 15577
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01.prefab:0af8cbc15d3f84e4f8a7c9863d2bfb57");

	// Token: 0x04003CDA RID: 15578
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1ExchangeE_01.prefab:45c6f8b97397bed4cb012212b63a857e");

	// Token: 0x04003CDB RID: 15579
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01.prefab:4366e37436d191b42b3834b96c6f68e0");

	// Token: 0x04003CDC RID: 15580
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02.prefab:e05e57faf5cc0184c9a70ac274813de7");

	// Token: 0x04003CDD RID: 15581
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03.prefab:99d914bb1e042ff4592b8b3892a07eeb");

	// Token: 0x04003CDE RID: 15582
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01.prefab:bc47c19f2c53eeb4381323e456080891");

	// Token: 0x04003CDF RID: 15583
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02.prefab:eb34d381066165b45853f2cea20ae637");

	// Token: 0x04003CE0 RID: 15584
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03.prefab:c31e9af630472934c8cf3aa8e1168d7f");

	// Token: 0x04003CE1 RID: 15585
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Intro_01.prefab:d73e87daf7ab344418bafa48d1e7346b");

	// Token: 0x04003CE2 RID: 15586
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Loss_01.prefab:3f056de75e46f9143b0f2adbcfdf4ec9");

	// Token: 0x04003CE3 RID: 15587
	private static readonly AssetReference VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Victory_01.prefab:0eb371f8f251d7c46b939f8fccb20f1e");

	// Token: 0x04003CE4 RID: 15588
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeB_01.prefab:fbf526cf04257c34082a45e5b37b86a5");

	// Token: 0x04003CE5 RID: 15589
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeC_01.prefab:21ff8edafd740c74eb259fddf9f6c232");

	// Token: 0x04003CE6 RID: 15590
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1ExchangeD_01.prefab:85943387edc668a4197dd6d9ccccaac8");

	// Token: 0x04003CE7 RID: 15591
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Intro_01.prefab:3f11648187f385e45af7a57734b3bdee");

	// Token: 0x04003CE8 RID: 15592
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission1Victory_01.prefab:fff76b2ef452488488babc1e29116456");

	// Token: 0x04003CE9 RID: 15593
	private List<string> m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPowerLines = new List<string>
	{
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_01,
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_02,
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1HeroPower_03
	};

	// Token: 0x04003CEA RID: 15594
	private List<string> m_VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1IdleLines = new List<string>
	{
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_01,
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_02,
		BoH_Rexxar_01.VO_Story_Hero_Leoroxx_Male_OrcOgre_Story_Rexxar_Mission1Idle_03
	};

	// Token: 0x04003CEB RID: 15595
	private HashSet<string> m_playedLines = new HashSet<string>();
}
