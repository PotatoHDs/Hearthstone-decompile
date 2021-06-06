using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class LoadCardSetTimingDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600197D RID: 6525 RVA: 0x00088ECC File Offset: 0x000870CC
	public List<CardSetTimingDbfRecord> GetRecords()
	{
		CardSetTimingDbfAsset cardSetTimingDbfAsset = this.assetBundleRequest.asset as CardSetTimingDbfAsset;
		if (cardSetTimingDbfAsset != null)
		{
			for (int i = 0; i < cardSetTimingDbfAsset.Records.Count; i++)
			{
				cardSetTimingDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetTimingDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600197E RID: 6526 RVA: 0x00088F22 File Offset: 0x00087122
	public LoadCardSetTimingDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetTimingDbfAsset));
	}

	// Token: 0x0600197F RID: 6527 RVA: 0x00088F45 File Offset: 0x00087145
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FA4 RID: 4004
	private AssetBundleRequest assetBundleRequest;
}
