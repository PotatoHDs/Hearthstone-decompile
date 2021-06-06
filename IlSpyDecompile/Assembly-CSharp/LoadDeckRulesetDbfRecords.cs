using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckRulesetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckRulesetDbfRecord> GetRecords()
	{
		DeckRulesetDbfAsset deckRulesetDbfAsset = assetBundleRequest.asset as DeckRulesetDbfAsset;
		if (deckRulesetDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetDbfAsset.Records.Count; i++)
			{
				deckRulesetDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckRulesetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
