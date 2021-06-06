using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class MigrationCardReplacementDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_stepId;

	[SerializeField]
	private int m_oldCardId;

	[SerializeField]
	private int m_newCardId;

	[SerializeField]
	private int m_requiredHeroClassId;

	[SerializeField]
	private int m_requiredHeroLevel;

	[SerializeField]
	private int m_sortOrder;

	[DbfField("STEP_ID")]
	public int StepId => m_stepId;

	[DbfField("OLD_CARD_ID")]
	public int OldCardId => m_oldCardId;

	public CardDbfRecord OldCardRecord => GameDbf.Card.GetRecord(m_oldCardId);

	[DbfField("NEW_CARD_ID")]
	public int NewCardId => m_newCardId;

	public CardDbfRecord NewCardRecord => GameDbf.Card.GetRecord(m_newCardId);

	[DbfField("REQUIRED_HERO_CLASS_ID")]
	public int RequiredHeroClassId => m_requiredHeroClassId;

	public ClassDbfRecord RequiredHeroClassRecord => GameDbf.Class.GetRecord(m_requiredHeroClassId);

	[DbfField("REQUIRED_HERO_LEVEL")]
	public int RequiredHeroLevel => m_requiredHeroLevel;

	[DbfField("SORT_ORDER")]
	public int SortOrder => m_sortOrder;

	public void SetStepId(int v)
	{
		m_stepId = v;
	}

	public void SetOldCardId(int v)
	{
		m_oldCardId = v;
	}

	public void SetNewCardId(int v)
	{
		m_newCardId = v;
	}

	public void SetRequiredHeroClassId(int v)
	{
		m_requiredHeroClassId = v;
	}

	public void SetRequiredHeroLevel(int v)
	{
		m_requiredHeroLevel = v;
	}

	public void SetSortOrder(int v)
	{
		m_sortOrder = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"STEP_ID" => m_stepId, 
			"OLD_CARD_ID" => m_oldCardId, 
			"NEW_CARD_ID" => m_newCardId, 
			"REQUIRED_HERO_CLASS_ID" => m_requiredHeroClassId, 
			"REQUIRED_HERO_LEVEL" => m_requiredHeroLevel, 
			"SORT_ORDER" => m_sortOrder, 
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
		case "STEP_ID":
			m_stepId = (int)val;
			break;
		case "OLD_CARD_ID":
			m_oldCardId = (int)val;
			break;
		case "NEW_CARD_ID":
			m_newCardId = (int)val;
			break;
		case "REQUIRED_HERO_CLASS_ID":
			m_requiredHeroClassId = (int)val;
			break;
		case "REQUIRED_HERO_LEVEL":
			m_requiredHeroLevel = (int)val;
			break;
		case "SORT_ORDER":
			m_sortOrder = (int)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"STEP_ID" => typeof(int), 
			"OLD_CARD_ID" => typeof(int), 
			"NEW_CARD_ID" => typeof(int), 
			"REQUIRED_HERO_CLASS_ID" => typeof(int), 
			"REQUIRED_HERO_LEVEL" => typeof(int), 
			"SORT_ORDER" => typeof(int), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMigrationCardReplacementDbfRecords loadRecords = new LoadMigrationCardReplacementDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MigrationCardReplacementDbfAsset migrationCardReplacementDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MigrationCardReplacementDbfAsset)) as MigrationCardReplacementDbfAsset;
		if (migrationCardReplacementDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"MigrationCardReplacementDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < migrationCardReplacementDbfAsset.Records.Count; i++)
		{
			migrationCardReplacementDbfAsset.Records[i].StripUnusedLocales();
		}
		records = migrationCardReplacementDbfAsset.Records as List<T>;
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
