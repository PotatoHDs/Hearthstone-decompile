using System.Collections;
using System.Collections.Generic;

public class BoH_Garrosh_01 : BoH_Garrosh_Dungeon
{
	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01.prefab:a27359e78de50f542a3be9d0069cbf1c");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01.prefab:3bbf297fecdf0db4e8f7726470d124e1");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01.prefab:4164528d7f86add4198eacd5b97bfffd");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01.prefab:b99434a4f221f5047b1105152e8459f0");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01.prefab:f080169bbbe9e0d48b0246eacc37e652");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01.prefab:f4f5905fb2c47f244b8301770c215a07");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02.prefab:78de3c71b449b6149b9427935ade9130");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03.prefab:32530928a287e5242895725f471e1352");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01.prefab:1b5d7cdd0cd0e4840b1bb203f4b10bad");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02.prefab:84252b2aeeca8c145a3e6a6b65821e63");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03.prefab:695fdea4057e5bd408048471af990db1");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01.prefab:7cf2773afbe47d446ba8c0ec745c379b");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01.prefab:fe035b8f20618c44a90fcf452afba8e5");

	private static readonly AssetReference VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01.prefab:1c4d40b8cdc53f546b0778a75b80bace");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01.prefab:6646be24734b4c249813b2a66eea0480");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01.prefab:ec8c83688fdfa68419898000a1de5f2c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01.prefab:2be3ba75ac5a4b44f9ec18e66e4aa22c");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01.prefab:4511ee3c405be304fb844ec17a2ba48d");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01.prefab:83c3bf0b027dd714f97d8099be6bb4d8");

	private static readonly AssetReference VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02 = new AssetReference("VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02.prefab:48bbb4aa8f1d0504483f4c171ee5ca96");

	private static readonly AssetReference VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01 = new AssetReference("VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01.prefab:99e85ad1471145a448721a7888786b7b");

	public static readonly AssetReference ThrallBrassRing = new AssetReference("Thrall_BrassRing_Quote.prefab:962e58c9390b0f842a8b64d0d44cf7b4");

	private List<string> m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPowerLines = new List<string> { VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03 };

	private List<string> m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1IdleLines = new List<string> { VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_02, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPower_03, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_02,
			VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Idle_03, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01,
			VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02
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
		yield return PlayLineAlways(actor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1EmoteResponse_01;
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_01);
			yield return PlayLineAlways(ThrallBrassRing, VO_Story_Hero_Thrall_Male_Orc_Story_Garrosh_Mission1Victory_02);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeA_01);
			break;
		case 3:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeB_01);
			yield return PlayLineAlways(enemyActor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeB_01);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeC_01);
			break;
		case 7:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Garrosh_Male_Orc_Story_Garrosh_Mission1ExchangeD_01);
			yield return PlayLineAlways(enemyActor, VO_Story_04_Geyah_Female_Orc_Story_Garrosh_Mission1ExchangeD_01);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BT);
	}
}
