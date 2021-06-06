using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTA_Fight_04 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01.prefab:b99e8ebb8836363418e17dbd92fa7cd7");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01.prefab:5305888bb582e254bb858c5ce5d7f9e3");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01.prefab:47f10b6b49af9fc44b9e6f2bc80b62a7");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01.prefab:5c03c208ff92d4642964d1b7175022f6");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01.prefab:ac3c840da6e4ed04795865006e16cfbf");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01.prefab:03b2b3317fea65844b0f619703be372c");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01.prefab:3180d3c3e9efbd74bbcdb6e9353c9f6c");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01.prefab:950571ab6cf8f0345bb9b1334c8e0a0e");

	private static readonly AssetReference VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01 = new AssetReference("VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01.prefab:2d5b6008b6890ab41a2a6fa751ec0c74");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01.prefab:e5fba2dd14dc48747bab9016996e123e");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01.prefab:f4dce4b2bc0d49145a2a6e766fc14541");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01.prefab:44af927cdea31e44997761e20adfb605");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01.prefab:1a8bfab4b13796647a2b39391cf0d46f");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01.prefab:54f93764ae0beb74e9d345f347462ccd");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01.prefab:dadfc8cc24c537c41b71501dd2f323fc");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01.prefab:921d4735151291e48b162409197f6c54");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01.prefab:bf25dfecb6641c444b84ca636e1140b7");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01.prefab:862be978ec1a6cb4d92d4db93b29f0e7");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01.prefab:ce41f3411985ec642885cc8fe02375ec");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01.prefab:dbbc642c42fbdb443aa9914cc6c69cf1");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02.prefab:64f73c485ab96f740bf28c9bc9466872");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03.prefab:4ab532bf70fd35346bd2c627b9e7a4b0");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04.prefab:501d0d5c1d9dbba44be9081523fe72e0");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01.prefab:48918c4a1a0841f46be43b0bfcf47da7");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01.prefab:8f601c1b96c97ef418ec2a825d543fa9");

	private static readonly AssetReference VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01 = new AssetReference("VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01.prefab:92c33f8d9667cc245b6ecd5e1c06a21c");

	public bool m_boolean_DisplayVictory;

	private List<string> m_VO_BTA_BOSS_04h_IdleLines = new List<string> { VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01 };

	private List<string> m_VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_Lines = new List<string> { VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_04()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01, VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01, VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01, VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOne_01, VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01,
			VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Shalja_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_01,
			VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_03, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_04, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleA_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleB_01, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_IdleC_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_04h_IdleLines;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		if (playState == TAG_PLAYSTATE.WON)
		{
			m_boolean_DisplayVictory = true;
		}
		return base.ShouldPlayHeroBlowUpSpells(playState);
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossDeath_01;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_04_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_BossStart_01);
		GameState.Get().SetBusy(busy: false);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_HeroPowerTrigger_02);
			break;
		case 102:
			yield return PlayLineAlways(actor2, VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryA_01);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryB_01);
			break;
		case 500:
			PlaySound(VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_Attack_01);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield return base.RespondToFriendlyPlayedCardWithTiming(entity);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_731":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_InfectiousSporeling_01);
				break;
			case "BTA_05":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Hero_Sklibb_01);
				break;
			case "BTA_07":
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_04_Hero_Karnuk_01);
				break;
			}
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return base.RespondToPlayedCardWithTiming(entity);
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "BT_199":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_UnstableFelbolt_01);
				break;
			case "BT_304":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_EnhancedDreadlord_01);
				break;
			case "BT_305":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_04h_Male_Dreadlord_Mission_Fight_04_Boss_ImprisonedImp_01);
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1)
		{
			yield return PlayLineAlways(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_04_TurnOneBrassRing_01);
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		yield return new WaitForSeconds(5f);
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (m_boolean_DisplayVictory)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryA_Alt_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_04_VictoryB_Alt_01);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_07"), BTA_Dungeon.KarnukBrassRing, VO_BTA_07_Male_Orc_Mission_Fight_04_VictoryC_Alt_01);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
