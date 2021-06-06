using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardChestDbfRecord : DbfRecord
{
	[SerializeField]
	private DbfLocValue m_name;

	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private bool m_showToReturningPlayer;

	[SerializeField]
	private string m_chestPrefab;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("SHOW_TO_RETURNING_PLAYER")]
	public bool ShowToReturningPlayer => m_showToReturningPlayer;

	[DbfField("CHEST_PREFAB")]
	public string ChestPrefab => m_chestPrefab;

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetShowToReturningPlayer(bool v)
	{
		m_showToReturningPlayer = v;
	}

	public void SetChestPrefab(string v)
	{
		m_chestPrefab = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"NAME" => m_name, 
			"DESCRIPTION" => m_description, 
			"SHOW_TO_RETURNING_PLAYER" => m_showToReturningPlayer, 
			"CHEST_PREFAB" => m_chestPrefab, 
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
		case "NAME":
			m_name = (DbfLocValue)val;
			break;
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "SHOW_TO_RETURNING_PLAYER":
			m_showToReturningPlayer = (bool)val;
			break;
		case "CHEST_PREFAB":
			m_chestPrefab = (string)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"NAME" => typeof(DbfLocValue), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"SHOW_TO_RETURNING_PLAYER" => typeof(bool), 
			"CHEST_PREFAB" => typeof(string), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardChestDbfRecords loadRecords = new LoadRewardChestDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardChestDbfAsset rewardChestDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardChestDbfAsset)) as RewardChestDbfAsset;
		if (rewardChestDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardChestDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardChestDbfAsset.Records.Count; i++)
		{
			rewardChestDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardChestDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
		m_description.StripUnusedLocales();
	}
}
