using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005C1 RID: 1473
public class TB_RumbleDome_Dungeon : BTA_Prologue_MissionEntity
{
	// Token: 0x0600511C RID: 20764 RVA: 0x001AB2E0 File Offset: 0x001A94E0
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

	// Token: 0x0600511D RID: 20765 RVA: 0x001AB358 File Offset: 0x001A9558
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

	// Token: 0x0600511E RID: 20766 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x0600511F RID: 20767 RVA: 0x001AB3C0 File Offset: 0x001A95C0
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06005120 RID: 20768 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06005121 RID: 20769 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06005122 RID: 20770 RVA: 0x001AB3D0 File Offset: 0x001A95D0
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

	// Token: 0x06005123 RID: 20771 RVA: 0x001AB4A4 File Offset: 0x001A96A4
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

	// Token: 0x06005124 RID: 20772 RVA: 0x001AB4BA File Offset: 0x001A96BA
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

	// Token: 0x06005125 RID: 20773 RVA: 0x001AB4D0 File Offset: 0x001A96D0
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

	// Token: 0x06005126 RID: 20774 RVA: 0x001AB56F File Offset: 0x001A976F
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x06005127 RID: 20775 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x06005128 RID: 20776 RVA: 0x001AB5AE File Offset: 0x001A97AE
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

	// Token: 0x06005129 RID: 20777 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x0600512A RID: 20778 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x0600512B RID: 20779 RVA: 0x001AB5C4 File Offset: 0x001A97C4
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

	// Token: 0x0600512C RID: 20780 RVA: 0x001AB6CE File Offset: 0x001A98CE
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

	// Token: 0x0600512D RID: 20781 RVA: 0x001AB6F2 File Offset: 0x001A98F2
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

	// Token: 0x0600512E RID: 20782 RVA: 0x001AB716 File Offset: 0x001A9916
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

	// Token: 0x0600512F RID: 20783 RVA: 0x001AB73A File Offset: 0x001A993A
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06005130 RID: 20784 RVA: 0x001AB757 File Offset: 0x001A9957
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06005131 RID: 20785 RVA: 0x001AB774 File Offset: 0x001A9974
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06005132 RID: 20786 RVA: 0x001AB791 File Offset: 0x001A9991
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06005133 RID: 20787 RVA: 0x001AB7B0 File Offset: 0x001A99B0
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

	// Token: 0x06005134 RID: 20788 RVA: 0x001AB840 File Offset: 0x001A9A40
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

	// Token: 0x06005135 RID: 20789 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x06005136 RID: 20790 RVA: 0x00177A80 File Offset: 0x00175C80
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHPrologue);
	}

	// Token: 0x0400487D RID: 18557
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x0400487E RID: 18558
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x0400487F RID: 18559
	public bool m_Heroic;

	// Token: 0x04004880 RID: 18560
	public bool m_Galakrond;

	// Token: 0x04004881 RID: 18561
	public string m_introLine;

	// Token: 0x04004882 RID: 18562
	public string m_deathLine;

	// Token: 0x04004883 RID: 18563
	public string m_standardEmoteResponseLine;

	// Token: 0x04004884 RID: 18564
	public List<string> m_BossIdleLines;

	// Token: 0x04004885 RID: 18565
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04004886 RID: 18566
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04004887 RID: 18567
	private int m_PlayBossVOLineIndex;

	// Token: 0x04004888 RID: 18568
	public int TurnOfPlotTwistLastPlayed;
}
