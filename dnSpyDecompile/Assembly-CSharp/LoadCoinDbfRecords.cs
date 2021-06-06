using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C1 RID: 449
public class LoadCoinDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A24 RID: 6692 RVA: 0x0008AB7C File Offset: 0x00088D7C
	public List<CoinDbfRecord> GetRecords()
	{
		CoinDbfAsset coinDbfAsset = this.assetBundleRequest.asset as CoinDbfAsset;
		if (coinDbfAsset != null)
		{
			for (int i = 0; i < coinDbfAsset.Records.Count; i++)
			{
				coinDbfAsset.Records[i].StripUnusedLocales();
			}
			return coinDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001A25 RID: 6693 RVA: 0x0008ABD2 File Offset: 0x00088DD2
	public LoadCoinDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CoinDbfAsset));
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x0008ABF5 File Offset: 0x00088DF5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FD5 RID: 4053
	private AssetBundleRequest assetBundleRequest;
}
