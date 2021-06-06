using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadSubsetRuleDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<SubsetRuleDbfRecord> GetRecords()
	{
		SubsetRuleDbfAsset subsetRuleDbfAsset = assetBundleRequest.asset as SubsetRuleDbfAsset;
		if (subsetRuleDbfAsset != null)
		{
			for (int i = 0; i < subsetRuleDbfAsset.Records.Count; i++)
			{
				subsetRuleDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetRuleDbfAsset.Records;
		}
		return null;
	}

	public LoadSubsetRuleDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetRuleDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
