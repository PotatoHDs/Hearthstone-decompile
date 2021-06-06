using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000552 RID: 1362
public class BoH_Uther_Dungeon : BoH_Uther_MissionEntity
{
	// Token: 0x06004B23 RID: 19235 RVA: 0x0018E920 File Offset: 0x0018CB20
	public static BoH_Uther_Dungeon InstantiateBoH_Uther_DungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		int missionId = GameMgr.Get().GetMissionId();
		Log.All.PrintError("BoH_Uther_Dungeon.InstantiateBoH_Uther_DungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BoH_Uther_Dungeon();
	}

	// Token: 0x06004B24 RID: 19236 RVA: 0x0018E960 File Offset: 0x0018CB60
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

	// Token: 0x06004B25 RID: 19237 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004B26 RID: 19238 RVA: 0x0018E9C8 File Offset: 0x0018CBC8
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004B27 RID: 19239 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004B28 RID: 19240 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004B29 RID: 19241 RVA: 0x0018E9D8 File Offset: 0x0018CBD8
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			return;
		}
		if (this.m_MissionDisableAutomaticVO)
		{
			return;
		}
		if (num < num2)
		{
			return;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
		{
			return;
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
			return;
		}
		Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeechOnce(text, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
	}

	// Token: 0x06004B2A RID: 19242 RVA: 0x0018EAB5 File Offset: 0x0018CCB5
	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (this.m_MissionDisableAutomaticVO)
		{
			yield break;
		}
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
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x06004B2B RID: 19243 RVA: 0x0018EACB File Offset: 0x0018CCCB
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		while (entity.GetCardType() == TAG_CARDTYPE.INVALID)
		{
			yield return null;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		yield return base.WaitForEntitySoundToFinish(entity);
		yield break;
	}

	// Token: 0x06004B2C RID: 19244 RVA: 0x0018EAE4 File Offset: 0x0018CCE4
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

	// Token: 0x06004B2D RID: 19245 RVA: 0x0018EB84 File Offset: 0x0018CD84
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (emoteType == EmoteType.START)
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_introLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
		if (this.m_MissionDisableAutomaticVO)
		{
			return;
		}
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			Gameplay.Get().StartCoroutine(base.PlaySoundAndBlockSpeech(this.m_standardEmoteResponseLine, Notification.SpeechBubbleDirection.TopRight, actor, 3f, 1f, true, false, 0f));
			return;
		}
	}

	// Token: 0x06004B2E RID: 19246 RVA: 0x0018EC16 File Offset: 0x0018CE16
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06004B2F RID: 19247 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06004B30 RID: 19248 RVA: 0x0018EC55 File Offset: 0x0018CE55
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (missionEvent <= 1010)
		{
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
				goto IL_381;
			case 1001:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
				yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
				goto IL_381;
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
				yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				goto IL_381;
			case 1003:
				GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
				SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
				yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
				goto IL_381;
			default:
				if (missionEvent == 1010)
				{
					if (this.m_forceAlwaysPlayLine)
					{
						this.m_forceAlwaysPlayLine = false;
						goto IL_381;
					}
					this.m_forceAlwaysPlayLine = true;
					goto IL_381;
				}
				break;
			}
		}
		else
		{
			if (missionEvent == 1100)
			{
				GameState.Get().SetBusy(true);
				this.m_MissionDisableAutomaticVO = true;
				GameState.Get().SetBusy(false);
				goto IL_381;
			}
			if (missionEvent == 1101)
			{
				GameState.Get().SetBusy(true);
				this.m_MissionDisableAutomaticVO = false;
				GameState.Get().SetBusy(false);
				goto IL_381;
			}
			if (missionEvent == 58023)
			{
				SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
				GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
				SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				goto IL_381;
			}
		}
		yield return base.HandleMissionEventWithTiming(missionEvent);
		IL_381:
		yield break;
	}

	// Token: 0x06004B31 RID: 19249 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004B32 RID: 19250 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004B33 RID: 19251 RVA: 0x0018EC6C File Offset: 0x0018CE6C
	public override void OnPlayThinkEmote()
	{
		if (this.m_enemySpeaking)
		{
			return;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			return;
		}
		if (this.m_MissionDisableAutomaticVO)
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

	// Token: 0x06004B34 RID: 19252 RVA: 0x0018ED7F File Offset: 0x0018CF7F
	public IEnumerator PlayAndRemoveRandomLineOnlyOnceWithBrassRing(Actor actor, AssetReference brassRingBackup, List<string> lines)
	{
		if (actor != null)
		{
			yield return this.PlayAndRemoveRandomLineOnlyOnce(actor, lines);
		}
		else if (brassRingBackup != null)
		{
			yield return this.PlayAndRemoveRandomLineOnlyOnce(brassRingBackup, lines);
		}
		yield break;
	}

	// Token: 0x06004B35 RID: 19253 RVA: 0x0018EDA3 File Offset: 0x0018CFA3
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

	// Token: 0x06004B36 RID: 19254 RVA: 0x0018EDC7 File Offset: 0x0018CFC7
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

	// Token: 0x06004B37 RID: 19255 RVA: 0x0018EDEB File Offset: 0x0018CFEB
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B38 RID: 19256 RVA: 0x0018EE08 File Offset: 0x0018D008
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B39 RID: 19257 RVA: 0x0018EE25 File Offset: 0x0018D025
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B3A RID: 19258 RVA: 0x0018EE42 File Offset: 0x0018D042
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004B3B RID: 19259 RVA: 0x0018EE60 File Offset: 0x0018D060
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

	// Token: 0x06004B3C RID: 19260 RVA: 0x0018EEF0 File Offset: 0x0018D0F0
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

	// Token: 0x06004B3D RID: 19261 RVA: 0x0017F615 File Offset: 0x0017D815
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_Mulligan);
		}
	}

	// Token: 0x06004B3E RID: 19262 RVA: 0x00177A80 File Offset: 0x00175C80
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologue);
	}

	// Token: 0x04003F98 RID: 16280
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003F99 RID: 16281
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003F9A RID: 16282
	public bool m_Heroic;

	// Token: 0x04003F9B RID: 16283
	public bool m_Galakrond;

	// Token: 0x04003F9C RID: 16284
	public string m_introLine;

	// Token: 0x04003F9D RID: 16285
	public string m_deathLine;

	// Token: 0x04003F9E RID: 16286
	public string m_standardEmoteResponseLine;

	// Token: 0x04003F9F RID: 16287
	public List<string> m_BossIdleLines;

	// Token: 0x04003FA0 RID: 16288
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003FA1 RID: 16289
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04003FA2 RID: 16290
	private int m_PlayBossVOLineIndex;

	// Token: 0x04003FA3 RID: 16291
	public int TurnOfPlotTwistLastPlayed;
}
