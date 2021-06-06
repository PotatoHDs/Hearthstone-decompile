using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets;
using Blizzard.T5.AssetManager;
using Blizzard.T5.Core;
using Blizzard.Telemetry.WTCG.Client;
using Hearthstone;
using PegasusShared;
using UnityEngine;
using UnityEngine.Serialization;

[CustomEditClass]
public class CollectionManagerDisplay : CollectibleDisplay
{
	[Serializable]
	public class CardSetIconMatOffset
	{
		public TAG_CARD_SET m_cardSet;

		public UnityEngine.Vector2 m_offset;
	}

	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateHiddenBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateShownBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateTutorialWelcomeBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateTutorialReminderBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_editDeckTutorialBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_convertDeckTutorialBone;

	[CustomEditField(Sections = "Bones")]
	public Transform m_setFilterTutorialBone;

	[CustomEditField(Sections = "Objects")]
	public ManaFilterTabManager m_manaTabManager;

	[CustomEditField(Sections = "Objects")]
	public CraftingModeButton m_craftingModeButton;

	[CustomEditField(Sections = "Objects")]
	public Notification m_deckTemplateCardReplacePopup;

	[CustomEditField(Sections = "Objects")]
	public NestedPrefab m_setFilterTrayContainer;

	[CustomEditField(Sections = "Controls")]
	public Texture m_allSetsTexture;

	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_allSetsIconOffset;

	[CustomEditField(Sections = "Controls")]
	public Texture m_wildSetsTexture;

	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_wildSetsIconOffset;

	[CustomEditField(Sections = "Controls")]
	public Texture m_featuredCardsTexture;

	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_featuredCardsIconOffset;

	[CustomEditField(Sections = "Controls")]
	public Texture m_classicSetsTexture;

	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_classicSetsIconOffset;

	[CustomEditField(Sections = "CM Customization Ref")]
	public GameObject m_bookBack;

	[CustomEditField(Sections = "CM Customization Ref")]
	[FormerlySerializedAs("m_tavernBrawlBookBackMesh")]
	public Mesh m_customBookBackMesh;

	[CustomEditField(Sections = "CM Customization Ref")]
	[FormerlySerializedAs("m_tavernBrawlObjectsToSwap")]
	public List<GameObject> m_customObjectsToSwap = new List<GameObject>();

	[CustomEditField(Sections = "Tavern Brawl Changes", T = EditType.TEXTURE)]
	[FormerlySerializedAs("m_corkBackTexture")]
	public string m_tbCorkBackTexture;

	[CustomEditField(Sections = "Tavern Brawl Changes")]
	public Material m_tavernBrawlElements;

	[CustomEditField(Sections = "Duels Changes", T = EditType.TEXTURE)]
	public string m_duelsCorkBackTexture;

	[CustomEditField(Sections = "Duels Changes")]
	public Material m_duelsElements;

	private Map<TAG_CLASS, AssetHandle<Texture>> m_loadedClassTextures = new Map<TAG_CLASS, AssetHandle<Texture>>();

	private AssetHandle<Texture> m_loadedCorkBackTexture;

	private bool m_selectingNewDeckHero;

	private long m_showDeckContentsRequest;

	private Notification m_deckHelpPopup;

	private Notification m_innkeeperLClickReminder;

	private List<FilterStateListener> m_setFilterListeners = new List<FilterStateListener>();

	private List<FilterStateListener> m_manaFilterListeners = new List<FilterStateListener>();

	private DeckTemplatePicker m_deckTemplatePickerPhone;

	private HeroPickerDisplay m_heroPickerDisplay;

	private Notification m_createDeckNotification;

	private Notification m_convertTutorialPopup;

	private IEnumerator m_showConvertTutorialCoroutine;

	private Notification m_setFilterTutorialPopup;

	private IEnumerator m_showSetFilterTutorialCoroutine;

	private bool m_showingDeckTemplateTips;

	private float m_deckTemplateTipWaitTime;

	private bool m_manaFilterIsFromSearchText;

	private ShareableDeck m_cachedShareableDeck;

	private string m_currentActiveFeaturedCardsEvent;

	public bool IsManaFilterEvenValues => m_manaTabManager.IsFilterEvenValues;

	public bool IsManaFilterOddValues => m_manaTabManager.IsFilterOddValues;

	public override void Start()
	{
		NetCache.Get().RegisterScreenCollectionManager(OnNetCacheReady);
		CollectionManager.Get().RegisterCollectionNetHandlers();
		CollectionManager.Get().RegisterCollectionLoadedListener(base.OnCollectionLoaded);
		CollectionManager.Get().RegisterCollectionChangedListener(OnCollectionChanged);
		CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreatedByPlayer);
		CollectionManager.Get().RegisterDeckContentsListener(OnDeckContents);
		CollectionManager.Get().RegisterNewCardSeenListener(OnNewCardSeen);
		CollectionManager.Get().RegisterCardRewardsInsertedListener(OnCardRewardsInserted);
		CardBackManager.Get().SetSearchText(null);
		GameSaveDataManager.Get().Request(GameSaveKeyId.COLLECTION_MANAGER, OnGameSaveDataReady);
		base.Start();
		if (m_setFilterTrayContainer != null)
		{
			m_setFilterTray = m_setFilterTrayContainer.PrefabGameObject(instantiateIfNeeded: true).GetComponentsInChildren<SetFilterTray>(includeInactive: true)[0];
			m_setFilterTray.m_toggleButton.AddEventListener(UIEventType.PRESS, delegate
			{
				OnSetFilterButtonPressed();
			});
		}
		if (m_filterButton != null)
		{
			m_filterButton.m_inactiveFilterButton.AddEventListener(UIEventType.PRESS, delegate
			{
				OnPhoneFilterButtonPressed();
			});
		}
		bool @bool = Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, defaultVal: false);
		ShowAdvancedCollectionManager(@bool);
		if (!@bool)
		{
			Options.Get().RegisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, OnShowAdvancedCMChanged);
		}
		DoEnterCollectionManagerEvents();
		if (!IsSpecialOneDeckMode())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_CollectionManager);
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial())
		{
			UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
		SetTavernBrawlTexturesIfNecessary();
		SetDuelsTexturesIfNecessary();
		CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded();
		StartCoroutine(WaitUntilReady());
	}

	protected override void Awake()
	{
		HearthstonePerformance.Get()?.StartPerformanceFlow(new FlowPerformance.SetupConfig
		{
			FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.COLLECTION_MANAGER
		});
		m_manaTabManager.OnFilterCleared += ManaFilterTab_OnManaFilterCleared;
		m_manaTabManager.OnManaValueActivated += ManaFilterTab_OnManaValueActivated;
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_deckTemplatePickerPhone = AssetLoader.Get().InstantiatePrefab("DeckTemplate_phone.prefab:a8a8fbcd170064edfb0eeac3f836a13b").GetComponent<DeckTemplatePicker>();
			SlidingTray component = m_deckTemplatePickerPhone.GetComponent<SlidingTray>();
			component.m_trayHiddenBone = m_deckTemplateHiddenBone.transform;
			component.m_trayShownBone = m_deckTemplateShownBone.transform;
		}
		base.Awake();
		StartCoroutine(InitCollectionWhenReady());
	}

	protected override void OnDestroy()
	{
		m_manaTabManager.OnFilterCleared -= ManaFilterTab_OnManaFilterCleared;
		m_manaTabManager.OnManaValueActivated -= ManaFilterTab_OnManaValueActivated;
		AssetHandle.SafeDispose(ref m_loadedCorkBackTexture);
		m_loadedClassTextures.DisposeValuesAndClear();
		if (m_deckTemplatePickerPhone != null)
		{
			UnityEngine.Object.Destroy(m_deckTemplatePickerPhone.gameObject);
			m_deckTemplatePickerPhone = null;
		}
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		base.OnDestroy();
	}

	private void Update()
	{
		if (HearthstoneApplication.IsInternal())
		{
			if (InputCollection.GetKeyDown(KeyCode.Alpha1))
			{
				SetViewMode(CollectionUtils.ViewMode.HERO_SKINS);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha2))
			{
				SetViewMode(CollectionUtils.ViewMode.CARDS);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha3))
			{
				SetViewMode(CollectionUtils.ViewMode.CARD_BACKS);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha4))
			{
				SetViewMode(CollectionUtils.ViewMode.DECK_TEMPLATE);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha4))
			{
				OnCraftingModeButtonReleased(null);
			}
		}
		ShowDeckTemplateTipsIfNeeded();
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			StartCoroutine(OnApplicationFocusCoroutine());
		}
	}

	private IEnumerator OnApplicationFocusCoroutine()
	{
		yield return null;
		CheckClipboardAndPromptPlayerToPaste();
	}

	public override void Unload()
	{
		m_unloading = true;
		NotificationManager.Get().DestroyAllPopUps();
		UnloadAllTextures();
		CollectionDeckTray.Get().GetCardsContent().UnregisterCardTileRightClickedListener(OnCardTileRightClicked);
		CollectionDeckTray.Get().Unload();
		CollectionInputMgr.Get().Unload();
		CollectionManager.Get().RemoveCollectionLoadedListener(base.OnCollectionLoaded);
		CollectionManager.Get().RemoveCollectionChangedListener(OnCollectionChanged);
		CollectionManager.Get().RemoveDeckCreatedListener(OnDeckCreatedByPlayer);
		CollectionManager.Get().RemoveDeckContentsListener(OnDeckContents);
		CollectionManager.Get().RemoveNewCardSeenListener(OnNewCardSeen);
		CollectionManager.Get().RemoveCardRewardsInsertedListener(OnCardRewardsInserted);
		CollectionManager.Get().RemoveCollectionNetHandlers();
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		Options.Get().UnregisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, OnShowAdvancedCMChanged);
		m_unloading = false;
	}

	public override void Exit()
	{
		EnableInput(enable: false);
		NotificationManager.Get().DestroyAllPopUps();
		CollectionDeckTray.Get().Exit();
		if (m_pageManager != null)
		{
			m_pageManager.Exit();
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetPrevMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			mode = SceneMgr.Mode.HUB;
		}
		if (!Network.IsLoggedIn() && mode != SceneMgr.Mode.HUB)
		{
			DialogManager.Get().ShowReconnectHelperDialog();
			mode = SceneMgr.Mode.HUB;
			Navigation.Clear();
		}
		HearthstonePerformance.Get()?.StopCurrentFlow();
		SceneMgr.Get().SetNextMode(mode);
	}

	public override void CollectionPageContentsChanged(List<CollectibleCard> cardsToDisplay, CollectionActorsReadyCallback callback, object callbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1}", m_pageManager.GetTransitionPageId(), m_pageManager.ArePagesTurning());
		bool flag = false;
		if (cardsToDisplay == null)
		{
			Log.CollectionManager.Print("artStacksToDisplay is null!");
			flag = true;
		}
		else if (cardsToDisplay.Count == 0)
		{
			Log.CollectionManager.Print("artStacksToDisplay has a count of 0!");
			flag = true;
		}
		if (flag)
		{
			List<CollectionCardActors> actors = new List<CollectionCardActors>();
			callback(actors, callbackData);
		}
		else
		{
			if (m_unloading)
			{
				return;
			}
			foreach (CollectionCardActors previousCardActor in m_previousCardActors)
			{
				previousCardActor.Destroy();
			}
			m_previousCardActors.Clear();
			m_previousCardActors = m_cardActors;
			m_cardActors = new List<CollectionCardActors>();
			long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
			new Map<string, CollectionCardActors>();
			foreach (CollectibleCard item in cardsToDisplay)
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(item.CardId);
				using DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(item.CardId);
				string text = (entityDef.IsHeroSkin() ? ActorNames.GetHeroSkinOrHandActor(entityDef.GetCardType(), item.PremiumType) : ActorNames.GetHandActor(entityDef.GetCardType(), item.PremiumType));
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(text, AssetLoadingOptions.IgnorePrefabPosition);
				if (gameObject == null)
				{
					Debug.LogError("Unable to load card actor.");
					continue;
				}
				Actor component = gameObject.GetComponent<Actor>();
				if (component == null)
				{
					Debug.LogError("Actor object does not contain Actor component.");
					continue;
				}
				component.SetEntityDef(entityDef);
				component.SetCardDef(cardDef);
				component.SetPremium(item.PremiumType);
				component.CreateBannedRibbon();
				if (item.OwnedCount == 0)
				{
					if (item.IsCraftable && arcaneDustBalance >= item.CraftBuyCost)
					{
						component.GhostCardEffect(GhostCard.Type.MISSING, item.PremiumType);
					}
					else if (entityDef.IsHeroSkin() && HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(entityDef.GetCardId()))
					{
						component.GhostCardEffect(GhostCard.Type.MISSING, item.PremiumType);
					}
					else
					{
						component.MissingCardEffect();
					}
				}
				component.UpdateAllComponents();
				m_cardActors.Add(new CollectionCardActors(component));
			}
			callback?.Invoke(m_cardActors, callbackData);
		}
	}

	public void CollectionPageContentsChangedToCardBacks(int pageNumber, int numCardBacksPerPage, CollectionActorsReadyCallback callback, object callbackData, bool showAll)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1}", m_pageManager.GetTransitionPageId(), m_pageManager.ArePagesTurning());
		CardBackManager cardBackManager = CardBackManager.Get();
		List<CollectionCardActors> result = new List<CollectionCardActors>();
		List<CardBackManager.OwnedCardBack> orderedEnabledCardBacks = cardBackManager.GetOrderedEnabledCardBacks(!showAll);
		if (orderedEnabledCardBacks.Count == 0)
		{
			if (callback != null)
			{
				callback(result, callbackData);
			}
			return;
		}
		int num = (pageNumber - 1) * numCardBacksPerPage;
		int count = Mathf.Min(orderedEnabledCardBacks.Count - num, numCardBacksPerPage);
		orderedEnabledCardBacks = orderedEnabledCardBacks.GetRange(num, count);
		int numCardBacksToLoad = orderedEnabledCardBacks.Count;
		Action<int, CardBackManager.OwnedCardBack, Actor> cbLoadedCallback = delegate(int index, CardBackManager.OwnedCardBack cardBack, Actor actor)
		{
			if (actor != null)
			{
				result[index] = new CollectionCardActors(actor);
				actor.SetCardbackUpdateIgnore(ignoreUpdate: true);
				CollectionCardBack component2 = actor.GetComponent<CollectionCardBack>();
				if (component2 != null)
				{
					component2.SetCardBackId(cardBack.m_cardBackId);
					component2.SetCardBackName(CardBackManager.Get().GetCardBackName(cardBack.m_cardBackId));
				}
				else
				{
					Debug.LogError("CollectionCardBack component does not exist on actor!");
				}
				if (!cardBack.m_owned)
				{
					if (cardBack.m_canBuy)
					{
						actor.GhostCardEffect(GhostCard.Type.MISSING);
					}
					else
					{
						actor.MissingCardEffect();
					}
				}
			}
			numCardBacksToLoad--;
			if (numCardBacksToLoad == 0 && callback != null)
			{
				callback(result, callbackData);
			}
		};
		if (m_previousCardBackActors != null)
		{
			foreach (CollectionCardActors previousCardBackActor in m_previousCardBackActors)
			{
				previousCardBackActor.Destroy();
			}
			m_previousCardBackActors.Clear();
		}
		m_previousCardBackActors = m_cardBackActors;
		m_cardBackActors = new List<CollectionCardActors>();
		for (int i = 0; i < orderedEnabledCardBacks.Count; i++)
		{
			int currIndex = i;
			CardBackManager.OwnedCardBack cardBackLoad = orderedEnabledCardBacks[i];
			int cardBackId = cardBackLoad.m_cardBackId;
			result.Add(null);
			if (!cardBackManager.LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
			{
				GameObject gameObject = cardBackData.m_GameObject;
				gameObject.transform.parent = base.transform;
				gameObject.name = gameObject.name + "_" + cardBackData.m_CardBackIndex;
				Actor component = gameObject.GetComponent<Actor>();
				if (component == null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					GameObject cardMesh = component.m_cardMesh;
					component.SetCardbackUpdateIgnore(ignoreUpdate: true);
					component.SetUnlit();
					if (cardMesh != null)
					{
						Material material = cardMesh.GetComponent<Renderer>().GetMaterial();
						if (material.HasProperty("_SpecularIntensity"))
						{
							material.SetFloat("_SpecularIntensity", 0f);
						}
					}
					m_cardBackActors.Add(new CollectionCardActors(component));
				}
				cbLoadedCallback(currIndex, cardBackLoad, component);
			}, "Collection_Card_Back.prefab:a208f592a46e4f447b3026e82444177e"))
			{
				cbLoadedCallback(currIndex, cardBackLoad, null);
			}
		}
	}

	public void RequestContentsToShowDeck(long deckID)
	{
		m_showDeckContentsRequest = deckID;
		CollectionManager.Get().RequestDeckContents(m_showDeckContentsRequest);
	}

	public void ShowPhoneDeckTemplateTray()
	{
		m_pageManager.UpdateDeckTemplate(m_deckTemplatePickerPhone);
		SlidingTray component = m_deckTemplatePickerPhone.GetComponent<SlidingTray>();
		component.RegisterTrayToggleListener(m_deckTemplatePickerPhone.OnTrayToggled);
		component.ShowTray();
	}

	public DeckTemplatePicker GetPhoneDeckTemplateTray()
	{
		return m_deckTemplatePickerPhone;
	}

	public override void SetViewMode(CollectionUtils.ViewMode mode, bool triggerResponse, CollectionUtils.ViewModeData userdata = null)
	{
		Log.CollectionManager.Print("mode={0}-->{1} triggerResponse={2} isUpdatingTrayMode={3}", m_currentViewMode, mode, triggerResponse, CollectionDeckTray.Get().IsUpdatingTrayMode());
		if (m_currentViewMode == mode || ((mode == CollectionUtils.ViewMode.HERO_SKINS || mode == CollectionUtils.ViewMode.CARD_BACKS || mode == CollectionUtils.ViewMode.COINS) && CollectionDeckTray.Get().IsUpdatingTrayMode()))
		{
			return;
		}
		if (mode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			if (!CollectionManager.Get().IsInEditMode() || SceneMgr.Get().IsInTavernBrawlMode())
			{
				return;
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				ShowPhoneDeckTemplateTray();
			}
		}
		CollectionUtils.ViewMode currentViewMode = m_currentViewMode;
		m_currentViewMode = mode;
		OnSwitchViewModeResponse(triggerResponse, currentViewMode, mode, userdata);
	}

	public bool ViewModeHasVisibleDeckList()
	{
		if (m_currentViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			return m_currentViewMode != CollectionUtils.ViewMode.MASS_DISENCHANT;
		}
		return false;
	}

	public void OnDoneEditingDeck()
	{
		ShowAppropriateSetFilters();
		if (m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			SetViewMode(CollectionUtils.ViewMode.CARDS, triggerResponse: false);
		}
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			m_pageManager.SetDeckRuleset(null);
		}
		FiresideGatheringManager.Get().UpdateDeckValidity();
		m_pageManager.OnDoneEditingDeck();
	}

	private void ManaFilterTab_OnManaFilterCleared(bool transitionPage)
	{
		ManaFilterTab_OnManaValueActivated(-1, transitionPage);
		m_manaFilterIsFromSearchText = false;
	}

	public void ManaFilterTab_OnManaValueActivated(int cost, bool transitionPage)
	{
		if (m_manaFilterIsFromSearchText)
		{
			bool updateManaFilterToMatchSearchText = false;
			RemoveManaTokenFromSearchText(updateManaFilterToMatchSearchText);
		}
		bool active = m_manaTabManager.IsManaValueActive(cost);
		string value = ((cost < 7) ? cost.ToString() : (cost + "+"));
		NotifyFilterUpdate(m_manaFilterListeners, active, value);
		m_pageManager.FilterByManaCost(cost, transitionPage);
	}

	public override void FilterBySearchText(string newSearchText)
	{
		string text = m_search.GetText();
		base.FilterBySearchText(newSearchText);
		OnSearchDeactivated_Internal(text, newSearchText, updateManaFilterToMatchSearchText: true);
	}

	private void RemoveManaTokenFromSearchText(bool updateManaFilterToMatchSearchText)
	{
		string text = m_search.GetText();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		string[] array = text.Split(CollectibleCardFilter.SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries);
		if (array.Length == 0)
		{
			return;
		}
		bool hasManaToken = false;
		Func<string, bool> isManaToken = GetIsManaSearchTokenFunc();
		string[] value = array.Where(delegate(string t)
		{
			if (isManaToken(t))
			{
				hasManaToken = true;
				return false;
			}
			return true;
		}).ToArray();
		if (hasManaToken)
		{
			string text2 = string.Join(new string(CollectibleCardFilter.SearchTokenDelimiters[0], 1), value);
			m_search.SetText(text2);
			OnSearchDeactivated_Internal(text, m_search.GetText(), updateManaFilterToMatchSearchText);
		}
	}

	private void UpdateManaFilterToMatchSearchText(string searchText, bool transitionPage = true)
	{
		if (string.IsNullOrEmpty(searchText) || !m_manaTabManager.Enabled)
		{
			m_manaTabManager.ClearFilter(transitionPage);
			return;
		}
		Func<string, bool> isManaSearchTokenFunc = GetIsManaSearchTokenFunc();
		string text = searchText.Split(CollectibleCardFilter.SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(isManaSearchTokenFunc);
		if (text != null)
		{
			if (m_pageManager.IsManaCostFilterActive)
			{
				m_pageManager.FilterByManaCost(-1, transitionPage);
			}
			string text2 = text.Split(CollectibleCardFilter.SearchTagColons, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
			GeneralUtils.ParseNumericRange(text2, out var isNumericalValue, out var minVal, out var maxVal);
			string text3 = null;
			if (isNumericalValue)
			{
				m_manaTabManager.SetFilter_Range(minVal, maxVal);
				text3 = text2;
			}
			else
			{
				string text4 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA").ToLower();
				string text5 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA").ToLower();
				string text6 = text2.ToLower();
				bool flag = text6 == text4;
				bool flag2 = !flag && text6 == text5;
				if (flag2 || flag)
				{
					m_manaTabManager.SetFilter_EvenOdd(flag2);
					text3 = CollectibleCardFilter.CreateSearchTerm_Mana_OddEven(flag2);
				}
			}
			if (text3 != null)
			{
				m_manaFilterIsFromSearchText = true;
				NotifyFilterUpdate(m_manaFilterListeners, active: true, text3);
			}
		}
		else
		{
			m_manaTabManager.ClearFilter(transitionPage);
		}
	}

	private Func<string, bool> GetIsManaSearchTokenFunc()
	{
		string manaToken = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MANA").ToLower();
		string evenTokenValue = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA").ToLower();
		string oddTokenValue = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA").ToLower();
		return delegate(string token)
		{
			string[] array = token.Split(CollectibleCardFilter.SearchTagColons, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length >= 2 && array[0].Trim().ToLower() == manaToken)
			{
				string text = array[1].Trim();
				GeneralUtils.ParseNumericRange(text, out var isNumericalValue, out var _, out var _);
				if (isNumericalValue)
				{
					return true;
				}
				string text2 = text.ToLower();
				if (text2 == oddTokenValue || text2 == evenTokenValue)
				{
					return true;
				}
			}
			return false;
		};
	}

	public override void HideAllTips()
	{
		if (m_innkeeperLClickReminder != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_innkeeperLClickReminder);
		}
		HideDeckHelpPopup();
		if (m_convertTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_convertTutorialPopup);
		}
		if (m_createDeckNotification != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_createDeckNotification);
		}
	}

	public void HideDeckHelpPopup()
	{
		if (m_deckHelpPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(m_deckHelpPopup);
		}
		if (CollectionDeckTray.Get() != null && CollectionDeckTray.Get().GetCardsContent() != null)
		{
			CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
		}
	}

	public override void ShowInnkeeperLClickHelp(EntityDef entityDef)
	{
		bool isHero = entityDef?.IsHeroSkin() ?? false;
		ShowInnkeeperLClickHelp(isHero);
	}

	private void ShowInnkeeperLClickHelp(bool isHero)
	{
		if (!CollectionDeckTray.Get().IsShowingDeckContents())
		{
			if (isHero)
			{
				m_innkeeperLClickReminder = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_LCLICK_HERO"), "", 3f);
			}
			else
			{
				m_innkeeperLClickReminder = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_LCLICK"), "", 3f);
			}
		}
	}

	public void ShowPremiumCardsNotOwned(bool show)
	{
		m_pageManager.ShowCardsNotOwned(show);
	}

	private void FeaturedCardsSetFilterCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, PegasusShared.FormatType formatType, SetFilterItem item, bool transitionPage)
	{
		SetLastSeenFeaturedCardsEvent(m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_ITEM);
		item.SetIconFxActive(active: false);
		SetFilterCallback(cardSets, specificCards, formatType, item, transitionPage);
	}

	public override void SetFilterCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, PegasusShared.FormatType formatType, SetFilterItem item, bool transitionPage)
	{
		bool flag = formatType == PegasusShared.FormatType.FT_WILD;
		if (flag && !CollectionManager.Get().AccountEverHadWildCards() && !SceneMgr.Get().IsInDuelsMode())
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_SET_FILTER_WILD_SET_HEADER"),
				m_text = GameStrings.Get("GLUE_COLLECTION_SET_FILTER_WILD_SET_BODY"),
				m_cancelText = GameStrings.Get("GLOBAL_CANCEL"),
				m_confirmText = GameStrings.Get("GLOBAL_BUTTON_YES"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
				m_responseCallback = delegate(AlertPopup.Response response, object userData)
				{
					if (response == AlertPopup.Response.CONFIRM)
					{
						ShowSetFilterCards(cardSets, specificCards, transitionPage);
					}
					else
					{
						m_setFilterTray.SelectPreviouslySelectedItem();
					}
				}
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			m_search.SetWildModeActive(flag);
			ShowSetFilterCards(cardSets, specificCards, transitionPage);
		}
	}

	private void ShowSetFilterCards(List<TAG_CARD_SET> cardSets, List<int> specificCards, bool transitionPage = true)
	{
		if (specificCards != null)
		{
			ShowSpecificCards(specificCards);
		}
		else
		{
			ShowSets(cardSets, transitionPage);
		}
	}

	private void ShowSets(List<TAG_CARD_SET> cardSets, bool transitionPage = true)
	{
		m_pageManager.FilterByCardSets(cardSets, transitionPage);
		NotifyFilterUpdate(m_setFilterListeners, cardSets != null, null);
	}

	protected override void ShowSpecificCards(List<int> specificCards)
	{
		base.ShowSpecificCards(specificCards);
		NotifyFilterUpdate(m_setFilterListeners, specificCards != null, null);
	}

	public HeroPickerDisplay GetHeroPickerDisplay()
	{
		return m_heroPickerDisplay;
	}

	public void EnterSelectNewDeckHeroMode()
	{
		if (!m_selectingNewDeckHero)
		{
			EnableInput(enable: false);
			m_selectingNewDeckHero = true;
			m_heroPickerDisplay = AssetLoader.Get().InstantiatePrefab("HeroPicker.prefab:59e2d2f899d09f4488a194df18967915").GetComponent<HeroPickerDisplay>();
			NotificationManager.Get().DestroyAllPopUps();
			if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
			{
				m_pageManager.HideNonDeckTemplateTabs(hide: true);
			}
			CheckClipboardAndPromptPlayerToPaste();
		}
	}

	public void ExitSelectNewDeckHeroMode()
	{
		m_selectingNewDeckHero = false;
	}

	public void CancelSelectNewDeckHeroMode()
	{
		EnableInput(enable: true);
		m_pageManager.HideNonDeckTemplateTabs(hide: false, updateTabs: true);
		ExitSelectNewDeckHeroMode();
	}

	public bool CanViewHeroSkins()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			return true;
		}
		return collectionManager.GetBestHeroesIOwn(editedDeck.GetClass()).Count > 1;
	}

	public bool CanViewCardBacks()
	{
		return CardBackManager.Get().GetCardBacksOwned().Count > 1;
	}

	public bool CanViewCoins()
	{
		if (CollectionManager.Get().GetEditedDeck() == null)
		{
			return (CoinManager.Get().GetCoinsOwned()?.Count ?? 0) > 1;
		}
		return false;
	}

	public void RegisterManaFilterListener(FilterStateListener listener)
	{
		m_manaFilterListeners.Add(listener);
	}

	public void UnregisterManaFilterListener(FilterStateListener listener)
	{
		m_manaFilterListeners.Remove(listener);
	}

	public void RegisterSetFilterListener(FilterStateListener listener)
	{
		m_setFilterListeners.Add(listener);
	}

	public void UnregisterSetFilterListener(FilterStateListener listener)
	{
		m_setFilterListeners.Remove(listener);
	}

	public override void ResetFilters(bool updateVisuals = true)
	{
		base.ResetFilters(updateVisuals);
		m_manaTabManager.ClearFilter();
		if (m_setFilterTray != null)
		{
			m_setFilterTray.ClearFilter();
		}
	}

	public void ShowAppropriateSetFilters()
	{
		bool flag = m_craftingTray != null && m_craftingTray.IsShown();
		bool flag2 = CollectionManager.Get().IsInEditMode();
		PegasusShared.FormatType formatType = PegasusShared.FormatType.FT_STANDARD;
		if (flag2)
		{
			formatType = CollectionManager.Get().GetEditedDeck().FormatType;
		}
		else if (RankMgr.Get().WildCardsAllowedInCurrentLeague())
		{
			if (CollectionManager.Get().ShouldAccountSeeStandardWild() || flag)
			{
				formatType = PegasusShared.FormatType.FT_WILD;
			}
		}
		else if (flag)
		{
			formatType = PegasusShared.FormatType.FT_STANDARD;
		}
		else if (CollectionManager.Get().AccountEverHadWildCards())
		{
			formatType = PegasusShared.FormatType.FT_WILD;
		}
		UpdateSetFilters(formatType, flag2, flag);
	}

	public void UpdateSetFilters(PegasusShared.FormatType formatType, bool editingDeck, bool showUnownedSets = false)
	{
		m_setFilterTray.UpdateSetFilters(formatType, editingDeck, showUnownedSets);
	}

	public void HideFilterTrayOnStartDragCard()
	{
		if (IsShowingSetFilterTray())
		{
			m_filterButton.m_setFilterTray.ToggleTraySlider(show: false);
		}
	}

	public void UnhideFilterTrayOnStopDragCard()
	{
		if (IsShowingSetFilterTray())
		{
			m_filterButton.m_setFilterTray.ToggleTraySlider(show: true);
		}
	}

	public void WaitThenUnhideFilterTrayOnStopDragCard()
	{
		if (IsShowingSetFilterTray())
		{
			StartCoroutine(WaitThenUnhideFilterTrayOnStopDragCard_Coroutine());
		}
	}

	private IEnumerator WaitThenUnhideFilterTrayOnStopDragCard_Coroutine()
	{
		yield return new WaitForSeconds(0.5f);
		if (CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay != null && IsShowingSetFilterTray() && CollectionInputMgr.Get() != null && !CollectionInputMgr.Get().HasHeldCard())
		{
			m_filterButton.m_setFilterTray.ToggleTraySlider(show: true);
		}
	}

	public bool SetFilterIsDefaultSelection()
	{
		if (m_setFilterTray == null)
		{
			return true;
		}
		return !m_setFilterTray.HasActiveFilter();
	}

	public bool IsShowingSetFilterTray()
	{
		if (m_setFilterTray == null)
		{
			return false;
		}
		return m_setFilterTray.IsShown();
	}

	public bool IsSelectingNewDeckHero()
	{
		return m_selectingNewDeckHero;
	}

	private void OnDeckContents(long deckID)
	{
		if (deckID == m_showDeckContentsRequest)
		{
			m_showDeckContentsRequest = 0L;
			ShowDeck(deckID, isNewDeck: false, showDeckTemplatePage: false);
		}
		else
		{
			CollectionDeckTray.Get().GetDecksContent().OnDeckContentsUpdated(deckID);
		}
	}

	private void OnDeckCreatedByPlayer(long deckID)
	{
		bool showDeckTemplatePage = false;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			CollectionManager collectionManager = CollectionManager.Get();
			if (collectionManager == null)
			{
				Debug.LogError("CollectionManagerDisplay.OnDeckCreatedByPlayer: CollectionManager.Get() returned null");
				return;
			}
			CollectionDeck deck = collectionManager.GetDeck(deckID);
			if (deck == null)
			{
				Debug.LogError("CollectionManagerDisplay.OnDeckCreatedByPlayer: Could not get deck " + deckID);
				return;
			}
			if (CollectionManager.Get().GetNonStarterTemplateDecks(deck.FormatType, deck.GetClass()).Count > 0)
			{
				showDeckTemplatePage = true;
			}
		}
		ShowDeck(deckID, isNewDeck: true, showDeckTemplatePage);
	}

	private void OnNewCardSeen(string cardID, TAG_PREMIUM premium)
	{
		m_pageManager.UpdateClassTabNewCardCounts();
	}

	private void OnCardRewardsInserted(List<string> cardID, List<TAG_PREMIUM> premium)
	{
		m_pageManager.RefreshCurrentPageContents();
	}

	protected override void OnCollectionChanged()
	{
		if (m_currentViewMode != CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			m_pageManager.NotifyOfCollectionChanged();
		}
	}

	private IEnumerator WaitUntilReady()
	{
		while (!m_netCacheReady && Network.IsLoggedIn())
		{
			yield return 0;
		}
		while (!m_gameSaveDataReady && Network.IsLoggedIn())
		{
			yield return 0;
		}
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		collectionDeckTray.Initialize();
		collectionDeckTray.RegisterModeSwitchedListener(delegate
		{
			UpdateCurrentPageCardLocks();
		});
		collectionDeckTray.GetCardsContent().RegisterCardTileRightClickedListener(OnCardTileRightClicked);
		m_isReady = true;
	}

	private IEnumerator InitCollectionWhenReady()
	{
		while (!m_pageManager.IsFullyLoaded())
		{
			yield return null;
		}
		m_pageManager.LoadMassDisenchantScreen();
		m_pageManager.OnCollectionLoaded();
	}

	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Collection.Manager)
		{
			if (!SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
			{
				SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
				Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_COLLECTION");
			}
		}
		else
		{
			m_netCacheReady = true;
		}
	}

	private void OnGameSaveDataReady(bool success)
	{
		if (!success)
		{
			Log.CollectionManager.PrintError("Error retrieving Game Save Key for Collection Manager!");
		}
		m_gameSaveDataReady = true;
	}

	private void OnShowAdvancedCMChanged(Option option, object prevValue, bool existed, object userData)
	{
		bool @bool = Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, defaultVal: false);
		if (@bool)
		{
			Options.Get().UnregisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, OnShowAdvancedCMChanged);
		}
		ShowAdvancedCollectionManager(@bool);
		m_manaTabManager.ActivateTabs(active: true);
	}

	private void OnCardTileRightClicked(DeckTrayDeckTileVisual cardTile)
	{
		if (GetViewMode() != CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			if (!cardTile.GetSlot().Owned && !DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID()))
			{
				CraftingManager.Get().EnterCraftMode(cardTile.GetActor());
			}
			GoToPageWithCard(cardTile.GetCardID(), cardTile.GetPremium());
		}
	}

	protected override void LoadAllTextures()
	{
		foreach (TAG_CLASS value in Enum.GetValues(typeof(TAG_CLASS)))
		{
			string classTextureAssetPath = GetClassTextureAssetPath(value);
			if (!string.IsNullOrEmpty(classTextureAssetPath))
			{
				AssetLoader.Get().LoadAsset<Texture>(classTextureAssetPath, OnClassTextureLoaded, value);
			}
		}
	}

	protected override void UnloadAllTextures()
	{
		m_loadedClassTextures.DisposeValuesAndClear();
	}

	public static string GetClassTextureAssetPath(TAG_CLASS classTag)
	{
		return classTag switch
		{
			TAG_CLASS.DRUID => "Druid.psd:e2417dc1394f54349956b2e24a27f923", 
			TAG_CLASS.HUNTER => "Hunter.psd:16178c8d6ed14814dae893bad9de80d5", 
			TAG_CLASS.MAGE => "Mage.psd:8dcb9bd578b6c01448cf1021c6157dfd", 
			TAG_CLASS.PALADIN => "Paladin.psd:50ba8fc595684d440866ac130c146d57", 
			TAG_CLASS.PRIEST => "Priest.psd:5fa4606c71c0dff4eb0b07b88ba83197", 
			TAG_CLASS.ROGUE => "Rogue.psd:47dc46a5269d7fc4a8a9ebada8f2d890", 
			TAG_CLASS.SHAMAN => "Shaman.psd:2e468e3b0f7a7804a9335333c9e673e2", 
			TAG_CLASS.WARLOCK => "Warlock.psd:d6077adee4894df43a67617620de56a9", 
			TAG_CLASS.WARRIOR => "Warrior.psd:5376d479d4155ca419f8afa5e42ba505", 
			_ => "", 
		};
	}

	private void SetTavernBrawlTexturesIfNecessary()
	{
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			return;
		}
		if (m_bookBack != null && !string.IsNullOrEmpty(m_tbCorkBackTexture) && m_customBookBackMesh != null)
		{
			m_bookBack.GetComponent<MeshFilter>().mesh = m_customBookBackMesh;
			AssetLoader.Get().LoadAsset(ref m_loadedCorkBackTexture, m_tbCorkBackTexture);
			m_bookBack.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", m_loadedCorkBackTexture);
			m_setFilterTray.m_toggleButton.SetButtonBackgroundMaterial();
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		foreach (GameObject item in m_customObjectsToSwap)
		{
			Renderer component = item.GetComponent<Renderer>();
			if (component != null)
			{
				component.SetMaterial(m_tavernBrawlElements);
				continue;
			}
			Debug.LogErrorFormat("Failed to swap material for TavernBrawl object: {0}", item.name);
		}
	}

	private void SetDuelsTexturesIfNecessary()
	{
		if (!SceneMgr.Get().IsInDuelsMode())
		{
			return;
		}
		if (m_bookBack != null && !string.IsNullOrEmpty(m_duelsCorkBackTexture) && m_customBookBackMesh != null)
		{
			m_bookBack.GetComponent<MeshFilter>().mesh = m_customBookBackMesh;
			AssetLoader.Get().LoadAsset(ref m_loadedCorkBackTexture, m_duelsCorkBackTexture);
			m_bookBack.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", m_loadedCorkBackTexture);
			m_setFilterTray.m_toggleButton.SetButtonBackgroundMaterial();
		}
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			return;
		}
		foreach (GameObject item in m_customObjectsToSwap)
		{
			Renderer component = item.GetComponent<Renderer>();
			if (component != null)
			{
				component.SetMaterial(m_duelsElements);
				continue;
			}
			Debug.LogErrorFormat("Failed to swap material for TavernBrawl object: {0}", item.name);
		}
	}

	private void OnClassTextureLoaded(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
	{
		if (loadedTexture == null)
		{
			Debug.LogWarning($"CollectionManagerDisplay.OnClassTextureLoaded(): asset for {assetRef} is null!");
			return;
		}
		TAG_CLASS key = (TAG_CLASS)callbackData;
		m_loadedClassTextures.SetOrReplaceDisposable(key, loadedTexture);
	}

	public void ShowCurrentEditedDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck != null)
		{
			TAG_CLASS @class = editedDeck.GetClass();
			ShowDeckHelper(editedDeck, @class, isNewDeck: false, showDeckTemplatePage: false);
		}
	}

	public void ShowDeck(long deckID, bool isNewDeck, bool showDeckTemplatePage, CollectionUtils.ViewMode? setNewViewMode = null)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck != null)
		{
			TAG_CLASS deckHeroClass = GetDeckHeroClass(deckID);
			ShowDeckHelper(deck, deckHeroClass, isNewDeck, showDeckTemplatePage, setNewViewMode);
		}
	}

	private void ShowDeckHelper(CollectionDeck currDeck, TAG_CLASS deckClass, bool isNewDeck, bool showDeckTemplatePage, CollectionUtils.ViewMode? setNewViewMode = null)
	{
		if (currDeck.HasUIHeroOverride() && m_currentViewMode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			m_pageManager.JumpToCollectionClassPage(deckClass);
		}
		if (!showDeckTemplatePage)
		{
			m_pageManager.HideNonDeckTemplateTabs(hide: false);
		}
		if (showDeckTemplatePage)
		{
			setNewViewMode = CollectionUtils.ViewMode.DECK_TEMPLATE;
		}
		else if ((m_currentViewMode == CollectionUtils.ViewMode.HERO_SKINS && !CanViewHeroSkins()) || (m_currentViewMode == CollectionUtils.ViewMode.CARD_BACKS && !CanViewCardBacks()) || (m_currentViewMode == CollectionUtils.ViewMode.COINS && !CanViewCoins()))
		{
			setNewViewMode = CollectionUtils.ViewMode.CARDS;
		}
		CollectionManager.Get().StartEditingDeck(currDeck, isNewDeck);
		CollectionDeckTray.Get().ShowDeck(setNewViewMode ?? GetViewMode());
		if (setNewViewMode.HasValue)
		{
			SetViewMode(setNewViewMode.Value);
		}
		UpdateSetFilters(currDeck.FormatType, editingDeck: true);
		m_pageManager.UpdateFiltersForDeck(currDeck, deckClass, isNewDeck);
		m_pageManager.UpdateCraftingModeButtonDustBottleVisibility();
		NotificationManager.Get().DestroyNotification(m_createDeckNotification, 0.25f);
	}

	private TAG_CLASS GetDeckHeroClass(long deckID)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck == null)
		{
			Log.CollectionManager.Print($"CollectionManagerDisplay no deck with ID {deckID}!");
			return TAG_CLASS.INVALID;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(deck.HeroCardID);
		if (entityDef == null)
		{
			Log.CollectionManager.Print($"CollectionManagerDisplay: CollectionManager doesn't have an entity def for {deck.HeroCardID}!");
			return TAG_CLASS.INVALID;
		}
		return entityDef.GetClass();
	}

	private IEnumerator DoBookOpeningAnimations()
	{
		while (m_isCoverLoading)
		{
			yield return null;
		}
		if (m_cover != null)
		{
			m_cover.Open(base.OnCoverOpened);
		}
		else
		{
			OnCoverOpened();
		}
		m_manaTabManager.ActivateTabs(active: true);
	}

	private IEnumerator SetBookToOpen()
	{
		while (m_isCoverLoading)
		{
			yield return null;
		}
		if (m_cover != null)
		{
			m_cover.SetOpenState();
		}
		m_manaTabManager.ActivateTabs(active: true);
	}

	private void DoBookClosingAnimations()
	{
		if (m_cover != null)
		{
			m_cover.Close();
		}
		m_manaTabManager.ActivateTabs(active: false);
	}

	private void ShowAdvancedCollectionManager(bool show)
	{
		show |= (bool)UniversalInputManager.UsePhoneUI;
		m_search.gameObject.SetActive(show);
		m_manaTabManager.gameObject.SetActive(show);
		if (m_setFilterTray != null)
		{
			bool buttonShown = show && !UniversalInputManager.UsePhoneUI;
			m_setFilterTray.SetButtonShown(buttonShown);
		}
		if (m_craftingTray == null)
		{
			AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "CraftingTray_phone.prefab:bd4719b05f6f24870be20fa595b2032a" : "CraftingTray.prefab:dae9f103e23a53f459baeef392daa984", OnCraftingTrayLoaded, null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		m_craftingModeButton.gameObject.SetActive(value: true);
		m_craftingModeButton.AddEventListener(UIEventType.RELEASE, OnCraftingModeButtonReleased);
		if (m_setFilterTray != null && show && !m_setFilterTrayInitialized)
		{
			m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_ALL_STANDARD_CARDS"), m_allSetsTexture, m_allSetsIconOffset, SetFilterCallback, new List<TAG_CARD_SET>(GameUtils.GetStandardSets()), null, PegasusShared.FormatType.FT_STANDARD, isAllStandard: true, tooltipActive: true, GameStrings.Get("GLUE_TOOLTIP_HEADER_ALL_STANDARD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_ALL_STANDARD_CARDS"));
			m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_WILD_CARDS"), m_wildSetsTexture, m_wildSetsIconOffset, SetFilterCallback, new List<TAG_CARD_SET>(GameUtils.GetAllWildPlayableSets()), null, PegasusShared.FormatType.FT_WILD, isAllStandard: false, tooltipActive: true, GameStrings.Get("GLUE_TOOLTIP_HEADER_WILD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_WILD_CARDS"));
			m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_VANILLA_CARDS"), m_classicSetsTexture, m_classicSetsIconOffset, SetFilterCallback, new List<TAG_CARD_SET> { TAG_CARD_SET.VANILLA }, null, PegasusShared.FormatType.FT_CLASSIC, isAllStandard: false, tooltipActive: true, GameStrings.Get("GLUE_TOOLTIP_HEADER_VANILLA_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_VANILLA_CARDS"));
			List<int> list = (from s in GameDbf.GetIndex().GetCardsWithFeaturedCardsEvent()
				select GameDbf.GetIndex().GetCardRecord(s) into c
				where SpecialEventManager.Get().IsEventActive(c.FeaturedCardsEvent, activeIfDoesNotExist: false)
				select c.ID).ToList();
			if (list.Any())
			{
				SetFilterItem setFilterItem = m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_NEW_CARDS"), m_featuredCardsTexture, m_featuredCardsIconOffset, FeaturedCardsSetFilterCallback, null, list, PegasusShared.FormatType.FT_STANDARD);
				m_currentActiveFeaturedCardsEvent = GameDbf.Card.GetRecord(list.First()).FeaturedCardsEvent;
				StartCoroutine(SetIconFxIfFeaturedCardsEventNotSeen(setFilterItem, m_currentActiveFeaturedCardsEvent));
				StartCoroutine(SetFeaturedCardsSetFilterGlowIfNotSeen(m_currentActiveFeaturedCardsEvent));
			}
			PopulateSetFilters();
			m_setFilterTrayInitialized = true;
		}
		else if (!show)
		{
			ShowSets(new List<TAG_CARD_SET>(GameUtils.GetStandardSets()));
		}
		ShowAppropriateSetFilters();
		if (show)
		{
			m_manaTabManager.SetUpTabs();
		}
	}

	private void AddDuelsSetFilters()
	{
		foreach (TAG_CARD_SET duelsSet in DuelsConfig.GetDuelsSets())
		{
			AddSetFilter(duelsSet);
		}
	}

	private void AddSetFilters(bool isWild)
	{
		foreach (TAG_CARD_SET item in from cardSetId in CollectionManager.Get().GetDisplayableCardSets()
			where GameUtils.IsWildCardSet(cardSetId) == isWild && !GameUtils.IsClassicCardSet(cardSetId) && (!GameUtils.IsLegacySet(cardSetId) || cardSetId == TAG_CARD_SET.LEGACY)
			orderby GameDbf.GetIndex().GetCardSet(cardSetId)?.ReleaseOrder ?? 0 descending
			select cardSetId)
		{
			AddSetFilter(item);
		}
	}

	private void AddSetFilter(TAG_CARD_SET cardSet)
	{
		List<TAG_CARD_SET> list = new List<TAG_CARD_SET>();
		if (cardSet == TAG_CARD_SET.LEGACY)
		{
			list.AddRange(GameUtils.GetLegacySets());
		}
		else if (!GameUtils.IsLegacySet(cardSet))
		{
			list.Add(cardSet);
		}
		string iconTextureAssetRef = null;
		UnityEngine.Vector2? iconOffset = null;
		CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(cardSet);
		if (cardSet2 != null)
		{
			iconTextureAssetRef = cardSet2.FilterIconTexture;
			iconOffset = new UnityEngine.Vector2((float)cardSet2.FilterIconOffsetX, (float)cardSet2.FilterIconOffsetY);
		}
		m_setFilterTray.AddItem(GameStrings.GetCardSetNameShortened(cardSet), iconTextureAssetRef, iconOffset, SetFilterCallback, list, GameUtils.GetCardSetFormat(cardSet));
	}

	public void PopulateSetFilters(bool shouldReset = false)
	{
		if (shouldReset)
		{
			m_setFilterTray.RemoveAllItems();
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_WILD_CARDS"), m_wildSetsTexture, m_wildSetsIconOffset, SetFilterCallback, new List<TAG_CARD_SET>(GameUtils.GetAllWildPlayableSets()), null, PegasusShared.FormatType.FT_WILD, isAllStandard: false, tooltipActive: true, GameStrings.Get("GLUE_TOOLTIP_HEADER_WILD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_WILD_CARDS"));
			m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_ALL_SETS"), PegasusShared.FormatType.FT_STANDARD);
			AddDuelsSetFilters();
		}
		else
		{
			m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_STANDARD_SETS"), PegasusShared.FormatType.FT_STANDARD);
			AddSetFilters(isWild: false);
			m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_WILD_SETS"), PegasusShared.FormatType.FT_WILD);
			AddSetFilters(isWild: true);
			if (CollectionManager.Get().GetDisplayableCardSets().Contains(TAG_CARD_SET.SLUSH))
			{
				AddSetFilter(TAG_CARD_SET.SLUSH);
			}
		}
		if (Options.GetInRankedPlayMode() && !SceneMgr.Get().IsInDuelsMode())
		{
			if (!m_setFilterTray.SelectFirstItemWithFormat(Options.GetFormatType()))
			{
				m_setFilterTray.SelectFirstItem();
			}
		}
		else
		{
			m_setFilterTray.SelectFirstItem();
		}
	}

	private long GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId gameSaveSubkeyId)
	{
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.COLLECTION_MANAGER, gameSaveSubkeyId, out List<long> values);
		long result = 0L;
		if (values != null && values.Any())
		{
			result = values.First();
		}
		return result;
	}

	private IEnumerator SetIconFxIfFeaturedCardsEventNotSeen(SetFilterItem setFilterItem, string currentActiveFeaturedCardsEvent)
	{
		while (!m_isReady)
		{
			yield return null;
		}
		if (m_gameSaveDataReady)
		{
			long lastSeenFeaturedCardsEvent = GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_ITEM);
			long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
			if (eventIdFromEventName != -1 && eventIdFromEventName != lastSeenFeaturedCardsEvent)
			{
				setFilterItem.SetIconFxActive(active: true);
			}
		}
	}

	private IEnumerator SetFeaturedCardsSetFilterGlowIfNotSeen(string currentActiveFeaturedCardsEvent)
	{
		while (!m_isReady)
		{
			yield return null;
		}
		if (!m_gameSaveDataReady)
		{
			yield break;
		}
		long lastSeenFeaturedCardsEvent = GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
		if (eventIdFromEventName != -1 && eventIdFromEventName != lastSeenFeaturedCardsEvent)
		{
			m_setFilterTray.SetFilterButtonGlowActive(active: true);
			if (m_filterButtonGlow != null)
			{
				m_filterButtonGlow.SetActive(value: true);
			}
		}
	}

	private void SetLastSeenFeaturedCardsEvent(string currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId subkeyId)
	{
		if (!string.IsNullOrEmpty(currentActiveFeaturedCardsEvent))
		{
			long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
			if (eventIdFromEventName != -1 && GetLastSeenFeaturedCardsEvent(subkeyId) != eventIdFromEventName)
			{
				GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.COLLECTION_MANAGER, subkeyId, eventIdFromEventName));
			}
		}
	}

	protected override void OnSearchDeactivated(string oldSearchText, string newSearchText)
	{
		OnSearchDeactivated_Internal(oldSearchText, newSearchText, updateManaFilterToMatchSearchText: true);
	}

	private void OnSearchDeactivated_Internal(string oldSearchText, string newSearchText, bool updateManaFilterToMatchSearchText)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			EnableInput(enable: true);
		}
		if (oldSearchText == newSearchText)
		{
			OnSearchFilterComplete();
			return;
		}
		if (m_currentViewMode == CollectionUtils.ViewMode.CARDS && !m_craftingTray.IsShown() && newSearchText.ToLower() == GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING"))
		{
			m_searchTriggeredCraftingTray = true;
			ShowCraftingTray();
		}
		else if (m_craftingTray.IsShown() && newSearchText.ToLower() != GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING") && m_searchTriggeredCraftingTray && m_craftingTray != null)
		{
			m_craftingTray.Hide();
			m_searchTriggeredCraftingTray = false;
		}
		NotifyFilterUpdate(m_searchFilterListeners, !string.IsNullOrEmpty(newSearchText), newSearchText);
		if (updateManaFilterToMatchSearchText)
		{
			UpdateManaFilterToMatchSearchText(newSearchText, transitionPage: false);
		}
		m_pageManager.ChangeSearchTextFilter(newSearchText, base.OnSearchFilterComplete, null);
	}

	protected override void OnSearchCleared(bool transitionPage)
	{
		if (m_searchTriggeredCraftingTray && m_craftingTray != null)
		{
			m_craftingTray.Hide();
			m_searchTriggeredCraftingTray = false;
		}
		NotifyFilterUpdate(m_searchFilterListeners, active: false, "");
		m_pageManager.ChangeSearchTextFilter("", transitionPage);
		if (m_manaFilterIsFromSearchText)
		{
			m_manaTabManager.ClearFilter();
		}
	}

	public void ShowTavernBrawlDeck(long deckID)
	{
		CollectionDeckTray.Get().GetDecksContent().SetEditingTraySection(0);
		CollectionDeckTray.Get().SetTrayMode(DeckTray.DeckContentTypes.Decks);
		RequestContentsToShowDeck(deckID);
	}

	public void ShowDuelsDeckHeader()
	{
		CollectionDeckTray.Get().GetDecksContent().SetEditingTraySection(0);
		CollectionDeckTray.Get().GetDecksContent().GetEditingTraySection()
			.m_deckBox.HideBanner();
	}

	private void DoEnterCollectionManagerEvents()
	{
		if (CollectionManager.Get().HasVisitedCollection() || IsSpecialOneDeckMode())
		{
			EnableInput(enable: true);
			OpenBookImmediately();
		}
		else
		{
			CollectionManager.Get().SetHasVisitedCollection(enable: true);
			EnableInput(enable: false);
			StartCoroutine(OpenBookWhenReady());
		}
	}

	private void OpenBookImmediately()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.COLLECTION);
		}
		StartCoroutine(SetBookToOpen());
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			StartCoroutine(ShowCollectionTipsIfNeeded());
		}
	}

	private IEnumerator OpenBookWhenReady()
	{
		while (CollectionManager.Get().IsWaitingForBoxTransition())
		{
			yield return null;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			PresenceMgr.Get().SetStatus(Global.PresenceStatus.COLLECTION);
		}
		m_pageManager.OnBookOpening();
		StartCoroutine(DoBookOpeningAnimations());
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			StartCoroutine(ShowCollectionTipsIfNeeded());
		}
	}

	private void ShowCraftingTipIfNeeded()
	{
		if (!Options.Get().GetBool(Option.TIP_CRAFTING_UNLOCKED, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("CollectionManagerDisplay.ShowCraftingTipIfNeeded"))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_DISENCHANT_31"), "VO_INNKEEPER_DISENCHANT_31.prefab:4a0246488dc2d8146b1db88de5c603ff");
			Options.Get().SetBool(Option.TIP_CRAFTING_UNLOCKED, val: true);
		}
	}

	private Vector3 GetNewDeckPosition()
	{
		Vector3 vector = (UniversalInputManager.UsePhoneUI ? new Vector3(25.7f, 2.6f, 0f) : new Vector3(17.5f, 0f, 0f));
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetNewDeckButtonPosition() - vector;
		}
		return new Vector3(0f, 0f, 0f);
	}

	private Vector3 GetLastDeckPosition()
	{
		Vector3 vector = (UniversalInputManager.UsePhoneUI ? new Vector3(15.8f, 0f, 6f) : new Vector3(9.6f, 0f, 3f));
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetLastUsedTraySection().transform.position - vector;
		}
		return new Vector3(0f, 0f, 0f);
	}

	private Vector3 GetMiddleDeckPosition()
	{
		int index = 4;
		Vector3 vector = (UniversalInputManager.UsePhoneUI ? new Vector3(15.8f, 0f, 6f) : new Vector3(9.6f, 0f, 3f));
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetTraySection(index).transform.position - vector;
		}
		return new Vector3(0f, 0f, 0f);
	}

	private void ShowSetRotationNewDeckIndicator(float f)
	{
		string text = "";
		Vector3 position;
		if (CollectionManager.Get().GetNumberOfWildDecks() >= 27)
		{
			text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL15");
			position = GetMiddleDeckPosition();
		}
		else
		{
			if (CollectionManager.Get().GetNumberOfWildDecks() <= 0)
			{
				return;
			}
			if (CollectionManager.Get().GetNumberOfStandardDecks() > 0)
			{
				text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL14");
				position = GetLastDeckPosition();
			}
			else
			{
				text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL10");
				CollectionDeckTray.Get().GetDecksContent().m_newDeckButton.m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				position = GetNewDeckPosition();
			}
		}
		m_createDeckNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, position, m_editDeckTutorialBone.localScale, text);
		if (m_createDeckNotification != null)
		{
			m_createDeckNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			m_createDeckNotification.PulseReminderEveryXSeconds(3f);
		}
	}

	public IEnumerator ShowCollectionTipsIfNeeded()
	{
		while (CollectionManager.Get().IsWaitingForBoxTransition())
		{
			yield return null;
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial() && UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, "CollectionManagerDisplay.ShowCollectionTipsIfNeeded:ShowSetRotationTutorial"))
		{
			int deckCount = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
			CollectionDeckTray deckTray = CollectionDeckTray.Get();
			while (deckTray.IsUpdatingTrayMode() || !deckTray.GetDecksContent().IsDoneEntering())
			{
				yield return null;
			}
			if (deckCount >= 27)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, GameStrings.Get("GLUE_COLLECTION_TUTORIAL11"), "VO_INNKEEPER_Male_Dwarf_FULL_DECKS_06.prefab:21adedb0a5456c24da1b2918c3d04e5a");
				ShowSetRotationNewDeckIndicator(0f);
			}
			else if (deckCount > (int)m_onscreenDecks)
			{
				deckTray.m_scrollbar.SetScroll(1f, ShowSetRotationNewDeckIndicator, iTween.EaseType.easeOutBounce, 0.75f, blockInputWhileScrolling: true);
			}
			else
			{
				ShowSetRotationNewDeckIndicator(0f);
			}
			yield break;
		}
		if (Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, defaultVal: false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER_AFTER_PRACTICE, val: true);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_COLLECTIONMANAGER, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("UserAttentionManager.CanShowAttentionGrabber:" + Option.HAS_SEEN_COLLECTIONMANAGER))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_WELCOME"), "VO_INNKEEPER_Male_Dwarf_CM_WELCOME_23.prefab:c8afdeaaf2189eb42aad9d29f6a97994");
			Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER, val: true);
			yield return new WaitForSeconds(3.5f);
		}
		else
		{
			yield return new WaitForSeconds(1f);
		}
		if (!Options.Get().GetBool(Option.HAS_STARTED_A_DECK, defaultVal: false) && UserAttentionManager.CanShowAttentionGrabber("CollectionManagerDisplay.ShowCollectionTipsIfNeeded:" + Option.HAS_STARTED_A_DECK))
		{
			m_deckHelpPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, m_editDeckTutorialBone.position, m_editDeckTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL07"));
			m_deckHelpPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			m_deckHelpPopup.PulseReminderEveryXSeconds(3f);
		}
	}

	private void ShowDeckTemplateTipsIfNeeded()
	{
		bool flag = m_deckHelpPopup != null && m_deckHelpPopup.gameObject != null;
		Notification deckHelpPopup = CollectionDeckTray.Get().GetCardsContent().GetDeckHelpPopup();
		bool flag2 = deckHelpPopup != null && deckHelpPopup.gameObject != null;
		bool flag3 = m_createDeckNotification != null && m_createDeckNotification.gameObject != null;
		bool flag4 = (m_craftingTray != null && m_craftingTray.IsShown()) || CraftingManager.GetIsInCraftingMode() || DeckHelper.Get().IsActive() || flag || flag2 || flag3 || SceneMgr.Get().IsInDuelsMode() || CollectionDeckTray.Get().GetDecksContent().IsShowingDeckOptions;
		DeckTrayDeckTileVisual firstInvalidCard = CollectionDeckTray.Get().GetCardsContent().GetFirstInvalidCard();
		if (firstInvalidCard != null && !flag4)
		{
			if (m_showingDeckTemplateTips || (m_currentViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE && (CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards || !CollectionDeckTray.Get().GetCardsContent().HasFinishedEntering())))
			{
				return;
			}
			string text = "";
			if (firstInvalidCard.GetSlot().Owned)
			{
				if (firstInvalidCard.GetSlot().Owned && Options.Get().GetBool(Option.HAS_SEEN_INVALID_ROTATED_CARD))
				{
					return;
				}
				text = (CollectionManager.Get().ShouldAccountSeeStandardWild() ? GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS") : GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"));
			}
			else
			{
				if (Options.Get().GetBool(Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD) || !UserAttentionManager.CanShowAttentionGrabber("CollectionManagerDisplay.ShowDeckTemplateTipsIfNeeded:" + Option.HAS_SEEN_DECK_TEMPLATE_GHOST_CARD))
				{
					return;
				}
				if (m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
				{
					if ((bool)UniversalInputManager.UsePhoneUI)
					{
						firstInvalidCard = m_deckTemplatePickerPhone.m_phoneTray.GetCardsContent().GetFirstInvalidCard();
						if (firstInvalidCard == null)
						{
							Debug.LogError("Phone Template Tray and CollectionDeckTray mismatch. Missing invalid card on Template.");
							return;
						}
					}
					text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1");
					if (m_deckTemplateTipWaitTime < 0.5f)
					{
						m_deckTemplateTipWaitTime += Time.deltaTime;
						return;
					}
				}
				else
				{
					text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2");
					if (m_deckTemplateTipWaitTime < 1f)
					{
						m_deckTemplateTipWaitTime += Time.deltaTime;
						return;
					}
				}
			}
			float num = -60f;
			Vector3 relativePosition = OverlayUI.Get().GetRelativePosition(firstInvalidCard.transform.position, Box.Get().m_Camera.GetComponent<Camera>(), OverlayUI.Get().m_heightScale.m_Center);
			Vector3 scale;
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				relativePosition.x -= 95.395f;
				relativePosition.z -= 0.25f;
				scale = 27.5f * Vector3.one;
				if (relativePosition.z < num)
				{
					relativePosition.z = num;
				}
			}
			else
			{
				relativePosition.x -= 50.5f;
				relativePosition.z -= 0.25f;
				scale = NotificationManager.NOTIFICATITON_WORLD_SCALE;
			}
			if (m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
			{
				m_deckTemplateCardReplacePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, relativePosition, scale, text, convertLegacyPosition: false);
				if (m_deckTemplateCardReplacePopup != null)
				{
					m_deckTemplateCardReplacePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
					NotificationManager.Get().DestroyNotification(m_deckTemplateCardReplacePopup, 3.5f);
				}
			}
			else
			{
				m_deckTemplateCardReplacePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, relativePosition, scale, text, convertLegacyPosition: false);
				if (m_deckTemplateCardReplacePopup != null)
				{
					m_deckTemplateCardReplacePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
					m_deckTemplateCardReplacePopup.PulseReminderEveryXSeconds(3f);
				}
			}
			m_deckTemplateTipWaitTime = 0f;
			m_showingDeckTemplateTips = true;
		}
		else
		{
			if (m_showingDeckTemplateTips)
			{
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"));
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"));
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"));
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"));
			}
			m_deckTemplateTipWaitTime = 0f;
			m_showingDeckTemplateTips = false;
		}
	}

	protected override void OnSwitchViewModeResponse(bool triggerResponse, CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode newMode, CollectionUtils.ViewModeData userdata)
	{
		base.OnSwitchViewModeResponse(triggerResponse, prevMode, newMode, userdata);
		EnableSetAndManaFiltersByViewMode(newMode);
	}

	private void EnableSetAndManaFiltersByViewMode(CollectionUtils.ViewMode viewMode)
	{
		bool flag = viewMode == CollectionUtils.ViewMode.CARDS;
		EnableSetAndManaFilters(flag);
	}

	private void EnableSetAndManaFilters(bool enabled)
	{
		if ((bool)UniversalInputManager.UsePhoneUI)
		{
			m_craftingModeButton.Enable(enabled);
		}
		m_manaTabManager.Enabled = enabled;
		if (m_setFilterTray != null)
		{
			m_setFilterTray.SetButtonEnabled(enabled);
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				m_setFilterTray.gameObject.SetActive(enabled);
			}
		}
		m_search.SetEnabled(enabled: true);
	}

	private void OnSetFilterButtonPressed()
	{
		SetLastSeenFeaturedCardsEvent(m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		m_setFilterTray.SetFilterButtonGlowActive(active: false);
	}

	private void OnPhoneFilterButtonPressed()
	{
		SetLastSeenFeaturedCardsEvent(m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		m_filterButtonGlow.SetActive(value: false);
	}

	private void OnCraftingTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.SetActive(value: false);
		m_craftingTray = go.GetComponent<CraftingTray>();
		go.transform.parent = m_craftingTrayShownBone.transform.parent;
		go.transform.localPosition = m_craftingTrayHiddenBone.transform.localPosition;
		go.transform.localScale = m_craftingTrayHiddenBone.transform.localScale;
		m_pageManager.UpdateMassDisenchant();
	}

	private void OnCraftingModeButtonReleased(UIEvent e)
	{
		if (m_craftingTray.IsShown())
		{
			m_craftingTray.Hide();
		}
		else
		{
			ShowCraftingTray();
		}
	}

	public void ShowCraftingTray(bool? includeUncraftable = null, bool? showOnlyGolden = null, bool? showOnlyDiamond = null, bool updatePage = true)
	{
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			CollectionDeckTrayDeckListContent decksContent = collectionDeckTray.GetDecksContent();
			if (decksContent != null)
			{
				decksContent.CancelRenameEditingDeck();
			}
		}
		HideDeckHelpPopup();
		m_craftingTray.gameObject.SetActive(value: true);
		m_craftingTray.Show(includeUncraftable, showOnlyGolden, showOnlyDiamond, updatePage);
		Hashtable args = iTween.Hash("position", m_craftingTrayShownBone.transform.localPosition, "isLocal", true, "time", 0.25f, "easeType", iTween.EaseType.easeOutBounce);
		iTween.Stop(m_craftingTray.gameObject);
		iTween.MoveTo(m_craftingTray.gameObject, args);
		m_craftingModeButton.ShowActiveGlow(show: true);
		ShowAppropriateSetFilters();
	}

	public void HideCraftingTray()
	{
		m_craftingTray.gameObject.SetActive(value: true);
		Hashtable args = iTween.Hash("position", m_craftingTrayHiddenBone.transform.localPosition, "isLocal", true, "time", 0.25f, "easeType", iTween.EaseType.easeOutBounce, "oncomplete", (Action<object>)delegate
		{
			m_craftingTray.gameObject.SetActive(value: false);
		});
		iTween.Stop(m_craftingTray.gameObject);
		iTween.MoveTo(m_craftingTray.gameObject, args);
		m_craftingModeButton.ShowActiveGlow(show: false);
		ShowAppropriateSetFilters();
	}

	public void ShowConvertTutorial(UserAttentionBlocker blocker)
	{
		if (UserAttentionManager.CanShowAttentionGrabber(blocker, "CollectionManagerDisplay.ShowConvertTutorial"))
		{
			m_showConvertTutorialCoroutine = ShowConvertTutorialCoroutine(blocker);
			StartCoroutine(m_showConvertTutorialCoroutine);
		}
	}

	private IEnumerator ShowConvertTutorialCoroutine(UserAttentionBlocker blocker)
	{
		if (m_createDeckNotification != null)
		{
			NotificationManager.Get().DestroyNotification(m_createDeckNotification, 0.25f);
		}
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		while (deckTray.IsUpdatingTrayMode() || !deckTray.GetDecksContent().IsDoneEntering())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		if (ViewModeHasVisibleDeckList())
		{
			m_convertTutorialPopup = NotificationManager.Get().CreatePopupText(blocker, m_convertDeckTutorialBone.position, m_convertDeckTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL12"));
			if (m_convertTutorialPopup != null)
			{
				m_convertTutorialPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				m_convertTutorialPopup.PulseReminderEveryXSeconds(3f);
			}
			m_showConvertTutorialCoroutine = null;
		}
	}

	public void HideConvertTutorial()
	{
		if (m_showConvertTutorialCoroutine != null)
		{
			StopCoroutine(m_showConvertTutorialCoroutine);
			m_showConvertTutorialCoroutine = null;
		}
		if (m_convertTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(m_convertTutorialPopup, 0.25f);
		}
	}

	public void ShowSetFilterTutorial(UserAttentionBlocker blocker)
	{
		if (UserAttentionManager.CanShowAttentionGrabber(blocker, "CollectionManagerDisplay.ShowSetFilterTutorial"))
		{
			m_showSetFilterTutorialCoroutine = ShowSetFilterTutorialCoroutine(blocker);
			StartCoroutine(m_showSetFilterTutorialCoroutine);
		}
	}

	private IEnumerator ShowSetFilterTutorialCoroutine(UserAttentionBlocker blocker)
	{
		if (m_setFilterTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(m_setFilterTutorialPopup, 0f);
		}
		m_setFilterTutorialPopup = NotificationManager.Get().CreatePopupText(blocker, m_setFilterTutorialBone.position, m_setFilterTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL17"));
		if (m_setFilterTutorialPopup != null)
		{
			m_setFilterTutorialPopup.ShowPopUpArrow(UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.Up : Notification.PopUpArrowDirection.LeftDown);
			m_setFilterTutorialPopup.PulseReminderEveryXSeconds(3f);
		}
		yield return new WaitForSeconds(6f);
		HideSetFilterTutorial();
	}

	public void HideSetFilterTutorial()
	{
		if (m_showSetFilterTutorialCoroutine != null)
		{
			StopCoroutine(m_showSetFilterTutorialCoroutine);
			m_showSetFilterTutorialCoroutine = null;
		}
		if (m_setFilterTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(m_setFilterTutorialPopup, 0.25f);
		}
	}

	public void ShowStandardInfoTutorial(UserAttentionBlocker blocker)
	{
		NotificationManager.Get().CreateInnkeeperQuote(blocker, GameStrings.Get("GLUE_COLLECTION_TUTORIAL13"), "VO_INNKEEPER_Male_Dwarf_STANDARD_WELCOME3_14.prefab:51e1d835435b64542b9a77944e00cc19");
	}

	public void CheckClipboardAndPromptPlayerToPaste()
	{
		if (!CheckIfClipboardNotificationHasBeenShown())
		{
			return;
		}
		if (!CheckClipboardAndGetValidityMessaging(out var message))
		{
			if (message != string.Empty)
			{
				CollectionInputMgr.AlertPlayerOnInvalidDeckPaste(message);
			}
			return;
		}
		string text = GameStrings.Get("GLUE_COLLECTION_DECK_VALID_PASTE_BODY");
		string headerText = GameStrings.Get("GLUE_COLLECTION_DECK_VALID_PASTE_HEADER");
		if (CollectionManager.Get().IsInEditMode() && CollectionManager.Get().GetEditedDeck().GetTotalCardCount() > 0)
		{
			text = GameStrings.Get("GLUE_COLLECTION_DECK_OVERWRITE_BODY");
			headerText = GameStrings.Get("GLUE_COLLECTION_DECK_OVERWRITE_HEADER");
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = headerText,
			m_text = text,
			m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_SAVE_ANYWAY"),
			m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_FINISH_FOR_ME"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CANCEL)
				{
					RejectDeckFromClipboard();
				}
				else
				{
					CreateDeckFromClipboard(m_cachedShareableDeck);
				}
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	private bool CheckIfClipboardNotificationHasBeenShown()
	{
		if (PlatformSettings.OS == OSCategory.iOS && !Options.Get().GetBool(Option.HAS_SEEN_CLIPBOARD_NOTIFICATION, defaultVal: false))
		{
			if (DialogManager.Get().ShowingDialog())
			{
				return false;
			}
			string headerText = GameStrings.Get("GLUE_COLLECTION_DECK_CLIPBOARD_ACCESS_HEADER");
			string text = GameStrings.Get("GLUE_COLLECTION_DECK_CLIPBOARD_ACCESS_BODY");
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = headerText,
				m_text = text,
				m_showAlertIcon = false,
				m_responseCallback = delegate
				{
					Options.Get().SetBool(Option.HAS_SEEN_CLIPBOARD_NOTIFICATION, val: true);
				}
			};
			DialogManager.Get().ShowPopup(info);
			return false;
		}
		return true;
	}

	public void PasteFromClipboardIfValidOrShowStatusMessage()
	{
		if (CheckIfClipboardNotificationHasBeenShown())
		{
			if (!CheckClipboardAndGetValidityMessaging(out var message))
			{
				UIStatus.Get().AddInfo(message);
				return;
			}
			ClipboardUtils.CopyToClipboard(string.Empty);
			CreateDeckFromClipboard(m_cachedShareableDeck);
		}
	}

	private bool CheckClipboardAndGetValidityMessaging(out string message)
	{
		message = string.Empty;
		ShareableDeck shareableDeck = ShareableDeck.DeserializeFromClipboard();
		if (shareableDeck == null)
		{
			return false;
		}
		if (DialogManager.Get().ShowingDialog())
		{
			if ((m_cachedShareableDeck != null && m_cachedShareableDeck.Equals(shareableDeck)) || !CanPasteShareableDeck(shareableDeck))
			{
				return false;
			}
			DialogManager.Get().ClearAllImmediately();
		}
		m_cachedShareableDeck = shareableDeck;
		return CanPasteShareableDeck(m_cachedShareableDeck, out message);
	}

	private bool CanPasteShareableDeck(ShareableDeck shareableDeck)
	{
		string alertMessage;
		return CanPasteShareableDeck(shareableDeck, out alertMessage);
	}

	private bool CanPasteShareableDeck(ShareableDeck shareableDeck, out string alertMessage)
	{
		alertMessage = string.Empty;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER && !CollectionManager.Get().IsInEditMode() && !CollectionDeckTray.Get().m_decksContent.CanShowNewDeckButton())
		{
			return false;
		}
		if (SceneMgr.Get().IsInTavernBrawlMode() && !TavernBrawlDisplay.Get().IsInDeckEditMode() && HeroPickerDisplay.Get() == null)
		{
			return false;
		}
		if (SceneMgr.Get().IsInDuelsMode() && !DuelsConfig.CanImportDecks())
		{
			return false;
		}
		if (CraftingTray.Get().IsShown())
		{
			return false;
		}
		if (!CollectionManager.Get().ShouldAccountSeeStandardWild() && shareableDeck.FormatType == PegasusShared.FormatType.FT_WILD)
		{
			alertMessage = GameStrings.Get("GLUE_COLLECTION_DECK_WILD_NOT_UNLOCKED");
			return false;
		}
		string text = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId);
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(text).GetClass();
		List<CollectibleCard> bestHeroesIOwn = CollectionManager.Get().GetBestHeroesIOwn(@class);
		ScenarioDbId scenarioDbId = ScenarioDbId.INVALID;
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.TAVERN_BRAWL)
		{
			scenarioDbId = (ScenarioDbId)TavernBrawlManager.Get().CurrentMission().missionId;
		}
		if (scenarioDbId != 0)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenarioDbId);
			if (record != null)
			{
				foreach (ClassExclusionsDbfRecord classExclusion in record.ClassExclusions)
				{
					if (classExclusion.ClassId == (int)@class)
					{
						return false;
					}
				}
			}
		}
		if (!bestHeroesIOwn.Any())
		{
			alertMessage = GameStrings.Get("GLUE_COLLECTION_DECK_HERO_NOT_UNLOCKED");
			return false;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (CollectionManager.Get().IsInEditMode())
		{
			TAG_CLASS class2 = DefLoader.Get().GetEntityDef(editedDeck.HeroCardID).GetClass();
			if (@class != class2)
			{
				return false;
			}
			if (editedDeck.GetShareableDeck().Equals(m_cachedShareableDeck))
			{
				return false;
			}
		}
		return true;
	}

	private void CreateDeckFromClipboard(ShareableDeck shareableDeck)
	{
		bool num = SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER;
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(shareableDeck.HeroCardDbId).GetClass();
		NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(@class);
		string heroCardID = ((favoriteHero != null) ? favoriteHero.Name : CollectionManager.GetHeroCardId(@class, CardHero.HeroType.VANILLA));
		if (num)
		{
			Options.SetFormatType(shareableDeck.FormatType);
		}
		string customDeckName = null;
		if (!string.IsNullOrEmpty(shareableDeck.DeckName))
		{
			customDeckName = shareableDeck.DeckName;
		}
		if (!CollectionManager.Get().IsInEditMode())
		{
			CollectionDeckTray.Get().GetDecksContent().CreateNewDeckFromUserSelection(@class, heroCardID, customDeckName, DeckSourceType.DECK_SOURCE_TYPE_PASTED_DECK, shareableDeck.Serialize(includeComments: false));
			CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreatedFromClipboard);
			CollectionManager.Get().RemoveDeckCreatedListener(OnDeckCreatedByPlayer);
			if (HeroPickerDisplay.Get() != null && HeroPickerDisplay.Get().IsShown())
			{
				DeckPickerTrayDisplay.Get().SkipHeroSelectionAndCloseTray();
			}
			return;
		}
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck.GetClass() != @class)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_TOOLTIP_HEADLINE"),
				m_text = GameStrings.Get("GLUE_COLLECTION_DECK_PASTE_INVALID_CLASS_BODY"),
				m_confirmText = GameStrings.Get("GLOBAL_OKAY"),
				m_showAlertIcon = true,
				m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM
			};
			DialogManager.Get().ShowPopup(info);
		}
		else
		{
			OnDeckCreatedFromClipboard(editedDeck.ID);
		}
	}

	private void OnDeckCreatedFromClipboard(long deckId)
	{
		CollectionManager.Get().RemoveDeckCreatedListener(OnDeckCreatedFromClipboard);
		CollectionManager.Get().RegisterDeckCreatedListener(OnDeckCreatedByPlayer);
		bool flag = CollectionManager.Get().IsInEditMode();
		if (GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			DeckTemplatePicker deckTemplatePicker = (UniversalInputManager.UsePhoneUI ? GetPhoneDeckTemplateTray() : m_pageManager.GetDeckTemplatePicker());
			if (deckTemplatePicker != null)
			{
				Navigation.RemoveHandler(deckTemplatePicker.OnNavigateBack);
			}
			if ((bool)UniversalInputManager.UsePhoneUI)
			{
				StartCoroutine(deckTemplatePicker.EnterDeckPhone());
			}
		}
		if (CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards)
		{
			CollectionDeckTray.Get().RegisterModeSwitchedListener(OnCollectionDeckTrayModeSwitched);
			ShowDeck(deckId, !flag, showDeckTemplatePage: false, CollectionUtils.ViewMode.CARDS);
		}
		else
		{
			ShowDeck(deckId, !flag, showDeckTemplatePage: false, CollectionUtils.ViewMode.CARDS);
			OnCollectionDeckTrayModeSwitched();
		}
	}

	private void OnCollectionDeckTrayModeSwitched()
	{
		CollectionDeckTray.Get().UnregisterModeSwitchedListener(OnCollectionDeckTrayModeSwitched);
		if (m_cachedShareableDeck != null)
		{
			CollectionInputMgr.PasteDeckInEditModeFromShareableDeck(m_cachedShareableDeck);
		}
		else
		{
			CollectionInputMgr.PasteDeckFromClipboard();
		}
		ClipboardUtils.CopyToClipboard(string.Empty);
		m_cachedShareableDeck = null;
	}

	private void RejectDeckFromClipboard()
	{
		ClipboardUtils.CopyToClipboard(string.Empty);
		m_cachedShareableDeck = null;
	}

	public static bool ShouldShowDeckOptionsMenu()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			return false;
		}
		return true;
	}

	public static bool ShouldShowDeckHeaderInfo()
	{
		return true;
	}

	public static bool IsSpecialOneDeckMode()
	{
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			return SceneMgr.Get().IsInDuelsMode();
		}
		return true;
	}
}
