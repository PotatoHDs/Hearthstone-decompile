using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAchieveConditionDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AchieveConditionDbfRecord> GetRecords()
	{
		AchieveConditionDbfAsset achieveConditionDbfAsset = assetBundleRequest.asset as AchieveConditionDbfAsset;
		if (achieveConditionDbfAsset != null)
		{
			for (int i = 0; i < achieveConditionDbfAsset.Records.Count; i++)
			{
				achieveConditionDbfAsset.Records[i].StripUnusedLocales();
			}
			return achieveConditionDbfAsset.Records;
		}
		return null;
	}

	public LoadAchieveConditionDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveConditionDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
