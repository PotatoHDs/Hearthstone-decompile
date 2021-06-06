using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX14_Sapphiron : NAX_MissionEntity
{
	private bool m_cardKtLinePlayed;

	private int m_numTimesFrostBreathMisses;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX14_01_EMOTE_01.prefab:8e779dac945dafe4cae350690bfe5adb");
		PreloadSound("VO_NAX14_01_EMOTE_02.prefab:2062a07b94954c44aab47bc4edc1c307");
		PreloadSound("VO_NAX14_01_EMOTE_03.prefab:225909a5368dd56489a130eb22afbd19");
		PreloadSound("VO_NAX14_01_CARD_01.prefab:349cacac917de9f49ad5a75f1352e53c");
		PreloadSound("VO_NAX14_01_HP_01.prefab:641e44497675c5b4497a0be59dcec408");
		PreloadSound("VO_KT_SAPPHIRON2_84.prefab:c2f8a9371bca45441b0d069994e2fc96");
		PreloadSound("VO_KT_SAPPHIRON3_85.prefab:0d71aa21bf415bb448a90c1d87f73a82");
		PreloadSound("VO_KT_SAPPHIRON4_ALT_87.prefab:58b23de92708c8146b0f278db588c7d7");
		PreloadSound("VO_KT_SAPPHIRON5_88.prefab:92e68b9f36ba81e4d94e20135a842f0d");
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
						m_soundName = "VO_NAX14_01_EMOTE_01.prefab:8e779dac945dafe4cae350690bfe5adb",
						m_stringTag = "VO_NAX14_01_EMOTE_01"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX14_01_EMOTE_02.prefab:2062a07b94954c44aab47bc4edc1c307",
						m_stringTag = "VO_NAX14_01_EMOTE_02"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX14_01_EMOTE_03.prefab:225909a5368dd56489a130eb22afbd19",
						m_stringTag = "VO_NAX14_01_EMOTE_03"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1 && GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId() != "NAX14_01H")
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON2_84", "VO_KT_SAPPHIRON2_84.prefab:c2f8a9371bca45441b0d069994e2fc96", allowRepeatDuringSession: false);
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON5_88", "VO_KT_SAPPHIRON5_88.prefab:92e68b9f36ba81e4d94e20135a842f0d");
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent == 1)
		{
			m_numTimesFrostBreathMisses++;
			if (m_numTimesFrostBreathMisses == 4)
			{
				NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON3_85", "VO_KT_SAPPHIRON3_85.prefab:0d71aa21bf415bb448a90c1d87f73a82");
			}
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
		if (!(cardId == "NAX14_02"))
		{
			if (cardId == "NAX14_04")
			{
				yield return new WaitForSeconds(1f);
				if (m_cardKtLinePlayed)
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX14_01_CARD_01.prefab:349cacac917de9f49ad5a75f1352e53c", Notification.SpeechBubbleDirection.TopRight, enemyActor));
					yield break;
				}
				NotificationManager.Get().CreateKTQuote("VO_KT_SAPPHIRON4_ALT_87", "VO_KT_SAPPHIRON4_ALT_87.prefab:58b23de92708c8146b0f278db588c7d7", allowRepeatDuringSession: false);
				m_cardKtLinePlayed = true;
			}
		}
		else
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX14_01_HP_01.prefab:641e44497675c5b4497a0be59dcec408", Notification.SpeechBubbleDirection.TopRight, enemyActor));
		}
	}
}
