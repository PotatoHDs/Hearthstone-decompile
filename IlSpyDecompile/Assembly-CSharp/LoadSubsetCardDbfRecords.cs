using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadSubsetCardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<SubsetCardDbfRecord> GetRecords()
	{
		SubsetCardDbfAsset subsetCardDbfAsset = assetBundleRequest.asset as SubsetCardDbfAsset;
		if (subsetCardDbfAsset != null)
		{
			for (int i = 0; i < subsetCardDbfAsset.Records.Count; i++)
			{
				subsetCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetCardDbfAsset.Records;
		}
		return null;
	}

	public LoadSubsetCardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetCardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
