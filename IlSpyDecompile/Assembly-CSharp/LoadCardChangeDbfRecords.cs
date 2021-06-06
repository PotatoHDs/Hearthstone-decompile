using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardChangeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardChangeDbfRecord> GetRecords()
	{
		CardChangeDbfAsset cardChangeDbfAsset = assetBundleRequest.asset as CardChangeDbfAsset;
		if (cardChangeDbfAsset != null)
		{
			for (int i = 0; i < cardChangeDbfAsset.Records.Count; i++)
			{
				cardChangeDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardChangeDbfAsset.Records;
		}
		return null;
	}

	public LoadCardChangeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardChangeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
