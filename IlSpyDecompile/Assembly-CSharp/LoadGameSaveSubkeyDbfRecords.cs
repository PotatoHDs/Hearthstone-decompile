using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadGameSaveSubkeyDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<GameSaveSubkeyDbfRecord> GetRecords()
	{
		GameSaveSubkeyDbfAsset gameSaveSubkeyDbfAsset = assetBundleRequest.asset as GameSaveSubkeyDbfAsset;
		if (gameSaveSubkeyDbfAsset != null)
		{
			for (int i = 0; i < gameSaveSubkeyDbfAsset.Records.Count; i++)
			{
				gameSaveSubkeyDbfAsset.Records[i].StripUnusedLocales();
			}
			return gameSaveSubkeyDbfAsset.Records;
		}
		return null;
	}

	public LoadGameSaveSubkeyDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(GameSaveSubkeyDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
