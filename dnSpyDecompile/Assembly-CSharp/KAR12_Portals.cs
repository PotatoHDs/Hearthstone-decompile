using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A4 RID: 932
public class KAR12_Portals : KAR_MissionEntity
{
	// Token: 0x06003556 RID: 13654 RVA: 0x0010F1A4 File Offset: 0x0010D3A4
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_KarazhanFreeMedivh);
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x0010F1B8 File Offset: 0x0010D3B8
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485");
		base.PreloadSound("VO_Moroes_Male_Human_FinalAltTurn5_01.prefab:9c414f092ce0ec14d9daf94a7ad6ac1f");
		base.PreloadSound("VO_Moroes_Male_Human_FinalMaidenTurn7_03.prefab:05caa36abf0fa5a47ba8fcbb0f5f9b3d");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalThirdSequence_04.prefab:2fd08853587f35e47898939b971b282c");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarTurn7_01.prefab:3ec08fc1a35e79c439b40fdcbcb0db1c");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FInalMalchezaarSacrificialPact_03.prefab:67beaf7b540d3dd46a3c302730ece026");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarMedivhSkin_01.prefab:d2ca787691dbb49468911ff1d1a5c1e6");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarHeroPower_01.prefab:35d5e0210c690e244830efff7b029b56");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarEmoteResponse_01.prefab:80deff75e71de3941ac8b8d4167fb814");
		base.PreloadSound("VO_Malchezaar_Male_Demon_Brawl_06.prefab:7ebc074519c4bee4b9ee2dc3a022cb8a");
		base.PreloadSound("VO_Malchezaar_Male_Demon_FinalAltOpening_01.prefab:9902000abbbc66348b13baaefad5a6ef");
		base.PreloadSound("VO_Malchezaar_Male_Demon_EmoteParty_01.prefab:77b4252ce1451884cb2d1148bdc636a7");
		base.PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6");
		base.PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_03.prefab:9a08107f3d877f44bb4ebef53db3c708");
		base.PreloadSound("VO_Medivh_Male_Human_FinalMedivhMedivhSkin_01.prefab:583768d83b20d12469684c50af5db9a0");
		base.PreloadSound("VO_Medivh_Male_Human_FinalMalchezaarTurn5_01.prefab:ce3f76baba16c4640877a9e5ae3e3221");
		base.PreloadSound("VO_Medivh_Male_Human_FinalMalchezaarWin_01.prefab:bf1f3b1d88b8dad42a03581c2e61a0e9");
		base.PreloadSound("VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:9dc5c97f68e466a45a0e5cd3dafb6a1a");
		base.PreloadSound("VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_02.prefab:9d151ec830f37f947b6188c3235ea5cc");
		base.PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_02.prefab:3501e9a3a477db7468459ea6b0c162f6");
		base.PreloadSound("VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240");
		base.PreloadSound("VO_Nazra_Female_Orc_FinalNazraEmoteResponse_01.prefab:6213bb0b535356b40b360716d732ed83");
		base.PreloadSound("VO_Nazra_Female_Orc_FinalNazraGrom_01.prefab:225efe7172be03148af639919e115b69");
		base.PreloadSound("VO_Nazra_Female_Orc_FinalNazraChogall_02.prefab:52d6f8fa86e142741ad1136a29722be7");
		base.PreloadSound("VO_Moroes_Male_Human_FinalMaidenTurn1_01.prefab:f172c91c0b7f52a47b8d95e4c89a64db");
		base.PreloadSound("VO_Moroes_Male_Human_FinalNazraTurn7_02.prefab:6a6ff647d93cf984a97c07d205013133");
		base.PreloadSound("VO_Nazra_Female_Orc_FinalNazraHeroPower_01.prefab:2e9958d6650ca08469a566b41f9a7df0");
		base.PreloadSound("VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130");
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x0010F310 File Offset: 0x0010D510
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId == "KARA_13_01" || cardId == "KARA_13_01H")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_Nazra_Female_Orc_FinalNazraEmoteResponse_01.prefab:6213bb0b535356b40b360716d732ed83", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!(cardId == "KARA_13_06") && !(cardId == "KARA_13_06H"))
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_Malchezaar_Male_Demon_FinalAltOpening_01.prefab:9902000abbbc66348b13baaefad5a6ef", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x0010F3DD File Offset: 0x0010D5DD
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		this.m_playedLines.Add(item);
		switch (missionEvent)
		{
		case 9:
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
			bool playLongSpell = base.ShouldPlayLine("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f", new MissionEntity.ShouldPlay(this.ShouldPlayLongCutscene));
			GameState.Get().SetBusy(true);
			yield return new WaitForSeconds(2f);
			if (playLongSpell)
			{
				yield return base.PlayCriticalLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240", 2.5f);
				yield return new WaitForSeconds(1f);
			}
			if (!playLongSpell)
			{
				yield return new WaitForSeconds(1.5f);
			}
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			yield return base.PlayCriticalLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		}
		case 12:
			GameState.Get().SetBusy(true);
			yield return base.PlayMissionFlavorLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarTurn7_01.prefab:3ec08fc1a35e79c439b40fdcbcb0db1c", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 13:
			GameState.Get().SetBusy(true);
			yield return base.PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FInalMalchezaarSacrificialPact_03.prefab:67beaf7b540d3dd46a3c302730ece026", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 14:
			if (base.ShouldPlayCriticalLine("VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240"))
			{
				GameState.Get().SetBusy(true);
				GameObject gameObject = GameObject.Find("Medivh_Hero");
				if (gameObject == null)
				{
					Log.All.PrintError("Could not find Medivh_Hero gameObject", Array.Empty<object>());
					GameState.Get().SetBusy(false);
				}
				else
				{
					Actor component = gameObject.GetComponent<Actor>();
					if (component == null)
					{
						Log.All.PrintError("Could not find actor component for Medivh_Hero gameObject", Array.Empty<object>());
						GameState.Get().SetBusy(false);
					}
					else
					{
						component.SetEntity(GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetEntity());
						yield return base.PlayCriticalLine(component, "VO_Medivh_Male_Human_FinalThirdSequence_03.prefab:9a08107f3d877f44bb4ebef53db3c708", 2.5f);
						GameState.Get().SetBusy(false);
					}
				}
			}
			break;
		case 16:
			if (base.ShouldPlayLine("VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6", new MissionEntity.ShouldPlay(this.ShouldPlayLongCutscene)))
			{
				GameState.Get().SetBusy(true);
				GameObject gameObject2 = GameObject.Find("Medivh_Hero");
				if (gameObject2 == null)
				{
					Log.All.PrintError("Could not find Medivh_Hero gameObject", Array.Empty<object>());
					GameState.Get().SetBusy(false);
				}
				else
				{
					Actor medivhActor = gameObject2.GetComponent<Actor>();
					if (medivhActor == null)
					{
						Log.All.PrintError("Could not find actor component for Medivh_Hero gameObject", Array.Empty<object>());
						GameState.Get().SetBusy(false);
					}
					else
					{
						medivhActor.SetEntity(GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetEntity());
						yield return new WaitForSeconds(1f);
						yield return base.PlayCriticalLine(medivhActor, "VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6", 2.5f);
						GameState.Get().SetBusy(false);
						medivhActor = null;
					}
				}
			}
			break;
		case 17:
			GameState.Get().SetBusy(true);
			yield return base.PlayEasterEggLine("Medivh_BigQuote.prefab:78e18a627031f6c48aef27a0fa1123c1", "VO_Medivh_Male_Human_FinalMedivhMedivhSkin_01.prefab:583768d83b20d12469684c50af5db9a0", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x0010F3F3 File Offset: 0x0010D5F3
	public override void NotifyOfRealTimeTagChange(Entity entity, Network.HistTagChange tagChange)
	{
		if (tagChange.Tag == 6 && tagChange.Value == 9)
		{
			if (TurnTimer.Get() != null)
			{
				TurnTimer.Get().OnEndTurnRequested();
			}
			EndTurnButton.Get().OnEndTurnRequested();
			GameState.Get().UpdateOptionHighlights();
		}
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x0010F433 File Offset: 0x0010D633
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId == "KARA_13_01" || cardId == "KARA_13_01H")
		{
			if (turn != 1)
			{
				if (turn != 7)
				{
					if (turn == 12)
					{
						GameState.Get().SetBusy(true);
						yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalAltTurn5_01.prefab:9c414f092ce0ec14d9daf94a7ad6ac1f", 2.5f);
						yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalMaidenTurn7_03.prefab:05caa36abf0fa5a47ba8fcbb0f5f9b3d", 2.5f);
						GameState.Get().SetBusy(false);
					}
				}
				else
				{
					yield return base.PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalNazraTurn7_02.prefab:6a6ff647d93cf984a97c07d205013133", 2.5f);
				}
			}
			else
			{
				yield return base.PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalMaidenTurn1_01.prefab:f172c91c0b7f52a47b8d95e4c89a64db", 2.5f);
			}
		}
		yield break;
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x0010F449 File Offset: 0x0010D649
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
		if (!(cardId == "KARA_13_11"))
		{
			if (!(cardId == "KARA_13_12"))
			{
				if (!(cardId == "KARA_00_02") && !(cardId == "KARA_13_13H"))
				{
					if (cardId == "EX1_312")
					{
						this.m_playedLines.Add(cardId);
						yield return base.PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalThirdSequence_04.prefab:2fd08853587f35e47898939b971b282c", 2.5f);
					}
				}
				else if (hero.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) > 2)
				{
					this.m_playedLines.Add(cardId);
					GameState.Get().SetBusy(true);
					yield return base.PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalMalchezaarHeroPower_01.prefab:35d5e0210c690e244830efff7b029b56", 2.5f);
					GameState.Get().SetBusy(false);
				}
			}
			else
			{
				this.m_playedLines.Add(cardId);
				yield return base.PlayBossLine(actor, "VO_Malchezaar_Male_Demon_Brawl_06.prefab:7ebc074519c4bee4b9ee2dc3a022cb8a", 2.5f);
			}
		}
		else
		{
			this.m_playedLines.Add(cardId);
			yield return base.PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalMalchezaarEmoteResponse_01.prefab:80deff75e71de3941ac8b8d4167fb814", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x0010F45F File Offset: 0x0010D65F
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		string cardId = entity.GetCardId();
		string cardId2 = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (cardId2 == "KARA_13_01" || cardId2 == "KARA_13_01H")
		{
			if (!(cardId == "EX1_414"))
			{
				if (cardId == "OG_121")
				{
					this.m_playedLines.Add(cardId);
					yield return new WaitForSeconds(3.7f);
					yield return base.PlayEasterEggLine(enemyActor, "VO_Nazra_Female_Orc_FinalNazraChogall_02.prefab:52d6f8fa86e142741ad1136a29722be7", 2.5f);
				}
			}
			else
			{
				this.m_playedLines.Add(cardId);
				yield return new WaitForSeconds(2.2f);
				yield return base.PlayEasterEggLine(enemyActor, "VO_Nazra_Female_Orc_FinalNazraGrom_01.prefab:225efe7172be03148af639919e115b69", 2.5f);
			}
		}
		else if (cardId2 == "KARA_13_06" || cardId2 == "KARA_13_06H")
		{
			if (!(cardId == "EX1_323"))
			{
				if (cardId == "CS2_034_H1")
				{
					this.m_playedLines.Add(cardId);
					yield return base.PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarMedivhSkin_01.prefab:d2ca787691dbb49468911ff1d1a5c1e6", 2.5f);
				}
			}
			else
			{
				this.m_playedLines.Add(cardId);
				if (base.ShouldPlayEasterEggLine("VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938"))
				{
					yield return new WaitForSeconds(5f);
					yield return base.PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938", 2.5f);
					yield return base.PlayEasterEggLine(friendlyActor, "VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:9dc5c97f68e466a45a0e5cd3dafb6a1a", 2.5f);
					yield return base.PlayEasterEggLine(friendlyActor, "VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_02.prefab:9d151ec830f37f947b6188c3235ea5cc", 2.5f);
				}
			}
		}
		yield break;
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x0010F475 File Offset: 0x0010D675
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return base.PlayClosingLine("Medivh_Quote.prefab:423c4a6b7e7a7f643bf0b2992ad3d31b", "VO_Medivh_Male_Human_FinalMalchezaarWin_01.prefab:bf1f3b1d88b8dad42a03581c2e61a0e9", 2.5f);
		}
		yield break;
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x0010F48B File Offset: 0x0010D68B
	private void OnIntroSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (this.m_introSpellInstance != spell || this.m_introSpellInstance.GetActiveState() != SpellStateType.NONE)
		{
			return;
		}
		UnityEngine.Object.Destroy(this.m_introSpellInstance.gameObject, 5f);
		this.m_introSpellInstance = null;
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue ShouldPlayLongCutscene()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x0010F4C5 File Offset: 0x0010D6C5
	public bool ShouldPlayLongMidmissionCutscene()
	{
		return base.ShouldPlayLine("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f", new MissionEntity.ShouldPlay(this.ShouldPlayLongCutscene));
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x0010F4DE File Offset: 0x0010D6DE
	public override IEnumerator DoCustomIntro(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		if (base.ShouldPlayLine("VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485", new MissionEntity.ShouldPlay(this.ShouldPlayLongCutscene)))
		{
			friendlyHeroLabel.SetFade(0f);
			enemyHeroLabel.SetFade(0f);
			versusText.gameObject.SetActive(false);
			friendlyHero.GetActor().Hide();
			enemyHero.GetActor().Hide();
			GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard().GetActor().Hide();
			yield return new WaitForSeconds(1.5f);
			if (!string.IsNullOrEmpty("Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907"))
			{
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907", AssetLoadingOptions.None);
				this.m_introSpellInstance = gameObject.GetComponent<Spell>();
				this.m_introSpellInstance.AddStateFinishedCallback(new Spell.StateFinishedCallback(this.OnIntroSpellStateFinished));
				this.m_introSpellInstance.SetSource(friendlyHero.gameObject);
				this.m_introSpellInstance.AddTarget(enemyHero.gameObject);
				this.m_introSpellInstance.ActivateState(SpellStateType.ACTION);
				yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485", 3f, 1f, true, false));
				Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
				yield return new WaitForSeconds(4f);
				Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
				if (UniversalInputManager.UsePhoneUI)
				{
					direction = Notification.SpeechBubbleDirection.TopLeft;
				}
				yield return Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_Nazra_Female_Orc_FinalNazraHeroPower_01.prefab:2e9958d6650ca08469a566b41f9a7df0", direction, enemyActor, 3f, 1f, false, false, 0.6f));
				if (UniversalInputManager.UsePhoneUI)
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130", "VO_Moroes_Male_Human_FinalTurn1_02", new Vector3(-4f, 0f, 0f), 1f, 0f, true, false, true, Notification.SpeechBubbleDirection.None, false));
				}
				else
				{
					yield return Gameplay.Get().StartCoroutine(base.PlayBigCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130", 3f, 1f, true, false));
				}
				while (this.m_introSpellInstance != null && !this.m_introSpellInstance.IsFinished())
				{
					yield return null;
				}
				enemyActor = null;
			}
			friendlyHeroLabel.FadeIn();
			enemyHeroLabel.FadeIn();
			versusText.gameObject.SetActive(true);
			versusText.FadeIn();
			Gameplay.Get().StartCoroutine(this.FlipInHeroPower());
			yield return new WaitForSeconds(1f);
		}
		yield break;
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x0010F512 File Offset: 0x0010D712
	public IEnumerator FlipInHeroPower()
	{
		yield return new WaitForSeconds(6f);
		GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard().GetActor().Show();
		GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard().ActivateActorSpell(SpellType.SUMMON_IN);
		yield break;
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x0010F51A File Offset: 0x0010D71A
	public override void OnCustomIntroCancelled(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		if (this.m_introSpellInstance != null)
		{
			this.m_introSpellInstance.ActivateState(SpellStateType.CANCEL);
		}
	}

	// Token: 0x04001CDE RID: 7390
	private HashSet<string> m_playedLines = new HashSet<string>();

	// Token: 0x04001CDF RID: 7391
	private const string m_introSpellPath = "Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907";

	// Token: 0x04001CE0 RID: 7392
	private Spell m_introSpellInstance;
}
