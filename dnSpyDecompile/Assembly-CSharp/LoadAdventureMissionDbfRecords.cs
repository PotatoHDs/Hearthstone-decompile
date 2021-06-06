using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class LoadAdventureMissionDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060017CA RID: 6090 RVA: 0x000832E4 File Offset: 0x000814E4
	public List<AdventureMissionDbfRecord> GetRecords()
	{
		AdventureMissionDbfAsset adventureMissionDbfAsset = this.assetBundleRequest.asset as AdventureMissionDbfAsset;
		if (adventureMissionDbfAsset != null)
		{
			for (int i = 0; i < adventureMissionDbfAsset.Records.Count; i++)
			{
				adventureMissionDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureMissionDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x0008333A File Offset: 0x0008153A
	public LoadAdventureMissionDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureMissionDbfAsset));
	}

	// Token: 0x060017CC RID: 6092 RVA: 0x0008335D File Offset: 0x0008155D
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F15 RID: 3861
	private AssetBundleRequest assetBundleRequest;
}
