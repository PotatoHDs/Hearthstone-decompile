using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000392 RID: 914
public class LOE12_Naga : LOE_MissionEntity
{
	// Token: 0x060034D4 RID: 13524 RVA: 0x0010D558 File Offset: 0x0010B758
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_12_RESPONSE.prefab:3d2ddf8b667255e4fb92ea9d2cce0b42");
		base.PreloadSound("VO_LOE_12_NAZJAR_TURN_1.prefab:9dcfc1a022a6c0540bbc71fa387b47e7");
		base.PreloadSound("VO_LOE_12_NAZJAR_TURN_1_FINLEY.prefab:8016f993cefe7514690bb0a1f83e4d80");
		base.PreloadSound("VO_LOE_12_NAZJAR_TURN_3_FINLEY.prefab:b471b5571b95207418831574c5f02285");
		base.PreloadSound("VO_LOE_NAZJAR_TURN_3_CARTOGRAPHER.prefab:26d95ed2b270ab546b6be7b791b10195");
		base.PreloadSound("VO_LOE_12_NAZJAR_TURN_5.prefab:e229c1ff3bc5b8c4dadb845e94ed0ffb");
		base.PreloadSound("VO_LOE_12_NAZJAR_TURN_5_FINLEY.prefab:4c4838aaf56c52e49a0ef57f02f02bb1");
		base.PreloadSound("VO_LOE_12_WIN.prefab:bc04587cf6d7a2049b8a2fcf262e3514");
		base.PreloadSound("VO_LOE_12_WIN_2.prefab:6d874c577c9cff8488bbd09a218351f1");
		base.PreloadSound("VO_LOE_12_NAZJAR_PEARL.prefab:04e77f4a01c1a7f46b2b82bf3b38a7c7");
	}

	// Token: 0x060034D5 RID: 13525 RVA: 0x0010D5D4 File Offset: 0x0010B7D4
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
						m_soundName = "VO_LOE_12_RESPONSE.prefab:3d2ddf8b667255e4fb92ea9d2cce0b42",
						m_stringTag = "VO_LOE_12_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x060034D6 RID: 13526 RVA: 0x0010D633 File Offset: 0x0010B833
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		string cardId = entity.GetCardId();
		if (cardId == "LOEA12_3")
		{
			if (this.m_pearlLinePlayed)
			{
				yield break;
			}
			this.m_pearlLinePlayed = true;
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_PEARL.prefab:04e77f4a01c1a7f46b2b82bf3b38a7c7", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034D7 RID: 13527 RVA: 0x0010D649 File Offset: 0x0010B849
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent == 1)
		{
			GameState.Get().GetFriendlySidePlayer().WipeZzzs();
			GameState.Get().GetOpposingSidePlayer().WipeZzzs();
		}
		yield break;
	}

	// Token: 0x060034D8 RID: 13528 RVA: 0x0010D65F File Offset: 0x0010B85F
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn != 5)
			{
				if (turn == 9)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_12_NAZJAR_TURN_5.prefab:e229c1ff3bc5b8c4dadb845e94ed0ffb", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					yield return new WaitForSeconds(5.7f);
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_5_FINLEY.prefab:4c4838aaf56c52e49a0ef57f02f02bb1", 3f, 1f, false, false));
				}
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_3_FINLEY.prefab:b471b5571b95207418831574c5f02285", 3f, 1f, false, false));
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_NAZJAR_TURN_3_CARTOGRAPHER.prefab:26d95ed2b270ab546b6be7b791b10195", 3f, 1f, false, false));
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_12_NAZJAR_TURN_1.prefab:9dcfc1a022a6c0540bbc71fa387b47e7", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(6.7f);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_12_NAZJAR_TURN_1_FINLEY.prefab:8016f993cefe7514690bb0a1f83e4d80", 3f, 1f, false, false));
		}
		yield break;
	}

	// Token: 0x060034D9 RID: 13529 RVA: 0x0010D675 File Offset: 0x0010B875
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOE_12_WIN.prefab:bc04587cf6d7a2049b8a2fcf262e3514", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CCD RID: 7373
	private bool m_pearlLinePlayed;
}
