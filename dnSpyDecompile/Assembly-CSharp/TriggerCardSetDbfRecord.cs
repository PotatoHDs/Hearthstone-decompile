using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200028C RID: 652
[Serializable]
public class TriggerCardSetDbfRecord : DbfRecord
{
	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x06002117 RID: 8471 RVA: 0x000A29C6 File Offset: 0x000A0BC6
	[DbfField("TRIGGER_ID")]
	public int TriggerId
	{
		get
		{
			return this.m_triggerId;
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x06002118 RID: 8472 RVA: 0x000A29CE File Offset: 0x000A0BCE
	[DbfField("CARD_SET_ID")]
	public int CardSetId
	{
		get
		{
			return this.m_cardSetId;
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x06002119 RID: 8473 RVA: 0x000A29D6 File Offset: 0x000A0BD6
	public CardSetDbfRecord CardSetRecord
	{
		get
		{
			return GameDbf.CardSet.GetRecord(this.m_cardSetId);
		}
	}

	// Token: 0x0600211A RID: 8474 RVA: 0x000A29E8 File Offset: 0x000A0BE8
	public void SetTriggerId(int v)
	{
		this.m_triggerId = v;
	}

	// Token: 0x0600211B RID: 8475 RVA: 0x000A29F1 File Offset: 0x000A0BF1
	public void SetCardSetId(int v)
	{
		this.m_cardSetId = v;
	}

	// Token: 0x0600211C RID: 8476 RVA: 0x000A29FC File Offset: 0x000A0BFC
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
		if (!(name == "CARD_SET_ID"))
		{
			return null;
		}
		return this.m_cardSetId;
	}

	// Token: 0x0600211D RID: 8477 RVA: 0x000A2A58 File Offset: 0x000A0C58
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
		if (!(name == "CARD_SET_ID"))
		{
			return;
		}
		this.m_cardSetId = (int)val;
	}

	// Token: 0x0600211E RID: 8478 RVA: 0x000A2AB4 File Offset: 0x000A0CB4
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
		if (!(name == "CARD_SET_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x0600211F RID: 8479 RVA: 0x000A2B0C File Offset: 0x000A0D0C
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadTriggerCardSetDbfRecords loadRecords = new LoadTriggerCardSetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06002120 RID: 8480 RVA: 0x000A2B24 File Offset: 0x000A0D24
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		TriggerCardSetDbfAsset triggerCardSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(TriggerCardSetDbfAsset)) as TriggerCardSetDbfAsset;
		if (triggerCardSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("TriggerCardSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < triggerCardSetDbfAsset.Records.Count; i++)
		{
			triggerCardSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (triggerCardSetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06002121 RID: 8481 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06002122 RID: 8482 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400126B RID: 4715
	[SerializeField]
	private int m_triggerId;

	// Token: 0x0400126C RID: 4716
	[SerializeField]
	private int m_cardSetId;
}
