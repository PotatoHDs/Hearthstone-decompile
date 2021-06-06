using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200017D RID: 381
[Serializable]
public class AdventureModeDbfRecord : DbfRecord
{
	// Token: 0x170001FF RID: 511
	// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00083B76 File Offset: 0x00081D76
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x060017F1 RID: 6129 RVA: 0x00083B7E File Offset: 0x00081D7E
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x060017F2 RID: 6130 RVA: 0x00083B87 File Offset: 0x00081D87
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "NOTE_DESC"))
		{
			return null;
		}
		return this.m_noteDesc;
	}

	// Token: 0x060017F3 RID: 6131 RVA: 0x00083BB9 File Offset: 0x00081DB9
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "NOTE_DESC"))
		{
			return;
		}
		this.m_noteDesc = (string)val;
	}

	// Token: 0x060017F4 RID: 6132 RVA: 0x00083BEF File Offset: 0x00081DEF
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "NOTE_DESC"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x060017F5 RID: 6133 RVA: 0x00083C24 File Offset: 0x00081E24
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAdventureModeDbfRecords loadRecords = new LoadAdventureModeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060017F6 RID: 6134 RVA: 0x00083C3C File Offset: 0x00081E3C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AdventureModeDbfAsset adventureModeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AdventureModeDbfAsset)) as AdventureModeDbfAsset;
		if (adventureModeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AdventureModeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < adventureModeDbfAsset.Records.Count; i++)
		{
			adventureModeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (adventureModeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060017F7 RID: 6135 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060017F8 RID: 6136 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F22 RID: 3874
	[SerializeField]
	private string m_noteDesc;
}
