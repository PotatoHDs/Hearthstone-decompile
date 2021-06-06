using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037E RID: 894
public class BRM09_RendBlackhand : BRM_MissionEntity
{
	// Token: 0x0600343E RID: 13374 RVA: 0x0010B94C File Offset: 0x00109B4C
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA09_1_RESPONSE_04.prefab:425f0a827cd452642835f70c4ddaf74b");
		base.PreloadSound("VO_BRMA09_1_HERO_POWER1_06.prefab:fb6da306ffffaee43b86149480e916d1");
		base.PreloadSound("VO_BRMA09_1_HERO_POWER2_07.prefab:f59ec9255ae38a64c83c90b88b9b04ca");
		base.PreloadSound("VO_BRMA09_1_HERO_POWER3_08.prefab:213cfaf6d91a2ba42b087e59ed8099a4");
		base.PreloadSound("VO_BRMA09_1_HERO_POWER4_09.prefab:1068556d8d1e5694eb2d7925ec96957f");
		base.PreloadSound("VO_BRMA09_1_CARD_05.prefab:2976c75ead5ee7c409ff9ee6ce5bb77d");
		base.PreloadSound("VO_BRMA09_1_TURN1_03.prefab:72e081aa0659e0d49ad1b5dfa890f1c5");
		base.PreloadSound("VO_NEFARIAN_REND1_52.prefab:46edb7255109f2c44884b4871e0a4ede");
	}

	// Token: 0x0600343F RID: 13375 RVA: 0x0010B9B4 File Offset: 0x00109BB4
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
						m_soundName = "VO_BRMA09_1_RESPONSE_04.prefab:425f0a827cd452642835f70c4ddaf74b",
						m_stringTag = "VO_BRMA09_1_RESPONSE_04"
					}
				}
			}
		};
	}

	// Token: 0x06003440 RID: 13376 RVA: 0x0010BA13 File Offset: 0x00109C13
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
		if (num <= 1831217230U)
		{
			if (num <= 321481783U)
			{
				if (num != 152572760U)
				{
					if (num != 321481783U)
					{
						goto IL_318;
					}
					if (!(cardId == "BRMA09_2H"))
					{
						goto IL_318;
					}
				}
				else
				{
					if (!(cardId == "BRMA09_5H"))
					{
						goto IL_318;
					}
					goto IL_2A2;
				}
			}
			else if (num != 790269377U)
			{
				if (num != 1831217230U)
				{
					goto IL_318;
				}
				if (!(cardId == "BRMA09_3H"))
				{
					goto IL_318;
				}
				goto IL_227;
			}
			else
			{
				if (!(cardId == "BRMA09_4H"))
				{
					goto IL_318;
				}
				goto IL_266;
			}
		}
		else if (num <= 3914798995U)
		{
			if (num != 3898021376U)
			{
				if (num != 3914798995U)
				{
					goto IL_318;
				}
				if (!(cardId == "BRMA09_4"))
				{
					goto IL_318;
				}
				goto IL_266;
			}
			else
			{
				if (!(cardId == "BRMA09_5"))
				{
					goto IL_318;
				}
				goto IL_2A2;
			}
		}
		else if (num != 3948354233U)
		{
			if (num != 3998687090U)
			{
				if (num != 4015464709U)
				{
					goto IL_318;
				}
				if (!(cardId == "BRMA09_2"))
				{
					goto IL_318;
				}
			}
			else
			{
				if (!(cardId == "BRMA09_3"))
				{
					goto IL_318;
				}
				goto IL_227;
			}
		}
		else
		{
			if (!(cardId == "BRMA09_6"))
			{
				goto IL_318;
			}
			if (this.m_cardLinePlayed)
			{
				yield break;
			}
			this.m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_CARD_05.prefab:2976c75ead5ee7c409ff9ee6ce5bb77d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			goto IL_318;
		}
		if (this.m_heroPower1LinePlayed)
		{
			yield break;
		}
		this.m_heroPower1LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_HERO_POWER1_06.prefab:fb6da306ffffaee43b86149480e916d1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		goto IL_318;
		IL_227:
		if (this.m_heroPower2LinePlayed)
		{
			yield break;
		}
		this.m_heroPower2LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_HERO_POWER2_07.prefab:f59ec9255ae38a64c83c90b88b9b04ca", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		goto IL_318;
		IL_266:
		if (this.m_heroPower3LinePlayed)
		{
			yield break;
		}
		this.m_heroPower3LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_HERO_POWER3_08.prefab:213cfaf6d91a2ba42b087e59ed8099a4", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		goto IL_318;
		IL_2A2:
		if (this.m_heroPower4LinePlayed)
		{
			yield break;
		}
		this.m_heroPower4LinePlayed = true;
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_HERO_POWER4_09.prefab:1068556d8d1e5694eb2d7925ec96957f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		IL_318:
		yield break;
	}

	// Token: 0x06003441 RID: 13377 RVA: 0x0010BA29 File Offset: 0x00109C29
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 quotePos = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA09_1_TURN1_03.prefab:72e081aa0659e0d49ad1b5dfa890f1c5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_REND1_52"), "VO_NEFARIAN_REND1_52.prefab:46edb7255109f2c44884b4871e0a4ede", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		yield break;
	}

	// Token: 0x06003442 RID: 13378 RVA: 0x0010BA3F File Offset: 0x00109C3F
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_REND_DEAD_53"), "VO_NEFARIAN_REND_DEAD_53.prefab:a81636f855f8587428d4998d2b6847b8", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C91 RID: 7313
	private bool m_heroPower1LinePlayed;

	// Token: 0x04001C92 RID: 7314
	private bool m_heroPower2LinePlayed;

	// Token: 0x04001C93 RID: 7315
	private bool m_heroPower3LinePlayed;

	// Token: 0x04001C94 RID: 7316
	private bool m_heroPower4LinePlayed;

	// Token: 0x04001C95 RID: 7317
	private bool m_cardLinePlayed;
}
