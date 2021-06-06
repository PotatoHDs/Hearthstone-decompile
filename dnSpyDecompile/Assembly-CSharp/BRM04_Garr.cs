using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000379 RID: 889
public class BRM04_Garr : BRM_MissionEntity
{
	// Token: 0x0600341F RID: 13343 RVA: 0x0010B444 File Offset: 0x00109644
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA04_1_RESPONSE_03.prefab:75a029ecfd071914aaf0def7bc041b85");
		base.PreloadSound("VO_BRMA04_1_HERO_POWER_05.prefab:1c2e947768a86424abf65a8b5ad573ec");
		base.PreloadSound("VO_BRMA04_1_CARD_04.prefab:53f20ec5598fc8a459615f6a57c661be");
		base.PreloadSound("VO_BRMA04_1_TURN1_02.prefab:198010c5061020b499e36ee02b9a6e9f");
		base.PreloadSound("VO_NEFARIAN_GARR2_35.prefab:17167cbeb359c8c459a1ce3824206474");
		base.PreloadSound("VO_NEFARIAN_GARR3_36.prefab:a9d3c5553f63ed54bac596039f115511");
		base.PreloadSound("VO_NEFARIAN_GARR4_37.prefab:12898207b42d4ca42b7cdb7a711f5726");
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x0010B4A0 File Offset: 0x001096A0
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
						m_soundName = "VO_BRMA04_1_RESPONSE_03.prefab:75a029ecfd071914aaf0def7bc041b85",
						m_stringTag = "VO_BRMA04_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x0010B4FF File Offset: 0x001096FF
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
		if (!(cardId == "BRMA04_2"))
		{
			if (cardId == "BRMA04_4" || cardId == "BRMA04_4H")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA04_1_CARD_04.prefab:53f20ec5598fc8a459615f6a57c661be", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA04_1_HERO_POWER_05.prefab:1c2e947768a86424abf65a8b5ad573ec", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x0010B515 File Offset: 0x00109715
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 position = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn <= 4)
		{
			if (turn != 1)
			{
				if (turn == 4)
				{
					if (!GameMgr.Get().IsClassChallengeMission())
					{
						NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR2_35"), "VO_NEFARIAN_GARR2_35.prefab:17167cbeb359c8c459a1ce3824206474", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
					}
				}
			}
			else
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA04_1_TURN1_02.prefab:198010c5061020b499e36ee02b9a6e9f", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else if (turn != 8)
		{
			if (turn == 12)
			{
				if (!GameMgr.Get().IsClassChallengeMission())
				{
					NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR4_37"), "VO_NEFARIAN_GARR4_37.prefab:12898207b42d4ca42b7cdb7a711f5726", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
				}
			}
		}
		else if (!GameMgr.Get().IsClassChallengeMission())
		{
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR3_36"), "VO_NEFARIAN_GARR3_36.prefab:a9d3c5553f63ed54bac596039f115511", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x0010B52B File Offset: 0x0010972B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_GARR_DEAD1_38"), "VO_NEFARIAN_GARR_DEAD1_38.prefab:7cfd65566df0d294f9591e2ad70e1781", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C8A RID: 7306
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C8B RID: 7307
	private bool m_cardLinePlayed;
}
