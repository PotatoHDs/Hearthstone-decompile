using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadRewardListDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<RewardListDbfRecord> GetRecords()
	{
		RewardListDbfAsset rewardListDbfAsset = assetBundleRequest.asset as RewardListDbfAsset;
		if (rewardListDbfAsset != null)
		{
			for (int i = 0; i < rewardListDbfAsset.Records.Count; i++)
			{
				rewardListDbfAsset.Records[i].StripUnusedLocales();
			}
			return rewardListDbfAsset.Records;
		}
		return null;
	}

	public LoadRewardListDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(RewardListDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
