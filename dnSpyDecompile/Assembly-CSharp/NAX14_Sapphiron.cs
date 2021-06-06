using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000590 RID: 1424
public class NAX14_Sapphiron : NAX_MissionEntity
{
	// Token: 0x06004F20 RID: 20256 RVA: 0x0019FAFC File Offset: 0x0019DCFC
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX14_01_EMOTE_01.prefab:8e779dac945dafe4cae350690bfe5adb");
		base.PreloadSound("VO_NAX14_01_EMOTE_02.prefab:2062a07b94954c44aab47bc4edc1c307");
		base.PreloadSound("VO_NAX14_01_EMOTE_03.prefab:225909a5368dd56489a130eb22afbd19");
		base.PreloadSound("VO_NAX14_01_CARD_01.prefab:349cacac917de9f49ad5a75f1352e53c");
		base.PreloadSound("VO_NAX14_01_HP_01.prefab:641e44497675c5b4497a0be59dcec408");
		base.PreloadSound("VO_KT_SAPPHIRON2_84.prefab:c2f8a9371bca45441b0d069994e2fc96");
		base.PreloadSound("VO_KT_SAPPHIRON3_85.prefab:0d71aa21bf415bb448a90c1d87f73a82");
		base.PreloadSound("VO_KT_SAPPHIRON4_ALT_87.prefab:58b23de92708c8146b0f278db588c7d7");
		base.PreloadSound("VO_KT_SAPPHIRON5_88.prefab:92e68b9f36ba81e4d94e20135a842f0d");
	}

	// Token: 0x06004F21 RID: 20257 RVA: 0x0019FB6C File Offset: 0x0019DD6C
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
						m_soundName = "VO_NAX14_01_EMOTE_01.prefab:8e779dac945dafe4cae350690bfe5adb",
						m_stringTag = "VO_NAX14_01_EMOTE_01"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX14_01_EMOTE_02.prefab:2062a07b94954c44aab47bc4edc1c307",
						m_stringTag = "VO_NAX14_01_EMOTE_02"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX14_01_EMOTE_03.prefab:225909a5368dd56489a130eb22afbd19",
						m_stringTag = "VO_NAX14_01_EMOTE_03"
					}
				}
			}
		};
	}

	// Token: 0x06004F22 RID: 20258 RVA: 0x0019FC0D File Offset: 0x0019DE0D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1 && GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId() != "NAX14_01H")
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON2_84", "VO_KT_SAPPHIRON2_84.prefab:c2f8a9371bca45441b0d069994e2fc96", false);
		}
		yield break;
	}

	// Token: 0x06004F23 RID: 20259 RVA: 0x0019FC1C File Offset: 0x0019DE1C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON5_88", "VO_KT_SAPPHIRON5_88.prefab:92e68b9f36ba81e4d94e20135a842f0d", true);
		}
		yield break;
	}

	// Token: 0x06004F24 RID: 20260 RVA: 0x0019FC2B File Offset: 0x0019DE2B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent == 1)
		{
			this.m_numTimesFrostBreathMisses++;
			if (this.m_numTimesFrostBreathMisses == 4)
			{
				NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON3_85", "VO_KT_SAPPHIRON3_85.prefab:0d71aa21bf415bb448a90c1d87f73a82", true);
			}
		}
		yield break;
	}

	// Token: 0x06004F25 RID: 20261 RVA: 0x0019FC41 File Offset: 0x0019DE41
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "NAX14_02"))
		{
			if (cardId == "NAX14_04")
			{
				yield return new WaitForSeconds(1f);
				if (this.m_cardKtLinePlayed)
				{
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX14_01_CARD_01.prefab:349cacac917de9f49ad5a75f1352e53c", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
				}
				else
				{
					NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON4_ALT_87", "VO_KT_SAPPHIRON4_ALT_87.prefab:58b23de92708c8146b0f278db588c7d7", false);
					this.m_cardKtLinePlayed = true;
				}
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX14_01_HP_01.prefab:641e44497675c5b4497a0be59dcec408", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0400454C RID: 17740
	private bool m_cardKtLinePlayed;

	// Token: 0x0400454D RID: 17741
	private int m_numTimesFrostBreathMisses;
}
