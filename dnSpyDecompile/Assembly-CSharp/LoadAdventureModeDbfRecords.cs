using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class LoadAdventureModeDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060017ED RID: 6125 RVA: 0x00083AF0 File Offset: 0x00081CF0
	public List<AdventureModeDbfRecord> GetRecords()
	{
		AdventureModeDbfAsset adventureModeDbfAsset = this.assetBundleRequest.asset as AdventureModeDbfAsset;
		if (adventureModeDbfAsset != null)
		{
			for (int i = 0; i < adventureModeDbfAsset.Records.Count; i++)
			{
				adventureModeDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureModeDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060017EE RID: 6126 RVA: 0x00083B46 File Offset: 0x00081D46
	public LoadAdventureModeDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureModeDbfAsset));
	}

	// Token: 0x060017EF RID: 6127 RVA: 0x00083B69 File Offset: 0x00081D69
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F21 RID: 3873
	private AssetBundleRequest assetBundleRequest;
}
