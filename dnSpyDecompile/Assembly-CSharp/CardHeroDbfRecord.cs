using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200019E RID: 414
[Serializable]
public class CardHeroDbfRecord : DbfRecord
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x0600190E RID: 6414 RVA: 0x00087676 File Offset: 0x00085876
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x0600190F RID: 6415 RVA: 0x0008767E File Offset: 0x0008587E
	[DbfField("CARD_BACK_ID")]
	public int CardBackId
	{
		get
		{
			return this.m_cardBackId;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x06001910 RID: 6416 RVA: 0x00087686 File Offset: 0x00085886
	public CardBackDbfRecord CardBackRecord
	{
		get
		{
			return GameDbf.CardBack.GetRecord(this.m_cardBackId);
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06001911 RID: 6417 RVA: 0x00087698 File Offset: 0x00085898
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000251 RID: 593
	// (get) Token: 0x06001912 RID: 6418 RVA: 0x000876A0 File Offset: 0x000858A0
	[DbfField("STORE_DESC")]
	public DbfLocValue StoreDesc
	{
		get
		{
			return this.m_storeDesc;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06001913 RID: 6419 RVA: 0x000876A8 File Offset: 0x000858A8
	[DbfField("STORE_DESC_PHONE")]
	public DbfLocValue StoreDescPhone
	{
		get
		{
			return this.m_storeDescPhone;
		}
	}

	// Token: 0x17000253 RID: 595
	// (get) Token: 0x06001914 RID: 6420 RVA: 0x000876B0 File Offset: 0x000858B0
	[DbfField("STORE_BANNER_PREFAB")]
	public string StoreBannerPrefab
	{
		get
		{
			return this.m_storeBannerPrefab;
		}
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06001915 RID: 6421 RVA: 0x000876B8 File Offset: 0x000858B8
	[DbfField("STORE_BACKGROUND_TEXTURE")]
	public string StoreBackgroundTexture
	{
		get
		{
			return this.m_storeBackgroundTexture;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06001916 RID: 6422 RVA: 0x000876C0 File Offset: 0x000858C0
	[DbfField("STORE_SORT_ORDER")]
	public int StoreSortOrder
	{
		get
		{
			return this.m_storeSortOrder;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06001917 RID: 6423 RVA: 0x000876C8 File Offset: 0x000858C8
	[DbfField("PURCHASE_COMPLETE_MSG")]
	public DbfLocValue PurchaseCompleteMsg
	{
		get
		{
			return this.m_purchaseCompleteMsg;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06001918 RID: 6424 RVA: 0x000876D0 File Offset: 0x000858D0
	[DbfField("HERO_TYPE")]
	public CardHero.HeroType HeroType
	{
		get
		{
			return this.m_heroType;
		}
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06001919 RID: 6425 RVA: 0x000876D8 File Offset: 0x000858D8
	[DbfField("COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")]
	public int CollectionManagerPurchaseProductId
	{
		get
		{
			return this.m_collectionManagerPurchaseProductId;
		}
	}

	// Token: 0x0600191A RID: 6426 RVA: 0x000876E0 File Offset: 0x000858E0
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x0600191B RID: 6427 RVA: 0x000876E9 File Offset: 0x000858E9
	public void SetCardBackId(int v)
	{
		this.m_cardBackId = v;
	}

	// Token: 0x0600191C RID: 6428 RVA: 0x000876F2 File Offset: 0x000858F2
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x0600191D RID: 6429 RVA: 0x0008770C File Offset: 0x0008590C
	public void SetStoreDesc(DbfLocValue v)
	{
		this.m_storeDesc = v;
		v.SetDebugInfo(base.ID, "STORE_DESC");
	}

	// Token: 0x0600191E RID: 6430 RVA: 0x00087726 File Offset: 0x00085926
	public void SetStoreDescPhone(DbfLocValue v)
	{
		this.m_storeDescPhone = v;
		v.SetDebugInfo(base.ID, "STORE_DESC_PHONE");
	}

	// Token: 0x0600191F RID: 6431 RVA: 0x00087740 File Offset: 0x00085940
	public void SetStoreBannerPrefab(string v)
	{
		this.m_storeBannerPrefab = v;
	}

	// Token: 0x06001920 RID: 6432 RVA: 0x00087749 File Offset: 0x00085949
	public void SetStoreBackgroundTexture(string v)
	{
		this.m_storeBackgroundTexture = v;
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x00087752 File Offset: 0x00085952
	public void SetStoreSortOrder(int v)
	{
		this.m_storeSortOrder = v;
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x0008775B File Offset: 0x0008595B
	public void SetPurchaseCompleteMsg(DbfLocValue v)
	{
		this.m_purchaseCompleteMsg = v;
		v.SetDebugInfo(base.ID, "PURCHASE_COMPLETE_MSG");
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x00087775 File Offset: 0x00085975
	public void SetHeroType(CardHero.HeroType v)
	{
		this.m_heroType = v;
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x0008777E File Offset: 0x0008597E
	public void SetCollectionManagerPurchaseProductId(int v)
	{
		this.m_collectionManagerPurchaseProductId = v;
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x00087788 File Offset: 0x00085988
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1263240540U)
		{
			if (num <= 451390141U)
			{
				if (num != 7265668U)
				{
					if (num != 439569377U)
					{
						if (num == 451390141U)
						{
							if (name == "CARD_ID")
							{
								return this.m_cardId;
							}
						}
					}
					else if (name == "STORE_DESC_PHONE")
					{
						return this.m_storeDescPhone;
					}
				}
				else if (name == "STORE_SORT_ORDER")
				{
					return this.m_storeSortOrder;
				}
			}
			else if (num != 874236059U)
			{
				if (num != 1103584457U)
				{
					if (num == 1263240540U)
					{
						if (name == "STORE_BANNER_PREFAB")
						{
							return this.m_storeBannerPrefab;
						}
					}
				}
				else if (name == "DESCRIPTION")
				{
					return this.m_description;
				}
			}
			else if (name == "STORE_BACKGROUND_TEXTURE")
			{
				return this.m_storeBackgroundTexture;
			}
		}
		else if (num <= 1640535074U)
		{
			if (num != 1458105184U)
			{
				if (num != 1560548161U)
				{
					if (num == 1640535074U)
					{
						if (name == "HERO_TYPE")
						{
							return this.m_heroType;
						}
					}
				}
				else if (name == "CARD_BACK_ID")
				{
					return this.m_cardBackId;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num != 1672933598U)
		{
			if (num != 2635266015U)
			{
				if (num == 3899252054U)
				{
					if (name == "PURCHASE_COMPLETE_MSG")
					{
						return this.m_purchaseCompleteMsg;
					}
				}
			}
			else if (name == "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")
			{
				return this.m_collectionManagerPurchaseProductId;
			}
		}
		else if (name == "STORE_DESC")
		{
			return this.m_storeDesc;
		}
		return null;
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x000879A4 File Offset: 0x00085BA4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 1263240540U)
		{
			if (num <= 1640535074U)
			{
				if (num != 1458105184U)
				{
					if (num != 1560548161U)
					{
						if (num != 1640535074U)
						{
							return;
						}
						if (!(name == "HERO_TYPE"))
						{
							return;
						}
						if (val == null)
						{
							this.m_heroType = CardHero.HeroType.UNKNOWN;
							return;
						}
						if (val is CardHero.HeroType || val is int)
						{
							this.m_heroType = (CardHero.HeroType)val;
							return;
						}
						if (val is string)
						{
							this.m_heroType = CardHero.ParseHeroTypeValue((string)val);
							return;
						}
					}
					else
					{
						if (!(name == "CARD_BACK_ID"))
						{
							return;
						}
						this.m_cardBackId = (int)val;
						return;
					}
				}
				else
				{
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
			}
			else if (num != 1672933598U)
			{
				if (num != 2635266015U)
				{
					if (num != 3899252054U)
					{
						return;
					}
					if (!(name == "PURCHASE_COMPLETE_MSG"))
					{
						return;
					}
					this.m_purchaseCompleteMsg = (DbfLocValue)val;
					return;
				}
				else
				{
					if (!(name == "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID"))
					{
						return;
					}
					this.m_collectionManagerPurchaseProductId = (int)val;
				}
			}
			else
			{
				if (!(name == "STORE_DESC"))
				{
					return;
				}
				this.m_storeDesc = (DbfLocValue)val;
				return;
			}
			return;
		}
		if (num <= 451390141U)
		{
			if (num != 7265668U)
			{
				if (num != 439569377U)
				{
					if (num != 451390141U)
					{
						return;
					}
					if (!(name == "CARD_ID"))
					{
						return;
					}
					this.m_cardId = (int)val;
					return;
				}
				else
				{
					if (!(name == "STORE_DESC_PHONE"))
					{
						return;
					}
					this.m_storeDescPhone = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "STORE_SORT_ORDER"))
				{
					return;
				}
				this.m_storeSortOrder = (int)val;
				return;
			}
		}
		else if (num != 874236059U)
		{
			if (num != 1103584457U)
			{
				if (num != 1263240540U)
				{
					return;
				}
				if (!(name == "STORE_BANNER_PREFAB"))
				{
					return;
				}
				this.m_storeBannerPrefab = (string)val;
				return;
			}
			else
			{
				if (!(name == "DESCRIPTION"))
				{
					return;
				}
				this.m_description = (DbfLocValue)val;
				return;
			}
		}
		else
		{
			if (!(name == "STORE_BACKGROUND_TEXTURE"))
			{
				return;
			}
			this.m_storeBackgroundTexture = (string)val;
			return;
		}
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x00087BE8 File Offset: 0x00085DE8
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1263240540U)
		{
			if (num <= 451390141U)
			{
				if (num != 7265668U)
				{
					if (num != 439569377U)
					{
						if (num == 451390141U)
						{
							if (name == "CARD_ID")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "STORE_DESC_PHONE")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (name == "STORE_SORT_ORDER")
				{
					return typeof(int);
				}
			}
			else if (num != 874236059U)
			{
				if (num != 1103584457U)
				{
					if (num == 1263240540U)
					{
						if (name == "STORE_BANNER_PREFAB")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "DESCRIPTION")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "STORE_BACKGROUND_TEXTURE")
			{
				return typeof(string);
			}
		}
		else if (num <= 1640535074U)
		{
			if (num != 1458105184U)
			{
				if (num != 1560548161U)
				{
					if (num == 1640535074U)
					{
						if (name == "HERO_TYPE")
						{
							return typeof(CardHero.HeroType);
						}
					}
				}
				else if (name == "CARD_BACK_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num != 1672933598U)
		{
			if (num != 2635266015U)
			{
				if (num == 3899252054U)
				{
					if (name == "PURCHASE_COMPLETE_MSG")
					{
						return typeof(DbfLocValue);
					}
				}
			}
			else if (name == "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")
			{
				return typeof(int);
			}
		}
		else if (name == "STORE_DESC")
		{
			return typeof(DbfLocValue);
		}
		return null;
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x00087E21 File Offset: 0x00086021
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardHeroDbfRecords loadRecords = new LoadCardHeroDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x00087E38 File Offset: 0x00086038
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardHeroDbfAsset cardHeroDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardHeroDbfAsset)) as CardHeroDbfAsset;
		if (cardHeroDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardHeroDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardHeroDbfAsset.Records.Count; i++)
		{
			cardHeroDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardHeroDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x00087EB7 File Offset: 0x000860B7
	public override void StripUnusedLocales()
	{
		this.m_description.StripUnusedLocales();
		this.m_storeDesc.StripUnusedLocales();
		this.m_storeDescPhone.StripUnusedLocales();
		this.m_purchaseCompleteMsg.StripUnusedLocales();
	}

	// Token: 0x04000F7E RID: 3966
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000F7F RID: 3967
	[SerializeField]
	private int m_cardBackId;

	// Token: 0x04000F80 RID: 3968
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000F81 RID: 3969
	[SerializeField]
	private DbfLocValue m_storeDesc;

	// Token: 0x04000F82 RID: 3970
	[SerializeField]
	private DbfLocValue m_storeDescPhone;

	// Token: 0x04000F83 RID: 3971
	[SerializeField]
	private string m_storeBannerPrefab;

	// Token: 0x04000F84 RID: 3972
	[SerializeField]
	private string m_storeBackgroundTexture;

	// Token: 0x04000F85 RID: 3973
	[SerializeField]
	private int m_storeSortOrder;

	// Token: 0x04000F86 RID: 3974
	[SerializeField]
	private DbfLocValue m_purchaseCompleteMsg;

	// Token: 0x04000F87 RID: 3975
	[SerializeField]
	private CardHero.HeroType m_heroType;

	// Token: 0x04000F88 RID: 3976
	[SerializeField]
	private int m_collectionManagerPurchaseProductId;
}
