using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE15_Boss1 : LOE_MissionEntity
{
	private bool m_magmaRagerLinePlayed;

	private bool m_lowHealth;

	private List<Zone> m_zonesToHide = new List<Zone>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_15_RESPONSE.prefab:a8accce27ae78cb4fb4b02f58e9c3036");
		PreloadSound("VO_LOEA15_1_LOW_HEALTH_10.prefab:a948e1a494daa01429f9c8af98ba2ba7");
		PreloadSound("VO_LOEA15_1_TURN1_08.prefab:6a6f93dab22e2aa44bafd03d5da8597a");
		PreloadSound("VO_LOEA15_1_MAGMA_RAGER_09.prefab:86f34787a5e7d5441a6331443d36880b");
		PreloadSound("VO_LOEA15_1_LOSS_11.prefab:7a3d507088374a240a490fc098f6791b");
		PreloadSound("VO_LOEA15_1_WIN_12.prefab:bdad6f025366f8645a65655e1d8fc751");
		PreloadSound("VO_LOEA15_GOLDEN.prefab:c7173172b70c5ca40b1f3157ee3e5279");
		PreloadSound("VO_LOEA15_1_START_07.prefab:663467aee38cfa3429a9f89eee6177fe");
		PreloadSound("VO_LOE_15_SPARE.prefab:a25a36df79b66394ebb054c12e4a9441");
		PreloadSound("VO_ELISE_WEIRD_DECK_05.prefab:89915bf24c91ba642bf3ae842e267fe3");
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
						m_soundName = "VO_LOE_15_RESPONSE.prefab:a8accce27ae78cb4fb4b02f58e9c3036",
						m_stringTag = "VO_LOE_15_RESPONSE"
					}
				}
			}
		};
	}

	public override bool DoAlternateMulliganIntro()
	{
		GameState.Get().GetOpposingSidePlayer().GetDeckZone()
			.SetVisibility(visible: false);
		m_zonesToHide.Clear();
		m_zonesToHide.AddRange(ZoneMgr.Get().FindZonesForTag(TAG_ZONE.HAND));
		m_zonesToHide.AddRange(ZoneMgr.Get().FindZonesForTag(TAG_ZONE.DECK));
		foreach (Zone item in m_zonesToHide)
		{
			foreach (Card card in item.GetCards())
			{
				card.HideCard();
				card.SetDoNotSort(on: true);
			}
		}
		return false;
	}

	public override IEnumerator DoActionsAfterIntroBeforeMulligan()
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor enemyActor = opposingSidePlayer.GetHeroCard().GetActor();
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("LOE_DeckTakeEvent.prefab:1d55e39305085094cbe8598e5fde37aa");
		LOE_DeckTakeEvent deckTakeEvent = gameObject.GetComponent<LOE_DeckTakeEvent>();
		yield return new WaitForEndOfFrame();
		ZoneDeck deckZone = GameState.Get().GetOpposingSidePlayer().GetDeckZone();
		deckZone.SetVisibility(visible: true);
		Gameplay.Get().SwapCardBacks();
		deckZone.SetVisibility(visible: false);
		GameState.Get().GetFriendlySidePlayer().GetDeckZone()
			.SetVisibility(visible: false);
		Gameplay.Get().StartCoroutine(deckTakeEvent.PlayTakeDeckAnim());
		yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_LOEA15_1_START_07.prefab:663467aee38cfa3429a9f89eee6177fe", Notification.SpeechBubbleDirection.TopRight, enemyActor));
		while (deckTakeEvent.AnimIsPlaying())
		{
			yield return null;
		}
		Gameplay.Get().StartCoroutine(deckTakeEvent.PlayReplacementDeckAnim());
		yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_15_SPARE.prefab:a25a36df79b66394ebb054c12e4a9441"));
		while (deckTakeEvent.AnimIsPlaying())
		{
			yield return null;
		}
		foreach (Zone item in m_zonesToHide)
		{
			foreach (Card card in item.GetCards())
			{
				card.ShowCard();
				card.SetDoNotSort(on: false);
			}
		}
		m_zonesToHide.Clear();
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		Actor actor = opposingSidePlayer.GetHeroCard().GetActor();
		if (!m_lowHealth && opposingSidePlayer.GetHero().GetCurrentHealth() < 10 && GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCurrentHealth() > 19)
		{
			m_lowHealth = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_LOW_HEALTH_10.prefab:a948e1a494daa01429f9c8af98ba2ba7", Notification.SpeechBubbleDirection.TopRight, actor));
			yield break;
		}
		switch (turn)
		{
		case 1:
			if (GameState.Get().GetGameEntity().GetCost() == 1)
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA15_GOLDEN.prefab:c7173172b70c5ca40b1f3157ee3e5279", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_TURN1_08.prefab:6a6f93dab22e2aa44bafd03d5da8597a", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case 5:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_ELISE_WEIRD_DECK_05.prefab:89915bf24c91ba642bf3ae842e267fe3"));
			break;
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
		if (cardId == "CS2_118" && !m_magmaRagerLinePlayed)
		{
			m_magmaRagerLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA15_1_MAGMA_RAGER_09.prefab:86f34787a5e7d5441a6331443d36880b", Notification.SpeechBubbleDirection.TopRight, actor));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (!GameMgr.Get().IsClassChallengeMission())
		{
			switch (gameResult)
			{
			case TAG_PLAYSTATE.WON:
				yield return new WaitForSeconds(5f);
				Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b", "VO_LOEA15_1_LOSS_11.prefab:7a3d507088374a240a490fc098f6791b", 0f, allowRepeatDuringSession: false));
				break;
			case TAG_PLAYSTATE.LOST:
				yield return new WaitForSeconds(5f);
				Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Rafaam_wrap_Quote.prefab:d7100015bf618604ea93bad6b9f54f8b", "VO_LOEA15_1_WIN_12.prefab:bdad6f025366f8645a65655e1d8fc751", 0f, allowRepeatDuringSession: false));
				break;
			}
		}
	}
}
