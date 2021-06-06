using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200018E RID: 398
public class LoadCardAdditonalSearchTermsDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001877 RID: 6263 RVA: 0x00085610 File Offset: 0x00083810
	public List<CardAdditonalSearchTermsDbfRecord> GetRecords()
	{
		CardAdditonalSearchTermsDbfAsset cardAdditonalSearchTermsDbfAsset = this.assetBundleRequest.asset as CardAdditonalSearchTermsDbfAsset;
		if (cardAdditonalSearchTermsDbfAsset != null)
		{
			for (int i = 0; i < cardAdditonalSearchTermsDbfAsset.Records.Count; i++)
			{
				cardAdditonalSearchTermsDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardAdditonalSearchTermsDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001878 RID: 6264 RVA: 0x00085666 File Offset: 0x00083866
	public LoadCardAdditonalSearchTermsDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardAdditonalSearchTermsDbfAsset));
	}

	// Token: 0x06001879 RID: 6265 RVA: 0x00085689 File Offset: 0x00083889
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F4D RID: 3917
	private AssetBundleRequest assetBundleRequest;
}
