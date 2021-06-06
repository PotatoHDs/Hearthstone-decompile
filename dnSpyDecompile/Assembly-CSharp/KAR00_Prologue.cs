using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000398 RID: 920
public class KAR00_Prologue : KAR_MissionEntity
{
	// Token: 0x060034F9 RID: 13561 RVA: 0x0010DD2B File Offset: 0x0010BF2B
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_KarazhanPrologue);
	}

	// Token: 0x060034FA RID: 13562 RVA: 0x0010DD40 File Offset: 0x0010BF40
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Malchezaar_Male_Demon_PrologueEmoteResponse_01.prefab:86f1f2ae80c38214ead0c43ba589b03c");
		base.PreloadSound("VO_Malchezaar_Male_Demon_PrologueHeroPower_01.prefab:34d993460aa6bf249874ef52c64bb8c8");
		base.PreloadSound("VO_Malchezaar_Male_Demon_PrologueWin_01.prefab:9d47723e51ef0fc469e66cb93ca6cba6");
		base.PreloadSound("VO_Malchezaar_Male_Demon_PrologueTurn1_04.prefab:335b8e61366fc9745b8b7cd0a61cc5c7");
		base.PreloadSound("VO_Malchezaar_Male_Demon_PrologueTurn11_02.prefab:fbd92ceb10f5ee94c9e2564aa6124df0");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueTurn1_02.prefab:102f563763c38a249bdb734e6963ae37");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueTurn3_02.prefab:da4713d2dea65a449be2485948df8f47");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueTurn9_01.prefab:b92c01b534d9bc446999405722204a9e");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueTurn11_01.prefab:e350d5335927c4c429be030ed6c3bda1");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueHeroPower_01.prefab:2a948f10bad7a4f4190c2e9b7d1f5e7c");
		base.PreloadSound("VO_Medivh_Male_Human_PrologueWin_01.prefab:fa080d80cc356ac4587e802bbb856afb");
		base.PreloadSound("VO_Moroes_Male_Human_PrologueTurn5_03.prefab:024bdb88f10206046a9a929fa350d4b9");
		base.PreloadSound("VO_Moroes_Male_Human_PrologueTurn7_01.prefab:9b60f6c47c2a6c5498fbba1a1c6ec35a");
		base.PreloadSound("VO_Moroes_Male_Human_PrologueTurn9_02.prefab:3840159d3b8b5414e94ab693ede0c0d2");
		base.PreloadSound("VO_Moroes_Male_Human_PrologueTurn11_01.prefab:a5383482754ffb646a13daf16819a892");
		base.PreloadSound("VO_Moroes_Male_Human_PrologueWin_02.prefab:8d0d87405b3c8d5438a44faa1ed92d17");
	}

	// Token: 0x060034FB RID: 13563 RVA: 0x0010DE00 File Offset: 0x0010C000
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
						m_soundName = "VO_Malchezaar_Male_Demon_PrologueEmoteResponse_01.prefab:86f1f2ae80c38214ead0c43ba589b03c",
						m_stringTag = "VO_Malchezaar_Male_Demon_PrologueEmoteResponse_01"
					}
				}
			}
		};
	}

	// Token: 0x060034FC RID: 13564 RVA: 0x0010DE5F File Offset: 0x0010C05F
	public override bool ShouldPlayHeroBlowUpSpells(TAG_PLAYSTATE playState)
	{
		return playState != TAG_PLAYSTATE.WON;
	}

	// Token: 0x060034FD RID: 13565 RVA: 0x0010DE68 File Offset: 0x0010C068
	public override string GetVictoryScreenBannerText()
	{
		return GameStrings.Get("GAMEPLAY_END_OF_GAME_VICTORY_MAYBE");
	}

	// Token: 0x060034FE RID: 13566 RVA: 0x0010DE74 File Offset: 0x0010C074
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (missionEvent == 2)
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayCriticalLine(actor, "VO_Medivh_Male_Human_PrologueWin_01.prefab:fa080d80cc356ac4587e802bbb856afb", 2.5f);
			yield return new WaitForSeconds(1f);
			yield return base.PlayCriticalLine(enemyActor, "VO_Malchezaar_Male_Demon_PrologueWin_01.prefab:9d47723e51ef0fc469e66cb93ca6cba6", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x060034FF RID: 13567 RVA: 0x0010DE8A File Offset: 0x0010C08A
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (turn != 1)
		{
			switch (turn)
			{
			case 4:
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine(friendlyActor, "VO_Medivh_Male_Human_PrologueTurn3_02.prefab:da4713d2dea65a449be2485948df8f47", 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 6:
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_PrologueTurn5_03.prefab:024bdb88f10206046a9a929fa350d4b9", 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 8:
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_PrologueTurn7_01.prefab:9b60f6c47c2a6c5498fbba1a1c6ec35a", 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 10:
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine(friendlyActor, "VO_Medivh_Male_Human_PrologueTurn9_01.prefab:b92c01b534d9bc446999405722204a9e", 2.5f);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_PrologueTurn9_02.prefab:3840159d3b8b5414e94ab693ede0c0d2", 2.5f);
				GameState.Get().SetBusy(false);
				break;
			case 12:
				GameState.Get().SetBusy(true);
				yield return base.PlayAdventureFlavorLine(actor, "VO_Malchezaar_Male_Demon_PrologueTurn11_02.prefab:fbd92ceb10f5ee94c9e2564aa6124df0", 2.5f);
				yield return base.PlayAdventureFlavorLine(friendlyActor, "VO_Medivh_Male_Human_PrologueTurn11_01.prefab:e350d5335927c4c429be030ed6c3bda1", 2.5f);
				yield return base.PlayAdventureFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_PrologueTurn11_01.prefab:a5383482754ffb646a13daf16819a892", 2.5f);
				GameState.Get().SetBusy(false);
				break;
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayOpeningLine(actor, "VO_Malchezaar_Male_Demon_PrologueTurn1_04.prefab:335b8e61366fc9745b8b7cd0a61cc5c7", 2.5f);
			yield return base.PlayAdventureFlavorLine(friendlyActor, "VO_Medivh_Male_Human_PrologueTurn1_02.prefab:102f563763c38a249bdb734e6963ae37", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003500 RID: 13568 RVA: 0x0010DEA0 File Offset: 0x0010C0A0
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
		if (this.m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		this.m_playedLines.Add(cardId);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		if (cardId == "KARA_00_02")
		{
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, "VO_Malchezaar_Male_Demon_PrologueHeroPower_01.prefab:34d993460aa6bf249874ef52c64bb8c8", 2.5f);
			yield return base.PlayMissionFlavorLine(friendlyActor, "VO_Medivh_Male_Human_PrologueHeroPower_01.prefab:2a948f10bad7a4f4190c2e9b7d1f5e7c", 2.5f);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06003501 RID: 13569 RVA: 0x0010DEB6 File Offset: 0x0010C0B6
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Moroes_Quote.prefab:ea3a21837aab2b0448ce4090103724cf", "VO_Moroes_Male_Human_PrologueWin_02.prefab:8d0d87405b3c8d5438a44faa1ed92d17", 2.5f);
		}
		yield break;
	}

	// Token: 0x04001CD3 RID: 7379
	private HashSet<string> m_playedLines = new HashSet<string>();
}
