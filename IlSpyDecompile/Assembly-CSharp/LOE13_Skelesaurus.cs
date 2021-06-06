using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE13_Skelesaurus : LOE_MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_13_TURN_1.prefab:14b44906b04430b4fb02ce52d956b953");
		PreloadSound("VO_LOE_13_TURN_1_2.prefab:7928a1ea0e6cfa14a8d02b93d859a827");
		PreloadSound("VO_LOE_13_TURN_5.prefab:09be25937885f60418b753ca91e245f1");
		PreloadSound("VO_LOE_13_TURN_5_2.prefab:73f05bc4602a46143a84119e9dc15ab6");
		PreloadSound("VO_LOE_13_TURN_9.prefab:1b65a335ab100884693b894b5bf292c5");
		PreloadSound("VO_LOE_13_TURN_9_2.prefab:3238c0c26db6245419bb557c38d94c7d");
		PreloadSound("VO_LOE_13_WIN.prefab:8cd30118968122a44898b30bd97e339c");
		PreloadSound("LOEA13_1_SkelesaurusHex_EmoteResponse.prefab:8a5faeba8169fe747a202bcbb54abbde");
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
						m_soundName = "LOEA13_1_SkelesaurusHex_EmoteResponse.prefab:8a5faeba8169fe747a202bcbb54abbde",
						m_stringTag = "VO_LOE_13_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_1.prefab:14b44906b04430b4fb02ce52d956b953"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_13_TURN_1_2.prefab:7928a1ea0e6cfa14a8d02b93d859a827"));
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_13_TURN_5.prefab:09be25937885f60418b753ca91e245f1"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_5_2.prefab:73f05bc4602a46143a84119e9dc15ab6"));
			break;
		case 9:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_13_TURN_9.prefab:1b65a335ab100884693b894b5bf292c5"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_9_2.prefab:3238c0c26db6245419bb557c38d94c7d"));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537", "VO_LOE_13_WIN.prefab:8cd30118968122a44898b30bd97e339c", 0f, allowRepeatDuringSession: false));
		}
	}
}
