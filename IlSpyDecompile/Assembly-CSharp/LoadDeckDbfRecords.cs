using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckDbfRecord> GetRecords()
	{
		DeckDbfAsset deckDbfAsset = assetBundleRequest.asset as DeckDbfAsset;
		if (deckDbfAsset != null)
		{
			for (int i = 0; i < deckDbfAsset.Records.Count; i++)
			{
				deckDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
