using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200024A RID: 586
[Serializable]
public class RegionOverridesDbfRecord : DbfRecord
{
	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0009BEAA File Offset: 0x0009A0AA
	[DbfField("EXTERNAL_URL_ID")]
	public int ExternalUrlId
	{
		get
		{
			return this.m_externalUrlId;
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x06001EED RID: 7917 RVA: 0x0009BEB2 File Offset: 0x0009A0B2
	[DbfField("REGION")]
	public string Region
	{
		get
		{
			return this.m_region;
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0009BEBA File Offset: 0x0009A0BA
	[DbfField("OVERRIDE_URL")]
	public string OverrideUrl
	{
		get
		{
			return this.m_overrideUrl;
		}
	}

	// Token: 0x06001EEF RID: 7919 RVA: 0x0009BEC2 File Offset: 0x0009A0C2
	public void SetExternalUrlId(int v)
	{
		this.m_externalUrlId = v;
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x0009BECB File Offset: 0x0009A0CB
	public void SetRegion(string v)
	{
		this.m_region = v;
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x0009BED4 File Offset: 0x0009A0D4
	public void SetOverrideUrl(string v)
	{
		this.m_overrideUrl = v;
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x0009BEE0 File Offset: 0x0009A0E0
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "EXTERNAL_URL_ID")
		{
			return this.m_externalUrlId;
		}
		if (name == "REGION")
		{
			return this.m_region;
		}
		if (!(name == "OVERRIDE_URL"))
		{
			return null;
		}
		return this.m_overrideUrl;
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x0009BF4C File Offset: 0x0009A14C
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (name == "EXTERNAL_URL_ID")
		{
			this.m_externalUrlId = (int)val;
			return;
		}
		if (name == "REGION")
		{
			this.m_region = (string)val;
			return;
		}
		if (!(name == "OVERRIDE_URL"))
		{
			return;
		}
		this.m_overrideUrl = (string)val;
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x0009BFC4 File Offset: 0x0009A1C4
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "EXTERNAL_URL_ID")
		{
			return typeof(int);
		}
		if (name == "REGION")
		{
			return typeof(string);
		}
		if (!(name == "OVERRIDE_URL"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x0009C034 File Offset: 0x0009A234
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadRegionOverridesDbfRecords loadRecords = new LoadRegionOverridesDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001EF6 RID: 7926 RVA: 0x0009C04C File Offset: 0x0009A24C
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		RegionOverridesDbfAsset regionOverridesDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(RegionOverridesDbfAsset)) as RegionOverridesDbfAsset;
		if (regionOverridesDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("RegionOverridesDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < regionOverridesDbfAsset.Records.Count; i++)
		{
			regionOverridesDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (regionOverridesDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001EF7 RID: 7927 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001EF8 RID: 7928 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x040011C3 RID: 4547
	[SerializeField]
	private int m_externalUrlId;

	// Token: 0x040011C4 RID: 4548
	[SerializeField]
	private string m_region;

	// Token: 0x040011C5 RID: 4549
	[SerializeField]
	private string m_overrideUrl;
}
