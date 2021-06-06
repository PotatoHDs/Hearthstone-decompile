using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000289 RID: 649
[Serializable]
public class TriggerCardDbfRecord : DbfRecord
{
	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x06002106 RID: 8454 RVA: 0x000A274E File Offset: 0x000A094E
	[DbfField("TRIGGER_ID")]
	public int TriggerId
	{
		get
		{
			return this.m_triggerId;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x06002107 RID: 8455 RVA: 0x000A2756 File Offset: 0x000A0956
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x06002108 RID: 8456 RVA: 0x000A275E File Offset: 0x000A095E
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x000A2770 File Offset: 0x000A0970
	public void SetTriggerId(int v)
	{
		this.m_triggerId = v;
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x000A2779 File Offset: 0x000A0979
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x000A2784 File Offset: 0x000A0984
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "TRIGGER_ID")
		{
			return this.m_triggerId;
		}
		if (!(name == "CARD_ID"))
		{
			return null;
		}
		return this.m_cardId;
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x000A27E0 File Offset: 0x000A09E0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "TRIGGER_ID")
		{
			this.m_triggerId = (int)val;
			return;
		}
		if (!(name == "CARD_ID"))
		{
			return;
		}
		this.m_cardId = (int)val;
	}

	// Token: 0x0600210D RID: 8461 RVA: 0x000A283C File Offset: 0x000A0A3C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "TRIGGER_ID")
		{
			return typeof(int);
		}
		if (!(name == "CARD_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600210E RID: 8462 RVA: 0x000A2894 File Offset: 0x000A0A94
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTriggerCardDbfRecords loadRecords = new LoadTriggerCardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600210F RID: 8463 RVA: 0x000A28AC File Offset: 0x000A0AAC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TriggerCardDbfAsset triggerCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TriggerCardDbfAsset)) as TriggerCardDbfAsset;
		if (triggerCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("TriggerCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < triggerCardDbfAsset.Records.Count; i++)
		{
			triggerCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (triggerCardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002110 RID: 8464 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002111 RID: 8465 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001267 RID: 4711
	[SerializeField]
	private int m_triggerId;

	// Token: 0x04001268 RID: 4712
	[SerializeField]
	private int m_cardId;
}
