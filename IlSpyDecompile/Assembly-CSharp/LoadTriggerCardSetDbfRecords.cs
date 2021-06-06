using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadTriggerCardSetDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<TriggerCardSetDbfRecord> GetRecords()
	{
		TriggerCardSetDbfAsset triggerCardSetDbfAsset = assetBundleRequest.asset as TriggerCardSetDbfAsset;
		if (triggerCardSetDbfAsset != null)
		{
			for (int i = 0; i < triggerCardSetDbfAsset.Records.Count; i++)
			{
				triggerCardSetDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerCardSetDbfAsset.Records;
		}
		return null;
	}

	public LoadTriggerCardSetDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerCardSetDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
