using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAdventureMissionDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AdventureMissionDbfRecord> GetRecords()
	{
		AdventureMissionDbfAsset adventureMissionDbfAsset = assetBundleRequest.asset as AdventureMissionDbfAsset;
		if (adventureMissionDbfAsset != null)
		{
			for (int i = 0; i < adventureMissionDbfAsset.Records.Count; i++)
			{
				adventureMissionDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureMissionDbfAsset.Records;
		}
		return null;
	}

	public LoadAdventureMissionDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureMissionDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
