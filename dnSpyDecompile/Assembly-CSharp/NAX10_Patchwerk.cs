using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200058C RID: 1420
public class NAX10_Patchwerk : NAX_MissionEntity
{
	// Token: 0x06004F0A RID: 20234 RVA: 0x0019F776 File Offset: 0x0019D976
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX10_01_HP_02.prefab:b7fff1c198650934c8b2902ddd5bcdbf");
		base.PreloadSound("VO_NAX10_01_EMOTE2_05.prefab:905bdb461afbd5244954f25829e1b99b");
		base.PreloadSound("VO_NAX10_01_EMOTE1_04.prefab:c5b2fff109f63134597f357b4b67c0b9");
	}

	// Token: 0x06004F0B RID: 20235 RVA: 0x0019F79C File Offset: 0x0019D99C
	protected override void InitEmoteResponses()
	{
		this.m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>
		{
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.GREETINGS,
					EmoteType.OOPS,
					EmoteType.SORRY,
					EmoteType.THANKS,
					EmoteType.THREATEN
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX10_01_EMOTE1_04.prefab:c5b2fff109f63134597f357b4b67c0b9",
						m_stringTag = "VO_NAX10_01_EMOTE1_04"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.WELL_PLAYED
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_NAX10_01_EMOTE2_05.prefab:905bdb461afbd5244954f25829e1b99b",
						m_stringTag = "VO_NAX10_01_EMOTE2_05"
					}
				}
			}
		};
	}

	// Token: 0x06004F0C RID: 20236 RVA: 0x0019F862 File Offset: 0x0019DA62
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_PATCHWERK2_69", "VO_KT_PATCHWERK2_69.prefab:b11d9d854c9a8414693838d75f455f21", true);
		}
		yield break;
	}

	// Token: 0x06004F0D RID: 20237 RVA: 0x0019F871 File Offset: 0x0019DA71
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		if (cardId == "NAX10_03")
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			yield return new WaitForSeconds(4.5f);
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX10_01_HP_02.prefab:b7fff1c198650934c8b2902ddd5bcdbf", Notification.SpeechBubbleDirection.TopRight, enemyActor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06004F0E RID: 20238 RVA: 0x0019F887 File Offset: 0x0019DA87
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn % 2 != 0)
		{
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(1f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x04004545 RID: 17733
	private bool m_heroPowerLinePlayed;
}
