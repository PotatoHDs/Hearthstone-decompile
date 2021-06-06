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

// Token: 0x02000112 RID: 274
[CustomEditClass]
public class CollectionManagerDisplay : CollectibleDisplay
{
	// Token: 0x0600114B RID: 4427 RVA: 0x0006243C File Offset: 0x0006063C
	public override void Start()
	{
		NetCache.Get().RegisterScreenCollectionManager(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		CollectionManager.Get().RegisterCollectionNetHandlers();
		CollectionManager.Get().RegisterCollectionLoadedListener(new CollectionManager.DelOnCollectionLoaded(base.OnCollectionLoaded));
		CollectionManager.Get().RegisterCollectionChangedListener(new CollectionManager.DelOnCollectionChanged(this.OnCollectionChanged));
		CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedByPlayer));
		CollectionManager.Get().RegisterDeckContentsListener(new CollectionManager.DelOnDeckContents(this.OnDeckContents));
		CollectionManager.Get().RegisterNewCardSeenListener(new CollectionManager.DelOnNewCardSeen(this.OnNewCardSeen));
		CollectionManager.Get().RegisterCardRewardsInsertedListener(new CollectionManager.DelOnCardRewardsInserted(this.OnCardRewardsInserted));
		CardBackManager.Get().SetSearchText(null);
		GameSaveDataManager.Get().Request(GameSaveKeyId.COLLECTION_MANAGER, new GameSaveDataManager.OnRequestDataResponseDelegate(this.OnGameSaveDataReady));
		base.Start();
		if (this.m_setFilterTrayContainer != null)
		{
			this.m_setFilterTray = this.m_setFilterTrayContainer.PrefabGameObject(true).GetComponentsInChildren<SetFilterTray>(true)[0];
			this.m_setFilterTray.m_toggleButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.OnSetFilterButtonPressed();
			});
		}
		if (this.m_filterButton != null)
		{
			this.m_filterButton.m_inactiveFilterButton.AddEventListener(UIEventType.PRESS, delegate(UIEvent e)
			{
				this.OnPhoneFilterButtonPressed();
			});
		}
		bool @bool = Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, false);
		this.ShowAdvancedCollectionManager(@bool);
		if (!@bool)
		{
			Options.Get().RegisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, new Options.ChangedCallback(this.OnShowAdvancedCMChanged));
		}
		this.DoEnterCollectionManagerEvents();
		if (!CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			MusicManager.Get().StartPlaylist(MusicPlaylistType.UI_CollectionManager);
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial(true))
		{
			UserAttentionManager.StartBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		}
		this.SetTavernBrawlTexturesIfNecessary();
		this.SetDuelsTexturesIfNecessary();
		CollectionManager.Get().RequestDeckContentsForDecksWithoutContentsLoaded(null);
		base.StartCoroutine(this.WaitUntilReady());
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x00062618 File Offset: 0x00060818
	protected override void Awake()
	{
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance != null)
		{
			hearthstonePerformance.StartPerformanceFlow(new global::FlowPerformance.SetupConfig
			{
				FlowType = Blizzard.Telemetry.WTCG.Client.FlowPerformance.FlowType.COLLECTION_MANAGER
			});
		}
		this.m_manaTabManager.OnFilterCleared += this.ManaFilterTab_OnManaFilterCleared;
		this.m_manaTabManager.OnManaValueActivated += this.ManaFilterTab_OnManaValueActivated;
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_deckTemplatePickerPhone = AssetLoader.Get().InstantiatePrefab("DeckTemplate_phone.prefab:a8a8fbcd170064edfb0eeac3f836a13b", AssetLoadingOptions.None).GetComponent<DeckTemplatePicker>();
			SlidingTray component = this.m_deckTemplatePickerPhone.GetComponent<SlidingTray>();
			component.m_trayHiddenBone = this.m_deckTemplateHiddenBone.transform;
			component.m_trayShownBone = this.m_deckTemplateShownBone.transform;
		}
		base.Awake();
		base.StartCoroutine(this.InitCollectionWhenReady());
	}

	// Token: 0x0600114D RID: 4429 RVA: 0x000626DC File Offset: 0x000608DC
	protected override void OnDestroy()
	{
		this.m_manaTabManager.OnFilterCleared -= this.ManaFilterTab_OnManaFilterCleared;
		this.m_manaTabManager.OnManaValueActivated -= this.ManaFilterTab_OnManaValueActivated;
		AssetHandle.SafeDispose<Texture>(ref this.m_loadedCorkBackTexture);
		this.m_loadedClassTextures.DisposeValuesAndClear<TAG_CLASS, AssetHandle<Texture>>();
		if (this.m_deckTemplatePickerPhone != null)
		{
			UnityEngine.Object.Destroy(this.m_deckTemplatePickerPhone.gameObject);
			this.m_deckTemplatePickerPhone = null;
		}
		UserAttentionManager.StopBlocking(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS);
		base.OnDestroy();
	}

	// Token: 0x0600114E RID: 4430 RVA: 0x00062760 File Offset: 0x00060960
	private void Update()
	{
		if (HearthstoneApplication.IsInternal())
		{
			if (InputCollection.GetKeyDown(KeyCode.Alpha1))
			{
				base.SetViewMode(CollectionUtils.ViewMode.HERO_SKINS, null);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha2))
			{
				base.SetViewMode(CollectionUtils.ViewMode.CARDS, null);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha3))
			{
				base.SetViewMode(CollectionUtils.ViewMode.CARD_BACKS, null);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha4))
			{
				base.SetViewMode(CollectionUtils.ViewMode.DECK_TEMPLATE, null);
			}
			else if (InputCollection.GetKeyDown(KeyCode.Alpha4))
			{
				this.OnCraftingModeButtonReleased(null);
			}
		}
		this.ShowDeckTemplateTipsIfNeeded();
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x000627D6 File Offset: 0x000609D6
	private void OnApplicationFocus(bool hasFocus)
	{
		if (!hasFocus)
		{
			return;
		}
		base.StartCoroutine(this.OnApplicationFocusCoroutine());
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000627E9 File Offset: 0x000609E9
	private IEnumerator OnApplicationFocusCoroutine()
	{
		yield return null;
		this.CheckClipboardAndPromptPlayerToPaste();
		yield break;
	}

	// Token: 0x06001151 RID: 4433 RVA: 0x000627F8 File Offset: 0x000609F8
	public override void Unload()
	{
		this.m_unloading = true;
		NotificationManager.Get().DestroyAllPopUps();
		this.UnloadAllTextures();
		CollectionDeckTray.Get().GetCardsContent().UnregisterCardTileRightClickedListener(new DeckTrayCardListContent.CardTileRightClicked(this.OnCardTileRightClicked));
		CollectionDeckTray.Get().Unload();
		CollectionInputMgr.Get().Unload();
		CollectionManager.Get().RemoveCollectionLoadedListener(new CollectionManager.DelOnCollectionLoaded(base.OnCollectionLoaded));
		CollectionManager.Get().RemoveCollectionChangedListener(new CollectionManager.DelOnCollectionChanged(this.OnCollectionChanged));
		CollectionManager.Get().RemoveDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedByPlayer));
		CollectionManager.Get().RemoveDeckContentsListener(new CollectionManager.DelOnDeckContents(this.OnDeckContents));
		CollectionManager.Get().RemoveNewCardSeenListener(new CollectionManager.DelOnNewCardSeen(this.OnNewCardSeen));
		CollectionManager.Get().RemoveCardRewardsInsertedListener(new CollectionManager.DelOnCardRewardsInserted(this.OnCardRewardsInserted));
		CollectionManager.Get().RemoveCollectionNetHandlers();
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		Options.Get().UnregisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, new Options.ChangedCallback(this.OnShowAdvancedCMChanged));
		this.m_unloading = false;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x0006291C File Offset: 0x00060B1C
	public override void Exit()
	{
		base.EnableInput(false);
		NotificationManager.Get().DestroyAllPopUps();
		CollectionDeckTray.Get().Exit();
		if (this.m_pageManager != null)
		{
			this.m_pageManager.Exit();
		}
		SceneMgr.Mode mode = SceneMgr.Get().GetPrevMode();
		if (mode == SceneMgr.Mode.GAMEPLAY)
		{
			mode = SceneMgr.Mode.HUB;
		}
		if (!Network.IsLoggedIn() && mode != SceneMgr.Mode.HUB)
		{
			DialogManager.Get().ShowReconnectHelperDialog(null, null);
			mode = SceneMgr.Mode.HUB;
			Navigation.Clear();
		}
		HearthstonePerformance hearthstonePerformance = HearthstonePerformance.Get();
		if (hearthstonePerformance != null)
		{
			hearthstonePerformance.StopCurrentFlow();
		}
		SceneMgr.Get().SetNextMode(mode, SceneMgr.TransitionHandlerType.SCENEMGR, null);
	}

	// Token: 0x06001153 RID: 4435 RVA: 0x000629AC File Offset: 0x00060BAC
	public override void CollectionPageContentsChanged(List<CollectibleCard> cardsToDisplay, CollectibleDisplay.CollectionActorsReadyCallback callback, object callbackData)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1}", new object[]
		{
			this.m_pageManager.GetTransitionPageId(),
			this.m_pageManager.ArePagesTurning()
		});
		bool flag = false;
		if (cardsToDisplay == null)
		{
			Log.CollectionManager.Print("artStacksToDisplay is null!", Array.Empty<object>());
			flag = true;
		}
		else if (cardsToDisplay.Count == 0)
		{
			Log.CollectionManager.Print("artStacksToDisplay has a count of 0!", Array.Empty<object>());
			flag = true;
		}
		if (flag)
		{
			List<CollectionCardActors> actors = new List<CollectionCardActors>();
			callback(actors, callbackData);
			return;
		}
		if (this.m_unloading)
		{
			return;
		}
		foreach (CollectionCardActors collectionCardActors in this.m_previousCardActors)
		{
			collectionCardActors.Destroy();
		}
		this.m_previousCardActors.Clear();
		this.m_previousCardActors = this.m_cardActors;
		this.m_cardActors = new List<CollectionCardActors>();
		long arcaneDustBalance = NetCache.Get().GetArcaneDustBalance();
		new Map<string, CollectionCardActors>();
		foreach (CollectibleCard collectibleCard in cardsToDisplay)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(collectibleCard.CardId);
			using (DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(collectibleCard.CardId, null))
			{
				string input = entityDef.IsHeroSkin() ? ActorNames.GetHeroSkinOrHandActor(entityDef.GetCardType(), collectibleCard.PremiumType) : ActorNames.GetHandActor(entityDef.GetCardType(), collectibleCard.PremiumType, false);
				GameObject gameObject = AssetLoader.Get().InstantiatePrefab(input, AssetLoadingOptions.IgnorePrefabPosition);
				if (gameObject == null)
				{
					Debug.LogError("Unable to load card actor.");
				}
				else
				{
					Actor component = gameObject.GetComponent<Actor>();
					if (component == null)
					{
						Debug.LogError("Actor object does not contain Actor component.");
					}
					else
					{
						component.SetEntityDef(entityDef);
						component.SetCardDef(cardDef);
						component.SetPremium(collectibleCard.PremiumType);
						component.CreateBannedRibbon();
						if (collectibleCard.OwnedCount == 0)
						{
							if (collectibleCard.IsCraftable && arcaneDustBalance >= (long)collectibleCard.CraftBuyCost)
							{
								component.GhostCardEffect(GhostCard.Type.MISSING, collectibleCard.PremiumType, true);
							}
							else if (entityDef.IsHeroSkin() && HeroSkinUtils.CanBuyHeroSkinFromCollectionManager(entityDef.GetCardId()))
							{
								component.GhostCardEffect(GhostCard.Type.MISSING, collectibleCard.PremiumType, true);
							}
							else
							{
								component.MissingCardEffect(true);
							}
						}
						component.UpdateAllComponents();
						this.m_cardActors.Add(new CollectionCardActors(component));
					}
				}
			}
		}
		if (callback != null)
		{
			callback(this.m_cardActors, callbackData);
		}
	}

	// Token: 0x06001154 RID: 4436 RVA: 0x00062C98 File Offset: 0x00060E98
	public void CollectionPageContentsChangedToCardBacks(int pageNumber, int numCardBacksPerPage, CollectibleDisplay.CollectionActorsReadyCallback callback, object callbackData, bool showAll)
	{
		Log.CollectionManager.Print("transitionPageId={0} pagesTurning={1}", new object[]
		{
			this.m_pageManager.GetTransitionPageId(),
			this.m_pageManager.ArePagesTurning()
		});
		CardBackManager cardBackManager = CardBackManager.Get();
		List<CollectionCardActors> result = new List<CollectionCardActors>();
		List<CardBackManager.OwnedCardBack> list = cardBackManager.GetOrderedEnabledCardBacks(!showAll);
		if (list.Count == 0)
		{
			if (callback != null)
			{
				callback(result, callbackData);
			}
			return;
		}
		int num = (pageNumber - 1) * numCardBacksPerPage;
		int count = Mathf.Min(list.Count - num, numCardBacksPerPage);
		list = list.GetRange(num, count);
		int numCardBacksToLoad = list.Count;
		Action<int, CardBackManager.OwnedCardBack, Actor> cbLoadedCallback = delegate(int index, CardBackManager.OwnedCardBack cardBack, Actor actor)
		{
			if (actor != null)
			{
				result[index] = new CollectionCardActors(actor);
				actor.SetCardbackUpdateIgnore(true);
				CollectionCardBack component = actor.GetComponent<CollectionCardBack>();
				if (component != null)
				{
					component.SetCardBackId(cardBack.m_cardBackId);
					component.SetCardBackName(CardBackManager.Get().GetCardBackName(cardBack.m_cardBackId));
				}
				else
				{
					Debug.LogError("CollectionCardBack component does not exist on actor!");
				}
				if (!cardBack.m_owned)
				{
					if (cardBack.m_canBuy)
					{
						actor.GhostCardEffect(GhostCard.Type.MISSING, TAG_PREMIUM.NORMAL, true);
					}
					else
					{
						actor.MissingCardEffect(true);
					}
				}
			}
			int numCardBacksToLoad;
			numCardBacksToLoad--;
			numCardBacksToLoad = numCardBacksToLoad;
			if (numCardBacksToLoad == 0 && callback != null)
			{
				callback(result, callbackData);
			}
		};
		if (this.m_previousCardBackActors != null)
		{
			foreach (CollectionCardActors collectionCardActors in this.m_previousCardBackActors)
			{
				collectionCardActors.Destroy();
			}
			this.m_previousCardBackActors.Clear();
		}
		this.m_previousCardBackActors = this.m_cardBackActors;
		this.m_cardBackActors = new List<CollectionCardActors>();
		for (int i = 0; i < list.Count; i++)
		{
			int currIndex = i;
			CardBackManager.OwnedCardBack cardBackLoad = list[i];
			int cardBackId = cardBackLoad.m_cardBackId;
			result.Add(null);
			if (!cardBackManager.LoadCardBackByIndex(cardBackId, delegate(CardBackManager.LoadCardBackData cardBackData)
			{
				GameObject gameObject = cardBackData.m_GameObject;
				gameObject.transform.parent = this.transform;
				GameObject gameObject2 = gameObject;
				gameObject2.name = gameObject2.name + "_" + cardBackData.m_CardBackIndex;
				Actor component = gameObject.GetComponent<Actor>();
				if (component == null)
				{
					UnityEngine.Object.Destroy(gameObject);
				}
				else
				{
					GameObject cardMesh = component.m_cardMesh;
					component.SetCardbackUpdateIgnore(true);
					component.SetUnlit();
					if (cardMesh != null)
					{
						Material material = cardMesh.GetComponent<Renderer>().GetMaterial();
						if (material.HasProperty("_SpecularIntensity"))
						{
							material.SetFloat("_SpecularIntensity", 0f);
						}
					}
					this.m_cardBackActors.Add(new CollectionCardActors(component));
				}
				cbLoadedCallback(currIndex, cardBackLoad, component);
			}, "Collection_Card_Back.prefab:a208f592a46e4f447b3026e82444177e", null))
			{
				cbLoadedCallback(currIndex, cardBackLoad, null);
			}
		}
	}

	// Token: 0x06001155 RID: 4437 RVA: 0x00062E8C File Offset: 0x0006108C
	public void RequestContentsToShowDeck(long deckID)
	{
		this.m_showDeckContentsRequest = deckID;
		CollectionManager.Get().RequestDeckContents(this.m_showDeckContentsRequest);
	}

	// Token: 0x06001156 RID: 4438 RVA: 0x00062EA5 File Offset: 0x000610A5
	public void ShowPhoneDeckTemplateTray()
	{
		this.m_pageManager.UpdateDeckTemplate(this.m_deckTemplatePickerPhone);
		SlidingTray component = this.m_deckTemplatePickerPhone.GetComponent<SlidingTray>();
		component.RegisterTrayToggleListener(new SlidingTray.TrayToggledListener(this.m_deckTemplatePickerPhone.OnTrayToggled));
		component.ShowTray();
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x00062EDF File Offset: 0x000610DF
	public DeckTemplatePicker GetPhoneDeckTemplateTray()
	{
		return this.m_deckTemplatePickerPhone;
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x00062EE8 File Offset: 0x000610E8
	public override void SetViewMode(CollectionUtils.ViewMode mode, bool triggerResponse, CollectionUtils.ViewModeData userdata = null)
	{
		Log.CollectionManager.Print("mode={0}-->{1} triggerResponse={2} isUpdatingTrayMode={3}", new object[]
		{
			this.m_currentViewMode,
			mode,
			triggerResponse,
			CollectionDeckTray.Get().IsUpdatingTrayMode()
		});
		if (this.m_currentViewMode == mode)
		{
			return;
		}
		if ((mode == CollectionUtils.ViewMode.HERO_SKINS || mode == CollectionUtils.ViewMode.CARD_BACKS || mode == CollectionUtils.ViewMode.COINS) && CollectionDeckTray.Get().IsUpdatingTrayMode())
		{
			return;
		}
		if (mode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			if (!CollectionManager.Get().IsInEditMode() || SceneMgr.Get().IsInTavernBrawlMode())
			{
				return;
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				this.ShowPhoneDeckTemplateTray();
			}
		}
		CollectionUtils.ViewMode currentViewMode = this.m_currentViewMode;
		this.m_currentViewMode = mode;
		this.OnSwitchViewModeResponse(triggerResponse, currentViewMode, mode, userdata);
	}

	// Token: 0x06001159 RID: 4441 RVA: 0x00062FA6 File Offset: 0x000611A6
	public bool ViewModeHasVisibleDeckList()
	{
		return this.m_currentViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE && this.m_currentViewMode != CollectionUtils.ViewMode.MASS_DISENCHANT;
	}

	// Token: 0x0600115A RID: 4442 RVA: 0x00062FC0 File Offset: 0x000611C0
	public void OnDoneEditingDeck()
	{
		this.ShowAppropriateSetFilters();
		if (this.m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			this.SetViewMode(CollectionUtils.ViewMode.CARDS, false, null);
		}
		if (!SceneMgr.Get().IsInTavernBrawlMode())
		{
			this.m_pageManager.SetDeckRuleset(null, false);
		}
		FiresideGatheringManager.Get().UpdateDeckValidity();
		this.m_pageManager.OnDoneEditingDeck();
	}

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x0600115B RID: 4443 RVA: 0x00063013 File Offset: 0x00061213
	public bool IsManaFilterEvenValues
	{
		get
		{
			return this.m_manaTabManager.IsFilterEvenValues;
		}
	}

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x0600115C RID: 4444 RVA: 0x00063020 File Offset: 0x00061220
	public bool IsManaFilterOddValues
	{
		get
		{
			return this.m_manaTabManager.IsFilterOddValues;
		}
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0006302D File Offset: 0x0006122D
	private void ManaFilterTab_OnManaFilterCleared(bool transitionPage)
	{
		this.ManaFilterTab_OnManaValueActivated(-1, transitionPage);
		this.m_manaFilterIsFromSearchText = false;
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x00063040 File Offset: 0x00061240
	public void ManaFilterTab_OnManaValueActivated(int cost, bool transitionPage)
	{
		if (this.m_manaFilterIsFromSearchText)
		{
			bool updateManaFilterToMatchSearchText = false;
			this.RemoveManaTokenFromSearchText(updateManaFilterToMatchSearchText);
		}
		bool active = this.m_manaTabManager.IsManaValueActive(cost);
		string value = (cost < 7) ? cost.ToString() : (cost.ToString() + "+");
		base.NotifyFilterUpdate(this.m_manaFilterListeners, active, value);
		this.m_pageManager.FilterByManaCost(cost, transitionPage);
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000630A8 File Offset: 0x000612A8
	public override void FilterBySearchText(string newSearchText)
	{
		string text = this.m_search.GetText();
		base.FilterBySearchText(newSearchText);
		this.OnSearchDeactivated_Internal(text, newSearchText, true);
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000630D4 File Offset: 0x000612D4
	private void RemoveManaTokenFromSearchText(bool updateManaFilterToMatchSearchText)
	{
		string text = this.m_search.GetText();
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
		Func<string, bool> isManaToken = this.GetIsManaSearchTokenFunc();
		string[] value = array.Where(delegate(string t)
		{
			if (isManaToken(t))
			{
				hasManaToken = true;
				return false;
			}
			return true;
		}).ToArray<string>();
		if (hasManaToken)
		{
			string text2 = string.Join(new string(CollectibleCardFilter.SearchTokenDelimiters[0], 1), value);
			this.m_search.SetText(text2);
			this.OnSearchDeactivated_Internal(text, this.m_search.GetText(), updateManaFilterToMatchSearchText);
		}
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x00063178 File Offset: 0x00061378
	private void UpdateManaFilterToMatchSearchText(string searchText, bool transitionPage = true)
	{
		if (string.IsNullOrEmpty(searchText) || !this.m_manaTabManager.Enabled)
		{
			this.m_manaTabManager.ClearFilter(transitionPage);
			return;
		}
		Func<string, bool> isManaSearchTokenFunc = this.GetIsManaSearchTokenFunc();
		string text = searchText.Split(CollectibleCardFilter.SearchTokenDelimiters, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(isManaSearchTokenFunc);
		if (text != null)
		{
			if (this.m_pageManager.IsManaCostFilterActive)
			{
				this.m_pageManager.FilterByManaCost(-1, transitionPage);
			}
			string text2 = text.Split(CollectibleCardFilter.SearchTagColons, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
			bool flag;
			int minCost;
			int maxCost;
			GeneralUtils.ParseNumericRange(text2, out flag, out minCost, out maxCost);
			string text3 = null;
			if (flag)
			{
				this.m_manaTabManager.SetFilter_Range(minCost, maxCost);
				text3 = text2;
			}
			else
			{
				string b = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EVEN_MANA").ToLower();
				string b2 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_ODD_MANA").ToLower();
				string a = text2.ToLower();
				bool flag2 = a == b;
				bool flag3 = !flag2 && a == b2;
				if (flag3 || flag2)
				{
					this.m_manaTabManager.SetFilter_EvenOdd(flag3);
					text3 = CollectibleCardFilter.CreateSearchTerm_Mana_OddEven(flag3);
				}
			}
			if (text3 != null)
			{
				this.m_manaFilterIsFromSearchText = true;
				base.NotifyFilterUpdate(this.m_manaFilterListeners, true, text3);
				return;
			}
		}
		else
		{
			this.m_manaTabManager.ClearFilter(transitionPage);
		}
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x000632A8 File Offset: 0x000614A8
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
				bool flag;
				int num;
				int num2;
				GeneralUtils.ParseNumericRange(text, out flag, out num, out num2);
				if (flag)
				{
					return true;
				}
				string a = text.ToLower();
				if (a == oddTokenValue || a == evenTokenValue)
				{
					return true;
				}
			}
			return false;
		};
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x00063304 File Offset: 0x00061504
	public override void HideAllTips()
	{
		if (this.m_innkeeperLClickReminder != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_innkeeperLClickReminder);
		}
		this.HideDeckHelpPopup();
		if (this.m_convertTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_convertTutorialPopup);
		}
		if (this.m_createDeckNotification != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_createDeckNotification);
		}
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x00063374 File Offset: 0x00061574
	public void HideDeckHelpPopup()
	{
		if (this.m_deckHelpPopup != null)
		{
			NotificationManager.Get().DestroyNotificationNowWithNoAnim(this.m_deckHelpPopup);
		}
		if (CollectionDeckTray.Get() != null && CollectionDeckTray.Get().GetCardsContent() != null)
		{
			CollectionDeckTray.Get().GetCardsContent().HideDeckHelpPopup();
		}
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x000633D0 File Offset: 0x000615D0
	public override void ShowInnkeeperLClickHelp(EntityDef entityDef)
	{
		bool isHero = entityDef != null && entityDef.IsHeroSkin();
		this.ShowInnkeeperLClickHelp(isHero);
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x000633F4 File Offset: 0x000615F4
	private void ShowInnkeeperLClickHelp(bool isHero)
	{
		if (CollectionDeckTray.Get().IsShowingDeckContents())
		{
			return;
		}
		if (isHero)
		{
			this.m_innkeeperLClickReminder = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_LCLICK_HERO"), "", 3f, null, false);
			return;
		}
		this.m_innkeeperLClickReminder = NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_LCLICK"), "", 3f, null, false);
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x00063460 File Offset: 0x00061660
	public void ShowPremiumCardsNotOwned(bool show)
	{
		this.m_pageManager.ShowCardsNotOwned(show);
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0006346E File Offset: 0x0006166E
	private void FeaturedCardsSetFilterCallback(List<TAG_CARD_SET> cardSets, List<int> specificCards, PegasusShared.FormatType formatType, SetFilterItem item, bool transitionPage)
	{
		this.SetLastSeenFeaturedCardsEvent(this.m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_ITEM);
		item.SetIconFxActive(false);
		this.SetFilterCallback(cardSets, specificCards, formatType, item, transitionPage);
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x00063498 File Offset: 0x00061698
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
						this.ShowSetFilterCards(cardSets, specificCards, transitionPage);
						return;
					}
					this.m_setFilterTray.SelectPreviouslySelectedItem();
				}
			};
			DialogManager.Get().ShowPopup(info);
			return;
		}
		this.m_search.SetWildModeActive(flag);
		this.ShowSetFilterCards(cardSets, specificCards, transitionPage);
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x00063581 File Offset: 0x00061781
	private void ShowSetFilterCards(List<TAG_CARD_SET> cardSets, List<int> specificCards, bool transitionPage = true)
	{
		if (specificCards != null)
		{
			this.ShowSpecificCards(specificCards);
			return;
		}
		this.ShowSets(cardSets, transitionPage);
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x00063596 File Offset: 0x00061796
	private void ShowSets(List<TAG_CARD_SET> cardSets, bool transitionPage = true)
	{
		this.m_pageManager.FilterByCardSets(cardSets, transitionPage);
		base.NotifyFilterUpdate(this.m_setFilterListeners, cardSets != null, null);
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x000635B6 File Offset: 0x000617B6
	protected override void ShowSpecificCards(List<int> specificCards)
	{
		base.ShowSpecificCards(specificCards);
		base.NotifyFilterUpdate(this.m_setFilterListeners, specificCards != null, null);
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x000635D0 File Offset: 0x000617D0
	public HeroPickerDisplay GetHeroPickerDisplay()
	{
		return this.m_heroPickerDisplay;
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x000635D8 File Offset: 0x000617D8
	public void EnterSelectNewDeckHeroMode()
	{
		if (this.m_selectingNewDeckHero)
		{
			return;
		}
		base.EnableInput(false);
		this.m_selectingNewDeckHero = true;
		this.m_heroPickerDisplay = AssetLoader.Get().InstantiatePrefab("HeroPicker.prefab:59e2d2f899d09f4488a194df18967915", AssetLoadingOptions.None).GetComponent<HeroPickerDisplay>();
		NotificationManager.Get().DestroyAllPopUps();
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			this.m_pageManager.HideNonDeckTemplateTabs(true, false);
		}
		this.CheckClipboardAndPromptPlayerToPaste();
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x00063647 File Offset: 0x00061847
	public void ExitSelectNewDeckHeroMode()
	{
		this.m_selectingNewDeckHero = false;
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x00063650 File Offset: 0x00061850
	public void CancelSelectNewDeckHeroMode()
	{
		base.EnableInput(true);
		this.m_pageManager.HideNonDeckTemplateTabs(false, true);
		this.ExitSelectNewDeckHeroMode();
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x00063670 File Offset: 0x00061870
	public bool CanViewHeroSkins()
	{
		CollectionManager collectionManager = CollectionManager.Get();
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		return editedDeck == null || collectionManager.GetBestHeroesIOwn(editedDeck.GetClass()).Count > 1;
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x000636A7 File Offset: 0x000618A7
	public bool CanViewCardBacks()
	{
		return CardBackManager.Get().GetCardBacksOwned().Count > 1;
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x000636BB File Offset: 0x000618BB
	public bool CanViewCoins()
	{
		if (CollectionManager.Get().GetEditedDeck() == null)
		{
			HashSet<int> coinsOwned = CoinManager.Get().GetCoinsOwned();
			return ((coinsOwned != null) ? coinsOwned.Count : 0) > 1;
		}
		return false;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x000636E4 File Offset: 0x000618E4
	public void RegisterManaFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_manaFilterListeners.Add(listener);
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x000636F2 File Offset: 0x000618F2
	public void UnregisterManaFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_manaFilterListeners.Remove(listener);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x00063701 File Offset: 0x00061901
	public void RegisterSetFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_setFilterListeners.Add(listener);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0006370F File Offset: 0x0006190F
	public void UnregisterSetFilterListener(CollectibleDisplay.FilterStateListener listener)
	{
		this.m_setFilterListeners.Remove(listener);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0006371E File Offset: 0x0006191E
	public override void ResetFilters(bool updateVisuals = true)
	{
		base.ResetFilters(updateVisuals);
		this.m_manaTabManager.ClearFilter(true);
		if (this.m_setFilterTray != null)
		{
			this.m_setFilterTray.ClearFilter(true);
		}
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x00063750 File Offset: 0x00061950
	public void ShowAppropriateSetFilters()
	{
		bool flag = this.m_craftingTray != null && this.m_craftingTray.IsShown();
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
		this.UpdateSetFilters(formatType, flag2, flag);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x000637D8 File Offset: 0x000619D8
	public void UpdateSetFilters(PegasusShared.FormatType formatType, bool editingDeck, bool showUnownedSets = false)
	{
		this.m_setFilterTray.UpdateSetFilters(formatType, editingDeck, showUnownedSets);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x000637E8 File Offset: 0x000619E8
	public void HideFilterTrayOnStartDragCard()
	{
		if (this.IsShowingSetFilterTray())
		{
			this.m_filterButton.m_setFilterTray.ToggleTraySlider(false, null, true);
		}
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x00063805 File Offset: 0x00061A05
	public void UnhideFilterTrayOnStopDragCard()
	{
		if (this.IsShowingSetFilterTray())
		{
			this.m_filterButton.m_setFilterTray.ToggleTraySlider(true, null, true);
		}
	}

	// Token: 0x0600117D RID: 4477 RVA: 0x00063822 File Offset: 0x00061A22
	public void WaitThenUnhideFilterTrayOnStopDragCard()
	{
		if (this.IsShowingSetFilterTray())
		{
			base.StartCoroutine(this.WaitThenUnhideFilterTrayOnStopDragCard_Coroutine());
		}
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x00063839 File Offset: 0x00061A39
	private IEnumerator WaitThenUnhideFilterTrayOnStopDragCard_Coroutine()
	{
		yield return new WaitForSeconds(0.5f);
		if (CollectionManager.Get().GetCollectibleDisplay() as CollectionManagerDisplay != null && this.IsShowingSetFilterTray() && CollectionInputMgr.Get() != null && !CollectionInputMgr.Get().HasHeldCard())
		{
			this.m_filterButton.m_setFilterTray.ToggleTraySlider(true, null, true);
		}
		yield break;
	}

	// Token: 0x0600117F RID: 4479 RVA: 0x00063848 File Offset: 0x00061A48
	public bool SetFilterIsDefaultSelection()
	{
		return this.m_setFilterTray == null || !this.m_setFilterTray.HasActiveFilter();
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x00063868 File Offset: 0x00061A68
	public bool IsShowingSetFilterTray()
	{
		return !(this.m_setFilterTray == null) && this.m_setFilterTray.IsShown();
	}

	// Token: 0x06001181 RID: 4481 RVA: 0x00063885 File Offset: 0x00061A85
	public bool IsSelectingNewDeckHero()
	{
		return this.m_selectingNewDeckHero;
	}

	// Token: 0x06001182 RID: 4482 RVA: 0x00063890 File Offset: 0x00061A90
	private void OnDeckContents(long deckID)
	{
		if (deckID == this.m_showDeckContentsRequest)
		{
			this.m_showDeckContentsRequest = 0L;
			this.ShowDeck(deckID, false, false, null);
			return;
		}
		CollectionDeckTray.Get().GetDecksContent().OnDeckContentsUpdated(deckID);
	}

	// Token: 0x06001183 RID: 4483 RVA: 0x000638D4 File Offset: 0x00061AD4
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
				Debug.LogError("CollectionManagerDisplay.OnDeckCreatedByPlayer: Could not get deck " + deckID.ToString());
				return;
			}
			if (CollectionManager.Get().GetNonStarterTemplateDecks(deck.FormatType, deck.GetClass()).Count > 0)
			{
				showDeckTemplatePage = true;
			}
		}
		this.ShowDeck(deckID, true, showDeckTemplatePage, null);
	}

	// Token: 0x06001184 RID: 4484 RVA: 0x00063958 File Offset: 0x00061B58
	private void OnNewCardSeen(string cardID, TAG_PREMIUM premium)
	{
		this.m_pageManager.UpdateClassTabNewCardCounts();
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x00063965 File Offset: 0x00061B65
	private void OnCardRewardsInserted(List<string> cardID, List<TAG_PREMIUM> premium)
	{
		this.m_pageManager.RefreshCurrentPageContents();
	}

	// Token: 0x06001186 RID: 4486 RVA: 0x00063972 File Offset: 0x00061B72
	protected override void OnCollectionChanged()
	{
		if (this.m_currentViewMode != CollectionUtils.ViewMode.MASS_DISENCHANT)
		{
			this.m_pageManager.NotifyOfCollectionChanged();
		}
	}

	// Token: 0x06001187 RID: 4487 RVA: 0x00063988 File Offset: 0x00061B88
	private IEnumerator WaitUntilReady()
	{
		while (!this.m_netCacheReady)
		{
			if (!Network.IsLoggedIn())
			{
				break;
			}
			yield return 0;
		}
		while (!this.m_gameSaveDataReady && Network.IsLoggedIn())
		{
			yield return 0;
		}
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		collectionDeckTray.Initialize();
		collectionDeckTray.RegisterModeSwitchedListener(delegate
		{
			base.UpdateCurrentPageCardLocks(false);
		});
		collectionDeckTray.GetCardsContent().RegisterCardTileRightClickedListener(new DeckTrayCardListContent.CardTileRightClicked(this.OnCardTileRightClicked));
		this.m_isReady = true;
		yield break;
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x00063997 File Offset: 0x00061B97
	private IEnumerator InitCollectionWhenReady()
	{
		while (!this.m_pageManager.IsFullyLoaded())
		{
			yield return null;
		}
		this.m_pageManager.LoadMassDisenchantScreen();
		this.m_pageManager.OnCollectionLoaded();
		yield break;
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000639A8 File Offset: 0x00061BA8
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		if (NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().Collection.Manager)
		{
			this.m_netCacheReady = true;
			return;
		}
		if (SceneMgr.Get().IsModeRequested(SceneMgr.Mode.HUB))
		{
			return;
		}
		SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
		Error.AddWarningLoc("GLOBAL_FEATURE_DISABLED_TITLE", "GLOBAL_FEATURE_DISABLED_MESSAGE_COLLECTION", Array.Empty<object>());
	}

	// Token: 0x0600118A RID: 4490 RVA: 0x00063A18 File Offset: 0x00061C18
	private void OnGameSaveDataReady(bool success)
	{
		if (!success)
		{
			Log.CollectionManager.PrintError("Error retrieving Game Save Key for Collection Manager!", Array.Empty<object>());
		}
		this.m_gameSaveDataReady = true;
	}

	// Token: 0x0600118B RID: 4491 RVA: 0x00063A38 File Offset: 0x00061C38
	private void OnShowAdvancedCMChanged(Option option, object prevValue, bool existed, object userData)
	{
		bool @bool = Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, false);
		if (@bool)
		{
			Options.Get().UnregisterChangedListener(Option.SHOW_ADVANCED_COLLECTIONMANAGER, new Options.ChangedCallback(this.OnShowAdvancedCMChanged));
		}
		this.ShowAdvancedCollectionManager(@bool);
		this.m_manaTabManager.ActivateTabs(true);
	}

	// Token: 0x0600118C RID: 4492 RVA: 0x00063A88 File Offset: 0x00061C88
	private void OnCardTileRightClicked(DeckTrayDeckTileVisual cardTile)
	{
		if (base.GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			return;
		}
		if (!cardTile.GetSlot().Owned && !DuelsConfig.IsCardLoadoutTreasure(cardTile.GetCardID()))
		{
			CraftingManager.Get().EnterCraftMode(cardTile.GetActor(), null);
		}
		base.GoToPageWithCard(cardTile.GetCardID(), cardTile.GetPremium());
	}

	// Token: 0x0600118D RID: 4493 RVA: 0x00063ADC File Offset: 0x00061CDC
	protected override void LoadAllTextures()
	{
		foreach (object obj in Enum.GetValues(typeof(TAG_CLASS)))
		{
			TAG_CLASS tag_CLASS = (TAG_CLASS)obj;
			string classTextureAssetPath = CollectionManagerDisplay.GetClassTextureAssetPath(tag_CLASS);
			if (!string.IsNullOrEmpty(classTextureAssetPath))
			{
				AssetLoader.Get().LoadAsset<Texture>(classTextureAssetPath, new AssetHandleCallback<Texture>(this.OnClassTextureLoaded), tag_CLASS, AssetLoadingOptions.None);
			}
		}
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x00063B6C File Offset: 0x00061D6C
	protected override void UnloadAllTextures()
	{
		this.m_loadedClassTextures.DisposeValuesAndClear<TAG_CLASS, AssetHandle<Texture>>();
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00063B7C File Offset: 0x00061D7C
	public static string GetClassTextureAssetPath(TAG_CLASS classTag)
	{
		switch (classTag)
		{
		case TAG_CLASS.DRUID:
			return "Druid.psd:e2417dc1394f54349956b2e24a27f923";
		case TAG_CLASS.HUNTER:
			return "Hunter.psd:16178c8d6ed14814dae893bad9de80d5";
		case TAG_CLASS.MAGE:
			return "Mage.psd:8dcb9bd578b6c01448cf1021c6157dfd";
		case TAG_CLASS.PALADIN:
			return "Paladin.psd:50ba8fc595684d440866ac130c146d57";
		case TAG_CLASS.PRIEST:
			return "Priest.psd:5fa4606c71c0dff4eb0b07b88ba83197";
		case TAG_CLASS.ROGUE:
			return "Rogue.psd:47dc46a5269d7fc4a8a9ebada8f2d890";
		case TAG_CLASS.SHAMAN:
			return "Shaman.psd:2e468e3b0f7a7804a9335333c9e673e2";
		case TAG_CLASS.WARLOCK:
			return "Warlock.psd:d6077adee4894df43a67617620de56a9";
		case TAG_CLASS.WARRIOR:
			return "Warrior.psd:5376d479d4155ca419f8afa5e42ba505";
		default:
			return "";
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00063BF4 File Offset: 0x00061DF4
	private void SetTavernBrawlTexturesIfNecessary()
	{
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			if (this.m_bookBack != null && !string.IsNullOrEmpty(this.m_tbCorkBackTexture) && this.m_customBookBackMesh != null)
			{
				this.m_bookBack.GetComponent<MeshFilter>().mesh = this.m_customBookBackMesh;
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_loadedCorkBackTexture, this.m_tbCorkBackTexture, AssetLoadingOptions.None);
				this.m_bookBack.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", this.m_loadedCorkBackTexture);
				this.m_setFilterTray.m_toggleButton.SetButtonBackgroundMaterial();
			}
			if (!UniversalInputManager.UsePhoneUI)
			{
				foreach (GameObject gameObject in this.m_customObjectsToSwap)
				{
					Renderer component = gameObject.GetComponent<Renderer>();
					if (component != null)
					{
						component.SetMaterial(this.m_tavernBrawlElements);
					}
					else
					{
						Debug.LogErrorFormat("Failed to swap material for TavernBrawl object: {0}", new object[]
						{
							gameObject.name
						});
					}
				}
			}
		}
	}

	// Token: 0x06001191 RID: 4497 RVA: 0x00063D28 File Offset: 0x00061F28
	private void SetDuelsTexturesIfNecessary()
	{
		if (SceneMgr.Get().IsInDuelsMode())
		{
			if (this.m_bookBack != null && !string.IsNullOrEmpty(this.m_duelsCorkBackTexture) && this.m_customBookBackMesh != null)
			{
				this.m_bookBack.GetComponent<MeshFilter>().mesh = this.m_customBookBackMesh;
				AssetLoader.Get().LoadAsset<Texture>(ref this.m_loadedCorkBackTexture, this.m_duelsCorkBackTexture, AssetLoadingOptions.None);
				this.m_bookBack.GetComponent<MeshRenderer>().GetMaterial().SetTexture("_MainTex", this.m_loadedCorkBackTexture);
				this.m_setFilterTray.m_toggleButton.SetButtonBackgroundMaterial();
			}
			if (!UniversalInputManager.UsePhoneUI)
			{
				foreach (GameObject gameObject in this.m_customObjectsToSwap)
				{
					Renderer component = gameObject.GetComponent<Renderer>();
					if (component != null)
					{
						component.SetMaterial(this.m_duelsElements);
					}
					else
					{
						Debug.LogErrorFormat("Failed to swap material for TavernBrawl object: {0}", new object[]
						{
							gameObject.name
						});
					}
				}
			}
		}
	}

	// Token: 0x06001192 RID: 4498 RVA: 0x00063E5C File Offset: 0x0006205C
	private void OnClassTextureLoaded(AssetReference assetRef, AssetHandle<Texture> loadedTexture, object callbackData)
	{
		if (loadedTexture == null)
		{
			Debug.LogWarning(string.Format("CollectionManagerDisplay.OnClassTextureLoaded(): asset for {0} is null!", assetRef));
			return;
		}
		TAG_CLASS key = (TAG_CLASS)callbackData;
		this.m_loadedClassTextures.SetOrReplaceDisposable(key, loadedTexture);
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x00063E94 File Offset: 0x00062094
	public void ShowCurrentEditedDeck()
	{
		CollectionDeck editedDeck = CollectionManager.Get().GetEditedDeck();
		if (editedDeck == null)
		{
			return;
		}
		TAG_CLASS @class = editedDeck.GetClass();
		this.ShowDeckHelper(editedDeck, @class, false, false, null);
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x00063ECC File Offset: 0x000620CC
	public void ShowDeck(long deckID, bool isNewDeck, bool showDeckTemplatePage, CollectionUtils.ViewMode? setNewViewMode = null)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck == null)
		{
			return;
		}
		TAG_CLASS deckHeroClass = this.GetDeckHeroClass(deckID);
		this.ShowDeckHelper(deck, deckHeroClass, isNewDeck, showDeckTemplatePage, setNewViewMode);
	}

	// Token: 0x06001195 RID: 4501 RVA: 0x00063F00 File Offset: 0x00062100
	private void ShowDeckHelper(CollectionDeck currDeck, TAG_CLASS deckClass, bool isNewDeck, bool showDeckTemplatePage, CollectionUtils.ViewMode? setNewViewMode = null)
	{
		if (currDeck.HasUIHeroOverride() && this.m_currentViewMode == CollectionUtils.ViewMode.HERO_SKINS)
		{
			this.m_pageManager.JumpToCollectionClassPage(deckClass);
		}
		if (!showDeckTemplatePage)
		{
			this.m_pageManager.HideNonDeckTemplateTabs(false, false);
		}
		if (showDeckTemplatePage)
		{
			setNewViewMode = new CollectionUtils.ViewMode?(CollectionUtils.ViewMode.DECK_TEMPLATE);
		}
		else if ((this.m_currentViewMode == CollectionUtils.ViewMode.HERO_SKINS && !this.CanViewHeroSkins()) || (this.m_currentViewMode == CollectionUtils.ViewMode.CARD_BACKS && !this.CanViewCardBacks()) || (this.m_currentViewMode == CollectionUtils.ViewMode.COINS && !this.CanViewCoins()))
		{
			setNewViewMode = new CollectionUtils.ViewMode?(CollectionUtils.ViewMode.CARDS);
		}
		CollectionManager.Get().StartEditingDeck(currDeck, isNewDeck);
		CollectionDeckTray.Get().ShowDeck(setNewViewMode ?? base.GetViewMode());
		if (setNewViewMode != null)
		{
			base.SetViewMode(setNewViewMode.Value, null);
		}
		this.UpdateSetFilters(currDeck.FormatType, true, false);
		this.m_pageManager.UpdateFiltersForDeck(currDeck, deckClass, isNewDeck, null, null);
		this.m_pageManager.UpdateCraftingModeButtonDustBottleVisibility();
		NotificationManager.Get().DestroyNotification(this.m_createDeckNotification, 0.25f);
	}

	// Token: 0x06001196 RID: 4502 RVA: 0x00064010 File Offset: 0x00062210
	private TAG_CLASS GetDeckHeroClass(long deckID)
	{
		CollectionDeck deck = CollectionManager.Get().GetDeck(deckID);
		if (deck == null)
		{
			Log.CollectionManager.Print(string.Format("CollectionManagerDisplay no deck with ID {0}!", deckID), Array.Empty<object>());
			return TAG_CLASS.INVALID;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(deck.HeroCardID);
		if (entityDef == null)
		{
			Log.CollectionManager.Print(string.Format("CollectionManagerDisplay: CollectionManager doesn't have an entity def for {0}!", deck.HeroCardID), Array.Empty<object>());
			return TAG_CLASS.INVALID;
		}
		return entityDef.GetClass();
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x00064088 File Offset: 0x00062288
	private IEnumerator DoBookOpeningAnimations()
	{
		while (this.m_isCoverLoading)
		{
			yield return null;
		}
		if (this.m_cover != null)
		{
			this.m_cover.Open(new CollectionCoverDisplay.DelOnOpened(base.OnCoverOpened));
		}
		else
		{
			base.OnCoverOpened();
		}
		this.m_manaTabManager.ActivateTabs(true);
		yield break;
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x00064097 File Offset: 0x00062297
	private IEnumerator SetBookToOpen()
	{
		while (this.m_isCoverLoading)
		{
			yield return null;
		}
		if (this.m_cover != null)
		{
			this.m_cover.SetOpenState();
		}
		this.m_manaTabManager.ActivateTabs(true);
		yield break;
	}

	// Token: 0x06001199 RID: 4505 RVA: 0x000640A6 File Offset: 0x000622A6
	private void DoBookClosingAnimations()
	{
		if (this.m_cover != null)
		{
			this.m_cover.Close();
		}
		this.m_manaTabManager.ActivateTabs(false);
	}

	// Token: 0x0600119A RID: 4506 RVA: 0x000640D0 File Offset: 0x000622D0
	private void ShowAdvancedCollectionManager(bool show)
	{
		show |= UniversalInputManager.UsePhoneUI;
		this.m_search.gameObject.SetActive(show);
		this.m_manaTabManager.gameObject.SetActive(show);
		if (this.m_setFilterTray != null)
		{
			bool buttonShown = show && !UniversalInputManager.UsePhoneUI;
			this.m_setFilterTray.SetButtonShown(buttonShown);
		}
		if (this.m_craftingTray == null)
		{
			AssetLoader.Get().InstantiatePrefab(UniversalInputManager.UsePhoneUI ? "CraftingTray_phone.prefab:bd4719b05f6f24870be20fa595b2032a" : "CraftingTray.prefab:dae9f103e23a53f459baeef392daa984", new PrefabCallback<GameObject>(this.OnCraftingTrayLoaded), null, AssetLoadingOptions.IgnorePrefabPosition);
		}
		this.m_craftingModeButton.gameObject.SetActive(true);
		this.m_craftingModeButton.AddEventListener(UIEventType.RELEASE, new UIEvent.Handler(this.OnCraftingModeButtonReleased));
		if (this.m_setFilterTray != null && show && !this.m_setFilterTrayInitialized)
		{
			this.m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_ALL_STANDARD_CARDS"), this.m_allSetsTexture, new UnityEngine.Vector2?(this.m_allSetsIconOffset), new SetFilterItem.ItemSelectedCallback(this.SetFilterCallback), new List<TAG_CARD_SET>(GameUtils.GetStandardSets()), null, PegasusShared.FormatType.FT_STANDARD, true, true, GameStrings.Get("GLUE_TOOLTIP_HEADER_ALL_STANDARD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_ALL_STANDARD_CARDS"));
			this.m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_WILD_CARDS"), this.m_wildSetsTexture, new UnityEngine.Vector2?(this.m_wildSetsIconOffset), new SetFilterItem.ItemSelectedCallback(this.SetFilterCallback), new List<TAG_CARD_SET>(GameUtils.GetAllWildPlayableSets()), null, PegasusShared.FormatType.FT_WILD, false, true, GameStrings.Get("GLUE_TOOLTIP_HEADER_WILD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_WILD_CARDS"));
			this.m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_VANILLA_CARDS"), this.m_classicSetsTexture, new UnityEngine.Vector2?(this.m_classicSetsIconOffset), new SetFilterItem.ItemSelectedCallback(this.SetFilterCallback), new List<TAG_CARD_SET>
			{
				TAG_CARD_SET.VANILLA
			}, null, PegasusShared.FormatType.FT_CLASSIC, false, true, GameStrings.Get("GLUE_TOOLTIP_HEADER_VANILLA_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_VANILLA_CARDS"));
			List<int> list = (from s in GameDbf.GetIndex().GetCardsWithFeaturedCardsEvent()
			select GameDbf.GetIndex().GetCardRecord(s) into c
			where SpecialEventManager.Get().IsEventActive(c.FeaturedCardsEvent, false)
			select c.ID).ToList<int>();
			if (list.Any<int>())
			{
				SetFilterItem setFilterItem = this.m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_NEW_CARDS"), this.m_featuredCardsTexture, new UnityEngine.Vector2?(this.m_featuredCardsIconOffset), new SetFilterItem.ItemSelectedCallback(this.FeaturedCardsSetFilterCallback), null, list, PegasusShared.FormatType.FT_STANDARD, false, false, null, null);
				this.m_currentActiveFeaturedCardsEvent = GameDbf.Card.GetRecord(list.First<int>()).FeaturedCardsEvent;
				base.StartCoroutine(this.SetIconFxIfFeaturedCardsEventNotSeen(setFilterItem, this.m_currentActiveFeaturedCardsEvent));
				base.StartCoroutine(this.SetFeaturedCardsSetFilterGlowIfNotSeen(this.m_currentActiveFeaturedCardsEvent));
			}
			this.PopulateSetFilters(false);
			this.m_setFilterTrayInitialized = true;
		}
		else if (!show)
		{
			this.ShowSets(new List<TAG_CARD_SET>(GameUtils.GetStandardSets()), true);
		}
		this.ShowAppropriateSetFilters();
		if (!show)
		{
			return;
		}
		this.m_manaTabManager.SetUpTabs();
	}

	// Token: 0x0600119B RID: 4507 RVA: 0x00064408 File Offset: 0x00062608
	private void AddDuelsSetFilters()
	{
		foreach (TAG_CARD_SET cardSet in DuelsConfig.GetDuelsSets())
		{
			this.AddSetFilter(cardSet);
		}
	}

	// Token: 0x0600119C RID: 4508 RVA: 0x0006445C File Offset: 0x0006265C
	private void AddSetFilters(bool isWild)
	{
		foreach (TAG_CARD_SET cardSet in (from cardSetId in CollectionManager.Get().GetDisplayableCardSets()
		where GameUtils.IsWildCardSet(cardSetId) == isWild && !GameUtils.IsClassicCardSet(cardSetId) && (!GameUtils.IsLegacySet(cardSetId) || cardSetId == TAG_CARD_SET.LEGACY)
		select cardSetId).OrderByDescending(delegate(TAG_CARD_SET cardSetId)
		{
			CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(cardSetId);
			if (cardSet2 == null)
			{
				return 0;
			}
			return cardSet2.ReleaseOrder;
		}))
		{
			this.AddSetFilter(cardSet);
		}
	}

	// Token: 0x0600119D RID: 4509 RVA: 0x000644F0 File Offset: 0x000626F0
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
			iconOffset = new UnityEngine.Vector2?(new UnityEngine.Vector2((float)cardSet2.FilterIconOffsetX, (float)cardSet2.FilterIconOffsetY));
		}
		this.m_setFilterTray.AddItem(GameStrings.GetCardSetNameShortened(cardSet), iconTextureAssetRef, iconOffset, new SetFilterItem.ItemSelectedCallback(this.SetFilterCallback), list, GameUtils.GetCardSetFormat(cardSet), false);
	}

	// Token: 0x0600119E RID: 4510 RVA: 0x0006458C File Offset: 0x0006278C
	public void PopulateSetFilters(bool shouldReset = false)
	{
		if (shouldReset)
		{
			this.m_setFilterTray.RemoveAllItems();
		}
		if (SceneMgr.Get().IsInDuelsMode())
		{
			this.m_setFilterTray.AddItemUsingTexture(GameStrings.Get("GLUE_COLLECTION_WILD_CARDS"), this.m_wildSetsTexture, new UnityEngine.Vector2?(this.m_wildSetsIconOffset), new SetFilterItem.ItemSelectedCallback(this.SetFilterCallback), new List<TAG_CARD_SET>(GameUtils.GetAllWildPlayableSets()), null, PegasusShared.FormatType.FT_WILD, false, true, GameStrings.Get("GLUE_TOOLTIP_HEADER_WILD_CARDS"), GameStrings.Get("GLUE_TOOLTIP_DESCRIPTION_WILD_CARDS"));
			this.m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_ALL_SETS"), PegasusShared.FormatType.FT_STANDARD);
			this.AddDuelsSetFilters();
		}
		else
		{
			this.m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_STANDARD_SETS"), PegasusShared.FormatType.FT_STANDARD);
			this.AddSetFilters(false);
			this.m_setFilterTray.AddHeader(GameStrings.Get("GLUE_COLLECTION_WILD_SETS"), PegasusShared.FormatType.FT_WILD);
			this.AddSetFilters(true);
			if (CollectionManager.Get().GetDisplayableCardSets().Contains(TAG_CARD_SET.SLUSH))
			{
				this.AddSetFilter(TAG_CARD_SET.SLUSH);
			}
		}
		if (Options.GetInRankedPlayMode() && !SceneMgr.Get().IsInDuelsMode())
		{
			if (!this.m_setFilterTray.SelectFirstItemWithFormat(Options.GetFormatType(), true))
			{
				this.m_setFilterTray.SelectFirstItem(true);
				return;
			}
		}
		else
		{
			this.m_setFilterTray.SelectFirstItem(true);
		}
	}

	// Token: 0x0600119F RID: 4511 RVA: 0x000646BC File Offset: 0x000628BC
	private long GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId gameSaveSubkeyId)
	{
		List<long> list;
		GameSaveDataManager.Get().GetSubkeyValue(GameSaveKeyId.COLLECTION_MANAGER, gameSaveSubkeyId, out list);
		long result = 0L;
		if (list != null && list.Any<long>())
		{
			result = list.First<long>();
		}
		return result;
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x000646F2 File Offset: 0x000628F2
	private IEnumerator SetIconFxIfFeaturedCardsEventNotSeen(SetFilterItem setFilterItem, string currentActiveFeaturedCardsEvent)
	{
		while (!this.m_isReady)
		{
			yield return null;
		}
		if (!this.m_gameSaveDataReady)
		{
			yield break;
		}
		long lastSeenFeaturedCardsEvent = this.GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_ITEM);
		long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
		if (eventIdFromEventName != -1L && eventIdFromEventName != lastSeenFeaturedCardsEvent)
		{
			setFilterItem.SetIconFxActive(true);
		}
		yield break;
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x0006470F File Offset: 0x0006290F
	private IEnumerator SetFeaturedCardsSetFilterGlowIfNotSeen(string currentActiveFeaturedCardsEvent)
	{
		while (!this.m_isReady)
		{
			yield return null;
		}
		if (!this.m_gameSaveDataReady)
		{
			yield break;
		}
		long lastSeenFeaturedCardsEvent = this.GetLastSeenFeaturedCardsEvent(GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
		if (eventIdFromEventName != -1L && eventIdFromEventName != lastSeenFeaturedCardsEvent)
		{
			this.m_setFilterTray.SetFilterButtonGlowActive(true);
			if (this.m_filterButtonGlow != null)
			{
				this.m_filterButtonGlow.SetActive(true);
			}
		}
		yield break;
	}

	// Token: 0x060011A2 RID: 4514 RVA: 0x00064728 File Offset: 0x00062928
	private void SetLastSeenFeaturedCardsEvent(string currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId subkeyId)
	{
		if (string.IsNullOrEmpty(currentActiveFeaturedCardsEvent))
		{
			return;
		}
		long eventIdFromEventName = SpecialEventManager.Get().GetEventIdFromEventName(currentActiveFeaturedCardsEvent);
		if (eventIdFromEventName == -1L)
		{
			return;
		}
		if (this.GetLastSeenFeaturedCardsEvent(subkeyId) == eventIdFromEventName)
		{
			return;
		}
		GameSaveDataManager.Get().SaveSubkey(new GameSaveDataManager.SubkeySaveRequest(GameSaveKeyId.COLLECTION_MANAGER, subkeyId, new long[]
		{
			eventIdFromEventName
		}), null);
	}

	// Token: 0x060011A3 RID: 4515 RVA: 0x0006477C File Offset: 0x0006297C
	protected override void OnSearchDeactivated(string oldSearchText, string newSearchText)
	{
		this.OnSearchDeactivated_Internal(oldSearchText, newSearchText, true);
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x00064788 File Offset: 0x00062988
	private void OnSearchDeactivated_Internal(string oldSearchText, string newSearchText, bool updateManaFilterToMatchSearchText)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			base.EnableInput(true);
		}
		if (oldSearchText == newSearchText)
		{
			base.OnSearchFilterComplete(null);
			return;
		}
		if (this.m_currentViewMode == CollectionUtils.ViewMode.CARDS && !this.m_craftingTray.IsShown() && newSearchText.ToLower() == GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING"))
		{
			this.m_searchTriggeredCraftingTray = true;
			this.ShowCraftingTray(null, null, null, true);
		}
		else if (this.m_craftingTray.IsShown() && newSearchText.ToLower() != GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING") && this.m_searchTriggeredCraftingTray && this.m_craftingTray != null)
		{
			this.m_craftingTray.Hide();
			this.m_searchTriggeredCraftingTray = false;
		}
		base.NotifyFilterUpdate(this.m_searchFilterListeners, !string.IsNullOrEmpty(newSearchText), newSearchText);
		if (updateManaFilterToMatchSearchText)
		{
			this.UpdateManaFilterToMatchSearchText(newSearchText, false);
		}
		this.m_pageManager.ChangeSearchTextFilter(newSearchText, new BookPageManager.DelOnPageTransitionComplete(base.OnSearchFilterComplete), null, true);
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x00064898 File Offset: 0x00062A98
	protected override void OnSearchCleared(bool transitionPage)
	{
		if (this.m_searchTriggeredCraftingTray && this.m_craftingTray != null)
		{
			this.m_craftingTray.Hide();
			this.m_searchTriggeredCraftingTray = false;
		}
		base.NotifyFilterUpdate(this.m_searchFilterListeners, false, "");
		this.m_pageManager.ChangeSearchTextFilter("", transitionPage);
		if (this.m_manaFilterIsFromSearchText)
		{
			this.m_manaTabManager.ClearFilter(true);
		}
	}

	// Token: 0x060011A6 RID: 4518 RVA: 0x00064904 File Offset: 0x00062B04
	public void ShowTavernBrawlDeck(long deckID)
	{
		CollectionDeckTray.Get().GetDecksContent().SetEditingTraySection(0);
		CollectionDeckTray.Get().SetTrayMode(DeckTray.DeckContentTypes.Decks);
		this.RequestContentsToShowDeck(deckID);
	}

	// Token: 0x060011A7 RID: 4519 RVA: 0x00064928 File Offset: 0x00062B28
	public void ShowDuelsDeckHeader()
	{
		CollectionDeckTray.Get().GetDecksContent().SetEditingTraySection(0);
		CollectionDeckTray.Get().GetDecksContent().GetEditingTraySection().m_deckBox.HideBanner();
	}

	// Token: 0x060011A8 RID: 4520 RVA: 0x00064954 File Offset: 0x00062B54
	private void DoEnterCollectionManagerEvents()
	{
		if (CollectionManager.Get().HasVisitedCollection() || CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			base.EnableInput(true);
			this.OpenBookImmediately();
			return;
		}
		CollectionManager.Get().SetHasVisitedCollection(true);
		base.EnableInput(false);
		base.StartCoroutine(this.OpenBookWhenReady());
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000649A4 File Offset: 0x00062BA4
	private void OpenBookImmediately()
	{
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.COLLECTION
			});
		}
		base.StartCoroutine(this.SetBookToOpen());
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			base.StartCoroutine(this.ShowCollectionTipsIfNeeded());
		}
	}

	// Token: 0x060011AA RID: 4522 RVA: 0x000649FF File Offset: 0x00062BFF
	private IEnumerator OpenBookWhenReady()
	{
		while (CollectionManager.Get().IsWaitingForBoxTransition())
		{
			yield return null;
		}
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.COLLECTION
			});
		}
		this.m_pageManager.OnBookOpening();
		base.StartCoroutine(this.DoBookOpeningAnimations());
		if (SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER)
		{
			base.StartCoroutine(this.ShowCollectionTipsIfNeeded());
		}
		yield break;
	}

	// Token: 0x060011AB RID: 4523 RVA: 0x00064A10 File Offset: 0x00062C10
	private void ShowCraftingTipIfNeeded()
	{
		if (Options.Get().GetBool(Option.TIP_CRAFTING_UNLOCKED, false) || !UserAttentionManager.CanShowAttentionGrabber("CollectionManagerDisplay.ShowCraftingTipIfNeeded"))
		{
			return;
		}
		NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_DISENCHANT_31"), "VO_INNKEEPER_DISENCHANT_31.prefab:4a0246488dc2d8146b1db88de5c603ff", 0f, null, false);
		Options.Get().SetBool(Option.TIP_CRAFTING_UNLOCKED, true);
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x00064A70 File Offset: 0x00062C70
	private Vector3 GetNewDeckPosition()
	{
		Vector3 b = UniversalInputManager.UsePhoneUI ? new Vector3(25.7f, 2.6f, 0f) : new Vector3(17.5f, 0f, 0f);
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetNewDeckButtonPosition() - b;
		}
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060011AD RID: 4525 RVA: 0x00064AEC File Offset: 0x00062CEC
	private Vector3 GetLastDeckPosition()
	{
		Vector3 b = UniversalInputManager.UsePhoneUI ? new Vector3(15.8f, 0f, 6f) : new Vector3(9.6f, 0f, 3f);
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetLastUsedTraySection().transform.position - b;
		}
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x00064B70 File Offset: 0x00062D70
	private Vector3 GetMiddleDeckPosition()
	{
		int index = 4;
		Vector3 b = UniversalInputManager.UsePhoneUI ? new Vector3(15.8f, 0f, 6f) : new Vector3(9.6f, 0f, 3f);
		CollectionDeckTray collectionDeckTray = CollectionDeckTray.Get();
		if (collectionDeckTray != null)
		{
			return collectionDeckTray.GetDecksContent().GetTraySection(index).transform.position - b;
		}
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x00064BF8 File Offset: 0x00062DF8
	private void ShowSetRotationNewDeckIndicator(float f)
	{
		string text;
		Vector3 position;
		if (CollectionManager.Get().GetNumberOfWildDecks() >= 27)
		{
			text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL15");
			position = this.GetMiddleDeckPosition();
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
				position = this.GetLastDeckPosition();
			}
			else
			{
				text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL10");
				CollectionDeckTray.Get().GetDecksContent().m_newDeckButton.m_highlightState.ChangeState(ActorStateType.HIGHLIGHT_PRIMARY_ACTIVE);
				position = this.GetNewDeckPosition();
			}
		}
		this.m_createDeckNotification = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, position, this.m_editDeckTutorialBone.localScale, text, true, NotificationManager.PopupTextType.BASIC);
		if (this.m_createDeckNotification != null)
		{
			this.m_createDeckNotification.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			this.m_createDeckNotification.PulseReminderEveryXSeconds(3f);
		}
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x00064CD6 File Offset: 0x00062ED6
	public IEnumerator ShowCollectionTipsIfNeeded()
	{
		while (CollectionManager.Get().IsWaitingForBoxTransition())
		{
			yield return null;
		}
		if (CollectionManager.Get().ShouldShowWildToStandardTutorial(true) && UserAttentionManager.CanShowAttentionGrabber(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, "CollectionManagerDisplay.ShowCollectionTipsIfNeeded:ShowSetRotationTutorial"))
		{
			int deckCount = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).Count;
			CollectionDeckTray deckTray = CollectionDeckTray.Get();
			while (deckTray.IsUpdatingTrayMode() || !deckTray.GetDecksContent().IsDoneEntering())
			{
				yield return null;
			}
			if (deckCount >= 27)
			{
				NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, GameStrings.Get("GLUE_COLLECTION_TUTORIAL11"), "VO_INNKEEPER_Male_Dwarf_FULL_DECKS_06.prefab:21adedb0a5456c24da1b2918c3d04e5a", 0f, null, false);
				this.ShowSetRotationNewDeckIndicator(0f);
			}
			else if (deckCount > this.m_onscreenDecks)
			{
				deckTray.m_scrollbar.SetScroll(1f, new UIBScrollable.OnScrollComplete(this.ShowSetRotationNewDeckIndicator), iTween.EaseType.easeOutBounce, 0.75f, true, true);
			}
			else
			{
				this.ShowSetRotationNewDeckIndicator(0f);
			}
			yield break;
		}
		if (Options.Get().GetBool(Option.HAS_SEEN_PRACTICE_MODE, false))
		{
			Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER_AFTER_PRACTICE, true);
		}
		if (!Options.Get().GetBool(Option.HAS_SEEN_COLLECTIONMANAGER, false) && UserAttentionManager.CanShowAttentionGrabber("UserAttentionManager.CanShowAttentionGrabber:" + Option.HAS_SEEN_COLLECTIONMANAGER))
		{
			NotificationManager.Get().CreateInnkeeperQuote(UserAttentionBlocker.NONE, GameStrings.Get("VO_INNKEEPER_CM_WELCOME"), "VO_INNKEEPER_Male_Dwarf_CM_WELCOME_23.prefab:c8afdeaaf2189eb42aad9d29f6a97994", 0f, null, false);
			Options.Get().SetBool(Option.HAS_SEEN_COLLECTIONMANAGER, true);
			yield return new WaitForSeconds(3.5f);
		}
		else
		{
			yield return new WaitForSeconds(1f);
		}
		if (!Options.Get().GetBool(Option.HAS_STARTED_A_DECK, false) && UserAttentionManager.CanShowAttentionGrabber("CollectionManagerDisplay.ShowCollectionTipsIfNeeded:" + Option.HAS_STARTED_A_DECK))
		{
			this.m_deckHelpPopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, this.m_editDeckTutorialBone.position, this.m_editDeckTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL07"), true, NotificationManager.PopupTextType.BASIC);
			this.m_deckHelpPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			this.m_deckHelpPopup.PulseReminderEveryXSeconds(3f);
		}
		yield break;
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x00064CE8 File Offset: 0x00062EE8
	private void ShowDeckTemplateTipsIfNeeded()
	{
		bool flag = this.m_deckHelpPopup != null && this.m_deckHelpPopup.gameObject != null;
		Notification deckHelpPopup = CollectionDeckTray.Get().GetCardsContent().GetDeckHelpPopup();
		bool flag2 = deckHelpPopup != null && deckHelpPopup.gameObject != null;
		bool flag3 = this.m_createDeckNotification != null && this.m_createDeckNotification.gameObject != null;
		bool flag4 = (this.m_craftingTray != null && this.m_craftingTray.IsShown()) || CraftingManager.GetIsInCraftingMode() || DeckHelper.Get().IsActive() || flag || flag2 || flag3 || SceneMgr.Get().IsInDuelsMode() || CollectionDeckTray.Get().GetDecksContent().IsShowingDeckOptions;
		DeckTrayDeckTileVisual firstInvalidCard = CollectionDeckTray.Get().GetCardsContent().GetFirstInvalidCard();
		if (!(firstInvalidCard != null) || flag4)
		{
			if (this.m_showingDeckTemplateTips)
			{
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1"), 0f);
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2"), 0f);
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS"), 0f);
				NotificationManager.Get().DestroyNotificationWithText(GameStrings.Get("GLUE_COLLECTION_TUTORIAL_REPLACE_WILD_CARDS_NPR"), 0f);
			}
			this.m_deckTemplateTipWaitTime = 0f;
			this.m_showingDeckTemplateTips = false;
			return;
		}
		if (this.m_showingDeckTemplateTips)
		{
			return;
		}
		if (this.m_currentViewMode != CollectionUtils.ViewMode.DECK_TEMPLATE && (CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards || !CollectionDeckTray.Get().GetCardsContent().HasFinishedEntering()))
		{
			return;
		}
		string text;
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
			if (this.m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
			{
				if (UniversalInputManager.UsePhoneUI)
				{
					firstInvalidCard = this.m_deckTemplatePickerPhone.m_phoneTray.GetCardsContent().GetFirstInvalidCard();
					if (firstInvalidCard == null)
					{
						Debug.LogError("Phone Template Tray and CollectionDeckTray mismatch. Missing invalid card on Template.");
						return;
					}
				}
				text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_1");
				if (this.m_deckTemplateTipWaitTime < 0.5f)
				{
					this.m_deckTemplateTipWaitTime += Time.deltaTime;
					return;
				}
			}
			else
			{
				text = GameStrings.Get("GLUE_COLLECTION_TUTORIAL_TEMPLATE_REPLACE_2");
				if (this.m_deckTemplateTipWaitTime < 1f)
				{
					this.m_deckTemplateTipWaitTime += Time.deltaTime;
					return;
				}
			}
		}
		float num = -60f;
		Vector3 relativePosition = OverlayUI.Get().GetRelativePosition(firstInvalidCard.transform.position, Box.Get().m_Camera.GetComponent<Camera>(), OverlayUI.Get().m_heightScale.m_Center, 0f);
		Vector3 scale;
		if (UniversalInputManager.UsePhoneUI)
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
		if (this.m_currentViewMode == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			this.m_deckTemplateCardReplacePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.NONE, relativePosition, scale, text, false, NotificationManager.PopupTextType.BASIC);
			if (this.m_deckTemplateCardReplacePopup != null)
			{
				this.m_deckTemplateCardReplacePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				NotificationManager.Get().DestroyNotification(this.m_deckTemplateCardReplacePopup, 3.5f);
			}
		}
		else
		{
			this.m_deckTemplateCardReplacePopup = NotificationManager.Get().CreatePopupText(UserAttentionBlocker.SET_ROTATION_CM_TUTORIALS, relativePosition, scale, text, false, NotificationManager.PopupTextType.BASIC);
			if (this.m_deckTemplateCardReplacePopup != null)
			{
				this.m_deckTemplateCardReplacePopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
				this.m_deckTemplateCardReplacePopup.PulseReminderEveryXSeconds(3f);
			}
		}
		this.m_deckTemplateTipWaitTime = 0f;
		this.m_showingDeckTemplateTips = true;
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x00065118 File Offset: 0x00063318
	protected override void OnSwitchViewModeResponse(bool triggerResponse, CollectionUtils.ViewMode prevMode, CollectionUtils.ViewMode newMode, CollectionUtils.ViewModeData userdata)
	{
		base.OnSwitchViewModeResponse(triggerResponse, prevMode, newMode, userdata);
		this.EnableSetAndManaFiltersByViewMode(newMode);
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x0006512C File Offset: 0x0006332C
	private void EnableSetAndManaFiltersByViewMode(CollectionUtils.ViewMode viewMode)
	{
		bool enabled = viewMode == CollectionUtils.ViewMode.CARDS;
		this.EnableSetAndManaFilters(enabled);
	}

	// Token: 0x060011B4 RID: 4532 RVA: 0x00065148 File Offset: 0x00063348
	private void EnableSetAndManaFilters(bool enabled)
	{
		if (UniversalInputManager.UsePhoneUI)
		{
			this.m_craftingModeButton.Enable(enabled);
		}
		this.m_manaTabManager.Enabled = enabled;
		if (this.m_setFilterTray != null)
		{
			this.m_setFilterTray.SetButtonEnabled(enabled);
			if (UniversalInputManager.UsePhoneUI)
			{
				this.m_setFilterTray.gameObject.SetActive(enabled);
			}
		}
		this.m_search.SetEnabled(true);
	}

	// Token: 0x060011B5 RID: 4533 RVA: 0x000651BC File Offset: 0x000633BC
	private void OnSetFilterButtonPressed()
	{
		this.SetLastSeenFeaturedCardsEvent(this.m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		this.m_setFilterTray.SetFilterButtonGlowActive(false);
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000651DB File Offset: 0x000633DB
	private void OnPhoneFilterButtonPressed()
	{
		this.SetLastSeenFeaturedCardsEvent(this.m_currentActiveFeaturedCardsEvent, GameSaveKeySubkeyId.COLLECTION_MANAGER_LAST_SEEN_FEATURED_CARDS_EVENT_BUTTON);
		this.m_filterButtonGlow.SetActive(false);
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x000651FC File Offset: 0x000633FC
	private void OnCraftingTrayLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		go.SetActive(false);
		this.m_craftingTray = go.GetComponent<CraftingTray>();
		go.transform.parent = this.m_craftingTrayShownBone.transform.parent;
		go.transform.localPosition = this.m_craftingTrayHiddenBone.transform.localPosition;
		go.transform.localScale = this.m_craftingTrayHiddenBone.transform.localScale;
		this.m_pageManager.UpdateMassDisenchant();
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x00065278 File Offset: 0x00063478
	private void OnCraftingModeButtonReleased(UIEvent e)
	{
		if (this.m_craftingTray.IsShown())
		{
			this.m_craftingTray.Hide();
			return;
		}
		this.ShowCraftingTray(null, null, null, true);
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000652C0 File Offset: 0x000634C0
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
		this.HideDeckHelpPopup();
		this.m_craftingTray.gameObject.SetActive(true);
		this.m_craftingTray.Show(includeUncraftable, showOnlyGolden, showOnlyDiamond, updatePage);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_craftingTrayShownBone.transform.localPosition,
			"isLocal",
			true,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutBounce
		});
		iTween.Stop(this.m_craftingTray.gameObject);
		iTween.MoveTo(this.m_craftingTray.gameObject, args);
		this.m_craftingModeButton.ShowActiveGlow(true);
		this.ShowAppropriateSetFilters();
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000653B0 File Offset: 0x000635B0
	public void HideCraftingTray()
	{
		this.m_craftingTray.gameObject.SetActive(true);
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_craftingTrayHiddenBone.transform.localPosition,
			"isLocal",
			true,
			"time",
			0.25f,
			"easeType",
			iTween.EaseType.easeOutBounce,
			"oncomplete",
			new Action<object>(delegate(object o)
			{
				this.m_craftingTray.gameObject.SetActive(false);
			})
		});
		iTween.Stop(this.m_craftingTray.gameObject);
		iTween.MoveTo(this.m_craftingTray.gameObject, args);
		this.m_craftingModeButton.ShowActiveGlow(false);
		this.ShowAppropriateSetFilters();
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0006547E File Offset: 0x0006367E
	public void ShowConvertTutorial(UserAttentionBlocker blocker)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "CollectionManagerDisplay.ShowConvertTutorial"))
		{
			return;
		}
		this.m_showConvertTutorialCoroutine = this.ShowConvertTutorialCoroutine(blocker);
		base.StartCoroutine(this.m_showConvertTutorialCoroutine);
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000654A8 File Offset: 0x000636A8
	private IEnumerator ShowConvertTutorialCoroutine(UserAttentionBlocker blocker)
	{
		if (this.m_createDeckNotification != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_createDeckNotification, 0.25f);
		}
		CollectionDeckTray deckTray = CollectionDeckTray.Get();
		while (deckTray.IsUpdatingTrayMode() || !deckTray.GetDecksContent().IsDoneEntering())
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		if (!this.ViewModeHasVisibleDeckList())
		{
			yield break;
		}
		this.m_convertTutorialPopup = NotificationManager.Get().CreatePopupText(blocker, this.m_convertDeckTutorialBone.position, this.m_convertDeckTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL12"), true, NotificationManager.PopupTextType.BASIC);
		if (this.m_convertTutorialPopup != null)
		{
			this.m_convertTutorialPopup.ShowPopUpArrow(Notification.PopUpArrowDirection.Right);
			this.m_convertTutorialPopup.PulseReminderEveryXSeconds(3f);
		}
		this.m_showConvertTutorialCoroutine = null;
		yield break;
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000654BE File Offset: 0x000636BE
	public void HideConvertTutorial()
	{
		if (this.m_showConvertTutorialCoroutine != null)
		{
			base.StopCoroutine(this.m_showConvertTutorialCoroutine);
			this.m_showConvertTutorialCoroutine = null;
		}
		if (this.m_convertTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_convertTutorialPopup, 0.25f);
		}
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x000654FE File Offset: 0x000636FE
	public void ShowSetFilterTutorial(UserAttentionBlocker blocker)
	{
		if (!UserAttentionManager.CanShowAttentionGrabber(blocker, "CollectionManagerDisplay.ShowSetFilterTutorial"))
		{
			return;
		}
		this.m_showSetFilterTutorialCoroutine = this.ShowSetFilterTutorialCoroutine(blocker);
		base.StartCoroutine(this.m_showSetFilterTutorialCoroutine);
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x00065528 File Offset: 0x00063728
	private IEnumerator ShowSetFilterTutorialCoroutine(UserAttentionBlocker blocker)
	{
		if (this.m_setFilterTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_setFilterTutorialPopup, 0f);
		}
		this.m_setFilterTutorialPopup = NotificationManager.Get().CreatePopupText(blocker, this.m_setFilterTutorialBone.position, this.m_setFilterTutorialBone.localScale, GameStrings.Get("GLUE_COLLECTION_TUTORIAL17"), true, NotificationManager.PopupTextType.BASIC);
		if (this.m_setFilterTutorialPopup != null)
		{
			this.m_setFilterTutorialPopup.ShowPopUpArrow(UniversalInputManager.UsePhoneUI ? Notification.PopUpArrowDirection.Up : Notification.PopUpArrowDirection.LeftDown);
			this.m_setFilterTutorialPopup.PulseReminderEveryXSeconds(3f);
		}
		yield return new WaitForSeconds(6f);
		this.HideSetFilterTutorial();
		yield break;
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0006553E File Offset: 0x0006373E
	public void HideSetFilterTutorial()
	{
		if (this.m_showSetFilterTutorialCoroutine != null)
		{
			base.StopCoroutine(this.m_showSetFilterTutorialCoroutine);
			this.m_showSetFilterTutorialCoroutine = null;
		}
		if (this.m_setFilterTutorialPopup != null)
		{
			NotificationManager.Get().DestroyNotification(this.m_setFilterTutorialPopup, 0.25f);
		}
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0006557E File Offset: 0x0006377E
	public void ShowStandardInfoTutorial(UserAttentionBlocker blocker)
	{
		NotificationManager.Get().CreateInnkeeperQuote(blocker, GameStrings.Get("GLUE_COLLECTION_TUTORIAL13"), "VO_INNKEEPER_Male_Dwarf_STANDARD_WELCOME3_14.prefab:51e1d835435b64542b9a77944e00cc19", 0f, null, false);
	}

	// Token: 0x060011C2 RID: 4546 RVA: 0x000655A4 File Offset: 0x000637A4
	public void CheckClipboardAndPromptPlayerToPaste()
	{
		if (!this.CheckIfClipboardNotificationHasBeenShown())
		{
			return;
		}
		string text;
		if (!this.CheckClipboardAndGetValidityMessaging(out text))
		{
			if (text != string.Empty)
			{
				CollectionInputMgr.AlertPlayerOnInvalidDeckPaste(text);
			}
			return;
		}
		string text2 = GameStrings.Get("GLUE_COLLECTION_DECK_VALID_PASTE_BODY");
		string headerText = GameStrings.Get("GLUE_COLLECTION_DECK_VALID_PASTE_HEADER");
		if (CollectionManager.Get().IsInEditMode() && CollectionManager.Get().GetEditedDeck().GetTotalCardCount() > 0)
		{
			text2 = GameStrings.Get("GLUE_COLLECTION_DECK_OVERWRITE_BODY");
			headerText = GameStrings.Get("GLUE_COLLECTION_DECK_OVERWRITE_HEADER");
		}
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = headerText,
			m_text = text2,
			m_cancelText = GameStrings.Get("GLUE_COLLECTION_DECK_SAVE_ANYWAY"),
			m_confirmText = GameStrings.Get("GLUE_COLLECTION_DECK_FINISH_FOR_ME"),
			m_showAlertIcon = false,
			m_responseDisplay = AlertPopup.ResponseDisplay.CONFIRM_CANCEL,
			m_responseCallback = delegate(AlertPopup.Response response, object userData)
			{
				if (response == AlertPopup.Response.CANCEL)
				{
					this.RejectDeckFromClipboard();
					return;
				}
				this.CreateDeckFromClipboard(this.m_cachedShareableDeck);
			}
		};
		DialogManager.Get().ShowPopup(info);
	}

	// Token: 0x060011C3 RID: 4547 RVA: 0x00065684 File Offset: 0x00063884
	private bool CheckIfClipboardNotificationHasBeenShown()
	{
		if (PlatformSettings.OS != OSCategory.iOS || Options.Get().GetBool(Option.HAS_SEEN_CLIPBOARD_NOTIFICATION, false))
		{
			return true;
		}
		if (DialogManager.Get().ShowingDialog())
		{
			return false;
		}
		string headerText = GameStrings.Get("GLUE_COLLECTION_DECK_CLIPBOARD_ACCESS_HEADER");
		string text = GameStrings.Get("GLUE_COLLECTION_DECK_CLIPBOARD_ACCESS_BODY");
		AlertPopup.PopupInfo popupInfo = new AlertPopup.PopupInfo();
		popupInfo.m_headerText = headerText;
		popupInfo.m_text = text;
		popupInfo.m_showAlertIcon = false;
		popupInfo.m_responseCallback = delegate(AlertPopup.Response response, object userData)
		{
			Options.Get().SetBool(Option.HAS_SEEN_CLIPBOARD_NOTIFICATION, true);
		};
		AlertPopup.PopupInfo info = popupInfo;
		DialogManager.Get().ShowPopup(info);
		return false;
	}

	// Token: 0x060011C4 RID: 4548 RVA: 0x00065720 File Offset: 0x00063920
	public void PasteFromClipboardIfValidOrShowStatusMessage()
	{
		if (!this.CheckIfClipboardNotificationHasBeenShown())
		{
			return;
		}
		string message;
		if (!this.CheckClipboardAndGetValidityMessaging(out message))
		{
			UIStatus.Get().AddInfo(message);
			return;
		}
		ClipboardUtils.CopyToClipboard(string.Empty);
		this.CreateDeckFromClipboard(this.m_cachedShareableDeck);
	}

	// Token: 0x060011C5 RID: 4549 RVA: 0x00065764 File Offset: 0x00063964
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
			if ((this.m_cachedShareableDeck != null && this.m_cachedShareableDeck.Equals(shareableDeck)) || !this.CanPasteShareableDeck(shareableDeck))
			{
				return false;
			}
			DialogManager.Get().ClearAllImmediately();
		}
		this.m_cachedShareableDeck = shareableDeck;
		return this.CanPasteShareableDeck(this.m_cachedShareableDeck, out message);
	}

	// Token: 0x060011C6 RID: 4550 RVA: 0x000657D0 File Offset: 0x000639D0
	private bool CanPasteShareableDeck(ShareableDeck shareableDeck)
	{
		string text;
		return this.CanPasteShareableDeck(shareableDeck, out text);
	}

	// Token: 0x060011C7 RID: 4551 RVA: 0x000657E8 File Offset: 0x000639E8
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
		string text = GameUtils.TranslateDbIdToCardId(shareableDeck.HeroCardDbId, false);
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
		if (scenarioDbId != ScenarioDbId.INVALID)
		{
			ScenarioDbfRecord record = GameDbf.Scenario.GetRecord((int)scenarioDbId);
			if (record != null)
			{
				using (List<ClassExclusionsDbfRecord>.Enumerator enumerator = record.ClassExclusions.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.ClassId == (int)@class)
						{
							return false;
						}
					}
				}
			}
		}
		if (!bestHeroesIOwn.Any<CollectibleCard>())
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
			if (editedDeck.GetShareableDeck().Equals(this.m_cachedShareableDeck))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060011C8 RID: 4552 RVA: 0x000659B4 File Offset: 0x00063BB4
	private void CreateDeckFromClipboard(ShareableDeck shareableDeck)
	{
		bool flag = SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER;
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(shareableDeck.HeroCardDbId, true).GetClass();
		NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(@class);
		string heroCardID;
		if (favoriteHero == null)
		{
			heroCardID = CollectionManager.GetHeroCardId(@class, CardHero.HeroType.VANILLA);
		}
		else
		{
			heroCardID = favoriteHero.Name;
		}
		if (flag)
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
			CollectionDeckTray.Get().GetDecksContent().CreateNewDeckFromUserSelection(@class, heroCardID, customDeckName, DeckSourceType.DECK_SOURCE_TYPE_PASTED_DECK, shareableDeck.Serialize(false));
			CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedFromClipboard));
			CollectionManager.Get().RemoveDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedByPlayer));
			if (HeroPickerDisplay.Get() != null && HeroPickerDisplay.Get().IsShown())
			{
				DeckPickerTrayDisplay.Get().SkipHeroSelectionAndCloseTray();
				return;
			}
		}
		else
		{
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
				return;
			}
			this.OnDeckCreatedFromClipboard(editedDeck.ID);
		}
	}

	// Token: 0x060011C9 RID: 4553 RVA: 0x00065B1C File Offset: 0x00063D1C
	private void OnDeckCreatedFromClipboard(long deckId)
	{
		CollectionManager.Get().RemoveDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedFromClipboard));
		CollectionManager.Get().RegisterDeckCreatedListener(new CollectionManager.DelOnDeckCreated(this.OnDeckCreatedByPlayer));
		bool flag = CollectionManager.Get().IsInEditMode();
		if (base.GetViewMode() == CollectionUtils.ViewMode.DECK_TEMPLATE)
		{
			DeckTemplatePicker deckTemplatePicker = UniversalInputManager.UsePhoneUI ? this.GetPhoneDeckTemplateTray() : this.m_pageManager.GetDeckTemplatePicker();
			if (deckTemplatePicker != null)
			{
				Navigation.RemoveHandler(new Navigation.NavigateBackHandler(deckTemplatePicker.OnNavigateBack));
			}
			if (UniversalInputManager.UsePhoneUI)
			{
				base.StartCoroutine(deckTemplatePicker.EnterDeckPhone());
			}
		}
		if (CollectionDeckTray.Get().GetCurrentContentType() != DeckTray.DeckContentTypes.Cards)
		{
			CollectionDeckTray.Get().RegisterModeSwitchedListener(new DeckTray.ModeSwitched(this.OnCollectionDeckTrayModeSwitched));
			this.ShowDeck(deckId, !flag, false, new CollectionUtils.ViewMode?(CollectionUtils.ViewMode.CARDS));
			return;
		}
		this.ShowDeck(deckId, !flag, false, new CollectionUtils.ViewMode?(CollectionUtils.ViewMode.CARDS));
		this.OnCollectionDeckTrayModeSwitched();
	}

	// Token: 0x060011CA RID: 4554 RVA: 0x00065C0C File Offset: 0x00063E0C
	private void OnCollectionDeckTrayModeSwitched()
	{
		CollectionDeckTray.Get().UnregisterModeSwitchedListener(new DeckTray.ModeSwitched(this.OnCollectionDeckTrayModeSwitched));
		if (this.m_cachedShareableDeck != null)
		{
			CollectionInputMgr.PasteDeckInEditModeFromShareableDeck(this.m_cachedShareableDeck);
		}
		else
		{
			CollectionInputMgr.PasteDeckFromClipboard();
		}
		ClipboardUtils.CopyToClipboard(string.Empty);
		this.m_cachedShareableDeck = null;
	}

	// Token: 0x060011CB RID: 4555 RVA: 0x00065C5A File Offset: 0x00063E5A
	private void RejectDeckFromClipboard()
	{
		ClipboardUtils.CopyToClipboard(string.Empty);
		this.m_cachedShareableDeck = null;
	}

	// Token: 0x060011CC RID: 4556 RVA: 0x00065C6D File Offset: 0x00063E6D
	public static bool ShouldShowDeckOptionsMenu()
	{
		return !SceneMgr.Get().IsInDuelsMode();
	}

	// Token: 0x060011CD RID: 4557 RVA: 0x000052EC File Offset: 0x000034EC
	public static bool ShouldShowDeckHeaderInfo()
	{
		return true;
	}

	// Token: 0x060011CE RID: 4558 RVA: 0x00065C7E File Offset: 0x00063E7E
	public static bool IsSpecialOneDeckMode()
	{
		return SceneMgr.Get().IsInTavernBrawlMode() || SceneMgr.Get().IsInDuelsMode();
	}

	// Token: 0x04000B4C RID: 2892
	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateHiddenBone;

	// Token: 0x04000B4D RID: 2893
	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateShownBone;

	// Token: 0x04000B4E RID: 2894
	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateTutorialWelcomeBone;

	// Token: 0x04000B4F RID: 2895
	[CustomEditField(Sections = "Bones")]
	public Transform m_deckTemplateTutorialReminderBone;

	// Token: 0x04000B50 RID: 2896
	[CustomEditField(Sections = "Bones")]
	public Transform m_editDeckTutorialBone;

	// Token: 0x04000B51 RID: 2897
	[CustomEditField(Sections = "Bones")]
	public Transform m_convertDeckTutorialBone;

	// Token: 0x04000B52 RID: 2898
	[CustomEditField(Sections = "Bones")]
	public Transform m_setFilterTutorialBone;

	// Token: 0x04000B53 RID: 2899
	[CustomEditField(Sections = "Objects")]
	public ManaFilterTabManager m_manaTabManager;

	// Token: 0x04000B54 RID: 2900
	[CustomEditField(Sections = "Objects")]
	public CraftingModeButton m_craftingModeButton;

	// Token: 0x04000B55 RID: 2901
	[CustomEditField(Sections = "Objects")]
	public Notification m_deckTemplateCardReplacePopup;

	// Token: 0x04000B56 RID: 2902
	[CustomEditField(Sections = "Objects")]
	public NestedPrefab m_setFilterTrayContainer;

	// Token: 0x04000B57 RID: 2903
	[CustomEditField(Sections = "Controls")]
	public Texture m_allSetsTexture;

	// Token: 0x04000B58 RID: 2904
	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_allSetsIconOffset;

	// Token: 0x04000B59 RID: 2905
	[CustomEditField(Sections = "Controls")]
	public Texture m_wildSetsTexture;

	// Token: 0x04000B5A RID: 2906
	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_wildSetsIconOffset;

	// Token: 0x04000B5B RID: 2907
	[CustomEditField(Sections = "Controls")]
	public Texture m_featuredCardsTexture;

	// Token: 0x04000B5C RID: 2908
	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_featuredCardsIconOffset;

	// Token: 0x04000B5D RID: 2909
	[CustomEditField(Sections = "Controls")]
	public Texture m_classicSetsTexture;

	// Token: 0x04000B5E RID: 2910
	[CustomEditField(Sections = "Controls")]
	public UnityEngine.Vector2 m_classicSetsIconOffset;

	// Token: 0x04000B5F RID: 2911
	[CustomEditField(Sections = "CM Customization Ref")]
	public GameObject m_bookBack;

	// Token: 0x04000B60 RID: 2912
	[CustomEditField(Sections = "CM Customization Ref")]
	[FormerlySerializedAs("m_tavernBrawlBookBackMesh")]
	public Mesh m_customBookBackMesh;

	// Token: 0x04000B61 RID: 2913
	[CustomEditField(Sections = "CM Customization Ref")]
	[FormerlySerializedAs("m_tavernBrawlObjectsToSwap")]
	public List<GameObject> m_customObjectsToSwap = new List<GameObject>();

	// Token: 0x04000B62 RID: 2914
	[CustomEditField(Sections = "Tavern Brawl Changes", T = EditType.TEXTURE)]
	[FormerlySerializedAs("m_corkBackTexture")]
	public string m_tbCorkBackTexture;

	// Token: 0x04000B63 RID: 2915
	[CustomEditField(Sections = "Tavern Brawl Changes")]
	public Material m_tavernBrawlElements;

	// Token: 0x04000B64 RID: 2916
	[CustomEditField(Sections = "Duels Changes", T = EditType.TEXTURE)]
	public string m_duelsCorkBackTexture;

	// Token: 0x04000B65 RID: 2917
	[CustomEditField(Sections = "Duels Changes")]
	public Material m_duelsElements;

	// Token: 0x04000B66 RID: 2918
	private Map<TAG_CLASS, AssetHandle<Texture>> m_loadedClassTextures = new Map<TAG_CLASS, AssetHandle<Texture>>();

	// Token: 0x04000B67 RID: 2919
	private AssetHandle<Texture> m_loadedCorkBackTexture;

	// Token: 0x04000B68 RID: 2920
	private bool m_selectingNewDeckHero;

	// Token: 0x04000B69 RID: 2921
	private long m_showDeckContentsRequest;

	// Token: 0x04000B6A RID: 2922
	private Notification m_deckHelpPopup;

	// Token: 0x04000B6B RID: 2923
	private Notification m_innkeeperLClickReminder;

	// Token: 0x04000B6C RID: 2924
	private List<CollectibleDisplay.FilterStateListener> m_setFilterListeners = new List<CollectibleDisplay.FilterStateListener>();

	// Token: 0x04000B6D RID: 2925
	private List<CollectibleDisplay.FilterStateListener> m_manaFilterListeners = new List<CollectibleDisplay.FilterStateListener>();

	// Token: 0x04000B6E RID: 2926
	private DeckTemplatePicker m_deckTemplatePickerPhone;

	// Token: 0x04000B6F RID: 2927
	private HeroPickerDisplay m_heroPickerDisplay;

	// Token: 0x04000B70 RID: 2928
	private Notification m_createDeckNotification;

	// Token: 0x04000B71 RID: 2929
	private Notification m_convertTutorialPopup;

	// Token: 0x04000B72 RID: 2930
	private IEnumerator m_showConvertTutorialCoroutine;

	// Token: 0x04000B73 RID: 2931
	private Notification m_setFilterTutorialPopup;

	// Token: 0x04000B74 RID: 2932
	private IEnumerator m_showSetFilterTutorialCoroutine;

	// Token: 0x04000B75 RID: 2933
	private bool m_showingDeckTemplateTips;

	// Token: 0x04000B76 RID: 2934
	private float m_deckTemplateTipWaitTime;

	// Token: 0x04000B77 RID: 2935
	private bool m_manaFilterIsFromSearchText;

	// Token: 0x04000B78 RID: 2936
	private ShareableDeck m_cachedShareableDeck;

	// Token: 0x04000B79 RID: 2937
	private string m_currentActiveFeaturedCardsEvent;

	// Token: 0x02001479 RID: 5241
	[Serializable]
	public class CardSetIconMatOffset
	{
		// Token: 0x0400A9D6 RID: 43478
		public TAG_CARD_SET m_cardSet;

		// Token: 0x0400A9D7 RID: 43479
		public UnityEngine.Vector2 m_offset;
	}
}
