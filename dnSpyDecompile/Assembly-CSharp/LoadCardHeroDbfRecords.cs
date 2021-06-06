using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200019D RID: 413
public class LoadCardHeroDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600190B RID: 6411 RVA: 0x000875F0 File Offset: 0x000857F0
	public List<CardHeroDbfRecord> GetRecords()
	{
		CardHeroDbfAsset cardHeroDbfAsset = this.assetBundleRequest.asset as CardHeroDbfAsset;
		if (cardHeroDbfAsset != null)
		{
			for (int i = 0; i < cardHeroDbfAsset.Records.Count; i++)
			{
				cardHeroDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardHeroDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600190C RID: 6412 RVA: 0x00087646 File Offset: 0x00085846
	public LoadCardHeroDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardHeroDbfAsset));
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x00087669 File Offset: 0x00085869
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F7D RID: 3965
	private AssetBundleRequest assetBundleRequest;
}
