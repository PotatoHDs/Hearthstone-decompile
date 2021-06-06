using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardTagDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardTagDbfRecord> GetRecords()
	{
		CardTagDbfAsset cardTagDbfAsset = assetBundleRequest.asset as CardTagDbfAsset;
		if (cardTagDbfAsset != null)
		{
			for (int i = 0; i < cardTagDbfAsset.Records.Count; i++)
			{
				cardTagDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardTagDbfAsset.Records;
		}
		return null;
	}

	public LoadCardTagDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardTagDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
