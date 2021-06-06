using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000222 RID: 546
public class LoadMultiClassGroupDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001DA6 RID: 7590 RVA: 0x00097ECC File Offset: 0x000960CC
	public List<MultiClassGroupDbfRecord> GetRecords()
	{
		MultiClassGroupDbfAsset multiClassGroupDbfAsset = this.assetBundleRequest.asset as MultiClassGroupDbfAsset;
		if (multiClassGroupDbfAsset != null)
		{
			for (int i = 0; i < multiClassGroupDbfAsset.Records.Count; i++)
			{
				multiClassGroupDbfAsset.Records[i].StripUnusedLocales();
			}
			return multiClassGroupDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x00097F22 File Offset: 0x00096122
	public LoadMultiClassGroupDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MultiClassGroupDbfAsset));
	}

	// Token: 0x06001DA8 RID: 7592 RVA: 0x00097F45 File Offset: 0x00096145
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400115F RID: 4447
	private AssetBundleRequest assetBundleRequest;
}
