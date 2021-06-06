using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadScenarioGuestHeroesDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ScenarioGuestHeroesDbfRecord> GetRecords()
	{
		ScenarioGuestHeroesDbfAsset scenarioGuestHeroesDbfAsset = assetBundleRequest.asset as ScenarioGuestHeroesDbfAsset;
		if (scenarioGuestHeroesDbfAsset != null)
		{
			for (int i = 0; i < scenarioGuestHeroesDbfAsset.Records.Count; i++)
			{
				scenarioGuestHeroesDbfAsset.Records[i].StripUnusedLocales();
			}
			return scenarioGuestHeroesDbfAsset.Records;
		}
		return null;
	}

	public LoadScenarioGuestHeroesDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScenarioGuestHeroesDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
