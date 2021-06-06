using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_15h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Death_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Death_02.prefab:53e487fe547ce424a99d083996127889");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01.prefab:88f00ff818b5b3c47b3f9ec33835bb85");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01.prefab:66243972da7da114499a411e6c434dd6");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01.prefab:4c63fdf26e33fef4cb6bddc573c47f7c");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02.prefab:a2dd6dbc3713ce34b9ee151c3d558904");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03.prefab:e269a1fece3444444ad4e7b456360bdd");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04.prefab:b34a00b601345cd4e964e86c60c275e1");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05.prefab:ac30df941bcd2e54e90b8b067dd2da60");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06.prefab:f89145871a2bb714284f733a22c1d5bf");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_01.prefab:85e720bb252e5744c9ba0b19e6304f0f");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_02 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_02.prefab:2f6bec85149accb47a69d143d0d7f1f1");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Idle_03 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Idle_03.prefab:14ff1bc1a3a44ec4f91013198afa848c");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_Intro_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_Intro_01.prefab:364d46ec967ce55459b6a6994209ab18");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01.prefab:86e00c690c422384ca8955a010ad5695");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01.prefab:8086f9ef6a886c04786bca5ca056dab2");

	private static readonly AssetReference VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01 = new AssetReference("VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01.prefab:695a8269b3591be48ae676b8971a3210");

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_15h_Female_NightElf_Idle_01, VO_DALA_BOSS_15h_Female_NightElf_Idle_02, VO_DALA_BOSS_15h_Female_NightElf_Idle_03 };

	private List<string> m_HeroPowerLines = new List<string> { VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_15h_Female_NightElf_Death_02, VO_DALA_BOSS_15h_Female_NightElf_DefeatPlayer_01, VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_01, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_02, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_03, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_04, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_05, VO_DALA_BOSS_15h_Female_NightElf_HeroPowerTrigger_06, VO_DALA_BOSS_15h_Female_NightElf_Idle_01,
			VO_DALA_BOSS_15h_Female_NightElf_Idle_02, VO_DALA_BOSS_15h_Female_NightElf_Idle_03, VO_DALA_BOSS_15h_Female_NightElf_Intro_01, VO_DALA_BOSS_15h_Female_NightElf_IntroKriziki_01, VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01, VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01
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
		m_introLine = VO_DALA_BOSS_15h_Female_NightElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_15h_Female_NightElf_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_15h_Female_NightElf_EmoteResponse_01;
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return true;
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_15h_Female_NightElf_PlayerHeal_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_15h_Female_NightElf_PlayerDemon_01);
			break;
		case 103:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerLines);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId != "DALA_George" && cardId != "DALA_Kriziki")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
