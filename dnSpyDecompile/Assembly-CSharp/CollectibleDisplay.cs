using System;
using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public abstract class CollectibleDisplay : MonoBehaviour
{
	// Token: 0x06000D6B RID: 3435 RVA: 0x0004CE50 File Offset: 0x0004B050
	public virtual void Start()
	{
		this.m_inputBlocker.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnInputBlockerRelease));
		this.m_search.RegisterActivatedListener(new CollectionSearch.ActivatedListener(this.OnSearchActivated));
		this.m_search.RegisterDeactivatedListener(new CollectionSearch.DeactivatedListener(this.OnSearchDeactivated));
		this.m_search.RegisterClearedListener(new CollectionSearch.ClearedListener(this.OnSearchCleared));
		int @int = Options.Get().GetInt(Option.PAGE_MOUSE_OVERS);
		if (this.m_pageManager.m_numPlageFlipsBeforeStopShowingArrows == 0 || @int < this.m_pageManager.m_numPlageFlipsBeforeStopShowingArrows || this.ALWAYS_SHOW_PAGING_ARROWS)
		{
			this.m_pageManager.LoadPagingArrows();
		}
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x0004CF00 File Offset: 0x0004B100
	protected virtual void Awake()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().SetCollectibleDisplay(this);
		}
		if (GraphicsManager.Get().RenderQualityLevel != GraphicsQuality.Low && PlatformSettings.Memory == MemoryCategory.High && this.m_cover == null)
		{
			this.m_isCoverLoading = true;
			AssetLoader.Get().InstantiatePrefab("CollectionBookCover.prefab:a639b04fb6567674cb0867564e54d673", new PrefabCallback<GameObject>(this.OnCoverLoaded), null, AssetLoadingOptions.None);
		}
		this.LoadAllTextures();
		this.EnableInput(true);
	}

	// Token: 0x06000D6D RID: 3437 RVA: 0x0004CF78 File Offset: 0x0004B178
	protected virtual void OnDestroy()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().SetCollectibleDisplay(null);
		}
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x0004CF8C File Offset: 0x0004B18C
	public Material GetGoldenCardNotOwnedMeshMaterial()
	{
		return this.m_goldenCardNotOwnedMeshMaterial;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x0004CF94 File Offset: 0x0004B194
	public Material GetCardNotOwnedMeshMaterial()
	{
		return this.m_cardNotOwnedMeshMaterial;
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x0004CF9C File Offset: 0x0004B19C
	public CollectionCardVisual GetCardVisualPrefab()
	{
		return this.m_cardVisualPrefab;
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x0004CFA4 File Offset: 0x0004B1A4
	public bool IsReady()
	{
		return this.m_isReady;
	}

	// Token: 0x06000D72 RID: 3442
	public abstract void Unload();

	// Token: 0x06000D73 RID: 3443
	public abstract void Exit();

	// Token: 0x06000D74 RID: 3444
	public abstract void CollectionPageContentsChanged(List<CollectibleCard> cardsToDisplay, CollectibleDisplay.CollectionActorsReadyCallback callback, object callbackData);

	// Token: 0x06000D75 RID: 3445
	public abstract void SetViewMode(CollectionUtils.ViewMode mode, bool triggerResponse, CollectionUtils.ViewModeData userdata = null);

	// Token: 0x06000D76 RID: 3446
	public abstract void HideAllTips();

	// Token: 0x06000D77 RID: 3447
	public abstract void SetFilterCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, SetFilterItem item, bool transitionPage);

	// Token: 0x06000D78 RID: 3448
	public abstract void ShowInnkeeperLClickHelp(EntityDef entityDef);

	// Token: 0x06000D79 RID: 3449 RVA: 0x0004CFAC File Offset: 0x0004B1AC
	public bool ShouldShowNewCardGlow(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard card = CollectionManager.Get().GetCard(cardID, premium);
		return card != null && card.IsNewCard;
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x0004CFD1 File Offset: 0x0004B1D1
	public CollectionUtils.CollectionPageLayoutSettings.Variables GetCurrentPageLayoutSettings()
	{
		return this.GetPageLayoutSettings(this.m_currentViewMode);
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x0004CFDF File Offset: 0x0004B1DF
	public CollectionUtils.CollectionPageLayoutSettings.Variables GetPageLayoutSettings(CollectionUtils.ViewMode viewMode)
	{
		return this.m_pageLayoutSettings.GetVariables(viewMode);
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x0004CFED File Offset: 0x0004B1ED
	public void SetViewMode(CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata = null)
	{
		this.SetViewMode(mode, true, userdata);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x0004CFF8 File Offset: 0x0004B1F8
	public CollectionUtils.ViewMode GetViewMode()
	{
		return this.m_currentViewMode;
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x0004D000 File Offset: 0x0004B200
	public bool SetFilterTrayInitialized()
	{
		return this.m_setFilterTrayInitialized;
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0004D008 File Offset: 0x0004B208
	public virtual void FilterBySearchText(string newSearchText)
	{
		this.m_search.SetText(newSearchText);
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x0004D016 File Offset: 0x0004B216
	public void ShowOnlyCardsIOwn()
	{
		this.ShowOnlyCardsIOwn(null);
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x0004D01F File Offset: 0x0004B21F
	public void ShowOnlyCardsIOwn(object obj)
	{
		this.m_pageManager.ShowOnlyCardsIOwn();
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x0004D02C File Offset: 0x0004B22C
	protected virtual void ShowSpecificCards(List<int> specificCards)
	{
		this.m_pageManager.FilterBySpecificCards(specificCards);
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x0004D03A File Offset: 0x0004B23A
	public void GoToPageWithCard(string cardID, TAG_PREMIUM premium)
	{
		if (this.m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			this.SetViewMode(CollectionUtils.ViewMode.CARDS, new CollectionUtils.ViewModeData
			{
				m_setPageByCard = cardID,
				m_setPageByPremium = premium
			});
			return;
		}
		this.m_pageManager.JumpToPageWithCard(cardID, premium);
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0004D06E File Offset: 0x0004B26E
	public void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		this.m_pageManager.UpdateCurrentPageCardLocks(playSound);
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0004D07C File Offset: 0x0004B27C
	public void RegisterSwitchViewModeListener(CollectibleDisplay.OnSwitchViewMode listener)
	{
		this.m_switchViewModeListeners.Add(listener);
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x0004D08A File Offset: 0x0004B28A
	public void RemoveSwitchViewModeListener(CollectibleDisplay.OnSwitchViewMode listener)
	{
		this.m_switchViewModeListeners.Remove(listener);
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0004D099 File Offset: 0x0004B299
	public void RegisterSearchFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_searchFilterListeners.Add(listener);
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x0004D0A7 File Offset: 0x0004B2A7
	public void UnregisterSearchFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_searchFilterListeners.Remove(listener);
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x0004D0B6 File Offset: 0x0004B2B6
	public virtual void ResetFilters(bool updateVisuals = true)
	{
		this.m_search.ClearFilter(updateVisuals);
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x0004D0C4 File Offset: 0x0004B2C4
	public void EnableInput(bool enable)
	{
		if (!enable)
		{
			this.m_inputBlockers++;
		}
		else if (this.m_inputBlockers > 0)
		{
			this.m_inputBlockers--;
		}
		bool active = this.m_inputBlockers > 0;
		this.m_inputBlocker.gameObject.SetActive(active);
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x0004D116 File Offset: 0x0004B316
	protected void OnCollectionLoaded()
	{
		this.m_pageManager.OnCollectionLoaded();
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0004D123 File Offset: 0x0004B323
	protected virtual void OnCollectionChanged()
	{
		this.m_pageManager.NotifyOfCollectionChanged();
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0004D130 File Offset: 0x0004B330
	protected void NotifyFilterUpdate(List<CollectibleDisplay.FilterStateListener> listeners, bool active, object value)
	{
		foreach (CollectibleDisplay.FilterStateListener filterStateListener in listeners)
		{
			filterStateListener(active, value);
		}
	}

	// Token: 0x06000D8E RID: 3470
	protected abstract void LoadAllTextures();

	// Token: 0x06000D8F RID: 3471
	protected abstract void UnloadAllTextures();

	// Token: 0x06000D90 RID: 3472 RVA: 0x0004D180 File Offset: 0x0004B380
	protected void OnCoverLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		this.m_isCoverLoading = false;
		this.m_cover = go.GetComponent<CollectionCoverDisplay>();
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x0004D195 File Offset: 0x0004B395
	protected void OnInputBlockerRelease(UIEvent e)
	{
		this.m_search.Deactivate();
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0004D1A2 File Offset: 0x0004B3A2
	protected void OnSearchActivated()
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.EnableInput(false);
		}
		this.m_pageManager.EnablePageTurn(false);
	}

	// Token: 0x06000D93 RID: 3475
	protected abstract void OnSearchDeactivated(string oldSearchText, string newSearchText);

	// Token: 0x06000D94 RID: 3476
	protected abstract void OnSearchCleared(bool transitionPage);

	// Token: 0x06000D95 RID: 3477 RVA: 0x0004D1C3 File Offset: 0x0004B3C3
	protected void OnSearchFilterComplete(object callbackdata = null)
	{
		this.m_pageManager.EnablePageTurn(true);
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x0004D1D1 File Offset: 0x0004B3D1
	protected void OnCoverOpened()
	{
		this.EnableInput(true);
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0004D1DC File Offset: 0x0004B3DC
	protected virtual void OnSwitchViewModeResponse(bool triggerResponse, CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode newMode, CollectionUtils.ViewModeData userdata)
	{
		CollectibleDisplay.OnSwitchViewMode[] array = this.m_switchViewModeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](prevMode, newMode, userdata, triggerResponse);
		}
	}

	// Token: 0x04000929 RID: 2345
	[CustomEditField(Sections = "Prefabs")]
	public CollectionCardVisual m_cardVisualPrefab;

	// Token: 0x0400092A RID: 2346
	[CustomEditField(Sections = "Bones")]
	public GameObject m_activeSearchBone;

	// Token: 0x0400092B RID: 2347
	[CustomEditField(Sections = "Bones")]
	public GameObject m_activeSearchBone_Win8;

	// Token: 0x0400092C RID: 2348
	[CustomEditField(Sections = "Bones")]
	public GameObject m_craftingTrayHiddenBone;

	// Token: 0x0400092D RID: 2349
	[CustomEditField(Sections = "Bones")]
	public GameObject m_craftingTrayShownBone;

	// Token: 0x0400092E RID: 2350
	[CustomEditField(Sections = "Objects")]
	public CollectionPageManager m_pageManager;

	// Token: 0x0400092F RID: 2351
	[CustomEditField(Sections = "Objects")]
	public CollectionCoverDisplay m_cover;

	// Token: 0x04000930 RID: 2352
	[CustomEditField(Sections = "Objects")]
	public CollectionSearch m_search;

	// Token: 0x04000931 RID: 2353
	[CustomEditField(Sections = "Objects")]
	public ActiveFilterButton m_filterButton;

	// Token: 0x04000932 RID: 2354
	[CustomEditField(Sections = "Objects")]
	public GameObject m_filterButtonGlow;

	// Token: 0x04000933 RID: 2355
	[CustomEditField(Sections = "Objects")]
	public PegUIElement m_inputBlocker;

	// Token: 0x04000934 RID: 2356
	[CustomEditField(Sections = "Controls")]
	public CollectionUtils.CollectionPageLayoutSettings m_pageLayoutSettings = new CollectionUtils.CollectionPageLayoutSettings();

	// Token: 0x04000935 RID: 2357
	[CustomEditField(Sections = "Materials")]
	public Material m_goldenCardNotOwnedMeshMaterial;

	// Token: 0x04000936 RID: 2358
	[CustomEditField(Sections = "Materials")]
	public Material m_cardNotOwnedMeshMaterial;

	// Token: 0x04000937 RID: 2359
	protected bool m_netCacheReady;

	// Token: 0x04000938 RID: 2360
	protected bool m_gameSaveDataReady;

	// Token: 0x04000939 RID: 2361
	protected bool m_isReady;

	// Token: 0x0400093A RID: 2362
	protected bool m_unloading;

	// Token: 0x0400093B RID: 2363
	protected List<CollectionCardActors> m_cardBackActors = new List<CollectionCardActors>();

	// Token: 0x0400093C RID: 2364
	protected List<CollectionCardActors> m_previousCardBackActors;

	// Token: 0x0400093D RID: 2365
	protected List<CollectionCardActors> m_cardActors = new List<CollectionCardActors>();

	// Token: 0x0400093E RID: 2366
	protected List<CollectionCardActors> m_previousCardActors = new List<CollectionCardActors>();

	// Token: 0x0400093F RID: 2367
	protected bool m_setFilterTrayInitialized;

	// Token: 0x04000940 RID: 2368
	protected bool m_isCoverLoading;

	// Token: 0x04000941 RID: 2369
	protected CraftingTray m_craftingTray;

	// Token: 0x04000942 RID: 2370
	protected SetFilterTray m_setFilterTray;

	// Token: 0x04000943 RID: 2371
	protected List<CollectibleDisplay.OnSwitchViewMode> m_switchViewModeListeners = new List<CollectibleDisplay.OnSwitchViewMode>();

	// Token: 0x04000944 RID: 2372
	protected CollectionUtils.ViewMode m_currentViewMode;

	// Token: 0x04000945 RID: 2373
	protected List<CollectibleDisplay.FilterStateListener> m_searchFilterListeners = new List<CollectibleDisplay.FilterStateListener>();

	// Token: 0x04000946 RID: 2374
	protected int m_inputBlockers;

	// Token: 0x04000947 RID: 2375
	protected bool m_searchTriggeredCraftingTray;

	// Token: 0x04000948 RID: 2376
	protected const float CRAFTING_TRAY_SLIDE_IN_TIME = 0.25f;

	// Token: 0x04000949 RID: 2377
	protected PlatformDependentValue<int> m_onscreenDecks = new PlatformDependentValue<int>(PlatformCategory.Screen)
	{
		PC = 8,
		Phone = 4
	};

	// Token: 0x0400094A RID: 2378
	protected readonly PlatformDependentValue<bool> ALWAYS_SHOW_PAGING_ARROWS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	// Token: 0x02001404 RID: 5124
	// (Invoke) Token: 0x0600D966 RID: 55654
	public delegate void DelTextureLoaded(TAG_CLASS classTag, Texture classTexture, object callbackData);

	// Token: 0x02001405 RID: 5125
	// (Invoke) Token: 0x0600D96A RID: 55658
	public delegate void CollectionActorsReadyCallback(List<CollectionCardActors> actors, object callbackData);

	// Token: 0x02001406 RID: 5126
	// (Invoke) Token: 0x0600D96E RID: 55662
	public delegate void OnSwitchViewMode(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse);

	// Token: 0x02001407 RID: 5127
	// (Invoke) Token: 0x0600D972 RID: 55666
	public delegate void FilterStateListener(bool filterActive, object value);
}
