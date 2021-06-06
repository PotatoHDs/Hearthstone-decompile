using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ICC_01_LICHKING : ICC_MissionEntity
{
	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	private static readonly string TEXT_TIRION_TURN_1 = "ICC_01_TIRIONTURNS_01";

	private static readonly string TEXT_TIRION_TURN_2 = "ICC_01_TIRIONTURNS_02";

	private static readonly string TEXT_TIRION_TURN_3 = "ICC_01_TIRIONTURNS_03";

	private static readonly float TIRION_POPUP_DISPLAY_TIME = 2.5f;

	private Notification TirionTurnPopup;

	private Vector3 popUpPos = new Vector3(0f, 0f, 4f);

	private float popUpScale = 1f;

	private HashSet<string> m_playedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>();
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>
		{
			{
				GameEntityOption.VICTORY_SCREEN_PREFAB_PATH,
				"VictoryTwoScoop_ICCPrologue.prefab:a9e377ed0578dc14aa0029dc4af183cb"
			},
			{
				GameEntityOption.VICTORY_AUDIO_PATH,
				null
			}
		};
	}

	public ICC_01_LICHKING()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
	}

	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		if (playerSide == Player.Side.OPPOSING)
		{
			Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
			if (opposingSidePlayer.GetHero() == opposingSidePlayer.GetStartingHero())
			{
				return GameStrings.Get("ICC_01_LICH_KING_SUBTEXT");
			}
			return GameStrings.Get("ICC_01_TIRION_SUBTEXT");
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	public override void PreloadAssets()
	{
		PreloadSound("VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858");
		PreloadSound("VO_ICC01_Jaina_Female_Human_JainaDKIntro_05.prefab:e2b9b4bad3d006d45a1ed347b0cf6662");
		PreloadSound("VO_ICC01_LichKing_Male_Human_EndOfTurn2_01.prefab:bf54a25aa869a9d4cafd7aca65c262c0");
		PreloadSound("VO_ICC01_LichKing_Male_Human_EndOfTurn2_02.prefab:d5fe6eeb5fb997848acead28c516fa06");
		PreloadSound("VO_ICC01_LichKing_Male_Human_PlaysFrostmourne_01.prefab:7a4d0659ac92e394c8f25378fa9283c5");
		PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_01.prefab:31e70b7a1cd5d61498f391d40b4f7d43");
		PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_02.prefab:4e7678cce8c778941bdec0e493bc9129");
		PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_03.prefab:fb89ff5c830365347ae6494f74ad45a6");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Intro_01.prefab:7febb86d30cc95342823c0d6e9881573");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn1_01.prefab:e56daff85a5f2b840bdd5b24e8ea4dbf");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn11_01.prefab:663695085edbf034cba88620470622a7");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn13_01.prefab:d0d218202afbefd44ae379a95fe32383");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn15_01.prefab:49adda6df4448ac4cbb3bf3c7a3788bd");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn2_02.prefab:d7d93710894c71e46baa401198b75f8b");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn4_01.prefab:f9efdb3e91190e24fa143ab0f169e06a");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn4_02.prefab:e518e35bf465166488721f9ea11fdfc3");
		PreloadSound("VO_ICC01_LichKing_Male_Human_Turn5_02.prefab:2b02682c870a30d479674a47f24c112f");
		PreloadSound("VO_ICC01_Tirion_Male_Human_EndOfTurn4_01.prefab:36458bc5f111ecf47b5c23d6cb88eb5c");
		PreloadSound("VO_ICC01_Tirion_Male_Human_EndOfTurn4_03.prefab:6f98e2b658702824d926f0d448bfe537");
		PreloadSound("VO_ICC01_Tirion_Male_Human_JainaDKintro_05.prefab:81291d917a3f01c44a87c67f82fd2f6c");
		PreloadSound("VO_ICC01_Tirion_Male_Human_TerribleTank_01.prefab:9ea7d69e8eaf4ca47a17e8e7fd55af70");
		PreloadSound("VO_ICC01_Tirion_Male_Human_AFKay_01.prefab:1f645b92bb8cdac46b1580bf24bb895b");
		PreloadSound("VO_ICC01_Tirion_Male_Human_PauseDeath_02.prefab:8fcc6731e21286e4f8c2147d85bce59f");
		PreloadSound("VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Turn2_01.prefab:139c12e7eb223cf468f2663f68ae48da");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Turn3_01.prefab:3c5498e7af98dbb4b8bb48a28fc8d2df");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Turn6_01.prefab:cc757390f0689184f8fdfcb0f8b51c41");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Turn6_02.prefab:600bfa0e8bb7f6b4f8467e0ebc5aeb74");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Turn8_01.prefab:bd2fe6fcbfe02b340a11339ebe9f1bd6");
		PreloadSound("VO_ICC01_Jaina_Female_Human_DrawsRager_01.prefab:e8af0892cde840d41bf498a45573303f");
		PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKintro_06.prefab:4a0038f435eb069408b4231c55a712aa");
		PreloadSound("VO_ICC01_LichKing_Male_Human_EmoteResponse_01.prefab:e99b3bedcc63f8248bd0e47d26947a41");
		PreloadSound("VO_ICC01_Tirion_Male_Human_EmoteResponse_02.prefab:472ecefae2bb58d40b80e893ab4960af");
		PreloadSound("VO_ICC01_Tirion_Male_Human_Flavor01_01.prefab:fbf6ddd4207473d428897980504f0c54");
	}

	public IEnumerator ShowTurnCounter(string text)
	{
		Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, popUpPos, TutorialEntity.GetTextScale() * popUpScale, GameStrings.Get(text), convertLegacyPosition: false, NotificationManager.PopupTextType.FANCY);
		yield return new WaitForSeconds(TIRION_POPUP_DISPLAY_TIME);
		NotificationManager.Get().DestroyNotification(popup, 0f);
	}

	public override void OnPlayThinkEmote()
	{
		if (m_playedLines.Contains("VO_ICC01_LichKing_Male_Human_Idle_01") || m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (currentPlayer.IsFriendlySide() && !currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			switch (GameState.Get().GetTurn())
			{
			case 1:
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, "VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19"));
				m_playedLines.Add("VO_ICC01_LichKing_Male_Human_Idle_01");
				break;
			case 5:
				Gameplay.Get().StartCoroutine(PlayBossLine(actor, "VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19"));
				m_playedLines.Add("VO_ICC01_LichKing_Male_Human_Idle_01");
				break;
			}
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_ICC01_LichKing_Male_Human_Intro_01.prefab:7febb86d30cc95342823c0d6e9881573", Notification.SpeechBubbleDirection.TopRight, actor));
		}
		else
		{
			if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
			{
				return;
			}
			if (!(cardId == "ICCA01_001"))
			{
				if (cardId == "ICCA01_013")
				{
					Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_ICC01_Tirion_Male_Human_EmoteResponse_02.prefab:472ecefae2bb58d40b80e893ab4960af", Notification.SpeechBubbleDirection.TopRight, actor));
				}
			}
			else
			{
				Gameplay.Get().StartCoroutine(PlaySoundAndBlockSpeech("VO_ICC01_LichKing_Male_Human_EmoteResponse_01.prefab:e99b3bedcc63f8248bd0e47d26947a41", Notification.SpeechBubbleDirection.TopRight, actor));
			}
		}
	}

	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (!m_playedLines.Contains(item))
		{
			switch (missionEvent)
			{
			case 101:
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_EndOfTurn2_01.prefab:bf54a25aa869a9d4cafd7aca65c262c0");
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_EndOfTurn2_02.prefab:d5fe6eeb5fb997848acead28c516fa06");
				GameState.Get().SetBusy(busy: false);
				break;
			case 102:
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_PlaysFrostmourne_01.prefab:7a4d0659ac92e394c8f25378fa9283c5");
				GameState.Get().SetBusy(busy: false);
				break;
			case 103:
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_EndOfTurn4_01.prefab:36458bc5f111ecf47b5c23d6cb88eb5c");
				yield return PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_EndOfTurn4_03.prefab:6f98e2b658702824d926f0d448bfe537");
				GameState.Get().SetBusy(busy: false);
				yield return ShowTurnCounter(TEXT_TIRION_TURN_1);
				break;
			case 105:
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
				Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(enemyActor, "VO_ICC01_Tirion_Male_Human_JainaDKintro_05.prefab:81291d917a3f01c44a87c67f82fd2f6c");
				GameState.Get().SetBusy(busy: false);
				yield return PlayBossLine("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_JainaDKintro_06.prefab:4a0038f435eb069408b4231c55a712aa");
				break;
			case 107:
				m_playedLines.Add(item);
				yield return new WaitForSeconds(0.7f);
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_TerribleTank_01.prefab:9ea7d69e8eaf4ca47a17e8e7fd55af70");
				GameState.Get().SetBusy(busy: false);
				break;
			case 108:
				m_playedLines.Add(item);
				yield return new WaitForSeconds(5.7f);
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_AFKay_01.prefab:1f645b92bb8cdac46b1580bf24bb895b");
				GameState.Get().SetBusy(busy: false);
				break;
			case 109:
				yield return new WaitForSeconds(0.75f);
				yield return PlayBossLine(enemyActor, "VO_ICC01_Tirion_Male_Human_PauseDeath_02.prefab:8fcc6731e21286e4f8c2147d85bce59f");
				break;
			case 111:
				GameState.Get().SetBusy(busy: true);
				yield return PlayBossLine(actor, "VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858");
				GameState.Get().SetBusy(busy: false);
				break;
			case 112:
				m_playedLines.Add(item);
				yield return new WaitForSeconds(2.2f);
				GameState.Get().SetBusy(busy: true);
				yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_Flavor01_01.prefab:fbf6ddd4207473d428897980504f0c54");
				GameState.Get().SetBusy(busy: false);
				break;
			case 114:
				yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn11_01.prefab:663695085edbf034cba88620470622a7");
				break;
			case 115:
				yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn13_01.prefab:d0d218202afbefd44ae379a95fe32383");
				break;
			case 116:
				yield return PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn15_01.prefab:49adda6df4448ac4cbb3bf3c7a3788bd");
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
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard()
			.GetActor();
		switch (turn)
		{
		case 1:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn1_01.prefab:e56daff85a5f2b840bdd5b24e8ea4dbf");
			GameState.Get().SetBusy(busy: false);
			break;
		case 2:
			GameState.Get().SetBusy(busy: true);
			yield return PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn2_01.prefab:139c12e7eb223cf468f2663f68ae48da");
			yield return ShowTurnCounter(TEXT_TIRION_TURN_3);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn2_02.prefab:d7d93710894c71e46baa401198b75f8b");
			GameState.Get().SetBusy(busy: false);
			break;
		case 3:
			yield return PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn3_01.prefab:3c5498e7af98dbb4b8bb48a28fc8d2df");
			yield return ShowTurnCounter(TEXT_TIRION_TURN_2);
			break;
		case 4:
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn4_01.prefab:f9efdb3e91190e24fa143ab0f169e06a");
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn4_02.prefab:e518e35bf465166488721f9ea11fdfc3");
			break;
		case 5:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(actor, "VO_ICC01_Jaina_Female_Human_DrawsRager_01.prefab:e8af0892cde840d41bf498a45573303f");
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn5_02.prefab:2b02682c870a30d479674a47f24c112f");
			GameState.Get().SetBusy(busy: false);
			break;
		case 6:
			yield return PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn6_01.prefab:cc757390f0689184f8fdfcb0f8b51c41");
			yield return PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn6_02.prefab:600bfa0e8bb7f6b4f8467e0ebc5aeb74");
			break;
		case 7:
			yield return PlayLineOnlyOnce(actor, "VO_ICC01_Jaina_Female_Human_JainaDKIntro_05.prefab:e2b9b4bad3d006d45a1ed347b0cf6662");
			break;
		case 8:
			GameState.Get().SetBusy(busy: true);
			yield return PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_Turn8_01.prefab:bd2fe6fcbfe02b340a11339ebe9f1bd6");
			GameState.Get().SetBusy(busy: false);
			break;
		}
	}

	private Actor GetActorbyCardId(string cardId)
	{
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == opposingSidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	public override string GetDefeatScreenBannerText()
	{
		if (!GameState.Get().IsGameOver())
		{
			return GameStrings.Get("GAMEPLAY_END_OF_GAME_DEFEAT_MAYBE");
		}
		return base.GetDefeatScreenBannerText();
	}

	public IEnumerator PlayLichKingRezLines()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		GameState.Get().SetBusy(busy: true);
		yield return PlayBossLine(enemyActor, "VO_ICC01_LichKing_Male_Human_JainaDKIntro_01.prefab:31e70b7a1cd5d61498f391d40b4f7d43");
		yield return PlayBossLine(enemyActor, "VO_ICC01_LichKing_Male_Human_JainaDKIntro_02.prefab:4e7678cce8c778941bdec0e493bc9129");
		GameState.Get().SetBusy(busy: false);
	}

	public IEnumerator PlayTirionVictoryScreenLine()
	{
		AudioSource preloadedSound = GetPreloadedSound("VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0");
		float num = 6.8f;
		if (preloadedSound != null && preloadedSound.clip != null)
		{
			num = preloadedSound.clip.length;
		}
		else
		{
			Log.Gameplay.PrintError("ICC_01_Lichking.PlayTirionVictoryScreenLine() - failed to find Preloaded Sound \"VO_ICC01_Tirion_Male_Human_PostDeath_01\"");
		}
		Notification notification = NotificationManager.Get().CreateBigCharacterQuoteWithText("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", NotificationManager.DEFAULT_CHARACTER_POS, "VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0", GameStrings.Get("VO_ICC01_Tirion_Male_Human_PostDeath_01"), num + 1f, null, useOverlayUI: false, Notification.SpeechBubbleDirection.BottomLeft);
		PlayMakerFSM fsm = notification.GetComponentInChildren<PlayMakerFSM>();
		if (fsm == null)
		{
			Log.Gameplay.PrintError("ICC_01_Lichking.PlayTirionVictoryScreenLine(): Tirion_BigQuote prefab does not have a PlayMakerFSM in its children!");
			yield break;
		}
		yield return new WaitForSeconds(num);
		fsm.SendEvent("DoEffect");
		yield return new WaitForSeconds(1f);
	}

	public IEnumerator PlayJainaVictoryScreenLine(Actor jaina)
	{
		GameState.Get().SetBusy(busy: true);
		float bubbleScale = (UniversalInputManager.UsePhoneUI ? 0.5f : 0.75f);
		yield return PlaySoundAndBlockSpeech("VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858", Notification.SpeechBubbleDirection.BottomLeft, jaina, 2f, 1f, parentBubbleToActor: false, delayCardSoundSpells: false, bubbleScale);
		GameState.Get().SetBusy(busy: false);
	}
}
