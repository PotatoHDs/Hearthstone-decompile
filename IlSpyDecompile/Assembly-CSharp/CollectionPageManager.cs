using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

[CustomEditClass]
public class CollectionPageManager : TabbedBookPageManager
{
	public static readonly Map<TAG_CLASS, UnityEngine.Vector2> s_classTextureOffsets = new Map<TAG_CLASS, UnityEngine.Vector2>
	{
		{
			TAG_CLASS.MAGE,
			new UnityEngine.Vector2(0f, 0f)
		},
		{
			TAG_CLASS.PALADIN,
			new UnityEngine.Vector2(0.205f, 0f)
		},
		{
			TAG_CLASS.PRIEST,
			new UnityEngine.Vector2(0.392f, 0f)
		},
		{
			TAG_CLASS.ROGUE,
			new UnityEngine.Vector2(0.58f, 0f)
		},
		{
			TAG_CLASS.SHAMAN,
			new UnityEngine.Vector2(0.774f, 0f)
		},
		{
			TAG_CLASS.WARLOCK,
			new UnityEngine.Vector2(0f, -0.2f)
		},
		{
			TAG_CLASS.WARRIOR,
			new UnityEngine.Vector2(0.205f, -0.2f)
		},
		{
			TAG_CLASS.DRUID,
			new UnityEngine.Vector2(0.392f, -0.2f)
		},
		{
			TAG_CLASS.HUNTER,
			new UnityEngine.Vector2(0.58f, -0.2f)
		},
		{
			TAG_CLASS.NEUTRAL,
			new UnityEngine.Vector2(0.774f, -0.2f)
		},
		{
			TAG_CLASS.WHIZBANG,
			new UnityEngine.Vector2(0f, -0.395f)
		},
		{
			TAG_CLASS.DEMONHUNTER,
			new UnityEngine.Vector2(0.205f, -0.4f)
		}
	};

	private static readonly Map<TAG_CLASS, Color> s_classColors = new Map<TAG_CLASS, Color>
	{
		{
			TAG_CLASS.MAGE,
			new Color(11f / 85f, 4f / 15f, 33f / 85f)
		},
		{
			TAG_CLASS.PALADIN,
			new Color(112f / 255f, 0.294117659f, 23f / 255f)
		},
		{
			TAG_CLASS.PRIEST,
			new Color(133f / 255f, 133f / 255f, 133f / 255f)
		},
		{
			TAG_CLASS.ROGUE,
			new Color(23f / 255f, 19f / 255f, 19f / 255f)
		},
		{
			TAG_CLASS.SHAMAN,
			new Color(11f / 85f, 44f / 255f, 19f / 51f)
		},
		{
			TAG_CLASS.WARLOCK,
			new Color(18f / 85f, 28f / 255f, 24f / 85f)
		},
		{
			TAG_CLASS.WARRIOR,
			new Color(14f / 51f, 13f / 255f, 7f / 85f)
		},
		{
			TAG_CLASS.DRUID,
			new Color(59f / 255f, 41f / 255f, 22f / 255f)
		},
		{
			TAG_CLASS.HUNTER,
			new Color(19f / 85f, 118f / 255f, 0.1764706f)
		},
		{
			TAG_CLASS.NEUTRAL,
			new Color(0f, 0f, 0f)
		},
		{
			TAG_CLASS.WHIZBANG,
			new Color(48f / 85f, 77f / 255f, 137f / 255f)
		},
		{
			TAG_CLASS.DEMONHUNTER,
			new Color(23f / 255f, 58f / 255f, 10f / 51f)
		}
	};

	public static TAG_CLASS[] CLASS_TAB_ORDER = new TAG_CLASS[11]
	{
		TAG_CLASS.DEMONHUNTER,
		TAG_CLASS.DRUID,
		TAG_CLASS.HUNTER,
		TAG_CLASS.MAGE,
		TAG_CLASS.PALADIN,
		TAG_CLASS.PRIEST,
		TAG_CLASS.ROGUE,
		TAG_CLASS.SHAMAN,
		TAG_CLASS.WARLOCK,
		TAG_CLASS.WARRIOR,
		TAG_CLASS.NEUTRAL
	};

	public CollectionClassTab m_heroSkinsTab;

	public CollectionClassTab m_cardBacksTab;

	public CollectionClassTab m_coinsTab;

	public ClassFilterHeaderButton m_classFilterHeader;

	public CollectionClassTab m_deckTemplateTab;

	[CustomEditField(Sections = "Deck Template", T = EditType.GAME_OBJECT)]
	public string m_deckTemplatePickerPrefab;

	public int m_numPlageFlipsBeforeStopShowingArrows;

	private static CollectionUtils.ViewMode[] TAG_ORDERING = new CollectionUtils.ViewMode[4]
	{
		CollectionUtils.ViewMode.CARDS,
		CollectionUtils.ViewMode.COINS,
		CollectionUtils.ViewMode.CARD_BACKS,
		CollectionUtils.ViewMode.HERO_SKINS
	};

	public static readonly float SELECT_TAB_ANIM_TIME = 0.2f;

	private static readonly Vector3 CLASS_TAB_LOCAL_EULERS = new Vector3(0f, 180f, 0f);

	private static readonly float HIDDEN_TAB_LOCAL_Z_POS = -0.42f;

	private static readonly string ANIMATE_TABS_COROUTINE_NAME = "AnimateTabs";

	private static readonly int NUM_PAGE_FLIPS_BEFORE_SET_FILTER_TUTORIAL = 3;

	private static readonly int NUM_PAGE_FLIPS_UNTIL_UNLOAD_UNUSED_ASSETS = 15;

	private static readonly string SELECT_TAB_COROUTINE_NAME = "SelectTabWhenReady";

	private static Map<TAG_CLASS, int> CLASS_TO_TAB_IDX = null;

	private List<CollectionClassTab> m_classTabs = new List<CollectionClassTab>();

	private CollectibleCard m_lastCardAnchor;

	private float m_deselectedClassTabHalfWidth;

	private bool m_initializedTabPositions;

	private MassDisenchant m_massDisenchant;

	private DeckTemplatePicker m_deckTemplatePicker;

	private CollectibleCardClassFilter m_cardsCollection = new CollectibleCardClassFilter();

	private CollectibleCardHeroesFilter m_heroesCollection = new CollectibleCardHeroesFilter();

	private Vector3 m_heroSkinsTabPos;

	private Vector3 m_cardBacksTabPos;

	private Vector3 m_coinsTabPos;

	private bool m_hideNonDeckTemplateTabs;

	private int m_numPageFlipsThisSession;

	private Coroutine m_turnPageCoroutine;

	protected TAG_CLASS m_currentClassContext;

	public bool IsManaCostFilterActive => m_cardsCollection.IsManaCostFilterActive;

	public static Color ColorForClass(TAG_CLASS tagClass)
	{
		return s_classColors[tagClass];
	}

	protected override void Awake()
	{
		base.Awake();
		if (CLASS_TO_TAB_IDX == null)
		{
			CLASS_TO_TAB_IDX = new Map<TAG_CLASS, int>();
			for (int i = 0; i < CLASS_TAB_ORDER.Length; i++)
			{
				CLASS_TO_TAB_IDX.Add(CLASS_TAB_ORDER[i], i);
			}
		}
		m_cardsCollection.Init(CLASS_TAB_ORDER, CollectiblePageDisplay.GetMaxCardsPerPage(CollectionUtils.ViewMode.CARDS));
		m_heroesCollection.Init(CollectiblePageDisplay.GetMaxCardsPerPage(CollectionUtils.ViewMode.HERO_SKINS));
		UpdateFilteredHeroes();
		UpdateFilteredCards();
		if ((bool)m_massDisenchant)
		{
			m_massDisenchant.Hide();
		}
		CollectionManager collectionManager = CollectionManager.Get();
		CollectionManagerDisplay collectionManagerDisplay = ((collectionManager != null) ? (collectionManager.GetCollectibleDisplay() as CollectionManagerDisplay) : null);
		collectionManager?.RegisterFavoriteHeroChangedListener(OnFavoriteHeroChanged);
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.RegisterSwitchViewModeListener(OnCollectionManagerViewModeChanged);
		}
		NetCache.Get().FavoriteCardBackChanged += OnFavoriteCardBackChanged;
		NetCache.Get().FavoriteCoinChanged += OnFavoriteCoinChanged;
	}

	private void OnDestroy()
	{
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().RemoveSwitchViewModeListener(OnCollectionManagerViewModeChanged);
		}
		CollectionManager.Get()?.RemoveFavoriteHeroChangedListener(OnFavoriteHeroChanged);
		if (NetCache.Get() != null)
		{
			NetCache.Get().FavoriteCardBackChanged -= OnFavoriteCardBackChanged;
			NetCache.Get().FavoriteCoinChanged -= OnFavoriteCoinChanged;
		}
	}

	protected override void Update()
	{
		base.Update();
		UpdateMouseWheel();
	}

	public void Exit()
	{
		CollectionPageDisplay currentCollectionPage = GetCurrentCollectionPage();
		if (!(currentCollectionPage == null))
		{
			currentCollectionPage.MarkAllShownCardsSeen();
		}
	}

	public bool HideNonDeckTemplateTabs(bool hide, bool updateTabs = false)
	{
		if (m_hideNonDeckTemplateTabs == hide)
		{
			return false;
		}
		m_hideNonDeckTemplateTabs = hide;
		if (updateTabs)
		{
			UpdateVisibleTabs();
		}
		return true;
	}

	public bool IsNonDeckTemplateTabsHidden()
	{
		return m_hideNonDeckTemplateTabs;
	}

	public void OnCollectionLoaded()
	{
		ShowOnlyCardsIOwn();
	}

	public void UpdateFiltersForDeck(CollectionDeck deck, TAG_CLASS deckClass, bool skipPageTurn, DelOnPageTransitionComplete callback = null, object callbackData = null)
	{
		m_skipNextPageTurn = skipPageTurn;
		bool flag = false;
		bool flag2 = false;
		if (deck != null && deck.GetRuleset() != null)
		{
			DeckRuleset ruleset = deck.GetRuleset();
			if (ruleset.EntityInDeckIgnoresRuleset(deck))
			{
				flag = true;
			}
			else
			{
				IEnumerable<DeckRule> source = ruleset.Rules.Where((DeckRule r) => r.Type == DeckRule.RuleType.IS_CLASS_CARD_OR_NEUTRAL);
				if (source.Any((DeckRule r) => r.RuleIsNot))
				{
					flag2 = true;
				}
				else if (!source.Any())
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			m_cardsCollection.FilterTheseClasses(null);
		}
		else if (flag2)
		{
			m_cardsCollection.FilterTheseClasses(CLASS_TAB_ORDER.Where((TAG_CLASS tag) => tag != deckClass).ToArray());
		}
		else
		{
			m_cardsCollection.FilterTheseClasses(deckClass, TAG_CLASS.NEUTRAL);
		}
		m_heroesCollection.FilterTheseClasses(deckClass);
		m_heroesCollection.FilterOnlyOwned(owned: true);
		UpdateFilteredCards();
		UpdateFilteredHeroes();
		UpdateVisibleTabs();
		bool flag3 = true;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (viewMode == CollectionUtils.ViewMode.DECK_TEMPLATE || viewMode == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			flag3 = false;
		}
		if (flag3)
		{
			switch (viewMode)
			{
			case CollectionUtils.ViewMode.CARDS:
				JumpToCollectionClassPage(deckClass, callback, callbackData);
				break;
			case CollectionUtils.ViewMode.HERO_SKINS:
			case CollectionUtils.ViewMode.CARD_BACKS:
				m_currentPageNum = 1;
				TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, callback, callbackData);
				break;
			}
		}
	}

	public bool JumpToPageWithCard(string cardID, TAG_PREMIUM premium)
	{
		return JumpToPageWithCard(cardID, premium, null, null);
	}

	public bool JumpToPageWithCard(string cardID, TAG_PREMIUM premium, DelOnPageTransitionComplete callback, object callbackData)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		TAG_CLASS classContext = TAG_CLASS.INVALID;
		if (editedDeck != null)
		{
			classContext = editedDeck.GetClass();
		}
		if (m_cardsCollection.GetPageContentsForCard(cardID, premium, out var collectionPage, classContext).Count == 0)
		{
			return false;
		}
		if (m_currentPageNum == collectionPage)
		{
			return false;
		}
		FlipToPage(collectionPage, callback, callbackData);
		return true;
	}

	private void RemoveAllClassFilters()
	{
		RemoveAllClassFilters(null, null);
	}

	private void RemoveAllClassFilters(DelOnPageTransitionComplete callback, object callbackData)
	{
		m_cardsCollection.FilterTheseClasses(null);
		m_heroesCollection.FilterTheseClasses(null);
		m_heroesCollection.FilterOnlyOwned(owned: false);
		UpdateFilteredCards();
		UpdateFilteredHeroes();
		PageTransitionType transitionType = ((CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS) ? PageTransitionType.SINGLE_PAGE_LEFT : PageTransitionType.NONE);
		TransitionPageWhenReady(transitionType, useCurrentPageNum: false, callback, callbackData);
	}

	public void FilterByManaCost(int cost, bool transitionPage = true)
	{
		if (cost == -1)
		{
			m_cardsCollection.FilterManaCost(null);
		}
		else
		{
			m_cardsCollection.FilterManaCost(cost);
		}
		UpdateFilteredCards();
		if (transitionPage)
		{
			TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, null, null);
		}
	}

	public void FilterByCardSets(List<TAG_CARD_SET> cardSets, bool transitionPage = true)
	{
		FilterByCardSets(cardSets, null, null, transitionPage);
	}

	public void FilterByCardSets(List<TAG_CARD_SET> cardSets, DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		TAG_CARD_SET[] cardSets2 = null;
		if (cardSets != null && cardSets.Count > 0)
		{
			cardSets2 = cardSets.ToArray();
		}
		m_cardsCollection.ClearOutFiltersFromSetFilterDropdown();
		m_cardsCollection.FilterTheseCardSets(cardSets2);
		UpdateFilteredCards();
		if (transitionPage)
		{
			PageTransitionType transitionType = ((!SceneMgr.Get().IsTransitioning()) ? PageTransitionType.SINGLE_PAGE_RIGHT : PageTransitionType.NONE);
			TransitionPageWhenReady(transitionType, useCurrentPageNum: false, callback, callbackData);
		}
	}

	public void FilterBySpecificCards(List<int> specificCards)
	{
		m_cardsCollection.ClearOutFiltersFromSetFilterDropdown();
		m_cardsCollection.FilterSpecificCards(specificCards);
		UpdateFilteredCards();
		PageTransitionType transitionType = ((!SceneMgr.Get().IsTransitioning()) ? PageTransitionType.SINGLE_PAGE_RIGHT : PageTransitionType.NONE);
		TransitionPageWhenReady(transitionType, useCurrentPageNum: false, null, null);
	}

	public bool CardSetFilterIncludesWild()
	{
		return m_cardsCollection.CardSetFilterIncludesWild();
	}

	public bool CardSetFilterIsClassic()
	{
		return m_cardsCollection.CardSetFilterIsClassicSet();
	}

	public void ChangeSearchTextFilter(string newSearchText, bool transitionPage = true)
	{
		ChangeSearchTextFilter(newSearchText, null, null, transitionPage);
	}

	public void ChangeSearchTextFilter(string newSearchText, DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		m_cardsCollection.FilterSearchText(newSearchText);
		m_heroesCollection.FilterSearchText(newSearchText);
		CardBackManager.Get().SetSearchText(newSearchText);
		UpdateFilteredCards();
		UpdateFilteredHeroes();
		if (transitionPage)
		{
			TransitionPageWhenReady(PageTransitionType.MANY_PAGE_LEFT, useCurrentPageNum: false, callback, callbackData);
		}
	}

	public void RemoveSearchTextFilter()
	{
		RemoveSearchTextFilter(null, null);
	}

	public void RemoveSearchTextFilter(DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		m_cardsCollection.FilterSearchText(null);
		m_heroesCollection.FilterSearchText(null);
		CardBackManager.Get().SetSearchText(null);
		UpdateFilteredCards();
		UpdateFilteredHeroes();
		if (transitionPage)
		{
			TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, callback, callbackData);
		}
	}

	public void ShowOnlyCardsIOwn()
	{
		ShowOnlyCardsIOwn(null, null);
	}

	public void ShowOnlyCardsIOwn(DelOnPageTransitionComplete callback, object callbackData)
	{
		m_cardsCollection.FilterOnlyOwned(owned: true);
		m_cardsCollection.FilterByMask(null);
		m_cardsCollection.FilterOnlyCraftable(onlyCraftable: false);
		m_cardsCollection.FilterOnlyUncraftable(onlyUncraftable: false, null);
		UpdateFilteredCards();
		TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, callback, callbackData);
	}

	public void ShowCardsNotOwned(bool includePremiums)
	{
		ShowCardsNotOwned(includePremiums, null, null);
	}

	public void ShowCardsNotOwned(bool includePremiums, DelOnPageTransitionComplete callback, object callbackData)
	{
		m_cardsCollection.FilterOnlyOwned(owned: false);
		m_cardsCollection.FilterByMask(null);
		UpdateFilteredCards();
		TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, callback, callbackData);
	}

	public void ShowCraftableCardsOnly(bool showCraftableCardsOnly)
	{
		ShowCraftableCardsOnly(showCraftableCardsOnly, null, null);
	}

	public void ShowCraftableCardsOnly(bool showCraftableCardsOnly, DelOnPageTransitionComplete callback, object callbackData)
	{
		m_cardsCollection.FilterOnlyCraftable(showCraftableCardsOnly);
		UpdateFilteredCards();
		TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, callback, callbackData);
	}

	public void ShowCraftingModeCards(bool showUncraftableCardsOnly, bool showGolden, bool showDiamond, bool updatePage = true, bool toggleChanged = false)
	{
		ShowCraftingModeCards(showUncraftableCardsOnly, showGolden, showDiamond, null, null, updatePage, toggleChanged);
	}

	public void ShowCraftingModeCards(bool showUncraftableCardsOnly, bool showGolden, bool showDiamond, DelOnPageTransitionComplete callback, object callbackData, bool updatePage = true, bool toggleChanged = false)
	{
		List<CollectibleCardFilter.FilterMask> list = new List<CollectibleCardFilter.FilterMask>
		{
			CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.OWNED,
			CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.UNOWNED
		};
		CollectibleCardFilter.FilterMask? premiums = null;
		if (showUncraftableCardsOnly && (showGolden || showDiamond))
		{
			premiums = ((showGolden && showDiamond) ? new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL) : ((!showGolden) ? new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN) : new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND)));
		}
		if (showUncraftableCardsOnly)
		{
			list[0] = CollectibleCardFilter.FilterMask.PREMIUM_ALL | CollectibleCardFilter.FilterMask.OWNED;
			list[1] = CollectibleCardFilter.FilterMask.PREMIUM_ALL | CollectibleCardFilter.FilterMask.UNOWNED;
		}
		else
		{
			if (showGolden)
			{
				list[0] = CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.OWNED;
				list[1] = CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.UNOWNED;
			}
			if (showDiamond)
			{
				CollectibleCardFilter.FilterMask filterMask = CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.OWNED;
				CollectibleCardFilter.FilterMask filterMask2 = CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.UNOWNED;
				list[1] = CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.UNOWNED;
				if (!showGolden)
				{
					list[0] = filterMask;
					list[1] = filterMask2;
				}
				else
				{
					list.Add(filterMask);
					list.Add(filterMask2);
				}
			}
		}
		m_cardsCollection.FilterByMask(list);
		m_cardsCollection.FilterOnlyOwned(owned: false);
		m_cardsCollection.FilterOnlyUncraftable(showUncraftableCardsOnly, premiums);
		m_cardsCollection.FilterLeagueBannedCardsSubset(RankMgr.Get().GetBannedCardsInCurrentLeague());
		UpdateFilteredCards();
		PageTransitionType transitionType = (toggleChanged ? PageTransitionType.MANY_PAGE_LEFT : PageTransitionType.NONE);
		if (toggleChanged)
		{
			m_lastCardAnchor = null;
		}
		if (updatePage)
		{
			TransitionPageWhenReady(transitionType, useCurrentPageNum: false, callback, callbackData);
		}
	}

	public void UpdateCurrentPageCardLocks(bool playSound)
	{
		GetCurrentCollectionPage().UpdateCurrentPageCardLocks(playSound);
	}

	public void UpdateClassTabNewCardCounts()
	{
		foreach (CollectionClassTab classTab in m_classTabs)
		{
			TAG_CLASS @class = classTab.GetClass();
			int numNewItems = ((classTab.m_tabViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE) ? GetNumNewCardsForClass(@class) : 0);
			classTab.UpdateNewItemCount(numNewItems);
		}
	}

	public int GetNumNewCardsForClass(TAG_CLASS tagClass)
	{
		return m_cardsCollection.GetNumNewCardsForClass(tagClass);
	}

	public void NotifyOfCollectionChanged()
	{
		UpdateMassDisenchant();
	}

	public void OnDoneEditingDeck()
	{
		RemoveAllClassFilters();
		UpdateCraftingModeButtonDustBottleVisibility();
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"));
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"));
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"));
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"));
		CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
	}

	public void UpdateCraftingModeButtonDustBottleVisibility()
	{
		bool show = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS && CollectionManager.Get().GetCardsToDisenchantCount() > 0;
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.m_craftingModeButton.ShowDustBottle(show);
		}
	}

	public int GetMassDisenchantAmount()
	{
		return CollectionManager.Get().GetCardsToDisenchantCount();
	}

	public void RefreshCurrentPageContents()
	{
		RefreshCurrentPageContents(PageTransitionType.NONE, null, null);
	}

	public void RefreshCurrentPageContents(PageTransitionType transition)
	{
		RefreshCurrentPageContents(transition, null, null);
	}

	public void RefreshCurrentPageContents(DelOnPageTransitionComplete callback, object callbackData)
	{
		RefreshCurrentPageContents(PageTransitionType.NONE, null, null);
	}

	public void RefreshCurrentPageContents(PageTransitionType transition, DelOnPageTransitionComplete callback, object callbackData)
	{
		UpdateFilteredCards();
		TransitionPageWhenReady(transition, useCurrentPageNum: true, callback, callbackData);
	}

	public CollectionCardVisual GetCardVisual(string cardID, TAG_PREMIUM premium)
	{
		return GetCurrentCollectionPage().GetCardVisual(cardID, premium);
	}

	public void LoadMassDisenchantScreen()
	{
		if (!(m_massDisenchant != null))
		{
			GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MassDisenchant.prefab:0bfb8a7db15d748b291be3096753ca24");
			m_massDisenchant = gameObject.GetComponent<MassDisenchant>();
			m_massDisenchant.Hide();
		}
	}

	public void OnCraftingTrayHidden(bool showingMassDisenchant)
	{
		m_cardsCollection.FilterOnlyCraftable(onlyCraftable: false);
		m_cardsCollection.FilterOnlyUncraftable(onlyUncraftable: false, null);
		m_cardsCollection.FilterByMask(null);
		m_cardsCollection.FilterOnlyOwned(owned: true);
		m_cardsCollection.FilterLeagueBannedCardsSubset(null);
		UpdateFilteredCards();
		TransitionPageWhenReady(showingMassDisenchant ? PageTransitionType.MANY_PAGE_LEFT : PageTransitionType.NONE, useCurrentPageNum: false, null, null);
	}

	public bool HasClassCardsAvailable(TAG_CLASS classTag)
	{
		return m_cardsCollection.GetNumPagesForClass(classTag) > 0;
	}

	protected override bool CanUserTurnPages()
	{
		if (CraftingManager.GetIsInCraftingMode())
		{
			return false;
		}
		if (SceneMgr.Get().IsInDuelsMode() && !PvPDungeonRunScene.IsEditingDeck())
		{
			return false;
		}
		if (CardBackInfoManager.IsLoadedAndShowingPreview())
		{
			return false;
		}
		if (HeroSkinInfoManager.IsLoadedAndShowingPreview())
		{
			return false;
		}
		return base.CanUserTurnPages();
	}

	private CollectionPageDisplay GetCurrentCollectionPage()
	{
		return PageAsCollectionPage(GetCurrentPage());
	}

	private CollectionPageDisplay PageAsCollectionPage(BookPageDisplay page)
	{
		if (!(page is CollectionPageDisplay))
		{
			Log.CollectionManager.PrintError("Page in CollectionPageManager is not a CollectionPageDisplay!  This should not happen!");
		}
		return page as CollectionPageDisplay;
	}

	protected override bool ShouldShowTab(BookTab tab)
	{
		if (!m_initializedTabPositions)
		{
			return true;
		}
		if (m_hideNonDeckTemplateTabs)
		{
			return tab.m_tabViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE;
		}
		if (tab.m_tabViewMode == CollectionUtils.ViewMode.CARDS)
		{
			CollectionClassTab collectionClassTab = tab as CollectionClassTab;
			if (collectionClassTab == null)
			{
				Log.CollectionManager.PrintError("CollectionPageManager.ShouldShowTab passed a non-CollectionClassTab object.");
				return false;
			}
			return HasClassCardsAvailable(collectionClassTab.GetClass());
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		bool flag = editedDeck != null;
		if (tab.m_tabViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			if (flag)
			{
				return !SceneMgr.Get().IsInTavernBrawlMode();
			}
			return false;
		}
		if (flag)
		{
			switch (tab.m_tabViewMode)
			{
			case CollectionUtils.ViewMode.HERO_SKINS:
				if (editedDeck.HasUIHeroOverride() || CollectionManager.Get().GetBestHeroesIOwn(editedDeck.GetClass()).Count <= 1 || SceneMgr.Get().IsInDuelsMode())
				{
					return false;
				}
				break;
			case CollectionUtils.ViewMode.CARD_BACKS:
			{
				HashSet<int> cardBacksOwned = CardBackManager.Get().GetCardBacksOwned();
				if (cardBacksOwned != null && cardBacksOwned.Count <= 1)
				{
					return false;
				}
				break;
			}
			case CollectionUtils.ViewMode.COINS:
				return false;
			}
		}
		return true;
	}

	protected override void SetUpBookTabs()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		bool receiveReleaseWithoutMouseDown = UniversalInputManager.Get().IsTouchMode();
		if (m_deckTemplateTab != null && m_deckTemplateTab.gameObject.activeSelf)
		{
			m_allTabs.Add(m_deckTemplateTab);
			m_classTabs.Add(m_deckTemplateTab);
			m_deckTemplateTab.AddEventListener(UIEventType.RELEASE, OnDeckTemplateTabPressed);
			m_deckTemplateTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver);
			m_deckTemplateTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut);
			m_deckTemplateTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver_Touch);
			m_deckTemplateTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut_Touch);
			m_deckTemplateTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			m_tabVisibility[m_deckTemplateTab] = true;
		}
		for (int i = 0; i < CLASS_TAB_ORDER.Length; i++)
		{
			TAG_CLASS classTag = CLASS_TAB_ORDER[i];
			CollectionClassTab collectionClassTab = (CollectionClassTab)GameUtils.Instantiate(m_classTabPrefab, m_classTabContainer);
			collectionClassTab.Init(classTag);
			collectionClassTab.transform.localScale = collectionClassTab.m_DeselectedLocalScale;
			collectionClassTab.transform.localEulerAngles = CLASS_TAB_LOCAL_EULERS;
			collectionClassTab.AddEventListener(UIEventType.RELEASE, OnClassTabPressed);
			collectionClassTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver);
			collectionClassTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut);
			collectionClassTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver_Touch);
			collectionClassTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut_Touch);
			collectionClassTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			collectionClassTab.gameObject.name = classTag.ToString();
			m_allTabs.Add(collectionClassTab);
			m_classTabs.Add(collectionClassTab);
			m_tabVisibility[collectionClassTab] = true;
			if (i <= 0)
			{
				m_deselectedClassTabHalfWidth = collectionClassTab.GetComponent<BoxCollider>().bounds.extents.x;
			}
		}
		if (m_heroSkinsTab != null)
		{
			m_heroSkinsTab.Init(TAG_CLASS.NEUTRAL);
			m_heroSkinsTab.AddEventListener(UIEventType.RELEASE, OnHeroSkinsTabPressed);
			m_heroSkinsTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver);
			m_heroSkinsTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut);
			m_heroSkinsTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver_Touch);
			m_heroSkinsTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut_Touch);
			m_heroSkinsTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			m_allTabs.Add(m_heroSkinsTab);
			m_tabVisibility[m_heroSkinsTab] = true;
			m_heroSkinsTabPos = m_heroSkinsTab.transform.localPosition;
		}
		if (m_cardBacksTab != null)
		{
			m_cardBacksTab.Init(TAG_CLASS.NEUTRAL);
			m_cardBacksTab.AddEventListener(UIEventType.RELEASE, OnCardBacksTabPressed);
			m_cardBacksTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver);
			m_cardBacksTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut);
			m_cardBacksTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver_Touch);
			m_cardBacksTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut_Touch);
			m_cardBacksTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			m_allTabs.Add(m_cardBacksTab);
			m_tabVisibility[m_cardBacksTab] = true;
			m_cardBacksTabPos = m_cardBacksTab.transform.localPosition;
		}
		if (m_coinsTab != null)
		{
			m_coinsTab.Init(TAG_CLASS.NEUTRAL);
			m_coinsTab.AddEventListener(UIEventType.RELEASE, OnCoinsTabPressed);
			m_coinsTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver);
			m_coinsTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut);
			m_coinsTab.AddEventListener(UIEventType.ROLLOVER, base.OnTabOver_Touch);
			m_coinsTab.AddEventListener(UIEventType.ROLLOUT, base.OnTabOut_Touch);
			m_coinsTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			m_allTabs.Add(m_coinsTab);
			m_tabVisibility[m_coinsTab] = true;
			m_coinsTabPos = m_coinsTab.transform.localPosition;
		}
		PositionBookTabs(animate: false);
		m_initializedTabPositions = true;
	}

	protected override void PositionBookTabs(bool animate)
	{
		Vector3 position = m_classTabContainer.transform.position;
		int num = CLASS_TAB_ORDER.Length;
		if (m_deckTemplateTab != null && m_deckTemplateTab.gameObject.activeSelf)
		{
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			CollectionClassTab collectionClassTab = m_classTabs[i];
			Vector3 vector;
			if (ShouldShowTab(collectionClassTab))
			{
				collectionClassTab.SetTargetVisibility(visible: true);
				position.x += m_spaceBetweenTabs;
				position.x += m_deselectedClassTabHalfWidth;
				vector = m_classTabContainer.transform.InverseTransformPoint(position);
				if (collectionClassTab == m_currentClassTab)
				{
					vector.y = collectionClassTab.m_SelectedLocalYPos;
				}
				position.x += m_deselectedClassTabHalfWidth;
			}
			else
			{
				collectionClassTab.SetTargetVisibility(visible: false);
				vector = collectionClassTab.transform.localPosition;
				vector.z = HIDDEN_TAB_LOCAL_Z_POS;
			}
			if (animate)
			{
				collectionClassTab.SetTargetLocalPosition(vector);
				continue;
			}
			collectionClassTab.SetIsVisible(collectionClassTab.ShouldBeVisible());
			collectionClassTab.transform.localPosition = vector;
		}
		bool showTab = ShouldShowTab(m_heroSkinsTab);
		PositionFixedTab(showTab, m_heroSkinsTab, m_heroSkinsTabPos, animate);
		bool showTab2 = ShouldShowTab(m_cardBacksTab);
		PositionFixedTab(showTab2, m_cardBacksTab, m_cardBacksTabPos, animate);
		bool showTab3 = ShouldShowTab(m_coinsTab);
		PositionFixedTab(showTab3, m_coinsTab, m_coinsTabPos, animate);
		if (animate)
		{
			StopCoroutine(ANIMATE_TABS_COROUTINE_NAME);
			StartCoroutine(ANIMATE_TABS_COROUTINE_NAME);
		}
	}

	private IEnumerator AnimateTabs()
	{
		bool playSounds = HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown();
		List<CollectionClassTab> list = new List<CollectionClassTab>();
		List<CollectionClassTab> tabsToShow = new List<CollectionClassTab>();
		List<CollectionClassTab> tabsToMove = new List<CollectionClassTab>();
		foreach (CollectionClassTab classTab in m_classTabs)
		{
			if (classTab.IsVisible() || classTab.ShouldBeVisible())
			{
				if (classTab.IsVisible() && classTab.ShouldBeVisible())
				{
					tabsToMove.Add(classTab);
				}
				else if (classTab.IsVisible() && !classTab.ShouldBeVisible())
				{
					list.Add(classTab);
				}
				else
				{
					tabsToShow.Add(classTab);
				}
			}
		}
		m_tabsAreAnimating = true;
		if (list.Count > 0)
		{
			foreach (CollectionClassTab tab in list)
			{
				if (playSounds)
				{
					SoundManager.Get().LoadAndPlay("class_tab_retract.prefab:da79957be76b10343999d6fa92a6a2f0", tab.gameObject);
				}
				yield return new WaitForSeconds(0.03f);
				tab.AnimateToTargetPosition(0.1f, iTween.EaseType.easeOutQuad);
			}
			yield return new WaitForSeconds(0.1f);
		}
		if (tabsToMove.Count > 0)
		{
			foreach (CollectionClassTab item in tabsToMove)
			{
				if (item.WillSlide() && playSounds)
				{
					SoundManager.Get().LoadAndPlay("class_tab_slides_across_top.prefab:04482bc6f531b76468ff92a5b4e979b6", item.gameObject);
				}
				item.AnimateToTargetPosition(0.25f, iTween.EaseType.easeOutQuad);
			}
			yield return new WaitForSeconds(0.25f);
		}
		if (tabsToShow.Count > 0)
		{
			foreach (CollectionClassTab item2 in tabsToShow)
			{
				if (playSounds)
				{
					SoundManager.Get().LoadAndPlay("class_tab_retract.prefab:da79957be76b10343999d6fa92a6a2f0", item2.gameObject);
				}
				item2.AnimateToTargetPosition(0.4f, iTween.EaseType.easeOutBounce);
			}
			yield return new WaitForSeconds(0.4f);
		}
		foreach (CollectionClassTab classTab2 in m_classTabs)
		{
			classTab2.SetIsVisible(classTab2.ShouldBeVisible());
		}
		m_tabsAreAnimating = false;
	}

	private void SetCurrentClassTab(TAG_CLASS? tabClass)
	{
		CollectionClassTab collectionClassTab = null;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_classFilterHeader.SetMode(viewMode, tabClass);
			return;
		}
		switch (viewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (tabClass.HasValue)
			{
				collectionClassTab = m_classTabs.Find((CollectionClassTab obj) => obj.GetClass() == tabClass.Value && obj.m_tabViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE);
			}
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
			collectionClassTab = m_heroSkinsTab;
			break;
		case CollectionUtils.ViewMode.CARD_BACKS:
			collectionClassTab = m_cardBacksTab;
			break;
		case CollectionUtils.ViewMode.COINS:
			collectionClassTab = m_coinsTab;
			break;
		default:
			collectionClassTab = null;
			break;
		}
		if (!(collectionClassTab == m_currentClassTab))
		{
			DeselectCurrentClassTab();
			m_currentClassTab = collectionClassTab;
			if (m_currentClassTab != null)
			{
				StopCoroutine(SELECT_TAB_COROUTINE_NAME);
				StartCoroutine(SELECT_TAB_COROUTINE_NAME, m_currentClassTab);
			}
		}
	}

	public void SetDeckRuleset(DeckRuleset deckRuleset, bool refresh = false)
	{
		m_cardsCollection.SetDeckRuleset(deckRuleset);
		if (refresh)
		{
			UpdateFilteredCards();
			TransitionPageWhenReady(PageTransitionType.NONE, useCurrentPageNum: false, null, null);
		}
	}

	private void OnClassTabPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			CollectionClassTab collectionClassTab = e.GetElement() as CollectionClassTab;
			if (!(collectionClassTab == null) && !(collectionClassTab == m_currentClassTab))
			{
				TAG_CLASS @class = collectionClassTab.GetClass();
				JumpToCollectionClassPage(@class);
			}
		}
	}

	private void OnDeckTemplateTabPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.DECK_TEMPLATE);
		}
	}

	private void OnHeroSkinsTabPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			CollectionClassTab collectionClassTab = e.GetElement() as CollectionClassTab;
			if (!(collectionClassTab == null) && !(collectionClassTab == m_currentClassTab) && ShouldShowTab(m_heroSkinsTab))
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.HERO_SKINS);
			}
		}
	}

	private void OnCardBacksTabPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			CollectionClassTab collectionClassTab = e.GetElement() as CollectionClassTab;
			if (!(collectionClassTab == null) && !(collectionClassTab == m_currentClassTab))
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARD_BACKS);
			}
		}
	}

	private void OnCoinsTabPressed(UIEvent e)
	{
		if (CanUserTurnPages())
		{
			CollectionClassTab collectionClassTab = e.GetElement() as CollectionClassTab;
			if (!(collectionClassTab == null) && !(collectionClassTab == m_currentClassTab) && ShouldShowTab(m_coinsTab))
			{
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.COINS);
			}
		}
	}

	public void UpdateMassDisenchant()
	{
		UpdateCraftingModeButtonDustBottleVisibility();
		if (CraftingTray.Get() != null)
		{
			CraftingTray.Get().SetMassDisenchantAmount();
		}
		if (MassDisenchant.Get() != null && CollectionManager.Get() != null)
		{
			MassDisenchant.Get().UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
		}
	}

	public void JumpToCollectionClassPage(TAG_CLASS pageClass)
	{
		JumpToCollectionClassPage(pageClass, null, null);
	}

	public void JumpToCollectionClassPage(TAG_CLASS pageClass, DelOnPageTransitionComplete callback, object callbackData)
	{
		CollectibleDisplay collectibleDisplay = CollectionManager.Get().GetCollectibleDisplay();
		if (collectibleDisplay != null && collectibleDisplay.GetViewMode() != 0)
		{
			collectibleDisplay.SetViewMode(CollectionUtils.ViewMode.CARDS, new CollectionUtils.ViewModeData
			{
				m_setPageByClass = pageClass
			});
		}
		else
		{
			int collectionPage = 0;
			m_cardsCollection.GetPageContentsForClass(pageClass, 1, calculateCollectionPage: true, out collectionPage);
			FlipToPage(collectionPage, callback, callbackData);
		}
	}

	protected override void AssembleEmptyPageUI(BookPageDisplay page)
	{
		base.AssembleEmptyPageUI(page);
		AssembleEmptyPageUI(PageAsCollectionPage(page), displayNoMatchesText: false);
	}

	private void AssembleEmptyPageUI(CollectionPageDisplay page, bool displayNoMatchesText)
	{
		page.SetClass(null);
		bool showHints = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		page.ShowNoMatchesFound(displayNoMatchesText, m_cardsCollection.FindCardsResult, showHints);
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS)
		{
			DeselectCurrentClassTab();
		}
		page.SetPageCountText(GameStrings.Get("GLUE_COLLECTION_EMPTY_PAGE"));
		page.SetPageTextColor();
	}

	private bool AssembleCollectionBasePage(TransitionReadyCallbackData transitionReadyCallbackData, bool emptyPage, FormatType formatType)
	{
		CollectionPageDisplay page = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		page.UpdateBasePage();
		page.SetPageType(formatType);
		page.ActivatePageCountText(active: true);
		if (emptyPage)
		{
			SetHasPreviousAndNextPages(hasPreviousPage: false, hasNextPage: false);
			AssembleEmptyPageUI(page, displayNoMatchesText: true);
			CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(null, delegate(List<CollectionCardActors> actorList, object data)
			{
				page.UpdateCollectionCards(actorList, CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
				TransitionPage(transitionReadyCallbackData);
			}, null);
			return true;
		}
		return false;
	}

	private void AssembleMassDisenchantPage(TransitionReadyCallbackData transitionReadyCallbackData, bool wildPage)
	{
		CollectionPageDisplay page = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		page.SetMassDisenchant();
		page.ActivatePageCountText(active: false);
		page.SetIsWild(wildPage);
		AssembleEmptyPageUI(page, displayNoMatchesText: false);
		SetHasPreviousAndNextPages(hasPreviousPage: false, hasNextPage: false);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(null, delegate
		{
			page.UpdatePageWithMassDisenchant();
			TransitionPage(transitionReadyCallbackData);
		}, null);
	}

	private void AssembleCardPage(TransitionReadyCallbackData transitionReadyCallbackData, List<CollectibleCard> cardsToDisplay, int totalNumPages)
	{
		bool flag = cardsToDisplay == null || cardsToDisplay.Count == 0;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} emptyPage={3} viewMode={4}", m_transitionPageId, m_pagesCurrentlyTurning, m_currentPageIsPageA, flag, viewMode);
		FormatType formatType = ((viewMode == CollectionUtils.ViewMode.HERO_SKINS) ? FormatType.FT_STANDARD : CollectionManager.Get().GetThemeShowingForCollectionPage());
		if (AssembleCollectionBasePage(transitionReadyCallbackData, flag, formatType))
		{
			return;
		}
		CollectionPageDisplay page = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		m_lastCardAnchor = cardsToDisplay[0];
		if (viewMode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			page.SetHeroSkins();
		}
		else if (viewMode == CollectionUtils.ViewMode.COINS)
		{
			page.SetCoins();
		}
		else
		{
			TAG_CLASS currentClassFromPage = m_cardsCollection.GetCurrentClassFromPage(m_currentPageNum);
			page.SetClass(currentClassFromPage);
			m_currentClassContext = currentClassFromPage;
		}
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", m_currentPageNum));
		page.SetPageTextColor();
		page.ShowNoMatchesFound(show: false);
		SetHasPreviousAndNextPages(m_currentPageNum > 1, m_currentPageNum < totalNumPages);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(cardsToDisplay, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdateCollectionCards(actorList, viewMode);
			TransitionPageNextFrame(transitionReadyCallbackData);
			if (m_deckTemplatePicker != null)
			{
				StartCoroutine(m_deckTemplatePicker.Show(show: false));
			}
		}, null);
	}

	private void AssembleCoinPage(TransitionReadyCallbackData transitionReadyCallbackData, List<CollectibleCard> cardsToDisplay)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} currentPageNum={3}", m_transitionPageId, m_pagesCurrentlyTurning, m_currentPageIsPageA, m_currentPageNum);
		int count = cardsToDisplay.Count;
		bool emptyPage = count == 0;
		if (AssembleCollectionBasePage(transitionReadyCallbackData, emptyPage, FormatType.FT_STANDARD))
		{
			return;
		}
		CollectionPageDisplay page = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		int maxCardsPerPage = CollectiblePageDisplay.GetMaxCardsPerPage();
		int num = count / maxCardsPerPage + ((count % maxCardsPerPage > 0) ? 1 : 0);
		m_currentPageNum = Mathf.Clamp(m_currentPageNum, 1, num);
		page.SetCoins();
		page.ShowNoMatchesFound(count == 0);
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", m_currentPageNum));
		SetHasPreviousAndNextPages(m_currentPageNum > 1, m_currentPageNum < num);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(cardsToDisplay, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdateCollectionCards(actorList, CollectionUtils.ViewMode.COINS);
			TransitionPage(transitionReadyCallbackData);
			if (m_deckTemplatePicker != null)
			{
				StartCoroutine(m_deckTemplatePicker.Show(show: false));
			}
		}, null);
	}

	private void TransitionPageNextFrame(TransitionReadyCallbackData transitionReadyCallbackData)
	{
		Processor.ScheduleCallback(0f, realTime: false, delegate
		{
			TransitionPage(transitionReadyCallbackData);
		});
	}

	private void AssembleDeckTemplatePage(TransitionReadyCallbackData transitionReadyCallbackData)
	{
		FormatType formatType = ((m_deckTemplatePicker != null && m_deckTemplatePicker.CurrentSelectedFormat != 0) ? m_deckTemplatePicker.CurrentSelectedFormat : FormatType.FT_STANDARD);
		if (AssembleCollectionBasePage(transitionReadyCallbackData, emptyPage: false, formatType))
		{
			return;
		}
		CollectionPageDisplay collectionPageDisplay = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		if (m_deckTemplatePicker == null && !string.IsNullOrEmpty(m_deckTemplatePickerPrefab))
		{
			m_deckTemplatePicker = GameUtils.LoadGameObjectWithComponent<DeckTemplatePicker>(m_deckTemplatePickerPrefab);
			if (m_deckTemplatePicker == null)
			{
				Debug.LogWarning("Failed to instantiate deck template picker prefab " + m_deckTemplatePickerPrefab);
				return;
			}
			m_deckTemplatePicker.RegisterOnTemplateDeckChosen(delegate
			{
				HideNonDeckTemplateTabs(hide: false, updateTabs: true);
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS);
			});
		}
		collectionPageDisplay.UpdateDeckTemplateHeader(m_deckTemplatePicker?.m_pageHeader, formatType);
		collectionPageDisplay.UpdateDeckTemplatePage(m_deckTemplatePicker);
		collectionPageDisplay.SetDeckTemplates();
		collectionPageDisplay.ShowNoMatchesFound(show: false);
		collectionPageDisplay.SetPageCountText(string.Empty);
		SetHasPreviousAndNextPages(hasPreviousPage: false, hasNextPage: false);
		UpdateDeckTemplate(m_deckTemplatePicker);
		TransitionPage(transitionReadyCallbackData);
	}

	public DeckTemplatePicker GetDeckTemplatePicker()
	{
		return m_deckTemplatePicker;
	}

	public void UpdateDeckTemplate(DeckTemplatePicker deckTemplatePicker)
	{
		if (deckTemplatePicker != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				deckTemplatePicker.SetDeckFormatAndClass(editedDeck.FormatType, editedDeck.GetClass());
			}
			StartCoroutine(deckTemplatePicker.Show(show: true));
		}
	}

	private void AssembleCardBackPage(TransitionReadyCallbackData transitionReadyCallbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} currentPageNum={3}", m_transitionPageId, m_pagesCurrentlyTurning, m_currentPageIsPageA, m_currentPageNum);
		int count = GetCurrentDeckTrayModeCardBackIds().Count;
		bool emptyPage = count == 0;
		if (AssembleCollectionBasePage(transitionReadyCallbackData, emptyPage, FormatType.FT_STANDARD))
		{
			return;
		}
		CollectionPageDisplay page = PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		int maxCardsPerPage = CollectiblePageDisplay.GetMaxCardsPerPage();
		int num = count / maxCardsPerPage + ((count % maxCardsPerPage > 0) ? 1 : 0);
		m_currentPageNum = Mathf.Clamp(m_currentPageNum, 1, num);
		page.SetCardBacks();
		page.ShowNoMatchesFound(count == 0);
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", m_currentPageNum));
		SetHasPreviousAndNextPages(m_currentPageNum > 1, m_currentPageNum < num);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (!(collectionManagerDisplay != null))
		{
			return;
		}
		collectionManagerDisplay.CollectionPageContentsChangedToCardBacks(m_currentPageNum, maxCardsPerPage, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdateCollectionCards(actorList, CollectionUtils.ViewMode.CARD_BACKS);
			foreach (CollectionCardActors actor in actorList)
			{
				CardBackManager.Get().UpdateCardBackWithInternalCardBack(actor.GetPreferredActor());
			}
			TransitionPage(transitionReadyCallbackData);
			if (m_deckTemplatePicker != null)
			{
				StartCoroutine(m_deckTemplatePicker.Show(show: false));
			}
		}, null, !CollectionManager.Get().IsInEditMode());
	}

	protected override void AssemblePage(TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		switch (CollectionManager.Get().GetCollectibleDisplay().GetViewMode())
		{
		case CollectionUtils.ViewMode.CARD_BACKS:
			AssembleCardBackPage(transitionReadyCallbackData);
			break;
		case CollectionUtils.ViewMode.DECK_TEMPLATE:
			AssembleDeckTemplatePage(transitionReadyCallbackData);
			break;
		case CollectionUtils.ViewMode.HERO_SKINS:
		{
			List<CollectibleCard> list2 = null;
			list2 = m_heroesCollection.GetHeroesContents(m_currentPageNum);
			AssembleCardPage(transitionReadyCallbackData, list2, m_heroesCollection.GetTotalNumPages());
			break;
		}
		case CollectionUtils.ViewMode.MASS_DISENCHANT:
		{
			bool wildPage = CollectionManager.Get().GetThemeShowing() == FormatType.FT_WILD;
			AssembleMassDisenchantPage(transitionReadyCallbackData, wildPage);
			break;
		}
		case CollectionUtils.ViewMode.CARDS:
		{
			List<CollectibleCard> list = null;
			if (useCurrentPageNum)
			{
				list = m_cardsCollection.GetPageContents(m_currentPageNum);
			}
			else if (m_lastCardAnchor == null)
			{
				m_currentPageNum = 1;
				list = m_cardsCollection.GetPageContents(m_currentPageNum);
			}
			else
			{
				list = m_cardsCollection.GetPageContentsForCard(m_lastCardAnchor.CardId, m_lastCardAnchor.PremiumType, out var collectionPage, m_currentClassContext);
				if (list.Count == 0)
				{
					list = m_cardsCollection.GetPageContentsForClass(m_currentClassContext, 1, calculateCollectionPage: true, out collectionPage);
				}
				if (list.Count == 0)
				{
					list = m_cardsCollection.GetPageContents(1);
					collectionPage = 1;
				}
				m_currentPageNum = ((list.Count != 0) ? collectionPage : 0);
			}
			if (list == null || list.Count == 0)
			{
				list = m_cardsCollection.GetFirstNonEmptyPage(out var collectionPage2);
				if (list.Count > 0)
				{
					m_currentPageNum = collectionPage2;
				}
			}
			AssembleCardPage(transitionReadyCallbackData, list, m_cardsCollection.GetTotalNumPages());
			break;
		}
		case CollectionUtils.ViewMode.COINS:
		{
			List<CollectibleCard> orderedCoinCards = CoinManager.Get().GetOrderedCoinCards();
			AssembleCoinPage(transitionReadyCallbackData, orderedCoinCards);
			break;
		}
		}
	}

	private void UpdateFilteredHeroes()
	{
		m_heroesCollection.UpdateResults();
	}

	private void UpdateFilteredCards()
	{
		m_cardsCollection.UpdateResults();
		UpdateClassTabNewCardCounts();
	}

	protected override void TransitionPage(object callbackData)
	{
		base.TransitionPage(callbackData);
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			DeselectCurrentClassTab();
		}
		else
		{
			SetCurrentClassTab(m_currentClassContext);
		}
	}

	protected override void OnPageTransitionRequested()
	{
		m_numPageFlipsThisSession++;
		int @int = Options.Get().GetInt(Option.PAGE_MOUSE_OVERS);
		int val = @int + 1;
		if (@int < m_numPlageFlipsBeforeStopShowingArrows)
		{
			Options.Get().SetInt(Option.PAGE_MOUSE_OVERS, val);
		}
		ShowSetFilterTutorialIfNeeded();
	}

	protected override void OnPageTurnComplete(object callbackData, int operationId)
	{
		if (m_numPageFlipsThisSession % NUM_PAGE_FLIPS_UNTIL_UNLOAD_UNUSED_ASSETS == 0)
		{
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.UnloadUnusedAssets();
			}
		}
		base.OnPageTurnComplete(callbackData, operationId);
	}

	private void ShowSetFilterTutorialIfNeeded()
	{
		if (!Options.Get().GetBool(Option.HAS_SEEN_SET_FILTER_TUTORIAL) && !CollectionManager.Get().IsInEditMode() && CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS && m_cardsCollection.CardSetFilterIsAllStandardSets())
		{
			CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
			if (!(collectionManagerDisplay == null) && !collectionManagerDisplay.IsShowingSetFilterTray() && CollectionManager.Get().AccountHasWildCards() && RankMgr.Get().WildCardsAllowedInCurrentLeague() && m_numPageFlipsThisSession >= NUM_PAGE_FLIPS_BEFORE_SET_FILTER_TUTORIAL)
			{
				collectionManagerDisplay.ShowSetFilterTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
				Options.Get().SetBool(Option.HAS_SEEN_SET_FILTER_TUTORIAL, val: true);
			}
		}
	}

	private void OnCollectionManagerViewModeChanged(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse)
	{
		if (!triggerResponse)
		{
			return;
		}
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} mode={2}-->{3} triggerResponse={4}", m_transitionPageId, m_pagesCurrentlyTurning, prevMode, mode, triggerResponse);
		UpdateCraftingModeButtonDustBottleVisibility();
		if (mode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			HideNonDeckTemplateTabs(hide: true);
		}
		if (mode != 0)
		{
			CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
		}
		m_currentPageNum = 1;
		if (userdata != null)
		{
			if (userdata.m_setPageByClass.HasValue)
			{
				m_cardsCollection.GetPageContentsForClass(userdata.m_setPageByClass.Value, 1, calculateCollectionPage: true, out m_currentPageNum);
			}
			else if (userdata.m_setPageByCard != null)
			{
				m_cardsCollection.GetPageContentsForCard(userdata.m_setPageByCard, userdata.m_setPageByPremium, out m_currentPageNum, m_currentClassContext);
			}
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < TAG_ORDERING.Length; i++)
		{
			if (prevMode == TAG_ORDERING[i])
			{
				num = i;
			}
			if (mode == TAG_ORDERING[i])
			{
				num2 = i;
			}
		}
		PageTransitionType transition = ((num2 - num >= 0) ? PageTransitionType.SINGLE_PAGE_RIGHT : PageTransitionType.SINGLE_PAGE_LEFT);
		DelOnPageTransitionComplete callback = null;
		object callbackData = null;
		if (userdata != null)
		{
			callback = userdata.m_pageTransitionCompleteCallback;
			callbackData = userdata.m_pageTransitionCompleteData;
		}
		if (m_turnPageCoroutine != null)
		{
			StopCoroutine(m_turnPageCoroutine);
		}
		CollectionDeckTray.Get().m_decksContent.UpdateDeckName();
		CollectionDeckTray.Get().UpdateDoneButtonText();
		m_turnPageCoroutine = StartCoroutine(ViewModeChangedWaitToTurnPage(transition, prevMode == CollectionUtils.ViewMode.DECK_TEMPLATE, callback, callbackData));
	}

	private IEnumerator ViewModeChangedWaitToTurnPage(PageTransitionType transition, bool hideDeckTemplateBottomPanel, DelOnPageTransitionComplete callback, object callbackData)
	{
		if (m_deckTemplatePicker != null && hideDeckTemplateBottomPanel)
		{
			CollectionManager.Get().GetCollectibleDisplay().m_inputBlocker.gameObject.SetActive(value: true);
			m_deckTemplatePicker.ShowBottomPanel(show: false);
			while (m_deckTemplatePicker.IsShowingBottomPanel())
			{
				yield return null;
			}
			yield return StartCoroutine(m_deckTemplatePicker.ShowPacks(show: false));
			CollectionManager.Get().GetCollectibleDisplay().m_inputBlocker.gameObject.SetActive(value: false);
		}
		TransitionPageWhenReady(transition, useCurrentPageNum: true, callback, callbackData);
	}

	public void OnFavoriteHeroChanged(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData)
	{
		GetCurrentCollectionPage().UpdateFavoriteHeroSkins(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		GetCurrentCollectionPage().UpdateFavoriteCardBack(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	public void OnFavoriteCoinChanged(int newFavoriteCoinId)
	{
		GetCurrentCollectionPage().UpdateFavoriteCoin(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	private HashSet<int> GetCurrentDeckTrayModeCardBackIds()
	{
		return CardBackManager.Get().GetCardBackIds(!CollectionManager.Get().IsInEditMode());
	}

	private void UpdateMouseWheel()
	{
		if (UniversalInputManager.Get().IsTouchMode() || !CanUserTurnPages())
		{
			return;
		}
		if (m_hasNextPage && Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (UniversalInputManager.Get().InputIsOver(GetCurrentCollectionPage().gameObject))
			{
				PageRight(null, null);
			}
		}
		else if (m_hasPreviousPage && Input.GetAxis("Mouse ScrollWheel") < 0f && UniversalInputManager.Get().InputIsOver(GetCurrentCollectionPage().gameObject))
		{
			PageLeft(null, null);
		}
	}
}
