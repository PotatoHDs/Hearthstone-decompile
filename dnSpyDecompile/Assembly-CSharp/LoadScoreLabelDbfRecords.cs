using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200026D RID: 621
public class LoadScoreLabelDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002054 RID: 8276 RVA: 0x000A0B1C File Offset: 0x0009ED1C
	public List<ScoreLabelDbfRecord> GetRecords()
	{
		ScoreLabelDbfAsset scoreLabelDbfAsset = this.assetBundleRequest.asset as ScoreLabelDbfAsset;
		if (scoreLabelDbfAsset != null)
		{
			for (int i = 0; i < scoreLabelDbfAsset.Records.Count; i++)
			{
				scoreLabelDbfAsset.Records[i].StripUnusedLocales();
			}
			return scoreLabelDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002055 RID: 8277 RVA: 0x000A0B72 File Offset: 0x0009ED72
	public LoadScoreLabelDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScoreLabelDbfAsset));
	}

	// Token: 0x06002056 RID: 8278 RVA: 0x000A0B95 File Offset: 0x0009ED95
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001237 RID: 4663
	private AssetBundleRequest assetBundleRequest;
}
