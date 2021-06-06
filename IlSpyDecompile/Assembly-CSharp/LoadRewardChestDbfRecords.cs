using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardChestDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardChestDbfRecord> GetRecords()
	{
		RewardChestDbfAsset rewardChestDbfAsset = assetBundleRequest.asset as RewardChestDbfAsset;
		if (rewardChestDbfAsset != null)
		{
			for (int i = 0; i < rewardChestDbfAsset.Records.Count; i++)
			{
				rewardChestDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardChestDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardChestDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardChestDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
