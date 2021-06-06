using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KAR12_Portals : KAR_MissionEntity
{
	private HashSet<string> m_playedLines = new HashSet<string>();

	private const string m_introSpellPath = "Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907";

	private Spell m_introSpellInstance;

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_KarazhanFreeMedivh);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485");
		PreloadSound("VO_Moroes_Male_Human_FinalAltTurn5_01.prefab:9c414f092ce0ec14d9daf94a7ad6ac1f");
		PreloadSound("VO_Moroes_Male_Human_FinalMaidenTurn7_03.prefab:05caa36abf0fa5a47ba8fcbb0f5f9b3d");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalThirdSequence_04.prefab:2fd08853587f35e47898939b971b282c");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarTurn7_01.prefab:3ec08fc1a35e79c439b40fdcbcb0db1c");
		PreloadSound("VO_Malchezaar_Male_Demon_FInalMalchezaarSacrificialPact_03.prefab:67beaf7b540d3dd46a3c302730ece026");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarMedivhSkin_01.prefab:d2ca787691dbb49468911ff1d1a5c1e6");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarHeroPower_01.prefab:35d5e0210c690e244830efff7b029b56");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalMalchezaarEmoteResponse_01.prefab:80deff75e71de3941ac8b8d4167fb814");
		PreloadSound("VO_Malchezaar_Male_Demon_Brawl_06.prefab:7ebc074519c4bee4b9ee2dc3a022cb8a");
		PreloadSound("VO_Malchezaar_Male_Demon_FinalAltOpening_01.prefab:9902000abbbc66348b13baaefad5a6ef");
		PreloadSound("VO_Malchezaar_Male_Demon_EmoteParty_01.prefab:77b4252ce1451884cb2d1148bdc636a7");
		PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6");
		PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_03.prefab:9a08107f3d877f44bb4ebef53db3c708");
		PreloadSound("VO_Medivh_Male_Human_FinalMedivhMedivhSkin_01.prefab:583768d83b20d12469684c50af5db9a0");
		PreloadSound("VO_Medivh_Male_Human_FinalMalchezaarTurn5_01.prefab:ce3f76baba16c4640877a9e5ae3e3221");
		PreloadSound("VO_Medivh_Male_Human_FinalMalchezaarWin_01.prefab:bf1f3b1d88b8dad42a03581c2e61a0e9");
		PreloadSound("VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:9dc5c97f68e466a45a0e5cd3dafb6a1a");
		PreloadSound("VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_02.prefab:9d151ec830f37f947b6188c3235ea5cc");
		PreloadSound("VO_Medivh_Male_Human_FinalThirdSequence_02.prefab:3501e9a3a477db7468459ea6b0c162f6");
		PreloadSound("VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240");
		PreloadSound("VO_Nazra_Female_Orc_FinalNazraEmoteResponse_01.prefab:6213bb0b535356b40b360716d732ed83");
		PreloadSound("VO_Nazra_Female_Orc_FinalNazraGrom_01.prefab:225efe7172be03148af639919e115b69");
		PreloadSound("VO_Nazra_Female_Orc_FinalNazraChogall_02.prefab:52d6f8fa86e142741ad1136a29722be7");
		PreloadSound("VO_Moroes_Male_Human_FinalMaidenTurn1_01.prefab:f172c91c0b7f52a47b8d95e4c89a64db");
		PreloadSound("VO_Moroes_Male_Human_FinalNazraTurn7_02.prefab:6a6ff647d93cf984a97c07d205013133");
		PreloadSound("VO_Nazra_Female_Orc_FinalNazraHeroPower_01.prefab:2e9958d6650ca08469a566b41f9a7df0");
		PreloadSound("VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130");
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			switch (GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCardId())
			{
			case "KARA_13_01":
			case "KARA_13_01H":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_Nazra_Female_Orc_FinalNazraEmoteResponse_01.prefab:6213bb0b535356b40b360716d732ed83", Notification.SpeechBubbleDirection.TopRight, actor));
				break;
			case "KARA_13_06":
			case "KARA_13_06H":
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_Malchezaar_Male_Demon_FinalAltOpening_01.prefab:9902000abbbc66348b13baaefad5a6ef", Notification.SpeechBubbleDirection.TopRight, actor));
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
		while (GameState.Get().IsBusy())
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (m_playedLines.Contains(item))
		{
			yield break;
		}
		m_playedLines.Add(item);
		switch (missionEvent)
		{
		case 9:
		{
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
			bool playLongSpell = ShouldPlayLine("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f", ShouldPlayLongCutscene);
			GameState.Get().SetBusy(busy: true);
			yield return new WaitForSeconds(2f);
			if (playLongSpell)
			{
				yield return PlayCriticalLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240");
				yield return new WaitForSeconds(1f);
			}
			if (!playLongSpell)
			{
				yield return new WaitForSeconds(1.5f);
			}
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			yield return PlayCriticalLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f");
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 12:
			GameState.Get().SetBusy(busy: true);
			yield return PlayMissionFlavorLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarTurn7_01.prefab:3ec08fc1a35e79c439b40fdcbcb0db1c");
			GameState.Get().SetBusy(busy: false);
			break;
		case 13:
			GameState.Get().SetBusy(busy: true);
			yield return PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FInalMalchezaarSacrificialPact_03.prefab:67beaf7b540d3dd46a3c302730ece026");
			GameState.Get().SetBusy(busy: false);
			break;
		case 14:
		{
			if (!ShouldPlayCriticalLine("VO_Moroes_Male_Human_FinalSecondSequence_01.prefab:7fafbacc56a622b4385a994f9b231240"))
			{
				break;
			}
			GameState.Get().SetBusy(busy: true);
			GameObject gameObject = GameObject.Find("Medivh_Hero");
			if (gameObject == null)
			{
				Log.All.PrintError("Could not find Medivh_Hero gameObject");
				GameState.Get().SetBusy(busy: false);
				break;
			}
			Actor component = gameObject.GetComponent<Actor>();
			if (component == null)
			{
				Log.All.PrintError("Could not find actor component for Medivh_Hero gameObject");
				GameState.Get().SetBusy(busy: false);
			}
			else
			{
				component.SetEntity(GameState.Get().GetFriendlySidePlayer().GetHeroCard()
					.GetEntity());
				yield return PlayCriticalLine(component, "VO_Medivh_Male_Human_FinalThirdSequence_03.prefab:9a08107f3d877f44bb4ebef53db3c708");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		}
		case 16:
		{
			if (!ShouldPlayLine("VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6", ShouldPlayLongCutscene))
			{
				break;
			}
			GameState.Get().SetBusy(busy: true);
			GameObject gameObject2 = GameObject.Find("Medivh_Hero");
			if (gameObject2 == null)
			{
				Log.All.PrintError("Could not find Medivh_Hero gameObject");
				GameState.Get().SetBusy(busy: false);
				break;
			}
			Actor medivhActor = gameObject2.GetComponent<Actor>();
			if (medivhActor == null)
			{
				Log.All.PrintError("Could not find actor component for Medivh_Hero gameObject");
				GameState.Get().SetBusy(busy: false);
				break;
			}
			medivhActor.SetEntity(GameState.Get().GetFriendlySidePlayer().GetHeroCard()
				.GetEntity());
			yield return new WaitForSeconds(1f);
			yield return PlayCriticalLine(medivhActor, "VO_Medivh_Male_Human_FinalThirdSequence_01.prefab:f7616460d19fffe4682697c0dd03d2b6");
			GameState.Get().SetBusy(busy: false);
			break;
		}
		case 17:
			GameState.Get().SetBusy(busy: true);
			yield return PlayEasterEggLine("Medivh_BigQuote.prefab:78e18a627031f6c48aef27a0fa1123c1", "VO_Medivh_Male_Human_FinalMedivhMedivhSkin_01.prefab:583768d83b20d12469684c50af5db9a0");
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

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

	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (cardId == "KARA_13_01" || cardId == "KARA_13_01H")
		{
			switch (turn)
			{
			case 1:
				yield return PlayOpeningLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalMaidenTurn1_01.prefab:f172c91c0b7f52a47b8d95e4c89a64db");
				break;
			case 7:
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalNazraTurn7_02.prefab:6a6ff647d93cf984a97c07d205013133");
				break;
			case 12:
				GameState.Get().SetBusy(busy: true);
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalAltTurn5_01.prefab:9c414f092ce0ec14d9daf94a7ad6ac1f");
				yield return PlayMissionFlavorLine("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalMaidenTurn7_03.prefab:05caa36abf0fa5a47ba8fcbb0f5f9b3d");
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
		if (m_playedLines.Contains(entity.GetCardId()))
		{
			yield break;
		}
		string cardId = entity.GetCardId();
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Entity hero = GameState.Get().GetOpposingSidePlayer().GetHero();
		switch (cardId)
		{
		case "KARA_13_11":
			m_playedLines.Add(cardId);
			yield return PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalMalchezaarEmoteResponse_01.prefab:80deff75e71de3941ac8b8d4167fb814");
			break;
		case "KARA_13_12":
			m_playedLines.Add(cardId);
			yield return PlayBossLine(actor, "VO_Malchezaar_Male_Demon_Brawl_06.prefab:7ebc074519c4bee4b9ee2dc3a022cb8a");
			break;
		case "KARA_00_02":
		case "KARA_13_13H":
			if (hero.GetTag(GAME_TAG.TAG_SCRIPT_DATA_NUM_1) > 2)
			{
				m_playedLines.Add(cardId);
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalMalchezaarHeroPower_01.prefab:35d5e0210c690e244830efff7b029b56");
				GameState.Get().SetBusy(busy: false);
			}
			break;
		case "EX1_312":
			m_playedLines.Add(cardId);
			yield return PlayBossLine(actor, "VO_Malchezaar_Male_Demon_FinalThirdSequence_04.prefab:2fd08853587f35e47898939b971b282c");
			break;
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		Actor friendlyActor = GameState.Get().GetFriendlySidePlayer().GetHero()
			.GetCard()
			.GetActor();
		string cardId = entity.GetCardId();
		switch (GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId())
		{
		case "KARA_13_01":
		case "KARA_13_01H":
			if (!(cardId == "EX1_414"))
			{
				if (cardId == "OG_121")
				{
					m_playedLines.Add(cardId);
					yield return new WaitForSeconds(3.7f);
					yield return PlayEasterEggLine(enemyActor, "VO_Nazra_Female_Orc_FinalNazraChogall_02.prefab:52d6f8fa86e142741ad1136a29722be7");
				}
			}
			else
			{
				m_playedLines.Add(cardId);
				yield return new WaitForSeconds(2.2f);
				yield return PlayEasterEggLine(enemyActor, "VO_Nazra_Female_Orc_FinalNazraGrom_01.prefab:225efe7172be03148af639919e115b69");
			}
			break;
		case "KARA_13_06":
		case "KARA_13_06H":
			if (!(cardId == "EX1_323"))
			{
				if (cardId == "CS2_034_H1")
				{
					m_playedLines.Add(cardId);
					yield return PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarMedivhSkin_01.prefab:d2ca787691dbb49468911ff1d1a5c1e6");
				}
				break;
			}
			m_playedLines.Add(cardId);
			if (ShouldPlayEasterEggLine("VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938"))
			{
				yield return new WaitForSeconds(5f);
				yield return PlayEasterEggLine(enemyActor, "VO_Malchezaar_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:c3955f874381c654a84c51134cb7d938");
				yield return PlayEasterEggLine(friendlyActor, "VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_01.prefab:9dc5c97f68e466a45a0e5cd3dafb6a1a");
				yield return PlayEasterEggLine(friendlyActor, "VO_Jaraxxus_Male_Demon_FinalMalchezaarJaraxxus_02.prefab:9d151ec830f37f947b6188c3235ea5cc");
			}
			break;
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult == TAG_PLAYSTATE.WON)
		{
			yield return new WaitForSeconds(5f);
			yield return PlayClosingLine("Medivh_Quote.prefab:423c4a6b7e7a7f643bf0b2992ad3d31b", "VO_Medivh_Male_Human_FinalMalchezaarWin_01.prefab:bf1f3b1d88b8dad42a03581c2e61a0e9");
		}
	}

	private void OnIntroSpellStateFinished(Spell spell, SpellStateType prevStateType, object userData)
	{
		if (!(m_introSpellInstance != spell) && m_introSpellInstance.GetActiveState() == SpellStateType.NONE)
		{
			Object.Destroy(m_introSpellInstance.gameObject, 5f);
			m_introSpellInstance = null;
		}
	}

	protected ShouldPlayValue ShouldPlayLongCutscene()
	{
		return ShouldPlayValue.Once;
	}

	public bool ShouldPlayLongMidmissionCutscene()
	{
		return ShouldPlayLine("VO_Malchezaar_Male_Demon_FinalThirdSequence_01.prefab:f3d5c93fce92fc8489d31c2472c6215f", ShouldPlayLongCutscene);
	}

	public override IEnumerator DoCustomIntro(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		if (!ShouldPlayLine("VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485", ShouldPlayLongCutscene))
		{
			yield break;
		}
		friendlyHeroLabel.SetFade(0f);
		enemyHeroLabel.SetFade(0f);
		versusText.gameObject.SetActive(value: false);
		friendlyHero.GetActor().Hide();
		enemyHero.GetActor().Hide();
		GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard()
			.GetActor()
			.Hide();
		yield return new WaitForSeconds(1.5f);
		if (!string.IsNullOrEmpty("Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907"))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("Nazra_PreMissionSummon.prefab:22f4f2bf8acd31541b4ce82bab9a1907");
			m_introSpellInstance = gameObject.GetComponent<Spell>();
			m_introSpellInstance.AddStateFinishedCallback(OnIntroSpellStateFinished);
			m_introSpellInstance.SetSource(friendlyHero.gameObject);
			m_introSpellInstance.AddTarget(enemyHero.gameObject);
			m_introSpellInstance.ActivateState(SpellStateType.ACTION);
			yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalThirdSequence_01.prefab:c1c97be0950451646bd4803829649485"));
			Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero()
				.GetCard()
				.GetActor();
			yield return new WaitForSeconds(4f);
			Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.TopRight;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				direction = Notification.SpeechBubbleDirection.TopLeft;
			}
			yield return Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_Nazra_Female_Orc_FinalNazraHeroPower_01.prefab:2e9958d6650ca08469a566b41f9a7df0", direction, enemyActor, 3f, 1f, parentBubbleToActor: false, delayCardSoundSpells: false, 0.6f));
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				yield return Gameplay.Get().StartCoroutine(PlayCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130", "VO_Moroes_Male_Human_FinalTurn1_02", new Vector3(-4f, 0f, 0f), 1f, 0f, allowRepeatDuringSession: true, delayCardSoundSpells: false, isBig: true));
			}
			else
			{
				yield return Gameplay.Get().StartCoroutine(PlayBigCharacterQuoteAndWait("Moroes_BigQuote.prefab:321274c1b67d79a4ba421a965bbc9e6d", "VO_Moroes_Male_Human_FinalTurn1_02.prefab:edfda4d27ac82974db1cb83c4628a130"));
			}
			while (m_introSpellInstance != null && !m_introSpellInstance.IsFinished())
			{
				yield return null;
			}
		}
		friendlyHeroLabel.FadeIn();
		enemyHeroLabel.FadeIn();
		versusText.gameObject.SetActive(value: true);
		versusText.FadeIn();
		Gameplay.Get().StartCoroutine(FlipInHeroPower());
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator FlipInHeroPower()
	{
		yield return new WaitForSeconds(6f);
		GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard()
			.GetActor()
			.Show();
		GameState.Get().GetOpposingSidePlayer().GetHeroPowerCard()
			.ActivateActorSpell(SpellType.SUMMON_IN);
	}

	public override void OnCustomIntroCancelled(Card friendlyHero, Card enemyHero, HeroLabel friendlyHeroLabel, HeroLabel enemyHeroLabel, GameStartVsLetters versusText)
	{
		if (m_introSpellInstance != null)
		{
			m_introSpellInstance.ActivateState(SpellStateType.CANCEL);
		}
	}
}
