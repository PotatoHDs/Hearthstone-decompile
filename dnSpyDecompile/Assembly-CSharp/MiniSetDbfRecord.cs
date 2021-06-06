using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000217 RID: 535
[Serializable]
public class MiniSetDbfRecord : DbfRecord
{
	// Token: 0x1700036F RID: 879
	// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00095E5A File Offset: 0x0009405A
	[DbfField("DECK_ID")]
	public int DeckId
	{
		get
		{
			return this.m_deckId;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00095E62 File Offset: 0x00094062
	public DeckDbfRecord DeckRecord
	{
		get
		{
			return GameDbf.Deck.GetRecord(this.m_deckId);
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00095E74 File Offset: 0x00094074
	[DbfField("BOOSTER_ID")]
	public int BoosterId
	{
		get
		{
			return this.m_boosterId;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00095E7C File Offset: 0x0009407C
	public BoosterDbfRecord BoosterRecord
	{
		get
		{
			return GameDbf.Booster.GetRecord(this.m_boosterId);
		}
	}

	// Token: 0x06001D25 RID: 7461 RVA: 0x00095E8E File Offset: 0x0009408E
	public void SetDeckId(int v)
	{
		this.m_deckId = v;
	}

	// Token: 0x06001D26 RID: 7462 RVA: 0x00095E97 File Offset: 0x00094097
	public void SetBoosterId(int v)
	{
		this.m_boosterId = v;
	}

	// Token: 0x06001D27 RID: 7463 RVA: 0x00095EA0 File Offset: 0x000940A0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "DECK_ID")
		{
			return this.m_deckId;
		}
		if (!(name == "BOOSTER_ID"))
		{
			return null;
		}
		return this.m_boosterId;
	}

	// Token: 0x06001D28 RID: 7464 RVA: 0x00095EFC File Offset: 0x000940FC
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "DECK_ID")
		{
			this.m_deckId = (int)val;
			return;
		}
		if (!(name == "BOOSTER_ID"))
		{
			return;
		}
		this.m_boosterId = (int)val;
	}

	// Token: 0x06001D29 RID: 7465 RVA: 0x00095F58 File Offset: 0x00094158
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "DECK_ID")
		{
			return typeof(int);
		}
		if (!(name == "BOOSTER_ID"))
		{
			return null;
		}
		return typeof(int);
	}

	// Token: 0x06001D2A RID: 7466 RVA: 0x00095FB0 File Offset: 0x000941B0
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadMiniSetDbfRecords loadRecords = new LoadMiniSetDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001D2B RID: 7467 RVA: 0x00095FC8 File Offset: 0x000941C8
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		MiniSetDbfAsset miniSetDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(MiniSetDbfAsset)) as MiniSetDbfAsset;
		if (miniSetDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("MiniSetDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < miniSetDbfAsset.Records.Count; i++)
		{
			miniSetDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (miniSetDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001D2C RID: 7468 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001D2D RID: 7469 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x0400112F RID: 4399
	[SerializeField]
	private int m_deckId;

	// Token: 0x04001130 RID: 4400
	[SerializeField]
	private int m_boosterId;
}
