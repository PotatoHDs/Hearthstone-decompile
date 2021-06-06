using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardChestContentsDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardChestContentsDbfRecord> GetRecords()
	{
		RewardChestContentsDbfAsset rewardChestContentsDbfAsset = assetBundleRequest.asset as RewardChestContentsDbfAsset;
		if (rewardChestContentsDbfAsset != null)
		{
			for (int i = 0; i < rewardChestContentsDbfAsset.Records.Count; i++)
			{
				rewardChestContentsDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardChestContentsDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardChestContentsDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardChestContentsDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
