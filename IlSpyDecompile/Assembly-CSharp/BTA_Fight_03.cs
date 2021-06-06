using System.Collections;
using System.Collections.Generic;

public class BTA_Fight_03 : BTA_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01.prefab:aa77a6190a706a9409f74424055e919d");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01.prefab:c2f61fcc26296f742b3ae0339bcf6d5f");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01.prefab:a238ff9cbe5b04d4dbc58a04c21f2170");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01.prefab:f958d5cd88f88ec49a179c2c9e4dd5e0");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01.prefab:4502e40b6c2c6d34589f2fcec0b56d66");

	private static readonly AssetReference VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01 = new AssetReference("VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01.prefab:5d9fe6f43cfefed49a6c99b3b115f94b");

	private static readonly AssetReference VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01 = new AssetReference("VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01.prefab:f0250cebc03517c4c965b6651451dcea");

	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01.prefab:981dcbdafca1eb0408b78d03458537cc");

	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01.prefab:bc143e2ea74384144ae654373969927a");

	private static readonly AssetReference VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01 = new AssetReference("VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01.prefab:72073a89bec549e4f97966c19544d294");

	private static readonly AssetReference BTA_BOSS_03h_BossStart_01 = new AssetReference("BTA_BOSS_03h_BossStart_01.prefab:ed4dd64ad7f74444c86bb7c59c26e43c");

	private static readonly AssetReference VO_BTA_BOSS_10h2_Male_Sporelok_Death_02 = new AssetReference("VO_BTA_BOSS_10h2_Male_Sporelok_Death_02.prefab:6df02684ddf4c454d9217e9998b3f09f");

	private List<string> m_missionEventTrigger501_Lines = new List<string> { VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01, VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BTA_Fight_03()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Misc_01, VO_BTA_01_Female_NightElf_Mission_Fight_03_Aranna_Response_01, VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01, VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_01, VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01, VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01, VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01, VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01, VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01, VO_BTA_09_Female_Naga_Mission_Fight_03_VictoryB_01,
			BTA_BOSS_03h_BossStart_01, VO_BTA_BOSS_10h2_Male_Sporelok_Death_02
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
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
		yield return PlayLineAlways(actor, VO_BTA_01_Female_NightElf_Mission_Fight_03_PlayerStart_01);
		yield return PlayLineAlways(enemyActor, BTA_BOSS_03h_BossStart_01);
		GameState.Get().SetBusy(busy: false);
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType);
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, VO_BTA_BOSS_10h2_Male_Sporelok_Death_02);
			GameState.Get().SetBusy(busy: false);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryA_Alt_01);
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_05"), BTA_Dungeon.SklibbBrassRing, VO_BTA_05_Male_Sporelok_Mission_Fight_03_VictoryB_Alt_01);
			yield return PlayLineAlways(friendlyActor, VO_BTA_01_Female_NightElf_Mission_Fight_03_VictoryC_Alt_01);
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
		if (!m_playedLines.Contains(entity.GetCardId()) || entity.GetCardType() == TAG_CARDTYPE.HERO_POWER)
		{
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "BTA_09")
			{
				yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, VO_BTA_09_Female_Naga_Mission_Fight_03_Shalja_Play_01);
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
			GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			GameState.Get().GetFriendlySidePlayer().GetHero()
				.GetCard()
				.GetActor();
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
			yield return PlayLineAlwaysWithBrassRing(GetFriendlyActorByCardId("BTA_09"), BTA_Dungeon.ShaljaBrassRing, VO_BTA_09_Female_Naga_Mission_Fight_03_BronzeRingNaga_01);
		}
	}
}
