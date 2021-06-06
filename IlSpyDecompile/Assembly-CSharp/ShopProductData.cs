using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewShopProductData", menuName = "ShopProductData", order = 59)]
public class ShopProductData : ScriptableObject
{
	[Serializable]
	public struct ProductTierData
	{
		public string tierId;

		public string tags;

		public string header;

		public long[] productIds;
	}

	[Serializable]
	public struct ProductData
	{
		public string name;

		public string description;

		public string tags;

		public long productId;

		public long[] licenseIds;

		public PriceData[] prices;
	}

	[Serializable]
	public struct ProductItemData
	{
		public string debugName;

		public long licenseId;

		public RewardItemType itemType;

		public int itemId;

		public int quantity;
	}

	[Serializable]
	public struct PriceData
	{
		public CurrencyType currencyType;

		public double amount;
	}

	[SerializeField]
	public ProductTierData[] productTierCatalog;

	[SerializeField]
	public ProductData[] productCatalog;

	[SerializeField]
	public ProductItemData[] productItemCatalog;
}
