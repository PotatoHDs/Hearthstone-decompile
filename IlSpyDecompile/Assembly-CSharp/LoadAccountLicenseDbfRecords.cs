using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadAccountLicenseDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<AccountLicenseDbfRecord> GetRecords()
	{
		AccountLicenseDbfAsset accountLicenseDbfAsset = assetBundleRequest.asset as AccountLicenseDbfAsset;
		if (accountLicenseDbfAsset != null)
		{
			for (int i = 0; i < accountLicenseDbfAsset.Records.Count; i++)
			{
				accountLicenseDbfAsset.Records[i].StripUnusedLocales();
			}
			return accountLicenseDbfAsset.Records;
		}
		return null;
	}

	public LoadAccountLicenseDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AccountLicenseDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
