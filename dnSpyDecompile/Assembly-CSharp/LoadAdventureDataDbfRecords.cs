using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000167 RID: 359
public class LoadAdventureDataDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001681 RID: 5761 RVA: 0x0007DAB8 File Offset: 0x0007BCB8
	public List<AdventureDataDbfRecord> GetRecords()
	{
		AdventureDataDbfAsset adventureDataDbfAsset = this.assetBundleRequest.asset as AdventureDataDbfAsset;
		if (adventureDataDbfAsset != null)
		{
			for (int i = 0; i < adventureDataDbfAsset.Records.Count; i++)
			{
				adventureDataDbfAsset.Records[i].StripUnusedLocales();
			}
			return adventureDataDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001682 RID: 5762 RVA: 0x0007DB0E File Offset: 0x0007BD0E
	public LoadAdventureDataDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(AdventureDataDbfAsset));
	}

	// Token: 0x06001683 RID: 5763 RVA: 0x0007DB31 File Offset: 0x0007BD31
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04000E98 RID: 3736
	private AssetBundleRequest assetBundleRequest;
}
