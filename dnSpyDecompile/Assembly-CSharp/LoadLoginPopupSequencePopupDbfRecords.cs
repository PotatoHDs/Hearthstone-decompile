using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200020D RID: 525
public class LoadLoginPopupSequencePopupDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001CCE RID: 7374 RVA: 0x00094C48 File Offset: 0x00092E48
	public List<LoginPopupSequencePopupDbfRecord> GetRecords()
	{
		LoginPopupSequencePopupDbfAsset loginPopupSequencePopupDbfAsset = this.assetBundleRequest.asset as LoginPopupSequencePopupDbfAsset;
		if (loginPopupSequencePopupDbfAsset != null)
		{
			for (int i = 0; i < loginPopupSequencePopupDbfAsset.Records.Count; i++)
			{
				loginPopupSequencePopupDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginPopupSequencePopupDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001CCF RID: 7375 RVA: 0x00094C9E File Offset: 0x00092E9E
	public LoadLoginPopupSequencePopupDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginPopupSequencePopupDbfAsset));
	}

	// Token: 0x06001CD0 RID: 7376 RVA: 0x00094CC1 File Offset: 0x00092EC1
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001114 RID: 4372
	private AssetBundleRequest assetBundleRequest;
}
