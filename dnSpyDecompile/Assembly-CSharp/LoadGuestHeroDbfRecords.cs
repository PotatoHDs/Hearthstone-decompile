using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F5 RID: 501
public class LoadGuestHeroDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001BF0 RID: 7152 RVA: 0x00091C80 File Offset: 0x0008FE80
	public List<GuestHeroDbfRecord> GetRecords()
	{
		GuestHeroDbfAsset guestHeroDbfAsset = this.assetBundleRequest.asset as GuestHeroDbfAsset;
		if (guestHeroDbfAsset != null)
		{
			for (int i = 0; i < guestHeroDbfAsset.Records.Count; i++)
			{
				guestHeroDbfAsset.Records[i].StripUnusedLocales();
			}
			return guestHeroDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001BF1 RID: 7153 RVA: 0x00091CD6 File Offset: 0x0008FED6
	public LoadGuestHeroDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GuestHeroDbfAsset));
	}

	// Token: 0x06001BF2 RID: 7154 RVA: 0x00091CF9 File Offset: 0x0008FEF9
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010CB RID: 4299
	private AssetBundleRequest assetBundleRequest;
}
