using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGameTypeRankedDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GameTypeRankedDbfRecord> GetRecords()
	{
		GameTypeRankedDbfAsset gameTypeRankedDbfAsset = assetBundleRequest.asset as GameTypeRankedDbfAsset;
		if (gameTypeRankedDbfAsset != null)
		{
			for (int i = 0; i < gameTypeRankedDbfAsset.Records.Count; i++)
			{
				gameTypeRankedDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameTypeRankedDbfAsset.Records;
		}
		return null;
	}

	public LoadGameTypeRankedDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameTypeRankedDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
