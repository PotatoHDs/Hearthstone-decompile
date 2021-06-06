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

// Token: 0x02000110 RID: 272
public class CollectionManager
{
	// Token: 0x14000018 RID: 24
	// (add) Token: 0x06001085 RID: 4229 RVA: 0x0005C4FC File Offset: 0x0005A6FC
	// (remove) Token: 0x06001086 RID: 4230 RVA: 0x0005C530 File Offset: 0x0005A730
	public static event CollectionManager.DelCollectionManagerReady OnCollectionManagerReady;

	// Token: 0x06001087 RID: 4231 RVA: 0x0005C564 File Offset: 0x0005A764
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
			cardStack2.Def.Name = GameUtils.TranslateDbIdToCardId(cardStack.CardDef.Asset, false);
			if (string.IsNullOrEmpty(cardStack2.Def.Name))
			{
				global::Error.AddDevFatal("CollectionManager.OnInitialCollectionReceived: failed to find a card with databaseId: {0}", new object[]
				{
					cardStack.CardDef.Asset
				});
				list.Add(cardStack.CardDef.Asset.ToString());
			}
			else
			{
				cardStack2.Def.Premium = (TAG_PREMIUM)cardStack.CardDef.Premium;
				cardStack2.Date = global::TimeUtils.PegDateToFileTimeUtc(cardStack.LatestInsertDate);
				cardStack2.Count = cardStack.Count;
				cardStack2.NumSeen = cardStack.NumSeen;
				netCacheCollection.Stacks.Add(cardStack2);
				netCacheCollection.TotalCardsOwned += cardStack2.Count;
				if (GameUtils.IsCardCollectible(cardStack2.Def.Name))
				{
					EntityDef entityDef = DefLoader.Get().GetEntityDef(cardStack2.Def.Name);
					this.SetCounts(cardStack2, entityDef);
					if (entityDef.IsCoreCard() && cardStack2.Def.Premium == TAG_PREMIUM.NORMAL)
					{
						netCacheCollection.CoreCardsUnlockedPerClass[entityDef.GetClass()].Add(entityDef.GetCardId());
					}
				}
			}
		}
		Action[] array = this.m_initialCollectionReceivedListeners.ToArray();
		for (int j = 0; j < array.Length; j++)
		{
			array[j]();
		}
		if (list.Count > 0)
		{
			string text = string.Join(", ", list.ToArray());
			global::Error.AddDevWarning("Card Errors", "CollectionManager.OnInitialCollectionRecieved: Cards with the following dbIds could not be found:\n{0}", new object[]
			{
				text
			});
		}
		this.BuildCoreCounterpartMap();
		return netCacheCollection;
	}

	// Token: 0x06001088 RID: 4232 RVA: 0x0005C75C File Offset: 0x0005A95C
	private void OnCardSale()
	{
		Network.CardSaleResult cardSaleResult = Network.Get().GetCardSaleResult();
		bool flag;
		switch (cardSaleResult.Action)
		{
		case Network.CardSaleResult.SaleResult.GENERIC_FAILURE:
			CraftingManager.Get().OnCardGenericError(cardSaleResult);
			flag = false;
			break;
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
		case Network.CardSaleResult.SaleResult.COUNT_MISMATCH:
			CraftingManager.Get().OnCardCountError(cardSaleResult);
			flag = false;
			break;
		default:
			CraftingManager.Get().OnCardUnknownError(cardSaleResult);
			flag = false;
			break;
		}
		string text = string.Format("CollectionManager.OnCardSale {0} for card {1} (asset {2}) premium {3}", new object[]
		{
			cardSaleResult.Action,
			cardSaleResult.AssetName,
			cardSaleResult.AssetID,
			cardSaleResult.Premium
		});
		if (!flag)
		{
			Debug.LogWarning(text);
			return;
		}
		global::Log.Crafting.Print(text, Array.Empty<object>());
		this.OnCollectionChanged();
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x0005C8AC File Offset: 0x0005AAAC
	private void OnMassDisenchantResponse()
	{
		Network.MassDisenchantResponse massDisenchantResponse = Network.Get().GetMassDisenchantResponse();
		if (massDisenchantResponse.Amount == 0)
		{
			Debug.LogError("CollectionManager.OnMassDisenchantResponse(): Amount is 0. This means the backend failed to mass disenchant correctly.");
			return;
		}
		CollectionManager.OnMassDisenchant[] array = this.m_massDisenchantListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](massDisenchantResponse.Amount);
		}
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x0005C900 File Offset: 0x0005AB00
	private void OnSetFavoriteHeroResponse()
	{
		Network.SetFavoriteHeroResponse setFavoriteHeroResponse = Network.Get().GetSetFavoriteHeroResponse();
		if (!setFavoriteHeroResponse.Success || TAG_CLASS.NEUTRAL == setFavoriteHeroResponse.HeroClass || setFavoriteHeroResponse.Hero == null)
		{
			return;
		}
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject != null)
		{
			netObject.FavoriteHeroes[setFavoriteHeroResponse.HeroClass] = setFavoriteHeroResponse.Hero;
			global::Log.CollectionManager.Print("CollectionManager.OnSetFavoriteHeroResponse: favorite hero for class {0} updated to {1}", new object[]
			{
				setFavoriteHeroResponse.HeroClass,
				setFavoriteHeroResponse.Hero
			});
		}
		this.UpdateFavoriteHero(setFavoriteHeroResponse.HeroClass, setFavoriteHeroResponse.Hero.Name, setFavoriteHeroResponse.Hero.Premium);
	}

	// Token: 0x0600108B RID: 4235 RVA: 0x0005C9A8 File Offset: 0x0005ABA8
	public void UpdateFavoriteHero(TAG_CLASS heroClass, string heroCardId, TAG_PREMIUM premium)
	{
		if (NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			using (List<NetCache.DeckHeader>.Enumerator enumerator = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					NetCache.DeckHeader deckHeader = enumerator.Current;
					if (!deckHeader.HeroOverridden)
					{
						EntityDef entityDef = DefLoader.Get().GetEntityDef(deckHeader.Hero);
						if (entityDef != null && heroClass == entityDef.GetClass())
						{
							deckHeader.Hero = heroCardId;
							deckHeader.HeroPremium = premium;
							CollectionDeck deck = this.GetDeck(deckHeader.ID);
							if (deck != null)
							{
								deck.HeroCardID = heroCardId;
								deck.HeroPremium = premium;
							}
							CollectionDeck baseDeck = this.GetBaseDeck(deckHeader.ID);
							if (baseDeck != null)
							{
								baseDeck.HeroCardID = heroCardId;
								baseDeck.HeroPremium = premium;
							}
						}
					}
				}
				goto IL_D1;
			}
		}
		global::Log.CollectionManager.PrintWarning("Received Favorite Heroes without NetCacheDecks being ready!", Array.Empty<object>());
		IL_D1:
		if (this.m_favoriteHeroChangedListeners.Count > 0)
		{
			NetCache.CardDefinition cardDefinition = new NetCache.CardDefinition();
			cardDefinition.Name = heroCardId;
			cardDefinition.Premium = premium;
			CollectionManager.FavoriteHeroChangedListener[] array = this.m_favoriteHeroChangedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire(heroClass, cardDefinition);
			}
		}
	}

	// Token: 0x0600108C RID: 4236 RVA: 0x0005CAE8 File Offset: 0x0005ACE8
	private void OnPVPDRSessionInfoResponse()
	{
		this.m_currentPVPDRDeckId = 0L;
		PVPDRSessionInfoResponse pvpdrsessionInfoResponse = Network.Get().GetPVPDRSessionInfoResponse();
		if (pvpdrsessionInfoResponse.HasSession)
		{
			this.m_currentPVPDRDeckId = pvpdrsessionInfoResponse.Session.DeckId;
		}
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x0005CB24 File Offset: 0x0005AD24
	public void NetCache_OnDecksReceived()
	{
		foreach (NetCache.DeckHeader deckHeader in NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks)
		{
			if (deckHeader.Type == DeckType.NORMAL_DECK && this.GetDeck(deckHeader.ID) == null && DefLoader.Get().GetEntityDef(deckHeader.Hero) != null)
			{
				this.AddDeck(deckHeader, false);
			}
		}
		for (int i = this.m_onNetCacheDecksProcessed.Count - 1; i >= 0; i--)
		{
			this.m_onNetCacheDecksProcessed[i]();
		}
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x0005CBD4 File Offset: 0x0005ADD4
	public void AddOnNetCacheDecksProcessedListener(Action a)
	{
		this.m_onNetCacheDecksProcessed.Add(a);
	}

	// Token: 0x0600108F RID: 4239 RVA: 0x0005CBE2 File Offset: 0x0005ADE2
	public void RemoveOnNetCacheDecksProcessedListener(Action a)
	{
		this.m_onNetCacheDecksProcessed.Remove(a);
	}

	// Token: 0x06001090 RID: 4240 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
	public void OnFavoriteCardBackChanged(int newFavoriteCardBackID)
	{
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject == null)
		{
			Debug.LogWarning(string.Format("CollectionManager.OnFavoriteCardBackChanged({0}): trying to access NetCacheDecks before it's been loaded", newFavoriteCardBackID));
			return;
		}
		foreach (NetCache.DeckHeader deckHeader in netObject.Decks)
		{
			if (!deckHeader.CardBackOverridden)
			{
				deckHeader.CardBack = newFavoriteCardBackID;
				CollectionDeck deck = this.GetDeck(deckHeader.ID);
				if (deck != null)
				{
					deck.CardBackID = deckHeader.CardBack;
				}
				CollectionDeck baseDeck = this.GetBaseDeck(deckHeader.ID);
				if (baseDeck != null)
				{
					baseDeck.CardBackID = deckHeader.CardBack;
				}
			}
		}
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x0005CCB0 File Offset: 0x0005AEB0
	public void OnInitialClientStateDeckContents(NetCache.NetCacheDecks netCacheDecks, List<DeckContents> deckContents)
	{
		if (deckContents != null)
		{
			foreach (NetCache.DeckHeader deckHeader in netCacheDecks.Decks)
			{
				if (deckHeader.Type != DeckType.PRECON_DECK)
				{
					this.AddDeck(deckHeader, false);
				}
			}
			this.UpdateFromDeckContents(deckContents);
		}
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x0005CD18 File Offset: 0x0005AF18
	private void OnGetDeckContentsResponse()
	{
		GetDeckContentsResponse deckContentsResponse = Network.Get().GetDeckContentsResponse();
		this.UpdateFromDeckContents(deckContentsResponse.Decks);
	}

	// Token: 0x06001093 RID: 4243 RVA: 0x0005CD3C File Offset: 0x0005AF3C
	public void UpdateFromDeckContents(List<DeckContents> deckContents)
	{
		if (deckContents == null)
		{
			global::Log.CollectionManager.PrintError("Could not update CollectionManager from Deck Contents. Deck Contents was null", Array.Empty<object>());
			return;
		}
		foreach (DeckContents deckContents2 in deckContents)
		{
			if (deckContents2 == null)
			{
				global::Log.CollectionManager.PrintError("UpdateFromDeckContents: deckContents contained a null deckContent.", Array.Empty<object>());
			}
			else
			{
				Network.DeckContents netDeck = Network.DeckContents.FromPacket(deckContents2);
				if (this.m_pendingRequestDeckContents != null)
				{
					this.m_pendingRequestDeckContents.Remove(netDeck.Deck);
				}
				CollectionDeck collectionDeck = null;
				if (this.m_decks != null)
				{
					this.m_decks.TryGetValue(netDeck.Deck, out collectionDeck);
				}
				else
				{
					global::Log.CollectionManager.PrintError("UpdateFromDeckContents: m_decks is null!", Array.Empty<object>());
				}
				CollectionDeck collectionDeck2 = null;
				if (this.m_baseDecks != null)
				{
					this.m_baseDecks.TryGetValue(netDeck.Deck, out collectionDeck2);
				}
				else
				{
					global::Log.CollectionManager.PrintError("UpdateFromDeckContents: m_baseDecks is null!", Array.Empty<object>());
				}
				bool flag = true;
				if (collectionDeck != null && this.IsInEditMode() && this.GetEditedDeck().ID == collectionDeck.ID)
				{
					flag = false;
				}
				if (collectionDeck == null || collectionDeck2 == null)
				{
					if (this.m_preconDecks == null || !this.m_preconDecks.Any((KeyValuePair<TAG_CLASS, CollectionManager.PreconDeck> kv) => kv.Value.ID == netDeck.Deck))
					{
						Debug.LogErrorFormat("Got contents for an unknown deck or baseDeck: deckId={0}", new object[]
						{
							netDeck.Deck
						});
					}
				}
				else if (collectionDeck != null && collectionDeck2 != null)
				{
					if (flag)
					{
						collectionDeck.ClearSlotContents();
					}
					collectionDeck2.ClearSlotContents();
					foreach (Network.CardUserData cardUserData in netDeck.Cards)
					{
						string text = GameUtils.TranslateDbIdToCardId(cardUserData.DbId, false);
						if (text == null)
						{
							Debug.LogError(string.Format("CollectionManager.OnDeck(): Could not find card with asset ID {0} in our card manifest", cardUserData.DbId));
						}
						else
						{
							if (flag)
							{
								collectionDeck.AddCard_IgnoreDeckRules(text, cardUserData.Premium, cardUserData.Count);
							}
							collectionDeck2.AddCard_IgnoreDeckRules(text, cardUserData.Premium, cardUserData.Count);
						}
					}
					collectionDeck.MarkNetworkContentsLoaded();
				}
				this.FireDeckContentsEvent(netDeck.Deck);
			}
		}
		using (SortedDictionary<long, CollectionDeck>.ValueCollection.Enumerator enumerator3 = this.GetDecks().Values.GetEnumerator())
		{
			while (enumerator3.MoveNext())
			{
				if (!enumerator3.Current.NetworkContentsLoaded())
				{
					return;
				}
			}
		}
		this.LogAllDeckStringsInCollection();
		if (this.m_pendingRequestDeckContents != null)
		{
			float now = Time.realtimeSinceStartup;
			long[] array = (from kv in this.m_pendingRequestDeckContents
			where now - kv.Value > 10f
			select kv.Key).ToArray<long>();
			for (int i = 0; i < array.Length; i++)
			{
				this.m_pendingRequestDeckContents.Remove(array[i]);
			}
		}
		if (this.m_pendingRequestDeckContents == null || this.m_pendingRequestDeckContents.Count == 0)
		{
			this.FireAllDeckContentsEvent();
		}
	}

	// Token: 0x06001094 RID: 4244 RVA: 0x0005D0C8 File Offset: 0x0005B2C8
	private void OnDBAction()
	{
		Network.DBAction response = Network.Get().GetDeckResponse();
		global::Log.CollectionManager.Print(string.Format("MetaData:{0} DBAction:{1} Result:{2}", response.MetaData, response.Action, response.Result), Array.Empty<object>());
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
		case Network.DBAction.ActionType.RENAME_DECK:
			flag = true;
			if (this.m_pendingDeckRenameList != null && this.m_pendingDeckRenameList.Any<CollectionManager.PendingDeckRenameData>())
			{
				this.m_pendingDeckRenameList.RemoveAll((CollectionManager.PendingDeckRenameData d) => d.m_deckId == response.MetaData);
			}
			break;
		case Network.DBAction.ActionType.SET_DECK:
			flag2 = true;
			if (this.m_decksToRequestContentsAfterDeckSetDataResonse.Contains(response.MetaData))
			{
				Network.Get().RequestDeckContents(new long[]
				{
					response.MetaData
				});
				this.m_decksToRequestContentsAfterDeckSetDataResonse.Remove(response.MetaData);
			}
			if (this.m_timeOfLastPlayerDeckSave != null)
			{
				double totalSeconds = (DateTime.Now - this.m_timeOfLastPlayerDeckSave).Value.TotalSeconds;
				TelemetryManager.Client().SendDeckUpdateResponseInfo((float)totalSeconds);
				this.SetTimeOfLastPlayerDeckSave(null);
			}
			if (this.m_pendingDeckEditList != null && this.m_pendingDeckEditList.Any<CollectionManager.PendingDeckEditData>())
			{
				this.m_pendingDeckEditList.RemoveAll((CollectionManager.PendingDeckEditData d) => d.m_deckId == response.MetaData);
			}
			break;
		}
		if (!flag && !flag2)
		{
			return;
		}
		long deckID = response.MetaData;
		CollectionDeck deck = this.GetDeck(deckID);
		CollectionDeck baseDeck = this.GetBaseDeck(deckID);
		if (deck == null)
		{
			return;
		}
		if (response.Result == Network.DBAction.ResultType.SUCCESS)
		{
			global::Log.CollectionManager.Print(string.Format("CollectionManager.OnDBAction(): overwriting baseDeck with {0} updated deck ({1}:{2})", deck.IsValidForRuleset ? "valid" : "INVALID", deck.ID, deck.Name), Array.Empty<object>());
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
				deckHeader2.LastModified = new DateTime?(DateTime.Now);
			}
		}
		else
		{
			global::Log.CollectionManager.Print(string.Format("CollectionManager.OnDBAction(): overwriting deck that failed to update with base deck ({0}:{1})", baseDeck.ID, baseDeck.Name), Array.Empty<object>());
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

	// Token: 0x06001095 RID: 4245 RVA: 0x0005D418 File Offset: 0x0005B618
	private void OnDeckCreatedNetworkResponse()
	{
		int? requestId;
		NetCache.DeckHeader createdDeck = Network.Get().GetCreatedDeck(out requestId);
		this.OnDeckCreated(createdDeck, requestId);
		List<DeckInfo> deckListFromNetCache = NetCache.Get().GetDeckListFromNetCache();
		OfflineDataCache.CacheLocalAndOriginalDeckList(deckListFromNetCache, deckListFromNetCache);
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x0005D44C File Offset: 0x0005B64C
	private void OnDeckCreated(NetCache.DeckHeader deck, int? requestId)
	{
		global::Log.CollectionManager.Print(string.Format("DeckCreated:{0} ID:{1} Hero:{2}", deck.Name, deck.ID, deck.Hero), Array.Empty<object>());
		this.m_pendingDeckCreate = null;
		this.AddDeck(deck).MarkNetworkContentsLoaded();
		if (requestId != null)
		{
			if (!this.m_inTransitDeckCreateRequests.Contains(requestId.Value))
			{
				return;
			}
			this.m_inTransitDeckCreateRequests.Remove(requestId.Value);
		}
		CollectionManager.DelOnDeckCreated[] array = this.m_deckCreatedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](deck.ID);
		}
	}

	// Token: 0x06001097 RID: 4247 RVA: 0x0005D4F5 File Offset: 0x0005B6F5
	private void OnDeckDeleted()
	{
		this.OnDeckDeleted(Network.Get().GetDeletedDeckID());
	}

	// Token: 0x06001098 RID: 4248 RVA: 0x0005D508 File Offset: 0x0005B708
	private void OnDeckDeleted(long deckId)
	{
		global::Log.CollectionManager.Print("CollectionManager.OnDeckDeleted", Array.Empty<object>());
		global::Log.CollectionManager.Print(string.Format("DeckDeleted:{0}", deckId), Array.Empty<object>());
		CollectionDeck collectionDeck = this.RemoveDeck(deckId);
		if (this.m_pendingDeckDeleteList != null && this.m_pendingDeckDeleteList.Any<CollectionManager.PendingDeckDeleteData>())
		{
			this.m_pendingDeckDeleteList.RemoveAll((CollectionManager.PendingDeckDeleteData d) => d.m_deckId == deckId);
		}
		if (CollectionDeckTray.Get() == null)
		{
			return;
		}
		CollectionDeck editedDeck = this.GetEditedDeck();
		if (this.IsInEditMode() && editedDeck != null && editedDeck.ID == deckId)
		{
			Navigation.Pop();
			SceneMgr.Get().SetNextMode(SceneMgr.Mode.HUB, SceneMgr.TransitionHandlerType.SCENEMGR, null);
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
			CollectionManager.DelOnDeckDeleted[] array = this.m_deckDeletedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i](collectionDeck);
			}
		}
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x0005D641 File Offset: 0x0005B841
	public void OnDeckDeletedWhileOffline(long deckId)
	{
		this.OnDeckDeleted(deckId);
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x0005D64A File Offset: 0x0005B84A
	public void AddPendingDeckDelete(long deckId)
	{
		if (this.m_pendingDeckDeleteList == null)
		{
			this.m_pendingDeckDeleteList = new List<CollectionManager.PendingDeckDeleteData>();
		}
		this.m_pendingDeckDeleteList.Add(new CollectionManager.PendingDeckDeleteData
		{
			m_deckId = deckId
		});
	}

	// Token: 0x0600109B RID: 4251 RVA: 0x0005D676 File Offset: 0x0005B876
	public void AddPendingDeckEdit(long deckId)
	{
		if (this.m_pendingDeckEditList == null)
		{
			this.m_pendingDeckEditList = new List<CollectionManager.PendingDeckEditData>();
		}
		this.m_pendingDeckEditList.Add(new CollectionManager.PendingDeckEditData
		{
			m_deckId = deckId
		});
	}

	// Token: 0x0600109C RID: 4252 RVA: 0x0005D6A2 File Offset: 0x0005B8A2
	public void AddPendingDeckRename(long deckId, string name)
	{
		if (this.m_pendingDeckRenameList == null)
		{
			this.m_pendingDeckRenameList = new List<CollectionManager.PendingDeckRenameData>();
		}
		this.m_pendingDeckRenameList.Add(new CollectionManager.PendingDeckRenameData
		{
			m_deckId = deckId,
			m_name = name
		});
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
	private void OnDeckRenamed()
	{
		Network.DeckName renamedDeck = Network.Get().GetRenamedDeck();
		long deck = renamedDeck.Deck;
		string name = renamedDeck.Name;
		this.OnDeckRenamed(deck, name);
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x0005D704 File Offset: 0x0005B904
	private void OnDeckRenamed(long deckId, string newName)
	{
		global::Log.CollectionManager.Print(string.Format("OnDeckRenamed {0}", deckId), Array.Empty<object>());
		CollectionDeck baseDeck = this.GetBaseDeck(deckId);
		CollectionDeck deck = this.GetDeck(deckId);
		if (baseDeck == null || deck == null)
		{
			Debug.LogWarning(string.Format("For deck with ID {0}, unable to handle OnDeckRenamed event to new name {1} due to null deck or null baseDeck", deckId, newName));
			return;
		}
		baseDeck.Name = newName;
		deck.Name = newName;
		NetCache.DeckHeader deckHeader2 = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Find((NetCache.DeckHeader deckHeader) => deckHeader.ID == deckId);
		if (deckHeader2 != null)
		{
			deckHeader2.Name = newName;
			deckHeader2.LastModified = new DateTime?(DateTime.Now);
		}
		OfflineDataCache.RenameDeck(deckId, newName);
		deck.OnNameChangeComplete();
	}

	// Token: 0x0600109F RID: 4255 RVA: 0x0005D7DC File Offset: 0x0005B9DC
	public static void Init()
	{
		if (CollectionManager.s_instance == null)
		{
			CollectionManager.s_instance = new CollectionManager();
			HearthstoneApplication.Get().WillReset += CollectionManager.s_instance.WillReset;
			NetCache.Get().FavoriteCardBackChanged += CollectionManager.s_instance.OnFavoriteCardBackChanged;
			CollectionManager.s_instance.InitImpl();
		}
	}

	// Token: 0x060010A0 RID: 4256 RVA: 0x0005D838 File Offset: 0x0005BA38
	public static CollectionManager Get()
	{
		return CollectionManager.s_instance;
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x0005D83F File Offset: 0x0005BA3F
	public CollectibleDisplay GetCollectibleDisplay()
	{
		return this.m_collectibleDisplay;
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x0005D847 File Offset: 0x0005BA47
	public bool IsFullyLoaded()
	{
		return this.m_collectionLoaded;
	}

	// Token: 0x060010A3 RID: 4259 RVA: 0x0005D850 File Offset: 0x0005BA50
	public void RegisterCollectionNetHandlers()
	{
		Network network = Network.Get();
		network.RegisterNetHandler(BoughtSoldCard.PacketID.ID, new Network.NetHandler(this.OnCardSale), null);
		network.RegisterNetHandler(MassDisenchantResponse.PacketID.ID, new Network.NetHandler(this.OnMassDisenchantResponse), null);
		network.RegisterNetHandler(SetFavoriteHeroResponse.PacketID.ID, new Network.NetHandler(this.OnSetFavoriteHeroResponse), null);
		network.RegisterNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse), null);
	}

	// Token: 0x060010A4 RID: 4260 RVA: 0x0005D8DC File Offset: 0x0005BADC
	public void RemoveCollectionNetHandlers()
	{
		Network network = Network.Get();
		network.RemoveNetHandler(BoughtSoldCard.PacketID.ID, new Network.NetHandler(this.OnCardSale));
		network.RemoveNetHandler(MassDisenchantResponse.PacketID.ID, new Network.NetHandler(this.OnMassDisenchantResponse));
		network.RemoveNetHandler(SetFavoriteHeroResponse.PacketID.ID, new Network.NetHandler(this.OnSetFavoriteHeroResponse));
		network.RemoveNetHandler(PVPDRSessionInfoResponse.PacketID.ID, new Network.NetHandler(this.OnPVPDRSessionInfoResponse));
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x0005D961 File Offset: 0x0005BB61
	public bool HasVisitedCollection()
	{
		return this.m_hasVisitedCollection;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x0005D969 File Offset: 0x0005BB69
	public void SetHasVisitedCollection(bool enable)
	{
		this.m_hasVisitedCollection = enable;
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x0005D972 File Offset: 0x0005BB72
	public bool IsWaitingForBoxTransition()
	{
		return this.m_waitingForBoxTransition;
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x0005D97A File Offset: 0x0005BB7A
	public void NotifyOfBoxTransitionStart()
	{
		Box.Get().AddTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		this.m_waitingForBoxTransition = true;
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x0005D999 File Offset: 0x0005BB99
	public void OnBoxTransitionFinished(object userData)
	{
		Box.Get().RemoveTransitionFinishedListener(new Box.TransitionFinishedCallback(this.OnBoxTransitionFinished));
		this.m_waitingForBoxTransition = false;
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x0005D9B9 File Offset: 0x0005BBB9
	public void SetCollectibleDisplay(CollectibleDisplay display)
	{
		this.m_collectibleDisplay = display;
	}

	// Token: 0x060010AB RID: 4267 RVA: 0x0005D9C4 File Offset: 0x0005BBC4
	public void AddCardReward(CardRewardData cardReward, bool markAsNew)
	{
		this.AddCardRewards(new List<CardRewardData>
		{
			cardReward
		}, markAsNew);
	}

	// Token: 0x060010AC RID: 4268 RVA: 0x0005D9E8 File Offset: 0x0005BBE8
	public void AddCardRewards(List<CardRewardData> cardRewards, bool markAsNew)
	{
		List<string> list = new List<string>();
		List<TAG_PREMIUM> list2 = new List<TAG_PREMIUM>();
		List<DateTime> list3 = new List<DateTime>();
		List<int> list4 = new List<int>();
		DateTime now = DateTime.Now;
		foreach (CardRewardData cardRewardData in cardRewards)
		{
			list.Add(cardRewardData.CardID);
			list2.Add(cardRewardData.Premium);
			list3.Add(now);
			list4.Add(cardRewardData.Count);
		}
		this.InsertNewCollectionCards(list, list2, list3, list4, !markAsNew);
		AchieveManager.Get().ValidateAchievesNow();
		CollectionManager.DelOnCardRewardsInserted[] array = this.m_cardRewardListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](list, list2);
		}
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x0005DAC4 File Offset: 0x0005BCC4
	public float CollectionLastModifiedTime()
	{
		return this.m_collectionLastModifiedTime;
	}

	// Token: 0x060010AE RID: 4270 RVA: 0x0005DACC File Offset: 0x0005BCCC
	public int EntityDefSortComparison(EntityDef entityDef1, EntityDef entityDef2)
	{
		int num = entityDef1.HasTag(GAME_TAG.DECK_LIST_SORT_ORDER) ? entityDef1.GetTag(GAME_TAG.DECK_LIST_SORT_ORDER) : int.MaxValue;
		int num2 = entityDef2.HasTag(GAME_TAG.DECK_LIST_SORT_ORDER) ? entityDef2.GetTag(GAME_TAG.DECK_LIST_SORT_ORDER) : int.MaxValue;
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
		int num5 = string.Compare(name, name2, true);
		if (num5 != 0)
		{
			return num5;
		}
		int cardTypeSortOrder = this.GetCardTypeSortOrder(entityDef1);
		int cardTypeSortOrder2 = this.GetCardTypeSortOrder(entityDef2);
		return cardTypeSortOrder - cardTypeSortOrder2;
	}

	// Token: 0x060010AF RID: 4271 RVA: 0x0005DB68 File Offset: 0x0005BD68
	public int GetCardTypeSortOrder(EntityDef entityDef)
	{
		switch (entityDef.GetCardType())
		{
		case TAG_CARDTYPE.MINION:
			return 3;
		case TAG_CARDTYPE.SPELL:
			return 2;
		case TAG_CARDTYPE.WEAPON:
			return 1;
		}
		return 0;
	}

	// Token: 0x060010B0 RID: 4272 RVA: 0x0005DBA0 File Offset: 0x0005BDA0
	private bool IsSetRotatedWithCache(TAG_CARD_SET set, global::Map<TAG_CARD_SET, bool> cache)
	{
		bool flag;
		if (!cache.TryGetValue(set, out flag))
		{
			flag = GameUtils.IsSetRotated(set);
			cache[set] = flag;
		}
		return flag;
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x0005DBC8 File Offset: 0x0005BDC8
	private void BuildCoreCounterpartMap()
	{
		this.m_coreCounterpartCardMap.Clear();
		foreach (CollectibleCard collectibleCard in this.m_collectibleCards)
		{
			if (collectibleCard.Set == TAG_CARD_SET.CORE && !this.m_coreCounterpartCardMap.ContainsKey(collectibleCard.CardDbId))
			{
				int tag = collectibleCard.GetEntityDef().GetTag(GAME_TAG.DECK_RULE_COUNT_AS_COPY_OF_CARD_ID);
				if (tag != 0 && this.GetCard(GameUtils.TranslateDbIdToCardId(tag, false), collectibleCard.PremiumType) != null)
				{
					this.m_coreCounterpartCardMap.Add(collectibleCard.CardDbId, tag);
				}
			}
		}
	}

	// Token: 0x060010B2 RID: 4274 RVA: 0x0005DC7C File Offset: 0x0005BE7C
	public CollectionManager.FindCardsResult FindCards(string searchString = null, List<CollectibleCardFilter.FilterMask> filterMasks = null, int? manaCost = null, TAG_CARD_SET[] theseCardSets = null, TAG_CLASS[] theseClassTypes = null, TAG_CARDTYPE[] theseCardTypes = null, TAG_RARITY? rarity = null, TAG_RACE? race = null, bool? isHero = null, int? minOwned = null, bool? notSeen = null, bool? isCraftable = null, CollectibleCardFilter.FilterMask? craftableFilterPremiums = null, CollectionManager.CollectibleCardFilterFunc[] priorityFilters = null, global::DeckRuleset deckRuleset = null, bool returnAfterFirstResult = false, HashSet<string> leagueBannedCardsSubset = null, List<int> specificCards = null, bool? filterCoreCounterpartCards = null)
	{
		CollectionManager.FindCardsResult results = new CollectionManager.FindCardsResult();
		global::Map<int, int> startsWithMatchNames = new global::Map<int, int>();
		CollectibleCardFilter.FilterMask searchFilterMask = CollectibleCardFilter.FilterMask.PREMIUM_ALL;
		this.m_filterCardSet.Clear();
		this.m_filterCardClass.Clear();
		this.m_filterCardType.Clear();
		this.m_filterIsSetRotatedCache.Clear();
		List<CollectionManager.CollectibleCardFilterFunc> filterFuncs = new List<CollectionManager.CollectibleCardFilterFunc>();
		if (priorityFilters != null)
		{
			filterFuncs.AddRange(priorityFilters);
		}
		CollectionManager.CollectibleCardFilterFunc item = (CollectibleCard card) => card.IsCraftable && this.GetOwnedCardCountByFilterMask(card.CardId, searchFilterMask) < card.DefaultMaxCopiesPerDeck;
		CollectionManager.CollectibleCardFilterFunc item2 = (CollectibleCard card) => card.IsCraftable && this.GetOwnedCardCountByFilterMask(card.CardId, searchFilterMask) > card.DefaultMaxCopiesPerDeck;
		CollectionManager.CollectibleCardFilterFunc item3 = delegate(CollectibleCard card)
		{
			CollectibleCardFilter.FilterMask filterMask2 = CollectibleCardFilter.FilterMaskFromPremiumType(card.PremiumType);
			if (card.OwnedCount > 0)
			{
				filterMask2 |= CollectibleCardFilter.FilterMask.OWNED;
			}
			else
			{
				filterMask2 |= CollectibleCardFilter.FilterMask.UNOWNED;
			}
			for (int j = 0; j < filterMasks.Count; j++)
			{
				if ((filterMasks[j] & filterMask2) == filterMask2)
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
			string[] source = searchString.ToLower().Split(new char[]
			{
				' '
			});
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
					foreach (CollectibleCardFilter.FilterMask filterMask in filterMasks)
					{
						if ((filterMask & CollectibleCardFilter.FilterMask.OWNED) != CollectibleCardFilter.FilterMask.NONE)
						{
							if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_NORMAL) != CollectibleCardFilter.FilterMask.NONE)
							{
								searchFilterMask = CollectibleCardFilter.FilterMask.PREMIUM_ALL;
								break;
							}
							searchFilterMask = filterMask;
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
				this.m_filterCardType.Add(item4);
			}
			filterFuncs.Add((CollectibleCard card) => this.m_filterCardType.Contains(card.CardType));
		}
		if (rarity != null)
		{
			filterFuncs.Add((CollectibleCard card) => card.Rarity == rarity.Value);
		}
		if (race != null)
		{
			filterFuncs.Add((CollectibleCard card) => card.Race == race.Value);
		}
		if (isHero != null)
		{
			filterFuncs.Add((CollectibleCard card) => card.IsHeroSkin == isHero.Value);
		}
		if (notSeen != null)
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
		if (isCraftable != null)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (craftableFilterPremiums != null)
				{
					CollectibleCardFilter.FilterMask filterMask2 = CollectibleCardFilter.FilterMaskFromPremiumType(card.PremiumType);
					if ((craftableFilterPremiums.Value & filterMask2) != filterMask2)
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
				if (lowerCardName.Split(new char[]
				{
					' '
				}).Any(delegate(string s)
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
					global::Map<int, int> startsWithMatchNames;
					if (!startsWithMatchNames.ContainsKey(card.CardDbId))
					{
						startsWithMatchNames[card.CardDbId] = 0;
					}
					startsWithMatchNames = startsWithMatchNames;
					int cardDbId = card.CardDbId;
					startsWithMatchNames[cardDbId] += card.OwnedCount;
				}
				return true;
			});
		}
		if (manaCost != null)
		{
			int minManaCost = manaCost.Value;
			int maxManaCost = manaCost.Value;
			if (maxManaCost >= 7)
			{
				maxManaCost = int.MaxValue;
			}
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool flag5 = card.ManaCost >= minManaCost && card.ManaCost <= maxManaCost;
				if (!flag5 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					results.m_resultsWithoutManaFilterExist = true;
				}
				return flag5;
			});
		}
		if (theseCardSets != null && theseCardSets.Length != 0)
		{
			foreach (TAG_CARD_SET item5 in theseCardSets)
			{
				this.m_filterCardSet.Add(item5);
			}
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (this.IsSetRotatedWithCache(card.Set, this.m_filterIsSetRotatedCache))
				{
					return true;
				}
				bool flag5 = this.m_filterCardSet.Contains(card.Set);
				if (!flag5 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					results.m_resultsWithoutSetFilterExist = true;
				}
				return flag5;
			});
		}
		if (minOwned != null)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool flag5 = card.OwnedCount >= minOwned.Value;
				global::Map<int, int> startsWithMatchNames;
				if (!flag5 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					startsWithMatchNames = startsWithMatchNames;
					int cardDbId = card.CardDbId;
					startsWithMatchNames[cardDbId] -= card.OwnedCount;
					if (startsWithMatchNames[card.CardDbId] < 1)
					{
						results.m_resultsUnownedExist = true;
					}
				}
				return flag5;
			});
		}
		if (theseCardSets != null && theseCardSets.Length != 0)
		{
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				if (!this.IsSetRotatedWithCache(card.Set, this.m_filterIsSetRotatedCache))
				{
					return true;
				}
				bool flag5 = this.m_filterCardSet.Contains(card.Set);
				if (!flag5 && startsWithMatchNames.ContainsKey(card.CardDbId))
				{
					results.m_resultsInWildExist = true;
				}
				return flag5;
			});
		}
		if (deckRuleset != null)
		{
			CollectionDeck deck = CollectionManager.Get().GetEditedDeck();
			filterFuncs.Add(delegate(CollectibleCard card)
			{
				bool flag5 = deckRuleset.Filter(card.GetEntityDef(), deck, Array.Empty<DeckRule.RuleType>());
				if (!flag5 && card.OwnedCount > 0 && deckRuleset.FilterFailsOnShowInvalidRule(card.GetEntityDef(), deck))
				{
					flag5 = true;
				}
				return flag5;
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
			CollectibleCard collectibleCard = this.m_collectibleCards.Find(match);
			if (collectibleCard != null)
			{
				results.m_cards.Add(collectibleCard);
			}
		}
		else
		{
			results.m_cards = this.m_collectibleCards.FindAll(match);
		}
		if (filterCoreCounterpartCards != null)
		{
			bool? flag3 = filterCoreCounterpartCards;
			bool flag4 = true;
			if (flag3.GetValueOrDefault() == flag4 & flag3 != null)
			{
				this.FilterOutCardWithCoreCounterparts(results.m_cards);
			}
		}
		return results;
	}

	// Token: 0x060010B3 RID: 4275 RVA: 0x0005E240 File Offset: 0x0005C440
	public CollectionManager.FindCardsResult FindOrderedCards(string searchString = null, List<CollectibleCardFilter.FilterMask> filterMasks = null, int? manaCost = null, TAG_CARD_SET[] theseCardSets = null, TAG_CLASS[] theseClassTypes = null, TAG_CARDTYPE[] theseCardTypes = null, TAG_RARITY? rarity = null, TAG_RACE? race = null, bool? isHero = null, int? minOwned = null, bool? notSeen = null, bool? isCraftable = null, CollectibleCardFilter.FilterMask? craftableFilterPremiums = null, CollectionManager.CollectibleCardFilterFunc[] priorityFilters = null, global::DeckRuleset deckRuleset = null, bool returnAfterFirstResult = false, HashSet<string> leagueBannedCardsSubset = null, List<int> specificCards = null, bool? filterCounterpartCards = null)
	{
		CollectionManager.FindCardsResult findCardsResult = this.FindCards(searchString, filterMasks, manaCost, theseCardSets, theseClassTypes, theseCardTypes, rarity, race, isHero, minOwned, notSeen, isCraftable, craftableFilterPremiums, priorityFilters, deckRuleset, returnAfterFirstResult, leagueBannedCardsSubset, specificCards, filterCounterpartCards);
		findCardsResult.m_cards = (from c in findCardsResult.m_cards
		orderby c.ManaCost, c.Name, c.PremiumType
		select c).ToList<CollectibleCard>();
		return findCardsResult;
	}

	// Token: 0x060010B4 RID: 4276 RVA: 0x0005E2F3 File Offset: 0x0005C4F3
	public bool OwnsCoreVersion(int originalCardId)
	{
		return this.m_coreCounterpartCardMap.ContainsValue(originalCardId);
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x0005E304 File Offset: 0x0005C504
	public void FilterOutCardWithCoreCounterparts(List<CollectibleCard> collectibleCards)
	{
		HashSet<CollectionManager.CollectibleCardIndex> counterpartCardsToRemove = new HashSet<CollectionManager.CollectibleCardIndex>();
		foreach (CollectibleCard collectibleCard in collectibleCards)
		{
			if (collectibleCard.Set == TAG_CARD_SET.CORE)
			{
				int dbId = 0;
				if (this.m_coreCounterpartCardMap.TryGetValue(collectibleCard.CardDbId, out dbId))
				{
					CollectibleCard card2 = this.GetCard(GameUtils.TranslateDbIdToCardId(dbId, false), collectibleCard.PremiumType);
					if (card2 != null)
					{
						string text;
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
							counterpartCardsToRemove.Add(new CollectionManager.CollectibleCardIndex(text, collectibleCard.PremiumType));
						}
					}
				}
			}
		}
		collectibleCards.RemoveAll((CollectibleCard card) => counterpartCardsToRemove.Contains(new CollectionManager.CollectibleCardIndex(card.CardId, card.PremiumType)));
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x0005E428 File Offset: 0x0005C628
	public List<CollectibleCard> GetAllCards()
	{
		return this.m_collectibleCards;
	}

	// Token: 0x060010B7 RID: 4279 RVA: 0x0005E430 File Offset: 0x0005C630
	public bool IsCardOwned(string cardId)
	{
		return this.GetTotalOwnedCount(cardId) > 0;
	}

	// Token: 0x060010B8 RID: 4280 RVA: 0x0005E43C File Offset: 0x0005C63C
	public void RegisterCollectionLoadedListener(CollectionManager.DelOnCollectionLoaded listener)
	{
		if (this.m_collectionLoadedListeners.Contains(listener))
		{
			return;
		}
		this.m_collectionLoadedListeners.Add(listener);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x0005E459 File Offset: 0x0005C659
	public bool RemoveCollectionLoadedListener(CollectionManager.DelOnCollectionLoaded listener)
	{
		return this.m_collectionLoadedListeners.Remove(listener);
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x0005E467 File Offset: 0x0005C667
	public void RegisterCollectionChangedListener(CollectionManager.DelOnCollectionChanged listener)
	{
		if (this.m_collectionChangedListeners.Contains(listener))
		{
			return;
		}
		this.m_collectionChangedListeners.Add(listener);
	}

	// Token: 0x060010BB RID: 4283 RVA: 0x0005E484 File Offset: 0x0005C684
	public bool RemoveCollectionChangedListener(CollectionManager.DelOnCollectionChanged listener)
	{
		return this.m_collectionChangedListeners.Remove(listener);
	}

	// Token: 0x060010BC RID: 4284 RVA: 0x0005E492 File Offset: 0x0005C692
	public void RegisterDeckCreatedListener(CollectionManager.DelOnDeckCreated listener)
	{
		if (this.m_deckCreatedListeners.Contains(listener))
		{
			return;
		}
		this.m_deckCreatedListeners.Add(listener);
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x0005E4AF File Offset: 0x0005C6AF
	public bool RemoveDeckCreatedListener(CollectionManager.DelOnDeckCreated listener)
	{
		return this.m_deckCreatedListeners.Remove(listener);
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x0005E4BD File Offset: 0x0005C6BD
	public void RegisterDeckDeletedListener(CollectionManager.DelOnDeckDeleted listener)
	{
		if (this.m_deckDeletedListeners.Contains(listener))
		{
			return;
		}
		this.m_deckDeletedListeners.Add(listener);
	}

	// Token: 0x060010BF RID: 4287 RVA: 0x0005E4DA File Offset: 0x0005C6DA
	public bool RemoveDeckDeletedListener(CollectionManager.DelOnDeckDeleted listener)
	{
		return this.m_deckDeletedListeners.Remove(listener);
	}

	// Token: 0x060010C0 RID: 4288 RVA: 0x0005E4E8 File Offset: 0x0005C6E8
	public void RegisterDeckContentsListener(CollectionManager.DelOnDeckContents listener)
	{
		if (this.m_deckContentsListeners.Contains(listener))
		{
			return;
		}
		this.m_deckContentsListeners.Add(listener);
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0005E505 File Offset: 0x0005C705
	public bool RemoveDeckContentsListener(CollectionManager.DelOnDeckContents listener)
	{
		return this.m_deckContentsListeners.Remove(listener);
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x0005E513 File Offset: 0x0005C713
	public void RegisterNewCardSeenListener(CollectionManager.DelOnNewCardSeen listener)
	{
		if (this.m_newCardSeenListeners.Contains(listener))
		{
			return;
		}
		this.m_newCardSeenListeners.Add(listener);
	}

	// Token: 0x060010C3 RID: 4291 RVA: 0x0005E530 File Offset: 0x0005C730
	public bool RemoveNewCardSeenListener(CollectionManager.DelOnNewCardSeen listener)
	{
		return this.m_newCardSeenListeners.Remove(listener);
	}

	// Token: 0x060010C4 RID: 4292 RVA: 0x0005E53E File Offset: 0x0005C73E
	public void RegisterCardRewardsInsertedListener(CollectionManager.DelOnCardRewardsInserted listener)
	{
		if (this.m_cardRewardListeners.Contains(listener))
		{
			return;
		}
		this.m_cardRewardListeners.Add(listener);
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x0005E55B File Offset: 0x0005C75B
	public bool RemoveCardRewardsInsertedListener(CollectionManager.DelOnCardRewardsInserted listener)
	{
		return this.m_cardRewardListeners.Remove(listener);
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x0005E569 File Offset: 0x0005C769
	public void RegisterMassDisenchantListener(CollectionManager.OnMassDisenchant listener)
	{
		if (this.m_massDisenchantListeners.Contains(listener))
		{
			return;
		}
		this.m_massDisenchantListeners.Add(listener);
	}

	// Token: 0x060010C7 RID: 4295 RVA: 0x0005E586 File Offset: 0x0005C786
	public void RemoveMassDisenchantListener(CollectionManager.OnMassDisenchant listener)
	{
		this.m_massDisenchantListeners.Remove(listener);
	}

	// Token: 0x060010C8 RID: 4296 RVA: 0x0005E595 File Offset: 0x0005C795
	public void RegisterEditedDeckChanged(CollectionManager.OnEditedDeckChanged listener)
	{
		this.m_editedDeckChangedListeners.Add(listener);
	}

	// Token: 0x060010C9 RID: 4297 RVA: 0x0005E5A3 File Offset: 0x0005C7A3
	public void RemoveEditedDeckChanged(CollectionManager.OnEditedDeckChanged listener)
	{
		this.m_editedDeckChangedListeners.Remove(listener);
	}

	// Token: 0x060010CA RID: 4298 RVA: 0x0005E5B2 File Offset: 0x0005C7B2
	public bool RegisterFavoriteHeroChangedListener(CollectionManager.FavoriteHeroChangedCallback callback)
	{
		return this.RegisterFavoriteHeroChangedListener(callback, null);
	}

	// Token: 0x060010CB RID: 4299 RVA: 0x0005E5BC File Offset: 0x0005C7BC
	public bool RegisterFavoriteHeroChangedListener(CollectionManager.FavoriteHeroChangedCallback callback, object userData)
	{
		CollectionManager.FavoriteHeroChangedListener favoriteHeroChangedListener = new CollectionManager.FavoriteHeroChangedListener();
		favoriteHeroChangedListener.SetCallback(callback);
		favoriteHeroChangedListener.SetUserData(userData);
		if (this.m_favoriteHeroChangedListeners.Contains(favoriteHeroChangedListener))
		{
			return false;
		}
		this.m_favoriteHeroChangedListeners.Add(favoriteHeroChangedListener);
		return true;
	}

	// Token: 0x060010CC RID: 4300 RVA: 0x0005E5FA File Offset: 0x0005C7FA
	public bool RemoveFavoriteHeroChangedListener(CollectionManager.FavoriteHeroChangedCallback callback)
	{
		return this.RemoveFavoriteHeroChangedListener(callback, null);
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x0005E604 File Offset: 0x0005C804
	public bool RemoveFavoriteHeroChangedListener(CollectionManager.FavoriteHeroChangedCallback callback, object userData)
	{
		CollectionManager.FavoriteHeroChangedListener favoriteHeroChangedListener = new CollectionManager.FavoriteHeroChangedListener();
		favoriteHeroChangedListener.SetCallback(callback);
		favoriteHeroChangedListener.SetUserData(userData);
		return this.m_favoriteHeroChangedListeners.Remove(favoriteHeroChangedListener);
	}

	// Token: 0x060010CE RID: 4302 RVA: 0x0005E631 File Offset: 0x0005C831
	public bool RegisterOnUIHeroOverrideCardRemovedListener(CollectionManager.OnUIHeroOverrideCardRemovedCallback callback)
	{
		return this.RegisterOnUIHeroOverrideCardRemovedListener(callback, null);
	}

	// Token: 0x060010CF RID: 4303 RVA: 0x0005E63C File Offset: 0x0005C83C
	public bool RegisterOnUIHeroOverrideCardRemovedListener(CollectionManager.OnUIHeroOverrideCardRemovedCallback callback, object userData)
	{
		CollectionManager.OnUIHeroOverrideCardRemovedListener onUIHeroOverrideCardRemovedListener = new CollectionManager.OnUIHeroOverrideCardRemovedListener();
		onUIHeroOverrideCardRemovedListener.SetCallback(callback);
		onUIHeroOverrideCardRemovedListener.SetUserData(userData);
		if (this.m_onUIHeroOverrideCardRemovedListeners.Contains(onUIHeroOverrideCardRemovedListener))
		{
			return false;
		}
		this.m_onUIHeroOverrideCardRemovedListeners.Add(onUIHeroOverrideCardRemovedListener);
		return true;
	}

	// Token: 0x060010D0 RID: 4304 RVA: 0x0005E67A File Offset: 0x0005C87A
	public bool RemoveOnUIHeroOverrideCardRemovedListener(CollectionManager.OnUIHeroOverrideCardRemovedCallback callback)
	{
		return this.RemoveOnUIHeroOverrideCardRemovedListener(callback, null);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x0005E684 File Offset: 0x0005C884
	public bool RemoveOnUIHeroOverrideCardRemovedListener(CollectionManager.OnUIHeroOverrideCardRemovedCallback callback, object userData)
	{
		CollectionManager.OnUIHeroOverrideCardRemovedListener onUIHeroOverrideCardRemovedListener = new CollectionManager.OnUIHeroOverrideCardRemovedListener();
		onUIHeroOverrideCardRemovedListener.SetCallback(callback);
		onUIHeroOverrideCardRemovedListener.SetUserData(userData);
		return this.m_onUIHeroOverrideCardRemovedListeners.Remove(onUIHeroOverrideCardRemovedListener);
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x0005E6B1 File Offset: 0x0005C8B1
	public void RegisterOnInitialCollectionReceivedListener(Action callback)
	{
		if (!this.m_initialCollectionReceivedListeners.Contains(callback))
		{
			this.m_initialCollectionReceivedListeners.Add(callback);
		}
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x0005E6CD File Offset: 0x0005C8CD
	public void RemoveOnInitialCollectionReceivedListener(Action callback)
	{
		if (this.m_initialCollectionReceivedListeners.Contains(callback))
		{
			this.m_initialCollectionReceivedListeners.Remove(callback);
		}
	}

	// Token: 0x060010D4 RID: 4308 RVA: 0x0005E6EC File Offset: 0x0005C8EC
	public TAG_PREMIUM GetBestCardPremium(string cardID)
	{
		CollectibleCard collectibleCard = null;
		if (this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardID, TAG_PREMIUM.DIAMOND), out collectibleCard) && collectibleCard.OwnedCount > 0)
		{
			return TAG_PREMIUM.DIAMOND;
		}
		if (this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardID, TAG_PREMIUM.GOLDEN), out collectibleCard) && collectibleCard.OwnedCount > 0)
		{
			return TAG_PREMIUM.GOLDEN;
		}
		return TAG_PREMIUM.NORMAL;
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x0005E740 File Offset: 0x0005C940
	public CollectibleCard GetCard(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard result = null;
		this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardID, premium), out result);
		return result;
	}

	// Token: 0x060010D6 RID: 4310 RVA: 0x0005E768 File Offset: 0x0005C968
	public List<CollectibleCard> GetHeroesIOwn(TAG_CLASS heroClass)
	{
		string searchString = null;
		List<CollectibleCardFilter.FilterMask> filterMasks = null;
		int? manaCost = null;
		TAG_CARD_SET[] theseCardSets = null;
		TAG_CLASS[] theseClassTypes = null;
		TAG_CARDTYPE[] theseCardTypes = null;
		TAG_RARITY? rarity = null;
		TAG_RACE? race = null;
		int? minOwned = new int?(1);
		return this.FindCards(searchString, filterMasks, manaCost, theseCardSets, theseClassTypes, theseCardTypes, rarity, race, new bool?(true), minOwned, null, null, null, null, null, false, null, null, null).m_cards;
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x0005E7DC File Offset: 0x0005C9DC
	public List<CollectibleCard> GetBestHeroesIOwn(TAG_CLASS heroClass)
	{
		string searchString = null;
		List<CollectibleCardFilter.FilterMask> filterMasks = null;
		int? manaCost = null;
		TAG_CARD_SET[] theseCardSets = null;
		int? minOwned = new int?(1);
		bool? isHero = new bool?(true);
		List<CollectibleCard> cards = this.FindCards(searchString, filterMasks, manaCost, theseCardSets, new TAG_CLASS[]
		{
			heroClass
		}, null, null, null, isHero, minOwned, null, null, null, null, null, false, null, null, null).m_cards;
		IEnumerable<CollectibleCard> enumerable = from h in cards
		where h.PremiumType == TAG_PREMIUM.GOLDEN
		select h;
		IEnumerable<CollectibleCard> enumerable2 = from h in cards
		where h.PremiumType == TAG_PREMIUM.NORMAL
		select h;
		List<CollectibleCard> list = new List<CollectibleCard>();
		foreach (CollectibleCard item in enumerable)
		{
			list.Add(item);
		}
		using (IEnumerator<CollectibleCard> enumerator = enumerable2.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CollectibleCard heroCard = enumerator.Current;
				if (list.Find((CollectibleCard e) => e.CardDbId == heroCard.CardDbId) == null)
				{
					list.Add(heroCard);
				}
			}
		}
		return list;
	}

	// Token: 0x060010D8 RID: 4312 RVA: 0x0005E958 File Offset: 0x0005CB58
	public NetCache.CardDefinition GetFavoriteHero(TAG_CLASS heroClass)
	{
		NetCache.NetCacheFavoriteHeroes netObject = NetCache.Get().GetNetObject<NetCache.NetCacheFavoriteHeroes>();
		if (netObject == null)
		{
			return this.GetFavoriteHeroFromOfflineData(heroClass);
		}
		if (!netObject.FavoriteHeroes.ContainsKey(heroClass))
		{
			return null;
		}
		return netObject.FavoriteHeroes[heroClass];
	}

	// Token: 0x060010D9 RID: 4313 RVA: 0x0005E998 File Offset: 0x0005CB98
	private NetCache.CardDefinition GetFavoriteHeroFromOfflineData(TAG_CLASS heroClass)
	{
		FavoriteHero favoriteHero = OfflineDataCache.GetFavoriteHeroesFromCache().FirstOrDefault((FavoriteHero h) => h.ClassId == (int)heroClass);
		if (favoriteHero == null)
		{
			return null;
		}
		return new NetCache.CardDefinition
		{
			Name = GameUtils.TranslateDbIdToCardId(favoriteHero.Hero.Asset, false),
			Premium = (TAG_PREMIUM)favoriteHero.Hero.Premium
		};
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x0005E9FC File Offset: 0x0005CBFC
	public int GetCoreCardsIOwn(TAG_CLASS cardClass)
	{
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		if (netObject == null)
		{
			return 0;
		}
		return netObject.CoreCardsUnlockedPerClass[cardClass].Count;
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x0005EA2C File Offset: 0x0005CC2C
	public List<CollectibleCard> GetOwnedCards()
	{
		return this.FindCards(null, null, null, null, null, null, null, null, null, new int?(1), null, null, null, null, null, false, null, null, null).m_cards;
	}

	// Token: 0x060010DC RID: 4316 RVA: 0x0005EAA0 File Offset: 0x0005CCA0
	public void GetOwnedCardCount(string cardId, out int normal, out int golden, out int diamond)
	{
		normal = 0;
		golden = 0;
		diamond = 0;
		CollectibleCard collectibleCard = null;
		if (this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.NORMAL), out collectibleCard))
		{
			normal += collectibleCard.OwnedCount;
		}
		if (this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.GOLDEN), out collectibleCard))
		{
			golden += collectibleCard.OwnedCount;
		}
		if (this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.DIAMOND), out collectibleCard))
		{
			diamond += collectibleCard.OwnedCount;
		}
	}

	// Token: 0x060010DD RID: 4317 RVA: 0x0005EB20 File Offset: 0x0005CD20
	public int GetOwnedCardCountByFilterMask(string cardId, CollectibleCardFilter.FilterMask filterMask)
	{
		int num = 0;
		CollectibleCard collectibleCard = null;
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_NORMAL) != CollectibleCardFilter.FilterMask.NONE && this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.NORMAL), out collectibleCard))
		{
			num += collectibleCard.OwnedCount;
		}
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_GOLDEN) != CollectibleCardFilter.FilterMask.NONE && this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.GOLDEN), out collectibleCard))
		{
			num += collectibleCard.OwnedCount;
		}
		if ((filterMask & CollectibleCardFilter.FilterMask.PREMIUM_DIAMOND) != CollectibleCardFilter.FilterMask.NONE && this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardId, TAG_PREMIUM.DIAMOND), out collectibleCard))
		{
			num += collectibleCard.OwnedCount;
		}
		return num;
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x0005EB9E File Offset: 0x0005CD9E
	public List<TAG_CARD_SET> GetDisplayableCardSets()
	{
		return this.m_displayableCardSets;
	}

	// Token: 0x060010DF RID: 4319 RVA: 0x0005EBA8 File Offset: 0x0005CDA8
	public List<CollectibleCard> GetChangedCards(List<CardChange.ChangeType> changeTypes, TAG_PREMIUM premium, string featuredCardsEventTiming = null)
	{
		List<CardChangeDbfRecord> list = new List<CardChangeDbfRecord>();
		foreach (CardChangeDbfRecord cardChangeDbfRecord in GameDbf.CardChange.GetRecords())
		{
			if (changeTypes.Contains(cardChangeDbfRecord.ChangeType))
			{
				CardDbfRecord record = GameDbf.Card.GetRecord(cardChangeDbfRecord.CardId);
				if (record != null && (featuredCardsEventTiming == null || !(record.FeaturedCardsEvent != featuredCardsEventTiming)) && (featuredCardsEventTiming != null || string.IsNullOrEmpty(record.FeaturedCardsEvent)))
				{
					list.Add(cardChangeDbfRecord);
				}
			}
		}
		list.Sort((CardChangeDbfRecord a, CardChangeDbfRecord b) => a.SortOrder - b.SortOrder);
		List<CollectibleCard> list2 = new List<CollectibleCard>();
		foreach (CardChangeDbfRecord cardChangeDbfRecord2 in list)
		{
			CollectibleCard card = this.GetCard(GameUtils.TranslateDbIdToCardId(cardChangeDbfRecord2.CardId, false), premium);
			if (card != null && !list2.Contains(card) && !ChangedCardMgr.Get().HasSeenCardChange(card.ChangeVersion, card.CardDbId))
			{
				list2.Add(card);
			}
		}
		return list2;
	}

	// Token: 0x060010E0 RID: 4320 RVA: 0x0005ECF8 File Offset: 0x0005CEF8
	public bool IsCardInCollection(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard collectibleCard = null;
		return this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardID, premium), out collectibleCard) && collectibleCard.OwnedCount > 0;
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x0005ED28 File Offset: 0x0005CF28
	public int GetNumCopiesInCollection(string cardID, TAG_PREMIUM premium)
	{
		CollectibleCard collectibleCard = null;
		this.m_collectibleCardIndex.TryGetValue(new CollectionManager.CollectibleCardIndex(cardID, premium), out collectibleCard);
		if (collectibleCard == null)
		{
			return 0;
		}
		return collectibleCard.OwnedCount;
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x0005ED58 File Offset: 0x0005CF58
	public List<CollectibleCard> GetMassDisenchantCards()
	{
		List<CollectibleCard> list = new List<CollectibleCard>();
		foreach (CollectibleCard collectibleCard in this.GetOwnedCards())
		{
			if (collectibleCard.DisenchantCount > 0)
			{
				list.Add(collectibleCard);
			}
		}
		return list;
	}

	// Token: 0x060010E3 RID: 4323 RVA: 0x0005EDBC File Offset: 0x0005CFBC
	public int GetCardsToDisenchantCount()
	{
		int num = 0;
		foreach (CollectibleCard collectibleCard in this.GetMassDisenchantCards())
		{
			num += collectibleCard.DisenchantCount;
		}
		return num;
	}

	// Token: 0x060010E4 RID: 4324 RVA: 0x0005EE14 File Offset: 0x0005D014
	public void MarkAllInstancesAsSeen(string cardID, TAG_PREMIUM premium)
	{
		NetCache.NetCacheCollection netObject = NetCache.Get().GetNetObject<NetCache.NetCacheCollection>();
		int num = GameUtils.TranslateCardIdToDbId(cardID, false);
		if (num == 0)
		{
			return;
		}
		CollectibleCard card = this.GetCard(cardID, premium);
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
		foreach (CollectionManager.DelOnNewCardSeen delOnNewCardSeen in this.m_newCardSeenListeners)
		{
			delOnNewCardSeen(cardID, premium);
		}
	}

	// Token: 0x060010E5 RID: 4325 RVA: 0x0005EEFC File Offset: 0x0005D0FC
	public void OnCardAdded(string cardID, TAG_PREMIUM premium, int count, bool seenBefore)
	{
		this.InsertNewCollectionCard(cardID, premium, DateTime.Now, count, seenBefore);
		this.OnCollectionChanged();
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x0005EF14 File Offset: 0x0005D114
	public void OnCardRemoved(string cardID, TAG_PREMIUM premium, int count)
	{
		this.RemoveCollectionCard(cardID, premium, count);
		this.OnCollectionChanged();
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x0005EF28 File Offset: 0x0005D128
	public void OnUIHeroOverrideCardRemoved()
	{
		if (this.m_onUIHeroOverrideCardRemovedListeners.Count > 0)
		{
			CollectionManager.OnUIHeroOverrideCardRemovedListener[] array = this.m_onUIHeroOverrideCardRemovedListeners.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Fire();
			}
		}
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x0005EF65 File Offset: 0x0005D165
	public CollectionManager.PreconDeck GetPreconDeck(TAG_CLASS heroClass)
	{
		if (!this.m_preconDecks.ContainsKey(heroClass))
		{
			global::Log.All.PrintWarning(string.Format("CollectionManager.GetPreconDeck(): Could not retrieve precon deck for class {0}", heroClass), Array.Empty<object>());
			return null;
		}
		return this.m_preconDecks[heroClass];
	}

	// Token: 0x060010E9 RID: 4329 RVA: 0x0005EFA4 File Offset: 0x0005D1A4
	public List<CollectionDeck> GetPreconCollectionDecks()
	{
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (CollectionManager.PreconDeck preconDeck in this.m_preconDecks.Values)
		{
			list.Add(this.m_decks[preconDeck.ID]);
		}
		return list;
	}

	// Token: 0x060010EA RID: 4330 RVA: 0x0005F014 File Offset: 0x0005D214
	public SortedDictionary<long, CollectionDeck> GetDecks()
	{
		SortedDictionary<long, CollectionDeck> sortedDictionary = new SortedDictionary<long, CollectionDeck>();
		foreach (KeyValuePair<long, CollectionDeck> keyValuePair in this.m_decks)
		{
			CollectionDeck value = keyValuePair.Value;
			if (value != null && (!value.IsBrawlDeck || TavernBrawlManager.Get().IsSeasonActive(value.Type, value.SeasonId, value.BrawlLibraryItemId)))
			{
				sortedDictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
		return sortedDictionary;
	}

	// Token: 0x060010EB RID: 4331 RVA: 0x0005F0AC File Offset: 0x0005D2AC
	public List<CollectionDeck> GetDecks(DeckType deckType)
	{
		if (!NetCache.Get().IsNetObjectAvailable<NetCache.NetCacheDecks>())
		{
			Debug.LogWarning("Attempting to get decks from CollectionManager, even though NetCacheDecks is not ready (meaning it's waiting for the decks to be updated)!");
		}
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (CollectionDeck collectionDeck in this.m_decks.Values)
		{
			if (collectionDeck.Type == deckType && (!collectionDeck.IsBrawlDeck || TavernBrawlManager.Get().IsSeasonActive(collectionDeck.Type, collectionDeck.SeasonId, collectionDeck.BrawlLibraryItemId)))
			{
				list.Add(collectionDeck);
			}
		}
		list.Sort(new CollectionManager.DeckSort());
		return list;
	}

	// Token: 0x060010EC RID: 4332 RVA: 0x0005F15C File Offset: 0x0005D35C
	public List<CollectionDeck> GetDecksWithClass(TAG_CLASS classType, DeckType deckType)
	{
		List<CollectionDeck> decks = this.GetDecks(deckType);
		List<CollectionDeck> list = new List<CollectionDeck>();
		foreach (CollectionDeck collectionDeck in decks)
		{
			if (collectionDeck.GetClass() == classType)
			{
				list.Add(collectionDeck);
			}
		}
		return list;
	}

	// Token: 0x060010ED RID: 4333 RVA: 0x0005F1C0 File Offset: 0x0005D3C0
	public List<long> LoadDeckFromDBF(int deckID, out string deckName, out string deckDescription)
	{
		deckName = string.Empty;
		deckDescription = string.Empty;
		DeckDbfRecord record = GameDbf.Deck.GetRecord(deckID);
		if (record == null)
		{
			Debug.LogError(string.Format("Unable to find deck with ID {0}", deckID));
			return null;
		}
		if (record.Name == null)
		{
			Debug.LogErrorFormat("Deck with ID {0} has no name defined.", new object[]
			{
				deckID
			});
		}
		else
		{
			deckName = record.Name.GetString(true);
		}
		if (record.Description != null)
		{
			deckDescription = record.Description.GetString(true);
		}
		List<long> list = new List<long>();
		DeckCardDbfRecord deckCardDbfRecord = GameDbf.DeckCard.GetRecord(record.TopCardId);
		while (deckCardDbfRecord != null)
		{
			int cardId = deckCardDbfRecord.CardId;
			list.Add((long)cardId);
			int nextCard = deckCardDbfRecord.NextCard;
			if (nextCard == 0)
			{
				deckCardDbfRecord = null;
			}
			else
			{
				deckCardDbfRecord = GameDbf.DeckCard.GetRecord(nextCard);
			}
		}
		return list;
	}

	// Token: 0x060010EE RID: 4334 RVA: 0x0005F294 File Offset: 0x0005D494
	public CollectionDeck GetDeck(long id)
	{
		CollectionDeck collectionDeck;
		if (!this.m_decks.TryGetValue(id, out collectionDeck))
		{
			return null;
		}
		if (collectionDeck != null && collectionDeck.IsBrawlDeck && !TavernBrawlManager.Get().IsSeasonActive(collectionDeck.Type, collectionDeck.SeasonId, collectionDeck.BrawlLibraryItemId))
		{
			return null;
		}
		return collectionDeck;
	}

	// Token: 0x060010EF RID: 4335 RVA: 0x0005F2E0 File Offset: 0x0005D4E0
	public CollectionDeck GetDuelsDeck()
	{
		List<CollectionDeck> decks = this.GetDecks(DeckType.PVPDR_DECK);
		if (decks != null)
		{
			for (int i = 0; i < decks.Count; i++)
			{
				if (decks[i].ID == this.m_currentPVPDRDeckId && !decks[i].IsBeingDeleted())
				{
					return decks[i];
				}
			}
		}
		return null;
	}

	// Token: 0x060010F0 RID: 4336 RVA: 0x0005F334 File Offset: 0x0005D534
	public bool AreAllDeckContentsReady()
	{
		if (!FixedRewardsMgr.Get().IsStartupFinished())
		{
			return false;
		}
		return this.m_decks.FirstOrDefault((KeyValuePair<long, CollectionDeck> kv) => !kv.Value.NetworkContentsLoaded() && !kv.Value.IsBrawlDeck && !kv.Value.IsDuelsDeck).Value == null;
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x0005F386 File Offset: 0x0005D586
	public bool ShouldAccountSeeStandardWild()
	{
		return RankMgr.Get().WildCardsAllowedInCurrentLeague() && (this.AccountEverHadWildCards() || this.AccountHasRotatedItems());
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0005F3AC File Offset: 0x0005D5AC
	public bool ShouldAccountSeeStandardComingSoon()
	{
		CollectionManager.<>c__DisplayClass208_0 CS$<>8__locals1 = new CollectionManager.<>c__DisplayClass208_0();
		if (SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, true))
		{
			return false;
		}
		if (this.m_showStandardComingSoonNotice)
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
		if (this.AccountHasRotatedItems() || this.AccountEverHadWildCards())
		{
			return false;
		}
		CS$<>8__locals1.upcomingSetRotationStartTime = SpecialEventManager.Get().GetEventStartTimeUtc(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021);
		if (CS$<>8__locals1.upcomingSetRotationStartTime == null)
		{
			return false;
		}
		CollectionManager.<>c__DisplayClass208_0 CS$<>8__locals2 = CS$<>8__locals1;
		DateTime? upcomingSetRotationStartTime = CS$<>8__locals1.upcomingSetRotationStartTime;
		TimeSpan t = new TimeSpan(0, 0, 1);
		CS$<>8__locals2.upcomingSetRotationStartTime = upcomingSetRotationStartTime + t;
		if (this.AccountHasRotatedItems(CS$<>8__locals1.upcomingSetRotationStartTime.Value))
		{
			this.m_showStandardComingSoonNotice = true;
			return true;
		}
		if (this.m_collectibleCards.Any((CollectibleCard c) => c.OwnedCount > 0 && GameUtils.IsCardRotated(c.GetEntityDef(), CS$<>8__locals1.upcomingSetRotationStartTime.Value)))
		{
			this.m_showStandardComingSoonNotice = true;
			return true;
		}
		return false;
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x0005F4A0 File Offset: 0x0005D6A0
	public bool AccountHasRotatedItems()
	{
		if (this.m_accountHasRotatedItems)
		{
			return true;
		}
		bool flag = this.AccountHasRotatedItems(DateTime.UtcNow);
		if (flag)
		{
			this.m_accountHasRotatedItems = true;
		}
		return flag;
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0005F4C4 File Offset: 0x0005D6C4
	public bool AccountHasRotatedBoosters(DateTime utcTimestamp)
	{
		NetCache.NetCacheBoosters netObject = NetCache.Get().GetNetObject<NetCache.NetCacheBoosters>();
		if (netObject != null)
		{
			using (List<NetCache.BoosterStack>.Enumerator enumerator = netObject.BoosterStacks.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (GameUtils.IsBoosterRotated((BoosterDbId)enumerator.Current.Id, utcTimestamp))
					{
						return true;
					}
				}
			}
			return false;
		}
		return false;
	}

	// Token: 0x060010F5 RID: 4341 RVA: 0x0005F534 File Offset: 0x0005D734
	private bool AccountHasRotatedItems(DateTime utcTimestamp)
	{
		if (this.AccountHasRotatedBoosters(utcTimestamp))
		{
			return true;
		}
		foreach (AdventureMission.WingProgress wingProgress in AdventureProgressMgr.Get().GetAllProgress())
		{
			if (wingProgress.IsOwned())
			{
				WingDbfRecord record = GameDbf.Wing.GetRecord(wingProgress.Wing);
				if (record != null && GameUtils.IsAdventureRotated((AdventureDbId)record.AdventureId, utcTimestamp))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060010F6 RID: 4342 RVA: 0x0005F5C0 File Offset: 0x0005D7C0
	public bool AccountEverHadWildCards()
	{
		if (this.m_accountEverHadWildCards)
		{
			return true;
		}
		this.m_accountEverHadWildCards = (SetRotationManager.HasSeenStandardModeTutorial() || this.AccountHasWildCards());
		return this.m_accountEverHadWildCards;
	}

	// Token: 0x060010F7 RID: 4343 RVA: 0x0005F5E8 File Offset: 0x0005D7E8
	public bool AccountHasWildCards()
	{
		if (this.GetNumberOfWildDecks() > 0)
		{
			return true;
		}
		if (this.m_lastSearchForWildCardsTime > this.m_collectionLastModifiedTime)
		{
			return this.m_accountHasWildCards;
		}
		this.m_accountHasWildCards = this.m_collectibleCards.Any((CollectibleCard c) => c.OwnedCount > 0 && GameUtils.IsCardRotated(c.GetEntityDef()));
		this.m_lastSearchForWildCardsTime = Time.realtimeSinceStartup;
		return this.m_accountHasWildCards;
	}

	// Token: 0x060010F8 RID: 4344 RVA: 0x0005F656 File Offset: 0x0005D856
	public int GetNumberOfWildDecks()
	{
		return this.m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_WILD);
	}

	// Token: 0x060010F9 RID: 4345 RVA: 0x0005F687 File Offset: 0x0005D887
	public int GetNumberOfStandardDecks()
	{
		return this.m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_STANDARD);
	}

	// Token: 0x060010FA RID: 4346 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
	public int GetNumberOfClassicDecks()
	{
		return this.m_decks.Values.Count((CollectionDeck deck) => deck.FormatType == FormatType.FT_CLASSIC);
	}

	// Token: 0x060010FB RID: 4347 RVA: 0x0005F6EC File Offset: 0x0005D8EC
	public bool AccountHasValidDeck(FormatType formatType)
	{
		foreach (CollectionDeck collectionDeck in CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK))
		{
			if (collectionDeck.IsValidForRuleset && collectionDeck.FormatType == formatType)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060010FC RID: 4348 RVA: 0x0005F758 File Offset: 0x0005D958
	public bool AccountHasAnyValidDeck()
	{
		using (List<CollectionDeck>.Enumerator enumerator = CollectionManager.Get().GetDecks(DeckType.NORMAL_DECK).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.IsValidForRuleset)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x060010FD RID: 4349 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
	public CollectionDeck GetEditedDeck()
	{
		CollectionDeck editedDeck = this.m_EditedDeck;
		if (editedDeck != null && editedDeck.IsBrawlDeck)
		{
			TavernBrawlManager tavernBrawlManager = TavernBrawlManager.Get();
			if (tavernBrawlManager != null)
			{
				TavernBrawlMission tavernBrawlMission = tavernBrawlManager.IsCurrentBrawlTypeActive ? tavernBrawlManager.CurrentMission() : null;
				if (tavernBrawlMission == null || editedDeck.SeasonId != tavernBrawlMission.seasonId)
				{
					return null;
				}
			}
		}
		return editedDeck;
	}

	// Token: 0x060010FE RID: 4350 RVA: 0x0005F806 File Offset: 0x0005DA06
	public int GetDeckSize()
	{
		if (this.m_deckRuleset == null)
		{
			return 30;
		}
		return this.m_deckRuleset.GetDeckSize(this.GetEditedDeck());
	}

	// Token: 0x060010FF RID: 4351 RVA: 0x0005F824 File Offset: 0x0005DA24
	public List<CollectionManager.TemplateDeck> GetTemplateDecks(FormatType formatType, TAG_CLASS classType)
	{
		if (this.m_templateDeckMap.Values.Count == 0)
		{
			this.LoadTemplateDecks();
		}
		List<CollectionManager.TemplateDeck> source = null;
		this.m_templateDecks.TryGetValue(classType, out source);
		if (formatType == FormatType.FT_WILD)
		{
			return (from x in source
			where x.m_formatType == FormatType.FT_STANDARD || x.m_formatType == FormatType.FT_WILD
			select x).ToList<CollectionManager.TemplateDeck>();
		}
		return (from x in source
		where x.m_formatType == formatType
		select x).ToList<CollectionManager.TemplateDeck>();
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x0005F8B4 File Offset: 0x0005DAB4
	public List<CollectionManager.TemplateDeck> GetNonStarterTemplateDecks(FormatType formatType, TAG_CLASS classType)
	{
		List<CollectionManager.TemplateDeck> templateDecks = this.GetTemplateDecks(formatType, classType);
		if (templateDecks == null)
		{
			return null;
		}
		return (from x in templateDecks
		where !x.m_isStarterDeck
		select x).ToList<CollectionManager.TemplateDeck>();
	}

	// Token: 0x06001101 RID: 4353 RVA: 0x0005F8FC File Offset: 0x0005DAFC
	public CollectionManager.TemplateDeck GetTemplateDeck(int id)
	{
		if (this.m_templateDeckMap.Values.Count == 0)
		{
			this.LoadTemplateDecks();
		}
		CollectionManager.TemplateDeck result;
		this.m_templateDeckMap.TryGetValue(id, out result);
		return result;
	}

	// Token: 0x06001102 RID: 4354 RVA: 0x0005F931 File Offset: 0x0005DB31
	public bool IsInEditMode()
	{
		return this.m_editMode;
	}

	// Token: 0x06001103 RID: 4355 RVA: 0x0005F93C File Offset: 0x0005DB3C
	public void StartEditingDeck(CollectionDeck deck, object callbackData = null)
	{
		if (deck == null)
		{
			return;
		}
		this.m_editMode = true;
		global::DeckRuleset deckRuleset;
		if (SceneMgr.Get().IsInTavernBrawlMode())
		{
			deckRuleset = TavernBrawlManager.Get().GetCurrentDeckRuleset();
		}
		else if (SceneMgr.Get().GetMode() == SceneMgr.Mode.PVP_DUNGEON_RUN)
		{
			if (deck.Type == DeckType.PVPDR_DECK)
			{
				deckRuleset = global::DeckRuleset.GetPVPDRRuleset();
			}
			else
			{
				deckRuleset = global::DeckRuleset.GetPVPDRDisplayRuleset();
			}
		}
		else
		{
			deckRuleset = global::DeckRuleset.GetRuleset(deck.FormatType);
			PresenceMgr.Get().SetStatus(new Enum[]
			{
				Global.PresenceStatus.DECKEDITOR
			});
		}
		this.SetDeckRuleset(deckRuleset);
		this.SetEditedDeck(deck, callbackData);
	}

	// Token: 0x06001104 RID: 4356 RVA: 0x0005F9CA File Offset: 0x0005DBCA
	public void DoneEditing()
	{
		bool editMode = this.m_editMode;
		this.m_editMode = false;
		if (editMode && SceneMgr.Get() != null && !SceneMgr.Get().IsInTavernBrawlMode())
		{
			PresenceMgr.Get().SetPrevStatus();
		}
		this.SetDeckRuleset(null);
		this.ClearEditedDeck();
	}

	// Token: 0x06001105 RID: 4357 RVA: 0x0005FA06 File Offset: 0x0005DC06
	public global::DeckRuleset GetDeckRuleset()
	{
		return this.m_deckRuleset;
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x0005FA10 File Offset: 0x0005DC10
	public FormatType GetThemeShowing(CollectionDeck deck = null)
	{
		if (CollectionManagerDisplay.IsSpecialOneDeckMode())
		{
			return FormatType.FT_STANDARD;
		}
		if (deck == null)
		{
			deck = this.GetEditedDeck();
		}
		if (deck != null && deck.Type != DeckType.CLIENT_ONLY_DECK)
		{
			return deck.FormatType;
		}
		if (this.m_collectibleDisplay != null && this.m_collectibleDisplay.SetFilterTrayInitialized())
		{
			if (this.m_collectibleDisplay.m_pageManager.CardSetFilterIncludesWild())
			{
				return FormatType.FT_WILD;
			}
			if (this.m_collectibleDisplay.m_pageManager.CardSetFilterIsClassic())
			{
				return FormatType.FT_CLASSIC;
			}
		}
		return FormatType.FT_STANDARD;
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x0005FA88 File Offset: 0x0005DC88
	public FormatType GetThemeShowingForCollectionPage(CollectionDeck deck = null)
	{
		if (SceneMgr.Get().IsInTavernBrawlMode() || SceneMgr.Get().IsInDuelsMode())
		{
			return FormatType.FT_STANDARD;
		}
		if (deck == null)
		{
			deck = this.GetEditedDeck();
		}
		if (deck != null && deck.Type != DeckType.CLIENT_ONLY_DECK)
		{
			return deck.FormatType;
		}
		if (this.m_collectibleDisplay != null && this.m_collectibleDisplay.SetFilterTrayInitialized())
		{
			if (this.m_collectibleDisplay.m_pageManager.CardSetFilterIncludesWild())
			{
				return FormatType.FT_WILD;
			}
			if (this.m_collectibleDisplay.m_pageManager.CardSetFilterIsClassic())
			{
				return FormatType.FT_CLASSIC;
			}
		}
		return FormatType.FT_STANDARD;
	}

	// Token: 0x06001108 RID: 4360 RVA: 0x0005FB11 File Offset: 0x0005DD11
	public void SetDeckRuleset(global::DeckRuleset deckRuleset)
	{
		this.m_deckRuleset = deckRuleset;
		if (this.m_collectibleDisplay != null)
		{
			this.m_collectibleDisplay.m_pageManager.SetDeckRuleset(deckRuleset, false);
		}
	}

	// Token: 0x06001109 RID: 4361 RVA: 0x0005FB3C File Offset: 0x0005DD3C
	public CollectionDeck SetEditedDeck(long deckId, object callbackData = null)
	{
		CollectionDeck collectionDeck = null;
		this.m_decks.TryGetValue(deckId, out collectionDeck);
		this.SetEditedDeck(collectionDeck, callbackData);
		return collectionDeck;
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x0005FB64 File Offset: 0x0005DD64
	public void SetEditedDeck(CollectionDeck deck, object callbackData = null)
	{
		CollectionDeck editedDeck = this.GetEditedDeck();
		if (deck == editedDeck)
		{
			return;
		}
		this.m_EditedDeck = deck;
		CollectionManager.OnEditedDeckChanged[] array = this.m_editedDeckChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](deck, editedDeck, callbackData);
		}
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x0005FBA9 File Offset: 0x0005DDA9
	public void ClearEditedDeck()
	{
		this.SetEditedDeck(null, null);
	}

	// Token: 0x0600110C RID: 4364 RVA: 0x0005FBB4 File Offset: 0x0005DDB4
	public void SendCreateDeck(DeckType deckType, string name, string heroCardID, DeckSourceType deckSourceType = DeckSourceType.DECK_SOURCE_TYPE_NORMAL, string pastedDeckHashString = null)
	{
		int num = GameUtils.TranslateCardIdToDbId(heroCardID, false);
		if (num == 0)
		{
			Debug.LogWarning(string.Format("CollectionManager.SendCreateDeck(): Unknown hero cardID {0}", heroCardID));
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
			Debug.LogWarning(string.Format("CollectionManager.SendCreateDeck(): Bad format type {0}", formatType.ToString()));
			return;
		}
		if (deckType - DeckType.TAVERN_BRAWL_DECK <= 1)
		{
			brawlLibraryItemId = TavernBrawlManager.Get().CurrentMission().SelectedBrawlLibraryItemId;
		}
		TAG_PREMIUM bestCardPremium = this.GetBestCardPremium(heroCardID);
		if (this.m_pendingDeckCreate != null)
		{
			global::Log.Offline.PrintWarning("SendCreateDeck - Attempting to create a deck while another is still pending.", Array.Empty<object>());
		}
		this.m_pendingDeckCreate = new CollectionManager.PendingDeckCreateData
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
			int? num2;
			Network.Get().CreateDeck(deckType, name, num, bestCardPremium, formatType, -100L, deckSourceType, out num2, pastedDeckHashString, brawlLibraryItemId);
			if (num2 != null)
			{
				this.m_inTransitDeckCreateRequests.Add(num2.Value);
				return;
			}
		}
		else
		{
			this.CreateDeckOffline(this.m_pendingDeckCreate);
		}
	}

	// Token: 0x0600110D RID: 4365 RVA: 0x0005FCE8 File Offset: 0x0005DEE8
	private void CreateDeckOffline(CollectionManager.PendingDeckCreateData data)
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
			CollectionManagerDisplay collectionManagerDisplay = this.m_collectibleDisplay as CollectionManagerDisplay;
			if (collectionManagerDisplay != null)
			{
				collectionManagerDisplay.CancelSelectNewDeckHeroMode();
			}
			if (CollectionDeckTray.Get() != null)
			{
				CollectionDeckTray.Get().m_doneButton.SetEnabled(true, false);
			}
			return;
		}
		NetCache.DeckHeader deckHeader = Network.GetDeckHeaderFromDeckInfo(deckInfo);
		Processor.ScheduleCallback(0.5f, false, delegate(object _0)
		{
			this.OnDeckCreated(deckHeader, null);
		}, null);
	}

	// Token: 0x0600110E RID: 4366 RVA: 0x0005FDD8 File Offset: 0x0005DFD8
	public void HandleDisconnect()
	{
		if (this.m_pendingDeckCreate != null)
		{
			this.CreateDeckOffline(this.m_pendingDeckCreate);
			this.m_pendingDeckCreate = null;
		}
		if (this.m_pendingDeckDeleteList != null)
		{
			foreach (CollectionManager.PendingDeckDeleteData pendingDeckDeleteData in this.m_pendingDeckDeleteList.ToArray())
			{
				this.OnDeckDeletedWhileOffline(pendingDeckDeleteData.m_deckId);
			}
			this.m_pendingDeckDeleteList = null;
		}
		if (this.m_pendingDeckEditList != null)
		{
			foreach (CollectionManager.PendingDeckEditData pendingDeckEditData in this.m_pendingDeckEditList)
			{
				CollectionDeck deck = this.GetDeck(pendingDeckEditData.m_deckId);
				if (deck != null)
				{
					deck.OnContentChangesComplete();
				}
			}
			this.m_pendingDeckEditList = null;
		}
		if (this.m_pendingDeckRenameList != null)
		{
			foreach (CollectionManager.PendingDeckRenameData pendingDeckRenameData in this.m_pendingDeckRenameList)
			{
				CollectionDeck deck2 = this.GetDeck(pendingDeckRenameData.m_deckId);
				if (deck2 != null)
				{
					OfflineDataCache.RenameDeck(pendingDeckRenameData.m_deckId, pendingDeckRenameData.m_name);
					deck2.OnNameChangeComplete();
				}
			}
			this.m_pendingDeckRenameList = null;
		}
	}

	// Token: 0x0600110F RID: 4367 RVA: 0x0005FF1C File Offset: 0x0005E11C
	public bool RequestDeckContentsForDecksWithoutContentsLoaded(CollectionManager.DelOnAllDeckContents callback = null)
	{
		float now = Time.realtimeSinceStartup;
		IEnumerable<KeyValuePair<long, CollectionDeck>> source = from kv in this.m_decks
		where !kv.Value.NetworkContentsLoaded()
		select kv;
		source = from kv in source
		where !kv.Value.IsBrawlDeck || TavernBrawlManager.Get().IsTavernBrawlActiveByDeckType(kv.Value.Type)
		select kv;
		if (!source.Any<KeyValuePair<long, CollectionDeck>>())
		{
			if (callback != null)
			{
				callback();
			}
			return false;
		}
		if (callback != null && !this.m_allDeckContentsListeners.Contains(callback))
		{
			this.m_allDeckContentsListeners.Add(callback);
		}
		if (this.m_pendingRequestDeckContents != null)
		{
			source = from kv in source
			where !this.m_pendingRequestDeckContents.ContainsKey(kv.Value.ID) || now - this.m_pendingRequestDeckContents[kv.Value.ID] >= 10f
			select kv;
		}
		IEnumerable<long> source2 = from kv in source
		select kv.Value.ID;
		if (source2.Any<long>())
		{
			long[] array = source2.ToArray<long>();
			if (this.m_pendingRequestDeckContents == null)
			{
				this.m_pendingRequestDeckContents = new global::Map<long, float>();
			}
			for (int i = 0; i < array.Length; i++)
			{
				this.m_pendingRequestDeckContents[array[i]] = now;
			}
			Network.Get().RequestDeckContents(array);
			return true;
		}
		return true;
	}

	// Token: 0x06001110 RID: 4368 RVA: 0x0006005C File Offset: 0x0005E25C
	public void RequestDeckContents(long id)
	{
		CollectionDeck deck = this.GetDeck(id);
		if (deck != null && deck.NetworkContentsLoaded())
		{
			this.FireDeckContentsEvent(id);
			return;
		}
		if (Network.IsLoggedIn())
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float num;
			if (this.m_pendingRequestDeckContents != null && this.m_pendingRequestDeckContents.TryGetValue(id, out num))
			{
				if (realtimeSinceStartup - num < 10f)
				{
					return;
				}
				this.m_pendingRequestDeckContents.Remove(id);
			}
			if (this.m_pendingRequestDeckContents == null)
			{
				this.m_pendingRequestDeckContents = new global::Map<long, float>();
			}
			this.m_pendingRequestDeckContents[id] = realtimeSinceStartup;
			Network.Get().RequestDeckContents(new long[]
			{
				id
			});
			return;
		}
		this.OnGetDeckContentsResponse();
	}

	// Token: 0x06001111 RID: 4369 RVA: 0x000600FC File Offset: 0x0005E2FC
	public CollectionDeck GetBaseDeck(long id)
	{
		CollectionDeck result;
		if (this.m_baseDecks.TryGetValue(id, out result))
		{
			return result;
		}
		return null;
	}

	// Token: 0x06001112 RID: 4370 RVA: 0x0006011C File Offset: 0x0005E31C
	public string AutoGenerateDeckName(TAG_CLASS classTag)
	{
		string className = GameStrings.GetClassName(classTag);
		int num = 1;
		string text;
		do
		{
			text = GameStrings.Format("GLUE_COLLECTION_CUSTOM_DECKNAME_TEMPLATE", new object[]
			{
				className,
				(num == 1) ? "" : num.ToString()
			});
			if (text.Length > CollectionDeck.DefaultMaxDeckNameCharacters)
			{
				text = GameStrings.Format("GLUE_COLLECTION_CUSTOM_DECKNAME_SHORT", new object[]
				{
					className,
					(num == 1) ? "" : num.ToString()
				});
			}
			num++;
		}
		while (this.IsDeckNameTaken(text));
		return text;
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000601A4 File Offset: 0x0005E3A4
	public void AutoFillDeck(CollectionDeck deck, bool allowSmartDeckCompletion, CollectionManager.DeckAutoFillCallback resultCallback)
	{
		if (!NetCache.Get().GetNetObject<NetCache.NetCacheFeatures>().EnableSmartDeckCompletion)
		{
			allowSmartDeckCompletion = false;
		}
		if (this.m_smartDeckCallbackByDeckId.ContainsKey(deck.ID))
		{
			global::Log.CollectionManager.PrintError("AutoFillDeck was called on Deck ID={0} multiple times before a response was received.", new object[]
			{
				deck.ID
			});
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
			this.m_smartDeckCallbackByDeckId.Add(deck.ID, resultCallback);
			Network.Get().RequestSmartDeckCompletion(deck);
			Processor.ScheduleCallback(5f, true, new Processor.ScheduledCallback(this.OnSmartDeckTimeout), deck.ID);
			return;
		}
		resultCallback(deck, DeckMaker.GetFillCards(deck, deck.GetRuleset()));
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x00060274 File Offset: 0x0005E474
	private void OnSmartDeckTimeout(object userdata)
	{
		long num = (long)userdata;
		if (!this.m_smartDeckCallbackByDeckId.ContainsKey(num))
		{
			return;
		}
		CollectionDeck deck = this.GetDeck(num);
		IEnumerable<DeckMaker.DeckFill> fillCards = DeckMaker.GetFillCards(deck, deck.GetRuleset());
		this.m_smartDeckCallbackByDeckId[num](deck, fillCards);
		this.m_smartDeckCallbackByDeckId.Remove(num);
	}

	// Token: 0x06001115 RID: 4373 RVA: 0x000602CC File Offset: 0x0005E4CC
	private void OnSmartDeckResponse()
	{
		SmartDeckResponse smartDeckResponse = Network.Get().GetSmartDeckResponse();
		if (smartDeckResponse.HasErrorCode && smartDeckResponse.ErrorCode != ErrorCode.ERROR_OK)
		{
			global::Log.CollectionManager.PrintError("OnSmartDeckResponse: Response contained errors. ErrorCode=" + smartDeckResponse.ErrorCode, Array.Empty<object>());
			if (smartDeckResponse.ResponseMessage != null)
			{
				this.OnSmartDeckTimeout(smartDeckResponse.ResponseMessage.DeckId);
			}
		}
		if (smartDeckResponse.ResponseMessage == null)
		{
			return;
		}
		long deckId = smartDeckResponse.ResponseMessage.DeckId;
		Processor.CancelScheduledCallback(new Processor.ScheduledCallback(this.OnSmartDeckTimeout), deckId);
		if (!this.m_smartDeckCallbackByDeckId.ContainsKey(deckId))
		{
			return;
		}
		CollectionDeck deck = this.GetDeck(deckId);
		List<DeckMaker.DeckFill> cardFillFromSmartDeckResponse = this.GetCardFillFromSmartDeckResponse(deck, smartDeckResponse);
		this.m_smartDeckCallbackByDeckId[deckId](deck, cardFillFromSmartDeckResponse);
		this.m_smartDeckCallbackByDeckId.Remove(deckId);
	}

	// Token: 0x06001116 RID: 4374 RVA: 0x000603A4 File Offset: 0x0005E5A4
	private List<DeckMaker.DeckFill> GetCardFillFromSmartDeckResponse(CollectionDeck deck, SmartDeckResponse response)
	{
		global::Log.CollectionManager.PrintDebug("Smart Deck Response Received: " + response.ToHumanReadableString(), Array.Empty<object>());
		List<DeckMaker.DeckFill> list = new List<DeckMaker.DeckFill>();
		foreach (DeckCardData deckCardData in response.ResponseMessage.PlayerDeckCard)
		{
			string cardID = GameUtils.TranslateDbIdToCardId(deckCardData.Def.Asset, false);
			int num = deckCardData.Qty - deck.GetCardIdCount(cardID, true);
			for (int i = 0; i < num; i++)
			{
				list.Add(new DeckMaker.DeckFill
				{
					m_addCard = DefLoader.Get().GetEntityDef(deckCardData.Def.Asset, true)
				});
			}
		}
		int num2 = deck.GetTotalValidCardCount() + list.Count;
		int num3 = deck.GetMaxCardCount() - num2;
		if (num3 > 0)
		{
			list.AddRange(DeckMaker.GetFillCards(deck, deck.GetRuleset()));
			global::Log.CollectionManager.PrintWarning("Smart Deck: Insufficient number of cards. Adding {0} more cards to deck {1}.", new object[]
			{
				num3,
				deck.ID
			});
		}
		return list;
	}

	// Token: 0x06001117 RID: 4375 RVA: 0x000604D8 File Offset: 0x0005E6D8
	private bool OnBnetErrorFromSmartDeckCompletion(BnetErrorInfo info, object data)
	{
		if (info.GetError() == BattleNetErrors.ERROR_ATTRIBUTE_MAX_SIZE_EXCEEDED && this.m_smartDeckCallbackByDeckId.Count > 0)
		{
			global::Log.CollectionManager.PrintError("Bnet Error: Attribute Max Size Exceeded when attempting to request a Smart Deck Completion.", Array.Empty<object>());
			foreach (long id in this.m_smartDeckCallbackByDeckId.Keys.ToArray<long>())
			{
				SmartDeckRequest smartDeckRequest = Network.GenerateSmartDeckRequestMessage(this.GetDeck(id));
				TelemetryManager.Client().SendSmartDeckCompleteFailed((int)smartDeckRequest.GetSerializedSize());
				Network.Get().RequestSmartDeckCompletion(this.GetDeck(id));
			}
			return true;
		}
		return false;
	}

	// Token: 0x06001118 RID: 4376 RVA: 0x00060568 File Offset: 0x0005E768
	public static string GetHeroCardId(TAG_CLASS heroClass, CardHero.HeroType heroType)
	{
		if (heroClass == TAG_CLASS.WHIZBANG)
		{
			return "BOT_914h";
		}
		foreach (CardHeroDbfRecord cardHeroDbfRecord in GameDbf.CardHero.GetRecords())
		{
			if (cardHeroDbfRecord.HeroType == heroType && GameUtils.GetTagClassFromCardDbId(cardHeroDbfRecord.CardId) == heroClass)
			{
				return GameUtils.TranslateDbIdToCardId(cardHeroDbfRecord.CardId, false);
			}
		}
		return string.Empty;
	}

	// Token: 0x06001119 RID: 4377 RVA: 0x000605F0 File Offset: 0x0005E7F0
	public bool ShouldShowDeckTemplatePageForClass(TAG_CLASS classType)
	{
		int @int = Options.Get().GetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, 0);
		int num = 1 << (int)classType;
		return (@int & num) == 0;
	}

	// Token: 0x0600111A RID: 4378 RVA: 0x0006061C File Offset: 0x0005E81C
	public void SetShowDeckTemplatePageForClass(TAG_CLASS classType, bool show)
	{
		int num = Options.Get().GetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, 0);
		int num2 = 1 << (int)classType;
		num |= num2;
		if (show)
		{
			num ^= num2;
		}
		Options.Get().SetInt(Option.SKIP_DECK_TEMPLATE_PAGE_FOR_CLASS_FLAGS, num);
	}

	// Token: 0x0600111B RID: 4379 RVA: 0x0006065C File Offset: 0x0005E85C
	public bool ShouldShowWildToStandardTutorial(bool checkPrevSceneIsPlayMode = true)
	{
		return this.ShouldAccountSeeStandardWild() && SceneMgr.Get().GetMode() == SceneMgr.Mode.COLLECTIONMANAGER && (!checkPrevSceneIsPlayMode || SceneMgr.Get().GetPrevMode() == SceneMgr.Mode.TOURNAMENT) && Options.Get().GetBool(Option.NEEDS_TO_MAKE_STANDARD_DECK);
	}

	// Token: 0x0600111C RID: 4380 RVA: 0x00060698 File Offset: 0x0005E898
	public bool UpdateDeckWithNewId(long oldId, long newId)
	{
		if (CollectionDeckTray.Get() != null && !CollectionDeckTray.Get().GetDecksContent().UpdateDeckBoxWithNewId(oldId, newId))
		{
			return false;
		}
		CollectionDeck editedDeck = this.GetEditedDeck();
		if (this.IsInEditMode() && editedDeck.ID == oldId && this.m_decks.ContainsKey(newId))
		{
			this.m_decks[newId].CopyContents(editedDeck);
			this.SetEditedDeck(this.m_decks[newId], null);
		}
		this.RemoveDeck(oldId);
		return true;
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x0006071C File Offset: 0x0005E91C
	public int GetOwnedCount(string cardId, TAG_PREMIUM premium)
	{
		int num;
		int num2;
		int num3;
		CollectionManager.Get().GetOwnedCardCount(cardId, out num, out num2, out num3);
		int result = 0;
		switch (premium)
		{
		case TAG_PREMIUM.NORMAL:
			result = num;
			break;
		case TAG_PREMIUM.GOLDEN:
			result = num2;
			break;
		case TAG_PREMIUM.DIAMOND:
			result = num3;
			break;
		}
		return result;
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x0006075C File Offset: 0x0005E95C
	public int GetTotalOwnedCount(string cardId)
	{
		int num;
		int num2;
		int num3;
		CollectionManager.Get().GetOwnedCardCount(cardId, out num, out num2, out num3);
		return num + num2 + num3;
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x00060780 File Offset: 0x0005E980
	private void InitImpl()
	{
		this.m_filterIsSetRotatedCache = new global::Map<TAG_CARD_SET, bool>(global::EnumUtils.Length<TAG_CARD_SET>(), new CollectionManager.TagCardSetEnumComparer());
		List<CardTagDbfRecord> list = GameDbf.CardTag.GetRecords().FindAll((CardTagDbfRecord record) => record.TagId == 1932);
		List<string> allCollectibleCardIds = GameUtils.GetAllCollectibleCardIds();
		this.m_collectibleCardIndex = new global::Map<CollectionManager.CollectibleCardIndex, CollectibleCard>(allCollectibleCardIds.Count * 2 + list.Count, new CollectionManager.CollectibleCardIndexComparer());
		foreach (string text in allCollectibleCardIds)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
			if (entityDef == null)
			{
				global::Error.AddDevFatal("Failed to find an EntityDef for collectible card {0}", new object[]
				{
					text
				});
				return;
			}
			this.RegisterCard(entityDef, text, TAG_PREMIUM.NORMAL);
			if (entityDef.GetCardSet() != TAG_CARD_SET.HERO_SKINS)
			{
				this.RegisterCard(entityDef, text, TAG_PREMIUM.GOLDEN);
			}
		}
		foreach (CardTagDbfRecord cardTagDbfRecord in list)
		{
			string text2 = GameUtils.TranslateDbIdToCardId(cardTagDbfRecord.CardId, false);
			EntityDef entityDef2 = DefLoader.Get().GetEntityDef(text2);
			if (entityDef2 != null)
			{
				this.RegisterCard(entityDef2, text2, TAG_PREMIUM.DIAMOND);
			}
		}
		Network network = Network.Get();
		network.RegisterNetHandler(GetDeckContentsResponse.PacketID.ID, new Network.NetHandler(this.OnGetDeckContentsResponse), null);
		network.RegisterNetHandler(DBAction.PacketID.ID, new Network.NetHandler(this.OnDBAction), null);
		network.RegisterNetHandler(DeckCreated.PacketID.ID, new Network.NetHandler(this.OnDeckCreatedNetworkResponse), null);
		network.RegisterNetHandler(DeckDeleted.PacketID.ID, new Network.NetHandler(this.OnDeckDeleted), null);
		network.RegisterNetHandler(DeckRenamed.PacketID.ID, new Network.NetHandler(this.OnDeckRenamed), null);
		network.RegisterNetHandler(SmartDeckResponse.PacketID.ID, new Network.NetHandler(this.OnSmartDeckResponse), null);
		network.AddBnetErrorListener(BnetFeature.Games, new Network.BnetErrorCallback(this.OnBnetErrorFromSmartDeckCompletion));
		NetCache.Get().RegisterCollectionManager(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		LoginManager.Get().OnAchievesLoaded += this.OnAchievesLoaded;
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x000609D4 File Offset: 0x0005EBD4
	private void WillReset()
	{
		this.m_achievesLoaded = false;
		this.m_netCacheLoaded = false;
		this.m_collectionLoaded = false;
		HearthstoneApplication.Get().WillReset -= CollectionManager.s_instance.WillReset;
		NetCache.Get().FavoriteCardBackChanged -= CollectionManager.s_instance.OnFavoriteCardBackChanged;
		NetCache.Get().RemoveUpdatedListener(typeof(NetCache.NetCacheDecks), new Action(CollectionManager.s_instance.NetCache_OnDecksReceived));
		this.m_decks.Clear();
		this.m_baseDecks.Clear();
		this.m_preconDecks.Clear();
		this.m_favoriteHeroChangedListeners.Clear();
		this.m_templateDecks.Clear();
		this.m_templateDeckMap.Clear();
		this.m_displayableCardSets.Clear();
		this.m_onUIHeroOverrideCardRemovedListeners.Clear();
		this.m_collectibleCards = new List<CollectibleCard>();
		this.m_collectibleCardIndex = new global::Map<CollectionManager.CollectibleCardIndex, CollectibleCard>();
		this.m_collectionLastModifiedTime = 0f;
		this.m_lastSearchForWildCardsTime = 0f;
		this.m_accountEverHadWildCards = false;
		this.m_accountHasRotatedItems = false;
		this.m_EditedDeck = null;
		CollectionManager.s_instance = null;
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x00060AF0 File Offset: 0x0005ECF0
	private void OnCollectionChanged()
	{
		CollectionManager.DelOnCollectionChanged[] array = this.m_collectionChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x00060B20 File Offset: 0x0005ED20
	private List<string> GetCardIDsInSet(TAG_CARD_SET? cardSet, TAG_CLASS? cardClass, TAG_RARITY? cardRarity, TAG_RACE? cardRace)
	{
		List<string> nonHeroSkinCollectibleCardIds = GameUtils.GetNonHeroSkinCollectibleCardIds();
		List<string> list = new List<string>();
		foreach (string text in nonHeroSkinCollectibleCardIds)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(text);
			if ((cardClass == null || cardClass.Value == entityDef.GetClass()) && (cardRarity == null || cardRarity.Value == entityDef.GetRarity()) && (cardSet == null || cardSet.Value == entityDef.GetCardSet()) && (cardRace == null || cardRace.Value == entityDef.GetRace()))
			{
				list.Add(text);
			}
		}
		return list;
	}

	// Token: 0x06001123 RID: 4387 RVA: 0x00060BE8 File Offset: 0x0005EDE8
	public int NumCardsOwnedInSet(TAG_CARD_SET cardSet)
	{
		string searchString = null;
		List<CollectibleCardFilter.FilterMask> filterMasks = null;
		int? manaCost = null;
		int? minOwned = new int?(1);
		List<CollectibleCard> cards = this.FindCards(searchString, filterMasks, manaCost, new TAG_CARD_SET[]
		{
			cardSet
		}, null, null, null, null, null, minOwned, null, null, null, null, null, false, null, null, null).m_cards;
		int num = 0;
		foreach (CollectibleCard collectibleCard in cards)
		{
			num += collectibleCard.OwnedCount;
		}
		return num;
	}

	// Token: 0x06001124 RID: 4388 RVA: 0x00060CB4 File Offset: 0x0005EEB4
	private CollectibleCard RegisterCard(EntityDef entityDef, string cardID, TAG_PREMIUM premium)
	{
		CollectionManager.CollectibleCardIndex key = new CollectionManager.CollectibleCardIndex(cardID, premium);
		CollectibleCard collectibleCard = null;
		if (!this.m_collectibleCardIndex.TryGetValue(key, out collectibleCard))
		{
			collectibleCard = new CollectibleCard(GameUtils.GetCardRecord(cardID), entityDef, premium);
			this.m_collectibleCards.Add(collectibleCard);
			this.m_collectibleCardIndex.Add(key, collectibleCard);
		}
		return collectibleCard;
	}

	// Token: 0x06001125 RID: 4389 RVA: 0x00060D04 File Offset: 0x0005EF04
	private void ClearCardCounts(EntityDef entityDef, string cardID, TAG_PREMIUM premium)
	{
		this.RegisterCard(entityDef, cardID, premium).ClearCounts();
	}

	// Token: 0x06001126 RID: 4390 RVA: 0x00060D14 File Offset: 0x0005EF14
	private CollectibleCard SetCounts(NetCache.CardStack netStack, EntityDef entityDef)
	{
		this.ClearCardCounts(entityDef, netStack.Def.Name, netStack.Def.Premium);
		return this.AddCounts(entityDef, netStack.Def.Name, netStack.Def.Premium, new DateTime(netStack.Date), netStack.Count, netStack.NumSeen);
	}

	// Token: 0x06001127 RID: 4391 RVA: 0x00060D74 File Offset: 0x0005EF74
	private CollectibleCard AddCounts(EntityDef entityDef, string cardID, TAG_PREMIUM premium, DateTime insertDate, int count, int numSeen)
	{
		if (entityDef == null)
		{
			Debug.LogError(string.Format("CollectionManager.RegisterCardStack(): DefLoader failed to get entity def for {0}", cardID));
			return null;
		}
		this.m_collectionLastModifiedTime = Time.realtimeSinceStartup;
		CollectibleCard collectibleCard = this.RegisterCard(entityDef, cardID, premium);
		if (GameUtils.IsCoreCard(cardID))
		{
			count = Math.Min(collectibleCard.DefaultMaxCopiesPerDeck - collectibleCard.OwnedCount, count);
			numSeen = Math.Min(numSeen, count);
		}
		collectibleCard.AddCounts(count, numSeen, insertDate);
		return collectibleCard;
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x00060DE4 File Offset: 0x0005EFE4
	private void AddPreconDeckFromNotice(NetCache.ProfileNoticePreconDeck preconDeckNotice)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(preconDeckNotice.HeroAsset, true);
		if (entityDef == null)
		{
			return;
		}
		this.AddPreconDeck(entityDef.GetClass(), preconDeckNotice.DeckID);
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject == null)
		{
			return;
		}
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

	// Token: 0x06001129 RID: 4393 RVA: 0x00060E98 File Offset: 0x0005F098
	private void AddPreconDeck(TAG_CLASS heroClass, long deckID)
	{
		if (this.m_preconDecks.ContainsKey(heroClass))
		{
			global::Log.CollectionManager.PrintDebug(string.Format("CollectionManager.AddPreconDeck(): Already have a precon deck for class {0}, cannot add deckID {1}", heroClass, deckID), Array.Empty<object>());
			return;
		}
		global::Log.CollectionManager.Print(string.Format("CollectionManager.AddPreconDeck() heroClass={0} deckID={1}", heroClass, deckID), Array.Empty<object>());
		this.m_preconDecks[heroClass] = new CollectionManager.PreconDeck(deckID);
	}

	// Token: 0x0600112A RID: 4394 RVA: 0x00060F10 File Offset: 0x0005F110
	private CollectionDeck AddDeck(NetCache.DeckHeader deckHeader)
	{
		return this.AddDeck(deckHeader, true);
	}

	// Token: 0x0600112B RID: 4395 RVA: 0x00060F1C File Offset: 0x0005F11C
	private CollectionDeck AddDeck(NetCache.DeckHeader deckHeader, bool updateNetCache)
	{
		if (deckHeader.Type != DeckType.NORMAL_DECK && !TavernBrawlManager.IsBrawlDeckType(deckHeader.Type) && deckHeader.Type != DeckType.PVPDR_DECK)
		{
			Debug.LogWarning(string.Format("CollectionManager.AddDeck(): deckHeader {0} is not of type NORMAL_DECK, Brawl, or PVPDR deck", deckHeader));
			return null;
		}
		ulong createDate = (ulong)deckHeader.ID;
		if (deckHeader.CreateDate != null)
		{
			createDate = global::TimeUtils.DateTimeToUnixTimeStamp(deckHeader.CreateDate.Value);
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
			collectionDeck.Name = GameStrings.Format("GLOBAL_BASIC_DECK_NAME", new object[]
			{
				GameStrings.GetClassName(collectionDeck.GetClass())
			});
			global::Log.CollectionManager.Print(string.Format("Set deck name to {0}", collectionDeck.Name), Array.Empty<object>());
		}
		if (!this.IsInEditMode() || this.GetEditedDeck() == null || this.GetEditedDeck().ID != collectionDeck.ID)
		{
			if (this.m_decks.ContainsKey(deckHeader.ID))
			{
				this.m_decks.Remove(deckHeader.ID);
			}
			this.m_decks.Add(deckHeader.ID, collectionDeck);
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
		if (this.m_baseDecks.ContainsKey(deckHeader.ID))
		{
			this.m_baseDecks.Remove(deckHeader.ID);
		}
		this.m_baseDecks.Add(deckHeader.ID, value);
		if (updateNetCache)
		{
			NetCache.Get().GetNetObject<NetCache.NetCacheDecks>().Decks.Add(deckHeader);
		}
		return collectionDeck;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0006122C File Offset: 0x0005F42C
	private CollectionDeck RemoveDeck(long id)
	{
		CollectionDeck result = null;
		if (this.m_baseDecks.TryGetValue(id, out result))
		{
			this.m_baseDecks.Remove(id);
		}
		if (this.m_decks.TryGetValue(id, out result))
		{
			this.m_decks.Remove(id);
		}
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject == null)
		{
			return result;
		}
		for (int i = 0; i < netObject.Decks.Count; i++)
		{
			if (netObject.Decks[i].ID == id)
			{
				netObject.Decks.RemoveAt(i);
				break;
			}
		}
		return result;
	}

	// Token: 0x0600112D RID: 4397 RVA: 0x000612C0 File Offset: 0x0005F4C0
	public static void DeleteDeckFromNetwork(CollectionDeck deck)
	{
		deck.MarkBeingDeleted();
		Network.Get().DeleteDeck(deck.ID, deck.Type);
		CollectionManager.s_instance.AddPendingDeckDelete(deck.ID);
		if (!Network.IsLoggedIn() || deck.ID <= 0L)
		{
			CollectionManager.s_instance.OnDeckDeletedWhileOffline(deck.ID);
		}
	}

	// Token: 0x0600112E RID: 4398 RVA: 0x0006131C File Offset: 0x0005F51C
	private void LogAllDeckStringsInCollection()
	{
		global::Log.Decks.PrintInfo("Deck Contents Received:", Array.Empty<object>());
		foreach (CollectionDeck collectionDeck in this.GetDecks().Values)
		{
			collectionDeck.LogDeckStringInformation();
		}
	}

	// Token: 0x0600112F RID: 4399 RVA: 0x00061388 File Offset: 0x0005F588
	private bool IsDeckNameTaken(string name)
	{
		using (SortedDictionary<long, CollectionDeck>.ValueCollection.Enumerator enumerator = this.GetDecks().Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Name.Trim().Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001130 RID: 4400 RVA: 0x000613F4 File Offset: 0x0005F5F4
	private void FireDeckContentsEvent(long id)
	{
		CollectionManager.DelOnDeckContents[] array = this.m_deckContentsListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](id);
		}
	}

	// Token: 0x06001131 RID: 4401 RVA: 0x00061424 File Offset: 0x0005F624
	private void FireAllDeckContentsEvent()
	{
		CollectionManager.DelOnAllDeckContents[] array = this.m_allDeckContentsListeners.ToArray();
		this.m_allDeckContentsListeners.Clear();
		CollectionManager.DelOnAllDeckContents[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i]();
		}
	}

	// Token: 0x06001132 RID: 4402 RVA: 0x00061460 File Offset: 0x0005F660
	private void OnNetCacheReady()
	{
		NetCache.Get().UnregisterNetCacheHandler(new NetCache.NetCacheCallback(this.OnNetCacheReady));
		this.m_netCacheLoaded = true;
		global::Log.CollectionManager.Print("CollectionManager.OnNetCacheReady", Array.Empty<object>());
		this.m_displayableCardSets.AddRange(from cardSetRecord in GameDbf.CardSet.GetRecords()
		where cardSetRecord != null && cardSetRecord.IsCollectible && cardSetRecord.ID != 17
		where SpecialEventManager.Get().IsEventActive((cardSetRecord != null) ? cardSetRecord.SetFilterEvent : null, false)
		select (TAG_CARD_SET)cardSetRecord.ID);
		this.UpdateShowAdvancedCMOption();
		if (Options.GetFormatType() == FormatType.FT_WILD && !this.ShouldAccountSeeStandardWild())
		{
			global::Log.CollectionManager.Print("Options are set to Wild mode, but account shouldn't see Standard/Wild, so setting format type to Standard!", Array.Empty<object>());
			Options.SetFormatType(FormatType.FT_STANDARD);
		}
		NetCache.NetCacheProfileNotices netObject = NetCache.Get().GetNetObject<NetCache.NetCacheProfileNotices>();
		if (netObject != null)
		{
			this.OnNewNotices(netObject.Notices, true);
		}
		NetCache.Get().RegisterNewNoticesListener(new NetCache.DelNewNoticesListener(this.OnNewNotices));
		this.CheckAchievesAndNetCacheLoaded();
	}

	// Token: 0x06001133 RID: 4403 RVA: 0x00061586 File Offset: 0x0005F786
	private void OnAchievesLoaded()
	{
		LoginManager.Get().OnAchievesLoaded -= this.OnAchievesLoaded;
		this.m_achievesLoaded = true;
		this.CheckAchievesAndNetCacheLoaded();
	}

	// Token: 0x06001134 RID: 4404 RVA: 0x000615AC File Offset: 0x0005F7AC
	private void CheckAchievesAndNetCacheLoaded()
	{
		if (!this.m_achievesLoaded || !this.m_netCacheLoaded)
		{
			return;
		}
		this.CreateCollectionDecksFromNetCache();
		CollectionManager.DelOnCollectionLoaded[] array = this.m_collectionLoadedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i]();
		}
		this.m_collectionLoaded = true;
		if (CollectionManager.OnCollectionManagerReady != null)
		{
			CollectionManager.OnCollectionManagerReady();
		}
	}

	// Token: 0x06001135 RID: 4405 RVA: 0x0006160C File Offset: 0x0005F80C
	private void CreateCollectionDecksFromNetCache()
	{
		List<NetCache.DeckHeader> list = new List<NetCache.DeckHeader>();
		NetCache.NetCacheDecks netObject = NetCache.Get().GetNetObject<NetCache.NetCacheDecks>();
		if (netObject != null)
		{
			list = netObject.Decks;
		}
		foreach (NetCache.DeckHeader deckHeader in list)
		{
			switch (deckHeader.Type)
			{
			case DeckType.NORMAL_DECK:
			case DeckType.TAVERN_BRAWL_DECK:
			case DeckType.FSG_BRAWL_DECK:
			case DeckType.PVPDR_DECK:
				this.AddDeck(deckHeader, false);
				continue;
			case DeckType.PRECON_DECK:
			{
				EntityDef entityDef = DefLoader.Get().GetEntityDef(deckHeader.Hero);
				if (entityDef == null)
				{
					Debug.LogErrorFormat("CollectionManager.OnAchievesLoaded: cannot add precon deck because cannot determine class for hero with string cardId={0} (deckId={1})", new object[]
					{
						deckHeader.Hero,
						deckHeader.ID
					});
					continue;
				}
				this.AddPreconDeck(entityDef.GetClass(), deckHeader.ID);
				continue;
			}
			}
			Debug.LogWarning(string.Format("CollectionManager.OnAchievesLoaded(): don't know how to handle deck type {0}", deckHeader.Type));
		}
		List<DeckContents> localDeckContentsFromCache = OfflineDataCache.GetLocalDeckContentsFromCache();
		if (localDeckContentsFromCache != null)
		{
			this.UpdateFromDeckContents(localDeckContentsFromCache);
		}
		NetCache.Get().RegisterUpdatedListener(typeof(NetCache.NetCacheDecks), new Action(CollectionManager.s_instance.NetCache_OnDecksReceived));
	}

	// Token: 0x06001136 RID: 4406 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public void FixedRewardsStartupComplete()
	{
	}

	// Token: 0x06001137 RID: 4407 RVA: 0x0006175C File Offset: 0x0005F95C
	private void OnNewNotices(List<NetCache.ProfileNotice> newNotices, bool isInitialNoticeList)
	{
		List<NetCache.ProfileNotice> list = newNotices.FindAll((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.PRECON_DECK);
		bool flag = false;
		foreach (NetCache.ProfileNotice profileNotice in list)
		{
			NetCache.ProfileNoticePreconDeck preconDeckNotice = profileNotice as NetCache.ProfileNoticePreconDeck;
			this.AddPreconDeckFromNotice(preconDeckNotice);
			flag = true;
		}
		bool flag2 = false;
		foreach (NetCache.ProfileNotice profileNotice2 in newNotices.FindAll((NetCache.ProfileNotice obj) => obj.Type == NetCache.ProfileNotice.NoticeType.DECK_REMOVED))
		{
			NetCache.ProfileNoticeDeckRemoved profileNoticeDeckRemoved = profileNotice2 as NetCache.ProfileNoticeDeckRemoved;
			this.RemoveDeck(profileNoticeDeckRemoved.DeckID);
			Network.Get().AckNotice(profileNoticeDeckRemoved.NoticeID);
			flag2 = true;
		}
		if (flag || flag2)
		{
			NetCache.Get().ReloadNetObject<NetCache.NetCacheDecks>();
		}
	}

	// Token: 0x06001138 RID: 4408 RVA: 0x0006186C File Offset: 0x0005FA6C
	private void UpdateShowAdvancedCMOption()
	{
		if (Options.Get().GetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, false))
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
			if (!this.AccountEverHadWildCards() && !flag)
			{
				return;
			}
		}
		else if (!flag)
		{
			return;
		}
		Options.Get().SetBool(Option.SHOW_ADVANCED_COLLECTIONMANAGER, true);
	}

	// Token: 0x06001139 RID: 4409 RVA: 0x000618D8 File Offset: 0x0005FAD8
	private void UpdateDeckHeroArt(string heroCardID)
	{
		TAG_PREMIUM bestCardPremium = this.GetBestCardPremium(heroCardID);
		TAG_CLASS @class = DefLoader.Get().GetEntityDef(heroCardID).GetClass();
		CollectionManager.PreconDeck preconDeck = this.GetPreconDeck(@class);
		if (preconDeck != null)
		{
			this.m_preconDecks[@class] = new CollectionManager.PreconDeck(preconDeck.ID);
		}
		foreach (CollectionDeck collectionDeck in this.m_baseDecks.Values.ToList<CollectionDeck>().FindAll((CollectionDeck obj) => obj.HeroCardID.Equals(heroCardID)))
		{
			collectionDeck.HeroPremium = bestCardPremium;
		}
		foreach (CollectionDeck collectionDeck2 in this.m_decks.Values.ToList<CollectionDeck>().FindAll((CollectionDeck obj) => obj.HeroCardID.Equals(heroCardID)))
		{
			collectionDeck2.HeroPremium = bestCardPremium;
		}
	}

	// Token: 0x0600113A RID: 4410 RVA: 0x000619F4 File Offset: 0x0005FBF4
	private void InsertNewCollectionCard(string cardID, TAG_PREMIUM premium, DateTime insertDate, int count, bool seenBefore)
	{
		EntityDef entityDef = DefLoader.Get().GetEntityDef(cardID);
		int numSeen = seenBefore ? count : 0;
		this.AddCounts(entityDef, cardID, premium, insertDate, count, numSeen);
		if (entityDef.IsHeroSkin())
		{
			if (premium == TAG_PREMIUM.GOLDEN)
			{
				this.UpdateDeckHeroArt(cardID);
			}
			StoreManager.Get().Catalog.UpdateProductStatus();
			return;
		}
		foreach (KeyValuePair<long, CollectionDeck> keyValuePair in this.m_decks)
		{
			keyValuePair.Value.OnCardInsertedToCollection(cardID, premium);
		}
		if (CollectionDeckTray.Get() != null)
		{
			CollectionDeckTray.Get().HandleAddedCardDeckUpdate(entityDef, premium, count);
			if (CollectionDeckTray.Get().m_decksContent != null)
			{
				CollectionDeckTray.Get().m_decksContent.RefreshMissingCardIndicators();
			}
		}
		this.NotifyNetCacheOfNewCards(new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		}, insertDate.Ticks, count, seenBefore);
		this.UpdateShowAdvancedCMOption();
	}

	// Token: 0x0600113B RID: 4411 RVA: 0x00061AFC File Offset: 0x0005FCFC
	private void InsertNewCollectionCards(List<string> cardIDs, List<TAG_PREMIUM> cardPremiums, List<DateTime> insertDates, List<int> counts, bool seenBefore)
	{
		for (int i = 0; i < cardIDs.Count; i++)
		{
			string cardID = cardIDs[i];
			TAG_PREMIUM premium = cardPremiums[i];
			DateTime insertDate = insertDates[i];
			int count = counts[i];
			this.InsertNewCollectionCard(cardID, premium, insertDate, count, seenBefore);
		}
	}

	// Token: 0x0600113C RID: 4412 RVA: 0x00061B4C File Offset: 0x0005FD4C
	private void RemoveCollectionCard(string cardID, TAG_PREMIUM premium, int count)
	{
		CollectibleCard card = this.GetCard(cardID, premium);
		card.RemoveCounts(count);
		this.m_collectionLastModifiedTime = Time.realtimeSinceStartup;
		int ownedCount = card.OwnedCount;
		foreach (CollectionDeck collectionDeck in this.GetDecks().Values)
		{
			if (!collectionDeck.Locked)
			{
				int cardCountFirstMatchingSlot = collectionDeck.GetCardCountFirstMatchingSlot(cardID, premium);
				if (cardCountFirstMatchingSlot > ownedCount)
				{
					int num = cardCountFirstMatchingSlot - ownedCount;
					for (int i = 0; i < num; i++)
					{
						collectionDeck.RemoveCard(cardID, premium, true);
					}
					if (!CollectionDeckTray.Get().HandleDeletedCardDeckUpdate(cardID, premium))
					{
						collectionDeck.SendChanges();
					}
				}
			}
		}
		this.NotifyNetCacheOfRemovedCards(new NetCache.CardDefinition
		{
			Name = cardID,
			Premium = premium
		}, count);
	}

	// Token: 0x0600113D RID: 4413 RVA: 0x00061C20 File Offset: 0x0005FE20
	private void UpdateCardCounts(NetCache.NetCacheCollection netCacheCards, NetCache.CardDefinition cardDef, int count, int newCount)
	{
		netCacheCards.TotalCardsOwned += count;
		if (cardDef.Premium == TAG_PREMIUM.NORMAL)
		{
			EntityDef entityDef = DefLoader.Get().GetEntityDef(cardDef.Name);
			if (entityDef.IsCoreCard())
			{
				int num = entityDef.IsElite() ? 1 : 2;
				if (newCount < 0 || newCount > num)
				{
					Debug.LogError(string.Concat(new object[]
					{
						"CollectionManager.UpdateCardCounts: created an illegal stack size of ",
						newCount,
						" for card ",
						entityDef
					}));
					count = 0;
				}
				netCacheCards.CoreCardsUnlockedPerClass[entityDef.GetClass()].Add(entityDef.GetCardId());
			}
		}
	}

	// Token: 0x0600113E RID: 4414 RVA: 0x00061CC4 File Offset: 0x0005FEC4
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
		this.UpdateCardCounts(netObject, cardDef, -count, cardStack.Count);
	}

	// Token: 0x0600113F RID: 4415 RVA: 0x00061D48 File Offset: 0x0005FF48
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
		this.UpdateCardCounts(netObject, cardDef, count, cardStack.Count);
	}

	// Token: 0x06001140 RID: 4416 RVA: 0x00061E0C File Offset: 0x0006000C
	private void LoadTemplateDecks()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (DeckTemplateDbfRecord deckTemplateDbfRecord in GameDbf.DeckTemplate.GetRecords())
		{
			string @event = deckTemplateDbfRecord.Event;
			if (string.IsNullOrEmpty(@event) || SpecialEventManager.Get().IsEventActive(@event, false))
			{
				int deckId = deckTemplateDbfRecord.DeckId;
				if (!this.m_templateDeckMap.ContainsKey(deckId))
				{
					DeckDbfRecord record = GameDbf.Deck.GetRecord(deckId);
					if (record == null)
					{
						Debug.LogError(string.Format("Unable to find deck with ID {0}", deckId));
					}
					else
					{
						global::Map<string, int> map = new global::Map<string, int>();
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
									global::Map<string, int> map2 = map;
									string key = noteMiniGuid;
									map2[key]++;
								}
								else
								{
									map[noteMiniGuid] = 1;
								}
							}
							else
							{
								Debug.LogError(string.Format("Card ID in deck not found in CARD.XML: {0}", cardId));
							}
							int nextCard = deckCardDbfRecord.NextCard;
							if (nextCard == 0)
							{
								deckCardDbfRecord = null;
							}
							else
							{
								deckCardDbfRecord = GameDbf.DeckCard.GetRecord(nextCard);
							}
						}
						TAG_CLASS classId = (TAG_CLASS)deckTemplateDbfRecord.ClassId;
						List<CollectionManager.TemplateDeck> list = null;
						if (!this.m_templateDecks.TryGetValue(classId, out list))
						{
							list = new List<CollectionManager.TemplateDeck>();
							this.m_templateDecks.Add(classId, list);
						}
						CollectionManager.TemplateDeck templateDeck = new CollectionManager.TemplateDeck
						{
							m_id = deckId,
							m_class = classId,
							m_sortOrder = deckTemplateDbfRecord.SortOrder,
							m_cardIds = map,
							m_title = record.Name,
							m_description = record.Description,
							m_displayTexture = deckTemplateDbfRecord.DisplayTexture,
							m_event = deckTemplateDbfRecord.Event,
							m_isStarterDeck = deckTemplateDbfRecord.IsStarterDeck,
							m_formatType = (FormatType)deckTemplateDbfRecord.FormatType
						};
						list.Add(templateDeck);
						this.m_templateDeckMap.Add(templateDeck.m_id, templateDeck);
					}
				}
			}
		}
		foreach (KeyValuePair<TAG_CLASS, List<CollectionManager.TemplateDeck>> keyValuePair in this.m_templateDecks)
		{
			keyValuePair.Value.Sort(delegate(CollectionManager.TemplateDeck a, CollectionManager.TemplateDeck b)
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
		global::Log.CollectionManager.Print("_decktemplate: Time spent loading template decks: " + (realtimeSinceStartup2 - realtimeSinceStartup), Array.Empty<object>());
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000620FC File Offset: 0x000602FC
	public TAG_PREMIUM GetPreferredPremium()
	{
		return this.m_premiumPreference;
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x00062104 File Offset: 0x00060304
	public void SetPremiumPreference(TAG_PREMIUM premium)
	{
		this.m_premiumPreference = premium;
		this.RefreshCurrentPageContents();
	}

	// Token: 0x06001143 RID: 4419 RVA: 0x00062113 File Offset: 0x00060313
	public void RefreshCurrentPageContents()
	{
		if (this.m_collectibleDisplay != null)
		{
			this.m_collectibleDisplay.m_pageManager.RefreshCurrentPageContents();
		}
	}

	// Token: 0x06001144 RID: 4420 RVA: 0x00062134 File Offset: 0x00060334
	public void RegisterDecksToRequestContentsAfterDeckSetDataResponse(List<long> decksToRequest)
	{
		foreach (long item in decksToRequest)
		{
			if (!this.m_decksToRequestContentsAfterDeckSetDataResonse.Contains(item))
			{
				this.m_decksToRequestContentsAfterDeckSetDataResonse.Add(item);
			}
		}
	}

	// Token: 0x06001145 RID: 4421 RVA: 0x00062198 File Offset: 0x00060398
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

	// Token: 0x06001146 RID: 4422 RVA: 0x000621E4 File Offset: 0x000603E4
	public void SetTimeOfLastPlayerDeckSave(DateTime? time)
	{
		this.m_timeOfLastPlayerDeckSave = time;
	}

	// Token: 0x06001147 RID: 4423 RVA: 0x000621F0 File Offset: 0x000603F0
	public static ShareableDeck GetShareableDeckFromTemplateRecord(DeckTemplateDbfRecord deckTemplateDBfRecord, bool usePremiumCardsFromCollection = true)
	{
		int favoriteHeroCardDBIdFromClass = GameUtils.GetFavoriteHeroCardDBIdFromClass((TAG_CLASS)deckTemplateDBfRecord.ClassId);
		CollectionDeck collectionDeck = new CollectionDeck
		{
			Name = deckTemplateDBfRecord.DeckRecord.Name,
			FormatType = (FormatType)deckTemplateDBfRecord.FormatType,
			HeroCardID = GameUtils.TranslateDbIdToCardId(favoriteHeroCardDBIdFromClass, false)
		};
		string text;
		string text2;
		List<long> list = CollectionManager.Get().LoadDeckFromDBF(deckTemplateDBfRecord.DeckId, out text, out text2);
		if (list == null)
		{
			Debug.LogError(string.Format("GetShareableDeckFromTemplateRecord: Failed to find cards for deck template {0}", deckTemplateDBfRecord.ID));
			return null;
		}
		foreach (long num in list)
		{
			string cardID = GameUtils.TranslateDbIdToCardId((int)num, false);
			TAG_PREMIUM premium = TAG_PREMIUM.NORMAL;
			if (usePremiumCardsFromCollection)
			{
				premium = CollectionManager.Get().GetBestCardPremium(cardID);
			}
			collectionDeck.AddCard(cardID, premium, false);
		}
		return collectionDeck.GetShareableDeck();
	}

	// Token: 0x04000B07 RID: 2823
	private const int NUM_CARDS_GRANTED_POST_TUTORIAL = 96;

	// Token: 0x04000B08 RID: 2824
	private const int NUM_CARDS_TO_UNLOCK_ADVANCED_CM = 116;

	// Token: 0x04000B09 RID: 2825
	private const int NUM_EXPERT_CARDS_TO_UNLOCK_CRAFTING = 20;

	// Token: 0x04000B0A RID: 2826
	public const int NUM_EXPERT_CARDS_TO_UNLOCK_FORGE = 20;

	// Token: 0x04000B0B RID: 2827
	public const int NUM_CORE_CARDS_PER_CLASS = 16;

	// Token: 0x04000B0C RID: 2828
	public const int NUM_CORE_CARDS_NEUTRAL = 75;

	// Token: 0x04000B0D RID: 2829
	public const int MAX_NUM_TEMPLATE_DECKS = 3;

	// Token: 0x04000B0E RID: 2830
	public const int MAX_DECKS_PER_PLAYER = 27;

	// Token: 0x04000B0F RID: 2831
	public const int NUM_CLASSES = 10;

	// Token: 0x04000B10 RID: 2832
	private const float SMART_DECK_COMPLETE_TIMEOUT = 5f;

	// Token: 0x04000B11 RID: 2833
	public const int DEFAULT_MAX_COPIES_PER_CARD_NORMAL = 2;

	// Token: 0x04000B12 RID: 2834
	public const int DEFAULT_MAX_COPIES_PER_CARD_LEGENDARY = 1;

	// Token: 0x04000B13 RID: 2835
	public const int DEFAULT_MAX_CARDS_PER_DECK = 30;

	// Token: 0x04000B14 RID: 2836
	private const float PENDING_DECK_CONTENTS_REQUEST_THRESHOLD_SECONDS = 10f;

	// Token: 0x04000B15 RID: 2837
	private static CollectionManager s_instance;

	// Token: 0x04000B16 RID: 2838
	private bool m_collectionLoaded;

	// Token: 0x04000B17 RID: 2839
	private bool m_achievesLoaded;

	// Token: 0x04000B18 RID: 2840
	private bool m_netCacheLoaded;

	// Token: 0x04000B19 RID: 2841
	private global::Map<long, CollectionDeck> m_decks = new global::Map<long, CollectionDeck>();

	// Token: 0x04000B1A RID: 2842
	private global::Map<long, CollectionDeck> m_baseDecks = new global::Map<long, CollectionDeck>();

	// Token: 0x04000B1B RID: 2843
	private global::Map<TAG_CLASS, CollectionManager.PreconDeck> m_preconDecks = new global::Map<TAG_CLASS, CollectionManager.PreconDeck>();

	// Token: 0x04000B1C RID: 2844
	private global::Map<TAG_CLASS, List<CollectionManager.TemplateDeck>> m_templateDecks = new global::Map<TAG_CLASS, List<CollectionManager.TemplateDeck>>();

	// Token: 0x04000B1D RID: 2845
	private global::Map<int, CollectionManager.TemplateDeck> m_templateDeckMap = new global::Map<int, CollectionManager.TemplateDeck>();

	// Token: 0x04000B1E RID: 2846
	private CollectionDeck m_EditedDeck;

	// Token: 0x04000B1F RID: 2847
	private List<TAG_CARD_SET> m_displayableCardSets = new List<TAG_CARD_SET>();

	// Token: 0x04000B21 RID: 2849
	private List<CollectionManager.DelOnCollectionLoaded> m_collectionLoadedListeners = new List<CollectionManager.DelOnCollectionLoaded>();

	// Token: 0x04000B22 RID: 2850
	private List<CollectionManager.DelOnCollectionChanged> m_collectionChangedListeners = new List<CollectionManager.DelOnCollectionChanged>();

	// Token: 0x04000B23 RID: 2851
	private List<CollectionManager.DelOnDeckCreated> m_deckCreatedListeners = new List<CollectionManager.DelOnDeckCreated>();

	// Token: 0x04000B24 RID: 2852
	private List<CollectionManager.DelOnDeckDeleted> m_deckDeletedListeners = new List<CollectionManager.DelOnDeckDeleted>();

	// Token: 0x04000B25 RID: 2853
	private List<CollectionManager.DelOnDeckContents> m_deckContentsListeners = new List<CollectionManager.DelOnDeckContents>();

	// Token: 0x04000B26 RID: 2854
	private List<CollectionManager.DelOnAllDeckContents> m_allDeckContentsListeners = new List<CollectionManager.DelOnAllDeckContents>();

	// Token: 0x04000B27 RID: 2855
	private List<CollectionManager.DelOnNewCardSeen> m_newCardSeenListeners = new List<CollectionManager.DelOnNewCardSeen>();

	// Token: 0x04000B28 RID: 2856
	private List<CollectionManager.DelOnCardRewardsInserted> m_cardRewardListeners = new List<CollectionManager.DelOnCardRewardsInserted>();

	// Token: 0x04000B29 RID: 2857
	private List<CollectionManager.OnMassDisenchant> m_massDisenchantListeners = new List<CollectionManager.OnMassDisenchant>();

	// Token: 0x04000B2A RID: 2858
	private List<CollectionManager.OnEditedDeckChanged> m_editedDeckChangedListeners = new List<CollectionManager.OnEditedDeckChanged>();

	// Token: 0x04000B2B RID: 2859
	private List<Action> m_initialCollectionReceivedListeners = new List<Action>();

	// Token: 0x04000B2C RID: 2860
	private global::Map<long, float> m_pendingRequestDeckContents;

	// Token: 0x04000B2D RID: 2861
	private List<CollectibleCard> m_collectibleCards = new List<CollectibleCard>();

	// Token: 0x04000B2E RID: 2862
	private global::Map<int, int> m_coreCounterpartCardMap = new global::Map<int, int>();

	// Token: 0x04000B2F RID: 2863
	private global::Map<CollectionManager.CollectibleCardIndex, CollectibleCard> m_collectibleCardIndex;

	// Token: 0x04000B30 RID: 2864
	private float m_collectionLastModifiedTime;

	// Token: 0x04000B31 RID: 2865
	private DateTime? m_timeOfLastPlayerDeckSave;

	// Token: 0x04000B32 RID: 2866
	private bool m_accountHasWildCards;

	// Token: 0x04000B33 RID: 2867
	private float m_lastSearchForWildCardsTime;

	// Token: 0x04000B34 RID: 2868
	private bool m_accountEverHadWildCards;

	// Token: 0x04000B35 RID: 2869
	private bool m_accountHasRotatedItems;

	// Token: 0x04000B36 RID: 2870
	private bool m_showStandardComingSoonNotice;

	// Token: 0x04000B37 RID: 2871
	private List<Action> m_onNetCacheDecksProcessed = new List<Action>();

	// Token: 0x04000B38 RID: 2872
	private Dictionary<long, CollectionManager.DeckAutoFillCallback> m_smartDeckCallbackByDeckId = new Dictionary<long, CollectionManager.DeckAutoFillCallback>();

	// Token: 0x04000B39 RID: 2873
	private HashSet<long> m_decksToRequestContentsAfterDeckSetDataResonse = new HashSet<long>();

	// Token: 0x04000B3A RID: 2874
	private HashSet<int> m_inTransitDeckCreateRequests = new HashSet<int>();

	// Token: 0x04000B3B RID: 2875
	private HashSet<TAG_CARD_SET> m_filterCardSet = new HashSet<TAG_CARD_SET>(new CollectionManager.TagCardSetEnumComparer());

	// Token: 0x04000B3C RID: 2876
	private HashSet<TAG_CLASS> m_filterCardClass = new HashSet<TAG_CLASS>(new CollectionManager.TagClassEnumComparer());

	// Token: 0x04000B3D RID: 2877
	private HashSet<TAG_CARDTYPE> m_filterCardType = new HashSet<TAG_CARDTYPE>(new CollectionManager.TagCardTypeEnumComparer());

	// Token: 0x04000B3E RID: 2878
	private global::Map<TAG_CARD_SET, bool> m_filterIsSetRotatedCache;

	// Token: 0x04000B3F RID: 2879
	private List<CollectionManager.FavoriteHeroChangedListener> m_favoriteHeroChangedListeners = new List<CollectionManager.FavoriteHeroChangedListener>();

	// Token: 0x04000B40 RID: 2880
	private List<CollectionManager.OnUIHeroOverrideCardRemovedListener> m_onUIHeroOverrideCardRemovedListeners = new List<CollectionManager.OnUIHeroOverrideCardRemovedListener>();

	// Token: 0x04000B41 RID: 2881
	private bool m_waitingForBoxTransition;

	// Token: 0x04000B42 RID: 2882
	private bool m_hasVisitedCollection;

	// Token: 0x04000B43 RID: 2883
	private bool m_editMode;

	// Token: 0x04000B44 RID: 2884
	private TAG_PREMIUM m_premiumPreference = TAG_PREMIUM.DIAMOND;

	// Token: 0x04000B45 RID: 2885
	private CollectibleDisplay m_collectibleDisplay;

	// Token: 0x04000B46 RID: 2886
	private CollectionManager.PendingDeckCreateData m_pendingDeckCreate;

	// Token: 0x04000B47 RID: 2887
	private List<CollectionManager.PendingDeckDeleteData> m_pendingDeckDeleteList;

	// Token: 0x04000B48 RID: 2888
	private List<CollectionManager.PendingDeckRenameData> m_pendingDeckRenameList;

	// Token: 0x04000B49 RID: 2889
	private List<CollectionManager.PendingDeckEditData> m_pendingDeckEditList;

	// Token: 0x04000B4A RID: 2890
	private long m_currentPVPDRDeckId;

	// Token: 0x04000B4B RID: 2891
	private global::DeckRuleset m_deckRuleset;

	// Token: 0x02001444 RID: 5188
	// (Invoke) Token: 0x0600DA36 RID: 55862
	public delegate bool CollectibleCardFilterFunc(CollectibleCard card);

	// Token: 0x02001445 RID: 5189
	public class PreconDeck
	{
		// Token: 0x0600DA39 RID: 55865 RVA: 0x003F15B8 File Offset: 0x003EF7B8
		public PreconDeck(long id)
		{
			this.m_id = id;
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x0600DA3A RID: 55866 RVA: 0x003F15C7 File Offset: 0x003EF7C7
		public long ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x0400A971 RID: 43377
		private long m_id;
	}

	// Token: 0x02001446 RID: 5190
	public class TemplateDeck
	{
		// Token: 0x0400A972 RID: 43378
		public int m_id;

		// Token: 0x0400A973 RID: 43379
		public TAG_CLASS m_class;

		// Token: 0x0400A974 RID: 43380
		public int m_sortOrder;

		// Token: 0x0400A975 RID: 43381
		public global::Map<string, int> m_cardIds = new global::Map<string, int>();

		// Token: 0x0400A976 RID: 43382
		public string m_title;

		// Token: 0x0400A977 RID: 43383
		public string m_description;

		// Token: 0x0400A978 RID: 43384
		public string m_displayTexture;

		// Token: 0x0400A979 RID: 43385
		public string m_event;

		// Token: 0x0400A97A RID: 43386
		public bool m_isStarterDeck;

		// Token: 0x0400A97B RID: 43387
		public FormatType m_formatType;
	}

	// Token: 0x02001447 RID: 5191
	public class FindCardsResult
	{
		// Token: 0x0400A97C RID: 43388
		public List<CollectibleCard> m_cards = new List<CollectibleCard>();

		// Token: 0x0400A97D RID: 43389
		public bool m_resultsWithoutManaFilterExist;

		// Token: 0x0400A97E RID: 43390
		public bool m_resultsWithoutSetFilterExist;

		// Token: 0x0400A97F RID: 43391
		public bool m_resultsUnownedExist;

		// Token: 0x0400A980 RID: 43392
		public bool m_resultsInWildExist;
	}

	// Token: 0x02001448 RID: 5192
	// (Invoke) Token: 0x0600DA3E RID: 55870
	public delegate void DelCollectionManagerReady();

	// Token: 0x02001449 RID: 5193
	// (Invoke) Token: 0x0600DA42 RID: 55874
	public delegate void DelOnCollectionLoaded();

	// Token: 0x0200144A RID: 5194
	// (Invoke) Token: 0x0600DA46 RID: 55878
	public delegate void DelOnCollectionChanged();

	// Token: 0x0200144B RID: 5195
	// (Invoke) Token: 0x0600DA4A RID: 55882
	public delegate void DelOnDeckCreated(long id);

	// Token: 0x0200144C RID: 5196
	// (Invoke) Token: 0x0600DA4E RID: 55886
	public delegate void DelOnDeckDeleted(CollectionDeck removedDeck);

	// Token: 0x0200144D RID: 5197
	// (Invoke) Token: 0x0600DA52 RID: 55890
	public delegate void DelOnDeckContents(long id);

	// Token: 0x0200144E RID: 5198
	// (Invoke) Token: 0x0600DA56 RID: 55894
	public delegate void DelOnAllDeckContents();

	// Token: 0x0200144F RID: 5199
	// (Invoke) Token: 0x0600DA5A RID: 55898
	public delegate void DelOnNewCardSeen(string cardID, TAG_PREMIUM premium);

	// Token: 0x02001450 RID: 5200
	// (Invoke) Token: 0x0600DA5E RID: 55902
	public delegate void DelOnCardRewardsInserted(List<string> cardIDs, List<TAG_PREMIUM> premium);

	// Token: 0x02001451 RID: 5201
	// (Invoke) Token: 0x0600DA62 RID: 55906
	public delegate void DelOnAchievesCompleted(List<global::Achievement> achievements);

	// Token: 0x02001452 RID: 5202
	// (Invoke) Token: 0x0600DA66 RID: 55910
	public delegate void OnMassDisenchant(int amount);

	// Token: 0x02001453 RID: 5203
	// (Invoke) Token: 0x0600DA6A RID: 55914
	public delegate void OnEditedDeckChanged(CollectionDeck newDeck, CollectionDeck oldDeck, object callbackData);

	// Token: 0x02001454 RID: 5204
	// (Invoke) Token: 0x0600DA6E RID: 55918
	public delegate void FavoriteHeroChangedCallback(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero, object userData);

	// Token: 0x02001455 RID: 5205
	// (Invoke) Token: 0x0600DA72 RID: 55922
	public delegate void OnUIHeroOverrideCardRemovedCallback();

	// Token: 0x02001456 RID: 5206
	// (Invoke) Token: 0x0600DA76 RID: 55926
	public delegate void DeckAutoFillCallback(CollectionDeck deck, IEnumerable<DeckMaker.DeckFill> deckFill);

	// Token: 0x02001457 RID: 5207
	private class TagCardSetEnumComparer : IEqualityComparer<TAG_CARD_SET>
	{
		// Token: 0x0600DA79 RID: 55929 RVA: 0x003F15F5 File Offset: 0x003EF7F5
		public bool Equals(TAG_CARD_SET x, TAG_CARD_SET y)
		{
			return x == y;
		}

		// Token: 0x0600DA7A RID: 55930 RVA: 0x0028AEDB File Offset: 0x002890DB
		public int GetHashCode(TAG_CARD_SET obj)
		{
			return (int)obj;
		}
	}

	// Token: 0x02001458 RID: 5208
	private class TagClassEnumComparer : IEqualityComparer<TAG_CLASS>
	{
		// Token: 0x0600DA7C RID: 55932 RVA: 0x003F15F5 File Offset: 0x003EF7F5
		public bool Equals(TAG_CLASS x, TAG_CLASS y)
		{
			return x == y;
		}

		// Token: 0x0600DA7D RID: 55933 RVA: 0x0028AEDB File Offset: 0x002890DB
		public int GetHashCode(TAG_CLASS obj)
		{
			return (int)obj;
		}
	}

	// Token: 0x02001459 RID: 5209
	private class TagCardTypeEnumComparer : IEqualityComparer<TAG_CARDTYPE>
	{
		// Token: 0x0600DA7F RID: 55935 RVA: 0x003F15F5 File Offset: 0x003EF7F5
		public bool Equals(TAG_CARDTYPE x, TAG_CARDTYPE y)
		{
			return x == y;
		}

		// Token: 0x0600DA80 RID: 55936 RVA: 0x0028AEDB File Offset: 0x002890DB
		public int GetHashCode(TAG_CARDTYPE obj)
		{
			return (int)obj;
		}
	}

	// Token: 0x0200145A RID: 5210
	private struct CollectibleCardIndex
	{
		// Token: 0x0600DA82 RID: 55938 RVA: 0x003F15FB File Offset: 0x003EF7FB
		public CollectibleCardIndex(string cardId, TAG_PREMIUM premium)
		{
			this.CardId = cardId;
			this.Premium = premium;
		}

		// Token: 0x0400A981 RID: 43393
		public string CardId;

		// Token: 0x0400A982 RID: 43394
		public TAG_PREMIUM Premium;
	}

	// Token: 0x0200145B RID: 5211
	private class CollectibleCardIndexComparer : IEqualityComparer<CollectionManager.CollectibleCardIndex>
	{
		// Token: 0x0600DA83 RID: 55939 RVA: 0x003F160B File Offset: 0x003EF80B
		public bool Equals(CollectionManager.CollectibleCardIndex x, CollectionManager.CollectibleCardIndex y)
		{
			return x.CardId == y.CardId && x.Premium == y.Premium;
		}

		// Token: 0x0600DA84 RID: 55940 RVA: 0x003F1630 File Offset: 0x003EF830
		public int GetHashCode(CollectionManager.CollectibleCardIndex obj)
		{
			return obj.CardId.GetHashCode();
		}
	}

	// Token: 0x0200145C RID: 5212
	private class FavoriteHeroChangedListener : global::EventListener<CollectionManager.FavoriteHeroChangedCallback>
	{
		// Token: 0x0600DA86 RID: 55942 RVA: 0x003F163D File Offset: 0x003EF83D
		public void Fire(TAG_CLASS heroClass, NetCache.CardDefinition favoriteHero)
		{
			this.m_callback(heroClass, favoriteHero, this.m_userData);
		}
	}

	// Token: 0x0200145D RID: 5213
	private class OnUIHeroOverrideCardRemovedListener : global::EventListener<CollectionManager.OnUIHeroOverrideCardRemovedCallback>
	{
		// Token: 0x0600DA88 RID: 55944 RVA: 0x003F165A File Offset: 0x003EF85A
		public void Fire()
		{
			this.m_callback();
		}
	}

	// Token: 0x0200145E RID: 5214
	private class PendingDeckCreateData
	{
		// Token: 0x0400A983 RID: 43395
		public DeckType m_deckType;

		// Token: 0x0400A984 RID: 43396
		public string m_name;

		// Token: 0x0400A985 RID: 43397
		public int m_heroDbId;

		// Token: 0x0400A986 RID: 43398
		public TAG_PREMIUM m_heroPremium;

		// Token: 0x0400A987 RID: 43399
		public FormatType m_formatType;

		// Token: 0x0400A988 RID: 43400
		public DeckSourceType m_sourceType;

		// Token: 0x0400A989 RID: 43401
		public string m_pastedDeckHash;
	}

	// Token: 0x0200145F RID: 5215
	private class PendingDeckDeleteData
	{
		// Token: 0x0400A98A RID: 43402
		public long m_deckId;
	}

	// Token: 0x02001460 RID: 5216
	private class PendingDeckEditData
	{
		// Token: 0x0400A98B RID: 43403
		public long m_deckId;
	}

	// Token: 0x02001461 RID: 5217
	private class PendingDeckRenameData
	{
		// Token: 0x0400A98C RID: 43404
		public long m_deckId;

		// Token: 0x0400A98D RID: 43405
		public string m_name;
	}

	// Token: 0x02001462 RID: 5218
	public class DeckSort : IComparer<CollectionDeck>
	{
		// Token: 0x0600DA8E RID: 55950 RVA: 0x003F166F File Offset: 0x003EF86F
		public int Compare(CollectionDeck a, CollectionDeck b)
		{
			if (a.SortOrder == b.SortOrder)
			{
				return b.CreateDate.CompareTo(a.CreateDate);
			}
			return a.SortOrder.CompareTo(b.SortOrder);
		}

		// Token: 0x0400A98E RID: 43406
		public const int CUSTOM_STARTING_SORT_ORDER = -100;
	}
}
