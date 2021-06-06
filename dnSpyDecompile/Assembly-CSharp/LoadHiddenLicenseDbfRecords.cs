using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class LoadHiddenLicenseDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C18 RID: 7192 RVA: 0x000922C4 File Offset: 0x000904C4
	public List<HiddenLicenseDbfRecord> GetRecords()
	{
		HiddenLicenseDbfAsset hiddenLicenseDbfAsset = this.assetBundleRequest.asset as HiddenLicenseDbfAsset;
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

	// Token: 0x06001C19 RID: 7193 RVA: 0x0009231A File Offset: 0x0009051A
	public LoadHiddenLicenseDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(HiddenLicenseDbfAsset));
	}

	// Token: 0x06001C1A RID: 7194 RVA: 0x0009233D File Offset: 0x0009053D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010D6 RID: 4310
	private AssetBundleRequest assetBundleRequest;
}
