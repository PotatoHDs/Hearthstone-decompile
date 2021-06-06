using System;
using System.Collections;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using Blizzard.T5.Services;
using Hearthstone;
using Hearthstone.Core;
using Hearthstone.DataModels;
using Hearthstone.Progression;
using Hearthstone.UI;
using PegasusUtil;
using UnityEngine;

public class CoinManager : IService
{
	private List<CollectibleCard> m_coinCards;

	private Map<int, int> m_cardIdCoinIdMap;

	public static readonly AssetReference COIN_PREVIEW_PREFAB = new AssetReference("CoinPreview.prefab:4c9e68cbb43064f4287a44286773f026");

	public Type[] GetDependencies()
	{
		return new Type[2]
		{
			typeof(NetCache),
			typeof(SceneMgr)
		};
	}

	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().Resetting += Resetting;
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.FavoriteCoinChanged += OnFavoriteCoinChanged;
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheCoins), NetCache_OnNetCacheCoinsUpdated);
		serviceLocator.Get<Network>().RegisterNetHandler(CoinUpdate.PacketID.ID, ReceiveCoinUpdateMessage);
		InitCoinData();
		yield break;
	}

	public void Shutdown()
	{
		if (HearthstoneServices.TryGet<NetCache>(out var service))
		{
			service.FavoriteCoinChanged -= OnFavoriteCoinChanged;
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.Resetting -= Resetting;
		}
	}

	private void Resetting()
	{
		InitCoinData();
	}

	public static CoinManager Get()
	{
		return HearthstoneServices.Get<CoinManager>();
	}

	private void InitCoinData()
	{
		Processor.RunCoroutine(InitCoinDataWhenReady());
	}

	private IEnumerator InitCoinDataWhenReady()
	{
		DefLoader defLoader = DefLoader.Get();
		while (!defLoader.HasLoadedEntityDefs())
		{
			yield return null;
		}
		NetCache.NetCacheCoins coins = GetCoins();
		if (coins == null)
		{
			yield break;
		}
		AddNewCoin(1);
		if (coins.FavoriteCoin == 0)
		{
			coins.FavoriteCoin = 1;
		}
		m_coinCards = new List<CollectibleCard>();
		m_cardIdCoinIdMap = new Map<int, int>();
		foreach (CoinDbfRecord record in GameDbf.Coin.GetRecords())
		{
			CardDbfRecord cardRecord = record.CardRecord;
			EntityDef entityDef = defLoader.GetEntityDef(cardRecord.NoteMiniGuid);
			CollectibleCard collectibleCard = new CollectibleCard(cardRecord, entityDef, TAG_PREMIUM.NORMAL);
			m_coinCards.Add(collectibleCard);
			m_cardIdCoinIdMap.Add(collectibleCard.CardDbId, record.ID);
		}
		UpdateCoinCards();
	}

	public List<CollectibleCard> GetOrderedCoinCards()
	{
		List<CollectibleCard> list = new List<CollectibleCard>(m_coinCards);
		HashSet<int> ownedCoins = GetCoinsOwned();
		if (ownedCoins != null)
		{
			list.Sort(delegate(CollectibleCard lhs, CollectibleCard rhs)
			{
				CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(lhs.Set);
				CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(rhs.Set);
				m_cardIdCoinIdMap.TryGetValue(lhs.CardDbId, out var value);
				m_cardIdCoinIdMap.TryGetValue(rhs.CardDbId, out var value2);
				bool flag = ownedCoins.Contains(value);
				bool flag2 = ownedCoins.Contains(value2);
				int result = value.CompareTo(value2);
				int num = 0;
				if (cardSet != null && cardSet2 != null)
				{
					num = cardSet.ReleaseOrder.CompareTo(cardSet2.ReleaseOrder);
				}
				if (flag == flag2)
				{
					if (num == 0)
					{
						return result;
					}
					return num;
				}
				return (!flag) ? 1 : (-1);
			});
		}
		return list;
	}

	public int GetFavoriteCoinId()
	{
		return GetCoins()?.FavoriteCoin ?? 1;
	}

	public string GetFavoriteCoinCardId()
	{
		int favoriteCoinId = GetFavoriteCoinId();
		foreach (CollectibleCard coinCard in m_coinCards)
		{
			if (m_cardIdCoinIdMap[coinCard.CardDbId] == favoriteCoinId)
			{
				return coinCard.CardId;
			}
		}
		Log.CoinManager.PrintWarning("GetFavoriteCoinCardId(): Favorite coin's card could not be found.");
		return "GAME_005";
	}

	public void UpdateCoinCards()
	{
		HashSet<int> coinsOwned = GetCoinsOwned();
		if (coinsOwned == null)
		{
			return;
		}
		foreach (CollectibleCard coinCard in m_coinCards)
		{
			int item = m_cardIdCoinIdMap[coinCard.CardDbId];
			coinCard.OwnedCount = (coinsOwned.Contains(item) ? 1 : 0);
		}
	}

	public void AddNewCoin(int coinId)
	{
		NetCache.NetCacheCoins coins = GetCoins();
		if (coins == null)
		{
			Log.CoinManager.PrintWarning($"AddNewCoin({coinId}): trying to access NetCacheCoins before it's been loaded");
		}
		else
		{
			coins.Coins.Add(coinId);
		}
	}

	public void SetFavoriteCoin(string cardId)
	{
		CollectibleCard collectibleCard = m_coinCards.Find((CollectibleCard cardIter) => cardIter.CardId == cardId);
		int value;
		if (collectibleCard == null)
		{
			Log.CoinManager.PrintError($"Trying to set favorite coin with an invalid CardID {cardId}");
		}
		else if (!m_cardIdCoinIdMap.TryGetValue(collectibleCard.CardDbId, out value))
		{
			Log.CoinManager.PrintError($"Trying to set favorite coin with an invalid CardID {cardId} (coinId match not found.)");
		}
		else
		{
			RequestSetFavoriteCoin(value);
		}
	}

	public void RequestSetFavoriteCoin(int newFavoriteCoinID)
	{
		Network.Get().SetFavoriteCoin(newFavoriteCoinID);
	}

	public void OnFavoriteCoinChanged(int newFavoriteCoinID)
	{
		Log.CoinManager.Print(string.Format("CoinManager - Favorite Coin Changed" + $" ID: {newFavoriteCoinID}"));
	}

	private void NetCache_OnNetCacheCoinsUpdated()
	{
		InitCoinData();
	}

	private void ReceiveCoinUpdateMessage()
	{
		CoinUpdate coinUpdate = Network.Get().GetCoinUpdate();
		if (coinUpdate == null)
		{
			return;
		}
		NetCache.NetCacheCoins netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCoins>();
		if (netObject == null)
		{
			return;
		}
		foreach (int item in coinUpdate.AddCoinId)
		{
			netObject.Coins.Add(item);
			Log.CoinManager.Print(string.Format($"CoinManager - Coin added. ID: {item}"));
		}
		foreach (int item2 in coinUpdate.RemoveCoinId)
		{
			netObject.Coins.Remove(item2);
			Log.CoinManager.Print(string.Format($"CoinManager - Coin removed. ID: {item2}"));
		}
		if (coinUpdate.HasFavoriteCoinId)
		{
			netObject.FavoriteCoin = coinUpdate.FavoriteCoinId;
			Log.CoinManager.Print(string.Format("CoinManager - Coin Favorite Set. " + $"ID: {coinUpdate.FavoriteCoinId}"));
		}
		UpdateCoinCards();
	}

	private NetCache.NetCacheCoins GetCoinsFromOfflineData()
	{
		Coins coinsFromCache = OfflineDataCache.GetCoinsFromCache();
		if (coinsFromCache == null)
		{
			return null;
		}
		return new NetCache.NetCacheCoins
		{
			Coins = new HashSet<int>(coinsFromCache.Coins_),
			FavoriteCoin = coinsFromCache.FavoriteCoin
		};
	}

	public NetCache.NetCacheCoins GetCoins()
	{
		NetCache.NetCacheCoins netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCoins>();
		if (netObject == null)
		{
			return GetCoinsFromOfflineData();
		}
		return netObject;
	}

	public HashSet<int> GetCoinsOwned()
	{
		NetCache.NetCacheCoins coins = GetCoins();
		if (coins == null)
		{
			Log.CoinManager.PrintWarning("GetCoinsOwned: Trying to access NetCacheCoins before it's been loaded");
			return null;
		}
		return coins.Coins;
	}

	public bool CoinCardOwned(string cardId)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Log.CoinManager.PrintWarning("CoinCardOwned: Card record not found.");
			return false;
		}
		if (!m_cardIdCoinIdMap.TryGetValue(cardRecord.ID, out var value))
		{
			Log.CoinManager.PrintWarning("CoinCardOwned: Coin id for card not found.");
			return false;
		}
		return GetCoinsOwned()?.Contains(value) ?? false;
	}

	public void ShowCoinPreview(string cardId, Transform startTransform)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Card record not found.");
			return;
		}
		if (!m_cardIdCoinIdMap.TryGetValue(cardRecord.ID, out var coinId))
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Coin id for card not found.");
			return;
		}
		CoinDbfRecord coinRecord = GameDbf.Coin.GetRecord(coinId);
		if (coinRecord == null)
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Coin record not found.");
			return;
		}
		Widget widget = WidgetInstance.Create(COIN_PREVIEW_PREFAB);
		widget.RegisterReadyListener(delegate
		{
			widget.GetComponentInChildren<CoinPreview>().Initialize(new CardDataModel
			{
				CardId = cardRecord.NoteMiniGuid,
				Name = coinRecord.Name,
				FlavorText = cardRecord.FlavorText,
				Premium = TAG_PREMIUM.NORMAL
			}, coinId, startTransform);
		});
	}
}
