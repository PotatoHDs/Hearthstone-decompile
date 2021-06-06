using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000532 RID: 1330
public class BoH_Malfurion_07 : BoH_Malfurion_Dungeon
{
	// Token: 0x06004883 RID: 18563 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004884 RID: 18564 RVA: 0x00184794 File Offset: 0x00182994
	public BoH_Malfurion_07()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Malfurion_07.s_booleanOptions);
	}

	// Token: 0x06004885 RID: 18565 RVA: 0x00184838 File Offset: 0x00182A38
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01,
			BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01,
			BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01,
			BoH_Malfurion_07.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01,
			BoH_Malfurion_07.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04,
			BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01,
			BoH_Malfurion_07.VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01,
			BoH_Malfurion_07.VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004886 RID: 18566 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004887 RID: 18567 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004888 RID: 18568 RVA: 0x001849AC File Offset: 0x00182BAC
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.MissionPlayVO(BoH_Malfurion_07.CenariusBrassRing, BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01);
		yield return base.MissionPlayVO(friendlyActor, BoH_Malfurion_07.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004889 RID: 18569 RVA: 0x001849BB File Offset: 0x00182BBB
	public override List<string> GetBossIdleLines()
	{
		return this.m_BossIdleLines;
	}

	// Token: 0x0600488A RID: 18570 RVA: 0x001849C3 File Offset: 0x00182BC3
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_BossUsesHeroPowerLines;
	}

	// Token: 0x0600488B RID: 18571 RVA: 0x001849CB File Offset: 0x00182BCB
	public override void OnCreateGame()
	{
		this.m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01;
	}

	// Token: 0x0600488C RID: 18572 RVA: 0x001849F0 File Offset: 0x00182BF0
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600488D RID: 18573 RVA: 0x00184A74 File Offset: 0x00182C74
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 504)
			{
				if (missionEvent == 507)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayLineAlways(actor, BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01, 2.5f);
					GameState.Get().SetBusy(false);
				}
				else
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("Story_08_Hamuul"), BoH_Malfurion_07.VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600488E RID: 18574 RVA: 0x00184A8A File Offset: 0x00182C8A
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

	// Token: 0x0600488F RID: 18575 RVA: 0x00184AA0 File Offset: 0x00182CA0
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

	// Token: 0x06004890 RID: 18576 RVA: 0x00184AB6 File Offset: 0x00182CB6
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 3)
		{
			if (turn != 1)
			{
				if (turn == 3)
				{
					yield return base.PlayLineAlways(BoH_Malfurion_07.CenariusBrassRing, BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01, 2.5f);
				}
			}
			else
			{
				yield return base.MissionPlayVO(base.GetFriendlyActorByCardId("Story_08_Hamuul"), BoH_Malfurion_07.VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03);
			}
		}
		else if (turn != 7)
		{
			if (turn == 11)
			{
				yield return base.PlayLineAlways(BoH_Malfurion_07.CenariusBrassRing, BoH_Malfurion_07.VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BoH_Malfurion_07.VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003C5A RID: 15450
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Malfurion_07.InitBooleanOptions();

	// Token: 0x04003C5B RID: 15451
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeA_01.prefab:90d506f6e68ede3488176813d4550746");

	// Token: 0x04003C5C RID: 15452
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7ExchangeD_01.prefab:a295e1a4542d1eb469e341690f98346a");

	// Token: 0x04003C5D RID: 15453
	private static readonly AssetReference VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Cenarius_Male_Demigod_Story_Malfurion_Mission7Intro_01.prefab:db6210202fa3cb7499242f5041938586");

	// Token: 0x04003C5E RID: 15454
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7ExchangeB_01.prefab:9df52d023443eaf4f837e011f96fa3e3");

	// Token: 0x04003C5F RID: 15455
	private static readonly AssetReference VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02 = new AssetReference("VO_Story_Hero_Malfurion_Male_NightElf_Story_Malfurion_Mission7Intro_02.prefab:7e1b0afab07582c46ae8457112ab8347");

	// Token: 0x04003C60 RID: 15456
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7_Victory_01.prefab:8f66f1279f881604f92002ce0223cbff");

	// Token: 0x04003C61 RID: 15457
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7EmoteResponse_01.prefab:e1b2f8901e118df4fa80583229331b18");

	// Token: 0x04003C62 RID: 15458
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01.prefab:e0bbad6ff4ddc914ba3e2b5db44a6742");

	// Token: 0x04003C63 RID: 15459
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02.prefab:5d0ca78992ff24e4fb0c36c5885d49ef");

	// Token: 0x04003C64 RID: 15460
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03.prefab:2627eb43a4e966d4896ed8924a72eca0");

	// Token: 0x04003C65 RID: 15461
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01.prefab:e3d10881bfe5c5c4982fcab1fc36165d");

	// Token: 0x04003C66 RID: 15462
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02.prefab:0cb81c58c81c0e54ca19b3f966425f29");

	// Token: 0x04003C67 RID: 15463
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03.prefab:0f8f072f141707344a10ebd0a9d8cdcc");

	// Token: 0x04003C68 RID: 15464
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Intro_04.prefab:eb94840e17b872b4ab6d7356ab162368");

	// Token: 0x04003C69 RID: 15465
	private static readonly AssetReference VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Loss_01.prefab:886b18ed9f44ef94e9f34e5d1962892b");

	// Token: 0x04003C6A RID: 15466
	private static readonly AssetReference VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7ExchangeC_01.prefab:07545a5fea43da744b2d570ebe9915c2");

	// Token: 0x04003C6B RID: 15467
	private static readonly AssetReference VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03 = new AssetReference("VO_Story_Minion_Hamuul_Male_Tauren_Story_Malfurion_Mission7Intro_03.prefab:d48df2c560d6cb54288c15481e206f93");

	// Token: 0x04003C6C RID: 15468
	public static readonly AssetReference CenariusBrassRing = new AssetReference("Cenarius_BrassRing_Quote.prefab:9157110d07b5b004fa0c0f651c71ef81");

	// Token: 0x04003C6D RID: 15469
	private List<string> m_BossUsesHeroPowerLines = new List<string>
	{
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_01,
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_02,
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7HeroPower_03
	};

	// Token: 0x04003C6E RID: 15470
	private new List<string> m_BossIdleLines = new List<string>
	{
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_01,
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_02,
		BoH_Malfurion_07.VO_Story_Hero_Ragnaros_Male_Elemental_Story_Malfurion_Mission7Idle_03
	};

	// Token: 0x04003C6F RID: 15471
	private HashSet<string> m_playedLines = new HashSet<string>();
}
