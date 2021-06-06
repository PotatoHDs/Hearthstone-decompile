using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000517 RID: 1303
public abstract class BoH_Andiun_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004666 RID: 18022 RVA: 0x0017BFB4 File Offset: 0x0017A1B4
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

	// Token: 0x06004667 RID: 18023 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004668 RID: 18024 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004669 RID: 18025 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x0600466A RID: 18026 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x0600466B RID: 18027 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x0600466C RID: 18028 RVA: 0x0017C01C File Offset: 0x0017A21C
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x0600466D RID: 18029 RVA: 0x0017C030 File Offset: 0x0017A230
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
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero().GetCard().GetActor();
		if (actor == null)
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
			yield break;
		}
		yield return this.MissionPlayVO(actor, text);
		yield break;
	}

	// Token: 0x0600466E RID: 18030 RVA: 0x0017C03F File Offset: 0x0017A23F
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

	// Token: 0x0600466F RID: 18031 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x06004670 RID: 18032 RVA: 0x0017C084 File Offset: 0x0017A284
	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		if (gameResult != TAG_PLAYSTATE.WON)
		{
			if (gameResult == TAG_PLAYSTATE.LOST)
			{
				GameState.Get().SetBusy(true);
				GameState.Get().SetBusy(false);
			}
		}
		else
		{
			GameState.Get().SetBusy(true);
			GameState.Get().SetBusy(false);
		}
		yield break;
	}

	// Token: 0x06004671 RID: 18033 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004672 RID: 18034 RVA: 0x0017C093 File Offset: 0x0017A293
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			MusicManager.Get().StartPlaylist(this.m_DefaultMusicTrack);
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004673 RID: 18035 RVA: 0x0017C0C0 File Offset: 0x0017A2C0
	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			if (this.m_OverrideMulliganMusicTrack == MusicPlaylistType.Invalid)
			{
				MusicManager.Get().StartPlaylist(this.m_DefaultMulliganMusicTrack);
				return;
			}
			MusicManager.Get().StartPlaylist(this.m_OverrideMulliganMusicTrack);
		}
	}

	// Token: 0x06004674 RID: 18036 RVA: 0x0017C0F0 File Offset: 0x0017A2F0
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

	// Token: 0x06004675 RID: 18037 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004676 RID: 18038 RVA: 0x0017C13C File Offset: 0x0017A33C
	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
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
		if (thinkEmoteBossIdleChancePercentage < num2 || (!this.m_Mission_FriendlyPlayIdleLines && this.m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = this.PopRandomLine(this.m_BossIdleLinesCopy, true);
			if (this.m_BossIdleLinesCopy.Count == 0)
			{
				this.m_BossIdleLinesCopy = new List<string>(this.m_BossIdleLines);
			}
			yield return this.MissionPlayVO(actor, line);
		}
		else
		{
			if (!this.m_Mission_FriendlyPlayIdleLines)
			{
				yield break;
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
			CardSoundSpell cardSoundSpell = GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
			AudioSource activeAudioSource = cardSoundSpell.GetActiveAudioSource();
			yield return GameState.Get().GetCurrentPlayer().GetHeroCard().PlayEmote(emoteType);
			yield return new WaitForSeconds(activeAudioSource.clip.length);
			activeAudioSource = null;
		}
		yield break;
	}

	// Token: 0x06004677 RID: 18039 RVA: 0x0017C14C File Offset: 0x0017A34C
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

	// Token: 0x06004678 RID: 18040 RVA: 0x0017C2D0 File Offset: 0x0017A4D0
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BoH_Andiun_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004679 RID: 18041 RVA: 0x0017C320 File Offset: 0x0017A520
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

	// Token: 0x0600467A RID: 18042 RVA: 0x0017C38C File Offset: 0x0017A58C
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

	// Token: 0x0600467B RID: 18043 RVA: 0x0017C41C File Offset: 0x0017A61C
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

	// Token: 0x0600467C RID: 18044 RVA: 0x0017C4AC File Offset: 0x0017A6AC
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

	// Token: 0x0600467D RID: 18045 RVA: 0x0017C5C0 File Offset: 0x0017A7C0
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

	// Token: 0x0600467E RID: 18046 RVA: 0x0017C5FC File Offset: 0x0017A7FC
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

	// Token: 0x0600467F RID: 18047 RVA: 0x0017C650 File Offset: 0x0017A850
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004680 RID: 18048 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004681 RID: 18049 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004682 RID: 18050 RVA: 0x0017C65E File Offset: 0x0017A85E
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004683 RID: 18051 RVA: 0x0017C666 File Offset: 0x0017A866
	protected IEnumerator MissionPlayVO(Actor actor, string line, bool bUseBubble, MissionEntity.ShouldPlay shouldPlay)
	{
		if (actor == null)
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

	// Token: 0x06004684 RID: 18052 RVA: 0x0017C68B File Offset: 0x0017A88B
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004685 RID: 18053 RVA: 0x0017C6A8 File Offset: 0x0017A8A8
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004686 RID: 18054 RVA: 0x0017C6C5 File Offset: 0x0017A8C5
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004687 RID: 18055 RVA: 0x0017C6E2 File Offset: 0x0017A8E2
	protected IEnumerator MissionPlayVO(Actor speaker, List<string> lines, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
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

	// Token: 0x06004688 RID: 18056 RVA: 0x0017C716 File Offset: 0x0017A916
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004689 RID: 18057 RVA: 0x0017C733 File Offset: 0x0017A933
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x0600468A RID: 18058 RVA: 0x0017C750 File Offset: 0x0017A950
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x0600468B RID: 18059 RVA: 0x0017C76D File Offset: 0x0017A96D
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x0600468C RID: 18060 RVA: 0x0017C78A File Offset: 0x0017A98A
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x0600468D RID: 18061 RVA: 0x0017C7A7 File Offset: 0x0017A9A7
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

	// Token: 0x0600468E RID: 18062 RVA: 0x0017C7CC File Offset: 0x0017A9CC
	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
	{
		bool removeLine = false;
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Once && !this.m_forceAlwaysPlayLine)
		{
			removeLine = true;
		}
		string line;
		if (bPlayOrder)
		{
			line = this.PopRandomLine(lines, removeLine);
		}
		else
		{
			line = this.PopNextLine(lines, removeLine);
		}
		yield return this.MissionPlayVO(brassRing, line, bUseBubble, shouldPlay);
		yield break;
	}

	// Token: 0x0600468F RID: 18063 RVA: 0x0017C800 File Offset: 0x0017AA00
	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, Actor minionOverride, MissionEntity.ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
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

	// Token: 0x06004690 RID: 18064 RVA: 0x0017C83C File Offset: 0x0017AA3C
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004691 RID: 18065 RVA: 0x0017C859 File Offset: 0x0017AA59
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004692 RID: 18066 RVA: 0x0017C876 File Offset: 0x0017AA76
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004693 RID: 18067 RVA: 0x0017C893 File Offset: 0x0017AA93
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004694 RID: 18068 RVA: 0x0017C8B0 File Offset: 0x0017AAB0
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004695 RID: 18069 RVA: 0x0017C8CD File Offset: 0x0017AACD
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004696 RID: 18070 RVA: 0x0017C8EA File Offset: 0x0017AAEA
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x06004697 RID: 18071 RVA: 0x0017C907 File Offset: 0x0017AB07
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004698 RID: 18072 RVA: 0x0017C924 File Offset: 0x0017AB24
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004699 RID: 18073 RVA: 0x0017C941 File Offset: 0x0017AB41
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x0600469A RID: 18074 RVA: 0x0017C95E File Offset: 0x0017AB5E
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x0600469B RID: 18075 RVA: 0x0017C97B File Offset: 0x0017AB7B
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x0600469C RID: 18076 RVA: 0x0017C998 File Offset: 0x0017AB98
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x0600469D RID: 18077 RVA: 0x0017C9B5 File Offset: 0x0017ABB5
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x040039B4 RID: 14772
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040039B5 RID: 14773
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x040039B6 RID: 14774
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x040039B7 RID: 14775
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x040039B8 RID: 14776
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x040039B9 RID: 14777
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040039BA RID: 14778
	public bool m_Mission_FriendlyPlayIdleLines;

	// Token: 0x040039BB RID: 14779
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x040039BC RID: 14780
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x040039BD RID: 14781
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x040039BE RID: 14782
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x040039BF RID: 14783
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x040039C0 RID: 14784
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x040039C1 RID: 14785
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x040039C2 RID: 14786
	public List<string> m_BossIdleLines;

	// Token: 0x040039C3 RID: 14787
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x040039C4 RID: 14788
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x040039C5 RID: 14789
	public int m_PlayBossVOLineIndex;

	// Token: 0x040039C6 RID: 14790
	public string m_introLine;

	// Token: 0x040039C7 RID: 14791
	public string m_deathLine;

	// Token: 0x040039C8 RID: 14792
	public string m_standardEmoteResponseLine;

	// Token: 0x040039C9 RID: 14793
	public bool m_DoEmoteDrivenStart;

	// Token: 0x040039CA RID: 14794
	public MusicPlaylistType m_DefaultMulliganMusicTrack = MusicPlaylistType.InGame_Mulligan;

	// Token: 0x040039CB RID: 14795
	public MusicPlaylistType m_DefaultMusicTrack = MusicPlaylistType.InGame_Default;

	// Token: 0x040039CC RID: 14796
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x040039CD RID: 14797
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x040039CE RID: 14798
	public string m_OverrideBossSubtext;

	// Token: 0x040039CF RID: 14799
	public string m_OverridePlayerSubtext;

	// Token: 0x040039D0 RID: 14800
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x040039D1 RID: 14801
	private Spell m_enemyBlowUpSpell;

	// Token: 0x040039D2 RID: 14802
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x040039D3 RID: 14803
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x040039D4 RID: 14804
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x040039D5 RID: 14805
	public const bool PlayLinesRandomOrder = false;

	// Token: 0x040039D6 RID: 14806
	public const bool PlayLinesInOrder = true;

	// Token: 0x040039D7 RID: 14807
	public const int InGame_BossAttacks = 500;

	// Token: 0x040039D8 RID: 14808
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x040039D9 RID: 14809
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x040039DA RID: 14810
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x040039DB RID: 14811
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x040039DC RID: 14812
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x040039DD RID: 14813
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x040039DE RID: 14814
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x040039DF RID: 14815
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x040039E0 RID: 14816
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x040039E1 RID: 14817
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x040039E2 RID: 14818
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x040039E3 RID: 14819
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x040039E4 RID: 14820
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x040039E5 RID: 14821
	public const int InGame_Introduction = 514;

	// Token: 0x040039E6 RID: 14822
	public const int InGame_EmoteResponse = 515;

	// Token: 0x040039E7 RID: 14823
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x040039E8 RID: 14824
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x040039E9 RID: 14825
	public const int DisableAutomaticVO = 602;

	// Token: 0x040039EA RID: 14826
	public const int EnableAutomaticVO = 603;

	// Token: 0x040039EB RID: 14827
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x040039EC RID: 14828
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x040039ED RID: 14829
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x040039EE RID: 14830
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x040039EF RID: 14831
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x040039F0 RID: 14832
	public const int PlayNextBossLine = 1002;

	// Token: 0x040039F1 RID: 14833
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x040039F2 RID: 14834
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x040039F3 RID: 14835
	public const int PlayAllVOLines = 1011;

	// Token: 0x040039F4 RID: 14836
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x040039F5 RID: 14837
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x040039F6 RID: 14838
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001C19 RID: 7193
	public static class MemberInfoGetting
	{
		// Token: 0x060104B3 RID: 66739 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
