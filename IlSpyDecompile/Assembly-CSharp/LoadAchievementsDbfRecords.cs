using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementsDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementsDbfRecord> GetRecords()
	{
		AchievementsDbfAsset achievementsDbfAsset = assetBundleRequest.asset as AchievementsDbfAsset;
		if (achievementsDbfAsset != null)
		{
			for (int i = 0; i < achievementsDbfAsset.Records.Count; i++)
			{
				achievementsDbfAsset.Records[i].StripUnusedLocales();
			}
			return achievementsDbfAsset.Records;
		}
		return null;
	}

	public LoadAchievementsDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementsDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
