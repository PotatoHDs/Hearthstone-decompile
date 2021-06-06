using System.Collections;
using System.Collections.Generic;
using Assets;
using Hearthstone.DataModels;
using Hearthstone.UI;
using PegasusShared;
using UnityEngine;

[RequireComponent(typeof(WidgetTemplate))]
public class AdventureLocationSelectBook : MonoBehaviour
{
	public AdventureBookPageManager m_BookPageManager;

	public AsyncReference m_AdventureBookCoverReference;

	public Material m_anomalyModeCardHighlightMaterial;

	public float m_anomalyModeCardHideAnimTime = 0.25f;

	public float m_anomalyModeCardDriftScale = 2f;

	public float m_anomalyModeTooltipScale = 6f;

	private PlayButton m_playButton;

	private VisualController m_playButtonController;

	private Widget m_bookCover;

	private Widget m_anomalyModeButton;

	private Widget m_deckTrayWidget;

	private List<WingDbfRecord> m_wingRecords = new List<WingDbfRecord>();

	private Actor m_anomalyModeCardActor;

	private Transform m_anomalyModeCardSourceBone;

	private Transform m_anomalyModeCardBone;

	private bool m_anomalyModeCardShown;

	private bool m_justSawDungeonCrawlSubScene;

	private const string BOOK_COVER_OPEN_EVENT = "PlayBookCoverOpen";

	private const string ANOMALY_BUTTON_UNLOCKED_STATE = "UNLOCKED_ANOMALY";

	private const string ANOMALY_BUTTON_ACTIVATED_STATE = "ACTIVATED_ANOMALY";

	private const string ANOMALY_BUTTON_LOCKED_STATE = "LOCKED_ANOMALY";

	private const string PLAY_BUTTON_BURST_FX = "BURST";

	private const string ENABLE_INTERACTION_EVENT = "EnableInteraction";

	private const string DISABLE_INTERACTION_EVENT = "DisableInteraction";

	private const string SHOW_BOOK_COVER_EVENT = "ShowBookCover";

	private const string SHOW_ANOMALY_MODE_BIG_CARD_EVENT_NAME = "ShowAnomalyModeBigCard";

	private const string HIDE_ANOMALY_MODE_BIG_CARD_EVENT_NAME = "HideAnomalyModeBigCard";

	private static AdventureLocationSelectBook m_instance;

	private void Awake()
	{
		m_instance = this;
	}

	private void Start()
	{
		GetComponent<AdventureSubScene>().AddSubSceneTransitionFinishedListener(OnSubSceneTransitionFinished);
		GameMgr.Get().RegisterFindGameEvent(OnFindGameEvent);
		WidgetTemplate widget = GetComponent<WidgetTemplate>();
		widget.RegisterReadyListener(delegate
		{
			OnTopLevelWidgetReady(widget);
		});
		m_AdventureBookCoverReference.RegisterReadyListener<Widget>(OnBookCoverReady);
		m_BookPageManager.PageTurnStart += OnPageTurnStart;
		m_BookPageManager.PageTurnComplete += OnPageTurnComplete;
		m_BookPageManager.PageClicked += OnPageClicked;
		m_BookPageManager.SetEnableInteractionCallback(EnableInteraction);
		EnableInteraction(enable: false);
		AdventureConfig.Get().AddAdventureMissionSetListener(OnMissionSet);
		StoreManager.Get().RegisterSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		m_justSawDungeonCrawlSubScene = AdventureConfig.Get().PreviousSubScene == AdventureData.Adventuresubscene.DUNGEON_CRAWL;
		if (ShouldShowBookCoverOpeningAnim())
		{
			widget.TriggerEvent("ShowBookCover");
		}
		Navigation.PushUnique(OnNavigateBack);
		StartCoroutine(InitChapterDataWhenReady());
	}

	private void OnDestroy()
	{
		GameMgr.Get()?.UnregisterFindGameEvent(OnFindGameEvent);
		m_BookPageManager.PageTurnStart -= OnPageTurnStart;
		m_BookPageManager.PageTurnComplete -= OnPageTurnComplete;
		m_BookPageManager.PageClicked -= OnPageClicked;
		AdventureConfig.Get()?.RemoveAdventureMissionSetListener(OnMissionSet);
		StoreManager.Get()?.RemoveSuccessfulPurchaseAckListener(OnSuccessfulPurchaseAck);
		m_instance = null;
	}

	private void OnTopLevelWidgetReady(Widget topLevelWidget)
	{
		StartCoroutine(SetUpAdventureBookTrayOnceWidgetIsReady(topLevelWidget));
	}

	private IEnumerator SetUpAdventureBookTrayOnceWidgetIsReady(Widget topLevelWidget)
	{
		while (topLevelWidget.IsChangingStates)
		{
			yield return null;
		}
		AdventureBookDeckTray componentInChildren = topLevelWidget.GetComponentInChildren<AdventureBookDeckTray>(includeInactive: false);
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "No AdventureBookDeckTray exists, or they're all hidden!");
		}
		else
		{
			SetUpAdventureBookTray(componentInChildren);
		}
	}

	private void OnSubSceneTransitionFinished()
	{
		StartCoroutine(StartAnimsWhenAllTransitionsComplete());
	}

	private IEnumerator StartAnimsWhenAllTransitionsComplete()
	{
		while (GameUtils.IsAnyTransitionActive() || PopupDisplayManager.Get().IsShowing)
		{
			yield return null;
		}
		m_BookPageManager.OnBookOpening();
		if (ShouldShowBookCoverOpeningAnim() && m_bookCover != null)
		{
			m_bookCover.TriggerEvent("PlayBookCoverOpen");
			Log.Adventures.Print("Waiting for Book Cover Opening animation to complete...");
		}
		else
		{
			AllInitialTransitionsComplete();
		}
	}

	private bool ShouldShowBookCoverOpeningAnim()
	{
		if (AdventureConfig.Get().PreviousSubScene == AdventureData.Adventuresubscene.CHOOSER)
		{
			return SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY;
		}
		return false;
	}

	private void OnCoverOpened(Object callbackData)
	{
		if (m_BookPageManager == null)
		{
			Log.Adventures.PrintError("OnCoverOpen: m_BookPageManager was null!");
			return;
		}
		Log.Adventures.Print("Book Cover Opening animation now complete!");
		PageData pageDataForCurrentPage = m_BookPageManager.GetPageDataForCurrentPage();
		if (pageDataForCurrentPage != null && pageDataForCurrentPage.PageType == AdventureBookPageType.MAP)
		{
			DungeonCrawlSubDef_VOLines.VOEventType voEvent = ((AdventureConfig.Get().GetSelectedMode() == AdventureModeDbId.DUNGEON_CRAWL_HEROIC) ? DungeonCrawlSubDef_VOLines.VOEventType.BOOK_REVEAL_HEROIC : DungeonCrawlSubDef_VOLines.VOEventType.BOOK_REVEAL);
			DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), WingDbId.INVALID, 0, voEvent);
		}
		AllInitialTransitionsComplete();
	}

	private void AllInitialTransitionsComplete()
	{
		EnableInteraction(enable: true);
		m_BookPageManager.AllInitialTransitionsComplete();
	}

	private IEnumerator InitChapterDataWhenReady()
	{
		while (!m_BookPageManager.IsFullyLoaded())
		{
			yield return null;
		}
		while (m_playButton == null)
		{
			yield return null;
		}
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdv = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		List<ScenarioDbfRecord> records = GameDbf.Scenario.GetRecords((ScenarioDbfRecord r) => r.AdventureId == (int)selectedAdv && r.ModeId == (int)selectedMode && r.WingId != 0);
		int num = 0;
		Map<int, List<ChapterPageData>> map = new Map<int, List<ChapterPageData>>();
		foreach (ScenarioDbfRecord scenarioRecord in records)
		{
			ChapterPageData chapterPageData = null;
			foreach (List<ChapterPageData> value in map.Values)
			{
				chapterPageData = value.Find((ChapterPageData x) => x.WingRecord.ID == scenarioRecord.WingId);
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
					Log.Adventures.PrintError("No Wing record found for ID {0}, referenced by Scenario {1}", scenarioRecord.WingId, scenarioRecord.ID);
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
				m_wingRecords.Add(record);
				num++;
			}
			chapterPageData.ScenarioRecords.Add(scenarioRecord);
		}
		int count = map.Count;
		List<List<ChapterPageData>> list = new List<List<ChapterPageData>>();
		foreach (int key in map.Keys)
		{
			list.Add(map[key]);
			foreach (ChapterPageData item2 in map[key])
			{
				item2.ScenarioRecords.Sort(GameUtils.MissionSortComparison);
			}
		}
		list.Sort(delegate(List<ChapterPageData> a, List<ChapterPageData> b)
		{
			if (a.Count < 1 || b.Count < 1)
			{
				Debug.LogError("AdventureLocationSelectBook: chapterDataBySection has a section with 0 chapters in it!");
				return 0;
			}
			return a[0].WingRecord.BookSection - b[0].WingRecord.BookSection;
		});
		foreach (List<ChapterPageData> item3 in list)
		{
			item3.Sort((ChapterPageData a, ChapterPageData b) => a.WingRecord.SortOrder - b.WingRecord.SortOrder);
		}
		List<PageNode> list2 = new List<PageNode>();
		bool flag = true;
		bool flag2 = true;
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdv, (int)selectedMode);
		if (adventureDataRecord != null)
		{
			if (adventureDataRecord.AdventureBookMapPageLocation == AdventureData.Adventurebooklocation.END)
			{
				Debug.LogErrorFormat("Adventure {0} and Mode {1} has the Map Page Location at END, but that is not yet supported by the code!", selectedAdv, selectedMode);
			}
			flag = adventureDataRecord.AdventureBookMapPageLocation != AdventureData.Adventurebooklocation.NOWHERE;
			if (adventureDataRecord.AdventureBookRewardPageLocation == AdventureData.Adventurebooklocation.BEGINNING)
			{
				Debug.LogErrorFormat("Adventure {0} and Mode {1} has the Reward Page Location at BEGINNING, but that is not yet supported by the code!", selectedAdv, selectedMode);
			}
			flag2 = adventureDataRecord.AdventureBookRewardPageLocation != AdventureData.Adventurebooklocation.NOWHERE;
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
			list2.Add(pageNode);
		}
		List<List<PageNode>> list3 = new List<List<PageNode>>();
		int num2 = 1;
		foreach (List<ChapterPageData> item4 in list)
		{
			List<PageNode> list4 = new List<PageNode>();
			list3.Add(list4);
			foreach (ChapterPageData item5 in item4)
			{
				item5.ChapterNumber = num2++;
				list4.Add(new PageNode(item5));
				map2.Add(item5.ChapterNumber, item5);
			}
			list2.AddRange(list4.ToArray());
		}
		UpdateRelationalChapterData(map2);
		List<PageNode> list5 = new List<PageNode>();
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
				list5.Add(item);
				list2.Add(list5[i]);
			}
		}
		if (count == 1 && list2.Count > 1 && pageNode != null && list2[0] == pageNode)
		{
			pageNode.PageToRight = list3[0][0];
		}
		for (int j = 0; j < list3.Count; j++)
		{
			List<PageNode> list6 = list3[j];
			for (int k = 0; k < list6.Count; k++)
			{
				PageNode pageNode2 = list6[k];
				if (k == 0)
				{
					pageNode2.PageToLeft = pageNode;
				}
				else
				{
					pageNode2.PageToLeft = list6[k - 1];
				}
				if (k == list6.Count - 1)
				{
					if (j < list5.Count)
					{
						pageNode2.PageToRight = list5[j];
						list5[j].PageToLeft = pageNode2;
					}
					else
					{
						pageNode2.PageToRight = null;
					}
				}
				else if (k + 1 < list6.Count)
				{
					pageNode2.PageToRight = list6[k + 1];
				}
				else
				{
					Log.Adventures.PrintWarning("No page to set for PageToRight for Chapter index {0} in section {1}!", k, j);
				}
			}
		}
		m_BookPageManager.Initialize(list2, num);
		while (m_BookPageManager.ArePagesTurning())
		{
			yield return null;
		}
		while (AchieveManager.Get().HasActiveLicenseAddedAchieves())
		{
			Log.Adventures.Print("Waiting on active license added achieves before entering the Adventure subscene!");
			yield return null;
		}
		GetComponent<AdventureSubScene>().SetIsLoaded(loaded: true);
	}

	private void UpdateRelationalChapterData(Map<int, ChapterPageData> chapterNumberToChapterDataMap)
	{
		foreach (ChapterPageData value in chapterNumberToChapterDataMap.Values)
		{
			foreach (ScenarioDbfRecord scenarioRecord in value.ScenarioRecords)
			{
				int missionReqProgress = 0;
				int wingId = 0;
				if (!AdventureConfig.GetMissionPlayableParameters(scenarioRecord.ID, ref wingId, ref missionReqProgress) || wingId == value.WingRecord.ID)
				{
					continue;
				}
				foreach (ChapterPageData value2 in chapterNumberToChapterDataMap.Values)
				{
					if (value2.WingRecord.ID == wingId)
					{
						if (value2.ChapterToFlipToWhenCompleted != 0)
						{
							Debug.LogWarningFormat("Chapter {0} already had a ChapterToFlipToWhenCompleted value of {1}, setting it to {2}!  Having scenarios from multiple wings that rely on the progress of a single wing is not currently supported!", value2.ChapterNumber, value2.ChapterToFlipToWhenCompleted, value.ChapterNumber);
						}
						value2.ChapterToFlipToWhenCompleted = value.ChapterNumber;
						Log.Adventures.Print("ChapterToFlipToWhenCompleted for Chapter {0} set to Chapter {1}", value2.ChapterNumber, value.ChapterNumber);
						break;
					}
				}
			}
		}
	}

	private void SetUpAdventureBookTray(AdventureBookDeckTray deckTray)
	{
		if (deckTray == null || deckTray.m_PlayButtonReference == null || deckTray.m_BackButton == null)
		{
			Error.AddDevWarning("UI Error!", "DeckTray was not properly configured!");
			return;
		}
		m_deckTrayWidget = deckTray.GetComponent<Widget>();
		if (m_deckTrayWidget != null)
		{
			m_deckTrayWidget.RegisterEventListener(DeckTrayEventListener);
		}
		deckTray.m_PlayButtonReference.RegisterReadyListener<VisualController>(OnPlayButtonReady);
		deckTray.m_AnomalyModeButtonReference.RegisterReadyListener<Widget>(OnAnomalyModeButtonReady);
		deckTray.m_BackButton.AddEventListener(UIEventType.RELEASE, delegate
		{
			OnBackButtonPress();
		});
		m_anomalyModeCardSourceBone = deckTray.m_anomalyModeCardSourceBone;
		m_anomalyModeCardBone = deckTray.m_anomalyModeCardBone;
		LoadAnomalyModeCard();
	}

	private void OnPlayButtonReady(VisualController buttonVisualController)
	{
		if (buttonVisualController == null)
		{
			Error.AddDevWarning("UI Error!", "PlayButton could not be found! You will not be able to click 'Play'!");
			return;
		}
		m_playButtonController = buttonVisualController;
		m_playButton = buttonVisualController.gameObject.GetComponent<PlayButton>();
		m_playButton.AddEventListener(UIEventType.RELEASE, PlayButtonRelease);
		SetPlayButtonStateForCurrentPage(showBurst: false);
	}

	private void SetPlayButtonEnabled(bool enable)
	{
		if (!(m_playButton != null))
		{
			return;
		}
		if (enable)
		{
			if (!m_playButton.IsEnabled())
			{
				m_playButton.Enable();
			}
		}
		else if (m_playButton.IsEnabled())
		{
			m_playButton.Disable();
		}
	}

	private void OnAnomalyModeButtonReady(Widget button)
	{
		m_anomalyModeButton = button;
		if (button == null)
		{
			return;
		}
		Clickable componentInChildren = m_anomalyModeButton.GetComponentInChildren<Clickable>();
		if (componentInChildren == null)
		{
			Error.AddDevWarning("UI Error!", "Anomaly Mode Button has no Clickable!  Unable to attach listeners.");
			return;
		}
		componentInChildren.AddEventListener(UIEventType.RELEASE, AnomalyModeButtonRelease);
		TooltipZone tooltipZone = m_anomalyModeButton.GetComponentInChildren<TooltipZone>();
		if (tooltipZone == null)
		{
			Error.AddDevWarning("UI Error!", "Anomaly Mode Button has no TooltipZone!  Unable to attach tooltip.");
			return;
		}
		componentInChildren.AddEventListener(UIEventType.ROLLOVER, delegate
		{
			AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
			AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
			WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(AdventureConfig.Get().GetMission());
			if (wingIdFromMissionId != 0)
			{
				if (!AdventureUtils.IsAnomalyModeAllowed(wingIdFromMissionId))
				{
					tooltipZone.ShowTooltip(GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_HEADER"), GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_UNAVAILABLE_TOOLTIP_BODY"), m_anomalyModeTooltipScale);
				}
				else if (AdventureUtils.IsAnomalyModeLocked(selectedAdventure, selectedMode))
				{
					tooltipZone.ShowTooltip(GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_HEADER"), GameStrings.Get("GLUE_ADVENTURE_DUNGEON_CRAWL_ANOMALY_MODE_BUTTON_LOCKED_TOOLTIP_BODY"), m_anomalyModeTooltipScale);
				}
			}
		});
		componentInChildren.AddEventListener(UIEventType.ROLLOUT, delegate
		{
			tooltipZone.HideTooltip();
		});
	}

	private void OnBookCoverReady(Widget bookCover)
	{
		m_bookCover = bookCover;
		StartCoroutine(SetUpBookCoverReferencesWhenResolved(bookCover));
	}

	private IEnumerator SetUpBookCoverReferencesWhenResolved(Widget bookCover)
	{
		if (bookCover == null)
		{
			Error.AddDevWarning("UI Issue!", "m_AdventureBookCover is not hooked up on AdventureLocationSelectBook, so things won't load!");
			yield break;
		}
		while (bookCover.IsChangingStates)
		{
			yield return null;
		}
		AnimationEventDispatcher componentInChildren = bookCover.GetComponentInChildren<AnimationEventDispatcher>();
		if (componentInChildren != null)
		{
			componentInChildren.RegisterAnimationEventListener(OnCoverOpened);
		}
	}

	public static bool OnNavigateBack()
	{
		if (m_instance == null)
		{
			Log.Adventures.PrintError("Trying to navigate back, but AdventureLocationSelectBook has been destroyed!");
			return false;
		}
		AdventureConfig.Get().SetMission(ScenarioDbId.INVALID);
		AdventureConfig.Get().SubSceneGoBack();
		AdventureBookPageManager bookPageManager = m_instance.m_BookPageManager;
		if (bookPageManager != null)
		{
			bookPageManager.HideAllPopups();
		}
		return true;
	}

	private void OnBackButtonPress()
	{
		Navigation.GoBack();
	}

	private void OnPageTurnStart(BookPageManager.PageTransitionType transitionType)
	{
		SetPlayButtonEnabled(enable: false);
		if (transitionType != 0)
		{
			AdventureConfig.Get().AnomalyModeActivated = false;
			AdventureConfig.Get().SetMission(ScenarioDbId.INVALID);
			ChapterPageData chapterPageData = m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData;
			AdventureChapterState adventureChapterState = AdventureChapterState.LOCKED;
			if (chapterPageData != null && chapterPageData.WingRecord != null)
			{
				adventureChapterState = AdventureProgressMgr.Get().AdventureBookChapterStateForWing(chapterPageData.WingRecord, chapterPageData.AdventureMode);
			}
			if (chapterPageData != null && adventureChapterState != 0)
			{
				AdventureConfig.Get().SetHasSeenUnlockedChapterPage((WingDbId)chapterPageData.WingRecord.ID, hasSeen: true);
			}
		}
	}

	private void OnPageTurnComplete(int currentPageNum)
	{
		AdventureBookPageDataModel currentPageDataModel = m_BookPageManager.GetCurrentPageDataModel();
		if (m_deckTrayWidget != null && currentPageDataModel != null)
		{
			m_deckTrayWidget.BindDataModel(currentPageDataModel);
		}
		AdventureChapterDataModel chapterData = currentPageDataModel.ChapterData;
		ChapterPageData chapterPageData = m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData;
		AdventureDbfRecord record = GameDbf.Adventure.GetRecord((int)AdventureConfig.Get().GetSelectedAdventure());
		if (chapterPageData != null && chapterPageData.PageType == AdventureBookPageType.CHAPTER && chapterData.ChapterState != 0 && chapterPageData.WingRecord.PmtProductIdForSingleWingPurchase == 0 && AdventureConfig.Get().ShouldSeeFirstTimeFlow && record != null && !record.MapPageHasButtonsToChapters)
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
				AdventureUtils.DisplayFirstChapterFreePopup(chapterPageData, OnFirstChapterFreePopupDisplayed);
			}
		}
		if (!m_justSawDungeonCrawlSubScene)
		{
			PlayPageSpecificVO();
		}
		m_justSawDungeonCrawlSubScene = false;
	}

	private void OnFirstChapterFreePopupDisplayed()
	{
		PlayPageSpecificVO();
	}

	private void OnMissionSet(ScenarioDbId mission, bool showDetails)
	{
		SetPlayButtonStateForCurrentPage(showBurst: true);
		if (m_deckTrayWidget != null)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)mission);
			string missionHeroCardId = GameUtils.GetMissionHeroCardId((int)mission);
			m_deckTrayWidget.GetDataModel(111, out var model);
			HeroDataModel heroDataModel = model as HeroDataModel;
			if (heroDataModel == null)
			{
				heroDataModel = new HeroDataModel();
				m_deckTrayWidget.BindDataModel(heroDataModel);
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
				heroDataModel.Description = (((bool)UniversalInputManager.UsePhoneUI && !string.IsNullOrEmpty(record.ShortDescription)) ? record.ShortDescription : record.Description);
			}
		}
		if (mission != 0 && AdventureProgressMgr.Get().CanPlayScenario((int)mission) && (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY || GameMgr.Get().GetPreviousMissionId() != (int)mission))
		{
			AdventureBossDef bossDef = AdventureConfig.Get().GetBossDef(mission);
			if (bossDef != null && bossDef.m_IntroLinePlayTime == AdventureBossDef.IntroLinePlayTime.MissionSelect)
			{
				AdventureUtils.PlayMissionQuote(bossDef, NotificationManager.DEFAULT_CHARACTER_POS);
			}
		}
	}

	private void OnSuccessfulPurchaseAck(Network.Bundle bundle, PaymentMethod purchaseMethod)
	{
		if (AdventureUtils.DoesBundleIncludeWingForAdventure(bundle, AdventureConfig.Get().GetSelectedAdventure()))
		{
			PlayPageSpecificVO();
		}
	}

	private void BurstPlayButton()
	{
		if (!(m_playButton == null) && m_playButton.IsEnabled())
		{
			if (m_playButtonController == null)
			{
				Log.Adventures.PrintError("Attempting to burst Play Button, but m_playButtonController is null!");
			}
			else
			{
				m_playButtonController.Owner.TriggerEvent("BURST");
			}
		}
	}

	private void OnPageClicked()
	{
		BurstPlayButton();
	}

	private void SetPlayButtonStateForCurrentPage(bool showBurst)
	{
		if (PlayButtonShouldBeEnabled())
		{
			SetPlayButtonEnabled(enable: true);
			if (showBurst)
			{
				BurstPlayButton();
			}
		}
		else
		{
			SetPlayButtonEnabled(enable: false);
		}
	}

	private void PlayButtonRelease(UIEvent e)
	{
		SetPlayButtonEnabled(enable: false);
		if (!PlayButtonShouldBeEnabled())
		{
			Log.Adventures.PrintError("Play Button should be disabled, but you clicked it anyway!");
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
			AdventureData.Adventuresubscene subscene = ((!GameUtils.DoesAdventureModeUseDungeonCrawlFormat(AdventureConfig.Get().GetSelectedMode()) || AdventureConfig.Get().IsHeroSelectedBeforeDungeonCrawlScreenForSelectedAdventure()) ? AdventureConfig.Get().SubSceneForPickingHeroForCurrentAdventure() : AdventureData.Adventuresubscene.DUNGEON_CRAWL);
			AdventureConfig.Get().ChangeSubScene(subscene);
		}
		else
		{
			GameMgr.Get().FindGame(GameType.GT_VS_AI, FormatType.FT_WILD, (int)AdventureConfig.Get().GetMissionToPlay(), 0, 0L);
		}
	}

	private bool PlayButtonShouldBeEnabled()
	{
		if (AdventureConfig.Get().ShouldSeeFirstTimeFlow)
		{
			return false;
		}
		ScenarioDbId mission = AdventureConfig.Get().GetMission();
		if (mission == ScenarioDbId.INVALID)
		{
			return false;
		}
		if (!AdventureProgressMgr.Get().CanPlayScenario((int)mission))
		{
			return false;
		}
		return true;
	}

	private void AnomalyModeButtonRelease(UIEvent e)
	{
		if (IsAnomalyModeAvailable())
		{
			AdventureConfig.Get().AnomalyModeActivated = !AdventureConfig.Get().AnomalyModeActivated;
		}
	}

	private bool IsAnomalyModeAvailable()
	{
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		WingDbId wingIdFromMissionId = GameUtils.GetWingIdFromMissionId(AdventureConfig.Get().GetMission());
		return AdventureUtils.IsAnomalyModeAvailable(selectedAdventure, selectedMode, wingIdFromMissionId);
	}

	private void LoadAnomalyModeCard()
	{
		AdventureConfig adventureConfig = AdventureConfig.Get();
		AdventureDbId selectedAdventure = adventureConfig.GetSelectedAdventure();
		AdventureModeDbId selectedMode = adventureConfig.GetSelectedMode();
		AdventureDataDbfRecord adventureDataRecord = GameUtils.GetAdventureDataRecord((int)selectedAdventure, (int)selectedMode);
		long value = 0L;
		if (adventureDataRecord.GameSaveDataServerKey > 0)
		{
			GameSaveDataManager.Get().GetSubkeyValue((GameSaveKeyId)adventureDataRecord.GameSaveDataServerKey, GameSaveKeySubkeyId.DUNGEON_CRAWL_ANOMALY_MODE_CARD_PREVIEW, out value);
		}
		if (value <= 0)
		{
			if (adventureDataRecord.AnomalyModeDefaultCardId == 0)
			{
				return;
			}
			value = adventureDataRecord.AnomalyModeDefaultCardId;
		}
		string cardId = GameUtils.TranslateDbIdToCardId((int)value);
		DefLoader.Get().LoadFullDef(cardId, OnAnomalyModeFullDefLoaded);
	}

	private void OnAnomalyModeFullDefLoaded(string cardId, DefLoader.DisposableFullDef fullDef, object userData)
	{
		if (fullDef == null)
		{
			Debug.LogWarningFormat("OnAnomalyModeFullDefLoaded: No FullDef found for cardId {0}!", cardId);
		}
		else
		{
			AssetLoader.Get().InstantiatePrefab(ActorNames.GetHandActor(fullDef.EntityDef, TAG_PREMIUM.NORMAL), OnAnomalyModeActorLoaded, fullDef, AssetLoadingOptions.IgnorePrefabPosition);
		}
	}

	private void OnAnomalyModeActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		DefLoader.DisposableFullDef disposableFullDef = callbackData as DefLoader.DisposableFullDef;
		using (disposableFullDef)
		{
			Actor actor = (m_anomalyModeCardActor = go.GetComponent<Actor>());
			if (disposableFullDef == null)
			{
				Debug.LogWarning("OnAnomalyModeActorLoaded: no FullDef passed in!");
				return;
			}
			if (actor == null)
			{
				Debug.LogWarningFormat("OnAnomalyModeActorLoaded: actor \"{0}\" has no Actor component", assetRef);
				return;
			}
			GameUtils.SetParent(actor, base.gameObject);
			SceneUtils.SetLayer(actor, base.gameObject.layer);
			actor.TurnOffCollider();
			actor.SetFullDef(disposableFullDef);
			actor.UpdateAllComponents();
			actor.SetUnlit();
			actor.Hide();
		}
	}

	private void DeckTrayEventListener(string eventName)
	{
		if (eventName.Equals("ShowAnomalyModeBigCard") && IsAnomalyModeAvailable())
		{
			ShowAnomalyModeBigCard();
		}
		else if (eventName.Equals("HideAnomalyModeBigCard"))
		{
			HideAnomalyModeBigCard();
		}
	}

	private void ShowAnomalyModeBigCard()
	{
		if (m_anomalyModeCardActor == null)
		{
			Debug.LogWarning("ShowAnomalyModeBigCard: m_anomalyModeCardActor is not loaded!");
		}
		else if (!m_anomalyModeCardShown)
		{
			m_anomalyModeCardShown = true;
			iTween.Stop(m_anomalyModeCardActor.gameObject);
			m_anomalyModeCardActor.Show();
			HighlightRender componentInChildren = m_anomalyModeCardActor.GetComponentInChildren<HighlightRender>();
			MeshRenderer meshRenderer = ((componentInChildren != null) ? componentInChildren.GetComponent<MeshRenderer>() : null);
			if (meshRenderer != null && m_anomalyModeCardHighlightMaterial != null)
			{
				meshRenderer.SetSharedMaterial(m_anomalyModeCardHighlightMaterial);
				meshRenderer.enabled = true;
			}
			m_anomalyModeCardActor.gameObject.transform.position = m_anomalyModeCardBone.position;
			m_anomalyModeCardActor.gameObject.transform.localScale = m_anomalyModeCardBone.localScale;
			AnimationUtil.GrowThenDrift(m_anomalyModeCardActor.gameObject, m_anomalyModeCardSourceBone.position, m_anomalyModeCardDriftScale);
		}
	}

	private void HideAnomalyModeBigCard()
	{
		if (m_anomalyModeCardActor == null)
		{
			Debug.LogWarning("ShowAnomalyModeBigCard: m_anomalyModeCardActor is not loaded!");
		}
		else if (m_anomalyModeCardShown)
		{
			m_anomalyModeCardShown = false;
			iTween.Stop(m_anomalyModeCardActor.gameObject);
			Hashtable args = iTween.Hash("position", m_anomalyModeCardSourceBone.position, "time", m_anomalyModeCardHideAnimTime, "easeType", iTween.EaseType.easeOutQuart);
			iTween.MoveTo(m_anomalyModeCardActor.gameObject, args);
			Hashtable args2 = iTween.Hash("scale", Vector3.one * 0.05f, "time", m_anomalyModeCardHideAnimTime, "oncomplete", "AnomalyModeCardShrinkComplete", "oncompletetarget", base.gameObject);
			iTween.ScaleTo(m_anomalyModeCardActor.gameObject, args2);
		}
	}

	private void AnomalyModeCardShrinkComplete()
	{
		m_anomalyModeCardActor.Hide();
	}

	private void EnableInteraction(bool enable)
	{
		Widget component = GetComponent<Widget>();
		if (component == null)
		{
			Error.AddDevWarning("UI Issue!", "The component AdventureLocationSelectBook is not attached to a Widget!");
		}
		else
		{
			component.TriggerEvent(enable ? "EnableInteraction" : "DisableInteraction");
		}
	}

	private void PlayPageSpecificVO()
	{
		PageData pageDataForCurrentPage = m_BookPageManager.GetPageDataForCurrentPage();
		if (pageDataForCurrentPage == null)
		{
			Log.Adventures.PrintWarning("AdventureBookPageManager.PlayPageSpecificVO was called but no page data exists.");
			return;
		}
		AdventureDbId selectedAdventure = AdventureConfig.Get().GetSelectedAdventure();
		AdventureModeDbId selectedMode = AdventureConfig.Get().GetSelectedMode();
		DungeonCrawlSubDef_VOLines.VOEventType vOEventType = DungeonCrawlSubDef_VOLines.VOEventType.INVALID;
		WingDbfRecord wingDbfRecord = (pageDataForCurrentPage as ChapterPageData)?.WingRecord;
		AdventureChapterState adventureChapterState = ((wingDbfRecord != null) ? AdventureProgressMgr.Get().AdventureBookChapterStateForWing(wingDbfRecord, selectedMode) : AdventureChapterState.LOCKED);
		WingDbId wingDbId = (WingDbId)(wingDbfRecord?.ID ?? 0);
		StopCoroutine("PlayChapterPageQuoteAfterDelay");
		switch (pageDataForCurrentPage.PageType)
		{
		case AdventureBookPageType.CHAPTER:
		{
			int numChaptersOwned = m_BookPageManager.GetNumChaptersOwned();
			if (adventureChapterState == AdventureChapterState.LOCKED || AdventureConfig.Get().ShouldSeeFirstTimeFlow)
			{
				vOEventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, wingDbId, 0, new DungeonCrawlSubDef_VOLines.VOEventType[1] { DungeonCrawlSubDef_VOLines.VOEventType.WING_UNLOCK });
			}
			if (vOEventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && numChaptersOwned == m_BookPageManager.NumChapters)
			{
				vOEventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, wingDbId, 0, new DungeonCrawlSubDef_VOLines.VOEventType[1] { DungeonCrawlSubDef_VOLines.VOEventType.ANOMALY_UNLOCK });
			}
			if (vOEventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && !AdventureConfig.Get().ShouldSeeFirstTimeFlow && adventureChapterState == AdventureChapterState.LOCKED)
			{
				vOEventType = DungeonCrawlSubDef_VOLines.VOEventType.CALL_TO_ACTION;
			}
			if (vOEventType == DungeonCrawlSubDef_VOLines.VOEventType.INVALID && !AdventureConfig.Get().ShouldSeeFirstTimeFlow && adventureChapterState != 0)
			{
				AdventureMission.WingProgress progress = AdventureProgressMgr.Get().GetProgress((int)wingDbId);
				if (progress != null && progress.IsOwned())
				{
					StartCoroutine("PlayChapterPageQuoteAfterDelay", AdventureScene.Get().GetWingDef(wingDbId));
					return;
				}
			}
			break;
		}
		case AdventureBookPageType.REWARD:
			vOEventType = DungeonCrawlSubDef_VOLines.GetNextValidEventType(selectedAdventure, WingDbId.INVALID, 0, new DungeonCrawlSubDef_VOLines.VOEventType[1] { DungeonCrawlSubDef_VOLines.VOEventType.REWARD_PAGE_REVEAL });
			break;
		}
		if (vOEventType != 0)
		{
			DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingDbId, 0, vOEventType);
		}
	}

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
		if (wingId != 0)
		{
			if (DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, DungeonCrawlSubDef_VOLines.VOEventType.CHAPTER_PAGE))
			{
				while (NotificationManager.Get().IsQuotePlaying)
				{
					yield return null;
				}
			}
			else if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && mission != 0 && !AdventureProgressMgr.Get().HasDefeatedScenario((int)mission))
			{
				DungeonCrawlSubDef_VOLines.VOEventType voEvent = (((m_BookPageManager.GetPageDataForCurrentPage() as ChapterPageData).BookSection == 0) ? DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1 : DungeonCrawlSubDef_VOLines.VOEventType.BOSS_LOSS_1_SECOND_BOOK_SECTION);
				if (DungeonCrawlSubDef_VOLines.PlayVOLine(AdventureConfig.Get().GetSelectedAdventure(), wingId, 0, voEvent))
				{
					yield break;
				}
			}
		}
		if (AdventureUtils.CanPlayWingOpenQuote(wingDef))
		{
			string legacyAssetName = new AssetReference(wingDef.m_OpenQuoteVOLine).GetLegacyAssetName();
			NotificationManager.Get().CreateCharacterQuote(wingDef.m_OpenQuotePrefab, GameStrings.Get(legacyAssetName), wingDef.m_OpenQuoteVOLine, allowRepeatDuringSession: false);
		}
	}

	private bool OnFindGameEvent(FindGameEventData eventData, object userData)
	{
		FindGameState state = eventData.m_state;
		if ((uint)(state - 2) <= 1u || (uint)(state - 7) <= 1u || state == FindGameState.SERVER_GAME_CANCELED)
		{
			HandleGameStartupFailure();
		}
		return false;
	}

	private void HandleGameStartupFailure()
	{
		SetPlayButtonEnabled(PlayButtonShouldBeEnabled());
	}
}
