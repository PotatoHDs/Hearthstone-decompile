using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureDataDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureDataDbfRecord> GetRecords()
	{
		AdventureDataDbfAsset adventureDataDbfAsset = assetBundleRequest.asset as AdventureDataDbfAsset;
		if (adventureDataDbfAsset != null)
		{
			for (int i = 0; i < adventureDataDbfAsset.Records.Count; i++)
			{
				adventureDataDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDataDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureDataDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDataDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
