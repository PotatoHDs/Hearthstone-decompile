using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using UnityEngine;

// Token: 0x02000115 RID: 277
[CustomEditClass]
public class CollectionPageManager : TabbedBookPageManager
{
	// Token: 0x060011FC RID: 4604 RVA: 0x00066A10 File Offset: 0x00064C10
	public static Color ColorForClass(TAG_CLASS tagClass)
	{
		return CollectionPageManager.s_classColors[tagClass];
	}

	// Token: 0x060011FD RID: 4605 RVA: 0x00066A20 File Offset: 0x00064C20
	protected override void Awake()
	{
		base.Awake();
		if (CollectionPageManager.CLASS_TO_TAB_IDX == null)
		{
			CollectionPageManager.CLASS_TO_TAB_IDX = new Map<TAG_CLASS, int>();
			for (int i = 0; i < CollectionPageManager.CLASS_TAB_ORDER.Length; i++)
			{
				CollectionPageManager.CLASS_TO_TAB_IDX.Add(CollectionPageManager.CLASS_TAB_ORDER[i], i);
			}
		}
		this.m_cardsCollection.Init(CollectionPageManager.CLASS_TAB_ORDER, CollectiblePageDisplay.GetMaxCardsPerPage(CollectionUtils.ViewMode.CARDS));
		this.m_heroesCollection.Init(CollectiblePageDisplay.GetMaxCardsPerPage(CollectionUtils.ViewMode.HERO_SKINS));
		this.UpdateFilteredHeroes();
		this.UpdateFilteredCards();
		if (this.m_massDisenchant)
		{
			this.m_massDisenchant.Hide();
		}
		CollectionManager collectionManager = CollectionManager.Get();
		CollectionManagerDisplay collectionManagerDisplay = (collectionManager != null) ? (collectionManager.GetCollectibleDisplay() as CollectionManagerDisplay) : null;
		if (collectionManager != null)
		{
			collectionManager.RegisterFavoriteHeroChangedListener(new CollectionManager.FavoriteHeroChangedCallback(this.OnFavoriteHeroChanged));
		}
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.RegisterSwitchViewModeListener(new CollectibleDisplay.OnSwitchViewMode(this.OnCollectionManagerViewModeChanged));
		}
		NetCache.Get().FavoriteCardBackChanged += this.OnFavoriteCardBackChanged;
		NetCache.Get().FavoriteCoinChanged += this.OnFavoriteCoinChanged;
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x00066B28 File Offset: 0x00064D28
	private void OnDestroy()
	{
		if (CollectionManager.Get().GetCollectibleDisplay() != null)
		{
			CollectionManager.Get().GetCollectibleDisplay().RemoveSwitchViewModeListener(new CollectibleDisplay.OnSwitchViewMode(this.OnCollectionManagerViewModeChanged));
		}
		CollectionManager collectionManager = CollectionManager.Get();
		if (collectionManager != null)
		{
			collectionManager.RemoveFavoriteHeroChangedListener(new CollectionManager.FavoriteHeroChangedCallback(this.OnFavoriteHeroChanged));
		}
		if (NetCache.Get() != null)
		{
			NetCache.Get().FavoriteCardBackChanged -= this.OnFavoriteCardBackChanged;
			NetCache.Get().FavoriteCoinChanged -= this.OnFavoriteCoinChanged;
		}
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x00066BB1 File Offset: 0x00064DB1
	protected override void Update()
	{
		base.Update();
		this.UpdateMouseWheel();
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x00066BC0 File Offset: 0x00064DC0
	public void Exit()
	{
		CollectionPageDisplay currentCollectionPage = this.GetCurrentCollectionPage();
		if (currentCollectionPage == null)
		{
			return;
		}
		currentCollectionPage.MarkAllShownCardsSeen();
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x00066BE4 File Offset: 0x00064DE4
	public bool HideNonDeckTemplateTabs(bool hide, bool updateTabs = false)
	{
		if (this.m_hideNonDeckTemplateTabs == hide)
		{
			return false;
		}
		this.m_hideNonDeckTemplateTabs = hide;
		if (updateTabs)
		{
			base.UpdateVisibleTabs();
		}
		return true;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x00066C02 File Offset: 0x00064E02
	public bool IsNonDeckTemplateTabsHidden()
	{
		return this.m_hideNonDeckTemplateTabs;
	}

	// Token: 0x06001203 RID: 4611 RVA: 0x00066C0A File Offset: 0x00064E0A
	public void OnCollectionLoaded()
	{
		this.ShowOnlyCardsIOwn();
	}

	// Token: 0x06001204 RID: 4612 RVA: 0x00066C14 File Offset: 0x00064E14
	public void UpdateFiltersForDeck(CollectionDeck deck, TAG_CLASS deckClass, bool skipPageTurn, BookPageManager.DelOnPageTransitionComplete callback = null, object callbackData = null)
	{
		this.m_skipNextPageTurn = skipPageTurn;
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
				IEnumerable<DeckRule> source = from r in ruleset.Rules
				where r.Type == DeckRule.RuleType.IS_CLASS_CARD_OR_NEUTRAL
				select r;
				if (source.Any((DeckRule r) => r.RuleIsNot))
				{
					flag2 = true;
				}
				else if (!source.Any<DeckRule>())
				{
					flag = true;
				}
			}
		}
		if (flag)
		{
			this.m_cardsCollection.FilterTheseClasses(null);
		}
		else if (flag2)
		{
			this.m_cardsCollection.FilterTheseClasses((from tag in CollectionPageManager.CLASS_TAB_ORDER
			where tag != deckClass
			select tag).ToArray<TAG_CLASS>());
		}
		else
		{
			this.m_cardsCollection.FilterTheseClasses(new TAG_CLASS[]
			{
				deckClass,
				TAG_CLASS.NEUTRAL
			});
		}
		this.m_heroesCollection.FilterTheseClasses(new TAG_CLASS[]
		{
			deckClass
		});
		this.m_heroesCollection.FilterOnlyOwned(true);
		this.UpdateFilteredCards();
		this.UpdateFilteredHeroes();
		base.UpdateVisibleTabs();
		bool flag3 = true;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (viewMode == CollectionUtils.ViewMode.DECK_TEMPLATE || viewMode == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			flag3 = false;
		}
		if (flag3)
		{
			if (viewMode == CollectionUtils.ViewMode.CARDS)
			{
				this.JumpToCollectionClassPage(deckClass, callback, callbackData);
				return;
			}
			if (viewMode == CollectionUtils.ViewMode.HERO_SKINS || viewMode == CollectionUtils.ViewMode.CARD_BACKS)
			{
				this.m_currentPageNum = 1;
				base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, callback, callbackData);
			}
		}
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x00066DA8 File Offset: 0x00064FA8
	public bool JumpToPageWithCard(string cardID, TAG_PREMIUM premium)
	{
		return this.JumpToPageWithCard(cardID, premium, null, null);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x00066DB4 File Offset: 0x00064FB4
	public bool JumpToPageWithCard(string cardID, TAG_PREMIUM premium, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		TAG_CLASS classContext = TAG_CLASS.INVALID;
		if (editedDeck != null)
		{
			classContext = editedDeck.GetClass();
		}
		int num;
		if (this.m_cardsCollection.GetPageContentsForCard(cardID, premium, out num, classContext).Count == 0)
		{
			return false;
		}
		if (this.m_currentPageNum == num)
		{
			return false;
		}
		base.FlipToPage(num, callback, callbackData);
		return true;
	}

	// Token: 0x06001207 RID: 4615 RVA: 0x00066E07 File Offset: 0x00065007
	private void RemoveAllClassFilters()
	{
		this.RemoveAllClassFilters(null, null);
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x00066E14 File Offset: 0x00065014
	private void RemoveAllClassFilters(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_cardsCollection.FilterTheseClasses(null);
		this.m_heroesCollection.FilterTheseClasses(null);
		this.m_heroesCollection.FilterOnlyOwned(false);
		this.UpdateFilteredCards();
		this.UpdateFilteredHeroes();
		BookPageManager.PageTransitionType transitionType = (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS) ? BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT : BookPageManager.PageTransitionType.NONE;
		base.TransitionPageWhenReady(transitionType, false, callback, callbackData);
	}

	// Token: 0x06001209 RID: 4617 RVA: 0x00066E74 File Offset: 0x00065074
	public void FilterByManaCost(int cost, bool transitionPage = true)
	{
		if (cost == -1)
		{
			this.m_cardsCollection.FilterManaCost(null);
		}
		else
		{
			this.m_cardsCollection.FilterManaCost(new int?(cost));
		}
		this.UpdateFilteredCards();
		if (transitionPage)
		{
			base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, null, null);
		}
	}

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x0600120A RID: 4618 RVA: 0x00066EBF File Offset: 0x000650BF
	public bool IsManaCostFilterActive
	{
		get
		{
			return this.m_cardsCollection.IsManaCostFilterActive;
		}
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x00066ECC File Offset: 0x000650CC
	public void FilterByCardSets(List<TAG_CARD_SET> cardSets, bool transitionPage = true)
	{
		this.FilterByCardSets(cardSets, null, null, transitionPage);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x00066ED8 File Offset: 0x000650D8
	public void FilterByCardSets(List<TAG_CARD_SET> cardSets, BookPageManager.DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		TAG_CARD_SET[] cardSets2 = null;
		if (cardSets != null && cardSets.Count > 0)
		{
			cardSets2 = cardSets.ToArray();
		}
		this.m_cardsCollection.ClearOutFiltersFromSetFilterDropdown();
		this.m_cardsCollection.FilterTheseCardSets(cardSets2);
		this.UpdateFilteredCards();
		if (transitionPage)
		{
			BookPageManager.PageTransitionType transitionType = SceneMgr.Get().IsTransitioning() ? BookPageManager.PageTransitionType.NONE : BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT;
			base.TransitionPageWhenReady(transitionType, false, callback, callbackData);
		}
	}

	// Token: 0x0600120D RID: 4621 RVA: 0x00066F38 File Offset: 0x00065138
	public void FilterBySpecificCards(List<int> specificCards)
	{
		this.m_cardsCollection.ClearOutFiltersFromSetFilterDropdown();
		this.m_cardsCollection.FilterSpecificCards(specificCards);
		this.UpdateFilteredCards();
		BookPageManager.PageTransitionType transitionType = SceneMgr.Get().IsTransitioning() ? BookPageManager.PageTransitionType.NONE : BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT;
		base.TransitionPageWhenReady(transitionType, false, null, null);
	}

	// Token: 0x0600120E RID: 4622 RVA: 0x00066F7D File Offset: 0x0006517D
	public bool CardSetFilterIncludesWild()
	{
		return this.m_cardsCollection.CardSetFilterIncludesWild();
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x00066F8A File Offset: 0x0006518A
	public bool CardSetFilterIsClassic()
	{
		return this.m_cardsCollection.CardSetFilterIsClassicSet();
	}

	// Token: 0x06001210 RID: 4624 RVA: 0x00066F97 File Offset: 0x00065197
	public void ChangeSearchTextFilter(string newSearchText, bool transitionPage = true)
	{
		this.ChangeSearchTextFilter(newSearchText, null, null, transitionPage);
	}

	// Token: 0x06001211 RID: 4625 RVA: 0x00066FA3 File Offset: 0x000651A3
	public void ChangeSearchTextFilter(string newSearchText, BookPageManager.DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		this.m_cardsCollection.FilterSearchText(newSearchText);
		this.m_heroesCollection.FilterSearchText(newSearchText);
		CardBackManager.Get().SetSearchText(newSearchText);
		this.UpdateFilteredCards();
		this.UpdateFilteredHeroes();
		if (transitionPage)
		{
			base.TransitionPageWhenReady(BookPageManager.PageTransitionType.MANY_PAGE_LEFT, false, callback, callbackData);
		}
	}

	// Token: 0x06001212 RID: 4626 RVA: 0x00066FE2 File Offset: 0x000651E2
	public void RemoveSearchTextFilter()
	{
		this.RemoveSearchTextFilter(null, null, true);
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x00066FED File Offset: 0x000651ED
	public void RemoveSearchTextFilter(BookPageManager.DelOnPageTransitionComplete callback, object callbackData, bool transitionPage = true)
	{
		this.m_cardsCollection.FilterSearchText(null);
		this.m_heroesCollection.FilterSearchText(null);
		CardBackManager.Get().SetSearchText(null);
		this.UpdateFilteredCards();
		this.UpdateFilteredHeroes();
		if (transitionPage)
		{
			base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, callback, callbackData);
		}
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x0006702B File Offset: 0x0006522B
	public void ShowOnlyCardsIOwn()
	{
		this.ShowOnlyCardsIOwn(null, null);
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x00067038 File Offset: 0x00065238
	public void ShowOnlyCardsIOwn(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_cardsCollection.FilterOnlyOwned(true);
		this.m_cardsCollection.FilterByMask(null);
		this.m_cardsCollection.FilterOnlyCraftable(false);
		this.m_cardsCollection.FilterOnlyUncraftable(false, null);
		this.UpdateFilteredCards();
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, callback, callbackData);
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x0006708E File Offset: 0x0006528E
	public void ShowCardsNotOwned(bool includePremiums)
	{
		this.ShowCardsNotOwned(includePremiums, null, null);
	}

	// Token: 0x06001217 RID: 4631 RVA: 0x00067099 File Offset: 0x00065299
	public void ShowCardsNotOwned(bool includePremiums, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_cardsCollection.FilterOnlyOwned(false);
		this.m_cardsCollection.FilterByMask(null);
		this.UpdateFilteredCards();
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, callback, callbackData);
	}

	// Token: 0x06001218 RID: 4632 RVA: 0x000670C3 File Offset: 0x000652C3
	public void ShowCraftableCardsOnly(bool showCraftableCardsOnly)
	{
		this.ShowCraftableCardsOnly(showCraftableCardsOnly, null, null);
	}

	// Token: 0x06001219 RID: 4633 RVA: 0x000670CE File Offset: 0x000652CE
	public void ShowCraftableCardsOnly(bool showCraftableCardsOnly, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.m_cardsCollection.FilterOnlyCraftable(showCraftableCardsOnly);
		this.UpdateFilteredCards();
		base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, callback, callbackData);
	}

	// Token: 0x0600121A RID: 4634 RVA: 0x000670EC File Offset: 0x000652EC
	public void ShowCraftingModeCards(bool showUncraftableCardsOnly, bool showGolden, bool showDiamond, bool updatePage = true, bool toggleChanged = false)
	{
		this.ShowCraftingModeCards(showUncraftableCardsOnly, showGolden, showDiamond, null, null, updatePage, toggleChanged);
	}

	// Token: 0x0600121B RID: 4635 RVA: 0x00067100 File Offset: 0x00065300
	public void ShowCraftingModeCards(bool showUncraftableCardsOnly, bool showGolden, bool showDiamond, BookPageManager.DelOnPageTransitionComplete callback, object callbackData, bool updatePage = true, bool toggleChanged = false)
	{
		List<CollectibleCardFilter.FilterMask> list = new List<CollectibleCardFilter.FilterMask>
		{
			CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.OWNED,
			CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.UNOWNED
		};
		CollectibleCardFilter.FilterMask? premiums = null;
		if (showUncraftableCardsOnly && (showGolden || showDiamond))
		{
			if (showGolden && showDiamond)
			{
				premiums = new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL);
			}
			else if (showGolden)
			{
				premiums = new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND);
			}
			else
			{
				premiums = new CollectibleCardFilter.FilterMask?(CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN);
			}
		}
		if (showUncraftableCardsOnly)
		{
			list[0] = (CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.OWNED);
			list[1] = (CollectibleCardFilter.FilterMask.PREMIUM_NORMAL | CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.UNOWNED);
		}
		else
		{
			if (showGolden)
			{
				list[0] = (CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.OWNED);
				list[1] = (CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN | CollectibleCardFilter.FilterMask.UNOWNED);
			}
			if (showDiamond)
			{
				CollectibleCardFilter.FilterMask filterMask = CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.OWNED;
				CollectibleCardFilter.FilterMask filterMask2 = CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.UNOWNED;
				list[1] = (CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND | CollectibleCardFilter.FilterMask.UNOWNED);
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
		this.m_cardsCollection.FilterByMask(list);
		this.m_cardsCollection.FilterOnlyOwned(false);
		this.m_cardsCollection.FilterOnlyUncraftable(showUncraftableCardsOnly, premiums);
		this.m_cardsCollection.FilterLeagueBannedCardsSubset(RankMgr.Get().GetBannedCardsInCurrentLeague());
		this.UpdateFilteredCards();
		BookPageManager.PageTransitionType transitionType = toggleChanged ? BookPageManager.PageTransitionType.MANY_PAGE_LEFT : BookPageManager.PageTransitionType.NONE;
		if (toggleChanged)
		{
			this.m_lastCardAnchor = null;
		}
		if (updatePage)
		{
			base.TransitionPageWhenReady(transitionType, false, callback, callbackData);
		}
	}

	// Token: 0x0600121C RID: 4636 RVA: 0x00067220 File Offset: 0x00065420
	public void UpdateCurrentPageCardLocks(bool playSound)
	{
		this.GetCurrentCollectionPage().UpdateCurrentPageCardLocks(playSound);
	}

	// Token: 0x0600121D RID: 4637 RVA: 0x00067230 File Offset: 0x00065430
	public void UpdateClassTabNewCardCounts()
	{
		foreach (CollectionClassTab collectionClassTab in this.m_classTabs)
		{
			TAG_CLASS @class = collectionClassTab.GetClass();
			int numNewItems = (collectionClassTab.m_tabViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE) ? 0 : this.GetNumNewCardsForClass(@class);
			collectionClassTab.UpdateNewItemCount(numNewItems);
		}
	}

	// Token: 0x0600121E RID: 4638 RVA: 0x0006729C File Offset: 0x0006549C
	public int GetNumNewCardsForClass(TAG_CLASS tagClass)
	{
		return this.m_cardsCollection.GetNumNewCardsForClass(tagClass);
	}

	// Token: 0x0600121F RID: 4639 RVA: 0x000672AA File Offset: 0x000654AA
	public void NotifyOfCollectionChanged()
	{
		this.UpdateMassDisenchant();
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000672B4 File Offset: 0x000654B4
	public void OnDoneEditingDeck()
	{
		this.RemoveAllClassFilters();
		this.UpdateCraftingModeButtonDustBottleVisibility();
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"), 0f);
		NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"), 0f);
		CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x00067340 File Offset: 0x00065540
	public void UpdateCraftingModeButtonDustBottleVisibility()
	{
		bool show = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS && CollectionManager.Get().GetCardsToDisenchantCount() > 0;
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.m_craftingModeButton.ShowDustBottle(show);
		}
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x00067397 File Offset: 0x00065597
	public int GetMassDisenchantAmount()
	{
		return CollectionManager.Get().GetCardsToDisenchantCount();
	}

	// Token: 0x06001223 RID: 4643 RVA: 0x000673A3 File Offset: 0x000655A3
	public void RefreshCurrentPageContents()
	{
		this.RefreshCurrentPageContents(BookPageManager.PageTransitionType.NONE, null, null);
	}

	// Token: 0x06001224 RID: 4644 RVA: 0x000673AE File Offset: 0x000655AE
	public void RefreshCurrentPageContents(BookPageManager.PageTransitionType transition)
	{
		this.RefreshCurrentPageContents(transition, null, null);
	}

	// Token: 0x06001225 RID: 4645 RVA: 0x000673A3 File Offset: 0x000655A3
	public void RefreshCurrentPageContents(BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.RefreshCurrentPageContents(BookPageManager.PageTransitionType.NONE, null, null);
	}

	// Token: 0x06001226 RID: 4646 RVA: 0x000673B9 File Offset: 0x000655B9
	public void RefreshCurrentPageContents(BookPageManager.PageTransitionType transition, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		this.UpdateFilteredCards();
		base.TransitionPageWhenReady(transition, true, callback, callbackData);
	}

	// Token: 0x06001227 RID: 4647 RVA: 0x000673CB File Offset: 0x000655CB
	public CollectionCardVisual GetCardVisual(string cardID, TAG_PREMIUM premium)
	{
		return this.GetCurrentCollectionPage().GetCardVisual(cardID, premium);
	}

	// Token: 0x06001228 RID: 4648 RVA: 0x000673DC File Offset: 0x000655DC
	public void LoadMassDisenchantScreen()
	{
		if (this.m_massDisenchant != null)
		{
			return;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab("MassDisenchant.prefab:0bfb8a7db15d748b291be3096753ca24", AssetLoadingOptions.None);
		this.m_massDisenchant = gameObject.GetComponent<MassDisenchant>();
		this.m_massDisenchant.Hide();
	}

	// Token: 0x06001229 RID: 4649 RVA: 0x00067428 File Offset: 0x00065628
	public void OnCraftingTrayHidden(bool showingMassDisenchant)
	{
		this.m_cardsCollection.FilterOnlyCraftable(false);
		this.m_cardsCollection.FilterOnlyUncraftable(false, null);
		this.m_cardsCollection.FilterByMask(null);
		this.m_cardsCollection.FilterOnlyOwned(true);
		this.m_cardsCollection.FilterLeagueBannedCardsSubset(null);
		this.UpdateFilteredCards();
		base.TransitionPageWhenReady(showingMassDisenchant ? BookPageManager.PageTransitionType.MANY_PAGE_LEFT : BookPageManager.PageTransitionType.NONE, false, null, null);
	}

	// Token: 0x0600122A RID: 4650 RVA: 0x00067490 File Offset: 0x00065690
	public bool HasClassCardsAvailable(TAG_CLASS classTag)
	{
		return this.m_cardsCollection.GetNumPagesForClass(classTag) > 0;
	}

	// Token: 0x0600122B RID: 4651 RVA: 0x000674A1 File Offset: 0x000656A1
	protected override bool CanUserTurnPages()
	{
		return !CraftingManager.GetIsInCraftingMode() && (!SceneMgr.Get().IsInDuelsMode() || PvPDungeonRunScene.IsEditingDeck()) && !CardBackInfoManager.IsLoadedAndShowingPreview() && !HeroSkinInfoManager.IsLoadedAndShowingPreview() && base.CanUserTurnPages();
	}

	// Token: 0x0600122C RID: 4652 RVA: 0x000674D9 File Offset: 0x000656D9
	private CollectionPageDisplay GetCurrentCollectionPage()
	{
		return this.PageAsCollectionPage(base.GetCurrentPage());
	}

	// Token: 0x0600122D RID: 4653 RVA: 0x000674E7 File Offset: 0x000656E7
	private CollectionPageDisplay PageAsCollectionPage(BookPageDisplay page)
	{
		if (!(page is CollectionPageDisplay))
		{
			Log.CollectionManager.PrintError("Page in CollectionPageManager is not a CollectionPageDisplay!  This should not happen!", Array.Empty<object>());
		}
		return page as CollectionPageDisplay;
	}

	// Token: 0x0600122E RID: 4654 RVA: 0x0006750C File Offset: 0x0006570C
	protected override bool ShouldShowTab(BookTab tab)
	{
		if (!this.m_initializedTabPositions)
		{
			return true;
		}
		if (this.m_hideNonDeckTemplateTabs)
		{
			return tab.m_tabViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE;
		}
		if (tab.m_tabViewMode == CollectionUtils.ViewMode.CARDS)
		{
			CollectionClassTab collectionClassTab = tab as CollectionClassTab;
			if (collectionClassTab == null)
			{
				Log.CollectionManager.PrintError("CollectionPageManager.ShouldShowTab passed a non-CollectionClassTab object.", Array.Empty<object>());
				return false;
			}
			return this.HasClassCardsAvailable(collectionClassTab.GetClass());
		}
		else
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			bool flag = editedDeck != null;
			if (tab.m_tabViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
			{
				return flag && !SceneMgr.Get().IsInTavernBrawlMode();
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
	}

	// Token: 0x0600122F RID: 4655 RVA: 0x00067614 File Offset: 0x00065814
	protected override void SetUpBookTabs()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		bool receiveReleaseWithoutMouseDown = UniversalInputManager.Get().IsTouchMode();
		if (this.m_deckTemplateTab != null && this.m_deckTemplateTab.gameObject.activeSelf)
		{
			this.m_allTabs.Add(this.m_deckTemplateTab);
			this.m_classTabs.Add(this.m_deckTemplateTab);
			this.m_deckTemplateTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnDeckTemplateTabPressed));
			this.m_deckTemplateTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver));
			this.m_deckTemplateTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut));
			this.m_deckTemplateTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver_Touch));
			this.m_deckTemplateTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut_Touch));
			this.m_deckTemplateTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			this.m_tabVisibility[this.m_deckTemplateTab] = true;
		}
		for (int i = 0; i < CollectionPageManager.CLASS_TAB_ORDER.Length; i++)
		{
			TAG_CLASS classTag = CollectionPageManager.CLASS_TAB_ORDER[i];
			CollectionClassTab collectionClassTab = (CollectionClassTab)GameUtils.Instantiate(this.m_classTabPrefab, this.m_classTabContainer, false);
			collectionClassTab.Init(classTag);
			collectionClassTab.transform.localScale = collectionClassTab.m_DeselectedLocalScale;
			collectionClassTab.transform.localEulerAngles = CollectionPageManager.CLASS_TAB_LOCAL_EULERS;
			collectionClassTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnClassTabPressed));
			collectionClassTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver));
			collectionClassTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut));
			collectionClassTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver_Touch));
			collectionClassTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut_Touch));
			collectionClassTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			collectionClassTab.gameObject.name = classTag.ToString();
			this.m_allTabs.Add(collectionClassTab);
			this.m_classTabs.Add(collectionClassTab);
			this.m_tabVisibility[collectionClassTab] = true;
			if (i <= 0)
			{
				this.m_deselectedClassTabHalfWidth = collectionClassTab.GetComponent<BoxCollider>().bounds.extents.x;
			}
		}
		if (this.m_heroSkinsTab != null)
		{
			this.m_heroSkinsTab.Init(TAG_CLASS.NEUTRAL);
			this.m_heroSkinsTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnHeroSkinsTabPressed));
			this.m_heroSkinsTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver));
			this.m_heroSkinsTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut));
			this.m_heroSkinsTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver_Touch));
			this.m_heroSkinsTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut_Touch));
			this.m_heroSkinsTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			this.m_allTabs.Add(this.m_heroSkinsTab);
			this.m_tabVisibility[this.m_heroSkinsTab] = true;
			this.m_heroSkinsTabPos = this.m_heroSkinsTab.transform.localPosition;
		}
		if (this.m_cardBacksTab != null)
		{
			this.m_cardBacksTab.Init(TAG_CLASS.NEUTRAL);
			this.m_cardBacksTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCardBacksTabPressed));
			this.m_cardBacksTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver));
			this.m_cardBacksTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut));
			this.m_cardBacksTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver_Touch));
			this.m_cardBacksTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut_Touch));
			this.m_cardBacksTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			this.m_allTabs.Add(this.m_cardBacksTab);
			this.m_tabVisibility[this.m_cardBacksTab] = true;
			this.m_cardBacksTabPos = this.m_cardBacksTab.transform.localPosition;
		}
		if (this.m_coinsTab != null)
		{
			this.m_coinsTab.Init(TAG_CLASS.NEUTRAL);
			this.m_coinsTab.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCoinsTabPressed));
			this.m_coinsTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver));
			this.m_coinsTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut));
			this.m_coinsTab.AddEventListener(UIEventType.ROLLOVER, new UIEvent.Handler(base.OnTabOver_Touch));
			this.m_coinsTab.AddEventListener(UIEventType.ROLLOUT, new UIEvent.Handler(base.OnTabOut_Touch));
			this.m_coinsTab.SetReceiveReleaseWithoutMouseDown(receiveReleaseWithoutMouseDown);
			this.m_allTabs.Add(this.m_coinsTab);
			this.m_tabVisibility[this.m_coinsTab] = true;
			this.m_coinsTabPos = this.m_coinsTab.transform.localPosition;
		}
		this.PositionBookTabs(false);
		this.m_initializedTabPositions = true;
	}

	// Token: 0x06001230 RID: 4656 RVA: 0x00067AF8 File Offset: 0x00065CF8
	protected override void PositionBookTabs(bool animate)
	{
		Vector3 position = this.m_classTabContainer.transform.position;
		int num = CollectionPageManager.CLASS_TAB_ORDER.Length;
		if (this.m_deckTemplateTab != null && this.m_deckTemplateTab.gameObject.activeSelf)
		{
			num++;
		}
		for (int i = 0; i < num; i++)
		{
			CollectionClassTab collectionClassTab = this.m_classTabs[i];
			Vector3 vector;
			if (this.ShouldShowTab(collectionClassTab))
			{
				collectionClassTab.SetTargetVisibility(true);
				position.x += this.m_spaceBetweenTabs;
				position.x += this.m_deselectedClassTabHalfWidth;
				vector = this.m_classTabContainer.transform.InverseTransformPoint(position);
				if (collectionClassTab == this.m_currentClassTab)
				{
					vector.y = collectionClassTab.m_SelectedLocalYPos;
				}
				position.x += this.m_deselectedClassTabHalfWidth;
			}
			else
			{
				collectionClassTab.SetTargetVisibility(false);
				vector = collectionClassTab.transform.localPosition;
				vector.z = CollectionPageManager.HIDDEN_TAB_LOCAL_Z_POS;
			}
			if (animate)
			{
				collectionClassTab.SetTargetLocalPosition(vector);
			}
			else
			{
				collectionClassTab.SetIsVisible(collectionClassTab.ShouldBeVisible());
				collectionClassTab.transform.localPosition = vector;
			}
		}
		bool showTab = this.ShouldShowTab(this.m_heroSkinsTab);
		base.PositionFixedTab(showTab, this.m_heroSkinsTab, this.m_heroSkinsTabPos, animate);
		bool showTab2 = this.ShouldShowTab(this.m_cardBacksTab);
		base.PositionFixedTab(showTab2, this.m_cardBacksTab, this.m_cardBacksTabPos, animate);
		bool showTab3 = this.ShouldShowTab(this.m_coinsTab);
		base.PositionFixedTab(showTab3, this.m_coinsTab, this.m_coinsTabPos, animate);
		if (!animate)
		{
			return;
		}
		base.StopCoroutine(CollectionPageManager.ANIMATE_TABS_COROUTINE_NAME);
		base.StartCoroutine(CollectionPageManager.ANIMATE_TABS_COROUTINE_NAME);
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x00067CAA File Offset: 0x00065EAA
	private IEnumerator AnimateTabs()
	{
		bool playSounds = HeroPickerDisplay.Get() == null || !HeroPickerDisplay.Get().IsShown();
		List<CollectionClassTab> list = new List<CollectionClassTab>();
		List<CollectionClassTab> tabsToShow = new List<CollectionClassTab>();
		List<CollectionClassTab> tabsToMove = new List<CollectionClassTab>();
		foreach (CollectionClassTab collectionClassTab in this.m_classTabs)
		{
			if (collectionClassTab.IsVisible() || collectionClassTab.ShouldBeVisible())
			{
				if (collectionClassTab.IsVisible() && collectionClassTab.ShouldBeVisible())
				{
					tabsToMove.Add(collectionClassTab);
				}
				else if (collectionClassTab.IsVisible() && !collectionClassTab.ShouldBeVisible())
				{
					list.Add(collectionClassTab);
				}
				else
				{
					tabsToShow.Add(collectionClassTab);
				}
			}
		}
		this.m_tabsAreAnimating = true;
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
				tab = null;
			}
			List<CollectionClassTab>.Enumerator enumerator2 = default(List<CollectionClassTab>.Enumerator);
			yield return new WaitForSeconds(0.1f);
		}
		if (tabsToMove.Count > 0)
		{
			foreach (CollectionClassTab collectionClassTab2 in tabsToMove)
			{
				if (collectionClassTab2.WillSlide() && playSounds)
				{
					SoundManager.Get().LoadAndPlay("class_tab_slides_across_top.prefab:04482bc6f531b76468ff92a5b4e979b6", collectionClassTab2.gameObject);
				}
				collectionClassTab2.AnimateToTargetPosition(0.25f, iTween.EaseType.easeOutQuad);
			}
			yield return new WaitForSeconds(0.25f);
		}
		if (tabsToShow.Count > 0)
		{
			foreach (CollectionClassTab collectionClassTab3 in tabsToShow)
			{
				if (playSounds)
				{
					SoundManager.Get().LoadAndPlay("class_tab_retract.prefab:da79957be76b10343999d6fa92a6a2f0", collectionClassTab3.gameObject);
				}
				collectionClassTab3.AnimateToTargetPosition(0.4f, iTween.EaseType.easeOutBounce);
			}
			yield return new WaitForSeconds(0.4f);
		}
		foreach (CollectionClassTab collectionClassTab4 in this.m_classTabs)
		{
			collectionClassTab4.SetIsVisible(collectionClassTab4.ShouldBeVisible());
		}
		this.m_tabsAreAnimating = false;
		yield break;
		yield break;
	}

	// Token: 0x06001232 RID: 4658 RVA: 0x00067CBC File Offset: 0x00065EBC
	private void SetCurrentClassTab(TAG_CLASS? tabClass)
	{
		CollectionClassTab collectionClassTab = null;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_classFilterHeader.SetMode(viewMode, tabClass);
			return;
		}
		switch (viewMode)
		{
		case CollectionUtils.ViewMode.CARDS:
			if (tabClass != null)
			{
				collectionClassTab = this.m_classTabs.Find((CollectionClassTab obj) => obj.GetClass() == tabClass.Value && obj.m_tabViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE);
				goto IL_A2;
			}
			goto IL_A2;
		case CollectionUtils.ViewMode.HERO_SKINS:
			collectionClassTab = this.m_heroSkinsTab;
			goto IL_A2;
		case CollectionUtils.ViewMode.CARD_BACKS:
			collectionClassTab = this.m_cardBacksTab;
			goto IL_A2;
		case CollectionUtils.ViewMode.COINS:
			collectionClassTab = this.m_coinsTab;
			goto IL_A2;
		}
		collectionClassTab = null;
		IL_A2:
		if (collectionClassTab == this.m_currentClassTab)
		{
			return;
		}
		base.DeselectCurrentClassTab();
		this.m_currentClassTab = collectionClassTab;
		if (this.m_currentClassTab != null)
		{
			base.StopCoroutine(CollectionPageManager.SELECT_TAB_COROUTINE_NAME);
			base.StartCoroutine(CollectionPageManager.SELECT_TAB_COROUTINE_NAME, this.m_currentClassTab);
		}
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x00067DB2 File Offset: 0x00065FB2
	public void SetDeckRuleset(DeckRuleset deckRuleset, bool refresh = false)
	{
		this.m_cardsCollection.SetDeckRuleset(deckRuleset);
		if (refresh)
		{
			this.UpdateFilteredCards();
			base.TransitionPageWhenReady(BookPageManager.PageTransitionType.NONE, false, null, null);
		}
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x00067DD4 File Offset: 0x00065FD4
	private void OnClassTabPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		CollectionClassTab collectionClassTab = e.GetElement() as CollectionClassTab;
		if (collectionClassTab == null || collectionClassTab == this.m_currentClassTab)
		{
			return;
		}
		TAG_CLASS @class = collectionClassTab.GetClass();
		this.JumpToCollectionClassPage(@class);
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x00067E1C File Offset: 0x0006601C
	private void OnDeckTemplateTabPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.DECK_TEMPLATE, null);
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x00067E38 File Offset: 0x00066038
	private void OnHeroSkinsTabPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		CollectionClassTab x = e.GetElement() as CollectionClassTab;
		if (x == null || x == this.m_currentClassTab)
		{
			return;
		}
		if (!this.ShouldShowTab(this.m_heroSkinsTab))
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.HERO_SKINS, null);
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x00067E94 File Offset: 0x00066094
	private void OnCardBacksTabPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		CollectionClassTab x = e.GetElement() as CollectionClassTab;
		if (x == null || x == this.m_currentClassTab)
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARD_BACKS, null);
	}

	// Token: 0x06001238 RID: 4664 RVA: 0x00067EE0 File Offset: 0x000660E0
	private void OnCoinsTabPressed(UIEvent e)
	{
		if (!this.CanUserTurnPages())
		{
			return;
		}
		CollectionClassTab x = e.GetElement() as CollectionClassTab;
		if (x == null || x == this.m_currentClassTab)
		{
			return;
		}
		if (!this.ShouldShowTab(this.m_coinsTab))
		{
			return;
		}
		CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.COINS, null);
	}

	// Token: 0x06001239 RID: 4665 RVA: 0x00067F3C File Offset: 0x0006613C
	public void UpdateMassDisenchant()
	{
		this.UpdateCraftingModeButtonDustBottleVisibility();
		if (CraftingTray.Get() != null)
		{
			CraftingTray.Get().SetMassDisenchantAmount();
		}
		if (MassDisenchant.Get() != null && CollectionManager.Get() != null)
		{
			MassDisenchant.Get().UpdateContents(CollectionManager.Get().GetMassDisenchantCards());
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x00067F8E File Offset: 0x0006618E
	public void JumpToCollectionClassPage(TAG_CLASS pageClass)
	{
		this.JumpToCollectionClassPage(pageClass, null, null);
	}

	// Token: 0x0600123B RID: 4667 RVA: 0x00067F9C File Offset: 0x0006619C
	public void JumpToCollectionClassPage(TAG_CLASS pageClass, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		CollectibleDisplay collectibleDisplay = CollectionManager.Get().GetCollectibleDisplay();
		if (collectibleDisplay != null && collectibleDisplay.GetViewMode() != CollectionUtils.ViewMode.CARDS)
		{
			collectibleDisplay.SetViewMode(CollectionUtils.ViewMode.CARDS, new CollectionUtils.ViewModeData
			{
				m_setPageByClass = new TAG_CLASS?(pageClass)
			});
			return;
		}
		int newPageNum = 0;
		this.m_cardsCollection.GetPageContentsForClass(pageClass, 1, true, out newPageNum);
		base.FlipToPage(newPageNum, callback, callbackData);
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x00067FFA File Offset: 0x000661FA
	protected override void AssembleEmptyPageUI(BookPageDisplay page)
	{
		base.AssembleEmptyPageUI(page);
		this.AssembleEmptyPageUI(this.PageAsCollectionPage(page), false);
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x00068014 File Offset: 0x00066214
	private void AssembleEmptyPageUI(CollectionPageDisplay page, bool displayNoMatchesText)
	{
		page.SetClass(null);
		bool showHints = CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS;
		page.ShowNoMatchesFound(displayNoMatchesText, this.m_cardsCollection.FindCardsResult, showHints);
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.CARDS)
		{
			base.DeselectCurrentClassTab();
		}
		page.SetPageCountText(GameStrings.Get("GLUE_COLLECTION_EMPTY_PAGE"));
		page.SetPageTextColor();
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x00068084 File Offset: 0x00066284
	private bool AssembleCollectionBasePage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool emptyPage, FormatType formatType)
	{
		CollectionPageDisplay page = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		page.UpdateBasePage();
		page.SetPageType(formatType);
		page.ActivatePageCountText(true);
		if (emptyPage)
		{
			base.SetHasPreviousAndNextPages(false, false);
			this.AssembleEmptyPageUI(page, true);
			CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(null, delegate(List<CollectionCardActors> actorList, object data)
			{
				page.UpdateCollectionCards(actorList, CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
				this.TransitionPage(transitionReadyCallbackData);
			}, null);
			return true;
		}
		return false;
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x00068118 File Offset: 0x00066318
	private void AssembleMassDisenchantPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool wildPage)
	{
		CollectionPageDisplay page = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		page.SetMassDisenchant();
		page.ActivatePageCountText(false);
		page.SetIsWild(wildPage);
		this.AssembleEmptyPageUI(page, false);
		base.SetHasPreviousAndNextPages(false, false);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(null, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdatePageWithMassDisenchant();
			this.TransitionPage(transitionReadyCallbackData);
		}, null);
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000681A8 File Offset: 0x000663A8
	private void AssembleCardPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, List<CollectibleCard> cardsToDisplay, int totalNumPages)
	{
		bool flag = cardsToDisplay == null || cardsToDisplay.Count == 0;
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} emptyPage={3} viewMode={4}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			this.m_currentPageIsPageA,
			flag,
			viewMode
		});
		FormatType formatType = (viewMode == CollectionUtils.ViewMode.HERO_SKINS) ? FormatType.FT_STANDARD : CollectionManager.Get().GetThemeShowingForCollectionPage(null);
		if (this.AssembleCollectionBasePage(transitionReadyCallbackData, flag, formatType))
		{
			return;
		}
		CollectionPageDisplay page = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		this.m_lastCardAnchor = cardsToDisplay[0];
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
			TAG_CLASS currentClassFromPage = this.m_cardsCollection.GetCurrentClassFromPage(this.m_currentPageNum);
			page.SetClass(new TAG_CLASS?(currentClassFromPage));
			this.m_currentClassContext = currentClassFromPage;
		}
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", new object[]
		{
			this.m_currentPageNum
		}));
		page.SetPageTextColor();
		page.ShowNoMatchesFound(false, null, true);
		base.SetHasPreviousAndNextPages(this.m_currentPageNum > 1, this.m_currentPageNum < totalNumPages);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(cardsToDisplay, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdateCollectionCards(actorList, viewMode);
			this.TransitionPageNextFrame(transitionReadyCallbackData);
			if (this.m_deckTemplatePicker != null)
			{
				this.StartCoroutine(this.m_deckTemplatePicker.Show(false));
			}
		}, null);
	}

	// Token: 0x06001241 RID: 4673 RVA: 0x00068360 File Offset: 0x00066560
	private void AssembleCoinPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, List<CollectibleCard> cardsToDisplay)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} currentPageNum={3}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			this.m_currentPageIsPageA,
			this.m_currentPageNum
		});
		int count = cardsToDisplay.Count;
		bool emptyPage = count == 0;
		if (this.AssembleCollectionBasePage(transitionReadyCallbackData, emptyPage, FormatType.FT_STANDARD))
		{
			return;
		}
		CollectionPageDisplay page = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		int maxCardsPerPage = CollectiblePageDisplay.GetMaxCardsPerPage();
		int num = count / maxCardsPerPage + ((count % maxCardsPerPage > 0) ? 1 : 0);
		this.m_currentPageNum = Mathf.Clamp(this.m_currentPageNum, 1, num);
		page.SetCoins();
		page.ShowNoMatchesFound(count == 0, null, true);
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", new object[]
		{
			this.m_currentPageNum
		}));
		base.SetHasPreviousAndNextPages(this.m_currentPageNum > 1, this.m_currentPageNum < num);
		CollectionManager.Get().GetCollectibleDisplay().CollectionPageContentsChanged(cardsToDisplay, delegate(List<CollectionCardActors> actorList, object data)
		{
			page.UpdateCollectionCards(actorList, CollectionUtils.ViewMode.COINS);
			this.TransitionPage(transitionReadyCallbackData);
			if (this.m_deckTemplatePicker != null)
			{
				this.StartCoroutine(this.m_deckTemplatePicker.Show(false));
			}
		}, null);
	}

	// Token: 0x06001242 RID: 4674 RVA: 0x000684A8 File Offset: 0x000666A8
	private void TransitionPageNextFrame(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData)
	{
		Processor.ScheduleCallback(0f, false, delegate(object userData)
		{
			this.TransitionPage(transitionReadyCallbackData);
		}, null);
	}

	// Token: 0x06001243 RID: 4675 RVA: 0x000684E4 File Offset: 0x000666E4
	private void AssembleDeckTemplatePage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData)
	{
		FormatType formatType = (this.m_deckTemplatePicker != null && this.m_deckTemplatePicker.CurrentSelectedFormat != FormatType.FT_UNKNOWN) ? this.m_deckTemplatePicker.CurrentSelectedFormat : FormatType.FT_STANDARD;
		if (this.AssembleCollectionBasePage(transitionReadyCallbackData, false, formatType))
		{
			return;
		}
		CollectionPageDisplay collectionPageDisplay = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		if (this.m_deckTemplatePicker == null && !string.IsNullOrEmpty(this.m_deckTemplatePickerPrefab))
		{
			this.m_deckTemplatePicker = GameUtils.LoadGameObjectWithComponent<DeckTemplatePicker>(this.m_deckTemplatePickerPrefab);
			if (this.m_deckTemplatePicker == null)
			{
				Debug.LogWarning("Failed to instantiate deck template picker prefab " + this.m_deckTemplatePickerPrefab);
				return;
			}
			this.m_deckTemplatePicker.RegisterOnTemplateDeckChosen(delegate
			{
				this.HideNonDeckTemplateTabs(false, true);
				CollectionManager.Get().GetCollectibleDisplay().SetViewMode(CollectionUtils.ViewMode.CARDS, null);
			});
		}
		CollectionPageDisplay collectionPageDisplay2 = collectionPageDisplay;
		DeckTemplatePicker deckTemplatePicker = this.m_deckTemplatePicker;
		collectionPageDisplay2.UpdateDeckTemplateHeader((deckTemplatePicker != null) ? deckTemplatePicker.m_pageHeader : null, formatType);
		collectionPageDisplay.UpdateDeckTemplatePage(this.m_deckTemplatePicker);
		collectionPageDisplay.SetDeckTemplates();
		collectionPageDisplay.ShowNoMatchesFound(false, null, true);
		collectionPageDisplay.SetPageCountText(string.Empty);
		base.SetHasPreviousAndNextPages(false, false);
		this.UpdateDeckTemplate(this.m_deckTemplatePicker);
		this.TransitionPage(transitionReadyCallbackData);
	}

	// Token: 0x06001244 RID: 4676 RVA: 0x000685F5 File Offset: 0x000667F5
	public DeckTemplatePicker GetDeckTemplatePicker()
	{
		return this.m_deckTemplatePicker;
	}

	// Token: 0x06001245 RID: 4677 RVA: 0x00068600 File Offset: 0x00066800
	public void UpdateDeckTemplate(DeckTemplatePicker deckTemplatePicker)
	{
		if (deckTemplatePicker != null)
		{
			CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
			if (editedDeck != null)
			{
				deckTemplatePicker.SetDeckFormatAndClass(editedDeck.FormatType, editedDeck.GetClass());
			}
			base.StartCoroutine(deckTemplatePicker.Show(true));
		}
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x00068644 File Offset: 0x00066844
	private void AssembleCardBackPage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} currentPageIsPageA={2} currentPageNum={3}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			this.m_currentPageIsPageA,
			this.m_currentPageNum
		});
		int count = this.GetCurrentDeckTrayModeCardBackIds().Count;
		bool emptyPage = count == 0;
		if (this.AssembleCollectionBasePage(transitionReadyCallbackData, emptyPage, FormatType.FT_STANDARD))
		{
			return;
		}
		CollectionPageDisplay page = this.PageAsCollectionPage(transitionReadyCallbackData.m_assembledPage);
		int maxCardsPerPage = CollectiblePageDisplay.GetMaxCardsPerPage();
		int num = count / maxCardsPerPage + ((count % maxCardsPerPage > 0) ? 1 : 0);
		this.m_currentPageNum = Mathf.Clamp(this.m_currentPageNum, 1, num);
		page.SetCardBacks();
		page.ShowNoMatchesFound(count == 0, null, true);
		page.SetPageCountText(GameStrings.Format("GLUE_COLLECTION_PAGE_NUM", new object[]
		{
			this.m_currentPageNum
		}));
		base.SetHasPreviousAndNextPages(this.m_currentPageNum > 1, this.m_currentPageNum < num);
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay != null)
		{
			collectionManagerDisplay.CollectionPageContentsChangedToCardBacks(this.m_currentPageNum, maxCardsPerPage, delegate(List<CollectionCardActors> actorList, object data)
			{
				page.UpdateCollectionCards(actorList, CollectionUtils.ViewMode.CARD_BACKS);
				foreach (CollectionCardActors collectionCardActors in actorList)
				{
					CardBackManager.Get().UpdateCardBackWithInternalCardBack(collectionCardActors.GetPreferredActor());
				}
				this.TransitionPage(transitionReadyCallbackData);
				if (this.m_deckTemplatePicker != null)
				{
					this.StartCoroutine(this.m_deckTemplatePicker.Show(false));
				}
			}, null, !CollectionManager.Get().IsInEditMode());
		}
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000687B8 File Offset: 0x000669B8
	protected override void AssemblePage(BookPageManager.TransitionReadyCallbackData transitionReadyCallbackData, bool useCurrentPageNum)
	{
		CollectionUtils.ViewMode viewMode = CollectionManager.Get().GetCollectibleDisplay().GetViewMode();
		if (viewMode == CollectionUtils.ViewMode.CARD_BACKS)
		{
			this.AssembleCardBackPage(transitionReadyCallbackData);
			return;
		}
		if (viewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			this.AssembleDeckTemplatePage(transitionReadyCallbackData);
			return;
		}
		if (viewMode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			List<CollectibleCard> heroesContents = this.m_heroesCollection.GetHeroesContents(this.m_currentPageNum);
			this.AssembleCardPage(transitionReadyCallbackData, heroesContents, this.m_heroesCollection.GetTotalNumPages());
			return;
		}
		if (viewMode == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			bool wildPage = CollectionManager.Get().GetThemeShowing(null) == FormatType.FT_WILD;
			this.AssembleMassDisenchantPage(transitionReadyCallbackData, wildPage);
			return;
		}
		if (viewMode == CollectionUtils.ViewMode.CARDS)
		{
			List<CollectibleCard> list;
			if (useCurrentPageNum)
			{
				list = this.m_cardsCollection.GetPageContents(this.m_currentPageNum);
			}
			else if (this.m_lastCardAnchor == null)
			{
				this.m_currentPageNum = 1;
				list = this.m_cardsCollection.GetPageContents(this.m_currentPageNum);
			}
			else
			{
				int num;
				list = this.m_cardsCollection.GetPageContentsForCard(this.m_lastCardAnchor.CardId, this.m_lastCardAnchor.PremiumType, out num, this.m_currentClassContext);
				if (list.Count == 0)
				{
					list = this.m_cardsCollection.GetPageContentsForClass(this.m_currentClassContext, 1, true, out num);
				}
				if (list.Count == 0)
				{
					list = this.m_cardsCollection.GetPageContents(1);
					num = 1;
				}
				this.m_currentPageNum = ((list.Count == 0) ? 0 : num);
			}
			if (list == null || list.Count == 0)
			{
				int currentPageNum;
				list = this.m_cardsCollection.GetFirstNonEmptyPage(out currentPageNum);
				if (list.Count > 0)
				{
					this.m_currentPageNum = currentPageNum;
				}
			}
			this.AssembleCardPage(transitionReadyCallbackData, list, this.m_cardsCollection.GetTotalNumPages());
			return;
		}
		if (viewMode == CollectionUtils.ViewMode.COINS)
		{
			List<CollectibleCard> orderedCoinCards = CoinManager.Get().GetOrderedCoinCards();
			this.AssembleCoinPage(transitionReadyCallbackData, orderedCoinCards);
		}
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00068944 File Offset: 0x00066B44
	private void UpdateFilteredHeroes()
	{
		this.m_heroesCollection.UpdateResults();
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00068951 File Offset: 0x00066B51
	private void UpdateFilteredCards()
	{
		this.m_cardsCollection.UpdateResults();
		this.UpdateClassTabNewCardCounts();
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00068964 File Offset: 0x00066B64
	protected override void TransitionPage(object callbackData)
	{
		base.TransitionPage(callbackData);
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() == CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			base.DeselectCurrentClassTab();
			return;
		}
		this.SetCurrentClassTab(new TAG_CLASS?(this.m_currentClassContext));
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00068998 File Offset: 0x00066B98
	protected override void OnPageTransitionRequested()
	{
		this.m_numPageFlipsThisSession++;
		int @int = Options.Get().GetInt(Option.PAGE_MOUSE_OVERS);
		int val = @int + 1;
		if (@int < this.m_numPlageFlipsBeforeStopShowingArrows)
		{
			Options.Get().SetInt(Option.PAGE_MOUSE_OVERS, val);
		}
		this.ShowSetFilterTutorialIfNeeded();
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x000689E0 File Offset: 0x00066BE0
	protected override void OnPageTurnComplete(object callbackData, int operationId)
	{
		if (this.m_numPageFlipsThisSession % CollectionPageManager.NUM_PAGE_FLIPS_UNTIL_UNLOAD_UNUSED_ASSETS == 0)
		{
			HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
			if (hearthstoneApplication != null)
			{
				hearthstoneApplication.UnloadUnusedAssets();
			}
		}
		base.OnPageTurnComplete(callbackData, operationId);
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x00068A18 File Offset: 0x00066C18
	private void ShowSetFilterTutorialIfNeeded()
	{
		if (Options.Get().GetBool(Option.HAS_SEEN_SET_FILTER_TUTORIAL))
		{
			return;
		}
		if (CollectionManager.Get().IsInEditMode())
		{
			return;
		}
		if (CollectionManager.Get().GetCollectibleDisplay().GetViewMode() != CollectionUtils.ViewMode.CARDS)
		{
			return;
		}
		if (!this.m_cardsCollection.CardSetFilterIsAllStandardSets())
		{
			return;
		}
		CollectionManagerDisplay collectionManagerDisplay = CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay;
		if (collectionManagerDisplay == null || collectionManagerDisplay.IsShowingSetFilterTray())
		{
			return;
		}
		if (!CollectionManager.Get().AccountHasWildCards())
		{
			return;
		}
		if (!RankMgr.Get().WildCardsAllowedInCurrentLeague())
		{
			return;
		}
		if (this.m_numPageFlipsThisSession >= CollectionPageManager.NUM_PAGE_FLIPS_BEFORE_SET_FILTER_TUTORIAL)
		{
			collectionManagerDisplay.ShowSetFilterTutorial(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
			Options.Get().SetBool(Option.HAS_SEEN_SET_FILTER_TUTORIAL, true);
		}
	}

	// Token: 0x0600124E RID: 4686 RVA: 0x00068AC4 File Offset: 0x00066CC4
	private void OnCollectionManagerViewModeChanged(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse)
	{
		if (!triggerResponse)
		{
			return;
		}
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1} mode={2}-->{3} triggerResponse={4}", new object[]
		{
			this.m_transitionPageId,
			this.m_pagesCurrentlyTurning,
			prevMode,
			mode,
			triggerResponse
		});
		this.UpdateCraftingModeButtonDustBottleVisibility();
		if (mode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			this.HideNonDeckTemplateTabs(true, false);
		}
		if (mode != CollectionUtils.ViewMode.CARDS)
		{
			CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
		}
		this.m_currentPageNum = 1;
		if (userdata != null)
		{
			if (userdata.m_setPageByClass != null)
			{
				this.m_cardsCollection.GetPageContentsForClass(userdata.m_setPageByClass.Value, 1, true, out this.m_currentPageNum);
			}
			else if (userdata.m_setPageByCard != null)
			{
				this.m_cardsCollection.GetPageContentsForCard(userdata.m_setPageByCard, userdata.m_setPageByPremium, out this.m_currentPageNum, this.m_currentClassContext);
			}
		}
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < CollectionPageManager.TAG_ORDERING.Length; i++)
		{
			if (prevMode == CollectionPageManager.TAG_ORDERING[i])
			{
				num = i;
			}
			if (mode == CollectionPageManager.TAG_ORDERING[i])
			{
				num2 = i;
			}
		}
		BookPageManager.PageTransitionType transition = (num2 - num < 0) ? BookPageManager.PageTransitionType.SINGLE_PAGE_LEFT : BookPageManager.PageTransitionType.SINGLE_PAGE_RIGHT;
		BookPageManager.DelOnPageTransitionComplete callback = null;
		object callbackData = null;
		if (userdata != null)
		{
			callback = userdata.m_pageTransitionCompleteCallback;
			callbackData = userdata.m_pageTransitionCompleteData;
		}
		if (this.m_turnPageCoroutine != null)
		{
			base.StopCoroutine(this.m_turnPageCoroutine);
		}
		CollectionDeckTray.Get().m_decksContent.UpdateDeckName(null);
		CollectionDeckTray.Get().UpdateDoneButtonText();
		this.m_turnPageCoroutine = base.StartCoroutine(this.ViewModeChangedWaitToTurnPage(transition, prevMode == CollectionUtils.ViewMode.DECK_TEMPLATE, callback, callbackData));
	}

	// Token: 0x0600124F RID: 4687 RVA: 0x00068C4C File Offset: 0x00066E4C
	private IEnumerator ViewModeChangedWaitToTurnPage(BookPageManager.PageTransitionType transition, bool hideDeckTemplateBottomPanel, BookPageManager.DelOnPageTransitionComplete callback, object callbackData)
	{
		if (this.m_deckTemplatePicker != null && hideDeckTemplateBottomPanel)
		{
			CollectionManager.Get().GetCollectibleDisplay().m_inputBlocker.gameObject.SetActive(true);
			this.m_deckTemplatePicker.ShowBottomPanel(false);
			while (this.m_deckTemplatePicker.IsShowingBottomPanel())
			{
				yield return null;
			}
			yield return base.StartCoroutine(this.m_deckTemplatePicker.ShowPacks(false));
			CollectionManager.Get().GetCollectibleDisplay().m_inputBlocker.gameObject.SetActive(false);
		}
		base.TransitionPageWhenReady(transition, true, callback, callbackData);
		yield break;
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x00068C78 File Offset: 0x00066E78
	public void OnFavoriteHeroChanged(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData)
	{
		this.GetCurrentCollectionPage().UpdateFavoriteHeroSkins(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x00068C94 File Offset: 0x00066E94
	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		this.GetCurrentCollectionPage().UpdateFavoriteCardBack(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x00068CB0 File Offset: 0x00066EB0
	public void OnFavoriteCoinChanged(int newFavoriteCoinId)
	{
		this.GetCurrentCollectionPage().UpdateFavoriteCoin(CollectionManager.Get().GetCollectibleDisplay().GetViewMode());
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x00068CCC File Offset: 0x00066ECC
	private HashSet<int> GetCurrentDeckTrayModeCardBackIds()
	{
		return CardBackManager.Get().GetCardBackIds(!CollectionManager.Get().IsInEditMode());
	}

	// Token: 0x06001254 RID: 4692 RVA: 0x00068CE8 File Offset: 0x00066EE8
	private void UpdateMouseWheel()
	{
		if (UniversalInputManager.Get().IsTouchMode() || !this.CanUserTurnPages())
		{
			return;
		}
		if (this.m_hasNextPage && Input.GetAxis("Mouse ScrollWheel") > 0f)
		{
			if (UniversalInputManager.Get().InputIsOver(this.GetCurrentCollectionPage().gameObject))
			{
				this.PageRight(null, null);
				return;
			}
		}
		else if (this.m_hasPreviousPage && Input.GetAxis("Mouse ScrollWheel") < 0f && UniversalInputManager.Get().InputIsOver(this.GetCurrentCollectionPage().gameObject))
		{
			this.PageLeft(null, null);
		}
	}

	// Token: 0x04000B92 RID: 2962
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

	// Token: 0x04000B93 RID: 2963
	private static readonly Map<TAG_CLASS, Color> s_classColors = new Map<TAG_CLASS, Color>
	{
		{
			TAG_CLASS.MAGE,
			new Color(0.12941177f, 0.26666668f, 0.3882353f)
		},
		{
			TAG_CLASS.PALADIN,
			new Color(0.4392157f, 0.29411766f, 0.09019608f)
		},
		{
			TAG_CLASS.PRIEST,
			new Color(0.52156866f, 0.52156866f, 0.52156866f)
		},
		{
			TAG_CLASS.ROGUE,
			new Color(0.09019608f, 0.07450981f, 0.07450981f)
		},
		{
			TAG_CLASS.SHAMAN,
			new Color(0.12941177f, 0.17254902f, 0.37254903f)
		},
		{
			TAG_CLASS.WARLOCK,
			new Color(0.21176471f, 0.10980392f, 0.28235295f)
		},
		{
			TAG_CLASS.WARRIOR,
			new Color(0.27450982f, 0.050980393f, 0.08235294f)
		},
		{
			TAG_CLASS.DRUID,
			new Color(0.23137255f, 0.16078432f, 0.08627451f)
		},
		{
			TAG_CLASS.HUNTER,
			new Color(0.22352941f, 0.4627451f, 0.1764706f)
		},
		{
			TAG_CLASS.NEUTRAL,
			new Color(0f, 0f, 0f)
		},
		{
			TAG_CLASS.WHIZBANG,
			new Color(0.5647059f, 0.3019608f, 0.5372549f)
		},
		{
			TAG_CLASS.DEMONHUNTER,
			new Color(0.09019608f, 0.22745098f, 0.19607843f)
		}
	};

	// Token: 0x04000B94 RID: 2964
	public static TAG_CLASS[] CLASS_TAB_ORDER = new TAG_CLASS[]
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

	// Token: 0x04000B95 RID: 2965
	public CollectionClassTab m_heroSkinsTab;

	// Token: 0x04000B96 RID: 2966
	public CollectionClassTab m_cardBacksTab;

	// Token: 0x04000B97 RID: 2967
	public CollectionClassTab m_coinsTab;

	// Token: 0x04000B98 RID: 2968
	public ClassFilterHeaderButton m_classFilterHeader;

	// Token: 0x04000B99 RID: 2969
	public CollectionClassTab m_deckTemplateTab;

	// Token: 0x04000B9A RID: 2970
	[CustomEditField(Sections = "Deck Template", T = EditType.GAME_OBJECT)]
	public string m_deckTemplatePickerPrefab;

	// Token: 0x04000B9B RID: 2971
	public int m_numPlageFlipsBeforeStopShowingArrows;

	// Token: 0x04000B9C RID: 2972
	private static CollectionUtils.ViewMode[] TAG_ORDERING = new CollectionUtils.ViewMode[]
	{
		CollectionUtils.ViewMode.CARDS,
		CollectionUtils.ViewMode.COINS,
		CollectionUtils.ViewMode.CARD_BACKS,
		CollectionUtils.ViewMode.HERO_SKINS
	};

	// Token: 0x04000B9D RID: 2973
	public static readonly float SELECT_TAB_ANIM_TIME = 0.2f;

	// Token: 0x04000B9E RID: 2974
	private static readonly Vector3 CLASS_TAB_LOCAL_EULERS = new Vector3(0f, 180f, 0f);

	// Token: 0x04000B9F RID: 2975
	private static readonly float HIDDEN_TAB_LOCAL_Z_POS = -0.42f;

	// Token: 0x04000BA0 RID: 2976
	private static readonly string ANIMATE_TABS_COROUTINE_NAME = "AnimateTabs";

	// Token: 0x04000BA1 RID: 2977
	private static readonly int NUM_PAGE_FLIPS_BEFORE_SET_FILTER_TUTORIAL = 3;

	// Token: 0x04000BA2 RID: 2978
	private static readonly int NUM_PAGE_FLIPS_UNTIL_UNLOAD_UNUSED_ASSETS = 15;

	// Token: 0x04000BA3 RID: 2979
	private static readonly string SELECT_TAB_COROUTINE_NAME = "SelectTabWhenReady";

	// Token: 0x04000BA4 RID: 2980
	private static Map<TAG_CLASS, int> CLASS_TO_TAB_IDX = null;

	// Token: 0x04000BA5 RID: 2981
	private List<CollectionClassTab> m_classTabs = new List<CollectionClassTab>();

	// Token: 0x04000BA6 RID: 2982
	private CollectibleCard m_lastCardAnchor;

	// Token: 0x04000BA7 RID: 2983
	private float m_deselectedClassTabHalfWidth;

	// Token: 0x04000BA8 RID: 2984
	private bool m_initializedTabPositions;

	// Token: 0x04000BA9 RID: 2985
	private MassDisenchant m_massDisenchant;

	// Token: 0x04000BAA RID: 2986
	private DeckTemplatePicker m_deckTemplatePicker;

	// Token: 0x04000BAB RID: 2987
	private CollectibleCardClassFilter m_cardsCollection = new CollectibleCardClassFilter();

	// Token: 0x04000BAC RID: 2988
	private CollectibleCardHeroesFilter m_heroesCollection = new CollectibleCardHeroesFilter();

	// Token: 0x04000BAD RID: 2989
	private Vector3 m_heroSkinsTabPos;

	// Token: 0x04000BAE RID: 2990
	private Vector3 m_cardBacksTabPos;

	// Token: 0x04000BAF RID: 2991
	private Vector3 m_coinsTabPos;

	// Token: 0x04000BB0 RID: 2992
	private bool m_hideNonDeckTemplateTabs;

	// Token: 0x04000BB1 RID: 2993
	private int m_numPageFlipsThisSession;

	// Token: 0x04000BB2 RID: 2994
	private Coroutine m_turnPageCoroutine;

	// Token: 0x04000BB3 RID: 2995
	protected TAG_CLASS m_currentClassContext;
}
