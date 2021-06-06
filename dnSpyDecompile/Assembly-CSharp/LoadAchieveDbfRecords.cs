using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000149 RID: 329
public class LoadAchieveDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600154D RID: 5453 RVA: 0x0007978C File Offset: 0x0007798C
	public List<AchieveDbfRecord> GetRecords()
	{
		AchieveDbfAsset achieveDbfAsset = this.assetBundleRequest.asset as AchieveDbfAsset;
		if (achieveDbfAsset != null)
		{
			for (int i = 0; i < achieveDbfAsset.Records.Count; i++)
			{
				achieveDbfAsset.Records[i].StripUnusedLocales();
			}
			return achieveDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600154E RID: 5454 RVA: 0x000797E2 File Offset: 0x000779E2
	public LoadAchieveDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveDbfAsset));
	}

	// Token: 0x0600154F RID: 5455 RVA: 0x00079805 File Offset: 0x00077A05
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E34 RID: 3636
	private AssetBundleRequest assetBundleRequest;
}
