using System;
using System.Collections.Generic;
using Assets;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001DD RID: 477
[Serializable]
public class ExternalUrlDbfRecord : DbfRecord
{
	// Token: 0x170002D0 RID: 720
	// (get) Token: 0x06001AFF RID: 6911 RVA: 0x0008D4B2 File Offset: 0x0008B6B2
	[DbfField("NOTE_DESC")]
	public string NoteDesc
	{
		get
		{
			return this.m_noteDesc;
		}
	}

	// Token: 0x170002D1 RID: 721
	// (get) Token: 0x06001B00 RID: 6912 RVA: 0x0008D4BA File Offset: 0x0008B6BA
	[DbfField("ASSET_FLAGS")]
	public ExternalUrl.AssetFlags AssetFlags
	{
		get
		{
			return this.m_assetFlags;
		}
	}

	// Token: 0x170002D2 RID: 722
	// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0008D4C2 File Offset: 0x0008B6C2
	[DbfField("ENDPOINT")]
	public ExternalUrl.Endpoint Endpoint
	{
		get
		{
			return this.m_endpoint;
		}
	}

	// Token: 0x170002D3 RID: 723
	// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0008D4CA File Offset: 0x0008B6CA
	[DbfField("GLOBAL_URL")]
	public string GlobalUrl
	{
		get
		{
			return this.m_globalUrl;
		}
	}

	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0008D4D2 File Offset: 0x0008B6D2
	public List<RegionOverridesDbfRecord> RegionOverrides
	{
		get
		{
			return GameDbf.RegionOverrides.GetRecords((RegionOverridesDbfRecord r) => r.ExternalUrlId == base.ID, -1);
		}
	}

	// Token: 0x06001B04 RID: 6916 RVA: 0x0008D4EB File Offset: 0x0008B6EB
	public void SetNoteDesc(string v)
	{
		this.m_noteDesc = v;
	}

	// Token: 0x06001B05 RID: 6917 RVA: 0x0008D4F4 File Offset: 0x0008B6F4
	public void SetAssetFlags(ExternalUrl.AssetFlags v)
	{
		this.m_assetFlags = v;
	}

	// Token: 0x06001B06 RID: 6918 RVA: 0x0008D4FD File Offset: 0x0008B6FD
	public void SetEndpoint(ExternalUrl.Endpoint v)
	{
		this.m_endpoint = v;
	}

	// Token: 0x06001B07 RID: 6919 RVA: 0x0008D506 File Offset: 0x0008B706
	public void SetGlobalUrl(string v)
	{
		this.m_globalUrl = v;
	}

	// Token: 0x06001B08 RID: 6920 RVA: 0x0008D510 File Offset: 0x0008B710
	public override object GetVar(string name)
	{
		if (name == "ID")
		{
			return base.ID;
		}
		if (name == "NOTE_DESC")
		{
			return this.m_noteDesc;
		}
		if (name == "ASSET_FLAGS")
		{
			return this.m_assetFlags;
		}
		if (name == "ENDPOINT")
		{
			return this.m_endpoint;
		}
		if (!(name == "GLOBAL_URL"))
		{
			return null;
		}
		return this.m_globalUrl;
	}

	// Token: 0x06001B09 RID: 6921 RVA: 0x0008D594 File Offset: 0x0008B794
	public override void SetVar(string name, object val)
	{
		if (name == "ID")
		{
			base.SetID((int)val);
			return;
		}
		if (!(name == "NOTE_DESC"))
		{
			if (!(name == "ASSET_FLAGS"))
			{
				if (!(name == "ENDPOINT"))
				{
					if (!(name == "GLOBAL_URL"))
					{
						return;
					}
					this.m_globalUrl = (string)val;
				}
				else
				{
					if (val == null)
					{
						this.m_endpoint = ExternalUrl.Endpoint.ACCOUNT;
						return;
					}
					if (val is ExternalUrl.Endpoint || val is int)
					{
						this.m_endpoint = (ExternalUrl.Endpoint)val;
						return;
					}
					if (val is string)
					{
						this.m_endpoint = ExternalUrl.ParseEndpointValue((string)val);
						return;
					}
				}
			}
			else
			{
				if (val == null)
				{
					this.m_assetFlags = ExternalUrl.AssetFlags.NONE;
					return;
				}
				if (val is ExternalUrl.AssetFlags || val is int)
				{
					this.m_assetFlags = (ExternalUrl.AssetFlags)val;
					return;
				}
				if (val is string)
				{
					this.m_assetFlags = ExternalUrl.ParseAssetFlagsValue((string)val);
					return;
				}
			}
			return;
		}
		this.m_noteDesc = (string)val;
	}

	// Token: 0x06001B0A RID: 6922 RVA: 0x0008D690 File Offset: 0x0008B890
	public override Type GetVarType(string name)
	{
		if (name == "ID")
		{
			return typeof(int);
		}
		if (name == "NOTE_DESC")
		{
			return typeof(string);
		}
		if (name == "ASSET_FLAGS")
		{
			return typeof(ExternalUrl.AssetFlags);
		}
		if (name == "ENDPOINT")
		{
			return typeof(ExternalUrl.Endpoint);
		}
		if (!(name == "GLOBAL_URL"))
		{
			return null;
		}
		return typeof(string);
	}

	// Token: 0x06001B0B RID: 6923 RVA: 0x0008D718 File Offset: 0x0008B918
	public override IEnumerator<IAsyncJobResult> Job_LoadRecordsFromAssetAsync<T>(string resourcePath, Action<List<T>> resultHandler)
	{
		LoadExternalUrlDbfRecords loadRecords = new LoadExternalUrlDbfRecords(resourcePath);
		yield return loadRecords;
		if (resultHandler != null)
		{
			resultHandler(loadRecords.GetRecords() as List<T>);
		}
		yield break;
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x0008D730 File Offset: 0x0008B930
	public override bool LoadRecordsFromAsset<T>(string resourcePath, out List<T> records)
	{
		ExternalUrlDbfAsset externalUrlDbfAsset = DbfShared.GetAssetBundle().LoadAsset(resourcePath, typeof(ExternalUrlDbfAsset)) as ExternalUrlDbfAsset;
		if (externalUrlDbfAsset == null)
		{
			records = new List<T>();
			Debug.LogError(string.Format("ExternalUrlDbfAsset.LoadRecordsFromAsset() - failed to load records from assetbundle: {0}", resourcePath));
			return false;
		}
		for (int i = 0; i < externalUrlDbfAsset.Records.Count; i++)
		{
			externalUrlDbfAsset.Records[i].StripUnusedLocales();
		}
		records = (externalUrlDbfAsset.Records as List<T>);
		return true;
	}

	// Token: 0x06001B0D RID: 6925 RVA: 0x0001FA65 File Offset: 0x0001DC65
	public override bool SaveRecordsToAsset<T>(string assetPath, List<T> records)
	{
		return false;
	}

	// Token: 0x06001B0E RID: 6926 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public override void StripUnusedLocales()
	{
	}

	// Token: 0x04001016 RID: 4118
	[SerializeField]
	private string m_noteDesc;

	// Token: 0x04001017 RID: 4119
	[SerializeField]
	private ExternalUrl.AssetFlags m_assetFlags;

	// Token: 0x04001018 RID: 4120
	[SerializeField]
	private ExternalUrl.Endpoint m_endpoint;

	// Token: 0x04001019 RID: 4121
	[SerializeField]
	private string m_globalUrl = "False";
}
