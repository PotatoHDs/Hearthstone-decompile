using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004CE RID: 1230
public class DRGA_Dungeon : DRGA_MissionEntity
{
	// Token: 0x060041D2 RID: 16850 RVA: 0x001608AC File Offset: 0x0015EAAC
	public static DRGA_Dungeon InstantiateDRGADungeonMissionEntityForBoss(List<Network.PowerHistory> powerList, Network.HistCreateGame createGame)
	{
		string opposingHeroCardID = GenericDungeonMissionEntity.GetOpposingHeroCardID(powerList, createGame);
		int missionId = GameMgr.Get().GetMissionId();
		if (missionId <= 3500)
		{
			if (missionId == 0)
			{
				return new DRGA_Evil_Fight_01();
			}
			switch (missionId)
			{
			case 3469:
				goto IL_17B;
			case 3470:
				goto IL_181;
			case 3471:
				goto IL_187;
			case 3472:
				goto IL_18D;
			case 3473:
				goto IL_193;
			case 3474:
			case 3476:
			case 3482:
			case 3485:
			case 3486:
			case 3487:
			case 3492:
			case 3496:
				goto IL_1C3;
			case 3475:
				goto IL_199;
			case 3477:
				goto IL_19F;
			case 3478:
				goto IL_1A5;
			case 3479:
				goto IL_1AB;
			case 3480:
				goto IL_1B1;
			case 3481:
				goto IL_1B7;
			case 3483:
				goto IL_1BD;
			case 3484:
				break;
			case 3488:
				goto IL_139;
			case 3489:
				goto IL_13F;
			case 3490:
				goto IL_145;
			case 3491:
				goto IL_14B;
			case 3493:
				goto IL_151;
			case 3494:
				goto IL_157;
			case 3495:
				goto IL_15D;
			case 3497:
				goto IL_163;
			case 3498:
				goto IL_169;
			case 3499:
				goto IL_16F;
			case 3500:
				goto IL_175;
			default:
				goto IL_1C3;
			}
		}
		else
		{
			if (missionId == 3556)
			{
				goto IL_17B;
			}
			switch (missionId)
			{
			case 3583:
				goto IL_181;
			case 3584:
				goto IL_187;
			case 3585:
				goto IL_18D;
			case 3586:
				goto IL_193;
			case 3587:
				goto IL_199;
			case 3588:
				goto IL_19F;
			case 3589:
				goto IL_1A5;
			case 3590:
				goto IL_1AB;
			case 3591:
				goto IL_1B1;
			case 3592:
				goto IL_1B7;
			case 3593:
				goto IL_1BD;
			case 3594:
				break;
			case 3595:
				goto IL_139;
			case 3596:
				goto IL_13F;
			case 3597:
				goto IL_145;
			case 3598:
				goto IL_14B;
			case 3599:
				goto IL_151;
			case 3600:
				goto IL_157;
			case 3601:
				goto IL_15D;
			case 3602:
				goto IL_163;
			case 3603:
				goto IL_169;
			case 3604:
				goto IL_16F;
			case 3605:
				goto IL_175;
			default:
				goto IL_1C3;
			}
		}
		return new DRGA_Evil_Fight_01();
		IL_139:
		return new DRGA_Evil_Fight_02();
		IL_13F:
		return new DRGA_Evil_Fight_03();
		IL_145:
		return new DRGA_Evil_Fight_04();
		IL_14B:
		return new DRGA_Evil_Fight_05();
		IL_151:
		return new DRGA_Evil_Fight_06();
		IL_157:
		return new DRGA_Evil_Fight_07();
		IL_15D:
		return new DRGA_Evil_Fight_08();
		IL_163:
		return new DRGA_Evil_Fight_09();
		IL_169:
		return new DRGA_Evil_Fight_10();
		IL_16F:
		return new DRGA_Evil_Fight_11();
		IL_175:
		return new DRGA_Evil_Fight_12();
		IL_17B:
		return new DRGA_Good_Fight_01();
		IL_181:
		return new DRGA_Good_Fight_02();
		IL_187:
		return new DRGA_Good_Fight_03();
		IL_18D:
		return new DRGA_Good_Fight_04();
		IL_193:
		return new DRGA_Good_Fight_05();
		IL_199:
		return new DRGA_Good_Fight_06();
		IL_19F:
		return new DRGA_Good_Fight_07();
		IL_1A5:
		return new DRGA_Good_Fight_08();
		IL_1AB:
		return new DRGA_Good_Fight_09();
		IL_1B1:
		return new DRGA_Good_Fight_10();
		IL_1B7:
		return new DRGA_Good_Fight_11();
		IL_1BD:
		return new DRGA_Good_Fight_12();
		IL_1C3:
		Log.All.PrintError("DRGA_Dungeon.InstantiateDRGADungeonMissionEntityForBoss() - Found unsupported enemy Boss {0}.", new object[]
		{
			opposingHeroCardID
		});
		return new DRGA_Dungeon();
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x00160A9C File Offset: 0x0015EC9C
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

	// Token: 0x060041D4 RID: 16852 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x060041D5 RID: 16853 RVA: 0x00160B04 File Offset: 0x0015ED04
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x060041D6 RID: 16854 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060041D7 RID: 16855 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x060041D8 RID: 16856 RVA: 0x00160B14 File Offset: 0x0015ED14
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

	// Token: 0x060041D9 RID: 16857 RVA: 0x00160BE8 File Offset: 0x0015EDE8
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

	// Token: 0x060041DA RID: 16858 RVA: 0x00160BFE File Offset: 0x0015EDFE
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

	// Token: 0x060041DB RID: 16859 RVA: 0x00160C14 File Offset: 0x0015EE14
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

	// Token: 0x060041DC RID: 16860 RVA: 0x00160CB4 File Offset: 0x0015EEB4
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

	// Token: 0x060041DD RID: 16861 RVA: 0x00160D40 File Offset: 0x0015EF40
	public override void OnCreateGame()
	{
		base.OnCreateGame();
		this.m_introLine = null;
		this.m_deathLine = null;
		this.m_standardEmoteResponseLine = null;
		this.m_BossIdleLines = new List<string>(this.GetIdleLines());
		this.m_BossIdleLinesCopy = new List<string>(this.GetIdleLines());
		this.m_Heroic = this.GetIsHeroic();
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x00160D98 File Offset: 0x0015EF98
	protected virtual bool GetIsHeroic()
	{
		int missionId = GameMgr.Get().GetMissionId();
		return missionId == 3556 || missionId - 3583 <= 22;
	}

	// Token: 0x060041DF RID: 16863 RVA: 0x0001FA65 File Offset: 0x0001DC65
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return false;
	}

	// Token: 0x060041E0 RID: 16864 RVA: 0x00160DC6 File Offset: 0x0015EFC6
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

	// Token: 0x060041E1 RID: 16865 RVA: 0x00160DDC File Offset: 0x0015EFDC
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x060041E2 RID: 16866 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossThinkChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x060041E3 RID: 16867 RVA: 0x00160DEC File Offset: 0x0015EFEC
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

	// Token: 0x060041E4 RID: 16868 RVA: 0x00160EF6 File Offset: 0x0015F0F6
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

	// Token: 0x060041E5 RID: 16869 RVA: 0x00160F1A File Offset: 0x0015F11A
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

	// Token: 0x060041E6 RID: 16870 RVA: 0x00160F3E File Offset: 0x0015F13E
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

	// Token: 0x060041E7 RID: 16871 RVA: 0x00160F62 File Offset: 0x0015F162
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x00160F7F File Offset: 0x0015F17F
	public IEnumerator PlayAndRemoveRandomLineOnlyOnce(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayLineOnlyOnce(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x00160F9C File Offset: 0x0015F19C
	public IEnumerator PlayRandomLineAlways(Actor actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060041EA RID: 16874 RVA: 0x00160FB9 File Offset: 0x0015F1B9
	public IEnumerator PlayRandomLineAlways(string actor, List<string> lines)
	{
		string text = base.PopRandomLine(lines);
		if (text != null)
		{
			yield return base.PlayBossLine(actor, text, 2.5f);
		}
		yield break;
	}

	// Token: 0x060041EB RID: 16875 RVA: 0x00160FD8 File Offset: 0x0015F1D8
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

	// Token: 0x060041EC RID: 16876 RVA: 0x00161068 File Offset: 0x0015F268
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

	// Token: 0x060041ED RID: 16877 RVA: 0x001610F8 File Offset: 0x0015F2F8
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRGMulligan);
		}
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x0016110D File Offset: 0x0015F30D
	public override void StartGameplaySoundtracks()
	{
		MusicManager.Get().StartPlaylist(MusicPlaylistType.InGame_DRG);
	}

	// Token: 0x0400311B RID: 12571
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x0400311C RID: 12572
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x0400311D RID: 12573
	public bool m_Heroic;

	// Token: 0x0400311E RID: 12574
	public bool m_Galakrond;

	// Token: 0x0400311F RID: 12575
	public static readonly AssetReference BrannBrassRing = new AssetReference("BrannBronzebeard_BrassRing_Quote.prefab:d1f8af47f0917e94289b63f3a42e52f7");

	// Token: 0x04003120 RID: 12576
	public static readonly AssetReference EliseBrassRing = new AssetReference("EliseStarseeker_BrassRing_Quote.prefab:7176acaa6d28fa447adbafde663037d3");

	// Token: 0x04003121 RID: 12577
	public static readonly AssetReference FinleyBrassRing = new AssetReference("SirFinley_BrassRing_Quote.prefab:5f94953d717142446b348e4d2f3a4ca8");

	// Token: 0x04003122 RID: 12578
	public static readonly AssetReference RenoBrassRing = new AssetReference("RenoJackson_BrassRing_Quote.prefab:74a27d2f94ef83744a0a8357dbac2e43");

	// Token: 0x04003123 RID: 12579
	public static readonly AssetReference RafaamBrassRing = new AssetReference("Rafaam_BrassRing_Quote.prefab:2d6ab3cc1d153ed4886ff98e47d129c6");

	// Token: 0x04003124 RID: 12580
	public string m_introLine;

	// Token: 0x04003125 RID: 12581
	public string m_deathLine;

	// Token: 0x04003126 RID: 12582
	public string m_standardEmoteResponseLine;

	// Token: 0x04003127 RID: 12583
	public List<string> m_BossIdleLines;

	// Token: 0x04003128 RID: 12584
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003129 RID: 12585
	private int m_PlayPlayerVOLineIndex;

	// Token: 0x0400312A RID: 12586
	private int m_PlayBossVOLineIndex;

	// Token: 0x0400312B RID: 12587
	public int TurnOfPlotTwistLastPlayed;
}
