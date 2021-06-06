using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE10_Giantfin : LOE_MissionEntity
{
	private bool m_cardLinePlayed1;

	private bool m_cardLinePlayed2;

	private bool m_nyahLinePlayed;

	private int m_turnToPlayFoundLine = -1;

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing3);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOEA10_1_MIDDLEFIN.prefab:db87360596b82634f9350b9fb516a52c");
		PreloadSound("VO_LOE10_NYAH_FINLEY.prefab:7d68f7ae697cde142ae6875d4758b0b0");
		PreloadSound("VO_LOE_10_NYAH.prefab:e9c4965c3cf4d274886180e4facf749f");
		PreloadSound("VO_LOE_10_RESPONSE.prefab:d59cf1de856198e4f9443ae4bdb2d04a");
		PreloadSound("VO_LOE_10_START_2.prefab:dec3b2452f06e4542b21afabb06cbdbf");
		PreloadSound("VO_LOE_10_TURN1.prefab:5f1e5b8506cd049419e78ee08f8143e4");
		PreloadSound("VO_LOE_10_WIN.prefab:31018ff004f803045bcb5e3af8447198");
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield return Gameplay.Get().StartCoroutine(base.HandleMissionEventWithTiming(missionEvent));
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		switch (missionEvent)
		{
		case 2:
			if (!m_cardLinePlayed2)
			{
				m_cardLinePlayed2 = true;
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA10_1_MIDDLEFIN.prefab:db87360596b82634f9350b9fb516a52c", Notification.SpeechBubbleDirection.TopRight, actor));
			}
			break;
		case 3:
			if (!m_cardLinePlayed1)
			{
				m_cardLinePlayed1 = true;
			}
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
		if (cardId == "LOEA10_5" && !m_nyahLinePlayed)
		{
			m_nyahLinePlayed = true;
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_10_NYAH.prefab:e9c4965c3cf4d274886180e4facf749f", Notification.SpeechBubbleDirection.TopRight, actor));
			yield return new WaitForSeconds(4f);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE10_NYAH_FINLEY.prefab:7d68f7ae697cde142ae6875d4758b0b0"));
		}
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
						m_soundName = "VO_LOE_10_RESPONSE.prefab:d59cf1de856198e4f9443ae4bdb2d04a",
						m_stringTag = "VO_LOE_10_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (m_turnToPlayFoundLine == 5)
		{
			m_turnToPlayFoundLine = 7;
		}
		if (turn == m_turnToPlayFoundLine)
		{
			m_turnToPlayFoundLine = -1;
		}
		else if (turn == 1)
		{
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOE_10_TURN1.prefab:5f1e5b8506cd049419e78ee08f8143e4", Notification.SpeechBubbleDirection.TopRight, actor));
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOE_10_START_2.prefab:dec3b2452f06e4542b21afabb06cbdbf"));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOE_10_WIN.prefab:31018ff004f803045bcb5e3af8447198", 0f, allowRepeatDuringSession: false));
		}
	}
}
