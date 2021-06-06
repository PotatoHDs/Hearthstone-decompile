using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEntity : GameEntity
{
	protected class EmoteResponse
	{
		public string m_soundName;

		public string m_stringTag;
	}

	protected class EmoteResponseGroup
	{
		public List<EmoteType> m_triggers = new List<EmoteType>();

		public List<EmoteResponse> m_responses = new List<EmoteResponse>();

		public int m_responseIndex;
	}

	protected enum ShouldPlayValue
	{
		Never,
		Once,
		Always
	}

	protected delegate ShouldPlayValue ShouldPlay();

	private static Map<GameEntityOption, bool> s_booleanOptions = InitBooleanOptions();

	private static Map<GameEntityOption, string> s_stringOptions = InitStringOptions();

	protected const float TIME_TO_WAIT_BEFORE_ENDING_QUOTE = 5f;

	protected const float MINIMUM_DISPLAY_TIME_FOR_BIG_QUOTE = 3f;

	protected const float DEFAULT_VO_DURATION = 2.5f;

	protected static readonly List<EmoteType> STANDARD_EMOTE_RESPONSE_TRIGGERS = new List<EmoteType>
	{
		EmoteType.GREETINGS,
		EmoteType.WELL_PLAYED,
		EmoteType.OOPS,
		EmoteType.SORRY,
		EmoteType.THANKS,
		EmoteType.THREATEN,
		EmoteType.WOW,
		EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_ONE,
		EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_TWO,
		EmoteType.FIRE_FESTIVAL_FIREWORKS_RANK_THREE,
		EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_ONE,
		EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_TWO,
		EmoteType.FROST_FESTIVAL_FIREWORKS_RANK_THREE,
		EmoteType.HAPPY_HALLOWEEN,
		EmoteType.HAPPY_NEW_YEAR
	};

	protected bool m_enemySpeaking;

	protected List<EmoteResponseGroup> m_emoteResponseGroups = new List<EmoteResponseGroup>();

	public bool m_forceAlwaysPlayLine;

	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool> { 
		{
			GameEntityOption.USE_SECRET_CLASS_NAMES,
			true
		} };
	}

	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	public MissionEntity()
	{
		m_gameOptions.AddOptions(s_booleanOptions, s_stringOptions);
		InitEmoteResponses();
	}

	public override void OnTagChanged(TagDelta change)
	{
		switch (change.tag)
		{
		case 198:
			if (change.newValue == 6)
			{
				HandleMainReadyStep();
			}
			else if (change.oldValue == 9 && change.newValue == 10 && GameState.Get().IsLocalSidePlayerTurn())
			{
				TurnStartManager.Get().BeginPlayingTurnEvents();
			}
			break;
		case 19:
			if (change.newValue == 4)
			{
				HandleMulliganTagChange();
			}
			else if (change.oldValue == 9 && change.newValue == 10 && !GameState.Get().IsFriendlySidePlayerTurn())
			{
				HandleStartOfTurn(GetTag(GAME_TAG.TURN));
			}
			break;
		case 6:
			HandleMissionEvent(change.newValue);
			break;
		}
		base.OnTagChanged(change);
	}

	public override void NotifyOfStartOfTurnEventsFinished()
	{
		HandleStartOfTurn(GetTag(GAME_TAG.TURN));
	}

	public override void SendCustomEvent(int eventID)
	{
		HandleMissionEvent(eventID);
	}

	public override void NotifyOfOpponentWillPlayCard(string cardId)
	{
		base.NotifyOfOpponentWillPlayCard(cardId);
		GameEntity.Coroutines.StartCoroutine(RespondToWillPlayCardWithTiming(cardId));
	}

	public override void NotifyOfOpponentPlayedCard(Entity entity)
	{
		base.NotifyOfOpponentPlayedCard(entity);
		GameEntity.Coroutines.StartCoroutine(RespondToPlayedCardWithTiming(entity));
	}

	public override void NotifyOfFriendlyPlayedCard(Entity entity)
	{
		base.NotifyOfFriendlyPlayedCard(entity);
		GameEntity.Coroutines.StartCoroutine(RespondToFriendlyPlayedCardWithTiming(entity));
	}

	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		GameEntity.Coroutines.StartCoroutine(HandleGameOverWithTiming(gameResult));
	}

	public override void NotifyOfResetGameStarted()
	{
		base.NotifyOfResetGameStarted();
		GameEntity.Coroutines.StopAllCoroutines();
	}

	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		base.NotifyOfResetGameFinished(source, oldGameEntity);
		GameEntity.Coroutines.StartCoroutine(RespondToResetGameFinishedWithTiming(source));
	}

	public override void OnEmotePlayed(Card card, EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (card.GetEntity().IsControlledByFriendlySidePlayer())
		{
			GameEntity.Coroutines.StartCoroutine(HandlePlayerEmoteWithTiming(emoteType, emoteSpell));
		}
	}

	public override bool DoAlternateMulliganIntro()
	{
		if (!ShouldDoAlternateMulliganIntro())
		{
			return false;
		}
		GameEntity.Coroutines.StartCoroutine(SkipStandardMulliganWithTiming());
		return true;
	}

	public bool IsHeroic()
	{
		return GameMgr.Get().IsHeroicMission();
	}

	public bool IsClassChallenge()
	{
		return GameMgr.Get().IsClassChallengeMission();
	}

	protected virtual void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("MissionEntity.HandleMainReadyStep(): GameState is null.");
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("MissionEntity.HandleMainReadyStep(): GameEntity is null.");
		}
		else
		{
			if (gameEntity.GetTag(GAME_TAG.TURN) != 1)
			{
				return;
			}
			if (GameState.Get().IsMulliganManagerActive())
			{
				GameState.Get().SetMulliganBusy(busy: true);
			}
			else if (!ShouldDoAlternateMulliganIntro())
			{
				GameState.Get().SetMulliganBusy(busy: true);
				if (MulliganManager.Get() != null)
				{
					MulliganManager.Get().SkipMulligan();
				}
			}
		}
	}

	public void SetBlockVo(bool shouldBlock, float unblockAfterSeconds = 0f)
	{
		if (unblockAfterSeconds < 0f)
		{
			unblockAfterSeconds = 0f;
		}
		if (!shouldBlock)
		{
			m_enemySpeaking = shouldBlock;
		}
		if (shouldBlock)
		{
			GameEntity.Coroutines.StartCoroutine(UnblockSpeechAgainAfterDuration(unblockAfterSeconds));
		}
	}

	private IEnumerator UnblockSpeechAgainAfterDuration(float durationInSeconds)
	{
		if (durationInSeconds <= 0f)
		{
			m_enemySpeaking = false;
			yield break;
		}
		while (m_enemySpeaking)
		{
			yield return null;
		}
		m_enemySpeaking = true;
		yield return new WaitForSeconds(durationInSeconds);
		m_enemySpeaking = false;
	}

	public bool IsVoBlocked()
	{
		return m_enemySpeaking;
	}

	protected virtual void HandleMulliganTagChange()
	{
		MulliganManager.Get().BeginMulligan();
	}

	protected void HandleStartOfTurn(int turn)
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.IS_CURRENT_TURN_AN_EXTRA_TURN) == 0)
		{
			int turn2 = turn - GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
			GameEntity.Coroutines.StartCoroutine(HandleStartOfTurnWithTiming(turn2));
		}
	}

	protected virtual IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		yield break;
	}

	protected void HandleMissionEvent(int missionEvent)
	{
		GameEntity.Coroutines.StartCoroutine(HandleMissionEventWithTiming(missionEvent));
	}

	protected virtual IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	protected virtual IEnumerator RespondToWillPlayCardWithTiming(string cardId)
	{
		yield break;
	}

	protected virtual IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield break;
	}

	protected virtual IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield break;
	}

	protected virtual IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield break;
	}

	protected virtual IEnumerator RespondToResetGameFinishedWithTiming(Entity source)
	{
		yield break;
	}

	protected void PlaySound(string soundPath, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells));
	}

	protected IEnumerator PlaySoundAndBlockSpeech(string soundPath, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		m_enemySpeaking = true;
		yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells));
		m_enemySpeaking = false;
	}

	protected IEnumerator PlaySoundAndBlockSpeechWithCustomGameString(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		m_enemySpeaking = true;
		if ((bool)actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeInHeroActor(actor);
		}
		yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, gameString, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells));
		if ((bool)actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeOutHeroActor(actor);
		}
		m_enemySpeaking = false;
	}

	protected IEnumerator PlaySoundAndBlockSpeech(string soundPath, Notification.SpeechBubbleDirection direction, Actor actor, float testingDuration = 3f, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float bubbleScale = 0f)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		m_enemySpeaking = true;
		if ((bool)actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && !MulliganManager.Get().IsCustomIntroActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			iTween.StopByName(MulliganManager.Get().gameObject, GetMulliganHeroFadeItweenName(actor));
			GameState.Get().GetGameEntity().FadeInHeroActor(actor);
		}
		yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, legacyAssetName, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, testingDuration, bubbleScale));
		if ((bool)actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && !MulliganManager.Get().IsCustomIntroActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeOutHeroActor(actor);
		}
		m_enemySpeaking = false;
	}

	protected IEnumerator PlaySoundAndBlockSpeechOnce(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
		{
			NotificationManager.Get().ForceAddSoundToPlayedList(soundPath);
			m_enemySpeaking = true;
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, gameString, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells));
			m_enemySpeaking = false;
		}
	}

	protected IEnumerator PlaySoundAndBlockSpeechOnce(string soundPath, Notification.SpeechBubbleDirection direction, Actor actor, float testingDuration = 3f, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float bubbleScale = 0f)
	{
		if (!NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
		{
			NotificationManager.Get().ForceAddSoundToPlayedList(soundPath);
			string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
			m_enemySpeaking = true;
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndWait(soundPath, legacyAssetName, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, testingDuration, bubbleScale));
			m_enemySpeaking = false;
		}
	}

	protected IEnumerator PlaySoundAndWait(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float testingDuration = 3f, float bubbleScale = 0f)
	{
		AudioSource audioSource = null;
		bool isJustTesting = false;
		if (string.IsNullOrEmpty(soundPath) || !CheckPreloadedSound(soundPath))
		{
			isJustTesting = true;
		}
		else
		{
			audioSource = GetPreloadedSound(soundPath);
		}
		if (!isJustTesting && (audioSource == null || audioSource.clip == null))
		{
			if (CheckPreloadedSound(soundPath))
			{
				RemovePreloadedSound(soundPath);
				PreloadSound(soundPath);
				while (IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = GetPreloadedSound(soundPath);
			}
			if (audioSource == null || audioSource.clip == null)
			{
				Log.Sound.PrintDebug("MissionEntity.PlaySoundAndWait() - sound error - " + soundPath);
				yield break;
			}
		}
		float num = testingDuration;
		if (!isJustTesting)
		{
			num = audioSource.clip.length;
		}
		float num2 = num * waitTimeScale;
		if (!isJustTesting)
		{
			SoundManager.Get().PlayPreloaded(audioSource);
		}
		if (delayCardSoundSpells)
		{
			GameEntity.Coroutines.StartCoroutine(WaitForCardSoundSpellDelay(num));
		}
		if (actor != null && direction != 0)
		{
			ShowBubble(gameString, direction, actor, destroyOnNewNotification: false, num, parentBubbleToActor, bubbleScale);
			num2 += 0.5f;
		}
		yield return new WaitForSeconds(num2);
	}

	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, NotificationManager.DEFAULT_CHARACTER_POS, 1f, testingDuration, allowRepeatDuringSession, delayCardSoundSpells));
	}

	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, NotificationManager.DEFAULT_CHARACTER_POS, 1f, testingDuration, allowRepeatDuringSession, delayCardSoundSpells));
	}

	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, Vector3 position, float waitTimeScale = 1f, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false, bool isBig = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false)
	{
		AudioSource audioSource = null;
		bool isJustTesting = false;
		if (string.IsNullOrEmpty(soundPath) || !CheckPreloadedSound(soundPath))
		{
			isJustTesting = true;
		}
		else
		{
			audioSource = GetPreloadedSound(soundPath);
		}
		if (!isJustTesting && (audioSource == null || audioSource.clip == null))
		{
			if (CheckPreloadedSound(soundPath))
			{
				RemovePreloadedSound(soundPath);
				PreloadSound(soundPath);
				while (IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = GetPreloadedSound(soundPath);
			}
			if (audioSource == null || audioSource.clip == null)
			{
				Log.Sound.PrintDebug("MissionEntity.PlaySoundAndWait() - sound error - " + soundPath);
				yield break;
			}
		}
		float num = ((!isJustTesting) ? audioSource.clip.length : testingDuration);
		if (!persistCharacter)
		{
			num = Mathf.Max(num, 3f);
		}
		float num2 = num * waitTimeScale;
		Log.Notifications.Print("PlayCharacterQuoteAndWait() - Playing quote with clipLength {0}.  waitTimeScale: {1}  MINIMUM_DISPLAY_TIME_FOR_BIG_QUOTE: {2}", num, waitTimeScale, 3f);
		if (delayCardSoundSpells)
		{
			GameEntity.Coroutines.StartCoroutine(WaitForCardSoundSpellDelay(num));
		}
		if (isBig)
		{
			NotificationManager.Get().CreateBigCharacterQuoteWithGameString(prefabPath, position, soundPath, gameString, allowRepeatDuringSession, num, null, useOverlayUI: false, bubbleDir, persistCharacter);
		}
		else
		{
			if (persistCharacter)
			{
				Log.All.PrintWarning("PersistCharacter is not currently supported for CharacterQuotes that are not big!");
			}
			NotificationManager.Get().CreateCharacterQuote(prefabPath, position, GameStrings.Get(gameString), soundPath, allowRepeatDuringSession, num * 2f);
		}
		num2 += 0.5f;
		yield return new WaitForSeconds(num2);
		if (!persistCharacter)
		{
			NotificationManager.Get().DestroyActiveQuote(0f);
		}
	}

	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, Vector3 characterPosition, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false, bool persistCharacter = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, characterPosition, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, isBig: true, bubbleDir, persistCharacter));
	}

	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, isBig: true));
	}

	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, isBig: true));
	}

	protected IEnumerator PlayBigCharacterQuoteAndWaitOnce(string prefabPath, string soundPath, float testingDuration = 3f, float waitTimeScale = 1f, bool delayCardSoundSpells = false, bool persistCharacter = false)
	{
		bool allowRepeatDuringSession = DemoMgr.Get().IsExpoDemo();
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, isBig: true, Notification.SpeechBubbleDirection.None, persistCharacter));
	}

	protected IEnumerator PlayBigCharacterQuoteAndWaitOnce(string prefabPath, string soundPath, string gameString, float testingDuration = 3f, float waitTimeScale = 1f, bool delayCardSoundSpells = false)
	{
		bool allowRepeatDuringSession = DemoMgr.Get().IsExpoDemo();
		yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, isBig: true));
	}

	protected IEnumerator WaitForCardSoundSpellDelay(float sec)
	{
		GetGameOptions().SetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS, value: true);
		yield return new WaitForSeconds(sec);
		GetGameOptions().SetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS, value: false);
	}

	protected void ShowBubble(string textKey, Notification.SpeechBubbleDirection direction, Actor speakingActor, bool destroyOnNewNotification, float duration, bool parentToActor, float bubbleScale = 0f)
	{
		if (!(speakingActor == null))
		{
			NotificationManager notificationManager = NotificationManager.Get();
			Notification notification = notificationManager.CreateSpeechBubble(GameStrings.Get(textKey), direction, speakingActor, destroyOnNewNotification, parentToActor, bubbleScale);
			if (duration > 0f)
			{
				notificationManager.DestroyNotification(notification, duration);
			}
		}
	}

	protected ShouldPlayValue InternalShouldPlayOpeningLine()
	{
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayBossLine()
	{
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayMissionFlavorLine()
	{
		if (IsHeroic())
		{
			return ShouldPlayValue.Once;
		}
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayOnlyOnce()
	{
		return ShouldPlayValue.Once;
	}

	protected ShouldPlayValue InternalShouldPlayAdventureFlavorLine()
	{
		if (IsHeroic())
		{
			return ShouldPlayValue.Once;
		}
		if (IsClassChallenge())
		{
			return ShouldPlayValue.Never;
		}
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayClosingLine()
	{
		if (IsClassChallenge())
		{
			return ShouldPlayValue.Never;
		}
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayEasterEggLine()
	{
		return ShouldPlayValue.Always;
	}

	protected ShouldPlayValue InternalShouldPlayCriticalLine()
	{
		return ShouldPlayValue.Always;
	}

	protected Notification.SpeechBubbleDirection GetDirection(Actor actor)
	{
		if (actor.GetEntity().IsControlledByFriendlySidePlayer())
		{
			return Notification.SpeechBubbleDirection.BottomLeft;
		}
		return Notification.SpeechBubbleDirection.TopRight;
	}

	protected string GetMulliganHeroFadeItweenName(Actor actor)
	{
		if (actor.GetEntity().IsControlledByFriendlySidePlayer())
		{
			return "MyHeroLightBlend";
		}
		return "HisHeroLightBlend";
	}

	protected IEnumerator PlayLittleCharacterLine(string speaker, string line, ShouldPlay shouldPlay)
	{
		yield return PlayLittleCharacterLine(speaker, line, shouldPlay, 2.5f);
	}

	protected IEnumerator PlayLittleCharacterLine(string speaker, string line, ShouldPlay shouldPlay, float testingDuration)
	{
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayCharacterQuoteAndWait(speaker, line, testingDuration));
		}
	}

	protected IEnumerator PlayLine(string speaker, string line, ShouldPlay shouldPlay, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, shouldPlay, Vector3.zero, Notification.SpeechBubbleDirection.None, duration);
	}

	protected IEnumerator PlayLine(string speaker, string line, ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		if (m_enemySpeaking)
		{
			yield return null;
		}
		m_enemySpeaking = true;
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, allowRepeatDuringSession: true, delayCardSoundSpells: false, persistCharacter));
		}
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, allowRepeatDuringSession: true, delayCardSoundSpells: false, persistCharacter));
		}
		else if (shouldPlay() == ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlayBigCharacterQuoteAndWaitOnce(speaker, line, duration, 1f, delayCardSoundSpells: false, persistCharacter));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		m_enemySpeaking = false;
	}

	protected IEnumerator PlayLine(Actor speaker, string line, ShouldPlay shouldPlay)
	{
		yield return PlayLine(speaker, line, shouldPlay, 2.5f);
	}

	protected IEnumerator PlayLine(Actor speaker, string line, ShouldPlay shouldPlay, float duration)
	{
		if (m_enemySpeaking)
		{
			yield return null;
		}
		m_enemySpeaking = true;
		Notification.SpeechBubbleDirection direction = GetDirection(speaker);
		if (m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, direction, speaker, duration));
		}
		if (shouldPlay() == ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeech(line, direction, speaker, duration));
		}
		else if (shouldPlay() == ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeechOnce(line, direction, speaker, duration));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		m_enemySpeaking = false;
	}

	protected bool ShouldPlayLine(string line, ShouldPlay shouldPlay)
	{
		bool result = false;
		switch (shouldPlay())
		{
		case ShouldPlayValue.Always:
			result = true;
			break;
		case ShouldPlayValue.Once:
			if (DemoMgr.Get().IsExpoDemo() || !NotificationManager.Get().HasSoundPlayedThisSession(line))
			{
				result = true;
			}
			break;
		}
		return result;
	}

	protected IEnumerator PlayOpeningLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayOpeningLine, duration);
	}

	protected IEnumerator PlayOpeningLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayOpeningLine, duration);
	}

	protected IEnumerator PlayBossLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayBossLine, duration);
	}

	protected IEnumerator PlayBossLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayBossLine, duration);
	}

	protected IEnumerator PlayLineOnlyOnce(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayOnlyOnce, duration);
	}

	protected IEnumerator PlayLineOnlyOnce(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayOnlyOnce, duration);
	}

	protected IEnumerator PlayMissionFlavorLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayMissionFlavorLine, duration);
	}

	protected IEnumerator PlayMissionFlavorLine(string speaker, string line, Vector3 quotePosition, Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.None, float duration = 2.5f, bool persistCharacter = false)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayMissionFlavorLine, quotePosition, direction, duration, persistCharacter);
	}

	protected IEnumerator PlayMissionFlavorLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayMissionFlavorLine, duration);
	}

	protected IEnumerator PlayAdventureFlavorLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayAdventureFlavorLine, duration);
	}

	protected IEnumerator PlayAdventureFlavorLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayAdventureFlavorLine, duration);
	}

	protected IEnumerator PlayClosingLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLittleCharacterLine(speaker, line, InternalShouldPlayClosingLine, duration);
	}

	protected IEnumerator PlayClosingLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayClosingLine, duration);
	}

	protected IEnumerator PlayEasterEggLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayEasterEggLine, duration);
	}

	protected IEnumerator PlayEasterEggLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayEasterEggLine, duration);
	}

	protected IEnumerator PlayCriticalLine(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayCriticalLine, duration);
	}

	protected IEnumerator PlayCriticalLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayCriticalLine, duration);
	}

	protected bool ShouldPlayClosingLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayClosingLine);
	}

	protected bool ShouldPlayCriticalLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayCriticalLine);
	}

	protected bool ShouldPlayAdventureFlavorLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayAdventureFlavorLine);
	}

	protected bool ShouldPlayMissionFlavorLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayMissionFlavorLine);
	}

	protected bool ShouldPlayBossLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayBossLine);
	}

	protected bool ShouldPlayEasterEggLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayEasterEggLine);
	}

	protected bool ShouldPlayOpeningLine(string line)
	{
		return ShouldPlayLine(line, InternalShouldPlayOpeningLine);
	}

	protected IEnumerator PlayLineAlways(string speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayBossLine, duration);
	}

	protected IEnumerator PlayLineAlways(Actor speaker, string line, float duration = 2.5f)
	{
		yield return PlayLine(speaker, line, InternalShouldPlayBossLine, duration);
	}

	protected IEnumerator PlayLineAlways(Actor speaker, string backupSpeaker, string line, float duration = 2.5f)
	{
		if (speaker == null)
		{
			yield return PlayLine(backupSpeaker, line, InternalShouldPlayBossLine, duration);
		}
		else
		{
			yield return PlayLine(speaker, line, InternalShouldPlayBossLine, duration);
		}
	}

	public IEnumerator PlayLineInOrderOnce(Actor actor, List<string> lines)
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
		if (text != null)
		{
			m_InOrderPlayedLines.Add(text);
			yield return PlayLineAlways(actor, text);
		}
	}

	public IEnumerator PlayLineInOrderOnce(string actor, List<string> lines)
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
		if (text != null)
		{
			m_InOrderPlayedLines.Add(text);
			yield return PlayLineAlways(actor, text);
		}
	}

	protected virtual void InitEmoteResponses()
	{
	}

	protected IEnumerator HandlePlayerEmoteWithTiming(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		while (emoteSpell.IsActive())
		{
			yield return null;
		}
		if (!m_enemySpeaking)
		{
			PlayEmoteResponse(emoteType, emoteSpell);
		}
	}

	protected virtual void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		foreach (EmoteResponseGroup emoteResponseGroup in m_emoteResponseGroups)
		{
			if (emoteResponseGroup.m_responses.Count != 0 && emoteResponseGroup.m_triggers.Contains(emoteType))
			{
				PlayNextEmoteResponse(emoteResponseGroup, actor);
				CycleNextResponseGroupIndex(emoteResponseGroup);
			}
		}
	}

	protected virtual IEnumerator PlayEmoteResponseWithTiming(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard()
			.GetActor();
		if (enemyActor == null)
		{
			yield break;
		}
		foreach (EmoteResponseGroup responseGroup in m_emoteResponseGroups)
		{
			if (responseGroup.m_responses.Count != 0 && responseGroup.m_triggers.Contains(emoteType))
			{
				int responseIndex = responseGroup.m_responseIndex;
				EmoteResponse emoteResponse = responseGroup.m_responses[responseIndex];
				yield return PlaySoundAndBlockSpeechWithCustomGameString(emoteResponse.m_soundName, emoteResponse.m_stringTag, Notification.SpeechBubbleDirection.TopRight, enemyActor);
				CycleNextResponseGroupIndex(responseGroup);
			}
		}
	}

	protected void PlayNextEmoteResponse(EmoteResponseGroup responseGroup, Actor actor)
	{
		int responseIndex = responseGroup.m_responseIndex;
		EmoteResponse emoteResponse = responseGroup.m_responses[responseIndex];
		GameEntity.Coroutines.StartCoroutine(PlaySoundAndBlockSpeechWithCustomGameString(emoteResponse.m_soundName, emoteResponse.m_stringTag, Notification.SpeechBubbleDirection.TopRight, actor));
	}

	protected virtual void CycleNextResponseGroupIndex(EmoteResponseGroup responseGroup)
	{
		if (responseGroup.m_responseIndex == responseGroup.m_responses.Count - 1)
		{
			responseGroup.m_responseIndex = 0;
		}
		else
		{
			responseGroup.m_responseIndex++;
		}
	}
}
