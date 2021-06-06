using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadBattlegroundsSeasonDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<BattlegroundsSeasonDbfRecord> GetRecords()
	{
		BattlegroundsSeasonDbfAsset battlegroundsSeasonDbfAsset = assetBundleRequest.asset as BattlegroundsSeasonDbfAsset;
		if (battlegroundsSeasonDbfAsset != null)
		{
			for (int i = 0; i < battlegroundsSeasonDbfAsset.Records.Count; i++)
			{
				battlegroundsSeasonDbfAsset.Records[i].StripUnusedLocales();
			}
			return battlegroundsSeasonDbfAsset.Records;
		}
		return null;
	}

	public LoadBattlegroundsSeasonDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(BattlegroundsSeasonDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
