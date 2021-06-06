using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOE09_LordSlitherspear : LOE_MissionEntity
{
	private bool m_finley_death_line;

	private bool m_finley_saved;

	private Card m_cauldronCard;

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_LOE_Wing3);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_LOEA09_1_RESPONSE.prefab:69af0af0192a1024eb47158a3af3f40a");
		PreloadSound("VO_LOEA09_UNSTABLE.prefab:2ba176281f83bb7418d2c01df2e900bc");
		PreloadSound("VO_LOEA09_HERO_POWER.prefab:e752937402315d043b487ed44f4a461a");
		PreloadSound("FX_MinionSummon_Cast.prefab:d0a0997a72042914f8779e138bb2755e");
		PreloadSound("VO_LOEA09_QUOTE1.prefab:ade9f08ae927cc94082169c6b71c13b9");
		PreloadSound("VO_LOEA09_FINLEY_DEATH.prefab:47571c42e9fc0014a94352f1eb0a3a1f");
		PreloadSound("VO_LOEA09_HERO_POWER1.prefab:4f4322070683ced41a83c03c0527ee0e");
		PreloadSound("VO_LOEA09_HERO_POWER2.prefab:bab01103d3d6d894b81e103efe850c11");
		PreloadSound("VO_LOEA09_HERO_POWER3.prefab:7ae0457d447b24e4c8c17ab4120d40cc");
		PreloadSound("VO_LOEA09_HERO_POWER4.prefab:1d497fab014f3c64c81ef834d8ee009b");
		PreloadSound("VO_LOEA09_HERO_POWER5.prefab:ef08dadc1d255714ab3105ab1b5cd2a8");
		PreloadSound("VO_LOEA09_HERO_POWER6.prefab:6ca24415fc389714885735792c3ef46c");
		PreloadSound("VO_LOEA09_WIN.prefab:add3634f635104d49987f553e3f83a8c");
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
						m_soundName = "VO_LOEA09_1_RESPONSE.prefab:69af0af0192a1024eb47158a3af3f40a",
						m_stringTag = "VO_LOEA09_1_RESPONSE"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (missionEvent)
		{
		case 1:
			m_finley_saved = true;
			GameState.Get().SetBusy(busy: true);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_QUOTE1.prefab:ade9f08ae927cc94082169c6b71c13b9"));
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_UNSTABLE.prefab:2ba176281f83bb7418d2c01df2e900bc", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
			if (!m_finley_death_line)
			{
				m_finley_death_line = true;
				GameState.Get().SetBusy(busy: true);
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_FINLEY_DEATH.prefab:47571c42e9fc0014a94352f1eb0a3a1f"));
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		if (m_finley_saved)
		{
			yield break;
		}
		if (m_cauldronCard == null)
		{
			int tag = GameState.Get().GetGameEntity().GetTag(GAME_TAG.TAG_SCRIPT_DATA_ENT_1);
			Entity entity = GameState.Get().GetEntity(tag);
			if (entity != null)
			{
				m_cauldronCard = entity.GetCard();
			}
		}
		if ((m_cauldronCard == null && turn > 1) || (m_cauldronCard != null && m_cauldronCard.GetEntity().GetZone() != TAG_ZONE.PLAY))
		{
			yield break;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		while (m_enemySpeaking)
		{
			yield return null;
		}
		switch (turn)
		{
		case 1:
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER.prefab:e752937402315d043b487ed44f4a461a", Notification.SpeechBubbleDirection.TopRight, enemyActor));
			if (!(m_cauldronCard == null))
			{
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWaitOnce("Finley_BigQuote.prefab:1c1c332cf5009194cb7dd7316c465aee", "VO_LOEA09_HERO_POWER1.prefab:4f4322070683ced41a83c03c0527ee0e"));
			}
			break;
		case 4:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER2.prefab:bab01103d3d6d894b81e103efe850c11", Notification.SpeechBubbleDirection.TopRight, m_cauldronCard.GetActor()));
			break;
		case 6:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER3.prefab:7ae0457d447b24e4c8c17ab4120d40cc", Notification.SpeechBubbleDirection.TopRight, m_cauldronCard.GetActor()));
			break;
		case 8:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER4.prefab:1d497fab014f3c64c81ef834d8ee009b", Notification.SpeechBubbleDirection.TopRight, m_cauldronCard.GetActor()));
			break;
		case 10:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER5.prefab:ef08dadc1d255714ab3105ab1b5cd2a8", Notification.SpeechBubbleDirection.TopRight, m_cauldronCard.GetActor()));
			break;
		case 12:
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeechOnce("VO_LOEA09_HERO_POWER6.prefab:6ca24415fc389714885735792c3ef46c", Notification.SpeechBubbleDirection.TopRight, m_cauldronCard.GetActor()));
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON && !GameMgr.Get().IsClassChallengeMission())
		{
			yield return new WaitForSeconds(5f);
			yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Blaggh_Quote.prefab:f5d1e7053e6368e4a930ca3906cff53a", "VO_LOEA09_WIN.prefab:add3634f635104d49987f553e3f83a8c", 0f, allowRepeatDuringSession: false));
		}
	}
}
