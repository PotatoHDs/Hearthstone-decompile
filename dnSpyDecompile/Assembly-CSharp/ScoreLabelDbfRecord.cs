using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200026E RID: 622
[Serializable]
public class ScoreLabelDbfRecord : DbfRecord
{
	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x06002057 RID: 8279 RVA: 0x000A0BA2 File Offset: 0x0009EDA2
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06002058 RID: 8280 RVA: 0x000A0BAA File Offset: 0x0009EDAA
	[DbfField("TEXT")]
	public DbfLocValue Text
	{
		get
		{
			return this.m_text;
		}
	}

	// Token: 0x06002059 RID: 8281 RVA: 0x000A0BB2 File Offset: 0x0009EDB2
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x0600205A RID: 8282 RVA: 0x000A0BBB File Offset: 0x0009EDBB
	public void SetText(DbfLocValue v)
	{
		this.m_text = v;
		v.SetDebugInfo(base.ID, "TEXT");
	}

	// Token: 0x0600205B RID: 8283 RVA: 0x000A0BD8 File Offset: 0x0009EDD8
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
		if (!(name == "TEXT"))
		{
			return null;
		}
		return this.m_text;
	}

	// Token: 0x0600205C RID: 8284 RVA: 0x000A0C2C File Offset: 0x0009EE2C
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
		if (!(name == "TEXT"))
		{
			return;
		}
		this.m_text = (DbfLocValue)val;
	}

	// Token: 0x0600205D RID: 8285 RVA: 0x000A0C88 File Offset: 0x0009EE88
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
		if (!(name == "TEXT"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x0600205E RID: 8286 RVA: 0x000A0CE0 File Offset: 0x0009EEE0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadScoreLabelDbfRecords loadRecords = new LoadScoreLabelDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600205F RID: 8287 RVA: 0x000A0CF8 File Offset: 0x0009EEF8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ScoreLabelDbfAsset scoreLabelDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ScoreLabelDbfAsset)) as ScoreLabelDbfAsset;
		if (scoreLabelDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ScoreLabelDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < scoreLabelDbfAsset.Records.Count; i++)
		{
			scoreLabelDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (scoreLabelDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002060 RID: 8288 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002061 RID: 8289 RVA: 0x000A0D77 File Offset: 0x0009EF77
	public override void StripUnusedLocales()
	{
		this.m_text.StripUnusedLocales();
	}

	// Token: 0x04001238 RID: 4664
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001239 RID: 4665
	[SerializeField]
	private DbfLocValue m_text;
}
