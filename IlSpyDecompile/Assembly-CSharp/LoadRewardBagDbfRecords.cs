using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardBagDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardBagDbfRecord> GetRecords()
	{
		RewardBagDbfAsset rewardBagDbfAsset = assetBundleRequest.asset as RewardBagDbfAsset;
		if (rewardBagDbfAsset != null)
		{
			for (int i = 0; i < rewardBagDbfAsset.Records.Count; i++)
			{
				rewardBagDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardBagDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardBagDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardBagDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
