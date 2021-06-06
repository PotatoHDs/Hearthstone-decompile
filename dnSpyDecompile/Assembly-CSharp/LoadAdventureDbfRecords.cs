using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class LoadAdventureDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x060016DA RID: 5850 RVA: 0x0007F5BC File Offset: 0x0007D7BC
	public List<AdventureDbfRecord> GetRecords()
	{
		AdventureDbfAsset adventureDbfAsset = this.assetBundleRequest.asset as AdventureDbfAsset;
		if (adventureDbfAsset != null)
		{
			for (int i = 0; i < adventureDbfAsset.Records.Count; i++)
			{
				adventureDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x0007F612 File Offset: 0x0007D812
	public LoadAdventureDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDbfAsset));
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x0007F635 File Offset: 0x0007D835
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000EBF RID: 3775
	private AssetBundleRequest assetBundleRequest;
}
