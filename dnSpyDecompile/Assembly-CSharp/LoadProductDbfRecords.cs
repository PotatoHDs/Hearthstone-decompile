using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000228 RID: 552
public class LoadProductDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001DCA RID: 7626 RVA: 0x0009847C File Offset: 0x0009667C
	public List<ProductDbfRecord> GetRecords()
	{
		ProductDbfAsset productDbfAsset = this.assetBundleRequest.asset as ProductDbfAsset;
		if (productDbfAsset != null)
		{
			for (int i = 0; i < productDbfAsset.Records.Count; i++)
			{
				productDbfAsset.Records[i].StripUnusedLocales();
			}
			return productDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001DCB RID: 7627 RVA: 0x000984D2 File Offset: 0x000966D2
	public LoadProductDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ProductDbfAsset));
	}

	// Token: 0x06001DCC RID: 7628 RVA: 0x000984F5 File Offset: 0x000966F5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001169 RID: 4457
	private AssetBundleRequest assetBundleRequest;
}
