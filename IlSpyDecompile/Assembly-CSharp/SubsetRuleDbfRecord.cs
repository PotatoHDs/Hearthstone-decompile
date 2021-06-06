using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class SubsetRuleDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_subsetId;

	[SerializeField]
	private SubsetRule.Type m_ruleType = SubsetRule.ParseTypeValue("invalid");

	[SerializeField]
	private bool m_ruleIsNot;

	[SerializeField]
	private int m_tagId;

	[SerializeField]
	private int m_minValue;

	[SerializeField]
	private int m_maxValue;

	[DbfField("SUBSET_ID")]
	public int SubsetId => m_subsetId;

	[DbfField("RULE_TYPE")]
	public SubsetRule.Type RuleType => m_ruleType;

	[DbfField("RULE_IS_NOT")]
	public bool RuleIsNot => m_ruleIsNot;

	[DbfField("TAG")]
	public int Tag => m_tagId;

	[DbfField("MIN_VALUE")]
	public int MinValue => m_minValue;

	[DbfField("MAX_VALUE")]
	public int MaxValue => m_maxValue;

	public void SetSubsetId(int v)
	{
		m_subsetId = v;
	}

	public void SetRuleType(SubsetRule.Type v)
	{
		m_ruleType = v;
	}

	public void SetRuleIsNot(bool v)
	{
		m_ruleIsNot = v;
	}

	public void SetTag(int v)
	{
		m_tagId = v;
	}

	public void SetMinValue(int v)
	{
		m_minValue = v;
	}

	public void SetMaxValue(int v)
	{
		m_maxValue = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"SUBSET_ID" => m_subsetId, 
			"RULE_TYPE" => m_ruleType, 
			"RULE_IS_NOT" => m_ruleIsNot, 
			"TAG" => m_tagId, 
			"MIN_VALUE" => m_minValue, 
			"MAX_VALUE" => m_maxValue, 
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
		case "SUBSET_ID":
			m_subsetId = (int)val;
			break;
		case "RULE_TYPE":
			if (val == null)
			{
				m_ruleType = SubsetRule.Type.INVALID;
			}
			else if (val is SubsetRule.Type || val is int)
			{
				m_ruleType = (SubsetRule.Type)val;
			}
			else if (val is string)
			{
				m_ruleType = SubsetRule.ParseTypeValue((string)val);
			}
			break;
		case "RULE_IS_NOT":
			m_ruleIsNot = (bool)val;
			break;
		case "TAG":
			m_tagId = (int)val;
			break;
		case "MIN_VALUE":
			m_minValue = (int)val;
			break;
		case "MAX_VALUE":
			m_maxValue = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"SUBSET_ID" => typeof(int), 
			"RULE_TYPE" => typeof(SubsetRule.Type), 
			"RULE_IS_NOT" => typeof(bool), 
			"TAG" => typeof(int), 
			"MIN_VALUE" => typeof(int), 
			"MAX_VALUE" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadSubsetRuleDbfRecords loadRecords = new LoadSubsetRuleDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		SubsetRuleDbfAsset subsetRuleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(SubsetRuleDbfAsset)) as SubsetRuleDbfAsset;
		if (subsetRuleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"SubsetRuleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < subsetRuleDbfAsset.Records.Count; i++)
		{
			subsetRuleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = subsetRuleDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
	}
}
