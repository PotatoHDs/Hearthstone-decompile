using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001AF RID: 431
public class LoadCardValueDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x17000278 RID: 632
	// (get) Token: 0x060019A6 RID: 6566 RVA: 0x00089494 File Offset: 0x00087694
	public List<CardValueDbfRecord> Records
	{
		get
		{
			CardValueDbfAsset cardValueDbfAsset = this.assetBundleRequest.asset as CardValueDbfAsset;
			if (cardValueDbfAsset != null)
			{
				for (int i = 0; i < cardValueDbfAsset.Records.Count; i++)
				{
					cardValueDbfAsset.Records[i].StripUnusedLocales();
				}
				return cardValueDbfAsset.Records;
			}
			return null;
		}
	}

	// Token: 0x060019A7 RID: 6567 RVA: 0x000894EA File Offset: 0x000876EA
	public LoadCardValueDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardValueDbfAsset));
	}

	// Token: 0x060019A8 RID: 6568 RVA: 0x0008950D File Offset: 0x0008770D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FB0 RID: 4016
	private AssetBundleRequest assetBundleRequest;
}
