using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039C RID: 924
public class KAR04_Julianne : KAR_MissionEntity
{
	// Token: 0x06003518 RID: 13592 RVA: 0x0010E4E4 File Offset: 0x0010C6E4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Julianne_Female_Human_JulianneHeroPower_01.prefab:52cf3ed754f5ae647a1fb2a27ae8e37d");
		base.PreloadSound("VO_Julianne_Female_Human_JulianneEmoteResponse_01.prefab:803a3576d6dd0a74fa6da433b25d638b");
		base.PreloadSound("VO_KARA_06_01_Male_Human_JulianneTurn1_01.prefab:2c91233e10b180441b1d8bf1a834e53a");
		base.PreloadSound("VO_Moroes_Male_Human_JulianneTurn5_01.prefab:d2e8c0e588e0cb045b1ad62cf02ac17f");
		base.PreloadSound("VO_Moroes_Male_Human_JulianneTurn9_02.prefab:bd1443ae4c72fb445a1fd9f558e9640e");
		base.PreloadSound("VO_Barnes_Male_Human_JulianneTurn5_01.prefab:614340cf7864460478f0984d527b5bba");
		base.PreloadSound("VO_KARA_06_01_Male_Human_JulianneDeadlyPoison_02.prefab:4b33353d9ef6009418520b1173a285e7");
		base.PreloadSound("VO_Julianne_Female_Human_JulianneFeignDeath_03.prefab:e28f8db1c88f12f4bba962b65e9ed936");
		base.PreloadSound("VO_Barnes_Male_Human_JulianneWin_01.prefab:09d4c4aaf43ac634aaf325c2badc72a8");
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x0010E554 File Offset: 0x0010C754
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
						m_soundName = "VO_Julianne_Female_Human_JulianneEmoteResponse_01.prefab:803a3576d6dd0a74fa6da433b25d638b",
						m_stringTag = "VO_Julianne_Female_Human_JulianneEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x0010E5B4 File Offset: 0x0010C7B4
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

	// Token: 0x0600351B RID: 13595 RVA: 0x0010E658 File Offset: 0x0010C858
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			if (turn != 6)
			{
				if (turn == 10)
				{
					GameState.Get().SetBusy(true);
					yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_JulianneTurn9_02.prefab:bd1443ae4c72fb445a1fd9f558e9640e", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_JulianneTurn5_01.prefab:d2e8c0e588e0cb045b1ad62cf02ac17f", 2.5f);
				yield return base.PlayAdventureFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_JulianneTurn5_01.prefab:614340cf7864460478f0984d527b5bba", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			Actor romulo = this.GetRomulo();
			if (romulo != null)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayOpeningLine(romulo, "VO_KARA_06_01_Male_Human_JulianneTurn1_01.prefab:2c91233e10b180441b1d8bf1a834e53a", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x0010E66E File Offset: 0x0010C86E
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 1)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, "VO_Julianne_Female_Human_JulianneHeroPower_01.prefab:52cf3ed754f5ae647a1fb2a27ae8e37d", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x0010E684 File Offset: 0x0010C884
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
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (!(cardId == "CS2_074"))
		{
			if (cardId == "GVG_026")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayEasterEggLine(actor, "VO_Julianne_Female_Human_JulianneFeignDeath_03.prefab:e28f8db1c88f12f4bba962b65e9ed936", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			Actor romulo = this.GetRomulo();
			if (romulo != null)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayEasterEggLine(romulo, "VO_KARA_06_01_Male_Human_JulianneDeadlyPoison_02.prefab:4b33353d9ef6009418520b1173a285e7", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		yield break;
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x0010E69A File Offset: 0x0010C89A
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a", "VO_Barnes_Male_Human_JulianneWin_01.prefab:09d4c4aaf43ac634aaf325c2badc72a8", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD6 RID: 7382
	private HashSet<string> m_playedLines = new HashSet<string>();
}
