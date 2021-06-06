using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000143 RID: 323
public class LoadAccountLicenseDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600152E RID: 5422 RVA: 0x00079328 File Offset: 0x00077528
	public List<AccountLicenseDbfRecord> GetRecords()
	{
		AccountLicenseDbfAsset accountLicenseDbfAsset = this.assetBundleRequest.asset as AccountLicenseDbfAsset;
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

	// Token: 0x0600152F RID: 5423 RVA: 0x0007937E File Offset: 0x0007757E
	public LoadAccountLicenseDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AccountLicenseDbfAsset));
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x000793A1 File Offset: 0x000775A1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E2D RID: 3629
	private AssetBundleRequest assetBundleRequest;
}
