using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX04_Noth : NAX_MissionEntity
{
	private bool m_cardLinePlayed;

	private bool m_heroPowerLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX4_01_HP_02.prefab:ef429c4ce7a413d4fa8ce390025bd388");
		PreloadSound("VO_NAX4_01_CARD_03.prefab:676865be5229fbb4ea8b71bb7570b6f2");
		PreloadSound("VO_NAX4_01_EMOTE_06.prefab:837b9665fb7727145966a74e0610ee05");
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
						m_soundName = "VO_NAX4_01_EMOTE_06.prefab:837b9665fb7727145966a74e0610ee05",
						m_stringTag = "VO_NAX4_01_EMOTE_06"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_NOTH2_53", "VO_KT_NOTH2_53.prefab:0ac170c747ea31f4182b1abce130228b");
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 1 && !m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX4_01_HP_02.prefab:ef429c4ce7a413d4fa8ce390025bd388", Notification.SpeechBubbleDirection.TopRight, actor));
		}
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
		if (cardId == "NAX4_05" && !m_cardLinePlayed)
		{
			m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX4_01_CARD_03.prefab:676865be5229fbb4ea8b71bb7570b6f2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
