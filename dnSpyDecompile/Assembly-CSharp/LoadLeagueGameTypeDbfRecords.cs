using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class LoadLeagueGameTypeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C7B RID: 7291 RVA: 0x000939B0 File Offset: 0x00091BB0
	public List<LeagueGameTypeDbfRecord> GetRecords()
	{
		LeagueGameTypeDbfAsset leagueGameTypeDbfAsset = this.assetBundleRequest.asset as LeagueGameTypeDbfAsset;
		if (leagueGameTypeDbfAsset != null)
		{
			for (int i = 0; i < leagueGameTypeDbfAsset.Records.Count; i++)
			{
				leagueGameTypeDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueGameTypeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001C7C RID: 7292 RVA: 0x00093A06 File Offset: 0x00091C06
	public LoadLeagueGameTypeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueGameTypeDbfAsset));
	}

	// Token: 0x06001C7D RID: 7293 RVA: 0x00093A29 File Offset: 0x00091C29
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010F8 RID: 4344
	private AssetBundleRequest assetBundleRequest;
}
