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

// Token: 0x020000E6 RID: 230
public class CoinManager : IService
{
	// Token: 0x06000D41 RID: 3393 RVA: 0x0004C322 File Offset: 0x0004A522
	public Type[] GetDependencies()
	{
		return new Type[]
		{
			typeof(NetCache),
			typeof(SceneMgr)
		};
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0004C344 File Offset: 0x0004A544
	public IEnumerator<IAsyncJobResult> Initialize(ServiceLocator serviceLocator)
	{
		HearthstoneApplication.Get().Resetting += this.Resetting;
		NetCache netCache = serviceLocator.Get<NetCache>();
		netCache.FavoriteCoinChanged += this.OnFavoriteCoinChanged;
		netCache.RegisterUpdatedListener(typeof(NetCache.NetCacheCoins), new Action(this.NetCache_OnNetCacheCoinsUpdated));
		serviceLocator.Get<Network>().RegisterNetHandler(CoinUpdate.PacketID.ID, new Network.NetHandler(this.ReceiveCoinUpdateMessage), null);
		this.InitCoinData();
		yield break;
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0004C35C File Offset: 0x0004A55C
	public void Shutdown()
	{
		NetCache netCache;
		if (HearthstoneServices.TryGet<NetCache>(out netCache))
		{
			netCache.FavoriteCoinChanged -= this.OnFavoriteCoinChanged;
		}
		HearthstoneApplication hearthstoneApplication = HearthstoneApplication.Get();
		if (hearthstoneApplication != null)
		{
			hearthstoneApplication.Resetting -= this.Resetting;
		}
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0004C3A5 File Offset: 0x0004A5A5
	private void Resetting()
	{
		this.InitCoinData();
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0004C3AD File Offset: 0x0004A5AD
	public static CoinManager Get()
	{
		return HearthstoneServices.Get<CoinManager>();
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0004C3B4 File Offset: 0x0004A5B4
	private void InitCoinData()
	{
		Processor.RunCoroutine(this.InitCoinDataWhenReady(), null);
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0004C3C3 File Offset: 0x0004A5C3
	private IEnumerator InitCoinDataWhenReady()
	{
		DefLoader defLoader = DefLoader.Get();
		while (!defLoader.HasLoadedEntityDefs())
		{
			yield return null;
		}
		NetCache.NetCacheCoins coins = this.GetCoins();
		if (coins == null)
		{
			yield break;
		}
		this.AddNewCoin(1);
		if (coins.FavoriteCoin == 0)
		{
			coins.FavoriteCoin = 1;
		}
		this.m_coinCards = new List<CollectibleCard>();
		this.m_cardIdCoinIdMap = new Map<int, int>();
		foreach (CoinDbfRecord coinDbfRecord in GameDbf.Coin.GetRecords())
		{
			CardDbfRecord cardRecord = coinDbfRecord.CardRecord;
			EntityDef entityDef = defLoader.GetEntityDef(cardRecord.NoteMiniGuid);
			CollectibleCard collectibleCard = new CollectibleCard(cardRecord, entityDef, TAG_PREMIUM.NORMAL);
			this.m_coinCards.Add(collectibleCard);
			this.m_cardIdCoinIdMap.Add(collectibleCard.CardDbId, coinDbfRecord.ID);
		}
		this.UpdateCoinCards();
		yield break;
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0004C3D4 File Offset: 0x0004A5D4
	public List<CollectibleCard> GetOrderedCoinCards()
	{
		List<CollectibleCard> list = new List<CollectibleCard>(this.m_coinCards);
		HashSet<int> ownedCoins = this.GetCoinsOwned();
		if (ownedCoins != null)
		{
			list.Sort(delegate(CollectibleCard lhs, CollectibleCard rhs)
			{
				CardSetDbfRecord cardSet = GameDbf.GetIndex().GetCardSet(lhs.Set);
				CardSetDbfRecord cardSet2 = GameDbf.GetIndex().GetCardSet(rhs.Set);
				int item;
				this.m_cardIdCoinIdMap.TryGetValue(lhs.CardDbId, out item);
				int num;
				this.m_cardIdCoinIdMap.TryGetValue(rhs.CardDbId, out num);
				bool flag = ownedCoins.Contains(item);
				bool flag2 = ownedCoins.Contains(num);
				int result = item.CompareTo(num);
				int num2 = 0;
				if (cardSet != null && cardSet2 != null)
				{
					num2 = cardSet.ReleaseOrder.CompareTo(cardSet2.ReleaseOrder);
				}
				if (flag == flag2)
				{
					if (num2 == 0)
					{
						return result;
					}
					return num2;
				}
				else
				{
					if (!flag)
					{
						return 1;
					}
					return -1;
				}
			});
		}
		return list;
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0004C424 File Offset: 0x0004A624
	public int GetFavoriteCoinId()
	{
		NetCache.NetCacheCoins coins = this.GetCoins();
		if (coins == null)
		{
			return 1;
		}
		return coins.FavoriteCoin;
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0004C444 File Offset: 0x0004A644
	public string GetFavoriteCoinCardId()
	{
		int favoriteCoinId = this.GetFavoriteCoinId();
		foreach (CollectibleCard collectibleCard in this.m_coinCards)
		{
			if (this.m_cardIdCoinIdMap[collectibleCard.CardDbId] == favoriteCoinId)
			{
				return collectibleCard.CardId;
			}
		}
		Log.CoinManager.PrintWarning("GetFavoriteCoinCardId(): Favorite coin's card could not be found.", Array.Empty<object>());
		return "GAME_005";
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0004C4D0 File Offset: 0x0004A6D0
	public void UpdateCoinCards()
	{
		HashSet<int> coinsOwned = this.GetCoinsOwned();
		if (coinsOwned == null)
		{
			return;
		}
		foreach (CollectibleCard collectibleCard in this.m_coinCards)
		{
			int item = this.m_cardIdCoinIdMap[collectibleCard.CardDbId];
			collectibleCard.OwnedCount = (coinsOwned.Contains(item) ? 1 : 0);
		}
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0004C54C File Offset: 0x0004A74C
	public void AddNewCoin(int coinId)
	{
		NetCache.NetCacheCoins coins = this.GetCoins();
		if (coins == null)
		{
			Log.CoinManager.PrintWarning(string.Format("AddNewCoin({0}): trying to access NetCacheCoins before it's been loaded", coinId), Array.Empty<object>());
			return;
		}
		coins.Coins.Add(coinId);
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0004C590 File Offset: 0x0004A790
	public void SetFavoriteCoin(string cardId)
	{
		CollectibleCard collectibleCard = this.m_coinCards.Find((CollectibleCard cardIter) => cardIter.CardId == cardId);
		if (collectibleCard == null)
		{
			Log.CoinManager.PrintError(string.Format("Trying to set favorite coin with an invalid CardID {0}", cardId), Array.Empty<object>());
			return;
		}
		int newFavoriteCoinID;
		if (!this.m_cardIdCoinIdMap.TryGetValue(collectibleCard.CardDbId, out newFavoriteCoinID))
		{
			Log.CoinManager.PrintError(string.Format("Trying to set favorite coin with an invalid CardID {0} (coinId match not found.)", cardId), Array.Empty<object>());
			return;
		}
		this.RequestSetFavoriteCoin(newFavoriteCoinID);
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0004C621 File Offset: 0x0004A821
	public void RequestSetFavoriteCoin(int newFavoriteCoinID)
	{
		Network.Get().SetFavoriteCoin(newFavoriteCoinID);
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0004C62E File Offset: 0x0004A82E
	public void OnFavoriteCoinChanged(int newFavoriteCoinID)
	{
		Log.CoinManager.Print(string.Format("CoinManager - Favorite Coin Changed" + string.Format(" ID: {0}", newFavoriteCoinID), Array.Empty<object>()), Array.Empty<object>());
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0004C3A5 File Offset: 0x0004A5A5
	private void NetCache_OnNetCacheCoinsUpdated()
	{
		this.InitCoinData();
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0004C664 File Offset: 0x0004A864
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
		foreach (int num in coinUpdate.AddCoinId)
		{
			netObject.Coins.Add(num);
			Log.CoinManager.Print(string.Format(string.Format("CoinManager - Coin added. ID: {0}", num), Array.Empty<object>()), Array.Empty<object>());
		}
		foreach (int num2 in coinUpdate.RemoveCoinId)
		{
			netObject.Coins.Remove(num2);
			Log.CoinManager.Print(string.Format(string.Format("CoinManager - Coin removed. ID: {0}", num2), Array.Empty<object>()), Array.Empty<object>());
		}
		if (coinUpdate.HasFavoriteCoinId)
		{
			netObject.FavoriteCoin = coinUpdate.FavoriteCoinId;
			Log.CoinManager.Print(string.Format("CoinManager - Coin Favorite Set. " + string.Format("ID: {0}", coinUpdate.FavoriteCoinId), Array.Empty<object>()), Array.Empty<object>());
		}
		this.UpdateCoinCards();
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0004C7CC File Offset: 0x0004A9CC
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

	// Token: 0x06000D53 RID: 3411 RVA: 0x0004C808 File Offset: 0x0004AA08
	public NetCache.NetCacheCoins GetCoins()
	{
		NetCache.NetCacheCoins netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCoins>();
		if (netObject == null)
		{
			return this.GetCoinsFromOfflineData();
		}
		return netObject;
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0004C82C File Offset: 0x0004AA2C
	public HashSet<int> GetCoinsOwned()
	{
		NetCache.NetCacheCoins coins = this.GetCoins();
		if (coins == null)
		{
			Log.CoinManager.PrintWarning("GetCoinsOwned: Trying to access NetCacheCoins before it's been loaded", Array.Empty<object>());
			return null;
		}
		return coins.Coins;
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0004C860 File Offset: 0x0004AA60
	public bool CoinCardOwned(string cardId)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Log.CoinManager.PrintWarning("CoinCardOwned: Card record not found.", Array.Empty<object>());
			return false;
		}
		int item;
		if (!this.m_cardIdCoinIdMap.TryGetValue(cardRecord.ID, out item))
		{
			Log.CoinManager.PrintWarning("CoinCardOwned: Coin id for card not found.", Array.Empty<object>());
			return false;
		}
		HashSet<int> coinsOwned = this.GetCoinsOwned();
		return coinsOwned != null && coinsOwned.Contains(item);
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0004C8CC File Offset: 0x0004AACC
	public void ShowCoinPreview(string cardId, Transform startTransform)
	{
		CardDbfRecord cardRecord = GameUtils.GetCardRecord(cardId);
		if (cardRecord == null)
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Card record not found.", Array.Empty<object>());
			return;
		}
		int coinId;
		if (!this.m_cardIdCoinIdMap.TryGetValue(cardRecord.ID, out coinId))
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Coin id for card not found.", Array.Empty<object>());
			return;
		}
		CoinDbfRecord coinRecord = GameDbf.Coin.GetRecord(coinId);
		if (coinRecord == null)
		{
			Log.CoinManager.PrintWarning("ShowCoinPreview: Coin record not found.", Array.Empty<object>());
			return;
		}
		Widget widget = WidgetInstance.Create(CoinManager.COIN_PREVIEW_PREFAB, false);
		widget.RegisterReadyListener(delegate(object _)
		{
			widget.GetComponentInChildren<CoinPreview>().Initialize(new CardDataModel
			{
				CardId = cardRecord.NoteMiniGuid,
				Name = coinRecord.Name,
				FlavorText = cardRecord.FlavorText,
				Premium = TAG_PREMIUM.NORMAL
			}, coinId, startTransform);
		}, null, true);
	}

	// Token: 0x04000915 RID: 2325
	private List<CollectibleCard> m_coinCards;

	// Token: 0x04000916 RID: 2326
	private Map<int, int> m_cardIdCoinIdMap;

	// Token: 0x04000917 RID: 2327
	public static readonly AssetReference COIN_PREVIEW_PREFAB = new AssetReference("CoinPreview.prefab:4c9e68cbb43064f4287a44286773f026");
}
