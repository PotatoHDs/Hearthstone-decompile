using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX10_Patchwerk : NAX_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX10_01_HP_02.prefab:b7fff1c198650934c8b2902ddd5bcdbf");
		PreloadSound("VO_NAX10_01_EMOTE2_05.prefab:905bdb461afbd5244954f25829e1b99b");
		PreloadSound("VO_NAX10_01_EMOTE1_04.prefab:c5b2fff109f63134597f357b4b67c0b9");
	}

	protected override void InitEmoteResponses()
	{
		m_emoteResponseGroups = new List<EmoteResponseGroup>
		{
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.GREETINGS,
					EmoteType.OOPS,
					EmoteType.SORRY,
					EmoteType.THANKS,
					EmoteType.THREATEN
				},
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_NAX10_01_EMOTE1_04.prefab:c5b2fff109f63134597f357b4b67c0b9",
						m_stringTag = "VO_NAX10_01_EMOTE1_04"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.WELL_PLAYED },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_NAX10_01_EMOTE2_05.prefab:905bdb461afbd5244954f25829e1b99b",
						m_stringTag = "VO_NAX10_01_EMOTE2_05"
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
			NotificationManager.Get().CreateKTQuote("VO_KT_PATCHWERK2_69", "VO_KT_PATCHWERK2_69.prefab:b11d9d854c9a8414693838d75f455f21");
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "NAX10_03" && !m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX10_01_HP_02.prefab:b7fff1c198650934c8b2902ddd5bcdbf", Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn % 2 != 0)
		{
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(1f);
			GameState.Get().SetBusy(busy: false);
		}
	}
}
