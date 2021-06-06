using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadBoosterDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<BoosterDbfRecord> GetRecords()
	{
		BoosterDbfAsset boosterDbfAsset = assetBundleRequest.asset as BoosterDbfAsset;
		if (boosterDbfAsset != null)
		{
			for (int i = 0; i < boosterDbfAsset.Records.Count; i++)
			{
				boosterDbfAsset.Records[i].StripUnusedLocales();
			}
			return boosterDbfAsset.Records;
		}
		return null;
	}

	public LoadBoosterDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoosterDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
