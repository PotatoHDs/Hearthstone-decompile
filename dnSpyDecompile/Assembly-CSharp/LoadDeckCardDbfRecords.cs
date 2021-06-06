using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class LoadDeckCardDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A4B RID: 6731 RVA: 0x0008B1B0 File Offset: 0x000893B0
	public List<DeckCardDbfRecord> GetRecords()
	{
		DeckCardDbfAsset deckCardDbfAsset = this.assetBundleRequest.asset as DeckCardDbfAsset;
		if (deckCardDbfAsset != null)
		{
			for (int i = 0; i < deckCardDbfAsset.Records.Count; i++)
			{
				deckCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckCardDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x0008B206 File Offset: 0x00089406
	public LoadDeckCardDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckCardDbfAsset));
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x0008B229 File Offset: 0x00089429
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FE0 RID: 4064
	private AssetBundleRequest assetBundleRequest;
}
