using System;
using System.Linq;
using Hearthstone.DataModels;
using PegasusUtil;
using UnityEngine;

// Token: 0x0200012C RID: 300
[CustomEditClass]
public static class HeroSkinUtils
{
	// Token: 0x060013FD RID: 5117 RVA: 0x00072D17 File Offset: 0x00070F17
	public static bool IsHeroSkinOwned(string cardId)
	{
		return CollectionManager.Get().IsCardOwned(cardId);
	}

	// Token: 0x060013FE RID: 5118 RVA: 0x00072D24 File Offset: 0x00070F24
	public static bool CanFavoriteHeroSkin(TAG_CLASS heroClass, string cardId)
	{
		if (!CollectionManager.Get().IsInEditMode())
		{
			NetCache.CardDefinition favoriteHero = CollectionManager.Get().GetFavoriteHero(heroClass);
			return HeroSkinUtils.IsHeroSkinOwned(cardId) && favoriteHero != null && favoriteHero.Name != cardId;
		}
		return false;
	}

	// Token: 0x060013FF RID: 5119 RVA: 0x00072D64 File Offset: 0x00070F64
	public static int GetCollectionManagerHeroSkinPurchaseProductId(string cardId)
	{
		CardHeroDbfRecord cardHeroDbfRecord = GameDbf.CardHero.GetRecords().Find((CardHeroDbfRecord r) => GameUtils.TranslateDbIdToCardId(r.CardId, false) == cardId);
		if (cardHeroDbfRecord == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPurchaseProductId failed to find Hero card " + cardId + " in the CardHero database");
			return 0;
		}
		return cardHeroDbfRecord.CollectionManagerPurchaseProductId;
	}

	// Token: 0x06001400 RID: 5120 RVA: 0x00072DBF File Offset: 0x00070FBF
	public static bool CanBuyHeroSkinFromCollectionManager(string cardId)
	{
		return !HeroSkinUtils.IsHeroSkinOwned(cardId) && HeroSkinUtils.IsHeroSkinPurchasableFromCollectionManager(cardId) && NetCache.Get().GetGoldBalance() >= HeroSkinUtils.GetCollectionManagerHeroSkinGoldCost(cardId);
	}

	// Token: 0x06001401 RID: 5121 RVA: 0x00072DEC File Offset: 0x00070FEC
	public static bool IsHeroSkinPurchasableFromCollectionManager(string cardId)
	{
		if (!StoreManager.Get().IsOpen(false))
		{
			return false;
		}
		if (!StoreManager.Get().IsBuyHeroSkinsFromCollectionManagerEnabled())
		{
			return false;
		}
		if (HeroSkinUtils.GetCollectionManagerHeroSkinPurchaseProductId(cardId) <= 0)
		{
			return false;
		}
		if (HeroSkinUtils.GetCollectionManagerHeroSkinPriceDataModel(cardId) == null)
		{
			Debug.LogError("HeroSkinUtils:IsHeroSkinPurchasableFromCollectionManager failed to get the price data model for Hero card " + cardId);
			return false;
		}
		return true;
	}

	// Token: 0x06001402 RID: 5122 RVA: 0x00072E3C File Offset: 0x0007103C
	public static Network.Bundle GetCollectionManagerHeroSkinProductBundle(string cardId)
	{
		int collectionManagerHeroSkinPurchaseProductId = HeroSkinUtils.GetCollectionManagerHeroSkinPurchaseProductId(cardId);
		if (collectionManagerHeroSkinPurchaseProductId <= 0)
		{
			return null;
		}
		int cardDbId = GameUtils.TranslateCardIdToDbId(cardId, false);
		Network.Bundle bundleFromPmtProductId = StoreManager.Get().GetBundleFromPmtProductId((long)collectionManagerHeroSkinPurchaseProductId);
		if (bundleFromPmtProductId == null)
		{
			Debug.LogError(string.Format("HeroSkinUtils:GetCollectionManagerHeroSkinProductBundle: Did not find a bundle with pmtProductId {0} for Hero card {1}", collectionManagerHeroSkinPurchaseProductId, cardId));
			return null;
		}
		if (!bundleFromPmtProductId.Items.Any((Network.BundleItem x) => x.ItemType == ProductType.PRODUCT_TYPE_HERO && x.ProductData == cardDbId))
		{
			Debug.LogError(string.Format("HeroSkinUtils:GetCollectionManagerHeroSkinProductBundle: Did not find any items with type PRODUCT_TYPE_HERO for bundle with pmtProductId {0} for Hero card {1}", collectionManagerHeroSkinPurchaseProductId, cardId));
			return null;
		}
		return bundleFromPmtProductId;
	}

	// Token: 0x06001403 RID: 5123 RVA: 0x00072EC4 File Offset: 0x000710C4
	public static PriceDataModel GetCollectionManagerHeroSkinPriceDataModel(string cardId)
	{
		Network.Bundle collectionManagerHeroSkinProductBundle = HeroSkinUtils.GetCollectionManagerHeroSkinProductBundle(cardId);
		if (collectionManagerHeroSkinProductBundle == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPriceDataModel failed to get bundle for Hero card " + cardId);
			return null;
		}
		if (collectionManagerHeroSkinProductBundle.GtappGoldCost == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinPriceDataModel bundle for Hero card " + cardId + " has no GTAPP gold cost");
			return null;
		}
		return new PriceDataModel
		{
			Currency = CurrencyType.GOLD,
			Amount = (float)collectionManagerHeroSkinProductBundle.GtappGoldCost.Value,
			DisplayText = Mathf.RoundToInt((float)collectionManagerHeroSkinProductBundle.GtappGoldCost.Value).ToString()
		};
	}

	// Token: 0x06001404 RID: 5124 RVA: 0x00072F58 File Offset: 0x00071158
	public static long GetCollectionManagerHeroSkinGoldCost(string cardId)
	{
		Network.Bundle collectionManagerHeroSkinProductBundle = HeroSkinUtils.GetCollectionManagerHeroSkinProductBundle(cardId);
		if (collectionManagerHeroSkinProductBundle == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinGoldCost called for a card back with no valid product bundle. Hero card Id = " + cardId);
			return 0L;
		}
		if (collectionManagerHeroSkinProductBundle.GtappGoldCost == null)
		{
			Debug.LogError("HeroSkinUtils:GetCollectionManagerHeroSkinGoldCost called for a card back with no gold cost. Hero card Id = " + cardId);
			return 0L;
		}
		return collectionManagerHeroSkinProductBundle.GtappGoldCost.Value;
	}
}
