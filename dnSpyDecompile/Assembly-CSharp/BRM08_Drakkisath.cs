using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037D RID: 893
public class BRM08_Drakkisath : BRM_MissionEntity
{
	// Token: 0x06003438 RID: 13368 RVA: 0x0010B876 File Offset: 0x00109A76
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA08_1_RESPONSE_04.prefab:6f5840c652a862f46baddba08396a839");
		base.PreloadSound("VO_BRMA08_1_CARD_05.prefab:fd0151285c6aec540b909e0a29f5acb8");
		base.PreloadSound("VO_BRMA08_1_TURN1_03.prefab:e4d206e77c3c8f548934be5fcce89ea5");
		base.PreloadSound("VO_NEFARIAN_DRAKKISATH_RESPOND_48.prefab:f422b6326aa079743967cb9988b445c7");
		base.PreloadSound("VO_BRMA08_1_TURN1_ALT_02.prefab:d71f74a42d0105446a5e2e3c4b60e067");
	}

	// Token: 0x06003439 RID: 13369 RVA: 0x0010B8B0 File Offset: 0x00109AB0
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
						m_soundName = "VO_BRMA08_1_RESPONSE_04.prefab:6f5840c652a862f46baddba08396a839",
						m_stringTag = "VO_BRMA08_1_RESPONSE_04"
					}
				}
			}
		};
	}

	// Token: 0x0600343A RID: 13370 RVA: 0x0010B90F File Offset: 0x00109B0F
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
		if (cardId == "BRMA08_3")
		{
			if (this.m_cardLinePlayed)
			{
				yield break;
			}
			this.m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA08_1_CARD_05.prefab:fd0151285c6aec540b909e0a29f5acb8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600343B RID: 13371 RVA: 0x0010B925 File Offset: 0x00109B25
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 quotePos = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			if (turn != 4)
			{
				if (turn == 6)
				{
					if (!GameMgr.Get().IsClassChallengeMission())
					{
						NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_DRAKKISATH1_49"), "VO_NEFARIAN_DRAKKISATH1_49.prefab:792c02424ea5f5d43989d65b4b3ca839", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
					}
				}
			}
			else if (!GameMgr.Get().IsClassChallengeMission())
			{
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA08_1_TURN1_03.prefab:e4d206e77c3c8f548934be5fcce89ea5", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_DRAKKISATH_RESPOND_48"), "VO_NEFARIAN_DRAKKISATH_RESPOND_48.prefab:f422b6326aa079743967cb9988b445c7", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		else
		{
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA08_1_TURN1_ALT_02.prefab:d71f74a42d0105446a5e2e3c4b60e067", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600343C RID: 13372 RVA: 0x0010B93B File Offset: 0x00109B3B
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_DRAKKISATH_DEAD_50"), "VO_NEFARIAN_DRAKKISATH_DEAD_50.prefab:a0d0aa371c62ff24ca675731ff3e5396", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C90 RID: 7312
	private bool m_cardLinePlayed;
}
