using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class LoadAchievementSubcategoryDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001653 RID: 5715 RVA: 0x0007D238 File Offset: 0x0007B438
	public List<AchievementSubcategoryDbfRecord> GetRecords()
	{
		AchievementSubcategoryDbfAsset achievementSubcategoryDbfAsset = this.assetBundleRequest.asset as AchievementSubcategoryDbfAsset;
		if (achievementSubcategoryDbfAsset != null)
		{
			for (int i = 0; i < achievementSubcategoryDbfAsset.Records.Count; i++)
			{
				achievementSubcategoryDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementSubcategoryDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001654 RID: 5716 RVA: 0x0007D28E File Offset: 0x0007B48E
	public LoadAchievementSubcategoryDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSubcategoryDbfAsset));
	}

	// Token: 0x06001655 RID: 5717 RVA: 0x0007D2B1 File Offset: 0x0007B4B1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E8A RID: 3722
	private AssetBundleRequest assetBundleRequest;
}
