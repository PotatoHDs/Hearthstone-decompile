using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200021D RID: 541
[Serializable]
public class ModularBundleLayoutDbfRecord : DbfRecord
{
	// Token: 0x1700037D RID: 893
	// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000968C2 File Offset: 0x00094AC2
	[DbfField("MODULAR_BUNDLE_ID")]
	public int ModularBundleId
	{
		get
		{
			return this.m_modularBundleId;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06001D54 RID: 7508 RVA: 0x000968CA File Offset: 0x00094ACA
	public ModularBundleDbfRecord ModularBundleRecord
	{
		get
		{
			return GameDbf.ModularBundle.GetRecord(this.m_modularBundleId);
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000968DC File Offset: 0x00094ADC
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06001D56 RID: 7510 RVA: 0x000968E4 File Offset: 0x00094AE4
	[DbfField("HIDDEN_LICENSE_ID")]
	public int HiddenLicenseId
	{
		get
		{
			return this.m_hiddenLicenseId;
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000968EC File Offset: 0x00094AEC
	public HiddenLicenseDbfRecord HiddenLicenseRecord
	{
		get
		{
			return GameDbf.HiddenLicense.GetRecord(this.m_hiddenLicenseId);
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x06001D58 RID: 7512 RVA: 0x000968FE File Offset: 0x00094AFE
	[DbfField("ACCENT_TEXTURE")]
	public string AccentTexture
	{
		get
		{
			return this.m_accentTexture;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00096906 File Offset: 0x00094B06
	[DbfField("REGIONS")]
	public string Regions
	{
		get
		{
			return this.m_regions;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0009690E File Offset: 0x00094B0E
	[DbfField("AB_VALUE")]
	public string AbValue
	{
		get
		{
			return this.m_abValue;
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00096916 File Offset: 0x00094B16
	[DbfField("PREFAB")]
	public string Prefab
	{
		get
		{
			return this.m_prefab;
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0009691E File Offset: 0x00094B1E
	[DbfField("DESCRIPTION_HEADLINE")]
	public DbfLocValue DescriptionHeadline
	{
		get
		{
			return this.m_descriptionHeadline;
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00096926 File Offset: 0x00094B26
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x06001D5E RID: 7518 RVA: 0x0009692E File Offset: 0x00094B2E
	[DbfField("ORDER_SUMMARY_NAME")]
	public DbfLocValue OrderSummaryName
	{
		get
		{
			return this.m_orderSummaryName;
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06001D5F RID: 7519 RVA: 0x00096936 File Offset: 0x00094B36
	[DbfField("ANIMATE_AFTER_PURCHASE")]
	public bool AnimateAfterPurchase
	{
		get
		{
			return this.m_animateAfterPurchase;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0009693E File Offset: 0x00094B3E
	[DbfField("STORE_SHAKE_DELAY")]
	public double StoreShakeDelay
	{
		get
		{
			return this.m_storeShakeDelay;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00096946 File Offset: 0x00094B46
	public List<ModularBundleLayoutNodeDbfRecord> Nodes
	{
		get
		{
			return GameDbf.ModularBundleLayoutNode.GetRecords((ModularBundleLayoutNodeDbfRecord r) => r.NodeLayoutId == base.ID, -1);
		}
	}

	// Token: 0x06001D62 RID: 7522 RVA: 0x0009695F File Offset: 0x00094B5F
	public void SetModularBundleId(int v)
	{
		this.m_modularBundleId = v;
	}

	// Token: 0x06001D63 RID: 7523 RVA: 0x00096968 File Offset: 0x00094B68
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x00096971 File Offset: 0x00094B71
	public void SetHiddenLicenseId(int v)
	{
		this.m_hiddenLicenseId = v;
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x0009697A File Offset: 0x00094B7A
	public void SetAccentTexture(string v)
	{
		this.m_accentTexture = v;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x00096983 File Offset: 0x00094B83
	public void SetRegions(string v)
	{
		this.m_regions = v;
	}

	// Token: 0x06001D67 RID: 7527 RVA: 0x0009698C File Offset: 0x00094B8C
	public void SetAbValue(string v)
	{
		this.m_abValue = v;
	}

	// Token: 0x06001D68 RID: 7528 RVA: 0x00096995 File Offset: 0x00094B95
	public void SetPrefab(string v)
	{
		this.m_prefab = v;
	}

	// Token: 0x06001D69 RID: 7529 RVA: 0x0009699E File Offset: 0x00094B9E
	public void SetDescriptionHeadline(DbfLocValue v)
	{
		this.m_descriptionHeadline = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION_HEADLINE");
	}

	// Token: 0x06001D6A RID: 7530 RVA: 0x000969B8 File Offset: 0x00094BB8
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001D6B RID: 7531 RVA: 0x000969D2 File Offset: 0x00094BD2
	public void SetOrderSummaryName(DbfLocValue v)
	{
		this.m_orderSummaryName = v;
		v.SetDebugInfo(base.ID, "ORDER_SUMMARY_NAME");
	}

	// Token: 0x06001D6C RID: 7532 RVA: 0x000969EC File Offset: 0x00094BEC
	public void SetAnimateAfterPurchase(bool v)
	{
		this.m_animateAfterPurchase = v;
	}

	// Token: 0x06001D6D RID: 7533 RVA: 0x000969F5 File Offset: 0x00094BF5
	public void SetStoreShakeDelay(double v)
	{
		this.m_storeShakeDelay = v;
	}

	// Token: 0x06001D6E RID: 7534 RVA: 0x00096A00 File Offset: 0x00094C00
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1775586830U)
		{
			if (num <= 1041024899U)
			{
				if (num != 556572225U)
				{
					if (num != 877574174U)
					{
						if (num == 1041024899U)
						{
							if (name == "PREFAB")
							{
								return this.m_prefab;
							}
						}
					}
					else if (name == "ORDER_SUMMARY_NAME")
					{
						return this.m_orderSummaryName;
					}
				}
				else if (name == "ANIMATE_AFTER_PURCHASE")
				{
					return this.m_animateAfterPurchase;
				}
			}
			else if (num != 1103584457U)
			{
				if (num != 1458105184U)
				{
					if (num == 1775586830U)
					{
						if (name == "DESCRIPTION_HEADLINE")
						{
							return this.m_descriptionHeadline;
						}
					}
				}
				else if (name == "ID")
				{
					return base.ID;
				}
			}
			else if (name == "DESCRIPTION")
			{
				return this.m_description;
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2090525019U)
			{
				if (num != 2186144642U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return this.m_noteDesc;
						}
					}
				}
				else if (name == "MODULAR_BUNDLE_ID")
				{
					return this.m_modularBundleId;
				}
			}
			else if (name == "HIDDEN_LICENSE_ID")
			{
				return this.m_hiddenLicenseId;
			}
		}
		else if (num <= 3227453679U)
		{
			if (num != 3211248554U)
			{
				if (num == 3227453679U)
				{
					if (name == "STORE_SHAKE_DELAY")
					{
						return this.m_storeShakeDelay;
					}
				}
			}
			else if (name == "REGIONS")
			{
				return this.m_regions;
			}
		}
		else if (num != 3915924868U)
		{
			if (num == 4036182035U)
			{
				if (name == "ACCENT_TEXTURE")
				{
					return this.m_accentTexture;
				}
			}
		}
		else if (name == "AB_VALUE")
		{
			return this.m_abValue;
		}
		return null;
	}

	// Token: 0x06001D6F RID: 7535 RVA: 0x00096C4C File Offset: 0x00094E4C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1775586830U)
		{
			if (num <= 1041024899U)
			{
				if (num != 556572225U)
				{
					if (num != 877574174U)
					{
						if (num != 1041024899U)
						{
							return;
						}
						if (!(name == "PREFAB"))
						{
							return;
						}
						this.m_prefab = (string)val;
						return;
					}
					else
					{
						if (!(name == "ORDER_SUMMARY_NAME"))
						{
							return;
						}
						this.m_orderSummaryName = (DbfLocValue)val;
						return;
					}
				}
				else
				{
					if (!(name == "ANIMATE_AFTER_PURCHASE"))
					{
						return;
					}
					this.m_animateAfterPurchase = (bool)val;
					return;
				}
			}
			else if (num != 1103584457U)
			{
				if (num != 1458105184U)
				{
					if (num != 1775586830U)
					{
						return;
					}
					if (!(name == "DESCRIPTION_HEADLINE"))
					{
						return;
					}
					this.m_descriptionHeadline = (DbfLocValue)val;
					return;
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
		else if (num <= 3022554311U)
		{
			if (num != 2090525019U)
			{
				if (num != 2186144642U)
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
					if (!(name == "MODULAR_BUNDLE_ID"))
					{
						return;
					}
					this.m_modularBundleId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "HIDDEN_LICENSE_ID"))
				{
					return;
				}
				this.m_hiddenLicenseId = (int)val;
				return;
			}
		}
		else if (num <= 3227453679U)
		{
			if (num != 3211248554U)
			{
				if (num != 3227453679U)
				{
					return;
				}
				if (!(name == "STORE_SHAKE_DELAY"))
				{
					return;
				}
				this.m_storeShakeDelay = (double)val;
				return;
			}
			else
			{
				if (!(name == "REGIONS"))
				{
					return;
				}
				this.m_regions = (string)val;
				return;
			}
		}
		else if (num != 3915924868U)
		{
			if (num != 4036182035U)
			{
				return;
			}
			if (!(name == "ACCENT_TEXTURE"))
			{
				return;
			}
			this.m_accentTexture = (string)val;
			return;
		}
		else
		{
			if (!(name == "AB_VALUE"))
			{
				return;
			}
			this.m_abValue = (string)val;
			return;
		}
	}

	// Token: 0x06001D70 RID: 7536 RVA: 0x00096E84 File Offset: 0x00095084
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1775586830U)
		{
			if (num <= 1041024899U)
			{
				if (num != 556572225U)
				{
					if (num != 877574174U)
					{
						if (num == 1041024899U)
						{
							if (name == "PREFAB")
							{
								return typeof(string);
							}
						}
					}
					else if (name == "ORDER_SUMMARY_NAME")
					{
						return typeof(DbfLocValue);
					}
				}
				else if (name == "ANIMATE_AFTER_PURCHASE")
				{
					return typeof(bool);
				}
			}
			else if (num != 1103584457U)
			{
				if (num != 1458105184U)
				{
					if (num == 1775586830U)
					{
						if (name == "DESCRIPTION_HEADLINE")
						{
							return typeof(DbfLocValue);
						}
					}
				}
				else if (name == "ID")
				{
					return typeof(int);
				}
			}
			else if (name == "DESCRIPTION")
			{
				return typeof(DbfLocValue);
			}
		}
		else if (num <= 3022554311U)
		{
			if (num != 2090525019U)
			{
				if (num != 2186144642U)
				{
					if (num == 3022554311U)
					{
						if (name == "NOTE_DESC")
						{
							return typeof(string);
						}
					}
				}
				else if (name == "MODULAR_BUNDLE_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "HIDDEN_LICENSE_ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3227453679U)
		{
			if (num != 3211248554U)
			{
				if (num == 3227453679U)
				{
					if (name == "STORE_SHAKE_DELAY")
					{
						return typeof(double);
					}
				}
			}
			else if (name == "REGIONS")
			{
				return typeof(string);
			}
		}
		else if (num != 3915924868U)
		{
			if (num == 4036182035U)
			{
				if (name == "ACCENT_TEXTURE")
				{
					return typeof(string);
				}
			}
		}
		else if (name == "AB_VALUE")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x06001D71 RID: 7537 RVA: 0x000970F5 File Offset: 0x000952F5
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadModularBundleLayoutDbfRecords loadRecords = new LoadModularBundleLayoutDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001D72 RID: 7538 RVA: 0x0009710C File Offset: 0x0009530C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ModularBundleLayoutDbfAsset modularBundleLayoutDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ModularBundleLayoutDbfAsset)) as ModularBundleLayoutDbfAsset;
		if (modularBundleLayoutDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ModularBundleLayoutDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < modularBundleLayoutDbfAsset.Records.Count; i++)
		{
			modularBundleLayoutDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (modularBundleLayoutDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001D73 RID: 7539 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001D74 RID: 7540 RVA: 0x0009718B File Offset: 0x0009538B
	public override void StripUnusedLocales()
	{
		this.m_descriptionHeadline.StripUnusedLocales();
		this.m_description.StripUnusedLocales();
		this.m_orderSummaryName.StripUnusedLocales();
	}

	// Token: 0x0400113F RID: 4415
	[SerializeField]
	private int m_modularBundleId;

	// Token: 0x04001140 RID: 4416
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001141 RID: 4417
	[SerializeField]
	private int m_hiddenLicenseId;

	// Token: 0x04001142 RID: 4418
	[SerializeField]
	private string m_accentTexture;

	// Token: 0x04001143 RID: 4419
	[SerializeField]
	private string m_regions = "*";

	// Token: 0x04001144 RID: 4420
	[SerializeField]
	private string m_abValue = "A";

	// Token: 0x04001145 RID: 4421
	[SerializeField]
	private string m_prefab;

	// Token: 0x04001146 RID: 4422
	[SerializeField]
	private DbfLocValue m_descriptionHeadline;

	// Token: 0x04001147 RID: 4423
	[SerializeField]
	private DbfLocValue m_description;

	// Token: 0x04001148 RID: 4424
	[SerializeField]
	private DbfLocValue m_orderSummaryName;

	// Token: 0x04001149 RID: 4425
	[SerializeField]
	private bool m_animateAfterPurchase;

	// Token: 0x0400114A RID: 4426
	[SerializeField]
	private double m_storeShakeDelay = 1.0;
}
