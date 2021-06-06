using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001AA RID: 426
[Serializable]
public class CardSetTimingDbfRecord : DbfRecord
{
	// Token: 0x1700026F RID: 623
	// (get) Token: 0x06001980 RID: 6528 RVA: 0x00088F52 File Offset: 0x00087152
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000270 RID: 624
	// (get) Token: 0x06001981 RID: 6529 RVA: 0x00088F5A File Offset: 0x0008715A
	[DbfField("CARD_SET_ID")]
	public int CardSetId
	{
		get
		{
			return this.m_cardSetId;
		}
	}

	// Token: 0x17000271 RID: 625
	// (get) Token: 0x06001982 RID: 6530 RVA: 0x00088F62 File Offset: 0x00087162
	public CardSetDbfRecord CardSetRecord
	{
		get
		{
			return GameDbf.CardSet.GetRecord(this.m_cardSetId);
		}
	}

	// Token: 0x17000272 RID: 626
	// (get) Token: 0x06001983 RID: 6531 RVA: 0x00088F74 File Offset: 0x00087174
	[DbfField("EVENT_TIMING_EVENT")]
	public string EventTimingEvent
	{
		get
		{
			return this.m_eventTimingEvent;
		}
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x00088F7C File Offset: 0x0008717C
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x00088F85 File Offset: 0x00087185
	public void SetCardSetId(int v)
	{
		this.m_cardSetId = v;
	}

	// Token: 0x06001986 RID: 6534 RVA: 0x00088F8E File Offset: 0x0008718E
	public void SetEventTimingEvent(string v)
	{
		this.m_eventTimingEvent = v;
	}

	// Token: 0x06001987 RID: 6535 RVA: 0x00088F98 File Offset: 0x00087198
	public override object GetVar(string name)
	{
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "CARD_SET_ID")
		{
			return this.m_cardSetId;
		}
		if (!(name == "EVENT_TIMING_EVENT"))
		{
			return null;
		}
		return this.m_eventTimingEvent;
	}

	// Token: 0x06001988 RID: 6536 RVA: 0x00088FF0 File Offset: 0x000871F0
	public override void SetVar(string name, object val)
	{
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (name == "CARD_SET_ID")
		{
			this.m_cardSetId = (int)val;
			return;
		}
		if (!(name == "EVENT_TIMING_EVENT"))
		{
			return;
		}
		this.m_eventTimingEvent = (string)val;
	}

	// Token: 0x06001989 RID: 6537 RVA: 0x0008904C File Offset: 0x0008724C
	public override Type GetVarType(string name)
	{
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "CARD_SET_ID")
		{
			return typeof(int);
		}
		if (!(name == "EVENT_TIMING_EVENT"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x0600198A RID: 6538 RVA: 0x000890A4 File Offset: 0x000872A4
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardSetTimingDbfRecords loadRecords = new LoadCardSetTimingDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x0600198B RID: 6539 RVA: 0x000890BC File Offset: 0x000872BC
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardSetTimingDbfAsset cardSetTimingDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardSetTimingDbfAsset)) as CardSetTimingDbfAsset;
		if (cardSetTimingDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardSetTimingDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardSetTimingDbfAsset.Records.Count; i++)
		{
			cardSetTimingDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardSetTimingDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x0600198C RID: 6540 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x0600198D RID: 6541 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FA5 RID: 4005
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000FA6 RID: 4006
	[SerializeField]
	private int m_cardSetId;

	// Token: 0x04000FA7 RID: 4007
	[SerializeField]
	private string m_eventTimingEvent = "always";
}
