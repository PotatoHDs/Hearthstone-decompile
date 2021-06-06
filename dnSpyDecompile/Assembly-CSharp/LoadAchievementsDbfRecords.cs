using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000158 RID: 344
public class LoadAchievementsDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600161F RID: 5663 RVA: 0x0007CA94 File Offset: 0x0007AC94
	public List<AchievementsDbfRecord> GetRecords()
	{
		AchievementsDbfAsset achievementsDbfAsset = this.assetBundleRequest.asset as AchievementsDbfAsset;
		if (achievementsDbfAsset != null)
		{
			for (int i = 0; i < achievementsDbfAsset.Records.Count; i++)
			{
				achievementsDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementsDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x0007CAEA File Offset: 0x0007ACEA
	public LoadAchievementsDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementsDbfAsset));
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x0007CB0D File Offset: 0x0007AD0D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E7D RID: 3709
	private AssetBundleRequest assetBundleRequest;
}
