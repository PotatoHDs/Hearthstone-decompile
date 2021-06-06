using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000170 RID: 368
public class LoadAdventureGuestHeroesDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x0600175B RID: 5979 RVA: 0x000818FC File Offset: 0x0007FAFC
	public List<AdventureGuestHeroesDbfRecord> GetRecords()
	{
		AdventureGuestHeroesDbfAsset adventureGuestHeroesDbfAsset = this.assetBundleRequest.asset as AdventureGuestHeroesDbfAsset;
		if (adventureGuestHeroesDbfAsset != null)
		{
			for (int i = 0; i < adventureGuestHeroesDbfAsset.Records.Count; i++)
			{
				adventureGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureGuestHeroesDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x0600175C RID: 5980 RVA: 0x00081952 File Offset: 0x0007FB52
	public LoadAdventureGuestHeroesDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureGuestHeroesDbfAsset));
	}

	// Token: 0x0600175D RID: 5981 RVA: 0x00081975 File Offset: 0x0007FB75
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000EF0 RID: 3824
	private AssetBundleRequest assetBundleRequest;
}
