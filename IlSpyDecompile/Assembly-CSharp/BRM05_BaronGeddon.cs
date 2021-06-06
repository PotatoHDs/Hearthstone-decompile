using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM05_BaronGeddon : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA05_1_RESPONSE_03.prefab:beac5b0620de49f42a2f2a66a906d4d6");
		PreloadSound("VO_BRMA05_1_HERO_POWER_06.prefab:2792e43708ba1df48baa3a41d636097a");
		PreloadSound("VO_BRMA05_1_CARD_05.prefab:c0bc2f9cc3d3ae047ba80ffa0f70dcb8");
		PreloadSound("VO_BRMA05_1_TURN1_02.prefab:b68353491d7f88a4a8479e7a031aec12");
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
						m_soundName = "VO_BRMA05_1_RESPONSE_03.prefab:beac5b0620de49f42a2f2a66a906d4d6",
						m_stringTag = "VO_BRMA05_1_RESPONSE_03"
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
		case "BRMA05_2":
		case "BRMA05_2H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA05_1_HERO_POWER_06.prefab:2792e43708ba1df48baa3a41d636097a", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "BRMA05_3":
		case "BRMA05_3H":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA05_1_CARD_05.prefab:c0bc2f9cc3d3ae047ba80ffa0f70dcb8", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA05_1_TURN1_02.prefab:b68353491d7f88a4a8479e7a031aec12", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_BARON_GEDDON_DEAD_40"), "VO_NEFARIAN_BARON_GEDDON_DEAD_40.prefab:6872a4eb94e17a847aebec382654c835");
		}
	}
}
