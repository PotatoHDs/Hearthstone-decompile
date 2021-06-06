using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000210 RID: 528
public class LoadLoginRewardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001CF3 RID: 7411 RVA: 0x00095620 File Offset: 0x00093820
	public List<LoginRewardDbfRecord> GetRecords()
	{
		LoginRewardDbfAsset loginRewardDbfAsset = this.assetBundleRequest.asset as LoginRewardDbfAsset;
		if (loginRewardDbfAsset != null)
		{
			for (int i = 0; i < loginRewardDbfAsset.Records.Count; i++)
			{
				loginRewardDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginRewardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001CF4 RID: 7412 RVA: 0x00095676 File Offset: 0x00093876
	public LoadLoginRewardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginRewardDbfAsset));
	}

	// Token: 0x06001CF5 RID: 7413 RVA: 0x00095699 File Offset: 0x00093899
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001122 RID: 4386
	private AssetBundleRequest assetBundleRequest;
}
