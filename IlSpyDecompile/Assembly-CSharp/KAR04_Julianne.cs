using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR04_Julianne : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	public override void PreloadAssets()
	{
		PreloadSound("VO_Julianne_Female_Human_JulianneHeroPower_01.prefab:52cf3ed754f5ae647a1fb2a27ae8e37d");
		PreloadSound("VO_Julianne_Female_Human_JulianneEmoteResponse_01.prefab:803a3576d6dd0a74fa6da433b25d638b");
		PreloadSound("VO_KARA_06_01_Male_Human_JulianneTurn1_01.prefab:2c91233e10b180441b1d8bf1a834e53a");
		PreloadSound("VO_Moroes_Male_Human_JulianneTurn5_01.prefab:d2e8c0e588e0cb045b1ad62cf02ac17f");
		PreloadSound("VO_Moroes_Male_Human_JulianneTurn9_02.prefab:bd1443ae4c72fb445a1fd9f558e9640e");
		PreloadSound("VO_Barnes_Male_Human_JulianneTurn5_01.prefab:614340cf7864460478f0984d527b5bba");
		PreloadSound("VO_KARA_06_01_Male_Human_JulianneDeadlyPoison_02.prefab:4b33353d9ef6009418520b1173a285e7");
		PreloadSound("VO_Julianne_Female_Human_JulianneFeignDeath_03.prefab:e28f8db1c88f12f4bba962b65e9ed936");
		PreloadSound("VO_Barnes_Male_Human_JulianneWin_01.prefab:09d4c4aaf43ac634aaf325c2badc72a8");
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
						m_soundName = "VO_Julianne_Female_Human_JulianneEmoteResponse_01.prefab:803a3576d6dd0a74fa6da433b25d638b",
						m_stringTag = "VO_Julianne_Female_Human_JulianneEmoteResponse_01"
					}
				}
			}
		};
	}

	private Actor GetRomulo()
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && (entity.GetCardId() == "KARA_06_01" || entity.GetCardId() == "KARA_06_01heroic"))
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
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
		{
			Actor romulo = GetRomulo();
			if (romulo != null)
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayOpeningLine(romulo, "VO_KARA_06_01_Male_Human_JulianneTurn1_01.prefab:2c91233e10b180441b1d8bf1a834e53a");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
		case 6:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_JulianneTurn5_01.prefab:d2e8c0e588e0cb045b1ad62cf02ac17f");
			yield return PlayAdventureFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_JulianneTurn5_01.prefab:614340cf7864460478f0984d527b5bba");
			GameState.Get().SetBusy(busy: false);
			break;
		case 10:
			GameState.Get().SetBusy(busy: true);
			yield return PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_JulianneTurn9_02.prefab:bd1443ae4c72fb445a1fd9f558e9640e");
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine(actor, "VO_Julianne_Female_Human_JulianneHeroPower_01.prefab:52cf3ed754f5ae647a1fb2a27ae8e37d");
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
		if (!(cardId == "CS2_074"))
		{
			if (cardId == "GVG_026")
			{
				GameState.Get().SetBusy(busy: true);
				yield return PlayEasterEggLine(actor, "VO_Julianne_Female_Human_JulianneFeignDeath_03.prefab:e28f8db1c88f12f4bba962b65e9ed936");
				GameState.Get().SetBusy(busy: false);
			}
			yield break;
		}
		Actor romulo = GetRomulo();
		if (romulo != null)
		{
			GameState.Get().SetBusy(busy: true);
			yield return PlayEasterEggLine(romulo, "VO_KARA_06_01_Male_Human_JulianneDeadlyPoison_02.prefab:4b33353d9ef6009418520b1173a285e7");
			GameState.Get().SetBusy(busy: false);
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a", "VO_Barnes_Male_Human_JulianneWin_01.prefab:09d4c4aaf43ac634aaf325c2badc72a8");
		}
	}
}
