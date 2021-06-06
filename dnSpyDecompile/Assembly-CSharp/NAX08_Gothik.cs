using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058A RID: 1418
public class NAX08_Gothik : NAX_MissionEntity
{
	// Token: 0x06004EFD RID: 20221 RVA: 0x0019F4F8 File Offset: 0x0019D6F8
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX8_01_CARD_02.prefab:39a2051167989ad48a234c7bfcf6bb0b");
		base.PreloadSound("VO_NAX8_01_CUSTOM_03.prefab:4e1adb5a87d8efd45ba4fab32ba9dff1");
		base.PreloadSound("VO_NAX8_01_EMOTE1_06.prefab:d000156f1cd9f7d4d816d660fc74caa0");
		base.PreloadSound("VO_NAX8_01_EMOTE2_07.prefab:61420eb86b3febb4da7077c26b66fe82");
		base.PreloadSound("VO_NAX8_01_EMOTE3_08.prefab:e54c3c21bf16794418b8d684af0d13ea");
		base.PreloadSound("VO_NAX8_01_EMOTE4_09.prefab:d9acf9ba48ac4324e96a0c2ec232545b");
		base.PreloadSound("VO_NAX8_01_EMOTE5_10.prefab:d6d0522885dbd074b9d554dd47fddbb5");
		base.PreloadSound("VO_NAX8_01_CUSTOM2_04.prefab:10c0d0ad330cfb44eb7faa0413cd2e23");
	}

	// Token: 0x06004EFE RID: 20222 RVA: 0x0019F560 File Offset: 0x0019D760
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
						m_soundName = "VO_NAX8_01_EMOTE1_06.prefab:d000156f1cd9f7d4d816d660fc74caa0",
						m_stringTag = "VO_NAX8_01_EMOTE1_06"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE2_07.prefab:61420eb86b3febb4da7077c26b66fe82",
						m_stringTag = "VO_NAX8_01_EMOTE2_07"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE3_08.prefab:e54c3c21bf16794418b8d684af0d13ea",
						m_stringTag = "VO_NAX8_01_EMOTE3_08"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE4_09.prefab:d9acf9ba48ac4324e96a0c2ec232545b",
						m_stringTag = "VO_NAX8_01_EMOTE4_09"
					},
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX8_01_EMOTE5_10.prefab:d6d0522885dbd074b9d554dd47fddbb5",
						m_stringTag = "VO_NAX8_01_EMOTE5_10"
					}
				}
			}
		};
	}

	// Token: 0x06004EFF RID: 20223 RVA: 0x0019F643 File Offset: 0x0019D843
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_GOTHIK2_62", "VO_KT_GOTHIK2_62.prefab:0ac7f3dd8ea055b4c81abd7f25e3f782", true);
		}
		yield break;
	}

	// Token: 0x06004F00 RID: 20224 RVA: 0x0019F652 File Offset: 0x0019D852
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
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
		if (!(cardId == "NAX8_03") && !(cardId == "NAX8_04") && !(cardId == "NAX8_05"))
		{
			if (cardId == "NAX8_02")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX8_01_CUSTOM_03.prefab:4e1adb5a87d8efd45ba4fab32ba9dff1", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_unrelentingMinionLinePlayed)
			{
				yield break;
			}
			this.m_unrelentingMinionLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX8_01_CARD_02.prefab:39a2051167989ad48a234c7bfcf6bb0b", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004F01 RID: 20225 RVA: 0x0019F668 File Offset: 0x0019D868
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (missionEvent == 1)
		{
			if (this.m_deadReturnLinePlayed)
			{
				yield break;
			}
			this.m_deadReturnLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX8_01_CUSTOM2_04.prefab:10c0d0ad330cfb44eb7faa0413cd2e23", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0400453F RID: 17727
	private bool m_cardLinePlayed;

	// Token: 0x04004540 RID: 17728
	private bool m_unrelentingMinionLinePlayed;

	// Token: 0x04004541 RID: 17729
	private bool m_deadReturnLinePlayed;
}
