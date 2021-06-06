using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class LoadAchievementDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060015CF RID: 5583 RVA: 0x0007BA44 File Offset: 0x00079C44
	public List<AchievementDbfRecord> GetRecords()
	{
		AchievementDbfAsset achievementDbfAsset = this.assetBundleRequest.asset as AchievementDbfAsset;
		if (achievementDbfAsset != null)
		{
			for (int i = 0; i < achievementDbfAsset.Records.Count; i++)
			{
				achievementDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060015D0 RID: 5584 RVA: 0x0007BA9A File Offset: 0x00079C9A
	public LoadAchievementDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementDbfAsset));
	}

	// Token: 0x060015D1 RID: 5585 RVA: 0x0007BABD File Offset: 0x00079CBD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E64 RID: 3684
	private AssetBundleRequest assetBundleRequest;
}
