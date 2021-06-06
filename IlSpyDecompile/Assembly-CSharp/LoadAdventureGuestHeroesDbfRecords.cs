using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureGuestHeroesDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureGuestHeroesDbfRecord> GetRecords()
	{
		AdventureGuestHeroesDbfAsset adventureGuestHeroesDbfAsset = assetBundleRequest.asset as AdventureGuestHeroesDbfAsset;
		if (adventureGuestHeroesDbfAsset != null)
		{
			for (int i = 0; i < adventureGuestHeroesDbfAsset.Records.Count; i++)
			{
				adventureGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureGuestHeroesDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureGuestHeroesDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureGuestHeroesDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
