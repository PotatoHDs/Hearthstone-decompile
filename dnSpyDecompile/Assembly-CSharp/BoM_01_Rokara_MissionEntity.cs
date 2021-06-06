using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000567 RID: 1383
public abstract class BoM_01_Rokara_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004C93 RID: 19603 RVA: 0x0019573C File Offset: 0x0019393C
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

	// Token: 0x06004C94 RID: 19604 RVA: 0x0016DED1 File Offset: 0x0016C0D1
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

	// Token: 0x06004C95 RID: 19605 RVA: 0x001957A4 File Offset: 0x001939A4
	public BoM_01_Rokara_MissionEntity()
	{
		this.m_gameOptions.AddBooleanOptions(BoM_01_Rokara_MissionEntity.s_booleanOptions);
	}

	// Token: 0x06004C96 RID: 19606 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004C97 RID: 19607 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004C98 RID: 19608 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004C99 RID: 19609 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004C9A RID: 19610 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004C9B RID: 19611 RVA: 0x00195812 File Offset: 0x00193A12
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x06004C9C RID: 19612 RVA: 0x00195826 File Offset: 0x00193A26
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

	// Token: 0x06004C9D RID: 19613 RVA: 0x00195835 File Offset: 0x00193A35
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

	// Token: 0x06004C9E RID: 19614 RVA: 0x0019584B File Offset: 0x00193A4B
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

	// Token: 0x06004C9F RID: 19615 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x06004CA0 RID: 19616 RVA: 0x00195861 File Offset: 0x00193A61
	protected IEnumerator DelayAndPlayInGameTrigger(int VOTriggerID)
	{
		float seconds = 3f;
		yield return new WaitForSeconds(seconds);
		yield return this.HandleMissionEventWithTiming(VOTriggerID);
		yield break;
	}

	// Token: 0x06004CA1 RID: 19617 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004CA2 RID: 19618 RVA: 0x00195877 File Offset: 0x00193A77
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004CA3 RID: 19619 RVA: 0x00195899 File Offset: 0x00193A99
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

	// Token: 0x06004CA4 RID: 19620 RVA: 0x001958BF File Offset: 0x00193ABF
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

	// Token: 0x06004CA5 RID: 19621 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004CA6 RID: 19622 RVA: 0x001958F8 File Offset: 0x00193AF8
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
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
			string line = this.PopRandomLine(this.m_BossIdleLinesCopy, true);
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

	// Token: 0x06004CA7 RID: 19623 RVA: 0x00195908 File Offset: 0x00193B08
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

	// Token: 0x06004CA8 RID: 19624 RVA: 0x00195A8C File Offset: 0x00193C8C
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BoM_01_Rokara_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004CA9 RID: 19625 RVA: 0x00195ADC File Offset: 0x00193CDC
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

	// Token: 0x06004CAA RID: 19626 RVA: 0x00195B48 File Offset: 0x00193D48
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

	// Token: 0x06004CAB RID: 19627 RVA: 0x00195BD8 File Offset: 0x00193DD8
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

	// Token: 0x06004CAC RID: 19628 RVA: 0x00195C68 File Offset: 0x00193E68
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

	// Token: 0x06004CAD RID: 19629 RVA: 0x00195D7C File Offset: 0x00193F7C
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

	// Token: 0x06004CAE RID: 19630 RVA: 0x00195DB8 File Offset: 0x00193FB8
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

	// Token: 0x06004CAF RID: 19631 RVA: 0x00195E0C File Offset: 0x0019400C
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004CB0 RID: 19632 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004CB1 RID: 19633 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004CB2 RID: 19634 RVA: 0x00195E1A File Offset: 0x0019401A
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004CB3 RID: 19635 RVA: 0x00195E22 File Offset: 0x00194022
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
		bool parentBubbleToActor = !(actor.GetCard() != null) || actor.GetCard().GetEntity() == null || !actor.GetCard().GetEntity().IsHeroPower();
		if (shouldPlay() == this.InternalShouldPlayAlways())
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeech(line, speakerDirection, actor, 2.5f, 1f, parentBubbleToActor, false, 0f));
		}
		else if (shouldPlay() == base.InternalShouldPlayOnlyOnce())
		{
			yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndBlockSpeechOnce(line, speakerDirection, actor, 2.5f, 1f, parentBubbleToActor, false, 0f));
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
		}
		yield break;
	}

	// Token: 0x06004CB4 RID: 19636 RVA: 0x00195E47 File Offset: 0x00194047
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004CB5 RID: 19637 RVA: 0x00195E64 File Offset: 0x00194064
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004CB6 RID: 19638 RVA: 0x00195E81 File Offset: 0x00194081
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004CB7 RID: 19639 RVA: 0x00195E9E File Offset: 0x0019409E
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

	// Token: 0x06004CB8 RID: 19640 RVA: 0x00195ED2 File Offset: 0x001940D2
	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		float waitTimeScale = 1f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		yield break;
	}

	// Token: 0x06004CB9 RID: 19641 RVA: 0x00195EE8 File Offset: 0x001940E8
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		bool removeLine = false;
		string line = this.PopRandomLine(lines, removeLine);
		yield return this.MissionPlaySound(actor, line);
		yield break;
	}

	// Token: 0x06004CBA RID: 19642 RVA: 0x00195F05 File Offset: 0x00194105
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004CBB RID: 19643 RVA: 0x00195F22 File Offset: 0x00194122
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004CBC RID: 19644 RVA: 0x00195F3F File Offset: 0x0019413F
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004CBD RID: 19645 RVA: 0x00195F5C File Offset: 0x0019415C
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004CBE RID: 19646 RVA: 0x00195F79 File Offset: 0x00194179
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

	// Token: 0x06004CBF RID: 19647 RVA: 0x00195F9E File Offset: 0x0019419E
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

	// Token: 0x06004CC0 RID: 19648 RVA: 0x00195FD2 File Offset: 0x001941D2
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

	// Token: 0x06004CC1 RID: 19649 RVA: 0x0019600E File Offset: 0x0019420E
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004CC2 RID: 19650 RVA: 0x0019602B File Offset: 0x0019422B
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004CC3 RID: 19651 RVA: 0x00196048 File Offset: 0x00194248
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004CC4 RID: 19652 RVA: 0x00196065 File Offset: 0x00194265
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004CC5 RID: 19653 RVA: 0x00196082 File Offset: 0x00194282
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004CC6 RID: 19654 RVA: 0x0019609F File Offset: 0x0019429F
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004CC7 RID: 19655 RVA: 0x001960BC File Offset: 0x001942BC
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004CC8 RID: 19656 RVA: 0x001960D9 File Offset: 0x001942D9
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004CC9 RID: 19657 RVA: 0x001960F6 File Offset: 0x001942F6
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004CCA RID: 19658 RVA: 0x00196113 File Offset: 0x00194313
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, true);
		yield break;
	}

	// Token: 0x06004CCB RID: 19659 RVA: 0x00196130 File Offset: 0x00194330
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, true);
		yield break;
	}

	// Token: 0x06004CCC RID: 19660 RVA: 0x0019614D File Offset: 0x0019434D
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, true);
		yield break;
	}

	// Token: 0x06004CCD RID: 19661 RVA: 0x0019616A File Offset: 0x0019436A
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004CCE RID: 19662 RVA: 0x00196187 File Offset: 0x00194387
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, true);
		yield break;
	}

	// Token: 0x06004CCF RID: 19663 RVA: 0x001961A4 File Offset: 0x001943A4
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

	// Token: 0x06004CD0 RID: 19664 RVA: 0x001961C8 File Offset: 0x001943C8
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

	// Token: 0x06004CD1 RID: 19665 RVA: 0x001961EC File Offset: 0x001943EC
	public IEnumerator MissionPlaySound(string minionSpeaker, AssetReference brassRing, List<string> lines)
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

	// Token: 0x06004CD2 RID: 19666 RVA: 0x00196210 File Offset: 0x00194410
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

	// Token: 0x06004CD3 RID: 19667 RVA: 0x00196234 File Offset: 0x00194434
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

	// Token: 0x06004CD4 RID: 19668 RVA: 0x00196258 File Offset: 0x00194458
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

	// Token: 0x06004CD5 RID: 19669 RVA: 0x0019627C File Offset: 0x0019447C
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

	// Token: 0x040041E6 RID: 16870
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040041E7 RID: 16871
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x040041E8 RID: 16872
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x040041E9 RID: 16873
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x040041EA RID: 16874
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x040041EB RID: 16875
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x040041EC RID: 16876
	public bool m_Mission_FriendlyPlayIdleLines = true;

	// Token: 0x040041ED RID: 16877
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x040041EE RID: 16878
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x040041EF RID: 16879
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x040041F0 RID: 16880
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x040041F1 RID: 16881
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x040041F2 RID: 16882
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x040041F3 RID: 16883
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x040041F4 RID: 16884
	public List<string> m_BossIdleLines;

	// Token: 0x040041F5 RID: 16885
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x040041F6 RID: 16886
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x040041F7 RID: 16887
	public int m_PlayBossVOLineIndex;

	// Token: 0x040041F8 RID: 16888
	public string m_introLine;

	// Token: 0x040041F9 RID: 16889
	public string m_deathLine;

	// Token: 0x040041FA RID: 16890
	public string m_standardEmoteResponseLine;

	// Token: 0x040041FB RID: 16891
	public bool m_DoEmoteDrivenStart;

	// Token: 0x040041FC RID: 16892
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x040041FD RID: 16893
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x040041FE RID: 16894
	public string m_OverrideBossSubtext;

	// Token: 0x040041FF RID: 16895
	public string m_OverridePlayerSubtext;

	// Token: 0x04004200 RID: 16896
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x04004201 RID: 16897
	private Spell m_enemyBlowUpSpell;

	// Token: 0x04004202 RID: 16898
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x04004203 RID: 16899
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x04004204 RID: 16900
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x04004205 RID: 16901
	public const bool PlayLinesRandomOrder = true;

	// Token: 0x04004206 RID: 16902
	public const bool PlayLinesInOrder = false;

	// Token: 0x04004207 RID: 16903
	private static Map<GameEntityOption, bool> s_booleanOptions = BoM_01_Rokara_MissionEntity.InitBooleanOptions();

	// Token: 0x04004208 RID: 16904
	public const int InGame_BossAttacks = 500;

	// Token: 0x04004209 RID: 16905
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x0400420A RID: 16906
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x0400420B RID: 16907
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x0400420C RID: 16908
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x0400420D RID: 16909
	public const int InGame_BossDeath = 516;

	// Token: 0x0400420E RID: 16910
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x0400420F RID: 16911
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x04004210 RID: 16912
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x04004211 RID: 16913
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x04004212 RID: 16914
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x04004213 RID: 16915
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x04004214 RID: 16916
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x04004215 RID: 16917
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x04004216 RID: 16918
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x04004217 RID: 16919
	public const int InGame_Introduction = 514;

	// Token: 0x04004218 RID: 16920
	public const int InGame_EmoteResponse = 515;

	// Token: 0x04004219 RID: 16921
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x0400421A RID: 16922
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x0400421B RID: 16923
	public const int DisableAutomaticVO = 602;

	// Token: 0x0400421C RID: 16924
	public const int EnableAutomaticVO = 603;

	// Token: 0x0400421D RID: 16925
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x0400421E RID: 16926
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x0400421F RID: 16927
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x04004220 RID: 16928
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x04004221 RID: 16929
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x04004222 RID: 16930
	public const int PlayNextBossLine = 1002;

	// Token: 0x04004223 RID: 16931
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x04004224 RID: 16932
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x04004225 RID: 16933
	public const int PlayAllVOLines = 1011;

	// Token: 0x04004226 RID: 16934
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x04004227 RID: 16935
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x04004228 RID: 16936
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001E16 RID: 7702
	public static class MemberInfoGetting
	{
		// Token: 0x06011089 RID: 69769 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
