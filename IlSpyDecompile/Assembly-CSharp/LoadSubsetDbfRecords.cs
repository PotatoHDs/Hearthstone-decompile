using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadSubsetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<SubsetDbfRecord> GetRecords()
	{
		SubsetDbfAsset subsetDbfAsset = assetBundleRequest.asset as SubsetDbfAsset;
		if (subsetDbfAsset != null)
		{
			for (int i = 0; i < subsetDbfAsset.Records.Count; i++)
			{
				subsetDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetDbfAsset.Records;
		}
		return null;
	}

	public LoadSubsetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
