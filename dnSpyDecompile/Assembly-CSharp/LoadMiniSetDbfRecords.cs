using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000216 RID: 534
public class LoadMiniSetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001D1E RID: 7454 RVA: 0x00095DD4 File Offset: 0x00093FD4
	public List<MiniSetDbfRecord> GetRecords()
	{
		MiniSetDbfAsset miniSetDbfAsset = this.assetBundleRequest.asset as MiniSetDbfAsset;
		if (miniSetDbfAsset != null)
		{
			for (int i = 0; i < miniSetDbfAsset.Records.Count; i++)
			{
				miniSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return miniSetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x00095E2A File Offset: 0x0009402A
	public LoadMiniSetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MiniSetDbfAsset));
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x00095E4D File Offset: 0x0009404D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400112E RID: 4398
	private AssetBundleRequest assetBundleRequest;
}
