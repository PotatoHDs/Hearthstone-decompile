using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX07_Razuvious : NAX_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX7_01_HP_02.prefab:cb3aadc3fbe355e40bbd5463f09ffdf8");
		PreloadSound("VO_NAX7_01_START_01.prefab:3fc94f039bccb2d4ca0e0a242b2f955e");
		PreloadSound("VO_NAX7_01_EMOTE_05.prefab:a116dabbfbb825e4cb519d18a2c21779");
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
						m_soundName = "VO_NAX7_01_EMOTE_05.prefab:a116dabbfbb825e4cb519d18a2c21779",
						m_stringTag = "VO_NAX7_01_EMOTE_05"
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
			NotificationManager.Get().CreateKTQuote("VO_KT_RAZUVIOUS2_59", "VO_KT_RAZUVIOUS2_59.prefab:58901b0d8c4e834489caca72c1fb5ecc");
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
		if (cardId == "NAX7_03" && !m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX7_01_HP_02.prefab:cb3aadc3fbe355e40bbd5463f09ffdf8", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent != 1)
		{
			yield break;
		}
		bool flag = false;
		PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
		Entity entity = currentTaskList?.GetSourceEntity();
		if (entity != null && entity.GetCardId() == "NAX7_05")
		{
			foreach (PowerTask task in currentTaskList.GetTaskList())
			{
				Network.PowerHistory power = task.GetPower();
				if (power.Type != Network.PowerType.META_DATA)
				{
					continue;
				}
				Network.HistMetaData histMetaData = power as Network.HistMetaData;
				if (histMetaData.MetaType != 0 || histMetaData.Info == null || histMetaData.Info.Count == 0)
				{
					continue;
				}
				for (int i = 0; i < histMetaData.Info.Count; i++)
				{
					Entity entity2 = GameState.Get().GetEntity(histMetaData.Info[i]);
					if (entity2 != null && entity2.GetCardId() == "NAX7_02")
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					break;
				}
			}
		}
		if (flag)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX7_01_START_01.prefab:3fc94f039bccb2d4ca0e0a242b2f955e", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}
}
