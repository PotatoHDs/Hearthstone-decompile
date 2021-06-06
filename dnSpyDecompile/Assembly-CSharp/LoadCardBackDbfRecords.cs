using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class LoadCardBackDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001887 RID: 6279 RVA: 0x00085890 File Offset: 0x00083A90
	public List<CardBackDbfRecord> GetRecords()
	{
		CardBackDbfAsset cardBackDbfAsset = this.assetBundleRequest.asset as CardBackDbfAsset;
		if (cardBackDbfAsset != null)
		{
			for (int i = 0; i < cardBackDbfAsset.Records.Count; i++)
			{
				cardBackDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardBackDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001888 RID: 6280 RVA: 0x000858E6 File Offset: 0x00083AE6
	public LoadCardBackDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardBackDbfAsset));
	}

	// Token: 0x06001889 RID: 6281 RVA: 0x00085909 File Offset: 0x00083B09
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F51 RID: 3921
	private AssetBundleRequest assetBundleRequest;
}
