using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class KAR05_Wolf : KAR_MissionEntity
{
	// Token: 0x06003520 RID: 13600 RVA: 0x0010E6C4 File Offset: 0x0010C8C4
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a");
		base.PreloadSound("VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4");
		base.PreloadSound("VO_Barnes_Male_Human_WolfTurn5_01.prefab:787048fde1485714fbdb2623e81ffcff");
		base.PreloadSound("VO_Barnes_Male_Human_WolfTurn9_01.prefab:9c2c7d0c1b1556849ba41e5a2d80a273");
		base.PreloadSound("VO_Barnes_Male_Human_WolfWin_01.prefab:5ea959aed7f1c3a4aa0389317a147030");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfBigMinion_01.prefab:266cb01ccfe8a73449c10b141d69523c");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfTurn1_01.prefab:532a2edae8723cb40a189f63a7d5af1e");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01.prefab:73c2f52a396fb5b498867c8d8e0b4a0a");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_01.prefab:d4457c645514a6b49be8345218d13cf6");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_02.prefab:af8f1f0f982c1e74789a3e358ec32f9e");
		base.PreloadSound("VO_BigBadWolf_Male_Worgen_WolfScarletCrusader_01.prefab:dcdcc54bc1a75374baf3a5237d0a7141");
		base.PreloadSound("VO_Moroes_Male_Human_WolfClaws_03.prefab:91c86f34e4a146f488899a60f8e4490b");
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x0010E758 File Offset: 0x0010C958
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
						m_soundName = "VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01.prefab:73c2f52a396fb5b498867c8d8e0b4a0a",
						m_stringTag = "VO_BigBadWolf_Male_Worgen_WolfEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x0010E7B7 File Offset: 0x0010C9B7
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent != 1)
		{
			if (missionEvent == 2)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayMissionFlavorLine(enemyActor, "VO_BigBadWolf_Male_Worgen_WolfTurn1_01.prefab:532a2edae8723cb40a189f63a7d5af1e", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			if (base.ShouldPlayMissionFlavorLine("VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a"))
			{
				yield return new WaitForSeconds(0.8f);
			}
			yield return base.PlayMissionFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfBigMinion_01.prefab:22f80cdb7ab6b6f44a643994882fb42a", 2.5f);
			yield return base.PlayMissionFlavorLine(enemyActor, "VO_BigBadWolf_Male_Worgen_WolfBigMinion_01.prefab:266cb01ccfe8a73449c10b141d69523c", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x0010E7CD File Offset: 0x0010C9CD
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (turn != 1)
		{
			if (turn == 10)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfTurn9_01.prefab:9c2c7d0c1b1556849ba41e5a2d80a273", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayOpeningLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfTurn5_01.prefab:787048fde1485714fbdb2623e81ffcff", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x0010E7E4 File Offset: 0x0010C9E4
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

	// Token: 0x06003525 RID: 13605 RVA: 0x0010E878 File Offset: 0x0010CA78
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		string text = "ENEMY_" + entity.GetCardId();
		if (this.m_playedLines.Contains(text))
		{
			yield break;
		}
		this.m_playedLines.Add(text);
		if (text == "KARA_05_02")
		{
			GameState.Get().SetBusy(true);
			if (base.ShouldPlayBossLine("VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4"))
			{
				yield return new WaitForSeconds(3f);
				yield return base.PlayMissionFlavorLine("Barnes_BigQuote.prefab:15c396b2577ab09449f3721d23da3dba", "VO_Barnes_Male_Human_WolfClaws_01.prefab:d1159c6b78ace3349a5040e46f92f7e4", 2.5f);
			}
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x0010E88E File Offset: 0x0010CA8E
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
		Actor direWolf;
		if (!(cardId == "EX1_162"))
		{
			if (cardId == "EX1_020")
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayEasterEggLine(actor, "VO_BigBadWolf_Male_Worgen_WolfScarletCrusader_01.prefab:dcdcc54bc1a75374baf3a5237d0a7141", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			direWolf = this.GetDireWolf();
			if (direWolf != null)
			{
				GameState.Get().SetBusy(true);
				yield return base.PlayEasterEggLine(actor, "VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_01.prefab:d4457c645514a6b49be8345218d13cf6", 2.5f);
				yield return base.PlayEasterEggLine(direWolf, "VO_BigBadWolf_Male_Worgen_WolfDireWolfAlpha_02.prefab:af8f1f0f982c1e74789a3e358ec32f9e", 2.5f);
				GameState.Get().SetBusy(false);
			}
		}
		direWolf = null;
		yield break;
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x0010E8A4 File Offset: 0x0010CAA4
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Barnes_Quote.prefab:2e7e9f28b5bc37149a12b2e5feaa244a", "VO_Barnes_Male_Human_WolfWin_01.prefab:5ea959aed7f1c3a4aa0389317a147030", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD7 RID: 7383
	private HashSet<string> m_playedLines = new HashSet<string>();
}
