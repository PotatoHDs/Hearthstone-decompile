using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class LoadScheduledCharacterDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002035 RID: 8245 RVA: 0x000A0384 File Offset: 0x0009E584
	public List<ScheduledCharacterDialogDbfRecord> GetRecords()
	{
		ScheduledCharacterDialogDbfAsset scheduledCharacterDialogDbfAsset = this.assetBundleRequest.asset as ScheduledCharacterDialogDbfAsset;
		if (scheduledCharacterDialogDbfAsset != null)
		{
			for (int i = 0; i < scheduledCharacterDialogDbfAsset.Records.Count; i++)
			{
				scheduledCharacterDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return scheduledCharacterDialogDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002036 RID: 8246 RVA: 0x000A03DA File Offset: 0x0009E5DA
	public LoadScheduledCharacterDialogDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScheduledCharacterDialogDbfAsset));
	}

	// Token: 0x06002037 RID: 8247 RVA: 0x000A03FD File Offset: 0x0009E5FD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x0400122C RID: 4652
	private AssetBundleRequest assetBundleRequest;
}
