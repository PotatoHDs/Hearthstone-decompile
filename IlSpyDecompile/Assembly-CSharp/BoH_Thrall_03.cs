using System.Collections;
using System.Collections.Generic;

public class BoH_Thrall_03 : BoH_Thrall_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03 = new AssetReference("VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03.prefab:1632e3415413bae4cae8baea9ac2d463");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01.prefab:82fcb3096fc3ce848b1dccf0c6e27e9b");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02.prefab:0252e71becc21194c9ce562ea6f8c558");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01.prefab:7c1bb55882755044b93c2d80cf5529d3");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01.prefab:d37737ad76c3c7147b48064ac3e1b749");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01.prefab:6e1084a2fe77fc6429cca7e7ea814bb4");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02.prefab:fbec9d12a083e7740afbc4325788156d");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03.prefab:f9b4c1fbbd747f34ea279adcdacfff08");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01.prefab:b63242d1f26861c4f86e947894b7ddaf");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02.prefab:b3100a568a7d2f9429cc1ee8f9fd0e00");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03.prefab:160f5d77dfdcfd24681e086e15b99017");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01.prefab:d6114deafb9b25a4abdcdc973ac0d9f6");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01.prefab:3987cc90ce6cc1849b8dd017d391ce50");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01.prefab:f3062ae46e0e6e04682219bbbba0ddcd");

	private static readonly AssetReference VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04 = new AssetReference("VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04.prefab:60a3c83e99cc3a44794d766a3b6817a5");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01.prefab:e82fe160cd510f84d8e65551e6159274");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02.prefab:2ff26dd9c751a754387eda26888d8866");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01.prefab:02659523d280b5f48941973d89cab85a");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02.prefab:b71ba06da00c1214b9f888399dfaf0b3");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02.prefab:9566d837a8ca6a34eae381e5204699e6");

	public static readonly AssetReference DrekTharBrassRing = new AssetReference("DrekThar_BrassRing_Quote.prefab:5df753488b9bf7846909a8badb04d0f3");

	private List<string> m_BossUsesHeroPowerLines = new List<string> { VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03 };

	private new List<string> m_BossIdleLines = new List<string> { VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Thrall_03()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_02, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3HeroPower_03, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_02,
			VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Idle_03, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02
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
		yield return MissionPlayVO(actor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Intro_01);
		yield return MissionPlayVO(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Intro_02);
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
		m_OverrideMusicTrack = MusicPlaylistType.InGame_BAR;
		m_standardEmoteResponseLine = VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3EmoteResponse_01;
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
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 507:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Loss_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3Victory_02);
			yield return PlayLineAlways(DrekTharBrassRing, VO_Story_Hero_DrekThar_Male_Orc_Story_Thrall_Mission3Victory_03);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3Victory_04);
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
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeA_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeA_02);
			break;
		case 3:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeB_02);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Thrall_Mission3ExchangeC_01);
			break;
		case 11:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Orgrim2_Male_Orc_Story_Thrall_Mission3ExchangeE_01);
			break;
		}
	}
}
