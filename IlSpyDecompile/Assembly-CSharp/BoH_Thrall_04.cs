using System.Collections;
using System.Collections.Generic;

public class BoH_Thrall_04 : BoH_Thrall_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01.prefab:4c0b63c39d320dc4b98e6c2534e6ae99");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01.prefab:2f36e1d15baa3494c99f76f99fff44aa");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01.prefab:239abdaddda43a44a94a635afadd2916");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02.prefab:a042711cd858b6847b2c4d8b14b2397b");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01.prefab:89a0644c39f524246bebb6071db02a15");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01.prefab:457bc30a75a8472f994825bfe91016a8");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02.prefab:e6fa581502262834b9cc4f70ecf089ce");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03.prefab:4acc9795798cb8845baae85e506e5380");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01.prefab:f9c85bf0404b33b4298054b731d0d348");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02.prefab:8044ee403a328e84fad151cf340ff56a");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03.prefab:36915d97e6c75b24ea3969c76b33e4de");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01.prefab:ac1859af0ade3ef4a8e161e60be20c5e");

	private static readonly AssetReference VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01 = new AssetReference("VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01.prefab:ec89c713c17fa70498f3f574bc130f55");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02.prefab:501b6ffbf23dad942bbadc35b9619169");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02.prefab:d0f98203b90be504385fba07ca2e3d1b");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01.prefab:e047763d128221c44b4606752da2fa28");

	private static readonly AssetReference VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01 = new AssetReference("VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01.prefab:a47ee3092fc3c9e4ea752540df8038ef");

	private static readonly AssetReference VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02 = new AssetReference("VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02.prefab:053e818ca35b1ed46aaaa876fe8eee53");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Thrall_04()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_02, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4HeroPower_03, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_02,
			VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Idle_03, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01, VO_Prologue_Mannoroth_Male_Demon_Prologue_Mannoroth_Death_01, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01, VO_Story_Hero_Grommash_Male_Orc_Story_Jaina_Mission4HeroPower_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Intro_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BRMAdventure;
		m_standardEmoteResponseLine = VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4EmoteResponse_01;
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor2, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4Victory_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeA_01);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeB_02);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Mannoroth_Male_Demon_Story_Thrall_Mission4ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission4ExchangeC_02);
			break;
		}
	}
}
