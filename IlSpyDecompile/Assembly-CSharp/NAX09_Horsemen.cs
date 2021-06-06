using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX09_Horsemen : NAX_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	private bool m_introSequenceComplete;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX9_01_CUSTOM_02.prefab:aabdff7ec08cc1f44a7c8c391c744e2f");
		PreloadSound("VO_NAX9_01_EMOTE_04.prefab:1d1eb70ed25d60c429b27471ec10b191");
		PreloadSound("VO_FP1_031_EnterPlay_06.prefab:51754c9428cdf374882cb4020bbd5627");
		PreloadSound("VO_NAX9_02_CUSTOM_01.prefab:520d0daa9374bfa47ab3f380f0e1ef65");
		PreloadSound("VO_NAX9_03_CUSTOM_01.prefab:4fb7d8593f95c404f97ddd63c29e939c");
		PreloadSound("VO_NAX9_04_CUSTOM_01.prefab:9581debb360b7dd478f7ddfeeda6768e");
		PreloadSound("VO_FP1_031_Attack_07.prefab:b4c323c69c7f5cf418ec6b228b188c5d");
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
						m_soundName = "VO_NAX9_01_EMOTE_04.prefab:1d1eb70ed25d60c429b27471ec10b191",
						m_stringTag = "VO_NAX9_01_EMOTE_04"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_BARON2_64", "VO_KT_BARON2_64.prefab:485607a6e18abc9458ba36d2b952d403");
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn != 1)
		{
			yield break;
		}
		Actor baronActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor blaumeuxActor = null;
		Actor thaneActor = null;
		Actor actor = null;
		foreach (Card card in GameState.Get().GetOpposingSidePlayer().GetBattlefieldZone()
			.GetCards())
		{
			switch (card.GetEntity().GetCardId())
			{
			case "NAX9_02":
				blaumeuxActor = card.GetActor();
				break;
			case "NAX9_03":
				thaneActor = card.GetActor();
				break;
			case "NAX9_04":
				actor = card.GetActor();
				break;
			}
		}
		if (actor == null)
		{
			m_introSequenceComplete = true;
			yield break;
		}
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX9_02_CUSTOM_01.prefab:520d0daa9374bfa47ab3f380f0e1ef65", Notification.SpeechBubbleDirection.TopRight, actor));
		if (blaumeuxActor != null)
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX9_03_CUSTOM_01.prefab:4fb7d8593f95c404f97ddd63c29e939c", Notification.SpeechBubbleDirection.TopRight, blaumeuxActor));
		}
		if (baronActor != null)
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX9_01_CUSTOM_02.prefab:aabdff7ec08cc1f44a7c8c391c744e2f", Notification.SpeechBubbleDirection.TopRight, baronActor));
		}
		if (thaneActor != null)
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX9_04_CUSTOM_01.prefab:9581debb360b7dd478f7ddfeeda6768e", Notification.SpeechBubbleDirection.TopRight, thaneActor));
		}
		m_introSequenceComplete = true;
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_introSequenceComplete)
		{
			yield break;
		}
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
		string cardId = entity.GetCardId();
		if (!(cardId == "NAX9_06"))
		{
			if (cardId == "NAX9_07" && !m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_FP1_031_Attack_07.prefab:b4c323c69c7f5cf418ec6b228b188c5d", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (!m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_FP1_031_EnterPlay_06.prefab:51754c9428cdf374882cb4020bbd5627", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
