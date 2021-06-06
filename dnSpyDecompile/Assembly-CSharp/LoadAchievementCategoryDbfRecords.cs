using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class LoadAchievementCategoryDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060015BB RID: 5563 RVA: 0x0007B744 File Offset: 0x00079944
	public List<AchievementCategoryDbfRecord> GetRecords()
	{
		AchievementCategoryDbfAsset achievementCategoryDbfAsset = this.assetBundleRequest.asset as AchievementCategoryDbfAsset;
		if (achievementCategoryDbfAsset != null)
		{
			for (int i = 0; i < achievementCategoryDbfAsset.Records.Count; i++)
			{
				achievementCategoryDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementCategoryDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060015BC RID: 5564 RVA: 0x0007B79A File Offset: 0x0007999A
	public LoadAchievementCategoryDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementCategoryDbfAsset));
	}

	// Token: 0x060015BD RID: 5565 RVA: 0x0007B7BD File Offset: 0x000799BD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E5F RID: 3679
	private AssetBundleRequest assetBundleRequest;
}
