using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadPvpdrRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<PvpdrRewardDbfRecord> GetRecords()
	{
		PvpdrRewardDbfAsset pvpdrRewardDbfAsset = assetBundleRequest.asset as PvpdrRewardDbfAsset;
		if (pvpdrRewardDbfAsset != null)
		{
			for (int i = 0; i < pvpdrRewardDbfAsset.Records.Count; i++)
			{
				pvpdrRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return pvpdrRewardDbfAsset.Records;
		}
		return null;
	}

	public LoadPvpdrRewardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(PvpdrRewardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
