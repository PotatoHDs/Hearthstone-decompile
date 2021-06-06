using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadBoosterCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<BoosterCardSetDbfRecord> GetRecords()
	{
		BoosterCardSetDbfAsset boosterCardSetDbfAsset = assetBundleRequest.asset as BoosterCardSetDbfAsset;
		if (boosterCardSetDbfAsset != null)
		{
			for (int i = 0; i < boosterCardSetDbfAsset.Records.Count; i++)
			{
				boosterCardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return boosterCardSetDbfAsset.Records;
		}
		return null;
	}

	public LoadBoosterCardSetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoosterCardSetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
