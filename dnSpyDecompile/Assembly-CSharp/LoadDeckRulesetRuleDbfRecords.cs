using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class LoadDeckRulesetRuleDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A8C RID: 6796 RVA: 0x0008BCFC File Offset: 0x00089EFC
	public List<DeckRulesetRuleDbfRecord> GetRecords()
	{
		DeckRulesetRuleDbfAsset deckRulesetRuleDbfAsset = this.assetBundleRequest.asset as DeckRulesetRuleDbfAsset;
		if (deckRulesetRuleDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetRuleDbfAsset.Records.Count; i++)
			{
				deckRulesetRuleDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetRuleDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001A8D RID: 6797 RVA: 0x0008BD52 File Offset: 0x00089F52
	public LoadDeckRulesetRuleDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetRuleDbfAsset));
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x0008BD75 File Offset: 0x00089F75
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FF1 RID: 4081
	private AssetBundleRequest assetBundleRequest;
}
