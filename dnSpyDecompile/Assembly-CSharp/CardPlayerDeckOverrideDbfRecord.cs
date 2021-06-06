using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A1 RID: 417
[Serializable]
public class CardPlayerDeckOverrideDbfRecord : DbfRecord
{
	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06001931 RID: 6449 RVA: 0x00087F7E File Offset: 0x0008617E
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06001932 RID: 6450 RVA: 0x00087F86 File Offset: 0x00086186
	[DbfField("HERO_CARD_ID")]
	public int HeroCardId
	{
		get
		{
			return this.m_heroCardId;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06001933 RID: 6451 RVA: 0x00087F8E File Offset: 0x0008618E
	public CardDbfRecord HeroCardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_heroCardId);
		}
	}

	// Token: 0x1700025C RID: 604
	// (get) Token: 0x06001934 RID: 6452 RVA: 0x00087FA0 File Offset: 0x000861A0
	[DbfField("DECK_NAME")]
	public DbfLocValue DeckName
	{
		get
		{
			return this.m_deckName;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x06001935 RID: 6453 RVA: 0x00087FA8 File Offset: 0x000861A8
	[DbfField("ADD_TO_DECK_WARNING_HEADER")]
	public DbfLocValue AddToDeckWarningHeader
	{
		get
		{
			return this.m_addToDeckWarningHeader;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06001936 RID: 6454 RVA: 0x00087FB0 File Offset: 0x000861B0
	[DbfField("ADD_TO_DECK_WARNING_BODY")]
	public DbfLocValue AddToDeckWarningBody
	{
		get
		{
			return this.m_addToDeckWarningBody;
		}
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x00087FB8 File Offset: 0x000861B8
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x00087FC1 File Offset: 0x000861C1
	public void SetHeroCardId(int v)
	{
		this.m_heroCardId = v;
	}

	// Token: 0x06001939 RID: 6457 RVA: 0x00087FCA File Offset: 0x000861CA
	public void SetDeckName(DbfLocValue v)
	{
		this.m_deckName = v;
		v.SetDebugInfo(base.ID, "DECK_NAME");
	}

	// Token: 0x0600193A RID: 6458 RVA: 0x00087FE4 File Offset: 0x000861E4
	public void SetAddToDeckWarningHeader(DbfLocValue v)
	{
		this.m_addToDeckWarningHeader = v;
		v.SetDebugInfo(base.ID, "ADD_TO_DECK_WARNING_HEADER");
	}

	// Token: 0x0600193B RID: 6459 RVA: 0x00087FFE File Offset: 0x000861FE
	public void SetAddToDeckWarningBody(DbfLocValue v)
	{
		this.m_addToDeckWarningBody = v;
		v.SetDebugInfo(base.ID, "ADD_TO_DECK_WARNING_BODY");
	}

	// Token: 0x0600193C RID: 6460 RVA: 0x00088018 File Offset: 0x00086218
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "HERO_CARD_ID")
		{
			return this.m_heroCardId;
		}
		if (name == "DECK_NAME")
		{
			return this.m_deckName;
		}
		if (name == "ADD_TO_DECK_WARNING_HEADER")
		{
			return this.m_addToDeckWarningHeader;
		}
		if (!(name == "ADD_TO_DECK_WARNING_BODY"))
		{
			return null;
		}
		return this.m_addToDeckWarningBody;
	}

	// Token: 0x0600193D RID: 6461 RVA: 0x000880B0 File Offset: 0x000862B0
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (name == "HERO_CARD_ID")
		{
			this.m_heroCardId = (int)val;
			return;
		}
		if (name == "DECK_NAME")
		{
			this.m_deckName = (DbfLocValue)val;
			return;
		}
		if (name == "ADD_TO_DECK_WARNING_HEADER")
		{
			this.m_addToDeckWarningHeader = (DbfLocValue)val;
			return;
		}
		if (!(name == "ADD_TO_DECK_WARNING_BODY"))
		{
			return;
		}
		this.m_addToDeckWarningBody = (DbfLocValue)val;
	}

	// Token: 0x0600193E RID: 6462 RVA: 0x0008815C File Offset: 0x0008635C
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "HERO_CARD_ID")
		{
			return typeof(int);
		}
		if (name == "DECK_NAME")
		{
			return typeof(DbfLocValue);
		}
		if (name == "ADD_TO_DECK_WARNING_HEADER")
		{
			return typeof(DbfLocValue);
		}
		if (!(name == "ADD_TO_DECK_WARNING_BODY"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x0600193F RID: 6463 RVA: 0x000881FC File Offset: 0x000863FC
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardPlayerDeckOverrideDbfRecords loadRecords = new LoadCardPlayerDeckOverrideDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001940 RID: 6464 RVA: 0x00088214 File Offset: 0x00086414
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardPlayerDeckOverrideDbfAsset cardPlayerDeckOverrideDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardPlayerDeckOverrideDbfAsset)) as CardPlayerDeckOverrideDbfAsset;
		if (cardPlayerDeckOverrideDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardPlayerDeckOverrideDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardPlayerDeckOverrideDbfAsset.Records.Count; i++)
		{
			cardPlayerDeckOverrideDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardPlayerDeckOverrideDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001941 RID: 6465 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001942 RID: 6466 RVA: 0x00088293 File Offset: 0x00086493
	public override void StripUnusedLocales()
	{
		this.m_deckName.StripUnusedLocales();
		this.m_addToDeckWarningHeader.StripUnusedLocales();
		this.m_addToDeckWarningBody.StripUnusedLocales();
	}

	// Token: 0x04000F8B RID: 3979
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000F8C RID: 3980
	[SerializeField]
	private int m_heroCardId;

	// Token: 0x04000F8D RID: 3981
	[SerializeField]
	private DbfLocValue m_deckName;

	// Token: 0x04000F8E RID: 3982
	[SerializeField]
	private DbfLocValue m_addToDeckWarningHeader;

	// Token: 0x04000F8F RID: 3983
	[SerializeField]
	private DbfLocValue m_addToDeckWarningBody;
}
