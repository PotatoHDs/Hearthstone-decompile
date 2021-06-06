using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200027C RID: 636
public class LoadSubsetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060020B7 RID: 8375 RVA: 0x000A1AB0 File Offset: 0x0009FCB0
	public List<SubsetDbfRecord> GetRecords()
	{
		SubsetDbfAsset subsetDbfAsset = this.assetBundleRequest.asset as SubsetDbfAsset;
		if (subsetDbfAsset != null)
		{
			for (int i = 0; i < subsetDbfAsset.Records.Count; i++)
			{
				subsetDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060020B8 RID: 8376 RVA: 0x000A1B06 File Offset: 0x0009FD06
	public LoadSubsetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetDbfAsset));
	}

	// Token: 0x060020B9 RID: 8377 RVA: 0x000A1B29 File Offset: 0x0009FD29
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001252 RID: 4690
	private AssetBundleRequest assetBundleRequest;
}
