using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadTriggerCardDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<TriggerCardDbfRecord> GetRecords()
	{
		TriggerCardDbfAsset triggerCardDbfAsset = assetBundleRequest.asset as TriggerCardDbfAsset;
		if (triggerCardDbfAsset != null)
		{
			for (int i = 0; i < triggerCardDbfAsset.Records.Count; i++)
			{
				triggerCardDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerCardDbfAsset.Records;
		}
		return null;
	}

	public LoadTriggerCardDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerCardDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
