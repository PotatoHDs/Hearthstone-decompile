using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000188 RID: 392
public class LoadBoosterCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001830 RID: 6192 RVA: 0x00084474 File Offset: 0x00082674
	public List<BoosterCardSetDbfRecord> GetRecords()
	{
		BoosterCardSetDbfAsset boosterCardSetDbfAsset = this.assetBundleRequest.asset as BoosterCardSetDbfAsset;
		if (boosterCardSetDbfAsset != null)
		{
			for (int i = 0; i < boosterCardSetDbfAsset.Records.Count; i++)
			{
				boosterCardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return boosterCardSetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001831 RID: 6193 RVA: 0x000844CA File Offset: 0x000826CA
	public LoadBoosterCardSetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BoosterCardSetDbfAsset));
	}

	// Token: 0x06001832 RID: 6194 RVA: 0x000844ED File Offset: 0x000826ED
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F32 RID: 3890
	private AssetBundleRequest assetBundleRequest;
}
