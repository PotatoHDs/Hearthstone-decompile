using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class LoadAchievementItemDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060015F9 RID: 5625 RVA: 0x0007C4EC File Offset: 0x0007A6EC
	public List<AchievementItemDbfRecord> GetRecords()
	{
		AchievementItemDbfAsset achievementItemDbfAsset = this.assetBundleRequest.asset as AchievementItemDbfAsset;
		if (achievementItemDbfAsset != null)
		{
			for (int i = 0; i < achievementItemDbfAsset.Records.Count; i++)
			{
				achievementItemDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementItemDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060015FA RID: 5626 RVA: 0x0007C542 File Offset: 0x0007A742
	public LoadAchievementItemDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementItemDbfAsset));
	}

	// Token: 0x060015FB RID: 5627 RVA: 0x0007C565 File Offset: 0x0007A765
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E73 RID: 3699
	private AssetBundleRequest assetBundleRequest;
}
