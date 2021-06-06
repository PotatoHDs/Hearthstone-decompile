using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadModularBundleLayoutDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ModularBundleLayoutDbfRecord> GetRecords()
	{
		ModularBundleLayoutDbfAsset modularBundleLayoutDbfAsset = assetBundleRequest.asset as ModularBundleLayoutDbfAsset;
		if (modularBundleLayoutDbfAsset != null)
		{
			for (int i = 0; i < modularBundleLayoutDbfAsset.Records.Count; i++)
			{
				modularBundleLayoutDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleLayoutDbfAsset.Records;
		}
		return null;
	}

	public LoadModularBundleLayoutDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleLayoutDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
