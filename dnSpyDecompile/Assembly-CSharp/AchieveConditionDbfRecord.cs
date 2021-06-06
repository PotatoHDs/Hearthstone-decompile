using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000147 RID: 327
[Serializable]
public class AchieveConditionDbfRecord : DbfRecord
{
	// Token: 0x170000FE RID: 254
	// (get) Token: 0x0600153F RID: 5439 RVA: 0x0007959A File Offset: 0x0007779A
	[DbfField("ACHIEVE_ID")]
	public int AchieveId
	{
		get
		{
			return this.m_achieveId;
		}
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06001540 RID: 5440 RVA: 0x000795A2 File Offset: 0x000777A2
	[DbfField("SCENARIO_ID")]
	public int ScenarioId
	{
		get
		{
			return this.m_scenarioId;
		}
	}

	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06001541 RID: 5441 RVA: 0x000795AA File Offset: 0x000777AA
	public ScenarioDbfRecord ScenarioRecord
	{
		get
		{
			return GameDbf.Scenario.GetRecord(this.m_scenarioId);
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000795BC File Offset: 0x000777BC
	public void SetAchieveId(int v)
	{
		this.m_achieveId = v;
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000795C5 File Offset: 0x000777C5
	public void SetScenarioId(int v)
	{
		this.m_scenarioId = v;
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000795D0 File Offset: 0x000777D0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "ACHIEVE_ID")
		{
			return this.m_achieveId;
		}
		if (!(name == "SCENARIO_ID"))
		{
			return null;
		}
		return this.m_scenarioId;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x0007962C File Offset: 0x0007782C
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "ACHIEVE_ID")
		{
			this.m_achieveId = (int)val;
			return;
		}
		if (!(name == "SCENARIO_ID"))
		{
			return;
		}
		this.m_scenarioId = (int)val;
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x00079688 File Offset: 0x00077888
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "ACHIEVE_ID")
		{
			return typeof(int);
		}
		if (!(name == "SCENARIO_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x000796E0 File Offset: 0x000778E0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadAchieveConditionDbfRecords loadRecords = new LoadAchieveConditionDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000796F8 File Offset: 0x000778F8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		AchieveConditionDbfAsset achieveConditionDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(AchieveConditionDbfAsset)) as AchieveConditionDbfAsset;
		if (achieveConditionDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("AchieveConditionDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < achieveConditionDbfAsset.Records.Count; i++)
		{
			achieveConditionDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (achieveConditionDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000E31 RID: 3633
	[SerializeField]
	private int m_achieveId;

	// Token: 0x04000E32 RID: 3634
	[SerializeField]
	private int m_scenarioId;
}
