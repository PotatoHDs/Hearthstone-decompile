using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadXpPerGameTypeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<XpPerGameTypeDbfRecord> GetRecords()
	{
		XpPerGameTypeDbfAsset xpPerGameTypeDbfAsset = assetBundleRequest.asset as XpPerGameTypeDbfAsset;
		if (xpPerGameTypeDbfAsset != null)
		{
			for (int i = 0; i < xpPerGameTypeDbfAsset.Records.Count; i++)
			{
				xpPerGameTypeDbfAsset.Records[i].StripUnusedLocales();
			}
			return xpPerGameTypeDbfAsset.Records;
		}
		return null;
	}

	public LoadXpPerGameTypeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(XpPerGameTypeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
