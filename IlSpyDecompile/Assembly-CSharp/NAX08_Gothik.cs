using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX08_Gothik : NAX_MissionEntity
{
	private bool m_cardLinePlayed;

	private bool m_unrelentingMinionLinePlayed;

	private bool m_deadReturnLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX8_01_CARD_02.prefab:39a2051167989ad48a234c7bfcf6bb0b");
		PreloadSound("VO_NAX8_01_CUSTOM_03.prefab:4e1adb5a87d8efd45ba4fab32ba9dff1");
		PreloadSound("VO_NAX8_01_EMOTE1_06.prefab:d000156f1cd9f7d4d816d660fc74caa0");
		PreloadSound("VO_NAX8_01_EMOTE2_07.prefab:61420eb86b3febb4da7077c26b66fe82");
		PreloadSound("VO_NAX8_01_EMOTE3_08.prefab:e54c3c21bf16794418b8d684af0d13ea");
		PreloadSound("VO_NAX8_01_EMOTE4_09.prefab:d9acf9ba48ac4324e96a0c2ec232545b");
		PreloadSound("VO_NAX8_01_EMOTE5_10.prefab:d6d0522885dbd074b9d554dd47fddbb5");
		PreloadSound("VO_NAX8_01_CUSTOM2_04.prefab:10c0d0ad330cfb44eb7faa0413cd2e23");
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
						m_soundName = "VO_NAX8_01_EMOTE1_06.prefab:d000156f1cd9f7d4d816d660fc74caa0",
						m_stringTag = "VO_NAX8_01_EMOTE1_06"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE2_07.prefab:61420eb86b3febb4da7077c26b66fe82",
						m_stringTag = "VO_NAX8_01_EMOTE2_07"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE3_08.prefab:e54c3c21bf16794418b8d684af0d13ea",
						m_stringTag = "VO_NAX8_01_EMOTE3_08"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE4_09.prefab:d9acf9ba48ac4324e96a0c2ec232545b",
						m_stringTag = "VO_NAX8_01_EMOTE4_09"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE5_10.prefab:d6d0522885dbd074b9d554dd47fddbb5",
						m_stringTag = "VO_NAX8_01_EMOTE5_10"
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
			NotificationManager.Get().CreateKTQuote("VO_KT_GOTHIK2_62", "VO_KT_GOTHIK2_62.prefab:0ac7f3dd8ea055b4c81abd7f25e3f782");
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
		case "NAX8_03":
		case "NAX8_04":
		case "NAX8_05":
			if (!m_unrelentingMinionLinePlayed)
			{
				m_unrelentingMinionLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX8_01_CARD_02.prefab:39a2051167989ad48a234c7bfcf6bb0b", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case "NAX8_02":
			if (!m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX8_01_CUSTOM_03.prefab:4e1adb5a87d8efd45ba4fab32ba9dff1", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (missionEvent == 1 && !m_deadReturnLinePlayed)
		{
			m_deadReturnLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX8_01_CUSTOM2_04.prefab:10c0d0ad330cfb44eb7faa0413cd2e23", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
