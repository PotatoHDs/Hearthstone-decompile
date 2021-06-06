using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000288 RID: 648
public class LoadTriggerCardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002103 RID: 8451 RVA: 0x000A26C8 File Offset: 0x000A08C8
	public List<TriggerCardDbfRecord> GetRecords()
	{
		TriggerCardDbfAsset triggerCardDbfAsset = this.assetBundleRequest.asset as TriggerCardDbfAsset;
		if (triggerCardDbfAsset != null)
		{
			for (int i = 0; i < triggerCardDbfAsset.Records.Count; i++)
			{
				triggerCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerCardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002104 RID: 8452 RVA: 0x000A271E File Offset: 0x000A091E
	public LoadTriggerCardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerCardDbfAsset));
	}

	// Token: 0x06002105 RID: 8453 RVA: 0x000A2741 File Offset: 0x000A0941
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001266 RID: 4710
	private AssetBundleRequest assetBundleRequest;
}
