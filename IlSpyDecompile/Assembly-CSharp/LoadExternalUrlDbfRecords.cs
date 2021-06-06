using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadExternalUrlDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ExternalUrlDbfRecord> GetRecords()
	{
		ExternalUrlDbfAsset externalUrlDbfAsset = assetBundleRequest.asset as ExternalUrlDbfAsset;
		if (externalUrlDbfAsset != null)
		{
			for (int i = 0; i < externalUrlDbfAsset.Records.Count; i++)
			{
				externalUrlDbfAsset.Records[i].StripUnusedLocales();
			}
			return externalUrlDbfAsset.Records;
		}
		return null;
	}

	public LoadExternalUrlDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ExternalUrlDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
