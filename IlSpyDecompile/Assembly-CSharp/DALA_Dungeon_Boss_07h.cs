using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_07h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01.prefab:ce6517223437e3e44aef1c095de05e1b");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Death_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Death_02.prefab:1f7c09a57b990764c955a16306d323af");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01.prefab:4760d90d196631947a684013b4694426");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01.prefab:d8937766bd4c9754095f5a69f7da6bb5");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02.prefab:075b1d9d88e41824d84229356e946d45");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03.prefab:6768cd1b074e1e34f9999934ae695fa2");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04.prefab:73676e2c215793c429e8c1c0bd92b8c1");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06.prefab:7e33cf311899928429f9764676e28267");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07.prefab:46c3bee69c6f3e84dbb29fc9b0657bbd");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_01.prefab:52367dffc466a154eb4bdb2e5ae0e2f8");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_02 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_02.prefab:23f4d1a30f688434d840b749a945edfb");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Idle_03 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Idle_03.prefab:b4e2dea35ecb43442a5cedcd6cacea51");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_Intro_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_Intro_01.prefab:52267af1f01386944928ccf738965d30");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01.prefab:6a88949089a597b47832653c84573978");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01.prefab:e96b9344c597e3d439a8317c2a72e323");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01.prefab:12dd0ef2a32772b4483ab2cfaf9aa2bc");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01.prefab:c40357e8bfe39254daba3fa8ee9e5f8e");

	private static readonly AssetReference VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01 = new AssetReference("VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01.prefab:15fba6062f731e948951f39561625bd2");

	private static List<string> m_HeroPowerTrigger = new List<string> { VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_07h_Male_BloodElf_Idle_01, VO_DALA_BOSS_07h_Male_BloodElf_Idle_02, VO_DALA_BOSS_07h_Male_BloodElf_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01, VO_DALA_BOSS_07h_Male_BloodElf_Death_02, VO_DALA_BOSS_07h_Male_BloodElf_DefeatPlayer_01, VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_02, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_03, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_04, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_06, VO_DALA_BOSS_07h_Male_BloodElf_HeroPower_07, VO_DALA_BOSS_07h_Male_BloodElf_Idle_01,
			VO_DALA_BOSS_07h_Male_BloodElf_Idle_02, VO_DALA_BOSS_07h_Male_BloodElf_Idle_03, VO_DALA_BOSS_07h_Male_BloodElf_Intro_01, VO_DALA_BOSS_07h_Male_BloodElf_IntroKriziki_01, VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01, VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01, VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01, VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01
		};
		SetBossVOLines(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public override List<string> GetIdleLines()
	{
		return m_IdleLines;
	}

	public override void OnCreateGame()
	{
		base.OnCreateGame();
		m_introLine = VO_DALA_BOSS_07h_Male_BloodElf_Intro_01;
		m_deathLine = VO_DALA_BOSS_07h_Male_BloodElf_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_07h_Male_BloodElf_EmoteResponse_01;
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
			yield return PlayLineOnlyOnce(actor, VO_DALA_BOSS_07h_Male_BloodElf_BossBeastBig_01);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTrigger);
			break;
		default:
			yield return base.HandleMissionEventWithTiming(missionEvent);
			break;
		case 103:
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
			if (cardId == "DALA_Barkeye")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_07h_Male_BloodElf_IntroOlBarkeye_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Kriziki" && cardId != "DALA_Squeamlish")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "DAL_747":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_07h_Male_BloodElf_PlayerFlightMaster_01);
				break;
			case "LOEA02_10a":
			case "NEW1_033":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_07h_Male_BloodElf_PlayerLeokk_01);
				break;
			case "DAL_378":
			case "DAL_378ts":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_07h_Male_BloodElf_PlayerUnleashtheBeast_01);
				break;
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
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
		}
	}
}
