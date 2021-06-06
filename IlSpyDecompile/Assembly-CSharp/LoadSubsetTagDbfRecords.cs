using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadSubsetTagDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<SubsetTagDbfRecord> GetRecords()
	{
		SubsetTagDbfAsset subsetTagDbfAsset = assetBundleRequest.asset as SubsetTagDbfAsset;
		if (subsetTagDbfAsset != null)
		{
			for (int i = 0; i < subsetTagDbfAsset.Records.Count; i++)
			{
				subsetTagDbfAsset.Records[i].StripUnusedLocales();
			}
			return subsetTagDbfAsset.Records;
		}
		return null;
	}

	public LoadSubsetTagDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(SubsetTagDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
