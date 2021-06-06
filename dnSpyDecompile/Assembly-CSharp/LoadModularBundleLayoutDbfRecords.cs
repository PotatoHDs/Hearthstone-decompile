using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200021C RID: 540
public class LoadModularBundleLayoutDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001D50 RID: 7504 RVA: 0x0009683C File Offset: 0x00094A3C
	public List<ModularBundleLayoutDbfRecord> GetRecords()
	{
		ModularBundleLayoutDbfAsset modularBundleLayoutDbfAsset = this.assetBundleRequest.asset as ModularBundleLayoutDbfAsset;
		if (modularBundleLayoutDbfAsset != null)
		{
			for (int i = 0; i < modularBundleLayoutDbfAsset.Records.Count; i++)
			{
				modularBundleLayoutDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleLayoutDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001D51 RID: 7505 RVA: 0x00096892 File Offset: 0x00094A92
	public LoadModularBundleLayoutDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleLayoutDbfAsset));
	}

	// Token: 0x06001D52 RID: 7506 RVA: 0x000968B5 File Offset: 0x00094AB5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400113E RID: 4414
	private AssetBundleRequest assetBundleRequest;
}
