using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

[Serializable]
public class GameSaveSubkeyDbfRecord : DbfRecord
{
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		return null;
	}

	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			SetID((int)val);
		}
	}

	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		return null;
	}

	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameSaveSubkeyDbfRecords loadRecords = new LoadGameSaveSubkeyDbfRecords(resourcePath);
		yield return loadRecords;
		resultHandler?.Invoke(loadRecords.GetRecords() as List<T>);
	}

	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameSaveSubkeyDbfAsset gameSaveSubkeyDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameSaveSubkeyDbfAsset)) as GameSaveSubkeyDbfAsset;
		if (gameSaveSubkeyDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError($"GameSaveSubkeyDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {resourcePath}");
			return false;
		}
		for (int i = 0; i < gameSaveSubkeyDbfAsset.Records.Count; i++)
		{
			gameSaveSubkeyDbfAsset.Records[i].StripUnusedLocales();
		}
		records = gameSaveSubkeyDbfAsset.Records as List<T>;
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
