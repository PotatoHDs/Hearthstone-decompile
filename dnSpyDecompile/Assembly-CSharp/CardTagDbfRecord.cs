using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001AD RID: 429
[Serializable]
public class CardTagDbfRecord : DbfRecord
{
	// Token: 0x17000273 RID: 627
	// (get) Token: 0x06001993 RID: 6547 RVA: 0x000891EA File Offset: 0x000873EA
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000274 RID: 628
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x000891F2 File Offset: 0x000873F2
	[DbfField("TAG_ID")]
	public int TagId
	{
		get
		{
			return this.m_tagId;
		}
	}

	// Token: 0x17000275 RID: 629
	// (get) Token: 0x06001995 RID: 6549 RVA: 0x000891FA File Offset: 0x000873FA
	[DbfField("TAG_VALUE")]
	public int TagValue
	{
		get
		{
			return this.m_tagValue;
		}
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06001996 RID: 6550 RVA: 0x00089202 File Offset: 0x00087402
	[DbfField("IS_REFERENCE_TAG")]
	public bool IsReferenceTag
	{
		get
		{
			return this.m_isReferenceTag;
		}
	}

	// Token: 0x17000277 RID: 631
	// (get) Token: 0x06001997 RID: 6551 RVA: 0x0008920A File Offset: 0x0008740A
	[DbfField("IS_POWER_KEYWORD_TAG")]
	public bool IsPowerKeywordTag
	{
		get
		{
			return this.m_isPowerKeywordTag;
		}
	}

	// Token: 0x06001998 RID: 6552 RVA: 0x00089212 File Offset: 0x00087412
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001999 RID: 6553 RVA: 0x0008921B File Offset: 0x0008741B
	public void SetTagId(int v)
	{
		this.m_tagId = v;
	}

	// Token: 0x0600199A RID: 6554 RVA: 0x00089224 File Offset: 0x00087424
	public void SetTagValue(int v)
	{
		this.m_tagValue = v;
	}

	// Token: 0x0600199B RID: 6555 RVA: 0x0008922D File Offset: 0x0008742D
	public void SetIsReferenceTag(bool v)
	{
		this.m_isReferenceTag = v;
	}

	// Token: 0x0600199C RID: 6556 RVA: 0x00089236 File Offset: 0x00087436
	public void SetIsPowerKeywordTag(bool v)
	{
		this.m_isPowerKeywordTag = v;
	}

	// Token: 0x0600199D RID: 6557 RVA: 0x00089240 File Offset: 0x00087440
	public override object GetVar(string name)
	{
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "TAG_ID")
		{
			return this.m_tagId;
		}
		if (name == "TAG_VALUE")
		{
			return this.m_tagValue;
		}
		if (name == "IS_REFERENCE_TAG")
		{
			return this.m_isReferenceTag;
		}
		if (!(name == "IS_POWER_KEYWORD_TAG"))
		{
			return null;
		}
		return this.m_isPowerKeywordTag;
	}

	// Token: 0x0600199E RID: 6558 RVA: 0x000892D0 File Offset: 0x000874D0
	public override void SetVar(string name, object val)
	{
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (name == "TAG_ID")
		{
			this.m_tagId = (int)val;
			return;
		}
		if (name == "TAG_VALUE")
		{
			this.m_tagValue = (int)val;
			return;
		}
		if (name == "IS_REFERENCE_TAG")
		{
			this.m_isReferenceTag = (bool)val;
			return;
		}
		if (!(name == "IS_POWER_KEYWORD_TAG"))
		{
			return;
		}
		this.m_isPowerKeywordTag = (bool)val;
	}

	// Token: 0x0600199F RID: 6559 RVA: 0x00089360 File Offset: 0x00087560
	public override Type GetVarType(string name)
	{
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "TAG_ID")
		{
			return typeof(int);
		}
		if (name == "TAG_VALUE")
		{
			return typeof(int);
		}
		if (name == "IS_REFERENCE_TAG")
		{
			return typeof(bool);
		}
		if (!(name == "IS_POWER_KEYWORD_TAG"))
		{
			return null;
		}
		return typeof(bool);
	}

	// Token: 0x060019A0 RID: 6560 RVA: 0x000893E8 File Offset: 0x000875E8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardTagDbfRecords loadRecords = new LoadCardTagDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060019A1 RID: 6561 RVA: 0x00089400 File Offset: 0x00087600
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardTagDbfAsset cardTagDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardTagDbfAsset)) as CardTagDbfAsset;
		if (cardTagDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardTagDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardTagDbfAsset.Records.Count; i++)
		{
			cardTagDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardTagDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060019A2 RID: 6562 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060019A3 RID: 6563 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000FAA RID: 4010
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000FAB RID: 4011
	[SerializeField]
	private int m_tagId;

	// Token: 0x04000FAC RID: 4012
	[SerializeField]
	private int m_tagValue;

	// Token: 0x04000FAD RID: 4013
	[SerializeField]
	private bool m_isReferenceTag;

	// Token: 0x04000FAE RID: 4014
	[SerializeField]
	private bool m_isPowerKeywordTag;
}
