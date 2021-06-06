using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000267 RID: 615
public class LoadScenarioGuestHeroesDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06002022 RID: 8226 RVA: 0x000A00B0 File Offset: 0x0009E2B0
	public List<ScenarioGuestHeroesDbfRecord> GetRecords()
	{
		ScenarioGuestHeroesDbfAsset scenarioGuestHeroesDbfAsset = this.assetBundleRequest.asset as ScenarioGuestHeroesDbfAsset;
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

	// Token: 0x06002023 RID: 8227 RVA: 0x000A0106 File Offset: 0x0009E306
	public LoadScenarioGuestHeroesDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScenarioGuestHeroesDbfAsset));
	}

	// Token: 0x06002024 RID: 8228 RVA: 0x000A0129 File Offset: 0x0009E329
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001227 RID: 4647
	private AssetBundleRequest assetBundleRequest;
}
