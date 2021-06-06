using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200021A RID: 538
[Serializable]
public class ModularBundleDbfRecord : DbfRecord
{
	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06001D33 RID: 7475 RVA: 0x000960E2 File Offset: 0x000942E2
	[DbfField("NAME")]
	public DbfLocValue Name
	{
		get
		{
			return this.m_name;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06001D34 RID: 7476 RVA: 0x000960EA File Offset: 0x000942EA
	[DbfField("SELECTOR_PREFAB")]
	public string SelectorPrefab
	{
		get
		{
			return this.m_selectorPrefab;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06001D35 RID: 7477 RVA: 0x000960F2 File Offset: 0x000942F2
	[DbfField("SELECTOR_PACK_AMOUNT_BANNER")]
	public int SelectorPackAmountBanner
	{
		get
		{
			return this.m_selectorPackAmountBanner;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06001D36 RID: 7478 RVA: 0x000960FA File Offset: 0x000942FA
	[DbfField("LAYOUT_BUTTON_SIZE")]
	public string LayoutButtonSize
	{
		get
		{
			return this.m_layoutButtonSize;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06001D37 RID: 7479 RVA: 0x00096102 File Offset: 0x00094302
	[DbfField("BACKGROUND")]
	public string Background
	{
		get
		{
			return this.m_background;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0009610A File Offset: 0x0009430A
	[DbfField("PLAYLIST")]
	public string Playlist
	{
		get
		{
			return this.m_playlist;
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00096112 File Offset: 0x00094312
	[DbfField("LOGO_TEXTURE")]
	public string LogoTexture
	{
		get
		{
			return this.m_logoTexture;
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0009611A File Offset: 0x0009431A
	[DbfField("LOGO_TEXTURE_GLOW")]
	public string LogoTextureGlow
	{
		get
		{
			return this.m_logoTextureGlow;
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00096122 File Offset: 0x00094322
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0009612A File Offset: 0x0009432A
	[DbfField("SHOW_AFTER_PURCHASE")]
	public bool ShowAfterPurchase
	{
		get
		{
			return this.m_showAfterPurchase;
		}
	}

	// Token: 0x06001D3D RID: 7485 RVA: 0x00096132 File Offset: 0x00094332
	public void SetName(DbfLocValue v)
	{
		this.m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	// Token: 0x06001D3E RID: 7486 RVA: 0x0009614C File Offset: 0x0009434C
	public void SetSelectorPrefab(string v)
	{
		this.m_selectorPrefab = v;
	}

	// Token: 0x06001D3F RID: 7487 RVA: 0x00096155 File Offset: 0x00094355
	public void SetSelectorPackAmountBanner(int v)
	{
		this.m_selectorPackAmountBanner = v;
	}

	// Token: 0x06001D40 RID: 7488 RVA: 0x0009615E File Offset: 0x0009435E
	public void SetLayoutButtonSize(string v)
	{
		this.m_layoutButtonSize = v;
	}

	// Token: 0x06001D41 RID: 7489 RVA: 0x00096167 File Offset: 0x00094367
	public void SetBackground(string v)
	{
		this.m_background = v;
	}

	// Token: 0x06001D42 RID: 7490 RVA: 0x00096170 File Offset: 0x00094370
	public void SetPlaylist(string v)
	{
		this.m_playlist = v;
	}

	// Token: 0x06001D43 RID: 7491 RVA: 0x00096179 File Offset: 0x00094379
	public void SetLogoTexture(string v)
	{
		this.m_logoTexture = v;
	}

	// Token: 0x06001D44 RID: 7492 RVA: 0x00096182 File Offset: 0x00094382
	public void SetLogoTextureGlow(string v)
	{
		this.m_logoTextureGlow = v;
	}

	// Token: 0x06001D45 RID: 7493 RVA: 0x0009618B File Offset: 0x0009438B
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x06001D46 RID: 7494 RVA: 0x00096194 File Offset: 0x00094394
	public void SetShowAfterPurchase(bool v)
	{
		this.m_showAfterPurchase = v;
	}

	// Token: 0x06001D47 RID: 7495 RVA: 0x000961A0 File Offset: 0x000943A0
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1397850731U)
		{
			if (num <= 589867744U)
			{
				if (num != 226638260U)
				{
					if (num == 589867744U)
					{
						if (name == "LAYOUT_BUTTON_SIZE")
						{
							return this.m_layoutButtonSize;
						}
					}
				}
				else if (name == "SELECTOR_PACK_AMOUNT_BANNER")
				{
					return this.m_selectorPackAmountBanner;
				}
			}
			else if (num != 670183358U)
			{
				if (num != 1387956774U)
				{
					if (num == 1397850731U)
					{
						if (name == "PLAYLIST")
						{
							return this.m_playlist;
						}
					}
				}
				else if (name == "NAME")
				{
					return this.m_name;
				}
			}
			else if (name == "LOGO_TEXTURE")
			{
				return this.m_logoTexture;
			}
		}
		else if (num <= 2319408461U)
		{
			if (num != 1458105184U)
			{
				if (num != 2200459534U)
				{
					if (num == 2319408461U)
					{
						if (name == "SHOW_AFTER_PURCHASE")
						{
							return this.m_showAfterPurchase;
						}
					}
				}
				else if (name == "LOGO_TEXTURE_GLOW")
				{
					return this.m_logoTextureGlow;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num != 2601291421U)
		{
			if (num != 3656563063U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return this.m_sortOrder;
					}
				}
			}
			else if (name == "SELECTOR_PREFAB")
			{
				return this.m_selectorPrefab;
			}
		}
		else if (name == "BACKGROUND")
		{
			return this.m_background;
		}
		return null;
	}

	// Token: 0x06001D48 RID: 7496 RVA: 0x00096384 File Offset: 0x00094584
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1397850731U)
		{
			if (num <= 589867744U)
			{
				if (num != 226638260U)
				{
					if (num != 589867744U)
					{
						return;
					}
					if (!(name == "LAYOUT_BUTTON_SIZE"))
					{
						return;
					}
					this.m_layoutButtonSize = (string)val;
					return;
				}
				else
				{
					if (!(name == "SELECTOR_PACK_AMOUNT_BANNER"))
					{
						return;
					}
					this.m_selectorPackAmountBanner = (int)val;
					return;
				}
			}
			else if (num != 670183358U)
			{
				if (num != 1387956774U)
				{
					if (num != 1397850731U)
					{
						return;
					}
					if (!(name == "PLAYLIST"))
					{
						return;
					}
					this.m_playlist = (string)val;
					return;
				}
				else
				{
					if (!(name == "NAME"))
					{
						return;
					}
					this.m_name = (DbfLocValue)val;
					return;
				}
			}
			else
			{
				if (!(name == "LOGO_TEXTURE"))
				{
					return;
				}
				this.m_logoTexture = (string)val;
				return;
			}
		}
		else if (num <= 2319408461U)
		{
			if (num != 1458105184U)
			{
				if (num != 2200459534U)
				{
					if (num != 2319408461U)
					{
						return;
					}
					if (!(name == "SHOW_AFTER_PURCHASE"))
					{
						return;
					}
					this.m_showAfterPurchase = (bool)val;
					return;
				}
				else
				{
					if (!(name == "LOGO_TEXTURE_GLOW"))
					{
						return;
					}
					this.m_logoTextureGlow = (string)val;
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
		else if (num != 2601291421U)
		{
			if (num != 3656563063U)
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
				if (!(name == "SELECTOR_PREFAB"))
				{
					return;
				}
				this.m_selectorPrefab = (string)val;
				return;
			}
		}
		else
		{
			if (!(name == "BACKGROUND"))
			{
				return;
			}
			this.m_background = (string)val;
			return;
		}
	}

	// Token: 0x06001D49 RID: 7497 RVA: 0x0009656C File Offset: 0x0009476C
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1397850731U)
		{
			if (num <= 589867744U)
			{
				if (num != 226638260U)
				{
					if (num == 589867744U)
					{
						if (name == "LAYOUT_BUTTON_SIZE")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "SELECTOR_PACK_AMOUNT_BANNER")
				{
					return typeof(int);
				}
			}
			else if (num != 670183358U)
			{
				if (num != 1387956774U)
				{
					if (num == 1397850731U)
					{
						if (name == "PLAYLIST")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "NAME")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (name == "LOGO_TEXTURE")
			{
				return typeof(string);
			}
		}
		else if (num <= 2319408461U)
		{
			if (num != 1458105184U)
			{
				if (num != 2200459534U)
				{
					if (num == 2319408461U)
					{
						if (name == "SHOW_AFTER_PURCHASE")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "LOGO_TEXTURE_GLOW")
				{
					return typeof(string);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num != 2601291421U)
		{
			if (num != 3656563063U)
			{
				if (num == 4214602626U)
				{
					if (name == "SORT_ORDER")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "SELECTOR_PREFAB")
			{
				return typeof(string);
			}
		}
		else if (name == "BACKGROUND")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001D4A RID: 7498 RVA: 0x00096771 File Offset: 0x00094971
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleDbfRecords loadRecords = new LoadModularBundleDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001D4B RID: 7499 RVA: 0x00096788 File Offset: 0x00094988
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleDbfAsset modularBundleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleDbfAsset)) as ModularBundleDbfAsset;
		if (modularBundleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ModularBundleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < modularBundleDbfAsset.Records.Count; i++)
		{
			modularBundleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (modularBundleDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001D4C RID: 7500 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001D4D RID: 7501 RVA: 0x00096807 File Offset: 0x00094A07
	public override void StripUnusedLocales()
	{
		this.m_name.StripUnusedLocales();
	}

	// Token: 0x04001133 RID: 4403
	[SerializeField]
	private DbfLocValue m_name;

	// Token: 0x04001134 RID: 4404
	[SerializeField]
	private string m_selectorPrefab;

	// Token: 0x04001135 RID: 4405
	[SerializeField]
	private int m_selectorPackAmountBanner;

	// Token: 0x04001136 RID: 4406
	[SerializeField]
	private string m_layoutButtonSize = "small";

	// Token: 0x04001137 RID: 4407
	[SerializeField]
	private string m_background;

	// Token: 0x04001138 RID: 4408
	[SerializeField]
	private string m_playlist;

	// Token: 0x04001139 RID: 4409
	[SerializeField]
	private string m_logoTexture;

	// Token: 0x0400113A RID: 4410
	[SerializeField]
	private string m_logoTextureGlow;

	// Token: 0x0400113B RID: 4411
	[SerializeField]
	private int m_sortOrder;

	// Token: 0x0400113C RID: 4412
	[SerializeField]
	private bool m_showAfterPurchase;
}
