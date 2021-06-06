using System;
using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

// Token: 0x02000042 RID: 66
[RequireComponent(typeof(WidgetTemplate))]
public class AdventureLocationSelectBook : MonoBehaviour
{
	// Token: 0x06000343 RID: 835 RVA: 0x00014563 File Offset: 0x00012763
	private void Awake()
	{
		AdventureLocationSelectBook.m_instance = this;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0001456C File Offset: 0x0001276C
	private void Start()
	{
		base.GetComponent<AdventureSubScene>().AddSubSceneTransitionFinishedListener(new AdventureSubScene.SubSceneTransitionFinished(this.OnSubSceneTransitionFinished));
		GameMgr.Get().RegisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		WidgetTemplate widget = base.GetComponent<WidgetTemplate>();
		widget.RegisterReadyListener(delegate(object _)
		{
			this.OnTopLevelWidgetReady(widget);
		}, null, true);
		this.m_AdventureBookCoverReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnBookCoverReady));
		this.m_BookPageManager.PageTurnStart += this.OnPageTurnStart;
		this.m_BookPageManager.PageTurnComplete += this.OnPageTurnComplete;
		this.m_BookPageManager.PageClicked += this.OnPageClicked;
		this.m_BookPageManager.SetEnableInteractionCallback(new AdventureBookPageDisplay.EnableInteractionCallback(this.EnableInteraction));
		this.EnableInteraction(false);
		AdventureConfig.Get().AddAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSet));
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		this.m_justSawDungeonCrawlSubScene = (AdventureConfig.Get().PreviousSubScene == AdventureData.Adventuresubscene.DUNGEON_CRAWL);
		if (this.ShouldShowBookCoverOpeningAnim())
		{
			widget.TriggerEvent("ShowBookCover", default(Widget.TriggerEventParameters));
		}
		Navigation.PushUnique(new Navigation.NavigateBackHandler(AdventureLocationSelectBook.OnNavigateBack));
		base.StartCoroutine(this.InitChapterDataWhenReady());
	}

	// Token: 0x06000345 RID: 837 RVA: 0x000146D4 File Offset: 0x000128D4
	private void OnDestroy()
	{
		GameMgr gameMgr = GameMgr.Get();
		if (gameMgr != null)
		{
			gameMgr.UnregisterFindGameEvent(new GameMgr.FindGameCallback(this.OnFindGameEvent));
		}
		this.m_BookPageManager.PageTurnStart -= this.OnPageTurnStart;
		this.m_BookPageManager.PageTurnComplete -= this.OnPageTurnComplete;
		this.m_BookPageManager.PageClicked -= this.OnPageClicked;
		AdventureConfig adventureConfig = AdventureConfig.Get();
		if (adventureConfig != null)
		{
			adventureConfig.RemoveAdventureMissionSetListener(new AdventureConfig.AdventureMissionSet(this.OnMissionSet));
		}
		StoreManager storeManager = StoreManager.Get();
		if (storeManager != null)
		{
			storeManager.RemoveSuccessfulPurchaseAckListener(new Action<Network.Bundle, PaymentMethod>(this.OnSuccessfulPurchaseAck));
		}
		AdventureLocationSelectBook.m_instance = null;
	}

	// Token: 0x06000346 RID: 838 RVA: 0x00014781 File Offset: 0x00012981
	private void OnTopLevelWidgetReady(Widget topLevelWidget)
	{
		base.StartCoroutine(this.SetUpAdventureBookTrayOnceWidgetIsReady(topLevelWidget));
	}

	// Token: 0x06000347 RID: 839 RVA: 0x00014791 File Offset: 0x00012991
	private IEnumerator SetUpAdventureBookTrayOnceWidgetIsReady(Widget topLevelWidget)
	{
		while (topLevelWidget.IsChangingStates)
		{
			yield return null;
		}
		AdventureBookDeckTray componentInChildren = topLevelWidget.GetComponentInChildren<AdventureBookDeckTray>(false);
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "No AdventureBookDeckTray exists, or they're all hidden!", Array.Empty<object>());
		}
		else
		{
			this.SetUpAdventureBookTray(componentInChildren);
		}
		yield break;
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000147A7 File Offset: 0x000129A7
	private void OnSubSceneTransitionFinished()
	{
		base.StartCoroutine(this.StartAnimsWhenAllTransitionsComplete());
	}

	// Token: 0x06000349 RID: 841 RVA: 0x000147B6 File Offset: 0x000129B6
	private IEnumerator StartAnimsWhenAllTransitionsComplete()
	{
		while (GameUtils.IsAnyTransitionActive() || PopupDisplayManager.Get().IsShowing)
		{
			yield return null;
		}
		this.m_BookPageManager.OnBookOpening();
		if (this.ShouldShowBookCoverOpeningAnim() && this.m_bookCover != null)
		{
			this.m_bookCover.TriggerEvent("PlayBookCoverOpen", default(Widget.TriggerEventParameters));
			Log.Adventures.Print("Waiting for Book Cover Opening animation to complete...", Array.Empty<object>());
		}
		else
		{
			this.AllInitialTransitionsComplete();
		}
		yield break;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000147C5 File Offset: 0x000129C5
	private bool ShouldShowBookCoverOpeningAnim()
	{
		return AdventureConfig.Get().PreviousSubScene == AdventureData.Adventuresubscene.CHOOSER && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000147E8 File Offset: 0x000129E8
	private void OnCoverOpened(UnityEngine.Object callbackData)
	{
		if (this.m_BookPageManager == null)
		{
			Log.Adventures.PrintError("OnCoverOpen: m_BookPageManager was null!", Array.Empty<object>());
			return;
		}
		Log.Adventures.Print("Book Cover Opening animation now complete!", Array.Empty<object>());
		PageData pageDataForCurrentPage = this.m_BookPageManager.GetPageDataForCurrentPage();
		if (pageDataForCurrentPage != null && pageDataForCurrentPage.PageType == AdventureBookPageType.MAP)
		{
			DungeonCrawlSubDef_VOLines.VOEventType voEvent = (AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.DUNGEON_CRAWL_HEROIC) ? DungeonCrawlSubDef_VOLines.VOEventType.BOOK_REVEAL_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.BOOK_REVEAL;
			DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), WingDbId.INVALID, 0, voEvent, 0, true);
		}
		this.AllInitialTransitionsComplete();
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00014873 File Offset: 0x00012A73
	private void AllInitialTransitionsComplete()
	{
		this.EnableInteraction(true);
		this.m_BookPageManager.AllInitialTransitionsComplete();
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00014887 File Offset: 0x00012A87
	private IEnumerator InitChapterDataWhenReady()
	{
		while (!this.m_BookPageManager.IsFullyLoaded())
		{
			yield return null;
		}
		while (this.m_playButton == null)
		{
			yield return null;
		}
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdv = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)selectedAdv && r.ModeId == (int)selectedMode && r.WingId != 0, -1);
		int num = 0;
		Map<int, List<ChapterPageData>> map = new Map<int, List<ChapterPageData>>();
		using (List<ScenarioDbfRecord>.Enumerator enumerator = records.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ScenarioDbfRecord scenarioRecord = enumerator.Current;
				ChapterPageData chapterPageData = null;
				Predicate<ChapterPageData> <>9__2;
				foreach (List<ChapterPageData> list in map.Values)
				{
					Predicate<ChapterPageData> match;
					if ((match = <>9__2) == null)
					{
						match = (<>9__2 = ((ChapterPageData x) => x.WingRecord.ID == scenarioRecord.WingId));
					}
					chapterPageData = list.Find(match);
					if (chapterPageData != null)
					{
						break;
					}
				}
				if (chapterPageData == null)
				{
					WingDbfRecord record = GameDbf.Wing.GetRecord(scenarioRecord.WingId);
					if (record == null)
					{
						Log.Adventures.PrintError("No Wing record found for ID {0}, referenced by Scenario {1}", new object[]
						{
							scenarioRecord.WingId,
							scenarioRecord.ID
						});
						continue;
					}
					chapterPageData = new ChapterPageData
					{
						Adventure = selectedAdv,
						AdventureMode = selectedMode,
						WingRecord = record,
						BookSection = record.BookSection
					};
					if (!map.ContainsKey(record.BookSection))
					{
						map.Add(record.BookSection, new List<ChapterPageData>());
					}
					map[record.BookSection].Add(chapterPageData);
					this.m_wingRecords.Add(record);
					num++;
				}
				chapterPageData.ScenarioRecords.Add(scenarioRecord);
			}
		}
		int count = map.Count;
		List<List<ChapterPageData>> list2 = new List<List<ChapterPageData>>();
		foreach (int key in map.Keys)
		{
			list2.Add(map[key]);
			foreach (ChapterPageData chapterPageData2 in map[key])
			{
				chapterPageData2.ScenarioRecords.Sort(new Comparison<ScenarioDbfRecord>(GameUtils.MissionSortComparison));
			}
		}
		list2.Sort(delegate(List<ChapterPageData> a, List<ChapterPageData> b)
		{
			if (a.Count < 1 || b.Count < 1)
			{
				Debug.LogError("AdventureLocationSelectBook: chapterDataBySection has a section with 0 chapters in it!");
				return 0;
			}
			return a[0].WingRecord.BookSection - b[0].WingRecord.BookSection;
		});
		using (List<List<ChapterPageData>>.Enumerator enumerator5 = list2.GetEnumerator())
		{
			while (enumerator5.MoveNext())
			{
				enumerator5.Current.Sort((ChapterPageData a, ChapterPageData b) => a.WingRecord.SortOrder - b.WingRecord.SortOrder);
			}
		}
		List<PageNode> list3 = new List<PageNode>();
		bool flag = true;
		bool flag2 = true;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdv, (int)selectedMode);
		if (adventureDataRecord != null)
		{
			if (adventureDataRecord.AdventureBookMapPageLocation == AdventureData.Adventurebooklocation.END)
			{
				Debug.LogErrorFormat("Adventure {0} and Mode {1} has the Map Page Location at END, but that is not yet supported by the code!", new object[]
				{
					selectedAdv,
					selectedMode
				});
			}
			flag = (adventureDataRecord.AdventureBookMapPageLocation != AdventureData.Adventurebooklocation.NOWHERE);
			if (adventureDataRecord.AdventureBookRewardPageLocation == AdventureData.Adventurebooklocation.BEGINNING)
			{
				Debug.LogErrorFormat("Adventure {0} and Mode {1} has the Reward Page Location at BEGINNING, but that is not yet supported by the code!", new object[]
				{
					selectedAdv,
					selectedMode
				});
			}
			flag2 = (adventureDataRecord.AdventureBookRewardPageLocation != AdventureData.Adventurebooklocation.NOWHERE);
		}
		Map<int, ChapterPageData> map2 = new Map<int, ChapterPageData>();
		PageNode pageNode = null;
		if (flag)
		{
			pageNode = new PageNode(new MapPageData
			{
				Adventure = selectedAdv,
				AdventureMode = selectedMode,
				NumSectionsInBook = count,
				BookSection = -1,
				ChapterData = map2
			});
			list3.Add(pageNode);
		}
		List<List<PageNode>> list4 = new List<List<PageNode>>();
		int num2 = 1;
		foreach (List<ChapterPageData> list5 in list2)
		{
			List<PageNode> list6 = new List<PageNode>();
			list4.Add(list6);
			foreach (ChapterPageData chapterPageData3 in list5)
			{
				chapterPageData3.ChapterNumber = num2++;
				list6.Add(new PageNode(chapterPageData3));
				map2.Add(chapterPageData3.ChapterNumber, chapterPageData3);
			}
			list3.AddRange(list6.ToArray());
		}
		this.UpdateRelationalChapterData(map2);
		List<PageNode> list7 = new List<PageNode>();
		if (flag2)
		{
			for (int i = 0; i < count; i++)
			{
				PageNode item = new PageNode(new RewardPageData
				{
					Adventure = selectedAdv,
					AdventureMode = selectedMode,
					BookSection = i,
					ChapterData = map2
				});
				list7.Add(item);
				list3.Add(list7[i]);
			}
		}
		if (count == 1 && list3.Count > 1 && pageNode != null && list3[0] == pageNode)
		{
			pageNode.PageToRight = list4[0][0];
		}
		for (int j = 0; j < list4.Count; j++)
		{
			List<PageNode> list8 = list4[j];
			for (int k = 0; k < list8.Count; k++)
			{
				PageNode pageNode2 = list8[k];
				if (k == 0)
				{
					pageNode2.PageToLeft = pageNode;
				}
				else
				{
					pageNode2.PageToLeft = list8[k - 1];
				}
				if (k == list8.Count - 1)
				{
					if (j < list7.Count)
					{
						pageNode2.PageToRight = list7[j];
						list7[j].PageToLeft = pageNode2;
					}
					else
					{
						pageNode2.PageToRight = null;
					}
				}
				else if (k + 1 < list8.Count)
				{
					pageNode2.PageToRight = list8[k + 1];
				}
				else
				{
					Log.Adventures.PrintWarning("No page to set for PageToRight for Chapter index {0} in section {1}!", new object[]
					{
						k,
						j
					});
				}
			}
		}
		this.m_BookPageManager.Initialize(list3, num);
		while (this.m_BookPageManager.ArePagesTurning())
		{
			yield return null;
		}
		while (AchieveManager.Get().HasActiveLicenseAddedAchieves())
		{
			Log.Adventures.Print("Waiting on active license added achieves before entering the Adventure subscene!", Array.Empty<object>());
			yield return null;
		}
		base.GetComponent<AdventureSubScene>().SetIsLoaded(true);
		yield break;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x00014898 File Offset: 0x00012A98
	private void UpdateRelationalChapterData(Map<int, ChapterPageData> chapterNumberToChapterDataMap)
	{
		foreach (ChapterPageData chapterPageData in chapterNumberToChapterDataMap.Values)
		{
			foreach (DbfRecord dbfRecord in chapterPageData.ScenarioRecords)
			{
				int num = 0;
				int num2 = 0;
				if (AdventureConfig.GetMissionPlayableParameters(dbfRecord.ID, ref num2, ref num) && num2 != chapterPageData.WingRecord.ID)
				{
					foreach (ChapterPageData chapterPageData2 in chapterNumberToChapterDataMap.Values)
					{
						if (chapterPageData2.WingRecord.ID == num2)
						{
							if (chapterPageData2.ChapterToFlipToWhenCompleted != 0)
							{
								Debug.LogWarningFormat("Chapter {0} already had a ChapterToFlipToWhenCompleted value of {1}, setting it to {2}!  Having scenarios from multiple wings that rely on the progress of a single wing is not currently supported!", new object[]
								{
									chapterPageData2.ChapterNumber,
									chapterPageData2.ChapterToFlipToWhenCompleted,
									chapterPageData.ChapterNumber
								});
							}
							chapterPageData2.ChapterToFlipToWhenCompleted = chapterPageData.ChapterNumber;
							Log.Adventures.Print("ChapterToFlipToWhenCompleted for Chapter {0} set to Chapter {1}", new object[]
							{
								chapterPageData2.ChapterNumber,
								chapterPageData.ChapterNumber
							});
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00014A54 File Offset: 0x00012C54
	private void SetUpAdventureBookTray(AdventureBookDeckTray deckTray)
	{
		if (deckTray == null || deckTray.m_PlayButtonReference == null || deckTray.m_BackButton == null)
		{
			Error.AddDevWarning("UI Error!", "DeckTray was not properly configured!", Array.Empty<object>());
			return;
		}
		this.m_deckTrayWidget = deckTray.GetComponent<Widget>();
		if (this.m_deckTrayWidget != null)
		{
			this.m_deckTrayWidget.RegisterEventListener(new Widget.EventListenerDelegate(this.DeckTrayEventListener));
		}
		deckTray.m_PlayButtonReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnPlayButtonReady));
		deckTray.m_AnomalyModeButtonReference.RegisterReadyListener<Widget>(new Action<Widget>(this.OnAnomalyModeButtonReady));
		deckTray.m_BackButton.AddEventListener(UIEventType.RELEASE, delegate(UIEvent e)
		{
			this.OnBackButtonPress();
		});
		this.m_anomalyModeCardSourceBone = deckTray.m_anomalyModeCardSourceBone;
		this.m_anomalyModeCardBone = deckTray.m_anomalyModeCardBone;
		this.LoadAnomalyModeCard();
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00014B2C File Offset: 0x00012D2C
	private void OnPlayButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!", Array.Empty<object>());
			return;
		}
		this.m_playButtonController = buttonVisualController;
		this.m_playButton = buttonVisualController.gameObject.GetComponent<PlayButton>();
		this.m_playButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.PlayButtonRelease));
		this.SetPlayButtonStateForCurrentPage(false);
	}

	// Token: 0x06000351 RID: 849 RVA: 0x00014B90 File Offset: 0x00012D90
	private void SetPlayButtonEnabled(bool enable)
	{
		if (this.m_playButton != null)
		{
			if (enable)
			{
				if (!this.m_playButton.IsEnabled())
				{
					this.m_playButton.Enable();
					return;
				}
			}
			else if (this.m_playButton.IsEnabled())
			{
				this.m_playButton.Disable(false);
			}
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00014BE0 File Offset: 0x00012DE0
	private void OnAnomalyModeButtonReady(Widget button)
	{
		this.m_anomalyModeButton = button;
		if (button == null)
		{
			return;
		}
		Clickable componentInChildren = this.m_anomalyModeButton.GetComponentInChildren<Clickable>();
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "Anomaly Mode Button has no Clickable!  Unable to attach listeners.", Array.Empty<object>());
			return;
		}
		componentInChildren.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.AnomalyModeButtonRelease));
		TooltipZone tooltipZone = this.m_anomalyModeButton.GetComponentInChildren<TooltipZone>();
		if (tooltipZone == null)
		{
			Error.AddDevWarning("UI Error!", "Anomaly Mode Button has no TooltipZone!  Unable to attach tooltip.", Array.Empty<object>());
			return;
		}
		componentInChildren.AddEventListener(UIEventType.ROLLOVER, delegate(UIEvent e)
		{
			AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
			AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
			WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(AdventureConfig.Get().GetMission());
			if (wingIdFromMissionId == WingDbId.INVALID)
			{
				return;
			}
			if (!AdventureUtils.IsAnomalyModeAllowed(wingIdFromMissionId))
			{
				tooltipZone.ShowTooltip(GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_HEADER"), GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_UNAVAILABLE_TOOLTIP_BODY"), this.m_anomalyModeTooltipScale, 0);
				return;
			}
			if (AdventureUtils.IsAnomalyModeLocked(selectedAdventure, selectedMode))
			{
				tooltipZone.ShowTooltip(GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_HEADER"), GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_BODY"), this.m_anomalyModeTooltipScale, 0);
			}
		});
		componentInChildren.AddEventListener(UIEventType.ROLLOUT, delegate(UIEvent e)
		{
			tooltipZone.HideTooltip();
		});
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00014CA5 File Offset: 0x00012EA5
	private void OnBookCoverReady(Widget bookCover)
	{
		this.m_bookCover = bookCover;
		base.StartCoroutine(this.SetUpBookCoverReferencesWhenResolved(bookCover));
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00014CBC File Offset: 0x00012EBC
	private IEnumerator SetUpBookCoverReferencesWhenResolved(Widget bookCover)
	{
		if (bookCover == null)
		{
			Error.AddDevWarning("UI Issue!", "m_AdventureBookCover is not hooked up on AdventureLocationSelectBook, so things won't load!", Array.Empty<object>());
			yield break;
		}
		while (bookCover.IsChangingStates)
		{
			yield return null;
		}
		AnimationEventDispatcher componentInChildren = bookCover.GetComponentInChildren<AnimationEventDispatcher>();
		if (componentInChildren != null)
		{
			componentInChildren.RegisterAnimationEventListener(new OnAnimationEvent(this.OnCoverOpened));
		}
		yield break;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00014CD4 File Offset: 0x00012ED4
	public static bool OnNavigateBack()
	{
		if (AdventureLocationSelectBook.m_instance == null)
		{
			Log.Adventures.PrintError("Trying to navigate back, but AdventureLocationSelectBook has been destroyed!", Array.Empty<object>());
			return false;
		}
		AdventureConfig.Get().SetMission(ScenarioDbId.INVALID, true);
		AdventureConfig.Get().SubSceneGoBack(true);
		AdventureBookPageManager bookPageManager = AdventureLocationSelectBook.m_instance.m_BookPageManager;
		if (bookPageManager != null)
		{
			bookPageManager.HideAllPopups();
		}
		return true;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00004EB5 File Offset: 0x000030B5
	private void OnBackButtonPress()
	{
		Navigation.GoBack();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00014D38 File Offset: 0x00012F38
	private void OnPageTurnStart(BookPageManager.PageTransitionType transitionType)
	{
		this.SetPlayButtonEnabled(false);
		if (transitionType != BookPageManager.PageTransitionType.NONE)
		{
			AdventureConfig.Get().AnomalyModeActivated = false;
			AdventureConfig.Get().SetMission(ScenarioDbId.INVALID, true);
			ChapterPageData chapterPageData = this.m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData;
			AdventureChapterState adventureChapterState = AdventureChapterState.LOCKED;
			if (chapterPageData != null && chapterPageData.WingRecord != null)
			{
				adventureChapterState = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(chapterPageData.WingRecord, chapterPageData.AdventureMode);
			}
			if (chapterPageData != null && adventureChapterState != AdventureChapterState.LOCKED)
			{
				AdventureConfig.Get().SetHasSeenUnlockedChapterPage((WingDbId)chapterPageData.WingRecord.ID, true);
			}
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x00014DB8 File Offset: 0x00012FB8
	private void OnPageTurnComplete(int currentPageNum)
	{
		AdventureBookPageDataModel currentPageDataModel = this.m_BookPageManager.GetCurrentPageDataModel();
		if (this.m_deckTrayWidget != null && currentPageDataModel != null)
		{
			this.m_deckTrayWidget.BindDataModel(currentPageDataModel, false);
		}
		AdventureChapterDataModel chapterData = currentPageDataModel.ChapterData;
		ChapterPageData chapterPageData = this.m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData;
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)AdventureConfig.Get().GetSelectedAdventure());
		if (chapterPageData != null && chapterPageData.PageType == AdventureBookPageType.CHAPTER && chapterData.ChapterState != AdventureChapterState.LOCKED && chapterPageData.WingRecord.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow && record != null && !record.MapPageHasButtonsToChapters)
		{
			if (AdventureUtils.IsEntireAdventureFree((AdventureDbId)record.ID))
			{
				if (chapterPageData.ChapterNumber == 1)
				{
					AdventureConfig.Get().MarkHasSeenFirstTimeFlowComplete();
				}
			}
			else
			{
				AdventureUtils.DisplayFirstChapterFreePopup(chapterPageData, new AdventureUtils.FirstChapterFreePopupCompleteCallback(this.OnFirstChapterFreePopupDisplayed));
			}
		}
		if (!this.m_justSawDungeonCrawlSubScene)
		{
			this.PlayPageSpecificVO();
		}
		this.m_justSawDungeonCrawlSubScene = false;
	}

	// Token: 0x06000359 RID: 857 RVA: 0x00014E9D File Offset: 0x0001309D
	private void OnFirstChapterFreePopupDisplayed()
	{
		this.PlayPageSpecificVO();
	}

	// Token: 0x0600035A RID: 858 RVA: 0x00014EA8 File Offset: 0x000130A8
	private void OnMissionSet(ScenarioDbId mission, bool showDetails)
	{
		this.SetPlayButtonStateForCurrentPage(true);
		if (this.m_deckTrayWidget != null)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)mission);
			string missionHeroCardId = GameUtils.GetMissionHeroCardId((int)mission);
			IDataModel dataModel;
			this.m_deckTrayWidget.GetDataModel(111, out dataModel);
			HeroDataModel heroDataModel = dataModel as HeroDataModel;
			if (heroDataModel == null)
			{
				heroDataModel = new HeroDataModel();
				this.m_deckTrayWidget.BindDataModel(heroDataModel, false);
			}
			if (heroDataModel.HeroCard == null)
			{
				heroDataModel.HeroCard = new CardDataModel();
			}
			heroDataModel.HeroCard.CardId = missionHeroCardId;
			string missionHeroPowerCardId = GameUtils.GetMissionHeroPowerCardId((int)mission);
			if (heroDataModel.HeroPowerCard == null)
			{
				heroDataModel.HeroPowerCard = new CardDataModel();
			}
			heroDataModel.HeroPowerCard.CardId = missionHeroPowerCardId;
			if (record == null)
			{
				heroDataModel.Name = null;
				heroDataModel.Description = null;
			}
			else
			{
				heroDataModel.Name = record.ShortName;
				heroDataModel.Description = ((UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
			}
		}
		if (mission != ScenarioDbId.INVALID && AdventureProgressMgr.Get().CanPlayScenario((int)mission, true))
		{
			if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && GameMgr.Get().GetPreviousMissionId() == (int)mission)
			{
				return;
			}
			AdventureBossDef bossDef = AdventureConfig.Get().GetBossDef(mission);
			if (bossDef != null && bossDef.m_IntroLinePlayTime == AdventureBossDef.IntroLinePlayTime.MissionSelect)
			{
				AdventureUtils.PlayMissionQuote(bossDef, NotificationManager.DEFAULT_CHARACTER_POS);
			}
		}
	}

	// Token: 0x0600035B RID: 859 RVA: 0x00015007 File Offset: 0x00013207
	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (!AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, AdventureConfig.Get().GetSelectedAdventure()))
		{
			return;
		}
		this.PlayPageSpecificVO();
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00015024 File Offset: 0x00013224
	private void BurstPlayButton()
	{
		if (this.m_playButton == null || !this.m_playButton.IsEnabled())
		{
			return;
		}
		if (this.m_playButtonController == null)
		{
			Log.Adventures.PrintError("Attempting to burst Play Button, but m_playButtonController is null!", Array.Empty<object>());
			return;
		}
		this.m_playButtonController.Owner.TriggerEvent("BURST", default(Widget.TriggerEventParameters));
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0001508F File Offset: 0x0001328F
	private void OnPageClicked()
	{
		this.BurstPlayButton();
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00015097 File Offset: 0x00013297
	private void SetPlayButtonStateForCurrentPage(bool showBurst)
	{
		if (this.PlayButtonShouldBeEnabled())
		{
			this.SetPlayButtonEnabled(true);
			if (showBurst)
			{
				this.BurstPlayButton();
				return;
			}
		}
		else
		{
			this.SetPlayButtonEnabled(false);
		}
	}

	// Token: 0x0600035F RID: 863 RVA: 0x000150BC File Offset: 0x000132BC
	private void PlayButtonRelease(UIEvent e)
	{
		this.SetPlayButtonEnabled(false);
		if (!this.PlayButtonShouldBeEnabled())
		{
			Log.Adventures.PrintError("Play Button should be disabled, but you clicked it anyway!", Array.Empty<object>());
			return;
		}
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		AdventureBossDef bossDef = AdventureConfig.Get().GetBossDef(mission);
		if (bossDef != null && bossDef.m_IntroLinePlayTime == AdventureBossDef.IntroLinePlayTime.MissionStart)
		{
			AdventureUtils.PlayMissionQuote(bossDef, NotificationManager.DEFAULT_CHARACTER_POS);
		}
		if (AdventureConfig.DoesMissionRequireDeck(AdventureConfig.Get().GetMission()))
		{
			AdventureData.Adventuresubscene subscene;
			if (GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()) && !AdventureConfig.Get().IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure())
			{
				subscene = AdventureData.Adventuresubscene.DUNGEON_CRAWL;
			}
			else
			{
				subscene = AdventureConfig.Get().SubSceneForPickingHeroForCurrentAdventure();
			}
			AdventureConfig.Get().ChangeSubScene(subscene, true);
			return;
		}
		GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMissionToPlay(), 0, 0L, null, null, false, null, GameType.GT_UNKNOWN);
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00015194 File Offset: 0x00013394
	private bool PlayButtonShouldBeEnabled()
	{
		if (AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			return false;
		}
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		return mission != ScenarioDbId.INVALID && AdventureProgressMgr.Get().CanPlayScenario((int)mission, true);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x000151D0 File Offset: 0x000133D0
	private void AnomalyModeButtonRelease(UIEvent e)
	{
		if (!this.IsAnomalyModeAvailable())
		{
			return;
		}
		AdventureConfig.Get().AnomalyModeActivated = !AdventureConfig.Get().AnomalyModeActivated;
	}

	// Token: 0x06000362 RID: 866 RVA: 0x000151F4 File Offset: 0x000133F4
	private bool IsAnomalyModeAvailable()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(AdventureConfig.Get().GetMission());
		return AdventureUtils.IsAnomalyModeAvailable(selectedAdventure, selectedMode, wingIdFromMissionId);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x00015230 File Offset: 0x00013430
	private void LoadAnomalyModeCard()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		long num = 0L;
		if (adventureDataRecord.GameSaveDataServerKey > 0)
		{
			GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ANOMALY_MODE_CARD_PREVIEW, out num);
		}
		if (num <= 0L)
		{
			if (adventureDataRecord.AnomalyModeDefaultCardId == 0)
			{
				return;
			}
			num = (long)adventureDataRecord.AnomalyModeDefaultCardId;
		}
		string cardId = GameUtils.TranslateDbIdToCardId((int)num, false);
		DefLoader.Get().LoadFullDef(cardId, new DefLoader.LoadDefCallback<DefLoader.DisposableFullDef>(this.OnAnomalyModeFullDefLoaded), null, null);
	}

	// Token: 0x06000364 RID: 868 RVA: 0x000152B8 File Offset: 0x000134B8
	private void OnAnomalyModeFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogWarningFormat("OnAnomalyModeFullDefLoaded: No FullDef found for cardId {0}!", new object[]
			{
				cardId
			});
			return;
		}
		AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, TAG_PREMIUM.NORMAL), new PrefabCallback<GameObject>(this.OnAnomalyModeActorLoaded), fullDef, AssetLoadingOptions.IgnorePrefabPosition);
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00015308 File Offset: 0x00013508
	private void OnAnomalyModeActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DefLoader.DisposableFullDef disposableFullDef = callbackData as DefLoader.DisposableFullDef;
		using (disposableFullDef)
		{
			Actor component = go.GetComponent<Actor>();
			this.m_anomalyModeCardActor = component;
			if (disposableFullDef == null)
			{
				Debug.LogWarning("OnAnomalyModeActorLoaded: no FullDef passed in!");
			}
			else if (component == null)
			{
				Debug.LogWarningFormat("OnAnomalyModeActorLoaded: actor \"{0}\" has no Actor component", new object[]
				{
					assetRef
				});
			}
			else
			{
				GameUtils.SetParent(component, base.gameObject, false);
				SceneUtils.SetLayer(component, base.gameObject.layer);
				component.TurnOffCollider();
				component.SetFullDef(disposableFullDef);
				component.UpdateAllComponents();
				component.SetUnlit();
				component.Hide();
			}
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000153B4 File Offset: 0x000135B4
	private void DeckTrayEventListener(string eventName)
	{
		if (eventName.Equals("ShowAnomalyModeBigCard") && this.IsAnomalyModeAvailable())
		{
			this.ShowAnomalyModeBigCard();
			return;
		}
		if (eventName.Equals("HideAnomalyModeBigCard"))
		{
			this.HideAnomalyModeBigCard();
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000153E8 File Offset: 0x000135E8
	private void ShowAnomalyModeBigCard()
	{
		if (this.m_anomalyModeCardActor == null)
		{
			Debug.LogWarning("ShowAnomalyModeBigCard: m_anomalyModeCardActor is not loaded!");
			return;
		}
		if (this.m_anomalyModeCardShown)
		{
			return;
		}
		this.m_anomalyModeCardShown = true;
		iTween.Stop(this.m_anomalyModeCardActor.gameObject);
		this.m_anomalyModeCardActor.Show();
		HighlightRender componentInChildren = this.m_anomalyModeCardActor.GetComponentInChildren<HighlightRender>();
		MeshRenderer meshRenderer = (componentInChildren != null) ? componentInChildren.GetComponent<MeshRenderer>() : null;
		if (meshRenderer != null && this.m_anomalyModeCardHighlightMaterial != null)
		{
			meshRenderer.SetSharedMaterial(this.m_anomalyModeCardHighlightMaterial);
			meshRenderer.enabled = true;
		}
		this.m_anomalyModeCardActor.gameObject.transform.position = this.m_anomalyModeCardBone.position;
		this.m_anomalyModeCardActor.gameObject.transform.localScale = this.m_anomalyModeCardBone.localScale;
		AnimationUtil.GrowThenDrift(this.m_anomalyModeCardActor.gameObject, this.m_anomalyModeCardSourceBone.position, this.m_anomalyModeCardDriftScale);
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000154E4 File Offset: 0x000136E4
	private void HideAnomalyModeBigCard()
	{
		if (this.m_anomalyModeCardActor == null)
		{
			Debug.LogWarning("ShowAnomalyModeBigCard: m_anomalyModeCardActor is not loaded!");
			return;
		}
		if (!this.m_anomalyModeCardShown)
		{
			return;
		}
		this.m_anomalyModeCardShown = false;
		iTween.Stop(this.m_anomalyModeCardActor.gameObject);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_anomalyModeCardSourceBone.position,
			"time",
			this.m_anomalyModeCardHideAnimTime,
			"easeType",
			iTween.EaseType.easeOutQuart
		});
		iTween.MoveTo(this.m_anomalyModeCardActor.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"scale",
			Vector3.one * 0.05f,
			"time",
			this.m_anomalyModeCardHideAnimTime,
			"oncomplete",
			"AnomalyModeCardShrinkComplete",
			"oncompletetarget",
			base.gameObject
		});
		iTween.ScaleTo(this.m_anomalyModeCardActor.gameObject, args2);
	}

	// Token: 0x06000369 RID: 873 RVA: 0x000155FC File Offset: 0x000137FC
	private void AnomalyModeCardShrinkComplete()
	{
		this.m_anomalyModeCardActor.Hide();
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0001560C File Offset: 0x0001380C
	private void EnableInteraction(bool enable)
	{
		Widget component = base.GetComponent<Widget>();
		if (component == null)
		{
			Error.AddDevWarning("UI Issue!", "The component AdventureLocationSelectBook is not attached to a Widget!", Array.Empty<object>());
			return;
		}
		component.TriggerEvent(enable ? "EnableInteraction" : "DisableInteraction", default(Widget.TriggerEventParameters));
	}

	// Token: 0x0600036B RID: 875 RVA: 0x00015660 File Offset: 0x00013860
	private void PlayPageSpecificVO()
	{
		PageData pageDataForCurrentPage = this.m_BookPageManager.GetPageDataForCurrentPage();
		if (pageDataForCurrentPage == null)
		{
			Log.Adventures.PrintWarning("AdventureBookPageManager.PlayPageSpecificVO was called but no page data exists.", Array.Empty<object>());
			return;
		}
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		DungeonCrawlSubDef_VOLines.VOEventType voeventType = DungeonCrawlSubDef_VOLines.VOEventType.INVALID;
		ChapterPageData chapterPageData = pageDataForCurrentPage as ChapterPageData;
		WingDbfRecord wingDbfRecord = (chapterPageData != null) ? chapterPageData.WingRecord : null;
		AdventureChapterState adventureChapterState = (wingDbfRecord != null) ? AdventureProgressMgr.Get().AdventureBookChapterStateForWing(wingDbfRecord, selectedMode) : AdventureChapterState.LOCKED;
		WingDbId wingDbId = (WingDbId)((wingDbfRecord != null) ? wingDbfRecord.ID : 0);
		base.StopCoroutine("PlayChapterPageQuoteAfterDelay");
		AdventureBookPageType pageType = pageDataForCurrentPage.PageType;
		if (pageType != AdventureBookPageType.CHAPTER)
		{
			if (pageType == AdventureBookPageType.REWARD)
			{
				voeventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, WingDbId.INVALID, 0, new DungeonCrawlSubDef_VOLines.VOEventType[]
				{
					DungeonCrawlSubDef_VOLines.VOEventType.REWARD_PAGE_REVEAL
				}, 0);
			}
		}
		else
		{
			int numChaptersOwned = this.m_BookPageManager.GetNumChaptersOwned();
			if (adventureChapterState == AdventureChapterState.LOCKED || AdventureConfig.Get().ShouldSeeFirstTimeFlow)
			{
				voeventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, wingDbId, 0, new DungeonCrawlSubDef_VOLines.VOEventType[]
				{
					DungeonCrawlSubDef_VOLines.VOEventType.WING_UNLOCK
				}, 0);
			}
			if (voeventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && numChaptersOwned == this.m_BookPageManager.NumChapters)
			{
				voeventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, wingDbId, 0, new DungeonCrawlSubDef_VOLines.VOEventType[]
				{
					DungeonCrawlSubDef_VOLines.VOEventType.ANOMALY_UNLOCK
				}, 0);
			}
			if (voeventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && !AdventureConfig.Get().ShouldSeeFirstTimeFlow && adventureChapterState == AdventureChapterState.LOCKED)
			{
				voeventType = DungeonCrawlSubDef_VOLines.VOEventType.CALL_TO_ACTION;
			}
			if (voeventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && !AdventureConfig.Get().ShouldSeeFirstTimeFlow && adventureChapterState != AdventureChapterState.LOCKED)
			{
				AdventureMission.WingProgress progress = AdventureProgressMgr.Get().GetProgress((int)wingDbId);
				if (progress != null && progress.IsOwned())
				{
					base.StartCoroutine("PlayChapterPageQuoteAfterDelay", AdventureScene.Get().GetWingDef(wingDbId));
					return;
				}
			}
		}
		if (voeventType != DungeonCrawlSubDef_VOLines.VOEventType.INVALID)
		{
			DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingDbId, 0, voeventType, 0, true);
		}
	}

	// Token: 0x0600036C RID: 876 RVA: 0x000157F2 File Offset: 0x000139F2
	private IEnumerator PlayChapterPageQuoteAfterDelay(AdventureWingDef wingDef)
	{
		if (wingDef == null)
		{
			Debug.LogError("AdventureLocationSelectBook.PlayChapterPageQuoteAfterDelay() called with no AdventureWingDef passed in!");
			yield break;
		}
		yield return new WaitForSeconds(wingDef.m_OpenQuoteDelay);
		if (NotificationManager.Get().IsQuotePlaying)
		{
			if (AdventureUtils.CanPlayWingOpenQuote(wingDef))
			{
				NotificationManager.Get().ForceAddSoundToPlayedList(wingDef.m_OpenQuoteVOLine);
			}
			yield break;
		}
		WingDbId wingId = wingDef.GetWingId();
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		if (wingId != WingDbId.INVALID)
		{
			if (DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, DungeonCrawlSubDef_VOLines.VOEventType.CHAPTER_PAGE, 0, true))
			{
				while (NotificationManager.Get().IsQuotePlaying)
				{
					yield return null;
				}
			}
			else if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && mission != ScenarioDbId.INVALID && !AdventureProgressMgr.Get().HasDefeatedScenario((int)mission))
			{
				DungeonCrawlSubDef_VOLines.VOEventType voEvent = ((this.m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData).BookSection == 0) ? DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1 : DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1_SECOND_BOOK_SECTION;
				if (DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, voEvent, 0, true))
				{
					yield break;
				}
			}
		}
		if (AdventureUtils.CanPlayWingOpenQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_OpenQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_OpenQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_OpenQuoteVOLine, false, 0f, CanvasAnchor.BOTTOM_LEFT, false);
		}
		yield break;
	}

	// Token: 0x0600036D RID: 877 RVA: 0x00015808 File Offset: 0x00013A08
	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if (state - FindGameState.CLIENT_CANCELED <= 1 || state - FindGameState.BNET_QUEUE_CANCELED <= 1 || state == FindGameState.SERVER_GAME_CANCELED)
		{
			this.HandleGameStartupFailure();
		}
		return false;
	}

	// Token: 0x0600036E RID: 878 RVA: 0x00015834 File Offset: 0x00013A34
	private void HandleGameStartupFailure()
	{
		this.SetPlayButtonEnabled(this.PlayButtonShouldBeEnabled());
	}

	// Token: 0x04000254 RID: 596
	public AdventureBookPageManager m_BookPageManager;

	// Token: 0x04000255 RID: 597
	public AsyncReference m_AdventureBookCoverReference;

	// Token: 0x04000256 RID: 598
	public Material m_anomalyModeCardHighlightMaterial;

	// Token: 0x04000257 RID: 599
	public float m_anomalyModeCardHideAnimTime = 0.25f;

	// Token: 0x04000258 RID: 600
	public float m_anomalyModeCardDriftScale = 2f;

	// Token: 0x04000259 RID: 601
	public float m_anomalyModeTooltipScale = 6f;

	// Token: 0x0400025A RID: 602
	private PlayButton m_playButton;

	// Token: 0x0400025B RID: 603
	private VisualController m_playButtonController;

	// Token: 0x0400025C RID: 604
	private Widget m_bookCover;

	// Token: 0x0400025D RID: 605
	private Widget m_anomalyModeButton;

	// Token: 0x0400025E RID: 606
	private Widget m_deckTrayWidget;

	// Token: 0x0400025F RID: 607
	private List<WingDbfRecord> m_wingRecords = new List<WingDbfRecord>();

	// Token: 0x04000260 RID: 608
	private Actor m_anomalyModeCardActor;

	// Token: 0x04000261 RID: 609
	private Transform m_anomalyModeCardSourceBone;

	// Token: 0x04000262 RID: 610
	private Transform m_anomalyModeCardBone;

	// Token: 0x04000263 RID: 611
	private bool m_anomalyModeCardShown;

	// Token: 0x04000264 RID: 612
	private bool m_justSawDungeonCrawlSubScene;

	// Token: 0x04000265 RID: 613
	private const string BOOK_COVER_OPEN_EVENT = "PlayBookCoverOpen";

	// Token: 0x04000266 RID: 614
	private const string ANOMALY_BUTTON_UNLOCKED_STATE = "UNLOCKED_ANOMALY";

	// Token: 0x04000267 RID: 615
	private const string ANOMALY_BUTTON_ACTIVATED_STATE = "ACTIVATED_ANOMALY";

	// Token: 0x04000268 RID: 616
	private const string ANOMALY_BUTTON_LOCKED_STATE = "LOCKED_ANOMALY";

	// Token: 0x04000269 RID: 617
	private const string PLAY_BUTTON_BURST_FX = "BURST";

	// Token: 0x0400026A RID: 618
	private const string ENABLE_INTERACTION_EVENT = "EnableInteraction";

	// Token: 0x0400026B RID: 619
	private const string DISABLE_INTERACTION_EVENT = "DisableInteraction";

	// Token: 0x0400026C RID: 620
	private const string SHOW_BOOK_COVER_EVENT = "ShowBookCover";

	// Token: 0x0400026D RID: 621
	private const string SHOW_ANOMALY_MODE_BIG_CARD_EVENT_NAME = "ShowAnomalyModeBigCard";

	// Token: 0x0400026E RID: 622
	private const string HIDE_ANOMALY_MODE_BIG_CARD_EVENT_NAME = "HideAnomalyModeBigCard";

	// Token: 0x0400026F RID: 623
	private static AdventureLocationSelectBook m_instance;
}
