using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class LoadBattlegroundsSeasonDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600180F RID: 6159 RVA: 0x00084014 File Offset: 0x00082214
	public List<BattlegroundsSeasonDbfRecord> GetRecords()
	{
		BattlegroundsSeasonDbfAsset battlegroundsSeasonDbfAsset = this.assetBundleRequest.asset as BattlegroundsSeasonDbfAsset;
		if (battlegroundsSeasonDbfAsset != null)
		{
			for (int i = 0; i < battlegroundsSeasonDbfAsset.Records.Count; i++)
			{
				battlegroundsSeasonDbfAsset.Records[i].StripUnusedLocales();
			}
			return battlegroundsSeasonDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001810 RID: 6160 RVA: 0x0008406A File Offset: 0x0008226A
	public LoadBattlegroundsSeasonDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BattlegroundsSeasonDbfAsset));
	}

	// Token: 0x06001811 RID: 6161 RVA: 0x0008408D File Offset: 0x0008228D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F2A RID: 3882
	private AssetBundleRequest assetBundleRequest;
}
