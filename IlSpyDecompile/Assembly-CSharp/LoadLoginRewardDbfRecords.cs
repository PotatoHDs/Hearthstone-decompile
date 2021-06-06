using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLoginRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LoginRewardDbfRecord> GetRecords()
	{
		LoginRewardDbfAsset loginRewardDbfAsset = assetBundleRequest.asset as LoginRewardDbfAsset;
		if (loginRewardDbfAsset != null)
		{
			for (int i = 0; i < loginRewardDbfAsset.Records.Count; i++)
			{
				loginRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginRewardDbfAsset.Records;
		}
		return null;
	}

	public LoadLoginRewardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginRewardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
