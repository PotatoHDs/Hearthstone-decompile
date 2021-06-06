using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000282 RID: 642
public class LoadSubsetTagDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060020DF RID: 8415 RVA: 0x000A21A4 File Offset: 0x000A03A4
	public List<SubsetTagDbfRecord> GetRecords()
	{
		SubsetTagDbfAsset subsetTagDbfAsset = this.assetBundleRequest.asset as SubsetTagDbfAsset;
		if (subsetTagDbfAsset != null)
		{
			for (int i = 0; i < subsetTagDbfAsset.Records.Count; i++)
			{
				subsetTagDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetTagDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x000A21FA File Offset: 0x000A03FA
	public LoadSubsetTagDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetTagDbfAsset));
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x000A221D File Offset: 0x000A041D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400125C RID: 4700
	private AssetBundleRequest assetBundleRequest;
}
