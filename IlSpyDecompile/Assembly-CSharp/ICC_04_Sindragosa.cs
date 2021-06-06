using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_04_Sindragosa : ICC_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		foreach (string item in new List<string>
		{
			"VO_ICC04_Sindragosa_Female_Dragon_Intro_01.prefab:528bdc19dcdbe164c9287634a6909ddc", "VO_ICC04_Sindragosa_Female_Dragon_PlayerPlaysMinion_02.prefab:ea852ea6a4e59da479ffaff920b02d89", "VO_ICC04_LichKing_Male_Human_BreathNoMinion_01.prefab:e5757d128803add49b1776b91e3ff1d8", "VO_ICC04_Sindragosa_Female_Dragon_BreathNoRoomLeft_01.prefab:2fbd9d2b20a240742ad7204aaa8f0f58", "VO_ICC04_LichKing_Male_Human_Turn5_01.prefab:0eba2d9a9381aaf40bdc4c6a565ad3bd", "VO_ICC04_LichKing_Male_Human_Turn5_02.prefab:2c31e9716459f874d9669aa26770b505", "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_02.prefab:5407a8811fc337e4b9568258f2f586ec", "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_04.prefab:4edcfb445a8569a4f8952f68eb82f9dc", "VO_ICC04_Sindragosa_Female_Dragon_SecondBreath_01.prefab:de6055ac3730d8f4aac6cd124780e00e", "VO_ICC04_Sindragosa_Female_Dragon_Death_01.prefab:3d8e08533b9f43f4abe38d847f1b4c44",
			"VO_ICC04_LichKing_Male_Human_Win_02.prefab:5400c41ce0d52c946b0f224f6d661042", "VO_ICC04_LichKing_Male_Human_Lose_01.prefab:50ad5afed627fb84d8b7bd6984a60c58", "VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01.prefab:3cff598aa476b5c4aab273531048deeb", "VO_ICC04_Sindragosa_Female_Dragon_LichKing_01.prefab:bde123d0e499c4f4ca4f3e52b35173b0", "VO_ICC04_Sindragosa_Female_Dragon_TransformDK_01.prefab:917feae6ace238f4b9fe685c1d95a2bd", "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_01.prefab:39dfe421e9ae2c24c8cdaf52fd993717", "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_02.prefab:8cae1ffdacabe1546bb9f65b744b15af", "VO_ICC04_Sindragosa_Female_Dragon_Bounce_01.prefab:39ba1793378a6d244bcad2e2aedc94d9", "VO_ICC04_Sindragosa_Female_Dragon_Malygos_01.prefab:5fedfe93360a076448f5853f98e8fb6f"
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
						m_soundName = "VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01.prefab:3cff598aa476b5c4aab273531048deeb",
						m_stringTag = "VO_ICC04_Sindragosa_Female_Dragon_EmoteResponse_01"
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
						m_soundName = "VO_ICC04_Sindragosa_Female_Dragon_Intro_01.prefab:528bdc19dcdbe164c9287634a6909ddc",
						m_stringTag = "VO_ICC04_Sindragosa_Female_Dragon_Intro_01"
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			switch (missionEvent)
			{
			case 110:
				m_playedLines.Add(item);
				yield return PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_PlayerPlaysMinion_02.prefab:ea852ea6a4e59da479ffaff920b02d89");
				break;
			case 102:
				m_playedLines.Add(item);
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_02.prefab:5407a8811fc337e4b9568258f2f586ec");
				GameState.Get().SetBusy(busy: false);
				break;
			case 103:
				m_playedLines.Add(item);
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_UnchainedMagic_04.prefab:4edcfb445a8569a4f8952f68eb82f9dc");
				GameState.Get().SetBusy(busy: false);
				break;
			case 104:
				m_playedLines.Add(item);
				yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_BreathNoMinion_01.prefab:e5757d128803add49b1776b91e3ff1d8");
				break;
			case 106:
				yield return PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_BreathNoRoomLeft_01.prefab:2fbd9d2b20a240742ad7204aaa8f0f58");
				break;
			case 108:
				yield return PlayLineOnlyOnce(actor, "VO_ICC04_Sindragosa_Female_Dragon_SecondBreath_01.prefab:de6055ac3730d8f4aac6cd124780e00e");
				break;
			case 109:
				yield return PlayBossLine(actor, "VO_ICC04_Sindragosa_Female_Dragon_Death_01.prefab:3d8e08533b9f43f4abe38d847f1b4c44");
				break;
			case 111:
				yield return PlayBossLine(actor, "VO_ICC04_Sindragosa_Female_Dragon_Bounce_01.prefab:39ba1793378a6d244bcad2e2aedc94d9");
				break;
			}
		}
	}

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		if (turn == 5)
		{
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_Turn5_01.prefab:0eba2d9a9381aaf40bdc4c6a565ad3bd");
			yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC04_LichKing_Male_Human_Turn5_02.prefab:2c31e9716459f874d9669aa26770b505");
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardID = entity.GetCardId();
			m_playedLines.Add(cardID);
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return WaitForEntitySoundToFinish(entity);
			switch (cardID)
			{
			case "ICC_314":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_LichKing_01.prefab:bde123d0e499c4f4ca4f3e52b35173b0");
				break;
			case "EX1_563":
				yield return PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_Malygos_01.prefab:5fedfe93360a076448f5853f98e8fb6f");
				break;
			case "ICC_838":
				Gameplay.Get().StartCoroutine(PlayEasterEggLine(enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_01.prefab:39dfe421e9ae2c24c8cdaf52fd993717"));
				yield return new WaitForSeconds(0.2f);
				yield return PlayEasterEggLine(GetActorByCardId("ICC_838"), "VO_ICC04_Sindragosa_Female_Dragon_Sindragosa_02.prefab:8cae1ffdacabe1546bb9f65b744b15af");
				break;
			}
			yield return IfPlayerPlaysDKHeroVO(entity, enemyActor, "VO_ICC04_Sindragosa_Female_Dragon_TransformDK_01.prefab:917feae6ace238f4b9fe685c1d95a2bd");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", "VO_ICC04_LichKing_Male_Human_Win_02.prefab:5400c41ce0d52c946b0f224f6d661042");
		}
		if (gameResult == TAG_PLAYSTATE.LOST)
		{
			yield return new WaitForSeconds(5f);
			string soundPath = "VO_ICC04_LichKing_Male_Human_Lose_01.prefab:50ad5afed627fb84d8b7bd6984a60c58";
			if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("LichKing_Banner_Quote.prefab:d42a8f4f69919f449b3dd8ebceaf2a3c", soundPath));
			}
		}
	}
}
