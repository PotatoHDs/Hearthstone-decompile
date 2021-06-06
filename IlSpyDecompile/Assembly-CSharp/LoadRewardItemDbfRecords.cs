using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardItemDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardItemDbfRecord> GetRecords()
	{
		RewardItemDbfAsset rewardItemDbfAsset = assetBundleRequest.asset as RewardItemDbfAsset;
		if (rewardItemDbfAsset != null)
		{
			for (int i = 0; i < rewardItemDbfAsset.Records.Count; i++)
			{
				rewardItemDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardItemDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardItemDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardItemDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
