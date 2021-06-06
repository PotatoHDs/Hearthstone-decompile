using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureDeckDbfRecord> GetRecords()
	{
		AdventureDeckDbfAsset adventureDeckDbfAsset = assetBundleRequest.asset as AdventureDeckDbfAsset;
		if (adventureDeckDbfAsset != null)
		{
			for (int i = 0; i < adventureDeckDbfAsset.Records.Count; i++)
			{
				adventureDeckDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDeckDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureDeckDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDeckDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
