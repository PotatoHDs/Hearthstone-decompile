using System.Collections.Generic;
using Blizzard.T5.Jobs;
using UnityEngine;

public class LoadQuestDialogOnReceivedDbfRecords : IJobDependency, IAsyncJobResult
{
	private AssetBundleRequest assetBundleRequest;

	public List<QuestDialogOnReceivedDbfRecord> GetRecords()
	{
		QuestDialogOnReceivedDbfAsset questDialogOnReceivedDbfAsset = assetBundleRequest.asset as QuestDialogOnReceivedDbfAsset;
		if (questDialogOnReceivedDbfAsset != null)
		{
			for (int i = 0; i < questDialogOnReceivedDbfAsset.Records.Count; i++)
			{
				questDialogOnReceivedDbfAsset.Records[i].StripUnusedLocales();
			}
			return questDialogOnReceivedDbfAsset.Records;
		}
		return null;
	}

	public LoadQuestDialogOnReceivedDbfRecords(string resourcePath)
	{
		assetBundleRequest = DbfShared.GetAssetBundle().LoadAssetAsync(resourcePath, typeof(QuestDialogOnReceivedDbfAsset));
	}

	public bool IsReady()
	{
		return assetBundleRequest.isDone;
	}
}
