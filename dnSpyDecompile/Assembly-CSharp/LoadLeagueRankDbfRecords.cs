using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class LoadLeagueRankDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C8D RID: 7309 RVA: 0x00093CDC File Offset: 0x00091EDC
	public List<LeagueRankDbfRecord> GetRecords()
	{
		LeagueRankDbfAsset leagueRankDbfAsset = this.assetBundleRequest.asset as LeagueRankDbfAsset;
		if (leagueRankDbfAsset != null)
		{
			for (int i = 0; i < leagueRankDbfAsset.Records.Count; i++)
			{
				leagueRankDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueRankDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x00093D32 File Offset: 0x00091F32
	public LoadLeagueRankDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueRankDbfAsset));
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x00093D55 File Offset: 0x00091F55
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010FD RID: 4349
	private AssetBundleRequest assetBundleRequest;
}
