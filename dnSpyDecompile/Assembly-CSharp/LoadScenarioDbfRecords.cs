using System;
using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

// Token: 0x02000264 RID: 612
public class LoadScenarioDbfRecords : IJobDependency, IAsyncJobResult
{
	// Token: 0x06001FCA RID: 8138 RVA: 0x0009E92C File Offset: 0x0009CB2C
	public List<ScenarioDbfRecord> GetRecords()
	{
		ScenarioDbfAsset scenarioDbfAsset = this.assetBundleRequest.asset as ScenarioDbfAsset;
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

	// Token: 0x06001FCB RID: 8139 RVA: 0x0009E982 File Offset: 0x0009CB82
	public LoadScenarioDbfRecords(string resourcePath)
	{
		this.assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScenarioDbfAsset));
	}

	// Token: 0x06001FCC RID: 8140 RVA: 0x0009E9A5 File Offset: 0x0009CBA5
	public bool IsReady()
	{
		return this.assetBundleRequest.isDone;
	}

	// Token: 0x04001206 RID: 4614
	private AssetBundleRequest assetBundleRequest;
}
