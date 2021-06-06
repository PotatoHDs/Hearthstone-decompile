using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200028B RID: 651
public class LoadTriggerCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002114 RID: 8468 RVA: 0x000A2940 File Offset: 0x000A0B40
	public List<TriggerCardSetDbfRecord> GetRecords()
	{
		TriggerCardSetDbfAsset triggerCardSetDbfAsset = this.assetBundleRequest.asset as TriggerCardSetDbfAsset;
		if (triggerCardSetDbfAsset != null)
		{
			for (int i = 0; i < triggerCardSetDbfAsset.Records.Count; i++)
			{
				triggerCardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerCardSetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002115 RID: 8469 RVA: 0x000A2996 File Offset: 0x000A0B96
	public LoadTriggerCardSetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerCardSetDbfAsset));
	}

	// Token: 0x06002116 RID: 8470 RVA: 0x000A29B9 File Offset: 0x000A0BB9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400126A RID: 4714
	private AssetBundleRequest assetBundleRequest;
}
