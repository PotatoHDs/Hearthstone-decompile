using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchieveDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchieveDbfRecord> GetRecords()
	{
		AchieveDbfAsset achieveDbfAsset = assetBundleRequest.asset as AchieveDbfAsset;
		if (achieveDbfAsset != null)
		{
			for (int i = 0; i < achieveDbfAsset.Records.Count; i++)
			{
				achieveDbfAsset.Records[i].StripUnusedLocales();
			}
			return achieveDbfAsset.Records;
		}
		return null;
	}

	public LoadAchieveDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
