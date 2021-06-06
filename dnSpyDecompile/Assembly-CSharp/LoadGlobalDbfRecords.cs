using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F2 RID: 498
public class LoadGlobalDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001BD2 RID: 7122 RVA: 0x000913D0 File Offset: 0x0008F5D0
	public List<GlobalDbfRecord> GetRecords()
	{
		GlobalDbfAsset globalDbfAsset = this.assetBundleRequest.asset as GlobalDbfAsset;
		if (globalDbfAsset != null)
		{
			for (int i = 0; i < globalDbfAsset.Records.Count; i++)
			{
				globalDbfAsset.Records[i].StripUnusedLocales();
			}
			return globalDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001BD3 RID: 7123 RVA: 0x00091426 File Offset: 0x0008F626
	public LoadGlobalDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GlobalDbfAsset));
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x00091449 File Offset: 0x0008F649
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010C0 RID: 4288
	private AssetBundleRequest assetBundleRequest;
}
