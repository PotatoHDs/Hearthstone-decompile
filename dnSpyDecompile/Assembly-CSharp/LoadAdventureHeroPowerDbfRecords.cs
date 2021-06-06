using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000173 RID: 371
public class LoadAdventureHeroPowerDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001778 RID: 6008 RVA: 0x00081F2C File Offset: 0x0008012C
	public List<AdventureHeroPowerDbfRecord> GetRecords()
	{
		AdventureHeroPowerDbfAsset adventureHeroPowerDbfAsset = this.assetBundleRequest.asset as AdventureHeroPowerDbfAsset;
		if (adventureHeroPowerDbfAsset != null)
		{
			for (int i = 0; i < adventureHeroPowerDbfAsset.Records.Count; i++)
			{
				adventureHeroPowerDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureHeroPowerDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x00081F82 File Offset: 0x00080182
	public LoadAdventureHeroPowerDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureHeroPowerDbfAsset));
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x00081FA5 File Offset: 0x000801A5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000EF9 RID: 3833
	private AssetBundleRequest assetBundleRequest;
}
