using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchievementSectionItemDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchievementSectionItemDbfRecord> GetRecords()
	{
		AchievementSectionItemDbfAsset achievementSectionItemDbfAsset = assetBundleRequest.asset as AchievementSectionItemDbfAsset;
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

	public LoadAchievementSectionItemDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchievementSectionItemDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
