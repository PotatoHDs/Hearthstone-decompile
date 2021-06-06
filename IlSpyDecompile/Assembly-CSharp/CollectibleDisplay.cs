using System.Collections.Generic;
using PegasusShared;
using UnityEngine;

public abstract class CollectibleDisplay : MonoBehaviour
{
	public delegate void DelTextureLoaded(TAG_CLASS classTag, Texture classTexture, object callbackData);

	public delegate void CollectionActorsReadyCallback(List<CollectionCardActors> actors, object callbackData);

	public delegate void OnSwitchViewMode(CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata, bool triggerResponse);

	public delegate void FilterStateListener(bool filterActive, object value);

	[CustomEditField(Sections = "Prefabs")]
	public CollectionCardVisual m_cardVisualPrefab;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_activeSearchBone;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_activeSearchBone_Win8;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_craftingTrayHiddenBone;

	[CustomEditField(Sections = "Bones")]
	public GameObject m_craftingTrayShownBone;

	[CustomEditField(Sections = "Objects")]
	public CollectionPageManager m_pageManager;

	[CustomEditField(Sections = "Objects")]
	public CollectionCoverDisplay m_cover;

	[CustomEditField(Sections = "Objects")]
	public CollectionSearch m_search;

	[CustomEditField(Sections = "Objects")]
	public ActiveFilterButton m_filterButton;

	[CustomEditField(Sections = "Objects")]
	public GameObject m_filterButtonGlow;

	[CustomEditField(Sections = "Objects")]
	public PegUIElement m_inputBlocker;

	[CustomEditField(Sections = "Controls")]
	public CollectionUtils.CollectionPageLayoutSettings m_pageLayoutSettings = new CollectionUtils.CollectionPageLayoutSettings();

	[CustomEditField(Sections = "Materials")]
	public Material m_goldenCardNotOwnedMeshMaterial;

	[CustomEditField(Sections = "Materials")]
	public Material m_cardNotOwnedMeshMaterial;

	protected bool m_netCacheReady;

	protected bool m_gameSaveDataReady;

	protected bool m_isReady;

	protected bool m_unloading;

	protected List<CollectionCardActors> m_cardBackActors = new List<CollectionCardActors>();

	protected List<CollectionCardActors> m_previousCardBackActors;

	protected List<CollectionCardActors> m_cardActors = new List<CollectionCardActors>();

	protected List<CollectionCardActors> m_previousCardActors = new List<CollectionCardActors>();

	protected bool m_setFilterTrayInitialized;

	protected bool m_isCoverLoading;

	protected CraftingTray m_craftingTray;

	protected SetFilterTray m_setFilterTray;

	protected List<OnSwitchViewMode> m_switchViewModeListeners = new List<OnSwitchViewMode>();

	protected CollectionUtils.ViewMode m_currentViewMode;

	protected List<FilterStateListener> m_searchFilterListeners = new List<FilterStateListener>();

	protected int m_inputBlockers;

	protected bool m_searchTriggeredCraftingTray;

	protected const float CRAFTING_TRAY_SLIDE_IN_TIME = 0.25f;

	protected PlatformDependentValue<int> m_onscreenDecks = new PlatformDependentValue<int>(PlatformCategory.Screen)
	{
		PC = 8,
		Phone = 4
	};

	protected readonly PlatformDependentValue<bool> ALWAYS_SHOW_PAGING_ARROWS = new PlatformDependentValue<bool>(PlatformCategory.OS)
	{
		iOS = true,
		Android = true,
		PC = false,
		Mac = false
	};

	public virtual void Start()
	{
		m_inputBlocker.AddEventListener(UIEventType.RELEASE, OnInputBlockerRelease);
		m_search.RegisterActivatedListener(OnSearchActivated);
		m_search.RegisterDeactivatedListener(OnSearchDeactivated);
		m_search.RegisterClearedListener(OnSearchCleared);
		int @int = Options.Get().GetInt(Option.PAGE_MOUSE_OVERS);
		if (m_pageManager.m_numPlageFlipsBeforeStopShowingArrows == 0 || @int < m_pageManager.m_numPlageFlipsBeforeStopShowingArrows || (bool)ALWAYS_SHOW_PAGING_ARROWS)
		{
			m_pageManager.LoadPagingArrows();
		}
	}

	protected virtual void Awake()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().SetCollectibleDisplay(this);
		}
		if (GraphicsManager.Get().RenderQualityLevel != 0 && PlatformSettings.Memory == MemoryCategory.High && m_cover == null)
		{
			m_isCoverLoading = true;
			AssetLoader.Get().InstantiatePrefab("CollectionBookCover.prefab:a639b04fb6567674cb0867564e54d673", OnCoverLoaded);
		}
		LoadAllTextures();
		EnableInput(enable: true);
	}

	protected virtual void OnDestroy()
	{
		if (CollectionManager.Get() != null)
		{
			CollectionManager.Get().SetCollectibleDisplay(null);
		}
	}

	public Material GetGoldenCardNotOwnedMeshMaterial()
	{
		return m_goldenCardNotOwnedMeshMaterial;
	}

	public Material GetCardNotOwnedMeshMaterial()
	{
		return m_cardNotOwnedMeshMaterial;
	}

	public CollectionCardVisual GetCardVisualPrefab()
	{
		return m_cardVisualPrefab;
	}

	public bool IsReady()
	{
		return m_isReady;
	}

	public abstract void Unload();

	public abstract void Exit();

	public abstract void CollectionPageContentsChanged(List<CollectibleCard> cardsToDisplay, CollectionActorsReadyCallback callback, object callbackData);

	public abstract void SetViewMode(CollectionUtils.ViewMode mode, bool triggerResponse, CollectionUtils.ViewModeData userdata = null);

	public abstract void HideAllTips();

	public abstract void SetFilterCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, FormatType formatType, SetFilterItem item, bool transitionPage);

	public abstract void ShowInnkeeperLClickHelp(EntityDef entityDef);

	public bool ShouldShowNewCardGlow(string cardID, TAG_PREMIUM premium)
	{
		return CollectionManager.Get().GetCard(cardID, premium)?.IsNewCard ?? false;
	}

	public CollectionUtils.CollectionPageLayoutSettings.Variables GetCurrentPageLayoutSettings()
	{
		return GetPageLayoutSettings(m_currentViewMode);
	}

	public CollectionUtils.CollectionPageLayoutSettings.Variables GetPageLayoutSettings(CollectionUtils.ViewMode viewMode)
	{
		return m_pageLayoutSettings.GetVariables(viewMode);
	}

	public void SetViewMode(CollectionUtils.ViewMode mode, CollectionUtils.ViewModeData userdata = null)
	{
		SetViewMode(mode, triggerResponse: true, userdata);
	}

	public CollectionUtils.ViewMode GetViewMode()
	{
		return m_currentViewMode;
	}

	public bool SetFilterTrayInitialized()
	{
		return m_setFilterTrayInitialized;
	}

	public virtual void FilterBySearchText(string newSearchText)
	{
		m_search.SetText(newSearchText);
	}

	public void ShowOnlyCardsIOwn()
	{
		ShowOnlyCardsIOwn(null);
	}

	public void ShowOnlyCardsIOwn(object obj)
	{
		m_pageManager.ShowOnlyCardsIOwn();
	}

	protected virtual void ShowSpecificCards(List<int> specificCards)
	{
		m_pageManager.FilterBySpecificCards(specificCards);
	}

	public void GoToPageWithCard(string cardID, TAG_PREMIUM premium)
	{
		if (m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			SetViewMode(CollectionUtils.ViewMode.CARDS, new CollectionUtils.ViewModeData
			{
				m_setPageByCard = cardID,
				m_setPageByPremium = premium
			});
		}
		else
		{
			m_pageManager.JumpToPageWithCard(cardID, premium);
		}
	}

	public void UpdateCurrentPageCardLocks(bool playSound = false)
	{
		m_pageManager.UpdateCurrentPageCardLocks(playSound);
	}

	public void RegisterSwitchViewModeListener(OnSwitchViewMode listener)
	{
		m_switchViewModeListeners.Add(listener);
	}

	public void RemoveSwitchViewModeListener(OnSwitchViewMode listener)
	{
		m_switchViewModeListeners.Remove(listener);
	}

	public void RegisterSearchFilterListener(FilterStateListener listener)
	{
		m_searchFilterListeners.Add(listener);
	}

	public void UnregisterSearchFilterListener(FilterStateListener listener)
	{
		m_searchFilterListeners.Remove(listener);
	}

	public virtual void ResetFilters(bool updateVisuals = true)
	{
		m_search.ClearFilter(updateVisuals);
	}

	public void EnableInput(bool enable)
	{
		if (!enable)
		{
			m_inputBlockers++;
		}
		else if (m_inputBlockers > 0)
		{
			m_inputBlockers--;
		}
		bool active = m_inputBlockers > 0;
		m_inputBlocker.gameObject.SetActive(active);
	}

	protected void OnCollectionLoaded()
	{
		m_pageManager.OnCollectionLoaded();
	}

	protected virtual void OnCollectionChanged()
	{
		m_pageManager.NotifyOfCollectionChanged();
	}

	protected void NotifyFilterUpdate(List<FilterStateListener> listeners, bool active, object value)
	{
		foreach (FilterStateListener listener in listeners)
		{
			listener(active, value);
		}
	}

	protected abstract void LoadAllTextures();

	protected abstract void UnloadAllTextures();

	protected void OnCoverLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		m_isCoverLoading = false;
		m_cover = go.GetComponent<CollectionCoverDisplay>();
	}

	protected void OnInputBlockerRelease(UIEvent e)
	{
		m_search.Deactivate();
	}

	protected void OnSearchActivated()
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			EnableInput(enable: false);
		}
		m_pageManager.EnablePageTurn(enable: false);
	}

	protected abstract void OnSearchDeactivated(string oldSearchText, string newSearchText);

	protected abstract void OnSearchCleared(bool transitionPage);

	protected void OnSearchFilterComplete(object callbackdata = null)
	{
		m_pageManager.EnablePageTurn(enable: true);
	}

	protected void OnCoverOpened()
	{
		EnableInput(enable: true);
	}

	protected virtual void OnSwitchViewModeResponse(bool triggerResponse, CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode newMode, CollectionUtils.ViewModeData userdata)
	{
		OnSwitchViewMode[] array = m_switchViewModeListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](prevMode, newMode, userdata, triggerResponse);
		}
	}
}
