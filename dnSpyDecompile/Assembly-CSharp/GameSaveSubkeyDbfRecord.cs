using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001ED RID: 493
[Serializable]
public class GameSaveSubkeyDbfRecord : DbfRecord
{
	// Token: 0x06001BB7 RID: 7095 RVA: 0x00090F7E File Offset: 0x0008F17E
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		return null;
	}

	// Token: 0x06001BB8 RID: 7096 RVA: 0x00090F9A File Offset: 0x0008F19A
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
		}
	}

	// Token: 0x06001BB9 RID: 7097 RVA: 0x00090FB5 File Offset: 0x0008F1B5
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		return null;
	}

	// Token: 0x06001BBA RID: 7098 RVA: 0x00090FD0 File Offset: 0x0008F1D0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadGameSaveSubkeyDbfRecords loadRecords = new LoadGameSaveSubkeyDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001BBB RID: 7099 RVA: 0x00090FE8 File Offset: 0x0008F1E8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		GameSaveSubkeyDbfAsset gameSaveSubkeyDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(GameSaveSubkeyDbfAsset)) as GameSaveSubkeyDbfAsset;
		if (gameSaveSubkeyDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("GameSaveSubkeyDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < gameSaveSubkeyDbfAsset.Records.Count; i++)
		{
			gameSaveSubkeyDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (gameSaveSubkeyDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}
}
