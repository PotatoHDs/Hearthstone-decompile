using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLoginPopupSequenceDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LoginPopupSequenceDbfRecord> GetRecords()
	{
		LoginPopupSequenceDbfAsset loginPopupSequenceDbfAsset = assetBundleRequest.asset as LoginPopupSequenceDbfAsset;
		if (loginPopupSequenceDbfAsset != null)
		{
			for (int i = 0; i < loginPopupSequenceDbfAsset.Records.Count; i++)
			{
				loginPopupSequenceDbfAsset.Records[i].StripUnusedLocales();
			}
			return loginPopupSequenceDbfAsset.Records;
		}
		return null;
	}

	public LoadLoginPopupSequenceDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LoginPopupSequenceDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
