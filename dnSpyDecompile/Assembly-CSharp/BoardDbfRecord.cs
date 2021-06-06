using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000186 RID: 390
[Serializable]
public class BoardDbfRecord : DbfRecord
{
	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06001823 RID: 6179 RVA: 0x0008429E File Offset: 0x0008249E
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06001824 RID: 6180 RVA: 0x000842A6 File Offset: 0x000824A6
	[DbfField("PREFAB")]
	public string Prefab
	{
		get
		{
			return this.m_prefab;
		}
	}

	// Token: 0x06001825 RID: 6181 RVA: 0x000842AE File Offset: 0x000824AE
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001826 RID: 6182 RVA: 0x000842B7 File Offset: 0x000824B7
	public void SetPrefab(string v)
	{
		this.m_prefab = v;
	}

	// Token: 0x06001827 RID: 6183 RVA: 0x000842C0 File Offset: 0x000824C0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (!(name == "PREFAB"))
		{
			return null;
		}
		return this.m_prefab;
	}

	// Token: 0x06001828 RID: 6184 RVA: 0x00084314 File Offset: 0x00082514
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "NOTE_DESC")
		{
			this.m_noteDesc = (string)val;
			return;
		}
		if (!(name == "PREFAB"))
		{
			return;
		}
		this.m_prefab = (string)val;
	}

	// Token: 0x06001829 RID: 6185 RVA: 0x00084370 File Offset: 0x00082570
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (!(name == "PREFAB"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x0600182A RID: 6186 RVA: 0x000843C8 File Offset: 0x000825C8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBoardDbfRecords loadRecords = new LoadBoardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600182B RID: 6187 RVA: 0x000843E0 File Offset: 0x000825E0
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BoardDbfAsset boardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BoardDbfAsset)) as BoardDbfAsset;
		if (boardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("BoardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < boardDbfAsset.Records.Count; i++)
		{
			boardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (boardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600182C RID: 6188 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600182D RID: 6189 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F2F RID: 3887
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000F30 RID: 3888
	[SerializeField]
	private string m_prefab;
}
