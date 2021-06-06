using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000285 RID: 645
public class LoadTavernBrawlTicketDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060020EF RID: 8431 RVA: 0x000A2398 File Offset: 0x000A0598
	public List<TavernBrawlTicketDbfRecord> GetRecords()
	{
		TavernBrawlTicketDbfAsset tavernBrawlTicketDbfAsset = this.assetBundleRequest.asset as TavernBrawlTicketDbfAsset;
		if (tavernBrawlTicketDbfAsset != null)
		{
			for (int i = 0; i < tavernBrawlTicketDbfAsset.Records.Count; i++)
			{
				tavernBrawlTicketDbfAsset.Records[i].StripUnusedLocales();
			}
			return tavernBrawlTicketDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x000A23EE File Offset: 0x000A05EE
	public LoadTavernBrawlTicketDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TavernBrawlTicketDbfAsset));
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x000A2411 File Offset: 0x000A0611
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001260 RID: 4704
	private AssetBundleRequest assetBundleRequest;
}
