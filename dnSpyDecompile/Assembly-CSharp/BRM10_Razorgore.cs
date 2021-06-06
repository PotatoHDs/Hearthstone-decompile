using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200037F RID: 895
public class BRM10_Razorgore : BRM_MissionEntity
{
	// Token: 0x06003444 RID: 13380 RVA: 0x0010BA50 File Offset: 0x00109C50
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA10_1_RESPONSE_03.prefab:efb8b297966889e429ba309443fff39c");
		base.PreloadSound("VO_BRMA10_1_HERO_POWER_04.prefab:5d2db978ae659d645a110f1bbfdefc03");
		base.PreloadSound("VO_BRMA10_1_EGG_DEATH_1_05.prefab:66bc1615e7b2e2143a07fd2cdcd3bd98");
		base.PreloadSound("VO_BRMA10_1_EGG_DEATH_2_06.prefab:178e26b2103924c48b8511c2a5424bff");
		base.PreloadSound("VO_BRMA10_1_EGG_DEATH_3_07.prefab:906089c0627eed044a123bd5cd9ae38d");
		base.PreloadSound("VO_BRMA10_1_TURN1_02.prefab:470bcd541d18f5f4e99f1b2b1fd97622");
	}

	// Token: 0x06003445 RID: 13381 RVA: 0x0010BAA0 File Offset: 0x00109CA0
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
						m_soundName = "VO_BRMA10_1_RESPONSE_03.prefab:efb8b297966889e429ba309443fff39c",
						m_stringTag = "VO_BRMA10_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x0010BAFF File Offset: 0x00109CFF
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
		if (cardId == "BRMA10_3" || cardId == "BRMA10_3H")
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA10_1_HERO_POWER_04.prefab:5d2db978ae659d645a110f1bbfdefc03", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003447 RID: 13383 RVA: 0x0010BB15 File Offset: 0x00109D15
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA10_1_TURN1_02.prefab:470bcd541d18f5f4e99f1b2b1fd97622", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x06003448 RID: 13384 RVA: 0x0010BB2B File Offset: 0x00109D2B
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 1)
		{
			this.m_eggDeathLinePlayed++;
			switch (this.m_eggDeathLinePlayed)
			{
			case 1:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_1_05.prefab:66bc1615e7b2e2143a07fd2cdcd3bd98", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			case 2:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_2_06.prefab:178e26b2103924c48b8511c2a5424bff", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			case 3:
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA10_1_EGG_DEATH_3_07.prefab:906089c0627eed044a123bd5cd9ae38d", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
				break;
			}
		}
		yield break;
	}

	// Token: 0x06003449 RID: 13385 RVA: 0x0010BB41 File Offset: 0x00109D41
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_RAZORGORE_DEAD_55"), "VO_NEFARIAN_RAZORGORE_DEAD_55.prefab:3d24bf2b8eb17e8459ae6a85a7900dde", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x04001C96 RID: 7318
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C97 RID: 7319
	private int m_eggDeathLinePlayed;
}
