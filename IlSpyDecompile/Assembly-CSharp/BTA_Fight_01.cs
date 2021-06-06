using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_01 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01.prefab:0c068f75d8fba6a4682afb60bbb1278e");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01.prefab:96b3672b809eaca4faabd5eba6e737a4");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01.prefab:6adab1d593f05a149a8e52e44bedc6bc");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01.prefab:6f7023e3a74acc54dacb48f33a7b4365");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01.prefab:eaf25d9b217062040b78a5dc32794636");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01.prefab:aa2df58926af1934681e539a3b66b6d7");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01.prefab:40301ef9bd9cbb249b8c6a6e210c9030");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01.prefab:cdff318e0aab37046845ce8aec7e9f4d");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01.prefab:3cbd9987375639d4caf650949710979e");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01.prefab:6912c42f7f1fb2b4ab3cc54b53c5966f");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01.prefab:1a87259a39d25c848ad95f13d1fa33a1");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01.prefab:22a5025fcc5812e44b81ee1c2afeb4a7");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01.prefab:d90ba43cbb1f9bf40824d9d0f24ffb83");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01.prefab:d0107e9dca4f1884b923d4ccc721f24f");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01.prefab:bb17374a2e13618439b80ed6e51567ec");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02.prefab:c0887a162128e4c489e1bdda41b018b3");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03.prefab:8c79596c492fc6d4f840903ed7bfcc55");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04.prefab:6a38f3af734b3074d8b63836e21355b9");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01.prefab:9bbfbee4397558f47881ef7329809fba");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01.prefab:5e13da692005931448940af8c035b64e");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01.prefab:97afd080de39c9b43a99314609ac45e2");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01.prefab:ae60b94ce602c4d429a0f2cd9537405f");

	private static readonly AssetReference VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01 = new AssetReference("VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01.prefab:86d255295e7492542a80ad593954e303");

	private List<string> m_VO_BTA_BOSS_01h_IdleLines = new List<string> { VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01 };

	private List<string> m_VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_Lines = new List<string> { VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_01()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01, VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01,
			VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_02, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_03, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_04, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleA_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleB_01,
			VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_IdleC_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_BTA_BOSS_01h_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_HeroPower_Lines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_deathLine = VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossDeath_01;
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_01_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_BossStart_01);
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Emote_Response_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_UnstableFelboltFriendlyDies_01);
			break;
		case 102:
			yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_BamboozleTrigger_01);
			break;
		case 500:
			yield return PlayLineOnlyOnce(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Attack_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_VictoryA_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_01_VictoryB_01);
			GameState.Get().SetBusy(busy: false);
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
		if (m_playedLines.Contains(entity.GetCardId()) && entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "CS2_077"))
		{
			if (cardId == "EX1_129")
			{
				yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_FanofKnives_01);
			}
		}
		else
		{
			yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Hero_Sprint_01);
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
			case "BT_300":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_HandofGuldan_01);
				break;
			case "BTA_13":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_Deteriorate_01);
				break;
			case "BTA_17":
				yield return PlayLineAlways(actor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_Boss_RustedVoidwalker_01);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (turn == 1)
		{
			yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_01_TurnOne_01);
			yield return PlayLineAlways(enemyActor, VO_BTA_BOSS_01h_Male_Demon_Mission_Fight_01_TurnOneResponse_01);
		}
	}
}
