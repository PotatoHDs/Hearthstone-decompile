using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class LoadDeckDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A61 RID: 6753 RVA: 0x0008B508 File Offset: 0x00089708
	public List<DeckDbfRecord> GetRecords()
	{
		DeckDbfAsset deckDbfAsset = this.assetBundleRequest.asset as DeckDbfAsset;
		if (deckDbfAsset != null)
		{
			for (int i = 0; i < deckDbfAsset.Records.Count; i++)
			{
				deckDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001A62 RID: 6754 RVA: 0x0008B55E File Offset: 0x0008975E
	public LoadDeckDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckDbfAsset));
	}

	// Token: 0x06001A63 RID: 6755 RVA: 0x0008B581 File Offset: 0x00089781
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FE6 RID: 4070
	private AssetBundleRequest assetBundleRequest;
}
