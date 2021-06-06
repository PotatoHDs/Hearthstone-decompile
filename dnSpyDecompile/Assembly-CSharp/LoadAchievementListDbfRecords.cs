using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000155 RID: 341
public class LoadAchievementListDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600160C RID: 5644 RVA: 0x0007C7C0 File Offset: 0x0007A9C0
	public List<AchievementListDbfRecord> GetRecords()
	{
		AchievementListDbfAsset achievementListDbfAsset = this.assetBundleRequest.asset as AchievementListDbfAsset;
		if (achievementListDbfAsset != null)
		{
			for (int i = 0; i < achievementListDbfAsset.Records.Count; i++)
			{
				achievementListDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementListDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600160D RID: 5645 RVA: 0x0007C816 File Offset: 0x0007AA16
	public LoadAchievementListDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementListDbfAsset));
	}

	// Token: 0x0600160E RID: 5646 RVA: 0x0007C839 File Offset: 0x0007AA39
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E78 RID: 3704
	private AssetBundleRequest assetBundleRequest;
}
