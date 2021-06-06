using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardTrackDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardTrackDbfRecord> GetRecords()
	{
		RewardTrackDbfAsset rewardTrackDbfAsset = assetBundleRequest.asset as RewardTrackDbfAsset;
		if (rewardTrackDbfAsset != null)
		{
			for (int i = 0; i < rewardTrackDbfAsset.Records.Count; i++)
			{
				rewardTrackDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardTrackDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardTrackDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardTrackDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
