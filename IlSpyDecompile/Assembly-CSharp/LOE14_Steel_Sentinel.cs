using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE14_Steel_Sentinel : LOE_MissionEntity
{
	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_14_START.prefab:0c76369f23915fd4897d9aecf53de768");
		PreloadSound("VO_LOE_14_TURN_5.prefab:878704acc2ab01c419de69caa7003a51");
		PreloadSound("VO_LOE_14_TURN_5_2.prefab:9a031cba8a034dd4681717165ff5bfb1");
		PreloadSound("VO_LOE_14_TURN_9.prefab:e729be5d0472cd54faaabb324d197263");
		PreloadSound("VO_LOE_14_TURN_13.prefab:8b8685be9e263a84aa5b477f69e0dd82");
		PreloadSound("VO_LOE_14_WIN.prefab:5feb008ac9f05354991afc1a0308cf03");
		PreloadSound("LOEA14_1_SteelSentinel_Response.prefab:f18eaf8aeb86dd047a849548e80cbd39");
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
						m_soundName = "LOEA14_1_SteelSentinel_Response.prefab:f18eaf8aeb86dd047a849548e80cbd39",
						m_stringTag = "VO_LOE_14_RESPONSE"
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
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_14_START.prefab:0c76369f23915fd4897d9aecf53de768"));
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_14_TURN_5.prefab:878704acc2ab01c419de69caa7003a51"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_14_TURN_5_2.prefab:9a031cba8a034dd4681717165ff5bfb1"));
			break;
		case 9:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_14_TURN_9.prefab:e729be5d0472cd54faaabb324d197263"));
			break;
		case 13:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_14_TURN_13.prefab:8b8685be9e263a84aa5b477f69e0dd82"));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537", "VO_LOE_14_WIN.prefab:5feb008ac9f05354991afc1a0308cf03", 0f, allowRepeatDuringSession: false));
		}
	}
}
