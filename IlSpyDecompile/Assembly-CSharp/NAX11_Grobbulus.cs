using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX11_Grobbulus : NAX_MissionEntity
{
	private bool m_cardLinePlayed;

	private bool m_heroPowerLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX11_01_HP_02.prefab:5683ba1493ba8aa47ba0fcbf61f2a39c");
		PreloadSound("VO_NAX11_01_CARD_03.prefab:5bb409481ec9ba74bb7336fb88b82ea2");
		PreloadSound("VO_NAX11_01_EMOTE_04.prefab:c3dd01740e7b8a14d9149abe560f05aa");
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
						m_soundName = "VO_NAX11_01_EMOTE_04.prefab:c3dd01740e7b8a14d9149abe560f05aa",
						m_stringTag = "VO_NAX11_01_EMOTE_04"
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
			NotificationManager.Get().CreateKTQuote("VO_KT_GROBBULUS2_71", "VO_KT_GROBBULUS2_71.prefab:352919c0987145140bdf6144c220956c");
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
		if (!(cardId == "NAX11_02"))
		{
			if (cardId == "NAX11_04" && !m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX11_01_CARD_03.prefab:5bb409481ec9ba74bb7336fb88b82ea2", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (!m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX11_01_HP_02.prefab:5683ba1493ba8aa47ba0fcbf61f2a39c", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
