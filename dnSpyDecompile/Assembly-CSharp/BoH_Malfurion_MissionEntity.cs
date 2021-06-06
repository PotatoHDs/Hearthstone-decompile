using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000535 RID: 1333
public abstract class BoH_Malfurion_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x060048AE RID: 18606 RVA: 0x0018526C File Offset: 0x0018346C
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

	// Token: 0x060048AF RID: 18607 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x060048B0 RID: 18608 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x060048B1 RID: 18609 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x060048B2 RID: 18610 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x060048B3 RID: 18611 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x060048B4 RID: 18612 RVA: 0x001852D4 File Offset: 0x001834D4
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x060048B5 RID: 18613 RVA: 0x001852E8 File Offset: 0x001834E8
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

	// Token: 0x060048B6 RID: 18614 RVA: 0x001852F7 File Offset: 0x001834F7
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

	// Token: 0x060048B7 RID: 18615 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x060048B8 RID: 18616 RVA: 0x0018530D File Offset: 0x0018350D
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

	// Token: 0x060048B9 RID: 18617 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x060048BA RID: 18618 RVA: 0x0018531C File Offset: 0x0018351C
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x060048BB RID: 18619 RVA: 0x0018533E File Offset: 0x0018353E
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

	// Token: 0x060048BC RID: 18620 RVA: 0x00185364 File Offset: 0x00183564
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

	// Token: 0x060048BD RID: 18621 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x060048BE RID: 18622 RVA: 0x0018539D File Offset: 0x0018359D
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

	// Token: 0x060048BF RID: 18623 RVA: 0x001853AC File Offset: 0x001835AC
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

	// Token: 0x060048C0 RID: 18624 RVA: 0x00185530 File Offset: 0x00183730
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BoH_Malfurion_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x060048C1 RID: 18625 RVA: 0x00185580 File Offset: 0x00183780
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

	// Token: 0x060048C2 RID: 18626 RVA: 0x001855EC File Offset: 0x001837EC
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

	// Token: 0x060048C3 RID: 18627 RVA: 0x0018567C File Offset: 0x0018387C
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

	// Token: 0x060048C4 RID: 18628 RVA: 0x0018570C File Offset: 0x0018390C
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

	// Token: 0x060048C5 RID: 18629 RVA: 0x00185820 File Offset: 0x00183A20
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

	// Token: 0x060048C6 RID: 18630 RVA: 0x0018585C File Offset: 0x00183A5C
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

	// Token: 0x060048C7 RID: 18631 RVA: 0x001858B0 File Offset: 0x00183AB0
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x060048C8 RID: 18632 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x060048C9 RID: 18633 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x060048CA RID: 18634 RVA: 0x001858BE File Offset: 0x00183ABE
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x060048CB RID: 18635 RVA: 0x001858C6 File Offset: 0x00183AC6
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

	// Token: 0x060048CC RID: 18636 RVA: 0x001858EB File Offset: 0x00183AEB
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x060048CD RID: 18637 RVA: 0x00185908 File Offset: 0x00183B08
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x060048CE RID: 18638 RVA: 0x00185925 File Offset: 0x00183B25
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x060048CF RID: 18639 RVA: 0x00185942 File Offset: 0x00183B42
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

	// Token: 0x060048D0 RID: 18640 RVA: 0x00185976 File Offset: 0x00183B76
	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		float waitTimeScale = 0f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		yield break;
	}

	// Token: 0x060048D1 RID: 18641 RVA: 0x0018598C File Offset: 0x00183B8C
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		bool removeLine = false;
		string line = this.PopRandomLine(lines, removeLine);
		yield return this.MissionPlaySound(actor, line);
		yield break;
	}

	// Token: 0x060048D2 RID: 18642 RVA: 0x001859A9 File Offset: 0x00183BA9
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x060048D3 RID: 18643 RVA: 0x001859C6 File Offset: 0x00183BC6
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x060048D4 RID: 18644 RVA: 0x001859E3 File Offset: 0x00183BE3
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x060048D5 RID: 18645 RVA: 0x00185A00 File Offset: 0x00183C00
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x060048D6 RID: 18646 RVA: 0x00185A1D File Offset: 0x00183C1D
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

	// Token: 0x060048D7 RID: 18647 RVA: 0x00185A42 File Offset: 0x00183C42
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

	// Token: 0x060048D8 RID: 18648 RVA: 0x00185A76 File Offset: 0x00183C76
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

	// Token: 0x060048D9 RID: 18649 RVA: 0x00185AB2 File Offset: 0x00183CB2
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x060048DA RID: 18650 RVA: 0x00185ACF File Offset: 0x00183CCF
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x060048DB RID: 18651 RVA: 0x00185AEC File Offset: 0x00183CEC
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x060048DC RID: 18652 RVA: 0x00185B09 File Offset: 0x00183D09
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x060048DD RID: 18653 RVA: 0x00185B26 File Offset: 0x00183D26
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x060048DE RID: 18654 RVA: 0x00185B43 File Offset: 0x00183D43
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x060048DF RID: 18655 RVA: 0x00185B60 File Offset: 0x00183D60
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x060048E0 RID: 18656 RVA: 0x00185B7D File Offset: 0x00183D7D
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x060048E1 RID: 18657 RVA: 0x00185B9A File Offset: 0x00183D9A
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x060048E2 RID: 18658 RVA: 0x00185BB7 File Offset: 0x00183DB7
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x060048E3 RID: 18659 RVA: 0x00185BD4 File Offset: 0x00183DD4
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x060048E4 RID: 18660 RVA: 0x00185BF1 File Offset: 0x00183DF1
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x060048E5 RID: 18661 RVA: 0x00185C0E File Offset: 0x00183E0E
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x060048E6 RID: 18662 RVA: 0x00185C2B File Offset: 0x00183E2B
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x04003C93 RID: 15507
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04003C94 RID: 15508
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x04003C95 RID: 15509
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x04003C96 RID: 15510
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x04003C97 RID: 15511
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x04003C98 RID: 15512
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04003C99 RID: 15513
	public bool m_Mission_FriendlyPlayIdleLines;

	// Token: 0x04003C9A RID: 15514
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x04003C9B RID: 15515
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x04003C9C RID: 15516
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x04003C9D RID: 15517
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x04003C9E RID: 15518
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x04003C9F RID: 15519
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003CA0 RID: 15520
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003CA1 RID: 15521
	public List<string> m_BossIdleLines;

	// Token: 0x04003CA2 RID: 15522
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003CA3 RID: 15523
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x04003CA4 RID: 15524
	public int m_PlayBossVOLineIndex;

	// Token: 0x04003CA5 RID: 15525
	public string m_introLine;

	// Token: 0x04003CA6 RID: 15526
	public string m_deathLine;

	// Token: 0x04003CA7 RID: 15527
	public string m_standardEmoteResponseLine;

	// Token: 0x04003CA8 RID: 15528
	public bool m_DoEmoteDrivenStart;

	// Token: 0x04003CA9 RID: 15529
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x04003CAA RID: 15530
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x04003CAB RID: 15531
	public string m_OverrideBossSubtext;

	// Token: 0x04003CAC RID: 15532
	public string m_OverridePlayerSubtext;

	// Token: 0x04003CAD RID: 15533
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x04003CAE RID: 15534
	private Spell m_enemyBlowUpSpell;

	// Token: 0x04003CAF RID: 15535
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x04003CB0 RID: 15536
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x04003CB1 RID: 15537
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x04003CB2 RID: 15538
	public const bool PlayLinesRandomOrder = false;

	// Token: 0x04003CB3 RID: 15539
	public const bool PlayLinesInOrder = true;

	// Token: 0x04003CB4 RID: 15540
	public const int InGame_BossAttacks = 500;

	// Token: 0x04003CB5 RID: 15541
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x04003CB6 RID: 15542
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x04003CB7 RID: 15543
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x04003CB8 RID: 15544
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x04003CB9 RID: 15545
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x04003CBA RID: 15546
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x04003CBB RID: 15547
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x04003CBC RID: 15548
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x04003CBD RID: 15549
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x04003CBE RID: 15550
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x04003CBF RID: 15551
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x04003CC0 RID: 15552
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x04003CC1 RID: 15553
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x04003CC2 RID: 15554
	public const int InGame_Introduction = 514;

	// Token: 0x04003CC3 RID: 15555
	public const int InGame_EmoteResponse = 515;

	// Token: 0x04003CC4 RID: 15556
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x04003CC5 RID: 15557
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x04003CC6 RID: 15558
	public const int DisableAutomaticVO = 602;

	// Token: 0x04003CC7 RID: 15559
	public const int EnableAutomaticVO = 603;

	// Token: 0x04003CC8 RID: 15560
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x04003CC9 RID: 15561
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x04003CCA RID: 15562
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x04003CCB RID: 15563
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x04003CCC RID: 15564
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x04003CCD RID: 15565
	public const int PlayNextBossLine = 1002;

	// Token: 0x04003CCE RID: 15566
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x04003CCF RID: 15567
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x04003CD0 RID: 15568
	public const int PlayAllVOLines = 1011;

	// Token: 0x04003CD1 RID: 15569
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x04003CD2 RID: 15570
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x04003CD3 RID: 15571
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001CCF RID: 7375
	public static class MemberInfoGetting
	{
		// Token: 0x060108EC RID: 67820 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
