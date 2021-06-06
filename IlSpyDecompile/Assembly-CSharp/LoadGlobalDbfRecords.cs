using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGlobalDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GlobalDbfRecord> GetRecords()
	{
		GlobalDbfAsset globalDbfAsset = assetBundleRequest.asset as GlobalDbfAsset;
		if (globalDbfAsset != null)
		{
			for (int i = 0; i < globalDbfAsset.Records.Count; i++)
			{
				globalDbfAsset.Records[i].StripUnusedLocales();
			}
			return globalDbfAsset.Records;
		}
		return null;
	}

	public LoadGlobalDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GlobalDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
