using System;
using UnityEngine;

// Token: 0x020006BC RID: 1724
[CreateAssetMenu(fileName = "NewShopProductData", menuName = "ShopProductData", order = 59)]
public class ShopProductData : ScriptableObject
{
	// Token: 0x040050DC RID: 20700
	[SerializeField]
	public ShopProductData.ProductTierData[] productTierCatalog;

	// Token: 0x040050DD RID: 20701
	[SerializeField]
	public ShopProductData.ProductData[] productCatalog;

	// Token: 0x040050DE RID: 20702
	[SerializeField]
	public ShopProductData.ProductItemData[] productItemCatalog;

	// Token: 0x02002205 RID: 8709
	[Serializable]
	public struct ProductTierData
	{
		// Token: 0x0400E222 RID: 57890
		public string tierId;

		// Token: 0x0400E223 RID: 57891
		public string tags;

		// Token: 0x0400E224 RID: 57892
		public string header;

		// Token: 0x0400E225 RID: 57893
		public long[] productIds;
	}

	// Token: 0x02002206 RID: 8710
	[Serializable]
	public struct ProductData
	{
		// Token: 0x0400E226 RID: 57894
		public string name;

		// Token: 0x0400E227 RID: 57895
		public string description;

		// Token: 0x0400E228 RID: 57896
		public string tags;

		// Token: 0x0400E229 RID: 57897
		public long productId;

		// Token: 0x0400E22A RID: 57898
		public long[] licenseIds;

		// Token: 0x0400E22B RID: 57899
		public ShopProductData.PriceData[] prices;
	}

	// Token: 0x02002207 RID: 8711
	[Serializable]
	public struct ProductItemData
	{
		// Token: 0x0400E22C RID: 57900
		public string debugName;

		// Token: 0x0400E22D RID: 57901
		public long licenseId;

		// Token: 0x0400E22E RID: 57902
		public RewardItemType itemType;

		// Token: 0x0400E22F RID: 57903
		public int itemId;

		// Token: 0x0400E230 RID: 57904
		public int quantity;
	}

	// Token: 0x02002208 RID: 8712
	[Serializable]
	public struct PriceData
	{
		// Token: 0x0400E231 RID: 57905
		public CurrencyType currencyType;

		// Token: 0x0400E232 RID: 57906
		public double amount;
	}
}
