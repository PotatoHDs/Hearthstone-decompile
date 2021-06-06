using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM11_Vaelastrasz : BRM_MissionEntity
{
	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA11_1_RESPONSE_03.prefab:f776a76ac4474254fac47aa4c2860190");
		PreloadSound("VO_BRMA11_1_HERO_POWER_05.prefab:a15ff3617c584fc4dacf011685a21dd2");
		PreloadSound("VO_BRMA11_1_CARD_04.prefab:1a5c6eac59471434a84477c152b8aa0d");
		PreloadSound("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad");
		PreloadSound("VO_BRMA11_1_ALEXSTRAZA_07.prefab:8d0705fc4be8e4144b7764931784dbca");
		PreloadSound("VO_BRMA11_1_TURN1_02.prefab:dd79459ebc3fe2346858c23caea3b337");
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
						m_soundName = "VO_BRMA11_1_RESPONSE_03.prefab:f776a76ac4474254fac47aa4c2860190",
						m_stringTag = "VO_BRMA11_1_RESPONSE_03"
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
		string cardId = entity.GetCardId();
		if (cardId == "BRMA11_3" && !m_cardLinePlayed)
		{
			m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_HERO_POWER_05.prefab:a15ff3617c584fc4dacf011685a21dd2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		default:
			yield break;
		case 1:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_TURN1_02.prefab:dd79459ebc3fe2346858c23caea3b337", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			yield break;
		case 2:
			break;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_CARD_04.prefab:1a5c6eac59471434a84477c152b8aa0d", Notification.SpeechBubbleDirection.TopRight, enemyActor));
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad", Notification.SpeechBubbleDirection.TopRight, actor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 2:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_ALEXSTRAZA_07.prefab:8d0705fc4be8e4144b7764931784dbca", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA11_1_KILL_PLAYER_06.prefab:52ea6310c1553f147a1de7d2bca500ad"));
		}
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_VAEL_DEAD_57"), "VO_NEFARIAN_VAEL_DEAD_57.prefab:f0f295213070e68488bb5a0b35948f77");
		}
	}
}
