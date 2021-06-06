using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class RewardListDbfRecord : DbfRecord
{
	[SerializeField]
	private DbfLocValue m_description;

	[SerializeField]
	private bool m_chooseOne;

	[DbfField("DESCRIPTION")]
	public DbfLocValue Description => m_description;

	[DbfField("CHOOSE_ONE")]
	public bool ChooseOne => m_chooseOne;

	public List<RewardItemDbfRecord> RewardItems => GameDbf.RewardItem.GetRecords((RewardItemDbfRecord r) => r.RewardListId == base.ID);

	public void SetDescription(DbfLocValue v)
	{
		m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	public void SetChooseOne(bool v)
	{
		m_chooseOne = v;
	}

	public override object GetVar(string name)
	{
		return name switch
		{
			"ID" => base.ID, 
			"DESCRIPTION" => m_description, 
			"CHOOSE_ONE" => m_chooseOne, 
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
		case "DESCRIPTION":
			m_description = (DbfLocValue)val;
			break;
		case "CHOOSE_ONE":
			m_chooseOne = (bool)val;
			break;
		}
	}

	public override Type GetVarType(string name)
	{
		return name switch
		{
			"ID" => typeof(int), 
			"DESCRIPTION" => typeof(DbfLocValue), 
			"CHOOSE_ONE" => typeof(bool), 
			_ => null, 
		};
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRewardListDbfRecords loadRecords = new LoadRewardListDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RewardListDbfAsset rewardListDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RewardListDbfAsset)) as RewardListDbfAsset;
		if (rewardListDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"RewardListDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < rewardListDbfAsset.Records.Count; i++)
		{
			rewardListDbfAsset.Records[i].StripUnusedLocales();
		}
		records = rewardListDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_description.StripUnusedLocales();
	}
}
