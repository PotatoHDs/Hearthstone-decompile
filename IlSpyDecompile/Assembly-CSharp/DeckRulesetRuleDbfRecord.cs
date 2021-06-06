using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class DeckRulesetRuleDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_deckRulesetId;

	[SerializeField]
	private int m_appliesToSubsetId;

	[SerializeField]
	private bool m_appliesToIsNot;

	[SerializeField]
	private DeckRulesetRule.RuleType m_ruleType = DeckRulesetRule.ParseRuleTypeValue("invalid_rule_type");

	[SerializeField]
	private bool m_ruleIsNot;

	[SerializeField]
	private int m_minValue;

	[SerializeField]
	private int m_maxValue;

	[SerializeField]
	private int m_tagId;

	[SerializeField]
	private int m_tagMinValue;

	[SerializeField]
	private int m_tagMaxValue;

	[SerializeField]
	private string m_stringValue;

	[SerializeField]
	private DbfLocValue m_errorString;

	[SerializeField]
	private bool m_showInvalidCards;

	[DbfField("DECK_RULESET_ID")]
	public int DeckRulesetId => m_deckRulesetId;

	[DbfField("APPLIES_TO_SUBSET_ID")]
	public int AppliesToSubsetId => m_appliesToSubsetId;

	public SubsetDbfRecord AppliesToSubsetRecord => GameDbf.Subset.GetRecord(m_appliesToSubsetId);

	[DbfField("APPLIES_TO_IS_NOT")]
	public bool AppliesToIsNot => m_appliesToIsNot;

	[DbfField("RULE_TYPE")]
	public DeckRulesetRule.RuleType RuleType => m_ruleType;

	[DbfField("RULE_IS_NOT")]
	public bool RuleIsNot => m_ruleIsNot;

	[DbfField("MIN_VALUE")]
	public int MinValue => m_minValue;

	[DbfField("MAX_VALUE")]
	public int MaxValue => m_maxValue;

	[DbfField("TAG")]
	public int Tag => m_tagId;

	[DbfField("TAG_MIN_VALUE")]
	public int TagMinValue => m_tagMinValue;

	[DbfField("TAG_MAX_VALUE")]
	public int TagMaxValue => m_tagMaxValue;

	[DbfField("STRING_VALUE")]
	public string StringValue => m_stringValue;

	[DbfField("ERROR_STRING")]
	public DbfLocValue ErrorString => m_errorString;

	[DbfField("SHOW_INVALID_CARDS")]
	public bool ShowInvalidCards => m_showInvalidCards;

	public List<DeckRulesetRuleSubsetDbfRecord> Subsets => GameDbf.DeckRulesetRuleSubset.GetRecords((DeckRulesetRuleSubsetDbfRecord r) => r.DeckRulesetRuleId == base.ID);

	public void SetDeckRulesetId(int v)
	{
		m_deckRulesetId = v;
	}

	public void SetAppliesToSubsetId(int v)
	{
		m_appliesToSubsetId = v;
	}

	public void SetAppliesToIsNot(bool v)
	{
		m_appliesToIsNot = v;
	}

	public void SetRuleType(DeckRulesetRule.RuleType v)
	{
		m_ruleType = v;
	}

	public void SetRuleIsNot(bool v)
	{
		m_ruleIsNot = v;
	}

	public void SetMinValue(int v)
	{
		m_minValue = v;
	}

	public void SetMaxValue(int v)
	{
		m_maxValue = v;
	}

	public void SetTag(int v)
	{
		m_tagId = v;
	}

	public void SetTagMinValue(int v)
	{
		m_tagMinValue = v;
	}

	public void SetTagMaxValue(int v)
	{
		m_tagMaxValue = v;
	}

	public void SetStringValue(string v)
	{
		m_stringValue = v;
	}

	public void SetErrorString(DbfLocValue v)
	{
		m_errorString = v;
		v.SetDebugInfo(base.ID, "ERROR_STRING");
	}

	public void SetShowInvalidCards(bool v)
	{
		m_showInvalidCards = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"DECK_RULESET_ID" => m_deckRulesetId, 
			"APPLIES_TO_SUBSET_ID" => m_appliesToSubsetId, 
			"APPLIES_TO_IS_NOT" => m_appliesToIsNot, 
			"RULE_TYPE" => m_ruleType, 
			"RULE_IS_NOT" => m_ruleIsNot, 
			"MIN_VALUE" => m_minValue, 
			"MAX_VALUE" => m_maxValue, 
			"TAG" => m_tagId, 
			"TAG_MIN_VALUE" => m_tagMinValue, 
			"TAG_MAX_VALUE" => m_tagMaxValue, 
			"STRING_VALUE" => m_stringValue, 
			"ERROR_STRING" => m_errorString, 
			"SHOW_INVALID_CARDS" => m_showInvalidCards, 
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
		case "DECK_RULESET_ID":
			m_deckRulesetId = (int)val;
			break;
		case "APPLIES_TO_SUBSET_ID":
			m_appliesToSubsetId = (int)val;
			break;
		case "APPLIES_TO_IS_NOT":
			m_appliesToIsNot = (bool)val;
			break;
		case "RULE_TYPE":
			if (val == null)
			{
				m_ruleType = DeckRulesetRule.RuleType.INVALID_RULE_TYPE;
			}
			else if (val is DeckRulesetRule.RuleType || val is int)
			{
				m_ruleType = (DeckRulesetRule.RuleType)val;
			}
			else if (val is string)
			{
				m_ruleType = DeckRulesetRule.ParseRuleTypeValue((string)val);
			}
			break;
		case "RULE_IS_NOT":
			m_ruleIsNot = (bool)val;
			break;
		case "MIN_VALUE":
			m_minValue = (int)val;
			break;
		case "MAX_VALUE":
			m_maxValue = (int)val;
			break;
		case "TAG":
			m_tagId = (int)val;
			break;
		case "TAG_MIN_VALUE":
			m_tagMinValue = (int)val;
			break;
		case "TAG_MAX_VALUE":
			m_tagMaxValue = (int)val;
			break;
		case "STRING_VALUE":
			m_stringValue = (string)val;
			break;
		case "ERROR_STRING":
			m_errorString = (DbfLocValue)val;
			break;
		case "SHOW_INVALID_CARDS":
			m_showInvalidCards = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"DECK_RULESET_ID" => typeof(int), 
			"APPLIES_TO_SUBSET_ID" => typeof(int), 
			"APPLIES_TO_IS_NOT" => typeof(bool), 
			"RULE_TYPE" => typeof(DeckRulesetRule.RuleType), 
			"RULE_IS_NOT" => typeof(bool), 
			"MIN_VALUE" => typeof(int), 
			"MAX_VALUE" => typeof(int), 
			"TAG" => typeof(int), 
			"TAG_MIN_VALUE" => typeof(int), 
			"TAG_MAX_VALUE" => typeof(int), 
			"STRING_VALUE" => typeof(string), 
			"ERROR_STRING" => typeof(DbfLocValue), 
			"SHOW_INVALID_CARDS" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckRulesetRuleDbfRecords loadRecords = new LoadDeckRulesetRuleDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckRulesetRuleDbfAsset deckRulesetRuleDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckRulesetRuleDbfAsset)) as DeckRulesetRuleDbfAsset;
		if (deckRulesetRuleDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"DeckRulesetRuleDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < deckRulesetRuleDbfAsset.Records.Count; i++)
		{
			deckRulesetRuleDbfAsset.Records[i].StripUnusedLocales();
		}
		records = deckRulesetRuleDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_errorString.StripUnusedLocales();
	}
}
