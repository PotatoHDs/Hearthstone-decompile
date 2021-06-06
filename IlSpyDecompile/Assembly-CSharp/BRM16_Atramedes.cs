using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM16_Atramedes : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	private int m_gongLinePlayed;

	private int m_weaponLinePlayed;

	public override string GetAlternatePlayerName()
	{
		return GameStrings.Get("MISSION_NEFARIAN_TITLE");
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA16_1_RESPONSE_03.prefab:ebc4ebb81b3a1b741b3895a371f72614");
		PreloadSound("VO_BRMA16_1_HERO_POWER_05.prefab:2facfeb30b95f49429ad143e643a3fe5");
		PreloadSound("VO_BRMA16_1_CARD_04.prefab:4c0923e1b9cbc854c9c78e549e2e62e4");
		PreloadSound("VO_BRMA16_1_GONG1_10.prefab:f36e1a59d28147749ad113da7831b5c6");
		PreloadSound("VO_BRMA16_1_GONG2_11.prefab:0064023a77f719646bea0ae472854c8b");
		PreloadSound("VO_BRMA16_1_GONG3_12.prefab:6d117262d495e6946aabe17ffff06c57");
		PreloadSound("VO_BRMA16_1_TRIGGER1_07.prefab:6651f227d949b2948b69f2317f29970c");
		PreloadSound("VO_BRMA16_1_TRIGGER2_08.prefab:97045358fdf509a42b86706dc0f3d477");
		PreloadSound("VO_BRMA16_1_TRIGGER3_09.prefab:6bd124e6a8a16fc4cacc8add95c429a6");
		PreloadSound("VO_BRMA16_1_TURN1_02.prefab:8edd557780fa3034c865f96650df136f");
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
						m_soundName = "VO_BRMA16_1_RESPONSE_03.prefab:ebc4ebb81b3a1b741b3895a371f72614",
						m_stringTag = "VO_BRMA16_1_RESPONSE_03"
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
		case "BRMA16_2":
		case "BRMA16_2H":
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_HERO_POWER_05.prefab:2facfeb30b95f49429ad143e643a3fe5", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "BRMA16_3":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_CARD_04.prefab:4c0923e1b9cbc854c9c78e549e2e62e4", Notification.SpeechBubbleDirection.TopRight, actor));
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
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_TURN1_02.prefab:8edd557780fa3034c865f96650df136f", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			m_gongLinePlayed++;
			switch (m_gongLinePlayed)
			{
			case 1:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG1_10.prefab:f36e1a59d28147749ad113da7831b5c6", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 2:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG3_12.prefab:6d117262d495e6946aabe17ffff06c57", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 3:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_GONG2_11.prefab:0064023a77f719646bea0ae472854c8b", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
			break;
		case 2:
			m_weaponLinePlayed++;
			switch (m_weaponLinePlayed)
			{
			case 1:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER1_07.prefab:6651f227d949b2948b69f2317f29970c", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 2:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER2_08.prefab:97045358fdf509a42b86706dc0f3d477", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 3:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA16_1_TRIGGER3_09.prefab:6bd124e6a8a16fc4cacc8add95c429a6", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NefarianDragon_Quote.prefab:179fec888df7e4c02b8de3b7ad109a23", GameStrings.Get("VO_NEFARIAN_ATRAMEDES_DEATH_76"), "VO_NEFARIAN_ATRAMEDES_DEATH_76.prefab:7f23d65dd346a234fb410aeea9ec0d44");
		}
	}
}
