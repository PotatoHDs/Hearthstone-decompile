using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX13_Thaddius : NAX_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX13_01_HP_02.prefab:cc9afdc24fabea54abc939924c34c7f8");
		PreloadSound("VO_NAX13_01_EMOTE_04.prefab:337ef024b2d71e84393f6da891bf83cc");
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
						m_soundName = "VO_NAX13_01_EMOTE_04.prefab:337ef024b2d71e84393f6da891bf83cc",
						m_stringTag = "VO_NAX13_01_EMOTE_04"
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
			NotificationManager.Get().CreateKTQuote("VO_KT_THADDIUS2_81", "VO_KT_THADDIUS2_81.prefab:47685f2ff524d944f90a9cb87b8e9861");
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
		if (cardId == "NAX13_02" && !m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX13_01_HP_02.prefab:cc9afdc24fabea54abc939924c34c7f8", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
