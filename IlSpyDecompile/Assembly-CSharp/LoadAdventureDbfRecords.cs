using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureDbfRecord> GetRecords()
	{
		AdventureDbfAsset adventureDbfAsset = assetBundleRequest.asset as AdventureDbfAsset;
		if (adventureDbfAsset != null)
		{
			for (int i = 0; i < adventureDbfAsset.Records.Count; i++)
			{
				adventureDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
