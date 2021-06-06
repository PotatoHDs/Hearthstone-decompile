using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000294 RID: 660
public class LoadWingDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002144 RID: 8516 RVA: 0x000A3070 File Offset: 0x000A1270
	public List<WingDbfRecord> GetRecords()
	{
		WingDbfAsset wingDbfAsset = this.assetBundleRequest.asset as WingDbfAsset;
		if (wingDbfAsset != null)
		{
			for (int i = 0; i < wingDbfAsset.Records.Count; i++)
			{
				wingDbfAsset.Records[i].StripUnusedLocales();
			}
			return wingDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002145 RID: 8517 RVA: 0x000A30C6 File Offset: 0x000A12C6
	public LoadWingDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(WingDbfAsset));
	}

	// Token: 0x06002146 RID: 8518 RVA: 0x000A30E9 File Offset: 0x000A12E9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001275 RID: 4725
	private AssetBundleRequest assetBundleRequest;
}
