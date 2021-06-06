using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class BTA_Dungeon_Heroic : BTA_MissionEntity_Heroic
{
	// Token: 0x06004369 RID: 17257 RVA: 0x0016D774 File Offset: 0x0016B974
	public static BTA_Dungeon_Heroic InstantiateBTADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 1471588522U)
		{
			if (num <= 154342855U)
			{
				if (num != 120243164U)
				{
					if (num == 154342855U)
					{
						if (opposingHeroCardID == "BTA_BOSS_20h")
						{
							return new BTA_Fight_19();
						}
					}
				}
				else if (opposingHeroCardID == "BTA_BOSS_23h")
				{
					return new BTA_Fight_23();
				}
			}
			else if (num != 187059450U)
			{
				if (num == 1471588522U)
				{
					if (opposingHeroCardID == "BTA_BOSS_18h")
					{
						return new BTA_Fight_18();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_25h")
			{
				return new BTA_Fight_25();
			}
		}
		else if (num <= 3777513843U)
		{
			if (num != 1840843235U)
			{
				if (num == 3777513843U)
				{
					if (opposingHeroCardID == "BTA_BOSS_24h")
					{
						return new BTA_Fight_24();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_19h")
			{
				return new BTA_Fight_20();
			}
		}
		else if (num != 3811613534U)
		{
			if (num != 3844330129U)
			{
				if (num == 3979139461U)
				{
					if (opposingHeroCardID == "BTA_BOSS_22h")
					{
						return new BTA_Fight_22();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_26h")
			{
				return new BTA_Fight_26();
			}
		}
		else if (opposingHeroCardID == "BTA_BOSS_21h")
		{
			return new BTA_Fight_21();
		}
		Log.All.PrintError("BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BTA_Dungeon_Heroic();
	}

	// Token: 0x0600436A RID: 17258 RVA: 0x0016D900 File Offset: 0x0016BB00
	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>();
		this.m_PlayerVOLines = new List<string>(list);
		foreach (string soundPath in list)
		{
			base.PreloadSound(soundPath);
		}
	}

	// Token: 0x0600436B RID: 17259 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x0600436C RID: 17260 RVA: 0x0016D968 File Offset: 0x0016BB68
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x0600436D RID: 17261 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600436E RID: 17262 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x0600436F RID: 17263 RVA: 0x0016D976 File Offset: 0x0016BB76
	protected virtual IEnumerable OnBossHeroPowerPlayed(Entity entity)
	{
		float chanceToPlay = this.ChanceToPlayBossHeroPowerVOLine();
		float chanceRoll = UnityEngine.Random.Range(0f, 1f);
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (chanceToPlay < chanceRoll)
		{
			yield return null;
		}
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (enemyActor == null)
		{
			yield break;
		}
		List<string> bossHeroPowerRandomLines = this.GetBossHeroPowerRandomLines();
		string text = "";
		while (bossHeroPowerRandomLines.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, bossHeroPowerRandomLines.Count);
			text = bossHeroPowerRandomLines[index];
			bossHeroPowerRandomLines.RemoveAt(index);
			if (!NotificationManager.Get().HasSoundPlayedThisSession(text))
			{
				break;
			}
		}
		if (text == "")
		{
			yield return null;
		}
		yield return base.PlayLineAlways(enemyActor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
		yield break;
	}

	// Token: 0x06004370 RID: 17264 RVA: 0x0016D986 File Offset: 0x0016BB86
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield break;
		}
		if (entity.GetCardType() != TAG_CARDTYPE.HERO_POWER)
		{
			yield break;
		}
		if (entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			yield return this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x06004371 RID: 17265 RVA: 0x0016D99C File Offset: 0x0016BB9C
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		GameState.Get().GetFriendlySidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetFriendlySidePlayer().GetHero().GetCardId();
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		entity.GetCardId();
		yield break;
	}

	// Token: 0x06004372 RID: 17266 RVA: 0x0016D9B4 File Offset: 0x0016BBB4
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (!this.m_enemySpeaking && !string.IsNullOrEmpty(this.m_deathLine) && gameResult == TAG_PLAYSTATE.WON)
		{
			if (this.GetShouldSuppressDeathTextBubble())
			{
				Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.None, actor, 3f, 1f, true, false, 0f));
				return;
			}
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_deathLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
		}
	}

	// Token: 0x06004373 RID: 17267 RVA: 0x0016DA54 File Offset: 0x0016BC54
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004374 RID: 17268 RVA: 0x0016DADD File Offset: 0x0016BCDD
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06004375 RID: 17269 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06004376 RID: 17270 RVA: 0x0016DB1C File Offset: 0x0016BD1C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		if (missionEvent == 911)
		{
			GameState.Get().SetBusy(true);
			while (this.m_enemySpeaking)
			{
				yield return null;
			}
			GameState.Get().SetBusy(false);
			yield break;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		UnityEngine.Random.Range(0f, 1f);
		base.GetTag(GAME_TAG.TURN);
		GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		switch (missionEvent)
		{
		case 1000:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (this.m_PlayPlayerVOLineIndex + 1 >= this.m_PlayerVOLines.Count)
			{
				this.m_PlayPlayerVOLineIndex = 0;
			}
			else
			{
				this.m_PlayPlayerVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
			yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
			goto IL_659;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
			yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
			goto IL_659;
		case 1002:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			if (this.m_PlayBossVOLineIndex + 1 >= this.m_BossVOLines.Count)
			{
				this.m_PlayBossVOLineIndex = 0;
			}
			else
			{
				this.m_PlayBossVOLineIndex++;
			}
			SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
			yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
			goto IL_659;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
			yield return base.PlayBossLine(enemyActor, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
			goto IL_659;
		case 1004:
		case 1005:
		case 1006:
		case 1007:
		case 1008:
		case 1009:
			break;
		case 1010:
			if (this.m_forceAlwaysPlayLine)
			{
				this.m_forceAlwaysPlayLine = false;
				goto IL_659;
			}
			this.m_forceAlwaysPlayLine = true;
			goto IL_659;
		case 1011:
		{
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string text in this.m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(text);
				yield return base.PlayLineAlways(enemyActor, text, 2.5f);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			foreach (string text2 in this.m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(text2);
				yield return base.PlayLineAlways(enemyActor, text2, 2.5f);
			}
			enumerator = default(List<string>.Enumerator);
			goto IL_659;
		}
		case 1012:
		{
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string text3 in this.m_BossVOLines)
			{
				SceneDebugger.Get().AddMessage(text3);
				yield return base.PlayLineAlways(enemyActor, text3, 2.5f);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			goto IL_659;
		}
		case 1013:
		{
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			foreach (string text4 in this.m_PlayerVOLines)
			{
				SceneDebugger.Get().AddMessage(text4);
				yield return base.PlayLineAlways(enemyActor, text4, 2.5f);
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			goto IL_659;
		}
		default:
			if (missionEvent == 58023)
			{
				SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
				GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
				SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				goto IL_659;
			}
			break;
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_659:
		yield break;
		yield break;
	}

	// Token: 0x06004377 RID: 17271 RVA: 0x0016CF56 File Offset: 0x0016B156
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		GameMgr.Get().GetMissionId();
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004378 RID: 17272 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004379 RID: 17273 RVA: 0x0016DB34 File Offset: 0x0016BD34
	protected Actor GetEnemyActorByCardId(string cardId)
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

	// Token: 0x0600437A RID: 17274 RVA: 0x0016DBC4 File Offset: 0x0016BDC4
	protected Actor GetFriendlyActorByCardId(string cardId)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == cardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x0600437B RID: 17275 RVA: 0x0016DC54 File Offset: 0x0016BE54
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600437C RID: 17276 RVA: 0x0016DC71 File Offset: 0x0016BE71
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600437D RID: 17277 RVA: 0x0016DC8E File Offset: 0x0016BE8E
	public IEnumerator PlayAndRemoveRandomLineOnlyOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return base.PlayAndRemoveRandomLineOnlyOnce(brassRingBackup, lines);
		}
		yield break;
	}

	// Token: 0x0600437E RID: 17278 RVA: 0x0016DCB2 File Offset: 0x0016BEB2
	protected IEnumerator PlayLineAlwaysWithBrassRing(Actor actor, AssetReference brassRingBackup, string line, float duration = 2.5f)
	{
		if (actor != null)
		{
			yield return base.PlayLineAlways(actor, line, 2.5f);
		}
		else if (brassRingBackup != null)
		{
			yield return base.PlayLineAlways(brassRingBackup, line, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600437F RID: 17279 RVA: 0x0016DCD6 File Offset: 0x0016BED6
	public IEnumerator PlayLineInOrderOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return base.PlayLineInOrderOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return base.PlayLineInOrderOnce(brassRingBackup, lines);
		}
		yield break;
	}

	// Token: 0x06004380 RID: 17280 RVA: 0x0016DCFC File Offset: 0x0016BEFC
	public override void OnPlayThinkEmote()
	{
		if (this.m_DisableIdle)
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
		float thinkEmoteBossThinkChancePercentage = this.GetThinkEmoteBossThinkChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossThinkChancePercentage > num && this.m_BossIdleLines != null && this.m_BossIdleLines.Count != 0)
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = base.PopRandomLine(this.m_BossIdleLinesCopy);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
			}
			Gameplay.Get().StartCoroutine(base.PlayBossLine(actor, line, 2.5f));
			return;
		}
		EmoteType emoteType = EmoteType.THINK1;
		switch (UnityEngine.Random.Range(1, 4))
		{
		case 1:
			emoteType = EmoteType.THINK1;
			break;
		case 2:
			emoteType = EmoteType.THINK2;
			break;
		case 3:
			emoteType = EmoteType.THINK3;
			break;
		}
		GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
	}

	// Token: 0x04003559 RID: 13657
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x0400355A RID: 13658
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x0400355B RID: 13659
	public bool m_DisableIdle;

	// Token: 0x0400355C RID: 13660
	private static readonly AssetReference Karnuk_Demon_Hunter_Popup_BrassRing = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	// Token: 0x0400355D RID: 13661
	public static readonly AssetReference KarnukBrassRing = new AssetReference("Karnuk_Outcast_Popup_BrassRing.prefab:d097e6294875881488492604e9320e64");

	// Token: 0x0400355E RID: 13662
	public static readonly AssetReference KarnukBrassRingDemonHunter = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	// Token: 0x0400355F RID: 13663
	public static readonly AssetReference ShaljaBrassRing = new AssetReference("Shalja_Outcast_Popup_BrassRing.prefab:0425972e057e448458abedcc24797c3a");

	// Token: 0x04003560 RID: 13664
	public static readonly AssetReference ShaljaBrassRingDemonHunter = new AssetReference("Shalja_Demon_Hunter_Popup_BrassRing.prefab:08f4bb41a6104a94ca96bb8003fa826f");

	// Token: 0x04003561 RID: 13665
	public static readonly AssetReference BaduuBrassRing = new AssetReference("Baduu_Outcast_Popup_BrassRing.prefab:9202d8afcf6e80542ae9dafd691df43f");

	// Token: 0x04003562 RID: 13666
	public static readonly AssetReference SklibbBrassRing = new AssetReference("Sklibb_Outcast_Popup_BrassRing.prefab:ec8003f5e3c1c564cb20b106672a8ed4");

	// Token: 0x04003563 RID: 13667
	public static readonly AssetReference SklibbBrassRingDemonHunter = new AssetReference("Sklibb_Demon_Hunter_Popup_BrassRing.prefab:6bf5ceddde5f11347bb7df1c1266fb20");

	// Token: 0x04003564 RID: 13668
	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	// Token: 0x04003565 RID: 13669
	public static readonly AssetReference ArannaBrassRing = new AssetReference("Aranna_Explorer_Popup_Banner.prefab:2d1aaedce4ece664680073bf82f191d6");

	// Token: 0x04003566 RID: 13670
	public static readonly AssetReference ArannaBrassRingInTraining = new AssetReference("Aranna_Training_Popup_BrassRing.prefab:d2b86b1c51e1f734daee22d98b4abdcf");

	// Token: 0x04003567 RID: 13671
	public static readonly AssetReference ArannaBrassRingDemonHunter = new AssetReference("Aranna_Demon_Hunter_Popup_BrassRing.prefab:57c34d7d7bffe1849a85ffbcf95cda3a");

	// Token: 0x04003568 RID: 13672
	public string m_introLine;

	// Token: 0x04003569 RID: 13673
	public string m_deathLine;

	// Token: 0x0400356A RID: 13674
	public string m_standardEmoteResponseLine;

	// Token: 0x0400356B RID: 13675
	public List<string> m_BossIdleLines;

	// Token: 0x0400356C RID: 13676
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x0400356D RID: 13677
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x0400356E RID: 13678
	private int m_PlayBossVOLineIndex;

	// Token: 0x0400356F RID: 13679
	public int TurnOfPlotTwistLastPlayed;
}
