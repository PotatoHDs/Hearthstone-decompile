using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone;
using PegasusShared;
using UnityEngine;

public class NarrativeManager : MonoBehaviour
{
	public delegate void CharacterQuotePlayedCallback();

	private const float DELAY_TIME_FOR_QUEST_PROGRESS = 1.5f;

	private const float DELAY_TIME_FOR_QUEST_COMPLETE = 1.5f;

	private const float DELAY_TIME_FOR_AUTO_DESTROY_QUEST_RECEIVED = 3.8f;

	private const float DELAY_TIME_BEFORE_QUEST_DESTROY = 0.8f;

	private const float DELAY_TIME_FOR_AUTO_DESTROY_POST_DESTROY = 1.3f;

	private const float DELAY_TIME_BEFORE_SHOW_BANNER = 1f;

	private const float FALLBACK_DURATION_ON_AUDIO_LOADING_FAIL = 3.5f;

	private static NarrativeManager s_instance;

	private Map<string, AudioSource> m_preloadedSounds = new Map<string, AudioSource>();

	private int m_preloadsNeeded;

	private Queue<CharacterDialogSequence> m_characterDialogSequenceToShow = new Queue<CharacterDialogSequence>();

	private Notification m_activeCharacterDialogNotification;

	private bool m_isBannerShowing;

	private bool m_showingBlockingDialog;

	private bool m_isProcessingQueuedDialogSequence;

	private bool m_hasDoneAllPopupsShownEvent;

	private static Map<ScheduledCharacterDialogEvent, Option> m_lastSeenScheduledCharacterDialogOptions = new Map<ScheduledCharacterDialogEvent, Option>
	{
		{
			ScheduledCharacterDialogEvent.DOUBLE_GOLD_QUEST_GRANTED,
			Option.LATEST_SEEN_SCHEDULED_DOUBLE_GOLD_VO
		},
		{
			ScheduledCharacterDialogEvent.ALL_POPUPS_SHOWN,
			Option.LATEST_SEEN_SCHEDULED_ALL_POPUPS_SHOWN_VO
		},
		{
			ScheduledCharacterDialogEvent.ENTERED_ARENA_DRAFT,
			Option.LATEST_SEEN_SCHEDULED_ENTERED_ARENA_DRAFT
		},
		{
			ScheduledCharacterDialogEvent.LOGIN_FLOW_COMPLETE,
			Option.LATEST_SEEN_SCHEDULED_LOGIN_FLOW_COMPLETE
		},
		{
			ScheduledCharacterDialogEvent.WELCOME_QUESTS_SHOWN,
			Option.LATEST_SEEN_SCHEDULED_WELCOME_QUEST_SHOWN_VO
		},
		{
			ScheduledCharacterDialogEvent.GENERIC_REWARD_SHOWN,
			Option.LATEST_SEEN_SCHEDULED_GENERIC_REWARD_SHOWN_VO
		},
		{
			ScheduledCharacterDialogEvent.ARENA_REWARD_SHOWN,
			Option.LATEST_SEEN_SCHEDULED_ARENA_REWARD_SHOWN_VO
		}
	};

	private static Map<ScheduledCharacterDialogEvent, GameSaveDataManager.GameSaveKeyTuple> m_lastSeenScheduledCharacterDialogKeys;

	private Map<ScheduledCharacterDialogEvent, List<ScheduledCharacterDialogDbfRecord>> m_scheduledCharacterDialogData = new Map<ScheduledCharacterDialogEvent, List<ScheduledCharacterDialogDbfRecord>>();

	public void Awake()
	{
		s_instance = this;
	}

	public void Update()
	{
		if (m_showingBlockingDialog)
		{
			OverlayUI.Get().RequestActivateClickBlocker();
		}
	}

	public void OnDestroy()
	{
		if (s_instance != null)
		{
			CleanUpEverything();
			s_instance = null;
		}
	}

	public static NarrativeManager Get()
	{
		return s_instance;
	}

	public void Initialize()
	{
		HearthstoneApplication.Get().WillReset += WillReset;
		SceneMgr.Get().RegisterScenePreLoadEvent(OnScenePreLoad);
		PopupDisplayManager.Get().RegisterCompletedQuestShownListener(s_instance.OnQuestCompleteShown);
		PopupDisplayManager.Get().RegisterGenericRewardShownListener(s_instance.OnGenericRewardShown);
		StoreManager.Get().RegisterSuccessfulPurchaseListener(OnBundlePurchased);
		StartCoroutine(WaitForAchievesThenInit());
	}

	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			CleanUpExceptListeners();
		}
	}

	public void OnQuestCompleteShown(int achieveId)
	{
		Achievement achievement = AchieveManager.Get().GetAchievement(achieveId);
		if (achievement.QuestDialogId != 0 && achievement.OnCompleteDialogSequence != null)
		{
			if (achievement.OnCompleteDialogSequence.m_deferOnComplete)
			{
				EnqueueIfNotPresent(achievement.OnCompleteDialogSequence);
			}
			else
			{
				PushDialogSequence(achievement.OnCompleteDialogSequence);
			}
		}
	}

	public void OnGenericRewardShown(long originData)
	{
		TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.GENERIC_REWARD_SHOWN, (int)originData);
	}

	private void EnqueueIfNotPresent(CharacterDialogSequence sequence)
	{
		foreach (CharacterDialogSequence item in m_characterDialogSequenceToShow)
		{
			if (item.m_characterDialogRecord == sequence.m_characterDialogRecord)
			{
				return;
			}
		}
		m_characterDialogSequenceToShow.Enqueue(sequence);
	}

	public void ShowOutstandingQuestDialogs()
	{
		StartCoroutine(ShowOutstandingCharacterDialogSequence());
	}

	public void OnWelcomeQuestsShown(List<Achievement> questsShown, List<Achievement> newlyAvailableQuests)
	{
		TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.WELCOME_QUESTS_SHOWN, 0L);
		bool flag = SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, activeIfDoesNotExist: false);
		foreach (Achievement item in questsShown)
		{
			if (item.AutoDestroy)
			{
				StartCoroutine(DestroyAndReplaceQuest(item));
				break;
			}
			if (item.QuestDialogId != 0)
			{
				StartCoroutine(HandleQuestReceived(item));
				break;
			}
			if (flag && item.IsAffectedByDoubleGold && newlyAvailableQuests.Contains(item) && !AchieveManager.Get().HasActiveAutoDestroyQuests() && !AchieveManager.Get().HasActiveUnseenWelcomeQuestDialog() && OnDoubleGoldQuestGranted())
			{
				break;
			}
		}
	}

	public bool HasCharacterDialogSequenceToShow()
	{
		return m_characterDialogSequenceToShow.Count > 0;
	}

	public bool IsShowingBlockingDialog()
	{
		return m_showingBlockingDialog;
	}

	public void PushDialogSequence(CharacterDialogSequence sequence)
	{
		EnqueueIfNotPresent(sequence);
		StartCoroutine(ShowOutstandingCharacterDialogSequence());
	}

	public IEnumerator<IAsyncJobResult> Job_WaitForOutstandingCharacterDialog()
	{
		StartCoroutine(ShowOutstandingCharacterDialogSequence());
		while (m_isProcessingQueuedDialogSequence)
		{
			yield return null;
		}
	}

	public IEnumerator ShowOutstandingCharacterDialogSequence()
	{
		if (m_characterDialogSequenceToShow.Count == 0 || m_isProcessingQueuedDialogSequence)
		{
			yield break;
		}
		m_isProcessingQueuedDialogSequence = true;
		yield return new WaitForSeconds(1.5f);
		int bannerIDToShow = 0;
		while (m_characterDialogSequenceToShow.Count > 0)
		{
			CharacterDialogSequence characterDialogSequence = m_characterDialogSequenceToShow.Peek();
			SetDialogBlocker(characterDialogSequence.m_blockInput);
			if (characterDialogSequence != null && characterDialogSequence.m_onCompleteBannerId != 0)
			{
				bannerIDToShow = characterDialogSequence.m_onCompleteBannerId;
			}
			if (characterDialogSequence.m_onPreShow != null)
			{
				characterDialogSequence.m_onPreShow(characterDialogSequence);
			}
			yield return StartCoroutine(PlayerCharacterDialogSequence(characterDialogSequence));
			m_characterDialogSequenceToShow.Dequeue();
		}
		if (bannerIDToShow != 0)
		{
			yield return new WaitForSeconds(1f);
			m_isBannerShowing = true;
			BannerManager.Get().ShowBanner(bannerIDToShow, OnQuestDialogCompleteBannerClosed);
		}
		SetDialogBlocker(value: false);
		while (m_isBannerShowing)
		{
			yield return null;
		}
		m_isProcessingQueuedDialogSequence = false;
	}

	public bool OnDoubleGoldQuestGranted()
	{
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.DOUBLE_GOLD_QUEST_GRANTED, 0L);
	}

	public bool OnAllPopupsShown()
	{
		if (m_hasDoneAllPopupsShownEvent)
		{
			return false;
		}
		m_hasDoneAllPopupsShownEvent = true;
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ALL_POPUPS_SHOWN, 0L);
	}

	public bool OnArenaDraftStarted()
	{
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_ARENA_DRAFT, 0L);
	}

	public bool OnArenaRewardsShown()
	{
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ARENA_REWARD_SHOWN, 0L);
	}

	public void OnLoginFlowComplete()
	{
		TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.LOGIN_FLOW_COMPLETE, 0L);
	}

	public bool OnBattlegroundsEntered()
	{
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_BATTLEGROUNDS, 0L);
	}

	public bool OnTavernBrawlEntered()
	{
		return TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_TAVERN_BRAWL, 0L);
	}

	public void OnBundlePurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle != null)
		{
			TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.PURCHASED_BUNDLE, bundle.PMTProductID.Value);
		}
	}

	private void SetDialogBlocker(bool value)
	{
		m_showingBlockingDialog = value;
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().UpdateMyAvailability();
		}
	}

	private void OnQuestDialogCompleteBannerClosed()
	{
		m_isBannerShowing = false;
	}

	private IEnumerator WaitForAchievesThenInit()
	{
		while (AchieveManager.Get() == null)
		{
			yield return null;
		}
		while (!AchieveManager.Get().IsReady())
		{
			yield return null;
		}
		PreloadActiveQuestDialog();
		InitScheduledCharacterDialogData();
		AchieveManager.Get().RegisterAchievesUpdatedListener(s_instance.OnAchievesUpdated);
		GameToastMgr.Get().RegisterQuestProgressToastShownListener(s_instance.OnQuestProgressToastShown);
		TavernBrawlManager.Get().OnTavernBrawlUpdated += s_instance.OnTavernBrawlUpdated;
	}

	private IEnumerator DestroyAndReplaceQuest(Achievement quest)
	{
		yield return new WaitForSeconds(3.8f);
		SoundDucker ducker = base.gameObject.AddComponent<SoundDucker>();
		ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (Global.SoundCategory value in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			if (value == Global.SoundCategory.AMBIENCE || value == Global.SoundCategory.MUSIC)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				soundDuckedCategoryDef.m_Category = value;
				soundDuckedCategoryDef.m_BeginSec = 0f;
				ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
		ducker.StartDucking();
		if (quest.QuestDialogId != 0)
		{
			foreach (CharacterDialog dialog2 in quest.OnReceivedDialogSequence)
			{
				if (IsCharacterDialogDisplayable(dialog2))
				{
					yield return new WaitForSeconds(dialog2.waitBefore);
					yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog2));
					yield return new WaitForSeconds(dialog2.waitAfter);
				}
			}
		}
		yield return new WaitForSeconds(0.8f);
		int nextQuestId = WelcomeQuests.Get().CompleteAndReplaceAutoDestroyQuestTile(quest.ID);
		yield return new WaitForSeconds(1.3f);
		Achievement achievement = AchieveManager.Get().GetAchievement(nextQuestId);
		if (achievement.QuestDialogId != 0)
		{
			int numLinesToPlay = achievement.OnReceivedDialogSequence.Count;
			foreach (CharacterDialog dialog2 in achievement.OnReceivedDialogSequence)
			{
				numLinesToPlay--;
				if (IsCharacterDialogDisplayable(dialog2))
				{
					yield return new WaitForSeconds(dialog2.waitBefore);
					if (numLinesToPlay == 0)
					{
						yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog2, OnWelcomeQuestNarrativeFinished));
					}
					else
					{
						yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog2));
					}
					yield return new WaitForSeconds(dialog2.waitAfter);
				}
			}
		}
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
	}

	private IEnumerator HandleQuestReceived(Achievement quest)
	{
		int numLinesToPlay = quest.OnReceivedDialogSequence.Count;
		if (Options.Get().GetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG) == quest.ID || numLinesToPlay <= 0)
		{
			OnWelcomeQuestNarrativeFinished();
			yield break;
		}
		SoundDucker ducker = base.gameObject.AddComponent<SoundDucker>();
		ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (Global.SoundCategory value in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			if (value == Global.SoundCategory.AMBIENCE || value == Global.SoundCategory.MUSIC)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				soundDuckedCategoryDef.m_Category = value;
				soundDuckedCategoryDef.m_BeginSec = 0f;
				ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
		ducker.StartDucking();
		foreach (CharacterDialog dialog in quest.OnReceivedDialogSequence)
		{
			numLinesToPlay--;
			if (IsCharacterDialogDisplayable(dialog))
			{
				yield return new WaitForSeconds(dialog.waitBefore);
				if (numLinesToPlay == 0)
				{
					yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog, OnWelcomeQuestNarrativeFinished));
				}
				else
				{
					yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog));
				}
				yield return new WaitForSeconds(dialog.waitAfter);
			}
		}
		Options.Get().SetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG, quest.ID);
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
	}

	private IEnumerator PlayCharacterQuoteAndWait(CharacterDialog dialog, CharacterQuotePlayedCallback callback = null, float waitTimeScale = 1f)
	{
		float minimumDurationSeconds = dialog.minimumDurationSeconds;
		if (Localization.DoesLocaleNeedExtraReadingTime(Localization.GetLocale()))
		{
			minimumDurationSeconds += dialog.localeExtraSeconds;
		}
		AudioSource audioSource = null;
		bool noSoundSpecified = string.IsNullOrEmpty(dialog.audioName);
		if (!noSoundSpecified)
		{
			audioSource = GetPreloadedSound(dialog.audioName);
			if (audioSource == null || audioSource.clip == null)
			{
				RemovePreloadedSound(dialog.audioName);
				PreloadSound(dialog.audioName);
				while (IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = GetPreloadedSound(dialog.audioName);
				if (audioSource == null || audioSource.clip == null)
				{
					Debug.Log("NarrativeManager.PlaySoundAndWait() - sound error - " + dialog.audioName);
					yield break;
				}
			}
		}
		float num = minimumDurationSeconds;
		if (audioSource != null)
		{
			num = Mathf.Max(minimumDurationSeconds, audioSource.clip.length);
		}
		else if (audioSource == null && !noSoundSpecified)
		{
			num = 3.5f;
		}
		Log.NarrativeManager.Print("PlayCharacterQuoteAndWait - durationSeconds: {0}  waitTimeScale: {1}", num, waitTimeScale);
		if (dialog.useInnkeeperQuote)
		{
			m_activeCharacterDialogNotification = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, dialog.bubbleText.GetString(), dialog.audioName, null);
			m_activeCharacterDialogNotification.ShowWithExistingPopups = true;
		}
		else
		{
			m_activeCharacterDialogNotification = NotificationManager.Get().CreateBigCharacterQuoteWithText(dialog.prefabName, NotificationManager.DEFAULT_CHARACTER_POS, dialog.audioName, dialog.bubbleText.GetString(), num, null, useOverlayUI: true, dialog.useAltSpeechBubble ? Notification.SpeechBubbleDirection.TopLeft : Notification.SpeechBubbleDirection.BottomLeft, dialog.persistPrefab, dialog.useAltPosition);
		}
		float num2 = num * waitTimeScale;
		num2 += 0.5f;
		yield return new WaitForSeconds(num2);
		callback?.Invoke();
	}

	private void OnAchievesUpdated(List<Achievement> updatedAchieves, List<Achievement> completedAchieves, object userData)
	{
		List<Achievement> activeQuests = AchieveManager.Get().GetActiveQuests();
		PreloadQuestDialog(activeQuests);
	}

	private void OnQuestProgressToastShown(int achieveId)
	{
		StartCoroutine(HandleOnQuestProgressToastShown(achieveId));
	}

	private void OnTavernBrawlUpdated()
	{
		if (!TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			return;
		}
		foreach (TavernBrawlMission mission in TavernBrawlManager.Get().Missions)
		{
			if (mission.FirstTimeSeenCharacterDialogID > 0)
			{
				PreloadQuestDialog(mission.FirstTimeSeenCharacterDialogSequence);
			}
		}
	}

	private IEnumerator HandleOnQuestProgressToastShown(int achieveId)
	{
		yield return new WaitForSeconds(1.5f);
		Achievement achievement = AchieveManager.Get().GetAchievement(achieveId);
		if ((achievement?.QuestDialogId ?? 0) != 0)
		{
			if (achievement.Progress == 1)
			{
				yield return PlayerCharacterDialogSequence(achievement.OnProgress1DialogSequence);
			}
			else if (achievement.Progress == 2)
			{
				yield return PlayerCharacterDialogSequence(achievement.OnProgress2DialogSequence);
			}
		}
	}

	public void OnAchieveDismissed(Achievement achieve)
	{
		if (achieve.OnDismissDialogSequence != null)
		{
			StartCoroutine(PlayerCharacterDialogSequence(achieve.OnDismissDialogSequence));
		}
	}

	private static bool IsCharacterDialogDisplayable(CharacterDialog dialog)
	{
		if (dialog.useInnkeeperQuote)
		{
			return true;
		}
		if (!string.IsNullOrEmpty(dialog.prefabName))
		{
			return true;
		}
		Log.All.Print("CharacterDialogItem id={0} is not displayable. To be displayable, either USE_INNKEEPER_QUOTE must be true or PREFAB_NAME is not null/empty.", dialog.dbfRecordId);
		return false;
	}

	private IEnumerator PlayerCharacterDialogSequence(CharacterDialogSequence dialogSequence)
	{
		if (dialogSequence == null)
		{
			yield break;
		}
		if (!dialogSequence.m_ignorePopups)
		{
			yield return StartCoroutine(PopupDisplayManager.Get().WaitForAllPopups());
		}
		foreach (CharacterDialog dialog in dialogSequence)
		{
			if (IsCharacterDialogDisplayable(dialog))
			{
				yield return new WaitForSeconds(dialog.waitBefore);
				yield return StartCoroutine(PlayCharacterQuoteAndWait(dialog));
				yield return new WaitForSeconds(dialog.waitAfter);
			}
		}
	}

	private void OnWelcomeQuestNarrativeFinished()
	{
		if (WelcomeQuests.Get() != null)
		{
			WelcomeQuests.Get().ActivateClickCatcher();
		}
	}

	private void PreloadActiveQuestDialog()
	{
		PreloadQuestDialog(AchieveManager.Get().GetActiveQuests());
	}

	private void PreloadQuestDialog(Achievement achievement)
	{
		if (achievement.QuestDialogId != 0)
		{
			PreloadQuestDialog(achievement.OnReceivedDialogSequence);
			PreloadQuestDialog(achievement.OnCompleteDialogSequence);
			PreloadQuestDialog(achievement.OnProgress1DialogSequence);
			PreloadQuestDialog(achievement.OnProgress2DialogSequence);
			PreloadQuestDialog(achievement.OnDismissDialogSequence);
		}
	}

	private void PreloadQuestDialog(List<Achievement> activeAchievements)
	{
		foreach (Achievement activeAchievement in activeAchievements)
		{
			PreloadQuestDialog(activeAchievement);
		}
	}

	private void PreloadQuestDialog(CharacterDialogSequence questDialogSequence)
	{
		foreach (CharacterDialog item in questDialogSequence)
		{
			if (!string.IsNullOrEmpty(item.audioName))
			{
				PreloadSound(item.audioName);
			}
		}
	}

	private void PreloadQuestDialog(List<string> audioNames)
	{
		foreach (string audioName in audioNames)
		{
			if (!string.IsNullOrEmpty(audioName))
			{
				PreloadSound(audioName);
			}
		}
	}

	private void PreloadSound(string soundPath)
	{
		if (!CheckPreloadedSound(soundPath))
		{
			m_preloadsNeeded++;
			SoundLoader.LoadSound(soundPath, OnSoundLoaded, null, SoundManager.Get().GetPlaceholderSound());
		}
	}

	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_preloadsNeeded--;
		if (go == null)
		{
			Debug.LogWarning($"NarrativeManager.OnSoundLoaded() - FAILED to load \"{assetRef}\"");
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			Debug.LogWarning($"NarrativeManager.OnSoundLoaded() - ERROR \"{assetRef}\" has no Spell component");
		}
		else if (!CheckPreloadedSound(assetRef.ToString()))
		{
			m_preloadedSounds.Add(assetRef.ToString(), component);
		}
	}

	private void RemovePreloadedSound(string soundPath)
	{
		m_preloadedSounds.Remove(soundPath);
	}

	private bool CheckPreloadedSound(string soundPath)
	{
		AudioSource value;
		return m_preloadedSounds.TryGetValue(soundPath, out value);
	}

	private AudioSource GetPreloadedSound(string soundPath)
	{
		if (m_preloadedSounds.TryGetValue(soundPath, out var value))
		{
			return value;
		}
		Debug.LogError($"NarrativeManager.GetPreloadedSound() - \"{soundPath}\" was not preloaded");
		return null;
	}

	private bool IsPreloadingAssets()
	{
		return m_preloadsNeeded > 0;
	}

	private void SetLastSeenScheduledCharacterDialog(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		if (eventType == ScheduledCharacterDialogEvent.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.SetLastSeenScheduledCharacterDialog was passed an INVALID ScheduledCharacterDialogEvent");
			return;
		}
		if (m_lastSeenScheduledCharacterDialogKeys.ContainsKey(eventType))
		{
			SetLastSeenScheduledCharacterDialog_GameSaveData(scheduledDialogId, eventType);
			return;
		}
		if (m_lastSeenScheduledCharacterDialogOptions.ContainsKey(eventType))
		{
			SetLastSeenScheduledCharacterDialog_ServerOption(scheduledDialogId, eventType);
			return;
		}
		Log.NarrativeManager.PrintError("NarrativeManager has no storage mechanism for event {0}", eventType.ToString());
	}

	private int GetLastSeenScheduledCharacterDialog(ScheduledCharacterDialogEvent eventType)
	{
		if (eventType == ScheduledCharacterDialogEvent.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.GetLastSeenScheduledCharacterDialog was passed an INVALID ScheduledCharacterDialogEvent");
			return -1;
		}
		if (m_lastSeenScheduledCharacterDialogKeys.ContainsKey(eventType))
		{
			return GetLastSeenScheduledCharacterDialog_GameSaveData(eventType);
		}
		if (m_lastSeenScheduledCharacterDialogOptions.ContainsKey(eventType))
		{
			return GetLastSeenScheduledCharacterDialog_ServerOption(eventType);
		}
		Log.NarrativeManager.PrintError("NarrativeManager has no storage mechanism for event {0}", eventType.ToString());
		return -1;
	}

	private void SetLastSeenScheduledCharacterDialog_ServerOption(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		m_lastSeenScheduledCharacterDialogOptions.TryGetValue(eventType, out var value);
		if (value == Option.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.SetLastSeenScheduledCharacterDialog option mapping had no corresponding option for event: {0}", eventType);
		}
		else
		{
			Options.Get().SetInt(value, scheduledDialogId);
		}
	}

	private int GetLastSeenScheduledCharacterDialog_ServerOption(ScheduledCharacterDialogEvent eventType)
	{
		m_lastSeenScheduledCharacterDialogOptions.TryGetValue(eventType, out var value);
		if (value == Option.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.GetLastSeenScheduledCharacterDialog option mapping had no corresponding option for event: {0}", eventType);
			return -1;
		}
		return Options.Get().GetInt(value);
	}

	private void SetLastSeenScheduledCharacterDialog_GameSaveData(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(m_lastSeenScheduledCharacterDialogKeys[eventType].Key, m_lastSeenScheduledCharacterDialogKeys[eventType].Subkey, scheduledDialogId));
	}

	private int GetLastSeenScheduledCharacterDialog_GameSaveData(ScheduledCharacterDialogEvent eventType)
	{
		GameSaveDataManager.Get().GetSubkeyValue(m_lastSeenScheduledCharacterDialogKeys[eventType].Key, m_lastSeenScheduledCharacterDialogKeys[eventType].Subkey, out long value);
		return (int)value;
	}

	private void InitScheduledCharacterDialogData()
	{
		foreach (ScheduledCharacterDialogDbfRecord record in GameDbf.ScheduledCharacterDialog.GetRecords())
		{
			if (!GeneralUtils.ForceBool(record.Enabled))
			{
				continue;
			}
			if (!string.IsNullOrEmpty(record.Event))
			{
				SpecialEventType eventType = SpecialEventManager.GetEventType(record.Event);
				if (eventType == SpecialEventType.UNKNOWN)
				{
					Log.All.PrintError("NarrativeManager.InitScheduledCharacterDialogData: unknown event=\"{0}\" for ScheduledCharacterDialogDbfRecord id={1}", record.Event, record.ID);
					continue;
				}
				if (SpecialEventManager.Get().HasEventEnded(eventType))
				{
					continue;
				}
			}
			if ((!record.ShowToNewPlayer && !AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY)) || (!record.ShowToReturningPlayer && ReturningPlayerMgr.Get().IsInReturningPlayerMode))
			{
				continue;
			}
			ScheduledCharacterDialogEvent @enum = EnumUtils.GetEnum<ScheduledCharacterDialogEvent>(record.ClientEvent.ToString(), StringComparison.OrdinalIgnoreCase);
			if (GetLastSeenScheduledCharacterDialogDisplayOrder(@enum) < record.DisplayOrder)
			{
				if (!m_scheduledCharacterDialogData.ContainsKey(@enum))
				{
					m_scheduledCharacterDialogData[@enum] = new List<ScheduledCharacterDialogDbfRecord>();
				}
				PreloadQuestDialog(CharacterDialogSequence.GetAudioOfCharacterDialogSequence(record.CharacterDialogId));
				m_scheduledCharacterDialogData[@enum].Add(record);
			}
		}
	}

	private int GetLastSeenScheduledCharacterDialogDisplayOrder(ScheduledCharacterDialogEvent dialogEvent)
	{
		int lastSeenScheduledCharacterDialog = GetLastSeenScheduledCharacterDialog(dialogEvent);
		int result = -1;
		ScheduledCharacterDialogDbfRecord record = GameDbf.ScheduledCharacterDialog.GetRecord(lastSeenScheduledCharacterDialog);
		if (record != null)
		{
			result = record.DisplayOrder;
		}
		return result;
	}

	public void ResetScheduledCharacterDialogEvent_Debug()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		foreach (ScheduledCharacterDialogEvent value in Enum.GetValues(typeof(ScheduledCharacterDialogEvent)))
		{
			if (value != 0)
			{
				SetLastSeenScheduledCharacterDialog(0, value);
			}
		}
		InitScheduledCharacterDialogData();
	}

	public bool TriggerScheduledCharacterDialogEvent_Debug(ScheduledCharacterDialogEvent eventType)
	{
		if (HearthstoneApplication.IsPublic())
		{
			return false;
		}
		return TriggerScheduledCharacterDialogEvent(eventType, 0L);
	}

	private bool TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent eventType, long eventData = 0L)
	{
		if (!m_scheduledCharacterDialogData.ContainsKey(eventType))
		{
			return false;
		}
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			return false;
		}
		ScheduledCharacterDialogDbfRecord recordToUse = null;
		int lastSeenScheduledCharacterDialogDisplayOrder = GetLastSeenScheduledCharacterDialogDisplayOrder(eventType);
		foreach (ScheduledCharacterDialogDbfRecord item in m_scheduledCharacterDialogData[eventType])
		{
			if (!string.IsNullOrEmpty(item.Event))
			{
				SpecialEventType eventType2 = SpecialEventManager.GetEventType(item.Event);
				if (eventType2 == SpecialEventType.UNKNOWN)
				{
					Log.All.PrintError("NarrativeManager.TriggerScheduledCharacterDialogEvent: unknown event=\"{0}\" for ScheduledCharacterDialogDbfRecord id={1}", item.Event, item.ID);
					continue;
				}
				if (!SpecialEventManager.Get().IsEventActive(eventType2, activeIfDoesNotExist: false))
				{
					continue;
				}
			}
			if ((eventData == 0L || eventData == item.ClientEventData) && item.DisplayOrder > lastSeenScheduledCharacterDialogDisplayOrder && (recordToUse == null || recordToUse.DisplayOrder > item.DisplayOrder))
			{
				recordToUse = item;
			}
		}
		if (recordToUse == null)
		{
			return false;
		}
		CharacterDialogSequence characterDialogSequence = new CharacterDialogSequence(recordToUse.CharacterDialogId);
		if (characterDialogSequence == null)
		{
			return false;
		}
		characterDialogSequence.m_onPreShow = delegate
		{
			SetLastSeenScheduledCharacterDialog(recordToUse.ID, eventType);
		};
		PushDialogSequence(characterDialogSequence);
		return true;
	}

	private void WillReset()
	{
		CleanUpEverything();
	}

	private void CleanUpEverything()
	{
		CleanUpExceptListeners();
		if (HearthstoneServices.TryGet<AchieveManager>(out var service))
		{
			service.RemoveAchievesUpdatedListener(OnAchievesUpdated);
		}
		if (GameToastMgr.Get() != null)
		{
			GameToastMgr.Get().RemoveQuestProgressToastShownListener(OnQuestProgressToastShown);
		}
		if (HearthstoneServices.TryGet<PopupDisplayManager>(out var service2))
		{
			service2.RemoveCompletedQuestShownListener(OnQuestCompleteShown);
		}
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out var service3))
		{
			service3.OnTavernBrawlUpdated -= OnTavernBrawlUpdated;
		}
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= WillReset;
		}
		if (HearthstoneServices.TryGet<SceneMgr>(out var service4))
		{
			service4.UnregisterScenePreLoadEvent(OnScenePreLoad);
		}
		if (HearthstoneServices.TryGet<LoginManager>(out var service5))
		{
			service5.OnFullLoginFlowComplete -= OnLoginFlowComplete;
		}
		if (StoreManager.Get() != null)
		{
			StoreManager.Get().RemoveSuccessfulPurchaseListener(OnBundlePurchased);
		}
	}

	private void CleanUpExceptListeners()
	{
		StopAllCoroutines();
		m_characterDialogSequenceToShow.Clear();
		m_preloadedSounds.Clear();
		if (NotificationManager.Get() != null && m_activeCharacterDialogNotification != null)
		{
			NotificationManager.Get().DestroyNotification(m_activeCharacterDialogNotification, 0f);
		}
		m_preloadsNeeded = 0;
		m_isBannerShowing = false;
		m_showingBlockingDialog = false;
		m_isProcessingQueuedDialogSequence = false;
		m_hasDoneAllPopupsShownEvent = false;
	}

	public List<Option> Cheat_ClearAllSeen()
	{
		List<Option> list = new List<Option>();
		list.AddRange(m_lastSeenScheduledCharacterDialogOptions.Values);
		list.Add(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG);
		list.Add(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD);
		list.Add(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD);
		list.Add(Option.LATEST_SEEN_TAVERNBRAWL_SEASON);
		list.Add(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON);
		foreach (Option item in list)
		{
			Options.Get().DeleteOption(item);
		}
		return list;
	}

	static NarrativeManager()
	{
		Map<ScheduledCharacterDialogEvent, GameSaveDataManager.GameSaveKeyTuple> map = new Map<ScheduledCharacterDialogEvent, GameSaveDataManager.GameSaveKeyTuple>();
		GameSaveDataManager.GameSaveKeyTuple value = new GameSaveDataManager.GameSaveKeyTuple
		{
			Key = GameSaveKeyId.CHARACTER_DIALOG,
			Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_BACON
		};
		map.Add(ScheduledCharacterDialogEvent.ENTERED_BATTLEGROUNDS, value);
		value = new GameSaveDataManager.GameSaveKeyTuple
		{
			Key = GameSaveKeyId.CHARACTER_DIALOG,
			Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_TAVERN_BRAWL
		};
		map.Add(ScheduledCharacterDialogEvent.ENTERED_TAVERN_BRAWL, value);
		value = new GameSaveDataManager.GameSaveKeyTuple
		{
			Key = GameSaveKeyId.CHARACTER_DIALOG,
			Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_PURCHASED_BUNDLE
		};
		map.Add(ScheduledCharacterDialogEvent.PURCHASED_BUNDLE, value);
		m_lastSeenScheduledCharacterDialogKeys = map;
	}
}
