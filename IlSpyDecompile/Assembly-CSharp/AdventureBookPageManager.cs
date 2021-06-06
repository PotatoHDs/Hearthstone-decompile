using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

public class AdventureBookPageManager : BookPageManager
{
	public delegate void PageClickCallback();

	public Transform m_UnlockChapterTooltipBone;

	private const string PAGE_CLICKED_EVENT_NAME = "PAGE_CLICKED";

	private const string FLIP_TO_SECTION_1_EVENT_NAME = "FLIP_TO_SECTION_1";

	private const string FLIP_TO_SECTION_2_EVENT_NAME = "FLIP_TO_SECTION_2";

	private List<PageNode> m_pageNodes = new List<PageNode>();

	private int m_mapPageNumber;

	private Notification m_unlockChapterTooltip;

	private bool m_allInitialTransitionsComplete;

	public static AdventureBookPageManager m_instance;

	private int CurrentPageIndex => Mathf.Max(0, m_currentPageNum - 1);

	public int NumChapters { get; private set; }

	private int DefaultPageNum
	{
		get
		{
			if (m_mapPageNumber <= 0)
			{
				return 1;
			}
			return m_mapPageNumber;
		}
	}

	private bool HasMapPage => m_mapPageNumber > 0;

	private int NumPages => m_pageNodes.Count;

	public event PageClickCallback PageClicked;

	protected override void Start()
	{
		m_instance = this;
		base.Start();
		LoadPagingArrows();
		AdventureBookPageDisplay adventureBookPageDisplay = PageAsAdventureBookPage(m_pageA);
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.SetPageEventListener(PageEventListener);
			adventureBookPageDisplay.SetFlipToChapterCallback(FlipToChapter);
		}
		adventureBookPageDisplay = PageAsAdventureBookPage(m_pageB);
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.SetPageEventListener(PageEventListener);
			adventureBookPageDisplay.SetFlipToChapterCallback(FlipToChapter);
		}
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
	}

	private void OnDestroy()
	{
		StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		if (m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_unlockChapterTooltip);
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
	}

	public void Initialize(List<PageNode> pageNodes, int numChapters)
	{
		if (!IsFullyLoaded())
		{
			Debug.LogWarning("AdventureBookPageManager is not fully loaded yet, you should not be calling Initialize()!");
		}
		for (int i = 0; i < pageNodes.Count; i++)
		{
			if (pageNodes[i].PageData.PageType == AdventureBookPageType.MAP)
			{
				m_mapPageNumber = PageNodeIndexToPageNum(i);
				break;
			}
		}
		m_pageNodes = pageNodes;
		NumChapters = numChapters;
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		int pageNum = 0;
		if (mission != 0)
		{
			m_currentPageNum = GetPageNumFromScenarioId(mission);
		}
		else if (PageExistsWithUnAckedCompletion(out pageNum))
		{
			m_currentPageNum = pageNum;
		}
		else
		{
			m_currentPageNum = DefaultPageNum;
		}
		TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: true, null, null);
	}

	public PageData GetPageDataForCurrentPage()
	{
		if (NumPages == 0 || CurrentPageIndex < 0 || CurrentPageIndex >= NumPages)
		{
			return null;
		}
		return m_pageNodes[CurrentPageIndex].PageData;
	}

	public int GetNumChaptersOwned()
	{
		int num = 0;
		foreach (PageNode pageNode in m_pageNodes)
		{
			ChapterPageData chapterPageData = pageNode.PageData as ChapterPageData;
			if (chapterPageData != null && AdventureProgressMgr.Get().OwnsWing(chapterPageData.WingRecord.ID))
			{
				num++;
			}
		}
		return num;
	}

	public AdventureBookPageDataModel GetCurrentPageDataModel()
	{
		return ((AdventureBookPageDisplay)GetCurrentPage()).GetAdventurePageDataModel();
	}

	public void AllInitialTransitionsComplete()
	{
		((AdventureBookPageDisplay)m_pageA).AllInitialTransitionsComplete();
		((AdventureBookPageDisplay)m_pageB).AllInitialTransitionsComplete();
		m_allInitialTransitionsComplete = true;
		ShowUnlockChapterTooltipIfNecessary();
	}

	public void SetEnableInteractionCallback(AdventureBookPageDisplay.EnableInteractionCallback callback)
	{
		((AdventureBookPageDisplay)m_pageA).SetEnableInteractionCallback(callback);
		((AdventureBookPageDisplay)m_pageB).SetEnableInteractionCallback(callback);
	}

	public void HideAllPopups()
	{
		StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		if (m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotification(m_unlockChapterTooltip, 0f);
		}
		AdventureBookPageDisplay adventureBookPageDisplay = GetCurrentPage() as AdventureBookPageDisplay;
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.HideAndSuppressChapterUnlockSequence();
		}
	}

	protected override void AssemblePage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		if (CurrentPageIndex < 0 || CurrentPageIndex >= m_pageNodes.Count)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - CurrentPageIndex ({0}) is out of bounds! Unable to assemble the current page.", CurrentPageIndex);
			return;
		}
		PageNode pageNode = m_pageNodes[CurrentPageIndex];
		if (pageNode == null)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - PageNode object at index {0} is null! Unable to assemble the current page.", CurrentPageIndex);
			return;
		}
		PageData pageData = pageNode.PageData;
		if (pageData == null)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - PageData object at index {0} is null! Unable to assemble the current page.", CurrentPageIndex);
			return;
		}
		if (pageData.PageType == AdventureBookPageType.MAP)
		{
			RemoveMapPageNavigation();
			AssembleMapPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			SaveMapPageNavigation();
			AssembleCardBackRewardPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		else
		{
			SaveMapPageNavigation();
			AssembleChapterPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		SetHasPreviousAndNextPages(pageNode.PageToLeft != null, pageNode.PageToRight != null);
	}

	private void AssembleMapPage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		Log.Adventures.Print("Assembling Map page.");
		AdventureBookPageDisplay adventureBookPageDisplay = PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage);
		int num = 0;
		foreach (PageNode pageNode in m_pageNodes)
		{
			if (pageNode.PageData.PageType != AdventureBookPageType.CHAPTER)
			{
				continue;
			}
			ChapterPageData chapterPageData = pageNode.PageData as ChapterPageData;
			if (chapterPageData != null)
			{
				int iD = chapterPageData.WingRecord.ID;
				if (AdventureProgressMgr.Get().IsWingComplete((AdventureDbId)chapterPageData.WingRecord.AdventureId, chapterPageData.AdventureMode, (WingDbId)iD))
				{
					num++;
				}
			}
		}
		AdventureDataModel adventureDataModel = AdventureConfig.Get().GetAdventureDataModel();
		if (adventureDataModel != null)
		{
			adventureDataModel.AllChaptersCompleted = num >= NumChapters;
			adventureDataModel.MapNewlyCompleted = adventureDataModel.AllChaptersCompleted && AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence;
		}
		MapPageData mapPageData = m_pageNodes[CurrentPageIndex].PageData as MapPageData;
		if (mapPageData == null)
		{
			Debug.LogErrorFormat("Page Data at index {0} is not a MapPageData object!", CurrentPageIndex);
			return;
		}
		mapPageData.NumChaptersCompletedText = GameStrings.Format("GLUE_NUM_CHAPTERS_COMPLETED", num, NumChapters);
		adventureBookPageDisplay.SetUpPage(mapPageData, delegate
		{
			TransitionPage(transitionReadyCallbackData);
		});
	}

	private void AssembleChapterPage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		if (CurrentPageIndex < 0 || CurrentPageIndex >= m_pageNodes.Count)
		{
			Log.Adventures.PrintError("Page Index {0} is invalid; there are only {1} Chapter Pages!", CurrentPageIndex, m_pageNodes.Count);
			return;
		}
		ChapterPageData chapterPageData = m_pageNodes[CurrentPageIndex].PageData as ChapterPageData;
		Log.Adventures.Print("Assembling Chapter page for chapter {0}, Wing {1}.", chapterPageData.ChapterNumber, chapterPageData.WingRecord.Name);
		PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage).SetUpPage(chapterPageData, delegate
		{
			TransitionPage(transitionReadyCallbackData);
		});
	}

	private void AssembleCardBackRewardPage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		Log.Adventures.Print("Assembling CardBack page.");
		AdventureBookPageDisplay adventureBookPageDisplay = PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage);
		PageData pageData = m_pageNodes[CurrentPageIndex].PageData;
		adventureBookPageDisplay.SetUpPage(pageData, delegate
		{
			TransitionPage(transitionReadyCallbackData);
		});
	}

	protected override void OnPageTurnComplete(object callbackData, int operationId)
	{
		base.OnPageTurnComplete(callbackData, operationId);
		ShowUnlockChapterTooltipIfNecessary();
	}

	private int GetPageNumFromScenarioId(ScenarioDbId scenarioId)
	{
		if (scenarioId == ScenarioDbId.INVALID)
		{
			return DefaultPageNum;
		}
		for (int i = 0; i < m_pageNodes.Count; i++)
		{
			if (m_pageNodes[i].PageData.PageType == AdventureBookPageType.CHAPTER)
			{
				ChapterPageData chapterPageData = m_pageNodes[i].PageData as ChapterPageData;
				if (chapterPageData != null && chapterPageData.ScenarioRecords.Exists((ScenarioDbfRecord r) => r.ID == (int)scenarioId))
				{
					return PageNodeIndexToPageNum(i);
				}
			}
		}
		return DefaultPageNum;
	}

	private bool PageExistsWithUnAckedCompletion(out int pageNum)
	{
		pageNum = DefaultPageNum;
		for (int i = 0; i < m_pageNodes.Count; i++)
		{
			PageData pageData = m_pageNodes[i].PageData;
			if (pageData == null || pageData.PageType != AdventureBookPageType.CHAPTER)
			{
				continue;
			}
			ChapterPageData chapterPageData = pageData as ChapterPageData;
			if (chapterPageData != null)
			{
				int iD = chapterPageData.WingRecord.ID;
				if (AdventureProgressMgr.Get().IsWingComplete(chapterPageData.Adventure, chapterPageData.AdventureMode, (WingDbId)iD, out var wingHasUnackedProgress) && wingHasUnackedProgress)
				{
					pageNum = PageNodeIndexToPageNum(i);
					return true;
				}
			}
		}
		return false;
	}

	private static int PageNodeIndexToPageNum(int pageNodeIndex)
	{
		return pageNodeIndex + 1;
	}

	private int ChapterNumberToPageNum(int chapterNumber)
	{
		if (m_mapPageNumber == 1)
		{
			return chapterNumber + 1;
		}
		return chapterNumber;
	}

	private void FlipToChapter(int chapterNumber)
	{
		if (CanUserTurnPages())
		{
			FlipToPage(ChapterNumberToPageNum(chapterNumber), null, null);
		}
	}

	private void FlipToFirstUncompletedPage(int section)
	{
		int num = 0;
		ChapterPageData chapterPageData = null;
		foreach (PageNode pageNode in m_pageNodes)
		{
			ChapterPageData chapterPageData2 = pageNode.PageData as ChapterPageData;
			if (chapterPageData2 != null && chapterPageData2.BookSection == section)
			{
				chapterPageData = chapterPageData2;
				bool wingHasUnackedProgress = false;
				if (!AdventureProgressMgr.Get().IsWingComplete(chapterPageData2.Adventure, chapterPageData2.AdventureMode, (WingDbId)chapterPageData2.WingRecord.ID, out wingHasUnackedProgress) || wingHasUnackedProgress)
				{
					num = chapterPageData2.ChapterNumber;
					break;
				}
			}
		}
		if (num == 0 && chapterPageData != null)
		{
			num = chapterPageData.ChapterNumber;
		}
		FlipToChapter(num);
	}

	private void PageClickedCallback()
	{
		if (this.PageClicked != null)
		{
			this.PageClicked();
		}
	}

	protected override void OnPageTransitionRequested()
	{
		if (m_pageRightArrow != null)
		{
			m_pageRightArrow.HideHighlight();
		}
		HideAllPopups();
	}

	private void PageEventListener(string eventName)
	{
		switch (eventName)
		{
		case "FLIP_TO_SECTION_1":
			FlipToFirstUncompletedPage(0);
			break;
		case "FLIP_TO_SECTION_2":
			FlipToFirstUncompletedPage(1);
			break;
		case "PAGE_CLICKED":
			PageClickedCallback();
			break;
		}
	}

	private AdventureBookPageDisplay PageAsAdventureBookPage(BookPageDisplay page)
	{
		AdventureBookPageDisplay obj = page as AdventureBookPageDisplay;
		if (obj == null)
		{
			Log.CollectionManager.PrintError("Page in AdventureBookPageManager is not a AdventureBookPageDisplay!  This should not happen!");
		}
		return obj;
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)selectedAdventure);
		if (record != null && record.MapPageHasButtonsToChapters)
		{
			if (AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, selectedAdventure))
			{
				NavigateToMapPage();
			}
			return;
		}
		AdventureBookPageDisplay adventureBookPageDisplay = PageAsAdventureBookPage(GetCurrentPage());
		if (adventureBookPageDisplay == null)
		{
			Debug.LogWarning("AdventureBookPageManager.OnSuccessfulPurchaseAck() - No current page on which to play an unlock sequence!");
		}
		else if (adventureBookPageDisplay.DoesBundleApplyToPage(bundle))
		{
			adventureBookPageDisplay.ShowNewlyPurchasedSequenceOnChapterPage();
		}
	}

	public static bool NavigateToMapPage()
	{
		if (m_instance == null)
		{
			Log.Adventures.PrintError("Trying to navigate to map page, but AdventureBookPageManager has been destroyed!");
			return false;
		}
		if (!m_instance.HasMapPage)
		{
			Debug.LogError("This Adventure Book does not have a Map Page, you should not be calling NavigateToMapPage()!");
			return false;
		}
		if (m_instance.m_currentPageNum == m_instance.m_mapPageNumber)
		{
			return false;
		}
		m_instance.FlipToPage(m_instance.m_mapPageNumber, null, null);
		return true;
	}

	private static void SaveMapPageNavigation()
	{
		if (m_instance == null)
		{
			Log.Adventures.PrintError("Trying to add map page to navigation stack, but AdventureBookPageManager has been destroyed!");
		}
		else if (m_instance.HasMapPage)
		{
			Navigation.PushUnique(NavigateToMapPage);
		}
	}

	private static void RemoveMapPageNavigation()
	{
		if (m_instance == null)
		{
			Log.Adventures.PrintError("Trying to remove map page from navigation stack, but AdventureBookPageManager has been destroyed!");
		}
		else if (m_instance.HasMapPage)
		{
			Navigation.RemoveHandler(NavigateToMapPage);
		}
	}

	private void ShowUnlockChapterTooltipIfNecessary()
	{
		if (m_allInitialTransitionsComplete && AdventureConfig.Get().ShouldSeeFirstTimeFlow && m_currentPageNum == m_mapPageNumber)
		{
			StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
			StartCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		}
	}

	private IEnumerator ShowUnlockChapterTooltipWhenNoQuotePlaying()
	{
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		if (m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_unlockChapterTooltip);
		}
		Notification.PopUpArrowDirection popUpArrowDirection = Notification.PopUpArrowDirection.Right;
		VisualController component = m_UnlockChapterTooltipBone.GetComponent<VisualController>();
		if (component != null)
		{
			string state = component.State;
			if (!(state == "SECTION_SELECT_LEFT_ARROW"))
			{
				if (state == "SECTION_SELECT_DOWN_ARROW")
				{
					popUpArrowDirection = Notification.PopUpArrowDirection.Down;
				}
			}
			else
			{
				popUpArrowDirection = Notification.PopUpArrowDirection.Left;
			}
		}
		if (popUpArrowDirection == Notification.PopUpArrowDirection.Right)
		{
			m_pageRightArrow.ShowHighlight();
		}
		m_unlockChapterTooltip = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, m_UnlockChapterTooltipBone.position, m_UnlockChapterTooltipBone.localScale, GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_UNLOCK_CHAPTER1"));
		m_unlockChapterTooltip.ShowPopUpArrow(popUpArrowDirection);
		m_unlockChapterTooltip.PulseReminderEveryXSeconds(3f);
	}

	protected override void PageRight(DelOnPageTransitionComplete callback, object callbackData)
	{
		if (NumPages == 0 || CurrentPageIndex < 0 || CurrentPageIndex >= NumPages || m_pageNodes[CurrentPageIndex] == null)
		{
			Debug.LogError("AdventureBookPageManager.PageRight - No current page found! Cannot turn page without more info!");
			return;
		}
		PageNode pageToRight = m_pageNodes[CurrentPageIndex].PageToRight;
		if (pageToRight == null)
		{
			Debug.LogError("AdventureBookPageManager.PageRight - No page to right!  You shouldn't be able to turn the page in this direction!");
			return;
		}
		m_currentPageNum = PageNodeIndexToPageNum(m_pageNodes.IndexOf(pageToRight));
		TransitionPageWhenReady(PageTransitionType.SINGLE_PAGE_RIGHT, useCurrentPageNum: true, callback, callbackData);
	}

	protected override void PageLeft(DelOnPageTransitionComplete callback, object callbackData)
	{
		if (NumPages == 0 || CurrentPageIndex < 0 || CurrentPageIndex >= NumPages || m_pageNodes[CurrentPageIndex] == null)
		{
			Debug.LogError("AdventureBookPageManager.PageLeft - No current page found! Cannot turn page without more info!");
			return;
		}
		PageNode pageToLeft = m_pageNodes[CurrentPageIndex].PageToLeft;
		if (pageToLeft == null)
		{
			Debug.LogError("AdventureBookPageManager.PageLeft - No page to left!  You shouldn't be able to turn the page in this direction!");
			return;
		}
		m_currentPageNum = PageNodeIndexToPageNum(m_pageNodes.IndexOf(pageToLeft));
		TransitionPageWhenReady(PageTransitionType.SINGLE_PAGE_LEFT, useCurrentPageNum: true, callback, callbackData);
	}
}
