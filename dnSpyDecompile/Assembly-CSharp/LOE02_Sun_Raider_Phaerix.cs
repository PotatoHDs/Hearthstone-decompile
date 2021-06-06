using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200038B RID: 907
public class LOE02_Sun_Raider_Phaerix : LOE_MissionEntity
{
	// Token: 0x06003488 RID: 13448 RVA: 0x0010C734 File Offset: 0x0010A934
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_LOE_01_RESPONSE.prefab:003ddb96a133c634b8f74c8a9ef1e55c");
		base.PreloadSound("VO_LOE_01_WOUNDED.prefab:0fb9c01bbbbacd0408fb478d13b9574b");
		base.PreloadSound("VO_LOE_01_STAFF.prefab:b412b19ab6e0def45a74aeb7ebb60ec1");
		base.PreloadSound("VO_LOE_01_STAFF_2.prefab:587ffb164487ac0429b1dac0ca33b9aa");
		base.PreloadSound("VO_LOE_02_PHAERIX_STAFF_RECOVER.prefab:bdbf17959a28fa247976168e5d545f5d");
		base.PreloadSound("VO_LOE_01_STAFF_2_RENO.prefab:81e1aae8f257ed448bcdbd89ea881fc5");
		base.PreloadSound("VO_LOE_01_WIN_2.prefab:e8acaf5e90b7f7a419739aba63b6f8bc");
		base.PreloadSound("VO_LOE_01_WIN_2_ALT_2.prefab:4d3c656d8071a8e438cd2fc2f4d5b862");
		base.PreloadSound("VO_LOE_01_START.prefab:e4840e7e825b2aa438f783b50bb19584");
		base.PreloadSound("VO_LOE_01_WIN.prefab:ece5a717571dc164db373500aed7a707");
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x0010C7B0 File Offset: 0x0010A9B0
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
						m_soundName = "VO_LOE_01_RESPONSE.prefab:003ddb96a133c634b8f74c8a9ef1e55c",
						m_stringTag = "VO_LOE_01_RESPONSE"
					}
				}
			}
		};
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x0010C80F File Offset: 0x0010AA0F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (this.m_staffLinesPlayed < missionEvent)
		{
			if (missionEvent > 9)
			{
				if (!this.m_damageLinePlayed)
				{
					this.m_damageLinePlayed = true;
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_WOUNDED.prefab:0fb9c01bbbbacd0408fb478d13b9574b", 3f, 1f, false, false));
				}
			}
			else
			{
				this.m_staffLinesPlayed = missionEvent;
				switch (missionEvent)
				{
				case 1:
					GameState.Get().SetBusy(true);
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_STAFF.prefab:b412b19ab6e0def45a74aeb7ebb60ec1", 3f, 1f, false, false));
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_01_STAFF_2.prefab:587ffb164487ac0429b1dac0ca33b9aa", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
					break;
				case 2:
					GameState.Get().SetBusy(true);
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce("VO_LOE_02_PHAERIX_STAFF_RECOVER.prefab:bdbf17959a28fa247976168e5d545f5d", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
					GameState.Get().SetBusy(false);
					break;
				case 3:
					GameState.Get().SetBusy(true);
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_STAFF_2_RENO.prefab:81e1aae8f257ed448bcdbd89ea881fc5", 3f, 1f, false, false));
					GameState.Get().SetBusy(false);
					break;
				}
			}
		}
		yield break;
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x0010C825 File Offset: 0x0010AA25
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_LOE_01_START.prefab:e4840e7e825b2aa438f783b50bb19584", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			yield return new WaitForSeconds(4f);
			yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_01_WIN_2.prefab:e8acaf5e90b7f7a419739aba63b6f8bc", 3f, 1f, true, false));
		}
		yield break;
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x0010C83B File Offset: 0x0010AA3B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_01_WIN.prefab:ece5a717571dc164db373500aed7a707", 0f, false, false));
			yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_01_WIN_2_ALT_2.prefab:4d3c656d8071a8e438cd2fc2f4d5b862", 0f, false, false));
		}
		yield break;
	}

	// Token: 0x04001CB9 RID: 7353
	private int m_staffLinesPlayed;

	// Token: 0x04001CBA RID: 7354
	private bool m_damageLinePlayed;
}
