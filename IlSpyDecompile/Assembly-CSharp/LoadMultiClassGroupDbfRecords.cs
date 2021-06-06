using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadMultiClassGroupDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<MultiClassGroupDbfRecord> GetRecords()
	{
		MultiClassGroupDbfAsset multiClassGroupDbfAsset = assetBundleRequest.asset as MultiClassGroupDbfAsset;
		if (multiClassGroupDbfAsset != null)
		{
			for (int i = 0; i < multiClassGroupDbfAsset.Records.Count; i++)
			{
				multiClassGroupDbfAsset.Records[i].StripUnusedLocales();
			}
			return multiClassGroupDbfAsset.Records;
		}
		return null;
	}

	public LoadMultiClassGroupDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MultiClassGroupDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
