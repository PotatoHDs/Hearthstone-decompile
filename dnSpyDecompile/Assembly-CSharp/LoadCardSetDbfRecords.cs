using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A3 RID: 419
public class LoadCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001945 RID: 6469 RVA: 0x000882CC File Offset: 0x000864CC
	public List<CardSetDbfRecord> GetRecords()
	{
		CardSetDbfAsset cardSetDbfAsset = this.assetBundleRequest.asset as CardSetDbfAsset;
		if (cardSetDbfAsset != null)
		{
			for (int i = 0; i < cardSetDbfAsset.Records.Count; i++)
			{
				cardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001946 RID: 6470 RVA: 0x00088322 File Offset: 0x00086522
	public LoadCardSetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetDbfAsset));
	}

	// Token: 0x06001947 RID: 6471 RVA: 0x00088345 File Offset: 0x00086545
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F91 RID: 3985
	private AssetBundleRequest assetBundleRequest;
}
