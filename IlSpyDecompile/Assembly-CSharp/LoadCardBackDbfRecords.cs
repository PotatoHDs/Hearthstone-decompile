using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardBackDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardBackDbfRecord> GetRecords()
	{
		CardBackDbfAsset cardBackDbfAsset = assetBundleRequest.asset as CardBackDbfAsset;
		if (cardBackDbfAsset != null)
		{
			for (int i = 0; i < cardBackDbfAsset.Records.Count; i++)
			{
				cardBackDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardBackDbfAsset.Records;
		}
		return null;
	}

	public LoadCardBackDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardBackDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
