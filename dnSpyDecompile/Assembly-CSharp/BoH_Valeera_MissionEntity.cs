using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x0200055D RID: 1373
public abstract class BoH_Valeera_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004BE9 RID: 19433 RVA: 0x00191C70 File Offset: 0x0018FE70
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

	// Token: 0x06004BEA RID: 19434 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004BEB RID: 19435 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004BEC RID: 19436 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004BED RID: 19437 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004BEE RID: 19438 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004BEF RID: 19439 RVA: 0x00191CD8 File Offset: 0x0018FED8
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x06004BF0 RID: 19440 RVA: 0x00191CEC File Offset: 0x0018FEEC
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

	// Token: 0x06004BF1 RID: 19441 RVA: 0x00191CFB File Offset: 0x0018FEFB
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

	// Token: 0x06004BF2 RID: 19442 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x06004BF3 RID: 19443 RVA: 0x00191D11 File Offset: 0x0018FF11
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

	// Token: 0x06004BF4 RID: 19444 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004BF5 RID: 19445 RVA: 0x00191D20 File Offset: 0x0018FF20
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			MusicManager.Get().StartPlaylist(this.m_DefaultMusicTrack);
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004BF6 RID: 19446 RVA: 0x00191D4D File Offset: 0x0018FF4D
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

	// Token: 0x06004BF7 RID: 19447 RVA: 0x00191D7D File Offset: 0x0018FF7D
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

	// Token: 0x06004BF8 RID: 19448 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004BF9 RID: 19449 RVA: 0x00191DB6 File Offset: 0x0018FFB6
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

	// Token: 0x06004BFA RID: 19450 RVA: 0x00191DC8 File Offset: 0x0018FFC8
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

	// Token: 0x06004BFB RID: 19451 RVA: 0x00191F4C File Offset: 0x0019014C
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BoH_Valeera_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004BFC RID: 19452 RVA: 0x00191F9C File Offset: 0x0019019C
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

	// Token: 0x06004BFD RID: 19453 RVA: 0x00192008 File Offset: 0x00190208
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

	// Token: 0x06004BFE RID: 19454 RVA: 0x00192098 File Offset: 0x00190298
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

	// Token: 0x06004BFF RID: 19455 RVA: 0x00192128 File Offset: 0x00190328
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

	// Token: 0x06004C00 RID: 19456 RVA: 0x0019223C File Offset: 0x0019043C
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

	// Token: 0x06004C01 RID: 19457 RVA: 0x00192278 File Offset: 0x00190478
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

	// Token: 0x06004C02 RID: 19458 RVA: 0x001922CC File Offset: 0x001904CC
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004C03 RID: 19459 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004C04 RID: 19460 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004C05 RID: 19461 RVA: 0x001922DA File Offset: 0x001904DA
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004C06 RID: 19462 RVA: 0x001922E2 File Offset: 0x001904E2
	protected IEnumerator MissionPlayVO(Actor actor, string line, bool bUseBubble, MissionEntity.ShouldPlay shouldPlay)
	{
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

	// Token: 0x06004C07 RID: 19463 RVA: 0x00192307 File Offset: 0x00190507
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004C08 RID: 19464 RVA: 0x00192324 File Offset: 0x00190524
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004C09 RID: 19465 RVA: 0x00192341 File Offset: 0x00190541
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004C0A RID: 19466 RVA: 0x0019235E File Offset: 0x0019055E
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

	// Token: 0x06004C0B RID: 19467 RVA: 0x00192392 File Offset: 0x00190592
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C0C RID: 19468 RVA: 0x001923AF File Offset: 0x001905AF
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C0D RID: 19469 RVA: 0x001923CC File Offset: 0x001905CC
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004C0E RID: 19470 RVA: 0x001923E9 File Offset: 0x001905E9
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004C0F RID: 19471 RVA: 0x00192406 File Offset: 0x00190606
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x06004C10 RID: 19472 RVA: 0x00192423 File Offset: 0x00190623
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

	// Token: 0x06004C11 RID: 19473 RVA: 0x00192448 File Offset: 0x00190648
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

	// Token: 0x06004C12 RID: 19474 RVA: 0x0019247C File Offset: 0x0019067C
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

	// Token: 0x06004C13 RID: 19475 RVA: 0x001924B8 File Offset: 0x001906B8
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004C14 RID: 19476 RVA: 0x001924D5 File Offset: 0x001906D5
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004C15 RID: 19477 RVA: 0x001924F2 File Offset: 0x001906F2
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C16 RID: 19478 RVA: 0x0019250F File Offset: 0x0019070F
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C17 RID: 19479 RVA: 0x0019252C File Offset: 0x0019072C
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004C18 RID: 19480 RVA: 0x00192549 File Offset: 0x00190749
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004C19 RID: 19481 RVA: 0x00192566 File Offset: 0x00190766
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x06004C1A RID: 19482 RVA: 0x00192583 File Offset: 0x00190783
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004C1B RID: 19483 RVA: 0x001925A0 File Offset: 0x001907A0
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004C1C RID: 19484 RVA: 0x001925BD File Offset: 0x001907BD
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C1D RID: 19485 RVA: 0x001925DA File Offset: 0x001907DA
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004C1E RID: 19486 RVA: 0x001925F7 File Offset: 0x001907F7
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004C1F RID: 19487 RVA: 0x00192614 File Offset: 0x00190814
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004C20 RID: 19488 RVA: 0x00192631 File Offset: 0x00190831
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x04004075 RID: 16501
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04004076 RID: 16502
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x04004077 RID: 16503
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x04004078 RID: 16504
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x04004079 RID: 16505
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x0400407A RID: 16506
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x0400407B RID: 16507
	public bool m_Mission_FriendlyPlayIdleLines;

	// Token: 0x0400407C RID: 16508
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x0400407D RID: 16509
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x0400407E RID: 16510
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x0400407F RID: 16511
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x04004080 RID: 16512
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x04004081 RID: 16513
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04004082 RID: 16514
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04004083 RID: 16515
	public List<string> m_BossIdleLines;

	// Token: 0x04004084 RID: 16516
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04004085 RID: 16517
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x04004086 RID: 16518
	public int m_PlayBossVOLineIndex;

	// Token: 0x04004087 RID: 16519
	public string m_introLine;

	// Token: 0x04004088 RID: 16520
	public string m_deathLine;

	// Token: 0x04004089 RID: 16521
	public string m_standardEmoteResponseLine;

	// Token: 0x0400408A RID: 16522
	public bool m_DoEmoteDrivenStart;

	// Token: 0x0400408B RID: 16523
	public MusicPlaylistType m_DefaultMulliganMusicTrack = MusicPlaylistType.InGame_Mulligan;

	// Token: 0x0400408C RID: 16524
	public MusicPlaylistType m_DefaultMusicTrack = MusicPlaylistType.InGame_Default;

	// Token: 0x0400408D RID: 16525
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x0400408E RID: 16526
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x0400408F RID: 16527
	public string m_OverrideBossSubtext;

	// Token: 0x04004090 RID: 16528
	public string m_OverridePlayerSubtext;

	// Token: 0x04004091 RID: 16529
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x04004092 RID: 16530
	private Spell m_enemyBlowUpSpell;

	// Token: 0x04004093 RID: 16531
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x04004094 RID: 16532
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x04004095 RID: 16533
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x04004096 RID: 16534
	public const bool PlayLinesRandomOrder = false;

	// Token: 0x04004097 RID: 16535
	public const bool PlayLinesInOrder = true;

	// Token: 0x04004098 RID: 16536
	public const int InGame_BossAttacks = 500;

	// Token: 0x04004099 RID: 16537
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x0400409A RID: 16538
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x0400409B RID: 16539
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x0400409C RID: 16540
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x0400409D RID: 16541
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x0400409E RID: 16542
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x0400409F RID: 16543
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x040040A0 RID: 16544
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x040040A1 RID: 16545
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x040040A2 RID: 16546
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x040040A3 RID: 16547
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x040040A4 RID: 16548
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x040040A5 RID: 16549
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x040040A6 RID: 16550
	public const int InGame_Introduction = 514;

	// Token: 0x040040A7 RID: 16551
	public const int InGame_EmoteResponse = 515;

	// Token: 0x040040A8 RID: 16552
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x040040A9 RID: 16553
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x040040AA RID: 16554
	public const int DisableAutomaticVO = 602;

	// Token: 0x040040AB RID: 16555
	public const int EnableAutomaticVO = 603;

	// Token: 0x040040AC RID: 16556
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x040040AD RID: 16557
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x040040AE RID: 16558
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x040040AF RID: 16559
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x040040B0 RID: 16560
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x040040B1 RID: 16561
	public const int PlayNextBossLine = 1002;

	// Token: 0x040040B2 RID: 16562
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x040040B3 RID: 16563
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x040040B4 RID: 16564
	public const int PlayAllVOLines = 1011;

	// Token: 0x040040B5 RID: 16565
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x040040B6 RID: 16566
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x040040B7 RID: 16567
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001DD3 RID: 7635
	public static class MemberInfoGetting
	{
		// Token: 0x06010EF8 RID: 69368 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
