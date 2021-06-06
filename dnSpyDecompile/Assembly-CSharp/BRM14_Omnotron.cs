using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000383 RID: 899
public class BRM14_Omnotron : BRM_MissionEntity
{
	// Token: 0x0600345E RID: 13406 RVA: 0x0010BEE4 File Offset: 0x0010A0E4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA14_1_RESPONSE1_10.prefab:6f97c57440f1e5a4b8bac49eb39f97a8");
		base.PreloadSound("VO_BRMA14_1_RESPONSE2_11.prefab:c213dd29b35274b41b5c4c982171be04");
		base.PreloadSound("VO_BRMA14_1_RESPONSE3_12.prefab:a4c9a33c03b2cf143baae5829af69701");
		base.PreloadSound("VO_BRMA14_1_RESPONSE4_13.prefab:1807877b8c9538246a11d69fa003e369");
		base.PreloadSound("VO_BRMA14_1_RESPONSE5_14.prefab:23cb6b0e25ba78345a67a15416600774");
		base.PreloadSound("VO_BRMA14_1_HP1_03.prefab:49a86de40b9af024ea935f62b98017b3");
		base.PreloadSound("VO_BRMA14_1_HP2_04.prefab:a3d3b1b54ba372a49b3320f600da1b0f");
		base.PreloadSound("VO_BRMA14_1_HP3_05.prefab:0f20a4c1fdb6d9143b9d10b617c8c901");
		base.PreloadSound("VO_BRMA14_1_HP4_06.prefab:481cbb26507bdf24eba7d240ae131496");
		base.PreloadSound("VO_BRMA14_1_HP5_07.prefab:99da345a6ef8bbb46b9ebf4e729b34c8");
		base.PreloadSound("VO_BRMA14_1_CARD_09.prefab:40a09cf3c2c8a334da8195687dfa6dc1");
		base.PreloadSound("VO_BRMA14_1_TURN1_02.prefab:32791f2d54bc6c4478f8d9f28720da20");
	}

	// Token: 0x0600345F RID: 13407 RVA: 0x0010BF78 File Offset: 0x0010A178
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
						m_soundName = "VO_BRMA14_1_RESPONSE1_10.prefab:6f97c57440f1e5a4b8bac49eb39f97a8",
						m_stringTag = "VO_BRMA14_1_RESPONSE1_10"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BRMA14_1_RESPONSE2_11.prefab:c213dd29b35274b41b5c4c982171be04",
						m_stringTag = "VO_BRMA14_1_RESPONSE2_11"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BRMA14_1_RESPONSE3_12.prefab:a4c9a33c03b2cf143baae5829af69701",
						m_stringTag = "VO_BRMA14_1_RESPONSE3_12"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BRMA14_1_RESPONSE4_13.prefab:1807877b8c9538246a11d69fa003e369",
						m_stringTag = "VO_BRMA14_1_RESPONSE4_13"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_BRMA14_1_RESPONSE5_14.prefab:23cb6b0e25ba78345a67a15416600774",
						m_stringTag = "VO_BRMA14_1_RESPONSE5_14"
					}
				}
			}
		};
	}

	// Token: 0x06003460 RID: 13408 RVA: 0x0010C05B File Offset: 0x0010A25B
	protected override void CycleNextResponseGroupIndex(MissionEntity.EmoteResponseGroup responseGroup)
	{
		if (responseGroup.m_responseIndex == responseGroup.m_responses.Count - 1)
		{
			return;
		}
		responseGroup.m_responseIndex++;
	}

	// Token: 0x06003461 RID: 13409 RVA: 0x0010C081 File Offset: 0x0010A281
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
		uint num = <PrivateImplementationDetails>.ComputeStringHash(cardId);
		if (num <= 2144132647U)
		{
			if (num <= 1020320431U)
			{
				if (num != 953209955U)
				{
					if (num != 1020320431U)
					{
						goto IL_3A4;
					}
					if (!(cardId == "BRMA14_6"))
					{
						goto IL_3A4;
					}
				}
				else
				{
					if (!(cardId == "BRMA14_2"))
					{
						goto IL_3A4;
					}
					goto IL_2B3;
				}
			}
			else
			{
				if (num != 1053875669U)
				{
					if (num != 1120986145U)
					{
						if (num != 2144132647U)
						{
							goto IL_3A4;
						}
						if (!(cardId == "BRMA14_4H"))
						{
							goto IL_3A4;
						}
					}
					else
					{
						if (!(cardId == "BRMA14_8"))
						{
							goto IL_3A4;
						}
						goto IL_2F2;
					}
				}
				else if (!(cardId == "BRMA14_4"))
				{
					goto IL_3A4;
				}
				if (this.m_heroPower2LinePlayed)
				{
					yield break;
				}
				this.m_heroPower2LinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_HP2_04.prefab:a3d3b1b54ba372a49b3320f600da1b0f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				goto IL_3A4;
			}
		}
		else
		{
			if (num <= 2612920241U)
			{
				if (num != 2484370318U)
				{
					if (num != 2547487051U)
					{
						if (num != 2612920241U)
						{
							goto IL_3A4;
						}
						if (!(cardId == "BRMA14_2H"))
						{
							goto IL_3A4;
						}
						goto IL_2B3;
					}
					else
					{
						if (!(cardId == "BRMA14_8H"))
						{
							goto IL_3A4;
						}
						goto IL_2F2;
					}
				}
				else if (!(cardId == "BRMA14_10H"))
				{
					goto IL_3A4;
				}
			}
			else if (num != 2747832741U)
			{
				if (num != 3384837810U)
				{
					if (num != 3401615429U)
					{
						goto IL_3A4;
					}
					if (!(cardId == "BRMA14_11"))
					{
						goto IL_3A4;
					}
					if (this.m_cardLinePlayed)
					{
						yield break;
					}
					this.m_cardLinePlayed = true;
					Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_CARD_09.prefab:40a09cf3c2c8a334da8195687dfa6dc1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
					goto IL_3A4;
				}
				else if (!(cardId == "BRMA14_10"))
				{
					goto IL_3A4;
				}
			}
			else
			{
				if (!(cardId == "BRMA14_6H"))
				{
					goto IL_3A4;
				}
				goto IL_235;
			}
			if (this.m_heroPower5LinePlayed)
			{
				yield break;
			}
			this.m_heroPower5LinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_HP5_07.prefab:99da345a6ef8bbb46b9ebf4e729b34c8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			goto IL_3A4;
		}
		IL_235:
		if (this.m_heroPower1LinePlayed)
		{
			yield break;
		}
		this.m_heroPower1LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_HP1_03.prefab:49a86de40b9af024ea935f62b98017b3", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		goto IL_3A4;
		IL_2B3:
		if (this.m_heroPower3LinePlayed)
		{
			yield break;
		}
		this.m_heroPower3LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_HP3_05.prefab:0f20a4c1fdb6d9143b9d10b617c8c901", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		goto IL_3A4;
		IL_2F2:
		if (this.m_heroPower4LinePlayed)
		{
			yield break;
		}
		this.m_heroPower4LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_HP4_06.prefab:481cbb26507bdf24eba7d240ae131496", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		IL_3A4:
		yield break;
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x0010C097 File Offset: 0x0010A297
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA14_1_TURN1_02.prefab:32791f2d54bc6c4478f8d9f28720da20", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x0010C0AD File Offset: 0x0010A2AD
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", GameStrings.Get("VO_NEFARIAN_OMNOTRON_DEAD_69"), "VO_NEFARIAN_OMNOTRON_DEAD_69.prefab:85ce5224ca43db3409797b209723d087", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C9E RID: 7326
	private bool m_heroPower1LinePlayed;

	// Token: 0x04001C9F RID: 7327
	private bool m_heroPower2LinePlayed;

	// Token: 0x04001CA0 RID: 7328
	private bool m_heroPower3LinePlayed;

	// Token: 0x04001CA1 RID: 7329
	private bool m_heroPower4LinePlayed;

	// Token: 0x04001CA2 RID: 7330
	private bool m_heroPower5LinePlayed;

	// Token: 0x04001CA3 RID: 7331
	private bool m_cardLinePlayed;
}
