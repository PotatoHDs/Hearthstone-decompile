using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM10_Razorgore : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private int m_eggDeathLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA10_1_RESPONSE_03.prefab:efb8b297966889e429ba309443fff39c");
		PreloadSound("VO_BRMA10_1_HERO_POWER_04.prefab:5d2db978ae659d645a110f1bbfdefc03");
		PreloadSound("VO_BRMA10_1_EGG_DEATH_1_05.prefab:66bc1615e7b2e2143a07fd2cdcd3bd98");
		PreloadSound("VO_BRMA10_1_EGG_DEATH_2_06.prefab:178e26b2103924c48b8511c2a5424bff");
		PreloadSound("VO_BRMA10_1_EGG_DEATH_3_07.prefab:906089c0627eed044a123bd5cd9ae38d");
		PreloadSound("VO_BRMA10_1_TURN1_02.prefab:470bcd541d18f5f4e99f1b2b1fd97622");
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
						m_soundName = "VO_BRMA10_1_RESPONSE_03.prefab:efb8b297966889e429ba309443fff39c",
						m_stringTag = "VO_BRMA10_1_RESPONSE_03"
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
		string cardId = entity.GetCardId();
		if ((cardId == "BRMA10_3" || cardId == "BRMA10_3H") && !m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA10_1_HERO_POWER_04.prefab:5d2db978ae659d645a110f1bbfdefc03", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA10_1_TURN1_02.prefab:470bcd541d18f5f4e99f1b2b1fd97622", Notification.SpeechBubbleDirection.TopRight, actor));
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
		if (missionEvent == 1)
		{
			m_eggDeathLinePlayed++;
			switch (m_eggDeathLinePlayed)
			{
			case 1:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_1_05.prefab:66bc1615e7b2e2143a07fd2cdcd3bd98", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 2:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_2_06.prefab:178e26b2103924c48b8511c2a5424bff", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case 3:
				yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_3_07.prefab:906089c0627eed044a123bd5cd9ae38d", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_RAZORGORE_DEAD_55"), "VO_NEFARIAN_RAZORGORE_DEAD_55.prefab:3d24bf2b8eb17e8459ae6a85a7900dde");
		}
	}
}
