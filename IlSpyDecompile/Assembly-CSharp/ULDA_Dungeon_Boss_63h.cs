using System.Collections;
using System.Collections.Generic;

public class ULDA_Dungeon_Boss_63h : ULDA_Dungeon
{
	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01.prefab:530b8dddac9eb044aaf60d1ad8cba961");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01.prefab:6203deb0428d5664982f2b7ae71ef565");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01.prefab:1799a5976ac96e94992cae474a6d1a6e");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01.prefab:52ea4f6e5cff0334bb18839ad70263f0");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01.prefab:9345cecf23a8cdc4cbd24b4510493f5f");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01.prefab:3eb46d22cdecb21458d15723d5193ad6");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01.prefab:1767d59f4aeea99449281a9d9ad3ce5e");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02.prefab:abf3ff922e98d0649b17f988f9c3dedf");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03.prefab:110a9e6222b89ea48b6a262d92a80a2c");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04.prefab:db8fe6a05423c1248b05f4230ea7a18e");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05.prefab:0fc0189270761f14b912b17ec0045a3f");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01.prefab:767b8135cbddfd643b11a0a0e2aebc1c");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01.prefab:5d04e94cc77827945b305fa3bd624563");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01.prefab:94aeec70cd130704b95d152fc8c83877");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01.prefab:a68cd9845137e9244ad4e960161ca730");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01.prefab:12b087cd156d772488c5c6f6be6f84a7");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01.prefab:37dc56526663fcf4396e403b1ba9de2c");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01.prefab:932d49c94b03bb848a8245c67202436a");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01.prefab:797dfb3672013504bafce8ad913ca8c2");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01.prefab:cd61ce439bbf9bc47814336adeb3560d");

	private static readonly AssetReference VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01 = new AssetReference("VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01.prefab:73386dc0dec033d42bc2991cee453823");

	private List<string> m_HeroPowerTriggerLines = new List<string> { VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01, VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01, VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01, VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01, VO_ULDA_BOSS_63h_Female_Sethrak_DefeatPlayer_01, VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_01, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_02, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_03, VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_04,
			VO_ULDA_BOSS_63h_Female_Sethrak_HeroPowerTrigger_05, VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01, VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01, VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01, VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01, VO_ULDA_BOSS_63h_Female_Sethrak_IntroElise_01, VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01, VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01, VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01, VO_ULDA_BOSS_63h_Female_Sethrak_PlayerRamkahenAlly_01,
			VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01
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
		m_introLine = VO_ULDA_BOSS_63h_Female_Sethrak_Intro_01;
		m_deathLine = VO_ULDA_BOSS_63h_Female_Sethrak_DeathALT_01;
		m_standardEmoteResponseLine = VO_ULDA_BOSS_63h_Female_Sethrak_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "ULDA_Reno")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_ULDA_BOSS_63h_Female_Sethrak_IntroReno_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "ULDA_Elise")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
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
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(actor, m_HeroPowerTriggerLines);
			break;
		case 103:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_63h_Female_Sethrak_Idle1_01);
			break;
		case 104:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_63h_Female_Sethrak_Idle2_01);
			break;
		case 105:
			yield return PlayBossLine(actor, VO_ULDA_BOSS_63h_Female_Sethrak_Idle3_01);
			break;
		case 102:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_OverloadPass_01);
			break;
		case 106:
			yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_PlayerOverloadTrigger_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (cardId == "ULDA_036")
			{
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_PlayerTamedLocust_01);
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "ULD_181":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_BossEarthquake_01);
				break;
			case "ULD_276":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_BossEVILTotem_01);
				break;
			case "TRL_351":
				yield return PlayLineOnlyOnce(actor, VO_ULDA_BOSS_63h_Female_Sethrak_BossRainofToads_01);
				break;
			}
		}
	}
}
