using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003AD RID: 941
public class ICC_10_Deathwhisper : ICC_MissionEntity
{
	// Token: 0x060035BF RID: 13759 RVA: 0x00111CE0 File Offset: 0x0010FEE0
	public override void PreloadAssets()
	{
		foreach (string soundPath in new List<string>
		{
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPowerInitial_01.prefab:1fd7f99733fdc7441824a4dc3614434d",
			"VO_ICC10_Deathwhisper_Female_Lich_Intro_01.prefab:b41fad72e4e63814db02e945fa920c14",
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_01.prefab:252ed7c4870613b41926094149096e8e",
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_02.prefab:fcf184de484c0e74c97691d610b9344d",
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_03.prefab:2ebc3096f0b78b64ba71fab42dc36a06",
			"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_04.prefab:ff6b646b1e84dcf49b7f0aa27022ddcc",
			"VO_ICC10_Deathwhisper_Female_Lich_DamageByValithria_01.prefab:a22ea72abd735984bb4b072e058d231a",
			"VO_ICC10_LichKing_Male_Human_DamageByValithria_02.prefab:7bfb1c2d7ad27fd4f964fc0ee3434d3f",
			"VO_ICC10_LichKing_Male_Human_Wounded_01.prefab:a7be2d6a619215f42810c29e5b29a9c2",
			"VO_ICC10_LichKing_Male_Human_Wounded_02.prefab:22827dc7abbc6994b83aa5a6026b259c",
			"VO_ICC10_Deathwhisper_Female_Lich_Death_01.prefab:18cf0851d5295764f84d7ffb0f08c6a7",
			"VO_ICC10_LichKing_Male_Human_Turn2_01.prefab:2fd73a4ef8369824db013bb85e6f4b69",
			"VO_ICC10_LichKing_Male_Human_Turn2_02.prefab:93005dd907a75be47b931f28e4e93490",
			"VO_ICC10_LichKing_Male_Human_Win_01.prefab:72a77758412ee16489751d7d1eadc561",
			"VO_ICC10_LichKing_Male_Human_Lose_01.prefab:e0877aec1d0153442a1735f55a907989",
			"VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01.prefab:b76e9b7eee7dfd843b030c32a25d0d0e",
			"VO_ICC10_Deathwhisper_Female_Lich_Equality_01.prefab:32227751093e7f84c9470b3c4a81eb58",
			"VO_ICC10_Deathwhisper_Female_Lich_LichKing_01.prefab:b57984f69945bc6468c87c148c5249aa",
			"VO_ICC10_Deathwhisper_Female_Lich_DKTransform_01.prefab:e0d1e9e7a2b2d0541b53b7b44c52b913",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaResurrect_01.prefab:8698cda0f3a87194ca7130d5fd1bf38c",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaDivineShield_01.prefab:36bbbf401cdb19343af09cfd1f8c6947",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaTaunt_01.prefab:d569b6549813ecb4e935531fb79dab91",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaWindfury_01.prefab:c4c80e77f50224a498609b11d012b85f",
			"VO_ICC10_Deathwhisper_Female_Lich_ValithriaBlessedChampion_01.prefab:acc4507541b1ee54ab0b2ccc71454777",
			"VO_ICCA10_001_Female_GreenDragon_Start_01.prefab:37b1777f5320ea94280cf846c17ae0c0",
			"VO_ICCA10_001_Female_GreenDragon_Healed_01.prefab:b7e1633643d89e847889da06d66051bb"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x060035C0 RID: 13760 RVA: 0x00111E50 File Offset: 0x00110050
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
						m_soundName = "VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01.prefab:b76e9b7eee7dfd843b030c32a25d0d0e",
						m_stringTag = "VO_ICC10_Deathwhisper_Female_Lich_EmoteResponse_01"
					}
				}
			},
			new MissionEntity.EmoteResponseGroup
			{
				m_triggers = new List<EmoteType>
				{
					EmoteType.START
				},
				m_responses = new List<MissionEntity.EmoteResponse>
				{
					new MissionEntity.EmoteResponse
					{
						m_soundName = "VO_ICC10_Deathwhisper_Female_Lich_Intro_01.prefab:b41fad72e4e63814db02e945fa920c14",
						m_stringTag = "VO_ICC10_Deathwhisper_Female_Lich_Intro_01"
					}
				}
			}
		};
	}

	// Token: 0x060035C1 RID: 13761 RVA: 0x00111EF8 File Offset: 0x001100F8
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_HeroPowerInitial_01.prefab:1fd7f99733fdc7441824a4dc3614434d", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 102:
			if (this.m_deathwhisperHeroPowerLines.Count != 0)
			{
				GameState.Get().SetBusy(true);
				string text = this.m_deathwhisperHeroPowerLines[UnityEngine.Random.Range(0, this.m_deathwhisperHeroPowerLines.Count)];
				this.m_deathwhisperHeroPowerLines.Remove(text);
				yield return base.PlayLineOnlyOnce(enemyActor, text, 2.5f);
				GameState.Get().SetBusy(false);
			}
			break;
		case 103:
			this.m_playedLines.Add(item);
			yield return base.PlayLineOnlyOnce(this.GetValithria(), "VO_ICCA10_001_Female_GreenDragon_Healed_01.prefab:b7e1633643d89e847889da06d66051bb", 2.5f);
			break;
		case 105:
			this.m_playedLines.Add(item);
			yield return base.PlayMissionFlavorLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_DamageByValithria_01.prefab:a22ea72abd735984bb4b072e058d231a", 2.5f);
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_DamageByValithria_02.prefab:7bfb1c2d7ad27fd4f964fc0ee3434d3f", 2.5f);
			break;
		case 106:
			this.m_playedLines.Add(item);
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Wounded_01.prefab:a7be2d6a619215f42810c29e5b29a9c2", 2.5f);
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Wounded_02.prefab:22827dc7abbc6994b83aa5a6026b259c", 2.5f);
			break;
		case 107:
			yield return base.PlayBossLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_Death_01.prefab:18cf0851d5295764f84d7ffb0f08c6a7", 2.5f);
			break;
		case 108:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaDivineShield_01.prefab:36bbbf401cdb19343af09cfd1f8c6947", 2.5f);
			break;
		case 109:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaBlessedChampion_01.prefab:acc4507541b1ee54ab0b2ccc71454777", 2.5f);
			break;
		case 111:
			yield return new WaitForSeconds(3f);
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaResurrect_01.prefab:8698cda0f3a87194ca7130d5fd1bf38c", 2.5f);
			break;
		case 112:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaTaunt_01.prefab:d569b6549813ecb4e935531fb79dab91", 2.5f);
			break;
		case 113:
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_ValithriaWindfury_01.prefab:c4c80e77f50224a498609b11d012b85f", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x060035C2 RID: 13762 RVA: 0x00111F0E File Offset: 0x0011010E
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			if (turn == 2)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Turn2_01.prefab:2fd73a4ef8369824db013bb85e6f4b69", 2.5f);
				yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC10_LichKing_Male_Human_Turn2_02.prefab:93005dd907a75be47b931f28e4e93490", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			yield return base.PlayLineOnlyOnce(this.GetValithria(), "VO_ICCA10_001_Female_GreenDragon_Start_01.prefab:37b1777f5320ea94280cf846c17ae0c0", 2.5f);
		}
		yield break;
	}

	// Token: 0x060035C3 RID: 13763 RVA: 0x00111F24 File Offset: 0x00110124
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "ICC_314"))
		{
			if (cardId == "EX1_619")
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_Equality_01.prefab:32227751093e7f84c9470b3c4a81eb58", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_LichKing_01.prefab:b57984f69945bc6468c87c148c5249aa", 2.5f);
		}
		yield return base.IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC10_Deathwhisper_Female_Lich_DKTransform_01.prefab:e0d1e9e7a2b2d0541b53b7b44c52b913");
		yield break;
	}

	// Token: 0x060035C4 RID: 13764 RVA: 0x00111F3A File Offset: 0x0011013A
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC10_LichKing_Male_Human_Win_01.prefab:72a77758412ee16489751d7d1eadc561", 2.5f);
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC10_LichKing_Male_Human_Lose_01.prefab:e0877aec1d0153442a1735f55a907989";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x060035C5 RID: 13765 RVA: 0x00111F50 File Offset: 0x00110150
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

	// Token: 0x04001D09 RID: 7433
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001D0A RID: 7434
	private List<string> m_deathwhisperHeroPowerLines = new List<string>
	{
		"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_01.prefab:252ed7c4870613b41926094149096e8e",
		"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_02.prefab:fcf184de484c0e74c97691d610b9344d",
		"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_03.prefab:2ebc3096f0b78b64ba71fab42dc36a06",
		"VO_ICC10_Deathwhisper_Female_Lich_HeroPower_04.prefab:ff6b646b1e84dcf49b7f0aa27022ddcc"
	};
}
