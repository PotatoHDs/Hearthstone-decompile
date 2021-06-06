using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementListDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementListDbfRecord> GetRecords()
	{
		AchievementListDbfAsset achievementListDbfAsset = assetBundleRequest.asset as AchievementListDbfAsset;
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

	public LoadAchievementListDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementListDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
