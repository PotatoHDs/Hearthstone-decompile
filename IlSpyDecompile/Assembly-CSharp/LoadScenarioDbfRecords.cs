using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadScenarioDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ScenarioDbfRecord> GetRecords()
	{
		ScenarioDbfAsset scenarioDbfAsset = assetBundleRequest.asset as ScenarioDbfAsset;
		if (scenarioDbfAsset != null)
		{
			for (int i = 0; i < scenarioDbfAsset.Records.Count; i++)
			{
				scenarioDbfAsset.Records[i].StripUnusedLocales();
			}
			return scenarioDbfAsset.Records;
		}
		return null;
	}

	public LoadScenarioDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScenarioDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
