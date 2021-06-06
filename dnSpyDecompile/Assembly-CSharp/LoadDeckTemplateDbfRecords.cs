using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class LoadDeckTemplateDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001AC6 RID: 6854 RVA: 0x0008C988 File Offset: 0x0008AB88
	public List<DeckTemplateDbfRecord> GetRecords()
	{
		DeckTemplateDbfAsset deckTemplateDbfAsset = this.assetBundleRequest.asset as DeckTemplateDbfAsset;
		if (deckTemplateDbfAsset != null)
		{
			for (int i = 0; i < deckTemplateDbfAsset.Records.Count; i++)
			{
				deckTemplateDbfAsset.Records[i].StripUnusedLocales();
			}
			return deckTemplateDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001AC7 RID: 6855 RVA: 0x0008C9DE File Offset: 0x0008ABDE
	public LoadDeckTemplateDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DeckTemplateDbfAsset));
	}

	// Token: 0x06001AC8 RID: 6856 RVA: 0x0008CA01 File Offset: 0x0008AC01
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001004 RID: 4100
	private AssetBundleRequest assetBundleRequest;
}
