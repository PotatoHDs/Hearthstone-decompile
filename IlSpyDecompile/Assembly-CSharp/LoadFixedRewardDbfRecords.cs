using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadFixedRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<FixedRewardDbfRecord> GetRecords()
	{
		FixedRewardDbfAsset fixedRewardDbfAsset = assetBundleRequest.asset as FixedRewardDbfAsset;
		if (fixedRewardDbfAsset != null)
		{
			for (int i = 0; i < fixedRewardDbfAsset.Records.Count; i++)
			{
				fixedRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return fixedRewardDbfAsset.Records;
		}
		return null;
	}

	public LoadFixedRewardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(FixedRewardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
