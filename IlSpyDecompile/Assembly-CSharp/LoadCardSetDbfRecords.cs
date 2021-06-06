using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardSetDbfRecord> GetRecords()
	{
		CardSetDbfAsset cardSetDbfAsset = assetBundleRequest.asset as CardSetDbfAsset;
		if (cardSetDbfAsset != null)
		{
			for (int i = 0; i < cardSetDbfAsset.Records.Count; i++)
			{
				cardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardSetDbfAsset.Records;
		}
		return null;
	}

	public LoadCardSetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardSetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
