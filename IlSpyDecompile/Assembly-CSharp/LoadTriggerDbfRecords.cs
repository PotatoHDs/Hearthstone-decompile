using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadTriggerDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<TriggerDbfRecord> GetRecords()
	{
		TriggerDbfAsset triggerDbfAsset = assetBundleRequest.asset as TriggerDbfAsset;
		if (triggerDbfAsset != null)
		{
			for (int i = 0; i < triggerDbfAsset.Records.Count; i++)
			{
				triggerDbfAsset.Records[i].StripUnusedLocales();
			}
			return triggerDbfAsset.Records;
		}
		return null;
	}

	public LoadTriggerDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(TriggerDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
