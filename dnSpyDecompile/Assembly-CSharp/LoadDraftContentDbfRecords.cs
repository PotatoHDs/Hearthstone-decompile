using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class LoadDraftContentDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001AE7 RID: 6887 RVA: 0x0008D118 File Offset: 0x0008B318
	public List<DraftContentDbfRecord> GetRecords()
	{
		DraftContentDbfAsset draftContentDbfAsset = this.assetBundleRequest.asset as DraftContentDbfAsset;
		if (draftContentDbfAsset != null)
		{
			for (int i = 0; i < draftContentDbfAsset.Records.Count; i++)
			{
				draftContentDbfAsset.Records[i].StripUnusedLocales();
			}
			return draftContentDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001AE8 RID: 6888 RVA: 0x0008D16E File Offset: 0x0008B36E
	public LoadDraftContentDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DraftContentDbfAsset));
	}

	// Token: 0x06001AE9 RID: 6889 RVA: 0x0008D191 File Offset: 0x0008B391
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400100F RID: 4111
	private AssetBundleRequest assetBundleRequest;
}
