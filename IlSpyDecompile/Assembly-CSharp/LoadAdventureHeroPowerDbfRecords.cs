using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureHeroPowerDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureHeroPowerDbfRecord> GetRecords()
	{
		AdventureHeroPowerDbfAsset adventureHeroPowerDbfAsset = assetBundleRequest.asset as AdventureHeroPowerDbfAsset;
		if (adventureHeroPowerDbfAsset != null)
		{
			for (int i = 0; i < adventureHeroPowerDbfAsset.Records.Count; i++)
			{
				adventureHeroPowerDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureHeroPowerDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureHeroPowerDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureHeroPowerDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
