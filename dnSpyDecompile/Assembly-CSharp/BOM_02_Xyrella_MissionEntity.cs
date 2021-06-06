using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000571 RID: 1393
public abstract class BOM_02_Xyrella_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004D47 RID: 19783 RVA: 0x001990D0 File Offset: 0x001972D0
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

	// Token: 0x06004D48 RID: 19784 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004D49 RID: 19785 RVA: 0x00199138 File Offset: 0x00197338
	public BOM_02_Xyrella_MissionEntity()
	{
		this.m_gameOptions.AddBooleanOptions(BOM_02_Xyrella_MissionEntity.s_booleanOptions);
	}

	// Token: 0x06004D4A RID: 19786 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004D4B RID: 19787 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004D4C RID: 19788 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004D4D RID: 19789 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004D4E RID: 19790 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004D4F RID: 19791 RVA: 0x001991B8 File Offset: 0x001973B8
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x06004D50 RID: 19792 RVA: 0x001991CC File Offset: 0x001973CC
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

	// Token: 0x06004D51 RID: 19793 RVA: 0x001991DB File Offset: 0x001973DB
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

	// Token: 0x06004D52 RID: 19794 RVA: 0x001991F1 File Offset: 0x001973F1
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

	// Token: 0x06004D53 RID: 19795 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x06004D54 RID: 19796 RVA: 0x00199207 File Offset: 0x00197407
	protected IEnumerator DelayAndPlayInGameTrigger(int VOTriggerID)
	{
		float seconds = 3f;
		yield return new WaitForSeconds(seconds);
		yield return this.HandleMissionEventWithTiming(VOTriggerID);
		yield break;
	}

	// Token: 0x06004D55 RID: 19797 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004D56 RID: 19798 RVA: 0x0019921D File Offset: 0x0019741D
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004D57 RID: 19799 RVA: 0x0019923F File Offset: 0x0019743F
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

	// Token: 0x06004D58 RID: 19800 RVA: 0x00199265 File Offset: 0x00197465
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

	// Token: 0x06004D59 RID: 19801 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004D5A RID: 19802 RVA: 0x0019929E File Offset: 0x0019749E
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
		if (this.m_BossIdleLinesCopy.Count == 0)
		{
			this.m_BossIdleLinesCopy = new List<string>(this.m_BossIdleLines);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_Mission_EnemyPlayIdleLines = false;
			}
			else
			{
				this.m_Mission_EnemyPlayIdleLines = true;
			}
		}
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

	// Token: 0x06004D5B RID: 19803 RVA: 0x001992B0 File Offset: 0x001974B0
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

	// Token: 0x06004D5C RID: 19804 RVA: 0x00199434 File Offset: 0x00197634
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BOM_02_Xyrella_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004D5D RID: 19805 RVA: 0x00199484 File Offset: 0x00197684
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

	// Token: 0x06004D5E RID: 19806 RVA: 0x001994F0 File Offset: 0x001976F0
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

	// Token: 0x06004D5F RID: 19807 RVA: 0x00199580 File Offset: 0x00197780
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

	// Token: 0x06004D60 RID: 19808 RVA: 0x00199610 File Offset: 0x00197810
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

	// Token: 0x06004D61 RID: 19809 RVA: 0x00199620 File Offset: 0x00197820
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

	// Token: 0x06004D62 RID: 19810 RVA: 0x00199734 File Offset: 0x00197934
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

	// Token: 0x06004D63 RID: 19811 RVA: 0x00199770 File Offset: 0x00197970
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

	// Token: 0x06004D64 RID: 19812 RVA: 0x001997C4 File Offset: 0x001979C4
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004D65 RID: 19813 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004D66 RID: 19814 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004D67 RID: 19815 RVA: 0x001997D2 File Offset: 0x001979D2
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004D68 RID: 19816 RVA: 0x001997DC File Offset: 0x001979DC
	public bool shouldPlayBanterVO()
	{
		float time = Time.time;
		float num = this.m_lastVOplayFinshtime + this.m_BanterVOSilenceTime;
		return time > num;
	}

	// Token: 0x06004D69 RID: 19817 RVA: 0x00199802 File Offset: 0x00197A02
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
		this.m_lastVOplayFinshtime = Time.time;
		yield break;
	}

	// Token: 0x06004D6A RID: 19818 RVA: 0x00199827 File Offset: 0x00197A27
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004D6B RID: 19819 RVA: 0x00199844 File Offset: 0x00197A44
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004D6C RID: 19820 RVA: 0x00199861 File Offset: 0x00197A61
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004D6D RID: 19821 RVA: 0x0019987E File Offset: 0x00197A7E
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

	// Token: 0x06004D6E RID: 19822 RVA: 0x001998B2 File Offset: 0x00197AB2
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004D6F RID: 19823 RVA: 0x001998CF File Offset: 0x00197ACF
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004D70 RID: 19824 RVA: 0x001998EC File Offset: 0x00197AEC
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004D71 RID: 19825 RVA: 0x00199909 File Offset: 0x00197B09
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004D72 RID: 19826 RVA: 0x00199926 File Offset: 0x00197B26
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

	// Token: 0x06004D73 RID: 19827 RVA: 0x0019994B File Offset: 0x00197B4B
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

	// Token: 0x06004D74 RID: 19828 RVA: 0x0019997F File Offset: 0x00197B7F
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

	// Token: 0x06004D75 RID: 19829 RVA: 0x001999BB File Offset: 0x00197BBB
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004D76 RID: 19830 RVA: 0x001999D8 File Offset: 0x00197BD8
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004D77 RID: 19831 RVA: 0x001999F5 File Offset: 0x00197BF5
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004D78 RID: 19832 RVA: 0x00199A12 File Offset: 0x00197C12
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004D79 RID: 19833 RVA: 0x00199A2F File Offset: 0x00197C2F
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004D7A RID: 19834 RVA: 0x00199A4C File Offset: 0x00197C4C
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004D7B RID: 19835 RVA: 0x00199A69 File Offset: 0x00197C69
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004D7C RID: 19836 RVA: 0x00199A86 File Offset: 0x00197C86
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004D7D RID: 19837 RVA: 0x00199AA3 File Offset: 0x00197CA3
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004D7E RID: 19838 RVA: 0x00199AC0 File Offset: 0x00197CC0
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004D7F RID: 19839 RVA: 0x00199ADD File Offset: 0x00197CDD
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004D80 RID: 19840 RVA: 0x00199AFA File Offset: 0x00197CFA
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004D81 RID: 19841 RVA: 0x00199B17 File Offset: 0x00197D17
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

	// Token: 0x06004D82 RID: 19842 RVA: 0x00199B3B File Offset: 0x00197D3B
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

	// Token: 0x06004D83 RID: 19843 RVA: 0x00199B5F File Offset: 0x00197D5F
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

	// Token: 0x06004D84 RID: 19844 RVA: 0x00199B83 File Offset: 0x00197D83
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

	// Token: 0x06004D85 RID: 19845 RVA: 0x00199BA7 File Offset: 0x00197DA7
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

	// Token: 0x06004D86 RID: 19846 RVA: 0x00199BCB File Offset: 0x00197DCB
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

	// Token: 0x06004D87 RID: 19847 RVA: 0x00199BEF File Offset: 0x00197DEF
	public IEnumerator MissionPlaySound(string line)
	{
		float waitTimeScale = 0f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		yield break;
	}

	// Token: 0x06004D88 RID: 19848 RVA: 0x00199C05 File Offset: 0x00197E05
	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004D89 RID: 19849 RVA: 0x00199C1B File Offset: 0x00197E1B
	public IEnumerator MissionPlaySound(AssetReference brassRing, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004D8A RID: 19850 RVA: 0x00199C31 File Offset: 0x00197E31
	public IEnumerator MissionPlaySound(string minionSpeaker, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004D8B RID: 19851 RVA: 0x00199C47 File Offset: 0x00197E47
	public IEnumerator MissionPlaySound(string minionSpeaker, AssetReference brassRing, string line)
	{
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004D8C RID: 19852 RVA: 0x00199C5D File Offset: 0x00197E5D
	public IEnumerator MissionPlaySound(List<string> lines)
	{
		string line = this.PopRandomLine(lines, true);
		yield return this.MissionPlaySound(line);
		yield break;
	}

	// Token: 0x06004D8D RID: 19853 RVA: 0x00199C73 File Offset: 0x00197E73
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004D8E RID: 19854 RVA: 0x00199C89 File Offset: 0x00197E89
	public IEnumerator MissionPlaySound(string minionSpeaker, AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004D8F RID: 19855 RVA: 0x00199C9F File Offset: 0x00197E9F
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x06004D90 RID: 19856 RVA: 0x00199CB5 File Offset: 0x00197EB5
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlaySound(lines);
		yield break;
	}

	// Token: 0x0400434A RID: 17226
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x0400434B RID: 17227
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x0400434C RID: 17228
	public bool m_Mission_EnemyPlayIdleLinesUseingEmoteSystem;

	// Token: 0x0400434D RID: 17229
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x0400434E RID: 17230
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x0400434F RID: 17231
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x04004350 RID: 17232
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04004351 RID: 17233
	public bool m_Mission_FriendlyPlayIdleLines = true;

	// Token: 0x04004352 RID: 17234
	public bool m_Mission_FriendlyPlayIdleLinesUseingEmoteSystem = true;

	// Token: 0x04004353 RID: 17235
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x04004354 RID: 17236
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x04004355 RID: 17237
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x04004356 RID: 17238
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x04004357 RID: 17239
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x04004358 RID: 17240
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04004359 RID: 17241
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x0400435A RID: 17242
	public List<string> m_BossIdleLines;

	// Token: 0x0400435B RID: 17243
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x0400435C RID: 17244
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x0400435D RID: 17245
	public int m_PlayBossVOLineIndex;

	// Token: 0x0400435E RID: 17246
	public string m_introLine;

	// Token: 0x0400435F RID: 17247
	public string m_deathLine;

	// Token: 0x04004360 RID: 17248
	public string m_standardEmoteResponseLine;

	// Token: 0x04004361 RID: 17249
	public bool m_DoEmoteDrivenStart;

	// Token: 0x04004362 RID: 17250
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x04004363 RID: 17251
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x04004364 RID: 17252
	public string m_OverrideBossSubtext;

	// Token: 0x04004365 RID: 17253
	public string m_OverridePlayerSubtext;

	// Token: 0x04004366 RID: 17254
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x04004367 RID: 17255
	private Spell m_enemyBlowUpSpell;

	// Token: 0x04004368 RID: 17256
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x04004369 RID: 17257
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x0400436A RID: 17258
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x0400436B RID: 17259
	public const bool PlayLinesRandomOrder = true;

	// Token: 0x0400436C RID: 17260
	public const bool PlayLinesInOrder = false;

	// Token: 0x0400436D RID: 17261
	public float m_lastVOplayFinshtime;

	// Token: 0x0400436E RID: 17262
	public float m_BanterVOSilenceTime = 2f;

	// Token: 0x0400436F RID: 17263
	private static Map<GameEntityOption, bool> s_booleanOptions = BOM_02_Xyrella_MissionEntity.InitBooleanOptions();

	// Token: 0x04004370 RID: 17264
	public const int InGame_BossAttacks = 500;

	// Token: 0x04004371 RID: 17265
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x04004372 RID: 17266
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x04004373 RID: 17267
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x04004374 RID: 17268
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x04004375 RID: 17269
	public const int InGame_BossDeath = 516;

	// Token: 0x04004376 RID: 17270
	public const int InGame_BossIdle = 517;

	// Token: 0x04004377 RID: 17271
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x04004378 RID: 17272
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x04004379 RID: 17273
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x0400437A RID: 17274
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x0400437B RID: 17275
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x0400437C RID: 17276
	public const int InGame_PlayerIdle = 518;

	// Token: 0x0400437D RID: 17277
	public const int InGame_PlayerDeath = 519;

	// Token: 0x0400437E RID: 17278
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x0400437F RID: 17279
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x04004380 RID: 17280
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x04004381 RID: 17281
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x04004382 RID: 17282
	public const int InGame_Introduction = 514;

	// Token: 0x04004383 RID: 17283
	public const int InGame_EmoteResponse = 515;

	// Token: 0x04004384 RID: 17284
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x04004385 RID: 17285
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x04004386 RID: 17286
	public const int DisableAutomaticVO = 602;

	// Token: 0x04004387 RID: 17287
	public const int EnableAutomaticVO = 603;

	// Token: 0x04004388 RID: 17288
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x04004389 RID: 17289
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x0400438A RID: 17290
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x0400438B RID: 17291
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x0400438C RID: 17292
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x0400438D RID: 17293
	public const int PlayNextBossLine = 1002;

	// Token: 0x0400438E RID: 17294
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x0400438F RID: 17295
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x04004390 RID: 17296
	public const int PlayAllVOLines = 1011;

	// Token: 0x04004391 RID: 17297
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x04004392 RID: 17298
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x04004393 RID: 17299
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001E62 RID: 7778
	public static class MemberInfoGetting
	{
		// Token: 0x06011250 RID: 70224 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
