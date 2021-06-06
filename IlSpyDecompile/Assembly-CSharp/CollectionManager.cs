using System;
using System.Collections.Generic;
using System.Linq;
using Assets;
using bgs;
using Hearthstone;
using Hearthstone.Core;
using PegasusShared;
using PegasusUtil;
using UnityEngine;

public class CollectionManager
{
	public delegate bool CollectibleCardFilterFunc(CollectibleCard card);

	public class PreconDeck
	{
		private long m_id;

		public long ID => m_id;

		public PreconDeck(long id)
		{
			m_id = id;
		}
	}

	public class TemplateDeck
	{
		public int m_id;

		public TAG_CLASS m_class;

		public int m_sortOrder;

		public Map<string, int> m_cardIds = new Map<string, int>();

		public string m_title;

		public string m_description;

		public string m_displayTexture;

		public string m_event;

		public bool m_isStarterDeck;

		public FormatType m_formatType;
	}

	public class FindCardsResult
	{
		public List<CollectibleCard> m_cards = new List<CollectibleCard>();

		public bool m_resultsWithoutManaFilterExist;

		public bool m_resultsWithoutSetFilterExist;

		public bool m_resultsUnownedExist;

		public bool m_resultsInWildExist;
	}

	public delegate void DelCollectionManagerReady();

	public delegate void DelOnCollectionLoaded();

	public delegate void DelOnCollectionChanged();

	public delegate void DelOnDeckCreated(long id);

	public delegate void DelOnDeckDeleted(CollectionDeck removedDeck);

	public delegate void DelOnDeckContents(long id);

	public delegate void DelOnAllDeckContents();

	public delegate void DelOnNewCardSeen(string cardID, TAG_PREMIUM premium);

	public delegate void DelOnCardRewardsInserted(List<string> cardIDs, List<TAG_PREMIUM> premium);

	public delegate void DelOnAchievesCompleted(List<Achievement> achievements);

	public delegate void OnMassDisenchant(int amount);

	public delegate void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, object callbackData);

	public delegate void FavoriteHeroChangedCallback(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData);

	public delegate void OnUIHeroOverrideCardRemovedCallback();

	public delegate void DeckAutoFillCallback(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> deckFill);

	private class TagCardSetEnumComparer : IEqualityComparer<TAG_CARD_SET>
	{
		public bool Equals(TAG_CARD_SET x, TAG_CARD_SET y)
		{
			return x == y;
		}

		public int GetHashCode(TAG_CARD_SET obj)
		{
			return (int)obj;
		}
	}

	private class TagClassEnumComparer : IEqualityComparer<TAG_CLASS>
	{
		public bool Equals(TAG_CLASS x, TAG_CLASS y)
		{
			return x == y;
		}

		public int GetHashCode(TAG_CLASS obj)
		{
			return (int)obj;
		}
	}

	private class TagCardTypeEnumComparer : IEqualityComparer<TAG_CARDTYPE>
	{
		public bool Equals(TAG_CARDTYPE x, TAG_CARDTYPE y)
		{
			return x == y;
		}

		public int GetHashCode(TAG_CARDTYPE obj)
		{
			return (int)obj;
		}
	}

	private struct CollectibleCardIndex
	{
		public string CardId;

		public TAG_PREMIUM Premium;

		public CollectibleCardIndex(string cardId, TAG_PREMIUM premium)
		{
			CardId = cardId;
			Premium = premium;
		}
	}

	private class CollectibleCardIndexComparer : IEqualityComparer<CollectibleCardIndex>
	{
		public bool Equals(CollectibleCardIndex x, CollectibleCardIndex y)
		{
			if (x.CardId == y.CardId)
			{
				return x.Premium == y.Premium;
			}
			return false;
		}

		public int GetHashCode(CollectibleCardIndex obj)
		{
			return obj.CardId.GetHashCode();
		}
	}

	private class FavoriteHeroChangedListener : EventListener<FavoriteHeroChangedCallback>
	{
		public void Fire(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero)
		{
			m_callback(heroClass, favoriteHero, m_userData);
		}
	}

	private class OnUIHeroOverrideCardRemovedListener : EventListener<OnUIHeroOverrideCardRemovedCallback>
	{
		public void Fire()
		{
			m_callback();
		}
	}

	private class PendingDeckCreateData
	{
		public DeckType m_deckType;

		public string m_name;

		public int m_heroDbId;

		public TAG_PREMIUM m_heroPremium;

		public FormatType m_formatType;

		public DeckSourceType m_sourceType;

		public string m_pastedDeckHash;
	}

	private class PendingDeckDeleteData
	{
		public long m_deckId;
	}

	private class PendingDeckEditData
	{
		public long m_deckId;
	}

	private class PendingDeckRenameData
	{
		public long m_deckId;

		public string m_name;
	}

	public class DeckSort : IComparer<CollectionDeck>
	{
		public const int CUSTOM_STARTING_SORT_ORDER = -100;

		public int Compare(CollectionDeck a, CollectionDeck b)
		{
			if (a.SortOrder == b.SortOrder)
			{
				return b.CreateDate.CompareTo(a.CreateDate);
			}
			return a.SortOrder.CompareTo(b.SortOrder);
		}
	}

	private const int NUM_CARDS_GRANTED_POST_TUTORIAL = 96;

	private const int NUM_CARDS_TO_UNLOCK_ADVANCED_CM = 116;

	private const int NUM_EXPERT_CARDS_TO_UNLOCK_CRAFTING = 20;

	public const int NUM_EXPERT_CARDS_TO_UNLOCK_FORGE = 20;

	public const int NUM_CORE_CARDS_PER_CLASS = 16;

	public const int NUM_CORE_CARDS_NEUTRAL = 75;

	public const int MAX_NUM_TEMPLATE_DECKS = 3;

	public const int MAX_DECKS_PER_PLAYER = 27;

	public const int NUM_CLASSES = 10;

	private const float SMART_DECK_COMPLETE_TIMEOUT = 5f;

	public const int DEFAULT_MAX_COPIES_PER_CARD_NORMAL = 2;

	public const int DEFAULT_MAX_COPIES_PER_CARD_LEGENDARY = 1;

	public const int DEFAULT_MAX_CARDS_PER_DECK = 30;

	private const float PENDING_DECK_CONTENTS_REQUEST_THRESHOLD_SECONDS = 10f;

	private static CollectionManager s_instance;

	private bool m_collectionLoaded;

	private bool m_achievesLoaded;

	private bool m_netCacheLoaded;

	private Map<long, CollectionDeck> m_decks = new Map<long, CollectionDeck>();

	private Map<long, CollectionDeck> m_baseDecks = new Map<long, CollectionDeck>();

	private Map<TAG_CLASS, PreconDeck> m_preconDecks = new Map<TAG_CLASS, PreconDeck>();

	private Map<TAG_CLASS, List<TemplateDeck>> m_templateDecks = new Map<TAG_CLASS, List<TemplateDeck>>();

	private Map<int, TemplateDeck> m_templateDeckMap = new Map<int, TemplateDeck>();

	private CollectionDeck m_EditedDeck;

	private List<TAG_CARD_SET> m_displayableCardSets = new List<TAG_CARD_SET>();

	private List<DelOnCollectionLoaded> m_collectionLoadedListeners = new List<DelOnCollectionLoaded>();

	private List<DelOnCollectionChanged> m_collectionChangedListeners = new List<DelOnCollectionChanged>();

	private List<DelOnDeckCreated> m_deckCreatedListeners = new List<DelOnDeckCreated>();

	private List<DelOnDeckDeleted> m_deckDeletedListeners = new List<DelOnDeckDeleted>();

	private List<DelOnDeckContents> m_deckContentsListeners = new List<DelOnDeckContents>();

	private List<DelOnAllDeckContents> m_allDeckContentsListeners = new List<DelOnAllDeckContents>();

	private List<DelOnNewCardSeen> m_newCardSeenListeners = new List<DelOnNewCardSeen>();

	private List<DelOnCardRewardsInserted> m_cardRewardListeners = new List<DelOnCardRewardsInserted>();

	private List<OnMassDisenchant> m_massDisenchantListeners = new List<OnMassDisenchant>();

	private List<OnEditedDeckChanged> m_editedDeckChangedListeners = new List<OnEditedDeckChanged>();

	private List<Action> m_initialCollectionReceivedListeners = new List<Action>();

	private Map<long, float> m_pendingRequestDeckContents;

	private List<CollectibleCard> m_collectibleCards = new List<CollectibleCard>();

	private Map<int, int> m_coreCounterpartCardMap = new Map<int, int>();

	private Map<CollectibleCardIndex, CollectibleCard> m_collectibleCardIndex;

	private float m_collectionLastModifiedTime;

	private DateTime? m_timeOfLastPlayerDeckSave;

	private bool m_accountHasWildCards;

	private float m_lastSearchForWildCardsTime;

	private bool m_accountEverHadWildCards;

	private bool m_accountHasRotatedItems;

	private bool m_showStandardComingSoonNotice;

	private List<Action> m_onNetCacheDecksProcessed = new List<Action>();

	private Dictionary<long, DeckAutoFillCallback> m_smartDeckCallbackByDeckId = new Dictionary<long, DeckAutoFillCallback>();

	private HashSet<long> m_decksToRequestContentsAfterDeckSetDataResonse = new HashSet<long>();

	private HashSet<int> m_inTransitDeckCreateRequests = new HashSet<int>();

	private HashSet<TAG_CARD_SET> m_filterCardSet = new HashSet<TAG_CARD_SET>(new TagCardSetEnumComparer());

	private HashSet<TAG_CLASS> m_filterCardClass = new HashSet<TAG_CLASS>(new TagClassEnumComparer());

	private HashSet<TAG_CARDTYPE> m_filterCardType = new HashSet<TAG_CARDTYPE>(new TagCardTypeEnumComparer());

	private Map<TAG_CARD_SET, bool> m_filterIsSetRotatedCache;

	private List<FavoriteHeroChangedListener> m_favoriteHeroChangedListeners = new List<FavoriteHeroChangedListener>();

	private List<OnUIHeroOverrideCardRemovedListener> m_onUIHeroOverrideCardRemovedListeners = new List<OnUIHeroOverrideCardRemovedListener>();

	private bool m_waitingForBoxTransition;

	private bool m_hasVisitedCollection;

	private bool m_editMode;

	private TAG_PREMIUM m_premiumPreference = TAG_PREMIUM.DIAMOND;

	private CollectibleDisplay m_collectibleDisplay;

	private PendingDeckCreateData m_pendingDeckCreate;

	private List<PendingDeckDeleteData> m_pendingDeckDeleteList;

	private List<PendingDeckRenameData> m_pendingDeckRenameList;

	private List<PendingDeckEditData> m_pendingDeckEditList;

	private long m_currentPVPDRDeckId;

	private DeckRuleset m_deckRuleset;

	public static event DelCollectionManagerReady OnCollectionManagerReady;

	public NetCache.NetCacheCollection OnInitialCollectionReceived(Collection collection)
	{
		NetCache.NetCacheCollection netCacheCollection = new NetCache.NetCacheCollection();
		if (collection == null)
		{
			return netCacheCollection;
		}
		List<string> list = new List<string>();
		for (int i = 0; i < collection.Stacks.Count; i++)
		{
			CardStack cardStack = collection.Stacks[i];
			NetCache.CardStack cardStack2 = new NetCache.CardStack();
			cardStack2.Def.Name = GameUtils.TranslateDbIdToCardId(cardStack.CardDef.Asset);
			if (string.IsNullOrEmpty(cardStack2.Def.Name))
			{
				Error.AddDevFatal("CollectionManager.OnInitialCollectionReceived: failed to find a card with databaseId: {0}", cardStack.CardDef.Asset);
				list.Add(cardStack.CardDef.Asset.ToString());
				continue;
			}
			cardStack2.Def.Premium = (TAG_PREMIUM)cardStack.CardDef.Premium;
			cardStack2.Date = TimeUtils.PegDateToFileTimeUtc(cardStack.LatestInsertDate);
			cardStack2.Count = cardStack.Count;
			cardStack2.NumSeen = cardStack.NumSeen;
			netCacheCollection.Stacks.Add(cardStack2);
			netCacheCollection.TotalCardsOwned += cardStack2.Count;
			if (GameUtils.IsCardCollectible(cardStack2.Def.Name))
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(cardStack2.Def.Name);
				SetCounts(cardStack2, entityDef);
				if (entityDef.IsCoreCard() && cardStack2.Def.Premium == TAG_PREMIUM.NORMAL)
				{
					netCacheCollection.CoreCardsUnlockedPerClass[entityDef.GetClass()].Add(entityDef.GetCardId());
				}
			}
		}
		Action[] array = m_initialCollectionReceivedListeners.ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			array[j]();
		}
		if (list.Count > 0)
		{
			string text = string.Join(", ", list.ToArray());
			Error.AddDevWarning("Card Errors", "CollectionManager.OnInitialCollectionRecieved: Cards with the following dbIds could not be found:\n{0}", text);
		}
		BuildCoreCounterpartMap();
		return netCacheCollection;
	}

	private void OnCardSale()
	{
		Network.CardSaleResult cardSaleResult = Network.Get().GetCardSaleResult();
		bool flag = false;
		switch (cardSaleResult.Action)
		{
		case Network.CardSaleResult.SaleResult.CARD_WAS_SOLD:
			CraftingManager.Get().OnCardDisenchanted(cardSaleResult);
			flag = true;
			break;
		case Network.CardSaleResult.SaleResult.CARD_WAS_BOUGHT:
			CraftingManager.Get().OnCardCreated(cardSaleResult);
			flag = true;
			break;
		case Network.CardSaleResult.SaleResult.SOULBOUND:
			CraftingManager.Get().OnCardDisenchantSoulboundError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.FAILED_WRONG_SELL_PRICE:
			CraftingManager.Get().OnCardValueChangedError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.FAILED_WRONG_BUY_PRICE:
			CraftingManager.Get().OnCardValueChangedError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.FAILED_NO_PERMISSION:
			CraftingManager.Get().OnCardPermissionError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.FAILED_EVENT_NOT_ACTIVE:
			CraftingManager.Get().OnCardCraftingEventNotActiveError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.GENERIC_FAILURE:
			CraftingManager.Get().OnCardGenericError(cardSaleResult);
			flag = false;
			break;
		case Network.CardSaleResult.SaleResult.COUNT_MISMATCH:
			CraftingManager.Get().OnCardCountError(cardSaleResult);
			flag = false;
			break;
		default:
			CraftingManager.Get().OnCardUnknownError(cardSaleResult);
			flag = false;
			break;
		}
		string text = $"CollectionManager.OnCardSale {cardSaleResult.Action} for card {cardSaleResult.AssetName} (asset {cardSaleResult.AssetID}) premium {cardSaleResult.Premium}";
		if (!flag)
		{
			Debug.LogWarning(text);
			return;
		}
		Log.Crafting.Print(text);
		OnCollectionChanged();
	}

	private void OnMassDisenchantResponse()
	{
		Network.MassDisenchantResponse massDisenchantResponse = Network.Get().GetMassDisenchantResponse();
		if (massDisenchantResponse.Amount == 0)
		{
			Debug.LogError("CollectionManager.OnMassDisenchantResponse(): Amount is 0. This means the backend failed to mass disenchant correctly.");
			return;
		}
		OnMassDisenchant[] array = m_massDisenchantListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](massDisenchantResponse.Amount);
		}
	}

	private void OnSetFavoriteHeroResponse()
	{
		Network.SetFavoriteHeroResponse setFavoriteHeroResponse = Network.Get().GetSetFavoriteHeroResponse();
		if (setFavoriteHeroResponse.Success && TAG_CLASS.NEUTRAL != setFavoriteHeroResponse.HeroClass && setFavoriteHeroResponse.Hero != null)
		{
			NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
			if (netObject != null)
			{
				netObject.FavoriteHeroes[setFavoriteHeroResponse.HeroClass] = setFavoriteHeroResponse.Hero;
				Log.CollectionManager.Print("CollectionManager.OnSetFavoriteHeroResponse: favorite hero for class {0} updated to {1}", setFavoriteHeroResponse.HeroClass, setFavoriteHeroResponse.Hero);
			}
			UpdateFavoriteHero(setFavoriteHeroResponse.HeroClass, setFavoriteHeroResponse.Hero.Name, setFavoriteHeroResponse.Hero.Premium);
		}
	}

	public void UpdateFavoriteHero(TAG_CLASS heroClass, string heroCardId, TAG_PREMIUM premium)
	{
		if (NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			foreach (NetCache.DeckHeader deck2 in NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks)
			{
				if (deck2.HeroOverridden)
				{
					continue;
				}
				EntityDef entityDef = DefLoader.Get().GetEntityDef(deck2.Hero);
				if (entityDef != null && heroClass == entityDef.GetClass())
				{
					deck2.Hero = heroCardId;
					deck2.HeroPremium = premium;
					CollectionDeck deck = GetDeck(deck2.ID);
					if (deck != null)
					{
						deck.HeroCardID = heroCardId;
						deck.HeroPremium = premium;
					}
					CollectionDeck baseDeck = GetBaseDeck(deck2.ID);
					if (baseDeck != null)
					{
						baseDeck.HeroCardID = heroCardId;
						baseDeck.HeroPremium = premium;
					}
				}
			}
		}
		else
		{
			Log.CollectionManager.PrintWarning("Received Favorite Heroes without NetCacheDecks being ready!");
		}
		if (m_favoriteHeroChangedListeners.Count > 0)
		{
			NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition();
			cardDefinition.Name = heroCardId;
			cardDefinition.Premium = premium;
			FavoriteHeroChangedListener[] array = m_favoriteHeroChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(heroClass, cardDefinition);
			}
		}
	}

	private void OnPVPDRSessionInfoResponse()
	{
		m_currentPVPDRDeckId = 0L;
		PVPDRSessionInfoResponse pVPDRSessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pVPDRSessionInfoResponse.HasSession)
		{
			m_currentPVPDRDeckId = pVPDRSessionInfoResponse.Session.DeckId;
		}
	}

	public void NetCache_OnDecksReceived()
	{
		foreach (NetCache.DeckHeader deck in NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks)
		{
			if (deck.Type == DeckType.NORMAL_DECK && GetDeck(deck.ID) == null && DefLoader.Get().GetEntityDef(deck.Hero) != null)
			{
				AddDeck(deck, updateNetCache: false);
			}
		}
		for (int num = m_onNetCacheDecksProcessed.Count - 1; num >= 0; num--)
		{
			m_onNetCacheDecksProcessed[num]();
		}
	}

	public void AddOnNetCacheDecksProcessedListener(Action a)
	{
		m_onNetCacheDecksProcessed.Add(a);
	}

	public void RemoveOnNetCacheDecksProcessedListener(Action a)
	{
		m_onNetCacheDecksProcessed.Remove(a);
	}

	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject == null)
		{
			Debug.LogWarning($"CollectionManager.OnFavoriteCardBackChanged({newFavoriteCardBackID}): trying to access NetCacheDecks before it's been loaded");
			return;
		}
		foreach (NetCache.DeckHeader deck2 in netObject.Decks)
		{
			if (!deck2.CardBackOverridden)
			{
				deck2.CardBack = newFavoriteCardBackID;
				CollectionDeck deck = GetDeck(deck2.ID);
				if (deck != null)
				{
					deck.CardBackID = deck2.CardBack;
				}
				CollectionDeck baseDeck = GetBaseDeck(deck2.ID);
				if (baseDeck != null)
				{
					baseDeck.CardBackID = deck2.CardBack;
				}
			}
		}
	}

	public void OnInitialClientStateDeckContents(NetCache.NetCacheDecks netCacheDecks, List<DeckContents> deckContents)
	{
		if (deckContents == null)
		{
			return;
		}
		foreach (NetCache.DeckHeader deck in netCacheDecks.Decks)
		{
			if (deck.Type != DeckType.PRECON_DECK)
			{
				AddDeck(deck, updateNetCache: false);
			}
		}
		UpdateFromDeckContents(deckContents);
	}

	private void OnGetDeckContentsResponse()
	{
		GetDeckContentsResponse deckContentsResponse = Network.Get().GetDeckContentsResponse();
		UpdateFromDeckContents(deckContentsResponse.Decks);
	}

	public void UpdateFromDeckContents(List<DeckContents> deckContents)
	{
		if (deckContents == null)
		{
			Log.CollectionManager.PrintError("Could not update CollectionManager from Deck Contents. Deck Contents was null");
			return;
		}
		foreach (DeckContents deckContent in deckContents)
		{
			if (deckContent == null)
			{
				Log.CollectionManager.PrintError("UpdateFromDeckContents: deckContents contained a null deckContent.");
				continue;
			}
			Network.DeckContents netDeck = Network.DeckContents.FromPacket(deckContent);
			if (m_pendingRequestDeckContents != null)
			{
				m_pendingRequestDeckContents.Remove(netDeck.Deck);
			}
			CollectionDeck value = null;
			if (m_decks != null)
			{
				m_decks.TryGetValue(netDeck.Deck, out value);
			}
			else
			{
				Log.CollectionManager.PrintError("UpdateFromDeckContents: m_decks is null!");
			}
			CollectionDeck value2 = null;
			if (m_baseDecks != null)
			{
				m_baseDecks.TryGetValue(netDeck.Deck, out value2);
			}
			else
			{
				Log.CollectionManager.PrintError("UpdateFromDeckContents: m_baseDecks is null!");
			}
			bool flag = true;
			if (value != null && IsInEditMode() && GetEditedDeck().ID == value.ID)
			{
				flag = false;
			}
			if (value == null || value2 == null)
			{
				if (m_preconDecks == null || !m_preconDecks.Any((KeyValuePair<TAG_CLASS, PreconDeck> kv) => kv.Value.ID == netDeck.Deck))
				{
					Debug.LogErrorFormat("Got contents for an unknown deck or baseDeck: deckId={0}", netDeck.Deck);
				}
			}
			else if (value != null && value2 != null)
			{
				if (flag)
				{
					value.ClearSlotContents();
				}
				value2.ClearSlotContents();
				foreach (Network.CardUserData card in netDeck.Cards)
				{
					string text = GameUtils.TranslateDbIdToCardId(card.DbId);
					if (text == null)
					{
						Debug.LogError($"CollectionManager.OnDeck(): Could not find card with asset ID {card.DbId} in our card manifest");
						continue;
					}
					if (flag)
					{
						value.AddCard_IgnoreDeckRules(text, card.Premium, card.Count);
					}
					value2.AddCard_IgnoreDeckRules(text, card.Premium, card.Count);
				}
				value.MarkNetworkContentsLoaded();
			}
			FireDeckContentsEvent(netDeck.Deck);
		}
		foreach (CollectionDeck value3 in GetDecks().Values)
		{
			if (!value3.NetworkContentsLoaded())
			{
				return;
			}
		}
		LogAllDeckStringsInCollection();
		if (m_pendingRequestDeckContents != null)
		{
			float now = Time.realtimeSinceStartup;
			long[] array = (from kv in m_pendingRequestDeckContents
				where now - kv.Value > 10f
				select kv.Key).ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				m_pendingRequestDeckContents.Remove(array[i]);
			}
		}
		if (m_pendingRequestDeckContents == null || m_pendingRequestDeckContents.Count == 0)
		{
			FireAllDeckContentsEvent();
		}
	}

	private void OnDBAction()
	{
		Network.DBAction response = Network.Get().GetDeckResponse();
		Log.CollectionManager.Print($"MetaData:{response.MetaData} DBAction:{response.Action} Result:{response.Result}");
		bool flag = false;
		bool flag2 = false;
		switch (response.Action)
		{
		case Network.DBAction.ActionType.CREATE_DECK:
			if (response.Result != Network.DBAction.ResultType.SUCCESS && CollectionDeckTray.Get() != null)
			{
				CollectionDeckTray.Get().GetDecksContent().CreateNewDeckCancelled();
			}
			break;
		case Network.DBAction.ActionType.SET_DECK:
			flag2 = true;
			if (m_decksToRequestContentsAfterDeckSetDataResonse.Contains(response.MetaData))
			{
				Network.Get().RequestDeckContents(response.MetaData);
				m_decksToRequestContentsAfterDeckSetDataResonse.Remove(response.MetaData);
			}
			if (m_timeOfLastPlayerDeckSave.HasValue)
			{
				DateTime now = DateTime.Now;
				DateTime? timeOfLastPlayerDeckSave = m_timeOfLastPlayerDeckSave;
				double totalSeconds = (now - timeOfLastPlayerDeckSave).Value.TotalSeconds;
				TelemetryManager.Client().SendDeckUpdateResponseInfo((float)totalSeconds);
				SetTimeOfLastPlayerDeckSave(null);
			}
			if (m_pendingDeckEditList != null && m_pendingDeckEditList.Any())
			{
				m_pendingDeckEditList.RemoveAll((PendingDeckEditData d) => d.m_deckId == response.MetaData);
			}
			break;
		case Network.DBAction.ActionType.RENAME_DECK:
			flag = true;
			if (m_pendingDeckRenameList != null && m_pendingDeckRenameList.Any())
			{
				m_pendingDeckRenameList.RemoveAll((PendingDeckRenameData d) => d.m_deckId == response.MetaData);
			}
			break;
		}
		if (!(flag || flag2))
		{
			return;
		}
		long deckID = response.MetaData;
		CollectionDeck deck = GetDeck(deckID);
		CollectionDeck baseDeck = GetBaseDeck(deckID);
		if (deck == null)
		{
			return;
		}
		if (response.Result == Network.DBAction.ResultType.SUCCESS)
		{
			Log.CollectionManager.Print(string.Format("CollectionManager.OnDBAction(): overwriting baseDeck with {0} updated deck ({1}:{2})", deck.IsValidForRuleset ? "valid" : "INVALID", deck.ID, deck.Name));
			baseDeck.CopyFrom(deck);
			NetCache.DeckHeader deckHeader2 = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Find((NetCache.DeckHeader deckHeader) => deckHeader.ID == deckID);
			if (deckHeader2 != null)
			{
				deckHeader2.HeroOverridden = deck.HeroOverridden;
				deckHeader2.CardBackOverridden = deck.CardBackOverridden;
				deckHeader2.SeasonId = deck.SeasonId;
				deckHeader2.BrawlLibraryItemId = deck.BrawlLibraryItemId;
				deckHeader2.NeedsName = deck.NeedsName;
				deckHeader2.FormatType = deck.FormatType;
				deckHeader2.LastModified = DateTime.Now;
			}
		}
		else
		{
			Log.CollectionManager.Print($"CollectionManager.OnDBAction(): overwriting deck that failed to update with base deck ({baseDeck.ID}:{baseDeck.Name})");
			deck.CopyFrom(baseDeck);
		}
		if (flag)
		{
			deck.OnNameChangeComplete();
		}
		if (flag2)
		{
			deck.OnContentChangesComplete();
		}
	}

	private void OnDeckCreatedNetworkResponse()
	{
		int? requestId;
		NetCache.DeckHeader createdDeck = Network.Get().GetCreatedDeck(out requestId);
		OnDeckCreated(createdDeck, requestId);
		List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
		OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
	}

	private void OnDeckCreated(NetCache.DeckHeader deck, int? requestId)
	{
		Log.CollectionManager.Print($"DeckCreated:{deck.Name} ID:{deck.ID} Hero:{deck.Hero}");
		m_pendingDeckCreate = null;
		AddDeck(deck).MarkNetworkContentsLoaded();
		if (requestId.HasValue)
		{
			if (!m_inTransitDeckCreateRequests.Contains(requestId.Value))
			{
				return;
			}
			m_inTransitDeckCreateRequests.Remove(requestId.Value);
		}
		DelOnDeckCreated[] array = m_deckCreatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](deck.ID);
		}
	}

	private void OnDeckDeleted()
	{
		OnDeckDeleted(Network.Get().GetDeletedDeckID());
	}

	private void OnDeckDeleted(long deckId)
	{
		Log.CollectionManager.Print("CollectionManager.OnDeckDeleted");
		Log.CollectionManager.Print($"DeckDeleted:{deckId}");
		CollectionDeck collectionDeck = RemoveDeck(deckId);
		if (m_pendingDeckDeleteList != null && m_pendingDeckDeleteList.Any())
		{
			m_pendingDeckDeleteList.RemoveAll((PendingDeckDeleteData d) => d.m_deckId == deckId);
		}
		if (CollectionDeckTray.Get() == null)
		{
			return;
		}
		CollectionDeck editedDeck = GetEditedDeck();
		if (IsInEditMode() && editedDeck != null && editedDeck.ID == deckId)
		{
			Navigation.Pop();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB);
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_HEADER"),
				m_text = GameStrings.Get("GLUE_OFFLINE_DECK_DELETED_REMOTELY_ERROR_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_showAlertIcon = true
			};
			DialogManager.Get().ShowPopup(info);
		}
		if (collectionDeck != null)
		{
			DelOnDeckDeleted[] array = m_deckDeletedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](collectionDeck);
			}
		}
	}

	public void OnDeckDeletedWhileOffline(long deckId)
	{
		OnDeckDeleted(deckId);
	}

	public void AddPendingDeckDelete(long deckId)
	{
		if (m_pendingDeckDeleteList == null)
		{
			m_pendingDeckDeleteList = new List<PendingDeckDeleteData>();
		}
		m_pendingDeckDeleteList.Add(new PendingDeckDeleteData
		{
			m_deckId = deckId
		});
	}

	public void AddPendingDeckEdit(long deckId)
	{
		if (m_pendingDeckEditList == null)
		{
			m_pendingDeckEditList = new List<PendingDeckEditData>();
		}
		m_pendingDeckEditList.Add(new PendingDeckEditData
		{
			m_deckId = deckId
		});
	}

	public void AddPendingDeckRename(long deckId, string name)
	{
		if (m_pendingDeckRenameList == null)
		{
			m_pendingDeckRenameList = new List<PendingDeckRenameData>();
		}
		m_pendingDeckRenameList.Add(new PendingDeckRenameData
		{
			m_deckId = deckId,
			m_name = name
		});
	}

	private void OnDeckRenamed()
	{
		Network.DeckName renamedDeck = Network.Get().GetRenamedDeck();
		long deck = renamedDeck.Deck;
		string name = renamedDeck.Name;
		OnDeckRenamed(deck, name);
	}

	private void OnDeckRenamed(long deckId, string newName)
	{
		Log.CollectionManager.Print($"OnDeckRenamed {deckId}");
		CollectionDeck baseDeck = GetBaseDeck(deckId);
		CollectionDeck deck = GetDeck(deckId);
		if (baseDeck == null || deck == null)
		{
			Debug.LogWarning($"For deck with ID {deckId}, unable to handle OnDeckRenamed event to new name {newName} due to null deck or null baseDeck");
			return;
		}
		baseDeck.Name = newName;
		deck.Name = newName;
		NetCache.DeckHeader deckHeader2 = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Find((NetCache.DeckHeader deckHeader) => deckHeader.ID == deckId);
		if (deckHeader2 != null)
		{
			deckHeader2.Name = newName;
			deckHeader2.LastModified = DateTime.Now;
		}
		OfflineDataCache.RenameDeck(deckId, newName);
		deck.OnNameChangeComplete();
	}

	public static void Init()
	{
		if (s_instance == null)
		{
			s_instance = new CollectionManager();
			HearthstoneApplication.Get().WillReset += s_instance.WillReset;
			NetCache.Get().FavoriteCardBackChanged += s_instance.OnFavoriteCardBackChanged;
			s_instance.InitImpl();
		}
	}

	public static CollectionManager Get()
	{
		return s_instance;
	}

	public CollectibleDisplay GetCollectibleDisplay()
	{
		return m_collectibleDisplay;
	}

	public bool IsFullyLoaded()
	{
		return m_collectionLoaded;
	}

	public void RegisterCollectionNetHandlers()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(BoughtSoldCard.PacketID.ID, OnCardSale);
		network.RegisterNetHandler(MassDisenchantResponse.PacketID.ID, OnMassDisenchantResponse);
		network.RegisterNetHandler(SetFavoriteHeroResponse.PacketID.ID, OnSetFavoriteHeroResponse);
		network.RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
	}

	public void RemoveCollectionNetHandlers()
	{
		Network network = Network.Get();
		network.RemoveNetHandler(BoughtSoldCard.PacketID.ID, OnCardSale);
		network.RemoveNetHandler(MassDisenchantResponse.PacketID.ID, OnMassDisenchantResponse);
		network.RemoveNetHandler(SetFavoriteHeroResponse.PacketID.ID, OnSetFavoriteHeroResponse);
		network.RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, OnPVPDRSessionInfoResponse);
	}

	public bool HasVisitedCollection()
	{
		return m_hasVisitedCollection;
	}

	public void SetHasVisitedCollection(bool enable)
	{
		m_hasVisitedCollection = enable;
	}

	public bool IsWaitingForBoxTransition()
	{
		return m_waitingForBoxTransition;
	}

	public void NotifyOfBoxTransitionStart()
	{
		Box.Get().AddTransitionFinishedListener(OnBoxTransitionFinished);
		m_waitingForBoxTransition = true;
	}

	public void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(OnBoxTransitionFinished);
		m_waitingForBoxTransition = false;
	}

	public void SetCollectibleDisplay(CollectibleDisplay display)
	{
		m_collectibleDisplay = display;
	}

	public void AddCardReward(CardRewardData cardReward, bool markAsNew)
	{
		List<CardRewardData> list = new List<CardRewardData>();
		list.Add(cardReward);
		AddCardRewards(list, markAsNew);
	}

	public void AddCardRewards(List<CardRewardData> cardRewards, bool markAsNew)
	{
		List<string> list = new List<string>();
		List<TAG_PREMIUM> list2 = new List<TAG_PREMIUM>();
		List<DateTime> list3 = new List<DateTime>();
		List<int> list4 = new List<int>();
		DateTime now = DateTime.Now;
		foreach (CardRewardData cardReward in cardRewards)
		{
			list.Add(cardReward.CardID);
			list2.Add(cardReward.Premium);
			list3.Add(now);
			list4.Add(cardReward.Count);
		}
		InsertNewCollectionCards(list, list2, list3, list4, !markAsNew);
		AchieveManager.Get().ValidateAchievesNow();
		DelOnCardRewardsInserted[] array = m_cardRewardListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](list, list2);
		}
	}

	public float CollectionLastModifiedTime()
	{
		return m_collectionLastModifiedTime;
	}

	public int EntityDefSortComparison(EntityDef entityDef1, EntityDef entityDef2)
	{
		int num = (entityDef1.HasTag(GAME_TAG.DECK_LIST_SORT_ORDER) ? entityDef1.GetTag(GAME_TAG.DECK_LIST_SORT_ORDER) : int.MaxValue);
		int num2 = (entityDef2.HasTag(GAME_TAG.DECK_LIST_SORT_ORDER) ? entityDef2.GetTag(GAME_TAG.DECK_LIST_SORT_ORDER) : int.MaxValue);
		int num3 = num - num2;
		if (num3 != 0)
		{
			return num3;
		}
		int cost = entityDef1.GetCost();
		int cost2 = entityDef2.GetCost();
		int num4 = cost - cost2;
		if (num4 != 0)
		{
			return num4;
		}
		string name = entityDef1.GetName();
		string name2 = entityDef2.GetName();
		int num5 = string.Compare(name, name2, ignoreCase: true);
		if (num5 != 0)
		{
			return num5;
		}
		int cardTypeSortOrder = GetCardTypeSortOrder(entityDef1);
		int cardTypeSortOrder2 = GetCardTypeSortOrder(entityDef2);
		return cardTypeSortOrder - cardTypeSortOrder2;
	}

	public int GetCardTypeSortOrder(EntityDef entityDef)
	{
		return entityDef.GetCardType() switch
		{
			TAG_CARDTYPE.WEAPON => 1, 
			TAG_CARDTYPE.SPELL => 2, 
			TAG_CARDTYPE.MINION => 3, 
			_ => 0, 
		};
	}

	private bool IsSetRotatedWithCache(TAG_CARD_SET set, Map<TAG_CARD_SET, bool> cache)
	{
		if (!cache.TryGetValue(set, out var value))
		{
			value = (cache[set] = GameUtils.IsSetRotated(set));
		}
		return value;
	}

	private void BuildCoreCounterpartMap()
	{
		m_coreCounterpartCardMap.Clear();
		foreach (CollectibleCard collectibleCard in m_collectibleCards)
		{
			if (collectibleCard.Set == TAG_CARD_SET.CORE && !m_coreCounterpartCardMap.ContainsKey(collectibleCard.CardDbId))
			{
				int tag = collectibleCard.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID);
				if (tag != 0 && GetCard(GameUtils.TranslateDbIdToCardId(tag), collectibleCard.PremiumType) != null)
				{
					m_coreCounterpartCardMap.Add(collectibleCard.CardDbId, tag);
				}
			}
		}
	}

	public FindCardsResult FindCards(string searchString = null, List<CollectibleCardFilter.FilterMask> filterMasks = null, int? manaCost = null, TAG_CARD_SET[] theseCardSets = null, TAG_CLASS[] theseClassTypes = null, TAG_CARDTYPE[] theseCardTypes = null, TAG_RARITY? rarity = null, TAG_RACE? race = null, bool? isHero = null, int? minOwned = null, bool? notSeen = null, bool? isCraftable = null, CollectibleCardFilter.FilterMask? craftableFilterPremiums = null, CollectibleCardFilterFunc[] priorityFilters = null, DeckRuleset deckRuleset = null, bool returnAfterFirstResult = false, HashSet<string> leagueBannedCardsSubset = null, List<int> specificCards = null, bool? filterCoreCounterpartCards = null)
	{
		FindCardsResult results = new FindCardsResult();
		Map<int, int> startsWithMatchNames = new Map<int, int>();
		CollectibleCardFilter.FilterMask searchFilterMask = CollectibleCardFilter.FilterMask.PREMIUM_ALL;
		m_filterCardSet.Clear();
		m_filterCardClass.Clear();
		m_filterCardType.Clear();
		m_filterIsSetRotatedCache.Clear();
		List<CollectibleCardFilterFunc> filterFuncs = new List<CollectibleCardFilterFunc>();
		if (priorityFilters != null)
		{
			filterFuncs.AddRange(priorityFilters);
		}
		CollectibleCardFilterFunc item = delegate(CollectibleCard card)
		{
			if (!card.IsCraftable)
			{
				return false;
			}
			return (GetOwnedCardCountByFilterMask(card.CardId, searchFilterMask) < card.DefaultMaxCopiesPerDeck) ? true : false;
		};
		CollectibleCardFilterFunc item2 = delegate(CollectibleCard card)
		{
			if (!card.IsCraftable)
			{
				return false;
			}
			return (GetOwnedCardCountByFilterMask(card.CardId, searchFilterMask) > card.DefaultMaxCopiesPerDeck) ? true : false;
		};
		CollectibleCardFilterFunc item3 = delegate(CollectibleCard card)
		{
			CollectibleCardFilter.FilterMask filterMask2 = CollectibleCardFilter.FilterMaskFromPremiumType(card.PremiumType);
			filterMask2 = ((card.OwnedCount <= 0) ? (filterMask2 | CollectibleCardFilter.FilterMask.UNOWNED) : (filterMask2 | CollectibleCardFilter.FilterMask.OWNED));
			for (int k = 0; k < filterMasks.Count; k++)
			{
				if ((filterMasks[k] & filterMask2) == filterMask2)
				{
					return true;
				}
			}
			return false;
		};
		bool flag = !string.IsNullOrEmpty(searchString);
		if (filterMasks != null)
		{
			filterFuncs.Add(item3);
		}
		if (flag)
		{
			string value = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_MISSING");
			string value2 = GameStrings.Get("GLUE_COLLECTION_MANAGER_SEARCH_EXTRA");
			string[] source = searchString.ToLower().Split(' ');
			if (filterMasks != null)
			{
				bool flag2 = false;
				if (source.Contains(value))
				{
					filterFuncs.Add(item);
					flag2 = true;
				}
				else if (source.Contains(value2))
				{
					filterFuncs.Add(item2);
					flag2 = true;
				}
				if (flag2)
				{
					foreach (CollectibleCardFilter.FilterMask filterMask3 in filterMasks)
					{
						if ((filterMask3 & CollectibleCardFilter.FilterMask.OWNED) != 0)
						{
							if ((filterMask3 & CollectibleCardFilter.FilterMask.PREMIUM_NORMAL) != 0)
							{
								searchFilterMask = CollectibleCardFilter.FilterMask.PREMIUM_ALL;
							}
							else
							{
								searchFilterMask = filterMask3;
							}
							break;
						}
					}
				}
			}
			filterFuncs.AddRange(CollectibleCardFilter.FiltersFromSearchString(searchString));
		}
		if (theseClassTypes != null && theseClassTypes.Length != 0)
		{
			filterFuncs.Add((CollectibleCard card) => theseClassTypes.Contains(card.Class));
		}
		if (theseCardTypes != null && theseCardTypes.Length != 0)
		{
			foreach (TAG_CARDTYPE item4 in theseCardTypes)
			{
				m_filterCardType.Add(item4);
			}
			filterFuncs.Add((CollectibleCard card) => m_filterCardType.Contains(card.CardType));
		}
		if (rarity.HasValue)
		{
			filterFuncs.Add((CollectibleCard card) => card.Rarity == rarity.Value);
		}
		if (race.HasValue)
		{
			filterFuncs.Add((CollectibleCard card) => card.Race == race.Value);
		}
		if (isHero.HasValue)
		{
			filterFuncs.Add((CollectibleCard card) => card.IsHeroSkin == isHero.Value);
		}
		if (notSeen.HasValue)
		{
			if (notSeen.Value)
			{
				filterFuncs.Add((CollectibleCard card) => card.SeenCount < card.OwnedCount);
			}
			else
			{
				filterFuncs.Add((CollectibleCard card) => card.SeenCount == card.OwnedCount);
			}
		}
		if (isCraftable.HasValue)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (craftableFilterPremiums.HasValue)
				{
					CollectibleCardFilter.FilterMask filterMask = CollectibleCardFilter.FilterMaskFromPremiumType(card.PremiumType);
					if ((craftableFilterPremiums.Value & filterMask) != filterMask)
					{
						return true;
					}
				}
				return card.IsCraftable == isCraftable.Value;
			});
		}
		if (flag)
		{
			string lowerSearchString = searchString.ToLower();
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				string lowerCardName = card.Name.ToLower();
				if (lowerCardName.Split(' ').Any(delegate(string s)
				{
					if (s.StartsWith(lowerSearchString))
					{
						return true;
					}
					string text = UberText.RemoveMarkupAndCollapseWhitespaces(lowerCardName).Trim().ToLower();
					string text2 = CollectibleCardFilter.ConvertEuropeanCharacters(text);
					string text3 = CollectibleCardFilter.RemoveDiacritics(text);
					return text.StartsWith(lowerSearchString, StringComparison.OrdinalIgnoreCase) || text2.StartsWith(lowerSearchString, StringComparison.OrdinalIgnoreCase) || text3.StartsWith(lowerSearchString, StringComparison.OrdinalIgnoreCase);
				}))
				{
					if (!startsWithMatchNames.ContainsKey(card.CardDbId))
					{
						startsWithMatchNames[card.CardDbId] = 0;
					}
					startsWithMatchNames[card.CardDbId] += card.OwnedCount;
				}
				return true;
			});
		}
		if (manaCost.HasValue)
		{
			int minManaCost = manaCost.Value;
			int maxManaCost = manaCost.Value;
			if (maxManaCost >= 7)
			{
				maxManaCost = int.MaxValue;
			}
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool num4 = card.ManaCost >= minManaCost && card.ManaCost <= maxManaCost;
				if (!num4 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					results.m_resultsWithoutManaFilterExist = true;
				}
				return num4;
			});
		}
		if (theseCardSets != null && theseCardSets.Length != 0)
		{
			foreach (TAG_CARD_SET item5 in theseCardSets)
			{
				m_filterCardSet.Add(item5);
			}
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (!IsSetRotatedWithCache(card.Set, m_filterIsSetRotatedCache))
				{
					bool num3 = m_filterCardSet.Contains(card.Set);
					if (!num3 && startsWithMatchNames.ContainsKey(card.CardDbId))
					{
						results.m_resultsWithoutSetFilterExist = true;
					}
					return num3;
				}
				return true;
			});
		}
		if (minOwned.HasValue)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool num2 = card.OwnedCount >= minOwned.Value;
				if (!num2 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					startsWithMatchNames[card.CardDbId] -= card.OwnedCount;
					if (startsWithMatchNames[card.CardDbId] < 1)
					{
						results.m_resultsUnownedExist = true;
					}
				}
				return num2;
			});
		}
		if (theseCardSets != null && theseCardSets.Length != 0)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (IsSetRotatedWithCache(card.Set, m_filterIsSetRotatedCache))
				{
					bool num = m_filterCardSet.Contains(card.Set);
					if (!num && startsWithMatchNames.ContainsKey(card.CardDbId))
					{
						results.m_resultsInWildExist = true;
					}
					return num;
				}
				return true;
			});
		}
		if (deckRuleset != null)
		{
			CollectionDeck deck = Get().GetEditedDeck();
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool flag3 = deckRuleset.Filter(card.GetEntityDef(), deck);
				if (!flag3 && card.OwnedCount > 0 && deckRuleset.FilterFailsOnShowInvalidRule(card.GetEntityDef(), deck))
				{
					flag3 = true;
				}
				return flag3;
			});
		}
		if (leagueBannedCardsSubset != null)
		{
			filterFuncs.Add((CollectibleCard card) => !leagueBannedCardsSubset.Contains(card.GetEntityDef().GetCardId()));
		}
		if (specificCards != null)
		{
			filterFuncs.Add((CollectibleCard card) => specificCards.Contains(card.CardDbId));
		}
		Predicate<CollectibleCard> match = delegate(CollectibleCard card)
		{
			for (int j = 0; j < filterFuncs.Count; j++)
			{
				if (!filterFuncs[j](card))
				{
					return false;
				}
			}
			return true;
		};
		if (returnAfterFirstResult)
		{
			CollectibleCard collectibleCard = m_collectibleCards.Find(match);
			if (collectibleCard != null)
			{
				results.m_cards.Add(collectibleCard);
			}
		}
		else
		{
			results.m_cards = m_collectibleCards.FindAll(match);
		}
		if (filterCoreCounterpartCards.HasValue && filterCoreCounterpartCards == true)
		{
			FilterOutCardWithCoreCounterparts(results.m_cards);
		}
		return results;
	}

	public FindCardsResult FindOrderedCards(string searchString = null, List<CollectibleCardFilter.FilterMask> filterMasks = null, int? manaCost = null, TAG_CARD_SET[] theseCardSets = null, TAG_CLASS[] theseClassTypes = null, TAG_CARDTYPE[] theseCardTypes = null, TAG_RARITY? rarity = null, TAG_RACE? race = null, bool? isHero = null, int? minOwned = null, bool? notSeen = null, bool? isCraftable = null, CollectibleCardFilter.FilterMask? craftableFilterPremiums = null, CollectibleCardFilterFunc[] priorityFilters = null, DeckRuleset deckRuleset = null, bool returnAfterFirstResult = false, HashSet<string> leagueBannedCardsSubset = null, List<int> specificCards = null, bool? filterCounterpartCards = null)
	{
		FindCardsResult findCardsResult = FindCards(searchString, filterMasks, manaCost, theseCardSets, theseClassTypes, theseCardTypes, rarity, race, isHero, minOwned, notSeen, isCraftable, craftableFilterPremiums, priorityFilters, deckRuleset, returnAfterFirstResult, leagueBannedCardsSubset, specificCards, filterCounterpartCards);
		findCardsResult.m_cards = (from c in findCardsResult.m_cards
			orderby c.ManaCost, c.Name, c.PremiumType
			select c).ToList();
		return findCardsResult;
	}

	public bool OwnsCoreVersion(int originalCardId)
	{
		return m_coreCounterpartCardMap.ContainsValue(originalCardId);
	}

	public void FilterOutCardWithCoreCounterparts(List<CollectibleCard> collectibleCards)
	{
		HashSet<CollectibleCardIndex> counterpartCardsToRemove = new HashSet<CollectibleCardIndex>();
		foreach (CollectibleCard collectibleCard in collectibleCards)
		{
			if (collectibleCard.Set != TAG_CARD_SET.CORE)
			{
				continue;
			}
			int value = 0;
			if (!m_coreCounterpartCardMap.TryGetValue(collectibleCard.CardDbId, out value))
			{
				continue;
			}
			CollectibleCard card2 = GetCard(GameUtils.TranslateDbIdToCardId(value), collectibleCard.PremiumType);
			if (card2 == null)
			{
				continue;
			}
			string text = null;
			if (collectibleCard.OwnedCount == card2.DefaultMaxCopiesPerDeck)
			{
				text = card2.CardId;
			}
			else
			{
				if (collectibleCard.OwnedCount == 1 && card2.OwnedCount == 1)
				{
					continue;
				}
				text = ((collectibleCard.OwnedCount >= card2.OwnedCount) ? card2.CardId : collectibleCard.CardId);
			}
			if (text != null)
			{
				counterpartCardsToRemove.Add(new CollectibleCardIndex(text, collectibleCard.PremiumType));
			}
		}
		collectibleCards.RemoveAll((CollectibleCard card) => counterpartCardsToRemove.Contains(new CollectibleCardIndex(card.CardId, card.PremiumType)));
	}

	public List<CollectibleCard> GetAllCards()
	{
		return m_collectibleCards;
	}

	public bool IsCardOwned(string cardId)
	{
		return GetTotalOwnedCount(cardId) > 0;
	}

	public void RegisterCollectionLoadedListener(DelOnCollectionLoaded listener)
	{
		if (!m_collectionLoadedListeners.Contains(listener))
		{
			m_collectionLoadedListeners.Add(listener);
		}
	}

	public bool RemoveCollectionLoadedListener(DelOnCollectionLoaded listener)
	{
		return m_collectionLoadedListeners.Remove(listener);
	}

	public void RegisterCollectionChangedListener(DelOnCollectionChanged listener)
	{
		if (!m_collectionChangedListeners.Contains(listener))
		{
			m_collectionChangedListeners.Add(listener);
		}
	}

	public bool RemoveCollectionChangedListener(DelOnCollectionChanged listener)
	{
		return m_collectionChangedListeners.Remove(listener);
	}

	public void RegisterDeckCreatedListener(DelOnDeckCreated listener)
	{
		if (!m_deckCreatedListeners.Contains(listener))
		{
			m_deckCreatedListeners.Add(listener);
		}
	}

	public bool RemoveDeckCreatedListener(DelOnDeckCreated listener)
	{
		return m_deckCreatedListeners.Remove(listener);
	}

	public void RegisterDeckDeletedListener(DelOnDeckDeleted listener)
	{
		if (!m_deckDeletedListeners.Contains(listener))
		{
			m_deckDeletedListeners.Add(listener);
		}
	}

	public bool RemoveDeckDeletedListener(DelOnDeckDeleted listener)
	{
		return m_deckDeletedListeners.Remove(listener);
	}

	public void RegisterDeckContentsListener(DelOnDeckContents listener)
	{
		if (!m_deckContentsListeners.Contains(listener))
		{
			m_deckContentsListeners.Add(listener);
		}
	}

	public bool RemoveDeckContentsListener(DelOnDeckContents listener)
	{
		return m_deckContentsListeners.Remove(listener);
	}

	public void RegisterNewCardSeenListener(DelOnNewCardSeen listener)
	{
		if (!m_newCardSeenListeners.Contains(listener))
		{
			m_newCardSeenListeners.Add(listener);
		}
	}

	public bool RemoveNewCardSeenListener(DelOnNewCardSeen listener)
	{
		return m_newCardSeenListeners.Remove(listener);
	}

	public void RegisterCardRewardsInsertedListener(DelOnCardRewardsInserted listener)
	{
		if (!m_cardRewardListeners.Contains(listener))
		{
			m_cardRewardListeners.Add(listener);
		}
	}

	public bool RemoveCardRewardsInsertedListener(DelOnCardRewardsInserted listener)
	{
		return m_cardRewardListeners.Remove(listener);
	}

	public void RegisterMassDisenchantListener(OnMassDisenchant listener)
	{
		if (!m_massDisenchantListeners.Contains(listener))
		{
			m_massDisenchantListeners.Add(listener);
		}
	}

	public void RemoveMassDisenchantListener(OnMassDisenchant listener)
	{
		m_massDisenchantListeners.Remove(listener);
	}

	public void RegisterEditedDeckChanged(OnEditedDeckChanged listener)
	{
		m_editedDeckChangedListeners.Add(listener);
	}

	public void RemoveEditedDeckChanged(OnEditedDeckChanged listener)
	{
		m_editedDeckChangedListeners.Remove(listener);
	}

	public bool RegisterFavoriteHeroChangedListener(FavoriteHeroChangedCallback callback)
	{
		return RegisterFavoriteHeroChangedListener(callback, null);
	}

	public bool RegisterFavoriteHeroChangedListener(FavoriteHeroChangedCallback callback, object userData)
	{
		FavoriteHeroChangedListener favoriteHeroChangedListener = new FavoriteHeroChangedListener();
		favoriteHeroChangedListener.SetCallback(callback);
		favoriteHeroChangedListener.SetUserData(userData);
		if (m_favoriteHeroChangedListeners.Contains(favoriteHeroChangedListener))
		{
			return false;
		}
		m_favoriteHeroChangedListeners.Add(favoriteHeroChangedListener);
		return true;
	}

	public bool RemoveFavoriteHeroChangedListener(FavoriteHeroChangedCallback callback)
	{
		return RemoveFavoriteHeroChangedListener(callback, null);
	}

	public bool RemoveFavoriteHeroChangedListener(FavoriteHeroChangedCallback callback, object userData)
	{
		FavoriteHeroChangedListener favoriteHeroChangedListener = new FavoriteHeroChangedListener();
		favoriteHeroChangedListener.SetCallback(callback);
		favoriteHeroChangedListener.SetUserData(userData);
		return m_favoriteHeroChangedListeners.Remove(favoriteHeroChangedListener);
	}

	public bool RegisterOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemovedCallback callback)
	{
		return RegisterOnUIHeroOverrideCardRemovedListener(callback, null);
	}

	public bool RegisterOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemovedCallback callback, object userData)
	{
		OnUIHeroOverrideCardRemovedListener onUIHeroOverrideCardRemovedListener = new OnUIHeroOverrideCardRemovedListener();
		onUIHeroOverrideCardRemovedListener.SetCallback(callback);
		onUIHeroOverrideCardRemovedListener.SetUserData(userData);
		if (m_onUIHeroOverrideCardRemovedListeners.Contains(onUIHeroOverrideCardRemovedListener))
		{
			return false;
		}
		m_onUIHeroOverrideCardRemovedListeners.Add(onUIHeroOverrideCardRemovedListener);
		return true;
	}

	public bool RemoveOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemovedCallback callback)
	{
		return RemoveOnUIHeroOverrideCardRemovedListener(callback, null);
	}

	public bool RemoveOnUIHeroOverrideCardRemovedListener(OnUIHeroOverrideCardRemovedCallback callback, object userData)
	{
		OnUIHeroOverrideCardRemovedListener onUIHeroOverrideCardRemovedListener = new OnUIHeroOverrideCardRemovedListener();
		onUIHeroOverrideCardRemovedListener.SetCallback(callback);
		onUIHeroOverrideCardRemovedListener.SetUserData(userData);
		return m_onUIHeroOverrideCardRemovedListeners.Remove(onUIHeroOverrideCardRemovedListener);
	}

	public void RegisterOnInitialCollectionReceivedListener(Action callback)
	{
		if (!m_initialCollectionReceivedListeners.Contains(callback))
		{
			m_initialCollectionReceivedListeners.Add(callback);
		}
	}

	public void RemoveOnInitialCollectionReceivedListener(Action callback)
	{
		if (m_initialCollectionReceivedListeners.Contains(callback))
		{
			m_initialCollectionReceivedListeners.Remove(callback);
		}
	}

	public TAG_PREMIUM GetBestCardPremium(string cardID)
	{
		CollectibleCard value = null;
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardID, TAG_PREMIUM.DIAMOND), out value) && value.OwnedCount > 0)
		{
			return TAG_PREMIUM.DIAMOND;
		}
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardID, TAG_PREMIUM.GOLDEN), out value) && value.OwnedCount > 0)
		{
			return TAG_PREMIUM.GOLDEN;
		}
		return TAG_PREMIUM.NORMAL;
	}

	public CollectibleCard GetCard(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard value = null;
		m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardID, premium), out value);
		return value;
	}

	public List<CollectibleCard> GetHeroesIOwn(TAG_CLASS heroClass)
	{
		return FindCards(null, null, null, null, null, null, null, null, minOwned: 1, isHero: true).m_cards;
	}

	public List<CollectibleCard> GetBestHeroesIOwn(TAG_CLASS heroClass)
	{
		List<CollectibleCard> cards = FindCards(null, null, null, null, minOwned: 1, isHero: true, theseClassTypes: new TAG_CLASS[1] { heroClass }).m_cards;
		IEnumerable<CollectibleCard> enumerable = cards.Where((CollectibleCard h) => h.PremiumType == TAG_PREMIUM.GOLDEN);
		IEnumerable<CollectibleCard> enumerable2 = cards.Where((CollectibleCard h) => h.PremiumType == TAG_PREMIUM.NORMAL);
		List<CollectibleCard> list = new List<CollectibleCard>();
		foreach (CollectibleCard item in enumerable)
		{
			list.Add(item);
		}
		foreach (CollectibleCard heroCard in enumerable2)
		{
			if (list.Find((CollectibleCard e) => e.CardDbId == heroCard.CardDbId) == null)
			{
				list.Add(heroCard);
			}
		}
		return list;
	}

	public NetCache.CardDefinition GetFavoriteHero(TAG_CLASS heroClass)
	{
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject == null)
		{
			return GetFavoriteHeroFromOfflineData(heroClass);
		}
		if (!netObject.FavoriteHeroes.ContainsKey(heroClass))
		{
			return null;
		}
		return netObject.FavoriteHeroes[heroClass];
	}

	private NetCache.CardDefinition GetFavoriteHeroFromOfflineData(TAG_CLASS heroClass)
	{
		FavoriteHero favoriteHero = OfflineDataCache.GetFavoriteHeroesFromCache().FirstOrDefault((FavoriteHero h) => h.ClassId == (int)heroClass);
		if (favoriteHero == null)
		{
			return null;
		}
		return new NetCache.CardDefinition
		{
			Name = GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset),
			Premium = (TAG_PREMIUM)favoriteHero.Hero.Premium
		};
	}

	public int GetCoreCardsIOwn(TAG_CLASS cardClass)
	{
		return NetCache.Get().GetNetObject<NetCache.NetCacheCollection>()?.CoreCardsUnlockedPerClass[cardClass].Count ?? 0;
	}

	public List<CollectibleCard> GetOwnedCards()
	{
		return FindCards(null, null, null, null, null, null, null, null, null, 1).m_cards;
	}

	public void GetOwnedCardCount(string cardId, out int normal, out int golden, out int diamond)
	{
		normal = 0;
		golden = 0;
		diamond = 0;
		CollectibleCard value = null;
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.NORMAL), out value))
		{
			normal += value.OwnedCount;
		}
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.GOLDEN), out value))
		{
			golden += value.OwnedCount;
		}
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.DIAMOND), out value))
		{
			diamond += value.OwnedCount;
		}
	}

	public int GetOwnedCardCountByFilterMask(string cardId, CollectibleCardFilter.FilterMask filterMask)
	{
		int num = 0;
		CollectibleCard value = null;
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_NORMAL) != 0 && m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.NORMAL), out value))
		{
			num += value.OwnedCount;
		}
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN) != 0 && m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.GOLDEN), out value))
		{
			num += value.OwnedCount;
		}
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND) != 0 && m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardId, TAG_PREMIUM.DIAMOND), out value))
		{
			num += value.OwnedCount;
		}
		return num;
	}

	public List<TAG_CARD_SET> GetDisplayableCardSets()
	{
		return m_displayableCardSets;
	}

	public List<CollectibleCard> GetChangedCards(List<CardChange.ChangeType> changeTypes, TAG_PREMIUM premium, string featuredCardsEventTiming = null)
	{
		List<CardChangeDbfRecord> list = new List<CardChangeDbfRecord>();
		foreach (CardChangeDbfRecord record2 in GameDbf.CardChange.GetRecords())
		{
			if (changeTypes.Contains(record2.ChangeType))
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(record2.CardId);
				if (record != null && (featuredCardsEventTiming == null || !(record.FeaturedCardsEvent != featuredCardsEventTiming)) && (featuredCardsEventTiming != null || string.IsNullOrEmpty(record.FeaturedCardsEvent)))
				{
					list.Add(record2);
				}
			}
		}
		list.Sort((CardChangeDbfRecord a, CardChangeDbfRecord b) => a.SortOrder - b.SortOrder);
		List<CollectibleCard> list2 = new List<CollectibleCard>();
		foreach (CardChangeDbfRecord item in list)
		{
			CollectibleCard card = GetCard(GameUtils.TranslateDbIdToCardId(item.CardId), premium);
			if (card != null && !list2.Contains(card) && !ChangedCardMgr.Get().HasSeenCardChange(card.ChangeVersion, card.CardDbId))
			{
				list2.Add(card);
			}
		}
		return list2;
	}

	public bool IsCardInCollection(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard value = null;
		if (m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardID, premium), out value))
		{
			return value.OwnedCount > 0;
		}
		return false;
	}

	public int GetNumCopiesInCollection(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard value = null;
		m_collectibleCardIndex.TryGetValue(new CollectibleCardIndex(cardID, premium), out value);
		return value?.OwnedCount ?? 0;
	}

	public List<CollectibleCard> GetMassDisenchantCards()
	{
		List<CollectibleCard> list = new List<CollectibleCard>();
		foreach (CollectibleCard ownedCard in GetOwnedCards())
		{
			if (ownedCard.DisenchantCount > 0)
			{
				list.Add(ownedCard);
			}
		}
		return list;
	}

	public int GetCardsToDisenchantCount()
	{
		int num = 0;
		foreach (CollectibleCard massDisenchantCard in GetMassDisenchantCards())
		{
			num += massDisenchantCard.DisenchantCount;
		}
		return num;
	}

	public void MarkAllInstancesAsSeen(string cardID, TAG_PREMIUM premium)
	{
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		int num = GameUtils.TranslateCardIdToDbId(cardID);
		if (num == 0)
		{
			return;
		}
		CollectibleCard card = GetCard(cardID, premium);
		if (card == null || card.SeenCount == card.OwnedCount)
		{
			return;
		}
		Network.Get().AckCardSeenBefore(num, premium);
		card.SeenCount = card.OwnedCount;
		NetCache.CardStack cardStack = netObject.Stacks.Find((NetCache.CardStack obj) => obj.Def.Name == card.CardId && obj.Def.Premium == card.PremiumType);
		if (cardStack != null)
		{
			cardStack.NumSeen = cardStack.Count;
		}
		foreach (DelOnNewCardSeen newCardSeenListener in m_newCardSeenListeners)
		{
			newCardSeenListener(cardID, premium);
		}
	}

	public void OnCardAdded(string cardID, TAG_PREMIUM premium, int count, bool seenBefore)
	{
		InsertNewCollectionCard(cardID, premium, DateTime.Now, count, seenBefore);
		OnCollectionChanged();
	}

	public void OnCardRemoved(string cardID, TAG_PREMIUM premium, int count)
	{
		RemoveCollectionCard(cardID, premium, count);
		OnCollectionChanged();
	}

	public void OnUIHeroOverrideCardRemoved()
	{
		if (m_onUIHeroOverrideCardRemovedListeners.Count > 0)
		{
			OnUIHeroOverrideCardRemovedListener[] array = m_onUIHeroOverrideCardRemovedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	public PreconDeck GetPreconDeck(TAG_CLASS heroClass)
	{
		if (!m_preconDecks.ContainsKey(heroClass))
		{
			Log.All.PrintWarning($"CollectionManager.GetPreconDeck(): Could not retrieve precon deck for class {heroClass}");
			return null;
		}
		return m_preconDecks[heroClass];
	}

	public List<CollectionDeck> GetPreconCollectionDecks()
	{
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (PreconDeck value in m_preconDecks.Values)
		{
			list.Add(m_decks[value.ID]);
		}
		return list;
	}

	public SortedDictionary<long, CollectionDeck> GetDecks()
	{
		SortedDictionary<long, CollectionDeck> sortedDictionary = new SortedDictionary<long, CollectionDeck>();
		foreach (KeyValuePair<long, CollectionDeck> deck in m_decks)
		{
			CollectionDeck value = deck.Value;
			if (value != null && (!value.IsBrawlDeck || TavernBrawlManager.Get().IsSeasonActive(value.Type, value.SeasonId, value.BrawlLibraryItemId)))
			{
				sortedDictionary.Add(deck.Key, deck.Value);
			}
		}
		return sortedDictionary;
	}

	public List<CollectionDeck> GetDecks(DeckType deckType)
	{
		if (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			Debug.LogWarning("Attempting to get decks from CollectionManager, even though NetCacheDecks is not ready (meaning it's waiting for the decks to be updated)!");
		}
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (CollectionDeck value in m_decks.Values)
		{
			if (value.Type == deckType && (!value.IsBrawlDeck || TavernBrawlManager.Get().IsSeasonActive(value.Type, value.SeasonId, value.BrawlLibraryItemId)))
			{
				list.Add(value);
			}
		}
		list.Sort(new DeckSort());
		return list;
	}

	public List<CollectionDeck> GetDecksWithClass(TAG_CLASS classType, DeckType deckType)
	{
		List<CollectionDeck> decks = GetDecks(deckType);
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (CollectionDeck item in decks)
		{
			if (item.GetClass() == classType)
			{
				list.Add(item);
			}
		}
		return list;
	}

	public List<long> LoadDeckFromDBF(int deckID, out string deckName, out string deckDescription)
	{
		deckName = string.Empty;
		deckDescription = string.Empty;
		DeckDbfRecord record = GameDbf.Deck.GetRecord(deckID);
		if (record == null)
		{
			Debug.LogError($"Unable to find deck with ID {deckID}");
			return null;
		}
		if (record.Name == null)
		{
			Debug.LogErrorFormat("Deck with ID {0} has no name defined.", deckID);
		}
		else
		{
			deckName = record.Name.GetString();
		}
		if (record.Description != null)
		{
			deckDescription = record.Description.GetString();
		}
		List<long> list = new List<long>();
		DeckCardDbfRecord deckCardDbfRecord = GameDbf.DeckCard.GetRecord(record.TopCardId);
		while (deckCardDbfRecord != null)
		{
			int cardId = deckCardDbfRecord.CardId;
			list.Add(cardId);
			int nextCard = deckCardDbfRecord.NextCard;
			deckCardDbfRecord = ((nextCard != 0) ? GameDbf.DeckCard.GetRecord(nextCard) : null);
		}
		return list;
	}

	public CollectionDeck GetDeck(long id)
	{
		if (m_decks.TryGetValue(id, out var value))
		{
			if (value != null && value.IsBrawlDeck && !TavernBrawlManager.Get().IsSeasonActive(value.Type, value.SeasonId, value.BrawlLibraryItemId))
			{
				return null;
			}
			return value;
		}
		return null;
	}

	public CollectionDeck GetDuelsDeck()
	{
		List<CollectionDeck> decks = GetDecks(DeckType.PVPDR_DECK);
		if (decks != null)
		{
			for (int i = 0; i < decks.Count; i++)
			{
				if (decks[i].ID == m_currentPVPDRDeckId && !decks[i].IsBeingDeleted())
				{
					return decks[i];
				}
			}
		}
		return null;
	}

	public bool AreAllDeckContentsReady()
	{
		if (!FixedRewardsMgr.Get().IsStartupFinished())
		{
			return false;
		}
		if (m_decks.FirstOrDefault((KeyValuePair<long, CollectionDeck> kv) => !kv.Value.NetworkContentsLoaded() && !kv.Value.IsBrawlDeck && !kv.Value.IsDuelsDeck).Value != null)
		{
			return false;
		}
		return true;
	}

	public bool ShouldAccountSeeStandardWild()
	{
		if (!RankMgr.Get().WildCardsAllowedInCurrentLeague())
		{
			return false;
		}
		if (AccountEverHadWildCards())
		{
			return true;
		}
		if (AccountHasRotatedItems())
		{
			return true;
		}
		return false;
	}

	public bool ShouldAccountSeeStandardComingSoon()
	{
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: true))
		{
			return false;
		}
		if (m_showStandardComingSoonNotice)
		{
			return true;
		}
		if (!RankMgr.Get().WildCardsAllowedInCurrentLeague())
		{
			return false;
		}
		if (SetRotationManager.HasSeenStandardModeTutorial())
		{
			return false;
		}
		if (AccountHasRotatedItems() || AccountEverHadWildCards())
		{
			return false;
		}
		DateTime? upcomingSetRotationStartTime = SpecialEventManager.Get().GetEventStartTimeUtc(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021);
		if (!upcomingSetRotationStartTime.HasValue)
		{
			return false;
		}
		upcomingSetRotationStartTime += new TimeSpan(0, 0, 1);
		if (AccountHasRotatedItems(upcomingSetRotationStartTime.Value))
		{
			m_showStandardComingSoonNotice = true;
			return true;
		}
		if (m_collectibleCards.Any((CollectibleCard c) => c.OwnedCount > 0 && GameUtils.IsCardRotated(c.GetEntityDef(), upcomingSetRotationStartTime.Value)))
		{
			m_showStandardComingSoonNotice = true;
			return true;
		}
		return false;
	}

	public bool AccountHasRotatedItems()
	{
		if (m_accountHasRotatedItems)
		{
			return true;
		}
		bool num = AccountHasRotatedItems(DateTime.UtcNow);
		if (num)
		{
			m_accountHasRotatedItems = true;
		}
		return num;
	}

	public bool AccountHasRotatedBoosters(DateTime utcTimestamp)
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			foreach (NetCache.BoosterStack boosterStack in netObject.BoosterStacks)
			{
				if (GameUtils.IsBoosterRotated((BoosterDbId)boosterStack.Id, utcTimestamp))
				{
					return true;
				}
			}
		}
		return false;
	}

	private bool AccountHasRotatedItems(DateTime utcTimestamp)
	{
		if (AccountHasRotatedBoosters(utcTimestamp))
		{
			return true;
		}
		foreach (AdventureMission.WingProgress item in AdventureProgressMgr.Get().GetAllProgress())
		{
			if (item.IsOwned())
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord(item.Wing);
				if (record != null && GameUtils.IsAdventureRotated((AdventureDbId)record.AdventureId, utcTimestamp))
				{
					return true;
				}
			}
		}
		return false;
	}

	public bool AccountEverHadWildCards()
	{
		if (m_accountEverHadWildCards)
		{
			return true;
		}
		m_accountEverHadWildCards = SetRotationManager.HasSeenStandardModeTutorial() || AccountHasWildCards();
		return m_accountEverHadWildCards;
	}

	public bool AccountHasWildCards()
	{
		if (GetNumberOfWildDecks() > 0)
		{
			return true;
		}
		if (m_lastSearchForWildCardsTime > m_collectionLastModifiedTime)
		{
			return m_accountHasWildCards;
		}
		m_accountHasWildCards = m_collectibleCards.Any((CollectibleCard c) => c.OwnedCount > 0 && GameUtils.IsCardRotated(c.GetEntityDef()));
		m_lastSearchForWildCardsTime = Time.realtimeSinceStartup;
		return m_accountHasWildCards;
	}

	public int GetNumberOfWildDecks()
	{
		return m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_WILD);
	}

	public int GetNumberOfStandardDecks()
	{
		return m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_STANDARD);
	}

	public int GetNumberOfClassicDecks()
	{
		return m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_CLASSIC);
	}

	public bool AccountHasValidDeck(FormatType formatType)
	{
		foreach (CollectionDeck deck in Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (deck.IsValidForRuleset && deck.FormatType == formatType)
			{
				return true;
			}
		}
		return false;
	}

	public bool AccountHasAnyValidDeck()
	{
		foreach (CollectionDeck deck in Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (deck.IsValidForRuleset)
			{
				return true;
			}
		}
		return false;
	}

	public CollectionDeck GetEditedDeck()
	{
		CollectionDeck editedDeck = m_EditedDeck;
		if (editedDeck != null && editedDeck.IsBrawlDeck)
		{
			TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
			if (tavernBrawlManager != null)
			{
				TavernBrawlMission tavernBrawlMission = (tavernBrawlManager.IsCurrentBrawlTypeActive ? tavernBrawlManager.CurrentMission() : null);
				if (tavernBrawlMission == null || editedDeck.SeasonId != tavernBrawlMission.seasonId)
				{
					return null;
				}
			}
		}
		return editedDeck;
	}

	public int GetDeckSize()
	{
		if (m_deckRuleset == null)
		{
			return 30;
		}
		return m_deckRuleset.GetDeckSize(GetEditedDeck());
	}

	public List<TemplateDeck> GetTemplateDecks(FormatType formatType, TAG_CLASS classType)
	{
		if (m_templateDeckMap.Values.Count == 0)
		{
			LoadTemplateDecks();
		}
		List<TemplateDeck> value = null;
		m_templateDecks.TryGetValue(classType, out value);
		if (formatType == FormatType.FT_WILD)
		{
			return value.Where((TemplateDeck x) => x.m_formatType == FormatType.FT_STANDARD || x.m_formatType == FormatType.FT_WILD).ToList();
		}
		return value.Where((TemplateDeck x) => x.m_formatType == formatType).ToList();
	}

	public List<TemplateDeck> GetNonStarterTemplateDecks(FormatType formatType, TAG_CLASS classType)
	{
		return GetTemplateDecks(formatType, classType)?.Where((TemplateDeck x) => !x.m_isStarterDeck).ToList();
	}

	public TemplateDeck GetTemplateDeck(int id)
	{
		if (m_templateDeckMap.Values.Count == 0)
		{
			LoadTemplateDecks();
		}
		m_templateDeckMap.TryGetValue(id, out var value);
		return value;
	}

	public bool IsInEditMode()
	{
		return m_editMode;
	}

	public void StartEditingDeck(CollectionDeck deck, object callbackData = null)
	{
		if (deck != null)
		{
			m_editMode = true;
			DeckRuleset deckRuleset;
			if (SceneMgr.Get().IsInTavernBrawlMode())
			{
				deckRuleset = TavernBrawlManager.Get().GetCurrentDeckRuleset();
			}
			else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN)
			{
				deckRuleset = ((deck.Type != DeckType.PVPDR_DECK) ? DeckRuleset.GetPVPDRDisplayRuleset() : DeckRuleset.GetPVPDRRuleset());
			}
			else
			{
				deckRuleset = DeckRuleset.GetRuleset(deck.FormatType);
				PresenceMgr.Get().SetStatus(Global.PresenceStatus.DECKEDITOR);
			}
			SetDeckRuleset(deckRuleset);
			SetEditedDeck(deck, callbackData);
		}
	}

	public void DoneEditing()
	{
		bool editMode = m_editMode;
		m_editMode = false;
		if (editMode && SceneMgr.Get() != null && !SceneMgr.Get().IsInTavernBrawlMode())
		{
			PresenceMgr.Get().SetPrevStatus();
		}
		SetDeckRuleset(null);
		ClearEditedDeck();
	}

	public DeckRuleset GetDeckRuleset()
	{
		return m_deckRuleset;
	}

	public FormatType GetThemeShowing(CollectionDeck deck = null)
	{
		if (CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			return FormatType.FT_STANDARD;
		}
		if (deck == null)
		{
			deck = GetEditedDeck();
		}
		if (deck != null && deck.Type != DeckType.CLIENT_ONLY_DECK)
		{
			return deck.FormatType;
		}
		if (m_collectibleDisplay != null && m_collectibleDisplay.SetFilterTrayInitialized())
		{
			if (m_collectibleDisplay.m_pageManager.CardSetFilterIncludesWild())
			{
				return FormatType.FT_WILD;
			}
			if (m_collectibleDisplay.m_pageManager.CardSetFilterIsClassic())
			{
				return FormatType.FT_CLASSIC;
			}
		}
		return FormatType.FT_STANDARD;
	}

	public FormatType GetThemeShowingForCollectionPage(CollectionDeck deck = null)
	{
		if (SceneMgr.Get().IsInTavernBrawlMode() || SceneMgr.Get().IsInDuelsMode())
		{
			return FormatType.FT_STANDARD;
		}
		if (deck == null)
		{
			deck = GetEditedDeck();
		}
		if (deck != null && deck.Type != DeckType.CLIENT_ONLY_DECK)
		{
			return deck.FormatType;
		}
		if (m_collectibleDisplay != null && m_collectibleDisplay.SetFilterTrayInitialized())
		{
			if (m_collectibleDisplay.m_pageManager.CardSetFilterIncludesWild())
			{
				return FormatType.FT_WILD;
			}
			if (m_collectibleDisplay.m_pageManager.CardSetFilterIsClassic())
			{
				return FormatType.FT_CLASSIC;
			}
		}
		return FormatType.FT_STANDARD;
	}

	public void SetDeckRuleset(DeckRuleset deckRuleset)
	{
		m_deckRuleset = deckRuleset;
		if (m_collectibleDisplay != null)
		{
			m_collectibleDisplay.m_pageManager.SetDeckRuleset(deckRuleset);
		}
	}

	public CollectionDeck SetEditedDeck(long deckId, object callbackData = null)
	{
		CollectionDeck value = null;
		m_decks.TryGetValue(deckId, out value);
		SetEditedDeck(value, callbackData);
		return value;
	}

	public void SetEditedDeck(CollectionDeck deck, object callbackData = null)
	{
		CollectionDeck editedDeck = GetEditedDeck();
		if (deck != editedDeck)
		{
			m_EditedDeck = deck;
			OnEditedDeckChanged[] array = m_editedDeckChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](deck, editedDeck, callbackData);
			}
		}
	}

	public void ClearEditedDeck()
	{
		SetEditedDeck(null);
	}

	public void SendCreateDeck(DeckType deckType, string name, string heroCardID, DeckSourceType deckSourceType = DeckSourceType.DECK_SOURCE_TYPE_NORMAL, string pastedDeckHashString = null)
	{
		int num = GameUtils.TranslateCardIdToDbId(heroCardID);
		if (num == 0)
		{
			Debug.LogWarning($"CollectionManager.SendCreateDeck(): Unknown hero cardID {heroCardID}");
			return;
		}
		FormatType formatType = Options.GetFormatType();
		int brawlLibraryItemId = 0;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			formatType = TavernBrawlManager.Get().CurrentMission().formatType;
		}
		if (deckType == DeckType.PVPDR_DECK)
		{
			formatType = FormatType.FT_WILD;
		}
		if (formatType == FormatType.FT_UNKNOWN)
		{
			Debug.LogWarning($"CollectionManager.SendCreateDeck(): Bad format type {formatType.ToString()}");
			return;
		}
		if ((uint)(deckType - 6) <= 1u)
		{
			brawlLibraryItemId = TavernBrawlManager.Get().CurrentMission().SelectedBrawlLibraryItemId;
		}
		TAG_PREMIUM bestCardPremium = GetBestCardPremium(heroCardID);
		if (m_pendingDeckCreate != null)
		{
			Log.Offline.PrintWarning("SendCreateDeck - Attempting to create a deck while another is still pending.");
		}
		m_pendingDeckCreate = new PendingDeckCreateData
		{
			m_deckType = deckType,
			m_name = name,
			m_heroDbId = num,
			m_heroPremium = bestCardPremium,
			m_formatType = formatType,
			m_sourceType = deckSourceType,
			m_pastedDeckHash = pastedDeckHashString
		};
		if (Network.IsLoggedIn())
		{
			Network.Get().CreateDeck(deckType, name, num, bestCardPremium, formatType, -100L, deckSourceType, out var requestId, pastedDeckHashString, brawlLibraryItemId);
			if (requestId.HasValue)
			{
				m_inTransitDeckCreateRequests.Add(requestId.Value);
			}
		}
		else
		{
			CreateDeckOffline(m_pendingDeckCreate);
		}
	}

	private void CreateDeckOffline(PendingDeckCreateData data)
	{
		DeckInfo deckInfo = OfflineDataCache.CreateDeck(data.m_deckType, data.m_name, data.m_heroDbId, data.m_heroPremium, data.m_formatType, -100L, data.m_sourceType, data.m_pastedDeckHash);
		if (deckInfo == null)
		{
			AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
			{
				m_headerText = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_HEADER"),
				m_text = GameStrings.Get("GLUE_OFFLINE_DECK_ERROR_BODY"),
				m_responseDisplay = AlertPopup.ResponseDisplay.OK,
				m_showAlertIcon = true
			};
			DialogManager.Get().ShowPopup(info);
			CollectionManagerDisplay collectionManagerDisplay = m_collectibleDisplay as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.CancelSelectNewDeckHeroMode();
			}
			if (CollectionDeckTray.Get() != null)
			{
				CollectionDeckTray.Get().m_doneButton.SetEnabled(enabled: true);
			}
		}
		else
		{
			NetCache.DeckHeader deckHeader = Network.GetDeckHeaderFromDeckInfo(deckInfo);
			Processor.ScheduleCallback(0.5f, realTime: false, delegate
			{
				OnDeckCreated(deckHeader, null);
			});
		}
	}

	public void HandleDisconnect()
	{
		if (m_pendingDeckCreate != null)
		{
			CreateDeckOffline(m_pendingDeckCreate);
			m_pendingDeckCreate = null;
		}
		if (m_pendingDeckDeleteList != null)
		{
			PendingDeckDeleteData[] array = m_pendingDeckDeleteList.ToArray();
			foreach (PendingDeckDeleteData pendingDeckDeleteData in array)
			{
				OnDeckDeletedWhileOffline(pendingDeckDeleteData.m_deckId);
			}
			m_pendingDeckDeleteList = null;
		}
		if (m_pendingDeckEditList != null)
		{
			foreach (PendingDeckEditData pendingDeckEdit in m_pendingDeckEditList)
			{
				GetDeck(pendingDeckEdit.m_deckId)?.OnContentChangesComplete();
			}
			m_pendingDeckEditList = null;
		}
		if (m_pendingDeckRenameList == null)
		{
			return;
		}
		foreach (PendingDeckRenameData pendingDeckRename in m_pendingDeckRenameList)
		{
			CollectionDeck deck = GetDeck(pendingDeckRename.m_deckId);
			if (deck != null)
			{
				OfflineDataCache.RenameDeck(pendingDeckRename.m_deckId, pendingDeckRename.m_name);
				deck.OnNameChangeComplete();
			}
		}
		m_pendingDeckRenameList = null;
	}

	public bool RequestDeckContentsForDecksWithoutContentsLoaded(DelOnAllDeckContents callback = null)
	{
		float now = Time.realtimeSinceStartup;
		IEnumerable<KeyValuePair<long, CollectionDeck>> source = m_decks.Where((KeyValuePair<long, CollectionDeck> kv) => !kv.Value.NetworkContentsLoaded());
		source = source.Where((KeyValuePair<long, CollectionDeck> kv) => !kv.Value.IsBrawlDeck || TavernBrawlManager.Get().IsTavernBrawlActiveByDeckType(kv.Value.Type));
		if (!source.Any())
		{
			callback?.Invoke();
			return false;
		}
		if (callback != null && !m_allDeckContentsListeners.Contains(callback))
		{
			m_allDeckContentsListeners.Add(callback);
		}
		if (m_pendingRequestDeckContents != null)
		{
			source = source.Where((KeyValuePair<long, CollectionDeck> kv) => !m_pendingRequestDeckContents.ContainsKey(kv.Value.ID) || now - m_pendingRequestDeckContents[kv.Value.ID] >= 10f);
		}
		IEnumerable<long> source2 = source.Select((KeyValuePair<long, CollectionDeck> kv) => kv.Value.ID);
		if (source2.Any())
		{
			long[] array = source2.ToArray();
			if (m_pendingRequestDeckContents == null)
			{
				m_pendingRequestDeckContents = new Map<long, float>();
			}
			for (int i = 0; i < array.Length; i++)
			{
				m_pendingRequestDeckContents[array[i]] = now;
			}
			Network.Get().RequestDeckContents(array);
			return true;
		}
		return true;
	}

	public void RequestDeckContents(long id)
	{
		CollectionDeck deck = GetDeck(id);
		if (deck != null && deck.NetworkContentsLoaded())
		{
			FireDeckContentsEvent(id);
		}
		else if (Network.IsLoggedIn())
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			if (m_pendingRequestDeckContents != null && m_pendingRequestDeckContents.TryGetValue(id, out var value))
			{
				if (realtimeSinceStartup - value < 10f)
				{
					return;
				}
				m_pendingRequestDeckContents.Remove(id);
			}
			if (m_pendingRequestDeckContents == null)
			{
				m_pendingRequestDeckContents = new Map<long, float>();
			}
			m_pendingRequestDeckContents[id] = realtimeSinceStartup;
			Network.Get().RequestDeckContents(id);
		}
		else
		{
			OnGetDeckContentsResponse();
		}
	}

	public CollectionDeck GetBaseDeck(long id)
	{
		if (m_baseDecks.TryGetValue(id, out var value))
		{
			return value;
		}
		return null;
	}

	public string AutoGenerateDeckName(TAG_CLASS classTag)
	{
		string className = GameStrings.GetClassName(classTag);
		int num = 1;
		string text;
		do
		{
			text = GameStrings.Format("GLUE_COLLECTION_CUSTOM_DECKNAME_TEMPLATE", className, (num == 1) ? "" : num.ToString());
			if (text.Length > CollectionDeck.DefaultMaxDeckNameCharacters)
			{
				text = GameStrings.Format("GLUE_COLLECTION_CUSTOM_DECKNAME_SHORT", className, (num == 1) ? "" : num.ToString());
			}
			num++;
		}
		while (IsDeckNameTaken(text));
		return text;
	}

	public void AutoFillDeck(CollectionDeck deck, bool allowSmartDeckCompletion, DeckAutoFillCallback resultCallback)
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().EnableSmartDeckCompletion)
		{
			allowSmartDeckCompletion = false;
		}
		if (m_smartDeckCallbackByDeckId.ContainsKey(deck.ID))
		{
			Log.CollectionManager.PrintError("AutoFillDeck was called on Deck ID={0} multiple times before a response was received.", deck.ID);
			return;
		}
		deck.IsCreatedWithDeckComplete = true;
		if (!Network.IsLoggedIn())
		{
			allowSmartDeckCompletion = false;
		}
		else if (deck.FormatType == FormatType.FT_CLASSIC)
		{
			allowSmartDeckCompletion = false;
		}
		if (allowSmartDeckCompletion)
		{
			m_smartDeckCallbackByDeckId.Add(deck.ID, resultCallback);
			Network.Get().RequestSmartDeckCompletion(deck);
			Processor.ScheduleCallback(5f, realTime: true, OnSmartDeckTimeout, deck.ID);
		}
		else
		{
			resultCallback(deck, DeckMaker.GetFillCards(deck, deck.GetRuleset()));
		}
	}

	private void OnSmartDeckTimeout(object userdata)
	{
		long num = (long)userdata;
		if (m_smartDeckCallbackByDeckId.ContainsKey(num))
		{
			CollectionDeck deck = GetDeck(num);
			IEnumerable<DeckMaker.DeckFill> fillCards = DeckMaker.GetFillCards(deck, deck.GetRuleset());
			m_smartDeckCallbackByDeckId[num](deck, fillCards);
			m_smartDeckCallbackByDeckId.Remove(num);
		}
	}

	private void OnSmartDeckResponse()
	{
		SmartDeckResponse smartDeckResponse = Network.Get().GetSmartDeckResponse();
		if (smartDeckResponse.HasErrorCode && smartDeckResponse.ErrorCode != 0)
		{
			Log.CollectionManager.PrintError("OnSmartDeckResponse: Response contained errors. ErrorCode=" + smartDeckResponse.ErrorCode);
			if (smartDeckResponse.ResponseMessage != null)
			{
				OnSmartDeckTimeout(smartDeckResponse.ResponseMessage.DeckId);
			}
		}
		if (smartDeckResponse.ResponseMessage != null)
		{
			long deckId = smartDeckResponse.ResponseMessage.DeckId;
			Processor.CancelScheduledCallback(OnSmartDeckTimeout, deckId);
			if (m_smartDeckCallbackByDeckId.ContainsKey(deckId))
			{
				CollectionDeck deck = GetDeck(deckId);
				List<DeckMaker.DeckFill> cardFillFromSmartDeckResponse = GetCardFillFromSmartDeckResponse(deck, smartDeckResponse);
				m_smartDeckCallbackByDeckId[deckId](deck, cardFillFromSmartDeckResponse);
				m_smartDeckCallbackByDeckId.Remove(deckId);
			}
		}
	}

	private List<DeckMaker.DeckFill> GetCardFillFromSmartDeckResponse(CollectionDeck deck, SmartDeckResponse response)
	{
		Log.CollectionManager.PrintDebug("Smart Deck Response Received: " + response.ToHumanReadableString());
		List<DeckMaker.DeckFill> list = new List<DeckMaker.DeckFill>();
		foreach (DeckCardData item in response.ResponseMessage.PlayerDeckCard)
		{
			string cardID = GameUtils.TranslateDbIdToCardId(item.Def.Asset);
			int num = item.Qty - deck.GetCardIdCount(cardID);
			for (int i = 0; i < num; i++)
			{
				list.Add(new DeckMaker.DeckFill
				{
					m_addCard = DefLoader.Get().GetEntityDef(item.Def.Asset)
				});
			}
		}
		int num2 = deck.GetTotalValidCardCount() + list.Count;
		int num3 = deck.GetMaxCardCount() - num2;
		if (num3 > 0)
		{
			list.AddRange(DeckMaker.GetFillCards(deck, deck.GetRuleset()));
			Log.CollectionManager.PrintWarning("Smart Deck: Insufficient number of cards. Adding {0} more cards to deck {1}.", num3, deck.ID);
		}
		return list;
	}

	private bool OnBnetErrorFromSmartDeckCompletion(BnetErrorInfo info, object data)
	{
		if (info.GetError() == BattleNetErrors.ERROR_ATTRIBUTE_MAX_SIZE_EXCEEDED && m_smartDeckCallbackByDeckId.Count > 0)
		{
			Log.CollectionManager.PrintError("Bnet Error: Attribute Max Size Exceeded when attempting to request a Smart Deck Completion.");
			long[] array = m_smartDeckCallbackByDeckId.Keys.ToArray();
			foreach (long id in array)
			{
				SmartDeckRequest smartDeckRequest = Network.GenerateSmartDeckRequestMessage(GetDeck(id));
				TelemetryManager.Client().SendSmartDeckCompleteFailed((int)smartDeckRequest.GetSerializedSize());
				Network.Get().RequestSmartDeckCompletion(GetDeck(id));
			}
			return true;
		}
		return false;
	}

	public static string GetHeroCardId(TAG_CLASS heroClass, CardHero.HeroType heroType)
	{
		if (heroClass == TAG_CLASS.WHIZBANG)
		{
			return "BOT_914h";
		}
		foreach (CardHeroDbfRecord record in GameDbf.CardHero.GetRecords())
		{
			if (record.HeroType == heroType && GameUtils.GetTagClassFromCardDbId(record.CardId) == heroClass)
			{
				return GameUtils.TranslateDbIdToCardId(record.CardId);
			}
		}
		return string.Empty;
	}

	public bool ShouldShowDeckTemplatePageForClass(TAG_CLASS classType)
	{
		int @int = Options.Get().GetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, 0);
		int num = 1 << (int)classType;
		return (@int & num) == 0;
	}

	public void SetShowDeckTemplatePageForClass(TAG_CLASS classType, bool show)
	{
		int @int = Options.Get().GetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, 0);
		int num = 1 << (int)classType;
		@int |= num;
		if (show)
		{
			@int ^= num;
		}
		Options.Get().SetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, @int);
	}

	public bool ShouldShowWildToStandardTutorial(bool checkPrevSceneIsPlayMode = true)
	{
		if (!ShouldAccountSeeStandardWild())
		{
			return false;
		}
		if (SceneMgr.Get().GetMode() != SceneMgr.Mode.COLLECTIONMANAGER)
		{
			return false;
		}
		if (checkPrevSceneIsPlayMode && SceneMgr.Get().GetPrevMode() != SceneMgr.Mode.TOURNAMENT)
		{
			return false;
		}
		return Options.Get().GetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK);
	}

	public bool UpdateDeckWithNewId(long oldId, long newId)
	{
		if (CollectionDeckTray.Get() != null && !CollectionDeckTray.Get().GetDecksContent().UpdateDeckBoxWithNewId(oldId, newId))
		{
			return false;
		}
		CollectionDeck editedDeck = GetEditedDeck();
		if (IsInEditMode() && editedDeck.ID == oldId && m_decks.ContainsKey(newId))
		{
			m_decks[newId].CopyContents(editedDeck);
			SetEditedDeck(m_decks[newId]);
		}
		RemoveDeck(oldId);
		return true;
	}

	public int GetOwnedCount(string cardId, TAG_PREMIUM premium)
	{
		Get().GetOwnedCardCount(cardId, out var normal, out var golden, out var diamond);
		int result = 0;
		switch (premium)
		{
		case TAG_PREMIUM.NORMAL:
			result = normal;
			break;
		case TAG_PREMIUM.GOLDEN:
			result = golden;
			break;
		case TAG_PREMIUM.DIAMOND:
			result = diamond;
			break;
		}
		return result;
	}

	public int GetTotalOwnedCount(string cardId)
	{
		Get().GetOwnedCardCount(cardId, out var normal, out var golden, out var diamond);
		return normal + golden + diamond;
	}

	private void InitImpl()
	{
		m_filterIsSetRotatedCache = new Map<TAG_CARD_SET, bool>(EnumUtils.Length<TAG_CARD_SET>(), new TagCardSetEnumComparer());
		List<CardTagDbfRecord> list = GameDbf.CardTag.GetRecords().FindAll((CardTagDbfRecord record) => record.TagId == 1932);
		List<string> allCollectibleCardIds = GameUtils.GetAllCollectibleCardIds();
		m_collectibleCardIndex = new Map<CollectibleCardIndex, CollectibleCard>(allCollectibleCardIds.Count * 2 + list.Count, new CollectibleCardIndexComparer());
		foreach (string item in allCollectibleCardIds)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(item);
			if (entityDef == null)
			{
				Error.AddDevFatal("Failed to find an EntityDef for collectible card {0}", item);
				return;
			}
			RegisterCard(entityDef, item, TAG_PREMIUM.NORMAL);
			if (entityDef.GetCardSet() != TAG_CARD_SET.HERO_SKINS)
			{
				RegisterCard(entityDef, item, TAG_PREMIUM.GOLDEN);
			}
		}
		foreach (CardTagDbfRecord item2 in list)
		{
			string text = GameUtils.TranslateDbIdToCardId(item2.CardId);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(text);
			if (entityDef2 != null)
			{
				RegisterCard(entityDef2, text, TAG_PREMIUM.DIAMOND);
			}
		}
		Network network = Network.Get();
		network.RegisterNetHandler(GetDeckContentsResponse.PacketID.ID, OnGetDeckContentsResponse);
		network.RegisterNetHandler(DBAction.PacketID.ID, OnDBAction);
		network.RegisterNetHandler(DeckCreated.PacketID.ID, OnDeckCreatedNetworkResponse);
		network.RegisterNetHandler(DeckDeleted.PacketID.ID, OnDeckDeleted);
		network.RegisterNetHandler(DeckRenamed.PacketID.ID, OnDeckRenamed);
		network.RegisterNetHandler(SmartDeckResponse.PacketID.ID, OnSmartDeckResponse);
		network.AddBnetErrorListener(BnetFeature.Games, OnBnetErrorFromSmartDeckCompletion);
		NetCache.Get().RegisterCollectionManager(OnNetCacheReady);
		LoginManager.Get().OnAchievesLoaded += OnAchievesLoaded;
	}

	private void WillReset()
	{
		m_achievesLoaded = false;
		m_netCacheLoaded = false;
		m_collectionLoaded = false;
		HearthstoneApplication.Get().WillReset -= s_instance.WillReset;
		NetCache.Get().FavoriteCardBackChanged -= s_instance.OnFavoriteCardBackChanged;
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheDecks), s_instance.NetCache_OnDecksReceived);
		m_decks.Clear();
		m_baseDecks.Clear();
		m_preconDecks.Clear();
		m_favoriteHeroChangedListeners.Clear();
		m_templateDecks.Clear();
		m_templateDeckMap.Clear();
		m_displayableCardSets.Clear();
		m_onUIHeroOverrideCardRemovedListeners.Clear();
		m_collectibleCards = new List<CollectibleCard>();
		m_collectibleCardIndex = new Map<CollectibleCardIndex, CollectibleCard>();
		m_collectionLastModifiedTime = 0f;
		m_lastSearchForWildCardsTime = 0f;
		m_accountEverHadWildCards = false;
		m_accountHasRotatedItems = false;
		m_EditedDeck = null;
		s_instance = null;
	}

	private void OnCollectionChanged()
	{
		DelOnCollectionChanged[] array = m_collectionChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	private List<string> GetCardIDsInSet(TAG_CARD_SET? cardSet, TAG_CLASS? cardClass, TAG_RARITY? cardRarity, TAG_RACE? cardRace)
	{
		List<string> nonHeroSkinCollectibleCardIds = GameUtils.GetNonHeroSkinCollectibleCardIds();
		List<string> list = new List<string>();
		foreach (string item in nonHeroSkinCollectibleCardIds)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(item);
			if ((!cardClass.HasValue || cardClass.Value == entityDef.GetClass()) && (!cardRarity.HasValue || cardRarity.Value == entityDef.GetRarity()) && (!cardSet.HasValue || cardSet.Value == entityDef.GetCardSet()) && (!cardRace.HasValue || cardRace.Value == entityDef.GetRace()))
			{
				list.Add(item);
			}
		}
		return list;
	}

	public int NumCardsOwnedInSet(TAG_CARD_SET cardSet)
	{
		List<CollectibleCard> cards = FindCards(null, null, null, minOwned: 1, theseCardSets: new TAG_CARD_SET[1] { cardSet }).m_cards;
		int num = 0;
		foreach (CollectibleCard item in cards)
		{
			num += item.OwnedCount;
		}
		return num;
	}

	private CollectibleCard RegisterCard(EntityDef entityDef, string cardID, TAG_PREMIUM premium)
	{
		CollectibleCardIndex key = new CollectibleCardIndex(cardID, premium);
		CollectibleCard value = null;
		if (!m_collectibleCardIndex.TryGetValue(key, out value))
		{
			value = new CollectibleCard(GameUtils.GetCardRecord(cardID), entityDef, premium);
			m_collectibleCards.Add(value);
			m_collectibleCardIndex.Add(key, value);
		}
		return value;
	}

	private void ClearCardCounts(EntityDef entityDef, string cardID, TAG_PREMIUM premium)
	{
		RegisterCard(entityDef, cardID, premium).ClearCounts();
	}

	private CollectibleCard SetCounts(NetCache.CardStack netStack, EntityDef entityDef)
	{
		ClearCardCounts(entityDef, netStack.Def.Name, netStack.Def.Premium);
		return AddCounts(entityDef, netStack.Def.Name, netStack.Def.Premium, new DateTime(netStack.Date), netStack.Count, netStack.NumSeen);
	}

	private CollectibleCard AddCounts(EntityDef entityDef, string cardID, TAG_PREMIUM premium, DateTime insertDate, int count, int numSeen)
	{
		if (entityDef == null)
		{
			Debug.LogError($"CollectionManager.RegisterCardStack(): DefLoader failed to get entity def for {cardID}");
			return null;
		}
		m_collectionLastModifiedTime = Time.realtimeSinceStartup;
		CollectibleCard collectibleCard = RegisterCard(entityDef, cardID, premium);
		if (GameUtils.IsCoreCard(cardID))
		{
			count = Math.Min(collectibleCard.DefaultMaxCopiesPerDeck - collectibleCard.OwnedCount, count);
			numSeen = Math.Min(numSeen, count);
		}
		collectibleCard.AddCounts(count, numSeen, insertDate);
		return collectibleCard;
	}

	private void AddPreconDeckFromNotice(NetCache.ProfileNoticePreconDeck preconDeckNotice)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(preconDeckNotice.HeroAsset);
		if (entityDef != null)
		{
			AddPreconDeck(entityDef.GetClass(), preconDeckNotice.DeckID);
			NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
			if (netObject != null)
			{
				NetCache.DeckHeader item = new NetCache.DeckHeader
				{
					ID = preconDeckNotice.DeckID,
					Name = "precon",
					Hero = entityDef.GetCardId(),
					HeroPower = GameUtils.GetHeroPowerCardIdFromHero(preconDeckNotice.HeroAsset),
					Type = DeckType.PRECON_DECK,
					SortOrder = preconDeckNotice.DeckID,
					SourceType = DeckSourceType.DECK_SOURCE_TYPE_BASIC_DECK
				};
				netObject.Decks.Add(item);
				Network.Get().AckNotice(preconDeckNotice.NoticeID);
			}
		}
	}

	private void AddPreconDeck(TAG_CLASS heroClass, long deckID)
	{
		if (m_preconDecks.ContainsKey(heroClass))
		{
			Log.CollectionManager.PrintDebug($"CollectionManager.AddPreconDeck(): Already have a precon deck for class {heroClass}, cannot add deckID {deckID}");
			return;
		}
		Log.CollectionManager.Print($"CollectionManager.AddPreconDeck() heroClass={heroClass} deckID={deckID}");
		m_preconDecks[heroClass] = new PreconDeck(deckID);
	}

	private CollectionDeck AddDeck(NetCache.DeckHeader deckHeader)
	{
		return AddDeck(deckHeader, updateNetCache: true);
	}

	private CollectionDeck AddDeck(NetCache.DeckHeader deckHeader, bool updateNetCache)
	{
		if (deckHeader.Type != DeckType.NORMAL_DECK && !TavernBrawlManager.IsBrawlDeckType(deckHeader.Type) && deckHeader.Type != DeckType.PVPDR_DECK)
		{
			Debug.LogWarning($"CollectionManager.AddDeck(): deckHeader {deckHeader} is not of type NORMAL_DECK, Brawl, or PVPDR deck");
			return null;
		}
		ulong createDate = (ulong)deckHeader.ID;
		if (deckHeader.CreateDate.HasValue)
		{
			createDate = TimeUtils.DateTimeToUnixTimeStamp(deckHeader.CreateDate.Value);
		}
		CollectionDeck collectionDeck = new CollectionDeck
		{
			ID = deckHeader.ID,
			Type = deckHeader.Type,
			Name = deckHeader.Name,
			HeroCardID = deckHeader.Hero,
			HeroPremium = deckHeader.HeroPremium,
			HeroOverridden = deckHeader.HeroOverridden,
			CardBackID = deckHeader.CardBack,
			CardBackOverridden = deckHeader.CardBackOverridden,
			SeasonId = deckHeader.SeasonId,
			BrawlLibraryItemId = deckHeader.BrawlLibraryItemId,
			NeedsName = deckHeader.NeedsName,
			SortOrder = deckHeader.SortOrder,
			FormatType = deckHeader.FormatType,
			SourceType = deckHeader.SourceType,
			CreateDate = createDate,
			Locked = deckHeader.Locked,
			UIHeroOverrideCardID = deckHeader.UIHeroOverride,
			UIHeroOverridePremium = deckHeader.UIHeroOverridePremium
		};
		if (collectionDeck.NeedsName && string.IsNullOrEmpty(collectionDeck.Name))
		{
			collectionDeck.Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", GameStrings.GetClassName(collectionDeck.GetClass()));
			Log.CollectionManager.Print($"Set deck name to {collectionDeck.Name}");
		}
		if (!IsInEditMode() || GetEditedDeck() == null || GetEditedDeck().ID != collectionDeck.ID)
		{
			if (m_decks.ContainsKey(deckHeader.ID))
			{
				m_decks.Remove(deckHeader.ID);
			}
			m_decks.Add(deckHeader.ID, collectionDeck);
		}
		CollectionDeck value = new CollectionDeck
		{
			ID = deckHeader.ID,
			Type = deckHeader.Type,
			Name = deckHeader.Name,
			HeroCardID = deckHeader.Hero,
			HeroPremium = deckHeader.HeroPremium,
			HeroOverridden = deckHeader.HeroOverridden,
			CardBackID = deckHeader.CardBack,
			CardBackOverridden = deckHeader.CardBackOverridden,
			SeasonId = deckHeader.SeasonId,
			BrawlLibraryItemId = deckHeader.BrawlLibraryItemId,
			NeedsName = deckHeader.NeedsName,
			SortOrder = deckHeader.SortOrder,
			FormatType = deckHeader.FormatType,
			SourceType = deckHeader.SourceType,
			UIHeroOverrideCardID = deckHeader.UIHeroOverride,
			UIHeroOverridePremium = deckHeader.UIHeroOverridePremium
		};
		if (m_baseDecks.ContainsKey(deckHeader.ID))
		{
			m_baseDecks.Remove(deckHeader.ID);
		}
		m_baseDecks.Add(deckHeader.ID, value);
		if (updateNetCache)
		{
			NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Add(deckHeader);
		}
		return collectionDeck;
	}

	private CollectionDeck RemoveDeck(long id)
	{
		CollectionDeck value = null;
		if (m_baseDecks.TryGetValue(id, out value))
		{
			m_baseDecks.Remove(id);
		}
		if (m_decks.TryGetValue(id, out value))
		{
			m_decks.Remove(id);
		}
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject == null)
		{
			return value;
		}
		for (int i = 0; i < netObject.Decks.Count; i++)
		{
			if (netObject.Decks[i].ID == id)
			{
				netObject.Decks.RemoveAt(i);
				break;
			}
		}
		return value;
	}

	public static void DeleteDeckFromNetwork(CollectionDeck deck)
	{
		deck.MarkBeingDeleted();
		Network.Get().DeleteDeck(deck.ID, deck.Type);
		s_instance.AddPendingDeckDelete(deck.ID);
		if (!Network.IsLoggedIn() || deck.ID <= 0)
		{
			s_instance.OnDeckDeletedWhileOffline(deck.ID);
		}
	}

	private void LogAllDeckStringsInCollection()
	{
		Log.Decks.PrintInfo("Deck Contents Received:");
		foreach (CollectionDeck value in GetDecks().Values)
		{
			value.LogDeckStringInformation();
		}
	}

	private bool IsDeckNameTaken(string name)
	{
		foreach (CollectionDeck value in GetDecks().Values)
		{
			if (value.Name.Trim().Equals(name, StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}

	private void FireDeckContentsEvent(long id)
	{
		DelOnDeckContents[] array = m_deckContentsListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](id);
		}
	}

	private void FireAllDeckContentsEvent()
	{
		DelOnAllDeckContents[] array = m_allDeckContentsListeners.ToArray();
		m_allDeckContentsListeners.Clear();
		DelOnAllDeckContents[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i]();
		}
	}

	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(OnNetCacheReady);
		m_netCacheLoaded = true;
		Log.CollectionManager.Print("CollectionManager.OnNetCacheReady");
		m_displayableCardSets.AddRange(from cardSetRecord in GameDbf.CardSet.GetRecords()
			where cardSetRecord != null && cardSetRecord.IsCollectible && cardSetRecord.ID != 17
			where SpecialEventManager.Get().IsEventActive(cardSetRecord?.SetFilterEvent, activeIfDoesNotExist: false)
			select (TAG_CARD_SET)cardSetRecord.ID);
		UpdateShowAdvancedCMOption();
		if (Options.GetFormatType() == FormatType.FT_WILD && !ShouldAccountSeeStandardWild())
		{
			Log.CollectionManager.Print("Options are set to Wild mode, but account shouldn't see Standard/Wild, so setting format type to Standard!");
			Options.SetFormatType(FormatType.FT_STANDARD);
		}
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject != null)
		{
			OnNewNotices(netObject.Notices, isInitialNoticeList: true);
		}
		NetCache.Get().RegisterNewNoticesListener(OnNewNotices);
		CheckAchievesAndNetCacheLoaded();
	}

	private void OnAchievesLoaded()
	{
		LoginManager.Get().OnAchievesLoaded -= OnAchievesLoaded;
		m_achievesLoaded = true;
		CheckAchievesAndNetCacheLoaded();
	}

	private void CheckAchievesAndNetCacheLoaded()
	{
		if (m_achievesLoaded && m_netCacheLoaded)
		{
			CreateCollectionDecksFromNetCache();
			DelOnCollectionLoaded[] array = m_collectionLoadedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i]();
			}
			m_collectionLoaded = true;
			if (CollectionManager.OnCollectionManagerReady != null)
			{
				CollectionManager.OnCollectionManagerReady();
			}
		}
	}

	private void CreateCollectionDecksFromNetCache()
	{
		List<NetCache.DeckHeader> list = new List<NetCache.DeckHeader>();
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject != null)
		{
			list = netObject.Decks;
		}
		foreach (NetCache.DeckHeader item in list)
		{
			switch (item.Type)
			{
			case DeckType.NORMAL_DECK:
			case DeckType.TAVERN_BRAWL_DECK:
			case DeckType.FSG_BRAWL_DECK:
			case DeckType.PVPDR_DECK:
				AddDeck(item, updateNetCache: false);
				break;
			case DeckType.PRECON_DECK:
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(item.Hero);
				if (entityDef == null)
				{
					Debug.LogErrorFormat("CollectionManager.OnAchievesLoaded: cannot add precon deck because cannot determine class for hero with string cardId={0} (deckId={1})", item.Hero, item.ID);
				}
				else
				{
					AddPreconDeck(entityDef.GetClass(), item.ID);
				}
				break;
			}
			default:
				Debug.LogWarning($"CollectionManager.OnAchievesLoaded(): don't know how to handle deck type {item.Type}");
				break;
			}
		}
		List<DeckContents> localDeckContentsFromCache = OfflineDataCache.GetLocalDeckContentsFromCache();
		if (localDeckContentsFromCache != null)
		{
			UpdateFromDeckContents(localDeckContentsFromCache);
		}
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheDecks), s_instance.NetCache_OnDecksReceived);
	}

	public void FixedRewardsStartupComplete()
	{
	}

	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<NetCache.ProfileNotice> list = newNotices.FindAll((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.PRECON_DECK);
		bool flag = false;
		foreach (NetCache.ProfileNotice item in list)
		{
			NetCache.ProfileNoticePreconDeck preconDeckNotice = item as NetCache.ProfileNoticePreconDeck;
			AddPreconDeckFromNotice(preconDeckNotice);
			flag = true;
		}
		bool flag2 = false;
		foreach (NetCache.ProfileNotice item2 in newNotices.FindAll((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.DECK_REMOVED))
		{
			NetCache.ProfileNoticeDeckRemoved profileNoticeDeckRemoved = item2 as NetCache.ProfileNoticeDeckRemoved;
			RemoveDeck(profileNoticeDeckRemoved.DeckID);
			Network.Get().AckNotice(profileNoticeDeckRemoved.NoticeID);
			flag2 = true;
		}
		if (flag || flag2)
		{
			NetCache.Get().ReloadNetObject<NetCache.NetCacheDecks>();
		}
	}

	private void UpdateShowAdvancedCMOption()
	{
		if (Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, defaultVal: false))
		{
			return;
		}
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		if (netObject == null)
		{
			return;
		}
		bool flag = netObject.TotalCardsOwned >= 116;
		if (RankMgr.Get().IsNewPlayer())
		{
			if (!AccountEverHadWildCards() && !flag)
			{
				return;
			}
		}
		else if (!flag)
		{
			return;
		}
		Options.Get().SetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, val: true);
	}

	private void UpdateDeckHeroArt(string heroCardID)
	{
		TAG_PREMIUM bestCardPremium = GetBestCardPremium(heroCardID);
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(heroCardID).GetClass();
		PreconDeck preconDeck = GetPreconDeck(@class);
		if (preconDeck != null)
		{
			m_preconDecks[@class] = new PreconDeck(preconDeck.ID);
		}
		foreach (CollectionDeck item in m_baseDecks.Values.ToList().FindAll((CollectionDeck obj) => obj.HeroCardID.Equals(heroCardID)))
		{
			item.HeroPremium = bestCardPremium;
		}
		foreach (CollectionDeck item2 in m_decks.Values.ToList().FindAll((CollectionDeck obj) => obj.HeroCardID.Equals(heroCardID)))
		{
			item2.HeroPremium = bestCardPremium;
		}
	}

	private void InsertNewCollectionCard(string cardID, TAG_PREMIUM premium, DateTime insertDate, int count, bool seenBefore)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		int numSeen = (seenBefore ? count : 0);
		AddCounts(entityDef, cardID, premium, insertDate, count, numSeen);
		if (entityDef.IsHeroSkin())
		{
			if (premium == TAG_PREMIUM.GOLDEN)
			{
				UpdateDeckHeroArt(cardID);
			}
			StoreManager.Get().Catalog.UpdateProductStatus();
			return;
		}
		foreach (KeyValuePair<long, CollectionDeck> deck in m_decks)
		{
			deck.Value.OnCardInsertedToCollection(cardID, premium);
		}
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().HandleAddedCardDeckUpdate(entityDef, premium, count);
			if (CollectionDeckTray.Get().m_decksContent != null)
			{
				CollectionDeckTray.Get().m_decksContent.RefreshMissingCardIndicators();
			}
		}
		NotifyNetCacheOfNewCards(new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		}, insertDate.Ticks, count, seenBefore);
		UpdateShowAdvancedCMOption();
	}

	private void InsertNewCollectionCards(List<string> cardIDs, List<TAG_PREMIUM> cardPremiums, List<DateTime> insertDates, List<int> counts, bool seenBefore)
	{
		for (int i = 0; i < cardIDs.Count; i++)
		{
			string cardID = cardIDs[i];
			TAG_PREMIUM premium = cardPremiums[i];
			DateTime insertDate = insertDates[i];
			int count = counts[i];
			InsertNewCollectionCard(cardID, premium, insertDate, count, seenBefore);
		}
	}

	private void RemoveCollectionCard(string cardID, TAG_PREMIUM premium, int count)
	{
		CollectibleCard card = GetCard(cardID, premium);
		card.RemoveCounts(count);
		m_collectionLastModifiedTime = Time.realtimeSinceStartup;
		int ownedCount = card.OwnedCount;
		foreach (CollectionDeck value in GetDecks().Values)
		{
			if (value.Locked)
			{
				continue;
			}
			int cardCountFirstMatchingSlot = value.GetCardCountFirstMatchingSlot(cardID, premium);
			if (cardCountFirstMatchingSlot > ownedCount)
			{
				int num = cardCountFirstMatchingSlot - ownedCount;
				for (int i = 0; i < num; i++)
				{
					value.RemoveCard(cardID, premium);
				}
				if (!CollectionDeckTray.Get().HandleDeletedCardDeckUpdate(cardID, premium))
				{
					value.SendChanges();
				}
			}
		}
		NotifyNetCacheOfRemovedCards(new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		}, count);
	}

	private void UpdateCardCounts(NetCache.NetCacheCollection netCacheCards, NetCache.CardDefinition cardDef, int count, int newCount)
	{
		netCacheCards.TotalCardsOwned += count;
		if (cardDef.Premium != 0)
		{
			return;
		}
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardDef.Name);
		if (entityDef.IsCoreCard())
		{
			int num = (entityDef.IsElite() ? 1 : 2);
			if (newCount < 0 || newCount > num)
			{
				Debug.LogError("CollectionManager.UpdateCardCounts: created an illegal stack size of " + newCount + " for card " + entityDef);
				count = 0;
			}
			netCacheCards.CoreCardsUnlockedPerClass[entityDef.GetClass()].Add(entityDef.GetCardId());
		}
	}

	private void NotifyNetCacheOfRemovedCards(NetCache.CardDefinition cardDef, int count)
	{
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		NetCache.CardStack cardStack = netObject.Stacks.Find((NetCache.CardStack obj) => obj.Def.Name.Equals(cardDef.Name) && obj.Def.Premium == cardDef.Premium);
		if (cardStack == null)
		{
			Debug.LogError("CollectionManager.NotifyNetCacheOfRemovedCards() - trying to remove a card from an empty stack!");
			return;
		}
		cardStack.Count -= count;
		if (cardStack.Count <= 0)
		{
			netObject.Stacks.Remove(cardStack);
		}
		UpdateCardCounts(netObject, cardDef, -count, cardStack.Count);
	}

	private void NotifyNetCacheOfNewCards(NetCache.CardDefinition cardDef, long insertDate, int count, bool seenBefore)
	{
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		if (netObject == null)
		{
			return;
		}
		NetCache.CardStack cardStack = netObject.Stacks.Find((NetCache.CardStack obj) => obj.Def.Name.Equals(cardDef.Name) && obj.Def.Premium == cardDef.Premium);
		if (cardStack == null)
		{
			cardStack = new NetCache.CardStack
			{
				Def = cardDef,
				Date = insertDate,
				Count = count,
				NumSeen = (seenBefore ? count : 0)
			};
			netObject.Stacks.Add(cardStack);
		}
		else
		{
			if (insertDate > cardStack.Date)
			{
				cardStack.Date = insertDate;
			}
			cardStack.Count += count;
			if (seenBefore)
			{
				cardStack.NumSeen += count;
			}
		}
		UpdateCardCounts(netObject, cardDef, count, cardStack.Count);
	}

	private void LoadTemplateDecks()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (DeckTemplateDbfRecord record3 in GameDbf.DeckTemplate.GetRecords())
		{
			string @event = record3.Event;
			if (!string.IsNullOrEmpty(@event) && !SpecialEventManager.Get().IsEventActive(@event, activeIfDoesNotExist: false))
			{
				continue;
			}
			int deckId = record3.DeckId;
			if (m_templateDeckMap.ContainsKey(deckId))
			{
				continue;
			}
			DeckDbfRecord record = GameDbf.Deck.GetRecord(deckId);
			if (record == null)
			{
				Debug.LogError($"Unable to find deck with ID {deckId}");
				continue;
			}
			Map<string, int> map = new Map<string, int>();
			DeckCardDbfRecord deckCardDbfRecord = GameDbf.DeckCard.GetRecord(record.TopCardId);
			while (deckCardDbfRecord != null)
			{
				int cardId = deckCardDbfRecord.CardId;
				CardDbfRecord record2 = GameDbf.Card.GetRecord(cardId);
				if (record2 != null)
				{
					string noteMiniGuid = record2.NoteMiniGuid;
					if (map.ContainsKey(noteMiniGuid))
					{
						map[noteMiniGuid]++;
					}
					else
					{
						map[noteMiniGuid] = 1;
					}
				}
				else
				{
					Debug.LogError($"Card ID in deck not found in CARD.XML: {cardId}");
				}
				int nextCard = deckCardDbfRecord.NextCard;
				deckCardDbfRecord = ((nextCard != 0) ? GameDbf.DeckCard.GetRecord(nextCard) : null);
			}
			TAG_CLASS classId = (TAG_CLASS)record3.ClassId;
			List<TemplateDeck> value = null;
			if (!m_templateDecks.TryGetValue(classId, out value))
			{
				value = new List<TemplateDeck>();
				m_templateDecks.Add(classId, value);
			}
			TemplateDeck templateDeck = new TemplateDeck
			{
				m_id = deckId,
				m_class = classId,
				m_sortOrder = record3.SortOrder,
				m_cardIds = map,
				m_title = record.Name,
				m_description = record.Description,
				m_displayTexture = record3.DisplayTexture,
				m_event = record3.Event,
				m_isStarterDeck = record3.IsStarterDeck,
				m_formatType = (FormatType)record3.FormatType
			};
			value.Add(templateDeck);
			m_templateDeckMap.Add(templateDeck.m_id, templateDeck);
		}
		foreach (KeyValuePair<TAG_CLASS, List<TemplateDeck>> templateDeck2 in m_templateDecks)
		{
			templateDeck2.Value.Sort(delegate(TemplateDeck a, TemplateDeck b)
			{
				int num = a.m_sortOrder.CompareTo(b.m_sortOrder);
				if (num == 0)
				{
					num = a.m_id.CompareTo(b.m_id);
				}
				return num;
			});
		}
		float realtimeSinceStartup2 = Time.realtimeSinceStartup;
		Log.CollectionManager.Print("_decktemplate: Time spent loading template decks: " + (realtimeSinceStartup2 - realtimeSinceStartup));
	}

	public TAG_PREMIUM GetPreferredPremium()
	{
		return m_premiumPreference;
	}

	public void SetPremiumPreference(TAG_PREMIUM premium)
	{
		m_premiumPreference = premium;
		RefreshCurrentPageContents();
	}

	public void RefreshCurrentPageContents()
	{
		if (m_collectibleDisplay != null)
		{
			m_collectibleDisplay.m_pageManager.RefreshCurrentPageContents();
		}
	}

	public void RegisterDecksToRequestContentsAfterDeckSetDataResponse(List<long> decksToRequest)
	{
		foreach (long item in decksToRequest)
		{
			if (!m_decksToRequestContentsAfterDeckSetDataResonse.Contains(item))
			{
				m_decksToRequestContentsAfterDeckSetDataResonse.Add(item);
			}
		}
	}

	public static void ShowFeatureDisabledWhileOfflinePopup()
	{
		AlertPopup.PopupInfo info = new AlertPopup.PopupInfo
		{
			m_headerText = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_HEADER"),
			m_text = GameStrings.Get("GLUE_OFFLINE_FEATURE_DISABLED_BODY"),
			m_responseDisplay = AlertPopup.ResponseDisplay.OK,
			m_showAlertIcon = false
		};
		DialogManager.Get().ShowPopup(info);
	}

	public void SetTimeOfLastPlayerDeckSave(DateTime? time)
	{
		m_timeOfLastPlayerDeckSave = time;
	}

	public static ShareableDeck GetShareableDeckFromTemplateRecord(DeckTemplateDbfRecord deckTemplateDBfRecord, bool usePremiumCardsFromCollection = true)
	{
		int favoriteHeroCardDBIdFromClass = GameUtils.GetFavoriteHeroCardDBIdFromClass((TAG_CLASS)deckTemplateDBfRecord.ClassId);
		CollectionDeck collectionDeck = new CollectionDeck
		{
			Name = deckTemplateDBfRecord.DeckRecord.Name,
			FormatType = (FormatType)deckTemplateDBfRecord.FormatType,
			HeroCardID = GameUtils.TranslateDbIdToCardId(favoriteHeroCardDBIdFromClass)
		};
		string deckName;
		string deckDescription;
		List<long> list = Get().LoadDeckFromDBF(deckTemplateDBfRecord.DeckId, out deckName, out deckDescription);
		if (list == null)
		{
			Debug.LogError($"GetShareableDeckFromTemplateRecord: Failed to find cards for deck template {deckTemplateDBfRecord.ID}");
			return null;
		}
		foreach (long item in list)
		{
			string cardID = GameUtils.TranslateDbIdToCardId((int)item);
			TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
			if (usePremiumCardsFromCollection)
			{
				premium = Get().GetBestCardPremium(cardID);
			}
			collectionDeck.AddCard(cardID, premium);
		}
		return collectionDeck.GetShareableDeck();
	}
}
