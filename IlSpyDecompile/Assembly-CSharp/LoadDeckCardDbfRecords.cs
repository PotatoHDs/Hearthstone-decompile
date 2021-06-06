using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckCardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckCardDbfRecord> GetRecords()
	{
		DeckCardDbfAsset deckCardDbfAsset = assetBundleRequest.asset as DeckCardDbfAsset;
		if (deckCardDbfAsset != null)
		{
			for (int i = 0; i < deckCardDbfAsset.Records.Count; i++)
			{
				deckCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckCardDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckCardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckCardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
