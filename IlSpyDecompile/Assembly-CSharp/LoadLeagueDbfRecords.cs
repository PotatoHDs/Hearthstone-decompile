using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadLeagueDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<LeagueDbfRecord> GetRecords()
	{
		LeagueDbfAsset leagueDbfAsset = assetBundleRequest.asset as LeagueDbfAsset;
		if (leagueDbfAsset != null)
		{
			for (int i = 0; i < leagueDbfAsset.Records.Count; i++)
			{
				leagueDbfAsset.Records[i].StripUnusedLocales();
			}
			return leagueDbfAsset.Records;
		}
		return null;
	}

	public LoadLeagueDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(LeagueDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
