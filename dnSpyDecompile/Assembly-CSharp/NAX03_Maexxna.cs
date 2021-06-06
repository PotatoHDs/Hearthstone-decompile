using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000585 RID: 1413
public class NAX03_Maexxna : NAX_MissionEntity
{
	// Token: 0x06004EDF RID: 20191 RVA: 0x0019F094 File Offset: 0x0019D294
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX3_01_EMOTE_01.prefab:a31d7a46b65b449479a2a531268f9299");
		base.PreloadSound("VO_NAX3_01_EMOTE_02.prefab:c1e3c5438c9b2a1469badb6ac78ed010");
		base.PreloadSound("VO_NAX3_01_EMOTE_03.prefab:2d1d268f8503f0d4090ac41624711149");
		base.PreloadSound("VO_NAX3_01_EMOTE_04.prefab:8163100101e44994680768b0f4220eec");
		base.PreloadSound("VO_NAX3_01_EMOTE_05.prefab:0fc3e310650ab7149b5bf2b794895869");
		base.PreloadSound("VO_NAX3_01_CARD_01.prefab:8f9084036cb9a31429b886ee01cc9bad");
		base.PreloadSound("VO_NAX3_01_HP_01.prefab:428343bce2fa95c42837e3f7fd220634");
		base.PreloadSound("VO_KT_MAEXXNA2_47.prefab:ba0b856a1b49d4249b511c6d2a7e5a66");
		base.PreloadSound("VO_KT_MAEXXNA6_51.prefab:105796bfd5d566249beef8e4c8672ee3");
		base.PreloadSound("VO_KT_MAEXXNA3_48.prefab:3bd05eb0fda073245bf190fc623a148c");
	}

	// Token: 0x06004EE0 RID: 20192 RVA: 0x0019F110 File Offset: 0x0019D310
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
						m_soundName = "VO_NAX3_01_EMOTE_01.prefab:a31d7a46b65b449479a2a531268f9299",
						m_stringTag = "VO_NAX3_01_EMOTE_01"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_02.prefab:c1e3c5438c9b2a1469badb6ac78ed010",
						m_stringTag = "VO_NAX3_01_EMOTE_02"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_03.prefab:2d1d268f8503f0d4090ac41624711149",
						m_stringTag = "VO_NAX3_01_EMOTE_03"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_04.prefab:8163100101e44994680768b0f4220eec",
						m_stringTag = "VO_NAX3_01_EMOTE_04"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_05.prefab:0fc3e310650ab7149b5bf2b794895869",
						m_stringTag = "VO_NAX3_01_EMOTE_05"
					}
				}
			}
		};
	}

	// Token: 0x06004EE1 RID: 20193 RVA: 0x0019F1F3 File Offset: 0x0019D3F3
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA2_47", "VO_KT_MAEXXNA2_47.prefab:ba0b856a1b49d4249b511c6d2a7e5a66", false);
		}
		yield break;
	}

	// Token: 0x06004EE2 RID: 20194 RVA: 0x0019F202 File Offset: 0x0019D402
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA4_49", "VO_KT_MAEXXNA4_49.prefab:449ab7cb30688e344896e51a9fc4dfd1", true);
		}
		yield break;
	}

	// Token: 0x06004EE3 RID: 20195 RVA: 0x0019F211 File Offset: 0x0019D411
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "NAX3_02"))
		{
			if (!(cardId == "NAX3_03"))
			{
				if (cardId == "EX1_586")
				{
					if (this.m_seaGiantLinePlayed)
					{
						yield break;
					}
					this.m_seaGiantLinePlayed = true;
					yield return new WaitForSeconds(1f);
					while (NotificationManager.Get().IsQuotePlaying)
					{
						yield return 0;
					}
					NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA3_48", "VO_KT_MAEXXNA3_48.prefab:3bd05eb0fda073245bf190fc623a148c", false);
				}
			}
			else
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX3_01_CARD_01.prefab:8f9084036cb9a31429b886ee01cc9bad", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX3_01_HP_01.prefab:428343bce2fa95c42837e3f7fd220634", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			while (this.m_enemySpeaking || NotificationManager.Get().IsQuotePlaying)
			{
				yield return 0;
			}
			NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA6_51", "VO_KT_MAEXXNA6_51.prefab:105796bfd5d566249beef8e4c8672ee3", false);
		}
		yield break;
	}

	// Token: 0x04004536 RID: 17718
	private bool m_heroPowerLinePlayed;

	// Token: 0x04004537 RID: 17719
	private bool m_seaGiantLinePlayed;
}
