using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureModeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureModeDbfRecord> GetRecords()
	{
		AdventureModeDbfAsset adventureModeDbfAsset = assetBundleRequest.asset as AdventureModeDbfAsset;
		if (adventureModeDbfAsset != null)
		{
			for (int i = 0; i < adventureModeDbfAsset.Records.Count; i++)
			{
				adventureModeDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureModeDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureModeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureModeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
