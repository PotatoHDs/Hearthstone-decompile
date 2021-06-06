using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class AchievementSectionDbfRecord : DbfRecord
{
	[SerializeField]
	private DbfLocValue m_name;

	[DbfField("NAME")]
	public DbfLocValue Name => m_name;

	public void SetName(DbfLocValue v)
	{
		m_name = v;
		v.SetDebugInfo(base.ID, "NAME");
	}

	public override object GetVar(string name)
	{
		if (!(name == "ID"))
		{
			if (name == "NAME")
			{
				return m_name;
			}
			return null;
		}
		return base.ID;
	}

	public override void SetVar(string name, object val)
	{
		if (!(name == "ID"))
		{
			if (name == "NAME")
			{
				m_name = (DbfLocValue)val;
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
			if (name == "NAME")
			{
				return typeof(DbfLocValue);
			}
			return null;
		}
		return typeof(int);
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchievementSectionDbfRecords loadRecords = new LoadAchievementSectionDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchievementSectionDbfAsset achievementSectionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchievementSectionDbfAsset)) as AchievementSectionDbfAsset;
		if (achievementSectionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"AchievementSectionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < achievementSectionDbfAsset.Records.Count; i++)
		{
			achievementSectionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = achievementSectionDbfAsset.Records as List<T>;
		return true;
	}

	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	public override void StripUnusedLocales()
	{
		m_name.StripUnusedLocales();
	}
}
