using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EE RID: 1262
public class BTA_Fight_04 : BTA_Dungeon
{
	// Token: 0x060043B3 RID: 17331 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x060043B4 RID: 17332 RVA: 0x0016EC0C File Offset: 0x0016CE0C
	public BTA_Fight_04()
	{
		this.m_gameOptions.AddBooleanOptions(BTA_Fight_04.s_booleanOptions);
	}

	// Token: 0x060043B5 RID: 17333 RVA: 0x0016ECB0 File Offset: 0x0016CEB0
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01,
			BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01,
			BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01,
			BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01,
			BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01
		};
		base.SetBossVOLines(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060043B6 RID: 17334 RVA: 0x0016EEB4 File Offset: 0x0016D0B4
	public override List<string> GetIdleLines()
	{
		return this.m_VO_BTA_BOSS_04h_IdleLines;
	}

	// Token: 0x060043B7 RID: 17335 RVA: 0x0016EEBC File Offset: 0x0016D0BC
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		if (playState == TAG_PLAYSTATE.WON)
		{
			this.m_boolean_DisplayVictory = true;
		}
		return base.ShouldPlayHeroBlowUpSpells(playState);
	}

	// Token: 0x060043B8 RID: 17336 RVA: 0x0016EED0 File Offset: 0x0016D0D0
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_deathLine = BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01;
	}

	// Token: 0x060043B9 RID: 17337 RVA: 0x000052EC File Offset: 0x000034EC
	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	// Token: 0x060043BA RID: 17338 RVA: 0x0016EEE8 File Offset: 0x0016D0E8
	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01, 2.5f);
		yield return base.PlayLineAlways(enemyActor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01, 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x060043BB RID: 17339 RVA: 0x0016EEF8 File Offset: 0x0016D0F8
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x060043BC RID: 17340 RVA: 0x0016EF80 File Offset: 0x0016D180
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent != 101)
		{
			if (missionEvent != 102)
			{
				if (missionEvent != 500)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					base.PlaySound(BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01, 1f, true, false);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor2, BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01, 2.5f);
				yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043BD RID: 17341 RVA: 0x0016EF96 File Offset: 0x0016D196
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
		if (!(cardId == "BT_731"))
		{
			if (!(cardId == "BTA_05"))
			{
				if (cardId == "BTA_07")
				{
					yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043BE RID: 17342 RVA: 0x0016EFAC File Offset: 0x0016D1AC
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
		if (!(cardId == "BT_199"))
		{
			if (!(cardId == "BT_304"))
			{
				if (cardId == "BT_305")
				{
					yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01, 2.5f);
				}
			}
			else
			{
				yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01, 2.5f);
			}
		}
		else
		{
			yield return base.PlayLineAlways(actor, BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043BF RID: 17343 RVA: 0x0016EFC2 File Offset: 0x0016D1C2
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (turn == 1)
		{
			yield return base.PlayLineAlways(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01, 2.5f);
		}
		yield break;
	}

	// Token: 0x060043C0 RID: 17344 RVA: 0x0016EFD8 File Offset: 0x0016D1D8
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (this.m_boolean_DisplayVictory)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01, 2.5f);
			yield return base.PlayLineAlways(friendlyActor, BTA_Fight_04.VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01, 2.5f);
			yield return base.PlayLineAlwaysWithBrassRing(base.GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, BTA_Fight_04.VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01, 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x040035B5 RID: 13749
	private static Map<GameEntityOption, bool> s_booleanOptions = BTA_Fight_04.InitBooleanOptions();

	// Token: 0x040035B6 RID: 13750
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01.prefab:b99e8ebb8836363418e17dbd92fa7cd7");

	// Token: 0x040035B7 RID: 13751
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01.prefab:5305888bb582e254bb858c5ce5d7f9e3");

	// Token: 0x040035B8 RID: 13752
	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01.prefab:47f10b6b49af9fc44b9e6f2bc80b62a7");

	// Token: 0x040035B9 RID: 13753
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01.prefab:5c03c208ff92d4642964d1b7175022f6");

	// Token: 0x040035BA RID: 13754
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01.prefab:ac3c840da6e4ed04795865006e16cfbf");

	// Token: 0x040035BB RID: 13755
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01.prefab:03b2b3317fea65844b0f619703be372c");

	// Token: 0x040035BC RID: 13756
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01.prefab:3180d3c3e9efbd74bbcdb6e9353c9f6c");

	// Token: 0x040035BD RID: 13757
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01.prefab:950571ab6cf8f0345bb9b1334c8e0a0e");

	// Token: 0x040035BE RID: 13758
	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01.prefab:2d5b6008b6890ab41a2a6fa751ec0c74");

	// Token: 0x040035BF RID: 13759
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01.prefab:e5fba2dd14dc48747bab9016996e123e");

	// Token: 0x040035C0 RID: 13760
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01.prefab:f4dce4b2bc0d49145a2a6e766fc14541");

	// Token: 0x040035C1 RID: 13761
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01.prefab:44af927cdea31e44997761e20adfb605");

	// Token: 0x040035C2 RID: 13762
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01.prefab:1a8bfab4b13796647a2b39391cf0d46f");

	// Token: 0x040035C3 RID: 13763
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01.prefab:54f93764ae0beb74e9d345f347462ccd");

	// Token: 0x040035C4 RID: 13764
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01.prefab:dadfc8cc24c537c41b71501dd2f323fc");

	// Token: 0x040035C5 RID: 13765
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01.prefab:921d4735151291e48b162409197f6c54");

	// Token: 0x040035C6 RID: 13766
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01.prefab:bf25dfecb6641c444b84ca636e1140b7");

	// Token: 0x040035C7 RID: 13767
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01.prefab:862be978ec1a6cb4d92d4db93b29f0e7");

	// Token: 0x040035C8 RID: 13768
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01.prefab:ce41f3411985ec642885cc8fe02375ec");

	// Token: 0x040035C9 RID: 13769
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01.prefab:dbbc642c42fbdb443aa9914cc6c69cf1");

	// Token: 0x040035CA RID: 13770
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02.prefab:64f73c485ab96f740bf28c9bc9466872");

	// Token: 0x040035CB RID: 13771
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03.prefab:4ab532bf70fd35346bd2c627b9e7a4b0");

	// Token: 0x040035CC RID: 13772
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04.prefab:501d0d5c1d9dbba44be9081523fe72e0");

	// Token: 0x040035CD RID: 13773
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01.prefab:48918c4a1a0841f46be43b0bfcf47da7");

	// Token: 0x040035CE RID: 13774
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01.prefab:8f601c1b96c97ef418ec2a825d543fa9");

	// Token: 0x040035CF RID: 13775
	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01.prefab:92c33f8d9667cc245b6ecd5e1c06a21c");

	// Token: 0x040035D0 RID: 13776
	public bool m_boolean_DisplayVictory;

	// Token: 0x040035D1 RID: 13777
	private List<string> m_VO_BTA_BOSS_04h_IdleLines = new List<string>
	{
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01,
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01,
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01
	};

	// Token: 0x040035D2 RID: 13778
	private List<string> m_VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_Lines = new List<string>
	{
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01,
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03,
		BTA_Fight_04.VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04
	};

	// Token: 0x040035D3 RID: 13779
	private HashSet<string> m_playedLines = new HashSet<string>();
}
