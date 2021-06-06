using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000223 RID: 547
[Serializable]
public class MultiClassGroupDbfRecord : DbfRecord
{
	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00097F52 File Offset: 0x00096152
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00097F5A File Offset: 0x0009615A
	[DbfField("ICON_ASSET_PATH")]
	public string IconAssetPath
	{
		get
		{
			return this.m_iconAssetPath;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00097F62 File Offset: 0x00096162
	[DbfField("CARD_COLOR_TYPE")]
	public int CardColorType
	{
		get
		{
			return this.m_cardColorType;
		}
	}

	// Token: 0x06001DAC RID: 7596 RVA: 0x00097F6A File Offset: 0x0009616A
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001DAD RID: 7597 RVA: 0x00097F73 File Offset: 0x00096173
	public void SetIconAssetPath(string v)
	{
		this.m_iconAssetPath = v;
	}

	// Token: 0x06001DAE RID: 7598 RVA: 0x00097F7C File Offset: 0x0009617C
	public void SetCardColorType(int v)
	{
		this.m_cardColorType = v;
	}

	// Token: 0x06001DAF RID: 7599 RVA: 0x00097F88 File Offset: 0x00096188
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
		if (name == "ICON_ASSET_PATH")
		{
			return this.m_iconAssetPath;
		}
		if (!(name == "CARD_COLOR_TYPE"))
		{
			return null;
		}
		return this.m_cardColorType;
	}

	// Token: 0x06001DB0 RID: 7600 RVA: 0x00097FF4 File Offset: 0x000961F4
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
		if (name == "ICON_ASSET_PATH")
		{
			this.m_iconAssetPath = (string)val;
			return;
		}
		if (!(name == "CARD_COLOR_TYPE"))
		{
			return;
		}
		this.m_cardColorType = (int)val;
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x0009806C File Offset: 0x0009626C
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
		if (name == "ICON_ASSET_PATH")
		{
			return typeof(string);
		}
		if (!(name == "CARD_COLOR_TYPE"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x000980DC File Offset: 0x000962DC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMultiClassGroupDbfRecords loadRecords = new LoadMultiClassGroupDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001DB3 RID: 7603 RVA: 0x000980F4 File Offset: 0x000962F4
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MultiClassGroupDbfAsset multiClassGroupDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MultiClassGroupDbfAsset)) as MultiClassGroupDbfAsset;
		if (multiClassGroupDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("MultiClassGroupDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < multiClassGroupDbfAsset.Records.Count; i++)
		{
			multiClassGroupDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (multiClassGroupDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001DB4 RID: 7604 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001DB5 RID: 7605 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001160 RID: 4448
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001161 RID: 4449
	[SerializeField]
	private string m_iconAssetPath;

	// Token: 0x04001162 RID: 4450
	[SerializeField]
	private int m_cardColorType;
}
