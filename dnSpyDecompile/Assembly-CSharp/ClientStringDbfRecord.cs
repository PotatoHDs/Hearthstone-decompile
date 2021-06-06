using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001BF RID: 447
[Serializable]
public class ClientStringDbfRecord : DbfRecord
{
	// Token: 0x17000293 RID: 659
	// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0008A986 File Offset: 0x00088B86
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x17000294 RID: 660
	// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0008A98E File Offset: 0x00088B8E
	[DbfField("TEXT")]
	public DbfLocValue Text
	{
		get
		{
			return this.m_text;
		}
	}

	// Token: 0x06001A19 RID: 6681 RVA: 0x0008A996 File Offset: 0x00088B96
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001A1A RID: 6682 RVA: 0x0008A99F File Offset: 0x00088B9F
	public void SetText(DbfLocValue v)
	{
		this.m_text = v;
		v.SetDebugInfo(base.ID, "TEXT");
	}

	// Token: 0x06001A1B RID: 6683 RVA: 0x0008A9BC File Offset: 0x00088BBC
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

	// Token: 0x06001A1C RID: 6684 RVA: 0x0008AA10 File Offset: 0x00088C10
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

	// Token: 0x06001A1D RID: 6685 RVA: 0x0008AA6C File Offset: 0x00088C6C
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

	// Token: 0x06001A1E RID: 6686 RVA: 0x0008AAC4 File Offset: 0x00088CC4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadClientStringDbfRecords loadRecords = new LoadClientStringDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A1F RID: 6687 RVA: 0x0008AADC File Offset: 0x00088CDC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ClientStringDbfAsset clientStringDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ClientStringDbfAsset)) as ClientStringDbfAsset;
		if (clientStringDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ClientStringDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < clientStringDbfAsset.Records.Count; i++)
		{
			clientStringDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (clientStringDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A20 RID: 6688 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A21 RID: 6689 RVA: 0x0008AB5B File Offset: 0x00088D5B
	public override void StripUnusedLocales()
	{
		this.m_text.StripUnusedLocales();
	}

	// Token: 0x04000FD2 RID: 4050
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04000FD3 RID: 4051
	[SerializeField]
	private DbfLocValue m_text;
}
