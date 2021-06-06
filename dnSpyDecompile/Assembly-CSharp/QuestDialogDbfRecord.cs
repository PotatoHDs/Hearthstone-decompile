using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000235 RID: 565
[Serializable]
public class QuestDialogDbfRecord : DbfRecord
{
	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001E36 RID: 7734 RVA: 0x000998EA File Offset: 0x00097AEA
	[DbfField("ON_COMPLETE_BANNER_ID")]
	public int OnCompleteBannerId
	{
		get
		{
			return this.m_onCompleteBannerId;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06001E37 RID: 7735 RVA: 0x000998F2 File Offset: 0x00097AF2
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000998FA File Offset: 0x00097AFA
	public List<QuestDialogOnCompleteDbfRecord> OnCompleteData
	{
		get
		{
			return GameDbf.QuestDialogOnComplete.GetRecords((QuestDialogOnCompleteDbfRecord r) => r.QuestDialogId == base.ID, -1);
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06001E39 RID: 7737 RVA: 0x00099913 File Offset: 0x00097B13
	public List<QuestDialogOnProgress1DbfRecord> OnProgress1
	{
		get
		{
			return GameDbf.QuestDialogOnProgress1.GetRecords((QuestDialogOnProgress1DbfRecord r) => r.QuestDialogId == base.ID, -1);
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x06001E3A RID: 7738 RVA: 0x0009992C File Offset: 0x00097B2C
	public List<QuestDialogOnProgress2DbfRecord> OnProgress2
	{
		get
		{
			return GameDbf.QuestDialogOnProgress2.GetRecords((QuestDialogOnProgress2DbfRecord r) => r.QuestDialogId == base.ID, -1);
		}
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x06001E3B RID: 7739 RVA: 0x00099945 File Offset: 0x00097B45
	public List<QuestDialogOnReceivedDbfRecord> OnReceivedData
	{
		get
		{
			return GameDbf.QuestDialogOnReceived.GetRecords((QuestDialogOnReceivedDbfRecord r) => r.QuestDialogId == base.ID, -1);
		}
	}

	// Token: 0x06001E3C RID: 7740 RVA: 0x0009995E File Offset: 0x00097B5E
	public void SetOnCompleteBannerId(int v)
	{
		this.m_onCompleteBannerId = v;
	}

	// Token: 0x06001E3D RID: 7741 RVA: 0x00099967 File Offset: 0x00097B67
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001E3E RID: 7742 RVA: 0x00099970 File Offset: 0x00097B70
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			return this.m_onCompleteBannerId;
		}
		if (!(name == "NOTE_DESC"))
		{
			return null;
		}
		return this.m_noteDesc;
	}

	// Token: 0x06001E3F RID: 7743 RVA: 0x000999C8 File Offset: 0x00097BC8
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			this.m_onCompleteBannerId = (int)val;
			return;
		}
		if (!(name == "NOTE_DESC"))
		{
			return;
		}
		this.m_noteDesc = (string)val;
	}

	// Token: 0x06001E40 RID: 7744 RVA: 0x00099A24 File Offset: 0x00097C24
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ON_COMPLETE_BANNER_ID")
		{
			return typeof(int);
		}
		if (!(name == "NOTE_DESC"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001E41 RID: 7745 RVA: 0x00099A7C File Offset: 0x00097C7C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestDialogDbfRecords loadRecords = new LoadQuestDialogDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001E42 RID: 7746 RVA: 0x00099A94 File Offset: 0x00097C94
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestDialogDbfAsset questDialogDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestDialogDbfAsset)) as QuestDialogDbfAsset;
		if (questDialogDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestDialogDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questDialogDbfAsset.Records.Count; i++)
		{
			questDialogDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questDialogDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001E43 RID: 7747 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001E44 RID: 7748 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001188 RID: 4488
	[SerializeField]
	private int m_onCompleteBannerId;

	// Token: 0x04001189 RID: 4489
	[SerializeField]
	private string m_noteDesc;
}
