using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDeckRulesetRuleDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DeckRulesetRuleDbfRecord> GetRecords()
	{
		DeckRulesetRuleDbfAsset deckRulesetRuleDbfAsset = assetBundleRequest.asset as DeckRulesetRuleDbfAsset;
		if (deckRulesetRuleDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetRuleDbfAsset.Records.Count; i++)
			{
				deckRulesetRuleDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetRuleDbfAsset.Records;
		}
		return null;
	}

	public LoadDeckRulesetRuleDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetRuleDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
