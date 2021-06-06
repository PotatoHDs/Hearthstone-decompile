using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000581 RID: 1409
public class MissionEntity : GameEntity
{
	// Token: 0x06004E6D RID: 20077 RVA: 0x0019DF67 File Offset: 0x0019C167
	private static Map<GameEntityOption, bool> InitBooleanOptions()
	{
		return new Map<GameEntityOption, bool>
		{
			{
				GameEntityOption.USE_SECRET_CLASS_NAMES,
				true
			}
		};
	}

	// Token: 0x06004E6E RID: 20078 RVA: 0x0010C860 File Offset: 0x0010AA60
	private static Map<GameEntityOption, string> InitStringOptions()
	{
		return new Map<GameEntityOption, string>();
	}

	// Token: 0x06004E6F RID: 20079 RVA: 0x0019DF77 File Offset: 0x0019C177
	public MissionEntity()
	{
		this.m_gameOptions.AddOptions(MissionEntity.s_booleanOptions, MissionEntity.s_stringOptions);
		this.InitEmoteResponses();
	}

	// Token: 0x06004E70 RID: 20080 RVA: 0x0019DFB0 File Offset: 0x0019C1B0
	public override void OnTagChanged(TagDelta change)
	{
		GAME_TAG tag = (GAME_TAG)change.tag;
		if (tag != GAME_TAG.MISSION_EVENT)
		{
			if (tag != GAME_TAG.STEP)
			{
				if (tag == GAME_TAG.NEXT_STEP)
				{
					if (change.newValue == 6)
					{
						this.HandleMainReadyStep();
					}
					else if (change.oldValue == 9 && change.newValue == 10 && GameState.Get().IsLocalSidePlayerTurn())
					{
						TurnStartManager.Get().BeginPlayingTurnEvents();
					}
				}
			}
			else if (change.newValue == 4)
			{
				this.HandleMulliganTagChange();
			}
			else if (change.oldValue == 9 && change.newValue == 10 && !GameState.Get().IsFriendlySidePlayerTurn())
			{
				this.HandleStartOfTurn(base.GetTag(GAME_TAG.TURN));
			}
		}
		else
		{
			this.HandleMissionEvent(change.newValue);
		}
		base.OnTagChanged(change);
	}

	// Token: 0x06004E71 RID: 20081 RVA: 0x0019E06C File Offset: 0x0019C26C
	public override void NotifyOfStartOfTurnEventsFinished()
	{
		this.HandleStartOfTurn(base.GetTag(GAME_TAG.TURN));
	}

	// Token: 0x06004E72 RID: 20082 RVA: 0x0019E07C File Offset: 0x0019C27C
	public override void SendCustomEvent(int eventID)
	{
		this.HandleMissionEvent(eventID);
	}

	// Token: 0x06004E73 RID: 20083 RVA: 0x0019E085 File Offset: 0x0019C285
	public override void NotifyOfOpponentWillPlayCard(string cardId)
	{
		base.NotifyOfOpponentWillPlayCard(cardId);
		GameEntity.Coroutines.StartCoroutine(this.RespondToWillPlayCardWithTiming(cardId));
	}

	// Token: 0x06004E74 RID: 20084 RVA: 0x0019E0A0 File Offset: 0x0019C2A0
	public override void NotifyOfOpponentPlayedCard(Entity entity)
	{
		base.NotifyOfOpponentPlayedCard(entity);
		GameEntity.Coroutines.StartCoroutine(this.RespondToPlayedCardWithTiming(entity));
	}

	// Token: 0x06004E75 RID: 20085 RVA: 0x0019E0BB File Offset: 0x0019C2BB
	public override void NotifyOfFriendlyPlayedCard(Entity entity)
	{
		base.NotifyOfFriendlyPlayedCard(entity);
		GameEntity.Coroutines.StartCoroutine(this.RespondToFriendlyPlayedCardWithTiming(entity));
	}

	// Token: 0x06004E76 RID: 20086 RVA: 0x0019E0D6 File Offset: 0x0019C2D6
	public override void NotifyOfGameOver(TAG_PLAYSTATE gameResult)
	{
		base.NotifyOfGameOver(gameResult);
		GameEntity.Coroutines.StartCoroutine(this.HandleGameOverWithTiming(gameResult));
	}

	// Token: 0x06004E77 RID: 20087 RVA: 0x0019E0F1 File Offset: 0x0019C2F1
	public override void NotifyOfResetGameStarted()
	{
		base.NotifyOfResetGameStarted();
		GameEntity.Coroutines.StopAllCoroutines();
	}

	// Token: 0x06004E78 RID: 20088 RVA: 0x0019E103 File Offset: 0x0019C303
	public override void NotifyOfResetGameFinished(Entity source, Entity oldGameEntity)
	{
		base.NotifyOfResetGameFinished(source, oldGameEntity);
		GameEntity.Coroutines.StartCoroutine(this.RespondToResetGameFinishedWithTiming(source));
	}

	// Token: 0x06004E79 RID: 20089 RVA: 0x0019E11F File Offset: 0x0019C31F
	public override void OnEmotePlayed(Card card, EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		if (card.GetEntity().IsControlledByFriendlySidePlayer())
		{
			GameEntity.Coroutines.StartCoroutine(this.HandlePlayerEmoteWithTiming(emoteType, emoteSpell));
		}
	}

	// Token: 0x06004E7A RID: 20090 RVA: 0x0019E141 File Offset: 0x0019C341
	public override bool DoAlternateMulliganIntro()
	{
		if (!this.ShouldDoAlternateMulliganIntro())
		{
			return false;
		}
		GameEntity.Coroutines.StartCoroutine(base.SkipStandardMulliganWithTiming());
		return true;
	}

	// Token: 0x06004E7B RID: 20091 RVA: 0x0019E15F File Offset: 0x0019C35F
	public bool IsHeroic()
	{
		return GameMgr.Get().IsHeroicMission();
	}

	// Token: 0x06004E7C RID: 20092 RVA: 0x0019E16B File Offset: 0x0019C36B
	public bool IsClassChallenge()
	{
		return GameMgr.Get().IsClassChallengeMission();
	}

	// Token: 0x06004E7D RID: 20093 RVA: 0x0019E178 File Offset: 0x0019C378
	protected virtual void HandleMainReadyStep()
	{
		if (GameState.Get() == null)
		{
			Log.Gameplay.PrintError("MissionEntity.HandleMainReadyStep(): GameState is null.", Array.Empty<object>());
			return;
		}
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		if (gameEntity == null)
		{
			Log.Gameplay.PrintError("MissionEntity.HandleMainReadyStep(): GameEntity is null.", Array.Empty<object>());
			return;
		}
		if (gameEntity.GetTag(GAME_TAG.TURN) != 1)
		{
			return;
		}
		if (GameState.Get().IsMulliganManagerActive())
		{
			GameState.Get().SetMulliganBusy(true);
			return;
		}
		if (this.ShouldDoAlternateMulliganIntro())
		{
			return;
		}
		GameState.Get().SetMulliganBusy(true);
		if (MulliganManager.Get() != null)
		{
			MulliganManager.Get().SkipMulligan();
		}
	}

	// Token: 0x06004E7E RID: 20094 RVA: 0x0019E213 File Offset: 0x0019C413
	public void SetBlockVo(bool shouldBlock, float unblockAfterSeconds = 0f)
	{
		if (unblockAfterSeconds < 0f)
		{
			unblockAfterSeconds = 0f;
		}
		if (!shouldBlock)
		{
			this.m_enemySpeaking = shouldBlock;
		}
		if (shouldBlock)
		{
			GameEntity.Coroutines.StartCoroutine(this.UnblockSpeechAgainAfterDuration(unblockAfterSeconds));
		}
	}

	// Token: 0x06004E7F RID: 20095 RVA: 0x0019E243 File Offset: 0x0019C443
	private IEnumerator UnblockSpeechAgainAfterDuration(float durationInSeconds)
	{
		if (durationInSeconds <= 0f)
		{
			this.m_enemySpeaking = false;
			yield break;
		}
		while (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_enemySpeaking = true;
		yield return new WaitForSeconds(durationInSeconds);
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E80 RID: 20096 RVA: 0x0019E259 File Offset: 0x0019C459
	public bool IsVoBlocked()
	{
		return this.m_enemySpeaking;
	}

	// Token: 0x06004E81 RID: 20097 RVA: 0x0019E261 File Offset: 0x0019C461
	protected virtual void HandleMulliganTagChange()
	{
		MulliganManager.Get().BeginMulligan();
	}

	// Token: 0x06004E82 RID: 20098 RVA: 0x0019E270 File Offset: 0x0019C470
	protected void HandleStartOfTurn(int turn)
	{
		if (GameState.Get().GetGameEntity().GetTag(GAME_TAG.IS_CURRENT_TURN_AN_EXTRA_TURN) != 0)
		{
			return;
		}
		int turn2 = turn - GameState.Get().GetGameEntity().GetTag(GAME_TAG.EXTRA_TURNS_TAKEN_THIS_GAME);
		GameEntity.Coroutines.StartCoroutine(this.HandleStartOfTurnWithTiming(turn2));
	}

	// Token: 0x06004E83 RID: 20099 RVA: 0x0019E2BD File Offset: 0x0019C4BD
	protected virtual IEnumerator HandleStartOfTurnWithTiming(int turn)
	{
		yield break;
	}

	// Token: 0x06004E84 RID: 20100 RVA: 0x0019E2C5 File Offset: 0x0019C4C5
	protected void HandleMissionEvent(int missionEvent)
	{
		GameEntity.Coroutines.StartCoroutine(this.HandleMissionEventWithTiming(missionEvent));
	}

	// Token: 0x06004E85 RID: 20101 RVA: 0x0019E2D9 File Offset: 0x0019C4D9
	protected virtual IEnumerator HandleMissionEventWithTiming(int missionEvent)
	{
		yield break;
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x0019E2E1 File Offset: 0x0019C4E1
	protected virtual IEnumerator RespondToWillPlayCardWithTiming(string cardId)
	{
		yield break;
	}

	// Token: 0x06004E87 RID: 20103 RVA: 0x0019E2E9 File Offset: 0x0019C4E9
	protected virtual IEnumerator RespondToPlayedCardWithTiming(Entity entity)
	{
		yield break;
	}

	// Token: 0x06004E88 RID: 20104 RVA: 0x0019E2F1 File Offset: 0x0019C4F1
	protected virtual IEnumerator RespondToFriendlyPlayedCardWithTiming(Entity entity)
	{
		yield break;
	}

	// Token: 0x06004E89 RID: 20105 RVA: 0x0019E2F9 File Offset: 0x0019C4F9
	protected virtual IEnumerator HandleGameOverWithTiming(TAG_PLAYSTATE gameResult)
	{
		yield break;
	}

	// Token: 0x06004E8A RID: 20106 RVA: 0x0019E301 File Offset: 0x0019C501
	protected virtual IEnumerator RespondToResetGameFinishedWithTiming(Entity source)
	{
		yield break;
	}

	// Token: 0x06004E8B RID: 20107 RVA: 0x0019E30C File Offset: 0x0019C50C
	protected void PlaySound(string soundPath, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
	}

	// Token: 0x06004E8C RID: 20108 RVA: 0x0019E33C File Offset: 0x0019C53C
	protected IEnumerator PlaySoundAndBlockSpeech(string soundPath, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		this.m_enemySpeaking = true;
		yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, null, Notification.SpeechBubbleDirection.None, null, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E8D RID: 20109 RVA: 0x0019E368 File Offset: 0x0019C568
	protected IEnumerator PlaySoundAndBlockSpeechWithCustomGameString(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		this.m_enemySpeaking = true;
		if (actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeInHeroActor(actor);
		}
		yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, gameString, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		if (actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeOutHeroActor(actor);
		}
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E8E RID: 20110 RVA: 0x0019E3B8 File Offset: 0x0019C5B8
	protected IEnumerator PlaySoundAndBlockSpeech(string soundPath, Notification.SpeechBubbleDirection direction, Actor actor, float testingDuration = 3f, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float bubbleScale = 0f)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		this.m_enemySpeaking = true;
		if (actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && !MulliganManager.Get().IsCustomIntroActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			iTween.StopByName(MulliganManager.Get().gameObject, this.GetMulliganHeroFadeItweenName(actor));
			GameState.Get().GetGameEntity().FadeInHeroActor(actor);
		}
		yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, legacyAssetName, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, testingDuration, bubbleScale));
		if (actor && MulliganManager.Get() != null && MulliganManager.Get().IsMulliganActive() && !MulliganManager.Get().IsCustomIntroActive() && actor.GetEntity() != null && actor.GetEntity().IsHero())
		{
			GameState.Get().GetGameEntity().FadeOutHeroActor(actor);
		}
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E8F RID: 20111 RVA: 0x0019E410 File Offset: 0x0019C610
	protected IEnumerator PlaySoundAndBlockSpeechOnce(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false)
	{
		if (NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
		{
			yield break;
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(soundPath);
		this.m_enemySpeaking = true;
		yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, gameString, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, 3f, 0f));
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E90 RID: 20112 RVA: 0x0019E460 File Offset: 0x0019C660
	protected IEnumerator PlaySoundAndBlockSpeechOnce(string soundPath, Notification.SpeechBubbleDirection direction, Actor actor, float testingDuration = 3f, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float bubbleScale = 0f)
	{
		if (NotificationManager.Get().HasSoundPlayedThisSession(soundPath))
		{
			yield break;
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(soundPath);
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		this.m_enemySpeaking = true;
		yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndWait(soundPath, legacyAssetName, direction, actor, waitTimeScale, parentBubbleToActor, delayCardSoundSpells, testingDuration, bubbleScale));
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004E91 RID: 20113 RVA: 0x0019E4B8 File Offset: 0x0019C6B8
	protected IEnumerator PlaySoundAndWait(string soundPath, string gameString, Notification.SpeechBubbleDirection direction, Actor actor, float waitTimeScale = 1f, bool parentBubbleToActor = true, bool delayCardSoundSpells = false, float testingDuration = 3f, float bubbleScale = 0f)
	{
		AudioSource audioSource = null;
		bool isJustTesting = false;
		if (string.IsNullOrEmpty(soundPath) || !base.CheckPreloadedSound(soundPath))
		{
			isJustTesting = true;
		}
		else
		{
			audioSource = base.GetPreloadedSound(soundPath);
		}
		if (!isJustTesting && (audioSource == null || audioSource.clip == null))
		{
			if (base.CheckPreloadedSound(soundPath))
			{
				base.RemovePreloadedSound(soundPath);
				base.PreloadSound(soundPath);
				while (base.IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = base.GetPreloadedSound(soundPath);
			}
			if (audioSource == null || audioSource.clip == null)
			{
				Log.Sound.PrintDebug("MissionEntity.PlaySoundAndWait() - sound error - " + soundPath, Array.Empty<object>());
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
			GameEntity.Coroutines.StartCoroutine(this.WaitForCardSoundSpellDelay(num));
		}
		if (actor != null && direction != Notification.SpeechBubbleDirection.None)
		{
			this.ShowBubble(gameString, direction, actor, false, num, parentBubbleToActor, bubbleScale);
			num2 += 0.5f;
		}
		yield return new WaitForSeconds(num2);
		yield break;
	}

	// Token: 0x06004E92 RID: 20114 RVA: 0x0019E517 File Offset: 0x0019C717
	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, NotificationManager.DEFAULT_CHARACTER_POS, 1f, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, false, Notification.SpeechBubbleDirection.None, false));
		yield break;
	}

	// Token: 0x06004E93 RID: 20115 RVA: 0x0019E54B File Offset: 0x0019C74B
	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, NotificationManager.DEFAULT_CHARACTER_POS, 1f, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, false, Notification.SpeechBubbleDirection.None, false));
		yield break;
	}

	// Token: 0x06004E94 RID: 20116 RVA: 0x0019E588 File Offset: 0x0019C788
	protected IEnumerator PlayCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, Vector3 position, float waitTimeScale = 1f, float testingDuration = 0f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false, bool isBig = false, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, bool persistCharacter = false)
	{
		AudioSource audioSource = null;
		bool isJustTesting = false;
		if (string.IsNullOrEmpty(soundPath) || !base.CheckPreloadedSound(soundPath))
		{
			isJustTesting = true;
		}
		else
		{
			audioSource = base.GetPreloadedSound(soundPath);
		}
		if (!isJustTesting && (audioSource == null || audioSource.clip == null))
		{
			if (base.CheckPreloadedSound(soundPath))
			{
				base.RemovePreloadedSound(soundPath);
				base.PreloadSound(soundPath);
				while (base.IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = base.GetPreloadedSound(soundPath);
			}
			if (audioSource == null || audioSource.clip == null)
			{
				Log.Sound.PrintDebug("MissionEntity.PlaySoundAndWait() - sound error - " + soundPath, Array.Empty<object>());
				yield break;
			}
		}
		float num;
		if (isJustTesting)
		{
			num = testingDuration;
		}
		else
		{
			num = audioSource.clip.length;
		}
		if (!persistCharacter)
		{
			num = Mathf.Max(num, 3f);
		}
		float num2 = num * waitTimeScale;
		Log.Notifications.Print("PlayCharacterQuoteAndWait() - Playing quote with clipLength {0}.  waitTimeScale: {1}  MINIMUM_DISPLAY_TIME_FOR_BIG_QUOTE: {2}", new object[]
		{
			num,
			waitTimeScale,
			3f
		});
		if (delayCardSoundSpells)
		{
			GameEntity.Coroutines.StartCoroutine(this.WaitForCardSoundSpellDelay(num));
		}
		if (isBig)
		{
			NotificationManager.Get().CreateBigCharacterQuoteWithGameString(prefabPath, position, soundPath, gameString, allowRepeatDuringSession, num, null, false, bubbleDir, persistCharacter, false);
		}
		else
		{
			if (persistCharacter)
			{
				Log.All.PrintWarning("PersistCharacter is not currently supported for CharacterQuotes that are not big!", Array.Empty<object>());
			}
			NotificationManager.Get().CreateCharacterQuote(prefabPath, position, GameStrings.Get(gameString), soundPath, allowRepeatDuringSession, num * 2f, null, CanvasAnchor.BOTTOM_LEFT, false);
		}
		num2 += 0.5f;
		yield return new WaitForSeconds(num2);
		if (!persistCharacter)
		{
			NotificationManager.Get().DestroyActiveQuote(0f, false);
		}
		yield break;
	}

	// Token: 0x06004E95 RID: 20117 RVA: 0x0019E5F8 File Offset: 0x0019C7F8
	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, Vector3 characterPosition, Notification.SpeechBubbleDirection bubbleDir = Notification.SpeechBubbleDirection.None, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false, bool persistCharacter = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, characterPosition, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, true, bubbleDir, persistCharacter));
		yield break;
	}

	// Token: 0x06004E96 RID: 20118 RVA: 0x0019E657 File Offset: 0x0019C857
	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, true, Notification.SpeechBubbleDirection.None, false));
		yield break;
	}

	// Token: 0x06004E97 RID: 20119 RVA: 0x0019E694 File Offset: 0x0019C894
	protected IEnumerator PlayBigCharacterQuoteAndWait(string prefabPath, string soundPath, string gameString, float testingDuration = 3f, float waitTimeScale = 1f, bool allowRepeatDuringSession = true, bool delayCardSoundSpells = false)
	{
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, true, Notification.SpeechBubbleDirection.None, false));
		yield break;
	}

	// Token: 0x06004E98 RID: 20120 RVA: 0x0019E6E3 File Offset: 0x0019C8E3
	protected IEnumerator PlayBigCharacterQuoteAndWaitOnce(string prefabPath, string soundPath, float testingDuration = 3f, float waitTimeScale = 1f, bool delayCardSoundSpells = false, bool persistCharacter = false)
	{
		bool allowRepeatDuringSession = DemoMgr.Get().IsExpoDemo();
		string legacyAssetName = new AssetReference(soundPath).GetLegacyAssetName();
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, legacyAssetName, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, true, Notification.SpeechBubbleDirection.None, persistCharacter));
		yield break;
	}

	// Token: 0x06004E99 RID: 20121 RVA: 0x0019E71F File Offset: 0x0019C91F
	protected IEnumerator PlayBigCharacterQuoteAndWaitOnce(string prefabPath, string soundPath, string gameString, float testingDuration = 3f, float waitTimeScale = 1f, bool delayCardSoundSpells = false)
	{
		bool allowRepeatDuringSession = DemoMgr.Get().IsExpoDemo();
		yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(prefabPath, soundPath, gameString, Vector3.zero, waitTimeScale, testingDuration, allowRepeatDuringSession, delayCardSoundSpells, true, Notification.SpeechBubbleDirection.None, false));
		yield break;
	}

	// Token: 0x06004E9A RID: 20122 RVA: 0x0019E75B File Offset: 0x0019C95B
	protected IEnumerator WaitForCardSoundSpellDelay(float sec)
	{
		base.GetGameOptions().SetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS, true);
		yield return new WaitForSeconds(sec);
		base.GetGameOptions().SetBooleanOption(GameEntityOption.DELAY_CARD_SOUND_SPELLS, false);
		yield break;
	}

	// Token: 0x06004E9B RID: 20123 RVA: 0x0019E774 File Offset: 0x0019C974
	protected void ShowBubble(string textKey, Notification.SpeechBubbleDirection direction, Actor speakingActor, bool destroyOnNewNotification, float duration, bool parentToActor, float bubbleScale = 0f)
	{
		if (speakingActor == null)
		{
			return;
		}
		NotificationManager notificationManager = NotificationManager.Get();
		Notification notification = notificationManager.CreateSpeechBubble(GameStrings.Get(textKey), direction, speakingActor, destroyOnNewNotification, parentToActor, bubbleScale);
		if (duration > 0f)
		{
			notificationManager.DestroyNotification(notification, duration);
		}
	}

	// Token: 0x06004E9C RID: 20124 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOpeningLine()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004E9D RID: 20125 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayBossLine()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004E9E RID: 20126 RVA: 0x0019E7B8 File Offset: 0x0019C9B8
	protected MissionEntity.ShouldPlayValue InternalShouldPlayMissionFlavorLine()
	{
		if (this.IsHeroic())
		{
			return MissionEntity.ShouldPlayValue.Once;
		}
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004E9F RID: 20127 RVA: 0x000052EC File Offset: 0x000034EC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayOnlyOnce()
	{
		return MissionEntity.ShouldPlayValue.Once;
	}

	// Token: 0x06004EA0 RID: 20128 RVA: 0x0019E7C5 File Offset: 0x0019C9C5
	protected MissionEntity.ShouldPlayValue InternalShouldPlayAdventureFlavorLine()
	{
		if (this.IsHeroic())
		{
			return MissionEntity.ShouldPlayValue.Once;
		}
		if (this.IsClassChallenge())
		{
			return MissionEntity.ShouldPlayValue.Never;
		}
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004EA1 RID: 20129 RVA: 0x0019E7DC File Offset: 0x0019C9DC
	protected MissionEntity.ShouldPlayValue InternalShouldPlayClosingLine()
	{
		if (this.IsClassChallenge())
		{
			return MissionEntity.ShouldPlayValue.Never;
		}
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004EA2 RID: 20130 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayEasterEggLine()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004EA3 RID: 20131 RVA: 0x000052D6 File Offset: 0x000034D6
	protected MissionEntity.ShouldPlayValue InternalShouldPlayCriticalLine()
	{
		return MissionEntity.ShouldPlayValue.Always;
	}

	// Token: 0x06004EA4 RID: 20132 RVA: 0x0019E7E9 File Offset: 0x0019C9E9
	protected Notification.SpeechBubbleDirection GetDirection(Actor actor)
	{
		if (actor.GetEntity().IsControlledByFriendlySidePlayer())
		{
			return Notification.SpeechBubbleDirection.BottomLeft;
		}
		return Notification.SpeechBubbleDirection.TopRight;
	}

	// Token: 0x06004EA5 RID: 20133 RVA: 0x0019E7FB File Offset: 0x0019C9FB
	protected string GetMulliganHeroFadeItweenName(Actor actor)
	{
		if (actor.GetEntity().IsControlledByFriendlySidePlayer())
		{
			return "MyHeroLightBlend";
		}
		return "HisHeroLightBlend";
	}

	// Token: 0x06004EA6 RID: 20134 RVA: 0x0019E815 File Offset: 0x0019CA15
	protected IEnumerator PlayLittleCharacterLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay)
	{
		yield return this.PlayLittleCharacterLine(speaker, line, shouldPlay, 2.5f);
		yield break;
	}

	// Token: 0x06004EA7 RID: 20135 RVA: 0x0019E839 File Offset: 0x0019CA39
	protected IEnumerator PlayLittleCharacterLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, float testingDuration)
	{
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlayCharacterQuoteAndWait(speaker, line, testingDuration, true, false));
		}
		yield break;
	}

	// Token: 0x06004EA8 RID: 20136 RVA: 0x0019E865 File Offset: 0x0019CA65
	protected IEnumerator PlayLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, shouldPlay, Vector3.zero, Notification.SpeechBubbleDirection.None, duration, false);
		yield break;
	}

	// Token: 0x06004EA9 RID: 20137 RVA: 0x0019E894 File Offset: 0x0019CA94
	protected IEnumerator PlayLine(string speaker, string line, MissionEntity.ShouldPlay shouldPlay, Vector3 quotePosition, Notification.SpeechBubbleDirection direction, float duration, bool persistCharacter = false)
	{
		if (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_enemySpeaking = true;
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, true, false, persistCharacter));
		}
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlayBigCharacterQuoteAndWait(speaker, line, quotePosition, direction, duration, 1f, true, false, persistCharacter));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlayBigCharacterQuoteAndWaitOnce(speaker, line, duration, 1f, false, persistCharacter));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004EAA RID: 20138 RVA: 0x0019E8E3 File Offset: 0x0019CAE3
	protected IEnumerator PlayLine(Actor speaker, string line, MissionEntity.ShouldPlay shouldPlay)
	{
		yield return this.PlayLine(speaker, line, shouldPlay, 2.5f);
		yield break;
	}

	// Token: 0x06004EAB RID: 20139 RVA: 0x0019E907 File Offset: 0x0019CB07
	protected IEnumerator PlayLine(Actor speaker, string line, MissionEntity.ShouldPlay shouldPlay, float duration)
	{
		if (this.m_enemySpeaking)
		{
			yield return null;
		}
		this.m_enemySpeaking = true;
		Notification.SpeechBubbleDirection direction = this.GetDirection(speaker);
		if (this.m_forceAlwaysPlayLine)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndBlockSpeech(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		if (shouldPlay() == MissionEntity.ShouldPlayValue.Always)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndBlockSpeech(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		else if (shouldPlay() == MissionEntity.ShouldPlayValue.Once)
		{
			yield return GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndBlockSpeechOnce(line, direction, speaker, duration, 1f, true, false, 0f));
		}
		NotificationManager.Get().ForceAddSoundToPlayedList(line);
		this.m_enemySpeaking = false;
		yield break;
	}

	// Token: 0x06004EAC RID: 20140 RVA: 0x0019E934 File Offset: 0x0019CB34
	protected bool ShouldPlayLine(string line, MissionEntity.ShouldPlay shouldPlay)
	{
		bool result = false;
		MissionEntity.ShouldPlayValue shouldPlayValue = shouldPlay();
		if (shouldPlayValue != MissionEntity.ShouldPlayValue.Once)
		{
			if (shouldPlayValue == MissionEntity.ShouldPlayValue.Always)
			{
				result = true;
			}
		}
		else if (DemoMgr.Get().IsExpoDemo() || !NotificationManager.Get().HasSoundPlayedThisSession(line))
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x0019E972 File Offset: 0x0019CB72
	protected IEnumerator PlayOpeningLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayOpeningLine), duration);
		yield break;
	}

	// Token: 0x06004EAE RID: 20142 RVA: 0x0019E996 File Offset: 0x0019CB96
	protected IEnumerator PlayOpeningLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayOpeningLine), duration);
		yield break;
	}

	// Token: 0x06004EAF RID: 20143 RVA: 0x0019E9BA File Offset: 0x0019CBBA
	protected IEnumerator PlayBossLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004EB0 RID: 20144 RVA: 0x0019E9DE File Offset: 0x0019CBDE
	protected IEnumerator PlayBossLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004EB1 RID: 20145 RVA: 0x0019EA02 File Offset: 0x0019CC02
	protected IEnumerator PlayLineOnlyOnce(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x06004EB2 RID: 20146 RVA: 0x0019EA26 File Offset: 0x0019CC26
	protected IEnumerator PlayLineOnlyOnce(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayOnlyOnce), duration);
		yield break;
	}

	// Token: 0x06004EB3 RID: 20147 RVA: 0x0019EA4A File Offset: 0x0019CC4A
	protected IEnumerator PlayMissionFlavorLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayMissionFlavorLine), duration);
		yield break;
	}

	// Token: 0x06004EB4 RID: 20148 RVA: 0x0019EA6E File Offset: 0x0019CC6E
	protected IEnumerator PlayMissionFlavorLine(string speaker, string line, Vector3 quotePosition, Notification.SpeechBubbleDirection direction = Notification.SpeechBubbleDirection.None, float duration = 2.5f, bool persistCharacter = false)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayMissionFlavorLine), quotePosition, direction, duration, persistCharacter);
		yield break;
	}

	// Token: 0x06004EB5 RID: 20149 RVA: 0x0019EAAA File Offset: 0x0019CCAA
	protected IEnumerator PlayMissionFlavorLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayMissionFlavorLine), duration);
		yield break;
	}

	// Token: 0x06004EB6 RID: 20150 RVA: 0x0019EACE File Offset: 0x0019CCCE
	protected IEnumerator PlayAdventureFlavorLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayAdventureFlavorLine), duration);
		yield break;
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x0019EAF2 File Offset: 0x0019CCF2
	protected IEnumerator PlayAdventureFlavorLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayAdventureFlavorLine), duration);
		yield break;
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x0019EB16 File Offset: 0x0019CD16
	protected IEnumerator PlayClosingLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLittleCharacterLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayClosingLine), duration);
		yield break;
	}

	// Token: 0x06004EB9 RID: 20153 RVA: 0x0019EB3A File Offset: 0x0019CD3A
	protected IEnumerator PlayClosingLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayClosingLine), duration);
		yield break;
	}

	// Token: 0x06004EBA RID: 20154 RVA: 0x0019EB5E File Offset: 0x0019CD5E
	protected IEnumerator PlayEasterEggLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayEasterEggLine), duration);
		yield break;
	}

	// Token: 0x06004EBB RID: 20155 RVA: 0x0019EB82 File Offset: 0x0019CD82
	protected IEnumerator PlayEasterEggLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayEasterEggLine), duration);
		yield break;
	}

	// Token: 0x06004EBC RID: 20156 RVA: 0x0019EBA6 File Offset: 0x0019CDA6
	protected IEnumerator PlayCriticalLine(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayCriticalLine), duration);
		yield break;
	}

	// Token: 0x06004EBD RID: 20157 RVA: 0x0019EBCA File Offset: 0x0019CDCA
	protected IEnumerator PlayCriticalLine(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayCriticalLine), duration);
		yield break;
	}

	// Token: 0x06004EBE RID: 20158 RVA: 0x0019EBEE File Offset: 0x0019CDEE
	protected bool ShouldPlayClosingLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayClosingLine));
	}

	// Token: 0x06004EBF RID: 20159 RVA: 0x0019EC03 File Offset: 0x0019CE03
	protected bool ShouldPlayCriticalLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayCriticalLine));
	}

	// Token: 0x06004EC0 RID: 20160 RVA: 0x0019EC18 File Offset: 0x0019CE18
	protected bool ShouldPlayAdventureFlavorLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayAdventureFlavorLine));
	}

	// Token: 0x06004EC1 RID: 20161 RVA: 0x0019EC2D File Offset: 0x0019CE2D
	protected bool ShouldPlayMissionFlavorLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayMissionFlavorLine));
	}

	// Token: 0x06004EC2 RID: 20162 RVA: 0x0019EC42 File Offset: 0x0019CE42
	protected bool ShouldPlayBossLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine));
	}

	// Token: 0x06004EC3 RID: 20163 RVA: 0x0019EC57 File Offset: 0x0019CE57
	protected bool ShouldPlayEasterEggLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayEasterEggLine));
	}

	// Token: 0x06004EC4 RID: 20164 RVA: 0x0019EC6C File Offset: 0x0019CE6C
	protected bool ShouldPlayOpeningLine(string line)
	{
		return this.ShouldPlayLine(line, new MissionEntity.ShouldPlay(this.InternalShouldPlayOpeningLine));
	}

	// Token: 0x06004EC5 RID: 20165 RVA: 0x0019EC81 File Offset: 0x0019CE81
	protected IEnumerator PlayLineAlways(string speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004EC6 RID: 20166 RVA: 0x0019ECA5 File Offset: 0x0019CEA5
	protected IEnumerator PlayLineAlways(Actor speaker, string line, float duration = 2.5f)
	{
		yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		yield break;
	}

	// Token: 0x06004EC7 RID: 20167 RVA: 0x0019ECC9 File Offset: 0x0019CEC9
	protected IEnumerator PlayLineAlways(Actor speaker, string backupSpeaker, string line, float duration = 2.5f)
	{
		if (speaker == null)
		{
			yield return this.PlayLine(backupSpeaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		}
		else
		{
			yield return this.PlayLine(speaker, line, new MissionEntity.ShouldPlay(this.InternalShouldPlayBossLine), duration);
		}
		yield break;
	}

	// Token: 0x06004EC8 RID: 20168 RVA: 0x0019ECF5 File Offset: 0x0019CEF5
	public IEnumerator PlayLineInOrderOnce(Actor actor, List<string> lines)
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
			yield break;
		}
		this.m_InOrderPlayedLines.Add(text);
		yield return this.PlayLineAlways(actor, text, 2.5f);
		yield break;
	}

	// Token: 0x06004EC9 RID: 20169 RVA: 0x0019ED12 File Offset: 0x0019CF12
	public IEnumerator PlayLineInOrderOnce(string actor, List<string> lines)
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
			yield break;
		}
		this.m_InOrderPlayedLines.Add(text);
		yield return this.PlayLineAlways(actor, text, 2.5f);
		yield break;
	}

	// Token: 0x06004ECA RID: 20170 RVA: 0x00003BE8 File Offset: 0x00001DE8
	protected virtual void InitEmoteResponses()
	{
	}

	// Token: 0x06004ECB RID: 20171 RVA: 0x0019ED2F File Offset: 0x0019CF2F
	protected IEnumerator HandlePlayerEmoteWithTiming(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		while (emoteSpell.IsActive())
		{
			yield return null;
		}
		if (this.m_enemySpeaking)
		{
			yield break;
		}
		this.PlayEmoteResponse(emoteType, emoteSpell);
		yield break;
	}

	// Token: 0x06004ECC RID: 20172 RVA: 0x0019ED4C File Offset: 0x0019CF4C
	protected virtual void PlayEmoteResponse(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor actor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		foreach (MissionEntity.EmoteResponseGroup emoteResponseGroup in this.m_emoteResponseGroups)
		{
			if (emoteResponseGroup.m_responses.Count != 0 && emoteResponseGroup.m_triggers.Contains(emoteType))
			{
				this.PlayNextEmoteResponse(emoteResponseGroup, actor);
				this.CycleNextResponseGroupIndex(emoteResponseGroup);
			}
		}
	}

	// Token: 0x06004ECD RID: 20173 RVA: 0x0019EDD8 File Offset: 0x0019CFD8
	protected virtual IEnumerator PlayEmoteResponseWithTiming(EmoteType emoteType, CardSoundSpell emoteSpell)
	{
		Actor enemyActor = GameState.Get().GetOpposingSidePlayer().GetHeroCard().GetActor();
		if (enemyActor == null)
		{
			yield break;
		}
		foreach (MissionEntity.EmoteResponseGroup responseGroup in this.m_emoteResponseGroups)
		{
			if (responseGroup.m_responses.Count != 0 && responseGroup.m_triggers.Contains(emoteType))
			{
				int responseIndex = responseGroup.m_responseIndex;
				MissionEntity.EmoteResponse emoteResponse = responseGroup.m_responses[responseIndex];
				yield return this.PlaySoundAndBlockSpeechWithCustomGameString(emoteResponse.m_soundName, emoteResponse.m_stringTag, Notification.SpeechBubbleDirection.TopRight, enemyActor, 1f, true, false);
				this.CycleNextResponseGroupIndex(responseGroup);
				responseGroup = null;
			}
		}
		List<MissionEntity.EmoteResponseGroup>.Enumerator enumerator = default(List<MissionEntity.EmoteResponseGroup>.Enumerator);
		yield break;
		yield break;
	}

	// Token: 0x06004ECE RID: 20174 RVA: 0x0019EDF0 File Offset: 0x0019CFF0
	protected void PlayNextEmoteResponse(MissionEntity.EmoteResponseGroup responseGroup, Actor actor)
	{
		int responseIndex = responseGroup.m_responseIndex;
		MissionEntity.EmoteResponse emoteResponse = responseGroup.m_responses[responseIndex];
		GameEntity.Coroutines.StartCoroutine(this.PlaySoundAndBlockSpeechWithCustomGameString(emoteResponse.m_soundName, emoteResponse.m_stringTag, Notification.SpeechBubbleDirection.TopRight, actor, 1f, true, false));
	}

	// Token: 0x06004ECF RID: 20175 RVA: 0x0019EE37 File Offset: 0x0019D037
	protected virtual void CycleNextResponseGroupIndex(MissionEntity.EmoteResponseGroup responseGroup)
	{
		if (responseGroup.m_responseIndex == responseGroup.m_responses.Count - 1)
		{
			responseGroup.m_responseIndex = 0;
			return;
		}
		responseGroup.m_responseIndex++;
	}

	// Token: 0x04004528 RID: 17704
	private static Map<GameEntityOption, bool> s_booleanOptions = MissionEntity.InitBooleanOptions();

	// Token: 0x04004529 RID: 17705
	private static Map<GameEntityOption, string> s_stringOptions = MissionEntity.InitStringOptions();

	// Token: 0x0400452A RID: 17706
	protected const float TIME_TO_WAIT_BEFORE_ENDING_QUOTE = 5f;

	// Token: 0x0400452B RID: 17707
	protected const float MINIMUM_DISPLAY_TIME_FOR_BIG_QUOTE = 3f;

	// Token: 0x0400452C RID: 17708
	protected const float DEFAULT_VO_DURATION = 2.5f;

	// Token: 0x0400452D RID: 17709
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

	// Token: 0x0400452E RID: 17710
	protected bool m_enemySpeaking;

	// Token: 0x0400452F RID: 17711
	protected List<MissionEntity.EmoteResponseGroup> m_emoteResponseGroups = new List<MissionEntity.EmoteResponseGroup>();

	// Token: 0x04004530 RID: 17712
	public bool m_forceAlwaysPlayLine;

	// Token: 0x04004531 RID: 17713
	private HashSet<string> m_InOrderPlayedLines = new HashSet<string>();

	// Token: 0x02001EE9 RID: 7913
	protected class EmoteResponse
	{
		// Token: 0x0400D66D RID: 54893
		public string m_soundName;

		// Token: 0x0400D66E RID: 54894
		public string m_stringTag;
	}

	// Token: 0x02001EEA RID: 7914
	protected class EmoteResponseGroup
	{
		// Token: 0x0400D66F RID: 54895
		public List<EmoteType> m_triggers = new List<EmoteType>();

		// Token: 0x0400D670 RID: 54896
		public List<MissionEntity.EmoteResponse> m_responses = new List<MissionEntity.EmoteResponse>();

		// Token: 0x0400D671 RID: 54897
		public int m_responseIndex;
	}

	// Token: 0x02001EEB RID: 7915
	protected enum ShouldPlayValue
	{
		// Token: 0x0400D673 RID: 54899
		Never,
		// Token: 0x0400D674 RID: 54900
		Once,
		// Token: 0x0400D675 RID: 54901
		Always
	}

	// Token: 0x02001EEC RID: 7916
	// (Invoke) Token: 0x06011569 RID: 71017
	protected delegate MissionEntity.ShouldPlayValue ShouldPlay();
}
