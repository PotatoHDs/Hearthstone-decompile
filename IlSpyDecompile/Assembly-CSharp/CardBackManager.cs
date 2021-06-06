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

public class CardBackManager : IService
{
	public class LoadCardBackData
	{
		public delegate void LoadCardBackCallback(LoadCardBackData cardBackData);

		public int m_CardBackIndex;

		public GameObject m_GameObject;

		public CardBack m_CardBack;

		public LoadCardBackCallback m_Callback;

		public string m_Name;

		public string m_Path;

		public CardBackSlot m_Slot;

		public bool m_Unlit;

		public bool m_ShadowActive;

		public object callbackData;
	}

	public class OwnedCardBack
	{
		public int m_cardBackId;

		public bool m_owned;

		public bool m_canBuy;

		public int m_sortOrder;

		public int m_sortCategory;

		public long m_seasonId = -1L;
	}

	public enum CardBackSlot
	{
		DEFAULT,
		FRIENDLY,
		OPPONENT,
		RANDOM
	}

	public delegate void UpdateCardbacksCallback();

	private class CardBackSlotData
	{
		public CardBack m_cardBack;

		public string m_cardBackAssetString;

		public bool m_isLoading;
	}

	private class UpdateCardbacksListener : EventListener<UpdateCardbacksCallback>
	{
		public void Fire()
		{
			m_callback();
		}
	}

	private GameObject m_sceneObject;

	private const int CARD_BACK_PRIMARY_MATERIAL_INDEX = 0;

	private const int CARD_BACK_SECONDARY_MATERIAL_INDEX = 1;

	private Map<int, CardBackData> m_cardBackData;

	private Map<string, CardBack> m_LoadedCardBacks;

	private Map<CardBackSlot, CardBackSlotData> m_LoadedCardBacksBySlot;

	private string m_searchText;

	private CardBackDbfRecord m_RandomCardBackRecord;

	private List<UpdateCardbacksListener> m_updateCardbacksListeners = new List<UpdateCardbacksListener>();

	private readonly object cardbackListenerCollectionLock = new object();

	private GameObject SceneObject
	{
		get
		{
			if (m_sceneObject == null)
			{
				m_sceneObject = new GameObject("CardBackManagerSceneObject", typeof(HSDontDestroyOnLoad));
			}
			return m_sceneObject;
		}
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().Resetting += Resetting;
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.FavoriteCardBackChanged += OnFavoriteCardBackChanged;
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheCardBacks), NetCache_OnNetCacheCardBacksUpdated);
		InitCardBackData();
		Options.Get().RegisterChangedListener(Option.CARD_BACK, OnCheatOptionChanged);
		Options.Get().RegisterChangedListener(Option.CARD_BACK2, OnCheatOptionChanged);
		serviceLocator.Get<SceneMgr>().RegisterSceneLoadedEvent(OnSceneLoaded);
		InitCardBacks();
		yield break;
	}

	public Type[] GetDependencies()
	{
		return new Type[4]
		{
			typeof(GameDbf),
			typeof(IAssetLoader),
			typeof(NetCache),
			typeof(SceneMgr)
		};
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.FavoriteCardBackChanged -= OnFavoriteCardBackChanged;
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.Resetting -= Resetting;
		}
	}

	private void Resetting()
	{
		InitCardBackData();
	}

	public static CardBackManager Get()
	{
		return HearthstoneServices.Get<CardBackManager>();
	}

	public bool RegisterUpdateCardbacksListener(UpdateCardbacksCallback callback)
	{
		UpdateCardbacksListener updateCardbacksListener = new UpdateCardbacksListener();
		updateCardbacksListener.SetCallback(callback);
		if (m_updateCardbacksListeners.Contains(updateCardbacksListener))
		{
			return false;
		}
		lock (cardbackListenerCollectionLock)
		{
			m_updateCardbacksListeners.Add(updateCardbacksListener);
		}
		return true;
	}

	public bool UnregisterUpdateCardbacksListener(UpdateCardbacksCallback callback)
	{
		UpdateCardbacksListener updateCardbacksListener = new UpdateCardbacksListener();
		updateCardbacksListener.SetCallback(callback);
		bool flag = false;
		lock (cardbackListenerCollectionLock)
		{
			return m_updateCardbacksListeners.Remove(updateCardbacksListener);
		}
	}

	public void SetSearchText(string searchText)
	{
		m_searchText = searchText?.ToLower();
	}

	public int GetDeckCardBackID(long deck)
	{
		NetCache.DeckHeader deckHeader = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Find((NetCache.DeckHeader obj) => obj.ID == deck);
		if (deckHeader == null)
		{
			UnityEngine.Debug.LogWarning($"CardBackManager.GetDeckCardBackID() could not find deck with ID {deck}");
			return 0;
		}
		return deckHeader.CardBack;
	}

	public CardBack GetFriendlyCardBack()
	{
		return GetCardBackBySlot(CardBackSlot.FRIENDLY);
	}

	public CardBack GetOpponentCardBack()
	{
		return GetCardBackBySlot(CardBackSlot.OPPONENT);
	}

	public CardBack GetCardBackForActor(Actor actor)
	{
		if (IsActorFriendly(actor))
		{
			return GetFriendlyCardBack();
		}
		return GetOpponentCardBack();
	}

	public CardBack GetCardBackBySlot(CardBackSlot slot)
	{
		if (m_LoadedCardBacksBySlot.TryGetValue(slot, out var value))
		{
			return value.m_cardBack;
		}
		return null;
	}

	public bool IsCardBackLoading(CardBackSlot slot)
	{
		if (m_LoadedCardBacksBySlot.TryGetValue(slot, out var value))
		{
			return value.m_isLoading;
		}
		return false;
	}

	public void UpdateAllCardBacksInSceneWhenReady()
	{
		Processor.RunCoroutine(UpdateAllCardBacksInSceneWhenReadyImpl());
	}

	public void SetGameCardBackIDs(int friendlyCardBackID, int opponentCardBackID)
	{
		int validCardBackID = GetValidCardBackID(friendlyCardBackID);
		LoadCardBack(m_cardBackData[validCardBackID].PrefabName, CardBackSlot.FRIENDLY);
		int validCardBackID2 = GetValidCardBackID(opponentCardBackID);
		LoadCardBack(m_cardBackData[validCardBackID2].PrefabName, CardBackSlot.OPPONENT);
		UpdateAllCardBacksInSceneWhenReady();
	}

	public bool LoadCardBackByIndex(int cardBackIdx, LoadCardBackData.LoadCardBackCallback callback, object callbackData = null)
	{
		string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9";
		return LoadCardBackByIndex(cardBackIdx, callback, unlit: false, actorName, callbackData);
	}

	public bool LoadCardBackByIndex(int cardBackIdx, LoadCardBackData.LoadCardBackCallback callback, string actorName, object callbackData = null)
	{
		return LoadCardBackByIndex(cardBackIdx, callback, unlit: false, actorName, callbackData);
	}

	public bool LoadCardBackByIndex(int cardBackIdx, LoadCardBackData.LoadCardBackCallback callback, bool unlit, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", object callbackData = null)
	{
		if (!m_cardBackData.ContainsKey(cardBackIdx))
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - wrong cardBackIdx {0}", cardBackIdx);
			return false;
		}
		LoadCardBackData loadCardBackData = new LoadCardBackData();
		loadCardBackData.m_CardBackIndex = cardBackIdx;
		loadCardBackData.m_Callback = callback;
		loadCardBackData.m_Unlit = unlit;
		loadCardBackData.m_Name = m_cardBackData[cardBackIdx].Name;
		loadCardBackData.callbackData = callbackData;
		AssetLoader.Get().InstantiatePrefab(actorName, OnHiddenActorLoaded, loadCardBackData, AssetLoadingOptions.IgnorePrefabPosition);
		return true;
	}

	public LoadCardBackData LoadCardBackByIndex(int cardBackIdx, bool unlit = false, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", bool shadowActive = false)
	{
		if (!m_cardBackData.ContainsKey(cardBackIdx))
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - wrong cardBackIdx {0}", cardBackIdx);
			return null;
		}
		LoadCardBackData loadCardBackData = new LoadCardBackData();
		loadCardBackData.m_CardBackIndex = cardBackIdx;
		loadCardBackData.m_Unlit = unlit;
		loadCardBackData.m_Name = m_cardBackData[cardBackIdx].Name;
		loadCardBackData.m_GameObject = AssetLoader.Get().InstantiatePrefab(actorName, AssetLoadingOptions.IgnorePrefabPosition);
		if (loadCardBackData.m_GameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - failed to load Actor {0}", actorName);
			return null;
		}
		string prefabName = m_cardBackData[cardBackIdx].PrefabName;
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(prefabName);
		if (gameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackByIndex() - failed to load CardBack {0}", prefabName);
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
		SetCardBack(component.m_cardMesh, loadCardBackData.m_CardBack, loadCardBackData.m_Unlit, shadowActive);
		component.SetCardbackUpdateIgnore(ignoreUpdate: true);
		loadCardBackData.m_CardBack.gameObject.transform.parent = loadCardBackData.m_GameObject.transform;
		return loadCardBackData;
	}

	public static Actor LoadCardBackActorByPrefab(string cardBackPrefab, bool unlit = false, string actorName = "Card_Hidden.prefab:1a94649d257bc284ca6e2962f634a8b9", bool shadowActive = false)
	{
		if (AssetLoader.Get() == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.LoadCardBackActorByPrefab() - AssetLoader not available");
			return null;
		}
		GameObject gameObject = AssetLoader.Get().InstantiatePrefab(cardBackPrefab);
		if (gameObject == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackActorByPrefab() - failed to load CardBack {0}", cardBackPrefab);
			return null;
		}
		GameObject gameObject2 = AssetLoader.Get().InstantiatePrefab(actorName, AssetLoadingOptions.IgnorePrefabPosition);
		if (gameObject2 == null)
		{
			Log.CardbackMgr.Print("CardBackManager.LoadCardBackActorByPrefab() - failed to load Actor {0}", actorName);
			return null;
		}
		Actor component = gameObject2.GetComponent<Actor>();
		CardBack componentInChildren = gameObject.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.LoadCardBackActorByPrefab() - cardback=null");
			return null;
		}
		SetCardBack(component.m_cardMesh, componentInChildren, unlit, shadowActive);
		component.SetCardbackUpdateIgnore(ignoreUpdate: true);
		componentInChildren.gameObject.transform.parent = gameObject2.transform;
		return component;
	}

	public void AddNewCardBack(int cardBackID)
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning($"CollectionManager.AddNewCardBack({cardBackID}): trying to access NetCacheCardBacks before it's been loaded");
			return;
		}
		cardBacks.CardBacks.Add(cardBackID);
		AddRandomCardBackIfEligible();
	}

	private void AddRandomCardBackIfEligible()
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject != null && m_RandomCardBackRecord != null && netObject.CardBacks.Count >= m_RandomCardBackRecord.Data1)
		{
			netObject.CardBacks.Add(m_RandomCardBackRecord.ID);
		}
	}

	public void RequestSetFavoriteCardBack(int cardBackID)
	{
		Network.Get().SetFavoriteCardBack(cardBackID);
		if (!Network.IsLoggedIn())
		{
			CollectionManager.Get().OnFavoriteCardBackChanged(cardBackID);
		}
	}

	public int GetFavoriteCardBackID()
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetFavoriteCardBackID(): trying to access NetCacheCardBacks before it's been loaded");
			return 0;
		}
		return cardBacks.FavoriteCardBack;
	}

	public string GetCardBackName(int cardBackId)
	{
		if (m_cardBackData.TryGetValue(cardBackId, out var value))
		{
			return value.Name;
		}
		return null;
	}

	public int GetNumCardBacksOwned()
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetNumCardBacksOwned(): trying to access NetCacheCardBacks before it's been loaded");
			return 0;
		}
		return cardBacks.CardBacks.Count;
	}

	public HashSet<int> GetCardBacksOwned()
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning("CardBackManager.GetCardBacksOwned(): trying to access NetCacheCardBacks before it's been loaded");
			return null;
		}
		return cardBacks.CardBacks;
	}

	public NetCache.NetCacheCardBacks GetCardBacks()
	{
		NetCache.NetCacheCardBacks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>();
		if (netObject == null)
		{
			return GetCardBacksFromOfflineData();
		}
		return netObject;
	}

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

	public HashSet<int> GetCardBackIds(bool all = true)
	{
		HashSet<int> hashSet = new HashSet<int>();
		HashSet<int> cardBacksOwned = GetCardBacksOwned();
		foreach (KeyValuePair<int, CardBackData> cardBackDatum in m_cardBackData)
		{
			if (cardBackDatum.Value.Enabled && (string.IsNullOrEmpty(m_searchText) || cardBackDatum.Value.Name.ToLower().Contains(m_searchText)) && (all || cardBacksOwned.Contains(cardBackDatum.Key)))
			{
				hashSet.Add(cardBackDatum.Key);
			}
		}
		return hashSet;
	}

	public bool IsCardBackOwned(int cardBackID)
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		if (cardBacks == null)
		{
			UnityEngine.Debug.LogWarning($"CardBackManager.IsCardBackOwned({cardBackID}): trying to access NetCacheCardBacks before it's been loaded");
			return false;
		}
		return cardBacks.CardBacks.Contains(cardBackID);
	}

	public bool CanFavoriteCardBack(int cardBackId)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			int favoriteCardBack = NetCache.Get().GetNetObject<NetCache.NetCacheCardBacks>().FavoriteCardBack;
			if (Get().IsCardBackOwned(cardBackId))
			{
				return favoriteCardBack != cardBackId;
			}
			return false;
		}
		return false;
	}

	public int GetCollectionManagerCardBackPurchaseProductId(int cardBackId)
	{
		CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBackId);
		if (record == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPurchaseProductId failed to find card back " + cardBackId + " in the CardBack database");
			return 0;
		}
		return record.CollectionManagerPurchaseProductId;
	}

	public bool CanBuyCardBackFromCollectionManager(int cardBackId)
	{
		if (IsCardBackOwned(cardBackId))
		{
			return false;
		}
		if (!IsCardBackPurchasableFromCollectionManager(cardBackId))
		{
			return false;
		}
		if (NetCache.Get().GetGoldBalance() < GetCollectionManagerCardBackGoldCost(cardBackId))
		{
			return false;
		}
		return true;
	}

	public bool IsCardBackPurchasableFromCollectionManager(int cardBackId)
	{
		if (!StoreManager.Get().IsOpen(printStatus: false))
		{
			return false;
		}
		if (!StoreManager.Get().IsBuyCardBacksFromCollectionManagerEnabled())
		{
			return false;
		}
		if (GetCollectionManagerCardBackPurchaseProductId(cardBackId) <= 0)
		{
			return false;
		}
		if (GetCollectionManagerCardBackPriceDataModel(cardBackId) == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:IsCardBackPurchasableFromCollectionManager failed to get the price data model for Card Back " + cardBackId);
			return false;
		}
		return true;
	}

	public Network.Bundle GetCollectionManagerCardBackProductBundle(int cardBackId)
	{
		int collectionManagerCardBackPurchaseProductId = GetCollectionManagerCardBackPurchaseProductId(cardBackId);
		if (collectionManagerCardBackPurchaseProductId <= 0)
		{
			return null;
		}
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(collectionManagerCardBackPurchaseProductId);
		if (bundleFromPmtProductId == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackProductBundle: Did not find a bundle with pmtProductId " + collectionManagerCardBackPurchaseProductId + " for Card Back " + cardBackId);
			return null;
		}
		if (!bundleFromPmtProductId.Items.Any((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_CARD_BACK && x.ProductData == cardBackId))
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackProductBundle: Did not find any items with type PRODUCT_TYPE_CARD_BACK for bundle with pmtProductId " + collectionManagerCardBackPurchaseProductId + " for Card Back " + cardBackId);
			return null;
		}
		return bundleFromPmtProductId;
	}

	public PriceDataModel GetCollectionManagerCardBackPriceDataModel(int cardBackId)
	{
		Network.Bundle collectionManagerCardBackProductBundle = GetCollectionManagerCardBackProductBundle(cardBackId);
		if (collectionManagerCardBackProductBundle == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPriceDataModel failed to get bundle for Card Back " + cardBackId);
			return null;
		}
		if (!collectionManagerCardBackProductBundle.GtappGoldCost.HasValue)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackPriceDataModel bundle for Card Back " + cardBackId + " has no GTAPP gold cost");
			return null;
		}
		return new PriceDataModel
		{
			Currency = CurrencyType.GOLD,
			Amount = collectionManagerCardBackProductBundle.GtappGoldCost.Value,
			DisplayText = Mathf.RoundToInt(collectionManagerCardBackProductBundle.GtappGoldCost.Value).ToString()
		};
	}

	private long GetCollectionManagerCardBackGoldCost(int cardBackId)
	{
		Network.Bundle collectionManagerCardBackProductBundle = GetCollectionManagerCardBackProductBundle(cardBackId);
		if (collectionManagerCardBackProductBundle == null)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackGoldCost called for a card back with no valid product bundle. Card Back Id = " + cardBackId);
			return 0L;
		}
		if (!collectionManagerCardBackProductBundle.GtappGoldCost.HasValue)
		{
			UnityEngine.Debug.LogError("CardBackManager:GetCollectionManagerCardBackGoldCost called for a card back with no gold cost. Card Back Id = " + cardBackId);
			return 0L;
		}
		return collectionManagerCardBackProductBundle.GtappGoldCost.Value;
	}

	public List<OwnedCardBack> GetOrderedEnabledCardBacks(bool requireOwned)
	{
		List<OwnedCardBack> list = new List<OwnedCardBack>();
		foreach (CardBackData value in m_cardBackData.Values)
		{
			if (!value.Enabled)
			{
				continue;
			}
			bool flag = IsCardBackOwned(value.ID);
			if (requireOwned && !flag)
			{
				continue;
			}
			bool canBuy = CanBuyCardBackFromCollectionManager(value.ID);
			if (string.IsNullOrEmpty(m_searchText) || value.Name.ToLower().Contains(m_searchText))
			{
				CardBackDbfRecord record = GameDbf.CardBack.GetRecord(value.ID);
				long seasonId = -1L;
				if (record.Source == "season")
				{
					seasonId = record.Data1;
				}
				list.Add(new OwnedCardBack
				{
					m_cardBackId = value.ID,
					m_owned = flag,
					m_canBuy = canBuy,
					m_sortOrder = record.SortOrder,
					m_sortCategory = (int)record.SortCategory,
					m_seasonId = seasonId
				});
			}
		}
		list.Sort(delegate(OwnedCardBack lhs, OwnedCardBack rhs)
		{
			if (lhs.m_owned != rhs.m_owned)
			{
				if (!lhs.m_owned)
				{
					return 1;
				}
				return -1;
			}
			if (lhs.m_canBuy != rhs.m_canBuy)
			{
				if (!lhs.m_canBuy)
				{
					return 1;
				}
				return -1;
			}
			if (lhs.m_sortCategory != rhs.m_sortCategory)
			{
				if (lhs.m_sortCategory >= rhs.m_sortCategory)
				{
					return 1;
				}
				return -1;
			}
			if (lhs.m_sortOrder != rhs.m_sortOrder)
			{
				if (lhs.m_sortOrder >= rhs.m_sortOrder)
				{
					return 1;
				}
				return -1;
			}
			return (lhs.m_seasonId != rhs.m_seasonId) ? ((lhs.m_seasonId <= rhs.m_seasonId) ? 1 : (-1)) : Mathf.Clamp(lhs.m_cardBackId - rhs.m_cardBackId, -1, 1);
		});
		return list;
	}

	public void SetCardBackTexture(Renderer renderer, int matIdx, CardBackSlot slot)
	{
		if (IsCardBackLoading(slot))
		{
			Processor.RunCoroutine(SetTextureWhenLoaded(renderer, matIdx, slot));
		}
		else
		{
			SetTexture(renderer, matIdx, slot);
		}
	}

	public void UpdateCardBack(Actor actor, CardBack cardBack)
	{
		if (!(actor.gameObject == null) && !(actor.m_cardMesh == null) && !(cardBack == null))
		{
			SetCardBack(actor.m_cardMesh, cardBack);
		}
	}

	public void UpdateCardBackWithInternalCardBack(Actor actor)
	{
		if (!(actor.gameObject == null) && !(actor.m_cardMesh == null))
		{
			CardBack componentInChildren = actor.gameObject.GetComponentInChildren<CardBack>();
			if (!(componentInChildren == null))
			{
				SetCardBack(actor.m_cardMesh, componentInChildren);
			}
		}
	}

	public void UpdateCardBack(GameObject go, CardBackSlot slot)
	{
		if (!(go == null))
		{
			if (IsCardBackLoading(slot))
			{
				Processor.RunCoroutine(SetCardBackWhenLoaded(go, slot));
			}
			else
			{
				SetCardBack(go, slot);
			}
		}
	}

	public void UpdateDeck(GameObject go, CardBackSlot slot)
	{
		if (!(go == null))
		{
			Processor.RunCoroutine(SetDeckCardBackWhenLoaded(go, slot));
		}
	}

	public void UpdateDragEffect(GameObject go, CardBackSlot slot)
	{
		if (!(go == null))
		{
			if (IsCardBackLoading(slot))
			{
				Processor.RunCoroutine(SetDragEffectsWhenLoaded(go, slot));
			}
			else
			{
				SetDragEffects(go, slot);
			}
		}
	}

	public bool IsActorFriendly(Actor actor)
	{
		if (actor == null)
		{
			Log.CardbackMgr.Print("CardBack IsActorFriendly: actor is null!");
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

	public bool IsFavoriteCardBackRandomAndEnabled()
	{
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBacks.FavoriteCardBack);
		if (record.IsRandomCardBack)
		{
			return record.Enabled;
		}
		return false;
	}

	public void LoadRandomCardBackOwnedByPlayer()
	{
		List<int> list = new List<int>();
		foreach (int cardBack in GetCardBacks().CardBacks)
		{
			CardBackDbfRecord record = GameDbf.CardBack.GetRecord(cardBack);
			if (record.Enabled && !record.IsRandomCardBack)
			{
				list.Add(cardBack);
			}
		}
		int index = UnityEngine.Random.Range(0, list.Count);
		LoadCardBackIntoSlot(list[index], CardBackSlot.RANDOM);
	}

	private void InitCardBacks()
	{
		CardBackData cardBackData = m_cardBackData[0];
		LoadCardBack(cardBackData.PrefabName, CardBackSlot.DEFAULT);
		if (!Application.isEditor)
		{
			return;
		}
		if (Options.Get().HasOption(Option.CARD_BACK))
		{
			int @int = Options.Get().GetInt(Option.CARD_BACK);
			if (m_cardBackData.ContainsKey(@int))
			{
				LoadCardBack(m_cardBackData[@int].PrefabName, CardBackSlot.FRIENDLY);
			}
		}
		if (Options.Get().HasOption(Option.CARD_BACK2))
		{
			int int2 = Options.Get().GetInt(Option.CARD_BACK2);
			if (m_cardBackData.ContainsKey(int2))
			{
				LoadCardBack(m_cardBackData[int2].PrefabName, CardBackSlot.OPPONENT);
			}
		}
	}

	public void InitCardBackData()
	{
		List<CardBackData> list = new List<CardBackData>();
		foreach (CardBackDbfRecord record in GameDbf.CardBack.GetRecords())
		{
			list.Add(new CardBackData(record.ID, EnumUtils.GetEnum<CardBackData.CardBackSource>(record.Source), record.Data1, record.Name, record.Enabled, record.PrefabName));
			if (record.IsRandomCardBack)
			{
				m_RandomCardBackRecord = record;
			}
		}
		m_cardBackData = new Map<int, CardBackData>();
		foreach (CardBackData item in list)
		{
			m_cardBackData[item.ID] = item;
		}
		m_LoadedCardBacks = new Map<string, CardBack>();
		m_LoadedCardBacksBySlot = new Map<CardBackSlot, CardBackSlotData>();
	}

	private IEnumerator SetTextureWhenLoaded(Renderer renderer, int matIdx, CardBackSlot slot)
	{
		while (IsCardBackLoading(slot))
		{
			yield return null;
		}
		SetTexture(renderer, matIdx, slot);
	}

	private void SetTexture(Renderer renderer, int matIdx, CardBackSlot slot)
	{
		if (renderer == null)
		{
			return;
		}
		int count = renderer.GetMaterials().Count;
		if (matIdx < 0 || matIdx >= count)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetTexture(): matIdx {0} is not within the bounds of renderer's materials (count {1})", matIdx, count);
			return;
		}
		CardBack cardBackBySlot = GetCardBackBySlot(slot);
		if (!(cardBackBySlot == null))
		{
			Texture cardBackTexture = cardBackBySlot.m_CardBackTexture;
			if (cardBackTexture == null)
			{
				UnityEngine.Debug.LogWarning($"CardBackManager SetTexture(): texture is null!   obj: {renderer.gameObject.name}  slot: {slot}");
			}
			else
			{
				renderer.GetMaterial(matIdx).mainTexture = cardBackTexture;
			}
		}
	}

	private IEnumerator SetCardBackWhenLoaded(GameObject go, CardBackSlot slot)
	{
		while (IsCardBackLoading(slot))
		{
			yield return null;
		}
		SetCardBack(go, slot);
	}

	private void SetCardBack(GameObject go, CardBackSlot slot)
	{
		CardBack cardBackBySlot = GetCardBackBySlot(slot);
		if (cardBackBySlot == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetCardBack(): cardback not loaded for Slot: {0}", slot);
			cardBackBySlot = GetCardBackBySlot(CardBackSlot.DEFAULT);
			if (cardBackBySlot == null)
			{
				UnityEngine.Debug.LogWarning("CardBackManager SetCardBack(): default cardback not loaded");
				return;
			}
		}
		SetCardBack(go, cardBackBySlot);
	}

	public static void SetCardBack(GameObject go, CardBack cardBack)
	{
		SetCardBack(go, cardBack, unlit: false, shadowActive: false);
	}

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
			UnityEngine.Debug.LogWarningFormat("CardBackManager SetCardBack() go=null, cardBack.name={0}, stacktrace=\n{1}", cardBack.name, stackTrace.ToString());
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
		Material[] array = new Material[(!(cardBackMaterial2 != null)) ? 1 : 2];
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
			foreach (Material item in materials)
			{
				if (!(item == null))
				{
					if (item.HasProperty("_Seed") && item.GetFloat("_Seed") == 0f)
					{
						item.SetFloat("_Seed", value2);
					}
					if (item.HasProperty("_LightingBlend"))
					{
						item.SetFloat("_LightingBlend", value);
					}
				}
			}
		}
		else
		{
			UnityEngine.Debug.LogWarning("CardBackManager SetCardBack() material=null");
		}
		if (cardBack.cardBackHelper == CardBack.cardBackHelpers.None)
		{
			RemoveCardBackHelper<CardBackHelperBubbleLevel>(go);
		}
		else if (cardBack.cardBackHelper == CardBack.cardBackHelpers.CardBackHelperBubbleLevel)
		{
			AddCardBackHelper<CardBackHelperBubbleLevel>(go);
		}
		Actor actor = SceneUtils.FindComponentInThisOrParents<Actor>(go);
		if (actor != null)
		{
			actor.UpdateMissingCardArt();
			actor.EnableCardbackShadow(shadowActive);
		}
	}

	public static T AddCardBackHelper<T>(GameObject go) where T : MonoBehaviour
	{
		RemoveCardBackHelper<T>(go);
		return go.AddComponent<T>();
	}

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

	private IEnumerator SetDragEffectsWhenLoaded(GameObject go, CardBackSlot slot)
	{
		while (IsCardBackLoading(slot))
		{
			yield return null;
		}
		SetDragEffects(go, slot);
	}

	private void SetDragEffects(GameObject go, CardBackSlot slot)
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
		CardBack cardBackBySlot = GetCardBackBySlot(slot);
		if (!(cardBackBySlot == null))
		{
			if (componentInChildren.m_EffectsRoot != null)
			{
				UnityEngine.Object.Destroy(componentInChildren.m_EffectsRoot);
			}
			if (!(cardBackBySlot.m_DragEffect == null))
			{
				GameObject gameObject = (componentInChildren.m_EffectsRoot = UnityEngine.Object.Instantiate(cardBackBySlot.m_DragEffect));
				gameObject.transform.parent = componentInChildren.gameObject.transform;
				gameObject.transform.localPosition = Vector3.zero;
				gameObject.transform.localRotation = Quaternion.identity;
				gameObject.transform.localScale = Vector3.one;
			}
		}
	}

	private IEnumerator SetDeckCardBackWhenLoaded(GameObject go, CardBackSlot slot)
	{
		while (IsCardBackLoading(slot))
		{
			yield return null;
		}
		SetDeckCardBack(go, slot);
	}

	private void SetDeckCardBack(GameObject go, CardBackSlot slot)
	{
		CardBack cardBackBySlot = GetCardBackBySlot(slot);
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

	private void OnCheatOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		Log.CardbackMgr.Print("Cheat Option Change Called");
		int @int = Options.Get().GetInt(option, 0);
		if (m_cardBackData.ContainsKey(@int))
		{
			CardBackSlot slot = CardBackSlot.FRIENDLY;
			if (option == Option.CARD_BACK2)
			{
				slot = CardBackSlot.OPPONENT;
			}
			LoadCardBack(m_cardBackData[@int].PrefabName, slot);
			UpdateAllCardBacksInSceneWhenReady();
		}
	}

	private void NetCache_OnNetCacheCardBacksUpdated()
	{
		Processor.RunCoroutine(HandleNetCacheCardBacksWhenReady());
	}

	private IEnumerator HandleNetCacheCardBacksWhenReady()
	{
		while (m_cardBackData == null || FixedRewardsMgr.Get() == null || !FixedRewardsMgr.Get().IsStartupFinished())
		{
			yield return null;
		}
		NetCache.NetCacheCardBacks cardBacks = GetCardBacks();
		AddNewCardBack(0);
		AddRandomCardBackIfEligible();
		if (!m_cardBackData.ContainsKey(cardBacks.FavoriteCardBack))
		{
			Log.CardbackMgr.Print("No cardback for {0}, set default to CardBackDbId.CLASSIC", cardBacks.FavoriteCardBack);
			cardBacks.FavoriteCardBack = 0;
		}
		LoadFavoriteCardBack(cardBacks.FavoriteCardBack);
	}

	private IEnumerator UpdateAllCardBacksInSceneWhenReadyImpl()
	{
		while (IsCardBackLoading(CardBackSlot.FRIENDLY) || IsCardBackLoading(CardBackSlot.OPPONENT) || IsCardBackLoading(CardBackSlot.RANDOM))
		{
			yield return null;
		}
		lock (cardbackListenerCollectionLock)
		{
			foreach (UpdateCardbacksListener updateCardbacksListener in m_updateCardbacksListeners)
			{
				updateCardbacksListener.Fire();
			}
		}
	}

	private void LoadCardBack(AssetReference assetRef, CardBackSlot slot)
	{
		string text = assetRef.ToString();
		if (!m_LoadedCardBacksBySlot.TryGetValue(slot, out var value))
		{
			value = new CardBackSlotData();
			m_LoadedCardBacksBySlot[slot] = value;
		}
		if (m_LoadedCardBacks.ContainsKey(text))
		{
			if (!(m_LoadedCardBacks[text] == null))
			{
				value.m_isLoading = false;
				value.m_cardBackAssetString = text;
				value.m_cardBack = m_LoadedCardBacks[text];
				return;
			}
			m_LoadedCardBacks.Remove(text);
		}
		if (!(value.m_cardBackAssetString == text))
		{
			value.m_isLoading = true;
			value.m_cardBackAssetString = text;
			value.m_cardBack = null;
			LoadCardBackData loadCardBackData = new LoadCardBackData();
			loadCardBackData.m_Slot = slot;
			loadCardBackData.m_Path = text;
			AssetLoader.Get().InstantiatePrefab(text, OnCardBackLoaded, loadCardBackData);
		}
	}

	private void OnCardBackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		LoadCardBackData loadCardBackData = callbackData as LoadCardBackData;
		if (go == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): Failed to load CardBack: {0} For: {1}", assetRef, loadCardBackData.m_Slot);
			m_LoadedCardBacksBySlot.Remove(loadCardBackData.m_Slot);
			return;
		}
		go.transform.parent = SceneObject.transform;
		go.transform.position = new Vector3(1000f, -1000f, -1000f);
		CardBack component = go.GetComponent<CardBack>();
		if (component == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): Failed to find CardBack component: {0} slot: {1}", loadCardBackData.m_Path, loadCardBackData.m_Slot);
			return;
		}
		if (component.m_CardBackMesh == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackMesh in null! - {0}", loadCardBackData.m_Path);
			return;
		}
		if (component.m_CardBackMaterial == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackMaterial in null! - {0}", loadCardBackData.m_Path);
			return;
		}
		if (component.m_CardBackTexture == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnCardBackLoaded(): cardBack.m_CardBackTexture in null! - {0}", loadCardBackData.m_Path);
			return;
		}
		m_LoadedCardBacks[loadCardBackData.m_Path] = component;
		if (m_LoadedCardBacksBySlot.TryGetValue(loadCardBackData.m_Slot, out var value))
		{
			value.m_isLoading = false;
			value.m_cardBack = component;
		}
	}

	private void OnHiddenActorLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		LoadCardBackData obj = (LoadCardBackData)callbackData;
		int cardBackIndex = obj.m_CardBackIndex;
		string prefabName = m_cardBackData[cardBackIndex].PrefabName;
		obj.m_GameObject = go;
		AssetLoader.Get().InstantiatePrefab(prefabName, OnHiddenActorCardBackLoaded, callbackData);
	}

	private void OnHiddenActorCardBackLoaded(AssetReference assetRef, GameObject go, object callbackData)
	{
		if (go == null)
		{
			Error.AddDevWarning("Error", "CardBackManager OnHiddenActorCardBackLoaded() path={0}, gameobject=null", assetRef);
			return;
		}
		CardBack componentInChildren = go.GetComponentInChildren<CardBack>();
		if (componentInChildren == null)
		{
			UnityEngine.Debug.LogWarningFormat("CardBackManager OnHiddenActorCardBackLoaded() path={0}, gameobject={1}, cardback=null", assetRef, go.name);
		}
		else
		{
			LoadCardBackData loadCardBackData = (LoadCardBackData)callbackData;
			loadCardBackData.m_CardBack = componentInChildren;
			Processor.RunCoroutine(HiddenActorCardBackLoadedSetup(loadCardBackData));
		}
	}

	private IEnumerator HiddenActorCardBackLoadedSetup(LoadCardBackData data)
	{
		yield return null;
		yield return null;
		if (data != null && !(data.m_GameObject == null))
		{
			SetCardBack(data.m_GameObject.GetComponent<Actor>().m_cardMesh, data.m_CardBack, data.m_Unlit, data.m_ShadowActive);
			data.m_CardBack.gameObject.transform.parent = data.m_GameObject.transform;
			data.m_Callback(data);
		}
	}

	private int GetValidCardBackID(int cardBackID)
	{
		if (!m_cardBackData.ContainsKey(cardBackID))
		{
			Log.CardbackMgr.Print("Cardback ID {0} not found, defaulting to Classic", cardBackID);
			return 0;
		}
		return cardBackID;
	}

	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		LoadFavoriteCardBack(newFavoriteCardBackID);
	}

	private void LoadFavoriteCardBack(int favoriteCardBackID)
	{
		if (!HearthstoneServices.TryGet<GameMgr>(out var service) || !service.IsSpectator())
		{
			LoadCardBackIntoSlot(favoriteCardBackID, CardBackSlot.FRIENDLY);
		}
	}

	private void OnSceneLoaded(SceneMgr.Mode mode, PegasusScene scene, object userData)
	{
		if (SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.GAMEPLAY && SceneMgr.Get().GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			CardBackData cardBackData = m_cardBackData[GetFavoriteCardBackID()];
			LoadCardBack(cardBackData.PrefabName, CardBackSlot.FRIENDLY);
		}
	}

	private void LoadCardBackIntoSlot(int cardBackId, CardBackSlot slot)
	{
		int validCardBackID = GetValidCardBackID(cardBackId);
		if (m_cardBackData.TryGetValue(validCardBackID, out var value))
		{
			LoadCardBack(value.PrefabName, slot);
			UpdateAllCardBacksInSceneWhenReady();
		}
	}
}
