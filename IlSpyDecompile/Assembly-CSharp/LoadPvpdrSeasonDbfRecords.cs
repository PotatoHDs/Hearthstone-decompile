using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadPvpdrSeasonDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<PvpdrSeasonDbfRecord> GetRecords()
	{
		PvpdrSeasonDbfAsset pvpdrSeasonDbfAsset = assetBundleRequest.asset as PvpdrSeasonDbfAsset;
		if (pvpdrSeasonDbfAsset != null)
		{
			for (int i = 0; i < pvpdrSeasonDbfAsset.Records.Count; i++)
			{
				pvpdrSeasonDbfAsset.Records[i].StripUnusedLocales();
			}
			return pvpdrSeasonDbfAsset.Records;
		}
		return null;
	}

	public LoadPvpdrSeasonDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(PvpdrSeasonDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
