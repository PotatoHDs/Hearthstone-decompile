using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementCategoryDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementCategoryDbfRecord> GetRecords()
	{
		AchievementCategoryDbfAsset achievementCategoryDbfAsset = assetBundleRequest.asset as AchievementCategoryDbfAsset;
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

	public LoadAchievementCategoryDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementCategoryDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
