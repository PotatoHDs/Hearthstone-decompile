using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000297 RID: 663
public class LoadXpPerGameTypeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002190 RID: 8592 RVA: 0x000A46A8 File Offset: 0x000A28A8
	public List<XpPerGameTypeDbfRecord> GetRecords()
	{
		XpPerGameTypeDbfAsset xpPerGameTypeDbfAsset = this.assetBundleRequest.asset as XpPerGameTypeDbfAsset;
		if (xpPerGameTypeDbfAsset != null)
		{
			for (int i = 0; i < xpPerGameTypeDbfAsset.Records.Count; i++)
			{
				xpPerGameTypeDbfAsset.Records[i].StripUnusedLocales();
			}
			return xpPerGameTypeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06002191 RID: 8593 RVA: 0x000A46FE File Offset: 0x000A28FE
	public LoadXpPerGameTypeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(XpPerGameTypeDbfAsset));
	}

	// Token: 0x06002192 RID: 8594 RVA: 0x000A4721 File Offset: 0x000A2921
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001295 RID: 4757
	private AssetBundleRequest assetBundleRequest;
}
