using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class LoadModularBundleDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001D30 RID: 7472 RVA: 0x0009605C File Offset: 0x0009425C
	public List<ModularBundleDbfRecord> GetRecords()
	{
		ModularBundleDbfAsset modularBundleDbfAsset = this.assetBundleRequest.asset as ModularBundleDbfAsset;
		if (modularBundleDbfAsset != null)
		{
			for (int i = 0; i < modularBundleDbfAsset.Records.Count; i++)
			{
				modularBundleDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001D31 RID: 7473 RVA: 0x000960B2 File Offset: 0x000942B2
	public LoadModularBundleDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleDbfAsset));
	}

	// Token: 0x06001D32 RID: 7474 RVA: 0x000960D5 File Offset: 0x000942D5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001132 RID: 4402
	private AssetBundleRequest assetBundleRequest;
}
