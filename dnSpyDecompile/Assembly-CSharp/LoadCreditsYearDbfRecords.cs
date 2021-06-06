using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class LoadCreditsYearDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A39 RID: 6713 RVA: 0x0008AEDC File Offset: 0x000890DC
	public List<CreditsYearDbfRecord> GetRecords()
	{
		CreditsYearDbfAsset creditsYearDbfAsset = this.assetBundleRequest.asset as CreditsYearDbfAsset;
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

	// Token: 0x06001A3A RID: 6714 RVA: 0x0008AF32 File Offset: 0x00089132
	public LoadCreditsYearDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CreditsYearDbfAsset));
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x0008AF55 File Offset: 0x00089155
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FDB RID: 4059
	private AssetBundleRequest assetBundleRequest;
}
