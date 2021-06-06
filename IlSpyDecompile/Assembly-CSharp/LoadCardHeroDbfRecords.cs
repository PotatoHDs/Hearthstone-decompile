using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardHeroDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardHeroDbfRecord> GetRecords()
	{
		CardHeroDbfAsset cardHeroDbfAsset = assetBundleRequest.asset as CardHeroDbfAsset;
		if (cardHeroDbfAsset != null)
		{
			for (int i = 0; i < cardHeroDbfAsset.Records.Count; i++)
			{
				cardHeroDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardHeroDbfAsset.Records;
		}
		return null;
	}

	public LoadCardHeroDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardHeroDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
