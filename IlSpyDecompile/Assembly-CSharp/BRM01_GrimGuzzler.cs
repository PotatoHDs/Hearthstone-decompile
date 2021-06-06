using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM01_GrimGuzzler : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	private bool m_eTCLinePlayed;

	private bool m_succubusLinePlayed;

	private bool m_warGolemLinePlayed;

	private bool m_disableSpecialCardVO = true;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA01_1_RESPONSE_03.prefab:6bce12880a0d1ef408aab318f0c0d699");
		PreloadSound("VO_BRMA01_1_HERO_POWER_04.prefab:ddd556f3fc3107642ba85ffa60e56efd");
		PreloadSound("VO_BRMA01_1_CARD_05.prefab:4d60a73b9cc4c3645a387eb198be2d8a");
		PreloadSound("VO_BRMA01_1_ETC_06.prefab:966f2e43e86303b4da0adc2529bd22a3");
		PreloadSound("VO_BRMA01_1_SUCCUBUS_08.prefab:6e04c1f0a2ce98d4187e5ee6499211a9");
		PreloadSound("VO_BRMA01_1_WARGOLEM_07.prefab:d45ef18ce5906d74e94ecb0e56323d37");
		PreloadSound("VO_BRMA01_1_TURN1_02.prefab:914cf2bf87da18c458f38f8fcbc98481");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_BRMA01_1_RESPONSE_03.prefab:6bce12880a0d1ef408aab318f0c0d699",
						m_stringTag = "VO_BRMA01_1_RESPONSE_03"
					}
				}
			}
		};
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (entity.GetCardId())
		{
		case "BRMA01_2":
		case "BRMA01_2H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_HERO_POWER_04.prefab:ddd556f3fc3107642ba85ffa60e56efd", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "BRMA01_4":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_CARD_05.prefab:4d60a73b9cc4c3645a387eb198be2d8a", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			if (!m_eTCLinePlayed && !m_disableSpecialCardVO)
			{
				m_eTCLinePlayed = true;
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_ETC_06.prefab:966f2e43e86303b4da0adc2529bd22a3", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 2:
			if (!m_succubusLinePlayed && !m_disableSpecialCardVO)
			{
				m_succubusLinePlayed = true;
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_SUCCUBUS_08.prefab:6e04c1f0a2ce98d4187e5ee6499211a9", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 3:
			if (!m_warGolemLinePlayed && !m_disableSpecialCardVO)
			{
				m_warGolemLinePlayed = true;
				GameState.Get().SetBusy(busy: true);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_WARGOLEM_07.prefab:d45ef18ce5906d74e94ecb0e56323d37", Notification.SpeechBubbleDirection.TopRight, actor));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA01_1_TURN1_02.prefab:914cf2bf87da18c458f38f8fcbc98481", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case 3:
			m_disableSpecialCardVO = false;
			break;
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_COREN_DEAD_28"), "VO_NEFARIAN_COREN_DEAD_28.prefab:0539437e9ff9ee9409bd7cd236d59d53");
		}
	}
}
