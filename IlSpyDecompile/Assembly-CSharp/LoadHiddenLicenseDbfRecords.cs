using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadHiddenLicenseDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<HiddenLicenseDbfRecord> GetRecords()
	{
		HiddenLicenseDbfAsset hiddenLicenseDbfAsset = assetBundleRequest.asset as HiddenLicenseDbfAsset;
		if (hiddenLicenseDbfAsset != null)
		{
			for (int i = 0; i < hiddenLicenseDbfAsset.Records.Count; i++)
			{
				hiddenLicenseDbfAsset.Records[i].StripUnusedLocales();
			}
			return hiddenLicenseDbfAsset.Records;
		}
		return null;
	}

	public LoadHiddenLicenseDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(HiddenLicenseDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
