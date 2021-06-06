using System;
using System.Collections;
using System.Collections.Generic;
using Hearthstone.DataModels;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000027 RID: 39
public class AdventureBookPageManager : BookPageManager
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x06000130 RID: 304 RVA: 0x000077F4 File Offset: 0x000059F4
	// (remove) Token: 0x06000131 RID: 305 RVA: 0x0000782C File Offset: 0x00005A2C
	public event AdventureBookPageManager.PageClickCallback PageClicked;

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000132 RID: 306 RVA: 0x00007861 File Offset: 0x00005A61
	private int CurrentPageIndex
	{
		get
		{
			return Mathf.Max(0, this.m_currentPageNum - 1);
		}
	}

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000133 RID: 307 RVA: 0x00007871 File Offset: 0x00005A71
	// (set) Token: 0x06000134 RID: 308 RVA: 0x00007879 File Offset: 0x00005A79
	public int NumChapters { get; private set; }

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000135 RID: 309 RVA: 0x00007882 File Offset: 0x00005A82
	private int DefaultPageNum
	{
		get
		{
			if (this.m_mapPageNumber <= 0)
			{
				return 1;
			}
			return this.m_mapPageNumber;
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x06000136 RID: 310 RVA: 0x00007895 File Offset: 0x00005A95
	private bool HasMapPage
	{
		get
		{
			return this.m_mapPageNumber > 0;
		}
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000078A0 File Offset: 0x00005AA0
	protected override void Start()
	{
		AdventureBookPageManager.m_instance = this;
		base.Start();
		base.LoadPagingArrows();
		AdventureBookPageDisplay adventureBookPageDisplay = this.PageAsAdventureBookPage(this.m_pageA);
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.SetPageEventListener(new Widget.EventListenerDelegate(this.PageEventListener));
			adventureBookPageDisplay.SetFlipToChapterCallback(new AdventureBookPageDisplay.FlipToChapterCallback(this.FlipToChapter));
		}
		adventureBookPageDisplay = this.PageAsAdventureBookPage(this.m_pageB);
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.SetPageEventListener(new Widget.EventListenerDelegate(this.PageEventListener));
			adventureBookPageDisplay.SetFlipToChapterCallback(new AdventureBookPageDisplay.FlipToChapterCallback(this.FlipToChapter));
		}
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000794C File Offset: 0x00005B4C
	private void OnDestroy()
	{
		base.StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		if (this.m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_unlockChapterTooltip);
		}
		StoreManager.Get().RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
	}

	// Token: 0x06000139 RID: 313 RVA: 0x00007998 File Offset: 0x00005B98
	public void Initialize(List<PageNode> pageNodes, int numChapters)
	{
		if (!base.IsFullyLoaded())
		{
			Debug.LogWarning("AdventureBookPageManager is not fully loaded yet, you should not be calling Initialize()!");
		}
		for (int i = 0; i < pageNodes.Count; i++)
		{
			if (pageNodes[i].PageData.PageType == AdventureBookPageType.MAP)
			{
				this.m_mapPageNumber = AdventureBookPageManager.PageNodeIndexToPageNum(i);
				break;
			}
		}
		this.m_pageNodes = pageNodes;
		this.NumChapters = numChapters;
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		int currentPageNum = 0;
		if (mission != ScenarioDbId.INVALID)
		{
			this.m_currentPageNum = this.GetPageNumFromScenarioId(mission);
		}
		else if (this.PageExistsWithUnAckedCompletion(out currentPageNum))
		{
			this.m_currentPageNum = currentPageNum;
		}
		else
		{
			this.m_currentPageNum = this.DefaultPageNum;
		}
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, true, null, null);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007A40 File Offset: 0x00005C40
	public PageData GetPageDataForCurrentPage()
	{
		if (this.NumPages == 0 || this.CurrentPageIndex < 0 || this.CurrentPageIndex >= this.NumPages)
		{
			return null;
		}
		return this.m_pageNodes[this.CurrentPageIndex].PageData;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00007A7C File Offset: 0x00005C7C
	public int GetNumChaptersOwned()
	{
		int num = 0;
		foreach (PageNode pageNode in this.m_pageNodes)
		{
			ChapterPageData chapterPageData = pageNode.PageData as ChapterPageData;
			if (chapterPageData != null && AdventureProgressMgr.Get().OwnsWing(chapterPageData.WingRecord.ID))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00007AF4 File Offset: 0x00005CF4
	public AdventureBookPageDataModel GetCurrentPageDataModel()
	{
		return ((AdventureBookPageDisplay)base.GetCurrentPage()).GetAdventurePageDataModel();
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00007B06 File Offset: 0x00005D06
	public void AllInitialTransitionsComplete()
	{
		((AdventureBookPageDisplay)this.m_pageA).AllInitialTransitionsComplete();
		((AdventureBookPageDisplay)this.m_pageB).AllInitialTransitionsComplete();
		this.m_allInitialTransitionsComplete = true;
		this.ShowUnlockChapterTooltipIfNecessary();
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007B35 File Offset: 0x00005D35
	public void SetEnableInteractionCallback(AdventureBookPageDisplay.EnableInteractionCallback callback)
	{
		((AdventureBookPageDisplay)this.m_pageA).SetEnableInteractionCallback(callback);
		((AdventureBookPageDisplay)this.m_pageB).SetEnableInteractionCallback(callback);
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00007B5C File Offset: 0x00005D5C
	public void HideAllPopups()
	{
		base.StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		if (this.m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_unlockChapterTooltip, 0f);
		}
		AdventureBookPageDisplay adventureBookPageDisplay = base.GetCurrentPage() as AdventureBookPageDisplay;
		if (adventureBookPageDisplay != null)
		{
			adventureBookPageDisplay.HideAndSuppressChapterUnlockSequence();
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00007BB4 File Offset: 0x00005DB4
	protected override void AssemblePage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		if (this.CurrentPageIndex < 0 || this.CurrentPageIndex >= this.m_pageNodes.Count)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - CurrentPageIndex ({0}) is out of bounds! Unable to assemble the current page.", new object[]
			{
				this.CurrentPageIndex
			});
			return;
		}
		PageNode pageNode = this.m_pageNodes[this.CurrentPageIndex];
		if (pageNode == null)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - PageNode object at index {0} is null! Unable to assemble the current page.", new object[]
			{
				this.CurrentPageIndex
			});
			return;
		}
		PageData pageData = pageNode.PageData;
		if (pageData == null)
		{
			Debug.LogErrorFormat("AdventureBookPageManager.AssemblePage - PageData object at index {0} is null! Unable to assemble the current page.", new object[]
			{
				this.CurrentPageIndex
			});
			return;
		}
		if (pageData.PageType == AdventureBookPageType.MAP)
		{
			AdventureBookPageManager.RemoveMapPageNavigation();
			this.AssembleMapPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		else if (pageData.PageType == AdventureBookPageType.REWARD)
		{
			AdventureBookPageManager.SaveMapPageNavigation();
			this.AssembleCardBackRewardPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		else
		{
			AdventureBookPageManager.SaveMapPageNavigation();
			this.AssembleChapterPage(transitionReadyCallbackData, useCurrentPageNum);
		}
		base.SetHasPreviousAndNextPages(pageNode.PageToLeft != null, pageNode.PageToRight != null);
	}

	// Token: 0x06000141 RID: 321 RVA: 0x00007CB0 File Offset: 0x00005EB0
	private void AssembleMapPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		Log.Adventures.Print("Assembling Map page.", Array.Empty<object>());
		AdventureBookPageDisplay adventureBookPageDisplay = this.PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage);
		int num = 0;
		foreach (PageNode pageNode in this.m_pageNodes)
		{
			if (pageNode.PageData.PageType == AdventureBookPageType.CHAPTER)
			{
				ChapterPageData chapterPageData = pageNode.PageData as ChapterPageData;
				if (chapterPageData != null)
				{
					int id = chapterPageData.WingRecord.ID;
					if (AdventureProgressMgr.Get().IsWingComplete((AdventureDbId)chapterPageData.WingRecord.AdventureId, chapterPageData.AdventureMode, (WingDbId)id))
					{
						num++;
					}
				}
			}
		}
		AdventureDataModel adventureDataModel = AdventureConfig.Get().GetAdventureDataModel();
		if (adventureDataModel != null)
		{
			adventureDataModel.AllChaptersCompleted = (num >= this.NumChapters);
			adventureDataModel.MapNewlyCompleted = (adventureDataModel.AllChaptersCompleted && AdventureBookPageDisplay.NeedToShowAdventureSectionCompletionSequence);
		}
		MapPageData mapPageData = this.m_pageNodes[this.CurrentPageIndex].PageData as MapPageData;
		if (mapPageData == null)
		{
			Debug.LogErrorFormat("Page Data at index {0} is not a MapPageData object!", new object[]
			{
				this.CurrentPageIndex
			});
			return;
		}
		mapPageData.NumChaptersCompletedText = GameStrings.Format("GLUE_NUM_CHAPTERS_COMPLETED", new object[]
		{
			num,
			this.NumChapters
		});
		adventureBookPageDisplay.SetUpPage(mapPageData, delegate
		{
			this.TransitionPage(transitionReadyCallbackData);
		});
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00007E48 File Offset: 0x00006048
	private void AssembleChapterPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		if (this.CurrentPageIndex < 0 || this.CurrentPageIndex >= this.m_pageNodes.Count)
		{
			Log.Adventures.PrintError("Page Index {0} is invalid; there are only {1} Chapter Pages!", new object[]
			{
				this.CurrentPageIndex,
				this.m_pageNodes.Count
			});
			return;
		}
		ChapterPageData chapterPageData = this.m_pageNodes[this.CurrentPageIndex].PageData as ChapterPageData;
		Log.Adventures.Print("Assembling Chapter page for chapter {0}, Wing {1}.", new object[]
		{
			chapterPageData.ChapterNumber,
			chapterPageData.WingRecord.Name
		});
		this.PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage).SetUpPage(chapterPageData, delegate
		{
			this.TransitionPage(transitionReadyCallbackData);
		});
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00007F2C File Offset: 0x0000612C
	private void AssembleCardBackRewardPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		Log.Adventures.Print("Assembling CardBack page.", Array.Empty<object>());
		AdventureBookPageDisplay adventureBookPageDisplay = this.PageAsAdventureBookPage(transitionReadyCallbackData.m_assembledPage);
		PageData pageData = this.m_pageNodes[this.CurrentPageIndex].PageData;
		adventureBookPageDisplay.SetUpPage(pageData, delegate
		{
			this.TransitionPage(transitionReadyCallbackData);
		});
	}

	// Token: 0x06000144 RID: 324 RVA: 0x00007F9B File Offset: 0x0000619B
	protected override void OnPageTurnComplete(object callbackData, int operationId)
	{
		base.OnPageTurnComplete(callbackData, operationId);
		this.ShowUnlockChapterTooltipIfNecessary();
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00007FAC File Offset: 0x000061AC
	private int GetPageNumFromScenarioId(ScenarioDbId scenarioId)
	{
		if (scenarioId == ScenarioDbId.INVALID)
		{
			return this.DefaultPageNum;
		}
		Predicate<ScenarioDbfRecord> <>9__0;
		for (int i = 0; i < this.m_pageNodes.Count; i++)
		{
			if (this.m_pageNodes[i].PageData.PageType == AdventureBookPageType.CHAPTER)
			{
				ChapterPageData chapterPageData = this.m_pageNodes[i].PageData as ChapterPageData;
				if (chapterPageData != null)
				{
					List<ScenarioDbfRecord> scenarioRecords = chapterPageData.ScenarioRecords;
					Predicate<ScenarioDbfRecord> match;
					if ((match = <>9__0) == null)
					{
						match = (<>9__0 = ((ScenarioDbfRecord r) => r.ID == (int)scenarioId));
					}
					if (scenarioRecords.Exists(match))
					{
						return AdventureBookPageManager.PageNodeIndexToPageNum(i);
					}
				}
			}
		}
		return this.DefaultPageNum;
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00008058 File Offset: 0x00006258
	private bool PageExistsWithUnAckedCompletion(out int pageNum)
	{
		pageNum = this.DefaultPageNum;
		for (int i = 0; i < this.m_pageNodes.Count; i++)
		{
			PageData pageData = this.m_pageNodes[i].PageData;
			if (pageData != null && pageData.PageType == AdventureBookPageType.CHAPTER)
			{
				ChapterPageData chapterPageData = pageData as ChapterPageData;
				if (chapterPageData != null)
				{
					int id = chapterPageData.WingRecord.ID;
					bool flag;
					if (AdventureProgressMgr.Get().IsWingComplete(chapterPageData.Adventure, chapterPageData.AdventureMode, (WingDbId)id, out flag) && flag)
					{
						pageNum = AdventureBookPageManager.PageNodeIndexToPageNum(i);
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x06000147 RID: 327 RVA: 0x000080E0 File Offset: 0x000062E0
	private static int PageNodeIndexToPageNum(int pageNodeIndex)
	{
		return pageNodeIndex + 1;
	}

	// Token: 0x06000148 RID: 328 RVA: 0x000080E5 File Offset: 0x000062E5
	private int ChapterNumberToPageNum(int chapterNumber)
	{
		if (this.m_mapPageNumber == 1)
		{
			return chapterNumber + 1;
		}
		return chapterNumber;
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000080F5 File Offset: 0x000062F5
	private void FlipToChapter(int chapterNumber)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		base.FlipToPage(this.ChapterNumberToPageNum(chapterNumber), null, null);
	}

	// Token: 0x0600014A RID: 330 RVA: 0x00008110 File Offset: 0x00006310
	private void FlipToFirstUncompletedPage(int section)
	{
		int num = 0;
		ChapterPageData chapterPageData = null;
		foreach (PageNode pageNode in this.m_pageNodes)
		{
			ChapterPageData chapterPageData2 = pageNode.PageData as ChapterPageData;
			if (chapterPageData2 != null && chapterPageData2.BookSection == section)
			{
				chapterPageData = chapterPageData2;
				bool flag = false;
				if (!AdventureProgressMgr.Get().IsWingComplete(chapterPageData2.Adventure, chapterPageData2.AdventureMode, (WingDbId)chapterPageData2.WingRecord.ID, out flag) || flag)
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
		this.FlipToChapter(num);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000081C4 File Offset: 0x000063C4
	private void PageClickedCallback()
	{
		if (this.PageClicked != null)
		{
			this.PageClicked();
		}
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000081D9 File Offset: 0x000063D9
	protected override void OnPageTransitionRequested()
	{
		if (this.m_pageRightArrow != null)
		{
			this.m_pageRightArrow.HideHighlight();
		}
		this.HideAllPopups();
	}

	// Token: 0x0600014D RID: 333 RVA: 0x000081FA File Offset: 0x000063FA
	private void PageEventListener(string eventName)
	{
		if (eventName == "FLIP_TO_SECTION_1")
		{
			this.FlipToFirstUncompletedPage(0);
			return;
		}
		if (eventName == "FLIP_TO_SECTION_2")
		{
			this.FlipToFirstUncompletedPage(1);
			return;
		}
		if (!(eventName == "PAGE_CLICKED"))
		{
			return;
		}
		this.PageClickedCallback();
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000823A File Offset: 0x0000643A
	private AdventureBookPageDisplay PageAsAdventureBookPage(BookPageDisplay page)
	{
		AdventureBookPageDisplay adventureBookPageDisplay = page as AdventureBookPageDisplay;
		if (adventureBookPageDisplay == null)
		{
			Log.CollectionManager.PrintError("Page in AdventureBookPageManager is not a AdventureBookPageDisplay!  This should not happen!", Array.Empty<object>());
		}
		return adventureBookPageDisplay;
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600014F RID: 335 RVA: 0x0000825F File Offset: 0x0000645F
	private int NumPages
	{
		get
		{
			return this.m_pageNodes.Count;
		}
	}

	// Token: 0x06000150 RID: 336 RVA: 0x0000826C File Offset: 0x0000646C
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)selectedAdventure);
		if (record != null && record.MapPageHasButtonsToChapters)
		{
			if (AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, selectedAdventure))
			{
				AdventureBookPageManager.NavigateToMapPage();
				return;
			}
		}
		else
		{
			AdventureBookPageDisplay adventureBookPageDisplay = this.PageAsAdventureBookPage(base.GetCurrentPage());
			if (adventureBookPageDisplay == null)
			{
				Debug.LogWarning("AdventureBookPageManager.OnSuccessfulPurchaseAck() - No current page on which to play an unlock sequence!");
				return;
			}
			if (adventureBookPageDisplay.DoesBundleApplyToPage(bundle))
			{
				adventureBookPageDisplay.ShowNewlyPurchasedSequenceOnChapterPage();
			}
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x000082DC File Offset: 0x000064DC
	public static bool NavigateToMapPage()
	{
		if (AdventureBookPageManager.m_instance == null)
		{
			Log.Adventures.PrintError("Trying to navigate to map page, but AdventureBookPageManager has been destroyed!", Array.Empty<object>());
			return false;
		}
		if (!AdventureBookPageManager.m_instance.HasMapPage)
		{
			Debug.LogError("This Adventure Book does not have a Map Page, you should not be calling NavigateToMapPage()!");
			return false;
		}
		if (AdventureBookPageManager.m_instance.m_currentPageNum == AdventureBookPageManager.m_instance.m_mapPageNumber)
		{
			return false;
		}
		AdventureBookPageManager.m_instance.FlipToPage(AdventureBookPageManager.m_instance.m_mapPageNumber, null, null);
		return true;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00008354 File Offset: 0x00006554
	private static void SaveMapPageNavigation()
	{
		if (AdventureBookPageManager.m_instance == null)
		{
			Log.Adventures.PrintError("Trying to add map page to navigation stack, but AdventureBookPageManager has been destroyed!", Array.Empty<object>());
			return;
		}
		if (!AdventureBookPageManager.m_instance.HasMapPage)
		{
			return;
		}
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureBookPageManager.NavigateToMapPage));
	}

	// Token: 0x06000153 RID: 339 RVA: 0x000083A4 File Offset: 0x000065A4
	private static void RemoveMapPageNavigation()
	{
		if (AdventureBookPageManager.m_instance == null)
		{
			Log.Adventures.PrintError("Trying to remove map page from navigation stack, but AdventureBookPageManager has been destroyed!", Array.Empty<object>());
			return;
		}
		if (!AdventureBookPageManager.m_instance.HasMapPage)
		{
			return;
		}
		Navigation.RemoveHandler(new Navigation.NavigateBackHandler(AdventureBookPageManager.NavigateToMapPage));
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000083F2 File Offset: 0x000065F2
	private void ShowUnlockChapterTooltipIfNecessary()
	{
		if (!this.m_allInitialTransitionsComplete || !AdventureConfig.Get().ShouldSeeFirstTimeFlow || this.m_currentPageNum != this.m_mapPageNumber)
		{
			return;
		}
		base.StopCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
		base.StartCoroutine("ShowUnlockChapterTooltipWhenNoQuotePlaying");
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0000842E File Offset: 0x0000662E
	private IEnumerator ShowUnlockChapterTooltipWhenNoQuotePlaying()
	{
		while (NotificationManager.Get().IsQuotePlaying)
		{
			yield return null;
		}
		if (this.m_unlockChapterTooltip != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_unlockChapterTooltip);
		}
		Notification.PopUpArrowDirection popUpArrowDirection = Notification.PopUpArrowDirection.Right;
		VisualController component = this.m_UnlockChapterTooltipBone.GetComponent<VisualController>();
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
			this.m_pageRightArrow.ShowHighlight();
		}
		this.m_unlockChapterTooltip = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.m_UnlockChapterTooltipBone.position, this.m_UnlockChapterTooltipBone.localScale, GameStrings.Get("GLUE_ADVENTURE_ADVENTUREBOOK_DAL_UNLOCK_CHAPTER1"), true, NotificationManager.PopupTextType.BASIC);
		this.m_unlockChapterTooltip.ShowPopUpArrow(popUpArrowDirection);
		this.m_unlockChapterTooltip.PulseReminderEveryXSeconds(3f);
		yield break;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00008440 File Offset: 0x00006640
	protected override void PageRight(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		if (this.NumPages == 0 || this.CurrentPageIndex < 0 || this.CurrentPageIndex >= this.NumPages || this.m_pageNodes[this.CurrentPageIndex] == null)
		{
			Debug.LogError("AdventureBookPageManager.PageRight - No current page found! Cannot turn page without more info!");
			return;
		}
		PageNode pageToRight = this.m_pageNodes[this.CurrentPageIndex].PageToRight;
		if (pageToRight == null)
		{
			Debug.LogError("AdventureBookPageManager.PageRight - No page to right!  You shouldn't be able to turn the page in this direction!");
			return;
		}
		this.m_currentPageNum = AdventureBookPageManager.PageNodeIndexToPageNum(this.m_pageNodes.IndexOf(pageToRight));
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT, true, callback, callbackData);
	}

	// Token: 0x06000157 RID: 343 RVA: 0x000084D0 File Offset: 0x000066D0
	protected override void PageLeft(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		if (this.NumPages == 0 || this.CurrentPageIndex < 0 || this.CurrentPageIndex >= this.NumPages || this.m_pageNodes[this.CurrentPageIndex] == null)
		{
			Debug.LogError("AdventureBookPageManager.PageLeft - No current page found! Cannot turn page without more info!");
			return;
		}
		PageNode pageToLeft = this.m_pageNodes[this.CurrentPageIndex].PageToLeft;
		if (pageToLeft == null)
		{
			Debug.LogError("AdventureBookPageManager.PageLeft - No page to left!  You shouldn't be able to turn the page in this direction!");
			return;
		}
		this.m_currentPageNum = AdventureBookPageManager.PageNodeIndexToPageNum(this.m_pageNodes.IndexOf(pageToLeft));
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT, true, callback, callbackData);
	}

	// Token: 0x040000DA RID: 218
	public Transform m_UnlockChapterTooltipBone;

	// Token: 0x040000DB RID: 219
	private const string PAGE_CLICKED_EVENT_NAME = "PAGE_CLICKED";

	// Token: 0x040000DC RID: 220
	private const string FLIP_TO_SECTION_1_EVENT_NAME = "FLIP_TO_SECTION_1";

	// Token: 0x040000DD RID: 221
	private const string FLIP_TO_SECTION_2_EVENT_NAME = "FLIP_TO_SECTION_2";

	// Token: 0x040000DE RID: 222
	private List<PageNode> m_pageNodes = new List<PageNode>();

	// Token: 0x040000DF RID: 223
	private int m_mapPageNumber;

	// Token: 0x040000E0 RID: 224
	private Notification m_unlockChapterTooltip;

	// Token: 0x040000E1 RID: 225
	private bool m_allInitialTransitionsComplete;

	// Token: 0x040000E4 RID: 228
	public static AdventureBookPageManager m_instance;

	// Token: 0x02001287 RID: 4743
	// (Invoke) Token: 0x0600D47B RID: 54395
	public delegate void PageClickCallback();
}
