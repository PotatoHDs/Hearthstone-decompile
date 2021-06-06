using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003A5 RID: 933
public class ICC_01_LICHKING : ICC_MissionEntity
{
	// Token: 0x06003566 RID: 13670 RVA: 0x0010F549 File Offset: 0x0010D749
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>();
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x0010F550 File Offset: 0x0010D750
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

	// Token: 0x06003568 RID: 13672 RVA: 0x0010F570 File Offset: 0x0010D770
	public ICC_01_LICHKING()
	{
		this.m_gameOptions.AddOptions(ICC_01_LICHKING.s_booleanOptions, ICC_01_LICHKING.s_stringOptions);
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x0010F5C8 File Offset: 0x0010D7C8
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_ICCLichKing);
	}

	// Token: 0x0600356A RID: 13674 RVA: 0x0010F5DC File Offset: 0x0010D7DC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		if (playerSide != Player.Side.OPPOSING)
		{
			return base.GetNameBannerSubtextOverride(playerSide);
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		if (opposingSidePlayer.GetHero() == opposingSidePlayer.GetStartingHero())
		{
			return GameStrings.Get("ICC_01_LICH_KING_SUBTEXT");
		}
		return GameStrings.Get("ICC_01_TIRION_SUBTEXT");
	}

	// Token: 0x0600356B RID: 13675 RVA: 0x0010F624 File Offset: 0x0010D824
	public override void PreloadAssets()
	{
		base.PreloadSound("VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858");
		base.PreloadSound("VO_ICC01_Jaina_Female_Human_JainaDKIntro_05.prefab:e2b9b4bad3d006d45a1ed347b0cf6662");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_EndOfTurn2_01.prefab:bf54a25aa869a9d4cafd7aca65c262c0");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_EndOfTurn2_02.prefab:d5fe6eeb5fb997848acead28c516fa06");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_PlaysFrostmourne_01.prefab:7a4d0659ac92e394c8f25378fa9283c5");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_01.prefab:31e70b7a1cd5d61498f391d40b4f7d43");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_02.prefab:4e7678cce8c778941bdec0e493bc9129");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKIntro_03.prefab:fb89ff5c830365347ae6494f74ad45a6");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Intro_01.prefab:7febb86d30cc95342823c0d6e9881573");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn1_01.prefab:e56daff85a5f2b840bdd5b24e8ea4dbf");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn11_01.prefab:663695085edbf034cba88620470622a7");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn13_01.prefab:d0d218202afbefd44ae379a95fe32383");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn15_01.prefab:49adda6df4448ac4cbb3bf3c7a3788bd");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn2_02.prefab:d7d93710894c71e46baa401198b75f8b");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn4_01.prefab:f9efdb3e91190e24fa143ab0f169e06a");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn4_02.prefab:e518e35bf465166488721f9ea11fdfc3");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_Turn5_02.prefab:2b02682c870a30d479674a47f24c112f");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_EndOfTurn4_01.prefab:36458bc5f111ecf47b5c23d6cb88eb5c");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_EndOfTurn4_03.prefab:6f98e2b658702824d926f0d448bfe537");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_JainaDKintro_05.prefab:81291d917a3f01c44a87c67f82fd2f6c");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_TerribleTank_01.prefab:9ea7d69e8eaf4ca47a17e8e7fd55af70");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_AFKay_01.prefab:1f645b92bb8cdac46b1580bf24bb895b");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_PauseDeath_02.prefab:8fcc6731e21286e4f8c2147d85bce59f");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Turn2_01.prefab:139c12e7eb223cf468f2663f68ae48da");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Turn3_01.prefab:3c5498e7af98dbb4b8bb48a28fc8d2df");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Turn6_01.prefab:cc757390f0689184f8fdfcb0f8b51c41");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Turn6_02.prefab:600bfa0e8bb7f6b4f8467e0ebc5aeb74");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Turn8_01.prefab:bd2fe6fcbfe02b340a11339ebe9f1bd6");
		base.PreloadSound("VO_ICC01_Jaina_Female_Human_DrawsRager_01.prefab:e8af0892cde840d41bf498a45573303f");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_JainaDKintro_06.prefab:4a0038f435eb069408b4231c55a712aa");
		base.PreloadSound("VO_ICC01_LichKing_Male_Human_EmoteResponse_01.prefab:e99b3bedcc63f8248bd0e47d26947a41");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_EmoteResponse_02.prefab:472ecefae2bb58d40b80e893ab4960af");
		base.PreloadSound("VO_ICC01_Tirion_Male_Human_Flavor01_01.prefab:fbf6ddd4207473d428897980504f0c54");
	}

	// Token: 0x0600356C RID: 13676 RVA: 0x0010F7B2 File Offset: 0x0010D9B2
	public IEnumerator ShowTurnCounter(string text)
	{
		Notification popup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.popUpPos, TutorialEntity.GetTextScale() * this.popUpScale, GameStrings.Get(text), false, NotificationManager.PopupTextType.FANCY);
		yield return new WaitForSeconds(ICC_01_LICHKING.TIRION_POPUP_DISPLAY_TIME);
		NotificationManager.Get().DestroyNotification(popup, 0f);
		yield break;
	}

	// Token: 0x0600356D RID: 13677 RVA: 0x0010F7C8 File Offset: 0x0010D9C8
	public override void OnPlayThinkEmote()
	{
		if (this.m_playedLines.Contains("VO_ICC01_LichKing_Male_Human_Idle_01"))
		{
			return;
		}
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		int turn = GameState.Get().GetTurn();
		if (turn == 1)
		{
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, "VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19", 2.5f));
			this.m_playedLines.Add("VO_ICC01_LichKing_Male_Human_Idle_01");
			return;
		}
		if (turn != 5)
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, "VO_ICC01_LichKing_Male_Human_Idle_01.prefab:4b9c6ba7bb584d04db2142a51282dd19", 2.5f));
		this.m_playedLines.Add("VO_ICC01_LichKing_Male_Human_Idle_01");
	}

	// Token: 0x0600356E RID: 13678 RVA: 0x0010F898 File Offset: 0x0010DA98
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		string cardId = GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_ICC01_LichKing_Male_Human_Intro_01.prefab:7febb86d30cc95342823c0d6e9881573", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (cardId == "ICCA01_001")
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_ICC01_LichKing_Male_Human_EmoteResponse_01.prefab:e99b3bedcc63f8248bd0e47d26947a41", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (!(cardId == "ICCA01_013"))
		{
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech("VO_ICC01_Tirion_Male_Human_EmoteResponse_02.prefab:472ecefae2bb58d40b80e893ab4960af", Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x0600356F RID: 13679 RVA: 0x0010F979 File Offset: 0x0010DB79
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		string item = "PLAYED_MISSION_EVENT_" + missionEvent;
		if (this.m_playedLines.Contains(item))
		{
			yield break;
		}
		switch (missionEvent)
		{
		case 101:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_EndOfTurn2_01.prefab:bf54a25aa869a9d4cafd7aca65c262c0", 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_EndOfTurn2_02.prefab:d5fe6eeb5fb997848acead28c516fa06", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 102:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_PlaysFrostmourne_01.prefab:7a4d0659ac92e394c8f25378fa9283c5", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 103:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_EndOfTurn4_01.prefab:36458bc5f111ecf47b5c23d6cb88eb5c", 2.5f);
			yield return base.PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_EndOfTurn4_03.prefab:6f98e2b658702824d926f0d448bfe537", 2.5f);
			GameState.Get().SetBusy(false);
			yield return this.ShowTurnCounter(ICC_01_LICHKING.TEXT_TIRION_TURN_1);
			break;
		case 105:
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).SetName("");
			Gameplay.Get().GetNameBannerForSide(Player.Side.OPPOSING).UpdateHeroNameBanner();
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(enemyActor, "VO_ICC01_Tirion_Male_Human_JainaDKintro_05.prefab:81291d917a3f01c44a87c67f82fd2f6c", 2.5f);
			GameState.Get().SetBusy(false);
			yield return base.PlayBossLine("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_JainaDKintro_06.prefab:4a0038f435eb069408b4231c55a712aa", 2.5f);
			break;
		case 107:
			this.m_playedLines.Add(item);
			yield return new WaitForSeconds(0.7f);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_TerribleTank_01.prefab:9ea7d69e8eaf4ca47a17e8e7fd55af70", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 108:
			this.m_playedLines.Add(item);
			yield return new WaitForSeconds(5.7f);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_AFKay_01.prefab:1f645b92bb8cdac46b1580bf24bb895b", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 109:
			yield return new WaitForSeconds(0.75f);
			yield return base.PlayBossLine(enemyActor, "VO_ICC01_Tirion_Male_Human_PauseDeath_02.prefab:8fcc6731e21286e4f8c2147d85bce59f", 2.5f);
			break;
		case 111:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine(actor, "VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 112:
			this.m_playedLines.Add(item);
			yield return new WaitForSeconds(2.2f);
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_Flavor01_01.prefab:fbf6ddd4207473d428897980504f0c54", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 114:
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn11_01.prefab:663695085edbf034cba88620470622a7", 2.5f);
			break;
		case 115:
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn13_01.prefab:d0d218202afbefd44ae379a95fe32383", 2.5f);
			break;
		case 116:
			yield return base.PlayLineOnlyOnce("LichKing_BigQuote.prefab:6d0439b386dc3cc41a591f989cbb93ed", "VO_ICC01_LichKing_Male_Human_Turn15_01.prefab:49adda6df4448ac4cbb3bf3c7a3788bd", 2.5f);
			break;
		}
		yield break;
	}

	// Token: 0x06003570 RID: 13680 RVA: 0x0010F98F File Offset: 0x0010DB8F
	protected override IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		switch (turn)
		{
		case 1:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn1_01.prefab:e56daff85a5f2b840bdd5b24e8ea4dbf", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 2:
			GameState.Get().SetBusy(true);
			yield return base.PlayBossLine("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn2_01.prefab:139c12e7eb223cf468f2663f68ae48da", 2.5f);
			yield return this.ShowTurnCounter(ICC_01_LICHKING.TEXT_TIRION_TURN_3);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn2_02.prefab:d7d93710894c71e46baa401198b75f8b", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 3:
			yield return base.PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn3_01.prefab:3c5498e7af98dbb4b8bb48a28fc8d2df", 2.5f);
			yield return this.ShowTurnCounter(ICC_01_LICHKING.TEXT_TIRION_TURN_2);
			break;
		case 4:
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn4_01.prefab:f9efdb3e91190e24fa143ab0f169e06a", 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn4_02.prefab:e518e35bf465166488721f9ea11fdfc3", 2.5f);
			break;
		case 5:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC01_Jaina_Female_Human_DrawsRager_01.prefab:e8af0892cde840d41bf498a45573303f", 2.5f);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_LichKing_Male_Human_Turn5_02.prefab:2b02682c870a30d479674a47f24c112f", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		case 6:
			yield return base.PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn6_01.prefab:cc757390f0689184f8fdfcb0f8b51c41", 2.5f);
			yield return base.PlayLineOnlyOnce("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", "VO_ICC01_Tirion_Male_Human_Turn6_02.prefab:600bfa0e8bb7f6b4f8467e0ebc5aeb74", 2.5f);
			break;
		case 7:
			yield return base.PlayLineOnlyOnce(actor, "VO_ICC01_Jaina_Female_Human_JainaDKIntro_05.prefab:e2b9b4bad3d006d45a1ed347b0cf6662", 2.5f);
			break;
		case 8:
			GameState.Get().SetBusy(true);
			yield return base.PlayLineOnlyOnce(enemyActor, "VO_ICC01_Tirion_Male_Human_Turn8_01.prefab:bd2fe6fcbfe02b340a11339ebe9f1bd6", 2.5f);
			GameState.Get().SetBusy(false);
			break;
		}
		yield break;
	}

	// Token: 0x06003571 RID: 13681 RVA: 0x0010F9A8 File Offset: 0x0010DBA8
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

	// Token: 0x06003572 RID: 13682 RVA: 0x0010FA38 File Offset: 0x0010DC38
	public override string GetDefeatScreenBannerText()
	{
		if (!GameState.Get().IsGameOver())
		{
			return GameStrings.Get("GAMEPLAY_END_OF_GAME_DEFEAT_MAYBE");
		}
		return base.GetDefeatScreenBannerText();
	}

	// Token: 0x06003573 RID: 13683 RVA: 0x0010FA57 File Offset: 0x0010DC57
	public IEnumerator PlayLichKingRezLines()
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().SetBusy(true);
		yield return base.PlayBossLine(enemyActor, "VO_ICC01_LichKing_Male_Human_JainaDKIntro_01.prefab:31e70b7a1cd5d61498f391d40b4f7d43", 2.5f);
		yield return base.PlayBossLine(enemyActor, "VO_ICC01_LichKing_Male_Human_JainaDKIntro_02.prefab:4e7678cce8c778941bdec0e493bc9129", 2.5f);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x06003574 RID: 13684 RVA: 0x0010FA66 File Offset: 0x0010DC66
	public IEnumerator PlayTirionVictoryScreenLine()
	{
		AudioSource preloadedSound = base.GetPreloadedSound("VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0");
		float num = 6.8f;
		if (preloadedSound != null && preloadedSound.clip != null)
		{
			num = preloadedSound.clip.length;
		}
		else
		{
			Log.Gameplay.PrintError("ICC_01_Lichking.PlayTirionVictoryScreenLine() - failed to find Preloaded Sound \"VO_ICC01_Tirion_Male_Human_PostDeath_01\"", Array.Empty<object>());
		}
		Notification notification = NotificationManager.Get().CreateBigCharacterQuoteWithText("Tirion_BigQuote.prefab:878fcebc1cddaf24f828c44edb07f7f8", NotificationManager.DEFAULT_CHARACTER_POS, "VO_ICC01_Tirion_Male_Human_PostDeath_01.prefab:baaf7891e78de8343ae7fca845615ed0", GameStrings.Get("VO_ICC01_Tirion_Male_Human_PostDeath_01"), num + 1f, null, false, Notification.SpeechBubbleDirection.BottomLeft, false, false);
		PlayMakerFSM fsm = notification.GetComponentInChildren<PlayMakerFSM>();
		if (fsm == null)
		{
			Log.Gameplay.PrintError("ICC_01_Lichking.PlayTirionVictoryScreenLine(): Tirion_BigQuote prefab does not have a PlayMakerFSM in its children!", Array.Empty<object>());
			yield break;
		}
		yield return new WaitForSeconds(num);
		fsm.SendEvent("DoEffect");
		yield return new WaitForSeconds(1f);
		yield break;
	}

	// Token: 0x06003575 RID: 13685 RVA: 0x0010FA75 File Offset: 0x0010DC75
	public IEnumerator PlayJainaVictoryScreenLine(Actor jaina)
	{
		GameState.Get().SetBusy(true);
		float bubbleScale = UniversalInputManager.UsePhoneUI ? 0.5f : 0.75f;
		yield return base.PlaySoundAndBlockSpeech("VO_ICC01_Jaina_Female_Human_PostDeath_02.prefab:32db8cb82e111fd4c8bb56f5db507858", Notification.SpeechBubbleDirection.BottomLeft, jaina, 2f, 1f, false, false, bubbleScale);
		GameState.Get().SetBusy(false);
		yield break;
	}

	// Token: 0x04001CE1 RID: 7393
	private static Map<GameEntityOption, bool> s_booleanOptions = ICC_01_LICHKING.InitBooleanOptions();

	// Token: 0x04001CE2 RID: 7394
	private static Map<GameEntityOption, string> s_stringOptions = ICC_01_LICHKING.InitStringOptions();

	// Token: 0x04001CE3 RID: 7395
	private static readonly string TEXT_TIRION_TURN_1 = "ICC_01_TIRIONTURNS_01";

	// Token: 0x04001CE4 RID: 7396
	private static readonly string TEXT_TIRION_TURN_2 = "ICC_01_TIRIONTURNS_02";

	// Token: 0x04001CE5 RID: 7397
	private static readonly string TEXT_TIRION_TURN_3 = "ICC_01_TIRIONTURNS_03";

	// Token: 0x04001CE6 RID: 7398
	private static readonly float TIRION_POPUP_DISPLAY_TIME = 2.5f;

	// Token: 0x04001CE7 RID: 7399
	private Notification TirionTurnPopup;

	// Token: 0x04001CE8 RID: 7400
	private Vector3 popUpPos = new Vector3(0f, 0f, 4f);

	// Token: 0x04001CE9 RID: 7401
	private float popUpScale = 1f;

	// Token: 0x04001CEA RID: 7402
	private HashSet<string> m_playedLines = new HashSet<string>();
}
