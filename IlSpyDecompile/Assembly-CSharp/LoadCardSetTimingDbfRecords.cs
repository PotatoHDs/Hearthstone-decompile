using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardSetTimingDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardSetTimingDbfRecord> GetRecords()
	{
		CardSetTimingDbfAsset cardSetTimingDbfAsset = assetBundleRequest.asset as CardSetTimingDbfAsset;
		if (cardSetTimingDbfAsset != null)
		{
			for (int i = 0; i < cardSetTimingDbfAsset.Records.Count; i++)
			{
				cardSetTimingDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetTimingDbfAsset.Records;
		}
		return null;
	}

	public LoadCardSetTimingDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetTimingDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
