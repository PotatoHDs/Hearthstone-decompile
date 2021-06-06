using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200027F RID: 639
public class LoadSubsetRuleDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060020C7 RID: 8391 RVA: 0x000A1C34 File Offset: 0x0009FE34
	public List<SubsetRuleDbfRecord> GetRecords()
	{
		SubsetRuleDbfAsset subsetRuleDbfAsset = this.assetBundleRequest.asset as SubsetRuleDbfAsset;
		if (subsetRuleDbfAsset != null)
		{
			for (int i = 0; i < subsetRuleDbfAsset.Records.Count; i++)
			{
				subsetRuleDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetRuleDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060020C8 RID: 8392 RVA: 0x000A1C8A File Offset: 0x0009FE8A
	public LoadSubsetRuleDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetRuleDbfAsset));
	}

	// Token: 0x060020C9 RID: 8393 RVA: 0x000A1CAD File Offset: 0x0009FEAD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001254 RID: 4692
	private AssetBundleRequest assetBundleRequest;
}
