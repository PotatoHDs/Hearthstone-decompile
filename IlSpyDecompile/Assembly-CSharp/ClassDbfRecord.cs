using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class ClassDbfRecord : DbfRecord
{
	[SerializeField]
	private Class.AssetFlags m_assetFlags;

	[DbfField("ASSET_FLAGS")]
	public Class.AssetFlags AssetFlags => m_assetFlags;

	public void SetAssetFlags(Class.AssetFlags v)
	{
		m_assetFlags = v;
	}

	public override object GetVar(string name)
	{
		if (name == "ASSET_FLAGS")
		{
			return m_assetFlags;
		}
		return null;
	}

	public override void SetVar(string name, object val)
	{
		if (name == "ASSET_FLAGS")
		{
			if (val == null)
			{
				m_assetFlags = Class.AssetFlags.NONE;
			}
			else if (val is Class.AssetFlags || val is int)
			{
				m_assetFlags = (Class.AssetFlags)val;
			}
			else if (val is string)
			{
				m_assetFlags = Class.ParseAssetFlagsValue((string)val);
			}
		}
	}

	public override Type GetVarType(string name)
	{
		if (name == "ASSET_FLAGS")
		{
			return typeof(Class.AssetFlags);
		}
		return null;
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadClassDbfRecords loadRecords = new LoadClassDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ClassDbfAsset classDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ClassDbfAsset)) as ClassDbfAsset;
		if (classDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"ClassDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < classDbfAsset.Records.Count; i++)
		{
			classDbfAsset.Records[i].StripUnusedLocales();
		}
		records = classDbfAsset.Records as List<T>;
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
