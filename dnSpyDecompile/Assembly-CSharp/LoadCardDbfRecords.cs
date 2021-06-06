using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class LoadCardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060018BB RID: 6331 RVA: 0x000863E4 File Offset: 0x000845E4
	public List<CardDbfRecord> GetRecords()
	{
		CardDbfAsset cardDbfAsset = this.assetBundleRequest.asset as CardDbfAsset;
		if (cardDbfAsset != null)
		{
			for (int i = 0; i < cardDbfAsset.Records.Count; i++)
			{
				cardDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060018BC RID: 6332 RVA: 0x0008643A File Offset: 0x0008463A
	public LoadCardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardDbfAsset));
	}

	// Token: 0x060018BD RID: 6333 RVA: 0x0008645D File Offset: 0x0008465D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F63 RID: 3939
	private AssetBundleRequest assetBundleRequest;
}
