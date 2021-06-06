using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_10_Deathwhisper : ICC_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private List<string> m_deathwhisperHeroPowerLines = new List<string> { "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_01.prefab:252ed7c4870613b41926094149096e8e", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_02.prefab:fcf184de484c0e74c97691d610b9344d", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_03.prefab:2ebc3096f0b78b64ba71fab42dc36a06", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_04.prefab:ff6b646b1e84dcf49b7f0aa27022ddcc" };

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPowerInitial_01.prefab:1fd7f99733fdc7441824a4dc3614434d", "VO_ICC10_Deathwhisper_Female_Lich_Intro_01.prefab:b41fad72e4e63814db02e945fa920c14", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_01.prefab:252ed7c4870613b41926094149096e8e", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_02.prefab:fcf184de484c0e74c97691d610b9344d", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_03.prefab:2ebc3096f0b78b64ba71fab42dc36a06", "VO_ICC10_Deathwhisper_Female_Lich_HeroPower_04.prefab:ff6b646b1e84dcf49b7f0aa27022ddcc", "VO_ICC10_Deathwhisper_Female_Lich_DamageByValithria_01.prefab:a22ea72abd735984bb4b072e058d231a", "VO_ICC10_LichKing_Male_Human_DamageByValithria_02.prefab:7bfb1c2d7ad27fd4f964fc0ee3434d3f", "VO_ICC10_LichKing_Male_Human_Wounded_01.prefab:a7be2d6a619215f42810c29e5b29a9c2", "VO_ICC10_LichKing_Male_Human_Wounded_02.prefab:22827dc7abbc6994b83aa5a6026b259c",
			"VO_ICC10_Deathwhisper_Female_Lich_Death_01.prefab:18cf0851d5295764f84d7ffb0f08c6a7", "VO_ICC10_LichKing_Male_Human_Turn2_01.prefab:2fd73a4ef8369824db013bb85e6f4b69", "VO_ICC10_LichKing_Male_Human_Turn2_02.prefab:93005dd907a75be47b931f28e4e93490", "VO_ICC10_LichKing_Male_Human_Win_01.prefab:72a77758412ee16489751d7d1eadc561", "VO_ICC10_LichKing_Male_Human_Lose_01.prefab:e0877aec1d0153442a1735f55a907989", "VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01.prefab:b76e9b7eee7dfd843b030c32a25d0d0e", "VO_ICC10_Deathwhisper_Female_Lich_Equality_01.prefab:32227751093e7f84c9470b3c4a81eb58", "VO_ICC10_Deathwhisper_Female_Lich_LichKing_01.prefab:b57984f69945bc6468c87c148c5249aa", "VO_ICC10_Deathwhisper_Female_Lich_DKTransform_01.prefab:e0d1e9e7a2b2d0541b53b7b44c52b913", "VO_ICC10_Deathwhisper_Female_Lich_ValithriaResurrect_01.prefab:8698cda0f3a87194ca7130d5fd1bf38c",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaDivineShield_01.prefab:36bbbf401cdb19343af09cfd1f8c6947", "VO_ICC10_Deathwhisper_Female_Lich_ValithriaTaunt_01.prefab:d569b6549813ecb4e935531fb79dab91", "VO_ICC10_Deathwhisper_Female_Lich_ValithriaWindfury_01.prefab:c4c80e77f50224a498609b11d012b85f", "VO_ICC10_Deathwhisper_Female_Lich_ValithriaBlessedChampion_01.prefab:acc4507541b1ee54ab0b2ccc71454777", "VO_ICCA10_001_Female_GreenDragon_Start_01.prefab:37b1777f5320ea94280cf846c17ae0c0", "VO_ICCA10_001_Female_GreenDragon_Healed_01.prefab:b7e1633643d89e847889da06d66051bb"
		})
		{
			PreloadSound(item);
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
						m_soundName = "VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01.prefab:b76e9b7eee7dfd843b030c32a25d0d0e",
						m_stringTag = "VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01"
					}
				}
			},
			new EmoteResponseGroup
			{
				m_triggers = new List<EmoteType> { EmoteType.START },
				m_responses = new List<EmoteResponse>
				{
					new EmoteResponse
					{
						m_soundName = "VO_ICC10_Deathwhisper_Female_Lich_Intro_01.prefab:b41fad72e4e63814db02e945fa920c14",
						m_stringTag = "VO_ICC10_Deathwhisper_Female_Lich_Intro_01"
					}
				}
			}
		};
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			m_playedLines.Add(item);
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_HeroPowerInitial_01.prefab:1fd7f99733fdc7441824a4dc3614434d");
			GameState.Get().SetBusy(busy: false);
			break;
		case 102:
			if (m_deathwhisperHeroPowerLines.Count != 0)
			{
				GameState.Get().SetBusy(busy: true);
				string text = m_deathwhisperHeroPowerLines[Random.Range(0, m_deathwhisperHeroPowerLines.Count)];
				m_deathwhisperHeroPowerLines.Remove(text);
				yield return PlayLineOnlyOnce(enemyActor, text);
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case 103:
			m_playedLines.Add(item);
			yield return PlayLineOnlyOnce(GetValithria(), "VO_ICCA10_001_Female_GreenDragon_Healed_01.prefab:b7e1633643d89e847889da06d66051bb");
			break;
		case 105:
			m_playedLines.Add(item);
			yield return PlayMissionFlavorLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_DamageByValithria_01.prefab:a22ea72abd735984bb4b072e058d231a");
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_DamageByValithria_02.prefab:7bfb1c2d7ad27fd4f964fc0ee3434d3f");
			break;
		case 106:
			m_playedLines.Add(item);
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Wounded_01.prefab:a7be2d6a619215f42810c29e5b29a9c2");
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Wounded_02.prefab:22827dc7abbc6994b83aa5a6026b259c");
			break;
		case 107:
			yield return PlayBossLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_Death_01.prefab:18cf0851d5295764f84d7ffb0f08c6a7");
			break;
		case 108:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaDivineShield_01.prefab:36bbbf401cdb19343af09cfd1f8c6947");
			break;
		case 109:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaBlessedChampion_01.prefab:acc4507541b1ee54ab0b2ccc71454777");
			break;
		case 111:
			yield return new WaitForSeconds(3f);
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaResurrect_01.prefab:8698cda0f3a87194ca7130d5fd1bf38c");
			break;
		case 112:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaTaunt_01.prefab:d569b6549813ecb4e935531fb79dab91");
			break;
		case 113:
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaWindfury_01.prefab:c4c80e77f50224a498609b11d012b85f");
			break;
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
			yield return PlayLineOnlyOnce(GetValithria(), "VO_ICCA10_001_Female_GreenDragon_Start_01.prefab:37b1777f5320ea94280cf846c17ae0c0");
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Turn2_01.prefab:2fd73a4ef8369824db013bb85e6f4b69");
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Turn2_02.prefab:93005dd907a75be47b931f28e4e93490");
			GameState.Get().SetBusy(busy: false);
			break;
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "ICC_314"))
		{
			if (cardId == "EX1_619")
			{
				yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_Equality_01.prefab:32227751093e7f84c9470b3c4a81eb58");
			}
		}
		else
		{
			yield return PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_LichKing_01.prefab:b57984f69945bc6468c87c148c5249aa");
		}
		yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_DKTransform_01.prefab:e0d1e9e7a2b2d0541b53b7b44c52b913");
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC10_LichKing_Male_Human_Win_01.prefab:72a77758412ee16489751d7d1eadc561");
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC10_LichKing_Male_Human_Lose_01.prefab:e0877aec1d0153442a1735f55a907989";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}

	private Actor GetValithria()
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == "ICCA10_001")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}
}
