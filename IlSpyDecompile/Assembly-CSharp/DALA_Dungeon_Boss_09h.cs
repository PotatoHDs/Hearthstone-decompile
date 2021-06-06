using System.Collections;
using System.Collections.Generic;

public class DALA_Dungeon_Boss_09h : DALA_Dungeon
{
	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01.prefab:2d3a0eca03e693048935dedaf40b5950");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Death_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Death_02.prefab:a521e874798c5f94e925cc7c38c92395");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02.prefab:6d0611de6e05d634995a4e5f9cf996ae");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01.prefab:5a3899f8e090a284ca9306c5133eda33");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01.prefab:b0f5526fd41939a42a7da237bf2f3b67");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02.prefab:b05b69160a5a2234a8c7276b6bb82418");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03.prefab:fd72d1583ebd0a748b126e08044f4181");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04.prefab:6342c54e4556d124fa6eb64060907fc2");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01.prefab:5347ca8e73d49e3458ad43b273a8b22d");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02.prefab:494387459013c6146a4497ecb65c0ab8");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_01.prefab:abe046261c6acca4cb3ee65b7caa0a6d");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_02 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_02.prefab:829eae7d0040b844c8384a07b1560871");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Idle_03 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Idle_03.prefab:e12237ce71051d74ca1c14fa312fef2a");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_Intro_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_Intro_01.prefab:a95b2f05fa1296d4fb3d7224c1784944");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01.prefab:00769729f73611444a4c0c57c66f26d7");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01.prefab:783cd7dcdb78d67469bebb203aec84d9");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01.prefab:685451e94347e1344a80bb2ca006eb8e");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01.prefab:69b0a0dacde67664ea064cbb354a5535");

	private static readonly AssetReference VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01 = new AssetReference("VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01.prefab:15acdf0666a18a243a578d76b97dd4da");

	private List<string> m_HeroPowerLines = new List<string> { VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04 };

	private List<string> m_HeroPowerBigLines = new List<string> { VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01, VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02 };

	private static List<string> m_IdleLines = new List<string> { VO_DALA_BOSS_09h_Female_Draenei_Idle_01, VO_DALA_BOSS_09h_Female_Draenei_Idle_02, VO_DALA_BOSS_09h_Female_Draenei_Idle_03 };

	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>
		{
			VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01, VO_DALA_BOSS_09h_Female_Draenei_Death_02, VO_DALA_BOSS_09h_Female_Draenei_DefeatPlayer_02, VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_01, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_02, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_03, VO_DALA_BOSS_09h_Female_Draenei_HeroPower_04, VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_01, VO_DALA_BOSS_09h_Female_Draenei_HeroPowerBig_02,
			VO_DALA_BOSS_09h_Female_Draenei_Idle_01, VO_DALA_BOSS_09h_Female_Draenei_Idle_02, VO_DALA_BOSS_09h_Female_Draenei_Idle_03, VO_DALA_BOSS_09h_Female_Draenei_Intro_01, VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01, VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01, VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01, VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01, VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01
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
		m_introLine = VO_DALA_BOSS_09h_Female_Draenei_Intro_01;
		m_deathLine = VO_DALA_BOSS_09h_Female_Draenei_Death_02;
		m_standardEmoteResponseLine = VO_DALA_BOSS_09h_Female_Draenei_EmoteResponse_01;
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			if (cardId == "DALA_George")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_09h_Female_Draenei_IntroGeorge_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId == "DALA_Tekahn")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(VO_DALA_BOSS_09h_Female_Draenei_IntroTekahn_01, Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else if (cardId != "DALA_Rakanishu")
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_introLine, Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech(m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 101:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPowerBigLines);
			break;
		case 102:
			yield return PlayAndRemoveRandomLineOnlyOnce(enemyActor, m_HeroPowerLines);
			break;
		case 103:
			yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_09h_Female_Draenei_BossBigMinion_01);
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
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			switch (cardId)
			{
			case "DALA_700":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_09h_Female_Draenei_PlayerBorrowedTime_01);
				break;
			case "FP1_013":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_09h_Female_Draenei_PlayerKelthuzad_01);
				break;
			case "UNG_028t":
				yield return PlayLineOnlyOnce(enemyActor, VO_DALA_BOSS_09h_Female_Draenei_PlayerTimeWarp_01);
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
