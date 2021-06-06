using System.Collections;
using System.Collections.Generic;

public class BoH_Garrosh_08 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:e5c5b670db416714c9b00b5135f3b3be");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01.prefab:1168cbcb23827e44896f0cab1580d8b6");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02.prefab:57b33b0e3629d4743860c5ecd806e43f");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:e53520db3376e6842b91969fcdb16fe7");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:a3a2d35f3727f144dac3df3258f1a938");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01.prefab:74551dbaf11c0924b9571c8c77d93405");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01.prefab:18971804117b5f346bf4901f7319f2c7");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01.prefab:a490641c7d4016c418b5b005ec1e2427");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01.prefab:dfad08cd8266489478a4186af6f330a0");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02.prefab:9654500b7882a4241823e66435dee83f");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03.prefab:b1f25de0583b4473acce86e0176f9dd6");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01.prefab:2a5a7d9c5e03292439894ff0e6292629");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02.prefab:ebad60e3d5c19d348a7ced60b9dfb99e");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03.prefab:c2e60147c7a1369459491c54859e38be");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01.prefab:829d2a0cfaa6caa4ab12c9d9de5b0ae2");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01.prefab:72c8a96e92e979d4291d5765c7c672f4");

	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPowerLines = new List<string> { VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03 };

	private List<string> m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8IdleLines = new List<string> { VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_02,
			VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPower_03, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_02, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Idle_03, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01
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

	public override IEnumerator DoActionsBeforeDealingBaseMulliganCards()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(actor, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8EmoteResponse_01;
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
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(busy: true);
			while (m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(busy: false);
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (missionEvent)
		{
		case 502:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeC_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeC_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8Loss_01);
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return PlayLineAlways(actor, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission8ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeA_01);
			break;
		case 9:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission8ExchangeB_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_TRLFinalBoss);
	}
}
