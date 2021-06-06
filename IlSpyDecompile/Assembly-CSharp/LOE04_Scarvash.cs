using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE04_Scarvash : LOE_MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("VO_LOEA04_BRANN_TURN_1.prefab:283cdd2a120367449ba678f0f4c113c9");
		PreloadSound("VO_LOE_04_SCARVASH_TURN_2.prefab:7b41ccd94c37b9947884fc622314d810");
		PreloadSound("VO_BRANN_MITHRIL_ALT_02.prefab:3971b9c4fc9a9984c98c90c7171529e1");
		PreloadSound("VO_LOE_SCARVASH_TURN_6_CARTOGRAPHER.prefab:46c51c07866cd2d4b9edf5d171e6f169");
		PreloadSound("VO_LOE_04_RESPONSE.prefab:24f04550b6de6324896847e3feeadb0f");
		PreloadSound("VO_LOE_04_WIN.prefab:2e63e9ce35e90d74d8eef59dfc5735a4");
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
						m_soundName = "VO_LOE_04_RESPONSE.prefab:24f04550b6de6324896847e3feeadb0f",
						m_stringTag = "VO_LOE_04_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA04_BRANN_TURN_1.prefab:283cdd2a120367449ba678f0f4c113c9"));
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_04_SCARVASH_TURN_2.prefab:7b41ccd94c37b9947884fc622314d810", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			break;
		case 7:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_BRANN_MITHRIL_ALT_02.prefab:3971b9c4fc9a9984c98c90c7171529e1"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_SCARVASH_TURN_6_CARTOGRAPHER.prefab:46c51c07866cd2d4b9edf5d171e6f169"));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_LOE_04_WIN.prefab:2e63e9ce35e90d74d8eef59dfc5735a4", 0f, allowRepeatDuringSession: false));
		}
	}
}
