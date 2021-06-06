using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class LoadDeckRulesetRuleSubsetDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001AB5 RID: 6837 RVA: 0x0008C780 File Offset: 0x0008A980
	public List<DeckRulesetRuleSubsetDbfRecord> GetRecords()
	{
		DeckRulesetRuleSubsetDbfAsset deckRulesetRuleSubsetDbfAsset = this.assetBundleRequest.asset as DeckRulesetRuleSubsetDbfAsset;
		if (deckRulesetRuleSubsetDbfAsset != null)
		{
			for (int i = 0; i < deckRulesetRuleSubsetDbfAsset.Records.Count; i++)
			{
				deckRulesetRuleSubsetDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckRulesetRuleSubsetDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001AB6 RID: 6838 RVA: 0x0008C7D6 File Offset: 0x0008A9D6
	public LoadDeckRulesetRuleSubsetDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckRulesetRuleSubsetDbfAsset));
	}

	// Token: 0x06001AB7 RID: 6839 RVA: 0x0008C7F9 File Offset: 0x0008A9F9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001000 RID: 4096
	private AssetBundleRequest assetBundleRequest;
}
