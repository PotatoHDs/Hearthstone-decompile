using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

// Token: 0x02000024 RID: 36
public class AdventureBookPageDisplay : BookPageDisplay
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060000EC RID: 236 RVA: 0x00005318 File Offset: 0x00003518
	// (set) Token: 0x060000ED RID: 237 RVA: 0x0000531F File Offset: 0x0000351F
	public static bool NeedToShowAdventureSectionCompletionSequence { get; private set; }

	// Token: 0x060000EE RID: 238 RVA: 0x00005328 File Offset: 0x00003528
	private void Start()
	{
		this.m_AdventureBookPageContentsReference.RegisterReadyListener<Widget>(new Action<Widget>(this.AdventureBookPageContentsIsReady));
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.UpdateMapOnWingProgressUpdated));
		AdventureConfig.Get().AddAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSet));
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00005379 File Offset: 0x00003579
	private void OnDestroy()
	{
		AdventureProgressMgr adventureProgressMgr = AdventureProgressMgr.Get();
		if (adventureProgressMgr != null)
		{
			adventureProgressMgr.RemoveProgressUpdatedListener(new AdventureProgressMgr.AdventureProgressUpdatedCallback(this.UpdateMapOnWingProgressUpdated));
		}
		AdventureConfig.Get().RemoveAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSet));
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x000053AE File Offset: 0x000035AE
	private void Update()
	{
		this.CheckForInputForCheats();
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x000053B6 File Offset: 0x000035B6
	public override bool IsLoaded()
	{
		if (this.m_basePageRenderer == null)
		{
			Log.Adventures.Print("Currently waiting on m_basePageRenderer to get set before IsLoaded() becomes true.", Array.Empty<object>());
			return false;
		}
		return true;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000053DD File Offset: 0x000035DD
	public void SetUpPage(PageData pageData, AdventureBookPageDisplay.PageReadyCallback callback)
	{
		base.StartCoroutine(this.SetUpPageWhenReady(pageData, callback));
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000053EE File Offset: 0x000035EE
	public void SetPageEventListener(Widget.EventListenerDelegate listener)
	{
		this.m_pageEventListener = listener;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000053F7 File Offset: 0x000035F7
	public void SetFlipToChapterCallback(AdventureBookPageDisplay.FlipToChapterCallback callback)
	{
		this.m_flipToChapterCallback = callback;
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x00005400 File Offset: 0x00003600
	public void SetEnableInteractionCallback(AdventureBookPageDisplay.EnableInteractionCallback callback)
	{
		this.m_enableInteractionCallback = callback;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00005409 File Offset: 0x00003609
	public AdventureBookPageDataModel GetAdventurePageDataModel()
	{
		return this.m_pageDataModel;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00005411 File Offset: 0x00003611
	public void AllInitialTransitionsComplete()
	{
		this.m_allInitialTransitionsComplete = true;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0000541C File Offset: 0x0000361C
	public override void Show()
	{
		base.Show();
		ScenarioDbId mission = ScenarioDbId.INVALID;
		if (this.m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = this.m_pageData as ChapterPageData;
			if (chapterPageData == null)
			{
				Debug.LogErrorFormat("Showing a Book Chapter, but it has no data associated with it!", Array.Empty<object>());
			}
			else if (chapterPageData.ScenarioRecords.Count == 0)
			{
				Debug.LogErrorFormat("Showing Book Chapter {0}, but it has no ScenarioIds associated with it!", new object[]
				{
					chapterPageData.WingRecord.Name
				});
			}
			else if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()))
			{
				mission = (ScenarioDbId)chapterPageData.ScenarioRecords[0].ID;
			}
			else
			{
				ScenarioDbId mission2 = AdventureConfig.Get().GetMission();
				if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && mission2 != ScenarioDbId.INVALID && !AdventureProgressMgr.Get().HasDefeatedScenario((int)mission2))
				{
					mission = mission2;
				}
			}
		}
		AdventureConfig.Get().SetMission(mission, true);
		base.StartCoroutine(this.ShowPageUpdateVisualsWhenReady());
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000054F8 File Offset: 0x000036F8
	public bool DoesBundleApplyToPage(Network.Bundle bundle)
	{
		if (this.m_pageData == null)
		{
			Debug.LogError("DoesBundleApplyToPage: No pageData defined for page!");
			return false;
		}
		if (this.m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = this.m_pageData as ChapterPageData;
			if (chapterPageData != null)
			{
				return AdventureUtils.DoesBundleIncludeWing(bundle, chapterPageData.WingRecord.ID);
			}
		}
		return AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, this.m_pageData.Adventure);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00005559 File Offset: 0x00003759
	private IEnumerator SetUpPageWhenReady(PageData pageData, AdventureBookPageDisplay.PageReadyCallback callback)
	{
		this.m_pageData = pageData;
		this.m_needToShowRewardChestAnim = false;
		this.m_rewardChestReadyToShowPopup = false;
		this.m_needToShowChapterCompletionAnim = false;
		this.m_chapterCompletionAnimFinished = false;
		this.m_readyToPlayAdventureNewlyCompletedVO = false;
		this.m_adventureNewlyCompletedSequenceFinished = false;
		this.m_needToShowMissionCompleteAnim = false;
		this.m_missionCompleteAnimFinished = false;
		this.m_needToShowMissionUnlockAnim = false;
		this.m_missionUnlockAnimFinished = false;
		while (this.m_adventureBookPageContentsWidget == null)
		{
			yield return null;
		}
		this.SetupPageDataModels(pageData);
		string eventName = "ShowBookMapPage";
		if (pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			eventName = "ShowChapterPage";
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			eventName = "ShowCardBackPage";
		}
		if (this.m_adventureBookPageContentsWidget.TriggerEvent(eventName, default(Widget.TriggerEventParameters)))
		{
			while (this.m_adventureBookPageContentsWidget.IsChangingStates)
			{
				yield return null;
			}
		}
		if (callback != null)
		{
			callback();
		}
		yield break;
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00005576 File Offset: 0x00003776
	private static AdventureBookPageMoralAlignment ConvertBookSectionToMoralAlignment(int section)
	{
		return (AdventureBookPageMoralAlignment)section;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x0000557C File Offset: 0x0000377C
	private void SetupPageDataModels(PageData pageData)
	{
		IDataModel dataModel;
		this.m_adventureBookPageContentsWidget.GetDataModel(2, out dataModel);
		this.m_pageDataModel = (dataModel as AdventureBookPageDataModel);
		if (this.m_pageDataModel == null)
		{
			this.m_pageDataModel = new AdventureBookPageDataModel();
			this.m_pageDataModel.ChapterData = new AdventureChapterDataModel();
			this.m_adventureBookPageContentsWidget.BindDataModel(this.m_pageDataModel, false);
		}
		else
		{
			this.m_pageDataModel.ChapterData = new AdventureChapterDataModel();
		}
		this.m_pageDataModel.PageType = pageData.PageType;
		this.m_pageDataModel.MoralAlignment = AdventureBookPageDisplay.ConvertBookSectionToMoralAlignment(pageData.BookSection);
		this.m_pageDataModel.ChapterData.TimeLocked = false;
		this.m_pageDataModel.ChapterData.FirstHeroBundledWithChapter = 0;
		this.m_pageDataModel.ChapterData.SecondHeroBundledWithChapter = 0;
		this.m_pageDataModel.ChapterData.CompletionRewardType = Reward.Type.NONE;
		this.m_pageDataModel.AllChaptersData.Clear();
		if (pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = pageData as ChapterPageData;
			if (chapterPageData == null)
			{
				Debug.LogError("SetupDataModelsAndRefresh(): PageData is not a valid ChapterPageData! Cannot cast properly.");
				return;
			}
			AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData(this.m_pageDataModel.ChapterData, chapterPageData);
			this.m_needToShowRewardChestAnim = this.m_pageDataModel.ChapterData.CompletionRewardsNewlyEarned;
			this.m_needToShowChapterCompletionAnim = this.m_pageDataModel.ChapterData.NewlyCompleted;
			if (this.m_needToShowChapterCompletionAnim && AdventureProgressMgr.Get().IsAdventureModeAndSectionComplete((AdventureDbId)chapterPageData.WingRecord.AdventureId, chapterPageData.AdventureMode, chapterPageData.BookSection))
			{
				Log.Adventures.Print("You've completed your final Chapter! Setting up Adventure Complete sequence.", Array.Empty<object>());
				AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence = true;
			}
			if (this.m_pageDataModel.ChapterData.NewlyUnlocked)
			{
				Log.Adventures.Print("Chapter {0} is newly unlocked!", new object[]
				{
					this.m_pageDataModel.ChapterData.ChapterNumber
				});
			}
			if (this.m_pageDataModel.ChapterData.NewlyCompleted)
			{
				Log.Adventures.Print("Chapter {0} is newly completed!", new object[]
				{
					this.m_pageDataModel.ChapterData.ChapterNumber
				});
			}
			if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(pageData.AdventureMode))
			{
				return;
			}
			using (IEnumerator<AdventureMissionDataModel> enumerator = this.m_pageDataModel.ChapterData.Missions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AdventureMissionDataModel adventureMissionDataModel = enumerator.Current;
					bool flag = adventureMissionDataModel.Rewards != null && adventureMissionDataModel.Rewards.Items != null && adventureMissionDataModel.Rewards.Items.Count > 0;
					if (adventureMissionDataModel.NewlyCompleted)
					{
						this.m_needToShowMissionCompleteAnim = true;
						if (flag)
						{
							this.m_needToShowRewardChestAnim = true;
						}
					}
					if (adventureMissionDataModel.NewlyUnlocked)
					{
						this.m_needToShowMissionUnlockAnim = true;
					}
				}
				return;
			}
		}
		if (pageData.PageType == AdventureBookPageType.MAP)
		{
			MapPageData mapPageData = pageData as MapPageData;
			if (mapPageData == null)
			{
				Debug.LogError("SetupDataModelsAndRefresh(): PageData is not a valid MapPageData! Cannot cast properly.");
				return;
			}
			this.m_pageDataModel.NumChaptersCompletedText = mapPageData.NumChaptersCompletedText;
			while (this.m_sortedChapterDataModels.Count < mapPageData.ChapterData.Values.Count)
			{
				this.m_sortedChapterDataModels.Add(new AdventureChapterDataModel());
			}
			int[] array = new int[mapPageData.NumSectionsInBook];
			int[] array2 = new int[mapPageData.NumSectionsInBook];
			int i = 0;
			foreach (ChapterPageData chapterPageData2 in mapPageData.ChapterData.Values)
			{
				AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData(this.m_sortedChapterDataModels[i], chapterPageData2);
				if (chapterPageData2.BookSection < 0 || chapterPageData2.BookSection >= mapPageData.NumSectionsInBook)
				{
					Debug.LogErrorFormat("AdventureBookPageDisplay.SetupDataModelsAndRefresh() - chapterData.BookSection {0} is not within the bounds of the number of sections {1}", new object[]
					{
						chapterPageData2.BookSection,
						mapPageData.NumSectionsInBook
					});
				}
				else
				{
					array2[chapterPageData2.BookSection]++;
					if (this.m_sortedChapterDataModels[i].PlayerOwnsChapter)
					{
						array[chapterPageData2.BookSection]++;
					}
				}
				i++;
			}
			this.m_sortedChapterDataModels.Sort((AdventureChapterDataModel a, AdventureChapterDataModel b) => a.ChapterNumber - b.ChapterNumber);
			this.m_pageDataModel.AllChaptersData.AddRange(this.m_sortedChapterDataModels);
			while (this.m_pageDataModel.NumChaptersOwnedText.Count < array.Length)
			{
				this.m_pageDataModel.NumChaptersOwnedText.Add("");
			}
			for (i = 0; i < array.Length; i++)
			{
				if (array[i] < array2[i])
				{
					this.m_pageDataModel.NumChaptersOwnedText[i] = GameStrings.Format("GLUE_ADVENTURE_NUM_CHAPTERS_OWNED", new object[]
					{
						array[i]
					});
				}
				else
				{
					this.m_pageDataModel.NumChaptersOwnedText[i] = "";
				}
			}
			this.UpdateMapButtonData(false);
			return;
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			this.UpdateRewardPageData(pageData);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00005A8C File Offset: 0x00003C8C
	private void OnChapterClickableRelease(UIEvent e)
	{
		AdventureBookPageDisplay.ChapterButtonData chapterButtonData = e.GetElement().GetData() as AdventureBookPageDisplay.ChapterButtonData;
		if (chapterButtonData == null)
		{
			Log.Adventures.PrintError("Chapter Button pressed, but the button has no data!", Array.Empty<object>());
			return;
		}
		Log.Adventures.Print("Released {0}!", new object[]
		{
			chapterButtonData.ButtonName
		});
		if (this.m_flipToChapterCallback != null)
		{
			this.m_flipToChapterCallback(chapterButtonData.ChapterData.ChapterNumber);
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00005B00 File Offset: 0x00003D00
	public void HideAndSuppressChapterUnlockSequence()
	{
		if (!this.m_isInUnlockedSequence)
		{
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_popupEffectFadeTime, null);
		if (string.IsNullOrEmpty(this.m_currentUnlockButtonName) || !this.m_chapterButtonClickablesNameMap.ContainsKey(this.m_currentUnlockButtonName))
		{
			return;
		}
		Clickable clickable = this.m_chapterButtonClickablesNameMap[this.m_currentUnlockButtonName];
		if (clickable == null)
		{
			Log.Adventures.PrintError("Chapter Button {0} is missing!", new object[]
			{
				this.m_currentUnlockButtonName
			});
			return;
		}
		VisualController component = clickable.GetComponent<VisualController>();
		if (component == null)
		{
			Error.AddDevWarning("Missing Visual Controller", "{0} does not have a visual controller!", new object[]
			{
				this.m_currentUnlockButtonName
			});
			return;
		}
		IDataModel dataModel;
		clickable.GetDataModel(3, out dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = false;
		}
		component.Owner.TriggerEvent("CODE_HIDE_AND_DISMISS", default(Widget.TriggerEventParameters));
		this.m_currentUnlockButtonName = null;
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00005BF0 File Offset: 0x00003DF0
	private void OnChapterUnlockButtonClicked()
	{
		ChapterPageData chapterPageData = this.m_pageData as ChapterPageData;
		if (chapterPageData == null)
		{
			return;
		}
		if (AdventureProgressMgr.Get().OwnsWing(chapterPageData.WingRecord.ID) && chapterPageData.WingRecord.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			AdventureUtils.DisplayFirstChapterFreePopup(chapterPageData, null);
			return;
		}
		if (AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			return;
		}
		bool flag = AdventureConfig.Get().GetSelectedAdventure() != AdventureDbId.DALARAN && AdventureConfig.Get().GetSelectedAdventure() != AdventureDbId.ULDUM;
		if (this.m_pageDataModel.ChapterData.AvailableForPurchase && chapterPageData.WingRecord.PmtProductIdForThisAndRestOfAdventure == 0)
		{
			AdventureBookPageDisplay.StartSingleWingPurchaseTransaction(this.m_pageData, this.m_pageDataModel);
			return;
		}
		if (this.m_pageDataModel.ChapterData.AvailableForPurchase || flag)
		{
			this.SetupAdventurePurchaseChoiceDialog(chapterPageData);
			return;
		}
		AdventureBookPageDisplay.StartFullBookPurchaseTransaction(this.m_pageData, this.m_pageDataModel);
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00005CDC File Offset: 0x00003EDC
	private void OnBossSelected(int bossOffset)
	{
		if (this.m_adventureBookPageContentsWidget == null)
		{
			Debug.LogError("AdventureBookPageDisplay: OnBossSelected() called when m_adventureBookPageContentsWidget is null!");
			return;
		}
		IDataModel dataModel;
		this.m_adventureBookPageContentsWidget.GetDataModel(2, out dataModel);
		AdventureBookPageDataModel adventureBookPageDataModel = dataModel as AdventureBookPageDataModel;
		if (adventureBookPageDataModel == null)
		{
			Error.AddDevWarning("UI Error", "No AdventureBookPageDataModel bound to the AdventureBookPageContents widget when the boss was selected!", Array.Empty<object>());
			return;
		}
		if (adventureBookPageDataModel.ChapterData == null)
		{
			Error.AddDevWarning("UI Error", "AdventureBookPageDataModel's ChapterData is null when the boss was selected!", Array.Empty<object>());
			return;
		}
		if (adventureBookPageDataModel.ChapterData.Missions.Count <= bossOffset)
		{
			Error.AddDevWarning("UI Error", "Selected boss index {0} but there are only {1} missions defined for Chapter {2}!", new object[]
			{
				bossOffset,
				adventureBookPageDataModel.ChapterData.Missions.Count,
				adventureBookPageDataModel.ChapterData.Name
			});
			return;
		}
		AdventureMissionDataModel adventureMissionDataModel = adventureBookPageDataModel.ChapterData.Missions[bossOffset];
		if (adventureMissionDataModel == null)
		{
			Error.AddDevWarning("UI Error", "AdventureMissionDataModel at index {0} for Chapter {1} is not valid!", new object[]
			{
				bossOffset,
				adventureBookPageDataModel.ChapterData.Name
			});
			return;
		}
		AdventureConfig.Get().SetMission(adventureMissionDataModel.ScenarioId, true);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00005DF8 File Offset: 0x00003FF8
	private void OnMissionSet(ScenarioDbId mission, bool showDetails)
	{
		IDataModel dataModel;
		this.m_adventureBookPageContentsWidget.GetDataModel(2, out dataModel);
		AdventureBookPageDataModel adventureBookPageDataModel = dataModel as AdventureBookPageDataModel;
		if (adventureBookPageDataModel != null && adventureBookPageDataModel.ChapterData != null)
		{
			foreach (AdventureMissionDataModel adventureMissionDataModel in adventureBookPageDataModel.ChapterData.Missions)
			{
				adventureMissionDataModel.Selected = (adventureMissionDataModel.ScenarioId == mission);
			}
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x00005E74 File Offset: 0x00004074
	private void OnChapterUnlockAnimationComplete(string eventName)
	{
		if (eventName != "CODE_UNLOCKED_ANIMATION_COMPLETE")
		{
			return;
		}
		this.m_isInUnlockedSequence = false;
		if (string.IsNullOrEmpty(this.m_currentUnlockButtonName))
		{
			Log.Adventures.PrintWarning("AdventureBookPageDisplay.OnChapterUnlockAnimationComplete: Current unlock button was not set, if this was manually activated outside the normal flow then this can be ignored.", Array.Empty<object>());
			return;
		}
		Clickable clickable = null;
		if (!this.m_chapterButtonClickablesNameMap.TryGetValue(this.m_currentUnlockButtonName, out clickable))
		{
			Log.Adventures.PrintError("AdventureBookPageDisplay.OnChapterUnlockAnimationComplete: Could not find current unlock button {0}.", new object[]
			{
				this.m_currentUnlockButtonName
			});
			return;
		}
		AdventureBookPageDisplay.ChapterButtonData chapterButtonData = clickable.GetData() as AdventureBookPageDisplay.ChapterButtonData;
		if (chapterButtonData != null)
		{
			AdventureConfig.Get().SetHasSeenUnlockedChapterPage((WingDbId)chapterButtonData.ChapterData.WingRecord.ID, false);
			AdventureConfig.AckCurrentWingProgress(chapterButtonData.ChapterData.WingRecord.ID);
			Log.Adventures.Print("Pressed {0} {1}!", new object[]
			{
				chapterButtonData.ButtonName,
				this.m_currentUnlockButtonName
			});
		}
		IDataModel dataModel;
		clickable.GetDataModel(3, out dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = false;
			adventureChapterDataModel.NewlyUnlocked = false;
			adventureChapterDataModel.ShowNewlyUnlockedHighlight = true;
		}
		this.m_currentUnlockButtonName = null;
		this.ShowChapterNewlyUnlockedMapSequenceIfNecessary();
		this.EnableInteraction(true);
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00005F94 File Offset: 0x00004194
	private void SetupAdventurePurchaseChoiceDialog(ChapterPageData pageData)
	{
		if (this.m_storeChooseWidget == null)
		{
			this.m_storeChooseWidget = WidgetInstance.Create(AdventureBookPageDisplay.m_chooseStoreWidgetPrefab, false);
			this.m_storeChooseWidget.transform.parent = base.transform;
		}
		this.m_storeChooseWidget.RegisterReadyListener(delegate(object _)
		{
			this.m_storeChooseWidget.BindDataModel(this.m_pageDataModel, false);
			FullScreenFXMgr.Get().StartStandardBlurVignette(this.m_popupEffectFadeTime);
			if (this.m_storeChooseBackButton == null)
			{
				this.m_storeChooseBackButton = this.m_storeChooseWidget.GetComponentInChildren<UIBButton>();
			}
			if (this.m_storeChoosePopup == null)
			{
				this.m_storeChoosePopup = this.m_storeChooseWidget.GetComponentInChildren<UIBPopup>();
			}
			this.m_storeChoosePopup.Show(false);
			Navigation.Push(new Navigation.NavigateBackHandler(this.HideStoreChoosePopup));
			this.m_storeChooseBackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent chooseEvent)
			{
				Navigation.GoBack();
			});
			this.m_storeChooseWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnBookStoreChosenEvent));
		}, null, true);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000600A File Offset: 0x0000420A
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod method)
	{
		if (!this.DoesBundleApplyToPage(bundle))
		{
			return;
		}
		if (this.m_storeChoosePopup != null && this.m_storeChoosePopup.IsShown())
		{
			Navigation.GoBack();
		}
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006038 File Offset: 0x00004238
	private bool HideStoreChoosePopup()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		if (this.m_storeChoosePopup == null)
		{
			return false;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(this.m_popupEffectFadeTime, null);
		this.m_storeChoosePopup.Hide();
		return true;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x00006088 File Offset: 0x00004288
	private static void StartFullBookPurchaseTransaction(PageData pageData, AdventureBookPageDataModel pageDataModel)
	{
		ChapterPageData chapterPageData = pageData as ChapterPageData;
		WingDbfRecord wingDbfRecord = (chapterPageData != null) ? chapterPageData.WingRecord : null;
		if (wingDbfRecord == null)
		{
			Debug.LogError("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: could not get the wing record from page data when trying to purchase the entire adventure book.");
			return;
		}
		WingDbfRecord firstUnownedAdventureWing = AdventureProgressMgr.Get().GetFirstUnownedAdventureWing((AdventureDbId)wingDbfRecord.AdventureId);
		if (firstUnownedAdventureWing == null)
		{
			Debug.LogError("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: could not find a first unowned wing - something went wrong!");
			return;
		}
		if (!AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(firstUnownedAdventureWing))
		{
			Debug.LogErrorFormat("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: You do not own wing {0}, you cannot purchase the entire adventure book starting at wing {1}!", new object[]
			{
				wingDbfRecord.OwnershipPrereqWingId,
				firstUnownedAdventureWing.ID
			});
			return;
		}
		StoreManager.Get().StartAdventureTransaction(ProductType.PRODUCT_TYPE_WING, wingDbfRecord.ID, null, null, ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET, 0, false, pageDataModel, firstUnownedAdventureWing.PmtProductIdForThisAndRestOfAdventure);
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00006130 File Offset: 0x00004330
	private static void StartSingleWingPurchaseTransaction(PageData pageData, AdventureBookPageDataModel pageDataModel)
	{
		ChapterPageData chapterPageData = pageData as ChapterPageData;
		WingDbfRecord wingDbfRecord = (chapterPageData != null) ? chapterPageData.WingRecord : null;
		if (wingDbfRecord == null)
		{
			Debug.LogError("AdventureBookPageDisplay.OnBookStoreChosenEvent: could not get the wing record from page data when trying to purchase a specific wing.");
			return;
		}
		if (!AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(wingDbfRecord))
		{
			Debug.LogErrorFormat("AdventureBookPageDisplay.OnBookStoreChosenEvent: You do not own wing {0}, you cannot purchase the wing on this page!", new object[]
			{
				wingDbfRecord.OwnershipPrereqWingId
			});
			return;
		}
		StoreManager.Get().StartAdventureTransaction(ProductType.PRODUCT_TYPE_WING, wingDbfRecord.ID, null, null, ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET, 0, false, pageDataModel, wingDbfRecord.PmtProductIdForSingleWingPurchase);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x000061A8 File Offset: 0x000043A8
	private void OnBookStoreChosenEvent(string eventName)
	{
		if (eventName == "book_selected")
		{
			if (this.m_storeChoosePopup != null && this.m_storeChoosePopup.IsShown())
			{
				Navigation.GoBack();
			}
			AdventureBookPageDisplay.StartFullBookPurchaseTransaction(this.m_pageData, this.m_pageDataModel);
			return;
		}
		if (eventName == "chapter_selected")
		{
			if (this.m_storeChoosePopup != null && this.m_storeChoosePopup.IsShown())
			{
				Navigation.GoBack();
			}
			AdventureBookPageDisplay.StartSingleWingPurchaseTransaction(this.m_pageData, this.m_pageDataModel);
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x00006234 File Offset: 0x00004434
	private void AdventureBookPageContentsIsReady(Widget bookPageContents)
	{
		this.m_adventureBookPageContentsWidget = bookPageContents;
		if (bookPageContents == null)
		{
			Error.AddDevWarning("Error", "Error: Adventure Book Page Contents Reference not hooked up to a Widget!", Array.Empty<object>());
			return;
		}
		base.StartCoroutine(this.SetUpBookPageReferencesWhenResolved(bookPageContents));
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00006269 File Offset: 0x00004469
	private IEnumerator SetUpBookPageReferencesWhenResolved(Widget bookPageContents)
	{
		while (bookPageContents.IsChangingStates)
		{
			yield return null;
		}
		bookPageContents.RegisterEventListener(new Widget.EventListenerDelegate(this.BookPageContentsEventListener));
		AdventureBookPageDisplayRefContainer componentInChildren = bookPageContents.gameObject.GetComponentInChildren<AdventureBookPageDisplayRefContainer>();
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "There is no AdventureBookPageDisplayRefContainer component on your AdventureBookPageContents Widget! This is necessary to initialize things like the Map Page.", Array.Empty<object>());
		}
		else
		{
			componentInChildren.m_AdventureBookMapReference.RegisterReadyListener<Widget>(new Action<Widget>(this.AdventureBookMapIsReady));
			componentInChildren.m_BasePageRendererReference.RegisterReadyListener<MeshRenderer>(new Action<MeshRenderer>(this.BasePageRendererIsReady));
		}
		yield break;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000627F File Offset: 0x0000447F
	private void BasePageRendererIsReady(MeshRenderer basePageRenderer)
	{
		this.m_basePageRenderer = basePageRenderer;
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00006288 File Offset: 0x00004488
	private void AdventureBookMapIsReady(Widget widget)
	{
		if (widget == null || !widget.IsReady)
		{
			Log.Adventures.PrintError("AdventureBookMap should be ready, but it's not!  Something terrible is happening!", Array.Empty<object>());
			return;
		}
		widget.RegisterEventListener(new Widget.EventListenerDelegate(this.OnChapterUnlockAnimationComplete));
		base.StartCoroutine(this.InitializeMapButtonsWhenResolved(widget));
	}

	// Token: 0x0600010D RID: 269 RVA: 0x000062DB File Offset: 0x000044DB
	public IEnumerator InitializeMapButtonsWhenResolved(Widget bookMapWidget)
	{
		AdventureBookPageDisplay.<>c__DisplayClass95_0 CS$<>8__locals1 = new AdventureBookPageDisplay.<>c__DisplayClass95_0();
		CS$<>8__locals1.<>4__this = this;
		while (bookMapWidget.IsChangingStates)
		{
			yield return null;
		}
		CS$<>8__locals1.mapData = (this.m_pageData as MapPageData);
		if (CS$<>8__locals1.mapData == null)
		{
			Log.Adventures.PrintError("SetUpPageWhenReady(): m_pageData is not a valid MapPageData! Cannot cast properly.", Array.Empty<object>());
			yield break;
		}
		ListOfChapterButtons componentInChildren = bookMapWidget.gameObject.GetComponentInChildren<ListOfChapterButtons>();
		if (componentInChildren == null)
		{
			yield break;
		}
		List<AsyncReference> chapterButtonClickableReferences = componentInChildren.m_ChapterButtonClickableReferences;
		if (chapterButtonClickableReferences.Count != CS$<>8__locals1.mapData.ChapterData.Count)
		{
			Error.AddDevWarning("Missing Adventure Buttons", "Error: there are not the same number of Chapter Buttons ({0}) as there are Chapters ({1}) defined for this Adventure!", new object[]
			{
				chapterButtonClickableReferences.Count,
				CS$<>8__locals1.mapData.ChapterData.Count
			});
		}
		this.m_chapterButtonClickablesNameMap.Clear();
		int i;
		int j;
		for (i = 0; i < chapterButtonClickableReferences.Count; i = j)
		{
			chapterButtonClickableReferences[i].RegisterReadyListener<Clickable>(delegate(Clickable chapterButton)
			{
				int num = i + 1;
				if (chapterButton == null)
				{
					Debug.LogErrorFormat("The reference to a ChapterButton at index {0} in the ListOfChapterButtons component is not a valid Clickable!", new object[]
					{
						num
					});
					return;
				}
				ChapterPageData chapterPageData;
				CS$<>8__locals1.mapData.ChapterData.TryGetValue(num, out chapterPageData);
				if (chapterPageData == null)
				{
					Log.Adventures.PrintError("No ChapterData in the MapPageData for Chapter {0}!", new object[]
					{
						num
					});
					return;
				}
				string text = chapterButton.gameObject.name + num;
				AdventureBookPageDisplay.ChapterButtonData data = new AdventureBookPageDisplay.ChapterButtonData
				{
					ChapterData = chapterPageData,
					ButtonName = text
				};
				CS$<>8__locals1.<>4__this.m_chapterButtonClickablesNameMap.Add(text, chapterButton);
				chapterButton.SetData(data);
				chapterButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(CS$<>8__locals1.<>4__this.OnChapterClickableRelease));
			});
			j = i + 1;
		}
		this.UpdateMapButtonData(false);
		yield break;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x000062F1 File Offset: 0x000044F1
	private void UpdateMapOnWingProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		this.UpdateMapButtonData(true);
	}

	// Token: 0x0600010F RID: 271 RVA: 0x000062FC File Offset: 0x000044FC
	private void UpdateMapButtonData(bool forceUpdate = false)
	{
		if (this.m_sortedChapterDataModels == null)
		{
			Debug.LogError("AdventureBookPageDisplay.UpdateMapButtonData() - m_sortedChapterDataModels is null!");
			return;
		}
		this.m_chapterNewlyUnlockedMapSequenceQueue.Clear();
		foreach (Clickable clickable in this.m_chapterButtonClickablesNameMap.Values)
		{
			AdventureBookPageDisplay.ChapterButtonData chapterButtonData = clickable.GetData() as AdventureBookPageDisplay.ChapterButtonData;
			if (chapterButtonData == null)
			{
				Log.Adventures.PrintError("Data on Chapter Button is not valid ChapterButtonData!", Array.Empty<object>());
			}
			else
			{
				AdventureChapterDataModel adventureChapterDataModel = this.m_sortedChapterDataModels.Find((AdventureChapterDataModel x) => x.ChapterNumber == chapterButtonData.ChapterData.ChapterNumber);
				if (adventureChapterDataModel == null)
				{
					Debug.LogErrorFormat("AdventureBookPageDisplay.UpdateMapButtonData() - No ChapterDataModel for Chapter {0} found in m_sortedChapterDataModels!", new object[]
					{
						chapterButtonData.ChapterData.ChapterNumber
					});
				}
				else
				{
					if (forceUpdate)
					{
						AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData(adventureChapterDataModel, chapterButtonData.ChapterData);
					}
					clickable.BindDataModel(adventureChapterDataModel, true, false);
					if (adventureChapterDataModel.NewlyUnlocked)
					{
						Log.Adventures.Print("Chapter {0} is newly unlocked!", new object[]
						{
							adventureChapterDataModel.ChapterNumber
						});
						this.m_chapterNewlyUnlockedMapSequenceQueue.Enqueue(chapterButtonData.ButtonName);
					}
					if (adventureChapterDataModel.NewlyCompleted)
					{
						Log.Adventures.Print("Chapter {0} is newly completed!", new object[]
						{
							adventureChapterDataModel.ChapterNumber
						});
					}
				}
			}
		}
	}

	// Token: 0x06000110 RID: 272 RVA: 0x00006488 File Offset: 0x00004688
	private static void UpdateChapterDataModelWithChapterData(AdventureChapterDataModel chapterDataModel, ChapterPageData chapterData)
	{
		WingDbfRecord wingRecord = chapterData.WingRecord;
		chapterDataModel.Name = wingRecord.Name;
		chapterDataModel.Description = wingRecord.Description;
		chapterDataModel.ChapterNumber = chapterData.ChapterNumber;
		chapterDataModel.WingId = wingRecord.ID;
		chapterDataModel.ChapterState = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(wingRecord, chapterData.AdventureMode);
		chapterDataModel.TimeLocked = !AdventureProgressMgr.IsWingEventActive(wingRecord.ID);
		chapterDataModel.UnlockChapterText = wingRecord.StoreBuyWingButtonLabel;
		chapterDataModel.StoreDescriptionText = wingRecord.StoreBuyWingDesc;
		chapterDataModel.IsAnomalyModeAvailable = AdventureUtils.IsAnomalyModeAvailable(chapterData.Adventure, chapterData.AdventureMode, (WingDbId)wingRecord.ID);
		if (chapterDataModel.TimeLocked)
		{
			chapterDataModel.TimeLockInfoMessage = wingRecord.ComingSoonLabel;
		}
		chapterDataModel.PlayerOwnsChapter = AdventureProgressMgr.Get().OwnsWing(wingRecord.ID);
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)chapterData.Adventure);
		if (chapterDataModel.PlayerOwnsChapter && wingRecord.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow && record != null && record.MapPageHasButtonsToChapters)
		{
			chapterDataModel.PlayerOwnsChapter = false;
		}
		chapterDataModel.IsPreviousChapterOwned = AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(wingRecord);
		WingDbfRecord record2 = GameDbf.Wing.GetRecord(wingRecord.OwnershipPrereqWingId);
		if (record2 != null && record2.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			chapterDataModel.IsPreviousChapterOwned = false;
		}
		chapterDataModel.AvailableForPurchase = (!chapterDataModel.PlayerOwnsChapter && chapterDataModel.IsPreviousChapterOwned && !GameUtils.IsModeHeroic(chapterData.AdventureMode));
		chapterDataModel.FinalPurchasableChapter = (wingRecord.PmtProductIdForThisAndRestOfAdventure == 0 && wingRecord.PmtProductIdForSingleWingPurchase != 0);
		int num;
		AdventureProgressMgr.Get().GetWingAck(wingRecord.ID, out num);
		chapterDataModel.NewlyUnlocked = (chapterDataModel.ChapterState == AdventureChapterState.UNLOCKED && num == 0);
		chapterDataModel.ShowNewlyUnlockedHighlight = !AdventureConfig.Get().GetHasSeenUnlockedChapterPage((WingDbId)chapterData.WingRecord.ID);
		List<int> guestHeroesForWing = AdventureConfig.GetGuestHeroesForWing(wingRecord.ID);
		if (guestHeroesForWing != null && guestHeroesForWing.Count != 0)
		{
			chapterDataModel.FirstHeroBundledWithChapter = guestHeroesForWing[0];
			if (guestHeroesForWing.Count >= 2)
			{
				chapterDataModel.SecondHeroBundledWithChapter = guestHeroesForWing[1];
			}
			if (guestHeroesForWing.Count > 2)
			{
				Log.Adventures.Print("{0} Guest Heroes defined for Wing {0}, but we only have room in the data model for 2!", new object[]
				{
					guestHeroesForWing.Count,
					wingRecord.ID
				});
			}
		}
		chapterDataModel.DisplayRaidBossHealth = wingRecord.DisplayRaidBossHealth;
		chapterDataModel.RaidBossHealthAmount = 0;
		if (chapterDataModel.DisplayRaidBossHealth)
		{
			string text = GameUtils.TranslateDbIdToCardId(wingRecord.RaidBossCardId, false);
			if (text == null || wingRecord.RaidBossCardId == 0)
			{
				Log.Adventures.PrintWarning("AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData() - No cardId for raid boss dbId {0}!", new object[]
				{
					wingRecord.RaidBossCardId
				});
			}
			else
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
				if (entityDef == null)
				{
					Log.Adventures.PrintWarning("AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData() - No EntityDef for raid boss card ID {0}!", new object[]
					{
						text
					});
				}
				else
				{
					chapterDataModel.RaidBossStartingHealthAmount = entityDef.GetTag(GAME_TAG.HEALTH);
					chapterDataModel.RaidBossHealthAmount = chapterDataModel.RaidBossStartingHealthAmount;
				}
			}
			AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)chapterData.Adventure, (int)chapterData.AdventureMode);
			if (adventureDataRecord != null)
			{
				GameSaveKeyId gameSaveDataServerKey = (GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey;
				List<long> list;
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_FINAL_BOSS_HEALTH, out list);
				int sortedWingUnlockIndex = GameUtils.GetSortedWingUnlockIndex(wingRecord);
				if (list != null && list.Count > sortedWingUnlockIndex)
				{
					chapterDataModel.RaidBossHealthAmount = Mathf.Clamp((int)list[sortedWingUnlockIndex], 0, chapterDataModel.RaidBossStartingHealthAmount);
				}
			}
		}
		chapterDataModel.CompletionRewards = new RewardListDataModel();
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.ADVENTURE_CHEST
		};
		List<RewardData> list2 = new List<RewardData>();
		List<global::Achievement> achievesForAdventureWing = AchieveManager.Get().GetAchievesForAdventureWing(wingRecord.ID);
		foreach (global::Achievement achievement in achievesForAdventureWing)
		{
			if (achievement.Scenarios.Count <= 0)
			{
				list2.AddRange(AchieveManager.Get().GetRewardsForAchieve(achievement.ID, rewardTimings));
			}
		}
		chapterDataModel.CompletionRewardsEarned = false;
		chapterDataModel.CompletionRewardsNewlyEarned = false;
		AdventureBookPageDisplay.Legacy_SetChapterCompletionRewardData(chapterDataModel, list2);
		foreach (RewardData rewardData in list2)
		{
			RewardItemDataModel rewardItemDataModel = RewardUtils.RewardDataToRewardItemDataModel(rewardData);
			if (rewardItemDataModel != null)
			{
				chapterDataModel.CompletionRewards.Items.Add(rewardItemDataModel);
			}
			if (rewardData.Origin == NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT)
			{
				global::Achievement achievement2 = AchieveManager.Get().GetAchievement((int)rewardData.OriginData);
				chapterDataModel.CompletionRewardsEarned |= achievement2.IsCompleted();
				chapterDataModel.CompletionRewardsNewlyEarned |= achievement2.IsNewlyCompleted();
			}
			else
			{
				Error.AddDevWarning("Reward Error!", "Wing Reward is from origin {0}, but we expected origin == ACHIEVEMENT!", new object[]
				{
					rewardData.Origin
				});
			}
		}
		chapterDataModel.PurchaseRewards = new RewardListDataModel();
		List<RewardData> list3 = new List<RewardData>();
		HashSet<Assets.Achieve.RewardTiming> rewardTimings2 = new HashSet<Assets.Achieve.RewardTiming>
		{
			Assets.Achieve.RewardTiming.IMMEDIATE
		};
		foreach (global::Achievement achievement3 in achievesForAdventureWing)
		{
			if (achievement3.AchieveTrigger == Assets.Achieve.Trigger.LICENSEDETECTED)
			{
				list3.AddRange(AchieveManager.Get().GetRewardsForAchieve(achievement3.ID, rewardTimings2));
			}
		}
		foreach (RewardData rewardData2 in list3)
		{
			RewardItemDataModel rewardItemDataModel2 = RewardUtils.RewardDataToRewardItemDataModel(rewardData2);
			if (rewardItemDataModel2 != null)
			{
				chapterDataModel.PurchaseRewards.Items.Add(rewardItemDataModel2);
			}
		}
		int mission = (int)AdventureConfig.Get().GetMission();
		chapterDataModel.Missions.Clear();
		bool flag = false;
		foreach (ScenarioDbfRecord scenarioDbfRecord in chapterData.ScenarioRecords)
		{
			AdventureMissionDataModel missionDataModel = new AdventureMissionDataModel();
			missionDataModel.Rewards = new RewardListDataModel();
			missionDataModel.ScenarioId = (ScenarioDbId)scenarioDbfRecord.ID;
			missionDataModel.Selected = (mission == scenarioDbfRecord.ID);
			missionDataModel.MissionState = AdventureProgressMgr.Get().AdventureMissionStateForScenario(scenarioDbfRecord.ID);
			HashSet<Assets.Achieve.RewardTiming> rewardTimings3 = new HashSet<Assets.Achieve.RewardTiming>
			{
				Assets.Achieve.RewardTiming.ADVENTURE_CHEST,
				Assets.Achieve.RewardTiming.IMMEDIATE
			};
			List<RewardData> rewardsForDefeatingScenario = AdventureProgressMgr.Get().GetRewardsForDefeatingScenario(scenarioDbfRecord.ID, rewardTimings3);
			missionDataModel.Rewards.Items.Clear();
			foreach (RewardData rewardData3 in rewardsForDefeatingScenario)
			{
				RewardItemDataModel rewardItemDataModel3 = RewardUtils.RewardDataToRewardItemDataModel(rewardData3);
				if (rewardItemDataModel3 != null)
				{
					missionDataModel.Rewards.Items.Add(rewardItemDataModel3);
				}
			}
			AdventureConfig.Get().LoadBossDef((ScenarioDbId)scenarioDbfRecord.ID, delegate(AdventureBossDef bossDef, bool success)
			{
				if (bossDef != null)
				{
					missionDataModel.CoinPortraitMaterial = bossDef.m_CoinPortraitMaterial.GetMaterial();
				}
			});
			int num2 = 0;
			int num3 = 0;
			bool flag2 = AdventureConfig.IsMissionNewlyAvailableAndGetReqs((int)missionDataModel.ScenarioId, ref num2, ref num3);
			missionDataModel.NewlyUnlocked = (missionDataModel.MissionState == AdventureMissionState.UNLOCKED && flag2);
			bool flag3 = false;
			if (AdventureConfig.Get().IsScenarioDefeatedAndInitCache((ScenarioDbId)scenarioDbfRecord.ID))
			{
				flag3 = AdventureConfig.Get().IsScenarioJustDefeated((ScenarioDbId)scenarioDbfRecord.ID);
			}
			missionDataModel.NewlyCompleted = (missionDataModel.MissionState == AdventureMissionState.COMPLETED && flag3);
			if (missionDataModel.NewlyCompleted)
			{
				flag = true;
			}
			chapterDataModel.Missions.Add(missionDataModel);
		}
		chapterDataModel.NewlyCompleted = (chapterDataModel.ChapterState == AdventureChapterState.COMPLETED && flag);
		chapterDataModel.MoralAlignment = AdventureBookPageDisplay.ConvertBookSectionToMoralAlignment(chapterData.BookSection);
	}

	// Token: 0x06000111 RID: 273 RVA: 0x00006CDC File Offset: 0x00004EDC
	private static void Legacy_SetChapterCompletionRewardData(AdventureChapterDataModel chapterDataModel, List<RewardData> wingCompletionRewards)
	{
		RewardData rewardData = null;
		if (wingCompletionRewards.Count > 0)
		{
			rewardData = wingCompletionRewards[0];
		}
		if (rewardData is BoosterPackRewardData)
		{
			chapterDataModel.CompletionRewardType = rewardData.RewardType;
			BoosterPackRewardData boosterPackRewardData = rewardData as BoosterPackRewardData;
			chapterDataModel.CompletionRewardId = boosterPackRewardData.Id;
			chapterDataModel.CompletionRewardQuantity = boosterPackRewardData.Count;
			return;
		}
		chapterDataModel.CompletionRewardType = Reward.Type.NONE;
		chapterDataModel.CompletionRewardId = 0;
		chapterDataModel.CompletionRewardQuantity = 0;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x00006D45 File Offset: 0x00004F45
	private IEnumerator ShowPageUpdateVisualsWhenReady()
	{
		while (!this.m_allInitialTransitionsComplete)
		{
			yield return null;
		}
		if (this.m_pageData.PageType == AdventureBookPageType.MAP)
		{
			if (AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence)
			{
				base.StartCoroutine(this.AnimateAdventureSectionComplete());
			}
			else
			{
				this.ShowChapterNewlyUnlockedMapSequenceIfNecessary();
			}
		}
		else if (this.m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			base.StartCoroutine(this.AnimateChapterRewardsAndCompletionIfNecessary());
		}
		yield break;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00006D54 File Offset: 0x00004F54
	private void ShowChapterNewlyUnlockedMapSequenceIfNecessary()
	{
		if (this.m_chapterNewlyUnlockedMapSequenceQueue.Count <= 0 || this.m_isInUnlockedSequence || AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			return;
		}
		string text = this.m_chapterNewlyUnlockedMapSequenceQueue.Dequeue();
		Clickable clickable = this.m_chapterButtonClickablesNameMap[text];
		if (clickable == null)
		{
			Log.Adventures.PrintError("m_chapterNewlyUnlockedPopupQueue had an invalid button name! Skipping...", Array.Empty<object>());
			this.ShowChapterNewlyUnlockedMapSequenceIfNecessary();
			return;
		}
		if (clickable.GetComponent<VisualController>() == null)
		{
			Error.AddDevWarning("Missing Visual Controller", "{0} does not have a visual controller!", new object[]
			{
				text
			});
			return;
		}
		IDataModel dataModel;
		clickable.GetDataModel(3, out dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = true;
		}
		this.m_currentUnlockButtonName = text;
		this.m_isInUnlockedSequence = true;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x00006E12 File Offset: 0x00005012
	public void ShowNewlyPurchasedSequenceOnChapterPage()
	{
		if (this.m_pageData.PageType != AdventureBookPageType.CHAPTER)
		{
			Debug.LogWarning("AdventureBookPageDisplay.ShowNewlyPurchasedSequenceOnChapterPage() called on a non-Chapter page!  This is not supported!");
			return;
		}
		base.StartCoroutine(this.AnimateNewlyPurchasedSequenceOnChapterPage());
	}

	// Token: 0x06000115 RID: 277 RVA: 0x00006E3A File Offset: 0x0000503A
	private IEnumerator AnimateNewlyPurchasedSequenceOnChapterPage()
	{
		this.m_chapterNewlyPurchasedAnimFinished = false;
		this.m_adventureBookPageContentsWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.ChapterNewlyPurchasedAnimEventListener));
		this.m_adventureBookPageContentsWidget.TriggerEvent("PLAY_CHAPTER_NEWLY_PURCHASED_ANIM", default(Widget.TriggerEventParameters));
		while (!this.m_chapterNewlyPurchasedAnimFinished)
		{
			yield return null;
		}
		this.m_adventureBookPageContentsWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.ChapterNewlyPurchasedAnimEventListener));
		this.RefreshPage();
		yield break;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00006E49 File Offset: 0x00005049
	private void RefreshPage()
	{
		this.SetupPageDataModels(this.m_pageData);
		base.StartCoroutine(this.ShowPageUpdateVisualsWhenReady());
		AdventureConfig.Get().SetMission(AdventureConfig.Get().GetMission(), true);
	}

	// Token: 0x06000117 RID: 279 RVA: 0x00006E79 File Offset: 0x00005079
	private IEnumerator AnimateAdventureSectionComplete()
	{
		this.EnableInteraction(false);
		AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence = false;
		this.m_readyToPlayAdventureNewlyCompletedVO = false;
		this.m_adventureNewlyCompletedSequenceFinished = false;
		this.m_adventureBookPageContentsWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.AdventureNewlyCompletedEventListener));
		this.m_adventureBookPageContentsWidget.TriggerEvent("AdventureNewlyCompletedSequence", default(Widget.TriggerEventParameters));
		while (!this.m_readyToPlayAdventureNewlyCompletedVO)
		{
			yield return null;
		}
		ChapterPageData chapterPageData = this.m_pageData as ChapterPageData;
		WingDbId wingId = (WingDbId)((chapterPageData != null) ? chapterPageData.WingRecord.ID : 0);
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		DungeonCrawlSubDef_VOLines.VOEventType eventType = DungeonCrawlSubDef_VOLines.VOEventType.INVALID;
		if (chapterPageData == null || chapterPageData.BookSection == 0)
		{
			eventType = (GameUtils.IsModeHeroic(selectedMode) ? DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS);
		}
		else
		{
			eventType = (GameUtils.IsModeHeroic(selectedMode) ? DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION);
		}
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, eventType, 0, true);
		while (!this.m_adventureNewlyCompletedSequenceFinished)
		{
			yield return null;
		}
		this.m_adventureBookPageContentsWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.AdventureNewlyCompletedEventListener));
		if (UserAttentionManager.CanShowAttentionGrabber("AdventureBookPageDisplay.AnimateAdventureComplete"))
		{
			AdventureBookPageDisplay.<>c__DisplayClass105_0 CS$<>8__locals1 = new AdventureBookPageDisplay.<>c__DisplayClass105_0();
			CS$<>8__locals1.allPopupsShown = false;
			PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate()
			{
				CS$<>8__locals1.allPopupsShown = true;
			});
			while (!CS$<>8__locals1.allPopupsShown)
			{
				yield return null;
			}
			CS$<>8__locals1 = null;
		}
		this.EnableInteraction(true);
		yield break;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00006E88 File Offset: 0x00005088
	private IEnumerator AnimateChapterRewardsAndCompletionIfNecessary()
	{
		if (this.m_needToShowMissionCompleteAnim)
		{
			this.EnableInteraction(false);
			this.m_missionCompleteAnimFinished = false;
			this.m_adventureBookPageContentsWidget.TriggerEvent("MISSION_NEWLY_COMPLETED", default(Widget.TriggerEventParameters));
			while (!this.m_missionCompleteAnimFinished)
			{
				yield return null;
			}
			this.EnableInteraction(true);
		}
		if (this.m_needToShowRewardChestAnim)
		{
			this.EnableInteraction(false);
			this.m_rewardChestReadyToShowPopup = false;
			this.m_adventureBookPageContentsWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.RewardChestAnimEventListener));
			this.m_adventureBookPageContentsWidget.TriggerEvent("OPEN_CHAPTER_CHEST_REWARD", default(Widget.TriggerEventParameters));
			while (!this.m_rewardChestReadyToShowPopup)
			{
				yield return null;
			}
			this.m_adventureBookPageContentsWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.RewardChestAnimEventListener));
			if (UserAttentionManager.CanShowAttentionGrabber("AdventureBookPageDisplay.AnimateChapterRewardsAndCompletionIfNecessary"))
			{
				AdventureBookPageDisplay.<>c__DisplayClass106_0 CS$<>8__locals1 = new AdventureBookPageDisplay.<>c__DisplayClass106_0();
				CS$<>8__locals1.allPopupsShown = false;
				if (AdventureScene.Get().IsDevMode)
				{
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = "Dummy Reward Popup";
					popupInfo.m_text = "This is when the reward popup would be shown if you had actually earned it!";
					popupInfo.m_showAlertIcon = false;
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
					{
						CS$<>8__locals1.allPopupsShown = true;
					};
					DialogManager.Get().ShowPopup(popupInfo);
				}
				else
				{
					PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate()
					{
						CS$<>8__locals1.allPopupsShown = true;
					});
				}
				while (!CS$<>8__locals1.allPopupsShown)
				{
					yield return null;
				}
				CS$<>8__locals1 = null;
			}
			this.EnableInteraction(true);
		}
		if (this.m_needToShowMissionCompleteAnim || this.m_needToShowRewardChestAnim)
		{
			foreach (AdventureMissionDataModel adventureMissionDataModel in this.m_pageDataModel.ChapterData.Missions)
			{
				adventureMissionDataModel.NewlyCompleted = false;
			}
		}
		if (this.m_needToShowMissionUnlockAnim)
		{
			this.EnableInteraction(false);
			this.m_missionUnlockAnimFinished = false;
			this.m_adventureBookPageContentsWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.MissionNewlyUnlockedAnimEventListener));
			this.m_adventureBookPageContentsWidget.TriggerEvent("MISSION_NEWLY_UNLOCKED", default(Widget.TriggerEventParameters));
			this.AckMissionUnlocksOnCurrentPage();
			while (!this.m_missionUnlockAnimFinished)
			{
				yield return null;
			}
			this.m_adventureBookPageContentsWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.MissionNewlyUnlockedAnimEventListener));
			foreach (AdventureMissionDataModel adventureMissionDataModel2 in this.m_pageDataModel.ChapterData.Missions)
			{
				adventureMissionDataModel2.NewlyUnlocked = false;
			}
			this.EnableInteraction(true);
		}
		if (this.m_needToShowChapterCompletionAnim)
		{
			this.EnableInteraction(false);
			this.m_chapterCompletionAnimFinished = false;
			this.m_adventureBookPageContentsWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.ChapterNewlyCompletedAnimEventListener));
			this.m_adventureBookPageContentsWidget.TriggerEvent("CHAPTER_NEWLY_COMPLETED", default(Widget.TriggerEventParameters));
			ChapterPageData chapterData = this.m_pageData as ChapterPageData;
			if (chapterData != null && chapterData.ChapterToFlipToWhenCompleted == 0)
			{
				AdventureConfig.AckCurrentWingProgress(chapterData.WingRecord.ID);
			}
			while (!this.m_chapterCompletionAnimFinished)
			{
				yield return null;
			}
			this.m_adventureBookPageContentsWidget.RemoveEventListener(new Widget.EventListenerDelegate(this.ChapterNewlyCompletedAnimEventListener));
			if (GameUtils.GetNormalModeFromHeroicMode(AdventureConfig.Get().GetSelectedMode()) != AdventureModeDbId.DUNGEON_CRAWL)
			{
				this.PlayChapterCompleteVO();
				while (NotificationManager.Get().IsQuotePlaying)
				{
					yield return null;
				}
			}
			AdventureDbfRecord adventureDbfRecord = (chapterData == null) ? null : GameDbf.Adventure.GetRecord((int)chapterData.Adventure);
			bool flag = adventureDbfRecord != null && adventureDbfRecord.MapPageHasButtonsToChapters;
			if (flag && AdventureConfig.Get().HasUnacknowledgedChapterUnlocks())
			{
				AdventureBookPageManager.NavigateToMapPage();
			}
			else if (AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence)
			{
				if (flag)
				{
					AdventureBookPageManager.NavigateToMapPage();
				}
				else
				{
					base.StartCoroutine(this.AnimateAdventureSectionComplete());
				}
			}
			else if (chapterData != null && chapterData.ChapterToFlipToWhenCompleted != 0)
			{
				if (this.m_flipToChapterCallback != null)
				{
					this.m_flipToChapterCallback(chapterData.ChapterToFlipToWhenCompleted);
				}
				this.EnableInteraction(true);
			}
			else
			{
				this.EnableInteraction(true);
			}
			chapterData = null;
		}
		yield break;
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00006E98 File Offset: 0x00005098
	private void AckMissionUnlocksOnCurrentPage()
	{
		HashSet<int> hashSet = new HashSet<int>();
		foreach (AdventureMissionDataModel adventureMissionDataModel in this.m_pageDataModel.ChapterData.Missions)
		{
			if (adventureMissionDataModel.NewlyUnlocked)
			{
				int num = 0;
				int item = 0;
				if (AdventureConfig.GetMissionPlayableParameters((int)adventureMissionDataModel.ScenarioId, ref item, ref num))
				{
					hashSet.Add(item);
				}
			}
		}
		foreach (int wingId in hashSet)
		{
			AdventureConfig.AckCurrentWingProgress(wingId);
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00006F54 File Offset: 0x00005154
	private void PlayChapterCompleteVO()
	{
		ChapterPageData chapterPageData = this.m_pageData as ChapterPageData;
		WingDbId wingId = (WingDbId)((chapterPageData != null) ? chapterPageData.WingRecord.ID : 0);
		AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(wingId);
		if (AdventureUtils.CanPlayWingCompleteQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_CompleteQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_CompleteQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_CompleteQuoteVOLine, false, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00006FCC File Offset: 0x000051CC
	private void BookPageContentsEventListener(string eventName)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
		if (num <= 1776546145U)
		{
			if (num != 731095978U)
			{
				if (num != 1647974134U)
				{
					if (num == 1776546145U)
					{
						if (eventName == "BOSS_4_SELECTED")
						{
							this.OnBossSelected(3);
						}
					}
				}
				else if (eventName == "MISSION_NEWLY_COMPLETED_ANIM_FINISHED")
				{
					this.m_missionCompleteAnimFinished = true;
				}
			}
			else if (eventName == "CHAPTER_UNLOCK_BUTTON_CLICKED")
			{
				this.OnChapterUnlockButtonClicked();
			}
		}
		else if (num <= 2858798930U)
		{
			if (num != 1831878848U)
			{
				if (num == 2858798930U)
				{
					if (eventName == "BOSS_3_SELECTED")
					{
						this.OnBossSelected(2);
					}
				}
			}
			else if (eventName == "BOSS_5_SELECTED")
			{
				this.OnBossSelected(4);
			}
		}
		else if (num != 3165605604U)
		{
			if (num == 3666758611U)
			{
				if (eventName == "BOSS_2_SELECTED")
				{
					this.OnBossSelected(1);
				}
			}
		}
		else if (eventName == "BOSS_1_SELECTED")
		{
			this.OnBossSelected(0);
		}
		if (this.m_pageEventListener != null)
		{
			this.m_pageEventListener(eventName);
		}
	}

	// Token: 0x0600011C RID: 284 RVA: 0x000070F9 File Offset: 0x000052F9
	private void RewardChestAnimEventListener(string eventName)
	{
		if ("READY_TO_SHOW_POPUP".Equals(eventName))
		{
			this.m_rewardChestReadyToShowPopup = true;
		}
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000710F File Offset: 0x0000530F
	private void ChapterNewlyCompletedAnimEventListener(string eventName)
	{
		if ("CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED".Equals(eventName))
		{
			this.m_chapterCompletionAnimFinished = true;
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00007125 File Offset: 0x00005325
	private void AdventureNewlyCompletedEventListener(string eventName)
	{
		if ("PlayAdventureNewlyCompletedVO".Equals(eventName))
		{
			this.m_readyToPlayAdventureNewlyCompletedVO = true;
			return;
		}
		if ("AdventureNewlyCompletedSequenceFinished".Equals(eventName))
		{
			this.m_adventureNewlyCompletedSequenceFinished = true;
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00007150 File Offset: 0x00005350
	private void MissionNewlyUnlockedAnimEventListener(string eventName)
	{
		if ("MISSION_NEWLY_UNLOCKED_ANIM_FINISHED".Equals(eventName))
		{
			this.m_missionUnlockAnimFinished = true;
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x00007166 File Offset: 0x00005366
	private void ChapterNewlyPurchasedAnimEventListener(string eventName)
	{
		if ("CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED".Equals(eventName))
		{
			this.m_chapterNewlyPurchasedAnimFinished = true;
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000717C File Offset: 0x0000537C
	private void EnableInteraction(bool enable)
	{
		if (this.m_enableInteractionCallback != null)
		{
			this.m_enableInteractionCallback(enable);
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00007194 File Offset: 0x00005394
	private void UpdateRewardPageData(PageData pageData)
	{
		RewardPageData rewardPageData = pageData as RewardPageData;
		if (rewardPageData == null)
		{
			Debug.LogError("UpdateRewardPageData(): PageData is not a valid RewardPageData! Cannot cast properly.");
		}
		else
		{
			this.m_pageDataModel.AllChaptersCompletedInCurrentSection = true;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (ChapterPageData chapterPageData in rewardPageData.ChapterData.Values)
			{
				if (chapterPageData.BookSection == pageData.BookSection)
				{
					num2 += chapterPageData.ScenarioRecords.Count;
					foreach (ScenarioDbfRecord scenarioDbfRecord in chapterPageData.ScenarioRecords)
					{
						bool flag = AdventureProgressMgr.Get().HasDefeatedScenario(scenarioDbfRecord.ID);
						if (flag)
						{
							num++;
						}
						else
						{
							this.m_pageDataModel.AllChaptersCompletedInCurrentSection = false;
						}
						HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
						{
							Assets.Achieve.RewardTiming.ADVENTURE_CHEST,
							Assets.Achieve.RewardTiming.IMMEDIATE
						};
						foreach (RewardData rewardData in AdventureProgressMgr.Get().GetRewardsForDefeatingScenario(scenarioDbfRecord.ID, rewardTimings))
						{
							if (rewardData.RewardType == Reward.Type.CARD)
							{
								CardRewardData cardRewardData = rewardData as CardRewardData;
								if (cardRewardData == null)
								{
									Debug.LogErrorFormat("AdventureBookPageDisplay.UpdateRewardPageData() - reward {0} is type CARD but is not a CardRewardData!", new object[]
									{
										rewardData
									});
								}
								else
								{
									num4 += cardRewardData.Count;
									if (flag)
									{
										num3 += cardRewardData.Count;
									}
								}
							}
						}
					}
				}
			}
			this.m_pageDataModel.NumBossesDefeatedText = GameStrings.Format("GLUE_ADVENTURE_NUM_BOSSES_DEFEATED", new object[]
			{
				num,
				num2
			});
			this.m_pageDataModel.NumCardsCollectedText = GameStrings.Format("GLUE_ADVENTURE_NUM_CARDS_COLLECTED", new object[]
			{
				num3,
				num4
			});
		}
		Reward.Type completionRewardType = AdventureConfig.Get().CompletionRewardType;
		if (completionRewardType == Reward.Type.CARD_BACK)
		{
			int completionRewardId = AdventureConfig.Get().CompletionRewardId;
			if (!CardBackManager.Get().LoadCardBackByIndex(completionRewardId, new CardBackManager.LoadCardBackData.LoadCardBackCallback(this.OnCardBackLoaded), null))
			{
				Log.Adventures.PrintError("AdventureBookPageDisplay.SetCardBack() - failed to load CardBack {0}", new object[]
				{
					completionRewardId
				});
				return;
			}
		}
		else if (completionRewardType != Reward.Type.NONE)
		{
			Log.Adventures.PrintWarning("Unsupported reward type for Reward Page = {0}", new object[]
			{
				completionRewardType
			});
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x00007450 File Offset: 0x00005650
	private void OnCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		Actor componentInChildren = base.GetComponentInChildren<Actor>();
		if (componentInChildren != null)
		{
			CardBackManager.SetCardBack(componentInChildren.m_cardMesh, cardbackData.m_CardBack);
			componentInChildren.SetCardbackUpdateIgnore(true);
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x00007488 File Offset: 0x00005688
	private void CheckForInputForCheats()
	{
		if (!AdventureScene.Get().IsDevMode || !base.IsShown)
		{
			return;
		}
		if (this.m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			AdventureChapterDataModel chapterData = this.m_pageDataModel.ChapterData;
			if (InputCollection.GetKeyDown(KeyCode.Z))
			{
				AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence = !AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence;
				if (AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence)
				{
					this.m_needToShowChapterCompletionAnim = true;
				}
				UIStatus.Get().AddInfo(string.Format("Adventure Completion anim {0} be played when you press Spacebar.", AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence ? "WILL" : "will NOT"));
				chapterData.NewlyCompleted = this.m_needToShowChapterCompletionAnim;
				return;
			}
			if (InputCollection.GetKeyDown(KeyCode.V))
			{
				this.m_needToShowChapterCompletionAnim = !this.m_needToShowChapterCompletionAnim;
				UIStatus.Get().AddInfo(string.Format("Chapter Completion anim {0} be played when you press Spacebar.", this.m_needToShowChapterCompletionAnim ? "WILL" : "will NOT"));
				chapterData.NewlyCompleted = this.m_needToShowChapterCompletionAnim;
				return;
			}
			if (InputCollection.GetKeyDown(KeyCode.C))
			{
				this.m_needToShowRewardChestAnim = !this.m_needToShowRewardChestAnim;
				UIStatus.Get().AddInfo(string.Format("Reward Chest anim {0} be played when you press Spacebar.", this.m_needToShowRewardChestAnim ? "WILL" : "will NOT"));
				chapterData.CompletionRewardsEarned = true;
				chapterData.CompletionRewardsNewlyEarned = this.m_needToShowRewardChestAnim;
				if (chapterData.Missions.Count > 0)
				{
					chapterData.Missions[0].NewlyCompleted = this.m_needToShowRewardChestAnim;
				}
				if (this.m_needToShowRewardChestAnim && chapterData.ChapterState == AdventureChapterState.COMPLETED)
				{
					chapterData.NewlyCompleted = true;
					return;
				}
			}
			else if (InputCollection.GetKeyDown(KeyCode.Space))
			{
				if (!this.m_needToShowChapterCompletionAnim && !this.m_needToShowRewardChestAnim)
				{
					UIStatus.Get().AddInfo("You attempted to play the reward sequence, but you have not enabled\nthe Reward Chest anim (key C) or Chapter Complete anim (key V).");
					return;
				}
				base.StopCoroutine(this.AnimateChapterRewardsAndCompletionIfNecessary());
				base.StartCoroutine(this.AnimateChapterRewardsAndCompletionIfNecessary());
				return;
			}
		}
		else if (this.m_pageData.PageType == AdventureBookPageType.MAP && InputCollection.GetKeyDown(KeyCode.Z))
		{
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)AdventureConfig.Get().GetSelectedAdventure());
			if (record != null && record.MapPageHasButtonsToChapters)
			{
				base.StopCoroutine(this.AnimateAdventureSectionComplete());
				base.StartCoroutine(this.AnimateAdventureSectionComplete());
			}
		}
	}

	// Token: 0x0400009D RID: 157
	public AsyncReference m_AdventureBookPageContentsReference;

	// Token: 0x0400009E RID: 158
	public float m_popupEffectFadeTime = 0.25f;

	// Token: 0x0400009F RID: 159
	private const string CHAPTER_UNLOCK_ANIMATION_COMPLETE = "CODE_UNLOCKED_ANIMATION_COMPLETE";

	// Token: 0x040000A0 RID: 160
	private const string CHAPTER_BUTTON_POPUP_DISMISS_EVENT_NAME = "CODE_HIDE_AND_DISMISS";

	// Token: 0x040000A1 RID: 161
	private const string BOOK_MAP_EVENT_NAME = "ShowBookMapPage";

	// Token: 0x040000A2 RID: 162
	private const string BOOK_CARD_BACK_EVENT_NAME = "ShowCardBackPage";

	// Token: 0x040000A3 RID: 163
	private const string BOOK_CHAPTER_EVENT_NAME = "ShowChapterPage";

	// Token: 0x040000A4 RID: 164
	private const string REWARD_CHEST_OPEN_ANIMATION_EVENT_NAME = "OPEN_CHAPTER_CHEST_REWARD";

	// Token: 0x040000A5 RID: 165
	private const string REWARD_CHEST_READY_TO_SHOW_POPUP_EVENT_NAME = "READY_TO_SHOW_POPUP";

	// Token: 0x040000A6 RID: 166
	private const string CHAPTER_NEWLY_COMPLETED_ANIMATION_EVENT_NAME = "CHAPTER_NEWLY_COMPLETED";

	// Token: 0x040000A7 RID: 167
	private const string CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED_EVENT_NAME = "CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED";

	// Token: 0x040000A8 RID: 168
	private const string ADVENTURE_NEWLY_COMPLETED_SEQUENCE_EVENT_NAME = "AdventureNewlyCompletedSequence";

	// Token: 0x040000A9 RID: 169
	private const string PLAY_ADVENTURE_NEWLY_COMPLETED_VO_EVENT_NAME = "PlayAdventureNewlyCompletedVO";

	// Token: 0x040000AA RID: 170
	private const string ADVENTURE_NEWLY_COMPLETED_SEQUENCE_FINISHED_EVENT_NAME = "AdventureNewlyCompletedSequenceFinished";

	// Token: 0x040000AB RID: 171
	private const string MISSION_NEWLY_COMPLETED_ANIM_EVENT_NAME = "MISSION_NEWLY_COMPLETED";

	// Token: 0x040000AC RID: 172
	private const string MISSION_NEWLY_COMPLETED_ANIM_FINISHED_EVENT_NAME = "MISSION_NEWLY_COMPLETED_ANIM_FINISHED";

	// Token: 0x040000AD RID: 173
	private const string MISSION_NEWLY_UNLOCKED_ANIM_EVENT_NAME = "MISSION_NEWLY_UNLOCKED";

	// Token: 0x040000AE RID: 174
	private const string MISSION_NEWLY_UNLOCKED_ANIM_FINISHED_EVENT_NAME = "MISSION_NEWLY_UNLOCKED_ANIM_FINISHED";

	// Token: 0x040000AF RID: 175
	private const string CHAPTER_NEWLY_PURCHASED_SEQUENCE_EVENT_NAME = "PLAY_CHAPTER_NEWLY_PURCHASED_ANIM";

	// Token: 0x040000B0 RID: 176
	private const string CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED_EVENT_NAME = "CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED";

	// Token: 0x040000B1 RID: 177
	private const string PURCHASE_INDIVIDUAL_WING_EVENT_NAME = "chapter_selected";

	// Token: 0x040000B2 RID: 178
	private const string PURCHASE_BOOK_EVENT_NAME = "book_selected";

	// Token: 0x040000B3 RID: 179
	private const string CHAPTER_UNLOCK_BUTTON_CLICKED_EVENT_NAME = "CHAPTER_UNLOCK_BUTTON_CLICKED";

	// Token: 0x040000B4 RID: 180
	private const string BOSS_1_SELECTED_EVENT_NAME = "BOSS_1_SELECTED";

	// Token: 0x040000B5 RID: 181
	private const string BOSS_2_SELECTED_EVENT_NAME = "BOSS_2_SELECTED";

	// Token: 0x040000B6 RID: 182
	private const string BOSS_3_SELECTED_EVENT_NAME = "BOSS_3_SELECTED";

	// Token: 0x040000B7 RID: 183
	private const string BOSS_4_SELECTED_EVENT_NAME = "BOSS_4_SELECTED";

	// Token: 0x040000B8 RID: 184
	private const string BOSS_5_SELECTED_EVENT_NAME = "BOSS_5_SELECTED";

	// Token: 0x040000B9 RID: 185
	private Widget m_adventureBookPageContentsWidget;

	// Token: 0x040000BA RID: 186
	private Widget.EventListenerDelegate m_pageEventListener;

	// Token: 0x040000BB RID: 187
	private AdventureBookPageDisplay.FlipToChapterCallback m_flipToChapterCallback;

	// Token: 0x040000BC RID: 188
	private PageData m_pageData;

	// Token: 0x040000BD RID: 189
	private AdventureBookPageDataModel m_pageDataModel;

	// Token: 0x040000BE RID: 190
	private bool m_allInitialTransitionsComplete;

	// Token: 0x040000BF RID: 191
	private AdventureBookPageDisplay.EnableInteractionCallback m_enableInteractionCallback;

	// Token: 0x040000C0 RID: 192
	private Map<string, Clickable> m_chapterButtonClickablesNameMap = new Map<string, Clickable>();

	// Token: 0x040000C1 RID: 193
	private Queue<string> m_chapterNewlyUnlockedMapSequenceQueue = new Queue<string>();

	// Token: 0x040000C2 RID: 194
	private bool m_isInUnlockedSequence;

	// Token: 0x040000C3 RID: 195
	private bool m_readyToPlayAdventureNewlyCompletedVO;

	// Token: 0x040000C4 RID: 196
	private bool m_adventureNewlyCompletedSequenceFinished;

	// Token: 0x040000C5 RID: 197
	private string m_currentUnlockButtonName;

	// Token: 0x040000C6 RID: 198
	private List<AdventureChapterDataModel> m_sortedChapterDataModels = new List<AdventureChapterDataModel>();

	// Token: 0x040000C7 RID: 199
	private bool m_needToShowRewardChestAnim;

	// Token: 0x040000C8 RID: 200
	private bool m_rewardChestReadyToShowPopup;

	// Token: 0x040000C9 RID: 201
	private bool m_needToShowChapterCompletionAnim;

	// Token: 0x040000CA RID: 202
	private bool m_chapterCompletionAnimFinished;

	// Token: 0x040000CB RID: 203
	private bool m_needToShowMissionCompleteAnim;

	// Token: 0x040000CC RID: 204
	private bool m_missionCompleteAnimFinished;

	// Token: 0x040000CD RID: 205
	private bool m_needToShowMissionUnlockAnim;

	// Token: 0x040000CE RID: 206
	private bool m_missionUnlockAnimFinished;

	// Token: 0x040000CF RID: 207
	private bool m_chapterNewlyPurchasedAnimFinished;

	// Token: 0x040000D1 RID: 209
	private static AssetReference m_chooseStoreWidgetPrefab = new AssetReference("AdventureStorymodeChooseStore.prefab:22dcec0cce5b1ec4ba4ea2e5048934fb");

	// Token: 0x040000D2 RID: 210
	private Widget m_storeChooseWidget;

	// Token: 0x040000D3 RID: 211
	private UIBButton m_storeChooseBackButton;

	// Token: 0x040000D4 RID: 212
	private UIBPopup m_storeChoosePopup;

	// Token: 0x02001274 RID: 4724
	// (Invoke) Token: 0x0600D430 RID: 54320
	public delegate void PageReadyCallback();

	// Token: 0x02001275 RID: 4725
	// (Invoke) Token: 0x0600D434 RID: 54324
	public delegate void FlipToChapterCallback(int chapterNumber);

	// Token: 0x02001276 RID: 4726
	// (Invoke) Token: 0x0600D438 RID: 54328
	public delegate void PageClickedCallback();

	// Token: 0x02001277 RID: 4727
	// (Invoke) Token: 0x0600D43C RID: 54332
	public delegate void EnableInteractionCallback(bool enable);

	// Token: 0x02001278 RID: 4728
	private class ChapterButtonData
	{
		// Token: 0x0400A38D RID: 41869
		public string ButtonName;

		// Token: 0x0400A38E RID: 41870
		public ChapterPageData ChapterData;
	}
}
