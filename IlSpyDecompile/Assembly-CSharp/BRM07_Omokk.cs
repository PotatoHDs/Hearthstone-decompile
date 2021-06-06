using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM07_Omokk : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA07_1_RESPONSE_03.prefab:b43d82ce7a9bb59438b594dd3c185050");
		PreloadSound("VO_BRMA07_1_HERO_POWER_05.prefab:10f8a1b1fc7c9374b8c2b741f27694be");
		PreloadSound("VO_BRMA07_1_CARD_04.prefab:f498bd13724f67d48a0f0bc55034c44b");
		PreloadSound("VO_BRMA07_1_TURN1_02.prefab:ac11bf2418c6e0f418f2216348b224c3");
		PreloadSound("VO_NEFARIAN_OMOKK1_44.prefab:82ad9b06a62bf044b9e5660054e5fae6");
		PreloadSound("VO_NEFARIAN_OMOKK2_45.prefab:bb20664f6b0c27149a6048f473ca0398");
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
						m_soundName = "VO_BRMA07_1_RESPONSE_03.prefab:b43d82ce7a9bb59438b594dd3c185050",
						m_stringTag = "VO_BRMA07_1_RESPONSE_03"
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
		case "BRMA07_2":
		case "BRMA07_2H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA07_1_HERO_POWER_05.prefab:10f8a1b1fc7c9374b8c2b741f27694be", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "BRMA07_3":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA07_1_CARD_04.prefab:f498bd13724f67d48a0f0bc55034c44b", Notification.SpeechBubbleDirection.TopRight, actor));
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA07_1_TURN1_02.prefab:ac11bf2418c6e0f418f2216348b224c3", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case 4:
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_OMOKK1_44"), "VO_NEFARIAN_OMOKK1_44.prefab:82ad9b06a62bf044b9e5660054e5fae6");
			break;
		case 8:
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", position, GameStrings.Get("VO_NEFARIAN_OMOKK2_45"), "VO_NEFARIAN_OMOKK2_45.prefab:bb20664f6b0c27149a6048f473ca0398");
			break;
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_OMOKK_DEAD_46"), "VO_NEFARIAN_OMOKK_DEAD_46.prefab:894d0e92341ab754281388682e449096");
		}
	}
}
