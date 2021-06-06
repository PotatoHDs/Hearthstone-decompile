using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class LoadSubsetCardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060020A6 RID: 8358 RVA: 0x000A18A8 File Offset: 0x0009FAA8
	public List<SubsetCardDbfRecord> GetRecords()
	{
		SubsetCardDbfAsset subsetCardDbfAsset = this.assetBundleRequest.asset as SubsetCardDbfAsset;
		if (subsetCardDbfAsset != null)
		{
			for (int i = 0; i < subsetCardDbfAsset.Records.Count; i++)
			{
				subsetCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetCardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x000A18FE File Offset: 0x0009FAFE
	public LoadSubsetCardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetCardDbfAsset));
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x000A1921 File Offset: 0x0009FB21
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400124E RID: 4686
	private AssetBundleRequest assetBundleRequest;
}
