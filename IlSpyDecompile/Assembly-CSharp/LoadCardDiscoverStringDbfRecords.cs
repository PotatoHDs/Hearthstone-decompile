using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardDiscoverStringDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardDiscoverStringDbfRecord> GetRecords()
	{
		CardDiscoverStringDbfAsset cardDiscoverStringDbfAsset = assetBundleRequest.asset as CardDiscoverStringDbfAsset;
		if (cardDiscoverStringDbfAsset != null)
		{
			for (int i = 0; i < cardDiscoverStringDbfAsset.Records.Count; i++)
			{
				cardDiscoverStringDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardDiscoverStringDbfAsset.Records;
		}
		return null;
	}

	public LoadCardDiscoverStringDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardDiscoverStringDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
