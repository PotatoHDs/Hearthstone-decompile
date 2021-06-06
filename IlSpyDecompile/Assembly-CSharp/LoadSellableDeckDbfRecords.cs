using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadSellableDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<SellableDeckDbfRecord> GetRecords()
	{
		SellableDeckDbfAsset sellableDeckDbfAsset = assetBundleRequest.asset as SellableDeckDbfAsset;
		if (sellableDeckDbfAsset != null)
		{
			for (int i = 0; i < sellableDeckDbfAsset.Records.Count; i++)
			{
				sellableDeckDbfAsset.Records[i].StripUnusedLocales();
			}
			return sellableDeckDbfAsset.Records;
		}
		return null;
	}

	public LoadSellableDeckDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SellableDeckDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
