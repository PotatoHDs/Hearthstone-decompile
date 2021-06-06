using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000291 RID: 657
public class LoadVisualBlacklistDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002133 RID: 8499 RVA: 0x000A2DF8 File Offset: 0x000A0FF8
	public List<VisualBlacklistDbfRecord> GetRecords()
	{
		VisualBlacklistDbfAsset visualBlacklistDbfAsset = this.assetBundleRequest.asset as VisualBlacklistDbfAsset;
		if (visualBlacklistDbfAsset != null)
		{
			for (int i = 0; i < visualBlacklistDbfAsset.Records.Count; i++)
			{
				visualBlacklistDbfAsset.Records[i].StripUnusedLocales();
			}
			return visualBlacklistDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002134 RID: 8500 RVA: 0x000A2E4E File Offset: 0x000A104E
	public LoadVisualBlacklistDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(VisualBlacklistDbfAsset));
	}

	// Token: 0x06002135 RID: 8501 RVA: 0x000A2E71 File Offset: 0x000A1071
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001271 RID: 4721
	private AssetBundleRequest assetBundleRequest;
}
