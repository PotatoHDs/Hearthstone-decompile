using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGuestHeroDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GuestHeroDbfRecord> GetRecords()
	{
		GuestHeroDbfAsset guestHeroDbfAsset = assetBundleRequest.asset as GuestHeroDbfAsset;
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

	public LoadGuestHeroDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GuestHeroDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
