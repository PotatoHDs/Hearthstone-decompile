using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x0200053C RID: 1340
public class BoH_Rexxar_07 : BoH_Rexxar_Dungeon
{
	// Token: 0x0600495C RID: 18780 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x0600495D RID: 18781 RVA: 0x00187AA8 File Offset: 0x00185CA8
	public BoH_Rexxar_07()
	{
		this.m_gameOptions.AddBooleanOptions(BoH_Rexxar_07.s_booleanOptions);
	}

	// Token: 0x0600495E RID: 18782 RVA: 0x00187B4C File Offset: 0x00185D4C
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01,
			BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01,
			BoH_Rexxar_07.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01,
			BoH_Rexxar_07.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02,
			BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01,
			BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01,
			BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01,
			BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01,
			BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01,
			BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01,
			BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01,
			BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01,
			BoH_Rexxar_07.VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600495F RID: 18783 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x06004960 RID: 18784 RVA: 0x00187D20 File Offset: 0x00185F20
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(BoH_Rexxar_07.ThrallBrassRing, BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01, 2.5f);
		yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06004961 RID: 18785 RVA: 0x00187D2F File Offset: 0x00185F2F
	public override List<string> GetIdleLines()
	{
		return this.m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7IdleLines;
	}

	// Token: 0x06004962 RID: 18786 RVA: 0x00187D37 File Offset: 0x00185F37
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPowerLines;
	}

	// Token: 0x06004963 RID: 18787 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x06004964 RID: 18788 RVA: 0x00187D3F File Offset: 0x00185F3F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_standardEmoteResponseLine = BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01;
	}

	// Token: 0x06004965 RID: 18789 RVA: 0x00187D58 File Offset: 0x00185F58
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

	// Token: 0x06004966 RID: 18790 RVA: 0x00187DE1 File Offset: 0x00185FE1
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
				yield return base.PlayLineAlways(actor, BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01, 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlways(BoH_Rexxar_07.ThrallBrassRing, BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004967 RID: 18791 RVA: 0x00187DF7 File Offset: 0x00185FF7
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (cardId == "Story_02_Cairne")
		{
			Actor friendlyActorByCardId = base.GetFriendlyActorByCardId("Story_02_Cairne");
			if (friendlyActorByCardId != null)
			{
				yield return base.PlayLineAlways(friendlyActorByCardId, BoH_Rexxar_07.VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01, 2.5f);
			}
			yield return base.PlayLineAlways(friendlyActor, BoH_Rexxar_07.VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004968 RID: 18792 RVA: 0x00187E0D File Offset: 0x0018600D
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

	// Token: 0x06004969 RID: 18793 RVA: 0x00187E23 File Offset: 0x00186023
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn <= 7)
		{
			if (turn != 5)
			{
				if (turn == 7)
				{
					yield return base.PlayLineAlways(BoH_Rexxar_07.ThrallBrassRing, BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(BoH_Rexxar_07.JainaBrassRing, BoH_Rexxar_07.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_07.ThrallBrassRing, BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01, 2.5f);
				yield return base.PlayLineAlways(BoH_Rexxar_07.JainaBrassRing, BoH_Rexxar_07.VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02, 2.5f);
			}
		}
		else if (turn != 13)
		{
			if (turn != 15)
			{
				if (turn == 19)
				{
					yield return base.PlayLineAlways(actor, BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(BoH_Rexxar_07.ThrallBrassRing, BoH_Rexxar_07.VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600496A RID: 18794 RVA: 0x00118E06 File Offset: 0x00117006
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_GILFinalBoss);
	}

	// Token: 0x04003D6E RID: 15726
	private static Map<GameEntityOption, bool> s_booleanOptions = BoH_Rexxar_07.InitBooleanOptions();

	// Token: 0x04003D6F RID: 15727
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Death_01.prefab:eac17d6de0a8b7049ad6e627a7ba65d4");

	// Token: 0x04003D70 RID: 15728
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7EmoteResponse_01.prefab:23beabab899cec5478bfc35ef1e4d536");

	// Token: 0x04003D71 RID: 15729
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeE_01.prefab:fb1686f802950ab4db51108db96418a1");

	// Token: 0x04003D72 RID: 15730
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7ExchangeF_01.prefab:d9024ecd826512d48a1297887e39b63d");

	// Token: 0x04003D73 RID: 15731
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01.prefab:9cdf0adf803e3a14d8c925af347a88d6");

	// Token: 0x04003D74 RID: 15732
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02.prefab:b69f1824146d31e4d8cb427bf3cd3f80");

	// Token: 0x04003D75 RID: 15733
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03.prefab:98403e93fc6880a49aaf78f196ee5bac");

	// Token: 0x04003D76 RID: 15734
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01.prefab:8de282a89d2a537439fd8dd1a2870275");

	// Token: 0x04003D77 RID: 15735
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02.prefab:91c67075e501f544ebb49244b298e438");

	// Token: 0x04003D78 RID: 15736
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03.prefab:e243226a72ac3e844a48bc9fc4f6ead7");

	// Token: 0x04003D79 RID: 15737
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Intro_01.prefab:e957ebe67fd8fc248ae8c47ef9fcd2ec");

	// Token: 0x04003D7A RID: 15738
	private static readonly AssetReference VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Loss_01.prefab:423295209b098444787d38d587233e1b");

	// Token: 0x04003D7B RID: 15739
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_01.prefab:e0265f3093767e9469ee3f1d041fd0fc");

	// Token: 0x04003D7C RID: 15740
	private static readonly AssetReference VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Jaina_Female_Human_Story_Rexxar_Mission7ExchangeA_02.prefab:22349d60ba8a971489aecf7533226ea2");

	// Token: 0x04003D7D RID: 15741
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7ExchangeC_01.prefab:18dd4908b2156fb47b585d1fb3350c51");

	// Token: 0x04003D7E RID: 15742
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Intro_01.prefab:1085697f9157d5d4daa1188985ae911e");

	// Token: 0x04003D7F RID: 15743
	private static readonly AssetReference VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Rexxar_Male_OrcOgre_Story_Rexxar_Mission7Victory_01.prefab:b5409ed173944b3469080b0325db629c");

	// Token: 0x04003D80 RID: 15744
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeA_01.prefab:6e90a9b4030b8f2479e26c02c6df59be");

	// Token: 0x04003D81 RID: 15745
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeB_01.prefab:891132071bcfb5442844a3657ac7ee80");

	// Token: 0x04003D82 RID: 15746
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7ExchangeD_01.prefab:72641b8ca4cf1ff41861c7a84d8cf1d8");

	// Token: 0x04003D83 RID: 15747
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Intro_01.prefab:c893332be7adfc74cbc70b0534dce55f");

	// Token: 0x04003D84 RID: 15748
	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Rexxar_Mission7Victory_01.prefab:bfc338f4beaae1d4da69bc43dd24b6cd");

	// Token: 0x04003D85 RID: 15749
	private static readonly AssetReference VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01 = new AssetReference("VO_Story_Minion_Cairne_Male_Tauren_Story_Rexxar_Mission7ExchangeC_01.prefab:80dcfbb24d09d2343856009c02c33ad0");

	// Token: 0x04003D86 RID: 15750
	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	// Token: 0x04003D87 RID: 15751
	public static readonly AssetReference JainaBrassRing = new AssetReference("JainaMid_BrassRing_Quote.prefab:7eba171d881f6764e81abddbb125bb19");

	// Token: 0x04003D88 RID: 15752
	private List<string> m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPowerLines = new List<string>
	{
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_01,
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_02,
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7HeroPower_03
	};

	// Token: 0x04003D89 RID: 15753
	private List<string> m_VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7IdleLines = new List<string>
	{
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_01,
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_02,
		BoH_Rexxar_07.VO_Story_Hero_Daelin_Male_Human_Story_Rexxar_Mission7Idle_03
	};

	// Token: 0x04003D8A RID: 15754
	private HashSet<string> m_playedLines = new HashSet<string>();
}
