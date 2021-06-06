using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200020A RID: 522
public class LoadLoginPopupSequenceDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001CBE RID: 7358 RVA: 0x00094A2C File Offset: 0x00092C2C
	public List<LoginPopupSequenceDbfRecord> GetRecords()
	{
		LoginPopupSequenceDbfAsset loginPopupSequenceDbfAsset = this.assetBundleRequest.asset as LoginPopupSequenceDbfAsset;
		if (loginPopupSequenceDbfAsset != null)
		{
			for (int i = 0; i < loginPopupSequenceDbfAsset.Records.Count; i++)
			{
				loginPopupSequenceDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginPopupSequenceDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001CBF RID: 7359 RVA: 0x00094A82 File Offset: 0x00092C82
	public LoadLoginPopupSequenceDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginPopupSequenceDbfAsset));
	}

	// Token: 0x06001CC0 RID: 7360 RVA: 0x00094AA5 File Offset: 0x00092CA5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001111 RID: 4369
	private AssetBundleRequest assetBundleRequest;
}
