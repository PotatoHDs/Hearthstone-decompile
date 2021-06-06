using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE01_Zinaar : LOE_MissionEntity
{
	private bool m_wishMoreWishesLinePlayed;

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOE_02_RESPONSE.prefab:81df49b8799ffe7408d1ca6d13a0b1a9");
		PreloadSound("VO_LOE_02_WISH.prefab:e58bbad31e7b0e944a7c5ae8c67a6837");
		PreloadSound("VO_LOE_02_START2.prefab:3f963f60c4ecb2341a001d4a8e80e4f0");
		PreloadSound("VO_LOE_02_START3.prefab:bf687ef7a2095b44bb25ff73d6795d90");
		PreloadSound("VO_LOE_02_TURN_6.prefab:d283d2a06fd1f4146a091e13e097492b");
		PreloadSound("VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2.prefab:240f36f0d8777774387afe495f53b2d5");
		PreloadSound("VO_LOE_02_TURN_6_2.prefab:1bea540bd8f9f134c92c6841cd6e564d");
		PreloadSound("VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2_ALT.prefab:13b73e3d2ea111f439b866c0da62773c");
		PreloadSound("VO_LOE_02_TURN_10.prefab:9c92e0752f28be44d902d98ea9617ab1");
		PreloadSound("VO_LOE_ZINAAR_TURN_10_CARTOGRAPHER_2.prefab:f4a2d6f5c7786cc4297a73fef98937fd");
		PreloadSound("VO_LOE_02_WIN.prefab:f311289fe2c9b5446943c3cb69f402da");
		PreloadSound("VO_LOE_02_MORE_WISHES.prefab:67291015ffb2c8f44b3e20553f25001b");
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
						m_soundName = "VO_LOE_02_RESPONSE.prefab:81df49b8799ffe7408d1ca6d13a0b1a9",
						m_stringTag = "VO_LOE_02_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		if (missionEvent == 2)
		{
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_WISH.prefab:e58bbad31e7b0e944a7c5ae8c67a6837"));
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		string cardId = entity.GetCardId();
		if (cardId == "LOEA02_06" && !m_wishMoreWishesLinePlayed)
		{
			m_wishMoreWishesLinePlayed = true;
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_MORE_WISHES.prefab:67291015ffb2c8f44b3e20553f25001b"));
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_START2.prefab:3f963f60c4ecb2341a001d4a8e80e4f0"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_02_START3.prefab:bf687ef7a2095b44bb25ff73d6795d90"));
			break;
		case 7:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_6.prefab:d283d2a06fd1f4146a091e13e097492b"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2.prefab:240f36f0d8777774387afe495f53b2d5"));
			break;
		case 9:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_6_2.prefab:1bea540bd8f9f134c92c6841cd6e564d"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_6_CARTOGRAPHER_2_ALT.prefab:13b73e3d2ea111f439b866c0da62773c"));
			break;
		case 13:
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Reno_BigQuote.prefab:63a25676d5e84264a9eb9c3d5c7e0921", "VO_LOE_02_TURN_10.prefab:9c92e0752f28be44d902d98ea9617ab1"));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Elise_BigQuote.prefab:932bc9e74bb49e047ae8dd480492db26", "VO_LOE_ZINAAR_TURN_10_CARTOGRAPHER_2.prefab:f4a2d6f5c7786cc4297a73fef98937fd"));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Reno_Quote.prefab:0a2e34fa6782a0747b4f5d5574d1331a", "VO_LOE_02_WIN.prefab:f311289fe2c9b5446943c3cb69f402da", 0f, allowRepeatDuringSession: false));
		}
	}
}
