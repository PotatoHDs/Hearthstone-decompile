using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class LoadRegionOverridesDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001EE9 RID: 7913 RVA: 0x0009BE24 File Offset: 0x0009A024
	public List<RegionOverridesDbfRecord> GetRecords()
	{
		RegionOverridesDbfAsset regionOverridesDbfAsset = this.assetBundleRequest.asset as RegionOverridesDbfAsset;
		if (regionOverridesDbfAsset != null)
		{
			for (int i = 0; i < regionOverridesDbfAsset.Records.Count; i++)
			{
				regionOverridesDbfAsset.Records[i].StripUnusedLocales();
			}
			return regionOverridesDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001EEA RID: 7914 RVA: 0x0009BE7A File Offset: 0x0009A07A
	public LoadRegionOverridesDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RegionOverridesDbfAsset));
	}

	// Token: 0x06001EEB RID: 7915 RVA: 0x0009BE9D File Offset: 0x0009A09D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040011C2 RID: 4546
	private AssetBundleRequest assetBundleRequest;
}
