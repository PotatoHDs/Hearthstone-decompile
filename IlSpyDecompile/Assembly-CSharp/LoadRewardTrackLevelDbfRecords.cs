using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardTrackLevelDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardTrackLevelDbfRecord> GetRecords()
	{
		RewardTrackLevelDbfAsset rewardTrackLevelDbfAsset = assetBundleRequest.asset as RewardTrackLevelDbfAsset;
		if (rewardTrackLevelDbfAsset != null)
		{
			for (int i = 0; i < rewardTrackLevelDbfAsset.Records.Count; i++)
			{
				rewardTrackLevelDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardTrackLevelDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardTrackLevelDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardTrackLevelDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
