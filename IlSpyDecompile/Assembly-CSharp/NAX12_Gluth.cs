using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NAX12_Gluth : NAX_MissionEntity
{
	private bool m_cardLinePlayed;

	private int m_heroPowerLinePlayed;

	private bool m_achievementTauntPlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_NAX12_01_HP_01.prefab:f2dea305f5fc82340ac4a42cc9ba7d8e");
		PreloadSound("VO_NAX12_01_EMOTE_03.prefab:1cabacf1a4889364ca9abdeddf452cab");
		PreloadSound("VO_NAX12_01_EMOTE_02.prefab:afb88d065639fd04ba9d2af2203d1ec6");
		PreloadSound("VO_NAX12_01_EMOTE_01.prefab:b3cf5e1f56a2b3847bac8d02ae55be18");
		PreloadSound("VO_NAX12_01_CARD_01.prefab:ecf464bc8e10cb44bb2a5b7967f534aa");
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
						m_soundName = "VO_NAX12_01_EMOTE_01.prefab:b3cf5e1f56a2b3847bac8d02ae55be18",
						m_stringTag = "VO_NAX12_01_EMOTE_01"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX12_01_EMOTE_02.prefab:afb88d065639fd04ba9d2af2203d1ec6",
						m_stringTag = "VO_NAX12_01_EMOTE_02"
					},
					new EmoteResponse
					{
						m_soundName = "VO_NAX12_01_EMOTE_03.prefab:1cabacf1a4889364ca9abdeddf452cab",
						m_stringTag = "VO_NAX12_01_EMOTE_03"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		switch (turn)
		{
		case 1:
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH2_73", "VO_KT_GLUTH2_73.prefab:acc2443dca75c1d439e33556405919a5", allowRepeatDuringSession: false);
			break;
		case 13:
			m_achievementTauntPlayed = true;
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH2_ALT_74", "VO_KT_GLUTH2_ALT_74.prefab:92a3d1d1ad601824b9bfd762584a7bbe", allowRepeatDuringSession: false);
			break;
		}
		yield break;
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH4_76", "VO_KT_GLUTH4_76.prefab:8287f5fc21b0b0942a6be5eb232bd187");
		}
		if (gameResult == TAG_PLAYSTATE.LOST && m_achievementTauntPlayed)
		{
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH3_75", "VO_KT_GLUTH3_75.prefab:a53397fa1f87755499d3dcec8472c812", allowRepeatDuringSession: false);
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
		if (!(cardId == "NAX12_02"))
		{
			if (cardId == "NAX12_04" && !m_cardLinePlayed)
			{
				m_cardLinePlayed = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX12_01_CARD_01.prefab:ecf464bc8e10cb44bb2a5b7967f534aa", Notification.SpeechBubbleDirection.TopRight, actor));
				yield return new WaitForSeconds(1f);
				NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH6_78", "VO_KT_GLUTH6_78.prefab:261d18f3504d8204bbca57e9a2ea89e4", allowRepeatDuringSession: false);
			}
		}
		else if (m_heroPowerLinePlayed <= 2)
		{
			m_heroPowerLinePlayed++;
			if (m_heroPowerLinePlayed == 1)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_NAX12_01_HP_01.prefab:f2dea305f5fc82340ac4a42cc9ba7d8e", Notification.SpeechBubbleDirection.TopRight, actor));
				yield break;
			}
			yield return new WaitForSeconds(2f);
			NotificationManager.Get().CreateKTQuote("VO_KT_GLUTH5_77", "VO_KT_GLUTH5_77.prefab:28d06adaae1843c43831aed6f1e1fb2f", allowRepeatDuringSession: false);
		}
	}
}
