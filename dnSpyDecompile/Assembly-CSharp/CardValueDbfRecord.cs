using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001B0 RID: 432
[Serializable]
public class CardValueDbfRecord : DbfRecord
{
	// Token: 0x060019A9 RID: 6569 RVA: 0x0008951C File Offset: 0x0008771C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		CardValueDbfAsset cardValueDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(CardValueDbfAsset)) as CardValueDbfAsset;
		if (cardValueDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("CardValueDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < cardValueDbfAsset.Records.Count; i++)
		{
			cardValueDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (cardValueDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x060019AA RID: 6570 RVA: 0x0008959B File Offset: 0x0008779B
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadCardValueDbfRecords loadRecords = new LoadCardValueDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.Records as List<T>);
		}
		yield break;
	}

	// Token: 0x060019AB RID: 6571 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x17000279 RID: 633
	// (get) Token: 0x060019AD RID: 6573 RVA: 0x000895B1 File Offset: 0x000877B1
	[DbfField("OVERRIDE_EVENT")]
	public string OverrideEvent
	{
		get
		{
			return this.m_OverrideEvent;
		}
	}

	// Token: 0x060019AE RID: 6574 RVA: 0x000895B9 File Offset: 0x000877B9
	public void SetOverrideEvent(string v)
	{
		this.m_OverrideEvent = v;
	}

	// Token: 0x060019AF RID: 6575 RVA: 0x000895C2 File Offset: 0x000877C2
	public override object GetVar(string name)
	{
		if (name == "OVERRIDE_EVENT")
		{
			return this.OverrideEvent;
		}
		return null;
	}

	// Token: 0x060019B0 RID: 6576 RVA: 0x000895D9 File Offset: 0x000877D9
	public override void SetVar(string name, object val)
	{
		if (name == "OVERRIDE_EVENT")
		{
			this.SetOverrideEvent((string)val);
		}
	}

	// Token: 0x060019B1 RID: 6577 RVA: 0x000895F4 File Offset: 0x000877F4
	public override Type GetVarType(string name)
	{
		if (name == "OVERRIDE_EVENT")
		{
			return typeof(string);
		}
		return null;
	}

	// Token: 0x04000FB1 RID: 4017
	[SerializeField]
	private string m_OverrideEvent;
}
