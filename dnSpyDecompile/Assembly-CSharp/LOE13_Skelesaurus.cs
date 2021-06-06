using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000393 RID: 915
public class LOE13_Skelesaurus : LOE_MissionEntity
{
	// Token: 0x060034DC RID: 13532 RVA: 0x0010D68C File Offset: 0x0010B88C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_13_TURN_1.prefab:14b44906b04430b4fb02ce52d956b953");
		base.PreloadSound("VO_LOE_13_TURN_1_2.prefab:7928a1ea0e6cfa14a8d02b93d859a827");
		base.PreloadSound("VO_LOE_13_TURN_5.prefab:09be25937885f60418b753ca91e245f1");
		base.PreloadSound("VO_LOE_13_TURN_5_2.prefab:73f05bc4602a46143a84119e9dc15ab6");
		base.PreloadSound("VO_LOE_13_TURN_9.prefab:1b65a335ab100884693b894b5bf292c5");
		base.PreloadSound("VO_LOE_13_TURN_9_2.prefab:3238c0c26db6245419bb557c38d94c7d");
		base.PreloadSound("VO_LOE_13_WIN.prefab:8cd30118968122a44898b30bd97e339c");
		base.PreloadSound("LOEA13_1_SkelesaurusHex_EmoteResponse.prefab:8a5faeba8169fe747a202bcbb54abbde");
	}

	// Token: 0x060034DD RID: 13533 RVA: 0x0010D6F4 File Offset: 0x0010B8F4
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "LOEA13_1_SkelesaurusHex_EmoteResponse.prefab:8a5faeba8169fe747a202bcbb54abbde",
						m_stringTag = "VO_LOE_13_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034DE RID: 13534 RVA: 0x0010D753 File Offset: 0x0010B953
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 9)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_13_TURN_9.prefab:1b65a335ab100884693b894b5bf292c5", 3f, 1f, false, false));
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_9_2.prefab:3238c0c26db6245419bb557c38d94c7d", 3f, 1f, false, false));
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_13_TURN_5.prefab:09be25937885f60418b753ca91e245f1", 3f, 1f, false, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_5_2.prefab:73f05bc4602a46143a84119e9dc15ab6", 3f, 1f, false, false));
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_13_TURN_1.prefab:14b44906b04430b4fb02ce52d956b953", 3f, 1f, false, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_13_TURN_1_2.prefab:7928a1ea0e6cfa14a8d02b93d859a827", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034DF RID: 13535 RVA: 0x0010D769 File Offset: 0x0010B969
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537", "VO_LOE_13_WIN.prefab:8cd30118968122a44898b30bd97e339c", 0f, false, false));
		}
		yield break;
	}
}
