using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public class AdventureBookPageDisplay : BookPageDisplay
{
	public delegate void PageReadyCallback();

	public delegate void FlipToChapterCallback(int chapterNumber);

	public delegate void PageClickedCallback();

	public delegate void EnableInteractionCallback(bool enable);

	private class ChapterButtonData
	{
		public string ButtonName;

		public ChapterPageData ChapterData;
	}

	public AsyncReference m_AdventureBookPageContentsReference;

	public float m_popupEffectFadeTime = 0.25f;

	private const string CHAPTER_UNLOCK_ANIMATION_COMPLETE = "CODE_UNLOCKED_ANIMATION_COMPLETE";

	private const string CHAPTER_BUTTON_POPUP_DISMISS_EVENT_NAME = "CODE_HIDE_AND_DISMISS";

	private const string BOOK_MAP_EVENT_NAME = "ShowBookMapPage";

	private const string BOOK_CARD_BACK_EVENT_NAME = "ShowCardBackPage";

	private const string BOOK_CHAPTER_EVENT_NAME = "ShowChapterPage";

	private const string REWARD_CHEST_OPEN_ANIMATION_EVENT_NAME = "OPEN_CHAPTER_CHEST_REWARD";

	private const string REWARD_CHEST_READY_TO_SHOW_POPUP_EVENT_NAME = "READY_TO_SHOW_POPUP";

	private const string CHAPTER_NEWLY_COMPLETED_ANIMATION_EVENT_NAME = "CHAPTER_NEWLY_COMPLETED";

	private const string CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED_EVENT_NAME = "CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED";

	private const string ADVENTURE_NEWLY_COMPLETED_SEQUENCE_EVENT_NAME = "AdventureNewlyCompletedSequence";

	private const string PLAY_ADVENTURE_NEWLY_COMPLETED_VO_EVENT_NAME = "PlayAdventureNewlyCompletedVO";

	private const string ADVENTURE_NEWLY_COMPLETED_SEQUENCE_FINISHED_EVENT_NAME = "AdventureNewlyCompletedSequenceFinished";

	private const string MISSION_NEWLY_COMPLETED_ANIM_EVENT_NAME = "MISSION_NEWLY_COMPLETED";

	private const string MISSION_NEWLY_COMPLETED_ANIM_FINISHED_EVENT_NAME = "MISSION_NEWLY_COMPLETED_ANIM_FINISHED";

	private const string MISSION_NEWLY_UNLOCKED_ANIM_EVENT_NAME = "MISSION_NEWLY_UNLOCKED";

	private const string MISSION_NEWLY_UNLOCKED_ANIM_FINISHED_EVENT_NAME = "MISSION_NEWLY_UNLOCKED_ANIM_FINISHED";

	private const string CHAPTER_NEWLY_PURCHASED_SEQUENCE_EVENT_NAME = "PLAY_CHAPTER_NEWLY_PURCHASED_ANIM";

	private const string CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED_EVENT_NAME = "CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED";

	private const string PURCHASE_INDIVIDUAL_WING_EVENT_NAME = "chapter_selected";

	private const string PURCHASE_BOOK_EVENT_NAME = "book_selected";

	private const string CHAPTER_UNLOCK_BUTTON_CLICKED_EVENT_NAME = "CHAPTER_UNLOCK_BUTTON_CLICKED";

	private const string BOSS_1_SELECTED_EVENT_NAME = "BOSS_1_SELECTED";

	private const string BOSS_2_SELECTED_EVENT_NAME = "BOSS_2_SELECTED";

	private const string BOSS_3_SELECTED_EVENT_NAME = "BOSS_3_SELECTED";

	private const string BOSS_4_SELECTED_EVENT_NAME = "BOSS_4_SELECTED";

	private const string BOSS_5_SELECTED_EVENT_NAME = "BOSS_5_SELECTED";

	private Widget m_adventureBookPageContentsWidget;

	private Widget.EventListenerDelegate m_pageEventListener;

	private FlipToChapterCallback m_flipToChapterCallback;

	private PageData m_pageData;

	private AdventureBookPageDataModel m_pageDataModel;

	private bool m_allInitialTransitionsComplete;

	private EnableInteractionCallback m_enableInteractionCallback;

	private Map<string, Clickable> m_chapterButtonClickablesNameMap = new Map<string, Clickable>();

	private Queue<string> m_chapterNewlyUnlockedMapSequenceQueue = new Queue<string>();

	private bool m_isInUnlockedSequence;

	private bool m_readyToPlayAdventureNewlyCompletedVO;

	private bool m_adventureNewlyCompletedSequenceFinished;

	private string m_currentUnlockButtonName;

	private List<AdventureChapterDataModel> m_sortedChapterDataModels = new List<AdventureChapterDataModel>();

	private bool m_needToShowRewardChestAnim;

	private bool m_rewardChestReadyToShowPopup;

	private bool m_needToShowChapterCompletionAnim;

	private bool m_chapterCompletionAnimFinished;

	private bool m_needToShowMissionCompleteAnim;

	private bool m_missionCompleteAnimFinished;

	private bool m_needToShowMissionUnlockAnim;

	private bool m_missionUnlockAnimFinished;

	private bool m_chapterNewlyPurchasedAnimFinished;

	private static AssetReference m_chooseStoreWidgetPrefab = new AssetReference("AdventureStorymodeChooseStore.prefab:22dcec0cce5b1ec4ba4ea2e5048934fb");

	private Widget m_storeChooseWidget;

	private UIBButton m_storeChooseBackButton;

	private UIBPopup m_storeChoosePopup;

	public static bool NeedToShowAdventureSectionCompletionSequence { get; private set; }

	private void Start()
	{
		m_AdventureBookPageContentsReference.RegisterReadyListener<Widget>(AdventureBookPageContentsIsReady);
		AdventureProgressMgr.Get().RegisterProgressUpdatedListener(UpdateMapOnWingProgressUpdated);
		AdventureConfig.Get().AddAdventureMissionSetListener(OnMissionSet);
	}

	private void OnDestroy()
	{
		AdventureProgressMgr.Get()?.RemoveProgressUpdatedListener(UpdateMapOnWingProgressUpdated);
		AdventureConfig.Get().RemoveAdventureMissionSetListener(OnMissionSet);
	}

	private void Update()
	{
		CheckForInputForCheats();
	}

	public override bool IsLoaded()
	{
		if (m_basePageRenderer == null)
		{
			Log.Adventures.Print("Currently waiting on m_basePageRenderer to get set before IsLoaded() becomes true.");
			return false;
		}
		return true;
	}

	public void SetUpPage(PageData pageData, PageReadyCallback callback)
	{
		StartCoroutine(SetUpPageWhenReady(pageData, callback));
	}

	public void SetPageEventListener(Widget.EventListenerDelegate listener)
	{
		m_pageEventListener = listener;
	}

	public void SetFlipToChapterCallback(FlipToChapterCallback callback)
	{
		m_flipToChapterCallback = callback;
	}

	public void SetEnableInteractionCallback(EnableInteractionCallback callback)
	{
		m_enableInteractionCallback = callback;
	}

	public AdventureBookPageDataModel GetAdventurePageDataModel()
	{
		return m_pageDataModel;
	}

	public void AllInitialTransitionsComplete()
	{
		m_allInitialTransitionsComplete = true;
	}

	public override void Show()
	{
		base.Show();
		ScenarioDbId mission = ScenarioDbId.INVALID;
		if (m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = m_pageData as ChapterPageData;
			if (chapterPageData == null)
			{
				Debug.LogErrorFormat("Showing a Book Chapter, but it has no data associated with it!");
			}
			else if (chapterPageData.ScenarioRecords.Count == 0)
			{
				Debug.LogErrorFormat("Showing Book Chapter {0}, but it has no ScenarioIds associated with it!", chapterPageData.WingRecord.Name);
			}
			else if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()))
			{
				mission = (ScenarioDbId)chapterPageData.ScenarioRecords[0].ID;
			}
			else
			{
				ScenarioDbId mission2 = AdventureConfig.Get().GetMission();
				if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && mission2 != 0 && !AdventureProgressMgr.Get().HasDefeatedScenario((int)mission2))
				{
					mission = mission2;
				}
			}
		}
		AdventureConfig.Get().SetMission(mission);
		StartCoroutine(ShowPageUpdateVisualsWhenReady());
	}

	public bool DoesBundleApplyToPage(Network.Bundle bundle)
	{
		if (m_pageData == null)
		{
			Debug.LogError("DoesBundleApplyToPage: No pageData defined for page!");
			return false;
		}
		if (m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = m_pageData as ChapterPageData;
			if (chapterPageData != null)
			{
				return AdventureUtils.DoesBundleIncludeWing(bundle, chapterPageData.WingRecord.ID);
			}
		}
		return AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, m_pageData.Adventure);
	}

	private IEnumerator SetUpPageWhenReady(PageData pageData, PageReadyCallback callback)
	{
		m_pageData = pageData;
		m_needToShowRewardChestAnim = false;
		m_rewardChestReadyToShowPopup = false;
		m_needToShowChapterCompletionAnim = false;
		m_chapterCompletionAnimFinished = false;
		m_readyToPlayAdventureNewlyCompletedVO = false;
		m_adventureNewlyCompletedSequenceFinished = false;
		m_needToShowMissionCompleteAnim = false;
		m_missionCompleteAnimFinished = false;
		m_needToShowMissionUnlockAnim = false;
		m_missionUnlockAnimFinished = false;
		while (m_adventureBookPageContentsWidget == null)
		{
			yield return null;
		}
		SetupPageDataModels(pageData);
		string eventName = "ShowBookMapPage";
		if (pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			eventName = "ShowChapterPage";
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			eventName = "ShowCardBackPage";
		}
		if (m_adventureBookPageContentsWidget.TriggerEvent(eventName))
		{
			while (m_adventureBookPageContentsWidget.IsChangingStates)
			{
				yield return null;
			}
		}
		callback?.Invoke();
	}

	private static AdventureBookPageMoralAlignment ConvertBookSectionToMoralAlignment(int section)
	{
		return (AdventureBookPageMoralAlignment)section;
	}

	private void SetupPageDataModels(PageData pageData)
	{
		m_adventureBookPageContentsWidget.GetDataModel(2, out var model);
		m_pageDataModel = model as AdventureBookPageDataModel;
		if (m_pageDataModel == null)
		{
			m_pageDataModel = new AdventureBookPageDataModel();
			m_pageDataModel.ChapterData = new AdventureChapterDataModel();
			m_adventureBookPageContentsWidget.BindDataModel(m_pageDataModel);
		}
		else
		{
			m_pageDataModel.ChapterData = new AdventureChapterDataModel();
		}
		m_pageDataModel.PageType = pageData.PageType;
		m_pageDataModel.MoralAlignment = ConvertBookSectionToMoralAlignment(pageData.BookSection);
		m_pageDataModel.ChapterData.TimeLocked = false;
		m_pageDataModel.ChapterData.FirstHeroBundledWithChapter = 0;
		m_pageDataModel.ChapterData.SecondHeroBundledWithChapter = 0;
		m_pageDataModel.ChapterData.CompletionRewardType = Reward.Type.NONE;
		m_pageDataModel.AllChaptersData.Clear();
		if (pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			ChapterPageData chapterPageData = pageData as ChapterPageData;
			if (chapterPageData == null)
			{
				Debug.LogError("SetupDataModelsAndRefresh(): PageData is not a valid ChapterPageData! Cannot cast properly.");
				return;
			}
			UpdateChapterDataModelWithChapterData(m_pageDataModel.ChapterData, chapterPageData);
			m_needToShowRewardChestAnim = m_pageDataModel.ChapterData.CompletionRewardsNewlyEarned;
			m_needToShowChapterCompletionAnim = m_pageDataModel.ChapterData.NewlyCompleted;
			if (m_needToShowChapterCompletionAnim && AdventureProgressMgr.Get().IsAdventureModeAndSectionComplete((AdventureDbId)chapterPageData.WingRecord.AdventureId, chapterPageData.AdventureMode, chapterPageData.BookSection))
			{
				Log.Adventures.Print("You've completed your final Chapter! Setting up Adventure Complete sequence.");
				NeedToShowAdventureSectionCompletionSequence = true;
			}
			if (m_pageDataModel.ChapterData.NewlyUnlocked)
			{
				Log.Adventures.Print("Chapter {0} is newly unlocked!", m_pageDataModel.ChapterData.ChapterNumber);
			}
			if (m_pageDataModel.ChapterData.NewlyCompleted)
			{
				Log.Adventures.Print("Chapter {0} is newly completed!", m_pageDataModel.ChapterData.ChapterNumber);
			}
			if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(pageData.AdventureMode))
			{
				return;
			}
			foreach (AdventureMissionDataModel mission in m_pageDataModel.ChapterData.Missions)
			{
				bool flag = mission.Rewards != null && mission.Rewards.Items != null && mission.Rewards.Items.Count > 0;
				if (mission.NewlyCompleted)
				{
					m_needToShowMissionCompleteAnim = true;
					if (flag)
					{
						m_needToShowRewardChestAnim = true;
					}
				}
				if (mission.NewlyUnlocked)
				{
					m_needToShowMissionUnlockAnim = true;
				}
			}
		}
		else if (pageData.PageType == AdventureBookPageType.MAP)
		{
			MapPageData mapPageData = pageData as MapPageData;
			if (mapPageData == null)
			{
				Debug.LogError("SetupDataModelsAndRefresh(): PageData is not a valid MapPageData! Cannot cast properly.");
				return;
			}
			m_pageDataModel.NumChaptersCompletedText = mapPageData.NumChaptersCompletedText;
			while (m_sortedChapterDataModels.Count < mapPageData.ChapterData.Values.Count)
			{
				m_sortedChapterDataModels.Add(new AdventureChapterDataModel());
			}
			int[] array = new int[mapPageData.NumSectionsInBook];
			int[] array2 = new int[mapPageData.NumSectionsInBook];
			int num = 0;
			foreach (ChapterPageData value in mapPageData.ChapterData.Values)
			{
				UpdateChapterDataModelWithChapterData(m_sortedChapterDataModels[num], value);
				if (value.BookSection < 0 || value.BookSection >= mapPageData.NumSectionsInBook)
				{
					Debug.LogErrorFormat("AdventureBookPageDisplay.SetupDataModelsAndRefresh() - chapterData.BookSection {0} is not within the bounds of the number of sections {1}", value.BookSection, mapPageData.NumSectionsInBook);
				}
				else
				{
					array2[value.BookSection]++;
					if (m_sortedChapterDataModels[num].PlayerOwnsChapter)
					{
						array[value.BookSection]++;
					}
				}
				num++;
			}
			m_sortedChapterDataModels.Sort((AdventureChapterDataModel a, AdventureChapterDataModel b) => a.ChapterNumber - b.ChapterNumber);
			m_pageDataModel.AllChaptersData.AddRange(m_sortedChapterDataModels);
			while (m_pageDataModel.NumChaptersOwnedText.Count < array.Length)
			{
				m_pageDataModel.NumChaptersOwnedText.Add("");
			}
			for (num = 0; num < array.Length; num++)
			{
				if (array[num] < array2[num])
				{
					m_pageDataModel.NumChaptersOwnedText[num] = GameStrings.Format("GLUE_ADVENTURE_NUM_CHAPTERS_OWNED", array[num]);
				}
				else
				{
					m_pageDataModel.NumChaptersOwnedText[num] = "";
				}
			}
			UpdateMapButtonData();
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			UpdateRewardPageData(pageData);
		}
	}

	private void OnChapterClickableRelease(UIEvent e)
	{
		ChapterButtonData chapterButtonData = e.GetElement().GetData() as ChapterButtonData;
		if (chapterButtonData == null)
		{
			Log.Adventures.PrintError("Chapter Button pressed, but the button has no data!");
			return;
		}
		Log.Adventures.Print("Released {0}!", chapterButtonData.ButtonName);
		if (m_flipToChapterCallback != null)
		{
			m_flipToChapterCallback(chapterButtonData.ChapterData.ChapterNumber);
		}
	}

	public void HideAndSuppressChapterUnlockSequence()
	{
		if (!m_isInUnlockedSequence)
		{
			return;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(m_popupEffectFadeTime);
		if (string.IsNullOrEmpty(m_currentUnlockButtonName) || !m_chapterButtonClickablesNameMap.ContainsKey(m_currentUnlockButtonName))
		{
			return;
		}
		Clickable clickable = m_chapterButtonClickablesNameMap[m_currentUnlockButtonName];
		if (clickable == null)
		{
			Log.Adventures.PrintError("Chapter Button {0} is missing!", m_currentUnlockButtonName);
			return;
		}
		VisualController component = clickable.GetComponent<VisualController>();
		if (component == null)
		{
			Error.AddDevWarning("Missing Visual Controller", "{0} does not have a visual controller!", m_currentUnlockButtonName);
			return;
		}
		clickable.GetDataModel(3, out var dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = false;
		}
		component.Owner.TriggerEvent("CODE_HIDE_AND_DISMISS");
		m_currentUnlockButtonName = null;
	}

	private void OnChapterUnlockButtonClicked()
	{
		ChapterPageData chapterPageData = m_pageData as ChapterPageData;
		if (chapterPageData == null)
		{
			return;
		}
		if (AdventureProgressMgr.Get().OwnsWing(chapterPageData.WingRecord.ID) && chapterPageData.WingRecord.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			AdventureUtils.DisplayFirstChapterFreePopup(chapterPageData);
		}
		else if (!AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			bool flag = AdventureConfig.Get().GetSelectedAdventure() != AdventureDbId.DALARAN && AdventureConfig.Get().GetSelectedAdventure() != AdventureDbId.ULDUM;
			if (m_pageDataModel.ChapterData.AvailableForPurchase && chapterPageData.WingRecord.PmtProductIdForThisAndRestOfAdventure == 0)
			{
				StartSingleWingPurchaseTransaction(m_pageData, m_pageDataModel);
			}
			else if (m_pageDataModel.ChapterData.AvailableForPurchase || flag)
			{
				SetupAdventurePurchaseChoiceDialog(chapterPageData);
			}
			else
			{
				StartFullBookPurchaseTransaction(m_pageData, m_pageDataModel);
			}
		}
	}

	private void OnBossSelected(int bossOffset)
	{
		if (m_adventureBookPageContentsWidget == null)
		{
			Debug.LogError("AdventureBookPageDisplay: OnBossSelected() called when m_adventureBookPageContentsWidget is null!");
			return;
		}
		m_adventureBookPageContentsWidget.GetDataModel(2, out var model);
		AdventureBookPageDataModel adventureBookPageDataModel = model as AdventureBookPageDataModel;
		if (adventureBookPageDataModel == null)
		{
			Error.AddDevWarning("UI Error", "No AdventureBookPageDataModel bound to the AdventureBookPageContents widget when the boss was selected!");
			return;
		}
		if (adventureBookPageDataModel.ChapterData == null)
		{
			Error.AddDevWarning("UI Error", "AdventureBookPageDataModel's ChapterData is null when the boss was selected!");
			return;
		}
		if (adventureBookPageDataModel.ChapterData.Missions.Count <= bossOffset)
		{
			Error.AddDevWarning("UI Error", "Selected boss index {0} but there are only {1} missions defined for Chapter {2}!", bossOffset, adventureBookPageDataModel.ChapterData.Missions.Count, adventureBookPageDataModel.ChapterData.Name);
			return;
		}
		AdventureMissionDataModel adventureMissionDataModel = adventureBookPageDataModel.ChapterData.Missions[bossOffset];
		if (adventureMissionDataModel == null)
		{
			Error.AddDevWarning("UI Error", "AdventureMissionDataModel at index {0} for Chapter {1} is not valid!", bossOffset, adventureBookPageDataModel.ChapterData.Name);
		}
		else
		{
			AdventureConfig.Get().SetMission(adventureMissionDataModel.ScenarioId);
		}
	}

	private void OnMissionSet(ScenarioDbId mission, bool showDetails)
	{
		m_adventureBookPageContentsWidget.GetDataModel(2, out var model);
		AdventureBookPageDataModel adventureBookPageDataModel = model as AdventureBookPageDataModel;
		if (adventureBookPageDataModel == null || adventureBookPageDataModel.ChapterData == null)
		{
			return;
		}
		foreach (AdventureMissionDataModel mission2 in adventureBookPageDataModel.ChapterData.Missions)
		{
			mission2.Selected = mission2.ScenarioId == mission;
		}
	}

	private void OnChapterUnlockAnimationComplete(string eventName)
	{
		if (eventName != "CODE_UNLOCKED_ANIMATION_COMPLETE")
		{
			return;
		}
		m_isInUnlockedSequence = false;
		if (string.IsNullOrEmpty(m_currentUnlockButtonName))
		{
			Log.Adventures.PrintWarning("AdventureBookPageDisplay.OnChapterUnlockAnimationComplete: Current unlock button was not set, if this was manually activated outside the normal flow then this can be ignored.");
			return;
		}
		Clickable value = null;
		if (!m_chapterButtonClickablesNameMap.TryGetValue(m_currentUnlockButtonName, out value))
		{
			Log.Adventures.PrintError("AdventureBookPageDisplay.OnChapterUnlockAnimationComplete: Could not find current unlock button {0}.", m_currentUnlockButtonName);
			return;
		}
		ChapterButtonData chapterButtonData = value.GetData() as ChapterButtonData;
		if (chapterButtonData != null)
		{
			AdventureConfig.Get().SetHasSeenUnlockedChapterPage((WingDbId)chapterButtonData.ChapterData.WingRecord.ID, hasSeen: false);
			AdventureConfig.AckCurrentWingProgress(chapterButtonData.ChapterData.WingRecord.ID);
			Log.Adventures.Print("Pressed {0} {1}!", chapterButtonData.ButtonName, m_currentUnlockButtonName);
		}
		value.GetDataModel(3, out var dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = false;
			adventureChapterDataModel.NewlyUnlocked = false;
			adventureChapterDataModel.ShowNewlyUnlockedHighlight = true;
		}
		m_currentUnlockButtonName = null;
		ShowChapterNewlyUnlockedMapSequenceIfNecessary();
		EnableInteraction(enable: true);
	}

	private void SetupAdventurePurchaseChoiceDialog(ChapterPageData pageData)
	{
		if (m_storeChooseWidget == null)
		{
			m_storeChooseWidget = WidgetInstance.Create(m_chooseStoreWidgetPrefab);
			m_storeChooseWidget.transform.parent = base.transform;
		}
		m_storeChooseWidget.RegisterReadyListener(delegate
		{
			m_storeChooseWidget.BindDataModel(m_pageDataModel);
			FullScreenFXMgr.Get().StartStandardBlurVignette(m_popupEffectFadeTime);
			if (m_storeChooseBackButton == null)
			{
				m_storeChooseBackButton = m_storeChooseWidget.GetComponentInChildren<UIBButton>();
			}
			if (m_storeChoosePopup == null)
			{
				m_storeChoosePopup = m_storeChooseWidget.GetComponentInChildren<UIBPopup>();
			}
			m_storeChoosePopup.Show(useOverlayUI: false);
			Navigation.Push(HideStoreChoosePopup);
			m_storeChooseBackButton.AddEventListener(UIEventType.RELEASE, delegate
			{
				Navigation.GoBack();
			});
			m_storeChooseWidget.RegisterEventListener(OnBookStoreChosenEvent);
		});
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod method)
	{
		if (DoesBundleApplyToPage(bundle) && m_storeChoosePopup != null && m_storeChoosePopup.IsShown())
		{
			Navigation.GoBack();
		}
	}

	private bool HideStoreChoosePopup()
	{
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		if (m_storeChoosePopup == null)
		{
			return false;
		}
		FullScreenFXMgr.Get().EndStandardBlurVignette(m_popupEffectFadeTime);
		m_storeChoosePopup.Hide();
		return true;
	}

	private static void StartFullBookPurchaseTransaction(PageData pageData, AdventureBookPageDataModel pageDataModel)
	{
		WingDbfRecord wingDbfRecord = (pageData as ChapterPageData)?.WingRecord;
		if (wingDbfRecord == null)
		{
			Debug.LogError("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: could not get the wing record from page data when trying to purchase the entire adventure book.");
			return;
		}
		WingDbfRecord firstUnownedAdventureWing = AdventureProgressMgr.Get().GetFirstUnownedAdventureWing((AdventureDbId)wingDbfRecord.AdventureId);
		if (firstUnownedAdventureWing == null)
		{
			Debug.LogError("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: could not find a first unowned wing - something went wrong!");
		}
		else if (!AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(firstUnownedAdventureWing))
		{
			Debug.LogErrorFormat("AdventureBookPageDisplay.StartFullBookPurchaseTransaction: You do not own wing {0}, you cannot purchase the entire adventure book starting at wing {1}!", wingDbfRecord.OwnershipPrereqWingId, firstUnownedAdventureWing.ID);
		}
		else
		{
			StoreManager.Get().StartAdventureTransaction(ProductType.PRODUCT_TYPE_WING, wingDbfRecord.ID, null, null, ShopType.ADVENTURE_STORE_FULL_PURCHASE_WIDGET, 0, useOverlayUI: false, pageDataModel, firstUnownedAdventureWing.PmtProductIdForThisAndRestOfAdventure);
		}
	}

	private static void StartSingleWingPurchaseTransaction(PageData pageData, AdventureBookPageDataModel pageDataModel)
	{
		WingDbfRecord wingDbfRecord = (pageData as ChapterPageData)?.WingRecord;
		if (wingDbfRecord == null)
		{
			Debug.LogError("AdventureBookPageDisplay.OnBookStoreChosenEvent: could not get the wing record from page data when trying to purchase a specific wing.");
		}
		else if (!AdventureProgressMgr.Get().OwnershipPrereqWingIsOwned(wingDbfRecord))
		{
			Debug.LogErrorFormat("AdventureBookPageDisplay.OnBookStoreChosenEvent: You do not own wing {0}, you cannot purchase the wing on this page!", wingDbfRecord.OwnershipPrereqWingId);
		}
		else
		{
			StoreManager.Get().StartAdventureTransaction(ProductType.PRODUCT_TYPE_WING, wingDbfRecord.ID, null, null, ShopType.ADVENTURE_STORE_WING_PURCHASE_WIDGET, 0, useOverlayUI: false, pageDataModel, wingDbfRecord.PmtProductIdForSingleWingPurchase);
		}
	}

	private void OnBookStoreChosenEvent(string eventName)
	{
		if (eventName == "book_selected")
		{
			if (m_storeChoosePopup != null && m_storeChoosePopup.IsShown())
			{
				Navigation.GoBack();
			}
			StartFullBookPurchaseTransaction(m_pageData, m_pageDataModel);
		}
		else if (eventName == "chapter_selected")
		{
			if (m_storeChoosePopup != null && m_storeChoosePopup.IsShown())
			{
				Navigation.GoBack();
			}
			StartSingleWingPurchaseTransaction(m_pageData, m_pageDataModel);
		}
	}

	private void AdventureBookPageContentsIsReady(Widget bookPageContents)
	{
		m_adventureBookPageContentsWidget = bookPageContents;
		if (bookPageContents == null)
		{
			Error.AddDevWarning("Error", "Error: Adventure Book Page Contents Reference not hooked up to a Widget!");
		}
		else
		{
			StartCoroutine(SetUpBookPageReferencesWhenResolved(bookPageContents));
		}
	}

	private IEnumerator SetUpBookPageReferencesWhenResolved(Widget bookPageContents)
	{
		while (bookPageContents.IsChangingStates)
		{
			yield return null;
		}
		bookPageContents.RegisterEventListener(BookPageContentsEventListener);
		AdventureBookPageDisplayRefContainer componentInChildren = bookPageContents.gameObject.GetComponentInChildren<AdventureBookPageDisplayRefContainer>();
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "There is no AdventureBookPageDisplayRefContainer component on your AdventureBookPageContents Widget! This is necessary to initialize things like the Map Page.");
			yield break;
		}
		componentInChildren.m_AdventureBookMapReference.RegisterReadyListener<Widget>(AdventureBookMapIsReady);
		componentInChildren.m_BasePageRendererReference.RegisterReadyListener<MeshRenderer>(BasePageRendererIsReady);
	}

	private void BasePageRendererIsReady(MeshRenderer basePageRenderer)
	{
		m_basePageRenderer = basePageRenderer;
	}

	private void AdventureBookMapIsReady(Widget widget)
	{
		if (widget == null || !widget.IsReady)
		{
			Log.Adventures.PrintError("AdventureBookMap should be ready, but it's not!  Something terrible is happening!");
			return;
		}
		widget.RegisterEventListener(OnChapterUnlockAnimationComplete);
		StartCoroutine(InitializeMapButtonsWhenResolved(widget));
	}

	public IEnumerator InitializeMapButtonsWhenResolved(Widget bookMapWidget)
	{
		while (bookMapWidget.IsChangingStates)
		{
			yield return null;
		}
		MapPageData mapData = m_pageData as MapPageData;
		if (mapData == null)
		{
			Log.Adventures.PrintError("SetUpPageWhenReady(): m_pageData is not a valid MapPageData! Cannot cast properly.");
			yield break;
		}
		ListOfChapterButtons componentInChildren = bookMapWidget.gameObject.GetComponentInChildren<ListOfChapterButtons>();
		if (componentInChildren == null)
		{
			yield break;
		}
		List<AsyncReference> chapterButtonClickableReferences = componentInChildren.m_ChapterButtonClickableReferences;
		if (chapterButtonClickableReferences.Count != mapData.ChapterData.Count)
		{
			Error.AddDevWarning("Missing Adventure Buttons", "Error: there are not the same number of Chapter Buttons ({0}) as there are Chapters ({1}) defined for this Adventure!", chapterButtonClickableReferences.Count, mapData.ChapterData.Count);
		}
		m_chapterButtonClickablesNameMap.Clear();
		int i;
		for (i = 0; i < chapterButtonClickableReferences.Count; i++)
		{
			chapterButtonClickableReferences[i].RegisterReadyListener(delegate(Clickable chapterButton)
			{
				int num = i + 1;
				if (chapterButton == null)
				{
					Debug.LogErrorFormat("The reference to a ChapterButton at index {0} in the ListOfChapterButtons component is not a valid Clickable!", num);
				}
				else
				{
					mapData.ChapterData.TryGetValue(num, out var value);
					if (value == null)
					{
						Log.Adventures.PrintError("No ChapterData in the MapPageData for Chapter {0}!", num);
					}
					else
					{
						string text = chapterButton.gameObject.name + num;
						ChapterButtonData data = new ChapterButtonData
						{
							ChapterData = value,
							ButtonName = text
						};
						m_chapterButtonClickablesNameMap.Add(text, chapterButton);
						chapterButton.SetData(data);
						chapterButton.AddEventListener(UIEventType.RELEASE, OnChapterClickableRelease);
					}
				}
			});
		}
		UpdateMapButtonData();
	}

	private void UpdateMapOnWingProgressUpdated(bool isStartupAction, AdventureMission.WingProgress oldProgress, AdventureMission.WingProgress newProgress, object userData)
	{
		UpdateMapButtonData(forceUpdate: true);
	}

	private void UpdateMapButtonData(bool forceUpdate = false)
	{
		if (m_sortedChapterDataModels == null)
		{
			Debug.LogError("AdventureBookPageDisplay.UpdateMapButtonData() - m_sortedChapterDataModels is null!");
			return;
		}
		m_chapterNewlyUnlockedMapSequenceQueue.Clear();
		foreach (Clickable value in m_chapterButtonClickablesNameMap.Values)
		{
			ChapterButtonData chapterButtonData = value.GetData() as ChapterButtonData;
			if (chapterButtonData == null)
			{
				Log.Adventures.PrintError("Data on Chapter Button is not valid ChapterButtonData!");
				continue;
			}
			AdventureChapterDataModel adventureChapterDataModel = m_sortedChapterDataModels.Find((AdventureChapterDataModel x) => x.ChapterNumber == chapterButtonData.ChapterData.ChapterNumber);
			if (adventureChapterDataModel == null)
			{
				Debug.LogErrorFormat("AdventureBookPageDisplay.UpdateMapButtonData() - No ChapterDataModel for Chapter {0} found in m_sortedChapterDataModels!", chapterButtonData.ChapterData.ChapterNumber);
				continue;
			}
			if (forceUpdate)
			{
				UpdateChapterDataModelWithChapterData(adventureChapterDataModel, chapterButtonData.ChapterData);
			}
			value.BindDataModel(adventureChapterDataModel);
			if (adventureChapterDataModel.NewlyUnlocked)
			{
				Log.Adventures.Print("Chapter {0} is newly unlocked!", adventureChapterDataModel.ChapterNumber);
				m_chapterNewlyUnlockedMapSequenceQueue.Enqueue(chapterButtonData.ButtonName);
			}
			if (adventureChapterDataModel.NewlyCompleted)
			{
				Log.Adventures.Print("Chapter {0} is newly completed!", adventureChapterDataModel.ChapterNumber);
			}
		}
	}

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
		chapterDataModel.AvailableForPurchase = !chapterDataModel.PlayerOwnsChapter && chapterDataModel.IsPreviousChapterOwned && !GameUtils.IsModeHeroic(chapterData.AdventureMode);
		chapterDataModel.FinalPurchasableChapter = wingRecord.PmtProductIdForThisAndRestOfAdventure == 0 && wingRecord.PmtProductIdForSingleWingPurchase != 0;
		AdventureProgressMgr.Get().GetWingAck(wingRecord.ID, out var ack);
		chapterDataModel.NewlyUnlocked = chapterDataModel.ChapterState == AdventureChapterState.UNLOCKED && ack == 0;
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
				Log.Adventures.Print("{0} Guest Heroes defined for Wing {0}, but we only have room in the data model for 2!", guestHeroesForWing.Count, wingRecord.ID);
			}
		}
		chapterDataModel.DisplayRaidBossHealth = wingRecord.DisplayRaidBossHealth;
		chapterDataModel.RaidBossHealthAmount = 0;
		if (chapterDataModel.DisplayRaidBossHealth)
		{
			string text = GameUtils.TranslateDbIdToCardId(wingRecord.RaidBossCardId);
			if (text == null || wingRecord.RaidBossCardId == 0)
			{
				Log.Adventures.PrintWarning("AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData() - No cardId for raid boss dbId {0}!", wingRecord.RaidBossCardId);
			}
			else
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
				if (entityDef == null)
				{
					Log.Adventures.PrintWarning("AdventureBookPageDisplay.UpdateChapterDataModelWithChapterData() - No EntityDef for raid boss card ID {0}!", text);
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
				GameSaveDataManager.Get().GetSubkeyValue(gameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_FINAL_BOSS_HEALTH, out List<long> values);
				int sortedWingUnlockIndex = GameUtils.GetSortedWingUnlockIndex(wingRecord);
				if (values != null && values.Count > sortedWingUnlockIndex)
				{
					chapterDataModel.RaidBossHealthAmount = Mathf.Clamp((int)values[sortedWingUnlockIndex], 0, chapterDataModel.RaidBossStartingHealthAmount);
				}
			}
		}
		chapterDataModel.CompletionRewards = new RewardListDataModel();
		HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming> { Assets.Achieve.RewardTiming.ADVENTURE_CHEST };
		List<RewardData> list = new List<RewardData>();
		List<Achievement> achievesForAdventureWing = AchieveManager.Get().GetAchievesForAdventureWing(wingRecord.ID);
		foreach (Achievement item in achievesForAdventureWing)
		{
			if (item.Scenarios.Count <= 0)
			{
				list.AddRange(AchieveManager.Get().GetRewardsForAchieve(item.ID, rewardTimings));
			}
		}
		chapterDataModel.CompletionRewardsEarned = false;
		chapterDataModel.CompletionRewardsNewlyEarned = false;
		Legacy_SetChapterCompletionRewardData(chapterDataModel, list);
		foreach (RewardData item2 in list)
		{
			RewardItemDataModel rewardItemDataModel = RewardUtils.RewardDataToRewardItemDataModel(item2);
			if (rewardItemDataModel != null)
			{
				chapterDataModel.CompletionRewards.Items.Add(rewardItemDataModel);
			}
			if (item2.Origin == NetCache.ProfileNotice.NoticeOrigin.ACHIEVEMENT)
			{
				Achievement achievement = AchieveManager.Get().GetAchievement((int)item2.OriginData);
				chapterDataModel.CompletionRewardsEarned |= achievement.IsCompleted();
				chapterDataModel.CompletionRewardsNewlyEarned |= achievement.IsNewlyCompleted();
			}
			else
			{
				Error.AddDevWarning("Reward Error!", "Wing Reward is from origin {0}, but we expected origin == ACHIEVEMENT!", item2.Origin);
			}
		}
		chapterDataModel.PurchaseRewards = new RewardListDataModel();
		List<RewardData> list2 = new List<RewardData>();
		HashSet<Assets.Achieve.RewardTiming> rewardTimings2 = new HashSet<Assets.Achieve.RewardTiming> { Assets.Achieve.RewardTiming.IMMEDIATE };
		foreach (Achievement item3 in achievesForAdventureWing)
		{
			if (item3.AchieveTrigger == Assets.Achieve.Trigger.LICENSEDETECTED)
			{
				list2.AddRange(AchieveManager.Get().GetRewardsForAchieve(item3.ID, rewardTimings2));
			}
		}
		foreach (RewardData item4 in list2)
		{
			RewardItemDataModel rewardItemDataModel2 = RewardUtils.RewardDataToRewardItemDataModel(item4);
			if (rewardItemDataModel2 != null)
			{
				chapterDataModel.PurchaseRewards.Items.Add(rewardItemDataModel2);
			}
		}
		int mission = (int)AdventureConfig.Get().GetMission();
		chapterDataModel.Missions.Clear();
		bool flag = false;
		foreach (ScenarioDbfRecord scenarioRecord in chapterData.ScenarioRecords)
		{
			AdventureMissionDataModel missionDataModel = new AdventureMissionDataModel();
			missionDataModel.Rewards = new RewardListDataModel();
			missionDataModel.ScenarioId = (ScenarioDbId)scenarioRecord.ID;
			missionDataModel.Selected = mission == scenarioRecord.ID;
			missionDataModel.MissionState = AdventureProgressMgr.Get().AdventureMissionStateForScenario(scenarioRecord.ID);
			HashSet<Assets.Achieve.RewardTiming> rewardTimings3 = new HashSet<Assets.Achieve.RewardTiming>
			{
				Assets.Achieve.RewardTiming.ADVENTURE_CHEST,
				Assets.Achieve.RewardTiming.IMMEDIATE
			};
			List<RewardData> rewardsForDefeatingScenario = AdventureProgressMgr.Get().GetRewardsForDefeatingScenario(scenarioRecord.ID, rewardTimings3);
			missionDataModel.Rewards.Items.Clear();
			foreach (RewardData item5 in rewardsForDefeatingScenario)
			{
				RewardItemDataModel rewardItemDataModel3 = RewardUtils.RewardDataToRewardItemDataModel(item5);
				if (rewardItemDataModel3 != null)
				{
					missionDataModel.Rewards.Items.Add(rewardItemDataModel3);
				}
			}
			AdventureConfig.Get().LoadBossDef((ScenarioDbId)scenarioRecord.ID, delegate(AdventureBossDef bossDef, bool success)
			{
				if (bossDef != null)
				{
					missionDataModel.CoinPortraitMaterial = bossDef.m_CoinPortraitMaterial.GetMaterial();
				}
			});
			int wingId = 0;
			int missionReqProgress = 0;
			bool flag2 = AdventureConfig.IsMissionNewlyAvailableAndGetReqs((int)missionDataModel.ScenarioId, ref wingId, ref missionReqProgress);
			missionDataModel.NewlyUnlocked = missionDataModel.MissionState == AdventureMissionState.UNLOCKED && flag2;
			bool flag3 = false;
			if (AdventureConfig.Get().IsScenarioDefeatedAndInitCache((ScenarioDbId)scenarioRecord.ID))
			{
				flag3 = AdventureConfig.Get().IsScenarioJustDefeated((ScenarioDbId)scenarioRecord.ID);
			}
			missionDataModel.NewlyCompleted = missionDataModel.MissionState == AdventureMissionState.COMPLETED && flag3;
			if (missionDataModel.NewlyCompleted)
			{
				flag = true;
			}
			chapterDataModel.Missions.Add(missionDataModel);
		}
		chapterDataModel.NewlyCompleted = chapterDataModel.ChapterState == AdventureChapterState.COMPLETED && flag;
		chapterDataModel.MoralAlignment = ConvertBookSectionToMoralAlignment(chapterData.BookSection);
	}

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
		}
		else
		{
			chapterDataModel.CompletionRewardType = Reward.Type.NONE;
			chapterDataModel.CompletionRewardId = 0;
			chapterDataModel.CompletionRewardQuantity = 0;
		}
	}

	private IEnumerator ShowPageUpdateVisualsWhenReady()
	{
		while (!m_allInitialTransitionsComplete)
		{
			yield return null;
		}
		if (m_pageData.PageType == AdventureBookPageType.MAP)
		{
			if (NeedToShowAdventureSectionCompletionSequence)
			{
				StartCoroutine(AnimateAdventureSectionComplete());
			}
			else
			{
				ShowChapterNewlyUnlockedMapSequenceIfNecessary();
			}
		}
		else if (m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			StartCoroutine(AnimateChapterRewardsAndCompletionIfNecessary());
		}
	}

	private void ShowChapterNewlyUnlockedMapSequenceIfNecessary()
	{
		if (m_chapterNewlyUnlockedMapSequenceQueue.Count <= 0 || m_isInUnlockedSequence || AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			return;
		}
		string text = m_chapterNewlyUnlockedMapSequenceQueue.Dequeue();
		Clickable clickable = m_chapterButtonClickablesNameMap[text];
		if (clickable == null)
		{
			Log.Adventures.PrintError("m_chapterNewlyUnlockedPopupQueue had an invalid button name! Skipping...");
			ShowChapterNewlyUnlockedMapSequenceIfNecessary();
			return;
		}
		if (clickable.GetComponent<VisualController>() == null)
		{
			Error.AddDevWarning("Missing Visual Controller", "{0} does not have a visual controller!", text);
			return;
		}
		clickable.GetDataModel(3, out var dataModel);
		AdventureChapterDataModel adventureChapterDataModel = dataModel as AdventureChapterDataModel;
		if (adventureChapterDataModel != null)
		{
			adventureChapterDataModel.WantsNewlyUnlockedSequence = true;
		}
		m_currentUnlockButtonName = text;
		m_isInUnlockedSequence = true;
	}

	public void ShowNewlyPurchasedSequenceOnChapterPage()
	{
		if (m_pageData.PageType != AdventureBookPageType.CHAPTER)
		{
			Debug.LogWarning("AdventureBookPageDisplay.ShowNewlyPurchasedSequenceOnChapterPage() called on a non-Chapter page!  This is not supported!");
		}
		else
		{
			StartCoroutine(AnimateNewlyPurchasedSequenceOnChapterPage());
		}
	}

	private IEnumerator AnimateNewlyPurchasedSequenceOnChapterPage()
	{
		m_chapterNewlyPurchasedAnimFinished = false;
		m_adventureBookPageContentsWidget.RegisterEventListener(ChapterNewlyPurchasedAnimEventListener);
		m_adventureBookPageContentsWidget.TriggerEvent("PLAY_CHAPTER_NEWLY_PURCHASED_ANIM");
		while (!m_chapterNewlyPurchasedAnimFinished)
		{
			yield return null;
		}
		m_adventureBookPageContentsWidget.RemoveEventListener(ChapterNewlyPurchasedAnimEventListener);
		RefreshPage();
	}

	private void RefreshPage()
	{
		SetupPageDataModels(m_pageData);
		StartCoroutine(ShowPageUpdateVisualsWhenReady());
		AdventureConfig.Get().SetMission(AdventureConfig.Get().GetMission());
	}

	private IEnumerator AnimateAdventureSectionComplete()
	{
		EnableInteraction(enable: false);
		NeedToShowAdventureSectionCompletionSequence = false;
		m_readyToPlayAdventureNewlyCompletedVO = false;
		m_adventureNewlyCompletedSequenceFinished = false;
		m_adventureBookPageContentsWidget.RegisterEventListener(AdventureNewlyCompletedEventListener);
		m_adventureBookPageContentsWidget.TriggerEvent("AdventureNewlyCompletedSequence");
		while (!m_readyToPlayAdventureNewlyCompletedVO)
		{
			yield return null;
		}
		ChapterPageData chapterPageData = m_pageData as ChapterPageData;
		WingDbId wingId = (WingDbId)(chapterPageData?.WingRecord.ID ?? 0);
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		DungeonCrawlSubDef_VOLines.VOEventType eventType = ((chapterPageData != null && chapterPageData.BookSection != 0) ? (GameUtils.IsModeHeroic(selectedMode) ? DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_SECOND_BOOK_SECTION) : (GameUtils.IsModeHeroic(selectedMode) ? DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.COMPLETE_ALL_WINGS));
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, eventType);
		while (!m_adventureNewlyCompletedSequenceFinished)
		{
			yield return null;
		}
		m_adventureBookPageContentsWidget.RemoveEventListener(AdventureNewlyCompletedEventListener);
		if (UserAttentionManager.CanShowAttentionGrabber("AdventureBookPageDisplay.AnimateAdventureComplete"))
		{
			bool allPopupsShown = false;
			PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate
			{
				allPopupsShown = true;
			});
			while (!allPopupsShown)
			{
				yield return null;
			}
		}
		EnableInteraction(enable: true);
	}

	private IEnumerator AnimateChapterRewardsAndCompletionIfNecessary()
	{
		if (m_needToShowMissionCompleteAnim)
		{
			EnableInteraction(enable: false);
			m_missionCompleteAnimFinished = false;
			m_adventureBookPageContentsWidget.TriggerEvent("MISSION_NEWLY_COMPLETED");
			while (!m_missionCompleteAnimFinished)
			{
				yield return null;
			}
			EnableInteraction(enable: true);
		}
		if (m_needToShowRewardChestAnim)
		{
			EnableInteraction(enable: false);
			m_rewardChestReadyToShowPopup = false;
			m_adventureBookPageContentsWidget.RegisterEventListener(RewardChestAnimEventListener);
			m_adventureBookPageContentsWidget.TriggerEvent("OPEN_CHAPTER_CHEST_REWARD");
			while (!m_rewardChestReadyToShowPopup)
			{
				yield return null;
			}
			m_adventureBookPageContentsWidget.RemoveEventListener(RewardChestAnimEventListener);
			if (UserAttentionManager.CanShowAttentionGrabber("AdventureBookPageDisplay.AnimateChapterRewardsAndCompletionIfNecessary"))
			{
				bool allPopupsShown = false;
				if (AdventureScene.Get().IsDevMode)
				{
					AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
					popupInfo.m_headerText = "Dummy Reward Popup";
					popupInfo.m_text = "This is when the reward popup would be shown if you had actually earned it!";
					popupInfo.m_showAlertIcon = false;
					popupInfo.m_responseDisplay = AlertPopup.ResponseDisplay.OK;
					popupInfo.m_responseCallback = delegate
					{
						allPopupsShown = true;
					};
					DialogManager.Get().ShowPopup(popupInfo);
				}
				else
				{
					PopupDisplayManager.Get().ShowAnyOutstandingPopups(delegate
					{
						allPopupsShown = true;
					});
				}
				while (!allPopupsShown)
				{
					yield return null;
				}
			}
			EnableInteraction(enable: true);
		}
		if (m_needToShowMissionCompleteAnim || m_needToShowRewardChestAnim)
		{
			foreach (AdventureMissionDataModel mission in m_pageDataModel.ChapterData.Missions)
			{
				mission.NewlyCompleted = false;
			}
		}
		if (m_needToShowMissionUnlockAnim)
		{
			EnableInteraction(enable: false);
			m_missionUnlockAnimFinished = false;
			m_adventureBookPageContentsWidget.RegisterEventListener(MissionNewlyUnlockedAnimEventListener);
			m_adventureBookPageContentsWidget.TriggerEvent("MISSION_NEWLY_UNLOCKED");
			AckMissionUnlocksOnCurrentPage();
			while (!m_missionUnlockAnimFinished)
			{
				yield return null;
			}
			m_adventureBookPageContentsWidget.RemoveEventListener(MissionNewlyUnlockedAnimEventListener);
			foreach (AdventureMissionDataModel mission2 in m_pageDataModel.ChapterData.Missions)
			{
				mission2.NewlyUnlocked = false;
			}
			EnableInteraction(enable: true);
		}
		if (!m_needToShowChapterCompletionAnim)
		{
			yield break;
		}
		EnableInteraction(enable: false);
		m_chapterCompletionAnimFinished = false;
		m_adventureBookPageContentsWidget.RegisterEventListener(ChapterNewlyCompletedAnimEventListener);
		m_adventureBookPageContentsWidget.TriggerEvent("CHAPTER_NEWLY_COMPLETED");
		ChapterPageData chapterData = m_pageData as ChapterPageData;
		if (chapterData != null && chapterData.ChapterToFlipToWhenCompleted == 0)
		{
			AdventureConfig.AckCurrentWingProgress(chapterData.WingRecord.ID);
		}
		while (!m_chapterCompletionAnimFinished)
		{
			yield return null;
		}
		m_adventureBookPageContentsWidget.RemoveEventListener(ChapterNewlyCompletedAnimEventListener);
		if (GameUtils.GetNormalModeFromHeroicMode(AdventureConfig.Get().GetSelectedMode()) != AdventureModeDbId.DUNGEON_CRAWL)
		{
			PlayChapterCompleteVO();
			while (NotificationManager.Get().IsQuotePlaying)
			{
				yield return null;
			}
		}
		bool flag = ((chapterData == null) ? null : GameDbf.Adventure.GetRecord((int)chapterData.Adventure))?.MapPageHasButtonsToChapters ?? false;
		if (flag && AdventureConfig.Get().HasUnacknowledgedChapterUnlocks())
		{
			AdventureBookPageManager.NavigateToMapPage();
		}
		else if (NeedToShowAdventureSectionCompletionSequence)
		{
			if (flag)
			{
				AdventureBookPageManager.NavigateToMapPage();
			}
			else
			{
				StartCoroutine(AnimateAdventureSectionComplete());
			}
		}
		else if (chapterData != null && chapterData.ChapterToFlipToWhenCompleted != 0)
		{
			if (m_flipToChapterCallback != null)
			{
				m_flipToChapterCallback(chapterData.ChapterToFlipToWhenCompleted);
			}
			EnableInteraction(enable: true);
		}
		else
		{
			EnableInteraction(enable: true);
		}
	}

	private void AckMissionUnlocksOnCurrentPage()
	{
		HashSet<int> hashSet = new HashSet<int>();
		foreach (AdventureMissionDataModel mission in m_pageDataModel.ChapterData.Missions)
		{
			if (mission.NewlyUnlocked)
			{
				int missionReqProgress = 0;
				int wingId = 0;
				if (AdventureConfig.GetMissionPlayableParameters((int)mission.ScenarioId, ref wingId, ref missionReqProgress))
				{
					hashSet.Add(wingId);
				}
			}
		}
		foreach (int item in hashSet)
		{
			AdventureConfig.AckCurrentWingProgress(item);
		}
	}

	private void PlayChapterCompleteVO()
	{
		WingDbId wingId = (WingDbId)((m_pageData as ChapterPageData)?.WingRecord.ID ?? 0);
		AdventureWingDef wingDef = AdventureScene.Get().GetWingDef(wingId);
		if (AdventureUtils.CanPlayWingCompleteQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_CompleteQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_CompleteQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_CompleteQuoteVOLine, allowRepeatDuringSession: false);
		}
	}

	private void BookPageContentsEventListener(string eventName)
	{
		switch (eventName)
		{
		case "CHAPTER_UNLOCK_BUTTON_CLICKED":
			OnChapterUnlockButtonClicked();
			break;
		case "BOSS_1_SELECTED":
			OnBossSelected(0);
			break;
		case "BOSS_2_SELECTED":
			OnBossSelected(1);
			break;
		case "BOSS_3_SELECTED":
			OnBossSelected(2);
			break;
		case "BOSS_4_SELECTED":
			OnBossSelected(3);
			break;
		case "BOSS_5_SELECTED":
			OnBossSelected(4);
			break;
		case "MISSION_NEWLY_COMPLETED_ANIM_FINISHED":
			m_missionCompleteAnimFinished = true;
			break;
		}
		if (m_pageEventListener != null)
		{
			m_pageEventListener(eventName);
		}
	}

	private void RewardChestAnimEventListener(string eventName)
	{
		if ("READY_TO_SHOW_POPUP".Equals(eventName))
		{
			m_rewardChestReadyToShowPopup = true;
		}
	}

	private void ChapterNewlyCompletedAnimEventListener(string eventName)
	{
		if ("CHAPTER_NEWLY_COMPLETED_ANIM_FINISHED".Equals(eventName))
		{
			m_chapterCompletionAnimFinished = true;
		}
	}

	private void AdventureNewlyCompletedEventListener(string eventName)
	{
		if ("PlayAdventureNewlyCompletedVO".Equals(eventName))
		{
			m_readyToPlayAdventureNewlyCompletedVO = true;
		}
		else if ("AdventureNewlyCompletedSequenceFinished".Equals(eventName))
		{
			m_adventureNewlyCompletedSequenceFinished = true;
		}
	}

	private void MissionNewlyUnlockedAnimEventListener(string eventName)
	{
		if ("MISSION_NEWLY_UNLOCKED_ANIM_FINISHED".Equals(eventName))
		{
			m_missionUnlockAnimFinished = true;
		}
	}

	private void ChapterNewlyPurchasedAnimEventListener(string eventName)
	{
		if ("CHAPTER_NEWLY_PURCHASED_ANIM_FINISHED".Equals(eventName))
		{
			m_chapterNewlyPurchasedAnimFinished = true;
		}
	}

	private void EnableInteraction(bool enable)
	{
		if (m_enableInteractionCallback != null)
		{
			m_enableInteractionCallback(enable);
		}
	}

	private void UpdateRewardPageData(PageData pageData)
	{
		RewardPageData rewardPageData = pageData as RewardPageData;
		if (rewardPageData == null)
		{
			Debug.LogError("UpdateRewardPageData(): PageData is not a valid RewardPageData! Cannot cast properly.");
		}
		else
		{
			m_pageDataModel.AllChaptersCompletedInCurrentSection = true;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			foreach (ChapterPageData value in rewardPageData.ChapterData.Values)
			{
				if (value.BookSection != pageData.BookSection)
				{
					continue;
				}
				num2 += value.ScenarioRecords.Count;
				foreach (ScenarioDbfRecord scenarioRecord in value.ScenarioRecords)
				{
					bool flag = AdventureProgressMgr.Get().HasDefeatedScenario(scenarioRecord.ID);
					if (flag)
					{
						num++;
					}
					else
					{
						m_pageDataModel.AllChaptersCompletedInCurrentSection = false;
					}
					HashSet<Assets.Achieve.RewardTiming> rewardTimings = new HashSet<Assets.Achieve.RewardTiming>
					{
						Assets.Achieve.RewardTiming.ADVENTURE_CHEST,
						Assets.Achieve.RewardTiming.IMMEDIATE
					};
					foreach (RewardData item in AdventureProgressMgr.Get().GetRewardsForDefeatingScenario(scenarioRecord.ID, rewardTimings))
					{
						if (item.RewardType != Reward.Type.CARD)
						{
							continue;
						}
						CardRewardData cardRewardData = item as CardRewardData;
						if (cardRewardData == null)
						{
							Debug.LogErrorFormat("AdventureBookPageDisplay.UpdateRewardPageData() - reward {0} is type CARD but is not a CardRewardData!", item);
							continue;
						}
						num4 += cardRewardData.Count;
						if (flag)
						{
							num3 += cardRewardData.Count;
						}
					}
				}
			}
			m_pageDataModel.NumBossesDefeatedText = GameStrings.Format("GLUE_ADVENTURE_NUM_BOSSES_DEFEATED", num, num2);
			m_pageDataModel.NumCardsCollectedText = GameStrings.Format("GLUE_ADVENTURE_NUM_CARDS_COLLECTED", num3, num4);
		}
		Reward.Type completionRewardType = AdventureConfig.Get().CompletionRewardType;
		switch (completionRewardType)
		{
		case Reward.Type.CARD_BACK:
		{
			int completionRewardId = AdventureConfig.Get().CompletionRewardId;
			if (!CardBackManager.Get().LoadCardBackByIndex(completionRewardId, OnCardBackLoaded))
			{
				Log.Adventures.PrintError("AdventureBookPageDisplay.SetCardBack() - failed to load CardBack {0}", completionRewardId);
			}
			break;
		}
		default:
			Log.Adventures.PrintWarning("Unsupported reward type for Reward Page = {0}", completionRewardType);
			break;
		case Reward.Type.NONE:
			break;
		}
	}

	private void OnCardBackLoaded(CardBackManager.LoadCardBackData cardbackData)
	{
		Actor componentInChildren = GetComponentInChildren<Actor>();
		if (componentInChildren != null)
		{
			CardBackManager.SetCardBack(componentInChildren.m_cardMesh, cardbackData.m_CardBack);
			componentInChildren.SetCardbackUpdateIgnore(ignoreUpdate: true);
		}
	}

	private void CheckForInputForCheats()
	{
		if (!AdventureScene.Get().IsDevMode || !base.IsShown)
		{
			return;
		}
		if (m_pageData.PageType == AdventureBookPageType.CHAPTER)
		{
			AdventureChapterDataModel chapterData = m_pageDataModel.ChapterData;
			if (InputCollection.GetKeyDown(KeyCode.Z))
			{
				NeedToShowAdventureSectionCompletionSequence = !NeedToShowAdventureSectionCompletionSequence;
				if (NeedToShowAdventureSectionCompletionSequence)
				{
					m_needToShowChapterCompletionAnim = true;
				}
				UIStatus.Get().AddInfo(string.Format("Adventure Completion anim {0} be played when you press Spacebar.", NeedToShowAdventureSectionCompletionSequence ? "WILL" : "will NOT"));
				chapterData.NewlyCompleted = m_needToShowChapterCompletionAnim;
			}
			else if (InputCollection.GetKeyDown(KeyCode.V))
			{
				m_needToShowChapterCompletionAnim = !m_needToShowChapterCompletionAnim;
				UIStatus.Get().AddInfo(string.Format("Chapter Completion anim {0} be played when you press Spacebar.", m_needToShowChapterCompletionAnim ? "WILL" : "will NOT"));
				chapterData.NewlyCompleted = m_needToShowChapterCompletionAnim;
			}
			else if (InputCollection.GetKeyDown(KeyCode.C))
			{
				m_needToShowRewardChestAnim = !m_needToShowRewardChestAnim;
				UIStatus.Get().AddInfo(string.Format("Reward Chest anim {0} be played when you press Spacebar.", m_needToShowRewardChestAnim ? "WILL" : "will NOT"));
				chapterData.CompletionRewardsEarned = true;
				chapterData.CompletionRewardsNewlyEarned = m_needToShowRewardChestAnim;
				if (chapterData.Missions.Count > 0)
				{
					chapterData.Missions[0].NewlyCompleted = m_needToShowRewardChestAnim;
				}
				if (m_needToShowRewardChestAnim && chapterData.ChapterState == AdventureChapterState.COMPLETED)
				{
					chapterData.NewlyCompleted = true;
				}
			}
			else if (InputCollection.GetKeyDown(KeyCode.Space))
			{
				if (!m_needToShowChapterCompletionAnim && !m_needToShowRewardChestAnim)
				{
					UIStatus.Get().AddInfo("You attempted to play the reward sequence, but you have not enabled\nthe Reward Chest anim (key C) or Chapter Complete anim (key V).");
					return;
				}
				StopCoroutine(AnimateChapterRewardsAndCompletionIfNecessary());
				StartCoroutine(AnimateChapterRewardsAndCompletionIfNecessary());
			}
		}
		else if (m_pageData.PageType == AdventureBookPageType.MAP && InputCollection.GetKeyDown(KeyCode.Z))
		{
			AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)AdventureConfig.Get().GetSelectedAdventure());
			if (record != null && record.MapPageHasButtonsToChapters)
			{
				StopCoroutine(AnimateAdventureSectionComplete());
				StartCoroutine(AnimateAdventureSectionComplete());
			}
		}
	}
}
