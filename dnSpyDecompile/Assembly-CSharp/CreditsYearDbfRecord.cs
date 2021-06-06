using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C5 RID: 453
[Serializable]
public class CreditsYearDbfRecord : DbfRecord
{
	// Token: 0x1700029A RID: 666
	// (get) Token: 0x06001A3C RID: 6716 RVA: 0x0008AF62 File Offset: 0x00089162
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700029B RID: 667
	// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0008AF6A File Offset: 0x0008916A
	[DbfField("CONTENTS_FILENAME")]
	public string ContentsFilename
	{
		get
		{
			return this.m_contentsFilename;
		}
	}

	// Token: 0x1700029C RID: 668
	// (get) Token: 0x06001A3E RID: 6718 RVA: 0x0008AF72 File Offset: 0x00089172
	[DbfField("BUTTON_LABEL")]
	public DbfLocValue ButtonLabel
	{
		get
		{
			return this.m_buttonLabel;
		}
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x0008AF7A File Offset: 0x0008917A
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x0008AF83 File Offset: 0x00089183
	public void SetContentsFilename(string v)
	{
		this.m_contentsFilename = v;
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x0008AF8C File Offset: 0x0008918C
	public void SetButtonLabel(DbfLocValue v)
	{
		this.m_buttonLabel = v;
		v.SetDebugInfo(base.ID, "BUTTON_LABEL");
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x0008AFA8 File Offset: 0x000891A8
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
		if (name == "CONTENTS_FILENAME")
		{
			return this.m_contentsFilename;
		}
		if (!(name == "BUTTON_LABEL"))
		{
			return null;
		}
		return this.m_buttonLabel;
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x0008B010 File Offset: 0x00089210
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
		if (name == "CONTENTS_FILENAME")
		{
			this.m_contentsFilename = (string)val;
			return;
		}
		if (!(name == "BUTTON_LABEL"))
		{
			return;
		}
		this.m_buttonLabel = (DbfLocValue)val;
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x0008B088 File Offset: 0x00089288
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
		if (name == "CONTENTS_FILENAME")
		{
			return typeof(string);
		}
		if (!(name == "BUTTON_LABEL"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x0008B0F8 File Offset: 0x000892F8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCreditsYearDbfRecords loadRecords = new LoadCreditsYearDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x0008B110 File Offset: 0x00089310
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CreditsYearDbfAsset creditsYearDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CreditsYearDbfAsset)) as CreditsYearDbfAsset;
		if (creditsYearDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CreditsYearDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < creditsYearDbfAsset.Records.Count; i++)
		{
			creditsYearDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (creditsYearDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x0008B18F File Offset: 0x0008938F
	public override void StripUnusedLocales()
	{
		this.m_buttonLabel.StripUnusedLocales();
	}

	// Token: 0x04000FDC RID: 4060
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000FDD RID: 4061
	[SerializeField]
	private string m_contentsFilename;

	// Token: 0x04000FDE RID: 4062
	[SerializeField]
	private DbfLocValue m_buttonLabel;
}
