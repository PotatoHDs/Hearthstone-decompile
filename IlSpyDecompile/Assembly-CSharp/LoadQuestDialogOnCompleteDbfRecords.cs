using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDialogOnCompleteDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDialogOnCompleteDbfRecord> GetRecords()
	{
		QuestDialogOnCompleteDbfAsset questDialogOnCompleteDbfAsset = assetBundleRequest.asset as QuestDialogOnCompleteDbfAsset;
		if (questDialogOnCompleteDbfAsset != null)
		{
			for (int i = 0; i < questDialogOnCompleteDbfAsset.Records.Count; i++)
			{
				questDialogOnCompleteDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnCompleteDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDialogOnCompleteDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnCompleteDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
