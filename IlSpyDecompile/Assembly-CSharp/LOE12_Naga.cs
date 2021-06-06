using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE12_Naga : LOE_MissionEntity
{
	private bool m_pearlLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_12_RESPONSE.prefab:3d2ddf8b667255e4fb92ea9d2cce0b42");
		PreloadSound("VO_LOE_12_NAZJAR_TURN_1.prefab:9dcfc1a022a6c0540bbc71fa387b47e7");
		PreloadSound("VO_LOE_12_NAZJAR_TURN_1_FINLEY.prefab:8016f993cefe7514690bb0a1f83e4d80");
		PreloadSound("VO_LOE_12_NAZJAR_TURN_3_FINLEY.prefab:b471b5571b95207418831574c5f02285");
		PreloadSound("VO_LOE_NAZJAR_TURN_3_CARTOGRAPHER.prefab:26d95ed2b270ab546b6be7b791b10195");
		PreloadSound("VO_LOE_12_NAZJAR_TURN_5.prefab:e229c1ff3bc5b8c4dadb845e94ed0ffb");
		PreloadSound("VO_LOE_12_NAZJAR_TURN_5_FINLEY.prefab:4c4838aaf56c52e49a0ef57f02f02bb1");
		PreloadSound("VO_LOE_12_WIN.prefab:bc04587cf6d7a2049b8a2fcf262e3514");
		PreloadSound("VO_LOE_12_WIN_2.prefab:6d874c577c9cff8488bbd09a218351f1");
		PreloadSound("VO_LOE_12_NAZJAR_PEARL.prefab:04e77f4a01c1a7f46b2b82bf3b38a7c7");
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
						m_soundName = "VO_LOE_12_RESPONSE.prefab:3d2ddf8b667255e4fb92ea9d2cce0b42",
						m_stringTag = "VO_LOE_12_RESPONSE"
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
		string cardId = entity.GetCardId();
		if (cardId == "LOEA12_3" && !m_pearlLinePlayed)
		{
			m_pearlLinePlayed = true;
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_PEARL.prefab:04e77f4a01c1a7f46b2b82bf3b38a7c7"));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent == 1)
		{
			GameState.Get().GetFriendlySidePlayer().WipeZzzs();
			GameState.Get().GetOpposingSidePlayer().WipeZzzs();
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_12_NAZJAR_TURN_1.prefab:9dcfc1a022a6c0540bbc71fa387b47e7", Notification.SpeechBubbleDirection.TopRight, actor));
			yield return new WaitForSeconds(6.7f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_1_FINLEY.prefab:8016f993cefe7514690bb0a1f83e4d80"));
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_3_FINLEY.prefab:b471b5571b95207418831574c5f02285"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_NAZJAR_TURN_3_CARTOGRAPHER.prefab:26d95ed2b270ab546b6be7b791b10195"));
			break;
		case 9:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_12_NAZJAR_TURN_5.prefab:e229c1ff3bc5b8c4dadb845e94ed0ffb", Notification.SpeechBubbleDirection.TopRight, actor));
			yield return new WaitForSeconds(5.7f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_5_FINLEY.prefab:4c4838aaf56c52e49a0ef57f02f02bb1"));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOE_12_WIN.prefab:bc04587cf6d7a2049b8a2fcf262e3514", 0f, allowRepeatDuringSession: false));
		}
	}
}
