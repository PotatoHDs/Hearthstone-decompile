using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000225 RID: 549
public class LoadProductClientDataDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001DB8 RID: 7608 RVA: 0x00098188 File Offset: 0x00096388
	public List<ProductClientDataDbfRecord> GetRecords()
	{
		ProductClientDataDbfAsset productClientDataDbfAsset = this.assetBundleRequest.asset as ProductClientDataDbfAsset;
		if (productClientDataDbfAsset != null)
		{
			for (int i = 0; i < productClientDataDbfAsset.Records.Count; i++)
			{
				productClientDataDbfAsset.Records[i].StripUnusedLocales();
			}
			return productClientDataDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001DB9 RID: 7609 RVA: 0x000981DE File Offset: 0x000963DE
	public LoadProductClientDataDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ProductClientDataDbfAsset));
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x00098201 File Offset: 0x00096401
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001164 RID: 4452
	private AssetBundleRequest assetBundleRequest;
}
