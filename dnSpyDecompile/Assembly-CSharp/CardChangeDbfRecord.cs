using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000195 RID: 405
[Serializable]
public class CardChangeDbfRecord : DbfRecord
{
	// Token: 0x1700022D RID: 557
	// (get) Token: 0x060018AA RID: 6314 RVA: 0x0008614A File Offset: 0x0008434A
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x060018AB RID: 6315 RVA: 0x00086152 File Offset: 0x00084352
	[DbfField("TAG_ID")]
	public int TagId
	{
		get
		{
			return this.m_tagId;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x060018AC RID: 6316 RVA: 0x0008615A File Offset: 0x0008435A
	[DbfField("CHANGE_TYPE")]
	public CardChange.ChangeType ChangeType
	{
		get
		{
			return this.m_changeType;
		}
	}

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x060018AD RID: 6317 RVA: 0x00086162 File Offset: 0x00084362
	[DbfField("SORT_ORDER")]
	public int SortOrder
	{
		get
		{
			return this.m_sortOrder;
		}
	}

	// Token: 0x060018AE RID: 6318 RVA: 0x0008616A File Offset: 0x0008436A
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x060018AF RID: 6319 RVA: 0x00086173 File Offset: 0x00084373
	public void SetTagId(int v)
	{
		this.m_tagId = v;
	}

	// Token: 0x060018B0 RID: 6320 RVA: 0x0008617C File Offset: 0x0008437C
	public void SetChangeType(CardChange.ChangeType v)
	{
		this.m_changeType = v;
	}

	// Token: 0x060018B1 RID: 6321 RVA: 0x00086185 File Offset: 0x00084385
	public void SetSortOrder(int v)
	{
		this.m_sortOrder = v;
	}

	// Token: 0x060018B2 RID: 6322 RVA: 0x00086190 File Offset: 0x00084390
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
		if (name == "CHANGE_TYPE")
		{
			return this.m_changeType;
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return this.m_sortOrder;
	}

	// Token: 0x060018B3 RID: 6323 RVA: 0x00086204 File Offset: 0x00084404
	public override void SetVar(string name, object val)
	{
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (!(name == "TAG_ID"))
		{
			if (!(name == "CHANGE_TYPE"))
			{
				if (!(name == "SORT_ORDER"))
				{
					return;
				}
				this.m_sortOrder = (int)val;
			}
			else
			{
				if (val == null)
				{
					this.m_changeType = CardChange.ChangeType.INVALID;
					return;
				}
				if (val is CardChange.ChangeType || val is int)
				{
					this.m_changeType = (CardChange.ChangeType)val;
					return;
				}
				if (val is string)
				{
					this.m_changeType = CardChange.ParseChangeTypeValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_tagId = (int)val;
	}

	// Token: 0x060018B4 RID: 6324 RVA: 0x000862B0 File Offset: 0x000844B0
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
		if (name == "CHANGE_TYPE")
		{
			return typeof(CardChange.ChangeType);
		}
		if (!(name == "SORT_ORDER"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x060018B5 RID: 6325 RVA: 0x00086320 File Offset: 0x00084520
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardChangeDbfRecords loadRecords = new LoadCardChangeDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x060018B6 RID: 6326 RVA: 0x00086338 File Offset: 0x00084538
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardChangeDbfAsset cardChangeDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardChangeDbfAsset)) as CardChangeDbfAsset;
		if (cardChangeDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardChangeDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardChangeDbfAsset.Records.Count; i++)
		{
			cardChangeDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardChangeDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060018B7 RID: 6327 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060018B8 RID: 6328 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F5E RID: 3934
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000F5F RID: 3935
	[SerializeField]
	private int m_tagId;

	// Token: 0x04000F60 RID: 3936
	[SerializeField]
	private CardChange.ChangeType m_changeType = CardChange.ParseChangeTypeValue("Invalid");

	// Token: 0x04000F61 RID: 3937
	[SerializeField]
	private int m_sortOrder;
}
