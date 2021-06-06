using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C8 RID: 456
[Serializable]
public class DeckCardDbfRecord : DbfRecord
{
	// Token: 0x1700029D RID: 669
	// (get) Token: 0x06001A4E RID: 6734 RVA: 0x0008B236 File Offset: 0x00089436
	[DbfField("NEXT_CARD")]
	public int NextCard
	{
		get
		{
			return this.m_nextCardId;
		}
	}

	// Token: 0x1700029E RID: 670
	// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0008B23E File Offset: 0x0008943E
	public DeckCardDbfRecord NextCardRecord
	{
		get
		{
			return GameDbf.DeckCard.GetRecord(this.m_nextCardId);
		}
	}

	// Token: 0x1700029F RID: 671
	// (get) Token: 0x06001A50 RID: 6736 RVA: 0x0008B250 File Offset: 0x00089450
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x170002A0 RID: 672
	// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0008B258 File Offset: 0x00089458
	public CardDbfRecord CardRecord
	{
		get
		{
			return GameDbf.Card.GetRecord(this.m_cardId);
		}
	}

	// Token: 0x170002A1 RID: 673
	// (get) Token: 0x06001A52 RID: 6738 RVA: 0x0008B26A File Offset: 0x0008946A
	[DbfField("DECK_ID")]
	public int DeckId
	{
		get
		{
			return this.m_deckId;
		}
	}

	// Token: 0x170002A2 RID: 674
	// (get) Token: 0x06001A53 RID: 6739 RVA: 0x0008B272 File Offset: 0x00089472
	[DbfField("DESCRIPTION")]
	public DbfLocValue Description
	{
		get
		{
			return this.m_description;
		}
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x0008B27A File Offset: 0x0008947A
	public void SetNextCard(int v)
	{
		this.m_nextCardId = v;
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x0008B283 File Offset: 0x00089483
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x0008B28C File Offset: 0x0008948C
	public void SetDeckId(int v)
	{
		this.m_deckId = v;
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x0008B295 File Offset: 0x00089495
	public void SetDescription(DbfLocValue v)
	{
		this.m_description = v;
		v.SetDebugInfo(base.ID, "DESCRIPTION");
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x0008B2B0 File Offset: 0x000894B0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "NEXT_CARD")
		{
			return this.m_nextCardId;
		}
		if (name == "CARD_ID")
		{
			return this.m_cardId;
		}
		if (name == "DECK_ID")
		{
			return this.m_deckId;
		}
		if (!(name == "DESCRIPTION"))
		{
			return null;
		}
		return this.m_description;
	}

	// Token: 0x06001A59 RID: 6745 RVA: 0x0008B338 File Offset: 0x00089538
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "NEXT_CARD")
		{
			this.m_nextCardId = (int)val;
			return;
		}
		if (name == "CARD_ID")
		{
			this.m_cardId = (int)val;
			return;
		}
		if (name == "DECK_ID")
		{
			this.m_deckId = (int)val;
			return;
		}
		if (!(name == "DESCRIPTION"))
		{
			return;
		}
		this.m_description = (DbfLocValue)val;
	}

	// Token: 0x06001A5A RID: 6746 RVA: 0x0008B3C8 File Offset: 0x000895C8
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "NEXT_CARD")
		{
			return typeof(int);
		}
		if (name == "CARD_ID")
		{
			return typeof(int);
		}
		if (name == "DECK_ID")
		{
			return typeof(int);
		}
		if (!(name == "DESCRIPTION"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001A5B RID: 6747 RVA: 0x0008B450 File Offset: 0x00089650
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadDeckCardDbfRecords loadRecords = new LoadDeckCardDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001A5C RID: 6748 RVA: 0x0008B468 File Offset: 0x00089668
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		DeckCardDbfAsset deckCardDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(DeckCardDbfAsset)) as DeckCardDbfAsset;
		if (deckCardDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("DeckCardDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < deckCardDbfAsset.Records.Count; i++)
		{
			deckCardDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (deckCardDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001A5D RID: 6749 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001A5E RID: 6750 RVA: 0x0008B4E7 File Offset: 0x000896E7
	public override void StripUnusedLocales()
	{
		this.m_description.StripUnusedLocales();
	}

	// Token: 0x04000FE1 RID: 4065
	[SerializeField]
	private int m_nextCardId;

	// Token: 0x04000FE2 RID: 4066
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000FE3 RID: 4067
	[SerializeField]
	private int m_deckId;

	// Token: 0x04000FE4 RID: 4068
	[SerializeField]
	private DbfLocValue m_description;
}
