using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001EF RID: 495
public class LoadGameTypeRankedDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001BC0 RID: 7104 RVA: 0x0009107C File Offset: 0x0008F27C
	public List<GameTypeRankedDbfRecord> GetRecords()
	{
		GameTypeRankedDbfAsset gameTypeRankedDbfAsset = this.assetBundleRequest.asset as GameTypeRankedDbfAsset;
		if (gameTypeRankedDbfAsset != null)
		{
			for (int i = 0; i < gameTypeRankedDbfAsset.Records.Count; i++)
			{
				gameTypeRankedDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameTypeRankedDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000910D2 File Offset: 0x0008F2D2
	public LoadGameTypeRankedDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameTypeRankedDbfAsset));
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000910F5 File Offset: 0x0008F2F5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010BB RID: 4283
	private AssetBundleRequest assetBundleRequest;
}
