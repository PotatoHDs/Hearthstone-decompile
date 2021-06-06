using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200022E RID: 558
public class LoadPvpdrSeasonDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001DEB RID: 7659 RVA: 0x000988A8 File Offset: 0x00096AA8
	public List<PvpdrSeasonDbfRecord> GetRecords()
	{
		PvpdrSeasonDbfAsset pvpdrSeasonDbfAsset = this.assetBundleRequest.asset as PvpdrSeasonDbfAsset;
		if (pvpdrSeasonDbfAsset != null)
		{
			for (int i = 0; i < pvpdrSeasonDbfAsset.Records.Count; i++)
			{
				pvpdrSeasonDbfAsset.Records[i].StripUnusedLocales();
			}
			return pvpdrSeasonDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x000988FE File Offset: 0x00096AFE
	public LoadPvpdrSeasonDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(PvpdrSeasonDbfAsset));
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00098921 File Offset: 0x00096B21
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001170 RID: 4464
	private AssetBundleRequest assetBundleRequest;
}
