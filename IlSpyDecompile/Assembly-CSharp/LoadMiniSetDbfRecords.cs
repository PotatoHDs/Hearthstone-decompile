using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadMiniSetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<MiniSetDbfRecord> GetRecords()
	{
		MiniSetDbfAsset miniSetDbfAsset = assetBundleRequest.asset as MiniSetDbfAsset;
		if (miniSetDbfAsset != null)
		{
			for (int i = 0; i < miniSetDbfAsset.Records.Count; i++)
			{
				miniSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return miniSetDbfAsset.Records;
		}
		return null;
	}

	public LoadMiniSetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(MiniSetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
