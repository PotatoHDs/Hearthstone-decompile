using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR06_Crone : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Crone_Female_Troll_CroneEmoteResponse_02.prefab:84a22c615324303489aa69ffb9423a7f");
		PreloadSound("VO_Crone_Female_Troll_CroneFlyingMonkeys_01.prefab:8f6b0cbfef4a3384286ec890a06d1d10");
		PreloadSound("VO_Crone_Female_Troll_CroneHeroPower_02.prefab:d368549c6d3ac224085adc9379623580");
		PreloadSound("VO_KARA_04_01_Female_Human_CroneLionTigerBear_02.prefab:3e90aeef2f8f4a243b34e38327499e93");
		PreloadSound("VO_KARA_04_01_Female_Human_CroneHuffer_01.prefab:08d4777be932bda4b9c516544e0f6dea");
		PreloadSound("VO_KARA_04_01_Female_Human_CroneTurn1_01.prefab:7b284f2e4c3942749a4841ee78d89d9f");
		PreloadSound("VO_KARA_04_01_Female_Human_CroneTurn3_01.prefab:48747ac249add864bb4c2e5a28b27205");
		PreloadSound("VO_Moroes_Male_Human_CroneTurn5_02.prefab:28a0e089d96edd241a04d24a6d686be5");
		PreloadSound("VO_Moroes_Male_Human_CroneWin_01.prefab:b3887f0f8b013314495204d89d64c121");
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
						m_soundName = "VO_Crone_Female_Troll_CroneHeroPower_02.prefab:d368549c6d3ac224085adc9379623580",
						m_stringTag = "VO_Crone_Female_Troll_CroneHeroPower_02"
					}
				}
			}
		};
	}

	private Actor GetDorothee()
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == "KARA_04_01")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		m_playedLines.Add(item);
		Actor dorothee = GetDorothee();
		if (!(dorothee == null))
		{
			switch (missionEvent)
			{
			case 1:
				yield return PlayEasterEggLine(dorothee, "VO_KARA_04_01_Female_Human_CroneLionTigerBear_02.prefab:3e90aeef2f8f4a243b34e38327499e93");
				break;
			case 2:
				yield return PlayEasterEggLine(dorothee, "VO_KARA_04_01_Female_Human_CroneHuffer_01.prefab:08d4777be932bda4b9c516544e0f6dea");
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
		Actor dorothee = GetDorothee();
		if (!(dorothee == null) || turn >= 7)
		{
			switch (turn)
			{
			case 1:
				yield return PlayOpeningLine(dorothee, "VO_KARA_04_01_Female_Human_CroneTurn1_01.prefab:7b284f2e4c3942749a4841ee78d89d9f");
				break;
			case 5:
				yield return PlayMissionFlavorLine(dorothee, "VO_KARA_04_01_Female_Human_CroneTurn3_01.prefab:48747ac249add864bb4c2e5a28b27205");
				break;
			case 10:
				GameState.Get().SetBusy(busy: true);
				yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_CroneTurn5_02.prefab:28a0e089d96edd241a04d24a6d686be5");
				GameState.Get().SetBusy(busy: false);
				break;
			}
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "KARA_04_05":
			case "KARA_04_05h":
				yield return PlayBossLine(actor, "VO_Crone_Female_Troll_CroneFlyingMonkeys_01.prefab:8f6b0cbfef4a3384286ec890a06d1d10");
				break;
			case "KARA_04_02hp":
				yield return PlayCriticalLine(actor, "VO_Crone_Female_Troll_CroneEmoteResponse_02.prefab:84a22c615324303489aa69ffb9423a7f");
				break;
			}
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_CroneWin_01.prefab:b3887f0f8b013314495204d89d64c121");
		}
	}
}
