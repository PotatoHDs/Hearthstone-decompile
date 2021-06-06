using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCoinDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CoinDbfRecord> GetRecords()
	{
		CoinDbfAsset coinDbfAsset = assetBundleRequest.asset as CoinDbfAsset;
		if (coinDbfAsset != null)
		{
			for (int i = 0; i < coinDbfAsset.Records.Count; i++)
			{
				coinDbfAsset.Records[i].StripUnusedLocales();
			}
			return coinDbfAsset.Records;
		}
		return null;
	}

	public LoadCoinDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CoinDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
