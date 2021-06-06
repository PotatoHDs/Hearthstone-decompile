using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR11_Netherspite : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01.prefab:f0a08435dd8aedb4b9d4b7f8b27a4d4f");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn3_02.prefab:909c9170498e2ff458bb5e607ae35fe1");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteTurn7_01.prefab:714c8bbda55d73b4a96f6c08ad3f2372");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteEmpowerment_01.prefab:066618c460879fc4f95694560aede66a");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteShadowBreath_01.prefab:3ec25ccdaa47c06428c1e4119b00a6e0");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteUnstablePortal_02.prefab:59a7527262c5e2d45b25672b8a2150f8");
		PreloadSound("VO_Netherspite_Male_Dragon_NetherspiteAngryChicken_01.prefab:4364929242fd7864eb2f681df1ab4f9e");
		PreloadSound("VO_Moroes_Male_Human_NetherspiteWin_01.prefab:012fd06d66e4106409d1cc9179f3a25b");
		PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn1_01.prefab:f5a7ea32cbace6448ba0c29b44018bbb");
		PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn7_01.prefab:4ab83e21ea834994498395f0678806ea");
		PreloadSound("VO_Moroes_Male_Human_NetherspiteTurn5_01.prefab:19d16638e6ae56f4b8c915d28b0b882f");
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
						m_soundName = "VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01.prefab:f0a08435dd8aedb4b9d4b7f8b27a4d4f",
						m_stringTag = "VO_Netherspite_Male_Dragon_NetherspiteEmoteResponse_01"
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
		switch (turn)
		{
		case 1:
			yield return PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn1_01.prefab:f5a7ea32cbace6448ba0c29b44018bbb");
			break;
		case 6:
			GameState.Get().SetBusy(busy: true);
			if (ShouldPlayMissionFlavorLine("VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa"))
			{
				yield return PlayMissionFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn5_01.prefab:5c5e1d24755bfe34f825ce62f9deb6fa");
				yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn5_01.prefab:19d16638e6ae56f4b8c915d28b0b882f");
			}
			GameState.Get().SetBusy(busy: false);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn7_01.prefab:714c8bbda55d73b4a96f6c08ad3f2372");
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_NetherspiteTurn7_01.prefab:4ab83e21ea834994498395f0678806ea");
			GameState.Get().SetBusy(busy: false);
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
		if (!m_playedLines.Contains(entity.GetCardId()))
		{
			string cardId = entity.GetCardId();
			m_playedLines.Add(cardId);
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			switch (cardId)
			{
			case "KARA_08_02":
			case "KARA_08_02H":
				yield return new WaitForSeconds(0.2f);
				GameState.Get().SetBusy(busy: true);
				yield return PlayMissionFlavorLine(enemyActor, "VO_Netherspite_Male_Dragon_NetherspiteEmpowerment_01.prefab:066618c460879fc4f95694560aede66a");
				GameState.Get().SetBusy(busy: false);
				break;
			case "KARA_08_05":
			case "KARA_08_05H":
				yield return PlayBossLine(enemyActor, "VO_Netherspite_Male_Dragon_NetherspiteShadowBreath_01.prefab:3ec25ccdaa47c06428c1e4119b00a6e0");
				break;
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			m_playedLines.Add(item);
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			if (missionEvent == 1)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayMissionFlavorLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteTurn3_02.prefab:909c9170498e2ff458bb5e607ae35fe1");
				GameState.Get().SetBusy(busy: false);
			}
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		if (!(cardId == "GVG_003"))
		{
			if (cardId == "EX1_009")
			{
				yield return PlayBossLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteAngryChicken_01.prefab:4364929242fd7864eb2f681df1ab4f9e");
			}
		}
		else
		{
			yield return PlayBossLine(actor, "VO_Netherspite_Male_Dragon_NetherspiteUnstablePortal_02.prefab:59a7527262c5e2d45b25672b8a2150f8");
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_NetherspiteWin_01.prefab:012fd06d66e4106409d1cc9179f3a25b");
		}
	}
}
