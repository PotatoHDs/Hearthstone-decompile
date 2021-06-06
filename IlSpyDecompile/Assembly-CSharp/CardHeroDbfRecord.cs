using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardHeroDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardId;

	[SerializeField]
	private int m_cardBackId;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private DbfLocValue m_storeDesc;

	[SerializeField]
	private DbfLocValue m_storeDescPhone;

	[SerializeField]
	private string m_storeBannerPrefab;

	[SerializeField]
	private string m_storeBackgroundTexture;

	[SerializeField]
	private int m_storeSortOrder;

	[SerializeField]
	private DbfLocValue m_purchaseCompleteMsg;

	[SerializeField]
	private CardHero.HeroType m_heroType;

	[SerializeField]
	private int m_collectionManagerPurchaseProductId;

	[DbfField("CARD_ID")]
	public int CardId => m_cardId;

	[DbfField("CARD_BACK_ID")]
	public int CardBackId => m_cardBackId;

	public CardBackDbfRecord CardBackRecord => GameDbf.CardBack.GetRecord(m_cardBackId);

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("STORE_DESC")]
	public DbfLocValue StoreDesc => m_storeDesc;

	[DbfField("STORE_DESC_PHONE")]
	public DbfLocValue StoreDescPhone => m_storeDescPhone;

	[DbfField("STORE_BANNER_PREFAB")]
	public string StoreBannerPrefab => m_storeBannerPrefab;

	[DbfField("STORE_BACKGROUND_TEXTURE")]
	public string StoreBackgroundTexture => m_storeBackgroundTexture;

	[DbfField("STORE_SORT_ORDER")]
	public int StoreSortOrder => m_storeSortOrder;

	[DbfField("PURCHASE_COMPLETE_MSG")]
	public DbfLocValue PurchaseCompleteMsg => m_purchaseCompleteMsg;

	[DbfField("HERO_TYPE")]
	public CardHero.HeroType HeroType => m_heroType;

	[DbfField("COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")]
	public int CollectionManagerPurchaseProductId => m_collectionManagerPurchaseProductId;

	public void SetCardId(int v)
	{
		m_cardId = v;
	}

	public void SetCardBackId(int v)
	{
		m_cardBackId = v;
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetStoreDesc(DbfLocValue v)
	{
		m_storeDesc = v;
		v.SetDebugInfo(base.ID, "STORE_DESC");
	}

	public void SetStoreDescPhone(DbfLocValue v)
	{
		m_storeDescPhone = v;
		v.SetDebugInfo(base.ID, "STORE_DESC_PHONE");
	}

	public void SetStoreBannerPrefab(string v)
	{
		m_storeBannerPrefab = v;
	}

	public void SetStoreBackgroundTexture(string v)
	{
		m_storeBackgroundTexture = v;
	}

	public void SetStoreSortOrder(int v)
	{
		m_storeSortOrder = v;
	}

	public void SetPurchaseCompleteMsg(DbfLocValue v)
	{
		m_purchaseCompleteMsg = v;
		v.SetDebugInfo(base.ID, "PURCHASE_COMPLETE_MSG");
	}

	public void SetHeroType(CardHero.HeroType v)
	{
		m_heroType = v;
	}

	public void SetCollectionManagerPurchaseProductId(int v)
	{
		m_collectionManagerPurchaseProductId = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"CARD_ID" => m_cardId, 
			"CARD_BACK_ID" => m_cardBackId, 
			"DESCRIPTION" => m_description, 
			"STORE_DESC" => m_storeDesc, 
			"STORE_DESC_PHONE" => m_storeDescPhone, 
			"STORE_BANNER_PREFAB" => m_storeBannerPrefab, 
			"STORE_BACKGROUND_TEXTURE" => m_storeBackgroundTexture, 
			"STORE_SORT_ORDER" => m_storeSortOrder, 
			"PURCHASE_COMPLETE_MSG" => m_purchaseCompleteMsg, 
			"HERO_TYPE" => m_heroType, 
			"COLLECTION_MANAGER_PURCHASE_PRODUCT_ID" => m_collectionManagerPurchaseProductId, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "ID":
			SetID((int)val);
			break;
		case "CARD_ID":
			m_cardId = (int)val;
			break;
		case "CARD_BACK_ID":
			m_cardBackId = (int)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "STORE_DESC":
			m_storeDesc = (DbfLocValue)val;
			break;
		case "STORE_DESC_PHONE":
			m_storeDescPhone = (DbfLocValue)val;
			break;
		case "STORE_BANNER_PREFAB":
			m_storeBannerPrefab = (string)val;
			break;
		case "STORE_BACKGROUND_TEXTURE":
			m_storeBackgroundTexture = (string)val;
			break;
		case "STORE_SORT_ORDER":
			m_storeSortOrder = (int)val;
			break;
		case "PURCHASE_COMPLETE_MSG":
			m_purchaseCompleteMsg = (DbfLocValue)val;
			break;
		case "HERO_TYPE":
			if (val == null)
			{
				m_heroType = CardHero.HeroType.UNKNOWN;
			}
			else if (val is CardHero.HeroType || val is int)
			{
				m_heroType = (CardHero.HeroType)val;
			}
			else if (val is string)
			{
				m_heroType = CardHero.ParseHeroTypeValue((string)val);
			}
			break;
		case "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID":
			m_collectionManagerPurchaseProductId = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"CARD_ID" => typeof(int), 
			"CARD_BACK_ID" => typeof(int), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"STORE_DESC" => typeof(DbfLocValue), 
			"STORE_DESC_PHONE" => typeof(DbfLocValue), 
			"STORE_BANNER_PREFAB" => typeof(string), 
			"STORE_BACKGROUND_TEXTURE" => typeof(string), 
			"STORE_SORT_ORDER" => typeof(int), 
			"PURCHASE_COMPLETE_MSG" => typeof(DbfLocValue), 
			"HERO_TYPE" => typeof(CardHero.HeroType), 
			"COLLECTION_MANAGER_PURCHASE_PRODUCT_ID" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardHeroDbfRecords loadRecords = new LoadCardHeroDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardHeroDbfAsset cardHeroDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardHeroDbfAsset)) as CardHeroDbfAsset;
		if (cardHeroDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardHeroDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardHeroDbfAsset.Records.Count; i++)
		{
			cardHeroDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardHeroDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_description.StripUnusedLocales();
		m_storeDesc.StripUnusedLocales();
		m_storeDescPhone.StripUnusedLocales();
		m_purchaseCompleteMsg.StripUnusedLocales();
	}
}
