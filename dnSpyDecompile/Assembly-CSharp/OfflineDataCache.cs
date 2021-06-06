using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bgs.types;
using Hearthstone;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

// Token: 0x020008F2 RID: 2290
public static class OfflineDataCache
{
	// Token: 0x06007F2C RID: 32556 RVA: 0x00293B94 File Offset: 0x00291D94
	public static void CacheLocalAndOriginalDeckList(List<DeckInfo> localDecklist, List<DeckInfo> originalDecklist)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.LocalDeckList = localDecklist;
		offlineData.OriginalDeckList = originalDecklist;
		Log.Offline.PrintDebug("OfflineDataCache: Caching local deck list. Local Count={0}, Original Count={1}", new object[]
		{
			localDecklist.Count,
			originalDecklist.Count
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F2D RID: 32557 RVA: 0x00293BF0 File Offset: 0x00291DF0
	public static void CacheLocalAndOriginalDeckContents(DeckContents localDeckContents, DeckContents originalDeckContents)
	{
		OfflineDataCache.OfflineData data = OfflineDataCache.ReadOfflineDataFromFile();
		OfflineDataCache.SetLocalDeckContentsInOfflineData(ref data, localDeckContents);
		OfflineDataCache.SetOriginalDeckContentsInOfflineData(ref data, originalDeckContents);
		OfflineDataCache.WriteOfflineDataToFile(data);
	}

	// Token: 0x06007F2E RID: 32558 RVA: 0x00293C1C File Offset: 0x00291E1C
	private static void SetOriginalDeckContentsInOfflineData(ref OfflineDataCache.OfflineData data, DeckContents packet)
	{
		if (data.OriginalDeckContents == null)
		{
			data.OriginalDeckContents = new List<DeckContents>();
		}
		data.OriginalDeckContents.RemoveAll((DeckContents c) => c.DeckId == packet.DeckId);
		data.OriginalDeckContents.Add(packet);
		Log.Offline.PrintDebug("OfflineDataCache: Caching original deck contents: id={0}", new object[]
		{
			packet.DeckId
		});
	}

	// Token: 0x06007F2F RID: 32559 RVA: 0x00293CA0 File Offset: 0x00291EA0
	private static void SetLocalDeckContentsInOfflineData(ref OfflineDataCache.OfflineData data, DeckContents packet)
	{
		if (data.LocalDeckContents == null)
		{
			data.LocalDeckContents = new List<DeckContents>();
		}
		data.LocalDeckContents.RemoveAll((DeckContents c) => c.DeckId == packet.DeckId);
		data.LocalDeckContents.Add(packet);
		Log.Offline.PrintDebug("OfflineDataCache: Caching local deck contents: id={0}", new object[]
		{
			packet.DeckId
		});
	}

	// Token: 0x06007F30 RID: 32560 RVA: 0x00293D24 File Offset: 0x00291F24
	public static void CacheFavoriteHeroes(FavoriteHeroesResponse packet)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.FavoriteHeroes = new List<FavoriteHero>(packet.FavoriteHeroes);
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite heroes: {0}", new object[]
		{
			packet.ToHumanReadableString()
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F31 RID: 32561 RVA: 0x00293D70 File Offset: 0x00291F70
	public static void CacheCardBacks(CardBacks packet)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.CardBacks = packet;
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite card backs: {0}", new object[]
		{
			packet.ToHumanReadableString()
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F32 RID: 32562 RVA: 0x00293DB0 File Offset: 0x00291FB0
	public static void CacheCoins(Coins packet)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.Coins = packet;
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite coins: {0}", new object[]
		{
			packet.ToHumanReadableString()
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F33 RID: 32563 RVA: 0x00293DEF File Offset: 0x00291FEF
	public static List<DeckInfo> GetOriginalDeckListFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().OriginalDeckList;
	}

	// Token: 0x06007F34 RID: 32564 RVA: 0x00293DFB File Offset: 0x00291FFB
	public static List<DeckInfo> GetLocalDeckListFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().LocalDeckList;
	}

	// Token: 0x06007F35 RID: 32565 RVA: 0x00293E07 File Offset: 0x00292007
	public static List<DeckContents> GetOriginalDeckContentsFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().OriginalDeckContents;
	}

	// Token: 0x06007F36 RID: 32566 RVA: 0x00293E13 File Offset: 0x00292013
	public static List<DeckContents> GetLocalDeckContentsFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().LocalDeckContents;
	}

	// Token: 0x06007F37 RID: 32567 RVA: 0x00293E1F File Offset: 0x0029201F
	public static List<FavoriteHero> GetFavoriteHeroesFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().FavoriteHeroes;
	}

	// Token: 0x06007F38 RID: 32568 RVA: 0x00293E2B File Offset: 0x0029202B
	public static CardBacks GetCardBacksFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().CardBacks;
	}

	// Token: 0x06007F39 RID: 32569 RVA: 0x00293E37 File Offset: 0x00292037
	public static Coins GetCoinsFromCache()
	{
		return OfflineDataCache.ReadOfflineDataFromFile().Coins;
	}

	// Token: 0x06007F3A RID: 32570 RVA: 0x00293E44 File Offset: 0x00292044
	public static bool DidDecksChangeWhileOffline()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (!offlineData.LocalDeckList.Equals(offlineData.OriginalDeckList))
		{
			return true;
		}
		using (List<DeckInfo>.Enumerator enumerator = offlineData.LocalDeckList.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DeckInfo deck = enumerator.Current;
				DeckContents deckContents = offlineData.LocalDeckContents.FirstOrDefault((DeckContents d) => d.DeckId == deck.Id);
				DeckContents deckContents2 = offlineData.OriginalDeckContents.FirstOrDefault((DeckContents d) => d.DeckId == deck.Id);
				if (deckContents == null || deckContents2 == null || !deckContents.Equals(deckContents2))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06007F3B RID: 32571 RVA: 0x00293F04 File Offset: 0x00292104
	public static DeckInfo GetDeckInfoFromDeckList(long deckId, List<DeckInfo> deckList)
	{
		if (deckList == null)
		{
			return null;
		}
		if (deckList.Count((DeckInfo d) => d.Id == deckId) > 1)
		{
			Log.Offline.PrintError("GetDeckInfoFromDeckList: Found multiple decks in cache with id: {0}", new object[]
			{
				deckId
			});
		}
		foreach (DeckInfo deckInfo in deckList)
		{
			if (deckInfo.Id == deckId)
			{
				return deckInfo;
			}
		}
		Log.Offline.PrintWarning("GetDeckInfoFromDeckList: No deck header found with id: {0}", new object[]
		{
			deckId
		});
		return null;
	}

	// Token: 0x06007F3C RID: 32572 RVA: 0x00293FD0 File Offset: 0x002921D0
	public static DeckContents GetDeckContentsFromDeckContentsList(long deckId, List<DeckContents> list)
	{
		if (list == null)
		{
			return null;
		}
		if (list.Count((DeckContents d) => d.DeckId == deckId) > 1)
		{
			Log.Offline.PrintError("GetDeckContentsFromDeckContentsList: Found multiple decks in cache with id: {0}", new object[]
			{
				deckId
			});
		}
		foreach (DeckContents deckContents in list)
		{
			if (deckContents.DeckId == deckId)
			{
				return deckContents;
			}
		}
		Log.Offline.PrintWarning("GetDeckContentsFromDeckContentsList: No deck contents found with id: {0}", new object[]
		{
			deckId
		});
		return null;
	}

	// Token: 0x06007F3D RID: 32573 RVA: 0x0029409C File Offset: 0x0029229C
	public static List<long> GetFakeDeckIds(OfflineDataCache.OfflineData data = null)
	{
		if (data == null)
		{
			data = OfflineDataCache.ReadOfflineDataFromFile();
		}
		List<long> list = new List<long>();
		if (data.FakeDeckIds == null)
		{
			return list;
		}
		foreach (long num in data.FakeDeckIds)
		{
			if (OfflineDataCache.IsValidFakeId(num) && !list.Contains(num))
			{
				list.Add(num);
			}
		}
		return list;
	}

	// Token: 0x06007F3E RID: 32574 RVA: 0x0029411C File Offset: 0x0029231C
	public static List<DeckInfo> GetFakeDeckInfos()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		List<DeckInfo> list = new List<DeckInfo>();
		using (List<long>.Enumerator enumerator = OfflineDataCache.GetFakeDeckIds(offlineData).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				long fakeDeckId = enumerator.Current;
				DeckInfo deckInfo = offlineData.LocalDeckList.FirstOrDefault((DeckInfo d) => d.Id == fakeDeckId);
				if (deckInfo != null)
				{
					list.Add(deckInfo);
				}
			}
		}
		return list;
	}

	// Token: 0x06007F3F RID: 32575 RVA: 0x002941A8 File Offset: 0x002923A8
	public static void ClearFakeDeckIds()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData.FakeDeckIds == null)
		{
			return;
		}
		foreach (long deckId in offlineData.FakeDeckIds)
		{
			OfflineDataCache.DeleteDeck(deckId);
		}
		offlineData.FakeDeckIds = new List<long>();
		offlineData.UniqueFakeDeckId--;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing Fake Deck Ids", Array.Empty<object>());
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F40 RID: 32576 RVA: 0x0029423C File Offset: 0x0029243C
	public static bool UpdateDeckWithNewId(long oldId, long newId)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		Log.Offline.PrintDebug("OfflineDataCache: Updating deck {0} with new id {1}", new object[]
		{
			oldId,
			newId
		});
		DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(oldId, offlineData.LocalDeckList);
		DeckContents deckContentsFromDeckContentsList = OfflineDataCache.GetDeckContentsFromDeckContentsList(oldId, offlineData.LocalDeckContents);
		if (deckInfoFromDeckList == null)
		{
			Log.Offline.PrintError("UpdateDeckWithNewId: No deck info found in Offline Data Cache with old id: {0}", new object[]
			{
				oldId
			});
			return false;
		}
		deckInfoFromDeckList.Id = newId;
		if (deckContentsFromDeckContentsList != null)
		{
			deckContentsFromDeckContentsList.DeckId = newId;
			OfflineDataCache.WriteOfflineDataToFile(offlineData);
			return true;
		}
		Log.Offline.PrintError("UpdateDeckWithNewId: No deck contents found in Offline Data Cache with old id: {0}", new object[]
		{
			oldId
		});
		return false;
	}

	// Token: 0x06007F41 RID: 32577 RVA: 0x002942F0 File Offset: 0x002924F0
	public static void RenameDeck(long deckId, string newName)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		DeckInfo deckInfoFromDeckList = OfflineDataCache.GetDeckInfoFromDeckList(deckId, offlineData.LocalDeckList);
		if (deckInfoFromDeckList == null)
		{
			Log.Offline.PrintError("Received a rename command for deck id={0}, name={1}, but a deck with that id was not found in the OfflineDataCache.", new object[]
			{
				deckId,
				newName
			});
			return;
		}
		deckInfoFromDeckList.Name = newName;
		Log.Offline.PrintDebug("OfflineDataCache: Renaming deck {0} to {1}", new object[]
		{
			deckId,
			newName
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F42 RID: 32578 RVA: 0x00294368 File Offset: 0x00292568
	public static void SetFavoriteCardBack(int cardBackId)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.CardBacks.FavoriteCardBack = cardBackId;
		offlineData.m_hasChangedCardBacksOffline = true;
		Log.Offline.PrintDebug("OfflineDataCache: Set favorite card back to {0}", new object[]
		{
			cardBackId
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F43 RID: 32579 RVA: 0x002943B3 File Offset: 0x002925B3
	public static void ClearCardBackDirtyFlag()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.m_hasChangedCardBacksOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing card back flag", Array.Empty<object>());
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F44 RID: 32580 RVA: 0x002943DC File Offset: 0x002925DC
	public static void SetFavoriteCoin(int coinId)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.Coins.FavoriteCoin = coinId;
		offlineData.m_hasChangedCoinsOffline = true;
		Log.Offline.PrintDebug("OfflineDataCache: Set favorite coin to {0}", new object[]
		{
			coinId
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F45 RID: 32581 RVA: 0x00294427 File Offset: 0x00292627
	public static void ClearCoinDirtyFlag()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.m_hasChangedCoinsOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing coin flag", Array.Empty<object>());
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F46 RID: 32582 RVA: 0x00294450 File Offset: 0x00292650
	public static void SetFavoriteHero(int heroClass, PegasusShared.CardDef cardDef, bool wasCalledOffline)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		bool flag = false;
		foreach (FavoriteHero favoriteHero in offlineData.FavoriteHeroes)
		{
			if (favoriteHero.ClassId == heroClass)
			{
				flag = true;
				favoriteHero.Hero = cardDef;
				break;
			}
		}
		if (!flag)
		{
			offlineData.FavoriteHeroes.Add(new FavoriteHero
			{
				ClassId = heroClass,
				Hero = cardDef
			});
		}
		if (wasCalledOffline)
		{
			offlineData.m_hasChangedFavoriteHeroesOffline = true;
		}
		Log.Offline.PrintDebug("OfflineDataCache: Setting favorite hero for class {0} to {1}", new object[]
		{
			heroClass,
			cardDef.ToHumanReadableString()
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F47 RID: 32583 RVA: 0x00294514 File Offset: 0x00292714
	public static void ClearFavoriteHeroesDirtyFlag()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.m_hasChangedFavoriteHeroesOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing favorite hero flag", Array.Empty<object>());
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F48 RID: 32584 RVA: 0x0029453C File Offset: 0x0029273C
	public static long GetCachedCollectionVersion()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData != null && offlineData.Collection != null && offlineData.Collection.HasCollectionVersion)
		{
			return offlineData.Collection.CollectionVersion;
		}
		return 0L;
	}

	// Token: 0x06007F49 RID: 32585 RVA: 0x00294578 File Offset: 0x00292778
	public static long GetCachedCollectionVersionLastModified()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData != null && offlineData.Collection != null && offlineData.Collection.HasCollectionVersionLastModified)
		{
			return offlineData.Collection.CollectionVersionLastModified;
		}
		return 0L;
	}

	// Token: 0x06007F4A RID: 32586 RVA: 0x002945B1 File Offset: 0x002927B1
	public static void CacheCollection(Collection collection)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.Collection = collection;
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F4B RID: 32587 RVA: 0x002945C8 File Offset: 0x002927C8
	public static Collection GetCachedCollection()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData == null)
		{
			return null;
		}
		return offlineData.Collection;
	}

	// Token: 0x06007F4C RID: 32588 RVA: 0x002945E8 File Offset: 0x002927E8
	public static List<GetAssetsVersion.DeckModificationTimes> GetCachedDeckContentsTimes()
	{
		List<GetAssetsVersion.DeckModificationTimes> list2 = new List<GetAssetsVersion.DeckModificationTimes>();
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData == null || offlineData.LocalDeckContents == null || offlineData.LocalDeckList == null)
		{
			return list2;
		}
		using (List<DeckContents>.Enumerator enumerator = offlineData.LocalDeckContents.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				DeckContents deckContent = enumerator.Current;
				DeckInfo deckInfo = offlineData.LocalDeckList.Find((DeckInfo list) => list.Id == deckContent.DeckId);
				if (deckInfo != null)
				{
					list2.Add(new GetAssetsVersion.DeckModificationTimes
					{
						DeckId = deckContent.DeckId,
						LastModified = deckInfo.LastModified
					});
				}
			}
		}
		return list2;
	}

	// Token: 0x06007F4D RID: 32589 RVA: 0x002946A8 File Offset: 0x002928A8
	public static void ApplyDeckSetDataLocally(DeckSetData packet)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		OfflineDataCache.ApplyDeckSetDataToDeck(packet, offlineData.LocalDeckList, offlineData.LocalDeckContents);
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F4E RID: 32590 RVA: 0x002946D4 File Offset: 0x002928D4
	public static void ApplyDeckSetDataToOriginalDeck(DeckSetData packet)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		OfflineDataCache.ApplyDeckSetDataToDeck(packet, offlineData.OriginalDeckList, offlineData.OriginalDeckContents);
		Log.Offline.PrintDebug("OfflineDataCache: Applying deck changes to deck. Changes: {0}", new object[]
		{
			packet.ToHumanReadableString()
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F4F RID: 32591 RVA: 0x00294720 File Offset: 0x00292920
	public static void ApplyDeckSetDataToDeck(DeckSetData packet, List<DeckInfo> deckList, List<DeckContents> deckContentsList)
	{
		DeckInfo deckInfo = null;
		foreach (DeckInfo deckInfo2 in deckList)
		{
			if (deckInfo2.Id == packet.Deck)
			{
				deckInfo = deckInfo2;
				break;
			}
		}
		if (deckInfo == null)
		{
			deckInfo = new DeckInfo();
			deckInfo.Id = packet.Deck;
			deckList.Add(deckInfo);
		}
		DeckContents deckContents = null;
		foreach (DeckContents deckContents2 in deckContentsList)
		{
			if (deckContents2.DeckId == packet.Deck)
			{
				deckContents = deckContents2;
				break;
			}
		}
		if (deckContents == null)
		{
			deckContents = new DeckContents();
			deckContents.DeckId = packet.Deck;
			deckContentsList.Add(deckContents);
		}
		if (packet.HasCardBack)
		{
			deckInfo.CardBack = packet.CardBack;
		}
		if (packet.HasHero)
		{
			deckInfo.Hero = packet.Hero.Asset;
			deckInfo.HeroPremium = packet.Hero.Premium;
		}
		if (packet.HasSortOrder)
		{
			deckInfo.SortOrder = packet.SortOrder;
		}
		if (packet.HasPastedDeckHash)
		{
			deckInfo.PastedDeckHash = packet.PastedDeckHash;
		}
		if (packet.Cards != null)
		{
			foreach (DeckCardData deckCardData in packet.Cards)
			{
				bool flag = false;
				foreach (DeckCardData deckCardData2 in deckContents.Cards)
				{
					if (deckCardData2.Def.Asset == deckCardData.Def.Asset && deckCardData2.Def.Premium == deckCardData.Def.Premium)
					{
						deckCardData2.Qty = deckCardData.Qty;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					deckContents.Cards.Add(deckCardData);
				}
			}
		}
		if (deckInfo.Name == null)
		{
			deckInfo.Name = "Unknown";
		}
		deckInfo.LastModified = (long)TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
	}

	// Token: 0x06007F50 RID: 32592 RVA: 0x00294970 File Offset: 0x00292B70
	public static bool GenerateDeckSetDataFromDiff(long deckId, DeckInfo patchingDeckInfo, DeckInfo originalDeckInfo, DeckContents patchingDeckContents, DeckContents originalDeckContents, out DeckSetData deckSetData)
	{
		deckSetData = new DeckSetData();
		deckSetData.Deck = deckId;
		bool result = false;
		if (patchingDeckInfo.CardBack != originalDeckInfo.CardBack)
		{
			deckSetData.CardBack = patchingDeckInfo.CardBack;
			result = true;
		}
		if (patchingDeckInfo.Hero != originalDeckInfo.Hero)
		{
			deckSetData.Hero = new PegasusShared.CardDef
			{
				Asset = patchingDeckInfo.Hero,
				Premium = patchingDeckInfo.HeroPremium
			};
			result = true;
		}
		if (!string.Equals(patchingDeckInfo.PastedDeckHash, originalDeckInfo.PastedDeckHash))
		{
			deckSetData.PastedDeckHash = patchingDeckInfo.PastedDeckHash;
			result = true;
		}
		if (patchingDeckInfo.SortOrder != originalDeckInfo.SortOrder)
		{
			deckSetData.SortOrder = patchingDeckInfo.SortOrder;
			result = true;
		}
		deckSetData.Cards = OfflineDataCache.GetDeckContentsDelta(patchingDeckContents, originalDeckContents);
		if (deckSetData.Cards.Any<DeckCardData>())
		{
			result = true;
		}
		return result;
	}

	// Token: 0x06007F51 RID: 32593 RVA: 0x00294A48 File Offset: 0x00292C48
	public static bool GenerateDeckSetDataFromDiff(long deckId, List<DeckInfo> patchingDeckList, List<DeckInfo> originalDeckList, List<DeckContents> patchingDeckContentsList, List<DeckContents> originalDeckContentsList, out DeckSetData deckSetData)
	{
		DeckInfo deckInfo = OfflineDataCache.GetDeckInfoFromDeckList(deckId, patchingDeckList);
		DeckInfo deckInfo2 = OfflineDataCache.GetDeckInfoFromDeckList(deckId, originalDeckList);
		DeckContents deckContents = OfflineDataCache.GetDeckContentsFromDeckContentsList(deckId, patchingDeckContentsList);
		DeckContents deckContents2 = OfflineDataCache.GetDeckContentsFromDeckContentsList(deckId, originalDeckContentsList);
		if (deckInfo == null)
		{
			deckInfo = new DeckInfo();
		}
		if (deckInfo2 == null)
		{
			deckInfo2 = new DeckInfo();
		}
		if (deckContents == null)
		{
			deckContents = new DeckContents();
		}
		if (deckContents2 == null)
		{
			deckContents2 = new DeckContents();
		}
		return OfflineDataCache.GenerateDeckSetDataFromDiff(deckId, deckInfo, deckInfo2, deckContents, deckContents2, out deckSetData);
	}

	// Token: 0x06007F52 RID: 32594 RVA: 0x00294AA6 File Offset: 0x00292CA6
	public static RenameDeck GenerateRenameDeckFromDiff(long deckId, DeckInfo patchingDeckInfo, DeckInfo originalDeckInfo)
	{
		if (!string.Equals(patchingDeckInfo.Name, originalDeckInfo.Name))
		{
			return new RenameDeck
			{
				Deck = deckId,
				Name = patchingDeckInfo.Name
			};
		}
		return null;
	}

	// Token: 0x06007F53 RID: 32595 RVA: 0x00294AD8 File Offset: 0x00292CD8
	public static void DeleteDeck(long deckId)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		offlineData.LocalDeckList.RemoveAll((DeckInfo d) => d.Id == deckId);
		offlineData.LocalDeckContents.RemoveAll((DeckContents d) => d.DeckId == deckId);
		Log.Offline.PrintDebug("OfflineDataCache: Deleting deck: {0}", new object[]
		{
			deckId
		});
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F54 RID: 32596 RVA: 0x00294B54 File Offset: 0x00292D54
	public static void RemoveAllOldDecksContents()
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (offlineData.LocalDeckContents != null)
		{
			DeckContents[] array = offlineData.LocalDeckContents.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				DeckContents deckContents = array[i];
				if (!offlineData.LocalDeckList.Any((DeckInfo d) => d.Id == deckContents.DeckId))
				{
					offlineData.LocalDeckContents.Remove(deckContents);
				}
			}
		}
		if (offlineData.OriginalDeckContents != null)
		{
			DeckContents[] array = offlineData.OriginalDeckContents.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				DeckContents deckContents = array[i];
				if (!offlineData.OriginalDeckList.Any((DeckInfo d) => d.Id == deckContents.DeckId))
				{
					offlineData.OriginalDeckContents.Remove(deckContents);
				}
			}
		}
		OfflineDataCache.WriteOfflineDataToFile(offlineData);
	}

	// Token: 0x06007F55 RID: 32597 RVA: 0x00294C2C File Offset: 0x00292E2C
	public static DeckInfo CreateDeck(DeckType deckType, string name, int heroDbId, TAG_PREMIUM heroPremium, FormatType formatType, long sortOrder, DeckSourceType sourceType, string pastedDeckHash = null)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		long andRecordNextFakeId = OfflineDataCache.GetAndRecordNextFakeId(offlineData.FakeDeckIds, offlineData);
		DeckInfo deckInfo = new DeckInfo
		{
			Id = andRecordNextFakeId,
			DeckType = deckType,
			Name = name,
			Hero = heroDbId,
			HeroPremium = (int)heroPremium,
			SourceType = sourceType,
			SortOrder = sortOrder,
			PastedDeckHash = pastedDeckHash,
			Validity = ((formatType == FormatType.FT_STANDARD) ? 128UL : 0UL),
			FormatType = formatType
		};
		offlineData.LocalDeckList.Add(deckInfo);
		DeckContents deckContents = new DeckContents();
		deckContents.DeckId = andRecordNextFakeId;
		offlineData.LocalDeckContents.Add(deckContents);
		Log.Offline.PrintDebug("OfflineDataCache: Creating offline deck: id={0}", new object[]
		{
			andRecordNextFakeId
		});
		if (!OfflineDataCache.WriteOfflineDataToFile(offlineData))
		{
			return null;
		}
		return deckInfo;
	}

	// Token: 0x06007F56 RID: 32598 RVA: 0x00294CF8 File Offset: 0x00292EF8
	public static SetFavoriteCardBack GenerateSetFavoriteCardBackFromDiff(int receivedFavoriteCardBack)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (!offlineData.m_hasChangedCardBacksOffline)
		{
			return null;
		}
		if (offlineData.CardBacks != null && offlineData.CardBacks.FavoriteCardBack != receivedFavoriteCardBack)
		{
			return new SetFavoriteCardBack
			{
				CardBack = offlineData.CardBacks.FavoriteCardBack
			};
		}
		return null;
	}

	// Token: 0x06007F57 RID: 32599 RVA: 0x00294D44 File Offset: 0x00292F44
	public static SetFavoriteCoin GenerateSetFavoriteCoinFromDiff(int receivedFavoriteCoin)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		if (!offlineData.m_hasChangedCoinsOffline)
		{
			return null;
		}
		if (offlineData.Coins != null && offlineData.Coins.FavoriteCoin != receivedFavoriteCoin)
		{
			return new SetFavoriteCoin
			{
				Coin = offlineData.Coins.FavoriteCoin
			};
		}
		return null;
	}

	// Token: 0x06007F58 RID: 32600 RVA: 0x00294D90 File Offset: 0x00292F90
	public static List<SetFavoriteHero> GenerateSetFavoriteHeroFromDiff(NetCache.NetCacheFavoriteHeroes receivedFavoriteHeroes)
	{
		OfflineDataCache.OfflineData offlineData = OfflineDataCache.ReadOfflineDataFromFile();
		List<SetFavoriteHero> list = new List<SetFavoriteHero>();
		if (!offlineData.m_hasChangedFavoriteHeroesOffline)
		{
			return list;
		}
		if (offlineData.FavoriteHeroes == null)
		{
			return list;
		}
		foreach (FavoriteHero favoriteHero in offlineData.FavoriteHeroes)
		{
			if (!string.Equals(receivedFavoriteHeroes.FavoriteHeroes[(TAG_CLASS)favoriteHero.ClassId].Name, GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset, false)) || receivedFavoriteHeroes.FavoriteHeroes[(TAG_CLASS)favoriteHero.ClassId].Premium != (TAG_PREMIUM)favoriteHero.Hero.Premium)
			{
				SetFavoriteHero item = new SetFavoriteHero
				{
					FavoriteHero = new FavoriteHero
					{
						ClassId = favoriteHero.ClassId,
						Hero = favoriteHero.Hero
					}
				};
				list.Add(item);
			}
		}
		return list;
	}

	// Token: 0x06007F59 RID: 32601 RVA: 0x00294E88 File Offset: 0x00293088
	private static string GetCacheFolderPath()
	{
		return string.Format("{0}/{1}", FileUtils.CachePath, "Offline");
	}

	// Token: 0x06007F5A RID: 32602 RVA: 0x00294EA0 File Offset: 0x002930A0
	private static string GetCacheFilePath()
	{
		string cacheFolderPath = OfflineDataCache.GetCacheFolderPath();
		EntityId myGameAccountId = Network.Get().GetMyGameAccountId();
		string text = string.Format("{0}_{1}", myGameAccountId.hi, myGameAccountId.lo);
		string text2 = Network.Get().GetCurrentRegion().ToString();
		string text3 = "";
		if (HearthstoneApplication.IsInternal())
		{
			text3 = string.Format("_{0}", "20.4");
			text3 = text3.Replace(".", "_");
		}
		return string.Format("{0}/offlineData_{1}_{2}{3}.cache", new object[]
		{
			cacheFolderPath,
			text,
			text2,
			text3
		});
	}

	// Token: 0x06007F5B RID: 32603 RVA: 0x00294F50 File Offset: 0x00293150
	private static void CreateCacheFolder()
	{
		string cacheFolderPath = OfflineDataCache.GetCacheFolderPath();
		if (Directory.Exists(cacheFolderPath))
		{
			return;
		}
		try
		{
			Directory.CreateDirectory(cacheFolderPath);
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Format("UberText.CreateCacheFolder() - Failed to create {0}. Reason={1}", cacheFolderPath, ex.Message));
		}
	}

	// Token: 0x06007F5C RID: 32604 RVA: 0x00294FA0 File Offset: 0x002931A0
	private static bool WriteOfflineDataToFile(OfflineDataCache.OfflineData data)
	{
		OfflineDataCache.CreateCacheFolder();
		string cacheFilePath = OfflineDataCache.GetCacheFilePath();
		try
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(cacheFilePath, FileMode.Create, FileAccess.Write)))
			{
				binaryWriter.Write(1);
				IOfflineDataSerializer serializer = OfflineDataSerializer.GetSerializer(1);
				if (serializer == null)
				{
					Debug.LogErrorFormat("Could not find serializer for writing version {0}. Make sure a new seralizer is added when incrementing versions.", new object[]
					{
						1
					});
					return false;
				}
				serializer.Serialize(data, binaryWriter);
			}
		}
		catch (IOException ex)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Is disk full? - Exception: {0}", new object[]
			{
				ex.InnerException
			});
			return false;
		}
		catch (UnauthorizedAccessException ex2)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Are write permissions correctly applied to the file attempting to be accessed? - Exception: {0}", new object[]
			{
				ex2.InnerException
			});
			return false;
		}
		catch (Exception ex3)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Unexpected exception thrown - Exception: {0}", new object[]
			{
				ex3.InnerException
			});
			return false;
		}
		return true;
	}

	// Token: 0x06007F5D RID: 32605 RVA: 0x002950B4 File Offset: 0x002932B4
	public static OfflineDataCache.OfflineData ReadOfflineDataFromFile()
	{
		OfflineDataCache.OfflineData result = new OfflineDataCache.OfflineData();
		string cacheFolderPath = OfflineDataCache.GetCacheFolderPath();
		string cacheFilePath = OfflineDataCache.GetCacheFilePath();
		if (!Directory.Exists(cacheFolderPath) || !File.Exists(cacheFilePath))
		{
			return result;
		}
		bool flag = false;
		try
		{
			using (BinaryReader binaryReader = new BinaryReader(File.Open(cacheFilePath, FileMode.Open)))
			{
				int num = binaryReader.ReadInt32();
				IOfflineDataSerializer serializer = OfflineDataSerializer.GetSerializer(num);
				if (serializer == null)
				{
					Debug.LogWarningFormat("Could not find serializer for offline data version {0}", new object[]
					{
						num
					});
					flag = true;
				}
				else
				{
					result = serializer.Deserialize(binaryReader);
				}
			}
		}
		catch (EndOfStreamException)
		{
			Log.Offline.PrintError("ReadOfflineDataFromFile - Not all protos are represented. Is this a new cache file?", Array.Empty<object>());
			flag = true;
		}
		catch (ProtocolBufferException)
		{
			Log.Offline.PrintError("Error parsing cached protobufs from cache. Recreating cache file.", Array.Empty<object>());
			flag = true;
		}
		if (flag)
		{
			OfflineDataCache.ClearLocalCacheFile();
		}
		return result;
	}

	// Token: 0x06007F5E RID: 32606 RVA: 0x002951A0 File Offset: 0x002933A0
	public static void ClearLocalCacheFile()
	{
		OfflineDataCache.OfflineData data = new OfflineDataCache.OfflineData();
		Log.Offline.PrintDebug("OfflineDataCache: Clearing local cache file", Array.Empty<object>());
		OfflineDataCache.WriteOfflineDataToFile(data);
	}

	// Token: 0x06007F5F RID: 32607 RVA: 0x002951C4 File Offset: 0x002933C4
	private static List<DeckCardData> GetDeckContentsDelta(DeckContents deckContentsLocal, DeckContents deckContentsOriginal)
	{
		List<DeckCardData> cards = deckContentsLocal.Cards;
		List<DeckCardData> cards2 = deckContentsOriginal.Cards;
		List<DeckCardData> list = new List<DeckCardData>();
		using (HashSet<PegasusShared.CardDef>.Enumerator enumerator = new HashSet<PegasusShared.CardDef>(from c in cards.Union(cards2).Except(cards.Intersect(cards2)).ToList<DeckCardData>()
		select c.Def).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				PegasusShared.CardDef cardDef = enumerator.Current;
				DeckCardData deckCardData = cards.FirstOrDefault((DeckCardData c) => c.Def.Asset == cardDef.Asset && c.Def.Premium == cardDef.Premium);
				DeckCardData deckCardData2 = cards2.FirstOrDefault((DeckCardData c) => c.Def.Asset == cardDef.Asset && c.Def.Premium == cardDef.Premium);
				int num = (deckCardData == null) ? 0 : deckCardData.Qty;
				int num2 = (deckCardData2 == null) ? 0 : deckCardData2.Qty;
				if (num != num2)
				{
					DeckCardData item = new DeckCardData
					{
						Def = cardDef,
						Qty = num
					};
					list.Add(item);
				}
			}
		}
		return list;
	}

	// Token: 0x06007F60 RID: 32608 RVA: 0x002952E8 File Offset: 0x002934E8
	private static long GetAndRecordNextFakeId(List<long> usedIds, OfflineDataCache.OfflineData data)
	{
		if (usedIds == null)
		{
			usedIds = new List<long>();
		}
		while (usedIds.Contains((long)data.UniqueFakeDeckId))
		{
			data.UniqueFakeDeckId--;
		}
		usedIds.Add((long)data.UniqueFakeDeckId);
		return (long)data.UniqueFakeDeckId;
	}

	// Token: 0x06007F61 RID: 32609 RVA: 0x00295327 File Offset: 0x00293527
	private static bool IsValidFakeId(long id)
	{
		return id < 0L;
	}

	// Token: 0x040066BE RID: 26302
	private const string DirectoryName = "Offline";

	// Token: 0x040066BF RID: 26303
	private const int OfflineDataVersionNumber = 1;

	// Token: 0x020025A3 RID: 9635
	public class OfflineData
	{
		// Token: 0x0400EE5A RID: 61018
		public int UniqueFakeDeckId = -999;

		// Token: 0x0400EE5B RID: 61019
		public List<long> FakeDeckIds;

		// Token: 0x0400EE5C RID: 61020
		public List<DeckInfo> OriginalDeckList;

		// Token: 0x0400EE5D RID: 61021
		public List<DeckInfo> LocalDeckList;

		// Token: 0x0400EE5E RID: 61022
		public List<DeckContents> OriginalDeckContents;

		// Token: 0x0400EE5F RID: 61023
		public List<DeckContents> LocalDeckContents;

		// Token: 0x0400EE60 RID: 61024
		public bool m_hasChangedFavoriteHeroesOffline;

		// Token: 0x0400EE61 RID: 61025
		public List<FavoriteHero> FavoriteHeroes;

		// Token: 0x0400EE62 RID: 61026
		public bool m_hasChangedCardBacksOffline;

		// Token: 0x0400EE63 RID: 61027
		public CardBacks CardBacks;

		// Token: 0x0400EE64 RID: 61028
		public Collection Collection;

		// Token: 0x0400EE65 RID: 61029
		public bool m_hasChangedCoinsOffline;

		// Token: 0x0400EE66 RID: 61030
		public Coins Coins;
	}
}
