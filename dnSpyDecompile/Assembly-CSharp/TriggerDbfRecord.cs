using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200028F RID: 655
[Serializable]
public class TriggerDbfRecord : DbfRecord
{
	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x06002128 RID: 8488 RVA: 0x000A2C3E File Offset: 0x000A0E3E
	[DbfField("TRIGGER_TYPE")]
	public Trigger.Triggertype TriggerType
	{
		get
		{
			return this.m_triggerType;
		}
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000A2C46 File Offset: 0x000A0E46
	public void SetTriggerType(Trigger.Triggertype v)
	{
		this.m_triggerType = v;
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x000A2C4F File Offset: 0x000A0E4F
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (!(name == "TRIGGER_TYPE"))
		{
			return null;
		}
		return this.m_triggerType;
	}

	// Token: 0x0600212B RID: 8491 RVA: 0x000A2C88 File Offset: 0x000A0E88
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "TRIGGER_TYPE"))
		{
			return;
		}
		if (val == null)
		{
			this.m_triggerType = Trigger.Triggertype.LUA;
			return;
		}
		if (val is Trigger.Triggertype || val is int)
		{
			this.m_triggerType = (Trigger.Triggertype)val;
			return;
		}
		if (val is string)
		{
			this.m_triggerType = Trigger.ParseTriggertypeValue((string)val);
		}
	}

	// Token: 0x0600212C RID: 8492 RVA: 0x000A2CFE File Offset: 0x000A0EFE
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (!(name == "TRIGGER_TYPE"))
		{
			return null;
		}
		return typeof(Trigger.Triggertype);
	}

	// Token: 0x0600212D RID: 8493 RVA: 0x000A2D33 File Offset: 0x000A0F33
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTriggerDbfRecords loadRecords = new LoadTriggerDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600212E RID: 8494 RVA: 0x000A2D4C File Offset: 0x000A0F4C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TriggerDbfAsset triggerDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TriggerDbfAsset)) as TriggerDbfAsset;
		if (triggerDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("TriggerDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < triggerDbfAsset.Records.Count; i++)
		{
			triggerDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (triggerDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600212F RID: 8495 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002130 RID: 8496 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400126F RID: 4719
	[SerializeField]
	private Trigger.Triggertype m_triggerType = Trigger.ParseTriggertypeValue("lua");
}
