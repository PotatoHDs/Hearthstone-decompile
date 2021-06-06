using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001BC RID: 444
[Serializable]
public class ClassExclusionsDbfRecord : DbfRecord
{
	// Token: 0x17000290 RID: 656
	// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0008A70E File Offset: 0x0008890E
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x17000291 RID: 657
	// (get) Token: 0x06001A07 RID: 6663 RVA: 0x0008A716 File Offset: 0x00088916
	[DbfField("CLASS_ID")]
	public int ClassId
	{
		get
		{
			return this.m_classId;
		}
	}

	// Token: 0x17000292 RID: 658
	// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0008A71E File Offset: 0x0008891E
	public ClassDbfRecord ClassRecord
	{
		get
		{
			return GameDbf.Class.GetRecord(this.m_classId);
		}
	}

	// Token: 0x06001A09 RID: 6665 RVA: 0x0008A730 File Offset: 0x00088930
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x06001A0A RID: 6666 RVA: 0x0008A739 File Offset: 0x00088939
	public void SetClassId(int v)
	{
		this.m_classId = v;
	}

	// Token: 0x06001A0B RID: 6667 RVA: 0x0008A744 File Offset: 0x00088944
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "SCENARIO_ID")
		{
			return this.m_scenarioId;
		}
		if (!(name == "CLASS_ID"))
		{
			return null;
		}
		return this.m_classId;
	}

	// Token: 0x06001A0C RID: 6668 RVA: 0x0008A7A0 File Offset: 0x000889A0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "SCENARIO_ID")
		{
			this.m_scenarioId = (int)val;
			return;
		}
		if (!(name == "CLASS_ID"))
		{
			return;
		}
		this.m_classId = (int)val;
	}

	// Token: 0x06001A0D RID: 6669 RVA: 0x0008A7FC File Offset: 0x000889FC
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "SCENARIO_ID")
		{
			return typeof(int);
		}
		if (!(name == "CLASS_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001A0E RID: 6670 RVA: 0x0008A854 File Offset: 0x00088A54
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadClassExclusionsDbfRecords loadRecords = new LoadClassExclusionsDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A0F RID: 6671 RVA: 0x0008A86C File Offset: 0x00088A6C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ClassExclusionsDbfAsset classExclusionsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ClassExclusionsDbfAsset)) as ClassExclusionsDbfAsset;
		if (classExclusionsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ClassExclusionsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < classExclusionsDbfAsset.Records.Count; i++)
		{
			classExclusionsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (classExclusionsDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A10 RID: 6672 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A11 RID: 6673 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FCE RID: 4046
	[SerializeField]
	private int m_scenarioId;

	// Token: 0x04000FCF RID: 4047
	[SerializeField]
	private int m_classId;
}
