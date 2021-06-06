using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLeagueGameTypeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LeagueGameTypeDbfRecord> GetRecords()
	{
		LeagueGameTypeDbfAsset leagueGameTypeDbfAsset = assetBundleRequest.asset as LeagueGameTypeDbfAsset;
		if (leagueGameTypeDbfAsset != null)
		{
			for (int i = 0; i < leagueGameTypeDbfAsset.Records.Count; i++)
			{
				leagueGameTypeDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueGameTypeDbfAsset.Records;
		}
		return null;
	}

	public LoadLeagueGameTypeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueGameTypeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
