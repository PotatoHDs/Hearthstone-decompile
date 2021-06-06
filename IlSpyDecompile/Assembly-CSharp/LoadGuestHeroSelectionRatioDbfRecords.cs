using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGuestHeroSelectionRatioDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GuestHeroSelectionRatioDbfRecord> GetRecords()
	{
		GuestHeroSelectionRatioDbfAsset guestHeroSelectionRatioDbfAsset = assetBundleRequest.asset as GuestHeroSelectionRatioDbfAsset;
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

	public LoadGuestHeroSelectionRatioDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GuestHeroSelectionRatioDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
