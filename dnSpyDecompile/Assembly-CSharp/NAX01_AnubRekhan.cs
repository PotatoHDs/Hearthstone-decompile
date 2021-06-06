using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000583 RID: 1411
public class NAX01_AnubRekhan : NAX_MissionEntity
{
	// Token: 0x06004ED3 RID: 20179 RVA: 0x0019EF10 File Offset: 0x0019D110
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_NAX1_01_HP_03.prefab:acb19fa6cb04b424b950583d2e71293e");
		base.PreloadSound("VO_NAX1_01_CARD_02.prefab:ed8f928f8289afd4b964eea03bf348fa");
		base.PreloadSound("VO_NAX1_01_EMOTE_04.prefab:ab367aeaf987d9a43ba10d474fa1e792");
	}

	// Token: 0x06004ED4 RID: 20180 RVA: 0x0019EF34 File Offset: 0x0019D134
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
						m_soundName = "VO_NAX1_01_EMOTE_04.prefab:ab367aeaf987d9a43ba10d474fa1e792",
						m_stringTag = "VO_NAX1_01_EMOTE_04"
					}
				}
			}
		};
	}

	// Token: 0x06004ED5 RID: 20181 RVA: 0x0019EF93 File Offset: 0x0019D193
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_ANUB2_43", "VO_KT_ANUB2_43.prefab:243d9a81662680b4dae85648d8f38f54", true);
		}
		yield break;
	}

	// Token: 0x06004ED6 RID: 20182 RVA: 0x0019EFA2 File Offset: 0x0019D1A2
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
		if (!(cardId == "NAX1_04"))
		{
			if (cardId == "NAX1_05")
			{
				if (this.m_locustSwarmLinePlayed)
				{
					yield break;
				}
				this.m_locustSwarmLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX1_01_CARD_02.prefab:ed8f928f8289afd4b964eea03bf348fa", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_NAX1_01_HP_03.prefab:acb19fa6cb04b424b950583d2e71293e", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x04004532 RID: 17714
	private bool m_locustSwarmLinePlayed;

	// Token: 0x04004533 RID: 17715
	private bool m_heroPowerLinePlayed;
}
