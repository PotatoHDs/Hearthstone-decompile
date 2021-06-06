using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020004EB RID: 1259
public class BTA_Fight_01 : BTA_Dungeon
{
	// Token: 0x06004383 RID: 17283 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004384 RID: 17284 RVA: 0x0016DEE4 File Offset: 0x0016C0E4
	public BTA_Fight_01()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_01.s_booleanOptions);
	}

	// Token: 0x06004385 RID: 17285 RVA: 0x0016DF98 File Offset: 0x0016C198
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01,
			BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01,
			BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01,
			BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x06004386 RID: 17286 RVA: 0x0016E16C File Offset: 0x0016C36C
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_01h_IdleLines;
	}

	// Token: 0x06004387 RID: 17287 RVA: 0x0016E174 File Offset: 0x0016C374
	public override List<string> GetBossHeroPowerRandomLines()
	{
		return this.m_VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_Lines;
	}

	// Token: 0x06004388 RID: 17288 RVA: 0x0016E17C File Offset: 0x0016C37C
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01;
	}

	// Token: 0x06004389 RID: 17289 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x0600438A RID: 17290 RVA: 0x0016E194 File Offset: 0x0016C394
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x0600438B RID: 17291 RVA: 0x0016E1A4 File Offset: 0x0016C3A4
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x0600438C RID: 17292 RVA: 0x0016E22C File Offset: 0x0016C42C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent <= 102)
		{
			if (missionEvent == 101)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01, 2.5f);
				goto IL_1F4;
			}
			if (missionEvent == 102)
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01, 2.5f);
				goto IL_1F4;
			}
		}
		else
		{
			if (missionEvent == 500)
			{
				yield return base.PlayLineOnlyOnce(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01, 2.5f);
				goto IL_1F4;
			}
			if (missionEvent == 501)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01, 2.5f);
				yield return base.PlayLineAlways(friendlyActor, BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01, 2.5f);
				GameState.Get().SetBusy(false);
				goto IL_1F4;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_1F4:
		yield break;
	}

	// Token: 0x0600438D RID: 17293 RVA: 0x0016E242 File Offset: 0x0016C442
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_077"))
		{
			if (cardId == "EX1_129")
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600438E RID: 17294 RVA: 0x0016E258 File Offset: 0x0016C458
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "BT_300"))
		{
			if (!(cardId == "BTA_13"))
			{
				if (cardId == "BTA_17")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600438F RID: 17295 RVA: 0x0016E26E File Offset: 0x0016C46E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_01.VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01, 2.5f);
			yield return base.PlayLineAlways(enemyActor, BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x04003570 RID: 13680
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_01.InitBooleanOptions();

	// Token: 0x04003571 RID: 13681
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01.prefab:0c068f75d8fba6a4682afb60bbb1278e");

	// Token: 0x04003572 RID: 13682
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01.prefab:96b3672b809eaca4faabd5eba6e737a4");

	// Token: 0x04003573 RID: 13683
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01.prefab:6adab1d593f05a149a8e52e44bedc6bc");

	// Token: 0x04003574 RID: 13684
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01.prefab:6f7023e3a74acc54dacb48f33a7b4365");

	// Token: 0x04003575 RID: 13685
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01.prefab:eaf25d9b217062040b78a5dc32794636");

	// Token: 0x04003576 RID: 13686
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01.prefab:aa2df58926af1934681e539a3b66b6d7");

	// Token: 0x04003577 RID: 13687
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01.prefab:40301ef9bd9cbb249b8c6a6e210c9030");

	// Token: 0x04003578 RID: 13688
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01.prefab:cdff318e0aab37046845ce8aec7e9f4d");

	// Token: 0x04003579 RID: 13689
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01.prefab:3cbd9987375639d4caf650949710979e");

	// Token: 0x0400357A RID: 13690
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01.prefab:6912c42f7f1fb2b4ab3cc54b53c5966f");

	// Token: 0x0400357B RID: 13691
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01.prefab:1a87259a39d25c848ad95f13d1fa33a1");

	// Token: 0x0400357C RID: 13692
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01.prefab:22a5025fcc5812e44b81ee1c2afeb4a7");

	// Token: 0x0400357D RID: 13693
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01.prefab:d90ba43cbb1f9bf40824d9d0f24ffb83");

	// Token: 0x0400357E RID: 13694
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01.prefab:d0107e9dca4f1884b923d4ccc721f24f");

	// Token: 0x0400357F RID: 13695
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01.prefab:bb17374a2e13618439b80ed6e51567ec");

	// Token: 0x04003580 RID: 13696
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02.prefab:c0887a162128e4c489e1bdda41b018b3");

	// Token: 0x04003581 RID: 13697
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03.prefab:8c79596c492fc6d4f840903ed7bfcc55");

	// Token: 0x04003582 RID: 13698
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04.prefab:6a38f3af734b3074d8b63836e21355b9");

	// Token: 0x04003583 RID: 13699
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01.prefab:9bbfbee4397558f47881ef7329809fba");

	// Token: 0x04003584 RID: 13700
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01.prefab:5e13da692005931448940af8c035b64e");

	// Token: 0x04003585 RID: 13701
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01.prefab:97afd080de39c9b43a99314609ac45e2");

	// Token: 0x04003586 RID: 13702
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01.prefab:ae60b94ce602c4d429a0f2cd9537405f");

	// Token: 0x04003587 RID: 13703
	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01.prefab:86d255295e7492542a80ad593954e303");

	// Token: 0x04003588 RID: 13704
	private List<string> m_VO_BTA_BOSS_01h_IdleLines = new List<string>
	{
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01,
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01,
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01
	};

	// Token: 0x04003589 RID: 13705
	private List<string> m_VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_Lines = new List<string>
	{
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01,
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02,
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03,
		BTA_Fight_01.VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04
	};

	// Token: 0x0400358A RID: 13706
	private HashSet<string> m_playedLines = new HashSet<string>();
}
