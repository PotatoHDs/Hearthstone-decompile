using System;
using System.Collections;
using System.Collections.Generic;
using PegasusGame;
using UnityEngine;

// Token: 0x02000589 RID: 1417
public class NAX07_Razuvious : NAX_MissionEntity
{
	// Token: 0x06004EF6 RID: 20214 RVA: 0x0019F438 File Offset: 0x0019D638
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX7_01_HP_02.prefab:cb3aadc3fbe355e40bbd5463f09ffdf8");
		base.PreloadSound("VO_NAX7_01_START_01.prefab:3fc94f039bccb2d4ca0e0a242b2f955e");
		base.PreloadSound("VO_NAX7_01_EMOTE_05.prefab:a116dabbfbb825e4cb519d18a2c21779");
	}

	// Token: 0x06004EF7 RID: 20215 RVA: 0x0019F45C File Offset: 0x0019D65C
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>(MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS),
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX7_01_EMOTE_05.prefab:a116dabbfbb825e4cb519d18a2c21779",
						m_stringTag = "VO_NAX7_01_EMOTE_05"
					}
				}
			}
		};
	}

	// Token: 0x06004EF8 RID: 20216 RVA: 0x0019F4BB File Offset: 0x0019D6BB
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_RAZUVIOUS2_59", "VO_KT_RAZUVIOUS2_59.prefab:58901b0d8c4e834489caca72c1fb5ecc", true);
		}
		yield break;
	}

	// Token: 0x06004EF9 RID: 20217 RVA: 0x0019F4CA File Offset: 0x0019D6CA
	protected override IEnumerator RespondToPlayedCardWithTiming(global::Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "NAX7_03")
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX7_01_HP_02.prefab:cb3aadc3fbe355e40bbd5463f09ffdf8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004EFA RID: 20218 RVA: 0x0019F4E0 File Offset: 0x0019D6E0
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		if (missionEvent == 1)
		{
			bool flag = false;
			PowerTaskList currentTaskList = GameState.Get().GetPowerProcessor().GetCurrentTaskList();
			global::Entity entity = (currentTaskList == null) ? null : currentTaskList.GetSourceEntity(true);
			if (entity != null && entity.GetCardId() == "NAX7_05")
			{
				foreach (PowerTask powerTask in currentTaskList.GetTaskList())
				{
					Network.PowerHistory power = powerTask.GetPower();
					if (power.Type == Network.PowerType.META_DATA)
					{
						Network.HistMetaData histMetaData = power as Network.HistMetaData;
						if (histMetaData.MetaType == HistoryMeta.Type.TARGET && histMetaData.Info != null && histMetaData.Info.Count != 0)
						{
							for (int i = 0; i < histMetaData.Info.Count; i++)
							{
								global::Entity entity2 = GameState.Get().GetEntity(histMetaData.Info[i]);
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
				}
			}
			if (flag)
			{
				Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX7_01_START_01.prefab:3fc94f039bccb2d4ca0e0a242b2f955e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		yield break;
	}

	// Token: 0x0400453E RID: 17726
	private bool m_heroPowerLinePlayed;
}
