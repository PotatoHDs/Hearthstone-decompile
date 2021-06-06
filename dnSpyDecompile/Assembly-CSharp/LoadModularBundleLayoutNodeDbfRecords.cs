using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200021F RID: 543
public class LoadModularBundleLayoutNodeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001D78 RID: 7544 RVA: 0x00097200 File Offset: 0x00095400
	public List<ModularBundleLayoutNodeDbfRecord> GetRecords()
	{
		ModularBundleLayoutNodeDbfAsset modularBundleLayoutNodeDbfAsset = this.assetBundleRequest.asset as ModularBundleLayoutNodeDbfAsset;
		if (modularBundleLayoutNodeDbfAsset != null)
		{
			for (int i = 0; i < modularBundleLayoutNodeDbfAsset.Records.Count; i++)
			{
				modularBundleLayoutNodeDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleLayoutNodeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001D79 RID: 7545 RVA: 0x00097256 File Offset: 0x00095456
	public LoadModularBundleLayoutNodeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleLayoutNodeDbfAsset));
	}

	// Token: 0x06001D7A RID: 7546 RVA: 0x00097279 File Offset: 0x00095479
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400114C RID: 4428
	private AssetBundleRequest assetBundleRequest;
}
