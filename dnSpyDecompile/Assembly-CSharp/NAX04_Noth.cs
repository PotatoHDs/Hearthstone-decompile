using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class NAX04_Noth : NAX_MissionEntity
{
	// Token: 0x06004EE5 RID: 20197 RVA: 0x0019F227 File Offset: 0x0019D427
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX4_01_HP_02.prefab:ef429c4ce7a413d4fa8ce390025bd388");
		base.PreloadSound("VO_NAX4_01_CARD_03.prefab:676865be5229fbb4ea8b71bb7570b6f2");
		base.PreloadSound("VO_NAX4_01_EMOTE_06.prefab:837b9665fb7727145966a74e0610ee05");
	}

	// Token: 0x06004EE6 RID: 20198 RVA: 0x0019F24C File Offset: 0x0019D44C
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
						m_soundName = "VO_NAX4_01_EMOTE_06.prefab:837b9665fb7727145966a74e0610ee05",
						m_stringTag = "VO_NAX4_01_EMOTE_06"
					}
				}
			}
		};
	}

	// Token: 0x06004EE7 RID: 20199 RVA: 0x0019F2AB File Offset: 0x0019D4AB
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_NOTH2_53", "VO_KT_NOTH2_53.prefab:0ac170c747ea31f4182b1abce130228b", true);
		}
		yield break;
	}

	// Token: 0x06004EE8 RID: 20200 RVA: 0x0019F2BA File Offset: 0x0019D4BA
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 1)
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX4_01_HP_02.prefab:ef429c4ce7a413d4fa8ce390025bd388", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004EE9 RID: 20201 RVA: 0x0019F2D0 File Offset: 0x0019D4D0
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
		if (cardId == "NAX4_05")
		{
			if (this.m_cardLinePlayed)
			{
				yield break;
			}
			this.m_cardLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX4_01_CARD_03.prefab:676865be5229fbb4ea8b71bb7570b6f2", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x04004538 RID: 17720
	private bool m_cardLinePlayed;

	// Token: 0x04004539 RID: 17721
	private bool m_heroPowerLinePlayed;
}
