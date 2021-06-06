using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001FE RID: 510
public class LoadKeywordTextDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C2B RID: 7211 RVA: 0x000925A4 File Offset: 0x000907A4
	public List<KeywordTextDbfRecord> GetRecords()
	{
		KeywordTextDbfAsset keywordTextDbfAsset = this.assetBundleRequest.asset as KeywordTextDbfAsset;
		if (keywordTextDbfAsset != null)
		{
			for (int i = 0; i < keywordTextDbfAsset.Records.Count; i++)
			{
				keywordTextDbfAsset.Records[i].StripUnusedLocales();
			}
			return keywordTextDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x000925FA File Offset: 0x000907FA
	public LoadKeywordTextDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(KeywordTextDbfAsset));
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x0009261D File Offset: 0x0009081D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010DB RID: 4315
	private AssetBundleRequest assetBundleRequest;
}
