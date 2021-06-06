using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000192 RID: 402
[Serializable]
public class CardBackDbfRecord : DbfRecord
{
	// Token: 0x17000223 RID: 547
	// (get) Token: 0x0600188A RID: 6282 RVA: 0x00085916 File Offset: 0x00083B16
	[DbfField("DATA1")]
	public long Data1
	{
		get
		{
			return this.m_data1;
		}
	}

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x0600188B RID: 6283 RVA: 0x0008591E File Offset: 0x00083B1E
	[DbfField("SOURCE")]
	public string Source
	{
		get
		{
			return this.m_source;
		}
	}

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x0600188C RID: 6284 RVA: 0x00085926 File Offset: 0x00083B26
	[DbfField("ENABLED")]
	public bool Enabled
	{
		get
		{
			return this.m_enabled;
		}
	}

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x0600188D RID: 6285 RVA: 0x0008592E File Offset: 0x00083B2E
	[DbfField("SORT_CATEGORY")]
	public Assets.CardBack.SortCategory SortCategory
	{
		get
		{
			return this.m_sortCategory;
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x0600188E RID: 6286 RVA: 0x00085936 File Offset: 0x00083B36
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x0600188F RID: 6287 RVA: 0x0008593E File Offset: 0x00083B3E
	[DbfField("PREFAB_NAME")]
	public string PrefabName
	{
		get
		{
			return this.m_prefabName;
		}
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06001890 RID: 6288 RVA: 0x00085946 File Offset: 0x00083B46
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06001891 RID: 6289 RVA: 0x0008594E File Offset: 0x00083B4E
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06001892 RID: 6290 RVA: 0x00085956 File Offset: 0x00083B56
	[DbfField("IS_RANDOM_CARD_BACK")]
	public bool IsRandomCardBack
	{
		get
		{
			return this.m_isRandomCardBack;
		}
	}

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06001893 RID: 6291 RVA: 0x0008595E File Offset: 0x00083B5E
	[DbfField("COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")]
	public int CollectionManagerPurchaseProductId
	{
		get
		{
			return this.m_collectionManagerPurchaseProductId;
		}
	}

	// Token: 0x06001894 RID: 6292 RVA: 0x00085966 File Offset: 0x00083B66
	public void SetData1(long v)
	{
		this.m_data1 = v;
	}

	// Token: 0x06001895 RID: 6293 RVA: 0x0008596F File Offset: 0x00083B6F
	public void SetSource(string v)
	{
		this.m_source = v;
	}

	// Token: 0x06001896 RID: 6294 RVA: 0x00085978 File Offset: 0x00083B78
	public void SetEnabled(bool v)
	{
		this.m_enabled = v;
	}

	// Token: 0x06001897 RID: 6295 RVA: 0x00085981 File Offset: 0x00083B81
	public void SetSortCategory(Assets.CardBack.SortCategory v)
	{
		this.m_sortCategory = v;
	}

	// Token: 0x06001898 RID: 6296 RVA: 0x0008598A File Offset: 0x00083B8A
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001899 RID: 6297 RVA: 0x00085993 File Offset: 0x00083B93
	public void SetPrefabName(string v)
	{
		this.m_prefabName = v;
	}

	// Token: 0x0600189A RID: 6298 RVA: 0x0008599C File Offset: 0x00083B9C
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x0600189B RID: 6299 RVA: 0x000859B6 File Offset: 0x00083BB6
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x0600189C RID: 6300 RVA: 0x000859D0 File Offset: 0x00083BD0
	public void SetIsRandomCardBack(bool v)
	{
		this.m_isRandomCardBack = v;
	}

	// Token: 0x0600189D RID: 6301 RVA: 0x000859D9 File Offset: 0x00083BD9
	public void SetCollectionManagerPurchaseProductId(int v)
	{
		this.m_collectionManagerPurchaseProductId = v;
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000859E4 File Offset: 0x00083BE4
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2294480894U)
		{
			if (num <= 1387956774U)
			{
				if (num != 1103584457U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return this.m_name;
						}
					}
				}
				else if (name == "DESCRIPTION")
				{
					return this.m_description;
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1821367228U)
				{
					if (num == 2294480894U)
					{
						if (name == "ENABLED")
						{
							return this.m_enabled;
						}
					}
				}
				else if (name == "DATA1")
				{
					return this.m_data1;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 2656358914U)
		{
			if (num != 2300801615U)
			{
				if (num != 2635266015U)
				{
					if (num == 2656358914U)
					{
						if (name == "IS_RANDOM_CARD_BACK")
						{
							return this.m_isRandomCardBack;
						}
					}
				}
				else if (name == "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")
				{
					return this.m_collectionManagerPurchaseProductId;
				}
			}
			else if (name == "PREFAB_NAME")
			{
				return this.m_prefabName;
			}
		}
		else if (num != 3111715480U)
		{
			if (num != 3923265666U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return this.m_sortOrder;
					}
				}
			}
			else if (name == "SORT_CATEGORY")
			{
				return this.m_sortCategory;
			}
		}
		else if (name == "SOURCE")
		{
			return this.m_source;
		}
		return null;
	}

	// Token: 0x0600189F RID: 6303 RVA: 0x00085BD4 File Offset: 0x00083DD4
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 2294480894U)
		{
			if (num <= 2656358914U)
			{
				if (num != 2300801615U)
				{
					if (num != 2635266015U)
					{
						if (num != 2656358914U)
						{
							return;
						}
						if (!(name == "IS_RANDOM_CARD_BACK"))
						{
							return;
						}
						this.m_isRandomCardBack = (bool)val;
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
					if (!(name == "PREFAB_NAME"))
					{
						return;
					}
					this.m_prefabName = (string)val;
					return;
				}
			}
			else if (num != 3111715480U)
			{
				if (num != 3923265666U)
				{
					if (num != 4214602626U)
					{
						return;
					}
					if (!(name == "SORT_ORDER"))
					{
						return;
					}
					this.m_sortOrder = (int)val;
					return;
				}
				else
				{
					if (!(name == "SORT_CATEGORY"))
					{
						return;
					}
					if (val == null)
					{
						this.m_sortCategory = Assets.CardBack.SortCategory.NONE;
						return;
					}
					if (val is Assets.CardBack.SortCategory || val is int)
					{
						this.m_sortCategory = (Assets.CardBack.SortCategory)val;
						return;
					}
					if (val is string)
					{
						this.m_sortCategory = Assets.CardBack.ParseSortCategoryValue((string)val);
						return;
					}
				}
			}
			else
			{
				if (!(name == "SOURCE"))
				{
					return;
				}
				this.m_source = (string)val;
				return;
			}
			return;
		}
		if (num <= 1387956774U)
		{
			if (num != 1103584457U)
			{
				if (num != 1387956774U)
				{
					return;
				}
				if (!(name == "NAME"))
				{
					return;
				}
				this.m_name = (DbfLocValue)val;
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
		else if (num != 1458105184U)
		{
			if (num != 1821367228U)
			{
				if (num != 2294480894U)
				{
					return;
				}
				if (!(name == "ENABLED"))
				{
					return;
				}
				this.m_enabled = (bool)val;
				return;
			}
			else
			{
				if (!(name == "DATA1"))
				{
					return;
				}
				this.m_data1 = (long)val;
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

	// Token: 0x060018A0 RID: 6304 RVA: 0x00085DEC File Offset: 0x00083FEC
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 2294480894U)
		{
			if (num <= 1387956774U)
			{
				if (num != 1103584457U)
				{
					if (num == 1387956774U)
					{
						if (name == "NAME")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "DESCRIPTION")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 1458105184U)
			{
				if (num != 1821367228U)
				{
					if (num == 2294480894U)
					{
						if (name == "ENABLED")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "DATA1")
				{
					return typeof(long);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 2656358914U)
		{
			if (num != 2300801615U)
			{
				if (num != 2635266015U)
				{
					if (num == 2656358914U)
					{
						if (name == "IS_RANDOM_CARD_BACK")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "COLLECTION_MANAGER_PURCHASE_PRODUCT_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "PREFAB_NAME")
			{
				return typeof(string);
			}
		}
		else if (num != 3111715480U)
		{
			if (num != 3923265666U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "SORT_CATEGORY")
			{
				return typeof(Assets.CardBack.SortCategory);
			}
		}
		else if (name == "SOURCE")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x060018A1 RID: 6305 RVA: 0x00085FEB File Offset: 0x000841EB
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardBackDbfRecords loadRecords = new LoadCardBackDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060018A2 RID: 6306 RVA: 0x00086004 File Offset: 0x00084204
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardBackDbfAsset cardBackDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardBackDbfAsset)) as CardBackDbfAsset;
		if (cardBackDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardBackDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardBackDbfAsset.Records.Count; i++)
		{
			cardBackDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardBackDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060018A3 RID: 6307 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060018A4 RID: 6308 RVA: 0x00086083 File Offset: 0x00084283
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x04000F52 RID: 3922
	[SerializeField]
	private long m_data1;

	// Token: 0x04000F53 RID: 3923
	[SerializeField]
	private string m_source = "TBD";

	// Token: 0x04000F54 RID: 3924
	[SerializeField]
	private bool m_enabled;

	// Token: 0x04000F55 RID: 3925
	[SerializeField]
	private Assets.CardBack.SortCategory m_sortCategory;

	// Token: 0x04000F56 RID: 3926
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04000F57 RID: 3927
	[SerializeField]
	private string m_prefabName;

	// Token: 0x04000F58 RID: 3928
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04000F59 RID: 3929
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04000F5A RID: 3930
	[SerializeField]
	private bool m_isRandomCardBack;

	// Token: 0x04000F5B RID: 3931
	[SerializeField]
	private int m_collectionManagerPurchaseProductId;
}
