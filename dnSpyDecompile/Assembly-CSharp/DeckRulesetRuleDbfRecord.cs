using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D1 RID: 465
[Serializable]
public class DeckRulesetRuleDbfRecord : DbfRecord
{
	// Token: 0x170002AD RID: 685
	// (get) Token: 0x06001A8F RID: 6799 RVA: 0x0008BD82 File Offset: 0x00089F82
	[DbfField("DECK_RULESET_ID")]
	public int DeckRulesetId
	{
		get
		{
			return this.m_deckRulesetId;
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0008BD8A File Offset: 0x00089F8A
	[DbfField("APPLIES_TO_SUBSET_ID")]
	public int AppliesToSubsetId
	{
		get
		{
			return this.m_appliesToSubsetId;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x06001A91 RID: 6801 RVA: 0x0008BD92 File Offset: 0x00089F92
	public SubsetDbfRecord AppliesToSubsetRecord
	{
		get
		{
			return GameDbf.Subset.GetRecord(this.m_appliesToSubsetId);
		}
	}

	// Token: 0x170002B0 RID: 688
	// (get) Token: 0x06001A92 RID: 6802 RVA: 0x0008BDA4 File Offset: 0x00089FA4
	[DbfField("APPLIES_TO_IS_NOT")]
	public bool AppliesToIsNot
	{
		get
		{
			return this.m_appliesToIsNot;
		}
	}

	// Token: 0x170002B1 RID: 689
	// (get) Token: 0x06001A93 RID: 6803 RVA: 0x0008BDAC File Offset: 0x00089FAC
	[DbfField("RULE_TYPE")]
	public DeckRulesetRule.RuleType RuleType
	{
		get
		{
			return this.m_ruleType;
		}
	}

	// Token: 0x170002B2 RID: 690
	// (get) Token: 0x06001A94 RID: 6804 RVA: 0x0008BDB4 File Offset: 0x00089FB4
	[DbfField("RULE_IS_NOT")]
	public bool RuleIsNot
	{
		get
		{
			return this.m_ruleIsNot;
		}
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x06001A95 RID: 6805 RVA: 0x0008BDBC File Offset: 0x00089FBC
	[DbfField("MIN_VALUE")]
	public int MinValue
	{
		get
		{
			return this.m_minValue;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0008BDC4 File Offset: 0x00089FC4
	[DbfField("MAX_VALUE")]
	public int MaxValue
	{
		get
		{
			return this.m_maxValue;
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x06001A97 RID: 6807 RVA: 0x0008BDCC File Offset: 0x00089FCC
	[DbfField("TAG")]
	public int Tag
	{
		get
		{
			return this.m_tagId;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06001A98 RID: 6808 RVA: 0x0008BDD4 File Offset: 0x00089FD4
	[DbfField("TAG_MIN_VALUE")]
	public int TagMinValue
	{
		get
		{
			return this.m_tagMinValue;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0008BDDC File Offset: 0x00089FDC
	[DbfField("TAG_MAX_VALUE")]
	public int TagMaxValue
	{
		get
		{
			return this.m_tagMaxValue;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06001A9A RID: 6810 RVA: 0x0008BDE4 File Offset: 0x00089FE4
	[DbfField("STRING_VALUE")]
	public string StringValue
	{
		get
		{
			return this.m_stringValue;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06001A9B RID: 6811 RVA: 0x0008BDEC File Offset: 0x00089FEC
	[DbfField("ERROR_STRING")]
	public DbfLocValue ErrorString
	{
		get
		{
			return this.m_errorString;
		}
	}

	// Token: 0x170002BA RID: 698
	// (get) Token: 0x06001A9C RID: 6812 RVA: 0x0008BDF4 File Offset: 0x00089FF4
	[DbfField("SHOW_INVALID_CARDS")]
	public bool ShowInvalidCards
	{
		get
		{
			return this.m_showInvalidCards;
		}
	}

	// Token: 0x170002BB RID: 699
	// (get) Token: 0x06001A9D RID: 6813 RVA: 0x0008BDFC File Offset: 0x00089FFC
	public List<DeckRulesetRuleSubsetDbfRecord> Subsets
	{
		get
		{
			return GameDbf.DeckRulesetRuleSubset.GetRecords((DeckRulesetRuleSubsetDbfRecord r) => r.DeckRulesetRuleId == base.ID, -1);
		}
	}

	// Token: 0x06001A9E RID: 6814 RVA: 0x0008BE15 File Offset: 0x0008A015
	public void SetDeckRulesetId(int v)
	{
		this.m_deckRulesetId = v;
	}

	// Token: 0x06001A9F RID: 6815 RVA: 0x0008BE1E File Offset: 0x0008A01E
	public void SetAppliesToSubsetId(int v)
	{
		this.m_appliesToSubsetId = v;
	}

	// Token: 0x06001AA0 RID: 6816 RVA: 0x0008BE27 File Offset: 0x0008A027
	public void SetAppliesToIsNot(bool v)
	{
		this.m_appliesToIsNot = v;
	}

	// Token: 0x06001AA1 RID: 6817 RVA: 0x0008BE30 File Offset: 0x0008A030
	public void SetRuleType(DeckRulesetRule.RuleType v)
	{
		this.m_ruleType = v;
	}

	// Token: 0x06001AA2 RID: 6818 RVA: 0x0008BE39 File Offset: 0x0008A039
	public void SetRuleIsNot(bool v)
	{
		this.m_ruleIsNot = v;
	}

	// Token: 0x06001AA3 RID: 6819 RVA: 0x0008BE42 File Offset: 0x0008A042
	public void SetMinValue(int v)
	{
		this.m_minValue = v;
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x0008BE4B File Offset: 0x0008A04B
	public void SetMaxValue(int v)
	{
		this.m_maxValue = v;
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x0008BE54 File Offset: 0x0008A054
	public void SetTag(int v)
	{
		this.m_tagId = v;
	}

	// Token: 0x06001AA6 RID: 6822 RVA: 0x0008BE5D File Offset: 0x0008A05D
	public void SetTagMinValue(int v)
	{
		this.m_tagMinValue = v;
	}

	// Token: 0x06001AA7 RID: 6823 RVA: 0x0008BE66 File Offset: 0x0008A066
	public void SetTagMaxValue(int v)
	{
		this.m_tagMaxValue = v;
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x0008BE6F File Offset: 0x0008A06F
	public void SetStringValue(string v)
	{
		this.m_stringValue = v;
	}

	// Token: 0x06001AA9 RID: 6825 RVA: 0x0008BE78 File Offset: 0x0008A078
	public void SetErrorString(DbfLocValue v)
	{
		this.m_errorString = v;
		v.SetDebugInfo(base.ID, "ERROR_STRING");
	}

	// Token: 0x06001AAA RID: 6826 RVA: 0x0008BE92 File Offset: 0x0008A092
	public void SetShowInvalidCards(bool v)
	{
		this.m_showInvalidCards = v;
	}

	// Token: 0x06001AAB RID: 6827 RVA: 0x0008BE9C File Offset: 0x0008A09C
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1144720106U)
		{
			if (num <= 451320876U)
			{
				if (num != 349684874U)
				{
					if (num != 406049971U)
					{
						if (num == 451320876U)
						{
							if (name == "TAG_MAX_VALUE")
							{
								return this.m_tagMaxValue;
							}
						}
					}
					else if (name == "TAG")
					{
						return this.m_tagId;
					}
				}
				else if (name == "STRING_VALUE")
				{
					return this.m_stringValue;
				}
			}
			else if (num <= 730241252U)
			{
				if (num != 463699011U)
				{
					if (num == 730241252U)
					{
						if (name == "APPLIES_TO_SUBSET_ID")
						{
							return this.m_appliesToSubsetId;
						}
					}
				}
				else if (name == "ERROR_STRING")
				{
					return this.m_errorString;
				}
			}
			else if (num != 779072232U)
			{
				if (num == 1144720106U)
				{
					if (name == "TAG_MIN_VALUE")
					{
						return this.m_tagMinValue;
					}
				}
			}
			else if (name == "APPLIES_TO_IS_NOT")
			{
				return this.m_appliesToIsNot;
			}
		}
		else if (num <= 3419090408U)
		{
			if (num != 1458105184U)
			{
				if (num != 3306344503U)
				{
					if (num == 3419090408U)
					{
						if (name == "SHOW_INVALID_CARDS")
						{
							return this.m_showInvalidCards;
						}
					}
				}
				else if (name == "DECK_RULESET_ID")
				{
					return this.m_deckRulesetId;
				}
			}
			else if (name == "ID")
			{
				return base.ID;
			}
		}
		else if (num <= 3917898540U)
		{
			if (num != 3868906878U)
			{
				if (num == 3917898540U)
				{
					if (name == "RULE_TYPE")
					{
						return this.m_ruleType;
					}
				}
			}
			else if (name == "RULE_IS_NOT")
			{
				return this.m_ruleIsNot;
			}
		}
		else if (num != 3988915625U)
		{
			if (num == 4234312411U)
			{
				if (name == "MAX_VALUE")
				{
					return this.m_maxValue;
				}
			}
		}
		else if (name == "MIN_VALUE")
		{
			return this.m_minValue;
		}
		return null;
	}

	// Token: 0x06001AAC RID: 6828 RVA: 0x0008C14C File Offset: 0x0008A34C
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 1144720106U)
		{
			if (num <= 3419090408U)
			{
				if (num != 1458105184U)
				{
					if (num != 3306344503U)
					{
						if (num != 3419090408U)
						{
							return;
						}
						if (!(name == "SHOW_INVALID_CARDS"))
						{
							return;
						}
						this.m_showInvalidCards = (bool)val;
					}
					else
					{
						if (!(name == "DECK_RULESET_ID"))
						{
							return;
						}
						this.m_deckRulesetId = (int)val;
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
			else if (num <= 3917898540U)
			{
				if (num != 3868906878U)
				{
					if (num != 3917898540U)
					{
						return;
					}
					if (!(name == "RULE_TYPE"))
					{
						return;
					}
					if (val == null)
					{
						this.m_ruleType = DeckRulesetRule.RuleType.INVALID_RULE_TYPE;
						return;
					}
					if (val is DeckRulesetRule.RuleType || val is int)
					{
						this.m_ruleType = (DeckRulesetRule.RuleType)val;
						return;
					}
					if (val is string)
					{
						this.m_ruleType = DeckRulesetRule.ParseRuleTypeValue((string)val);
						return;
					}
				}
				else
				{
					if (!(name == "RULE_IS_NOT"))
					{
						return;
					}
					this.m_ruleIsNot = (bool)val;
					return;
				}
			}
			else if (num != 3988915625U)
			{
				if (num != 4234312411U)
				{
					return;
				}
				if (!(name == "MAX_VALUE"))
				{
					return;
				}
				this.m_maxValue = (int)val;
				return;
			}
			else
			{
				if (!(name == "MIN_VALUE"))
				{
					return;
				}
				this.m_minValue = (int)val;
				return;
			}
			return;
		}
		if (num <= 451320876U)
		{
			if (num != 349684874U)
			{
				if (num != 406049971U)
				{
					if (num != 451320876U)
					{
						return;
					}
					if (!(name == "TAG_MAX_VALUE"))
					{
						return;
					}
					this.m_tagMaxValue = (int)val;
					return;
				}
				else
				{
					if (!(name == "TAG"))
					{
						return;
					}
					this.m_tagId = (int)val;
					return;
				}
			}
			else
			{
				if (!(name == "STRING_VALUE"))
				{
					return;
				}
				this.m_stringValue = (string)val;
				return;
			}
		}
		else if (num <= 730241252U)
		{
			if (num != 463699011U)
			{
				if (num != 730241252U)
				{
					return;
				}
				if (!(name == "APPLIES_TO_SUBSET_ID"))
				{
					return;
				}
				this.m_appliesToSubsetId = (int)val;
				return;
			}
			else
			{
				if (!(name == "ERROR_STRING"))
				{
					return;
				}
				this.m_errorString = (DbfLocValue)val;
				return;
			}
		}
		else if (num != 779072232U)
		{
			if (num != 1144720106U)
			{
				return;
			}
			if (!(name == "TAG_MIN_VALUE"))
			{
				return;
			}
			this.m_tagMinValue = (int)val;
			return;
		}
		else
		{
			if (!(name == "APPLIES_TO_IS_NOT"))
			{
				return;
			}
			this.m_appliesToIsNot = (bool)val;
			return;
		}
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x0008C3F4 File Offset: 0x0008A5F4
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1144720106U)
		{
			if (num <= 451320876U)
			{
				if (num != 349684874U)
				{
					if (num != 406049971U)
					{
						if (num == 451320876U)
						{
							if (name == "TAG_MAX_VALUE")
							{
								return typeof(int);
							}
						}
					}
					else if (name == "TAG")
					{
						return typeof(int);
					}
				}
				else if (name == "STRING_VALUE")
				{
					return typeof(string);
				}
			}
			else if (num <= 730241252U)
			{
				if (num != 463699011U)
				{
					if (num == 730241252U)
					{
						if (name == "APPLIES_TO_SUBSET_ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "ERROR_STRING")
				{
					return typeof(DbfLocValue);
				}
			}
			else if (num != 779072232U)
			{
				if (num == 1144720106U)
				{
					if (name == "TAG_MIN_VALUE")
					{
						return typeof(int);
					}
				}
			}
			else if (name == "APPLIES_TO_IS_NOT")
			{
				return typeof(bool);
			}
		}
		else if (num <= 3419090408U)
		{
			if (num != 1458105184U)
			{
				if (num != 3306344503U)
				{
					if (num == 3419090408U)
					{
						if (name == "SHOW_INVALID_CARDS")
						{
							return typeof(bool);
						}
					}
				}
				else if (name == "DECK_RULESET_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "ID")
			{
				return typeof(int);
			}
		}
		else if (num <= 3917898540U)
		{
			if (num != 3868906878U)
			{
				if (num == 3917898540U)
				{
					if (name == "RULE_TYPE")
					{
						return typeof(DeckRulesetRule.RuleType);
					}
				}
			}
			else if (name == "RULE_IS_NOT")
			{
				return typeof(bool);
			}
		}
		else if (num != 3988915625U)
		{
			if (num == 4234312411U)
			{
				if (name == "MAX_VALUE")
				{
					return typeof(int);
				}
			}
		}
		else if (name == "MIN_VALUE")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001AAE RID: 6830 RVA: 0x0008C6A0 File Offset: 0x0008A8A0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckRulesetRuleDbfRecords loadRecords = new LoadDeckRulesetRuleDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001AAF RID: 6831 RVA: 0x0008C6B8 File Offset: 0x0008A8B8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetRuleDbfAsset deckRulesetRuleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetRuleDbfAsset)) as DeckRulesetRuleDbfAsset;
		if (deckRulesetRuleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckRulesetRuleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckRulesetRuleDbfAsset.Records.Count; i++)
		{
			deckRulesetRuleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckRulesetRuleDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001AB0 RID: 6832 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001AB1 RID: 6833 RVA: 0x0008C737 File Offset: 0x0008A937
	public override void StripUnusedLocales()
	{
		this.m_errorString.StripUnusedLocales();
	}

	// Token: 0x04000FF2 RID: 4082
	[SerializeField]
	private int m_deckRulesetId;

	// Token: 0x04000FF3 RID: 4083
	[SerializeField]
	private int m_appliesToSubsetId;

	// Token: 0x04000FF4 RID: 4084
	[SerializeField]
	private bool m_appliesToIsNot;

	// Token: 0x04000FF5 RID: 4085
	[SerializeField]
	private DeckRulesetRule.RuleType m_ruleType = DeckRulesetRule.ParseRuleTypeValue("invalid_rule_type");

	// Token: 0x04000FF6 RID: 4086
	[SerializeField]
	private bool m_ruleIsNot;

	// Token: 0x04000FF7 RID: 4087
	[SerializeField]
	private int m_minValue;

	// Token: 0x04000FF8 RID: 4088
	[SerializeField]
	private int m_maxValue;

	// Token: 0x04000FF9 RID: 4089
	[SerializeField]
	private int m_tagId;

	// Token: 0x04000FFA RID: 4090
	[SerializeField]
	private int m_tagMinValue;

	// Token: 0x04000FFB RID: 4091
	[SerializeField]
	private int m_tagMaxValue;

	// Token: 0x04000FFC RID: 4092
	[SerializeField]
	private string m_stringValue;

	// Token: 0x04000FFD RID: 4093
	[SerializeField]
	private DbfLocValue m_errorString;

	// Token: 0x04000FFE RID: 4094
	[SerializeField]
	private bool m_showInvalidCards;
}
