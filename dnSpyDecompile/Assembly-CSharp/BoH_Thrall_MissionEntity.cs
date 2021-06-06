using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

// Token: 0x02000549 RID: 1353
public abstract class BoH_Thrall_MissionEntity : GenericDungeonMissionEntity
{
	// Token: 0x06004A49 RID: 19017 RVA: 0x0018B7F0 File Offset: 0x001899F0
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

	// Token: 0x06004A4A RID: 19018 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	// Token: 0x06004A4B RID: 19019 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004A4C RID: 19020 RVA: 0x0012CC98 File Offset: 0x0012AE98
	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	// Token: 0x06004A4D RID: 19021 RVA: 0x00112BA2 File Offset: 0x00110DA2
	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	// Token: 0x06004A4E RID: 19022 RVA: 0x000C3B6A File Offset: 0x000C1D6A
	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	// Token: 0x06004A4F RID: 19023 RVA: 0x0018B858 File Offset: 0x00189A58
	public void MissionPause(bool pause)
	{
		this.m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	// Token: 0x06004A50 RID: 19024 RVA: 0x0018B86C File Offset: 0x00189A6C
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

	// Token: 0x06004A51 RID: 19025 RVA: 0x0018B87B File Offset: 0x00189A7B
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

	// Token: 0x06004A52 RID: 19026 RVA: 0x0017C055 File Offset: 0x0017A255
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

	// Token: 0x06004A53 RID: 19027 RVA: 0x0018B891 File Offset: 0x00189A91
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

	// Token: 0x06004A54 RID: 19028 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	// Token: 0x06004A55 RID: 19029 RVA: 0x0018B8A0 File Offset: 0x00189AA0
	public override void StartGameplaySoundtracks()
	{
		if (this.m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
			return;
		}
		MusicManager.Get().StartPlaylist(this.m_OverrideMusicTrack);
	}

	// Token: 0x06004A56 RID: 19030 RVA: 0x0018B8C2 File Offset: 0x00189AC2
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

	// Token: 0x06004A57 RID: 19031 RVA: 0x0018B8E8 File Offset: 0x00189AE8
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

	// Token: 0x06004A58 RID: 19032 RVA: 0x0017C129 File Offset: 0x0017A329
	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(this.OnPlayThinkEmoteWithTiming());
	}

	// Token: 0x06004A59 RID: 19033 RVA: 0x0018B921 File Offset: 0x00189B21
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

	// Token: 0x06004A5A RID: 19034 RVA: 0x0018B930 File Offset: 0x00189B30
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

	// Token: 0x06004A5B RID: 19035 RVA: 0x0018BAB4 File Offset: 0x00189CB4
	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = BoH_Thrall_MissionEntity.GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out num);
		return num == 0L;
	}

	// Token: 0x06004A5C RID: 19036 RVA: 0x0018BB04 File Offset: 0x00189D04
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

	// Token: 0x06004A5D RID: 19037 RVA: 0x0018BB70 File Offset: 0x00189D70
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

	// Token: 0x06004A5E RID: 19038 RVA: 0x0018BC00 File Offset: 0x00189E00
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

	// Token: 0x06004A5F RID: 19039 RVA: 0x0018BC90 File Offset: 0x00189E90
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

	// Token: 0x06004A60 RID: 19040 RVA: 0x0018BDA4 File Offset: 0x00189FA4
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

	// Token: 0x06004A61 RID: 19041 RVA: 0x0018BDE0 File Offset: 0x00189FE0
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

	// Token: 0x06004A62 RID: 19042 RVA: 0x0018BE34 File Offset: 0x0018A034
	public void SetBossVOLines(List<string> VOLines)
	{
		this.m_BossVOLines = new List<string>(VOLines);
	}

	// Token: 0x06004A63 RID: 19043 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAlways()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004A64 RID: 19044 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004A65 RID: 19045 RVA: 0x0018BE42 File Offset: 0x0018A042
	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return this.m_SupressEnemyDeathTextBubble;
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x0018BE4A File Offset: 0x0018A04A
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

	// Token: 0x06004A67 RID: 19047 RVA: 0x0018BE6F File Offset: 0x0018A06F
	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, false, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004A68 RID: 19048 RVA: 0x0018BE8C File Offset: 0x0018A08C
	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004A69 RID: 19049 RVA: 0x0018BEA9 File Offset: 0x0018A0A9
	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return this.MissionPlayVO(actor, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004A6A RID: 19050 RVA: 0x0018BEC6 File Offset: 0x0018A0C6
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

	// Token: 0x06004A6B RID: 19051 RVA: 0x0018BEFA File Offset: 0x0018A0FA
	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		float waitTimeScale = 0f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(base.PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		yield break;
	}

	// Token: 0x06004A6C RID: 19052 RVA: 0x0018BF10 File Offset: 0x0018A110
	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		bool removeLine = false;
		string line = this.PopRandomLine(lines, removeLine);
		yield return this.MissionPlaySound(actor, line);
		yield break;
	}

	// Token: 0x06004A6D RID: 19053 RVA: 0x0018BF2D File Offset: 0x0018A12D
	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004A6E RID: 19054 RVA: 0x0018BF4A File Offset: 0x0018A14A
	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004A6F RID: 19055 RVA: 0x0018BF67 File Offset: 0x0018A167
	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004A70 RID: 19056 RVA: 0x0018BF84 File Offset: 0x0018A184
	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return this.MissionPlayVO(actor, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x06004A71 RID: 19057 RVA: 0x0018BFA1 File Offset: 0x0018A1A1
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

	// Token: 0x06004A72 RID: 19058 RVA: 0x0018BFC6 File Offset: 0x0018A1C6
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

	// Token: 0x06004A73 RID: 19059 RVA: 0x0018BFFA File Offset: 0x0018A1FA
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

	// Token: 0x06004A74 RID: 19060 RVA: 0x0018C036 File Offset: 0x0018A236
	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004A75 RID: 19061 RVA: 0x0018C053 File Offset: 0x0018A253
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return this.MissionPlayVO(brassRing, line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004A76 RID: 19062 RVA: 0x0018C070 File Offset: 0x0018A270
	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004A77 RID: 19063 RVA: 0x0018C08D File Offset: 0x0018A28D
	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004A78 RID: 19064 RVA: 0x0018C0AA File Offset: 0x0018A2AA
	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004A79 RID: 19065 RVA: 0x0018C0C7 File Offset: 0x0018A2C7
	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004A7A RID: 19066 RVA: 0x0018C0E4 File Offset: 0x0018A2E4
	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return this.MissionPlayVO(brassRing, lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x06004A7B RID: 19067 RVA: 0x0018C101 File Offset: 0x0018A301
	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), true, false);
		yield break;
	}

	// Token: 0x06004A7C RID: 19068 RVA: 0x0018C11E File Offset: 0x0018A31E
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnce));
		yield break;
	}

	// Token: 0x06004A7D RID: 19069 RVA: 0x0018C13B File Offset: 0x0018A33B
	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004A7E RID: 19070 RVA: 0x0018C158 File Offset: 0x0018A358
	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways), false, false);
		yield break;
	}

	// Token: 0x06004A7F RID: 19071 RVA: 0x0018C175 File Offset: 0x0018A375
	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), false, false);
		yield break;
	}

	// Token: 0x06004A80 RID: 19072 RVA: 0x0018C192 File Offset: 0x0018A392
	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), line, true, new MissionEntity.ShouldPlay(this.InternalShouldPlayAlways));
		yield break;
	}

	// Token: 0x06004A81 RID: 19073 RVA: 0x0018C1AF File Offset: 0x0018A3AF
	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return this.MissionPlayVO(this.GetActorByCardId(minionSpeaker), lines, new MissionEntity.ShouldPlay(base.InternalShouldPlayOnlyOnce), true, false);
		yield break;
	}

	// Token: 0x04003E92 RID: 16018
	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04003E93 RID: 16019
	public bool m_Mission_EnemyPlayIdleLines = true;

	// Token: 0x04003E94 RID: 16020
	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	// Token: 0x04003E95 RID: 16021
	public bool m_Mission_EnemyPlayHeroPowerLines;

	// Token: 0x04003E96 RID: 16022
	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	// Token: 0x04003E97 RID: 16023
	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	// Token: 0x04003E98 RID: 16024
	public bool m_Mission_FriendlyPlayIdleLines;

	// Token: 0x04003E99 RID: 16025
	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	// Token: 0x04003E9A RID: 16026
	public bool m_Mission_FriendlyPlayHeroPowerLines;

	// Token: 0x04003E9B RID: 16027
	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	// Token: 0x04003E9C RID: 16028
	public bool m_MissionDisableAutomaticVO;

	// Token: 0x04003E9D RID: 16029
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x04003E9E RID: 16030
	public List<string> m_BossVOLines = new List<string>();

	// Token: 0x04003E9F RID: 16031
	public List<string> m_PlayerVOLines = new List<string>();

	// Token: 0x04003EA0 RID: 16032
	public List<string> m_BossIdleLines;

	// Token: 0x04003EA1 RID: 16033
	public List<string> m_BossIdleLinesCopy;

	// Token: 0x04003EA2 RID: 16034
	public int m_PlayPlayerVOLineIndex;

	// Token: 0x04003EA3 RID: 16035
	public int m_PlayBossVOLineIndex;

	// Token: 0x04003EA4 RID: 16036
	public string m_introLine;

	// Token: 0x04003EA5 RID: 16037
	public string m_deathLine;

	// Token: 0x04003EA6 RID: 16038
	public string m_standardEmoteResponseLine;

	// Token: 0x04003EA7 RID: 16039
	public bool m_DoEmoteDrivenStart;

	// Token: 0x04003EA8 RID: 16040
	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	// Token: 0x04003EA9 RID: 16041
	public MusicPlaylistType m_OverrideMusicTrack;

	// Token: 0x04003EAA RID: 16042
	public string m_OverrideBossSubtext;

	// Token: 0x04003EAB RID: 16043
	public string m_OverridePlayerSubtext;

	// Token: 0x04003EAC RID: 16044
	public bool m_SupressEnemyDeathTextBubble;

	// Token: 0x04003EAD RID: 16045
	private Spell m_enemyBlowUpSpell;

	// Token: 0x04003EAE RID: 16046
	private Spell m_friendlyBlowUpSpell;

	// Token: 0x04003EAF RID: 16047
	public const bool ShowSpeechBubbleTrue = true;

	// Token: 0x04003EB0 RID: 16048
	public const bool ShowSpeechBubbleFalse = false;

	// Token: 0x04003EB1 RID: 16049
	public const bool PlayLinesRandomOrder = false;

	// Token: 0x04003EB2 RID: 16050
	public const bool PlayLinesInOrder = true;

	// Token: 0x04003EB3 RID: 16051
	public const int InGame_BossAttacks = 500;

	// Token: 0x04003EB4 RID: 16052
	public const int InGame_BossAttacksSpecial = 501;

	// Token: 0x04003EB5 RID: 16053
	public const int InGame_BossUsesHeroPower = 510;

	// Token: 0x04003EB6 RID: 16054
	public const int InGame_BossUsesHeroPowerSpecial = 511;

	// Token: 0x04003EB7 RID: 16055
	public const int InGame_BossEquipWeapon = 513;

	// Token: 0x04003EB8 RID: 16056
	public const int InGame_PlayerAttacks = 502;

	// Token: 0x04003EB9 RID: 16057
	public const int InGame_PlayerAttacksSpecial = 503;

	// Token: 0x04003EBA RID: 16058
	public const int InGame_PlayerUsesHeroPower = 508;

	// Token: 0x04003EBB RID: 16059
	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	// Token: 0x04003EBC RID: 16060
	public const int InGame_PlayerEquipWeapon = 512;

	// Token: 0x04003EBD RID: 16061
	public const int InGame_VictoryPreExplosion = 504;

	// Token: 0x04003EBE RID: 16062
	public const int InGame_VictoryPostExplosion = 505;

	// Token: 0x04003EBF RID: 16063
	public const int InGame_LossPreExplosion = 506;

	// Token: 0x04003EC0 RID: 16064
	public const int InGame_LossPostExplosion = 507;

	// Token: 0x04003EC1 RID: 16065
	public const int InGame_Introduction = 514;

	// Token: 0x04003EC2 RID: 16066
	public const int InGame_EmoteResponse = 515;

	// Token: 0x04003EC3 RID: 16067
	public const int TurnOffBossExplodingOnDeath = 600;

	// Token: 0x04003EC4 RID: 16068
	public const int TurnOffPlayerExplodingOnDeath = 601;

	// Token: 0x04003EC5 RID: 16069
	public const int DisableAutomaticVO = 602;

	// Token: 0x04003EC6 RID: 16070
	public const int EnableAutomaticVO = 603;

	// Token: 0x04003EC7 RID: 16071
	public const int TurnOnBossExplodingOnDeath = 610;

	// Token: 0x04003EC8 RID: 16072
	public const int TurnOnPlayerExplodingOnDeath = 611;

	// Token: 0x04003EC9 RID: 16073
	public const int DoEmoteDrivenStart = 612;

	// Token: 0x04003ECA RID: 16074
	public const int PlayNextPlayerLine = 1000;

	// Token: 0x04003ECB RID: 16075
	public const int PlayRepeatPlayerLine = 1001;

	// Token: 0x04003ECC RID: 16076
	public const int PlayNextBossLine = 1002;

	// Token: 0x04003ECD RID: 16077
	public const int PlayRepeatBossLine = 1003;

	// Token: 0x04003ECE RID: 16078
	public const int ToggleAlwaysPlayLines = 1010;

	// Token: 0x04003ECF RID: 16079
	public const int PlayAllVOLines = 1011;

	// Token: 0x04003ED0 RID: 16080
	public const int PlayAllBossVOLines = 1012;

	// Token: 0x04003ED1 RID: 16081
	public const int PlayAllPlayerVOLines = 1013;

	// Token: 0x04003ED2 RID: 16082
	public const int HearthStoneUsed = 58023;

	// Token: 0x02001D51 RID: 7505
	public static class MemberInfoGetting
	{
		// Token: 0x06010BF2 RID: 68594 RVA: 0x00481530 File Offset: 0x0047F730
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}
}
