using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000394 RID: 916
public class LOE14_Steel_Sentinel : LOE_MissionEntity
{
	// Token: 0x060034E1 RID: 13537 RVA: 0x0010D780 File Offset: 0x0010B980
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_14_START.prefab:0c76369f23915fd4897d9aecf53de768");
		base.PreloadSound("VO_LOE_14_TURN_5.prefab:878704acc2ab01c419de69caa7003a51");
		base.PreloadSound("VO_LOE_14_TURN_5_2.prefab:9a031cba8a034dd4681717165ff5bfb1");
		base.PreloadSound("VO_LOE_14_TURN_9.prefab:e729be5d0472cd54faaabb324d197263");
		base.PreloadSound("VO_LOE_14_TURN_13.prefab:8b8685be9e263a84aa5b477f69e0dd82");
		base.PreloadSound("VO_LOE_14_WIN.prefab:5feb008ac9f05354991afc1a0308cf03");
		base.PreloadSound("LOEA14_1_SteelSentinel_Response.prefab:f18eaf8aeb86dd047a849548e80cbd39");
	}

	// Token: 0x060034E2 RID: 13538 RVA: 0x0010D7DC File Offset: 0x0010B9DC
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
						m_soundName = "LOEA14_1_SteelSentinel_Response.prefab:f18eaf8aeb86dd047a849548e80cbd39",
						m_stringTag = "VO_LOE_14_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034E3 RID: 13539 RVA: 0x0010D83B File Offset: 0x0010BA3B
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn <= 5)
		{
			if (turn != 1)
			{
				if (turn == 5)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_14_TURN_5.prefab:878704acc2ab01c419de69caa7003a51", 3f, 1f, false, false));
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_14_TURN_5_2.prefab:9a031cba8a034dd4681717165ff5bfb1", 3f, 1f, false, false));
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOE_14_START.prefab:0c76369f23915fd4897d9aecf53de768", 3f, 1f, false, false));
			}
		}
		else if (turn != 9)
		{
			if (turn == 13)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_14_TURN_13.prefab:8b8685be9e263a84aa5b477f69e0dd82", 3f, 1f, false, false));
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Rafaam_wrap_BigQuote.prefab:ee7dbbb027adc1947b64b05f31d4c124", "VO_LOE_14_TURN_9.prefab:e729be5d0472cd54faaabb324d197263", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034E4 RID: 13540 RVA: 0x0010D851 File Offset: 0x0010BA51
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Cartographer_Quote.prefab:c6056bfb8c0025a458553adabc8ed537", "VO_LOE_14_WIN.prefab:5feb008ac9f05354991afc1a0308cf03", 0f, false, false));
		}
		yield break;
	}
}
