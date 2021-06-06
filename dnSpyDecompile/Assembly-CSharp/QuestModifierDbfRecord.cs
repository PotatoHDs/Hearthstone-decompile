using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000244 RID: 580
[Serializable]
public class QuestModifierDbfRecord : DbfRecord
{
	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0009B59E File Offset: 0x0009979E
	[DbfField("EVENT")]
	public string Event
	{
		get
		{
			return this.m_event;
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0009B5A6 File Offset: 0x000997A6
	[DbfField("QUOTA")]
	public int Quota
	{
		get
		{
			return this.m_quota;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0009B5AE File Offset: 0x000997AE
	[DbfField("DESCRIPTION")]
	public string Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0009B5B6 File Offset: 0x000997B6
	[DbfField("STYLE_NAME")]
	public string StyleName
	{
		get
		{
			return this.m_styleName;
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0009B5BE File Offset: 0x000997BE
	[DbfField("QUEST_NAME")]
	public DbfLocValue QuestName
	{
		get
		{
			return this.m_questName;
		}
	}

	// Token: 0x06001EC3 RID: 7875 RVA: 0x0009B5C6 File Offset: 0x000997C6
	public void SetEvent(string v)
	{
		this.m_event = v;
	}

	// Token: 0x06001EC4 RID: 7876 RVA: 0x0009B5CF File Offset: 0x000997CF
	public void SetQuota(int v)
	{
		this.m_quota = v;
	}

	// Token: 0x06001EC5 RID: 7877 RVA: 0x0009B5D8 File Offset: 0x000997D8
	public void SetDescription(string v)
	{
		this.m_description = v;
	}

	// Token: 0x06001EC6 RID: 7878 RVA: 0x0009B5E1 File Offset: 0x000997E1
	public void SetStyleName(string v)
	{
		this.m_styleName = v;
	}

	// Token: 0x06001EC7 RID: 7879 RVA: 0x0009B5EA File Offset: 0x000997EA
	public void SetQuestName(DbfLocValue v)
	{
		this.m_questName = v;
		v.SetDebugInfo(base.ID, "QUEST_NAME");
	}

	// Token: 0x06001EC8 RID: 7880 RVA: 0x0009B604 File Offset: 0x00099804
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "EVENT")
		{
			return this.m_event;
		}
		if (name == "QUOTA")
		{
			return this.m_quota;
		}
		if (name == "DESCRIPTION")
		{
			return this.m_description;
		}
		if (name == "STYLE_NAME")
		{
			return this.m_styleName;
		}
		if (!(name == "QUEST_NAME"))
		{
			return null;
		}
		return this.m_questName;
	}

	// Token: 0x06001EC9 RID: 7881 RVA: 0x0009B698 File Offset: 0x00099898
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "EVENT")
		{
			this.m_event = (string)val;
			return;
		}
		if (name == "QUOTA")
		{
			this.m_quota = (int)val;
			return;
		}
		if (name == "DESCRIPTION")
		{
			this.m_description = (string)val;
			return;
		}
		if (name == "STYLE_NAME")
		{
			this.m_styleName = (string)val;
			return;
		}
		if (!(name == "QUEST_NAME"))
		{
			return;
		}
		this.m_questName = (DbfLocValue)val;
	}

	// Token: 0x06001ECA RID: 7882 RVA: 0x0009B744 File Offset: 0x00099944
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "EVENT")
		{
			return typeof(string);
		}
		if (name == "QUOTA")
		{
			return typeof(int);
		}
		if (name == "DESCRIPTION")
		{
			return typeof(string);
		}
		if (name == "STYLE_NAME")
		{
			return typeof(string);
		}
		if (!(name == "QUEST_NAME"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001ECB RID: 7883 RVA: 0x0009B7E4 File Offset: 0x000999E4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadQuestModifierDbfRecords loadRecords = new LoadQuestModifierDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001ECC RID: 7884 RVA: 0x0009B7FC File Offset: 0x000999FC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		QuestModifierDbfAsset questModifierDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(QuestModifierDbfAsset)) as QuestModifierDbfAsset;
		if (questModifierDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("QuestModifierDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < questModifierDbfAsset.Records.Count; i++)
		{
			questModifierDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (questModifierDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001ECD RID: 7885 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001ECE RID: 7886 RVA: 0x0009B87B File Offset: 0x00099A7B
	public override void StripUnusedLocales()
	{
		this.m_questName.StripUnusedLocales();
	}

	// Token: 0x040011B4 RID: 4532
	[SerializeField]
	private string m_event = "none";

	// Token: 0x040011B5 RID: 4533
	[SerializeField]
	private int m_quota;

	// Token: 0x040011B6 RID: 4534
	[SerializeField]
	private string m_description;

	// Token: 0x040011B7 RID: 4535
	[SerializeField]
	private string m_styleName;

	// Token: 0x040011B8 RID: 4536
	[SerializeField]
	private DbfLocValue m_questName;
}
