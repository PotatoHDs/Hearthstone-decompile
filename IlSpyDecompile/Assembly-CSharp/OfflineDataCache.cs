using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bgs.types;
using Hearthstone;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public static class OfflineDataCache
{
	public class OfflineData
	{
		public int UniqueFakeDeckId = -999;

		public List<long> FakeDeckIds;

		public List<DeckInfo> OriginalDeckList;

		public List<DeckInfo> LocalDeckList;

		public List<DeckContents> OriginalDeckContents;

		public List<DeckContents> LocalDeckContents;

		public bool m_hasChangedFavoriteHeroesOffline;

		public List<FavoriteHero> FavoriteHeroes;

		public bool m_hasChangedCardBacksOffline;

		public CardBacks CardBacks;

		public Collection Collection;

		public bool m_hasChangedCoinsOffline;

		public Coins Coins;
	}

	private const string DirectoryName = "Offline";

	private const int OfflineDataVersionNumber = 1;

	public static void CacheLocalAndOriginalDeckList(List<DeckInfo> localDecklist, List<DeckInfo> originalDecklist)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.LocalDeckList = localDecklist;
		offlineData.OriginalDeckList = originalDecklist;
		Log.Offline.PrintDebug("OfflineDataCache: Caching local deck list. Local Count={0}, Original Count={1}", localDecklist.Count, originalDecklist.Count);
		WriteOfflineDataToFile(offlineData);
	}

	public static void CacheLocalAndOriginalDeckContents(DeckContents localDeckContents, DeckContents originalDeckContents)
	{
		OfflineData data = ReadOfflineDataFromFile();
		SetLocalDeckContentsInOfflineData(ref data, localDeckContents);
		SetOriginalDeckContentsInOfflineData(ref data, originalDeckContents);
		WriteOfflineDataToFile(data);
	}

	private static void SetOriginalDeckContentsInOfflineData(ref OfflineData data, DeckContents packet)
	{
		if (data.OriginalDeckContents == null)
		{
			data.OriginalDeckContents = new List<DeckContents>();
		}
		data.OriginalDeckContents.RemoveAll((DeckContents c) => c.DeckId == packet.DeckId);
		data.OriginalDeckContents.Add(packet);
		Log.Offline.PrintDebug("OfflineDataCache: Caching original deck contents: id={0}", packet.DeckId);
	}

	private static void SetLocalDeckContentsInOfflineData(ref OfflineData data, DeckContents packet)
	{
		if (data.LocalDeckContents == null)
		{
			data.LocalDeckContents = new List<DeckContents>();
		}
		data.LocalDeckContents.RemoveAll((DeckContents c) => c.DeckId == packet.DeckId);
		data.LocalDeckContents.Add(packet);
		Log.Offline.PrintDebug("OfflineDataCache: Caching local deck contents: id={0}", packet.DeckId);
	}

	public static void CacheFavoriteHeroes(FavoriteHeroesResponse packet)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.FavoriteHeroes = new List<FavoriteHero>(packet.FavoriteHeroes);
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite heroes: {0}", packet.ToHumanReadableString());
		WriteOfflineDataToFile(offlineData);
	}

	public static void CacheCardBacks(CardBacks packet)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.CardBacks = packet;
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite card backs: {0}", packet.ToHumanReadableString());
		WriteOfflineDataToFile(offlineData);
	}

	public static void CacheCoins(Coins packet)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.Coins = packet;
		Log.Offline.PrintDebug("OfflineDataCache: Caching favorite coins: {0}", packet.ToHumanReadableString());
		WriteOfflineDataToFile(offlineData);
	}

	public static List<DeckInfo> GetOriginalDeckListFromCache()
	{
		return ReadOfflineDataFromFile().OriginalDeckList;
	}

	public static List<DeckInfo> GetLocalDeckListFromCache()
	{
		return ReadOfflineDataFromFile().LocalDeckList;
	}

	public static List<DeckContents> GetOriginalDeckContentsFromCache()
	{
		return ReadOfflineDataFromFile().OriginalDeckContents;
	}

	public static List<DeckContents> GetLocalDeckContentsFromCache()
	{
		return ReadOfflineDataFromFile().LocalDeckContents;
	}

	public static List<FavoriteHero> GetFavoriteHeroesFromCache()
	{
		return ReadOfflineDataFromFile().FavoriteHeroes;
	}

	public static CardBacks GetCardBacksFromCache()
	{
		return ReadOfflineDataFromFile().CardBacks;
	}

	public static Coins GetCoinsFromCache()
	{
		return ReadOfflineDataFromFile().Coins;
	}

	public static bool DidDecksChangeWhileOffline()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (!offlineData.LocalDeckList.Equals(offlineData.OriginalDeckList))
		{
			return true;
		}
		foreach (DeckInfo deck in offlineData.LocalDeckList)
		{
			DeckContents deckContents = offlineData.LocalDeckContents.FirstOrDefault((DeckContents d) => d.DeckId == deck.Id);
			DeckContents deckContents2 = offlineData.OriginalDeckContents.FirstOrDefault((DeckContents d) => d.DeckId == deck.Id);
			if (deckContents == null || deckContents2 == null || !deckContents.Equals(deckContents2))
			{
				return true;
			}
		}
		return false;
	}

	public static DeckInfo GetDeckInfoFromDeckList(long deckId, List<DeckInfo> deckList)
	{
		if (deckList == null)
		{
			return null;
		}
		if (deckList.Count((DeckInfo d) => d.Id == deckId) > 1)
		{
			Log.Offline.PrintError("GetDeckInfoFromDeckList: Found multiple decks in cache with id: {0}", deckId);
		}
		foreach (DeckInfo deck in deckList)
		{
			if (deck.Id == deckId)
			{
				return deck;
			}
		}
		Log.Offline.PrintWarning("GetDeckInfoFromDeckList: No deck header found with id: {0}", deckId);
		return null;
	}

	public static DeckContents GetDeckContentsFromDeckContentsList(long deckId, List<DeckContents> list)
	{
		if (list == null)
		{
			return null;
		}
		if (list.Count((DeckContents d) => d.DeckId == deckId) > 1)
		{
			Log.Offline.PrintError("GetDeckContentsFromDeckContentsList: Found multiple decks in cache with id: {0}", deckId);
		}
		foreach (DeckContents item in list)
		{
			if (item.DeckId == deckId)
			{
				return item;
			}
		}
		Log.Offline.PrintWarning("GetDeckContentsFromDeckContentsList: No deck contents found with id: {0}", deckId);
		return null;
	}

	public static List<long> GetFakeDeckIds(OfflineData data = null)
	{
		if (data == null)
		{
			data = ReadOfflineDataFromFile();
		}
		List<long> list = new List<long>();
		if (data.FakeDeckIds == null)
		{
			return list;
		}
		foreach (long fakeDeckId in data.FakeDeckIds)
		{
			if (IsValidFakeId(fakeDeckId) && !list.Contains(fakeDeckId))
			{
				list.Add(fakeDeckId);
			}
		}
		return list;
	}

	public static List<DeckInfo> GetFakeDeckInfos()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		List<DeckInfo> list = new List<DeckInfo>();
		foreach (long fakeDeckId in GetFakeDeckIds(offlineData))
		{
			DeckInfo deckInfo = offlineData.LocalDeckList.FirstOrDefault((DeckInfo d) => d.Id == fakeDeckId);
			if (deckInfo != null)
			{
				list.Add(deckInfo);
			}
		}
		return list;
	}

	public static void ClearFakeDeckIds()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (offlineData.FakeDeckIds == null)
		{
			return;
		}
		foreach (long fakeDeckId in offlineData.FakeDeckIds)
		{
			DeleteDeck(fakeDeckId);
		}
		offlineData.FakeDeckIds = new List<long>();
		offlineData.UniqueFakeDeckId--;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing Fake Deck Ids");
		WriteOfflineDataToFile(offlineData);
	}

	public static bool UpdateDeckWithNewId(long oldId, long newId)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		Log.Offline.PrintDebug("OfflineDataCache: Updating deck {0} with new id {1}", oldId, newId);
		DeckInfo deckInfoFromDeckList = GetDeckInfoFromDeckList(oldId, offlineData.LocalDeckList);
		DeckContents deckContentsFromDeckContentsList = GetDeckContentsFromDeckContentsList(oldId, offlineData.LocalDeckContents);
		if (deckInfoFromDeckList != null)
		{
			deckInfoFromDeckList.Id = newId;
			if (deckContentsFromDeckContentsList != null)
			{
				deckContentsFromDeckContentsList.DeckId = newId;
				WriteOfflineDataToFile(offlineData);
				return true;
			}
			Log.Offline.PrintError("UpdateDeckWithNewId: No deck contents found in Offline Data Cache with old id: {0}", oldId);
			return false;
		}
		Log.Offline.PrintError("UpdateDeckWithNewId: No deck info found in Offline Data Cache with old id: {0}", oldId);
		return false;
	}

	public static void RenameDeck(long deckId, string newName)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		DeckInfo deckInfoFromDeckList = GetDeckInfoFromDeckList(deckId, offlineData.LocalDeckList);
		if (deckInfoFromDeckList == null)
		{
			Log.Offline.PrintError("Received a rename command for deck id={0}, name={1}, but a deck with that id was not found in the OfflineDataCache.", deckId, newName);
		}
		else
		{
			deckInfoFromDeckList.Name = newName;
			Log.Offline.PrintDebug("OfflineDataCache: Renaming deck {0} to {1}", deckId, newName);
			WriteOfflineDataToFile(offlineData);
		}
	}

	public static void SetFavoriteCardBack(int cardBackId)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.CardBacks.FavoriteCardBack = cardBackId;
		offlineData.m_hasChangedCardBacksOffline = true;
		Log.Offline.PrintDebug("OfflineDataCache: Set favorite card back to {0}", cardBackId);
		WriteOfflineDataToFile(offlineData);
	}

	public static void ClearCardBackDirtyFlag()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.m_hasChangedCardBacksOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing card back flag");
		WriteOfflineDataToFile(offlineData);
	}

	public static void SetFavoriteCoin(int coinId)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.Coins.FavoriteCoin = coinId;
		offlineData.m_hasChangedCoinsOffline = true;
		Log.Offline.PrintDebug("OfflineDataCache: Set favorite coin to {0}", coinId);
		WriteOfflineDataToFile(offlineData);
	}

	public static void ClearCoinDirtyFlag()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.m_hasChangedCoinsOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing coin flag");
		WriteOfflineDataToFile(offlineData);
	}

	public static void SetFavoriteHero(int heroClass, PegasusShared.CardDef cardDef, bool wasCalledOffline)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
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
		Log.Offline.PrintDebug("OfflineDataCache: Setting favorite hero for class {0} to {1}", heroClass, cardDef.ToHumanReadableString());
		WriteOfflineDataToFile(offlineData);
	}

	public static void ClearFavoriteHeroesDirtyFlag()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.m_hasChangedFavoriteHeroesOffline = false;
		Log.Offline.PrintDebug("OfflineDataCache: Clearing favorite hero flag");
		WriteOfflineDataToFile(offlineData);
	}

	public static long GetCachedCollectionVersion()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (offlineData != null && offlineData.Collection != null && offlineData.Collection.HasCollectionVersion)
		{
			return offlineData.Collection.CollectionVersion;
		}
		return 0L;
	}

	public static long GetCachedCollectionVersionLastModified()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (offlineData != null && offlineData.Collection != null && offlineData.Collection.HasCollectionVersionLastModified)
		{
			return offlineData.Collection.CollectionVersionLastModified;
		}
		return 0L;
	}

	public static void CacheCollection(Collection collection)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.Collection = collection;
		WriteOfflineDataToFile(offlineData);
	}

	public static Collection GetCachedCollection()
	{
		return ReadOfflineDataFromFile()?.Collection;
	}

	public static List<GetAssetsVersion.DeckModificationTimes> GetCachedDeckContentsTimes()
	{
		List<GetAssetsVersion.DeckModificationTimes> list2 = new List<GetAssetsVersion.DeckModificationTimes>();
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (offlineData == null || offlineData.LocalDeckContents == null || offlineData.LocalDeckList == null)
		{
			return list2;
		}
		foreach (DeckContents deckContent in offlineData.LocalDeckContents)
		{
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
		return list2;
	}

	public static void ApplyDeckSetDataLocally(DeckSetData packet)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		ApplyDeckSetDataToDeck(packet, offlineData.LocalDeckList, offlineData.LocalDeckContents);
		WriteOfflineDataToFile(offlineData);
	}

	public static void ApplyDeckSetDataToOriginalDeck(DeckSetData packet)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		ApplyDeckSetDataToDeck(packet, offlineData.OriginalDeckList, offlineData.OriginalDeckContents);
		Log.Offline.PrintDebug("OfflineDataCache: Applying deck changes to deck. Changes: {0}", packet.ToHumanReadableString());
		WriteOfflineDataToFile(offlineData);
	}

	public static void ApplyDeckSetDataToDeck(DeckSetData packet, List<DeckInfo> deckList, List<DeckContents> deckContentsList)
	{
		DeckInfo deckInfo = null;
		foreach (DeckInfo deck in deckList)
		{
			if (deck.Id == packet.Deck)
			{
				deckInfo = deck;
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
			foreach (DeckCardData card in packet.Cards)
			{
				bool flag = false;
				foreach (DeckCardData card2 in deckContents.Cards)
				{
					if (card2.Def.Asset == card.Def.Asset && card2.Def.Premium == card.Def.Premium)
					{
						card2.Qty = card.Qty;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					deckContents.Cards.Add(card);
				}
			}
		}
		if (deckInfo.Name == null)
		{
			deckInfo.Name = "Unknown";
		}
		deckInfo.LastModified = (long)TimeUtils.DateTimeToUnixTimeStamp(DateTime.Now);
	}

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
		deckSetData.Cards = GetDeckContentsDelta(patchingDeckContents, originalDeckContents);
		if (deckSetData.Cards.Any())
		{
			result = true;
		}
		return result;
	}

	public static bool GenerateDeckSetDataFromDiff(long deckId, List<DeckInfo> patchingDeckList, List<DeckInfo> originalDeckList, List<DeckContents> patchingDeckContentsList, List<DeckContents> originalDeckContentsList, out DeckSetData deckSetData)
	{
		DeckInfo deckInfo = GetDeckInfoFromDeckList(deckId, patchingDeckList);
		DeckInfo deckInfo2 = GetDeckInfoFromDeckList(deckId, originalDeckList);
		DeckContents deckContents = GetDeckContentsFromDeckContentsList(deckId, patchingDeckContentsList);
		DeckContents deckContents2 = GetDeckContentsFromDeckContentsList(deckId, originalDeckContentsList);
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
		return GenerateDeckSetDataFromDiff(deckId, deckInfo, deckInfo2, deckContents, deckContents2, out deckSetData);
	}

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

	public static void DeleteDeck(long deckId)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		offlineData.LocalDeckList.RemoveAll((DeckInfo d) => d.Id == deckId);
		offlineData.LocalDeckContents.RemoveAll((DeckContents d) => d.DeckId == deckId);
		Log.Offline.PrintDebug("OfflineDataCache: Deleting deck: {0}", deckId);
		WriteOfflineDataToFile(offlineData);
	}

	public static void RemoveAllOldDecksContents()
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		if (offlineData.LocalDeckContents != null)
		{
			DeckContents[] array = offlineData.LocalDeckContents.ToArray();
			foreach (DeckContents deckContents in array)
			{
				if (!offlineData.LocalDeckList.Any((DeckInfo d) => d.Id == deckContents.DeckId))
				{
					offlineData.LocalDeckContents.Remove(deckContents);
				}
			}
		}
		if (offlineData.OriginalDeckContents != null)
		{
			DeckContents[] array = offlineData.OriginalDeckContents.ToArray();
			foreach (DeckContents deckContents2 in array)
			{
				if (!offlineData.OriginalDeckList.Any((DeckInfo d) => d.Id == deckContents2.DeckId))
				{
					offlineData.OriginalDeckContents.Remove(deckContents2);
				}
			}
		}
		WriteOfflineDataToFile(offlineData);
	}

	public static DeckInfo CreateDeck(DeckType deckType, string name, int heroDbId, TAG_PREMIUM heroPremium, FormatType formatType, long sortOrder, DeckSourceType sourceType, string pastedDeckHash = null)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
		long andRecordNextFakeId = GetAndRecordNextFakeId(offlineData.FakeDeckIds, offlineData);
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
			Validity = (ulong)((formatType == FormatType.FT_STANDARD) ? 128 : 0),
			FormatType = formatType
		};
		offlineData.LocalDeckList.Add(deckInfo);
		DeckContents deckContents = new DeckContents();
		deckContents.DeckId = andRecordNextFakeId;
		offlineData.LocalDeckContents.Add(deckContents);
		Log.Offline.PrintDebug("OfflineDataCache: Creating offline deck: id={0}", andRecordNextFakeId);
		if (!WriteOfflineDataToFile(offlineData))
		{
			return null;
		}
		return deckInfo;
	}

	public static SetFavoriteCardBack GenerateSetFavoriteCardBackFromDiff(int receivedFavoriteCardBack)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
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

	public static SetFavoriteCoin GenerateSetFavoriteCoinFromDiff(int receivedFavoriteCoin)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
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

	public static List<SetFavoriteHero> GenerateSetFavoriteHeroFromDiff(NetCache.NetCacheFavoriteHeroes receivedFavoriteHeroes)
	{
		OfflineData offlineData = ReadOfflineDataFromFile();
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
			if (!string.Equals(receivedFavoriteHeroes.FavoriteHeroes[(TAG_CLASS)favoriteHero.ClassId].Name, GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset)) || receivedFavoriteHeroes.FavoriteHeroes[(TAG_CLASS)favoriteHero.ClassId].Premium != (TAG_PREMIUM)favoriteHero.Hero.Premium)
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

	private static string GetCacheFolderPath()
	{
		return string.Format("{0}/{1}", FileUtils.CachePath, "Offline");
	}

	private static string GetCacheFilePath()
	{
		string cacheFolderPath = GetCacheFolderPath();
		EntityId myGameAccountId = Network.Get().GetMyGameAccountId();
		string text = $"{myGameAccountId.hi}_{myGameAccountId.lo}";
		string text2 = Network.Get().GetCurrentRegion().ToString();
		string text3 = "";
		if (HearthstoneApplication.IsInternal())
		{
			text3 = string.Format("_{0}", "20.4");
			text3 = text3.Replace(".", "_");
		}
		return $"{cacheFolderPath}/offlineData_{text}_{text2}{text3}.cache";
	}

	private static void CreateCacheFolder()
	{
		string cacheFolderPath = GetCacheFolderPath();
		if (!Directory.Exists(cacheFolderPath))
		{
			try
			{
				Directory.CreateDirectory(cacheFolderPath);
			}
			catch (Exception ex)
			{
				Debug.LogError($"UberText.CreateCacheFolder() - Failed to create {cacheFolderPath}. Reason={ex.Message}");
			}
		}
	}

	private static bool WriteOfflineDataToFile(OfflineData data)
	{
		CreateCacheFolder();
		string cacheFilePath = GetCacheFilePath();
		try
		{
			using BinaryWriter binaryWriter = new BinaryWriter(File.Open(cacheFilePath, FileMode.Create, FileAccess.Write));
			binaryWriter.Write(1);
			IOfflineDataSerializer serializer = OfflineDataSerializer.GetSerializer(1);
			if (serializer == null)
			{
				Debug.LogErrorFormat("Could not find serializer for writing version {0}. Make sure a new seralizer is added when incrementing versions.", 1);
				return false;
			}
			serializer.Serialize(data, binaryWriter);
		}
		catch (IOException ex)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Is disk full? - Exception: {0}", ex.InnerException);
			return false;
		}
		catch (UnauthorizedAccessException ex2)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Are write permissions correctly applied to the file attempting to be accessed? - Exception: {0}", ex2.InnerException);
			return false;
		}
		catch (Exception ex3)
		{
			Log.Offline.PrintError("WriteOfflineDataToFile - Unexpected exception thrown - Exception: {0}", ex3.InnerException);
			return false;
		}
		return true;
	}

	public static OfflineData ReadOfflineDataFromFile()
	{
		OfflineData result = new OfflineData();
		string cacheFolderPath = GetCacheFolderPath();
		string cacheFilePath = GetCacheFilePath();
		if (!Directory.Exists(cacheFolderPath) || !File.Exists(cacheFilePath))
		{
			return result;
		}
		bool flag = false;
		try
		{
			using BinaryReader binaryReader = new BinaryReader(File.Open(cacheFilePath, FileMode.Open));
			int num = binaryReader.ReadInt32();
			IOfflineDataSerializer serializer = OfflineDataSerializer.GetSerializer(num);
			if (serializer == null)
			{
				Debug.LogWarningFormat("Could not find serializer for offline data version {0}", num);
				flag = true;
			}
			else
			{
				result = serializer.Deserialize(binaryReader);
			}
		}
		catch (EndOfStreamException)
		{
			Log.Offline.PrintError("ReadOfflineDataFromFile - Not all protos are represented. Is this a new cache file?");
			flag = true;
		}
		catch (ProtocolBufferException)
		{
			Log.Offline.PrintError("Error parsing cached protobufs from cache. Recreating cache file.");
			flag = true;
		}
		if (flag)
		{
			ClearLocalCacheFile();
		}
		return result;
	}

	public static void ClearLocalCacheFile()
	{
		OfflineData data = new OfflineData();
		Log.Offline.PrintDebug("OfflineDataCache: Clearing local cache file");
		WriteOfflineDataToFile(data);
	}

	private static List<DeckCardData> GetDeckContentsDelta(DeckContents deckContentsLocal, DeckContents deckContentsOriginal)
	{
		List<DeckCardData> cards = deckContentsLocal.Cards;
		List<DeckCardData> cards2 = deckContentsOriginal.Cards;
		List<DeckCardData> list = new List<DeckCardData>();
		foreach (PegasusShared.CardDef cardDef in new HashSet<PegasusShared.CardDef>(from c in cards.Union(cards2).Except(cards.Intersect(cards2)).ToList()
			select c.Def))
		{
			DeckCardData deckCardData = cards.FirstOrDefault((DeckCardData c) => c.Def.Asset == cardDef.Asset && c.Def.Premium == cardDef.Premium);
			DeckCardData deckCardData2 = cards2.FirstOrDefault((DeckCardData c) => c.Def.Asset == cardDef.Asset && c.Def.Premium == cardDef.Premium);
			int num = deckCardData?.Qty ?? 0;
			int num2 = deckCardData2?.Qty ?? 0;
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
		return list;
	}

	private static long GetAndRecordNextFakeId(List<long> usedIds, OfflineData data)
	{
		if (usedIds == null)
		{
			usedIds = new List<long>();
		}
		while (usedIds.Contains(data.UniqueFakeDeckId))
		{
			data.UniqueFakeDeckId--;
		}
		usedIds.Add(data.UniqueFakeDeckId);
		return data.UniqueFakeDeckId;
	}

	private static bool IsValidFakeId(long id)
	{
		return id < 0;
	}
}
