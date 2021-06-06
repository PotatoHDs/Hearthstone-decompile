using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadWingDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<WingDbfRecord> GetRecords()
	{
		WingDbfAsset wingDbfAsset = assetBundleRequest.asset as WingDbfAsset;
		if (wingDbfAsset != null)
		{
			for (int i = 0; i < wingDbfAsset.Records.Count; i++)
			{
				wingDbfAsset.Records[i].StripUnusedLocales();
			}
			return wingDbfAsset.Records;
		}
		return null;
	}

	public LoadWingDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(WingDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
