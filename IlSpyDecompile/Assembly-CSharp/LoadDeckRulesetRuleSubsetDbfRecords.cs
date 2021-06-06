using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckRulesetRuleSubsetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckRulesetRuleSubsetDbfRecord> GetRecords()
	{
		DeckRulesetRuleSubsetDbfAsset deckRulesetRuleSubsetDbfAsset = assetBundleRequest.asset as DeckRulesetRuleSubsetDbfAsset;
		if (deckRulesetRuleSubsetDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetRuleSubsetDbfAsset.Records.Count; i++)
			{
				deckRulesetRuleSubsetDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetRuleSubsetDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckRulesetRuleSubsetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetRuleSubsetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
