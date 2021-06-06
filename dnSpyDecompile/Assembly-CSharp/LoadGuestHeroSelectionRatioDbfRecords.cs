using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class LoadGuestHeroSelectionRatioDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001C05 RID: 7173 RVA: 0x00091FF0 File Offset: 0x000901F0
	public List<GuestHeroSelectionRatioDbfRecord> GetRecords()
	{
		GuestHeroSelectionRatioDbfAsset guestHeroSelectionRatioDbfAsset = this.assetBundleRequest.asset as GuestHeroSelectionRatioDbfAsset;
		if (guestHeroSelectionRatioDbfAsset != null)
		{
			for (int i = 0; i < guestHeroSelectionRatioDbfAsset.Records.Count; i++)
			{
				guestHeroSelectionRatioDbfAsset.Records[i].StripUnusedLocales();
			}
			return guestHeroSelectionRatioDbfAsset.Records;
		}
		return null;
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x00092046 File Offset: 0x00090246
	public LoadGuestHeroSelectionRatioDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GuestHeroSelectionRatioDbfAsset));
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00092069 File Offset: 0x00090269
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x040010D1 RID: 4305
	private AssetBundleRequest assetBundleRequest;
}
