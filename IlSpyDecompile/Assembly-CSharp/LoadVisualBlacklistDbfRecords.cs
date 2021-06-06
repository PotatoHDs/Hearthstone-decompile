using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadVisualBlacklistDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<VisualBlacklistDbfRecord> GetRecords()
	{
		VisualBlacklistDbfAsset visualBlacklistDbfAsset = assetBundleRequest.asset as VisualBlacklistDbfAsset;
		if (visualBlacklistDbfAsset != null)
		{
			for (int i = 0; i < visualBlacklistDbfAsset.Records.Count; i++)
			{
				visualBlacklistDbfAsset.Records[i].StripUnusedLocales();
			}
			return visualBlacklistDbfAsset.Records;
		}
		return null;
	}

	public LoadVisualBlacklistDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(VisualBlacklistDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
