using System.Collections;
using System.Collections.Generic;

public class BoH_Anduin_07 : BoH_Anduin_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01.prefab:d845d3bfb2f6fe04a9bb3741f0b5b713");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01.prefab:2164189090f6aa94fb75474f122ad6ed");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02.prefab:b78ac66284a921a4999d7532fd3bf8f8");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01.prefab:345fe423e3c1cc748b05493f17fef94d");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01.prefab:296ae0a731f235a46b149eb9b1924e9e");

	private static readonly AssetReference VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03 = new AssetReference("VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03.prefab:a34a3051225d65549a3ceced41813c2b");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01.prefab:6283d04613c321442a92ae360aa6fcdc");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01.prefab:ee51597d2de69e342bf4b28f7564a22d");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02.prefab:854c94bdc042c6e4b9939f34f9172bb0");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01.prefab:8a127d6e4493e7e4cb950dac500e94cf");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01.prefab:b64cf8e781652a44aaf370799ed10a85");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02.prefab:f1bc0f0d15f00fe4998bafd5945917cc");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03.prefab:f8290f36cfb34e5458fd016b8b050a7f");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01.prefab:0641e7c422bb3e140a3d4d471c760e49");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02.prefab:fe4962465803a8246b989c0bd3a9290a");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03.prefab:9c9223d9342edf34587fe9b899fbf297");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01.prefab:a0fc887b9b603a74bb2787143873a5b3");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02.prefab:028c060e4f253554c81ff691832b723b");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03.prefab:041d9c3886db4794aa7428b5c47e7eb7");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01.prefab:8b58e873ffa31e44a9ac79ccd47e8060");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01.prefab:b56aa1f374b497a4aa31dd654030e220");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01.prefab:0b235d11449ec4c44bfce28ec1506a66");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03 };

	private List<string> m_missionEventTrigger502Lines = new List<string> { VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Anduin_07()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01,
			VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ShaCallout_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7HeroPower_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Idle_03, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01,
			VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
	}

	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return MissionPlayVO(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetBossIdleLines()
	{
		return m_BossIdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_BossUsesHeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_OverrideMulliganMusicTrack = MusicPlaylistType.InGame_BT;
		m_standardEmoteResponseLine = VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7EmoteResponse_01;
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Anduin_Mission7Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 103:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Anduin_Mission7Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 101:
			GameState.Get().SetBusy(busy: true);
			yield return MissionPlayVO(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 112:
			yield return MissionPlayVOOnce(actor, m_missionEventTrigger502Lines);
			break;
		case 515:
			yield return MissionPlayVO(actor, m_standardEmoteResponseLine);
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeA_02);
			break;
		case 5:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeB_01);
			yield return MissionPlayVO(enemyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission7ExchangeB_01);
			break;
		case 9:
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_01);
			yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Anduin_Male_Human_Story_Garrosh_Mission7ExchangeD_02);
			break;
		}
	}
}
