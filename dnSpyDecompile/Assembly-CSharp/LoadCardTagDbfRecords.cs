using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001AC RID: 428
public class LoadCardTagDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001990 RID: 6544 RVA: 0x00089164 File Offset: 0x00087364
	public List<CardTagDbfRecord> GetRecords()
	{
		CardTagDbfAsset cardTagDbfAsset = this.assetBundleRequest.asset as CardTagDbfAsset;
		if (cardTagDbfAsset != null)
		{
			for (int i = 0; i < cardTagDbfAsset.Records.Count; i++)
			{
				cardTagDbfAsset.Records[i].StripUnusedLocales();
			}
			return cardTagDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001991 RID: 6545 RVA: 0x000891BA File Offset: 0x000873BA
	public LoadCardTagDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(CardTagDbfAsset));
	}

	// Token: 0x06001992 RID: 6546 RVA: 0x000891DD File Offset: 0x000873DD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000FA9 RID: 4009
	private AssetBundleRequest assetBundleRequest;
}
