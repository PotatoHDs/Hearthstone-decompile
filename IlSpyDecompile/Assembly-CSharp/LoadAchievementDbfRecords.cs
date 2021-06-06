using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementDbfRecord> GetRecords()
	{
		AchievementDbfAsset achievementDbfAsset = assetBundleRequest.asset as AchievementDbfAsset;
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

	public LoadAchievementDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
