using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM12_Chromaggus : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("ChromaggusBoss_EmoteResponse_1.prefab:c1df8f6a22644644e8542c7c40106c71");
		PreloadSound("VO_NEFARIAN_CHROMAGGUS_DEAD_63.prefab:5111b36941f988646844fb71a6dadc9c");
		PreloadSound("VO_NEFARIAN_CHROMAGGUS1_59.prefab:fce85696482b4af4b8d60144e1e464eb");
		PreloadSound("VO_NEFARIAN_CHROMAGGUS2_60.prefab:ca5b9fe6e57f09d4d816bfc602c82785");
		PreloadSound("VO_NEFARIAN_CHROMAGGUS3_61.prefab:5a872d426eeb0494cb1e3fdc8365158d");
		PreloadSound("VO_NEFARIAN_CHROMAGGUS4_62.prefab:0b066e824cd787d498aa5a54d21820df");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "ChromaggusBoss_EmoteResponse_1.prefab:c1df8f6a22644644e8542c7c40106c71",
						m_stringTag = "ChromaggusBoss_EmoteResponse_1"
					}
				}
			}
		};
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		Vector3 quotePos = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		switch (entity.GetCardId())
		{
		case "BRMA12_2":
		case "BRMA12_2H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS4_62"), "VO_NEFARIAN_CHROMAGGUS4_62.prefab:0b066e824cd787d498aa5a54d21820df");
			}
			break;
		case "BRMA12_8":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", quotePos, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS3_61"), "VO_NEFARIAN_CHROMAGGUS3_61.prefab:5a872d426eeb0494cb1e3fdc8365158d");
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 position = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		switch (turn)
		{
		case 2:
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS1_59"), "VO_NEFARIAN_CHROMAGGUS1_59.prefab:fce85696482b4af4b8d60144e1e464eb");
			break;
		case 6:
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_CHROMAGGUS2_60"), "VO_NEFARIAN_CHROMAGGUS2_60.prefab:ca5b9fe6e57f09d4d816bfc602c82785");
			break;
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_CHROMAGGUS_DEAD_63"), "VO_NEFARIAN_CHROMAGGUS_DEAD_63.prefab:5111b36941f988646844fb71a6dadc9c");
		}
	}
}
