using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class LoadBoosterDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600183F RID: 6207 RVA: 0x0008466C File Offset: 0x0008286C
	public List<BoosterDbfRecord> GetRecords()
	{
		BoosterDbfAsset boosterDbfAsset = this.assetBundleRequest.asset as BoosterDbfAsset;
		if (boosterDbfAsset != null)
		{
			for (int i = 0; i < boosterDbfAsset.Records.Count; i++)
			{
				boosterDbfAsset.Records[i].StripUnusedLocales();
			}
			return boosterDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001840 RID: 6208 RVA: 0x000846C2 File Offset: 0x000828C2
	public LoadBoosterDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoosterDbfAsset));
	}

	// Token: 0x06001841 RID: 6209 RVA: 0x000846E5 File Offset: 0x000828E5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F35 RID: 3893
	private AssetBundleRequest assetBundleRequest;
}
