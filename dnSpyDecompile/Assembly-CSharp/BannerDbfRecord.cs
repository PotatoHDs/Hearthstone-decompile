using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000180 RID: 384
[Serializable]
public class BannerDbfRecord : DbfRecord
{
	// Token: 0x17000200 RID: 512
	// (get) Token: 0x060017FE RID: 6142 RVA: 0x00083D56 File Offset: 0x00081F56
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x060017FF RID: 6143 RVA: 0x00083D5E File Offset: 0x00081F5E
	[DbfField("HEADER_TEXT")]
	public DbfLocValue HeaderText
	{
		get
		{
			return this.m_headerText;
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06001800 RID: 6144 RVA: 0x00083D66 File Offset: 0x00081F66
	[DbfField("TEXT")]
	public DbfLocValue Text
	{
		get
		{
			return this.m_text;
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06001801 RID: 6145 RVA: 0x00083D6E File Offset: 0x00081F6E
	[DbfField("PREFAB")]
	public string Prefab
	{
		get
		{
			return this.m_prefab;
		}
	}

	// Token: 0x06001802 RID: 6146 RVA: 0x00083D76 File Offset: 0x00081F76
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001803 RID: 6147 RVA: 0x00083D7F File Offset: 0x00081F7F
	public void SetHeaderText(DbfLocValue v)
	{
		this.m_headerText = v;
		v.SetDebugInfo(base.ID, "HEADER_TEXT");
	}

	// Token: 0x06001804 RID: 6148 RVA: 0x00083D99 File Offset: 0x00081F99
	public void SetText(DbfLocValue v)
	{
		this.m_text = v;
		v.SetDebugInfo(base.ID, "TEXT");
	}

	// Token: 0x06001805 RID: 6149 RVA: 0x00083DB3 File Offset: 0x00081FB3
	public void SetPrefab(string v)
	{
		this.m_prefab = v;
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x00083DBC File Offset: 0x00081FBC
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
		if (name == "HEADER_TEXT")
		{
			return this.m_headerText;
		}
		if (name == "TEXT")
		{
			return this.m_text;
		}
		if (!(name == "PREFAB"))
		{
			return null;
		}
		return this.m_prefab;
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x00083E38 File Offset: 0x00082038
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
		if (name == "HEADER_TEXT")
		{
			this.m_headerText = (DbfLocValue)val;
			return;
		}
		if (name == "TEXT")
		{
			this.m_text = (DbfLocValue)val;
			return;
		}
		if (!(name == "PREFAB"))
		{
			return;
		}
		this.m_prefab = (string)val;
	}

	// Token: 0x06001808 RID: 6152 RVA: 0x00083EC8 File Offset: 0x000820C8
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
		if (name == "HEADER_TEXT")
		{
			return typeof(DbfLocValue);
		}
		if (name == "TEXT")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "PREFAB"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001809 RID: 6153 RVA: 0x00083F50 File Offset: 0x00082150
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadBannerDbfRecords loadRecords = new LoadBannerDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600180A RID: 6154 RVA: 0x00083F68 File Offset: 0x00082168
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		BannerDbfAsset bannerDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(BannerDbfAsset)) as BannerDbfAsset;
		if (bannerDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("BannerDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < bannerDbfAsset.Records.Count; i++)
		{
			bannerDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (bannerDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600180B RID: 6155 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600180C RID: 6156 RVA: 0x00083FE7 File Offset: 0x000821E7
	public override void StripUnusedLocales()
	{
		this.m_headerText.StripUnusedLocales();
		this.m_text.StripUnusedLocales();
	}

	// Token: 0x04000F25 RID: 3877
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000F26 RID: 3878
	[SerializeField]
	private DbfLocValue m_headerText;

	// Token: 0x04000F27 RID: 3879
	[SerializeField]
	private DbfLocValue m_text;

	// Token: 0x04000F28 RID: 3880
	[SerializeField]
	private string m_prefab;
}
