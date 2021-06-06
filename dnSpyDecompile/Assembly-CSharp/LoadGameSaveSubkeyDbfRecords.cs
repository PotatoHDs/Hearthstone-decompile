using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class LoadGameSaveSubkeyDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001BB4 RID: 7092 RVA: 0x00090EF8 File Offset: 0x0008F0F8
	public List<GameSaveSubkeyDbfRecord> GetRecords()
	{
		GameSaveSubkeyDbfAsset gameSaveSubkeyDbfAsset = this.assetBundleRequest.asset as GameSaveSubkeyDbfAsset;
		if (gameSaveSubkeyDbfAsset != null)
		{
			for (int i = 0; i < gameSaveSubkeyDbfAsset.Records.Count; i++)
			{
				gameSaveSubkeyDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameSaveSubkeyDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001BB5 RID: 7093 RVA: 0x00090F4E File Offset: 0x0008F14E
	public LoadGameSaveSubkeyDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameSaveSubkeyDbfAsset));
	}

	// Token: 0x06001BB6 RID: 7094 RVA: 0x00090F71 File Offset: 0x0008F171
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010B9 RID: 4281
	private AssetBundleRequest assetBundleRequest;
}
