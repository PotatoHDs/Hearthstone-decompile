using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementSectionDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementSectionDbfRecord> GetRecords()
	{
		AchievementSectionDbfAsset achievementSectionDbfAsset = assetBundleRequest.asset as AchievementSectionDbfAsset;
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

	public LoadAchievementSectionDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSectionDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
