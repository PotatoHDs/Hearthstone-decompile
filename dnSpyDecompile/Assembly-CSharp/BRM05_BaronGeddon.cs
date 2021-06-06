using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037A RID: 890
public class BRM05_BaronGeddon : BRM_MissionEntity
{
	// Token: 0x06003425 RID: 13349 RVA: 0x0010B53A File Offset: 0x0010973A
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA05_1_RESPONSE_03.prefab:beac5b0620de49f42a2f2a66a906d4d6");
		base.PreloadSound("VO_BRMA05_1_HERO_POWER_06.prefab:2792e43708ba1df48baa3a41d636097a");
		base.PreloadSound("VO_BRMA05_1_CARD_05.prefab:c0bc2f9cc3d3ae047ba80ffa0f70dcb8");
		base.PreloadSound("VO_BRMA05_1_TURN1_02.prefab:b68353491d7f88a4a8479e7a031aec12");
	}

	// Token: 0x06003426 RID: 13350 RVA: 0x0010B568 File Offset: 0x00109768
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
						m_soundName = "VO_BRMA05_1_RESPONSE_03.prefab:beac5b0620de49f42a2f2a66a906d4d6",
						m_stringTag = "VO_BRMA05_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x06003427 RID: 13351 RVA: 0x0010B5C7 File Offset: 0x001097C7
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
		if (!(cardId == "BRMA05_2") && !(cardId == "BRMA05_2H"))
		{
			if (cardId == "BRMA05_3" || cardId == "BRMA05_3H")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA05_1_CARD_05.prefab:c0bc2f9cc3d3ae047ba80ffa0f70dcb8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA05_1_HERO_POWER_06.prefab:2792e43708ba1df48baa3a41d636097a", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003428 RID: 13352 RVA: 0x0010B5DD File Offset: 0x001097DD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA05_1_TURN1_02.prefab:b68353491d7f88a4a8479e7a031aec12", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003429 RID: 13353 RVA: 0x0010B5F3 File Offset: 0x001097F3
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_BARON_GEDDON_DEAD_40"), "VO_NEFARIAN_BARON_GEDDON_DEAD_40.prefab:6872a4eb94e17a847aebec382654c835", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C8C RID: 7308
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C8D RID: 7309
	private bool m_cardLinePlayed;
}
