using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM04_Garr : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA04_1_RESPONSE_03.prefab:75a029ecfd071914aaf0def7bc041b85");
		PreloadSound("VO_BRMA04_1_HERO_POWER_05.prefab:1c2e947768a86424abf65a8b5ad573ec");
		PreloadSound("VO_BRMA04_1_CARD_04.prefab:53f20ec5598fc8a459615f6a57c661be");
		PreloadSound("VO_BRMA04_1_TURN1_02.prefab:198010c5061020b499e36ee02b9a6e9f");
		PreloadSound("VO_NEFARIAN_GARR2_35.prefab:17167cbeb359c8c459a1ce3824206474");
		PreloadSound("VO_NEFARIAN_GARR3_36.prefab:a9d3c5553f63ed54bac596039f115511");
		PreloadSound("VO_NEFARIAN_GARR4_37.prefab:12898207b42d4ca42b7cdb7a711f5726");
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
						m_soundName = "VO_BRMA04_1_RESPONSE_03.prefab:75a029ecfd071914aaf0def7bc041b85",
						m_stringTag = "VO_BRMA04_1_RESPONSE_03"
					}
				}
			}
		};
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		switch (entity.GetCardId())
		{
		case "BRMA04_2":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA04_1_HERO_POWER_05.prefab:1c2e947768a86424abf65a8b5ad573ec", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "BRMA04_4":
		case "BRMA04_4H":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA04_1_CARD_04.prefab:53f20ec5598fc8a459615f6a57c661be", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Vector3 position = new Vector3(95f, NotificationManager.DEPTH, 36.8f);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA04_1_TURN1_02.prefab:198010c5061020b499e36ee02b9a6e9f", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case 4:
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR2_35"), "VO_NEFARIAN_GARR2_35.prefab:17167cbeb359c8c459a1ce3824206474");
			}
			break;
		case 8:
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR3_36"), "VO_NEFARIAN_GARR3_36.prefab:a9d3c5553f63ed54bac596039f115511");
			}
			break;
		case 12:
			if (!GameMgr.Get().IsClassChallengeMission())
			{
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_GARR4_37"), "VO_NEFARIAN_GARR4_37.prefab:12898207b42d4ca42b7cdb7a711f5726");
			}
			break;
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_GARR_DEAD1_38"), "VO_NEFARIAN_GARR_DEAD1_38.prefab:7cfd65566df0d294f9591e2ad70e1781");
		}
	}
}
