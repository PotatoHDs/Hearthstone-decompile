using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200015E RID: 350
public class LoadAchievementSectionItemDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001640 RID: 5696 RVA: 0x0007CF64 File Offset: 0x0007B164
	public List<AchievementSectionItemDbfRecord> GetRecords()
	{
		AchievementSectionItemDbfAsset achievementSectionItemDbfAsset = this.assetBundleRequest.asset as AchievementSectionItemDbfAsset;
		if (achievementSectionItemDbfAsset != null)
		{
			for (int i = 0; i < achievementSectionItemDbfAsset.Records.Count; i++)
			{
				achievementSectionItemDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementSectionItemDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001641 RID: 5697 RVA: 0x0007CFBA File Offset: 0x0007B1BA
	public LoadAchievementSectionItemDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSectionItemDbfAsset));
	}

	// Token: 0x06001642 RID: 5698 RVA: 0x0007CFDD File Offset: 0x0007B1DD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E85 RID: 3717
	private AssetBundleRequest assetBundleRequest;
}
