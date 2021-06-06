using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardDbfRecord> GetRecords()
	{
		CardDbfAsset cardDbfAsset = assetBundleRequest.asset as CardDbfAsset;
		if (cardDbfAsset != null)
		{
			for (int i = 0; i < cardDbfAsset.Records.Count; i++)
			{
				cardDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardDbfAsset.Records;
		}
		return null;
	}

	public LoadCardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
