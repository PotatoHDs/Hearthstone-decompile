using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class LoadTriggerDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002125 RID: 8485 RVA: 0x000A2BB8 File Offset: 0x000A0DB8
	public List<TriggerDbfRecord> GetRecords()
	{
		TriggerDbfAsset triggerDbfAsset = this.assetBundleRequest.asset as TriggerDbfAsset;
		if (triggerDbfAsset != null)
		{
			for (int i = 0; i < triggerDbfAsset.Records.Count; i++)
			{
				triggerDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x000A2C0E File Offset: 0x000A0E0E
	public LoadTriggerDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerDbfAsset));
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x000A2C31 File Offset: 0x000A0E31
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400126E RID: 4718
	private AssetBundleRequest assetBundleRequest;
}
