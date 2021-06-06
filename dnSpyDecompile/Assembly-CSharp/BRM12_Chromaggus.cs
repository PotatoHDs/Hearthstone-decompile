using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000381 RID: 897
public class BRM12_Chromaggus : BRM_MissionEntity
{
	// Token: 0x06003452 RID: 13394 RVA: 0x0010BC58 File Offset: 0x00109E58
	public override void PreloadAssets()
	{
		base.PreloadSound("ChromaggusBoss_EmoteResponse_1.prefab:c1df8f6a22644644e8542c7c40106c71");
		base.PreloadSound("VO_NEFARIAN_CHROMAGGUS_DEAD_63.prefab:5111b36941f988646844fb71a6dadc9c");
		base.PreloadSound("VO_NEFARIAN_CHROMAGGUS1_59.prefab:fce85696482b4af4b8d60144e1e464eb");
		base.PreloadSound("VO_NEFARIAN_CHROMAGGUS2_60.prefab:ca5b9fe6e57f09d4d816bfc602c82785");
		base.PreloadSound("VO_NEFARIAN_CHROMAGGUS3_61.prefab:5a872d426eeb0494cb1e3fdc8365158d");
		base.PreloadSound("VO_NEFARIAN_CHROMAGGUS4_62.prefab:0b066e824cd787d498aa5a54d21820df");
	}

	// Token: 0x06003453 RID: 13395 RVA: 0x0010BCA8 File Offset: 0x00109EA8
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
						m_soundName = "ChromaggusBoss_EmoteResponse_1.prefab:c1df8f6a22644644e8542c7c40106c71",
						m_stringTag = "ChromaggusBoss_EmoteResponse_1"
					}
				}
			}
		};
	}

	// Token: 0x06003454 RID: 13396 RVA: 0x0010BD07 File Offset: 0x00109F07
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		Vector3 quotePos = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		string cardId = entity.GetCardId();
		if (!(cardId == "BRMA12_2") && !(cardId == "BRMA12_2H"))
		{
			if (cardId == "BRMA12_8")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS3_61"), "VO_NEFARIAN_CHROMAGGUS3_61.prefab:5a872d426eeb0494cb1e3fdc8365158d", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS4_62"), "VO_NEFARIAN_CHROMAGGUS4_62.prefab:0b066e824cd787d498aa5a54d21820df", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x06003455 RID: 13397 RVA: 0x0010BD1D File Offset: 0x00109F1D
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 position = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		if (turn != 2)
		{
			if (turn == 6)
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS2_60"), "VO_NEFARIAN_CHROMAGGUS2_60.prefab:ca5b9fe6e57f09d4d816bfc602c82785", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		else
		{
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS1_59"), "VO_NEFARIAN_CHROMAGGUS1_59.prefab:fce85696482b4af4b8d60144e1e464eb", true, 0f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x06003456 RID: 13398 RVA: 0x0010BD2C File Offset: 0x00109F2C
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_CHROMAGGUS_DEAD_63"), "VO_NEFARIAN_CHROMAGGUS_DEAD_63.prefab:5111b36941f988646844fb71a6dadc9c", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C99 RID: 7321
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C9A RID: 7322
	private bool m_cardLinePlayed;
}
