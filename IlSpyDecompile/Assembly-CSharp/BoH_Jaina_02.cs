using System.Collections;
using System.Collections.Generic;

public class BoH_Jaina_02 : BoH_Jaina_Dungeon
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01.prefab:32b4d155b3bd7a34bb39aefe2fc6530b");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01.prefab:cea475a6fa82c344cbfac3b3bd381325");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01.prefab:a67a1b32b9ae9d54283996e038f07745");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01.prefab:8f771159bea1bbe40810215964797a8a");

	private static readonly AssetReference VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01.prefab:e7f55d97437688c4cbe0b286a7ada7ba");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01.prefab:a565ea69aeb20074fb73c1a7eca1866f");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01.prefab:815d9dbcff0ecce4e984b48da6ab5f53");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01.prefab:dca6d1d61215e7446afe84643082779e");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01.prefab:5c0be8feedf10d749a3e776bf61831f2");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02.prefab:67439fcb09d8b7d408acd5644dc77913");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01.prefab:27c51b6237312534dae795d2ce02d3d4");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02.prefab:93713c64656c29847a669cdaba7860a6");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03.prefab:82fa6b2ba04141b40890af58dbbded3d");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01.prefab:afff785af98ce0d4d831d96e3ff82322");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02.prefab:2798ac2782e0b634581e59b498e05e89");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03.prefab:38c35d1826da5bb49a63c5eb08dec98e");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01.prefab:7ce759b2460180e4481f1eef84492108");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01.prefab:d20943824575eb04ab3d1e420b17f163");

	private static readonly AssetReference VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01 = new AssetReference("VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01.prefab:fb5d7df277fa9be4ba3da1d3209157a4");

	private List<string> m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPowerLines = new List<string> { VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03 };

	private List<string> m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2IdleLines = new List<string> { VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.DO_OPENING_TAUNTS,
			false
		} };
	}

	public BoH_Jaina_02()
	{
		m_gameOptions.AddBooleanOptions(s_booleanOptions);
	}

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02,
			VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_02, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2HeroPower_03, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_02, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Idle_03, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01
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
		yield return PlayLineAlways(actor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Intro_01);
		yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Intro_01);
		GameState.Get().SetBusy(busy: false);
	}

	public override List<string> GetIdleLines()
	{
		return m_VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2IdleLines;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission1EmoteResponse_01, Notification.SpeechBubbleDirection.TopRight, actor));
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
		case 501:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Victory_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2Victory_01);
			GameState.Get().SetBusy(busy: false);
			break;
		case 504:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineAlways(actor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2Loss_01);
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
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeA_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeA_01);
			break;
		case 5:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeB_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeB_01);
			break;
		case 7:
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_01);
			yield return PlayLineAlways(friendlyActor, VO_Story_Hero_Jaina_Human_Female_Story_Jaina_Mission2ExchangeC_01);
			yield return PlayLineAlways(enemyActor, VO_Story_Hero_Kaelthas_Male_BloodElf_Story_Jaina_Mission2ExchangeC_02);
			break;
		}
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Karazhan);
	}
}
