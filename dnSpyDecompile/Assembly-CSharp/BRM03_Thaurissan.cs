using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000378 RID: 888
public class BRM03_Thaurissan : BRM_MissionEntity
{
	// Token: 0x06003418 RID: 13336 RVA: 0x0010B334 File Offset: 0x00109534
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_BRMA03_1_RESPONSE_03.prefab:e078caaf7789928409f4e3f3c4934db5");
		base.PreloadSound("VO_BRMA03_1_HERO_POWER_06.prefab:2ad44580bf0939c4292a8a454a6fb859");
		base.PreloadSound("VO_BRMA03_1_CARD_04.prefab:2ebdf13895d3b4e4e8979764b99e89e0");
		base.PreloadSound("VO_BRMA03_1_MOIRA_DEATH_05.prefab:917ad0f8ed4c2674aad35244c2284fc8");
		base.PreloadSound("VO_BRMA03_1_VS_RAG_07.prefab:bb0cc8e74eb40f44e99eacd240257b37");
		base.PreloadSound("VO_BRMA03_1_TURN1_02.prefab:f452794f36ba643449268981c5d6a6fc");
	}

	// Token: 0x06003419 RID: 13337 RVA: 0x0010B384 File Offset: 0x00109584
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
						m_soundName = "VO_BRMA03_1_RESPONSE_03.prefab:e078caaf7789928409f4e3f3c4934db5",
						m_stringTag = "VO_BRMA03_1_RESPONSE_03"
					}
				}
			}
		};
	}

	// Token: 0x0600341A RID: 13338 RVA: 0x0010B3E3 File Offset: 0x001095E3
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
		if (!(cardId == "BRMA03_2"))
		{
			if (cardId == "BRMA_01")
			{
				if (this.m_cardLinePlayed)
				{
					yield break;
				}
				this.m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA03_1_CARD_04.prefab:2ebdf13895d3b4e4e8979764b99e89e0", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			if (this.m_heroPowerLinePlayed)
			{
				yield break;
			}
			this.m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA03_1_HERO_POWER_06.prefab:2ad44580bf0939c4292a8a454a6fb859", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600341B RID: 13339 RVA: 0x0010B3F9 File Offset: 0x001095F9
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA03_1_TURN1_02.prefab:f452794f36ba643449268981c5d6a6fc", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
		yield break;
	}

	// Token: 0x0600341C RID: 13340 RVA: 0x0010B40F File Offset: 0x0010960F
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA03_1_VS_RAG_07.prefab:bb0cc8e74eb40f44e99eacd240257b37", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			}
		}
		else
		{
			this.m_moiraDead = true;
			GameState.Get().SetBusy(true);
			yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_BRMA03_1_MOIRA_DEATH_05.prefab:917ad0f8ed4c2674aad35244c2284fc8", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600341D RID: 13341 RVA: 0x0010B425 File Offset: 0x00109625
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			if (this.m_moiraDead)
			{
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_THAURISSAN_DEAD2_33"), "VO_NEFARIAN_THAURISSAN_DEAD2_33.prefab:3d40d6eb234aaa14a9bd5f6c1567dba8", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
			}
			else
			{
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_THAURISSAN_DEAD_32"), "VO_NEFARIAN_THAURISSAN_DEAD_32.prefab:efd675abb496ce843985c15d96c69183", true, 0f, CanvasAnchor.BOTTOM_LEFT, false);
			}
		}
		yield break;
	}

	// Token: 0x04001C87 RID: 7303
	private bool m_heroPowerLinePlayed;

	// Token: 0x04001C88 RID: 7304
	private bool m_cardLinePlayed;

	// Token: 0x04001C89 RID: 7305
	private bool m_moiraDead;
}
