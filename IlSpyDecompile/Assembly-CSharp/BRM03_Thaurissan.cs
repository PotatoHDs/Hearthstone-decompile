using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRM03_Thaurissan : BRM_MissionEntity
{
	private bool m_heroPowerLinePlayed;

	private bool m_cardLinePlayed;

	private bool m_moiraDead;

	public override void PreloadAssets()
	{
		PreloadSound("VO_BRMA03_1_RESPONSE_03.prefab:e078caaf7789928409f4e3f3c4934db5");
		PreloadSound("VO_BRMA03_1_HERO_POWER_06.prefab:2ad44580bf0939c4292a8a454a6fb859");
		PreloadSound("VO_BRMA03_1_CARD_04.prefab:2ebdf13895d3b4e4e8979764b99e89e0");
		PreloadSound("VO_BRMA03_1_MOIRA_DEATH_05.prefab:917ad0f8ed4c2674aad35244c2284fc8");
		PreloadSound("VO_BRMA03_1_VS_RAG_07.prefab:bb0cc8e74eb40f44e99eacd240257b37");
		PreloadSound("VO_BRMA03_1_TURN1_02.prefab:f452794f36ba643449268981c5d6a6fc");
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
						m_soundName = "VO_BRMA03_1_RESPONSE_03.prefab:e078caaf7789928409f4e3f3c4934db5",
						m_stringTag = "VO_BRMA03_1_RESPONSE_03"
					}
				}
			}
		};
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
		if (!(cardId == "BRMA03_2"))
		{
			if (cardId == "BRMA_01" && !m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA03_1_CARD_04.prefab:2ebdf13895d3b4e4e8979764b99e89e0", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else if (!m_heroPowerLinePlayed)
		{
			m_heroPowerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA03_1_HERO_POWER_06.prefab:2ad44580bf0939c4292a8a454a6fb859", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA03_1_TURN1_02.prefab:f452794f36ba643449268981c5d6a6fc", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		yield break;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 1:
			m_moiraDead = true;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA03_1_MOIRA_DEATH_05.prefab:917ad0f8ed4c2674aad35244c2284fc8", Notification.SpeechBubbleDirection.TopRight, actor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 2:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_BRMA03_1_VS_RAG_07.prefab:bb0cc8e74eb40f44e99eacd240257b37", Notification.SpeechBubbleDirection.TopRight, actor));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			if (m_moiraDead)
			{
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_THAURISSAN_DEAD2_33"), "VO_NEFARIAN_THAURISSAN_DEAD2_33.prefab:3d40d6eb234aaa14a9bd5f6c1567dba8");
			}
			else
			{
				yield return new WaitForSeconds(5f);
				NotificationManager.Get().CreateCharacterQuote("NormalNefarian_Quote.prefab:708840e536eb141479a23b632ebcc913", GameStrings.Get("VO_NEFARIAN_THAURISSAN_DEAD_32"), "VO_NEFARIAN_THAURISSAN_DEAD_32.prefab:efd675abb496ce843985c15d96c69183");
			}
		}
	}
}
