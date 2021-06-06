using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckTemplateDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckTemplateDbfRecord> GetRecords()
	{
		DeckTemplateDbfAsset deckTemplateDbfAsset = assetBundleRequest.asset as DeckTemplateDbfAsset;
		if (deckTemplateDbfAsset != null)
		{
			for (int i = 0; i < deckTemplateDbfAsset.Records.Count; i++)
			{
				deckTemplateDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckTemplateDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckTemplateDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckTemplateDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
