using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureLoadoutTreasuresDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureLoadoutTreasuresDbfRecord> GetRecords()
	{
		AdventureLoadoutTreasuresDbfAsset adventureLoadoutTreasuresDbfAsset = assetBundleRequest.asset as AdventureLoadoutTreasuresDbfAsset;
		if (adventureLoadoutTreasuresDbfAsset != null)
		{
			for (int i = 0; i < adventureLoadoutTreasuresDbfAsset.Records.Count; i++)
			{
				adventureLoadoutTreasuresDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureLoadoutTreasuresDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureLoadoutTreasuresDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureLoadoutTreasuresDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
