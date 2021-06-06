using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM08_Drakkisath : BRM_MissionEntity
{
	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA08_1_RESPONSE_04.prefab:6f5840c652a862f46baddba08396a839");
		PreloadSound("VO_BRMA08_1_CARD_05.prefab:fd0151285c6aec540b909e0a29f5acb8");
		PreloadSound("VO_BRMA08_1_TURN1_03.prefab:e4d206e77c3c8f548934be5fcce89ea5");
		PreloadSound("VO_NEFARIAN_DRAKKISATH_RESPOND_48.prefab:f422b6326aa079743967cb9988b445c7");
		PreloadSound("VO_BRMA08_1_TURN1_ALT_02.prefab:d71f74a42d0105446a5e2e3c4b60e067");
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
						m_soundName = "VO_BRMA08_1_RESPONSE_04.prefab:6f5840c652a862f46baddba08396a839",
						m_stringTag = "VO_BRMA08_1_RESPONSE_04"
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
		if (cardId == "BRMA08_3" && !m_cardLinePlayed)
		{
			m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA08_1_CARD_05.prefab:fd0151285c6aec540b909e0a29f5acb8", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 quotePos = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA08_1_TURN1_ALT_02.prefab:d71f74a42d0105446a5e2e3c4b60e067", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case 4:
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA08_1_TURN1_03.prefab:e4d206e77c3c8f548934be5fcce89ea5", Notification.SpeechBubbleDirection.TopRight, actor));
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_DRAKKISATH_RESPOND_48"), "VO_NEFARIAN_DRAKKISATH_RESPOND_48.prefab:f422b6326aa079743967cb9988b445c7");
			}
			break;
		case 6:
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_DRAKKISATH1_49"), "VO_NEFARIAN_DRAKKISATH1_49.prefab:792c02424ea5f5d43989d65b4b3ca839");
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_DRAKKISATH_DEAD_50"), "VO_NEFARIAN_DRAKKISATH_DEAD_50.prefab:a0d0aa371c62ff24ca675731ff3e5396");
		}
	}
}
