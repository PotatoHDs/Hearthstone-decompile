using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class LoadAdventureDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600173A RID: 5946 RVA: 0x00081188 File Offset: 0x0007F388
	public List<AdventureDeckDbfRecord> GetRecords()
	{
		AdventureDeckDbfAsset adventureDeckDbfAsset = this.assetBundleRequest.asset as AdventureDeckDbfAsset;
		if (adventureDeckDbfAsset != null)
		{
			for (int i = 0; i < adventureDeckDbfAsset.Records.Count; i++)
			{
				adventureDeckDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDeckDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600173B RID: 5947 RVA: 0x000811DE File Offset: 0x0007F3DE
	public LoadAdventureDeckDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDeckDbfAsset));
	}

	// Token: 0x0600173C RID: 5948 RVA: 0x00081201 File Offset: 0x0007F401
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000EE5 RID: 3813
	private AssetBundleRequest assetBundleRequest;
}
