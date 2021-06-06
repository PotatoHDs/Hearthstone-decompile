using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000201 RID: 513
public class LoadLeagueDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C43 RID: 7235 RVA: 0x00092AA4 File Offset: 0x00090CA4
	public List<LeagueDbfRecord> GetRecords()
	{
		LeagueDbfAsset leagueDbfAsset = this.assetBundleRequest.asset as LeagueDbfAsset;
		if (leagueDbfAsset != null)
		{
			for (int i = 0; i < leagueDbfAsset.Records.Count; i++)
			{
				leagueDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001C44 RID: 7236 RVA: 0x00092AFA File Offset: 0x00090CFA
	public LoadLeagueDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueDbfAsset));
	}

	// Token: 0x06001C45 RID: 7237 RVA: 0x00092B1D File Offset: 0x00090D1D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010E3 RID: 4323
	private AssetBundleRequest assetBundleRequest;
}
