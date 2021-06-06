using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadCreditsYearDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<CreditsYearDbfRecord> GetRecords()
	{
		CreditsYearDbfAsset creditsYearDbfAsset = assetBundleRequest.asset as CreditsYearDbfAsset;
		if (creditsYearDbfAsset != null)
		{
			for (int i = 0; i < creditsYearDbfAsset.Records.Count; i++)
			{
				creditsYearDbfAsset.Records[i].StripUnusedLocales();
			}
			return creditsYearDbfAsset.Records;
		}
		return null;
	}

	public LoadCreditsYearDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CreditsYearDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
