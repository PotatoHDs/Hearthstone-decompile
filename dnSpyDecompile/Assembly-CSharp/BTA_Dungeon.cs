using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004E8 RID: 1256
public class BTA_Dungeon : BTA_MissionEntity
{
	// Token: 0x06004347 RID: 17223 RVA: 0x0016CA1C File Offset: 0x0016AC1C
	public static BTA_Dungeon InstantiateBTADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		uint num = <PrivateImplementationDetails>.ComputeStringHash(opposingHeroCardID);
		if (num <= 2043748781U)
		{
			if (num <= 1808023472U)
			{
				if (num <= 1372055641U)
				{
					if (num != 1339235878U)
					{
						if (num == 1372055641U)
						{
							if (opposingHeroCardID == "BTA_BOSS_13h")
							{
								return new BTA_Fight_13();
							}
						}
					}
					else if (opposingHeroCardID == "BTA_BOSS_14h")
					{
						return new BTA_Fight_14();
					}
				}
				else if (num != 1406052164U)
				{
					if (num == 1808023472U)
					{
						if (opposingHeroCardID == "BTA_BOSS_12h")
						{
							return new BTA_Fight_12();
						}
					}
				}
				else if (opposingHeroCardID == "BTA_BOSS_16h")
				{
					return new BTA_Fight_16();
				}
			}
			else if (num <= 1976932495U)
			{
				if (num != 1842123163U)
				{
					if (num == 1976932495U)
					{
						if (opposingHeroCardID == "BTA_BOSS_15h")
						{
							return new BTA_Fight_15();
						}
					}
				}
				else if (opposingHeroCardID == "BTA_BOSS_11h")
				{
					return new BTA_Fight_11();
				}
			}
			else if (num != 2009752258U)
			{
				if (num == 2043748781U)
				{
					if (opposingHeroCardID == "BTA_BOSS_17h")
					{
						return new BTA_Fight_17();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_10h")
			{
				return new BTA_Fight_10();
			}
		}
		else if (num <= 3294178162U)
		{
			if (num <= 3126652235U)
			{
				if (num != 3092552544U)
				{
					if (num == 3126652235U)
					{
						if (opposingHeroCardID == "BTA_BOSS_06h")
						{
							return new BTA_Fight_06();
						}
					}
				}
				else if (opposingHeroCardID == "BTA_BOSS_05h")
				{
					return new BTA_Fight_05();
				}
			}
			else if (num != 3261461567U)
			{
				if (num == 3294178162U)
				{
					if (opposingHeroCardID == "BTA_BOSS_07h")
					{
						return new BTA_Fight_07();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_02h")
			{
				return new BTA_Fight_02();
			}
		}
		else if (num <= 3697532566U)
		{
			if (num != 3326997925U)
			{
				if (num == 3697532566U)
				{
					if (opposingHeroCardID == "BTA_BOSS_03h")
					{
						return new BTA_Fight_03();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_08h")
			{
				return new BTA_Fight_08();
			}
		}
		else if (num != 3730249161U)
		{
			if (num != 3762965756U)
			{
				if (num == 3764348852U)
				{
					if (opposingHeroCardID == "BTA_BOSS_01h")
					{
						return new BTA_Fight_01();
					}
				}
			}
			else if (opposingHeroCardID == "BTA_BOSS_09h")
			{
				return new BTA_Fight_09();
			}
		}
		else if (opposingHeroCardID == "BTA_BOSS_04h")
		{
			return new BTA_Fight_04();
		}
		Log.All.PrintError("BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BTA_Dungeon();
	}

	// Token: 0x06004348 RID: 17224 RVA: 0x0016CD24 File Offset: 0x0016AF24
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

	// Token: 0x06004349 RID: 17225 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x0600434A RID: 17226 RVA: 0x0016CD8C File Offset: 0x0016AF8C
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x0600434B RID: 17227 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600434C RID: 17228 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x0600434D RID: 17229 RVA: 0x0016CD9A File Offset: 0x0016AF9A
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

	// Token: 0x0600434E RID: 17230 RVA: 0x0016CDAA File Offset: 0x0016AFAA
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

	// Token: 0x0600434F RID: 17231 RVA: 0x0016CDC0 File Offset: 0x0016AFC0
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

	// Token: 0x06004350 RID: 17232 RVA: 0x0016CDD8 File Offset: 0x0016AFD8
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

	// Token: 0x06004351 RID: 17233 RVA: 0x0016CE78 File Offset: 0x0016B078
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

	// Token: 0x06004352 RID: 17234 RVA: 0x0016CF01 File Offset: 0x0016B101
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06004353 RID: 17235 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06004354 RID: 17236 RVA: 0x0016CF40 File Offset: 0x0016B140
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

	// Token: 0x06004355 RID: 17237 RVA: 0x0016CF56 File Offset: 0x0016B156
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		GameMgr.Get().GetMissionId();
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004356 RID: 17238 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004357 RID: 17239 RVA: 0x0016CF70 File Offset: 0x0016B170
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

	// Token: 0x06004358 RID: 17240 RVA: 0x0016D000 File Offset: 0x0016B200
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

	// Token: 0x06004359 RID: 17241 RVA: 0x0016D090 File Offset: 0x0016B290
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600435A RID: 17242 RVA: 0x0016D0AD File Offset: 0x0016B2AD
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600435B RID: 17243 RVA: 0x0016D0CA File Offset: 0x0016B2CA
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

	// Token: 0x0600435C RID: 17244 RVA: 0x0016D0EE File Offset: 0x0016B2EE
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

	// Token: 0x0600435D RID: 17245 RVA: 0x0016D112 File Offset: 0x0016B312
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

	// Token: 0x0600435E RID: 17246 RVA: 0x0016D138 File Offset: 0x0016B338
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

	// Token: 0x04003524 RID: 13604
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003525 RID: 13605
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003526 RID: 13606
	public bool m_DisableIdle;

	// Token: 0x04003527 RID: 13607
	private static readonly AssetReference Karnuk_Demon_Hunter_Popup_BrassRing = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	// Token: 0x04003528 RID: 13608
	public static readonly AssetReference KarnukBrassRing = new AssetReference("Karnuk_Outcast_Popup_BrassRing.prefab:d097e6294875881488492604e9320e64");

	// Token: 0x04003529 RID: 13609
	public static readonly AssetReference KarnukBrassRingDemonHunter = new AssetReference("Karnuk_Demon_Hunter_Popup_BrassRing.prefab:af78f17e1126eef41b6700cad3d1bccb");

	// Token: 0x0400352A RID: 13610
	public static readonly AssetReference ShaljaBrassRing = new AssetReference("Shalja_Outcast_Popup_BrassRing.prefab:0425972e057e448458abedcc24797c3a");

	// Token: 0x0400352B RID: 13611
	public static readonly AssetReference ShaljaBrassRingDemonHunter = new AssetReference("Shalja_Demon_Hunter_Popup_BrassRing.prefab:08f4bb41a6104a94ca96bb8003fa826f");

	// Token: 0x0400352C RID: 13612
	public static readonly AssetReference BaduuBrassRing = new AssetReference("Baduu_Outcast_Popup_BrassRing.prefab:9202d8afcf6e80542ae9dafd691df43f");

	// Token: 0x0400352D RID: 13613
	public static readonly AssetReference SklibbBrassRing = new AssetReference("Sklibb_Outcast_Popup_BrassRing.prefab:ec8003f5e3c1c564cb20b106672a8ed4");

	// Token: 0x0400352E RID: 13614
	public static readonly AssetReference SklibbBrassRingDemonHunter = new AssetReference("Sklibb_Demon_Hunter_Popup_BrassRing.prefab:6bf5ceddde5f11347bb7df1c1266fb20");

	// Token: 0x0400352F RID: 13615
	public static readonly AssetReference IllidanBrassRing = new AssetReference("DemonHunter_Illidan_Popup_BrassRing.prefab:8c007b8e8be417c4fbd9738960e6f7f0");

	// Token: 0x04003530 RID: 13616
	public static readonly AssetReference ArannaBrassRing = new AssetReference("Aranna_Explorer_Popup_Banner.prefab:2d1aaedce4ece664680073bf82f191d6");

	// Token: 0x04003531 RID: 13617
	public static readonly AssetReference ArannaBrassRingInTraining = new AssetReference("Aranna_Training_Popup_BrassRing.prefab:d2b86b1c51e1f734daee22d98b4abdcf");

	// Token: 0x04003532 RID: 13618
	public static readonly AssetReference ArannaBrassRingDemonHunter = new AssetReference("Aranna_Demon_Hunter_Popup_BrassRing.prefab:57c34d7d7bffe1849a85ffbcf95cda3a");

	// Token: 0x04003533 RID: 13619
	public string m_introLine;

	// Token: 0x04003534 RID: 13620
	public string m_deathLine;

	// Token: 0x04003535 RID: 13621
	public string m_standardEmoteResponseLine;

	// Token: 0x04003536 RID: 13622
	public List<string> m_BossIdleLines;

	// Token: 0x04003537 RID: 13623
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003538 RID: 13624
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04003539 RID: 13625
	private int m_PlayBossVOLineIndex;

	// Token: 0x0400353A RID: 13626
	public int TurnOfPlotTwistLastPlayed;
}
