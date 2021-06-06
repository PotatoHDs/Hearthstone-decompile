using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class LoadDeckRulesetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001A7C RID: 6780 RVA: 0x0008BA9C File Offset: 0x00089C9C
	public List<DeckRulesetDbfRecord> GetRecords()
	{
		DeckRulesetDbfAsset deckRulesetDbfAsset = this.assetBundleRequest.asset as DeckRulesetDbfAsset;
		if (deckRulesetDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetDbfAsset.Records.Count; i++)
			{
				deckRulesetDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001A7D RID: 6781 RVA: 0x0008BAF2 File Offset: 0x00089CF2
	public LoadDeckRulesetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetDbfAsset));
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x0008BB15 File Offset: 0x00089D15
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FEE RID: 4078
	private AssetBundleRequest assetBundleRequest;
}
