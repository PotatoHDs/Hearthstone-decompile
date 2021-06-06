using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementItemDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementItemDbfRecord> GetRecords()
	{
		AchievementItemDbfAsset achievementItemDbfAsset = assetBundleRequest.asset as AchievementItemDbfAsset;
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

	public LoadAchievementItemDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementItemDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
