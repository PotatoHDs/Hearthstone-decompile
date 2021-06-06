using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000270 RID: 624
public class LoadSellableDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002064 RID: 8292 RVA: 0x000A0D98 File Offset: 0x0009EF98
	public List<SellableDeckDbfRecord> GetRecords()
	{
		SellableDeckDbfAsset sellableDeckDbfAsset = this.assetBundleRequest.asset as SellableDeckDbfAsset;
		if (sellableDeckDbfAsset != null)
		{
			for (int i = 0; i < sellableDeckDbfAsset.Records.Count; i++)
			{
				sellableDeckDbfAsset.Records[i].StripUnusedLocales();
			}
			return sellableDeckDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002065 RID: 8293 RVA: 0x000A0DEE File Offset: 0x0009EFEE
	public LoadSellableDeckDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SellableDeckDbfAsset));
	}

	// Token: 0x06002066 RID: 8294 RVA: 0x000A0E11 File Offset: 0x0009F011
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400123B RID: 4667
	private AssetBundleRequest assetBundleRequest;
}
