using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLoginPopupSequencePopupDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LoginPopupSequencePopupDbfRecord> GetRecords()
	{
		LoginPopupSequencePopupDbfAsset loginPopupSequencePopupDbfAsset = assetBundleRequest.asset as LoginPopupSequencePopupDbfAsset;
		if (loginPopupSequencePopupDbfAsset != null)
		{
			for (int i = 0; i < loginPopupSequencePopupDbfAsset.Records.Count; i++)
			{
				loginPopupSequencePopupDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginPopupSequencePopupDbfAsset.Records;
		}
		return null;
	}

	public LoadLoginPopupSequencePopupDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginPopupSequencePopupDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
