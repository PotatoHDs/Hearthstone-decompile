using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardLevelDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardLevelDbfRecord> GetRecords()
	{
		RewardLevelDbfAsset rewardLevelDbfAsset = assetBundleRequest.asset as RewardLevelDbfAsset;
		if (rewardLevelDbfAsset != null)
		{
			for (int i = 0; i < rewardLevelDbfAsset.Records.Count; i++)
			{
				rewardLevelDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardLevelDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardLevelDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardLevelDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
