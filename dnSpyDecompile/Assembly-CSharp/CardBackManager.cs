using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

// Token: 0x020000DC RID: 220
public class CardBackManager : IService
{
	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00049E27 File Offset: 0x00048027
	private GameObject SceneObject
	{
		get
		{
			if (this.m_sceneObject == null)
			{
				this.m_sceneObject = new GameObject("CardBackManagerSceneObject", new Type[]
				{
					typeof(HSDontDestroyOnLoad)
				});
			}
			return this.m_sceneObject;
		}
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00049E60 File Offset: 0x00048060
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().Resetting += this.Resetting;
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.FavoriteCardBackChanged += this.OnFavoriteCardBackChanged;
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheCardBacks), new Action(this.NetCache_OnNetCacheCardBacksUpdated));
		this.InitCardBackData();
		Options.Get().RegisterChangedListener(Option.CARD_BACK, new Options.ChangedCallback(this.OnCheatOptionChanged));
		Options.Get().RegisterChangedListener(Option.CARD_BACK2, new Options.ChangedCallback(this.OnCheatOptionChanged));
		serviceLocator.Get<SceneMgr>().RegisterSceneLoadedEvent(new SceneMgr.SceneLoadedCallback(this.OnSceneLoaded));
		this.InitCardBacks();
		yield break;
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x00049E76 File Offset: 0x00048076
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(GameDbf),
			typeof(IAssetLoader),
			typeof(NetCache),
			typeof(SceneMgr)
		};
	}

	// Token: 0x06000CC3 RID: 3267 RVA: 0x00049EB4 File Offset: 0x000480B4
	public void Shutdown()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.FavoriteCardBackChanged -= this.OnFavoriteCardBackChanged;
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.Resetting -= this.Resetting;
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00049EFD File Offset: 0x000480FD
	private void Resetting()
	{
		this.InitCardBackData();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00049F05 File Offset: 0x00048105
	public static CardBackManager Get()
	{
		return HearthstoneServices.Get<CardBackManager>();
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00049F0C File Offset: 0x0004810C
	public bool RegisterUpdateCardbacksListener(CardBackManager.UpdateCardbacksCallback callback)
	{
		CardBackManager.UpdateCardbacksListener updateCardbacksListener = new CardBackManager.UpdateCardbacksListener();
		updateCardbacksListener.SetCallback(callback);
		if (this.m_updateCardbacksListeners.Contains(updateCardbacksListener))
		{
			return false;
		}
		object obj = this.cardbackListenerCollectionLock;
		lock (obj)
		{
			this.m_updateCardbacksListeners.Add(updateCardbacksListener);
		}
		return true;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00049F70 File Offset: 0x00048170
	public bool UnregisterUpdateCardbacksListener(CardBackManager.UpdateCardbacksCallback callback)
	{
		CardBackManager.UpdateCardbacksListener updateCardbacksListener = new CardBackManager.UpdateCardbacksListener();
		updateCardbacksListener.SetCallback(callback);
		bool result = false;
		object obj = this.cardbackListenerCollectionLock;
		lock (obj)
		{
			result = this.m_updateCardbacksListeners.Remove(updateCardbacksListener);
		}
		return result;
	}

	// Token: 0x06000CC8 RID: 3272 RVA: 0x00049FC8 File Offset: 0x000481C8
	public void SetSearchText(string searchText)
	{
		this.m_searchText = ((searchText != null) ? searchText.ToLower() : null);
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00049FDC File Offset: 0x000481DC
	public int GetDeckCardBackID(long deck)
	{
		NetCache.DeckHeader deckHeader = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Find((NetCache.DeckHeader obj) => obj.ID == deck);
		if (deckHeader == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("CardBackManager.GetDeckCardBackID() could not find deck with ID {0}", deck));
			return 0;
		}
		return deckHeader.CardBack;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0004A03C File Offset: 0x0004823C
	public CardBack GetFriendlyCardBack()
	{
		return this.GetCardBackBySlot(CardBackManager.CardBackSlot.FRIENDLY);
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x0004A045 File Offset: 0x00048245
	public CardBack GetOpponentCardBack()
	{
		return this.GetCardBackBySlot(CardBackManager.CardBackSlot.OPPONENT);
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x0004A04E File Offset: 0x0004824E
	public CardBack GetCardBackForActor(Actor actor)
	{
		if (this.IsActorFriendly(actor))
		{
			return this.GetFriendlyCardBack();
		}
		return this.GetOpponentCardBack();
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0004A068 File Offset: 0x00048268
	public CardBack GetCardBackBySlot(CardBackManager.CardBackSlot slot)
	{
		CardBackManager.CardBackSlotData cardBackSlotData;
		if (this.m_LoadedCardBacksBySlot.TryGetValue(slot, out cardBackSlotData))
		{
			return cardBackSlotData.m_cardBack;
		}
		return null;
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x0004A090 File Offset: 0x00048290
	public bool IsCardBackLoading(CardBackManager.CardBackSlot slot)
	{
		CardBackManager.CardBackSlotData cardBackSlotData;
		return this.m_LoadedCardBacksBySlot.TryGetValue(slot, out cardBackSlotData) && cardBackSlotData.m_isLoading;
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x0004A0B5 File Offset: 0x000482B5
	public void UpdateAllCardBacksInSceneWhenReady()
	{
		Processor.RunCoroutine(this.UpdateAllCardBacksInSceneWhenReadyImpl(), null);
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x0004A0C4 File Offset: 0x000482C4
	public void SetGameCardBackIDs(int friendlyCardBackID, int opponentCardBackID)
	{
		int validCardBackID = this.GetValidCardBackID(friendlyCardBackID);
		this.LoadCardBack(this.m_cardBackData[validCardBackID].PrefabName, CardBackManager.CardBackSlot.FRIENDLY);
		int validCardBackID2 = this.GetValidCardBackID(opponentCardBackID);
		this.LoadCardBack(this.m_cardBackData[validCardBackID2].PrefabName, CardBackManager.CardBackSlot.OPPONENT);
		this.UpdateAllCardBacksInSceneWhenReady();
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x0004A124 File Offset: 0x00048324
	public bool LoadCardBackByIndex(int cardBackIdx, CardBackManager.LoadCardBackData.LoadCardBackCallback callback, object callbackData = null)
	{
		string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
		return this.LoadCardBackByIndex(cardBackIdx, callback, false, actorName, callbackData);
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0004A142 File Offset: 0x00048342
	public bool LoadCardBackByIndex(int cardBackIdx, CardBackManager.LoadCardBackData.LoadCardBackCallback callback, string actorName, object callbackData = null)
	{
		return this.LoadCardBackByIndex(cardBackIdx, callback, false, actorName, callbackData);
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x0004A150 File Offset: 0x00048350
	public bool LoadCardBackByIndex(int cardBackIdx, CardBackManager.LoadCardBackData.LoadCardBackCallback callback, bool unlit, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", object callbackData = null)
	{
		if (!this.m_cardBackData.ContainsKey(cardBackIdx))
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - wrong cardBackIdx {0}", new object[]
			{
				cardBackIdx
			});
			return false;
		}
		CardBackManager.LoadCardBackData loadCardBackData = new CardBackManager.LoadCardBackData();
		loadCardBackData.m_CardBackIndex = cardBackIdx;
		loadCardBackData.m_Callback = callback;
		loadCardBackData.m_Unlit = unlit;
		loadCardBackData.m_Name = this.m_cardBackData[cardBackIdx].Name;
		loadCardBackData.callbackData = callbackData;
		AssetLoader.Get().InstantiatePrefab(actorName, new PrefabCallback<GameObject>(this.OnHiddenActorLoaded), loadCardBackData, AssetLoadingOptions.IgnorePrefabPosition);
		return true;
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0004A1E8 File Offset: 0x000483E8
	public CardBackManager.LoadCardBackData LoadCardBackByIndex(int cardBackIdx, bool unlit = false, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", bool shadowActive = false)
	{
		if (!this.m_cardBackData.ContainsKey(cardBackIdx))
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - wrong cardBackIdx {0}", new object[]
			{
				cardBackIdx
			});
			return null;
		}
		CardBackManager.LoadCardBackData loadCardBackData = new CardBackManager.LoadCardBackData();
		loadCardBackData.m_CardBackIndex = cardBackIdx;
		loadCardBackData.m_Unlit = unlit;
		loadCardBackData.m_Name = this.m_cardBackData[cardBackIdx].Name;
		loadCardBackData.m_GameObject = AssetLoader.Get().InstantiatePrefab(actorName, AssetLoadingOptions.IgnorePrefabPosition);
		if (loadCardBackData.m_GameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - failed to load Actor {0}", new object[]
			{
				actorName
			});
			return null;
		}
		string prefabName = this.m_cardBackData[cardBackIdx].PrefabName;
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabName, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - failed to load CardBack {0}", new object[]
			{
				prefabName
			});
			return null;
		}
		CardBack componentInChildren = gameObject.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.LoadCardBackByIndex() - cardback=null");
			return null;
		}
		loadCardBackData.m_CardBack = componentInChildren;
		Actor component = loadCardBackData.m_GameObject.GetComponent<Actor>();
		CardBackManager.SetCardBack(component.m_cardMesh, loadCardBackData.m_CardBack, loadCardBackData.m_Unlit, shadowActive);
		component.SetCardbackUpdateIgnore(true);
		loadCardBackData.m_CardBack.gameObject.transform.parent = loadCardBackData.m_GameObject.transform;
		return loadCardBackData;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0004A344 File Offset: 0x00048544
	public static Actor LoadCardBackActorByPrefab(string cardBackPrefab, bool unlit = false, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", bool shadowActive = false)
	{
		if (AssetLoader.Get() == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.LoadCardBackActorByPrefab() - AssetLoader not available");
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(cardBackPrefab, AssetLoadingOptions.None);
		if (gameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackActorByPrefab() - failed to load CardBack {0}", new object[]
			{
				cardBackPrefab
			});
			return null;
		}
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(actorName, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackActorByPrefab() - failed to load Actor {0}", new object[]
			{
				actorName
			});
			return null;
		}
		Actor component = gameObject2.GetComponent<Actor>();
		CardBack componentInChildren = gameObject.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.LoadCardBackActorByPrefab() - cardback=null");
			return null;
		}
		CardBackManager.SetCardBack(component.m_cardMesh, componentInChildren, unlit, shadowActive);
		component.SetCardbackUpdateIgnore(true);
		componentInChildren.gameObject.transform.parent = gameObject2.transform;
		return component;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0004A420 File Offset: 0x00048620
	public void AddNewCardBack(int cardBackID)
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("CollectionManager.AddNewCardBack({0}): trying to access NetCacheCardBacks before it's been loaded", cardBackID));
			return;
		}
		cardBacks.CardBacks.Add(cardBackID);
		this.AddRandomCardBackIfEligible();
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0004A460 File Offset: 0x00048660
	private void AddRandomCardBackIfEligible()
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject != null && this.m_RandomCardBackRecord != null && (long)netObject.CardBacks.Count >= this.m_RandomCardBackRecord.Data1)
		{
			netObject.CardBacks.Add(this.m_RandomCardBackRecord.ID);
		}
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x0004A4B3 File Offset: 0x000486B3
	public void RequestSetFavoriteCardBack(int cardBackID)
	{
		Network.Get().SetFavoriteCardBack(cardBackID);
		if (!Network.IsLoggedIn())
		{
			CollectionManager.Get().OnFavoriteCardBackChanged(cardBackID);
		}
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x0004A4D4 File Offset: 0x000486D4
	public int GetFavoriteCardBackID()
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetFavoriteCardBackID(): trying to access NetCacheCardBacks before it's been loaded");
			return 0;
		}
		return cardBacks.FavoriteCardBack;
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0004A500 File Offset: 0x00048700
	public string GetCardBackName(int cardBackId)
	{
		CardBackData cardBackData;
		if (this.m_cardBackData.TryGetValue(cardBackId, out cardBackData))
		{
			return cardBackData.Name;
		}
		return null;
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x0004A528 File Offset: 0x00048728
	public int GetNumCardBacksOwned()
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetNumCardBacksOwned(): trying to access NetCacheCardBacks before it's been loaded");
			return 0;
		}
		return cardBacks.CardBacks.Count;
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x0004A558 File Offset: 0x00048758
	public HashSet<int> GetCardBacksOwned()
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetCardBacksOwned(): trying to access NetCacheCardBacks before it's been loaded");
			return null;
		}
		return cardBacks.CardBacks;
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x0004A584 File Offset: 0x00048784
	public NetCache.NetCacheCardBacks GetCardBacks()
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject == null)
		{
			return this.GetCardBacksFromOfflineData();
		}
		return netObject;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x0004A5A8 File Offset: 0x000487A8
	public NetCache.NetCacheCardBacks GetCardBacksFromOfflineData()
	{
		CardBacks cardBacksFromCache = OfflineDataCache.GetCardBacksFromCache();
		if (cardBacksFromCache == null)
		{
			return null;
		}
		return new NetCache.NetCacheCardBacks
		{
			CardBacks = new HashSet<int>(cardBacksFromCache.CardBacks_),
			FavoriteCardBack = cardBacksFromCache.FavoriteCardBack
		};
	}

	// Token: 0x06000CDF RID: 3295 RVA: 0x0004A5E4 File Offset: 0x000487E4
	public HashSet<int> GetCardBackIds(bool all = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> cardBacksOwned = this.GetCardBacksOwned();
		foreach (KeyValuePair<int, CardBackData> keyValuePair in this.m_cardBackData)
		{
			if (keyValuePair.Value.Enabled && (string.IsNullOrEmpty(this.m_searchText) || keyValuePair.Value.Name.ToLower().Contains(this.m_searchText)) && (all || cardBacksOwned.Contains(keyValuePair.Key)))
			{
				hashSet.Add(keyValuePair.Key);
			}
		}
		return hashSet;
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x0004A698 File Offset: 0x00048898
	public bool IsCardBackOwned(int cardBackID)
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("CardBackManager.IsCardBackOwned({0}): trying to access NetCacheCardBacks before it's been loaded", cardBackID));
			return false;
		}
		return cardBacks.CardBacks.Contains(cardBackID);
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x0004A6D4 File Offset: 0x000488D4
	public bool CanFavoriteCardBack(int cardBackId)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			int favoriteCardBack = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>().FavoriteCardBack;
			return CardBackManager.Get().IsCardBackOwned(cardBackId) && favoriteCardBack != cardBackId;
		}
		return false;
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x0004A718 File Offset: 0x00048918
	public int GetCollectionManagerCardBackPurchaseProductId(int cardBackId)
	{
		CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBackId);
		if (record == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPurchaseProductId failed to find card back " + cardBackId.ToString() + " in the CardBack database");
			return 0;
		}
		return record.CollectionManagerPurchaseProductId;
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x0004A757 File Offset: 0x00048957
	public bool CanBuyCardBackFromCollectionManager(int cardBackId)
	{
		return !this.IsCardBackOwned(cardBackId) && this.IsCardBackPurchasableFromCollectionManager(cardBackId) && NetCache.Get().GetGoldBalance() >= this.GetCollectionManagerCardBackGoldCost(cardBackId);
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x0004A788 File Offset: 0x00048988
	public bool IsCardBackPurchasableFromCollectionManager(int cardBackId)
	{
		if (!StoreManager.Get().IsOpen(false))
		{
			return false;
		}
		if (!StoreManager.Get().IsBuyCardBacksFromCollectionManagerEnabled())
		{
			return false;
		}
		if (this.GetCollectionManagerCardBackPurchaseProductId(cardBackId) <= 0)
		{
			return false;
		}
		if (this.GetCollectionManagerCardBackPriceDataModel(cardBackId) == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:IsCardBackPurchasableFromCollectionManager failed to get the price data model for Card Back " + cardBackId.ToString());
			return false;
		}
		return true;
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x0004A7E0 File Offset: 0x000489E0
	public Network.Bundle GetCollectionManagerCardBackProductBundle(int cardBackId)
	{
		int collectionManagerCardBackPurchaseProductId = this.GetCollectionManagerCardBackPurchaseProductId(cardBackId);
		if (collectionManagerCardBackPurchaseProductId <= 0)
		{
			return null;
		}
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId((long)collectionManagerCardBackPurchaseProductId);
		if (bundleFromPmtProductId == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackProductBundle: Did not find a bundle with pmtProductId " + collectionManagerCardBackPurchaseProductId.ToString() + " for Card Back " + cardBackId.ToString());
			return null;
		}
		if (!bundleFromPmtProductId.Items.Any((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_CARD_BACK && x.ProductData == cardBackId))
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackProductBundle: Did not find any items with type PRODUCT_TYPE_CARD_BACK for bundle with pmtProductId " + collectionManagerCardBackPurchaseProductId.ToString() + " for Card Back " + cardBackId.ToString());
			return null;
		}
		return bundleFromPmtProductId;
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0004A888 File Offset: 0x00048A88
	public PriceDataModel GetCollectionManagerCardBackPriceDataModel(int cardBackId)
	{
		Network.Bundle collectionManagerCardBackProductBundle = this.GetCollectionManagerCardBackProductBundle(cardBackId);
		if (collectionManagerCardBackProductBundle == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPriceDataModel failed to get bundle for Card Back " + cardBackId.ToString());
			return null;
		}
		if (collectionManagerCardBackProductBundle.GtappGoldCost == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPriceDataModel bundle for Card Back " + cardBackId.ToString() + " has no GTAPP gold cost");
			return null;
		}
		return new PriceDataModel
		{
			Currency = CurrencyType.GOLD,
			Amount = (float)collectionManagerCardBackProductBundle.GtappGoldCost.Value,
			DisplayText = Mathf.RoundToInt((float)collectionManagerCardBackProductBundle.GtappGoldCost.Value).ToString()
		};
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0004A928 File Offset: 0x00048B28
	private long GetCollectionManagerCardBackGoldCost(int cardBackId)
	{
		Network.Bundle collectionManagerCardBackProductBundle = this.GetCollectionManagerCardBackProductBundle(cardBackId);
		if (collectionManagerCardBackProductBundle == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackGoldCost called for a card back with no valid product bundle. Card Back Id = " + cardBackId.ToString());
			return 0L;
		}
		if (collectionManagerCardBackProductBundle.GtappGoldCost == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackGoldCost called for a card back with no gold cost. Card Back Id = " + cardBackId.ToString());
			return 0L;
		}
		return collectionManagerCardBackProductBundle.GtappGoldCost.Value;
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0004A990 File Offset: 0x00048B90
	public List<CardBackManager.OwnedCardBack> GetOrderedEnabledCardBacks(bool requireOwned)
	{
		List<CardBackManager.OwnedCardBack> list = new List<CardBackManager.OwnedCardBack>();
		foreach (CardBackData cardBackData in this.m_cardBackData.Values)
		{
			if (cardBackData.Enabled)
			{
				bool flag = this.IsCardBackOwned(cardBackData.ID);
				if (!requireOwned || flag)
				{
					bool canBuy = this.CanBuyCardBackFromCollectionManager(cardBackData.ID);
					if (string.IsNullOrEmpty(this.m_searchText) || cardBackData.Name.ToLower().Contains(this.m_searchText))
					{
						CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBackData.ID);
						long seasonId = -1L;
						if (record.Source == "season")
						{
							seasonId = record.Data1;
						}
						list.Add(new CardBackManager.OwnedCardBack
						{
							m_cardBackId = cardBackData.ID,
							m_owned = flag,
							m_canBuy = canBuy,
							m_sortOrder = record.SortOrder,
							m_sortCategory = (int)record.SortCategory,
							m_seasonId = seasonId
						});
					}
				}
			}
		}
		list.Sort(delegate(CardBackManager.OwnedCardBack lhs, CardBackManager.OwnedCardBack rhs)
		{
			if (lhs.m_owned != rhs.m_owned)
			{
				if (!lhs.m_owned)
				{
					return 1;
				}
				return -1;
			}
			else if (lhs.m_canBuy != rhs.m_canBuy)
			{
				if (!lhs.m_canBuy)
				{
					return 1;
				}
				return -1;
			}
			else if (lhs.m_sortCategory != rhs.m_sortCategory)
			{
				if (lhs.m_sortCategory >= rhs.m_sortCategory)
				{
					return 1;
				}
				return -1;
			}
			else if (lhs.m_sortOrder != rhs.m_sortOrder)
			{
				if (lhs.m_sortOrder >= rhs.m_sortOrder)
				{
					return 1;
				}
				return -1;
			}
			else
			{
				if (lhs.m_seasonId == rhs.m_seasonId)
				{
					return Mathf.Clamp(lhs.m_cardBackId - rhs.m_cardBackId, -1, 1);
				}
				if (lhs.m_seasonId <= rhs.m_seasonId)
				{
					return 1;
				}
				return -1;
			}
		});
		return list;
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0004AAE4 File Offset: 0x00048CE4
	public void SetCardBackTexture(Renderer renderer, int matIdx, CardBackManager.CardBackSlot slot)
	{
		if (this.IsCardBackLoading(slot))
		{
			Processor.RunCoroutine(this.SetTextureWhenLoaded(renderer, matIdx, slot), null);
			return;
		}
		this.SetTexture(renderer, matIdx, slot);
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0004AB09 File Offset: 0x00048D09
	public void UpdateCardBack(Actor actor, CardBack cardBack)
	{
		if (actor.gameObject == null || actor.m_cardMesh == null || cardBack == null)
		{
			return;
		}
		CardBackManager.SetCardBack(actor.m_cardMesh, cardBack);
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0004AB40 File Offset: 0x00048D40
	public void UpdateCardBackWithInternalCardBack(Actor actor)
	{
		if (actor.gameObject == null || actor.m_cardMesh == null)
		{
			return;
		}
		CardBack componentInChildren = actor.gameObject.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			return;
		}
		CardBackManager.SetCardBack(actor.m_cardMesh, componentInChildren);
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0004AB8C File Offset: 0x00048D8C
	public void UpdateCardBack(GameObject go, CardBackManager.CardBackSlot slot)
	{
		if (go == null)
		{
			return;
		}
		if (this.IsCardBackLoading(slot))
		{
			Processor.RunCoroutine(this.SetCardBackWhenLoaded(go, slot), null);
			return;
		}
		this.SetCardBack(go, slot);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0004ABB9 File Offset: 0x00048DB9
	public void UpdateDeck(GameObject go, CardBackManager.CardBackSlot slot)
	{
		if (go == null)
		{
			return;
		}
		Processor.RunCoroutine(this.SetDeckCardBackWhenLoaded(go, slot), null);
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0004ABD4 File Offset: 0x00048DD4
	public void UpdateDragEffect(GameObject go, CardBackManager.CardBackSlot slot)
	{
		if (go == null)
		{
			return;
		}
		if (this.IsCardBackLoading(slot))
		{
			Processor.RunCoroutine(this.SetDragEffectsWhenLoaded(go, slot), null);
			return;
		}
		this.SetDragEffects(go, slot);
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x0004AC04 File Offset: 0x00048E04
	public bool IsActorFriendly(Actor actor)
	{
		if (actor == null)
		{
			Log.CardbackMgr.Print("CardBack IsActorFriendly: actor is null!", Array.Empty<object>());
			return true;
		}
		Entity entity = actor.GetEntity();
		if (entity != null)
		{
			Player controller = entity.GetController();
			if (controller != null && controller.GetSide() == Player.Side.OPPOSING)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x0004AC50 File Offset: 0x00048E50
	public bool IsFavoriteCardBackRandomAndEnabled()
	{
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBacks.FavoriteCardBack);
		return record.IsRandomCardBack && record.Enabled;
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0004AC88 File Offset: 0x00048E88
	public void LoadRandomCardBackOwnedByPlayer()
	{
		List<int> list = new List<int>();
		foreach (int num in this.GetCardBacks().CardBacks)
		{
			CardBackDbfRecord record = GameDbf.CardBack.GetRecord(num);
			if (record.Enabled && !record.IsRandomCardBack)
			{
				list.Add(num);
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		this.LoadCardBackIntoSlot(list[index], CardBackManager.CardBackSlot.RANDOM);
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0004AD20 File Offset: 0x00048F20
	private void InitCardBacks()
	{
		CardBackData cardBackData = this.m_cardBackData[0];
		this.LoadCardBack(cardBackData.PrefabName, CardBackManager.CardBackSlot.DEFAULT);
		if (Application.isEditor)
		{
			if (Options.Get().HasOption(Option.CARD_BACK))
			{
				int @int = Options.Get().GetInt(Option.CARD_BACK);
				if (this.m_cardBackData.ContainsKey(@int))
				{
					this.LoadCardBack(this.m_cardBackData[@int].PrefabName, CardBackManager.CardBackSlot.FRIENDLY);
				}
			}
			if (Options.Get().HasOption(Option.CARD_BACK2))
			{
				int int2 = Options.Get().GetInt(Option.CARD_BACK2);
				if (this.m_cardBackData.ContainsKey(int2))
				{
					this.LoadCardBack(this.m_cardBackData[int2].PrefabName, CardBackManager.CardBackSlot.OPPONENT);
				}
			}
		}
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0004ADE4 File Offset: 0x00048FE4
	public void InitCardBackData()
	{
		List<CardBackData> list = new List<CardBackData>();
		foreach (CardBackDbfRecord cardBackDbfRecord in GameDbf.CardBack.GetRecords())
		{
			list.Add(new CardBackData(cardBackDbfRecord.ID, EnumUtils.GetEnum<CardBackData.CardBackSource>(cardBackDbfRecord.Source), cardBackDbfRecord.Data1, cardBackDbfRecord.Name, cardBackDbfRecord.Enabled, cardBackDbfRecord.PrefabName));
			if (cardBackDbfRecord.IsRandomCardBack)
			{
				this.m_RandomCardBackRecord = cardBackDbfRecord;
			}
		}
		this.m_cardBackData = new Map<int, CardBackData>();
		foreach (CardBackData cardBackData in list)
		{
			this.m_cardBackData[cardBackData.ID] = cardBackData;
		}
		this.m_LoadedCardBacks = new Map<string, CardBack>();
		this.m_LoadedCardBacksBySlot = new Map<CardBackManager.CardBackSlot, CardBackManager.CardBackSlotData>();
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0004AEF0 File Offset: 0x000490F0
	private IEnumerator SetTextureWhenLoaded(Renderer renderer, int matIdx, CardBackManager.CardBackSlot slot)
	{
		while (this.IsCardBackLoading(slot))
		{
			yield return null;
		}
		this.SetTexture(renderer, matIdx, slot);
		yield break;
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0004AF14 File Offset: 0x00049114
	private void SetTexture(Renderer renderer, int matIdx, CardBackManager.CardBackSlot slot)
	{
		if (renderer == null)
		{
			return;
		}
		int count = renderer.GetMaterials().Count;
		if (matIdx < 0 || matIdx >= count)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetTexture(): matIdx {0} is not within the bounds of renderer's materials (count {1})", new object[]
			{
				matIdx,
				count
			});
			return;
		}
		CardBack cardBackBySlot = this.GetCardBackBySlot(slot);
		if (cardBackBySlot == null)
		{
			return;
		}
		Texture cardBackTexture = cardBackBySlot.m_CardBackTexture;
		if (cardBackTexture == null)
		{
			UnityEngine.Debug.LogWarning(string.Format("CardBackManager SetTexture(): texture is null!   obj: {0}  slot: {1}", renderer.gameObject.name, slot));
			return;
		}
		renderer.GetMaterial(matIdx).mainTexture = cardBackTexture;
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0004AFB2 File Offset: 0x000491B2
	private IEnumerator SetCardBackWhenLoaded(GameObject go, CardBackManager.CardBackSlot slot)
	{
		while (this.IsCardBackLoading(slot))
		{
			yield return null;
		}
		this.SetCardBack(go, slot);
		yield break;
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0004AFD0 File Offset: 0x000491D0
	private void SetCardBack(GameObject go, CardBackManager.CardBackSlot slot)
	{
		CardBack cardBackBySlot = this.GetCardBackBySlot(slot);
		if (cardBackBySlot == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetCardBack(): cardback not loaded for Slot: {0}", new object[]
			{
				slot
			});
			cardBackBySlot = this.GetCardBackBySlot(CardBackManager.CardBackSlot.DEFAULT);
			if (cardBackBySlot == null)
			{
				UnityEngine.Debug.LogWarning("CardBackManager SetCardBack(): default cardback not loaded");
				return;
			}
		}
		CardBackManager.SetCardBack(go, cardBackBySlot);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0004B02A File Offset: 0x0004922A
	public static void SetCardBack(GameObject go, CardBack cardBack)
	{
		CardBackManager.SetCardBack(go, cardBack, false, false);
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0004B038 File Offset: 0x00049238
	public static void SetCardBack(GameObject go, CardBack cardBack, bool unlit, bool shadowActive)
	{
		if (cardBack == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager SetCardBack() cardback=null");
			return;
		}
		if (go == null)
		{
			StackTrace stackTrace = new StackTrace();
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetCardBack() go=null, cardBack.name={0}, stacktrace=\n{1}", new object[]
			{
				cardBack.name,
				stackTrace.ToString()
			});
			return;
		}
		Mesh cardBackMesh = cardBack.m_CardBackMesh;
		if (cardBackMesh != null)
		{
			MeshFilter component = go.GetComponent<MeshFilter>();
			if (component != null)
			{
				component.mesh = cardBackMesh;
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("CardBackManager SetCardBack() mesh=null");
		}
		float value = 0f;
		if (!unlit && SceneMgr.Get() != null && SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			value = 1f;
		}
		Material cardBackMaterial = cardBack.m_CardBackMaterial;
		Material cardBackMaterial2 = cardBack.m_CardBackMaterial1;
		Material[] array = new Material[(cardBackMaterial2 != null) ? 2 : 1];
		array[0] = cardBackMaterial;
		if (cardBackMaterial2 != null)
		{
			array[1] = cardBackMaterial2;
		}
		if (array.Length != 0 && array[0] != null)
		{
			Renderer component2 = go.GetComponent<Renderer>();
			component2.SetSharedMaterials(array);
			List<Material> materials = component2.GetMaterials();
			float value2 = UnityEngine.Random.Range(0f, 1f);
			using (List<Material>.Enumerator enumerator = materials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Material material = enumerator.Current;
					if (!(material == null))
					{
						if (material.HasProperty("_Seed") && material.GetFloat("_Seed") == 0f)
						{
							material.SetFloat("_Seed", value2);
						}
						if (material.HasProperty("_LightingBlend"))
						{
							material.SetFloat("_LightingBlend", value);
						}
					}
				}
				goto IL_19A;
			}
		}
		UnityEngine.Debug.LogWarning("CardBackManager SetCardBack() material=null");
		IL_19A:
		if (cardBack.cardBackHelper == CardBack.cardBackHelpers.None)
		{
			CardBackManager.RemoveCardBackHelper<CardBackHelperBubbleLevel>(go);
		}
		else if (cardBack.cardBackHelper == CardBack.cardBackHelpers.CardBackHelperBubbleLevel)
		{
			CardBackManager.AddCardBackHelper<CardBackHelperBubbleLevel>(go);
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(go);
		if (actor != null)
		{
			actor.UpdateMissingCardArt();
			actor.EnableCardbackShadow(shadowActive);
		}
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0004B234 File Offset: 0x00049434
	public static T AddCardBackHelper<T>(GameObject go) where T : MonoBehaviour
	{
		CardBackManager.RemoveCardBackHelper<T>(go);
		return go.AddComponent<T>();
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0004B244 File Offset: 0x00049444
	public static bool RemoveCardBackHelper<T>(GameObject go) where T : MonoBehaviour
	{
		T[] components = go.GetComponents<T>();
		if (components != null)
		{
			T[] array = components;
			for (int i = 0; i < array.Length; i++)
			{
				UnityEngine.Object.Destroy(array[i]);
			}
			return true;
		}
		return false;
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0004B27F File Offset: 0x0004947F
	private IEnumerator SetDragEffectsWhenLoaded(GameObject go, CardBackManager.CardBackSlot slot)
	{
		while (this.IsCardBackLoading(slot))
		{
			yield return null;
		}
		this.SetDragEffects(go, slot);
		yield break;
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0004B29C File Offset: 0x0004949C
	private void SetDragEffects(GameObject go, CardBackManager.CardBackSlot slot)
	{
		if (go == null)
		{
			return;
		}
		CardBackDragEffect componentInChildren = go.GetComponentInChildren<CardBackDragEffect>();
		if (componentInChildren == null)
		{
			return;
		}
		CardBack cardBackBySlot = this.GetCardBackBySlot(slot);
		if (cardBackBySlot == null)
		{
			return;
		}
		if (componentInChildren.m_EffectsRoot != null)
		{
			UnityEngine.Object.Destroy(componentInChildren.m_EffectsRoot);
		}
		if (cardBackBySlot.m_DragEffect == null)
		{
			return;
		}
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(cardBackBySlot.m_DragEffect);
		componentInChildren.m_EffectsRoot = gameObject;
		gameObject.transform.parent = componentInChildren.gameObject.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0004B357 File Offset: 0x00049557
	private IEnumerator SetDeckCardBackWhenLoaded(GameObject go, CardBackManager.CardBackSlot slot)
	{
		while (this.IsCardBackLoading(slot))
		{
			yield return null;
		}
		this.SetDeckCardBack(go, slot);
		yield break;
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0004B374 File Offset: 0x00049574
	private void SetDeckCardBack(GameObject go, CardBackManager.CardBackSlot slot)
	{
		CardBack cardBackBySlot = this.GetCardBackBySlot(slot);
		if (cardBackBySlot == null)
		{
			return;
		}
		Texture cardBackTexture = cardBackBySlot.m_CardBackTexture;
		if (cardBackTexture == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager SetDeckCardBack(): texture is null!");
			return;
		}
		Renderer[] componentsInChildren = go.GetComponentsInChildren<Renderer>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].GetMaterial().mainTexture = cardBackTexture;
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0004B3D4 File Offset: 0x000495D4
	private void OnCheatOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		Log.CardbackMgr.Print("Cheat Option Change Called", Array.Empty<object>());
		int @int = Options.Get().GetInt(option, 0);
		if (!this.m_cardBackData.ContainsKey(@int))
		{
			return;
		}
		CardBackManager.CardBackSlot slot = CardBackManager.CardBackSlot.FRIENDLY;
		if (option == Option.CARD_BACK2)
		{
			slot = CardBackManager.CardBackSlot.OPPONENT;
		}
		this.LoadCardBack(this.m_cardBackData[@int].PrefabName, slot);
		this.UpdateAllCardBacksInSceneWhenReady();
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0004B43D File Offset: 0x0004963D
	private void NetCache_OnNetCacheCardBacksUpdated()
	{
		Processor.RunCoroutine(this.HandleNetCacheCardBacksWhenReady(), null);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0004B44C File Offset: 0x0004964C
	private IEnumerator HandleNetCacheCardBacksWhenReady()
	{
		while (this.m_cardBackData == null || FixedRewardsMgr.Get() == null || !FixedRewardsMgr.Get().IsStartupFinished())
		{
			yield return null;
		}
		NetCache.NetCacheCardBacks cardBacks = this.GetCardBacks();
		this.AddNewCardBack(0);
		this.AddRandomCardBackIfEligible();
		if (!this.m_cardBackData.ContainsKey(cardBacks.FavoriteCardBack))
		{
			Log.CardbackMgr.Print("No cardback for {0}, set default to CardBackDbId.CLASSIC", new object[]
			{
				cardBacks.FavoriteCardBack
			});
			cardBacks.FavoriteCardBack = 0;
		}
		this.LoadFavoriteCardBack(cardBacks.FavoriteCardBack);
		yield break;
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0004B45B File Offset: 0x0004965B
	private IEnumerator UpdateAllCardBacksInSceneWhenReadyImpl()
	{
		while (this.IsCardBackLoading(CardBackManager.CardBackSlot.FRIENDLY) || this.IsCardBackLoading(CardBackManager.CardBackSlot.OPPONENT) || this.IsCardBackLoading(CardBackManager.CardBackSlot.RANDOM))
		{
			yield return null;
		}
		object obj = this.cardbackListenerCollectionLock;
		lock (obj)
		{
			using (List<CardBackManager.UpdateCardbacksListener>.Enumerator enumerator = this.m_updateCardbacksListeners.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					CardBackManager.UpdateCardbacksListener updateCardbacksListener = enumerator.Current;
					updateCardbacksListener.Fire();
				}
				yield break;
			}
		}
		yield break;
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0004B46C File Offset: 0x0004966C
	private void LoadCardBack(AssetReference assetRef, CardBackManager.CardBackSlot slot)
	{
		string text = assetRef.ToString();
		CardBackManager.CardBackSlotData cardBackSlotData;
		if (!this.m_LoadedCardBacksBySlot.TryGetValue(slot, out cardBackSlotData))
		{
			cardBackSlotData = new CardBackManager.CardBackSlotData();
			this.m_LoadedCardBacksBySlot[slot] = cardBackSlotData;
		}
		if (this.m_LoadedCardBacks.ContainsKey(text))
		{
			if (!(this.m_LoadedCardBacks[text] == null))
			{
				cardBackSlotData.m_isLoading = false;
				cardBackSlotData.m_cardBackAssetString = text;
				cardBackSlotData.m_cardBack = this.m_LoadedCardBacks[text];
				return;
			}
			this.m_LoadedCardBacks.Remove(text);
		}
		if (cardBackSlotData.m_cardBackAssetString == text)
		{
			return;
		}
		cardBackSlotData.m_isLoading = true;
		cardBackSlotData.m_cardBackAssetString = text;
		cardBackSlotData.m_cardBack = null;
		CardBackManager.LoadCardBackData loadCardBackData = new CardBackManager.LoadCardBackData();
		loadCardBackData.m_Slot = slot;
		loadCardBackData.m_Path = text;
		AssetLoader.Get().InstantiatePrefab(text, new PrefabCallback<GameObject>(this.OnCardBackLoaded), loadCardBackData, AssetLoadingOptions.None);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0004B54C File Offset: 0x0004974C
	private void OnCardBackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		CardBackManager.LoadCardBackData loadCardBackData = callbackData as CardBackManager.LoadCardBackData;
		if (go == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): Failed to load CardBack: {0} For: {1}", new object[]
			{
				assetRef,
				loadCardBackData.m_Slot
			});
			this.m_LoadedCardBacksBySlot.Remove(loadCardBackData.m_Slot);
			return;
		}
		go.transform.parent = this.SceneObject.transform;
		go.transform.position = new Vector3(1000f, -1000f, -1000f);
		CardBack component = go.GetComponent<CardBack>();
		if (component == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): Failed to find CardBack component: {0} slot: {1}", new object[]
			{
				loadCardBackData.m_Path,
				loadCardBackData.m_Slot
			});
			return;
		}
		if (component.m_CardBackMesh == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackMesh in null! - {0}", new object[]
			{
				loadCardBackData.m_Path
			});
			return;
		}
		if (component.m_CardBackMaterial == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackMaterial in null! - {0}", new object[]
			{
				loadCardBackData.m_Path
			});
			return;
		}
		if (component.m_CardBackTexture == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackTexture in null! - {0}", new object[]
			{
				loadCardBackData.m_Path
			});
			return;
		}
		this.m_LoadedCardBacks[loadCardBackData.m_Path] = component;
		CardBackManager.CardBackSlotData cardBackSlotData;
		if (this.m_LoadedCardBacksBySlot.TryGetValue(loadCardBackData.m_Slot, out cardBackSlotData))
		{
			cardBackSlotData.m_isLoading = false;
			cardBackSlotData.m_cardBack = component;
		}
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0004B6B8 File Offset: 0x000498B8
	private void OnHiddenActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		CardBackManager.LoadCardBackData loadCardBackData = (CardBackManager.LoadCardBackData)callbackData;
		int cardBackIndex = loadCardBackData.m_CardBackIndex;
		string prefabName = this.m_cardBackData[cardBackIndex].PrefabName;
		loadCardBackData.m_GameObject = go;
		AssetLoader.Get().InstantiatePrefab(prefabName, new PrefabCallback<GameObject>(this.OnHiddenActorCardBackLoaded), callbackData, AssetLoadingOptions.None);
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0004B70C File Offset: 0x0004990C
	private void OnHiddenActorCardBackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevWarning("Error", "CardBackManager OnHiddenActorCardBackLoaded() path={0}, gameobject=null", new object[]
			{
				assetRef
			});
			return;
		}
		CardBack componentInChildren = go.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnHiddenActorCardBackLoaded() path={0}, gameobject={1}, cardback=null", new object[]
			{
				assetRef,
				go.name
			});
			return;
		}
		CardBackManager.LoadCardBackData loadCardBackData = (CardBackManager.LoadCardBackData)callbackData;
		loadCardBackData.m_CardBack = componentInChildren;
		Processor.RunCoroutine(this.HiddenActorCardBackLoadedSetup(loadCardBackData), null);
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0004B786 File Offset: 0x00049986
	private IEnumerator HiddenActorCardBackLoadedSetup(CardBackManager.LoadCardBackData data)
	{
		yield return null;
		yield return null;
		if (data == null || data.m_GameObject == null)
		{
			yield break;
		}
		CardBackManager.SetCardBack(data.m_GameObject.GetComponent<Actor>().m_cardMesh, data.m_CardBack, data.m_Unlit, data.m_ShadowActive);
		data.m_CardBack.gameObject.transform.parent = data.m_GameObject.transform;
		data.m_Callback(data);
		yield break;
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0004B795 File Offset: 0x00049995
	private int GetValidCardBackID(int cardBackID)
	{
		if (!this.m_cardBackData.ContainsKey(cardBackID))
		{
			Log.CardbackMgr.Print("Cardback ID {0} not found, defaulting to Classic", new object[]
			{
				cardBackID
			});
			return 0;
		}
		return cardBackID;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0004B7C6 File Offset: 0x000499C6
	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		this.LoadFavoriteCardBack(newFavoriteCardBackID);
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0004B7D0 File Offset: 0x000499D0
	private void LoadFavoriteCardBack(int favoriteCardBackID)
	{
		GameMgr gameMgr;
		if (HearthstoneServices.TryGet<GameMgr>(out gameMgr) && gameMgr.IsSpectator())
		{
			return;
		}
		this.LoadCardBackIntoSlot(favoriteCardBackID, CardBackManager.CardBackSlot.FRIENDLY);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0004B7F8 File Offset: 0x000499F8
	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.GAMEPLAY || SceneMgr.Get().GetMode() == SceneMgr.Mode.GAMEPLAY)
		{
			return;
		}
		CardBackData cardBackData = this.m_cardBackData[this.GetFavoriteCardBackID()];
		this.LoadCardBack(cardBackData.PrefabName, CardBackManager.CardBackSlot.FRIENDLY);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0004B844 File Offset: 0x00049A44
	private void LoadCardBackIntoSlot(int cardBackId, CardBackManager.CardBackSlot slot)
	{
		int validCardBackID = this.GetValidCardBackID(cardBackId);
		CardBackData cardBackData;
		if (this.m_cardBackData.TryGetValue(validCardBackID, out cardBackData))
		{
			this.LoadCardBack(cardBackData.PrefabName, slot);
			this.UpdateAllCardBacksInSceneWhenReady();
		}
	}

	// Token: 0x040008EF RID: 2287
	private GameObject m_sceneObject;

	// Token: 0x040008F0 RID: 2288
	private const int CARD_BACK_PRIMARY_MATERIAL_INDEX = 0;

	// Token: 0x040008F1 RID: 2289
	private const int CARD_BACK_SECONDARY_MATERIAL_INDEX = 1;

	// Token: 0x040008F2 RID: 2290
	private Map<int, CardBackData> m_cardBackData;

	// Token: 0x040008F3 RID: 2291
	private Map<string, CardBack> m_LoadedCardBacks;

	// Token: 0x040008F4 RID: 2292
	private Map<CardBackManager.CardBackSlot, CardBackManager.CardBackSlotData> m_LoadedCardBacksBySlot;

	// Token: 0x040008F5 RID: 2293
	private string m_searchText;

	// Token: 0x040008F6 RID: 2294
	private CardBackDbfRecord m_RandomCardBackRecord;

	// Token: 0x040008F7 RID: 2295
	private List<CardBackManager.UpdateCardbacksListener> m_updateCardbacksListeners = new List<CardBackManager.UpdateCardbacksListener>();

	// Token: 0x040008F8 RID: 2296
	private readonly object cardbackListenerCollectionLock = new object();

	// Token: 0x020013E5 RID: 5093
	public class LoadCardBackData
	{
		// Token: 0x0400A835 RID: 43061
		public int m_CardBackIndex;

		// Token: 0x0400A836 RID: 43062
		public GameObject m_GameObject;

		// Token: 0x0400A837 RID: 43063
		public CardBack m_CardBack;

		// Token: 0x0400A838 RID: 43064
		public CardBackManager.LoadCardBackData.LoadCardBackCallback m_Callback;

		// Token: 0x0400A839 RID: 43065
		public string m_Name;

		// Token: 0x0400A83A RID: 43066
		public string m_Path;

		// Token: 0x0400A83B RID: 43067
		public CardBackManager.CardBackSlot m_Slot;

		// Token: 0x0400A83C RID: 43068
		public bool m_Unlit;

		// Token: 0x0400A83D RID: 43069
		public bool m_ShadowActive;

		// Token: 0x0400A83E RID: 43070
		public object callbackData;

		// Token: 0x0200297A RID: 10618
		// (Invoke) Token: 0x06013EF1 RID: 81649
		public delegate void LoadCardBackCallback(CardBackManager.LoadCardBackData cardBackData);
	}

	// Token: 0x020013E6 RID: 5094
	public class OwnedCardBack
	{
		// Token: 0x0400A83F RID: 43071
		public int m_cardBackId;

		// Token: 0x0400A840 RID: 43072
		public bool m_owned;

		// Token: 0x0400A841 RID: 43073
		public bool m_canBuy;

		// Token: 0x0400A842 RID: 43074
		public int m_sortOrder;

		// Token: 0x0400A843 RID: 43075
		public int m_sortCategory;

		// Token: 0x0400A844 RID: 43076
		public long m_seasonId = -1L;
	}

	// Token: 0x020013E7 RID: 5095
	public enum CardBackSlot
	{
		// Token: 0x0400A846 RID: 43078
		DEFAULT,
		// Token: 0x0400A847 RID: 43079
		FRIENDLY,
		// Token: 0x0400A848 RID: 43080
		OPPONENT,
		// Token: 0x0400A849 RID: 43081
		RANDOM
	}

	// Token: 0x020013E8 RID: 5096
	// (Invoke) Token: 0x0600D8E6 RID: 55526
	public delegate void UpdateCardbacksCallback();

	// Token: 0x020013E9 RID: 5097
	private class CardBackSlotData
	{
		// Token: 0x0400A84A RID: 43082
		public CardBack m_cardBack;

		// Token: 0x0400A84B RID: 43083
		public string m_cardBackAssetString;

		// Token: 0x0400A84C RID: 43084
		public bool m_isLoading;
	}

	// Token: 0x020013EA RID: 5098
	private class UpdateCardbacksListener : EventListener<CardBackManager.UpdateCardbacksCallback>
	{
		// Token: 0x0600D8EA RID: 55530 RVA: 0x003EF1D8 File Offset: 0x003ED3D8
		public void Fire()
		{
			this.m_callback();
		}
	}
}
