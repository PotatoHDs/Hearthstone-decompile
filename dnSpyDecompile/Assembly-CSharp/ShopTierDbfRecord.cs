using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000274 RID: 628
[Serializable]
public class ShopTierDbfRecord : DbfRecord
{
	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x06002079 RID: 8313 RVA: 0x000A10A6 File Offset: 0x0009F2A6
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x0600207A RID: 8314 RVA: 0x000A10AE File Offset: 0x0009F2AE
	[DbfField("STYLE")]
	public string Style
	{
		get
		{
			return this.m_style;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x0600207B RID: 8315 RVA: 0x000A10B6 File Offset: 0x0009F2B6
	[DbfField("TAGS")]
	public string Tags
	{
		get
		{
			return this.m_tags;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x0600207C RID: 8316 RVA: 0x000A10BE File Offset: 0x0009F2BE
	[DbfField("HEADER")]
	public DbfLocValue Header
	{
		get
		{
			return this.m_header;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x0600207D RID: 8317 RVA: 0x000A10C6 File Offset: 0x0009F2C6
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x0600207E RID: 8318 RVA: 0x000A10CE File Offset: 0x0009F2CE
	[DbfField("DISABLED")]
	public bool Disabled
	{
		get
		{
			return this.m_disabled;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x0600207F RID: 8319 RVA: 0x000A10D6 File Offset: 0x0009F2D6
	public List<ShopTierProductSaleDbfRecord> ProductSales
	{
		get
		{
			return GameDbf.ShopTierProductSale.GetRecords((ShopTierProductSaleDbfRecord r) => r.TierId == base.ID, -1);
		}
	}

	// Token: 0x06002080 RID: 8320 RVA: 0x000A10EF File Offset: 0x0009F2EF
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06002081 RID: 8321 RVA: 0x000A10F8 File Offset: 0x0009F2F8
	public void SetStyle(string v)
	{
		this.m_style = v;
	}

	// Token: 0x06002082 RID: 8322 RVA: 0x000A1101 File Offset: 0x0009F301
	public void SetTags(string v)
	{
		this.m_tags = v;
	}

	// Token: 0x06002083 RID: 8323 RVA: 0x000A110A File Offset: 0x0009F30A
	public void SetHeader(DbfLocValue v)
	{
		this.m_header = v;
		v.SetDebugInfo(base.ID, "HEADER");
	}

	// Token: 0x06002084 RID: 8324 RVA: 0x000A1124 File Offset: 0x0009F324
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06002085 RID: 8325 RVA: 0x000A112D File Offset: 0x0009F32D
	public void SetDisabled(bool v)
	{
		this.m_disabled = v;
	}

	// Token: 0x06002086 RID: 8326 RVA: 0x000A1138 File Offset: 0x0009F338
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 304993157U)
			{
				if (num != 1450621046U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "STYLE")
				{
					return this.m_style;
				}
			}
			else if (name == "DISABLED")
			{
				return this.m_disabled;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2263853280U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return this.m_noteDesc;
					}
				}
			}
			else if (name == "HEADER")
			{
				return this.m_header;
			}
		}
		else if (num != 4187495584U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return this.m_sortOrder;
				}
			}
		}
		else if (name == "TAGS")
		{
			return this.m_tags;
		}
		return null;
	}

	// Token: 0x06002087 RID: 8327 RVA: 0x000A1258 File Offset: 0x0009F458
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 304993157U)
			{
				if (num != 1450621046U)
				{
					if (num != 1458105184U)
					{
						return;
					}
					if (!(name == "ID"))
					{
						return;
					}
					base.SetID((int)val);
					return;
				}
				else
				{
					if (!(name == "STYLE"))
					{
						return;
					}
					this.m_style = (string)val;
					return;
				}
			}
			else
			{
				if (!(name == "DISABLED"))
				{
					return;
				}
				this.m_disabled = (bool)val;
				return;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2263853280U)
			{
				if (num != 3022554311U)
				{
					return;
				}
				if (!(name == "NOTE_DESC"))
				{
					return;
				}
				this.m_noteDesc = (string)val;
				return;
			}
			else
			{
				if (!(name == "HEADER"))
				{
					return;
				}
				this.m_header = (DbfLocValue)val;
				return;
			}
		}
		else if (num != 4187495584U)
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
			if (!(name == "TAGS"))
			{
				return;
			}
			this.m_tags = (string)val;
			return;
		}
	}

	// Token: 0x06002088 RID: 8328 RVA: 0x000A1378 File Offset: 0x0009F578
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 304993157U)
			{
				if (num != 1450621046U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "STYLE")
				{
					return typeof(string);
				}
			}
			else if (name == "DISABLED")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2263853280U)
			{
				if (num == 3022554311U)
				{
					if (name == "NOTE_DESC")
					{
						return typeof(string);
					}
				}
			}
			else if (name == "HEADER")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num != 4187495584U)
		{
			if (num == 4214602626U)
			{
				if (name == "SORT_ORDER")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "TAGS")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06002089 RID: 8329 RVA: 0x000A14A6 File Offset: 0x0009F6A6
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadShopTierDbfRecords loadRecords = new LoadShopTierDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x000A14BC File Offset: 0x0009F6BC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ShopTierDbfAsset shopTierDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ShopTierDbfAsset)) as ShopTierDbfAsset;
		if (shopTierDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ShopTierDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < shopTierDbfAsset.Records.Count; i++)
		{
			shopTierDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (shopTierDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x000A153B File Offset: 0x0009F73B
	public override void StripUnusedLocales()
	{
		this.m_header.StripUnusedLocales();
	}

	// Token: 0x04001240 RID: 4672
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001241 RID: 4673
	[SerializeField]
	private string m_style;

	// Token: 0x04001242 RID: 4674
	[SerializeField]
	private string m_tags;

	// Token: 0x04001243 RID: 4675
	[SerializeField]
	private DbfLocValue m_header;

	// Token: 0x04001244 RID: 4676
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x04001245 RID: 4677
	[SerializeField]
	private bool m_disabled;
}
