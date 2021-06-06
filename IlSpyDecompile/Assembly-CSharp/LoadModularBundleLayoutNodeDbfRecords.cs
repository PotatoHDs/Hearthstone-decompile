using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadModularBundleLayoutNodeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ModularBundleLayoutNodeDbfRecord> GetRecords()
	{
		ModularBundleLayoutNodeDbfAsset modularBundleLayoutNodeDbfAsset = assetBundleRequest.asset as ModularBundleLayoutNodeDbfAsset;
		if (modularBundleLayoutNodeDbfAsset != null)
		{
			for (int i = 0; i < modularBundleLayoutNodeDbfAsset.Records.Count; i++)
			{
				modularBundleLayoutNodeDbfAsset.Records[i].StripUnusedLocales();
			}
			return modularBundleLayoutNodeDbfAsset.Records;
		}
		return null;
	}

	public LoadModularBundleLayoutNodeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ModularBundleLayoutNodeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
