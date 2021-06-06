using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000146 RID: 326
public class LoadAchieveConditionDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600153C RID: 5436 RVA: 0x00079514 File Offset: 0x00077714
	public List<AchieveConditionDbfRecord> GetRecords()
	{
		AchieveConditionDbfAsset achieveConditionDbfAsset = this.assetBundleRequest.asset as AchieveConditionDbfAsset;
		if (achieveConditionDbfAsset != null)
		{
			for (int i = 0; i < achieveConditionDbfAsset.Records.Count; i++)
			{
				achieveConditionDbfAsset.Records[i].StripUnusedLocales();
			}
			return achieveConditionDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x0007956A File Offset: 0x0007776A
	public LoadAchieveConditionDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AchieveConditionDbfAsset));
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x0007958D File Offset: 0x0007778D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E30 RID: 3632
	private AssetBundleRequest assetBundleRequest;
}
