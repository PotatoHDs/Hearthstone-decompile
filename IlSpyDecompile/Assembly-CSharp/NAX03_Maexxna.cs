using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX03_Maexxna : NAX_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_seaGiantLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX3_01_EMOTE_01.prefab:a31d7a46b65b449479a2a531268f9299");
		PreloadSound("VO_NAX3_01_EMOTE_02.prefab:c1e3c5438c9b2a1469badb6ac78ed010");
		PreloadSound("VO_NAX3_01_EMOTE_03.prefab:2d1d268f8503f0d4090ac41624711149");
		PreloadSound("VO_NAX3_01_EMOTE_04.prefab:8163100101e44994680768b0f4220eec");
		PreloadSound("VO_NAX3_01_EMOTE_05.prefab:0fc3e310650ab7149b5bf2b794895869");
		PreloadSound("VO_NAX3_01_CARD_01.prefab:8f9084036cb9a31429b886ee01cc9bad");
		PreloadSound("VO_NAX3_01_HP_01.prefab:428343bce2fa95c42837e3f7fd220634");
		PreloadSound("VO_KT_MAEXXNA2_47.prefab:ba0b856a1b49d4249b511c6d2a7e5a66");
		PreloadSound("VO_KT_MAEXXNA6_51.prefab:105796bfd5d566249beef8e4c8672ee3");
		PreloadSound("VO_KT_MAEXXNA3_48.prefab:3bd05eb0fda073245bf190fc623a148c");
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
						m_soundName = "VO_NAX3_01_EMOTE_01.prefab:a31d7a46b65b449479a2a531268f9299",
						m_stringTag = "VO_NAX3_01_EMOTE_01"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_02.prefab:c1e3c5438c9b2a1469badb6ac78ed010",
						m_stringTag = "VO_NAX3_01_EMOTE_02"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_03.prefab:2d1d268f8503f0d4090ac41624711149",
						m_stringTag = "VO_NAX3_01_EMOTE_03"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_04.prefab:8163100101e44994680768b0f4220eec",
						m_stringTag = "VO_NAX3_01_EMOTE_04"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX3_01_EMOTE_05.prefab:0fc3e310650ab7149b5bf2b794895869",
						m_stringTag = "VO_NAX3_01_EMOTE_05"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA2_47", "VO_KT_MAEXXNA2_47.prefab:ba0b856a1b49d4249b511c6d2a7e5a66", allowRepeatDuringSession: false);
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA4_49", "VO_KT_MAEXXNA4_49.prefab:449ab7cb30688e344896e51a9fc4dfd1");
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
		switch (entity.GetCardId())
		{
		case "NAX3_02":
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX3_01_HP_01.prefab:428343bce2fa95c42837e3f7fd220634", Notification.SpeechBubbleDirection.TopRight, actor));
			if (!m_heroPowerLinePlayed)
			{
				m_heroPowerLinePlayed = true;
				while (m_enemySpeaking || NotificationManager.Get().IsQuotePlaying)
				{
					yield return 0;
				}
				NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA6_51", "VO_KT_MAEXXNA6_51.prefab:105796bfd5d566249beef8e4c8672ee3", allowRepeatDuringSession: false);
			}
			break;
		case "NAX3_03":
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX3_01_CARD_01.prefab:8f9084036cb9a31429b886ee01cc9bad", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		case "EX1_586":
			if (!m_seaGiantLinePlayed)
			{
				m_seaGiantLinePlayed = true;
				yield return new WaitForSeconds(1f);
				while (NotificationManager.Get().IsQuotePlaying)
				{
					yield return 0;
				}
				NotificationManager.Get().CreateKTQuote("VO_KT_MAEXXNA3_48", "VO_KT_MAEXXNA3_48.prefab:3bd05eb0fda073245bf190fc623a148c", allowRepeatDuringSession: false);
			}
			break;
		}
	}
}
