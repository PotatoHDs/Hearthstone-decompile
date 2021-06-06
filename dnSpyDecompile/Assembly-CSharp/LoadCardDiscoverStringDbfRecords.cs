using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class LoadCardDiscoverStringDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060018FB RID: 6395 RVA: 0x00087404 File Offset: 0x00085604
	public List<CardDiscoverStringDbfRecord> GetRecords()
	{
		CardDiscoverStringDbfAsset cardDiscoverStringDbfAsset = this.assetBundleRequest.asset as CardDiscoverStringDbfAsset;
		if (cardDiscoverStringDbfAsset != null)
		{
			for (int i = 0; i < cardDiscoverStringDbfAsset.Records.Count; i++)
			{
				cardDiscoverStringDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardDiscoverStringDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x0008745A File Offset: 0x0008565A
	public LoadCardDiscoverStringDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardDiscoverStringDbfAsset));
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x0008747D File Offset: 0x0008567D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F79 RID: 3961
	private AssetBundleRequest assetBundleRequest;
}
