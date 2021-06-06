using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class LoadAdventureLoadoutTreasuresDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600179C RID: 6044 RVA: 0x00082784 File Offset: 0x00080984
	public List<AdventureLoadoutTreasuresDbfRecord> GetRecords()
	{
		AdventureLoadoutTreasuresDbfAsset adventureLoadoutTreasuresDbfAsset = this.assetBundleRequest.asset as AdventureLoadoutTreasuresDbfAsset;
		if (adventureLoadoutTreasuresDbfAsset != null)
		{
			for (int i = 0; i < adventureLoadoutTreasuresDbfAsset.Records.Count; i++)
			{
				adventureLoadoutTreasuresDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureLoadoutTreasuresDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000827DA File Offset: 0x000809DA
	public LoadAdventureLoadoutTreasuresDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureLoadoutTreasuresDbfAsset));
	}

	// Token: 0x0600179E RID: 6046 RVA: 0x000827FD File Offset: 0x000809FD
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000F05 RID: 3845
	private AssetBundleRequest assetBundleRequest;
}
