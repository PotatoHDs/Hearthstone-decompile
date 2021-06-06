using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class LoadCardChangeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060018A7 RID: 6311 RVA: 0x000860C4 File Offset: 0x000842C4
	public List<CardChangeDbfRecord> GetRecords()
	{
		CardChangeDbfAsset cardChangeDbfAsset = this.assetBundleRequest.asset as CardChangeDbfAsset;
		if (cardChangeDbfAsset != null)
		{
			for (int i = 0; i < cardChangeDbfAsset.Records.Count; i++)
			{
				cardChangeDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardChangeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060018A8 RID: 6312 RVA: 0x0008611A File Offset: 0x0008431A
	public LoadCardChangeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardChangeDbfAsset));
	}

	// Token: 0x060018A9 RID: 6313 RVA: 0x0008613D File Offset: 0x0008433D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F5D RID: 3933
	private AssetBundleRequest assetBundleRequest;
}
