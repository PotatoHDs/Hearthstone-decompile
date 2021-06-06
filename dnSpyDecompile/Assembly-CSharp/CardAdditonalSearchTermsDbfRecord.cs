using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200018F RID: 399
[Serializable]
public class CardAdditonalSearchTermsDbfRecord : DbfRecord
{
	// Token: 0x17000221 RID: 545
	// (get) Token: 0x0600187A RID: 6266 RVA: 0x00085696 File Offset: 0x00083896
	[DbfField("CARD_ID")]
	public int CardId
	{
		get
		{
			return this.m_cardId;
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x0008569E File Offset: 0x0008389E
	[DbfField("SEARCH_TERM")]
	public DbfLocValue SearchTerm
	{
		get
		{
			return this.m_searchTerm;
		}
	}

	// Token: 0x0600187C RID: 6268 RVA: 0x000856A6 File Offset: 0x000838A6
	public void SetCardId(int v)
	{
		this.m_cardId = v;
	}

	// Token: 0x0600187D RID: 6269 RVA: 0x000856AF File Offset: 0x000838AF
	public void SetSearchTerm(DbfLocValue v)
	{
		this.m_searchTerm = v;
		v.SetDebugInfo(base.ID, "SEARCH_TERM");
	}

	// Token: 0x0600187E RID: 6270 RVA: 0x000856CC File Offset: 0x000838CC
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
		if (!(name == "SEARCH_TERM"))
		{
			return null;
		}
		return this.m_searchTerm;
	}

	// Token: 0x0600187F RID: 6271 RVA: 0x00085724 File Offset: 0x00083924
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
		if (!(name == "SEARCH_TERM"))
		{
			return;
		}
		this.m_searchTerm = (DbfLocValue)val;
	}

	// Token: 0x06001880 RID: 6272 RVA: 0x00085780 File Offset: 0x00083980
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
		if (!(name == "SEARCH_TERM"))
		{
			return null;
		}
		return typeof(DbfLocValue);
	}

	// Token: 0x06001881 RID: 6273 RVA: 0x000857D8 File Offset: 0x000839D8
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardAdditonalSearchTermsDbfRecords loadRecords = new LoadCardAdditonalSearchTermsDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001882 RID: 6274 RVA: 0x000857F0 File Offset: 0x000839F0
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardAdditonalSearchTermsDbfAsset cardAdditonalSearchTermsDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardAdditonalSearchTermsDbfAsset)) as CardAdditonalSearchTermsDbfAsset;
		if (cardAdditonalSearchTermsDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardAdditonalSearchTermsDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardAdditonalSearchTermsDbfAsset.Records.Count; i++)
		{
			cardAdditonalSearchTermsDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardAdditonalSearchTermsDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001883 RID: 6275 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001884 RID: 6276 RVA: 0x0008586F File Offset: 0x00083A6F
	public override void StripUnusedLocales()
	{
		this.m_searchTerm.StripUnusedLocales();
	}

	// Token: 0x04000F4E RID: 3918
	[SerializeField]
	private int m_cardId;

	// Token: 0x04000F4F RID: 3919
	[SerializeField]
	private DbfLocValue m_searchTerm;
}
