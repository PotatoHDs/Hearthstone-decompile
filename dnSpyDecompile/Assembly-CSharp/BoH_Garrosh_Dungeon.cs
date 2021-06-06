using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000520 RID: 1312
public class BoH_Garrosh_Dungeon : BoH_Garrosh_MissionEntity
{
	// Token: 0x06004731 RID: 18225 RVA: 0x0017EF20 File Offset: 0x0017D120
	public static BoH_Garrosh_Dungeon InstantiateBoH_Garrosh_DungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		int missionId = GameMgr.Get().GetMissionId();
		Log.All.PrintError("BoH_Garrosh_Dungeon.InstantiateBoH_Garrosh_DungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new BoH_Garrosh_Dungeon();
	}

	// Token: 0x06004732 RID: 18226 RVA: 0x0017EF60 File Offset: 0x0017D160
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

	// Token: 0x06004733 RID: 18227 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x0017EFC8 File Offset: 0x0017D1C8
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004736 RID: 18230 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004737 RID: 18231 RVA: 0x0017EFD8 File Offset: 0x0017D1D8
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

	// Token: 0x06004738 RID: 18232 RVA: 0x0017F0B5 File Offset: 0x0017D2B5
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

	// Token: 0x06004739 RID: 18233 RVA: 0x0017F0CB File Offset: 0x0017D2CB
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

	// Token: 0x0600473A RID: 18234 RVA: 0x0017F0E4 File Offset: 0x0017D2E4
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

	// Token: 0x0600473B RID: 18235 RVA: 0x0017F184 File Offset: 0x0017D384
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

	// Token: 0x0600473C RID: 18236 RVA: 0x0017F216 File Offset: 0x0017D416
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
	}

	// Token: 0x0600473D RID: 18237 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x0600473E RID: 18238 RVA: 0x0017F255 File Offset: 0x0017D455
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

	// Token: 0x0600473F RID: 18239 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004740 RID: 18240 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004741 RID: 18241 RVA: 0x0017F26C File Offset: 0x0017D46C
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

	// Token: 0x06004742 RID: 18242 RVA: 0x0017F37F File Offset: 0x0017D57F
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

	// Token: 0x06004743 RID: 18243 RVA: 0x0017F3A3 File Offset: 0x0017D5A3
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

	// Token: 0x06004744 RID: 18244 RVA: 0x0017F3C7 File Offset: 0x0017D5C7
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

	// Token: 0x06004745 RID: 18245 RVA: 0x0017F3EB File Offset: 0x0017D5EB
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004746 RID: 18246 RVA: 0x0017F408 File Offset: 0x0017D608
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004747 RID: 18247 RVA: 0x0017F425 File Offset: 0x0017D625
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004748 RID: 18248 RVA: 0x0017F442 File Offset: 0x0017D642
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x06004749 RID: 18249 RVA: 0x0017F460 File Offset: 0x0017D660
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

	// Token: 0x0600474A RID: 18250 RVA: 0x0017F4F0 File Offset: 0x0017D6F0
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

	// Token: 0x0600474B RID: 18251 RVA: 0x00176BF4 File Offset: 0x00174DF4
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DHMulligan);
		}
	}

	// Token: 0x04003AB2 RID: 15026
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003AB3 RID: 15027
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003AB4 RID: 15028
	public bool m_Heroic;

	// Token: 0x04003AB5 RID: 15029
	public bool m_Galakrond;

	// Token: 0x04003AB6 RID: 15030
	public string m_introLine;

	// Token: 0x04003AB7 RID: 15031
	public string m_deathLine;

	// Token: 0x04003AB8 RID: 15032
	public string m_standardEmoteResponseLine;

	// Token: 0x04003AB9 RID: 15033
	public List<string> m_BossIdleLines;

	// Token: 0x04003ABA RID: 15034
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003ABB RID: 15035
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x04003ABC RID: 15036
	private int m_PlayBossVOLineIndex;

	// Token: 0x04003ABD RID: 15037
	public int TurnOfPlotTwistLastPlayed;
}
