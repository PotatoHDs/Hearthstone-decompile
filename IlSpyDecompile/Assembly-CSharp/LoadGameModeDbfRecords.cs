using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGameModeDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GameModeDbfRecord> GetRecords()
	{
		GameModeDbfAsset gameModeDbfAsset = assetBundleRequest.asset as GameModeDbfAsset;
		if (gameModeDbfAsset != null)
		{
			for (int i = 0; i < gameModeDbfAsset.Records.Count; i++)
			{
				gameModeDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameModeDbfAsset.Records;
		}
		return null;
	}

	public LoadGameModeDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameModeDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
