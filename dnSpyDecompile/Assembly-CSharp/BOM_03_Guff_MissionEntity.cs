using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x0200057B RID: 1403
public abstract class BOM_03_Guff_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004DFC RID: 19964 RVA: 0x0019CA90 File Offset: 0x0019AC90
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

	// Token: 0x06004DFD RID: 19965 RVA: 0x0016DED1 File Offset: 0x0016C0D1
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.DO_OPENING_TAUNTS,
				false
			}
		};
	}

	// Token: 0x06004DFE RID: 19966 RVA: 0x0019CAF8 File Offset: 0x0019ACF8
	public BOM_03_Guff_MissionEntity()
	{
		this.m_gameOptions.AddBooleanOptions(BOM_03_Guff_MissionEntity.s_booleanOptions);
	}

	// Token: 0x06004DFF RID: 19967 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004E00 RID: 19968 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004E01 RID: 19969 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004E02 RID: 19970 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004E03 RID: 19971 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004E04 RID: 19972 RVA: 0x0019CB78 File Offset: 0x0019AD78
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x06004E05 RID: 19973 RVA: 0x0019CB8C File Offset: 0x0019AD8C
	protected virtual IEnumerator OnBossHeroPowerPlayed(Entity entity)
	{
		float num = this.ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		if (this.m_MissionDisableAutomaticVO)
		{
			yield break;
		}
		if (num < num2)
		{
			yield break;
		}
		if (GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor() == null)
		{
			yield break;
		}
		yield return this.HandleMissionEventWithTiming(510);
		yield break;
	}

	// Token: 0x06004E06 RID: 19974 RVA: 0x0019CB9B File Offset: 0x0019AD9B
	protected override IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
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
		if (entity.GetControllerSide() == Player.Side.FRIENDLY)
		{
			yield return this.HandleMissionEventWithTiming(508);
		}
		yield break;
	}

	// Token: 0x06004E07 RID: 19975 RVA: 0x0019CBB1 File Offset: 0x0019ADB1
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
			yield return this.OnBossHeroPowerPlayed(entity);
		}
		yield break;
	}

	// Token: 0x06004E08 RID: 19976 RVA: 0x0017C055 File Offset: 0x0017A255
	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (!MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType))
		{
			return;
		}
		if (this.m_enemySpeaking)
		{
			return;
		}
		GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(515));
	}

	// Token: 0x06004E09 RID: 19977 RVA: 0x0019CBC7 File Offset: 0x0019ADC7
	protected IEnumerator DelayAndPlayInGameTrigger(int VOTriggerID)
	{
		float seconds = 3f;
		yield return new WaitForSeconds(seconds);
		yield return this.HandleMissionEventWithTiming(VOTriggerID);
		yield break;
	}

	// Token: 0x06004E0A RID: 19978 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004E0B RID: 19979 RVA: 0x0019CBDD File Offset: 0x0019ADDD
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004E0C RID: 19980 RVA: 0x0019CBFF File Offset: 0x0019ADFF
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			if (this.m_OverrideMulliganMusicTrack == MusicPlaylistType.Invalid)
			{
				base.StartMulliganSoundtracks(soft);
				return;
			}
			MusicManager.Get().StartPlaylist(this.m_OverrideMulliganMusicTrack);
		}
	}

	// Token: 0x06004E0D RID: 19981 RVA: 0x0019CC25 File Offset: 0x0019AE25
	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		if (playerSide == Player.Side.OPPOSING && this.m_OverrideBossSubtext != null)
		{
			return GameStrings.Get(this.m_OverrideBossSubtext);
		}
		if (playerSide == Player.Side.FRIENDLY && this.m_OverridePlayerSubtext != null)
		{
			return GameStrings.Get(this.m_OverridePlayerSubtext);
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	// Token: 0x06004E0E RID: 19982 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004E0F RID: 19983 RVA: 0x0019CC5E File Offset: 0x0019AE5E
	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide())
		{
			yield break;
		}
		if (currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero().GetCardId();
		if (!this.m_Mission_FriendlyPlayIdleLines && !this.m_Mission_EnemyPlayIdleLines)
		{
			yield break;
		}
		float thinkIdleChancePercentage = this.GetThinkIdleChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkIdleChancePercentage < num)
		{
			yield break;
		}
		float thinkEmoteBossIdleChancePercentage = this.GetThinkEmoteBossIdleChancePercentage();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if ((thinkEmoteBossIdleChancePercentage < num2 || !this.m_Mission_FriendlyPlayIdleLines) && this.m_Mission_EnemyPlayIdleLines)
		{
			if (this.m_Mission_EnemyPlayIdleLinesUseingEmoteSystem)
			{
				yield return this.MissionPlayThinkEmote(actor);
			}
			else
			{
				yield return GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(517));
			}
		}
		else
		{
			if (!this.m_Mission_FriendlyPlayIdleLines)
			{
				yield break;
			}
			if (this.m_Mission_FriendlyPlayIdleLinesUseingEmoteSystem)
			{
				yield return this.MissionPlayThinkEmote(actor2);
			}
			else
			{
				yield return GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(518));
			}
		}
		yield break;
	}

	// Token: 0x06004E10 RID: 19984 RVA: 0x0019CC70 File Offset: 0x0019AE70
	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Gameplay.Get().SaveOriginalTimeScale();
		AchievementManager achievementManager = AchievementManager.Get();
		if (achievementManager != null)
		{
			achievementManager.PauseToastNotifications();
		}
		if (this.ShouldPlayHeroBlowUpSpells(playState))
		{
			switch (playState)
			{
			case TAG_PLAYSTATE.WON:
			{
				string stringOption = base.GetGameOptions().GetStringOption(GameEntityOption.VICTORY_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (this.m_Mission_EnemyHeroShouldExplodeOnDefeat)
				{
					this.m_enemyBlowUpSpell = this.BlowUpHero(heroCard, SpellType.ENDGAME_WIN);
				}
				break;
			}
			case TAG_PLAYSTATE.LOST:
			{
				string stringOption = base.GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (this.m_Mission_FriendlyHeroShouldExplodeOnDefeat)
				{
					this.m_friendlyBlowUpSpell = this.BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				}
				break;
			}
			case TAG_PLAYSTATE.TIED:
			{
				string stringOption = base.GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (this.m_Mission_EnemyHeroShouldExplodeOnDefeat)
				{
					this.m_enemyBlowUpSpell = this.BlowUpHero(heroCard, SpellType.ENDGAME_DRAW);
				}
				if (this.m_Mission_FriendlyHeroShouldExplodeOnDefeat)
				{
					this.m_friendlyBlowUpSpell = this.BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				}
				break;
			}
			}
		}
		base.ShowEndGameScreen(playState, this.m_enemyBlowUpSpell, this.m_friendlyBlowUpSpell);
		GameEntity.Coroutines.StartCoroutine(this.HandleGameOverWithTiming(playState));
	}

	// Token: 0x06004E11 RID: 19985 RVA: 0x0019CDF4 File Offset: 0x0019AFF4
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BOM_03_Guff_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004E12 RID: 19986 RVA: 0x0019CE44 File Offset: 0x0019B044
	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord adventureDataDbfRecord in GameDbf.AdventureData.GetRecords())
		{
			if (adventureDataDbfRecord.AdventureId == adventureId && adventureDataDbfRecord.ModeId == modeId)
			{
				return adventureDataDbfRecord;
			}
		}
		return null;
	}

	// Token: 0x06004E13 RID: 19987 RVA: 0x0019CEB0 File Offset: 0x0019B0B0
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

	// Token: 0x06004E14 RID: 19988 RVA: 0x0019CF40 File Offset: 0x0019B140
	protected Actor GetFriendlyActorByCardId(string CardId)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == CardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x06004E15 RID: 19989 RVA: 0x0019CFD0 File Offset: 0x0019B1D0
	protected IEnumerator MissionPlayThinkEmote(Actor thinkingActor)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		Actor actor2 = GameState.Get().GetFriendlySidePlayer().GetHero().GetCard().GetActor();
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
		if (thinkingActor == actor)
		{
			CardSoundSpell cardSoundSpell = GameState.Get().GetOpposingSidePlayer().GetHeroCard().PlayEmote(emoteType);
			AudioSource activeAudioSource = cardSoundSpell.GetActiveAudioSource();
			yield return GameState.Get().GetOpposingSidePlayer().GetHeroCard().PlayEmote(emoteType);
			yield return new WaitForSeconds(activeAudioSource.clip.length);
			activeAudioSource = null;
		}
		else if (thinkingActor == actor2)
		{
			CardSoundSpell cardSoundSpell2 = GameState.Get().GetOpposingSidePlayer().GetHeroCard().PlayEmote(emoteType);
			AudioSource activeAudioSource = cardSoundSpell2.GetActiveAudioSource();
			yield return GameState.Get().GetOpposingSidePlayer().GetHeroCard().PlayEmote(emoteType);
			yield return new WaitForSeconds(activeAudioSource.clip.length);
			activeAudioSource = null;
		}
		yield break;
	}

	// Token: 0x06004E16 RID: 19990 RVA: 0x0019CFE0 File Offset: 0x0019B1E0
	protected Actor GetActorByCardId(string CardId)
	{
		Player friendlySidePlayer = GameState.Get().GetFriendlySidePlayer();
		foreach (Card card in friendlySidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity = card.GetEntity();
			if (entity.GetControllerId() == friendlySidePlayer.GetPlayerId() && entity.GetCardId() == CardId)
			{
				return entity.GetCard().GetActor();
			}
		}
		Player opposingSidePlayer = GameState.Get().GetOpposingSidePlayer();
		foreach (Card card2 in opposingSidePlayer.GetBattlefieldZone().GetCards())
		{
			Entity entity2 = card2.GetEntity();
			if (entity2.GetControllerId() == opposingSidePlayer.GetPlayerId() && entity2.GetCardId() == CardId)
			{
				return entity2.GetCard().GetActor();
			}
		}
		return null;
	}

	// Token: 0x06004E17 RID: 19991 RVA: 0x0019D0F4 File Offset: 0x0019B2F4
	protected string PopRandomLine(List<string> lines, bool removeLine = true)
	{
		if (lines == null)
		{
			return null;
		}
		if (lines.Count == 0)
		{
			return null;
		}
		string text = lines[UnityEngine.Random.Range(0, lines.Count)];
		if (removeLine)
		{
			lines.Remove(text);
		}
		return text;
	}

	// Token: 0x06004E18 RID: 19992 RVA: 0x0019D130 File Offset: 0x0019B330
	protected string PopNextLine(List<string> lines, bool removeLine = true)
	{
		string text = null;
		for (int i = 0; i < lines.Count; i++)
		{
			if (!this.m_InOrderPlayedLines.Contains(lines[i]))
			{
				text = lines[i];
				break;
			}
		}
		if (text == null)
		{
			return null;
		}
		if (removeLine)
		{
			this.m_InOrderPlayedLines.Add(text);
		}
		return text;
	}

	// Token: 0x06004E19 RID: 19993 RVA: 0x0019D184 File Offset: 0x0019B384
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004E1A RID: 19994 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004E1B RID: 19995 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004E1C RID: 19996 RVA: 0x0019D192 File Offset: 0x0019B392
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004E1D RID: 19997 RVA: 0x0019D19C File Offset: 0x0019B39C
	public bool shouldPlayBanterVO()
	{
		float time = Time.time;
		float num = this.m_lastVOplayFinshtime + this.m_BanterVOSilenceTime;
		return time > num;
	}

	// Token: 0x06004E1E RID: 19998 RVA: 0x0019D1C2 File Offset: 0x0019B3C2
	protected IEnumerator MissionPlayVO(Actor actor, string line, bool bUseBubble, MissionEntity.ShouldPlay shouldPlay)
	{
		if (actor == null)
		{
			yield break;
		}
		if (line == null)
		{
			yield break;
		}
		Notification.SpeechBubbleDirection speakerDirection = base.GetDirection(actor);
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayLine(actor, line, shouldPlay, 2.5f));
		}
		if (shouldPlay() == this.InternalShouldPlayAlways())
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeech(line, speakerDirection, actor, 2.5f, 1f, true, false, 0f));
		}
		else if (shouldPlay() == base.InternalShouldPlayOnlyOnce())
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeechOnce(line, speakerDirection, actor, 2.5f, 1f, true, false, 0f));
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
		}
		yield break;
	}

	// Token: 0x06004E1F RID: 19999 RVA: 0x0019D1E7 File Offset: 0x0019B3E7
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004E20 RID: 20000 RVA: 0x0019D204 File Offset: 0x0019B404
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004E21 RID: 20001 RVA: 0x0019D221 File Offset: 0x0019B421
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004E22 RID: 20002 RVA: 0x0019D23E File Offset: 0x0019B43E
	protected IEnumerator MissionPlayVO(Actor speaker, List<string> lines, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = true)
	{
		bool removeLine = false;
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Once && !this.m_forceAlwaysPlayLine)
		{
			removeLine = true;
		}
		string text;
		if (bPlayOrder)
		{
			text = this.PopRandomLine(lines, removeLine);
		}
		else
		{
			text = this.PopNextLine(lines, removeLine);
		}
		if (text == null)
		{
			yield break;
		}
		yield return this.MissionPlayVO(speaker, text, bUseBubble, shouldPlay);
		yield break;
	}

	// Token: 0x06004E23 RID: 20003 RVA: 0x0019D272 File Offset: 0x0019B472
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004E24 RID: 20004 RVA: 0x0019D28F File Offset: 0x0019B48F
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004E25 RID: 20005 RVA: 0x0019D2AC File Offset: 0x0019B4AC
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004E26 RID: 20006 RVA: 0x0019D2C9 File Offset: 0x0019B4C9
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004E27 RID: 20007 RVA: 0x0019D2E6 File Offset: 0x0019B4E6
	protected IEnumerator MissionPlayVO(string brassRing, string line, bool bUseBubble, MissionEntity.ShouldPlay shouldPlay)
	{
		if (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_enemySpeaking = true;
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWait(brassRing, line, 3f, 1f, true, false));
		}
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWait(brassRing, line, 3f, 1f, true, false));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlayBigCharacterQuoteAndWaitOnce(brassRing, line, 3f, 1f, false, false));
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
		}
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E28 RID: 20008 RVA: 0x0019D30B File Offset: 0x0019B50B
	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = true)
	{
		bool removeLine = false;
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Once && !this.m_forceAlwaysPlayLine)
		{
			removeLine = true;
		}
		string line;
		if (bPlayOrder)
		{
			line = this.PopNextLine(lines, removeLine);
		}
		else
		{
			line = this.PopRandomLine(lines, removeLine);
		}
		yield return this.MissionPlayVO(brassRing, line, bUseBubble, shouldPlay);
		yield break;
	}

	// Token: 0x06004E29 RID: 20009 RVA: 0x0019D33F File Offset: 0x0019B53F
	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, Actor minionOverride, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = true)
	{
		if (minionOverride == null)
		{
			yield return this.MissionPlayVO(brassRing, lines, shouldPlay, bUseBubble, bPlayOrder);
		}
		else
		{
			yield return this.MissionPlayVO(minionOverride, lines, shouldPlay, bUseBubble, bPlayOrder);
		}
		yield break;
	}

	// Token: 0x06004E2A RID: 20010 RVA: 0x0019D37B File Offset: 0x0019B57B
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004E2B RID: 20011 RVA: 0x0019D398 File Offset: 0x0019B598
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004E2C RID: 20012 RVA: 0x0019D3B5 File Offset: 0x0019B5B5
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004E2D RID: 20013 RVA: 0x0019D3D2 File Offset: 0x0019B5D2
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004E2E RID: 20014 RVA: 0x0019D3EF File Offset: 0x0019B5EF
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004E2F RID: 20015 RVA: 0x0019D40C File Offset: 0x0019B60C
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004E30 RID: 20016 RVA: 0x0019D429 File Offset: 0x0019B629
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004E31 RID: 20017 RVA: 0x0019D446 File Offset: 0x0019B646
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004E32 RID: 20018 RVA: 0x0019D463 File Offset: 0x0019B663
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004E33 RID: 20019 RVA: 0x0019D480 File Offset: 0x0019B680
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004E34 RID: 20020 RVA: 0x0019D49D File Offset: 0x0019B69D
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004E35 RID: 20021 RVA: 0x0019D4BA File Offset: 0x0019B6BA
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004E36 RID: 20022 RVA: 0x0019D4D7 File Offset: 0x0019B6D7
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		if (this.GetActorByCardId(minionSpeaker) == null)
		{
			yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		}
		else
		{
			yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		}
		yield break;
	}

	// Token: 0x06004E37 RID: 20023 RVA: 0x0019D4FB File Offset: 0x0019B6FB
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, AssetReference brassRing, string line)
	{
		if (this.GetActorByCardId(minionSpeaker) == null)
		{
			yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		}
		else
		{
			yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		}
		yield break;
	}

	// Token: 0x06004E38 RID: 20024 RVA: 0x0019D51F File Offset: 0x0019B71F
	public IEnumerator MissionPlayVO(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		if (this.GetActorByCardId(minionSpeaker) == null)
		{
			yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		}
		else
		{
			yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		}
		yield break;
	}

	// Token: 0x06004E39 RID: 20025 RVA: 0x0019D543 File Offset: 0x0019B743
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		if (this.GetActorByCardId(minionSpeaker) == null)
		{
			yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		}
		else
		{
			yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		}
		yield break;
	}

	// Token: 0x06004E3A RID: 20026 RVA: 0x0019D567 File Offset: 0x0019B767
	public IEnumerator MissionPlayVO(string minionSpeaker, AssetReference brassRing, string line)
	{
		if (this.GetActorByCardId(minionSpeaker) == null)
		{
			yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		}
		else
		{
			yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		}
		yield break;
	}

	// Token: 0x06004E3B RID: 20027 RVA: 0x0019D58B File Offset: 0x0019B78B
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		Actor actorByCardId = this.GetActorByCardId(minionSpeaker);
		if (actorByCardId == null)
		{
			yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		}
		else
		{
			yield return this.MissionPlayVO(actorByCardId, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		}
		yield break;
	}

	// Token: 0x06004E3C RID: 20028 RVA: 0x0019D5AF File Offset: 0x0019B7AF
	public IEnumerator MissionPlaySound(string line)
	{
		float waitTimeScale = 0f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		yield break;
	}

	// Token: 0x06004E3D RID: 20029 RVA: 0x0019D5C5 File Offset: 0x0019B7C5
	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004E3E RID: 20030 RVA: 0x0019D5DB File Offset: 0x0019B7DB
	public IEnumerator MissionPlaySound(AssetReference brassRing, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004E3F RID: 20031 RVA: 0x0019D5F1 File Offset: 0x0019B7F1
	public IEnumerator MissionPlaySound(string minionSpeaker, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004E40 RID: 20032 RVA: 0x0019D607 File Offset: 0x0019B807
	public IEnumerator MissionPlaySound(string minionSpeaker, AssetReference brassRing, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004E41 RID: 20033 RVA: 0x0019D61D File Offset: 0x0019B81D
	public IEnumerator MissionPlaySound(List<string> lines)
	{
		string line = this.PopRandomLine(lines, true);
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004E42 RID: 20034 RVA: 0x0019D633 File Offset: 0x0019B833
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004E43 RID: 20035 RVA: 0x0019D649 File Offset: 0x0019B849
	public IEnumerator MissionPlaySound(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004E44 RID: 20036 RVA: 0x0019D65F File Offset: 0x0019B85F
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004E45 RID: 20037 RVA: 0x0019D675 File Offset: 0x0019B875
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x040044C9 RID: 17609
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040044CA RID: 17610
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x040044CB RID: 17611
	public bool m_Mission_EnemyPlayIdleLinesUseingEmoteSystem;

	// Token: 0x040044CC RID: 17612
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x040044CD RID: 17613
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x040044CE RID: 17614
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x040044CF RID: 17615
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040044D0 RID: 17616
	public bool m_Mission_FriendlyPlayIdleLines = true;

	// Token: 0x040044D1 RID: 17617
	public bool m_Mission_FriendlyPlayIdleLinesUseingEmoteSystem = true;

	// Token: 0x040044D2 RID: 17618
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x040044D3 RID: 17619
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x040044D4 RID: 17620
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x040044D5 RID: 17621
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x040044D6 RID: 17622
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x040044D7 RID: 17623
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x040044D8 RID: 17624
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x040044D9 RID: 17625
	public List<string> m_BossIdleLines;

	// Token: 0x040044DA RID: 17626
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x040044DB RID: 17627
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x040044DC RID: 17628
	public int m_PlayBossVOLineIndex;

	// Token: 0x040044DD RID: 17629
	public string m_introLine;

	// Token: 0x040044DE RID: 17630
	public string m_deathLine;

	// Token: 0x040044DF RID: 17631
	public string m_standardEmoteResponseLine;

	// Token: 0x040044E0 RID: 17632
	public bool m_DoEmoteDrivenStart;

	// Token: 0x040044E1 RID: 17633
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x040044E2 RID: 17634
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x040044E3 RID: 17635
	public string m_OverrideBossSubtext;

	// Token: 0x040044E4 RID: 17636
	public string m_OverridePlayerSubtext;

	// Token: 0x040044E5 RID: 17637
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x040044E6 RID: 17638
	private Spell m_enemyBlowUpSpell;

	// Token: 0x040044E7 RID: 17639
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x040044E8 RID: 17640
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x040044E9 RID: 17641
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x040044EA RID: 17642
	public const bool PlayLinesRandomOrder = true;

	// Token: 0x040044EB RID: 17643
	public const bool PlayLinesInOrder = false;

	// Token: 0x040044EC RID: 17644
	public float m_lastVOplayFinshtime;

	// Token: 0x040044ED RID: 17645
	public float m_BanterVOSilenceTime = 2f;

	// Token: 0x040044EE RID: 17646
	private static Map<GameEntityOption, bool> s_booleanOptions = BOM_03_Guff_MissionEntity.InitBooleanOptions();

	// Token: 0x040044EF RID: 17647
	public const int InGame_BossAttacks = 500;

	// Token: 0x040044F0 RID: 17648
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x040044F1 RID: 17649
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x040044F2 RID: 17650
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x040044F3 RID: 17651
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x040044F4 RID: 17652
	public const int InGame_BossDeath = 516;

	// Token: 0x040044F5 RID: 17653
	public const int InGame_BossIdle = 517;

	// Token: 0x040044F6 RID: 17654
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x040044F7 RID: 17655
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x040044F8 RID: 17656
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x040044F9 RID: 17657
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x040044FA RID: 17658
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x040044FB RID: 17659
	public const int InGame_PlayerIdle = 518;

	// Token: 0x040044FC RID: 17660
	public const int InGame_PlayerDeath = 519;

	// Token: 0x040044FD RID: 17661
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x040044FE RID: 17662
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x040044FF RID: 17663
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x04004500 RID: 17664
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x04004501 RID: 17665
	public const int InGame_Introduction = 514;

	// Token: 0x04004502 RID: 17666
	public const int InGame_EmoteResponse = 515;

	// Token: 0x04004503 RID: 17667
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x04004504 RID: 17668
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x04004505 RID: 17669
	public const int DisableAutomaticVO = 602;

	// Token: 0x04004506 RID: 17670
	public const int EnableAutomaticVO = 603;

	// Token: 0x04004507 RID: 17671
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x04004508 RID: 17672
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x04004509 RID: 17673
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x0400450A RID: 17674
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x0400450B RID: 17675
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x0400450C RID: 17676
	public const int PlayNextBossLine = 1002;

	// Token: 0x0400450D RID: 17677
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x0400450E RID: 17678
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x0400450F RID: 17679
	public const int PlayAllVOLines = 1011;

	// Token: 0x04004510 RID: 17680
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x04004511 RID: 17681
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x04004512 RID: 17682
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001EB4 RID: 7860
	public static class MemberInfoGetting
	{
		// Token: 0x0601143B RID: 70715 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
