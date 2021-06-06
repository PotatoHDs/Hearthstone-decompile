using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001A0 RID: 416
public class LoadCardPlayerDeckOverrideDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600192E RID: 6446 RVA: 0x00087EF8 File Offset: 0x000860F8
	public List<CardPlayerDeckOverrideDbfRecord> GetRecords()
	{
		CardPlayerDeckOverrideDbfAsset cardPlayerDeckOverrideDbfAsset = this.assetBundleRequest.asset as CardPlayerDeckOverrideDbfAsset;
		if (cardPlayerDeckOverrideDbfAsset != null)
		{
			for (int i = 0; i < cardPlayerDeckOverrideDbfAsset.Records.Count; i++)
			{
				cardPlayerDeckOverrideDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardPlayerDeckOverrideDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x00087F4E File Offset: 0x0008614E
	public LoadCardPlayerDeckOverrideDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardPlayerDeckOverrideDbfAsset));
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x00087F71 File Offset: 0x00086171
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F8A RID: 3978
	private AssetBundleRequest assetBundleRequest;
}
