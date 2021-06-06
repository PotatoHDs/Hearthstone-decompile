using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038D RID: 909
public class LOE04_Scarvash : LOE_MissionEntity
{
	// Token: 0x060034A3 RID: 13475 RVA: 0x0010CD0C File Offset: 0x0010AF0C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOEA04_BRANN_TURN_1.prefab:283cdd2a120367449ba678f0f4c113c9");
		base.PreloadSound("VO_LOE_04_SCARVASH_TURN_2.prefab:7b41ccd94c37b9947884fc622314d810");
		base.PreloadSound("VO_BRANN_MITHRIL_ALT_02.prefab:3971b9c4fc9a9984c98c90c7171529e1");
		base.PreloadSound("VO_LOE_SCARVASH_TURN_6_CARTOGRAPHER.prefab:46c51c07866cd2d4b9edf5d171e6f169");
		base.PreloadSound("VO_LOE_04_RESPONSE.prefab:24f04550b6de6324896847e3feeadb0f");
		base.PreloadSound("VO_LOE_04_WIN.prefab:2e63e9ce35e90d74d8eef59dfc5735a4");
	}

	// Token: 0x060034A4 RID: 13476 RVA: 0x0010CD5C File Offset: 0x0010AF5C
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
						m_soundName = "VO_LOE_04_RESPONSE.prefab:24f04550b6de6324896847e3feeadb0f",
						m_stringTag = "VO_LOE_04_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034A5 RID: 13477 RVA: 0x0010CDBB File Offset: 0x0010AFBB
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn == 7)
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_BRANN_MITHRIL_ALT_02.prefab:3971b9c4fc9a9984c98c90c7171529e1", 3f, 1f, false, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_SCARVASH_TURN_6_CARTOGRAPHER.prefab:46c51c07866cd2d4b9edf5d171e6f169", 3f, 1f, false, false));
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA04_BRANN_TURN_1.prefab:283cdd2a120367449ba678f0f4c113c9", 3f, 1f, false, false));
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_04_SCARVASH_TURN_2.prefab:7b41ccd94c37b9947884fc622314d810", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x060034A6 RID: 13478 RVA: 0x0010CDD1 File Offset: 0x0010AFD1
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_LOE_04_WIN.prefab:2e63e9ce35e90d74d8eef59dfc5735a4", 0f, false, false));
		}
		yield break;
	}
}
