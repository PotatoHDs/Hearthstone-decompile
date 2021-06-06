using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Core;
using Blizzard.T5.Jobs;
using Hearthstone;
using PegasusShared;
using UnityEngine;

// Token: 0x020005FD RID: 1533
public class NarrativeManager : MonoBehaviour
{
	// Token: 0x0600536F RID: 21359 RVA: 0x001B461C File Offset: 0x001B281C
	public void Awake()
	{
		NarrativeManager.s_instance = this;
	}

	// Token: 0x06005370 RID: 21360 RVA: 0x001B4624 File Offset: 0x001B2824
	public void Update()
	{
		if (this.m_showingBlockingDialog)
		{
			OverlayUI.Get().RequestActivateClickBlocker();
		}
	}

	// Token: 0x06005371 RID: 21361 RVA: 0x001B4638 File Offset: 0x001B2838
	public void OnDestroy()
	{
		if (NarrativeManager.s_instance != null)
		{
			this.CleanUpEverything();
			NarrativeManager.s_instance = null;
		}
	}

	// Token: 0x06005372 RID: 21362 RVA: 0x001B4653 File Offset: 0x001B2853
	public static NarrativeManager Get()
	{
		return NarrativeManager.s_instance;
	}

	// Token: 0x06005373 RID: 21363 RVA: 0x001B465C File Offset: 0x001B285C
	public void Initialize()
	{
		HearthstoneApplication.Get().WillReset += this.WillReset;
		SceneMgr.Get().RegisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		PopupDisplayManager.Get().RegisterCompletedQuestShownListener(new Action<int>(NarrativeManager.s_instance.OnQuestCompleteShown));
		PopupDisplayManager.Get().RegisterGenericRewardShownListener(new Action<long>(NarrativeManager.s_instance.OnGenericRewardShown));
		StoreManager.Get().RegisterSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnBundlePurchased));
		base.StartCoroutine(this.WaitForAchievesThenInit());
	}

	// Token: 0x06005374 RID: 21364 RVA: 0x001B46EC File Offset: 0x001B28EC
	private void OnScenePreLoad(SceneMgr.Mode prevMode, SceneMgr.Mode mode, object userData)
	{
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			this.CleanUpExceptListeners();
		}
	}

	// Token: 0x06005375 RID: 21365 RVA: 0x001B46F8 File Offset: 0x001B28F8
	public void OnQuestCompleteShown(int achieveId)
	{
		global::Achievement achievement = AchieveManager.Get().GetAchievement(achieveId);
		if (achievement.QuestDialogId == 0 || achievement.OnCompleteDialogSequence == null)
		{
			return;
		}
		if (achievement.OnCompleteDialogSequence.m_deferOnComplete)
		{
			this.EnqueueIfNotPresent(achievement.OnCompleteDialogSequence);
			return;
		}
		this.PushDialogSequence(achievement.OnCompleteDialogSequence);
	}

	// Token: 0x06005376 RID: 21366 RVA: 0x001B4748 File Offset: 0x001B2948
	public void OnGenericRewardShown(long originData)
	{
		this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.GENERIC_REWARD_SHOWN, (long)((int)originData));
	}

	// Token: 0x06005377 RID: 21367 RVA: 0x001B4758 File Offset: 0x001B2958
	private void EnqueueIfNotPresent(CharacterDialogSequence sequence)
	{
		using (Queue<CharacterDialogSequence>.Enumerator enumerator = this.m_characterDialogSequenceToShow.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.m_characterDialogRecord == sequence.m_characterDialogRecord)
				{
					return;
				}
			}
		}
		this.m_characterDialogSequenceToShow.Enqueue(sequence);
	}

	// Token: 0x06005378 RID: 21368 RVA: 0x001B47C0 File Offset: 0x001B29C0
	public void ShowOutstandingQuestDialogs()
	{
		base.StartCoroutine(this.ShowOutstandingCharacterDialogSequence());
	}

	// Token: 0x06005379 RID: 21369 RVA: 0x001B47D0 File Offset: 0x001B29D0
	public void OnWelcomeQuestsShown(List<global::Achievement> questsShown, List<global::Achievement> newlyAvailableQuests)
	{
		this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.WELCOME_QUESTS_SHOWN, 0L);
		bool flag = SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_GOLD_DOUBLED, false);
		foreach (global::Achievement achievement in questsShown)
		{
			if (achievement.AutoDestroy)
			{
				base.StartCoroutine(this.DestroyAndReplaceQuest(achievement));
				break;
			}
			if (achievement.QuestDialogId != 0)
			{
				base.StartCoroutine(this.HandleQuestReceived(achievement));
				break;
			}
			if (flag && achievement.IsAffectedByDoubleGold && newlyAvailableQuests.Contains(achievement) && !AchieveManager.Get().HasActiveAutoDestroyQuests() && !AchieveManager.Get().HasActiveUnseenWelcomeQuestDialog() && this.OnDoubleGoldQuestGranted())
			{
				break;
			}
		}
	}

	// Token: 0x0600537A RID: 21370 RVA: 0x001B4898 File Offset: 0x001B2A98
	public bool HasCharacterDialogSequenceToShow()
	{
		return this.m_characterDialogSequenceToShow.Count > 0;
	}

	// Token: 0x0600537B RID: 21371 RVA: 0x001B48A8 File Offset: 0x001B2AA8
	public bool IsShowingBlockingDialog()
	{
		return this.m_showingBlockingDialog;
	}

	// Token: 0x0600537C RID: 21372 RVA: 0x001B48B0 File Offset: 0x001B2AB0
	public void PushDialogSequence(CharacterDialogSequence sequence)
	{
		this.EnqueueIfNotPresent(sequence);
		base.StartCoroutine(this.ShowOutstandingCharacterDialogSequence());
	}

	// Token: 0x0600537D RID: 21373 RVA: 0x001B48C6 File Offset: 0x001B2AC6
	public IEnumerator<IAsyncJobResult> Job_WaitForOutstandingCharacterDialog()
	{
		base.StartCoroutine(this.ShowOutstandingCharacterDialogSequence());
		while (this.m_isProcessingQueuedDialogSequence)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600537E RID: 21374 RVA: 0x001B48D5 File Offset: 0x001B2AD5
	public IEnumerator ShowOutstandingCharacterDialogSequence()
	{
		if (this.m_characterDialogSequenceToShow.Count == 0 || this.m_isProcessingQueuedDialogSequence)
		{
			yield break;
		}
		this.m_isProcessingQueuedDialogSequence = true;
		yield return new WaitForSeconds(1.5f);
		int bannerIDToShow = 0;
		while (this.m_characterDialogSequenceToShow.Count > 0)
		{
			CharacterDialogSequence characterDialogSequence = this.m_characterDialogSequenceToShow.Peek();
			this.SetDialogBlocker(characterDialogSequence.m_blockInput);
			if (characterDialogSequence != null && characterDialogSequence.m_onCompleteBannerId != 0)
			{
				bannerIDToShow = characterDialogSequence.m_onCompleteBannerId;
			}
			if (characterDialogSequence.m_onPreShow != null)
			{
				characterDialogSequence.m_onPreShow(characterDialogSequence);
			}
			yield return base.StartCoroutine(this.PlayerCharacterDialogSequence(characterDialogSequence));
			this.m_characterDialogSequenceToShow.Dequeue();
		}
		if (bannerIDToShow != 0)
		{
			yield return new WaitForSeconds(1f);
			this.m_isBannerShowing = true;
			BannerManager.Get().ShowBanner(bannerIDToShow, new BannerManager.DelOnCloseBanner(this.OnQuestDialogCompleteBannerClosed));
		}
		this.SetDialogBlocker(false);
		while (this.m_isBannerShowing)
		{
			yield return null;
		}
		this.m_isProcessingQueuedDialogSequence = false;
		yield break;
	}

	// Token: 0x0600537F RID: 21375 RVA: 0x001B48E4 File Offset: 0x001B2AE4
	public bool OnDoubleGoldQuestGranted()
	{
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.DOUBLE_GOLD_QUEST_GRANTED, 0L);
	}

	// Token: 0x06005380 RID: 21376 RVA: 0x001B48EF File Offset: 0x001B2AEF
	public bool OnAllPopupsShown()
	{
		if (this.m_hasDoneAllPopupsShownEvent)
		{
			return false;
		}
		this.m_hasDoneAllPopupsShownEvent = true;
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ALL_POPUPS_SHOWN, 0L);
	}

	// Token: 0x06005381 RID: 21377 RVA: 0x001B490B File Offset: 0x001B2B0B
	public bool OnArenaDraftStarted()
	{
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_ARENA_DRAFT, 0L);
	}

	// Token: 0x06005382 RID: 21378 RVA: 0x001B4916 File Offset: 0x001B2B16
	public bool OnArenaRewardsShown()
	{
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ARENA_REWARD_SHOWN, 0L);
	}

	// Token: 0x06005383 RID: 21379 RVA: 0x001B4921 File Offset: 0x001B2B21
	public void OnLoginFlowComplete()
	{
		this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.LOGIN_FLOW_COMPLETE, 0L);
	}

	// Token: 0x06005384 RID: 21380 RVA: 0x001B492D File Offset: 0x001B2B2D
	public bool OnBattlegroundsEntered()
	{
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_BATTLEGROUNDS, 0L);
	}

	// Token: 0x06005385 RID: 21381 RVA: 0x001B4938 File Offset: 0x001B2B38
	public bool OnTavernBrawlEntered()
	{
		return this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.ENTERED_TAVERN_BRAWL, 0L);
	}

	// Token: 0x06005386 RID: 21382 RVA: 0x001B4944 File Offset: 0x001B2B44
	public void OnBundlePurchased(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (bundle != null)
		{
			this.TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent.PURCHASED_BUNDLE, bundle.PMTProductID.Value);
		}
	}

	// Token: 0x06005387 RID: 21383 RVA: 0x001B496B File Offset: 0x001B2B6B
	private void SetDialogBlocker(bool value)
	{
		this.m_showingBlockingDialog = value;
		if (FriendChallengeMgr.Get() != null)
		{
			FriendChallengeMgr.Get().UpdateMyAvailability();
		}
	}

	// Token: 0x06005388 RID: 21384 RVA: 0x001B4986 File Offset: 0x001B2B86
	private void OnQuestDialogCompleteBannerClosed()
	{
		this.m_isBannerShowing = false;
	}

	// Token: 0x06005389 RID: 21385 RVA: 0x001B498F File Offset: 0x001B2B8F
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
		this.PreloadActiveQuestDialog();
		this.InitScheduledCharacterDialogData();
		AchieveManager.Get().RegisterAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(NarrativeManager.s_instance.OnAchievesUpdated), null);
		GameToastMgr.Get().RegisterQuestProgressToastShownListener(new GameToastMgr.QuestProgressToastShownCallback(NarrativeManager.s_instance.OnQuestProgressToastShown));
		TavernBrawlManager.Get().OnTavernBrawlUpdated += NarrativeManager.s_instance.OnTavernBrawlUpdated;
		yield break;
	}

	// Token: 0x0600538A RID: 21386 RVA: 0x001B499E File Offset: 0x001B2B9E
	private IEnumerator DestroyAndReplaceQuest(global::Achievement quest)
	{
		yield return new WaitForSeconds(3.8f);
		SoundDucker ducker = null;
		ducker = base.gameObject.AddComponent<SoundDucker>();
		ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			Global.SoundCategory soundCategory = (Global.SoundCategory)obj;
			if (soundCategory == Global.SoundCategory.AMBIENCE || soundCategory == Global.SoundCategory.MUSIC)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				soundDuckedCategoryDef.m_Category = soundCategory;
				soundDuckedCategoryDef.m_BeginSec = 0f;
				ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
		ducker.StartDucking();
		if (quest.QuestDialogId != 0)
		{
			foreach (CharacterDialog dialog in quest.OnReceivedDialogSequence)
			{
				if (NarrativeManager.IsCharacterDialogDisplayable(dialog))
				{
					yield return new WaitForSeconds(dialog.waitBefore);
					yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, null, 1f));
					yield return new WaitForSeconds(dialog.waitAfter);
				}
				dialog = default(CharacterDialog);
			}
			IEnumerator<CharacterDialog> enumerator2 = null;
		}
		yield return new WaitForSeconds(0.8f);
		int nextQuestId = WelcomeQuests.Get().CompleteAndReplaceAutoDestroyQuestTile(quest.ID);
		yield return new WaitForSeconds(1.3f);
		global::Achievement achievement = AchieveManager.Get().GetAchievement(nextQuestId);
		if (achievement.QuestDialogId != 0)
		{
			int numLinesToPlay = achievement.OnReceivedDialogSequence.Count;
			foreach (CharacterDialog dialog in achievement.OnReceivedDialogSequence)
			{
				int num = numLinesToPlay;
				numLinesToPlay = num - 1;
				if (NarrativeManager.IsCharacterDialogDisplayable(dialog))
				{
					yield return new WaitForSeconds(dialog.waitBefore);
					if (numLinesToPlay == 0)
					{
						yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, new NarrativeManager.CharacterQuotePlayedCallback(this.OnWelcomeQuestNarrativeFinished), 1f));
					}
					else
					{
						yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, null, 1f));
					}
					yield return new WaitForSeconds(dialog.waitAfter);
				}
				dialog = default(CharacterDialog);
			}
			IEnumerator<CharacterDialog> enumerator2 = null;
		}
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
		yield break;
		yield break;
	}

	// Token: 0x0600538B RID: 21387 RVA: 0x001B49B4 File Offset: 0x001B2BB4
	private IEnumerator HandleQuestReceived(global::Achievement quest)
	{
		int numLinesToPlay = quest.OnReceivedDialogSequence.Count;
		if (Options.Get().GetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG) == quest.ID || numLinesToPlay <= 0)
		{
			this.OnWelcomeQuestNarrativeFinished();
			yield break;
		}
		SoundDucker ducker = null;
		ducker = base.gameObject.AddComponent<SoundDucker>();
		ducker.m_DuckedCategoryDefs = new List<SoundDuckedCategoryDef>();
		foreach (object obj in Enum.GetValues(typeof(Global.SoundCategory)))
		{
			Global.SoundCategory soundCategory = (Global.SoundCategory)obj;
			if (soundCategory == Global.SoundCategory.AMBIENCE || soundCategory == Global.SoundCategory.MUSIC)
			{
				SoundDuckedCategoryDef soundDuckedCategoryDef = new SoundDuckedCategoryDef();
				soundDuckedCategoryDef.m_Category = soundCategory;
				soundDuckedCategoryDef.m_BeginSec = 0f;
				ducker.m_DuckedCategoryDefs.Add(soundDuckedCategoryDef);
			}
		}
		ducker.StartDucking();
		foreach (CharacterDialog dialog in quest.OnReceivedDialogSequence)
		{
			int num = numLinesToPlay;
			numLinesToPlay = num - 1;
			if (NarrativeManager.IsCharacterDialogDisplayable(dialog))
			{
				yield return new WaitForSeconds(dialog.waitBefore);
				if (numLinesToPlay == 0)
				{
					yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, new NarrativeManager.CharacterQuotePlayedCallback(this.OnWelcomeQuestNarrativeFinished), 1f));
				}
				else
				{
					yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, null, 1f));
				}
				yield return new WaitForSeconds(dialog.waitAfter);
			}
			dialog = default(CharacterDialog);
		}
		IEnumerator<CharacterDialog> enumerator2 = null;
		Options.Get().SetInt(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG, quest.ID);
		if (ducker != null)
		{
			ducker.StopDucking();
			UnityEngine.Object.Destroy(ducker);
		}
		yield break;
		yield break;
	}

	// Token: 0x0600538C RID: 21388 RVA: 0x001B49CA File Offset: 0x001B2BCA
	private IEnumerator PlayCharacterQuoteAndWait(CharacterDialog dialog, NarrativeManager.CharacterQuotePlayedCallback callback = null, float waitTimeScale = 1f)
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
			audioSource = this.GetPreloadedSound(dialog.audioName);
			if (audioSource == null || audioSource.clip == null)
			{
				this.RemovePreloadedSound(dialog.audioName);
				this.PreloadSound(dialog.audioName);
				while (this.IsPreloadingAssets())
				{
					yield return null;
				}
				audioSource = this.GetPreloadedSound(dialog.audioName);
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
		Log.NarrativeManager.Print("PlayCharacterQuoteAndWait - durationSeconds: {0}  waitTimeScale: {1}", new object[]
		{
			num,
			waitTimeScale
		});
		if (dialog.useInnkeeperQuote)
		{
			this.m_activeCharacterDialogNotification = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, dialog.bubbleText.GetString(true), dialog.audioName, null, false);
			this.m_activeCharacterDialogNotification.ShowWithExistingPopups = true;
		}
		else
		{
			this.m_activeCharacterDialogNotification = NotificationManager.Get().CreateBigCharacterQuoteWithText(dialog.prefabName, NotificationManager.DEFAULT_CHARACTER_POS, dialog.audioName, dialog.bubbleText.GetString(true), num, null, true, dialog.useAltSpeechBubble ? Notification.SpeechBubbleDirection.TopLeft : Notification.SpeechBubbleDirection.BottomLeft, dialog.persistPrefab, dialog.useAltPosition);
		}
		float num2 = num * waitTimeScale;
		num2 += 0.5f;
		yield return new WaitForSeconds(num2);
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	// Token: 0x0600538D RID: 21389 RVA: 0x001B49F0 File Offset: 0x001B2BF0
	private void OnAchievesUpdated(List<global::Achievement> updatedAchieves, List<global::Achievement> completedAchieves, object userData)
	{
		List<global::Achievement> activeQuests = AchieveManager.Get().GetActiveQuests(false);
		this.PreloadQuestDialog(activeQuests);
	}

	// Token: 0x0600538E RID: 21390 RVA: 0x001B4A10 File Offset: 0x001B2C10
	private void OnQuestProgressToastShown(int achieveId)
	{
		base.StartCoroutine(this.HandleOnQuestProgressToastShown(achieveId));
	}

	// Token: 0x0600538F RID: 21391 RVA: 0x001B4A20 File Offset: 0x001B2C20
	private void OnTavernBrawlUpdated()
	{
		if (TavernBrawlManager.Get().IsTavernBrawlActive(BrawlType.BRAWL_TYPE_TAVERN_BRAWL))
		{
			foreach (TavernBrawlMission tavernBrawlMission in TavernBrawlManager.Get().Missions)
			{
				if (tavernBrawlMission.FirstTimeSeenCharacterDialogID > 0)
				{
					this.PreloadQuestDialog(tavernBrawlMission.FirstTimeSeenCharacterDialogSequence);
				}
			}
		}
	}

	// Token: 0x06005390 RID: 21392 RVA: 0x001B4A94 File Offset: 0x001B2C94
	private IEnumerator HandleOnQuestProgressToastShown(int achieveId)
	{
		yield return new WaitForSeconds(1.5f);
		global::Achievement achievement = AchieveManager.Get().GetAchievement(achieveId);
		if (achievement == null || achievement.QuestDialogId == 0)
		{
			yield break;
		}
		if (achievement.Progress == 1)
		{
			yield return this.PlayerCharacterDialogSequence(achievement.OnProgress1DialogSequence);
		}
		else if (achievement.Progress == 2)
		{
			yield return this.PlayerCharacterDialogSequence(achievement.OnProgress2DialogSequence);
		}
		yield break;
	}

	// Token: 0x06005391 RID: 21393 RVA: 0x001B4AAA File Offset: 0x001B2CAA
	public void OnAchieveDismissed(global::Achievement achieve)
	{
		if (achieve.OnDismissDialogSequence != null)
		{
			base.StartCoroutine(this.PlayerCharacterDialogSequence(achieve.OnDismissDialogSequence));
		}
	}

	// Token: 0x06005392 RID: 21394 RVA: 0x001B4AC7 File Offset: 0x001B2CC7
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
		Log.All.Print("CharacterDialogItem id={0} is not displayable. To be displayable, either USE_INNKEEPER_QUOTE must be true or PREFAB_NAME is not null/empty.", new object[]
		{
			dialog.dbfRecordId
		});
		return false;
	}

	// Token: 0x06005393 RID: 21395 RVA: 0x001B4B06 File Offset: 0x001B2D06
	private IEnumerator PlayerCharacterDialogSequence(CharacterDialogSequence dialogSequence)
	{
		if (dialogSequence == null)
		{
			yield break;
		}
		if (!dialogSequence.m_ignorePopups)
		{
			yield return base.StartCoroutine(PopupDisplayManager.Get().WaitForAllPopups());
		}
		foreach (CharacterDialog dialog in dialogSequence)
		{
			if (NarrativeManager.IsCharacterDialogDisplayable(dialog))
			{
				yield return new WaitForSeconds(dialog.waitBefore);
				yield return base.StartCoroutine(this.PlayCharacterQuoteAndWait(dialog, null, 1f));
				yield return new WaitForSeconds(dialog.waitAfter);
			}
			dialog = default(CharacterDialog);
		}
		IEnumerator<CharacterDialog> enumerator = null;
		yield break;
		yield break;
	}

	// Token: 0x06005394 RID: 21396 RVA: 0x001B4B1C File Offset: 0x001B2D1C
	private void OnWelcomeQuestNarrativeFinished()
	{
		if (WelcomeQuests.Get() != null)
		{
			WelcomeQuests.Get().ActivateClickCatcher();
		}
	}

	// Token: 0x06005395 RID: 21397 RVA: 0x001B4B35 File Offset: 0x001B2D35
	private void PreloadActiveQuestDialog()
	{
		this.PreloadQuestDialog(AchieveManager.Get().GetActiveQuests(false));
	}

	// Token: 0x06005396 RID: 21398 RVA: 0x001B4B48 File Offset: 0x001B2D48
	private void PreloadQuestDialog(global::Achievement achievement)
	{
		if (achievement.QuestDialogId == 0)
		{
			return;
		}
		this.PreloadQuestDialog(achievement.OnReceivedDialogSequence);
		this.PreloadQuestDialog(achievement.OnCompleteDialogSequence);
		this.PreloadQuestDialog(achievement.OnProgress1DialogSequence);
		this.PreloadQuestDialog(achievement.OnProgress2DialogSequence);
		this.PreloadQuestDialog(achievement.OnDismissDialogSequence);
	}

	// Token: 0x06005397 RID: 21399 RVA: 0x001B4B9C File Offset: 0x001B2D9C
	private void PreloadQuestDialog(List<global::Achievement> activeAchievements)
	{
		foreach (global::Achievement achievement in activeAchievements)
		{
			this.PreloadQuestDialog(achievement);
		}
	}

	// Token: 0x06005398 RID: 21400 RVA: 0x001B4BEC File Offset: 0x001B2DEC
	private void PreloadQuestDialog(CharacterDialogSequence questDialogSequence)
	{
		foreach (CharacterDialog characterDialog in questDialogSequence)
		{
			if (!string.IsNullOrEmpty(characterDialog.audioName))
			{
				this.PreloadSound(characterDialog.audioName);
			}
		}
	}

	// Token: 0x06005399 RID: 21401 RVA: 0x001B4C48 File Offset: 0x001B2E48
	private void PreloadQuestDialog(List<string> audioNames)
	{
		foreach (string text in audioNames)
		{
			if (!string.IsNullOrEmpty(text))
			{
				this.PreloadSound(text);
			}
		}
	}

	// Token: 0x0600539A RID: 21402 RVA: 0x001B4CA0 File Offset: 0x001B2EA0
	private void PreloadSound(string soundPath)
	{
		if (!this.CheckPreloadedSound(soundPath))
		{
			this.m_preloadsNeeded++;
			SoundLoader.LoadSound(soundPath, new PrefabCallback<GameObject>(this.OnSoundLoaded), null, SoundManager.Get().GetPlaceholderSound());
		}
	}

	// Token: 0x0600539B RID: 21403 RVA: 0x001B4CDC File Offset: 0x001B2EDC
	private void OnSoundLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_preloadsNeeded--;
		if (go == null)
		{
			Debug.LogWarning(string.Format("NarrativeManager.OnSoundLoaded() - FAILED to load \"{0}\"", assetRef));
			return;
		}
		AudioSource component = go.GetComponent<AudioSource>();
		if (component == null)
		{
			Debug.LogWarning(string.Format("NarrativeManager.OnSoundLoaded() - ERROR \"{0}\" has no Spell component", assetRef));
			return;
		}
		if (!this.CheckPreloadedSound(assetRef.ToString()))
		{
			this.m_preloadedSounds.Add(assetRef.ToString(), component);
		}
	}

	// Token: 0x0600539C RID: 21404 RVA: 0x001B4D52 File Offset: 0x001B2F52
	private void RemovePreloadedSound(string soundPath)
	{
		this.m_preloadedSounds.Remove(soundPath);
	}

	// Token: 0x0600539D RID: 21405 RVA: 0x001B4D64 File Offset: 0x001B2F64
	private bool CheckPreloadedSound(string soundPath)
	{
		AudioSource audioSource;
		return this.m_preloadedSounds.TryGetValue(soundPath, out audioSource);
	}

	// Token: 0x0600539E RID: 21406 RVA: 0x001B4D80 File Offset: 0x001B2F80
	private AudioSource GetPreloadedSound(string soundPath)
	{
		AudioSource result;
		if (this.m_preloadedSounds.TryGetValue(soundPath, out result))
		{
			return result;
		}
		Debug.LogError(string.Format("NarrativeManager.GetPreloadedSound() - \"{0}\" was not preloaded", soundPath));
		return null;
	}

	// Token: 0x0600539F RID: 21407 RVA: 0x001B4DB0 File Offset: 0x001B2FB0
	private bool IsPreloadingAssets()
	{
		return this.m_preloadsNeeded > 0;
	}

	// Token: 0x060053A0 RID: 21408 RVA: 0x001B4DBC File Offset: 0x001B2FBC
	private void SetLastSeenScheduledCharacterDialog(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		if (eventType == ScheduledCharacterDialogEvent.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.SetLastSeenScheduledCharacterDialog was passed an INVALID ScheduledCharacterDialogEvent", Array.Empty<object>());
			return;
		}
		if (NarrativeManager.m_lastSeenScheduledCharacterDialogKeys.ContainsKey(eventType))
		{
			this.SetLastSeenScheduledCharacterDialog_GameSaveData(scheduledDialogId, eventType);
			return;
		}
		if (NarrativeManager.m_lastSeenScheduledCharacterDialogOptions.ContainsKey(eventType))
		{
			this.SetLastSeenScheduledCharacterDialog_ServerOption(scheduledDialogId, eventType);
			return;
		}
		Log.NarrativeManager.PrintError("NarrativeManager has no storage mechanism for event {0}", new object[]
		{
			eventType.ToString()
		});
	}

	// Token: 0x060053A1 RID: 21409 RVA: 0x001B4E34 File Offset: 0x001B3034
	private int GetLastSeenScheduledCharacterDialog(ScheduledCharacterDialogEvent eventType)
	{
		if (eventType == ScheduledCharacterDialogEvent.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.GetLastSeenScheduledCharacterDialog was passed an INVALID ScheduledCharacterDialogEvent", Array.Empty<object>());
			return -1;
		}
		if (NarrativeManager.m_lastSeenScheduledCharacterDialogKeys.ContainsKey(eventType))
		{
			return this.GetLastSeenScheduledCharacterDialog_GameSaveData(eventType);
		}
		if (NarrativeManager.m_lastSeenScheduledCharacterDialogOptions.ContainsKey(eventType))
		{
			return this.GetLastSeenScheduledCharacterDialog_ServerOption(eventType);
		}
		Log.NarrativeManager.PrintError("NarrativeManager has no storage mechanism for event {0}", new object[]
		{
			eventType.ToString()
		});
		return -1;
	}

	// Token: 0x060053A2 RID: 21410 RVA: 0x001B4EAC File Offset: 0x001B30AC
	private void SetLastSeenScheduledCharacterDialog_ServerOption(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		Option option;
		NarrativeManager.m_lastSeenScheduledCharacterDialogOptions.TryGetValue(eventType, out option);
		if (option == Option.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.SetLastSeenScheduledCharacterDialog option mapping had no corresponding option for event: {0}", new object[]
			{
				eventType
			});
			return;
		}
		Options.Get().SetInt(option, scheduledDialogId);
	}

	// Token: 0x060053A3 RID: 21411 RVA: 0x001B4EF8 File Offset: 0x001B30F8
	private int GetLastSeenScheduledCharacterDialog_ServerOption(ScheduledCharacterDialogEvent eventType)
	{
		Option option;
		NarrativeManager.m_lastSeenScheduledCharacterDialogOptions.TryGetValue(eventType, out option);
		if (option == Option.INVALID)
		{
			Log.NarrativeManager.PrintError("NarrativeManager.GetLastSeenScheduledCharacterDialog option mapping had no corresponding option for event: {0}", new object[]
			{
				eventType
			});
			return -1;
		}
		return Options.Get().GetInt(option);
	}

	// Token: 0x060053A4 RID: 21412 RVA: 0x001B4F41 File Offset: 0x001B3141
	private void SetLastSeenScheduledCharacterDialog_GameSaveData(int scheduledDialogId, ScheduledCharacterDialogEvent eventType)
	{
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(NarrativeManager.m_lastSeenScheduledCharacterDialogKeys[eventType].Key, NarrativeManager.m_lastSeenScheduledCharacterDialogKeys[eventType].Subkey, new long[]
		{
			(long)scheduledDialogId
		}), null);
	}

	// Token: 0x060053A5 RID: 21413 RVA: 0x001B4F80 File Offset: 0x001B3180
	private int GetLastSeenScheduledCharacterDialog_GameSaveData(ScheduledCharacterDialogEvent eventType)
	{
		long num;
		GameSaveDataManager.Get().GetSubkeyValue(NarrativeManager.m_lastSeenScheduledCharacterDialogKeys[eventType].Key, NarrativeManager.m_lastSeenScheduledCharacterDialogKeys[eventType].Subkey, out num);
		return (int)num;
	}

	// Token: 0x060053A6 RID: 21414 RVA: 0x001B4FBC File Offset: 0x001B31BC
	private void InitScheduledCharacterDialogData()
	{
		foreach (ScheduledCharacterDialogDbfRecord scheduledCharacterDialogDbfRecord in GameDbf.ScheduledCharacterDialog.GetRecords())
		{
			if (GeneralUtils.ForceBool(scheduledCharacterDialogDbfRecord.Enabled))
			{
				if (!string.IsNullOrEmpty(scheduledCharacterDialogDbfRecord.Event))
				{
					SpecialEventType eventType = SpecialEventManager.GetEventType(scheduledCharacterDialogDbfRecord.Event);
					if (eventType == SpecialEventType.UNKNOWN)
					{
						Log.All.PrintError("NarrativeManager.InitScheduledCharacterDialogData: unknown event=\"{0}\" for ScheduledCharacterDialogDbfRecord id={1}", new object[]
						{
							scheduledCharacterDialogDbfRecord.Event,
							scheduledCharacterDialogDbfRecord.ID
						});
						continue;
					}
					if (SpecialEventManager.Get().HasEventEnded(eventType))
					{
						continue;
					}
				}
				if ((scheduledCharacterDialogDbfRecord.ShowToNewPlayer || AchieveManager.Get().HasUnlockedFeature(Achieve.Unlocks.DAILY)) && (scheduledCharacterDialogDbfRecord.ShowToReturningPlayer || !ReturningPlayerMgr.Get().IsInReturningPlayerMode))
				{
					ScheduledCharacterDialogEvent @enum = EnumUtils.GetEnum<ScheduledCharacterDialogEvent>(scheduledCharacterDialogDbfRecord.ClientEvent.ToString(), StringComparison.OrdinalIgnoreCase);
					if (this.GetLastSeenScheduledCharacterDialogDisplayOrder(@enum) < scheduledCharacterDialogDbfRecord.DisplayOrder)
					{
						if (!this.m_scheduledCharacterDialogData.ContainsKey(@enum))
						{
							this.m_scheduledCharacterDialogData[@enum] = new List<ScheduledCharacterDialogDbfRecord>();
						}
						this.PreloadQuestDialog(CharacterDialogSequence.GetAudioOfCharacterDialogSequence(scheduledCharacterDialogDbfRecord.CharacterDialogId));
						this.m_scheduledCharacterDialogData[@enum].Add(scheduledCharacterDialogDbfRecord);
					}
				}
			}
		}
	}

	// Token: 0x060053A7 RID: 21415 RVA: 0x001B5128 File Offset: 0x001B3328
	private int GetLastSeenScheduledCharacterDialogDisplayOrder(ScheduledCharacterDialogEvent dialogEvent)
	{
		int lastSeenScheduledCharacterDialog = this.GetLastSeenScheduledCharacterDialog(dialogEvent);
		int result = -1;
		ScheduledCharacterDialogDbfRecord record = GameDbf.ScheduledCharacterDialog.GetRecord(lastSeenScheduledCharacterDialog);
		if (record != null)
		{
			result = record.DisplayOrder;
		}
		return result;
	}

	// Token: 0x060053A8 RID: 21416 RVA: 0x001B5158 File Offset: 0x001B3358
	public void ResetScheduledCharacterDialogEvent_Debug()
	{
		if (HearthstoneApplication.IsPublic())
		{
			return;
		}
		foreach (object obj in Enum.GetValues(typeof(ScheduledCharacterDialogEvent)))
		{
			ScheduledCharacterDialogEvent scheduledCharacterDialogEvent = (ScheduledCharacterDialogEvent)obj;
			if (scheduledCharacterDialogEvent != ScheduledCharacterDialogEvent.INVALID)
			{
				this.SetLastSeenScheduledCharacterDialog(0, scheduledCharacterDialogEvent);
			}
		}
		this.InitScheduledCharacterDialogData();
	}

	// Token: 0x060053A9 RID: 21417 RVA: 0x001B51CC File Offset: 0x001B33CC
	public bool TriggerScheduledCharacterDialogEvent_Debug(ScheduledCharacterDialogEvent eventType)
	{
		return !HearthstoneApplication.IsPublic() && this.TriggerScheduledCharacterDialogEvent(eventType, 0L);
	}

	// Token: 0x060053AA RID: 21418 RVA: 0x001B51E0 File Offset: 0x001B33E0
	private bool TriggerScheduledCharacterDialogEvent(ScheduledCharacterDialogEvent eventType, long eventData = 0L)
	{
		if (!this.m_scheduledCharacterDialogData.ContainsKey(eventType))
		{
			return false;
		}
		if (UserAttentionManager.IsBlockedBy(UserAttentionBlocker.SET_ROTATION_INTRO))
		{
			return false;
		}
		ScheduledCharacterDialogDbfRecord recordToUse = null;
		int lastSeenScheduledCharacterDialogDisplayOrder = this.GetLastSeenScheduledCharacterDialogDisplayOrder(eventType);
		foreach (ScheduledCharacterDialogDbfRecord scheduledCharacterDialogDbfRecord in this.m_scheduledCharacterDialogData[eventType])
		{
			if (!string.IsNullOrEmpty(scheduledCharacterDialogDbfRecord.Event))
			{
				SpecialEventType eventType2 = SpecialEventManager.GetEventType(scheduledCharacterDialogDbfRecord.Event);
				if (eventType2 == SpecialEventType.UNKNOWN)
				{
					Log.All.PrintError("NarrativeManager.TriggerScheduledCharacterDialogEvent: unknown event=\"{0}\" for ScheduledCharacterDialogDbfRecord id={1}", new object[]
					{
						scheduledCharacterDialogDbfRecord.Event,
						scheduledCharacterDialogDbfRecord.ID
					});
					continue;
				}
				if (!SpecialEventManager.Get().IsEventActive(eventType2, false))
				{
					continue;
				}
			}
			if ((eventData == 0L || eventData == scheduledCharacterDialogDbfRecord.ClientEventData) && scheduledCharacterDialogDbfRecord.DisplayOrder > lastSeenScheduledCharacterDialogDisplayOrder && (recordToUse == null || recordToUse.DisplayOrder > scheduledCharacterDialogDbfRecord.DisplayOrder))
			{
				recordToUse = scheduledCharacterDialogDbfRecord;
			}
		}
		if (recordToUse == null)
		{
			return false;
		}
		CharacterDialogSequence characterDialogSequence = new CharacterDialogSequence(recordToUse.CharacterDialogId, CharacterDialogEventType.UNSPECIFIED);
		if (characterDialogSequence == null)
		{
			return false;
		}
		characterDialogSequence.m_onPreShow = delegate(CharacterDialogSequence sequence)
		{
			this.SetLastSeenScheduledCharacterDialog(recordToUse.ID, eventType);
		};
		this.PushDialogSequence(characterDialogSequence);
		return true;
	}

	// Token: 0x060053AB RID: 21419 RVA: 0x001B535C File Offset: 0x001B355C
	private void WillReset()
	{
		this.CleanUpEverything();
	}

	// Token: 0x060053AC RID: 21420 RVA: 0x001B5364 File Offset: 0x001B3564
	private void CleanUpEverything()
	{
		this.CleanUpExceptListeners();
		AchieveManager achieveManager;
		if (HearthstoneServices.TryGet<AchieveManager>(out achieveManager))
		{
			achieveManager.RemoveAchievesUpdatedListener(new AchieveManager.AchievesUpdatedCallback(this.OnAchievesUpdated));
		}
		if (GameToastMgr.Get() != null)
		{
			GameToastMgr.Get().RemoveQuestProgressToastShownListener(new GameToastMgr.QuestProgressToastShownCallback(this.OnQuestProgressToastShown));
		}
		PopupDisplayManager popupDisplayManager;
		if (HearthstoneServices.TryGet<PopupDisplayManager>(out popupDisplayManager))
		{
			popupDisplayManager.RemoveCompletedQuestShownListener(new Action<int>(this.OnQuestCompleteShown));
		}
		TavernBrawlManager tavernBrawlManager;
		if (HearthstoneServices.TryGet<TavernBrawlManager>(out tavernBrawlManager))
		{
			tavernBrawlManager.OnTavernBrawlUpdated -= this.OnTavernBrawlUpdated;
		}
		if (HearthstoneApplication.Get() != null)
		{
			HearthstoneApplication.Get().WillReset -= this.WillReset;
		}
		SceneMgr sceneMgr;
		if (HearthstoneServices.TryGet<SceneMgr>(out sceneMgr))
		{
			sceneMgr.UnregisterScenePreLoadEvent(new SceneMgr.ScenePreLoadCallback(this.OnScenePreLoad));
		}
		LoginManager loginManager;
		if (HearthstoneServices.TryGet<LoginManager>(out loginManager))
		{
			loginManager.OnFullLoginFlowComplete -= this.OnLoginFlowComplete;
		}
		if (StoreManager.Get() != null)
		{
			StoreManager.Get().RemoveSuccessfulPurchaseListener(new Action<Network.Bundle, PaymentMethod>(this.OnBundlePurchased));
		}
	}

	// Token: 0x060053AD RID: 21421 RVA: 0x001B5468 File Offset: 0x001B3668
	private void CleanUpExceptListeners()
	{
		base.StopAllCoroutines();
		this.m_characterDialogSequenceToShow.Clear();
		this.m_preloadedSounds.Clear();
		if (NotificationManager.Get() != null && this.m_activeCharacterDialogNotification != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_activeCharacterDialogNotification, 0f);
		}
		this.m_preloadsNeeded = 0;
		this.m_isBannerShowing = false;
		this.m_showingBlockingDialog = false;
		this.m_isProcessingQueuedDialogSequence = false;
		this.m_hasDoneAllPopupsShownEvent = false;
	}

	// Token: 0x060053AE RID: 21422 RVA: 0x001B54E4 File Offset: 0x001B36E4
	public List<Option> Cheat_ClearAllSeen()
	{
		List<Option> list = new List<Option>();
		list.AddRange(NarrativeManager.m_lastSeenScheduledCharacterDialogOptions.Values);
		list.Add(Option.LATEST_SEEN_WELCOME_QUEST_DIALOG);
		list.Add(Option.LATEST_SEEN_TAVERNBRAWL_SEASON_CHALKBOARD);
		list.Add(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON_CHALKBOARD);
		list.Add(Option.LATEST_SEEN_TAVERNBRAWL_SEASON);
		list.Add(Option.LATEST_SEEN_FIRESIDEBRAWL_SEASON);
		foreach (Option option in list)
		{
			Options.Get().DeleteOption(option);
		}
		return list;
	}

	// Token: 0x04004A1C RID: 18972
	private const float DELAY_TIME_FOR_QUEST_PROGRESS = 1.5f;

	// Token: 0x04004A1D RID: 18973
	private const float DELAY_TIME_FOR_QUEST_COMPLETE = 1.5f;

	// Token: 0x04004A1E RID: 18974
	private const float DELAY_TIME_FOR_AUTO_DESTROY_QUEST_RECEIVED = 3.8f;

	// Token: 0x04004A1F RID: 18975
	private const float DELAY_TIME_BEFORE_QUEST_DESTROY = 0.8f;

	// Token: 0x04004A20 RID: 18976
	private const float DELAY_TIME_FOR_AUTO_DESTROY_POST_DESTROY = 1.3f;

	// Token: 0x04004A21 RID: 18977
	private const float DELAY_TIME_BEFORE_SHOW_BANNER = 1f;

	// Token: 0x04004A22 RID: 18978
	private const float FALLBACK_DURATION_ON_AUDIO_LOADING_FAIL = 3.5f;

	// Token: 0x04004A23 RID: 18979
	private static NarrativeManager s_instance;

	// Token: 0x04004A24 RID: 18980
	private Map<string, AudioSource> m_preloadedSounds = new Map<string, AudioSource>();

	// Token: 0x04004A25 RID: 18981
	private int m_preloadsNeeded;

	// Token: 0x04004A26 RID: 18982
	private Queue<CharacterDialogSequence> m_characterDialogSequenceToShow = new Queue<CharacterDialogSequence>();

	// Token: 0x04004A27 RID: 18983
	private Notification m_activeCharacterDialogNotification;

	// Token: 0x04004A28 RID: 18984
	private bool m_isBannerShowing;

	// Token: 0x04004A29 RID: 18985
	private bool m_showingBlockingDialog;

	// Token: 0x04004A2A RID: 18986
	private bool m_isProcessingQueuedDialogSequence;

	// Token: 0x04004A2B RID: 18987
	private bool m_hasDoneAllPopupsShownEvent;

	// Token: 0x04004A2C RID: 18988
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

	// Token: 0x04004A2D RID: 18989
	private static Map<ScheduledCharacterDialogEvent, GameSaveDataManager.GameSaveKeyTuple> m_lastSeenScheduledCharacterDialogKeys = new Map<ScheduledCharacterDialogEvent, GameSaveDataManager.GameSaveKeyTuple>
	{
		{
			ScheduledCharacterDialogEvent.ENTERED_BATTLEGROUNDS,
			new GameSaveDataManager.GameSaveKeyTuple
			{
				Key = GameSaveKeyId.CHARACTER_DIALOG,
				Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_BACON
			}
		},
		{
			ScheduledCharacterDialogEvent.ENTERED_TAVERN_BRAWL,
			new GameSaveDataManager.GameSaveKeyTuple
			{
				Key = GameSaveKeyId.CHARACTER_DIALOG,
				Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_TAVERN_BRAWL
			}
		},
		{
			ScheduledCharacterDialogEvent.PURCHASED_BUNDLE,
			new GameSaveDataManager.GameSaveKeyTuple
			{
				Key = GameSaveKeyId.CHARACTER_DIALOG,
				Subkey = GameSaveKeySubkeyId.CHARACTER_DIALOG_LAST_SEEN_PURCHASED_BUNDLE
			}
		}
	};

	// Token: 0x04004A2E RID: 18990
	private Map<ScheduledCharacterDialogEvent, List<ScheduledCharacterDialogDbfRecord>> m_scheduledCharacterDialogData = new Map<ScheduledCharacterDialogEvent, List<ScheduledCharacterDialogDbfRecord>>();

	// Token: 0x02002041 RID: 8257
	// (Invoke) Token: 0x06011C98 RID: 72856
	public delegate void CharacterQuotePlayedCallback();
}
