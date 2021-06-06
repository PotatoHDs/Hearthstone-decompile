using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200019B RID: 411
[Serializable]
public class CardDiscoverStringDbfRecord : DbfRecord
{
	// Token: 0x1700024B RID: 587
	// (get) Token: 0x060018FE RID: 6398 RVA: 0x0008748A File Offset: 0x0008568A
	[DbfField("NOTE_MINI_GUID")]
	public string NoteMiniGuid
	{
		get
		{
			return this.m_noteMiniGuid;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x060018FF RID: 6399 RVA: 0x00087492 File Offset: 0x00085692
	[DbfField("STRING_ID")]
	public string StringId
	{
		get
		{
			return this.m_stringId;
		}
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x0008749A File Offset: 0x0008569A
	public void SetNoteMiniGuid(string v)
	{
		this.m_noteMiniGuid = v;
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x000874A3 File Offset: 0x000856A3
	public void SetStringId(string v)
	{
		this.m_stringId = v;
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x000874AC File Offset: 0x000856AC
	public override object GetVar(string name)
	{
		if (name == "NOTE_MINI_GUID")
		{
			return this.m_noteMiniGuid;
		}
		if (!(name == "STRING_ID"))
		{
			return null;
		}
		return this.m_stringId;
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x000874D9 File Offset: 0x000856D9
	public override void SetVar(string name, object val)
	{
		if (name == "NOTE_MINI_GUID")
		{
			this.m_noteMiniGuid = (string)val;
			return;
		}
		if (!(name == "STRING_ID"))
		{
			return;
		}
		this.m_stringId = (string)val;
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x0008750F File Offset: 0x0008570F
	public override Type GetVarType(string name)
	{
		if (name == "NOTE_MINI_GUID")
		{
			return typeof(string);
		}
		if (!(name == "STRING_ID"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001905 RID: 6405 RVA: 0x00087544 File Offset: 0x00085744
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardDiscoverStringDbfRecords loadRecords = new LoadCardDiscoverStringDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001906 RID: 6406 RVA: 0x0008755C File Offset: 0x0008575C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardDiscoverStringDbfAsset cardDiscoverStringDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardDiscoverStringDbfAsset)) as CardDiscoverStringDbfAsset;
		if (cardDiscoverStringDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardDiscoverStringDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardDiscoverStringDbfAsset.Records.Count; i++)
		{
			cardDiscoverStringDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardDiscoverStringDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04000F7A RID: 3962
	[SerializeField]
	private string m_noteMiniGuid;

	// Token: 0x04000F7B RID: 3963
	[SerializeField]
	private string m_stringId;
}
