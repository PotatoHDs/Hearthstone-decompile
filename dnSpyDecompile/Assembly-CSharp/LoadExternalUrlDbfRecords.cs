using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class LoadExternalUrlDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001AFC RID: 6908 RVA: 0x0008D42C File Offset: 0x0008B62C
	public List<ExternalUrlDbfRecord> GetRecords()
	{
		ExternalUrlDbfAsset externalUrlDbfAsset = this.assetBundleRequest.asset as ExternalUrlDbfAsset;
		if (externalUrlDbfAsset != null)
		{
			for (int i = 0; i < externalUrlDbfAsset.Records.Count; i++)
			{
				externalUrlDbfAsset.Records[i].StripUnusedLocales();
			}
			return externalUrlDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001AFD RID: 6909 RVA: 0x0008D482 File Offset: 0x0008B682
	public LoadExternalUrlDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ExternalUrlDbfAsset));
	}

	// Token: 0x06001AFE RID: 6910 RVA: 0x0008D4A5 File Offset: 0x0008B6A5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001015 RID: 4117
	private AssetBundleRequest assetBundleRequest;
}
