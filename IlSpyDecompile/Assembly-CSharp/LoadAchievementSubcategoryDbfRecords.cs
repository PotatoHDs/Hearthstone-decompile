using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementSubcategoryDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementSubcategoryDbfRecord> GetRecords()
	{
		AchievementSubcategoryDbfAsset achievementSubcategoryDbfAsset = assetBundleRequest.asset as AchievementSubcategoryDbfAsset;
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

	public LoadAchievementSubcategoryDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSubcategoryDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
