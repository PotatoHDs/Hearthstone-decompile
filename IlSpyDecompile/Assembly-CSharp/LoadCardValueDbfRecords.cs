using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCardValueDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CardValueDbfRecord> Records
	{
		get
		{
			CardValueDbfAsset cardValueDbfAsset = assetBundleRequest.asset as CardValueDbfAsset;
			if (cardValueDbfAsset != null)
			{
				for (int i = 0; i < cardValueDbfAsset.Records.Count; i++)
				{
					cardValueDbfAsset.Records[i].StripUnusedLocales();
				}
				return cardValueDbfAsset.Records;
			}
			return null;
		}
	}

	public LoadCardValueDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardValueDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
