using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class XpPerGameTypeDbfRecord : DbfRecord
{
	[SerializeField]
	private int m_rewardTrackId;

	[DbfField("REWARD_TRACK_ID")]
	public int RewardTrackId => m_rewardTrackId;

	public void SetRewardTrackId(int v)
	{
		m_rewardTrackId = v;
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "REWARD_TRACK_ID")
			{
				return m_rewardTrackId;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "REWARD_TRACK_ID")
			{
				m_rewardTrackId = (int)val;
			}
		}
		else
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "REWARD_TRACK_ID")
			{
				return typeof(int);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadXpPerGameTypeDbfRecords loadRecords = new LoadXpPerGameTypeDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		XpPerGameTypeDbfAsset xpPerGameTypeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(XpPerGameTypeDbfAsset)) as XpPerGameTypeDbfAsset;
		if (xpPerGameTypeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"XpPerGameTypeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < xpPerGameTypeDbfAsset.Records.Count; i++)
		{
			xpPerGameTypeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = xpPerGameTypeDbfAsset.Records as List<T>;
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
