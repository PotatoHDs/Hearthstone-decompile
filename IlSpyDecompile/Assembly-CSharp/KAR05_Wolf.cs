using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR05_Wolf : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a");
		PreloadSound("VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4");
		PreloadSound("VO_Barnes_Male_Human_WolfTurn5_01.prefab:787048fde1485714fbdb2623e81ffcff");
		PreloadSound("VO_Barnes_Male_Human_WolfTurn9_01.prefab:9c2c7d0c1b1556849ba41e5a2d80a273");
		PreloadSound("VO_Barnes_Male_Human_WolfWin_01.prefab:5ea959aed7f1c3a4aa0389317a147030");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfBigMinion_01.prefab:266cb01ccfe8a73449c10b141d69523c");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfTurn1_01.prefab:532a2edae8723cb40a189f63a7d5af1e");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01.prefab:73c2f52a396fb5b498867c8d8e0b4a0a");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_01.prefab:d4457c645514a6b49be8345218d13cf6");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_02.prefab:af8f1f0f982c1e74789a3e358ec32f9e");
		PreloadSound("VO_BigBadWolf_Male_Worgen_WolfScarletCrusader_01.prefab:dcdcc54bc1a75374baf3a5237d0a7141");
		PreloadSound("VO_Moroes_Male_Human_WolfClaws_03.prefab:91c86f34e4a146f488899a60f8e4490b");
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
						m_soundName = "VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01.prefab:73c2f52a396fb5b498867c8d8e0b4a0a",
						m_stringTag = "VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01"
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
		switch (missionEvent)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			if (ShouldPlayMissionFlavorLine("VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a"))
			{
				yield return new WaitForSeconds(0.8f);
			}
			yield return PlayMissionFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a");
			yield return PlayMissionFlavorLine(enemyActor, "VO_BigBadWolf_Male_Worgen_WolfBigMinion_01.prefab:266cb01ccfe8a73449c10b141d69523c");
			GameState.Get().SetBusy(busy: false);
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(enemyActor, "VO_BigBadWolf_Male_Worgen_WolfTurn1_01.prefab:532a2edae8723cb40a189f63a7d5af1e");
			GameState.Get().SetBusy(busy: false);
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
			GameState.Get().SetBusy(busy: true);
			yield return PlayOpeningLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfTurn5_01.prefab:787048fde1485714fbdb2623e81ffcff");
			GameState.Get().SetBusy(busy: false);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfTurn9_01.prefab:9c2c7d0c1b1556849ba41e5a2d80a273");
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	private Actor GetDireWolf()
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == "EX1_162")
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
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
		string text = "ENEMY_" + entity.GetCardId();
		if (m_playedLines.Contains(text))
		{
			yield break;
		}
		m_playedLines.Add(text);
		if (text == "KARA_05_02")
		{
			GameState.Get().SetBusy(busy: true);
			if (ShouldPlayBossLine("VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4"))
			{
				yield return new WaitForSeconds(3f);
				yield return PlayMissionFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4");
			}
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (!(cardId == "EX1_162"))
		{
			if (cardId == "EX1_020")
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayEasterEggLine(actor, "VO_BigBadWolf_Male_Worgen_WolfScarletCrusader_01.prefab:dcdcc54bc1a75374baf3a5237d0a7141");
				GameState.Get().SetBusy(busy: false);
			}
			yield break;
		}
		Actor direWolf = GetDireWolf();
		if (direWolf != null)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayEasterEggLine(actor, "VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_01.prefab:d4457c645514a6b49be8345218d13cf6");
			yield return PlayEasterEggLine(direWolf, "VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_02.prefab:af8f1f0f982c1e74789a3e358ec32f9e");
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a", "VO_Barnes_Male_Human_WolfWin_01.prefab:5ea959aed7f1c3a4aa0389317a147030");
		}
	}
}
