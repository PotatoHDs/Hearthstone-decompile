using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200053B RID: 1339
public class BoH_Rexxar_06 : BoH_Rexxar_Dungeon
{
	// Token: 0x06004949 RID: 18761 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600494A RID: 18762 RVA: 0x00187620 File Offset: 0x00185820
	public BoH_Rexxar_06()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_06.s_booleanOptions);
	}

	// Token: 0x0600494B RID: 18763 RVA: 0x001876C4 File Offset: 0x001858C4
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01,
			BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01,
			BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01,
			BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01,
			BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01,
			BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01,
			BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01,
			BoH_Rexxar_06.VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01,
			BoH_Rexxar_06.VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600494C RID: 18764 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600494D RID: 18765 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x0600494E RID: 18766 RVA: 0x00187858 File Offset: 0x00185A58
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600494F RID: 18767 RVA: 0x00187867 File Offset: 0x00185A67
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6IdleLines;
	}

	// Token: 0x06004950 RID: 18768 RVA: 0x0018786F File Offset: 0x00185A6F
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPowerLines;
	}

	// Token: 0x06004951 RID: 18769 RVA: 0x00187877 File Offset: 0x00185A77
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01;
	}

	// Token: 0x06004952 RID: 18770 RVA: 0x00187890 File Offset: 0x00185A90
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

	// Token: 0x06004953 RID: 18771 RVA: 0x00187919 File Offset: 0x00185B19
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 501)
		{
			if (missionEvent != 504)
			{
				yield return base.HandleMissionEventWithTiming(missionEvent);
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(actor2, BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01, 2.5f);
			Actor enemyActorByCardId = base.GetEnemyActorByCardId("Story_02_Baine");
			if (enemyActorByCardId != null)
			{
				yield return base.PlayLineAlways(enemyActorByCardId, BoH_Rexxar_06.VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01, 2.5f);
			}
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004954 RID: 18772 RVA: 0x0018792F File Offset: 0x00185B2F
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

	// Token: 0x06004955 RID: 18773 RVA: 0x00187945 File Offset: 0x00185B45
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

	// Token: 0x06004956 RID: 18774 RVA: 0x0018795B File Offset: 0x00185B5B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn != 1)
		{
			if (turn != 3)
			{
				if (turn == 13)
				{
					yield return base.PlayLineAlways(actor, BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01, 2.5f);
					yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01, 2.5f);
			}
		}
		else
		{
			Actor enemyActorByCardId = base.GetEnemyActorByCardId("Story_02_Baine");
			if (enemyActorByCardId != null)
			{
				yield return base.PlayLineAlways(enemyActorByCardId, BoH_Rexxar_06.VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01, 2.5f);
			}
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_06.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004957 RID: 18775 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x04003D57 RID: 15703
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_06.InitBooleanOptions();

	// Token: 0x04003D58 RID: 15704
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Death_01.prefab:7399820ffe8358c4d9e62a99c7f4537a");

	// Token: 0x04003D59 RID: 15705
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6EmoteResponse_01.prefab:e236430c43795544aad7a3cfee0dab7e");

	// Token: 0x04003D5A RID: 15706
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeB_01.prefab:3b646d2702651c44cb1866c65a8922aa");

	// Token: 0x04003D5B RID: 15707
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6ExchangeC_01.prefab:1b569d23197f05742b82066210283a7e");

	// Token: 0x04003D5C RID: 15708
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01.prefab:036cc7e9be96f0a44818344b218288fd");

	// Token: 0x04003D5D RID: 15709
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02.prefab:c0e26a8cbacc4f9489868d741c6baf14");

	// Token: 0x04003D5E RID: 15710
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03.prefab:ed3cab9b40a442440a20ed4f0ed53afb");

	// Token: 0x04003D5F RID: 15711
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01.prefab:3fdcbfa6a3f475e46a88e8cacc1222ea");

	// Token: 0x04003D60 RID: 15712
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02.prefab:f683936bee3988645bea7c0a56aec9f6");

	// Token: 0x04003D61 RID: 15713
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03.prefab:e0ed4fbff1fd0e149b678f6a03b96ac3");

	// Token: 0x04003D62 RID: 15714
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Intro_01.prefab:61123b7c46905db4cb4e8476c1388d22");

	// Token: 0x04003D63 RID: 15715
	private static readonly AssetReference VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01 = new AssetReference("VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Loss_01.prefab:41b4841110cfa3d4eb0aabedc873fc70");

	// Token: 0x04003D64 RID: 15716
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeA_01.prefab:d1e1dc742d7fec144bbb3d185f8004c8");

	// Token: 0x04003D65 RID: 15717
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeB_01.prefab:69aca0cd9ccea0548bdadb969652f4c2");

	// Token: 0x04003D66 RID: 15718
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6ExchangeC_01.prefab:d5f9a3d8ee2f5b946933655f5059c6cc");

	// Token: 0x04003D67 RID: 15719
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Intro_01.prefab:8062f8f2b27a4d749a942ae31826cae7");

	// Token: 0x04003D68 RID: 15720
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission6Victory_01.prefab:3ae4b33ffab3b864caa9935c5cb61f1b");

	// Token: 0x04003D69 RID: 15721
	private static readonly AssetReference VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01 = new AssetReference("VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6ExchangeA_01.prefab:70c5f8b2a0d6e924a9189e97232a2344");

	// Token: 0x04003D6A RID: 15722
	private static readonly AssetReference VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01 = new AssetReference("VO_Story_Minion_Baine_Male_Tauren_Story_Rexxar_Mission6Victory_01.prefab:1f7a8dd5eae9dfd4187243bf1f26de84");

	// Token: 0x04003D6B RID: 15723
	private List<string> m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPowerLines = new List<string>
	{
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_01,
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_02,
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6HeroPower_03
	};

	// Token: 0x04003D6C RID: 15724
	private List<string> m_VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6IdleLines = new List<string>
	{
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_01,
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_02,
		BoH_Rexxar_06.VO_Story_02_Centaur_Male_Centaur_Story_Rexxar_Mission6Idle_03
	};

	// Token: 0x04003D6D RID: 15725
	private HashSet<string> m_playedLines = new HashSet<string>();
}
