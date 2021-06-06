using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

[CustomEditClass]
public static class HeroSkinUtils
{
	public static bool IsHeroSkinOwned(string cardId)
	{
		return CollectionManager.Get().IsCardOwned(cardId);
	}

	public static bool CanFavoriteHeroSkin(TAG_CLASS heroClass, string cardId)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(heroClass);
			if (IsHeroSkinOwned(cardId) && favoriteHero != null)
			{
				return favoriteHero.Name != cardId;
			}
			return false;
		}
		return false;
	}

	public static int GetCollectionManagerHeroSkinPurchaseProductId(string cardId)
	{
		CardHeroDbfRecord cardHeroDbfRecord = GameDbf.CardHero.GetRecords().Find((CardHeroDbfRecord r) => GameUtils.TranslateDbIdToCardId(r.CardId) == cardId);
		if (cardHeroDbfRecord == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPurchaseProductId failed to find Hero card " + cardId + " in the CardHero database");
			return 0;
		}
		return cardHeroDbfRecord.CollectionManagerPurchaseProductId;
	}

	public static bool CanBuyHeroSkinFromCollectionManager(string cardId)
	{
		if (IsHeroSkinOwned(cardId))
		{
			return false;
		}
		if (!IsHeroSkinPurchasableFromCollectionManager(cardId))
		{
			return false;
		}
		if (NetCache.Get().GetGoldBalance() < GetCollectionManagerHeroSkinGoldCost(cardId))
		{
			return false;
		}
		return true;
	}

	public static bool IsHeroSkinPurchasableFromCollectionManager(string cardId)
	{
		if (!StoreManager.Get().IsOpen(printStatus: false))
		{
			return false;
		}
		if (!StoreManager.Get().IsBuyHeroSkinsFromCollectionManagerEnabled())
		{
			return false;
		}
		if (GetCollectionManagerHeroSkinPurchaseProductId(cardId) <= 0)
		{
			return false;
		}
		if (GetCollectionManagerHeroSkinPriceDataModel(cardId) == null)
		{
			Debug.LogError("HeroSkinUtils:IsHeroSkinPurchasableFromCollectionManager failed to get the price data model for Hero card " + cardId);
			return false;
		}
		return true;
	}

	public static Network.Bundle GetCollectionManagerHeroSkinProductBundle(string cardId)
	{
		int collectionManagerHeroSkinPurchaseProductId = GetCollectionManagerHeroSkinPurchaseProductId(cardId);
		if (collectionManagerHeroSkinPurchaseProductId <= 0)
		{
			return null;
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId);
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId(collectionManagerHeroSkinPurchaseProductId);
		if (bundleFromPmtProductId == null)
		{
			Debug.LogError($"HeroSkinUtils:GetCollectionManagerHeroSkinProductBundle: Did not find a bundle with pmtProductId {collectionManagerHeroSkinPurchaseProductId} for Hero card {cardId}");
			return null;
		}
		if (!bundleFromPmtProductId.Items.Any((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_HERO && x.ProductData == cardDbId))
		{
			Debug.LogError($"HeroSkinUtils:GetCollectionManagerHeroSkinProductBundle: Did not find any items with type PRODUCT_TYPE_HERO for bundle with pmtProductId {collectionManagerHeroSkinPurchaseProductId} for Hero card {cardId}");
			return null;
		}
		return bundleFromPmtProductId;
	}

	public static PriceDataModel GetCollectionManagerHeroSkinPriceDataModel(string cardId)
	{
		Network.Bundle collectionManagerHeroSkinProductBundle = GetCollectionManagerHeroSkinProductBundle(cardId);
		if (collectionManagerHeroSkinProductBundle == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPriceDataModel failed to get bundle for Hero card " + cardId);
			return null;
		}
		if (!collectionManagerHeroSkinProductBundle.GtappGoldCost.HasValue)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPriceDataModel bundle for Hero card " + cardId + " has no GTAPP gold cost");
			return null;
		}
		return new PriceDataModel
		{
			Currency = CurrencyType.GOLD,
			Amount = collectionManagerHeroSkinProductBundle.GtappGoldCost.Value,
			DisplayText = Mathf.RoundToInt(collectionManagerHeroSkinProductBundle.GtappGoldCost.Value).ToString()
		};
	}

	public static long GetCollectionManagerHeroSkinGoldCost(string cardId)
	{
		Network.Bundle collectionManagerHeroSkinProductBundle = GetCollectionManagerHeroSkinProductBundle(cardId);
		if (collectionManagerHeroSkinProductBundle == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinGoldCost called for a card back with no valid product bundle. Hero card Id = " + cardId);
			return 0L;
		}
		if (!collectionManagerHeroSkinProductBundle.GtappGoldCost.HasValue)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinGoldCost called for a card back with no gold cost. Hero card Id = " + cardId);
			return 0L;
		}
		return collectionManagerHeroSkinProductBundle.GtappGoldCost.Value;
	}
}
