using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE08_Archaedas : LOE_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_08_RESPONSE.prefab:4efe606a6afa83b459855b0d4566f17e");
		PreloadSound("VO_LOEA08_TURN_1_BRANN.prefab:c48fbd5e80d73194c91ead50e9ee20ef");
		PreloadSound("VO_LOE_ARCHAEDAS_TURN_1_CARTOGRAPHER.prefab:1fbc6b6415f7c604cb00d0b88651e303");
		PreloadSound("VO_LOE_08_LANDSLIDE.prefab:c56764ff130183f4688c0dfb30eaf8b2");
		PreloadSound("VO_LOE_08_ANIMATE_STONE.prefab:75c31e408053e0748aae95242e662f27");
		PreloadSound("VO_LOE_08_WIN.prefab:d40a0f7dc56bcf74692815bb06710a00");
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
						m_soundName = "VO_LOE_08_RESPONSE.prefab:4efe606a6afa83b459855b0d4566f17e",
						m_stringTag = "VO_LOE_08_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Brann_BigQuote.prefab:a03dd286404083c439e371ba84d7a82b", "VO_LOEA08_TURN_1_BRANN.prefab:c48fbd5e80d73194c91ead50e9ee20ef"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ARCHAEDAS_TURN_1_CARTOGRAPHER.prefab:1fbc6b6415f7c604cb00d0b88651e303", 5f));
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = entity.GetCardId();
		if (!(cardId == "LOEA06_04"))
		{
			if (cardId == "LOEA06_03")
			{
				m_playedLines.Add(cardId);
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_08_ANIMATE_STONE.prefab:75c31e408053e0748aae95242e662f27", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
		else
		{
			m_playedLines.Add(cardId);
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_08_LANDSLIDE.prefab:c56764ff130183f4688c0dfb30eaf8b2", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Brann_Quote.prefab:2c11651ab7740924189734944b8d7089", "VO_LOE_08_WIN.prefab:d40a0f7dc56bcf74692815bb06710a00", 0f, allowRepeatDuringSession: false));
		}
	}
}
