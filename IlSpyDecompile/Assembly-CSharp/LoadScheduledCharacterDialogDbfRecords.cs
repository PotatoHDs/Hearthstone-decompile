using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadScheduledCharacterDialogDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<ScheduledCharacterDialogDbfRecord> GetRecords()
	{
		ScheduledCharacterDialogDbfAsset scheduledCharacterDialogDbfAsset = assetBundleRequest.asset as ScheduledCharacterDialogDbfAsset;
		if (scheduledCharacterDialogDbfAsset != null)
		{
			for (int i = 0; i < scheduledCharacterDialogDbfAsset.Records.Count; i++)
			{
				scheduledCharacterDialogDbfAsset.Records[i].StripUnusedLocales();
			}
			return scheduledCharacterDialogDbfAsset.Records;
		}
		return null;
	}

	public LoadScheduledCharacterDialogDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(ScheduledCharacterDialogDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
