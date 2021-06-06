using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000508 RID: 1288
public class BTA_Prologue_Dungeon : BTA_Prologue_MissionEntity
{
	// Token: 0x06004563 RID: 17763 RVA: 0x00177404 File Offset: 0x00175604
	public static BTA_Prologue_Dungeon InstantiateBTA_PrologueDungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId == 3624)
		{
			return new BTA_Prologue_Fight_01();
		}
		switch (missionId)
		{
		case 3648:
			return new BTA_Prologue_Fight_02();
		case 3649:
			return new BTA_Prologue_Fight_03();
		case 3650:
			return new BTA_Prologue_Fight_04();
		default:
			Log.All.PrintError("BTA_Dungeon.InstantiateBTADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
			{
				opposingHeroCardID
			});
			return new BTA_Prologue_Dungeon();
		}
	}

	// Token: 0x06004564 RID: 17764 RVA: 0x0017747C File Offset: 0x0017567C
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

	// Token: 0x06004565 RID: 17765 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004566 RID: 17766 RVA: 0x001774E4 File Offset: 0x001756E4
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004567 RID: 17767 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004568 RID: 17768 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004569 RID: 17769 RVA: 0x001774F4 File Offset: 0x001756F4
	protected virtual void OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
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

	// Token: 0x0600456A RID: 17770 RVA: 0x001775C8 File Offset: 0x001757C8
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
			this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x0600456B RID: 17771 RVA: 0x001775DE File Offset: 0x001757DE
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

	// Token: 0x0600456C RID: 17772 RVA: 0x001775F4 File Offset: 0x001757F4
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

	// Token: 0x0600456D RID: 17773 RVA: 0x00177694 File Offset: 0x00175894
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

	// Token: 0x0600456E RID: 17774 RVA: 0x0017771D File Offset: 0x0017591D
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x0600456F RID: 17775 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06004570 RID: 17776 RVA: 0x0017775C File Offset: 0x0017595C
	protected override IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		Actor actor = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
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
			break;
		case 1001:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex]);
			yield return base.PlayBossLine(actor, this.m_PlayerVOLines[this.m_PlayPlayerVOLineIndex], 2.5f);
			break;
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
			break;
		case 1003:
			GameState.Get().GetGameEntity().SetTag(GAME_TAG.MISSION_EVENT, 0);
			SceneDebugger.Get().AddMessage(this.m_BossVOLines[this.m_PlayBossVOLineIndex]);
			yield return base.PlayBossLine(actor2, this.m_BossVOLines[this.m_PlayBossVOLineIndex], 2.5f);
			break;
		default:
			if (missionEvent != 1010)
			{
				if (missionEvent != 58023)
				{
					yield return base.HandleMissionEventWithTiming(missionEvent);
				}
				else
				{
					SceneMgr.Mode postGameSceneMode = GameMgr.Get().GetPostGameSceneMode();
					GameMgr.Get().PreparePostGameSceneMode(postGameSceneMode);
					SceneMgr.Get().SetNextMode(postGameSceneMode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
				}
			}
			else if (this.m_forceAlwaysPlayLine)
			{
				this.m_forceAlwaysPlayLine = false;
			}
			else
			{
				this.m_forceAlwaysPlayLine = true;
			}
			break;
		}
		yield break;
	}

	// Token: 0x06004571 RID: 17777 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004572 RID: 17778 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004573 RID: 17779 RVA: 0x00177774 File Offset: 0x00175974
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

	// Token: 0x06004574 RID: 17780 RVA: 0x0017787E File Offset: 0x00175A7E
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

	// Token: 0x06004575 RID: 17781 RVA: 0x001778A2 File Offset: 0x00175AA2
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

	// Token: 0x06004576 RID: 17782 RVA: 0x001778C6 File Offset: 0x00175AC6
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

	// Token: 0x06004577 RID: 17783 RVA: 0x001778EA File Offset: 0x00175AEA
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004578 RID: 17784 RVA: 0x00177907 File Offset: 0x00175B07
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004579 RID: 17785 RVA: 0x00177924 File Offset: 0x00175B24
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600457A RID: 17786 RVA: 0x00177941 File Offset: 0x00175B41
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x0600457B RID: 17787 RVA: 0x00177960 File Offset: 0x00175B60
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

	// Token: 0x0600457C RID: 17788 RVA: 0x001779F0 File Offset: 0x00175BF0
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

	// Token: 0x0600457D RID: 17789 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x0600457E RID: 17790 RVA: 0x00177A80 File Offset: 0x00175C80
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologue);
	}

	// Token: 0x04003835 RID: 14389
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003836 RID: 14390
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003837 RID: 14391
	public bool m_Heroic;

	// Token: 0x04003838 RID: 14392
	public bool m_Galakrond;

	// Token: 0x04003839 RID: 14393
	public string m_introLine;

	// Token: 0x0400383A RID: 14394
	public string m_deathLine;

	// Token: 0x0400383B RID: 14395
	public string m_standardEmoteResponseLine;

	// Token: 0x0400383C RID: 14396
	public List<string> m_BossIdleLines;

	// Token: 0x0400383D RID: 14397
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x0400383E RID: 14398
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x0400383F RID: 14399
	private int m_PlayBossVOLineIndex;

	// Token: 0x04003840 RID: 14400
	public int TurnOfPlotTwistLastPlayed;
}
