using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Hearthstone.Progression;
using UnityEngine;

public abstract class BoH_Malfurion_MissionEntity : GenericDungeonMissionEntity
{
	public static class MemberInfoGetting
	{
		public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
		{
			return ((MemberExpression)memberExpression.Body).Member.Name;
		}
	}

	public bool m_Mission_EnemyHeroShouldExplodeOnDefeat = true;

	public bool m_Mission_EnemyPlayIdleLines = true;

	public bool m_Mission_EnemyPlayIdleLinesInOrder = true;

	public bool m_Mission_EnemyPlayHeroPowerLines;

	public bool m_Mission_EnemyPlayHeroPowerLinesInOrder;

	public bool m_Mission_FriendlyHeroShouldExplodeOnDefeat = true;

	public bool m_Mission_FriendlyPlayIdleLines;

	public bool m_Mission_FriendlylayIdleLinesInOrder = true;

	public bool m_Mission_FriendlyPlayHeroPowerLines;

	public bool m_Mission_FriendlyPlayHeroPowerLinesInOrder;

	public bool m_MissionDisableAutomaticVO;

	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	public List<string> m_BossVOLines = new List<string>();

	public List<string> m_PlayerVOLines = new List<string>();

	public List<string> m_BossIdleLines;

	public List<string> m_BossIdleLinesCopy;

	public int m_PlayPlayerVOLineIndex;

	public int m_PlayBossVOLineIndex;

	public string m_introLine;

	public string m_deathLine;

	public string m_standardEmoteResponseLine;

	public bool m_DoEmoteDrivenStart;

	public MusicPlaylistType m_OverrideMulliganMusicTrack;

	public MusicPlaylistType m_OverrideMusicTrack;

	public string m_OverrideBossSubtext;

	public string m_OverridePlayerSubtext;

	public bool m_SupressEnemyDeathTextBubble;

	private Spell m_enemyBlowUpSpell;

	private Spell m_friendlyBlowUpSpell;

	public const bool ShowSpeechBubbleTrue = true;

	public const bool ShowSpeechBubbleFalse = false;

	public const bool PlayLinesRandomOrder = false;

	public const bool PlayLinesInOrder = true;

	public const int InGame_BossAttacks = 500;

	public const int InGame_BossAttacksSpecial = 501;

	public const int InGame_BossUsesHeroPower = 510;

	public const int InGame_BossUsesHeroPowerSpecial = 511;

	public const int InGame_BossEquipWeapon = 513;

	public const int InGame_PlayerAttacks = 502;

	public const int InGame_PlayerAttacksSpecial = 503;

	public const int InGame_PlayerUsesHeroPower = 508;

	public const int InGame_PlayerUsesHeroPowerSpecial = 509;

	public const int InGame_PlayerEquipWeapon = 512;

	public const int InGame_VictoryPreExplosion = 504;

	public const int InGame_VictoryPostExplosion = 505;

	public const int InGame_LossPreExplosion = 506;

	public const int InGame_LossPostExplosion = 507;

	public const int InGame_Introduction = 514;

	public const int InGame_EmoteResponse = 515;

	public const int TurnOffBossExplodingOnDeath = 600;

	public const int TurnOffPlayerExplodingOnDeath = 601;

	public const int DisableAutomaticVO = 602;

	public const int EnableAutomaticVO = 603;

	public const int TurnOnBossExplodingOnDeath = 610;

	public const int TurnOnPlayerExplodingOnDeath = 611;

	public const int DoEmoteDrivenStart = 612;

	public const int PlayNextPlayerLine = 1000;

	public const int PlayRepeatPlayerLine = 1001;

	public const int PlayNextBossLine = 1002;

	public const int PlayRepeatBossLine = 1003;

	public const int ToggleAlwaysPlayLines = 1010;

	public const int PlayAllVOLines = 1011;

	public const int PlayAllBossVOLines = 1012;

	public const int PlayAllPlayerVOLines = 1013;

	public const int HearthStoneUsed = 58023;

	public override void PreloadAssets()
	{
		base.PreloadAssets();
		List<string> list = new List<string>();
		m_PlayerVOLines = new List<string>(list);
		foreach (string item in list)
		{
			PreloadSound(item);
		}
	}

	public virtual List<string> GetBossIdleLines()
	{
		return new List<string>();
	}

	public virtual float GetThinkEmoteBossIdleChancePercentage()
	{
		return 0.25f;
	}

	public virtual float GetThinkIdleChancePercentage()
	{
		return 0.25f;
	}

	public virtual List<string> GetBossHeroPowerRandomLines()
	{
		return new List<string>();
	}

	protected virtual float ChanceToPlayBossHeroPowerVOLine()
	{
		return 1f;
	}

	public void MissionPause(bool pause)
	{
		m_MissionDisableAutomaticVO = pause;
		GameState.Get().SetBusy(pause);
	}

	protected virtual IEnumerator OnBossHeroPowerPlayed(Entity entity)
	{
		float num = ChanceToPlayBossHeroPowerVOLine();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (m_enemySpeaking || m_MissionDisableAutomaticVO || num < num2)
		{
			yield break;
		}
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCard()
			.GetActor();
		if (actor == null)
		{
			yield break;
		}
		List<string> bossHeroPowerRandomLines = GetBossHeroPowerRandomLines();
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
		if (!(text == ""))
		{
			yield return MissionPlayVO(actor, text);
		}
	}

	protected override IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		if (!m_MissionDisableAutomaticVO && !m_enemySpeaking && entity.GetCardType() != 0 && entity.GetCardType() == TAG_CARDTYPE.HERO_POWER && entity.GetControllerSide() == Player.Side.OPPOSING)
		{
			yield return OnBossHeroPowerPlayed(entity);
		}
	}

	protected override void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (MissionEntity.STANDARD_EMOTE_RESPONSE_TRIGGERS.Contains(emoteType) && !m_enemySpeaking)
		{
			GameEntity.Coroutines.StartCoroutine(HandleMissionEventWithTiming(515));
		}
	}

	protected override IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		switch (gameResult)
		{
		case TAG_PLAYSTATE.WON:
			GameState.Get().SetBusy(busy: true);
			GameState.Get().SetBusy(busy: false);
			break;
		case TAG_PLAYSTATE.LOST:
			GameState.Get().SetBusy(busy: true);
			GameState.Get().SetBusy(busy: false);
			break;
		}
		yield break;
	}

	public override bool ShouldShowHeroClassDuringMulligan(Player.Side playerSide)
	{
		return false;
	}

	public override void StartGameplaySoundtracks()
	{
		if (m_OverrideMusicTrack == MusicPlaylistType.Invalid)
		{
			base.StartGameplaySoundtracks();
		}
		else
		{
			MusicManager.Get().StartPlaylist(m_OverrideMusicTrack);
		}
	}

	public override void StartMulliganSoundtracks(bool soft)
	{
		if (!soft)
		{
			if (m_OverrideMulliganMusicTrack == MusicPlaylistType.Invalid)
			{
				base.StartMulliganSoundtracks(soft);
			}
			else
			{
				MusicManager.Get().StartPlaylist(m_OverrideMulliganMusicTrack);
			}
		}
	}

	public override string GetNameBannerSubtextOverride(Player.Side playerSide)
	{
		if (playerSide == Player.Side.OPPOSING && m_OverrideBossSubtext != null)
		{
			return GameStrings.Get(m_OverrideBossSubtext);
		}
		if (playerSide == Player.Side.FRIENDLY && m_OverridePlayerSubtext != null)
		{
			return GameStrings.Get(m_OverridePlayerSubtext);
		}
		return base.GetNameBannerSubtextOverride(playerSide);
	}

	public override void OnPlayThinkEmote()
	{
		Gameplay.Get().StartCoroutine(OnPlayThinkEmoteWithTiming());
	}

	public override IEnumerator OnPlayThinkEmoteWithTiming()
	{
		if (m_enemySpeaking)
		{
			yield break;
		}
		Player currentPlayer = GameState.Get().GetCurrentPlayer();
		if (!currentPlayer.IsFriendlySide() || currentPlayer.GetHeroCard().HasActiveEmoteSound())
		{
			yield break;
		}
		GameState.Get().GetOpposingSidePlayer().GetHero()
			.GetCardId();
		if (!m_Mission_FriendlyPlayIdleLines && !m_Mission_EnemyPlayIdleLines)
		{
			yield break;
		}
		float thinkIdleChancePercentage = GetThinkIdleChancePercentage();
		float num = UnityEngine.Random.Range(0f, 1f);
		if (thinkIdleChancePercentage < num)
		{
			yield break;
		}
		float thinkEmoteBossIdleChancePercentage = GetThinkEmoteBossIdleChancePercentage();
		float num2 = UnityEngine.Random.Range(0f, 1f);
		if (thinkEmoteBossIdleChancePercentage < num2 || (!m_Mission_FriendlyPlayIdleLines && m_Mission_EnemyPlayIdleLines))
		{
			Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
				.GetActor();
			string line = PopRandomLine(m_BossIdleLinesCopy);
			if (m_BossIdleLinesCopy.Count == 0)
			{
				m_BossIdleLinesCopy = new List<string>(m_BossIdleLines);
			}
			yield return MissionPlayVO(actor, line);
		}
		else if (m_Mission_FriendlyPlayIdleLines)
		{
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
			CardSoundSpell cardSoundSpell = GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
			AudioSource activeAudioSource = cardSoundSpell.GetActiveAudioSource();
			yield return GameState.Get().GetCurrentPlayer().GetHeroCard()
				.PlayEmote(emoteType);
			yield return new WaitForSeconds(activeAudioSource.clip.length);
		}
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE playState)
	{
		PegCursor.Get().SetMode(PegCursor.Mode.STOPWAITING);
		MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_EndGameScreen);
		Card heroCard = GameState.Get().GetOpposingSidePlayer().GetHeroCard();
		Card heroCard2 = GameState.Get().GetFriendlySidePlayer().GetHeroCard();
		Gameplay.Get().SaveOriginalTimeScale();
		AchievementManager.Get()?.PauseToastNotifications();
		if (ShouldPlayHeroBlowUpSpells(playState))
		{
			switch (playState)
			{
			case TAG_PLAYSTATE.WON:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.VICTORY_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (m_Mission_EnemyHeroShouldExplodeOnDefeat)
				{
					m_enemyBlowUpSpell = BlowUpHero(heroCard, SpellType.ENDGAME_WIN);
				}
				break;
			}
			case TAG_PLAYSTATE.LOST:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (m_Mission_FriendlyHeroShouldExplodeOnDefeat)
				{
					m_friendlyBlowUpSpell = BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				}
				break;
			}
			case TAG_PLAYSTATE.TIED:
			{
				string stringOption = GetGameOptions().GetStringOption(GameEntityOption.DEFEAT_AUDIO_PATH);
				if (!string.IsNullOrEmpty(stringOption))
				{
					SoundManager.Get().LoadAndPlay(stringOption);
				}
				if (m_Mission_EnemyHeroShouldExplodeOnDefeat)
				{
					m_enemyBlowUpSpell = BlowUpHero(heroCard, SpellType.ENDGAME_DRAW);
				}
				if (m_Mission_FriendlyHeroShouldExplodeOnDefeat)
				{
					m_friendlyBlowUpSpell = BlowUpHero(heroCard2, SpellType.ENDGAME_LOSE);
				}
				break;
			}
			}
		}
		ShowEndGameScreen(playState, m_enemyBlowUpSpell, m_friendlyBlowUpSpell);
		GameEntity.Coroutines.StartCoroutine(HandleGameOverWithTiming(playState));
	}

	public static bool GetIsFirstBoss()
	{
		int @int = Options.Get().GetInt(Option.SELECTED_ADVENTURE);
		AdventureModeDbId int2 = (AdventureModeDbId)Options.Get().GetInt(Option.SELECTED_ADVENTURE_MODE);
		AdventureDataDbfRecord adventureDataRecord = GetAdventureDataRecord(@int, (int)int2);
		if (adventureDataRecord == null)
		{
			return true;
		}
		GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
		GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_IS_RUN_ACTIVE, out long value);
		if (value == 0L)
		{
			return true;
		}
		return false;
	}

	public static AdventureDataDbfRecord GetAdventureDataRecord(int adventureId, int modeId)
	{
		foreach (AdventureDataDbfRecord record in GameDbf.AdventureData.GetRecords())
		{
			if (record.AdventureId == adventureId && record.ModeId == modeId)
			{
				return record;
			}
		}
		return null;
	}

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

	protected string PopNextLine(List<string> lines, bool removeLine = true)
	{
		string text = null;
		for (int i = 0; i < lines.Count; i++)
		{
			if (!m_InOrderPlayedLines.Contains(lines[i]))
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
			m_InOrderPlayedLines.Add(text);
		}
		return text;
	}

	public void SetBossVOLines(List<string> VOLines)
	{
		m_BossVOLines = new List<string>(VOLines);
	}

	protected ShouldPlayValue InternalShouldPlayAlways()
	{
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayOnce()
	{
		return ShouldPlayValue.Once;
	}

	protected virtual bool GetShouldSuppressDeathTextBubble()
	{
		return m_SupressEnemyDeathTextBubble;
	}

	protected IEnumerator MissionPlayVO(Actor actor, string line, bool bUseBubble, ShouldPlay shouldPlay)
	{
		Notification.SpeechBubbleDirection speakerDirection = GetDirection(actor);
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayLine(actor, line, shouldPlay, 2.5f));
		}
		if (shouldPlay() == InternalShouldPlayAlways())
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, speakerDirection, actor, 2.5f));
		}
		else if (shouldPlay() == InternalShouldPlayOnlyOnce())
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeechOnce(line, speakerDirection, actor, 2.5f));
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
		}
	}

	public IEnumerator MissionPlayVOSound(Actor actor, string line)
	{
		yield return MissionPlayVO(actor, line, bUseBubble: false, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVO(Actor actor, string line)
	{
		yield return MissionPlayVO(actor, line, bUseBubble: true, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnce(Actor actor, string line)
	{
		yield return MissionPlayVO(actor, line, bUseBubble: true, InternalShouldPlayOnce);
	}

	protected IEnumerator MissionPlayVO(Actor speaker, List<string> lines, ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
	{
		bool removeLine = false;
		if (shouldPlay() == ShouldPlayValue.Once && !m_forceAlwaysPlayLine)
		{
			removeLine = true;
		}
		string text = ((!bPlayOrder) ? PopNextLine(lines, removeLine) : PopRandomLine(lines, removeLine));
		if (text != null)
		{
			yield return MissionPlayVO(speaker, text, bUseBubble, shouldPlay);
		}
	}

	public IEnumerator MissionPlaySound(Actor actor, string line)
	{
		float waitTimeScale = 0f;
		bool parentBubbleToActor = true;
		bool delayCardSoundSpells = false;
		yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(line, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells));
	}

	public IEnumerator MissionPlaySound(Actor actor, List<string> lines)
	{
		bool removeLine = false;
		string line = PopRandomLine(lines, removeLine);
		yield return MissionPlaySound(actor, line);
	}

	public IEnumerator MissionPlayVO(Actor actor, List<string> lines)
	{
		yield return MissionPlayVO(actor, lines, InternalShouldPlayAlways, bUseBubble: false);
	}

	public IEnumerator MissionPlayVOOnce(Actor actor, List<string> lines)
	{
		yield return MissionPlayVO(actor, lines, base.InternalShouldPlayOnlyOnce, bUseBubble: false);
	}

	public IEnumerator MissionPlayVOInOrder(Actor actor, List<string> lines)
	{
		yield return MissionPlayVO(actor, lines, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnceInOrder(Actor actor, List<string> lines)
	{
		yield return MissionPlayVO(actor, lines, base.InternalShouldPlayOnlyOnce);
	}

	protected IEnumerator MissionPlayVO(string brassRing, string line, bool bUseBubble, ShouldPlay shouldPlay)
	{
		if (m_enemySpeaking)
		{
			yield return null;
		}
		m_enemySpeaking = true;
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(brassRing, line));
		}
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(brassRing, line));
		}
		else if (shouldPlay() == ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWaitOnce(brassRing, line));
			NotificationManager.Get().ForceAddSoundToPlayedList(line);
		}
		m_enemySpeaking = false;
	}

	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
	{
		bool removeLine = false;
		if (shouldPlay() == ShouldPlayValue.Once && !m_forceAlwaysPlayLine)
		{
			removeLine = true;
		}
		yield return MissionPlayVO(line: (!bPlayOrder) ? PopNextLine(lines, removeLine) : PopRandomLine(lines, removeLine), brassRing: brassRing, bUseBubble: bUseBubble, shouldPlay: shouldPlay);
	}

	protected IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines, Actor minionOverride, ShouldPlay shouldPlay, bool bUseBubble = true, bool bPlayOrder = false)
	{
		if (minionOverride == null)
		{
			yield return MissionPlayVO(brassRing, lines, shouldPlay, bUseBubble, bPlayOrder);
		}
		else
		{
			yield return MissionPlayVO(minionOverride, lines, shouldPlay, bUseBubble, bPlayOrder);
		}
	}

	public IEnumerator MissionPlayVO(AssetReference brassRing, string line)
	{
		yield return MissionPlayVO(brassRing, line, bUseBubble: true, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, string line)
	{
		yield return MissionPlayVO(brassRing, line, bUseBubble: true, InternalShouldPlayOnce);
	}

	public IEnumerator MissionPlaySound(AssetReference brassRing, List<string> lines)
	{
		yield return MissionPlayVO(brassRing, lines, InternalShouldPlayAlways, bUseBubble: false);
	}

	public IEnumerator MissionPlayVO(AssetReference brassRing, List<string> lines)
	{
		yield return MissionPlayVO(brassRing, lines, InternalShouldPlayAlways, bUseBubble: false);
	}

	public IEnumerator MissionPlayVOOnce(AssetReference brassRing, List<string> lines)
	{
		yield return MissionPlayVO(brassRing, lines, base.InternalShouldPlayOnlyOnce, bUseBubble: false);
	}

	public IEnumerator MissionPlayVOInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return MissionPlayVO(brassRing, lines, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnceInOrder(AssetReference brassRing, List<string> lines)
	{
		yield return MissionPlayVO(brassRing, lines, base.InternalShouldPlayOnlyOnce);
	}

	public IEnumerator MissionPlayVOInOrder(string minionSpeaker, List<string> lines)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), lines, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnce(string minionSpeaker, string line)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), line, bUseBubble: true, InternalShouldPlayOnce);
	}

	public IEnumerator MissionPlaySound(string minionSpeaker, List<string> lines)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), lines, InternalShouldPlayAlways, bUseBubble: false);
	}

	public IEnumerator MissionPlayVO(string minionSpeaker, List<string> lines)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), lines, InternalShouldPlayAlways, bUseBubble: false);
	}

	public IEnumerator MissionPlayVOOnce(string minionSpeaker, List<string> lines)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), lines, base.InternalShouldPlayOnlyOnce, bUseBubble: false);
	}

	public IEnumerator MissionPlayVO(string minionSpeaker, string line)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), line, bUseBubble: true, InternalShouldPlayAlways);
	}

	public IEnumerator MissionPlayVOOnceInOrder(string minionSpeaker, List<string> lines)
	{
		yield return MissionPlayVO(GetActorByCardId(minionSpeaker), lines, base.InternalShouldPlayOnlyOnce);
	}
}
