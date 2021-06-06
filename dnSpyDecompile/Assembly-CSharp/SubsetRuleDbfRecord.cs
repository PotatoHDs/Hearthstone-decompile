using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000280 RID: 640
[Serializable]
public class SubsetRuleDbfRecord : DbfRecord
{
	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x060020CA RID: 8394 RVA: 0x000A1CBA File Offset: 0x0009FEBA
	[DbfField("SUBSET_ID")]
	public int SubsetId
	{
		get
		{
			return this.m_subsetId;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x060020CB RID: 8395 RVA: 0x000A1CC2 File Offset: 0x0009FEC2
	[DbfField("RULE_TYPE")]
	public SubsetRule.Type RuleType
	{
		get
		{
			return this.m_ruleType;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x060020CC RID: 8396 RVA: 0x000A1CCA File Offset: 0x0009FECA
	[DbfField("RULE_IS_NOT")]
	public bool RuleIsNot
	{
		get
		{
			return this.m_ruleIsNot;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x060020CD RID: 8397 RVA: 0x000A1CD2 File Offset: 0x0009FED2
	[DbfField("TAG")]
	public int Tag
	{
		get
		{
			return this.m_tagId;
		}
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x060020CE RID: 8398 RVA: 0x000A1CDA File Offset: 0x0009FEDA
	[DbfField("MIN_VALUE")]
	public int MinValue
	{
		get
		{
			return this.m_minValue;
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x060020CF RID: 8399 RVA: 0x000A1CE2 File Offset: 0x0009FEE2
	[DbfField("MAX_VALUE")]
	public int MaxValue
	{
		get
		{
			return this.m_maxValue;
		}
	}

	// Token: 0x060020D0 RID: 8400 RVA: 0x000A1CEA File Offset: 0x0009FEEA
	public void SetSubsetId(int v)
	{
		this.m_subsetId = v;
	}

	// Token: 0x060020D1 RID: 8401 RVA: 0x000A1CF3 File Offset: 0x0009FEF3
	public void SetRuleType(SubsetRule.Type v)
	{
		this.m_ruleType = v;
	}

	// Token: 0x060020D2 RID: 8402 RVA: 0x000A1CFC File Offset: 0x0009FEFC
	public void SetRuleIsNot(bool v)
	{
		this.m_ruleIsNot = v;
	}

	// Token: 0x060020D3 RID: 8403 RVA: 0x000A1D05 File Offset: 0x0009FF05
	public void SetTag(int v)
	{
		this.m_tagId = v;
	}

	// Token: 0x060020D4 RID: 8404 RVA: 0x000A1D0E File Offset: 0x0009FF0E
	public void SetMinValue(int v)
	{
		this.m_minValue = v;
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x000A1D17 File Offset: 0x0009FF17
	public void SetMaxValue(int v)
	{
		this.m_maxValue = v;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x000A1D20 File Offset: 0x0009FF20
	public override object GetVar(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 406049971U)
			{
				if (num != 699650505U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return base.ID;
						}
					}
				}
				else if (name == "SUBSET_ID")
				{
					return this.m_subsetId;
				}
			}
			else if (name == "TAG")
			{
				return this.m_tagId;
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

	// Token: 0x060020D7 RID: 8407 RVA: 0x000A1E58 File Offset: 0x000A0058
	public override void SetVar(string name, object val)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num > 1458105184U)
		{
			if (num <= 3917898540U)
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
						this.m_ruleType = SubsetRule.Type.INVALID;
						return;
					}
					if (val is SubsetRule.Type || val is int)
					{
						this.m_ruleType = (SubsetRule.Type)val;
						return;
					}
					if (val is string)
					{
						this.m_ruleType = SubsetRule.ParseTypeValue((string)val);
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
		if (num != 406049971U)
		{
			if (num != 699650505U)
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
				if (!(name == "SUBSET_ID"))
				{
					return;
				}
				this.m_subsetId = (int)val;
				return;
			}
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

	// Token: 0x060020D8 RID: 8408 RVA: 0x000A1FB4 File Offset: 0x000A01B4
	public override Type GetVarType(string name)
	{
		uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
		if (num <= 1458105184U)
		{
			if (num != 406049971U)
			{
				if (num != 699650505U)
				{
					if (num == 1458105184U)
					{
						if (name == "ID")
						{
							return typeof(int);
						}
					}
				}
				else if (name == "SUBSET_ID")
				{
					return typeof(int);
				}
			}
			else if (name == "TAG")
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
						return typeof(SubsetRule.Type);
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

	// Token: 0x060020D9 RID: 8409 RVA: 0x000A20E2 File Offset: 0x000A02E2
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetRuleDbfRecords loadRecords = new LoadSubsetRuleDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x000A20F8 File Offset: 0x000A02F8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetRuleDbfAsset subsetRuleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetRuleDbfAsset)) as SubsetRuleDbfAsset;
		if (subsetRuleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("SubsetRuleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < subsetRuleDbfAsset.Records.Count; i++)
		{
			subsetRuleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (subsetRuleDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001255 RID: 4693
	[SerializeField]
	private int m_subsetId;

	// Token: 0x04001256 RID: 4694
	[SerializeField]
	private SubsetRule.Type m_ruleType = SubsetRule.ParseTypeValue("invalid");

	// Token: 0x04001257 RID: 4695
	[SerializeField]
	private bool m_ruleIsNot;

	// Token: 0x04001258 RID: 4696
	[SerializeField]
	private int m_tagId;

	// Token: 0x04001259 RID: 4697
	[SerializeField]
	private int m_minValue;

	// Token: 0x0400125A RID: 4698
	[SerializeField]
	private int m_maxValue;
}
