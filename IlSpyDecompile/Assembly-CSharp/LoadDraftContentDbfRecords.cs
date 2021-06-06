using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadDraftContentDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<DraftContentDbfRecord> GetRecords()
	{
		DraftContentDbfAsset draftContentDbfAsset = assetBundleRequest.asset as DraftContentDbfAsset;
		if (draftContentDbfAsset != null)
		{
			for (int i = 0; i < draftContentDbfAsset.Records.Count; i++)
			{
				draftContentDbfAsset.Records[i].StripUnusedLocales();
			}
			return draftContentDbfAsset.Records;
		}
		return null;
	}

	public LoadDraftContentDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(DraftContentDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
