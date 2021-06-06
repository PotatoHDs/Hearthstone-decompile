using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A7 RID: 935
public class ICC_04_Sindragosa : ICC_MissionEntity
{
	// Token: 0x0600358B RID: 13707 RVA: 0x0010FF08 File Offset: 0x0010E108
	public override void PreloadAssets()
	{
		foreach (string soundPath in new List<string>
		{
			"VO_ICC04_Sindragosa_Female_Dragon_Intro_01.prefab:528bdc19dcdbe164c9287634a6909ddc",
			"VO_ICC04_Sindragosa_Female_Dragon_PlayerPlaysMinion_02.prefab:ea852ea6a4e59da479ffaff920b02d89",
			"VO_ICC04_LichKing_Male_Human_BreathNoMinion_01.prefab:e5757d128803add49b1776b91e3ff1d8",
			"VO_ICC04_Sindragosa_Female_Dragon_BreathNoRoomLeft_01.prefab:2fbd9d2b20a240742ad7204aaa8f0f58",
			"VO_ICC04_LichKing_Male_Human_Turn5_01.prefab:0eba2d9a9381aaf40bdc4c6a565ad3bd",
			"VO_ICC04_LichKing_Male_Human_Turn5_02.prefab:2c31e9716459f874d9669aa26770b505",
			"VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_02.prefab:5407a8811fc337e4b9568258f2f586ec",
			"VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_04.prefab:4edcfb445a8569a4f8952f68eb82f9dc",
			"VO_ICC04_Sindragosa_Female_Dragon_SecondBreath_01.prefab:de6055ac3730d8f4aac6cd124780e00e",
			"VO_ICC04_Sindragosa_Female_Dragon_Death_01.prefab:3d8e08533b9f43f4abe38d847f1b4c44",
			"VO_ICC04_LichKing_Male_Human_Win_02.prefab:5400c41ce0d52c946b0f224f6d661042",
			"VO_ICC04_LichKing_Male_Human_Lose_01.prefab:50ad5afed627fb84d8b7bd6984a60c58",
			"VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01.prefab:3cff598aa476b5c4aab273531048deeb",
			"VO_ICC04_Sindragosa_Female_Dragon_LichKing_01.prefab:bde123d0e499c4f4ca4f3e52b35173b0",
			"VO_ICC04_Sindragosa_Female_Dragon_TransformDK_01.prefab:917feae6ace238f4b9fe685c1d95a2bd",
			"VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_01.prefab:39dfe421e9ae2c24c8cdaf52fd993717",
			"VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_02.prefab:8cae1ffdacabe1546bb9f65b744b15af",
			"VO_ICC04_Sindragosa_Female_Dragon_Bounce_01.prefab:39ba1793378a6d244bcad2e2aedc94d9",
			"VO_ICC04_Sindragosa_Female_Dragon_Malygos_01.prefab:5fedfe93360a076448f5853f98e8fb6f"
		})
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x0011002C File Offset: 0x0010E22C
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
						m_soundName = "VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01.prefab:3cff598aa476b5c4aab273531048deeb",
						m_stringTag = "VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01"
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
						m_soundName = "VO_ICC04_Sindragosa_Female_Dragon_Intro_01.prefab:528bdc19dcdbe164c9287634a6909ddc",
						m_stringTag = "VO_ICC04_Sindragosa_Female_Dragon_Intro_01"
					}
				}
			}
		};
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x001100D4 File Offset: 0x0010E2D4
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 102:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_02.prefab:5407a8811fc337e4b9568258f2f586ec", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 103:
			this.m_playedLines.Add(item);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_04.prefab:4edcfb445a8569a4f8952f68eb82f9dc", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 104:
			this.m_playedLines.Add(item);
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_BreathNoMinion_01.prefab:e5757d128803add49b1776b91e3ff1d8", 2.5f);
			break;
		case 106:
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_BreathNoRoomLeft_01.prefab:2fbd9d2b20a240742ad7204aaa8f0f58", 2.5f);
			break;
		case 108:
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_SecondBreath_01.prefab:de6055ac3730d8f4aac6cd124780e00e", 2.5f);
			break;
		case 109:
			yield return base.PlayBossLine(actor, "VO_ICC04_Sindragosa_Female_Dragon_Death_01.prefab:3d8e08533b9f43f4abe38d847f1b4c44", 2.5f);
			break;
		case 110:
			this.m_playedLines.Add(item);
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_PlayerPlaysMinion_02.prefab:ea852ea6a4e59da479ffaff920b02d89", 2.5f);
			break;
		case 111:
			yield return base.PlayBossLine(actor, "VO_ICC04_Sindragosa_Female_Dragon_Bounce_01.prefab:39ba1793378a6d244bcad2e2aedc94d9", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x001100EA File Offset: 0x0010E2EA
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn == 5)
		{
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_Turn5_01.prefab:0eba2d9a9381aaf40bdc4c6a565ad3bd", 2.5f);
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_Turn5_02.prefab:2c31e9716459f874d9669aa26770b505", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x00110100 File Offset: 0x0010E300
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
		string cardID = entity.GetCardId();
		this.m_playedLines.Add(cardID);
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		yield return base.WaitForEntitySoundToFinish(entity);
		string a = cardID;
		if (!(a == "ICC_314"))
		{
			if (!(a == "EX1_563"))
			{
				if (a == "ICC_838")
				{
					Gameplay.Get().StartCoroutine(base.PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_01.prefab:39dfe421e9ae2c24c8cdaf52fd993717", 2.5f));
					yield return new WaitForSeconds(0.2f);
					yield return base.PlayEasterEggLine(base.GetActorByCardId("ICC_838"), "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_02.prefab:8cae1ffdacabe1546bb9f65b744b15af", 2.5f);
				}
			}
			else
			{
				yield return base.PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_Malygos_01.prefab:5fedfe93360a076448f5853f98e8fb6f", 2.5f);
			}
		}
		else
		{
			yield return base.PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_LichKing_01.prefab:bde123d0e499c4f4ca4f3e52b35173b0", 2.5f);
		}
		yield return base.IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_TransformDK_01.prefab:917feae6ace238f4b9fe685c1d95a2bd");
		yield break;
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x00110116 File Offset: 0x0010E316
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC04_LichKing_Male_Human_Win_02.prefab:5400c41ce0d52c946b0f224f6d661042", 2.5f);
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC04_LichKing_Male_Human_Lose_01.prefab:50ad5afed627fb84d8b7bd6984a60c58";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath, 0f, true, false));
			}
		}
		yield break;
	}

	// Token: 0x04001CF0 RID: 7408
	private HashSet<string> m_playedLines = new HashSet<string>();
}
