using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class CardSetSpellOverrideDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_cardSetId;

	[SerializeField]
	private string m_spellType = "NONE";

	[SerializeField]
	private string m_overridePrefab;

	[DbfField("CARD_SET_ID")]
	public int CardSetId => m_cardSetId;

	[DbfField("SPELL_TYPE")]
	public string SpellType => m_spellType;

	[DbfField("OVERRIDE_PREFAB")]
	public string OverridePrefab => m_overridePrefab;

	public void SetCardSetId(int v)
	{
		m_cardSetId = v;
	}

	public void SetSpellType(string v)
	{
		m_spellType = v;
	}

	public void SetOverridePrefab(string v)
	{
		m_overridePrefab = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"CARD_SET_ID" => m_cardSetId, 
			"SPELL_TYPE" => m_spellType, 
			"OVERRIDE_PREFAB" => m_overridePrefab, 
			_ => null, 
		};
	}

	public override void SetVar(string name, object val)
	{
		switch (name)
		{
		case "CARD_SET_ID":
			m_cardSetId = (int)val;
			break;
		case "SPELL_TYPE":
			m_spellType = (string)val;
			break;
		case "OVERRIDE_PREFAB":
			m_overridePrefab = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"CARD_SET_ID" => typeof(int), 
			"SPELL_TYPE" => typeof(string), 
			"OVERRIDE_PREFAB" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardSetSpellOverrideDbfRecords loadRecords = new LoadCardSetSpellOverrideDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardSetSpellOverrideDbfAsset cardSetSpellOverrideDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardSetSpellOverrideDbfAsset)) as CardSetSpellOverrideDbfAsset;
		if (cardSetSpellOverrideDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"CardSetSpellOverrideDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < cardSetSpellOverrideDbfAsset.Records.Count; i++)
		{
			cardSetSpellOverrideDbfAsset.Records[i].StripUnusedLocales();
		}
		records = cardSetSpellOverrideDbfAsset.Records as List<T>;
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
