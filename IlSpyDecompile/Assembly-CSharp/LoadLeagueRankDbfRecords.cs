using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLeagueRankDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LeagueRankDbfRecord> GetRecords()
	{
		LeagueRankDbfAsset leagueRankDbfAsset = assetBundleRequest.asset as LeagueRankDbfAsset;
		if (leagueRankDbfAsset != null)
		{
			for (int i = 0; i < leagueRankDbfAsset.Records.Count; i++)
			{
				leagueRankDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueRankDbfAsset.Records;
		}
		return null;
	}

	public LoadLeagueRankDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueRankDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
