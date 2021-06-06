using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class LoadAchievementSectionDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001632 RID: 5682 RVA: 0x0007CD68 File Offset: 0x0007AF68
	public List<AchievementSectionDbfRecord> GetRecords()
	{
		AchievementSectionDbfAsset achievementSectionDbfAsset = this.assetBundleRequest.asset as AchievementSectionDbfAsset;
		if (achievementSectionDbfAsset != null)
		{
			for (int i = 0; i < achievementSectionDbfAsset.Records.Count; i++)
			{
				achievementSectionDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementSectionDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001633 RID: 5683 RVA: 0x0007CDBE File Offset: 0x0007AFBE
	public LoadAchievementSectionDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSectionDbfAsset));
	}

	// Token: 0x06001634 RID: 5684 RVA: 0x0007CDE1 File Offset: 0x0007AFE1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E82 RID: 3714
	private AssetBundleRequest assetBundleRequest;
}
