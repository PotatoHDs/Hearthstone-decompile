using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadModularBundleDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ModularBundleDbfRecord> GetRecords()
	{
		ModularBundleDbfAsset modularBundleDbfAsset = assetBundleRequest.asset as ModularBundleDbfAsset;
		if (modularBundleDbfAsset != null)
		{
			for (int i = 0; i < modularBundleDbfAsset.Records.Count; i++)
			{
				modularBundleDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleDbfAsset.Records;
		}
		return null;
	}

	public LoadModularBundleDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
