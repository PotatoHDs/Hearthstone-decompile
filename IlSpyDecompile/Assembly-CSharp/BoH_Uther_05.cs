using System.Collections;
using System.Collections.Generic;

public class BoH_Uther_05 : BoH_Uther_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01.prefab:54906235720dd1a4b8eb19b637d7d5e7");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01.prefab:60d461e053787cf4db229e217b16340a");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01.prefab:5e3816881f0f7b849a61f6a2289ee5b9");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02.prefab:7478ea3636febc64b937e471423d3a2e");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03.prefab:8e3369e156e1d1541a13df4a4f90f741");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01.prefab:9f989fd48cf9cc54ca0df8c4edb99fef");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02.prefab:eb437c7c6810c264681bfcb2e28f98f0");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03.prefab:a4450e194f14d4c4a8ecdf7ef0cbb155");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01.prefab:abc3a388ca7084446a26ca173afd0bfa");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01.prefab:bda83977e08ef5a42838259dd60c3e87");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01.prefab:e3c76fcb46d2ffa4fa6b134ea0275142");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01.prefab:fd73050bdc03ee64dbd4a7b865b7a2a3");

	private static readonly AssetReference VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01.prefab:e325d9f1482854343a13118882774668");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01.prefab:8cf590f8fcdd88940a9db94d6891e0ce");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02.prefab:8afe9c6a2e2b20546ab5da1e9e565f8b");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01.prefab:bd82767ee0e8e2140a35734ab213c2cf");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01.prefab:8e22b7a203c97d84c92ed1bf18069f86");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02.prefab:92720addc1a4e564fb9e62f589ac7762");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01.prefab:2766583d1f91d4c4bbb9eb725d6d21e5");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02.prefab:a66067c97bc5fee4ead7a451ec7d8e8b");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01.prefab:419fac51d65b28d4ba8e250d6c96dc21");

	private static readonly AssetReference VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02 = new AssetReference("VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02.prefab:af77db00c5d636f4a888fb81386cf23b");

	private static readonly AssetReference VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01 = new AssetReference("VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01.prefab:f5ab5ede04b896744b4bfe5eca00f60f");

	public static readonly AssetReference ArthasBrassRing = new AssetReference("Arthas_BrassRing_Quote.prefab:49bb0ac905ae3c54cbf3624451b62ab4");

	private List<string> m_HeroPowerLines = new List<string> { VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03 };

	private List<string> m_IdleLines = new List<string> { VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Death_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_02, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5HeroPower_03, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_02, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Idle_03, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01,
			VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01,
			VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_01);
		yield return PlayLineAlways(enemyActor, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Intro_02);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override List<string> GetBossHeroPowerRandomLines()
	{
		return m_HeroPowerLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_standardEmoteResponseLine = VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType != EmoteType.START && MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		case 504:
			yield return PlayLineAlways(actor, VO_Story_03_Blademaster_Male_Orc_Story_Uther_Mission5Loss_01);
			break;
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_01);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5Victory_02);
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
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (turn)
		{
		case 3:
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeA_02);
			break;
		case 7:
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeB_01);
			break;
		case 13:
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_01);
			yield return PlayLineAlways(ArthasBrassRing, VO_Story_Hero_Arthas_Male_Human_Story_Uther_Mission5ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Uther_Male_Human_Story_Uther_Mission5ExchangeC_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_BRMAdventure);
	}
}
