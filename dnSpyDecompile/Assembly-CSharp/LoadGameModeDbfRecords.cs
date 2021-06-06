using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001E9 RID: 489
public class LoadGameModeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001B90 RID: 7056 RVA: 0x000905C4 File Offset: 0x0008E7C4
	public List<GameModeDbfRecord> GetRecords()
	{
		GameModeDbfAsset gameModeDbfAsset = this.assetBundleRequest.asset as GameModeDbfAsset;
		if (gameModeDbfAsset != null)
		{
			for (int i = 0; i < gameModeDbfAsset.Records.Count; i++)
			{
				gameModeDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameModeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001B91 RID: 7057 RVA: 0x0009061A File Offset: 0x0008E81A
	public LoadGameModeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameModeDbfAsset));
	}

	// Token: 0x06001B92 RID: 7058 RVA: 0x0009063D File Offset: 0x0008E83D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010AB RID: 4267
	private AssetBundleRequest assetBundleRequest;
}
